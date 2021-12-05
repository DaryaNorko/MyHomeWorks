using System;
using System.Collections.Generic;

namespace HW._14.Task2
{
    class MyQueueDateTime
    {
        public List<DateTime> DateTimeList { get; set; }

        public MyQueueDateTime()
        {
            DateTimeList = new();
        }
        public void Enqueue(DateTime dt)
        {
            DateTimeList.Add(dt);
        }
        public object Dequeue()
        {
            DateTime dtReturn = Peek();
            DateTimeList.Remove(dtReturn);

            return dtReturn;
        }
        public DateTime Peek()
        {
            return DateTimeList[0];
        }
    }
}
