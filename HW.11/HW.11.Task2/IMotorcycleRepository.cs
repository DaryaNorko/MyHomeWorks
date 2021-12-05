using System;
using System.Collections.Generic;

namespace HW._11.Task2
{
    interface IMotorcycleRepository
    {
        Motorcycle GetMotorcycleById(Guid id);
        void GetMotorcycles();
        void CreateMotorcycle(Motorcycle motorcycle);
        void UpdateMotorcycle(Motorcycle motorcycle);
        void DeleteMotorcycle(Motorcycle motorcycle);        
    }
}
