using System;

namespace HW._05.Task2
{
    class Program
    {
        static void Main(string[] args)
        {
          
            Console.WriteLine(" Input the length of the array ");
            int[] nums = new int[int.Parse(Console.ReadLine())];

            Console.WriteLine($"Please, input {nums.Length-1} values.");
            Console.WriteLine();

            for (int i = 0; i < nums.Length-1; i++)
            {
                Console.Write($" Value number {i + 1} - ");
                int num = int.Parse(Console.ReadLine());
                nums[i] = num;
            }
            Console.WriteLine();

            Console.WriteLine(" OK. This is your array: ");

            for (int i = 0; i < nums.Length; i++)
            {
                Console.Write($"{nums[i]} \t");
            }
            Console.WriteLine();

            Console.WriteLine(" Please, input one more value.");
            int addNum = int.Parse(Console.ReadLine());

            Console.WriteLine(" What is the position of this number in the array? ");
            int indexAddNum = int.Parse(Console.ReadLine());

            while (indexAddNum>nums.Length-1)
            {
                Console.WriteLine($" You must choose position from 0 to {nums.Length-1}. " +
                    $"Please, try again.");
                indexAddNum = int.Parse(Console.ReadLine());
            }

            if (indexAddNum == nums.Length - 1)
            {
                nums[indexAddNum] = addNum;
            }
            else
            {
                for (int i = nums.Length-1; i > indexAddNum; i--)
                {
                    nums[i] = nums[i - 1];
                }
                nums[indexAddNum] = addNum;
            }

            Console.WriteLine("Good! Here's your new array!");

            for (int i = 0; i < nums.Length; i++)
            {
                Console.Write($"{nums[i]} \t");
            }
        }
    }
}
