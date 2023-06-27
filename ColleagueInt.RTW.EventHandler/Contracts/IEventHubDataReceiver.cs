

namespace ColleagueInt.RTW.EventHandler.EventHandler.Contracts
{
    using System;
    using System.Threading.Tasks;

    public interface  IEventHubDataReceiver
    {
        event EventHandler<EventHubMessageEventArgs> EventHubData;
        event EventHandler<EventHubErrorEventArgs> EventHubError;

        void CreateEventHubClient(string eventHubsConnectionString, string eventHubName, string storageConnectionString, string blobContainerName, string consumerGroup);
        Task StartReceivingMessagesAsync();
    }
}
