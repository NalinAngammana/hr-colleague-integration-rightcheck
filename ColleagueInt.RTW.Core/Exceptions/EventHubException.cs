using System;

namespace ColleagueInt.RTW.Core.Exceptions
{
    public class EventHubException : Exception
    {
        public EventHubException() : base()
        {

        }

        public EventHubException(string message) : base(message)
        {

        }

        public EventHubException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
