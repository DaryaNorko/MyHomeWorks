using System;
using System.Collections;
using System.Linq;

namespace HW._06.Task3
{
    class Program
    {
        static void Main(string[] args)
        {  
            string numSym = "gdfgdf234dg54gf*23oP42";
            char[] allChars = numSym.ToCharArray();
            
            char mathSym = allChars.Single(sym => !Char.IsNumber(sym) && !Char.IsLetter(sym));
            int indexMathSym = Array.IndexOf(allChars, mathSym);

            char [] num1Str = allChars.Take(indexMathSym).Where(sym => Char.IsNumber(sym)).ToArray();
            int num1 = Convert.ToInt32(String.Join("", num1Str));

            char[] num2Str = allChars.Skip(indexMathSym+1).Where(sym => Char.IsNumber(sym)).ToArray();
            int num2 = Convert.ToInt32(String.Join("", num2Str));

            switch (mathSym)
            {
                case '+':
                    Console.WriteLine(num1+num2);
                    break;
                case '-':
                    Console.WriteLine(num1-num2);
                    break;
                case '*':
                    Console.WriteLine(num1*num2);
                    break;
                case '/':
                    Console.WriteLine(num1/num2);
                    break;
            }
        }
    }
}
