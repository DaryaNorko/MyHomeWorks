using System;

namespace HW._09.Booking.com.Models
{
    class Person
    {
        Guid Id;

        public Apartment bookApart;

        public Hotel hotel;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PassportDates { get; set; }
        public DateTime FirstDate { get; set; }
        public DateTime LastDate { get; set; }
        public string Password { get; set; }
        public string CardNumber { get; set; } = "1234 3456 3456 2345";
        public double Money { get; set; } = 1000;
        public double PriceApartment { get; set; }

        public Person()
        {
            Guid Id = new Guid();
        }

        public override string ToString()
        {
            return $"\n Дата заезда - {FirstDate.ToString("d")}, дата выезда - {LastDate.ToString("d")}. Выезд осуществляется до 12:00." +
                $"Ваши данные: {FirstName} {LastName}, e-mail - {Email}.\n Серия и номер паспорта - {PassportDates}.";
        }
    }
}
