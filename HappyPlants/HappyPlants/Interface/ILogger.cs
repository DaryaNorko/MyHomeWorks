using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HappyPlants.Interface
{
    interface ILogger
    {
        public void Debug(MethodBase methodbase, string message);
        public void Info(MethodBase methodbase, string message);
        public void Error(MethodBase methodbase, string message);
    }
}
