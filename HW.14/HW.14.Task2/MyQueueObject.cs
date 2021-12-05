using System;
using System.Collections;

namespace HW._14.Task2
{
    class MyQueueObject
    {
        public ArrayList objectList { get; set; }

        public MyQueueObject()
        {
            objectList = new();
        }
        public void Enqueue(object obj)
        {
            objectList.Add(obj);
        }
        public object Dequeue()
        {
            object objectReturn = Peek();
            objectList.Remove(objectReturn);

            return objectReturn;
        }
        public object Peek()
        {
            return objectList[0];
        }
    }
    class ClassMyQueueObject2  // Это попытка сделать очередь с помощью обычного массива.
    {
        public object[] array;

        private int position { get; set; }

        public ClassMyQueueObject2()
        {
            array = new object[5];
            position = 0;
        }
        private object[] AllocateMoreMemory()
        {
            Array.Resize<object>(ref array, array.Length * 2);

            return array;
        }

        public void Enqueue(object obj)
        {
            array[position] = obj;
            if (position == array.Length - 1)
            {
                array = AllocateMoreMemory();
            }
            position++;
        }
        public object Dequeue()
        {
            object elementZero = Peek();

            for (int i = 0; i < array.Length - 1; i++)
            {
                if (i + 1 != array.Length - 1)
                    array[i] = array[i + 1];
                else
                {
                    array[i] = array[i + 1];
                    array[i + 1] = default;
                }
            }
            return elementZero;
        }
        public object Peek()
        {
            return array[0];
        }
    }
}