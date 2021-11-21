using System;

namespace HW._10.Task2.Models
{
    class ComputerProgram : Item
    {
        public ComputerProgram(int code, string name, string category, double size)
        {
            Code = code;
            Name = name;
            Category = category;
            Size = size;
        }
        public override string ToString()
        {
            return $" Code - {Code}, Name - {Name}, Category - {Category}, Size - {Size} Mb.";
        }
    }
}
