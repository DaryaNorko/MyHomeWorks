using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyPlants.Exceptions
{
    public class EmptyOrWhiteSpaceFieldException : Exception
    {
        public EmptyOrWhiteSpaceFieldException() { }
        public EmptyOrWhiteSpaceFieldException(string message) : base(message) { }
        public EmptyOrWhiteSpaceFieldException(string message, Exception inner) : base(message, inner) { }
        protected EmptyOrWhiteSpaceFieldException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
