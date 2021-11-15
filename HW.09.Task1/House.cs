using System;

//Create a class "House", with an attribute "area", a constructor that sets its value 
//and  a  method  "ShowData"  to  display  "I  am  a  house,  my  area  is  200  m2" 
//(instead of 200, it will show the real surface). Include getters an setters for the 
//area, too. 
//• The  "House"  will  contain  a  door.  Each  door  will  have  an  attribute  "color"  (a 
//string),  and a  method  "ShowData"  wich  will  display  "I  am  a  door,  my  color  is 
//brown"  (or  whatever  color  it  really  is).  Include  a  getter  and  a  setter.  Also, 
//create a "GetDoor" in the house. 
//• A "SmallApartment" is a subclass of House, with a preset area of 50 m2. 
//• Also create a class Person, with a name (string). Each person will have a house. 
//The method "ShowData" for a person will display his/her name, show the data 
//of his/her house and the data of the door of that house. 
//• Write a Main to create a SmallApartment, a person to live in it, and to show the 
//data of the person.
namespace HW._09.Task1
{
    class House
    {
        Door door = new("brown");

        public int area;

        public int Area
        {
            get { return area;}
            set { area = value;}
        }

        public House(int area)
        {
            this.area = area;
        }

        public void ShowData()
        {
            Console.WriteLine($"I'm a house, my area is {area} m2.");
        }

        public Door GetDoor()
        {
            return door;
        }

    }
}
