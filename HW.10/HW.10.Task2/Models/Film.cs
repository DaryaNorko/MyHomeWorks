using System;

namespace HW._10.Task2.Models
{
    class Film : Item
    {
        public string Director { get; set; }
        public string MainActor { get; set; }
        public string MainActress { get; set; }

        public Film(int code, string name, string category, double size)
        {
            Code = code;
            Name = name;
            Category = category;
            Size = size;
        }
        public override string ToString()
        {
            return $" Code - {Code}, Name - {Name}, Category - {Category}, Size - {Size} Gb, Director - {Director}, MainActor - {MainActor}, MainActress - {MainActress}.";
        }
        public void AddSpecificInform(string director, string mainActor, string mainActress = "---")
        {
            Director = director;
            MainActor = mainActor;
            MainActress = mainActress;
        }
    }
}
