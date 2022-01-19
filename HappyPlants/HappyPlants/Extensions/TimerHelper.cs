using System;
using System.Reflection;
using System.Threading;

namespace HappyPlants.Models
{
    static class TimerHelper
    {
        public static void LogStopTimer(this Timer timer)
        {
            if(Timer.ActiveCount == 0)
            {
                MyLogger log = new();
                log.Info(MethodBase.GetCurrentMethod(), "Таймеры остановлены.");
            }
        }
    }
}
