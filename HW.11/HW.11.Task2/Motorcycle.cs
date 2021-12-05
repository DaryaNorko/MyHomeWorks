using System;

namespace HW._11.Task2
{
    public class Motorcycle 
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

        public Motorcycle()
        {
           Id = Guid.NewGuid();
        }
        public override string ToString()
        {
            return $"Id - {Id}, Name - {Name}, Model - {Model}, Year - {Year}, Odometer - {Odometer}";
        }
    }
}
