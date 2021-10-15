using System;

namespace HW._05.Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            int [] arrayRandom = new int [10];
            Random rand = new Random();
            for (int i = 0; i<arrayRandom.Length; i++)
            {
                arrayRandom[i] = rand.Next(0, 200);
            }

            int[] arrayUser = new int[arrayRandom.Length];
            Console.WriteLine($"Please, input {arrayUser.Length} values.");
            Console.WriteLine();
            
            for (int i = 0; i<arrayUser.Length; i++)
            {
                Console.Write($" Value number {i+1} - ");
               int num = int.Parse(Console.ReadLine());
                arrayUser[i] = num;
            }
            Console.WriteLine();

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
