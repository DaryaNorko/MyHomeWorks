using System;

namespace HW._09.Booking.com.Models
{
    class Hotel
    {
        public bool isBreakfast;
        public bool isFreeCancellationAvailable;
        public bool isAirportShuttle;
        public bool isWifi;

        public string Locality { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Rating { get; set; }
        public int Room { get; set; }

        public Hotel(string locality, string category, string name, string address, double rating) // Конструктор для создания отеля или хостела.
        {
            Locality = locality;
            Category = category;
            Name = name;
            Address = address;
            Rating = rating;
        }
        public Hotel(string locality, string address, int room, int guest, double rating, string category = "Апартаменты", string name = "") // Конструктор для создания квартиры.
        {
            Locality = locality;
            Category = category;
            Address = address;
            Room = room;
            Rating = rating;
            Name = name;
        }

        public override string ToString()
        {
            if (string.Equals(Category, "Апартаменты"))
            return $"{Locality},{Category}, {Address}. Рейтинг - {Rating}, количество комнат - {Room}.";
                    else
            return $"{Locality},{Category} {Name}, {Address}. Наш рейтинг - {Rating}.";
        }

        public void AllIn()
        {
            isBreakfast = true;
            isFreeCancellationAvailable = true;
            isAirportShuttle = true;
            isWifi = true;
        }

        public string PrintBonus()
        {
            string allInformation = string.Empty;

            if (isBreakfast)
                allInformation += " \nХороший завтрак!";
            if (isFreeCancellationAvailable)
                allInformation += " \nДоступна бесплатная отмена бронирования!";
            if (isAirportShuttle)
                allInformation += " \nТрансфер в аэропорт!";
            if(isWifi)
                allInformation += " \nБесплатный WiFi!";
            
            return allInformation;
        }
    }
}
