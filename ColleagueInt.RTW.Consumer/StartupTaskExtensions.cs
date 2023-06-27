using AutoMapper;
using ColleagueInt.RTW.Consumer.Configuration;
using ColleagueInt.RTW.Consumer.Data;
using ColleagueInt.RTW.Consumer.Data.Contracts;
using ColleagueInt.RTW.Consumer.Misc;
using ColleagueInt.RTW.Consumer.Services.myHRDataReceiver;
using ColleagueInt.RTW.Consumer.Services.myHRDataReceiver.Contracts;
using ColleagueInt.RTW.Consumer.Services.Outbound;
using ColleagueInt.RTW.Core;
using ColleagueInt.RTW.Database;
using ColleagueInt.RTW.EventHandler.EventHandler;
using ColleagueInt.RTW.EventHandler.EventHandler.Contracts;
using ColleagueInt.RTW.Repositories;
using ColleagueInt.RTW.Repositories.Contracts;
using ColleagueInt.RTW.Services;
using ColleagueInt.RTW.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using ColleagueInt.RTW.Core.ServiceNow.Contracts;
using ColleagueInt.RTW.Core.ServiceNow.Services;
using ColleagueInt.RTW.Database.Constants;
using ColleagueInt.RTW.ViewModels;
using ColleagueInt.RTW.Consumer.Services.ErrorHandler;
using Microsoft.Extensions.Logging.EventLog;
using ColleagueInt.RTW.Consumer.Services.Inbound;
using ColleagueInt.RTW.Core.RestApiService;
using ColleagueInt.RTW.Core.RestApiService.Contracts;
using ColleagueInt.RTW.Core.Exceptions;
using ColleagueInt.RTW.Consumer.Services.IncidentManagement;
using ColleagueInt.RTW.Consumer.Services.Archive;
using ColleagueInt.RTW.Consumer.Services.HCMDataServices;
using ColleagueInt.RTW.Consumer.Services.HCMDataServices.Contracts;
using ColleagueInt.RTW.Consumer.Services.Inbound.Patch;

namespace ColleagueInt.RTW.Consumer
{
    public static class StartupTaskExtensions
    {
        
        public static IServiceProvider ServiceProvider { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public static void ConfigureServices(this IServiceCollection services, IConfigurationRoot configRoot)
        {
            try
            {
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
                // var keyVaultUrl = configRoot.GetValue<string>("KeyVaultSettings:KeyVaultUrl");
                var keyVaultUrl = "https://colleagueint-kv-test01.vault.azure.net/";
                services.AddTransient<IKeyVaultService, KeyVaultService>(serviceProvider => new KeyVaultService(keyVaultUrl));

                // Settings 
                services.Configure<GeneralSettings>(configRoot.GetSection("GeneralSettings"));
                services.Configure<HcmSettings>(configRoot.GetSection("HCMDataSection"));
                services.Configure<EventHubSettings>(configRoot.GetSection("EventHubGenericSettings"));
                services.Configure<KeyVaultSettings>(configRoot.GetSection("KeyVaultSettings"));
                services.Configure<RTWSettings>(configRoot.GetSection("RTWSettings"));
                services.Configure<EventLogSettings>(config =>
                {
                    config.LogName = "ColleagueInt_RTW";
                    config.SourceName = "ColleagueInt_RTW";
                });

                //initialise 
                services.AddTransient<IJwTokenService, JwTokenService>();

                // Configure API Service for RTW
                InitialiseApiServices(services, configRoot);

                // Configure Service Now.
                ConfigureServiceNow(services, configRoot);

                // Auto Mapper Configurations
                var mapperConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });
                IMapper mapper = mapperConfig.CreateMapper();
                services.AddSingleton(mapper);
                services.AddTransient<IDataMapper, DataMapper>();

                // Repositories
                services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
                services.AddTransient<IFilterDataRepository, FilterDataRepository>();
                services.AddTransient<IAppSettingsRepository, AppSettingsRepository>();
                services.AddTransient<IColleagueRepository, ColleagueRepository>();
                services.AddTransient<IErrorRepository, ErrorEntryRepository>();
                services.AddTransient<IIncidentRepository, IncidentRepository>();
                services.AddTransient<IDocumentTypeRepository, DocumentTypeRepository>();
                services.AddTransient<ICountryRepository, CountryRepository>();
                services.AddTransient<IInboundErrorLogRepository, InboundErrorLogRepository>();

                // Services
                services.AddTransient<IFilterDataService, FilterDataService>();
                services.AddTransient<IAppSettingsService, AppSettingsService>();
                services.AddTransient<IColleagueService, ColleagueService>();
                services.AddTransient<IErrorService, ErrorService>();
                services.AddTransient<IIncidentService, IncidentService>();
                services.AddTransient<IDocumentTypeService, DocumentTypeService>();
                services.AddTransient<ICountryService, CountryService>();
                services.AddTransient<IInboundErrorLogService, InboundErrorLogService>();                

                // Register eventhub classes
                services.AddTransient<IEventHubAdapter, EventHubAdapter>();
                services.AddTransient<IEventHubDataReceiver, EventHubDataReceiver>();
                services.AddTransient<BaseEnrichedDataReceiver, PublishedEnrichedDataReceiver>();
                services.AddTransient<BaseEnrichedDataReceiver, UpdatedEnrichedDataReceiver>();

                services.AddTransient<IHCMAdditionalData, HCMAdditionalData>();

             //   InitialiseEventHubDataReceiver(services);
                InitialiseBackgroundServices(services);
                
            }
            catch (Exception ex)
            {
                IncidentErrorDescription description = IncidentErrorDescription.NoIncident;
                
                if (ex is DatabaseException)
                    description = IncidentErrorDescription.AzureDatabaseError;
                else if (ex is KeyVaultException)
                    description = IncidentErrorDescription.AzureKeyVaultError;
                else if (ex is EventHubException)
                    description = IncidentErrorDescription.AzureEventHubError;
                else
                    description = IncidentErrorDescription.GenericException;

                RaiseIncident(services, configRoot, ex, description);
                throw new Exception(ex.Message);
            }
        }
              

