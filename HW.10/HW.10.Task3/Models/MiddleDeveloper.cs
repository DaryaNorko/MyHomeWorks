using System;
using System.Collections.Generic;

namespace HW._10.Task3.Models
{
    class MiddleDeveloper : Engineer
    {
        public int SalaryRatio { get; set; } = 3;
        public MiddleDeveloper(string firstName, string lastName, int experience, string github)
        {
            Id = Guid.NewGuid();
            Title = "Middle Developer";
            FirstName = firstName;
            LastName = LastName;
            Experience = experience;
            Github = github;
            Responsibilities = DescribeResponsibilities();
        }
        public override int FindSalary()
        {
            return BaseSalary*SalaryRatio;
        }
        public override List<string> DescribeResponsibilities()
        {
            Responsibilities = new()
            {
                "Development of a project with microservice architecture",
                "Taking an active part in solving architectural issues",
                "Preparation of project documentation",
                "Understand the principles and methods of integration with other systems",
                "Intermediate English or higher"
            };
            return Responsibilities;
        }
    }
}
