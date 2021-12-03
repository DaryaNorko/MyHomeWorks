using System;
using System.Runtime.Serialization;

namespace HW._14.Task1
{
    public class TransportNotFoundException : Exception
    {
        public TransportNotFoundException() { }
        public TransportNotFoundException(string message) : base(message) { }
        public TransportNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected TransportNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
