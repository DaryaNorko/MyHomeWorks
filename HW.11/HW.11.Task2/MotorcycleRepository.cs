using Serilog;
using System;
using System.Collections.Generic;

namespace HW._11.Task2
{
    sealed public class MotorcycleRepository : IMotorcycleRepository
    {
        static private List<Motorcycle> _motorcycles;

        private Motorcycle _motoForCheckException;

        private Motorcycle motoForCheckException
        {
            get { return _motoForCheckException; }
            set 
            {
                if (_motoForCheckException == null)
                {
                    throw new MotorcycleNotFoundException("Motorcycle not found.");
                } 
                else
                    _motoForCheckException = value;
            }  
        }

        static MotorcycleRepository()
        {
            _motorcycles = new();
        }

        public int NumberOfMotorcycle()
        {
            return _motorcycles.Count;
        }
        public void CreateMotorcycle(Motorcycle motorcycle)
        {
            _motorcycles.Add(motorcycle);

            Log.Information("The new object was added to the common list.");
        }

        public void DeleteMotorcycle(Motorcycle motorcycle)
        {
            _motorcycles.Remove(motoForCheckException);
        }

        public Motorcycle GetMotorcycleById(Guid id)
        {
            motoForCheckException = _motorcycles.Find(moto => moto.Id == id);
                
            return motoForCheckException;
        }

        public void GetMotorcycles()
        {          
            foreach (Motorcycle moto in _motorcycles)
            {
                Console.WriteLine(moto.ToString());
            }
            Log.Information("The process of outputting data to the console has begun.");
        }

        public void UpdateMotorcycle(Motorcycle motorcycle)
        {
            bool isReUpdateNeed = false;

            do
            {
                ParameterToUpdate userParams = SelectParams();

                try
                {
                    switch (userParams)
                    {
                        case ParameterToUpdate.Name:
                            Console.WriteLine("Please, input new name");
                            motorcycle.Name = Console.ReadLine();
                            break;
                        case ParameterToUpdate.Model:
                            Console.WriteLine("Please, input new model");
                            motorcycle.Model = Console.ReadLine();
                            break;
                        case ParameterToUpdate.Year:
                            Console.WriteLine("Please, input new year");
                            motorcycle.Year = int.Parse(Console.ReadLine());
                            break;
                        case ParameterToUpdate.Odometer:
                            Console.WriteLine("Please, input new odometer");
                            motorcycle.Odometer = double.Parse(Console.ReadLine());
                            break;
                        default:
                            Console.WriteLine("Parameters were inputed incorrectly.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Log.Warning(ex.Message);
                    Log.Warning(ex.StackTrace);
                }

                Console.WriteLine("Do you want to make any other changes to this motorcycle? Input Yes or No.");
                if ((Console.ReadLine().ToLowerInvariant()).Equals("yes"))
                    isReUpdateNeed = true;
                else
                {
                    isReUpdateNeed = false;
                    Console.WriteLine($"Data update completed successfully! \n {motorcycle.ToString()}");
                }
            } while (isReUpdateNeed);

            Log.Information($"Data of motorcycle {motorcycle.ToString()} was updated.");
        }
        public ParameterToUpdate SelectParams()
        {
            ParameterToUpdate userParams = default;

            Console.WriteLine("Please, input which of the parameters you want to update.");

            for (int i = 1; i < 5; i++)
            {
                Console.WriteLine(i + "-" + (ParameterToUpdate)i);
            }

            try
            {
                userParams = (ParameterToUpdate)Enum.Parse(typeof(ParameterToUpdate), Console.ReadLine(), true);
            }
            catch (Exception ex)
            {
                Log.Warning(ex.Message);
                Log.Warning(ex.StackTrace);
            }
            return userParams;
        }
    }
}
