using System;
using System.Diagnostics;


namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = { 12, 0, 3, 18 };

            Array.Reverse(array, 0, array.Length);

            Console.WriteLine(ReverseTest());
        }

        static Array ArrayReverse1(int[] array) {
     
                for (int i = 0; i < array.Length/2; i++)
            {
                int littleBox = array[array.Length - 1 - i];
                array[array.Length - 1 - i] = array[i];
                array[i] = littleBox;
            }
            return array;
        }

        static Array ArrayReverse2(int[] array)
        {
            int[] arrayRev = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                arrayRev[i] = array[array.Length-1-i];
            }
            return arrayRev;
        } 

        static string ReverseTest()
        {
            long[] largeArray = new long[100000000]; // здесь значение меньше указанного в задаче,так как у меня
            Random rand = new Random();              // компьютер зависал при оперировании массивом в миллиард значений.

            for (int i = 0; i < largeArray.Length; i++)
            {
                largeArray[i] = rand.Next();
            }

            Stopwatch speed1 = new Stopwatch();

            speed1.Start();

            for (int i = 0; i < largeArray.Length/2; i++)
            {
                long littlebox = largeArray[largeArray.Length-1-i];
                largeArray[largeArray.Length - 1 - i] = largeArray[i];
                largeArray[i] = littlebox;
            }
            speed1.Stop();

            Stopwatch speed2 = new Stopwatch();

            speed2.Start();

            Array.Reverse(largeArray, 0, largeArray.Length);

            speed2.Stop();

            return $"Time reverse with my algorithm: {speed1.ElapsedMilliseconds} \n " +
                $"Time reverse with method Revers: {speed2.ElapsedMilliseconds}";
        }    
    }
}
