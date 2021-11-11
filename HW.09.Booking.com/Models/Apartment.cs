using System;

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
        public Apartment(int guest, double pricePerPerson, bool isBathInTheRoom = false)
        {
            Guest = guest;
            isBathInTheRoom = _isBathInTheRoom;
            freeDates = CreateFreeDates();
            PricePerPerson = pricePerPerson;
        }
        public Apartment(int guest, double pricePerPerson)
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
            int day;
            int month;
            int year;
            for (int i = 0; i < freeDates.Length; i++)
            {
                DateTime dateNow = DateTime.Now;

                day = random.Next(0, 31);
                month = dateNow.Month;
                year = random.Next(dateNow.Year);

                freeDates[i] = new DateTime(day, month, year);
            }
            return freeDates;
        }
    }
}
