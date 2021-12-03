using System;

namespace HW._14.Task1.Models
{
    class Car:Transport
    {
        public string Category { get; }
        public Car()
        {
            Guid id = Guid.NewGuid();
            Category = "Car";
        }
        public override string ToString()
        {
            return $"Category - {Category}, Id - {Id}, Name - {Name}, Model - {Model}, Year - {Year}, Odometer - {Odometer} km.";
        }
    }
}
