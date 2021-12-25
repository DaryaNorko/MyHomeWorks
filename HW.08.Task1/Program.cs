using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HW._08.Task1
{
    class Program
    {
        static void Main(string[] args)
        {          
            string symbolString = "1a!2.3!!.. 4.!.?6 7! ..?";
            int indexSym = symbolString.IndexOf('?');

            char[] symbolArray = symbolString.ToCharArray();

            string part1 = new(symbolArray.TakeWhile(sym => sym != '?').Where(sym => sym != '!' && sym != '.').ToArray());
            
            string part2 = new(symbolArray.Skip(indexSym).ToArray());

            part2 = part2.Replace(' ', '_');

            Console.WriteLine(part1+part2);
        }
    }
}