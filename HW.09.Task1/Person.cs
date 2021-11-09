using System;

namespace HW._09.Task1
{
    class Person
    {
        public string name;
        public House houseOfPerson;
       
        public void ShowData()
        {
            Console.WriteLine($"Name - {name}");
            houseOfPerson.ShowData();
             var houseDoor = houseOfPerson.GetDoor();
            houseDoor.ShowData();
        }
    }
}
