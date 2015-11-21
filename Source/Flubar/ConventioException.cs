using System;

namespace Flubar
{
    [Serializable]
    public class ConventioException : Exception
    {
        public ConventioException() { }
        public ConventioException(string message) : base(message) { }
        public ConventioException(string message, Exception inner) : base(message, inner) { }
        protected ConventioException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
