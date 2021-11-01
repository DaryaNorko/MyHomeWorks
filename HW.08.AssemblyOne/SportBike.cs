using System;

namespace HW._08.AssemblyOne
{
    class SportBike:Motorcycle
    {
      public void Sportbike()
      { 
            var moto1 = MaxSpeedPublic;
            var moto2 = MaxWeightProtected;
            var moto3 = colourRedInternal;
            var moto4 = colourGreenProtInternal;

            // PowerReservePrivate(); Нельзя вызвать данный метод, так как он доступен только в классе Motorcycle.

            RearDrivePrivateProtected();

      }
    }
}

