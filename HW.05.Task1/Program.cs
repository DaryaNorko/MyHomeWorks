using System;

namespace HW._05.Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" Let's create an array! Input the length of the array.");
            int[] arrayUser = new int[int.Parse(Console.ReadLine())];

            Console.WriteLine($"Please, input {arrayUser.Length} values.");
            Console.WriteLine();

            for (int i = 0; i < arrayUser.Length; i++)
            {
                Console.Write($" Value number {i + 1} - ");
                int num = int.Parse(Console.ReadLine());
                arrayUser[i] = num;
            }
            Console.WriteLine();

            int [] arrayRandom = new int [arrayUser.Length];
            Random rand = new Random();
            for (int i = 0; i<arrayRandom.Length; i++)
            {
                arrayRandom[i] = rand.Next(0, 200);
            }
          
            int[] arraySum = new int[arrayUser.Length];
            for (int i = 0; i < arraySum.Length; i++)
            {
                arraySum[i] = arrayRandom[i] + arrayUser[i];
            }

            Console.WriteLine(" Array of random values: ");
            for (int i = 0; i < arrayRandom.Length; i++)
            {
                Console.Write($"{arrayRandom[i]} \t");
            }
            Console.WriteLine();

            Console.WriteLine(" Array of user values: ");
            for (int i = 0; i < arrayUser.Length; i++)
            {
                Console.Write($"{arrayUser[i]} \t");
            }
            Console.WriteLine();

            Console.WriteLine(" Array of sum values: ");
            for (int i = 0; i < arraySum.Length; i++)
            {
                Console.Write($"{arraySum[i]} \t");
            }
        }
    }
}