        private static bool CheckDatabaseConnection()
        {
            using RTWContext dbContext = new RTWContext();
            return dbContext.Database.CanConnect();
        }

        private static void InitialiseEventHubDataReceiver(IServiceCollection collection)
        {
            try
            {
                var serviceProvider = collection.BuildServiceProvider();
                foreach (var eventHubRxr in serviceProvider.GetServices<BaseEnrichedDataReceiver>())
                {
                    eventHubRxr.InitializeDataAsync();
                }
            }
            catch (Exception ex)
            {
                throw new EventHubException($"Exception while initialising EventHub adapters" + $"Message : {ex.Message}" +  $"StackTrace : {ex.StackTrace}");
            }
        }

        private static void InitialiseBackgroundServices(IServiceCollection services)
        {
            try
            {
                services.AddHostedService<HCMDocumentTypeLookupService>();
                //services.AddHostedService<DataDispatcherService>();
                services.AddHostedService<DataReaderService>();
                ////services.AddHostedService<OutboundErrorService>();   No longer need this service
                // services.AddHostedService<InboundErrorService>();
                //services.AddHostedService<UpdateIncidentService>();
                //services.AddHostedService<CleanUpDataService>();
                //services.AddHostedService<ApprovedVisaDataDispatcher>();
                //services.AddHostedService<PassportImagePathService>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception while initialising background services" + $"Message : {ex.Message}" +  $"StackTrace : {ex.StackTrace}");
            }
        }

        private static void InitialiseApiServices(IServiceCollection collection, IConfiguration configRoot)
        {
            try
            {
                var serviceProvider = collection.BuildServiceProvider();
                var keyVaultService = serviceProvider.GetService<IKeyVaultService>();
                if (keyVaultService == null)
                    return;

                var rtwJSOrganisationId = configRoot.GetValue<string>("KeyVaultSettings:RTWJSOrganisationId");
                var rtwJSOrganisationIdValue = keyVaultService.GetSecretValue(rtwJSOrganisationId).GetAwaiter().GetResult();
                collection.Configure<RTWSettings>(x =>
                {
                    x.OrganisationId = rtwJSOrganisationIdValue;
                });

                var rtwApiSubscriptionKey = configRoot.GetValue<string>("KeyVaultSettings:RTWApiSubscriptionKey");
                var rtwApiSubscriptionKeyValue = keyVaultService.GetSecretValue(rtwApiSubscriptionKey).GetAwaiter().GetResult();
                var apiService = new AzureApiService(rtwApiSubscriptionKeyValue);
                collection.AddTransient<IAzureApiService, AzureApiService>(provider => apiService);
                

                var certificateKeyName = configRoot.GetValue<string>("KeyVaultSettings:SSLCertificateKeyName");
                var certificateKey = keyVaultService.GetCertificate(certificateKeyName).GetAwaiter().GetResult();
                var issuer = configRoot.GetValue<string>("KeyVaultSettings:SSLCertificateIssuerName");
                var hcmFeedUserName = keyVaultService
                    .GetSecretValue(configRoot.GetValue<string>("KeyVaultSettings:HcmFeedUserName")).GetAwaiter().GetResult();

                var jwTokenService = serviceProvider.GetService<IJwTokenService>();
                jwTokenService?.GenerateJWTTokenAsync(certificateKey, issuer, hcmFeedUserName,
                    CacheConstants.CloudApplicationCertificate);                
            }
            catch (Exception ex)
            {
                throw new KeyVaultException($"Exception while retrieving data from keyvault" + $"Message : {ex.Message}" + $"StackTrace : {ex.StackTrace}");
            }

        }

        private static void ConfigureServiceNow(IServiceCollection services, IConfiguration configRoot)
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
                throw new Exception($"Exception while configuring ServiceNow service." +  $"Message : {ex.Message}" +  $"StackTrace : {ex.StackTrace}");
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
                incidentViewModel.ServiceNowDescription = "Azure RTW consumer application failed to start";
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
    }
}
