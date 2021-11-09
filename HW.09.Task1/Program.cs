using System;

namespace HW._09.Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            SmallApartment smallApartment = new();
            Person person = new();
            person.name = "Darya Norko";
            person.houseOfPerson = smallApartment;
            person.ShowData();

        }
    }
}
