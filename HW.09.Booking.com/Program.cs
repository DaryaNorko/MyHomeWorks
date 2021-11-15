using HW._09.Booking.com.Models;
using System;
using System.Collections.Generic;

namespace HW._09.Booking.com
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isSearch = true;
            bool isLogIn = false;

            int numOfList = 0;
            int child = 0;
            int adult = 0;
            int nightInHotel = 0;

            List<Apartment> findingResults = new();
            DateTime[] allNights;
            Person person = new();

            var allApartments = CreateApartmentBase(); // Создание базы апартаментов.

            Console.WriteLine("Приветствуем Вас на сервисе booking.com! " +
                "Напоминаем, что все апартаменты доступны для брони только до конца текущего месяца." +
                "Для совершения бронирования необходимо пройти регистрацию. " +
                "\n Если желаете пройти регистрацию сейчас, нажмите 1." +
                "\n Чтобы начать поиск и пройти регистрацию позже, нажмите 2.");
            
            switch (Console.ReadLine())
            {
                case "1":
                    while (!isLogIn)
                    {
                        isLogIn = LogIn(person, isLogIn);
                    }
                    break;
                case "2":
                    break;
                default:
                    Console.WriteLine("Введено недопустимое значение.");
                    break;
            }

            while (isSearch)
            {
                Console.WriteLine("Введите название населенного пункта");
                string userInputLocality = Console.ReadLine();

                allNights = FindUserNight(person);       // Введение дат заезда/отъезда.    
                
                nightInHotel = allNights.Length;

                Console.WriteLine("Введите количество взрослых");
                adult = int.Parse(Console.ReadLine());

                Console.WriteLine("Введите количество детей");
                child = int.Parse(Console.ReadLine());

                findingResults = SearchApartment(allApartments, userInputLocality, allNights, adult, child); // Поиск апартаметов и возвращение найденных результатов.

                if (findingResults.Count == 0)
                {
                    Console.WriteLine("По Вашему запросу ничего не найдено. Если желаете возобновить поиск, введите 1");
                    if (Console.ReadLine() == "1")
                        isSearch = true;
                    else 
                    {
                        Console.WriteLine("Будем рады видеть Вас снова! Всего доброго!");
                        return;
                    }
                }
                else 
                {
                    PrintResultsOnConsole(findingResults, adult, child, numOfList, nightInHotel); // Выведение найденных результатов на консоль.
                    isSearch = false;
                }
            }

            Console.WriteLine("Желаете применить дополнительные фильтры (наличие ванны в апартаментах, бесплатный WiFi, бесплатная отмена бронирования, " +
                "хороший завтрак, трансфер в аэропорт, высокий рейтинг)? Введите Да или Нет.");

            if (Console.ReadLine() == "Да")
            {
                findingResults = AddSearchFilters(findingResults); // Поиск с дополнительными фильтрами.

                if (findingResults.Count == 0)
                {
                    Console.WriteLine("По Вашему запросу ничего не найдено. Если желаете просмотреть предыдущие " +
                        "результаты поиска, введите 1. Если хотите выйти из Booking.com, введите 2.");

                    switch (Console.ReadLine())
                    {
                        case "1":
                            PrintResultsOnConsole(findingResults, adult, child, numOfList, nightInHotel);
                            break;
                        case "2":
                            Console.WriteLine("Будем рады видеть Вас снова! Всего доброго!");
                            return;
                        default:
                            Console.WriteLine("Было введено недопустимое значение.");
                            return;
                    }
                }
                else
                    PrintResultsOnConsole(findingResults, adult, child, numOfList, nightInHotel); // Выведение на консоль результатов поиска.
            }
           
            findingResults = Book(findingResults, isLogIn, person); // Бронирование.

            person.PriceApartment = FindPrice(findingResults, adult, child, numOfList, nightInHotel); // Подсчёт цены. Детям скидка 25%.

            Console.WriteLine($"Данные о бронировани: {person.hotel.ToString()} \n {person.bookApart.ToString()}. Цена:" +
                $"{person.PriceApartment}. {person.ToString()}");

            Console.WriteLine("\n Желаете оплатить бронирование? Введите Да или Нет.");

            if (Console.ReadLine() == "Нет")
            {
                Console.WriteLine("Благодарим за использование нашего сервиса!");
                return;
            }
            else
                Purchase(person); // Оплата.
        }

        public static void Purchase(Person person)
        {

             Console.WriteLine("Введите номер платежной карты");
             person.CardNumber = Console.ReadLine();
               

            if (person.Money < person.PriceApartment)
                Console.WriteLine("На Вашей карте недостаточно средств.");
            else
            {
                person.Money -= person.PriceApartment;
                Console.WriteLine("Оплата прошла успешно. Благодарим за использование нашего сервиса.");
            }                
        }

        public static bool LogIn(Person person, bool isLogIn)
        {
            Console.WriteLine("Введите имя");
            person.FirstName = Console.ReadLine();

            Console.WriteLine("Введите фамилию");
            person.LastName = Console.ReadLine();

            Console.WriteLine("Введите email");
            person.Email = Console.ReadLine();

            Console.WriteLine("Введите серию и номер паспорта");
            person.PassportDates = Console.ReadLine();

            Console.WriteLine("Придумайте пароль");
            string password = Console.ReadLine();

            Console.WriteLine("Повторите пароль");

            if (password.Equals(Console.ReadLine()))
            {
                person.Password = password;
                Console.WriteLine("Регистрация успешно пройдена!");
                isLogIn = true;
            }
            else
            {
                Console.WriteLine("При регистрации возникла ошибка. Пожалуйста, пройдите регистрацию ещё раз.");
                isLogIn = false;
            }
            return isLogIn;
        }

        public static List<Apartment> Book(List<Apartment> findingResults, bool isLogIn, Person person)
        {
            Console.WriteLine("Введите номер результата апартаментов, которые Вы хотите забронировать.");
            
            int numberBookApartment = int.Parse(Console.ReadLine());

            while (!isLogIn)
            {
                Console.WriteLine("Для завершения бронирования необходимо пройти регистрацию.");
                isLogIn = LogIn(person, isLogIn);
            }

            Apartment[] findBookApart = findingResults.ToArray();

            for (int i = 0; i < findBookApart.Length; i++)
            {
                if (findBookApart[i].NumberOfApartment == numberBookApartment)
                {
                    person.bookApart = findBookApart[i];
                    person.hotel = findBookApart[i].hotel;

                    findingResults.Clear();
                    findingResults.Add(person.bookApart);
                }
            }         
            Console.WriteLine("Бронирование успешно завершено!");

            return findingResults;
        }

        static void PrintResultsOnConsole(List<Apartment> findingResults, int adult, int child, int numOfList, int nightInHotel)
        {
            Console.WriteLine($"Количество найденных результатов - {findingResults.Count}.");
            foreach (var apart in findingResults)
            {
                double priceForAllDays = FindPrice(findingResults, adult, child, numOfList, nightInHotel);

                numOfList++;
                apart.NumberOfApartment = numOfList;

                Console.WriteLine($"Результат {numOfList}: \n {apart.hotel.ToString()} {apart.ToString()} Цена: {priceForAllDays} BYN");
            }
        }
        
        public static List<Apartment> AddSearchFilters(List<Apartment> findingResults)
        {
            Console.WriteLine("Доступны следующие фильтры: \n 1 Бесплатный WiFi. 2 Бесплатная отмена бронирования. 3 Хороший завтрак." +
                "4 Трансфер в аэропорт. 5 Высокий рейтинг (более 8.0). Введите через запятую номера фильтров, которые Вы хотите применить. ");

            Apartment[] apartments = findingResults.ToArray();
            List<Apartment> apartmentsWithFilters = new();

            string userChoice = Console.ReadLine();
            string[] userChoiceArray = userChoice.Split(',');

            for (int i = 0; i < userChoiceArray.Length; i++)
            {
                switch (userChoiceArray[i])
                {
                    case "1":
                        for (int ind = 0; ind < apartments.Length; ind++)
                        {
                            if (apartments[ind].hotel.isWifi && !apartmentsWithFilters.Contains(apartments[ind]))
                                apartmentsWithFilters.Add(apartments[ind]);
                        }
                        break;
                    case "2":
                        for (int ind = 0; ind < apartments.Length; ind++)
                        {
                            if (apartments[ind].hotel.isFreeCancellationAvailable && !apartmentsWithFilters.Contains(apartments[ind]))
                                apartmentsWithFilters.Add(apartments[ind]);
                        }
                        break;
                    case "3":
                        for (int ind = 0; ind < apartments.Length; ind++)
                        {
                            if (apartments[ind].hotel.isBreakfast && !apartmentsWithFilters.Contains(apartments[ind]))
                                apartmentsWithFilters.Add(apartments[ind]);
                        }
                        break;
                    case "4":
                        for (int ind = 0; ind < apartments.Length; ind++)
                        {
                            if (apartments[ind].hotel.isAirportShuttle && !apartmentsWithFilters.Contains(apartments[ind]))
                                apartmentsWithFilters.Add(apartments[ind]);
                        }
                        break;
                    case "5":
                        for (int ind = 0; ind < apartments.Length; ind++)
                        {
                            if (apartments[ind].hotel.Rating > 8.0 && !apartmentsWithFilters.Contains(apartments[ind]))
                                apartmentsWithFilters.Add(apartments[ind]);
                        }
                        break;
                }
            }
            return apartmentsWithFilters;
        }
        
        public static double FindPrice(List<Apartment> findingResults, int adult, int child, int numOfList, int nightInHotel)
        {
            double priceForKids = 0.25;      // При бронировании номеров на сервисе детям предоставляется скидка 25%.

            Apartment [] findResultsArray = findingResults.ToArray();

            double price = (adult * findResultsArray[numOfList].PricePerPerson + child * findResultsArray[numOfList].PricePerPerson * priceForKids) * nightInHotel;

            return price;
        }

        public static DateTime[] FindUserNight(Person person)
        {
            bool isCorrectDateInput = false;

            DateTime firstUserDate = new();
            DateTime lastUserDate = new();

            while (!isCorrectDateInput)
            { 
                Console.WriteLine("Введите дату заезда в формате год.месяц.день. Например: 2000.10.6");
                string userDate1 = (Console.ReadLine());
                firstUserDate = DateTime.Parse(userDate1);
                person.FirstDate = firstUserDate;

                if (firstUserDate < DateTime.Now)
                    Console.WriteLine("Данная дата недоступна. Пожалуйста, выберите другую дату.");
                else
                    isCorrectDateInput = true;
            }

            while (isCorrectDateInput)
            {            
                Console.WriteLine("Введите дату выезда в формате год.месяц.день. Например: 2000.10.6");
                string userDate2 = (Console.ReadLine());
                lastUserDate = DateTime.Parse(userDate2);
                person.LastDate = lastUserDate;

                if (lastUserDate < DateTime.Now || lastUserDate < firstUserDate)
                    Console.WriteLine("Данная дата недоступна. Пожалуйста, выберите другую дату.");
                else
                    isCorrectDateInput = false;
            }

            int userNight = lastUserDate.Subtract(firstUserDate).Days;
            DateTime[] allNigths = new DateTime[userNight];

            for (int i = 0; i < allNigths.Length; i++)
            {
                allNigths[i] = firstUserDate.AddDays(i);
            }
            return allNigths;
        }

        public static Apartment [] CreateApartmentBase()
        {
            Hotel hotelSun = new ("Минск", "Отель", "Солнышко", "улица Столичная 10.", 9.5);
            hotelSun.AllIn();

            Hotel hotelDaryaPalace = new("Минск", "Отель", "DaryaPalace", "Рафиева 119.", 10);
            hotelSun.AllIn();

            Hotel hotelBelarus = new("Минск", "Отель", "Беларусь", "улица Столичная 15.", 9.5);
            hotelBelarus.AllIn();

            Hotel hostelPutnik = new("Минск", "Hostel", "Усталый путник", "Бобруйская 116.", 7.0);
            hostelPutnik.isWifi = true;

            Hotel apartBob83 = new("Минск", "Бобруйская 83", 3, 4, 6.0);
            apartBob83.isWifi = apartBob83.isFreeCancellationAvailable = true;

            Apartment[] apartments = new Apartment[10];
            apartments[0] = new(1, 50, true);
            apartments[0].hotel = hotelSun;

            apartments[1] = new(2, 50, true);
            apartments[1].hotel = hotelSun;

            apartments[2] = new(3, 75, true);
            apartments[2].hotel = hotelSun;

            apartments[3] = new(1, 84.50, true);
            apartments[3].hotel = hotelDaryaPalace;

            apartments[4] = new(2, 77.0, true);
            apartments[4].hotel = hotelDaryaPalace;

            apartments[5] = new(3, 100, true);
            apartments[5].hotel = hotelDaryaPalace;

            apartments[6] = new(6, 30);
            apartments[6].hotel = hostelPutnik;

            apartments[7] = new(4, 45, true);
            apartments[7].hotel = hotelBelarus;

            apartments[8] = new(3, 45, true);
            apartments[8].hotel = hotelBelarus;

            apartments[9] = new(4, 45);
            apartments[9].hotel = apartBob83;

            return apartments;
        }

        public static List<Apartment> SearchApartment(Apartment[] allApartments, string userInputLocality, DateTime[] allNights, int adult, int child)
        {
            List<Apartment> apartmentsFoundsOnRequest = new List<Apartment>();
            int countUserNights = allNights.Length;

            for (int i = 0; i < allApartments.Length; i++) 
            {
                    DateTime[] freeDate = allApartments[i].freeDates;
                    for (int index = 0; index < freeDate.Length; index++)
                    {               
                            for (int night = 0; night < allNights.Length; night++)
                            {
                                if (freeDate[index] == allNights[night])
                                    countUserNights--;
                            } 
                    }
                if (countUserNights == 0 && allApartments[i].hotel.Locality == userInputLocality && allApartments[i].Guest == (adult + child))
                {
                    apartmentsFoundsOnRequest.Add(allApartments[i]);
                }   
            }           
            return apartmentsFoundsOnRequest;
        }
    }
}
