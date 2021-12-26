using System;
using System.Linq;

namespace HW._09.Booking.com.Models
{
    class Apartment
    {
        bool _isBathInTheRoom;
        public int NumberOfApartment { get; set; }
        public int Guest { get; set; }
        public double PricePerPerson { get; set; }
        public double PriceForAllDays { get; set; }

        public Hotel hotel;

        public DateTime [] freeDates = CreateFreeDates();         
        public Apartment(int guest, double pricePerPerson, bool isBathInTheRoom = false) // Конструктор для создания номера в отеле или хостеле.
        {
            Guest = guest;
            isBathInTheRoom = _isBathInTheRoom;
            freeDates = CreateFreeDates();
            PricePerPerson = pricePerPerson;
        }
        public Apartment(int guest, double pricePerPerson) // Констуктор для создания квартиры.
        {
            Guest = guest;
            freeDates = CreateFreeDates();
            PricePerPerson = pricePerPerson;
        }

        public override string ToString()
        {
            if (_isBathInTheRoom)
                return $"Информация о номере: Количество спальных мест - {Guest}, собственная ванная комната.";
            else
                return $"Информация о номере: Количество спальных мест - {Guest}";
        }

        private static DateTime [] CreateFreeDates()
        {
            DateTime[] freeDates = new DateTime[15];
            Random random = new();

            freeDates = freeDates.Select(date => new DateTime(DateTime.Now.Year, DateTime.Now.Month, random.Next(1, 31))).ToArray();

            return freeDates;
        }
    }
}
