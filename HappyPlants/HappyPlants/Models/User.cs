using System;
using System.Collections.Generic;
using System.Linq;
using HappyPlants.Exceptions;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HappyPlants.Models
{
    class User
    {
        [JsonProperty]
        private string _password { get; set; }
        public int UserId { get; set; } = 0;
        public string Name { get; set; }
        public Plant plant { get; set; }
        public List<Plant> plants { get; set; }

        public User()
        {
        }
        public User(string name, string password)
        {
            Name = name;
            _password = password;
            plants = new();
        }
        public List<Plant> GetSortDateTimePlants()
        {
            List<Plant> pp = plants.OrderBy(c => c.WateringDate).OrderBy(c => c.WateringTime).ToList();

            return pp;
        }
        public bool ComparePasswords(string userInputPassword)
        {
            return String.Equals(userInputPassword, _password);
        }

        public bool PrintPlantsListIfFull()
        {
            bool isListFull = default;
            Console.Clear();

            if (!plants.Any())
            {
                Console.WriteLine($" Ваш список растений пуст.");
                isListFull = false;

                Console.WriteLine("\n Для выхода в Главное меню нажмите любую клавишу.");
                Console.ReadKey();
            }
            else
            {
                int count = 1;
                foreach (Plant plant in plants)
                {
                    Console.WriteLine(count + " - " + plant.ToString());
                    count++;
                }
                isListFull = true;
            }           
            return isListFull;
        }

        public void UpdatePassword(string newPassword)
        {
            _password = newPassword;
        }
    }
}
