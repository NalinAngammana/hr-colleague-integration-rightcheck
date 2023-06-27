using ColleagueInt.RTW.Consumer.Configuration;
using ColleagueInt.RTW.Consumer.Data.Contracts;
using ColleagueInt.RTW.Consumer.Services.HCMDataServices.Contracts;
using ColleagueInt.RTW.Consumer.Services.myHRDataReceiver.Contracts;
using ColleagueInt.RTW.Core.RestApiService.Contracts;
using ColleagueInt.RTW.Services.Contracts;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace ColleagueInt.RTW.Consumer.Services.myHRDataReceiver
{
    public class PublishedEnrichedDataReceiver : BaseEnrichedDataReceiver
    {

        public PublishedEnrichedDataReceiver(
                                ILogger<PublishedEnrichedDataReceiver> logger,
                                IServiceProvider serviceProvider,
                                IEventHubAdapter eventHubAdapter,
                                IOptions<EventHubSettings> eventHubSettings,
                                IOptions<GeneralSettings> generalSettings,
                                IOptions<HcmSettings> hcmSettings,
                                IColleagueService colleagueService,
                                IFilterDataService filterDataService,
                                IJwTokenService jwTokenService,
                                IHCMAdditionalData hcmAdditionalData,
                                IDataMapper dataMapper
                    ) : base(
                        logger,
                        serviceProvider,
                        eventHubAdapter,
                        eventHubSettings,
                        generalSettings,
                        hcmSettings,
                        colleagueService,
                        filterDataService,
                        jwTokenService,
                        hcmAdditionalData,
                        dataMapper,
                        "Published")
        {
        }
    }
}
