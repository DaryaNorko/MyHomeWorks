using System;

namespace HW._05.Task4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" Input userValue from 0 to 100");
            string str = Console.ReadLine();

            int userValue = int.Parse(str);
            int arrayLength = userValue * 2 - 1;
            int countStrings = 0;
            int number = 1;
 
            while (arrayLength>0)
            {              
                char[] numsAndSpaces = new char[arrayLength];
                for (int i = 0; i <= numsAndSpaces.Length-1; i++)
                {                   
                    numsAndSpaces[i] = char.Parse(Convert.ToString(number));
                   if (i < numsAndSpaces.Length - 1)
                   {
                      i++;
                      numsAndSpaces[i] = ' ';
                   }
                }

                string spaceAtTheBeginning = new string(' ', countStrings);
                string numberStr = new string(numsAndSpaces);
                Console.WriteLine(spaceAtTheBeginning + numberStr);

                    number++;
                    arrayLength-=2;
                    countStrings++;

                if (number == 10)
                    number = 0;
            } 
        }
    }
}

