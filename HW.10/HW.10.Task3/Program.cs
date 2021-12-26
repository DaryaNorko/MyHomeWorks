using HW._10.Task3.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HW._10.Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Engineer> engineers = CreateEngineersList();

            Console.WriteLine("Список обязанностей сотрудников.");

            foreach (Engineer employee in engineers)
            {
                Console.WriteLine(employee.Title);

                foreach (string resp in employee.Responsibilities)
                {
                    Console.WriteLine(resp);
                }
                Console.WriteLine();
            }

            Console.WriteLine("Список сотрудников (отсортирован по опыту работы).");

            List <Engineer> engineersList= SortExperience(engineers);

            foreach (Engineer engineer in engineersList)
            {
                Console.WriteLine($"\nCompany: {Engineer.company}, Full Name: {engineer.FirstName} {engineer.LastName}," +
                    $"Experience: {engineer.Experience}, Title - {engineer.Title}, Salary - {engineer.FindSalary()}$, " +
                    $"GitHub: {engineer.Github}");
            }            
        }
        public static List<Engineer> SortExperience( List <Engineer> engineers)
        {
            engineers = engineers.OrderByDescending(engineer => engineer.Experience).ToList();
           
            return engineers;
        }           
        public static List<Engineer> CreateEngineersList()
        {
            JuniorDeveloper jun1 = new("Андрей", "Бабушкин", 1, "https://github.com/AndyBabushkin");
            JuniorDeveloper jun2 = new("Светлана", "Заяц", 1, "https://github.com/SvetZayz");

            MiddleDeveloper middle1 = new("Андрей", "Вечерников", 3, "https://github.com/AndyVechernikov");
            MiddleDeveloper middle2 = new("Ольга", "Зумова", 2, "https://github.com/OlgaZumova");

            SeniorDeveloper senior1 = new("Владимир", "Сохин", 4, "https://github.com/VladimirSohin");
            SeniorDeveloper senior2 = new("Сергей", "Бобров", 4, "https://github.com/SergeyBobrov");

            TeamLeader teamLeader1 = new("Елена", "Павлова", 6, "https://github.com/ElenaPavlova");
            TeamLeader teamLeader2 = new("Сергей", "Зубов", 5, "https://github.com/SergeyZubov");

            Architect architect = new("Сергей", "Соболь", 8, "https://github.com/SergeySobol");

            List<Engineer> engineers = new() { jun1, jun2, middle1, middle2, senior1, senior2, teamLeader1, teamLeader2, architect };

            return engineers;
        }
    }
}
