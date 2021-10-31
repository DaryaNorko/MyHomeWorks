using System;
using System.Text;

namespace HW._08.Task1
{
    class Program
    {
        static void Main(string[] args)
        {
           
            string symbolString = "1a!2.3!!.. 4.!.?6 7! ..?";
            int index = symbolString.IndexOf('?');

            StringBuilder part1SB = new StringBuilder(symbolString);
            StringBuilder part2SB = new StringBuilder(symbolString);
         

            part1SB.Remove(index+1, part1SB.Length -1-index).Replace('!', '.');
            char[] part1Array = part1SB.ToString().ToCharArray();
            part1SB.Clear();

            for (int i = 0; i < part1Array.Length; i++)
            {
                if (part1Array[i] != '.')
                    part1SB.Append(part1Array[i]);
            }
            
            part1SB.Append(part2SB.Remove(0, index+1).Replace(' ', '_'));
            symbolString = part1SB.ToString();

            Console.WriteLine(symbolString);
        }
    }
}