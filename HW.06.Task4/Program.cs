using System;
using System.IO;

namespace HW._06.Task4
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader textReader = new StreamReader(@"C:\Users\user\source\repos\DaryaNorko\MyHomeWorks\HW.06.Task4\assets\FindMe.txt", true);
            string textReaderResult = textReader.ReadToEnd();
            Console.WriteLine(textReaderResult);
            char invisibleChar = default;
            int count = 0;
            string index = string.Empty;

            char[] textChars = textReaderResult.ToCharArray();
           
            for (int i = 0; i < textChars.Length; i++)
            {
                if (Char.IsControl(textChars[i]))
                {
                    invisibleChar = textChars[i];
                    count++;
                    index = String.Concat(index, i, " ");
                }                  
            }           
            Console.WriteLine($" Невидимый символ в тексте - {invisibleChar}. Его обозначение в 10м формате - " +
                $"{Convert.ToInt32(invisibleChar)}. \n Количество данных символов в тексте - {count}. \n Индексы символов: {index}");
        }
    }
}
