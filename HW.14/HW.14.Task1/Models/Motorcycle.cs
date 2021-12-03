using System;

namespace HW._14.Task1.Models
{
    class Motorcycle:Transport
    {
        public string Category { get; }
        public Motorcycle()
        {
            Guid id = Guid.NewGuid();
            Category = "Motorcycle";
        }
        public override string ToString()
        {
            return $"Category - {Category}, Id - {Id}, Name - {Name}, Model - {Model}, Year - {Year}, Odometer - {Odometer} km.";
        }
    }
}
