

namespace ColleagueInt.RTW.EventHandler.EventHandler
{
    using System;

    public class EventHubMessageEventArgs : EventArgs
    {
        public DateTimeOffset EnqueuedTime { get; set; }
        public string EventMessage { get; set; }
        public long SequenceNumber { get; }
        public long Offset { get; }

        public EventHubMessageEventArgs(string message, long sequenceNumber, long offSet)
        {
            EventMessage = message;
            SequenceNumber = sequenceNumber;
            Offset = offSet;
        }
    }

    public class EventHubErrorEventArgs : EventArgs
    {
        public DateTimeOffset EnqueuedTime { get; set; }
        public string EventMessage { get; set; }

        public EventHubErrorEventArgs(string message)
        {
            EventMessage = message;
        }
    }
}
