using System;

namespace HW._05.GoodDay
{
    class Program
    {
        static void Main(string[] args)
        {
            //Вариант решения с оператором switch я добавила прямо в практическую, когда получилось все отправить в Git.
            //Но там я не заметила, что нельзя указывать каждый час. Здесь другой вариант решения.

            TimeSpan time = DateTime.Now.TimeOfDay;

            switch (time.Hours)
            {
                case >=9 when time.Hours < 12:
                    Console.WriteLine("Good morning, guys!");
                    break;
                case >=12 when time.Hours < 15:
                    Console.WriteLine("Good day, guys!");
                    break;
                case >= 15 when time.Hours < 22:
                    Console.WriteLine("Good evening, guys!");
                    break;
                default:
                    break;
            }
        }
    }
}
