using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyPlants.Models
{
    interface IRepository
    {
        public void AddUser(User user);
        public void AddNewPlant(User user);
        public void UpdatePlant(User user, Plant plantToUpdate);
        public void DeletePlants(User user);
        public bool UpdateUserData(User user, string inputValue);
        public bool DeleteProfile(User user);

    }
}
