using System;

namespace Trips
{
    [Serializable]
    public class TripRecordIsNotReadException : Exception
    {
        public TripRecordIsNotReadException() { }
        public TripRecordIsNotReadException(string message) : base(message) { }
        public TripRecordIsNotReadException(string message, Exception inner) : base(message, inner) { }
        protected TripRecordIsNotReadException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
