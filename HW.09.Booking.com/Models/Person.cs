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
        public string CardNumber { get; set; }
        public DateTime FirstDate { get; set; }
        public DateTime LastDate { get; set; }
        public string Password { get; set; }

        public Person()
        {
            Guid Id = new Guid();
        }

        public string PrintFinishBookMessage()
        {
            return $"Данные о бронировании:\n {hotel.ToString()} \n {bookApart.ToString()} \n" +
                $"{hotel.PrintBonus()}\n Дата заезда - {FirstDate}, дата выезда - {LastDate}. Выезд осуществляется до 12:00." +
                $"Ваши данные: {FirstName} {LastName}, e-mail - {Email}.\n Серия и номер паспорта - {PassportDates}." +
                $"\n Благодарим за использование нашего сервиса!"
        }
    }
}
