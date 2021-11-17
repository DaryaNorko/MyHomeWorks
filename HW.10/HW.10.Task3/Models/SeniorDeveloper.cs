using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW._10.Task3.Models
{
    class SeniorDeveloper : Engineer
    {
        public int SalaryRatio { get; set; } = 5;
        public int Bonus { get; set; } = 300;
        public SeniorDeveloper(string firstName, string lastName, int experience, string github)
        {
            Id = Guid.NewGuid();
            Title = "Senior Developer";
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
                "Create new solutions and see them through, from conception to production",
                "Turn requirements into simple and sophisticated technological systems. Design, build, and maintain efficient, reusable, and reliable code",
                "Research new technologies and techniques to find new and efficient ways to solve day to day challenges",
                "Ship high-value features quickly",
                "Intermediate English or higher"
            };
            return Responsibilities;
        }
    }
}
