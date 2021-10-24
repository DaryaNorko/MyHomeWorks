using System;

namespace HW._06.Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string numSym = "gdfgdf234dg54gf*23oP42";
            char[] allChars = numSym.ToCharArray();
            string num1Str = null;
            string num2Str = null;
            char mathSym = default;
            

            for (int i = 0; i < allChars.Length; i++)
            {
                if (Char.IsNumber(allChars[i]) && mathSym == default)
                    num1Str = String.Concat(num1Str, allChars[i]);
                else if (!Char.IsNumber(allChars[i]) && !Char.IsLetter(allChars[i]))
                    mathSym = allChars[i];
                else if (Char.IsNumber(allChars[i]) && mathSym != default)
                    num2Str = String.Concat(num2Str, allChars[i]);
            }

            int num1 = Convert.ToInt32(num1Str);
            int num2 = Convert.ToInt32(num2Str);

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
