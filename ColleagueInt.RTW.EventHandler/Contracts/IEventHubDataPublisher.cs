
namespace ColleagueInt.RTW.EventHandler.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEventHubDataPublisher
    {
        void CreateEventHubClient(string eventHubsConnectionString, string eventHubName);
        Task<bool> PublishMessage(string messageToPublish);
        Task<int> PublishMessages(List<string> messagesToPublish, int chunkSize);
    }
}
