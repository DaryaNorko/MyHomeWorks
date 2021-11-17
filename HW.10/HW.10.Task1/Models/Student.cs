using System;

namespace HW._10.Task1.Models
{
    class Student : Person
    {
        public void GoToClasses()
        {
            Console.WriteLine("I'm going to class.");
        }

        public void ShowAge()
        {
            Console.WriteLine($"My age is: {SetAge(Age)}");
        }
    }
}
