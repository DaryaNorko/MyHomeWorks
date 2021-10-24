using System;

namespace HW._06.Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Пожалуйста, введите стихотворение в одну строку, разделяя строки точкой с запятой.");
            string poem = Console.ReadLine();

            poem = poem.Replace('о', 'а');
            poem = poem.Replace('О', 'А');

            string[] linesOfPoem = poem.Split(';');

            foreach (string line in linesOfPoem)
            {
                Console.WriteLine(line);
            }
        }
    }
}
