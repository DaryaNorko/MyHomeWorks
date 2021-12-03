using HW._14.Task1.Models;
using Serilog;
using System;
using System.Collections.Generic;

namespace HW._14.Task1
{
    sealed public class Repository<T> : IRepository <T> where T: Transport // Здесь указываю Transport, чтобы можно было обращаться к элементам класса
                                                                            // (при осуществлении поиска по id). 
    {
        static private List<T> _transports;

        private T _transport;

        private T transport
        {
            get { return _transport; }
            set
            {
                if (_transport == null)
                {
                    throw new TransportNotFoundException("Transport not found.");
                }
                else
                    _transport = value;
            }
        }

        static Repository()
        {
            _transports = new();
        }

        public int NumberOfTransportsInList()
        {
            return _transports.Count;
        }
        public void CreateTransport(T transport)
        {
            if (transport != null)
            {
                _transports.Add(transport);

                Log.Information("The new object was added to the common list.");
            }
        }

        public void DeleteTransport(T transport)
        {
            _transports.Remove(this.transport);
        }

        public T GetTransportById(Guid id)
        {
            transport = _transports.Find(tr => tr.Id == id);

            return transport;
        }
        public void GetTransports()
        {
            foreach (T tr in _transports)
            {
                Console.WriteLine(tr.ToString());
            }
            Log.Information("The process of outputting data to the console has begun.");
        }

        public void UpdateTransport(T transport)
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
                            transport.Name = Console.ReadLine();
                            break;
                        case ParameterToUpdate.Model:
                            Console.WriteLine("Please, input new model");
                            transport.Model = Console.ReadLine();
                            break;
                        case ParameterToUpdate.Year:
                            Console.WriteLine("Please, input new year");
                            transport.Year = int.Parse(Console.ReadLine());
                            break;
                        case ParameterToUpdate.Odometer:
                            Console.WriteLine("Please, input new odometer");
                            transport.Odometer = double.Parse(Console.ReadLine());
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

                Console.WriteLine("Do you want to make any other changes to this transport? Input Yes or No.");
                if ((Console.ReadLine().ToLowerInvariant()).Equals("yes"))
                    isReUpdateNeed = true;
                else
                {
                    isReUpdateNeed = false;
                    Console.WriteLine($"Data update completed successfully! \n {transport.ToString()}");
                }
            } while (isReUpdateNeed);

            Log.Information($"Data of motorcycle {transport.ToString()} was updated.");
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
