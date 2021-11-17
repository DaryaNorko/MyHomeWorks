using System;

namespace HW._10.Task2.Models
{
    abstract class Item
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public double Size { get; set; }
        public void Play() 
        {
            Console.WriteLine(ToString());
        }
        
    }
}
