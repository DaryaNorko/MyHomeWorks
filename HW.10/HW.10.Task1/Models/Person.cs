using System;

namespace HW._10.Task1.Models
{
    class Person
    {
        public int Age { get; set; }
        public void SayHello()
        {
            Console.WriteLine("Hello!");
        }

        public string SetAge(int n)
        {
             Age = n;
             return $"{n} years old.";
        }
    }
}
