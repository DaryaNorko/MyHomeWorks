using HW._14.Task1.Models;
using Serilog;
using System;

namespace HW._14.Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Hour)
                .CreateLogger();

            Log.Information("Program start working!");

            Guid id;
            bool isFinish = false;
            Repository<Transport> transportRepository = new();

            while (!isFinish)
            {
                Console.WriteLine("You can Create, Update, Delete transport, Get transport by Id or Get list of transports.");
                for (int i = 0; i < 6; i++)
                {
                    Console.WriteLine(i + "-" + (Actions)i);
                }

                string userAnswer = Console.ReadLine();

                Actions action;
                bool isUserInputCorrect = Enum.TryParse(userAnswer, true, out action);

                switch (action)
                {
                    case Actions.Create:
                        Transport transport = InputDatesForTransportCreation();
                        transportRepository.CreateTransport(transport);
                        break;

                    case Actions.Update:
                        if (CheckingListFullness(transportRepository))
                        {
                            Console.WriteLine("Please, input the id of the transport you want to update. Do you want to see the whole list of the transports first?" +
              "Input Yes or No");
                            if ((Console.ReadLine().ToLowerInvariant()).Equals("yes"))
                                transportRepository.GetTransports();

                            id = GetTransportIdFromUser();

                            try
                            {
                                Transport transportById = transportRepository.GetTransportById(id);
                                Console.WriteLine(transportById.ToString());

                                transportRepository.UpdateTransport(transportById);
                            }
                            catch (TransportNotFoundException ex)
                            {
                                Log.Warning(ex.Message);
                                Log.Warning(ex.StackTrace);
                            }
                        }
                        else
                            Console.WriteLine("There are no transport in the list. Please, create transport.");
                        break;

                    case Actions.GetById:
                        if (CheckingListFullness(transportRepository))
                        {
                            id = GetTransportIdFromUser();
                            try
                            {
                                Transport transportById = transportRepository.GetTransportById(id);
                            }
                            catch (TransportNotFoundException ex)
                            {
                                Log.Warning(ex.Message);
                                Log.Warning(ex.StackTrace);
                            }
                        }
                        else
                            Console.WriteLine("There are no transport in the list. Please, create motorcycle.");
                        break;

                    case Actions.GetList:
                        if (CheckingListFullness(transportRepository))
                            transportRepository.GetTransports();
                        break;

                    case Actions.Delete:
                        if (CheckingListFullness(transportRepository))
                        {
                            Console.WriteLine("Please, input the id of the transport you want to delete. Do you want to see the whole list of motorcycles first?" +
               "Input Yes or No");

                            if ((Console.ReadLine().ToLowerInvariant()).Equals("yes"))
                                transportRepository.GetTransports();

                            id = GetTransportIdFromUser();
                            try
                            {
                                Transport transportById = transportRepository.GetTransportById(id);
                                transportRepository.DeleteTransport(transportById);
                            }
                            catch (TransportNotFoundException ex)
                            {
                                Log.Warning(ex.Message);
                                Log.Warning(ex.StackTrace);
                            }
                        }
                        else
                            Console.WriteLine("The list is empty. You cannot perform this operation.");
                        break;

                    case Actions.Exit:
                        isFinish = true;
                        Log.Information("Exit program.");
                        break;

                    default:
                        isFinish = true;
                        Log.Information("Incorrect input value (action). Exit program.");
                        break;
                }
            }
        }
        public static bool CheckingListFullness(Repository<Transport> transportRepository)
        {
            int numberOfTransports = transportRepository.NumberOfTransportsInList();

            if (numberOfTransports == 0)
                return false;
            else
                return true;
        }
        public static Transport SelectCategoryForTransportCreation()
        {
            Console.WriteLine("Input the category of object you want to create - Car or Motorcycle.");
            string userCategory = Console.ReadLine();
            Transport transport;

            if (String.Equals("car", userCategory.ToLowerInvariant()))
            {
                transport = new Car();
                return transport;
            }
            else if (String.Equals("motorcycle", userCategory.ToLowerInvariant()))
            {
                transport = new Motorcycle();
                return transport;
            }
            else
            {
                Console.WriteLine("Incorrect value.");
            }
            return null;
        }
        public static Transport InputDatesForTransportCreation()
        {
            Transport transport;
           
                transport = SelectCategoryForTransportCreation();
            if (transport == null)
                return null;

                Console.WriteLine("Please, input the name of the transport");
                transport.Name = Console.ReadLine();

                Console.WriteLine("Please, input the model of the transport");
                transport.Model = Console.ReadLine();

            try
            {
                Console.WriteLine("Please, input the year of the transport");
                transport.Year = int.Parse(Console.ReadLine());

                Console.WriteLine("Please, input the path traveled by the transport");
                transport.Odometer = double.Parse(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Log.Warning(ex.Message);
                Log.Warning(ex.StackTrace);
            }
                Log.Information("The new object was created." + transport.ToString()); 

            return transport;
        }
        public static Guid GetTransportIdFromUser()
        {
            Guid id = new();

            try
            {
                Console.WriteLine("Please, input the id of the transport.");
                id = Guid.Parse(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Log.Warning(ex.Message);
            }
            return id;
        }
    }
}
