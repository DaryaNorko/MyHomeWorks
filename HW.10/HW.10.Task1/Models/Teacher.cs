using System;

namespace HW._10.Task1.Models
{
    class Teacher : Person
    {
        private string _subject { get; set; }
        public void Explain()
        {
            Console.WriteLine("Explanation begins.");
        }
    }
}
