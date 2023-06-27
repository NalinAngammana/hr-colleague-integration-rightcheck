using ColleagueInt.RTW.Consumer.Configuration;
using ColleagueInt.RTW.Consumer.Data;
using ColleagueInt.RTW.Consumer.Misc;
using ColleagueInt.RTW.Core.RestApiService.Contracts;
using ColleagueInt.RTW.Database.Constants;
using ColleagueInt.RTW.Services.Contracts;
using ColleagueInt.RTW.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ColleagueInt.RTW.Consumer.Services.HCMDataServices
{
    public class HCMDocumentTypeLookupService : BackgroundService
    {
        private readonly ILogger<HCMDocumentTypeLookupService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly HcmSettings _hcmSettings;
        private readonly GeneralSettings _generalSettings;
        private readonly IJwTokenService _jwTokenService;
        private string _hcmServerHost;
        private int _hcmServerPort;
        private string _hcmURL;

        private IEnumerable<DocumentTypeViewModel> documentTypeViewModel;


        public HCMDocumentTypeLookupService(
                        ILogger<HCMDocumentTypeLookupService> logger,
                        IOptions<HcmSettings> hcmSettings,
                        IOptions<GeneralSettings> generalSettings,
                        IServiceProvider serviceProvider,
                        IJwTokenService jwTokenService
            )

        {
            _logger = logger;
            _hcmSettings = hcmSettings.Value;
            _generalSettings = generalSettings.Value;
            _serviceProvider = serviceProvider;
            _jwTokenService = jwTokenService;
            SetUpHCMData();
        }

        void SetUpHCMData()
        {
            _hcmServerHost = _hcmSettings.HcmServerHost;
            _hcmServerPort = _hcmSettings.HcmServerPort;
            _hcmURL = $"{_hcmServerHost}:{_hcmServerPort}" +
                $"/hcmRestApi/resources/latest/hrDocumentTypesLOV?" +
                $"fields=DocumentTypeId,DocumentType&onlyData=true&limit=1000";
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var lookupRefreshIntervalInDays = Convert.ToDouble(_generalSettings.LookupRefreshIntervalInDays);
                    Logging.LogInformation(_logger, "HCM DocumentTypeLookup service started");

                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var hcmDocType = GetHCMDocumentTypeAsync().GetAwaiter().GetResult();
                        if (hcmDocType != null)
                        {
                            var hcmDocTypeLookups = hcmDocType.items.ToList();

                            var documentTypeService = scope.ServiceProvider.GetRequiredService<IDocumentTypeService>();
                            documentTypeViewModel = await documentTypeService.GetAllDocumentTypesAsync();

                            foreach (var doc in documentTypeViewModel)
                            {
                                var docTypeId = hcmDocTypeLookups.Where(x => x.DocumentType.Trim() == doc.HCMDocumentName.Trim()).Select(y => y.DocumentTypeId).FirstOrDefault();
                                if (docTypeId == 0)
                                {
                                    Logging.LogInformation(_logger, $"HCM DocumentTypeId not found for row : {doc.Id}, DocumentType : {doc.HCMDocumentName}");
                                    continue;
                                }
                                var status = await documentTypeService.UpdateDocumentTypeIdAsync(doc.Id, docTypeId);
                                if (status)
                                {
                                    Logging.LogInformation(_logger, $"HCM DocumentTypeId updated successfully for row : {doc.Id}, DocumentType : {doc.HCMDocumentName}, DocumentTypeId: {docTypeId}");
                                }
                            }
                        }
                        else
                        {
                            var error = new Exception($"Error occurred while retrieving HCM URLs during DocumentTypeLookup Background process");
                            await Logging.LogExceptionAsync(_serviceProvider, _logger, error, IncidentErrorDescription.GenericException);
                            lookupRefreshIntervalInDays = 1;
                            Logging.LogInformation(_logger, $"{error}, HCM DocumentTypeLookup will be next run after {lookupRefreshIntervalInDays} day(s) or during service start up");
                        }
                    }
                    await Task.Delay(TimeSpan.FromDays(lookupRefreshIntervalInDays), stoppingToken);
                }
                catch (Exception ex)
                {
                    await Logging.LogExceptionAsync(_serviceProvider, _logger, ex, IncidentErrorDescription.GenericException);
                }
            }
        }

        private async Task<HCMDocumentTypes> GetHCMDocumentTypeAsync()
        {
            var restResponse = await _jwTokenService.GetResponseWithJWTokenAsync(_hcmURL, CacheConstants.CloudApplicationCertificate);
            if (restResponse.StatusCode != System.Net.HttpStatusCode.OK)
            {
                Logging.LogError(_logger, $"HCM Api call to get document types failed with Status :{restResponse.StatusCode}, Details : {restResponse.StatusDescription}");
                return null;
            }

            var hcmDocTypes = JsonConvert.DeserializeObject<HCMDocumentTypes>(restResponse.Content);
            return hcmDocTypes;
        }


        public class HCMDocumentTypes
        {
            public Item[] items { get; set; }
            public int count { get; set; }
            public bool hasMore { get; set; }
            public int limit { get; set; }
            public int offset { get; set; }
            public Link[] links { get; set; }
        }

        public class Item
        {
            public long DocumentTypeId { get; set; }
            public string DocumentType { get; set; }
        }

        public class Link
        {
            public string rel { get; set; }
            public string href { get; set; }
            public string name { get; set; }
            public string kind { get; set; }
        }

    }
}
