using System;

namespace HW._10.Task2.Models
{
    class Song : Item
    {
        public string Singer { get; set; }
        public int Length { get; set; }

        public Song(int code, string name, string category, double size)
        {
            Code = code;
            Name = name;
            Category = category;
            Size = size;
        }
        public override string ToString()
        {
            return $" Code - {Code}, Name - {Name}, Category - {Category}, Size - {Size} Mb, Singer - {Singer}, Length - {Length} seconds.";
        }
        public void AddSpecificInform(string singer, int length)
        {
            Singer = singer;
            Length = length;
        }
    }
}
