using HW._10.Task1.Models;
using System;

namespace HW._10.Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Person person = new();
            Console.WriteLine(person.GetType().Name);
            person.SayHello();
            Console.WriteLine();

            Student student = new();
            Console.WriteLine(student.GetType().Name);
            student.SetAge(21);
            student.SayHello();
            student.ShowAge();
            Console.WriteLine();

            Teacher teacher = new();
            Console.WriteLine(teacher.GetType().Name);
            teacher.SetAge(30);
            teacher.SayHello();
            teacher.Explain();         
        }
    }
}
