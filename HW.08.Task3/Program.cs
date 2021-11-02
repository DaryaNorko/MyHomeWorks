using System;

namespace HW._08.Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please, input a zipcode");
            string zipcode = Console.ReadLine();
            string addresses = "123 Main Street St. Louisville OH 43071,432 Main Long Road St. Louisville OH 43071,786 High Street Pollocksville NY 56432";
           
            Console.WriteLine(Travel(addresses, zipcode));
        }

        static string Travel(string addresses, string zipcode)
        {
            string newAddressFormat = new($"{zipcode}:");
            string houseNumber = string.Empty;
            string streetAndTown = string.Empty;
            int IndexOfSpace;

            int addressCount = 0;
            string allHouseNumbers = string.Empty;
            string allStreetsAndTowns = string.Empty;            

            if (!addresses.Contains(zipcode))
                return $"{zipcode}:/";
            else
            {
                string[] addressesArray = addresses.Split(',');
                for (int i = 0; i < addressesArray.Length; )
                {
                    if (addressesArray[i].Contains(zipcode))
                    {
                        addressesArray[i].Trim();
                        IndexOfSpace = addressesArray[i].IndexOf(' ');
                        houseNumber = addressesArray[i].Substring(0, IndexOfSpace);
                        streetAndTown = addressesArray[i].Replace(zipcode, " ").Replace(houseNumber, " ").Trim();
                        addressCount++;
                    }
                    if (addressCount == 1)
                    {
                        allHouseNumbers = houseNumber;
                        allStreetsAndTowns = streetAndTown;
                        i++;
                    }
                    else if (addressCount > 1&& addressesArray[i].Contains(zipcode))
                    {
                        allHouseNumbers = allHouseNumbers + "," + houseNumber;
                        allStreetsAndTowns = allStreetsAndTowns + "," + streetAndTown;
                        i++;
                    }
                    else i++;
                }
                newAddressFormat += allStreetsAndTowns + "/" + allHouseNumbers;             
            }
            return newAddressFormat;
        }
    }
}
