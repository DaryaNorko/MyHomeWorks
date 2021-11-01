using System;

namespace HW._08.AssemblyOne
{
    class Program
    {
        static void Main(string[] args)
        {
           Motorcycle motorcycle = new();

           var moto1Const = Motorcycle.MaxSpeedPublic;

           // var moto2Const = Motorcycle.MaxWeightProtected; Не работает, так как класс Program не является наследником класса Motorcycle.

           var moto3 = motorcycle.colourRedInternal;
           var moto4 = motorcycle.colourGreenProtInternal;

            // motorcycle.RearDrivePrivate(); Не работает. Можно использовать только в самом классе Motorcycle.

            // motorcycle.PowerReservePrivateProtected(); Не работает. Можно использовать только в самом классе Motorcycle.
        }
    }
}
