using System;

namespace Trips
{
    [Serializable]
    public class InvalidUserInputException : Exception
    {
        public InvalidUserInputException() { }
        public InvalidUserInputException(string message) : base(message) { }
        public InvalidUserInputException(string message, Exception inner) : base(message, inner) { }
        protected InvalidUserInputException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
