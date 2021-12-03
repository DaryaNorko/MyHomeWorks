using System;
using System.Collections.Generic;

namespace HW._14.Task2
{
    class MyQueueClass<T> where T: class
    {
        public List<T> classes { get; set; }

        public MyQueueClass()
        {
            classes = new();
        }
        public void Enqueue(T tClass)
        {
            classes.Add(tClass);
        }
        public object Dequeue()
        {
            T tClassReturn = Peek();
            classes.Remove(tClassReturn);

            return tClassReturn;
        }
        public T Peek()
        {
            return classes[0];
        }
    }
}
