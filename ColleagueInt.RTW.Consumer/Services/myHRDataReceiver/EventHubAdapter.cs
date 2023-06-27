using ColleagueInt.RTW.Consumer.Data;
using ColleagueInt.RTW.Consumer.Services.myHRDataReceiver.Contracts;
using ColleagueInt.RTW.EventHandler.Contracts;
using ColleagueInt.RTW.EventHandler.EventHandler.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ColleagueInt.RTW.Consumer.Services.myHRDataReceiver
{
    public class EventHubAdapter : IEventHubAdapter
    {
        private readonly IServiceProvider _serviceProvider;

        public EventHubAdapter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IEventHubDataPublisher GetIEventHubPublisherAsync(EventHubClientData eventHubClientData)
        {
            using var scope = _serviceProvider.CreateScope();
            var eventHubDataPublisher = scope.ServiceProvider.GetRequiredService<IEventHubDataPublisher>();
            eventHubDataPublisher.CreateEventHubClient(eventHubClientData.EHNS_ConnectionString, eventHubClientData.EH_Name);
            return eventHubDataPublisher;
        }       

        public IEventHubDataReceiver GetIEventHubReceiverAsync(EventHubClientData eventHubClientData)
        {
            using var scope = _serviceProvider.CreateScope();
            var eventHubClient = scope.ServiceProvider.GetRequiredService<IEventHubDataReceiver>();
            eventHubClient.CreateEventHubClient(
                eventHubClientData.EHNS_ConnectionString,
                eventHubClientData.EH_Name,
                eventHubClientData.EH_Stg_ConnectionString,
                eventHubClientData.EH_BlobContainerName,
                eventHubClientData.EH_ConsumerGroup);
            return eventHubClient;
        }
      
    }
}
