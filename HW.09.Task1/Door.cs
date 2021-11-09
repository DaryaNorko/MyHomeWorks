using System;

namespace HW._09.Task1
{
    class Door
    {      

        public string Color { get; set; }
        public Door(string color)
        {
            Color = color;
        }

        public void ShowData()
        {
            Console.WriteLine($"I am a door, my color is {Color}.");
        }

    }
}
