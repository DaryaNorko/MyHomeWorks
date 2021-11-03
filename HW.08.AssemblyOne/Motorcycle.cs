using System;

namespace HW._08.AssemblyOne
{
    // internal class Motorcycle. Модификатор доступа internal не позволяет создать объект класса Motorcycle в другой сборке
    // (в нашем случае - в HW.08.AssemblyTwo), поэтому его необходимо заменить на public.

      public class Motorcycle
      {
        public const ushort MaxSpeedPublic = 300;
        protected const ushort MaxWeightProtected = 439;
        protected internal string colourRedInternal = "red";
        protected internal string colourGreenProtInternal = "green";

        private void PowerReservePrivate() { }

        private protected void RearDrivePrivateProtected() { }
      }
}
