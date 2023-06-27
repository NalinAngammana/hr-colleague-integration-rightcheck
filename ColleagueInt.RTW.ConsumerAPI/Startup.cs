using AutoMapper;
using ColleagueInt.RTW.ConsumerAPI.Configuration;
using ColleagueInt.RTW.ConsumerAPI.Data;
using ColleagueInt.RTW.ConsumerAPI.Data.Contracts;
using ColleagueInt.RTW.ConsumerAPI.Middleware;
using ColleagueInt.RTW.ConsumerAPI.Misc;
using ColleagueInt.RTW.Core;
using ColleagueInt.RTW.Core.Exceptions;
using ColleagueInt.RTW.Core.RestApiService;
using ColleagueInt.RTW.Core.RestApiService.Contracts;
using ColleagueInt.RTW.Core.ServiceNow.Contracts;
using ColleagueInt.RTW.Core.ServiceNow.Services;
using ColleagueInt.RTW.Database;
using ColleagueInt.RTW.Database.Constants;
using ColleagueInt.RTW.Repositories;
using ColleagueInt.RTW.Repositories.Contracts;
using ColleagueInt.RTW.Services;
using ColleagueInt.RTW.Services.Contracts;
using ColleagueInt.RTW.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace ColleagueInt.RTW.ConsumerAPI
{
    public class Startup
    {
        public IConfigurationRoot configRoot { get; set; }

        public Startup()
        {
            
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            try
            {
                configRoot = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();

                services.AddDbContext<RTWContext>(
                   options => options.UseSqlServer("name=ConnectionStrings:RTWConsumerContext"
                       , sqlServerOptions => sqlServerOptions.CommandTimeout(60))
                           .EnableSensitiveDataLogging(), ServiceLifetime.Transient);
                var now = DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss:ffff");
                if (CheckDatabaseConnection())
                {
                    Console.WriteLine($"{now}: Database connected");
                }
                else
                {
                    throw new DatabaseException("Error connecting database, please validate the connection settings in appSettings.json");
                }

                // Configure Key Vault
                var keyVaultUrl = "https://colleagueint-kv-test01.vault.azure.net/"; // configRoot.GetValue<string>("KeyVaultSettings:KeyVaultUrl");
                services.AddTransient<IKeyVaultService, KeyVaultService>(serviceProvider => new KeyVaultService(keyVaultUrl));

                //Settings
                services.Configure<KeyVaultSettings>(configRoot.GetSection("KeyVaultSettings"));
                services.Configure<RTWSettings>(configRoot.GetSection("RTWSettings"));

                // Configure Service Now.
                ConfigureServiceNow(services, configRoot);

                //// Auto Mapper Configurations
                var mapperConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });
                IMapper mapper = mapperConfig.CreateMapper();
                services.AddSingleton(mapper);
                services.AddTransient<IDataMapper, DataMapper>();

                // Repositories
                services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
                services.AddTransient<IColleagueRepository, ColleagueRepository>();
                services.AddTransient<IIncidentRepository, IncidentRepository>();

                // Services            
                services.AddTransient<IColleagueService, ColleagueService>();
                // Configure API Service for RTW
                InitialiseApiServices(services, configRoot);

                //Initialise middleware and controller classes
                services.AddControllers();
            }
            catch (Exception ex)
            {
                IncidentErrorDescription description = IncidentErrorDescription.NoIncident;

                if (ex is DatabaseException)
                    description = IncidentErrorDescription.AzureDatabaseError;
                else if (ex is KeyVaultException)
                    description = IncidentErrorDescription.AzureKeyVaultError;               
                else
                    description = IncidentErrorDescription.GenericException;
                RaiseIncident(services, configRoot, ex, description);
                throw new Exception(ex.Message);
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<WebhookAuthentication>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private bool CheckDatabaseConnection()
        {
            using RTWContext dbContext = new RTWContext();
            return dbContext.Database.CanConnect();
        }

        private static void InitialiseApiServices(IServiceCollection collection, IConfiguration configRoot)
        {
            try
            {
                var serviceProvider = collection.BuildServiceProvider();
                var keyVaultService = serviceProvider.GetService<IKeyVaultService>();
                if (keyVaultService == null)
                    return;

                var rtwApiSubscriptionKey = configRoot.GetValue<string>("KeyVaultSettings:RTWApiSubscriptionKey");
                var rtwApiSubscriptionKeyValue = keyVaultService.GetSecretValue(rtwApiSubscriptionKey).GetAwaiter().GetResult();
                var apiService = new AzureApiService(rtwApiSubscriptionKeyValue);
                collection.AddTransient<IAzureApiService, AzureApiService>(provider => apiService);
            }
            catch (Exception ex)
            {
                throw new KeyVaultException($"Exception while retrieving data from keyvault" + $"Message : {ex.Message}" + $"StackTrace : {ex.StackTrace}");
            }

        }
        private static void RaiseIncident(IServiceCollection services, IConfigurationRoot configRoot, Exception ex, IncidentErrorDescription errorCode)
        {
            try
            {
                ConfigureServiceNow(services, configRoot);
                var serviceProvider = services.BuildServiceProvider();

                var snowService = serviceProvider.GetRequiredService<ISnowIncidentService>();
                IncidentViewModel incidentViewModel = new IncidentViewModel();
                incidentViewModel.Description = ex.Message;
                incidentViewModel.ServiceNowDescription = "Azure RTW consumer application (Web API) failed to start";
                incidentViewModel.ConfigurationItem = "Azure Cloud Platform";
                incidentViewModel.UndefinedCaller = "HR Engineering";
                incidentViewModel.UndefinedLocation = "HR Engineering";
                incidentViewModel.Summary = errorCode.ToString();
                incidentViewModel.BusinessService = "Application Platform";
                //TO-DO -- add impact (priority) for incident.

                //All startup service incidents will be displayed only on console and 
                //entries will not be added into database as those services are not initialized.
                var incidentNumber = snowService.CreateIncidentAsync(incidentViewModel).GetAwaiter().GetResult();
                var now = DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss:ffff");
                Console.WriteLine($"{now}: Exception while starting the service. Details: {ex.Message}. Incident - {incidentNumber} raised.");
            }
            catch (Exception exc)
            {
                Console.WriteLine($"Exception while raise incident. Details: {exc.Message}");
            }
        }

        private static void ConfigureServiceNow(IServiceCollection services, IConfigurationRoot configRoot)
        {
            try
            {
                var allowServiceNowIntegration = configRoot.GetValue<bool>("GeneralSettings:AllowServiceNowIntegration");
                if (allowServiceNowIntegration)
                {
                    var serviceProvider = services.BuildServiceProvider();
                    var keyVaultService = serviceProvider.GetService<IKeyVaultService>();
                    if (keyVaultService == null)
                        return;

                    var serviceNowUrl = keyVaultService.GetSecretValue(
                            configRoot.GetValue<string>("KeyVaultSettings:ServiceNowIntegrationUrl")).GetAwaiter().GetResult();
                    services.AddTransient<ISnowIncidentService, SnowIncidentService>(provider => new SnowIncidentService(serviceNowUrl));
                }
                else
                {
                    services.AddTransient<ISnowIncidentService, FakeSnowIncidentService>(serviceProvider => new FakeSnowIncidentService(string.Empty));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception while configuring ServiceNow service." + $"Message : {ex.Message}" + $"StackTrace : {ex.StackTrace}");
            }
        }
    }
}
