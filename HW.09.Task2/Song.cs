using System;

namespace HW._09.Task2
{
    class Song
    {
        string Name { get; set; }
        int Code { get; set; }
        string Category { get; set; }
        string Singer { get; set; }
        int Length { get; set; }

        public Song(int code, string name, string category, string singer, int length)
        {
            Code = code;
            Name = name;
            Category = category;
            Singer = singer;
            Length = length;
        }

        public override string ToString()
        {
            return $"Code - {Code}, Name - {Name}, Category - {Category}, Singer - {Singer}, Length - {Length} seconds.";
        }

        public void Play()
        {
            Console.WriteLine(ToString());
        }
    }
}
