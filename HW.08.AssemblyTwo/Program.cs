using System;
using HW._08.AssemblyOne;

namespace HW._08.AssemblyTwo
{
    class Program
    {
        static void Main(string[] args)
        {
            Motorcycle motorcycle = new();

            var moto1 = Motorcycle.MaxSpeedPublic;

            // var moto2 = Motorcycle.MaxWeightProtected; Не работает, так как класс Program
            // не является наследником класса Motorcycle.

            // var moto3 = motorcycle.colourRedInternal; Модификатор доступа internal - экземпляр
            // доступен только в той сборке, где описан.

            // var moto4 = motorcycle.colourGreenProtInternal;  Не работает, так как класс Program
            // находится в другой сборке и не является наследником класса Motorcycle.

            // motorcycle.PowerReservePrivate(); Доступен только в классе Motorcycle.

            // motorcycle.RearDrivePrivateProtected(); Не работает, так как класс Program
            // находится в другой сборке.

            // ВЫВОД: для работы с экзамплярами класса в новой сборке (если класс не наследуется)
            // необходимо использовать модификатор доступа public.
        }
    }
}
