using System;
using System.Collections.Generic;

namespace HW._14.Task1
{
    interface IRepository<T> where T : class
    {
        T GetTransportById(Guid id);
        void GetTransports();
        void CreateTransport(T transport);
        void UpdateTransport(T transport);
        void DeleteTransport(T transport);
    }
}
