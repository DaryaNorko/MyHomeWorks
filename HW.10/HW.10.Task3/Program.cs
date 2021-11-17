using HW._10.Task3.Models;
using System;
using System.Collections.Generic;

namespace HW._10.Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Engineer> engeneers = CreateEngineersList();

            Console.WriteLine("Список обязанностей сотрудников.");

            foreach (Engineer employee in engeneers)
            {
                Console.WriteLine(employee.Title);

                foreach (string resp in employee.Responsibilities)
                {
                    Console.WriteLine(resp);
                }
                Console.WriteLine();
            }

            Console.WriteLine("Список сотрудников (отсортирован по опыту работы).");

            Engineer[] engineersArray = SortExperience(engeneers);

            foreach (Engineer engineer in engineersArray)
            {
                Console.WriteLine($"\nCompany: {Engineer.company}, Full Name: {engineer.FirstName} {engineer.LastName}," +
                    $"Experience: {engineer.Experience}, Title - {engineer.Title}, Salary - {engineer.FindSalary()}$, " +
                    $"GitHub: {engineer.Github}");
            }            
        }

        public static Engineer [] SortExperience( List <Engineer> engeneers)
        {
            Engineer[] engineersArray = engeneers.ToArray();
            for (int i = 0; i < engineersArray.Length; i++)
            {
                for (int d = i+1; d < engineersArray.Length - 1; d++)
                {
                    Engineer eng;

                    if (engineersArray[i].Experience > engineersArray[d].Experience)
                    {
                        eng = engineersArray[i];
                        engineersArray[i] = engineersArray[d];
                        engineersArray[d] = eng;
                    }
                }
            }
            return engineersArray;
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
