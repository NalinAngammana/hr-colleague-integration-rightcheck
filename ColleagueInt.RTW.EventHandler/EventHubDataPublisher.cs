
namespace ColleagueInt.RTW.EventHandler.EventHandler
{
    using Azure.Messaging.EventHubs.Producer;
    using ColleagueInt.RTW.EventHandler.Contracts;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AzureEventData = Azure.Messaging.EventHubs.EventData;

    public class EventHubDataPublisher : IEventHubDataPublisher
    {
        private EventHubProducerClient _eventHubProducerClient;

        public EventHubDataPublisher()
        {
            
        }

        public EventHubDataPublisher(string eventHubsConnectionString, string eventHubName)
        {
            _eventHubProducerClient = new EventHubProducerClient(eventHubsConnectionString, eventHubName);
        }

        public void CreateEventHubClient(string eventHubsConnectionString, string eventHubName)
        {
            _eventHubProducerClient = new EventHubProducerClient(eventHubsConnectionString, eventHubName);
        }

        public async Task<bool> PublishMessage(string messageToPublish)
        {
            List<AzureEventData> eventDatas = new List<AzureEventData>();
            var data = new AzureEventData(Encoding.UTF8.GetBytes(messageToPublish));
            eventDatas.Add(data);

            await _eventHubProducerClient.SendAsync(eventDatas);
            return true;
        }

        public async Task<int> PublishMessages(List<string> messagesToPublish, int chunkSize = 0)
        {
            var publishCount = 0;
            if (chunkSize == 0)
            {
                List<AzureEventData> eventDataList = new List<AzureEventData>();
                foreach (var message in messagesToPublish)
                {
                    var eventData = new AzureEventData(Encoding.UTF8.GetBytes(message));
                    eventDataList.Add(eventData);
                }
                await _eventHubProducerClient.SendAsync(eventDataList);
                publishCount = messagesToPublish.Count;
            }
            else
            {
                for (int page = 0; page < messagesToPublish.Count; page += chunkSize)
                {
                    List<AzureEventData> azureEventData = new List<AzureEventData>();
                    var messageBundle = messagesToPublish.Skip(page).Take(chunkSize);
                    foreach (var message in messageBundle)
                    {
                        var eventData = new AzureEventData(Encoding.UTF8.GetBytes(message));
                        azureEventData.Add(eventData);
                        publishCount++;
                    }
                    await _eventHubProducerClient.SendAsync(azureEventData);
                }
            }
            return publishCount;
        }
    }
}
