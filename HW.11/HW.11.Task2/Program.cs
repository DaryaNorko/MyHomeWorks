using System;
using System.Collections.Generic;
using Serilog;

namespace HW._11.Task2
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
            MotorcycleRepository motorcycleRepository = new();

            while (!isFinish)
            {
                Console.WriteLine("You can Create, Update, Delete motorcycle, Get motorcycle by Id or Get list of motorcycles.");
                for (int i = 0; i < 6; i++)
                {
                    Console.WriteLine(i + "-"+ (Actions)i);
                }

                string userAnswer = Console.ReadLine();
                Actions action = (Actions)Enum.Parse(typeof(Actions), userAnswer, true);

                switch (action)
                {
                    case Actions.Create:
                        Motorcycle motorcycle = InputDatesForMotorcycleCreation();
                        motorcycleRepository.CreateMotorcycle(motorcycle);
                        break;

                    case Actions.Update:
                        if (CheckingListFullness(motorcycleRepository))
                        {
                            Console.WriteLine("Please, input the id of the motorcycle you want to update. Do you want to see the whole list of motorcycles first?" +
              "Input Yes or No");
                            if ((Console.ReadLine().ToLowerInvariant()).Equals("yes"))
                                motorcycleRepository.GetMotorcycles();

                            id = GetMotorcycleIdFromUser();

                            try
                            {
                                Motorcycle motoById = motorcycleRepository.GetMotorcycleById(id);
                                Console.WriteLine(motoById.ToString());

                                motorcycleRepository.UpdateMotorcycle(motoById);
                            }
                            catch (MotorcycleNotFoundException ex)
                            {
                                Log.Warning(ex.Message);
                                Log.Warning(ex.StackTrace);
                            }
                        }
                        else
                            Console.WriteLine("There are no motorcycle in the list. Please, create motorcycle.");
                        break;

                    case Actions.GetById:                        
                        if (CheckingListFullness(motorcycleRepository))
                        {
                            id = GetMotorcycleIdFromUser();
                            try
                            {
                                Motorcycle motorcycleById = motorcycleRepository.GetMotorcycleById(id);
                            }
                            catch (MotorcycleNotFoundException ex)
                            {
                                Log.Warning(ex.Message);
                                Log.Warning(ex.StackTrace);
                            }
                        }
                        else
                            Console.WriteLine("There are no motorcycle in the list. Please, create motorcycle.");
                        break;

                    case Actions.GetList:
                        if (CheckingListFullness(motorcycleRepository))
                            motorcycleRepository.GetMotorcycles(); 
                        break;

                    case Actions.Delete:
                        if (CheckingListFullness(motorcycleRepository))
                        {
                            Console.WriteLine("Please, input the id of the motorcycle you want to delete. Do you want to see the whole list of motorcycles first?" +
               "Input Yes or No");

                            if ((Console.ReadLine().ToLowerInvariant()).Equals("yes"))
                                motorcycleRepository.GetMotorcycles();

                            id = GetMotorcycleIdFromUser();
                            try
                            {
                                Motorcycle motorcycleById = motorcycleRepository.GetMotorcycleById(id);
                                motorcycleRepository.DeleteMotorcycle(motorcycleById);
                            }
                            catch (MotorcycleNotFoundException ex)
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
        public static bool CheckingListFullness(MotorcycleRepository motorcycleRepository)
        {
            int numberOfMotorcycles = motorcycleRepository.NumberOfMotorcycle();

            if (numberOfMotorcycles == 0)
                return false;
            else
                return true;
        }
        public static Motorcycle InputDatesForMotorcycleCreation()
        {
            Motorcycle motorcycle = new();
            Console.WriteLine("Please, input the name of the motorcycle");
            motorcycle.Name = Console.ReadLine();

            Console.WriteLine("Please, input the model of the motorcycle");
            motorcycle.Model = Console.ReadLine();

            try
            {
                Console.WriteLine("Please, input the year of the motorcycle");
                motorcycle.Year = int.Parse(Console.ReadLine());

                Console.WriteLine("Please, input the path traveled by the motorcycle");
                motorcycle.Odometer = double.Parse(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Log.Warning(ex.Message);
                Log.Warning(ex.StackTrace);
            }
            Log.Information("The new object was created." + motorcycle.ToString());

            return motorcycle;
        }
        public static Guid GetMotorcycleIdFromUser()
        {
            Guid id = new();

            try
            {
                Console.WriteLine("Please, input the id of the motorcycle.");
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
