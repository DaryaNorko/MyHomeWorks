using System;
using System.Collections.Generic;
using System.Linq;

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
            string houseNumber;
            string streetAndTown;

            string allHouseNumbers = string.Empty;
            string allStreetsAndTowns = string.Empty;            

            if (!addresses.Contains(zipcode))
                return $"{zipcode}:/";
            else
            {
                string[] addressesArray = addresses.Split(',');

                IEnumerable <string> addressesFind = addressesArray.Where(address => address.Contains(zipcode))
                    .Select(a => a.Replace(zipcode, "").Trim());

                foreach (string address in addressesFind)
                {
                    houseNumber = address.Substring(0, address.IndexOf(' '));
                    allHouseNumbers += houseNumber;        
                    
                    streetAndTown = address.Replace(houseNumber, "").Trim();
                    allStreetsAndTowns += streetAndTown;

                    if (address != addressesFind.Last())
                    {
                        allHouseNumbers += ",";
                        allStreetsAndTowns += ",";
                    }
                }
                newAddressFormat += allStreetsAndTowns + "/" + allHouseNumbers;             
            }
            return newAddressFormat;
        }
    }
}
