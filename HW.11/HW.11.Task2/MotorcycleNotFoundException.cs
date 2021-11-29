using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HW._11.Task2
{
    public class MotorcycleNotFoundException : Exception
    {
        public MotorcycleNotFoundException() { }
        public MotorcycleNotFoundException(string message) : base(message) { }
        public MotorcycleNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected MotorcycleNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    } 
}
