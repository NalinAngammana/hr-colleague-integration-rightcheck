

namespace ColleagueInt.RTW.EventHandler.EventHandler
{
    using Azure.Messaging.EventHubs;
    using Azure.Storage.Blobs;
    using ColleagueInt.RTW.EventHandler.EventHandler.Contracts;
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;

    public class EventHubDataReceiver : IEventHubDataReceiver
    {
        private EventProcessorClient _processor;
        private Stopwatch checkpointStopWatch;

        public delegate void EventHubMessageEventHandler(object obj, EventHubMessageEventArgs eventArgs);
        public event EventHandler<EventHubMessageEventArgs> EventHubData;

        public delegate void EventHubErrorEventHandler(object obj, EventHubErrorEventArgs eventArgs);
        public event EventHandler<EventHubErrorEventArgs> EventHubError;

        public void CreateEventHubClient(string eventHubsConnectionString, string eventHubName, string storageConnectionString, string blobContainerName, string consumerGroup)
        {
            BlobContainerClient storageClient = new BlobContainerClient(storageConnectionString, blobContainerName);
            _processor = new EventProcessorClient(storageClient, consumerGroup, eventHubsConnectionString, eventHubName);

            this.checkpointStopWatch = new Stopwatch();
            this.checkpointStopWatch.Start();
        }

        public async Task StartReceivingMessagesAsync()
        {
            try
            {
                using var cancellationSource = new CancellationTokenSource();
              //  cancellationSource.CancelAfter(TimeSpan.FromSeconds(30));

                _processor.ProcessEventAsync += Processor_ProcessEventAsync;
                _processor.ProcessErrorAsync += Processor_ProcessErrorAsync;

                try
                {
                    await _processor.StartProcessingAsync(cancellationSource.Token);
                    await Task.Delay(Timeout.Infinite, cancellationSource.Token);
                }
                catch (TaskCanceledException)
                {
                    // This is expected if the cancellation token is signaled.
                }
                finally
                {
                    await _processor.StopProcessingAsync();
                }
            }
            catch
            {
                // The processor will automatically attempt to recover from any failures, either transient or fatal, and continue processing.
                // Errors in the processor's operation will be surfaced through its error handler.
                // If this block is invoked, then something external to the processor was the source of the exception.
            }
            finally
            {
                // It is encouraged that you unregister your handlers when you have finished using the Event Processor to ensure proper cleanup.  This
                // is especially important when using lambda expressions or handlers in any form that may contain closure scopes or hold other references.
                _processor.ProcessEventAsync -= Processor_ProcessEventAsync;
                _processor.ProcessErrorAsync -= Processor_ProcessErrorAsync;
            }
        }

        private Task Processor_ProcessErrorAsync(Azure.Messaging.EventHubs.Processor.ProcessErrorEventArgs args)
        {
            try
            {
                if (EventHubError != null)
                {
                    var message = $"Error in the EventProcessorClient at {DateTime.Now}. {Environment.NewLine} Operation: {args.Operation} and Exception {args.Exception}. {Environment.NewLine}";
                    var eventArgs = new EventHubErrorEventArgs(message);
                    EventHubError(this, eventArgs);
                }
            }
            catch (Exception ex)
            {
                // It is very important that you always guard against exceptions in your handler code; the processor does
                // not have enough understanding of your code to determine the correct action to take. Any exceptions from your handlers go uncaught by
                // the processor and will NOT be handled in any way.
                var message = $"Unhandled Exception {ex.Message} in EventProcessorClient while perfoming Operation {args.Operation} in Event hub Reciever.";
                var eventArgs = new EventHubErrorEventArgs(message);
                if (EventHubError != null)
                {
                    EventHubError(this, eventArgs);
                }

                //Application.HandleErrorException(args, ex);
            }

            return Task.CompletedTask;
        }

        private async Task Processor_ProcessEventAsync(Azure.Messaging.EventHubs.Processor.ProcessEventArgs args)
        {
            try
            {
                if (args.CancellationToken.IsCancellationRequested)
                {
                    //return Task.CompletedTask;
                }

                byte[] eventBody = args.Data.Body.ToArray();
                var str = System.Text.Encoding.Default.GetString(eventBody);
                if (EventHubData != null)
                {
                    var eventArgs = new EventHubMessageEventArgs(str, args.Data.SequenceNumber, args.Data.Offset);
                    eventArgs.EnqueuedTime = args.Data.EnqueuedTime;
                    EventHubData(this, eventArgs);
                }

                //Call checkpoint every 2 minutes, so that worker can resume processing from 2 minutes back if it restarts.
                if (this.checkpointStopWatch.Elapsed > TimeSpan.FromMinutes(2))
                {
                    await args.UpdateCheckpointAsync(args.CancellationToken);
                    var sequenceNumber = args.Data.SequenceNumber;
                    var offSet = args.Data.Offset;

                    Console.WriteLine($"Check Point updated at SequenceNumber: {sequenceNumber}, Offset: {offSet} at {DateTime.Now.ToString()}");

                    this.checkpointStopWatch.Restart();
                }
            }
            catch(Exception ex)
            {
                // It is very important that you always guard against exceptions in your handler code; the processor does
                // not have enough understanding of your code to determine the correct action to take. Any exceptions from your handlers go uncaught by
                // the processor and will NOT be handled in any way.
                var message = $"Unhandled Exception {ex.Message} in Processor_ProcessEventAsync in Event Hub Reciever.";
                var eventArgs = new EventHubErrorEventArgs(message);
                if (EventHubError != null)
                {
                    EventHubError(this, eventArgs);
                }
            }
        }
    }
}
