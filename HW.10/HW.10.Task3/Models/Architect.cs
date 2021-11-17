using System;
using System.Collections.Generic;

namespace HW._10.Task3.Models
{
    class Architect : Engineer
    {
        public int SalaryRatio { get; set; } = 12;
        public int Bonus { get; set; } = 1000;
        public Architect(string firstName, string lastName, int experience, string github)
        {
            Id = Guid.NewGuid();
            Title = "Architect";
            FirstName = firstName;
            LastName = LastName;
            Experience = experience;
            Github = github;
            Responsibilities = DescribeResponsibilities();
        }
        public override int FindSalary()
        {
            return BaseSalary * SalaryRatio + Bonus;
        }
        public override List<string> DescribeResponsibilities()
        {
            Responsibilities = new()
            {
                "Provide ongoing development for several progressive web applications (PWA's) that govern users' medical data and support contact tracking",
                "Refactor existing back-end and middle-tier C# .NET Core applications",
                "Heavy API development focus within Azure",
                "Contribute to the QA function as necessary, support broader team's overall development & release initiatives",
                "Help train and mentor less-experienced team members"
            };
            return Responsibilities;
        }
    }
}
