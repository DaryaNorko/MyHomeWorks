using System;

namespace HW._14.Task1
{
    abstract public class Transport
    {
        private int year;
        public int Year
        {
            get { return year; }
            set
            {
                if (value > DateTime.Now.Year)
                    Console.WriteLine("Invalid value");
                else
                    year = value;
            }
        }
        public Guid Id { get; }
        public string Name { get; set; }
        public string Model { get; set; }
        public double Odometer { get; set; }
    }
}
