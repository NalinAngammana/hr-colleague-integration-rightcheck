using ColleagueInt.RTW.Consumer.Data;
using ColleagueInt.RTW.EventHandler.Contracts;
using ColleagueInt.RTW.EventHandler.EventHandler.Contracts;

namespace ColleagueInt.RTW.Consumer.Services.myHRDataReceiver.Contracts
{
    public interface IEventHubAdapter
    {
        public IEventHubDataPublisher GetIEventHubPublisherAsync(EventHubClientData eventHubClientData);
        public IEventHubDataReceiver GetIEventHubReceiverAsync(EventHubClientData eventHubClientData);

    }
}
