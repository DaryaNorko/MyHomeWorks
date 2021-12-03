using System;

namespace HW._14.Task2
{
    class ClassMyQueueObject2
    {
        public object[] array;
        private int ;

        public int _position
        {
           
        }
        public ClassMyQueueObject2()
        {
            array = new object[5];
            Position = 0;
        }
        private object[] AllocateMoreMemory() 
        {
            Array.Resize<object>(ref array, array.Length * 2);

            return array;
        }
        
        public void Enqueue(object obj)
        {
            array[Position] = obj;
            if(Position == array.Length - 1)
            {
                array = AllocateMoreMemory();
            }
            Position++;
        }
        public object Dequeue()
        {
            object elementZero = array[0];

            for (int i = 0; i < array.Length-1; i++)
            {
                if (i+1 != array.Length - 1)
                    array[i] = array[i+1];
                else
                {
                    array[i] = array[i+1];
                    array[i+1] = default;
                }   
            }
            return elementZero;
        }
        public void Show()
        {
            foreach (var item in array)
            {
                Console.WriteLine(item);
            }
        }
    }
}
