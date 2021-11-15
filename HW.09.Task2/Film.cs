using System;
namespace HW._09.Task2
{
    class Film
    {
        int Code { get; set; }
        string Name { get; set; }
        string Category { get; set; }
        string Director { get; set; }
        string MainActor { get; set; }
        string MainActress { get; set; }

        public Film(int code, string name, string category, string director, string mainActor, string mainActress = "---")
        {
            Code = code;
            Name = name;
            Category = category;
            Director = director;
            MainActor = mainActor;
            MainActress = mainActress;
        }
        public override string ToString()
        {
            return $" Code - {Code}, Name - {Name}, Category - {Category}, Director - {Director}, MainActor - {MainActor}, MainActress - {MainActress}.";
        }

        public void Play()
        {
            Console.WriteLine(ToString());
        }
    }
}
