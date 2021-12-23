using System;
using System.Linq;

namespace HW._06.Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Пожалуйста, введите стихотворение в одну строку, разделяя строки точкой с запятой.");
            string[] linesOfPoem = Console.ReadLine().Split(';');

            linesOfPoem = linesOfPoem.Select(str => str.Replace('о', 'а')).ToArray();
            linesOfPoem = linesOfPoem.Select(str => str.Replace('О', 'А')).ToArray();

            foreach (string line in linesOfPoem)

            {
                Console.WriteLine(line);
            }
        }
    }
}
