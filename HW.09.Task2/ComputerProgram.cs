using System;

namespace HW._09.Task2
{
    class ComputerProgram
    {
        int Code { get; set; }
        string Name { get; set; }
        string Category { get; set; }
        double Size { get; set; }

        public ComputerProgram(int code, string name, string category, double size)
        {
            Code = code;
            Name = name;
            Category = category;
            Size = size;
        }

        public void Print()
        {
            Console.WriteLine($"Code - {Code}, Name - {Name}, Category - {Category}, Size - {Size}.");
        }
    }
}
