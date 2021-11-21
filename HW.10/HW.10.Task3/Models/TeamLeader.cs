using System;
using System.Collections.Generic;

namespace HW._10.Task3.Models
{
    class TeamLeader : Engineer
    {
        public int SalaryRatio { get; set; } = 8;
        public int Bonus { get; set; } = 500;
        public TeamLeader(string firstName, string lastName, int experience, string github)
        {
            Id = Guid.NewGuid();
            Title = "Team Leader";
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
                "Develop a solution that will analyze and aggregate huge amounts of data in near real-time",
                "Manage data flows to send real-time push notifications to millions of users",
                "Participate in the design and architecture of new public open API",
                "Write clean, dry and modular code, providing proper documentation when necessary",
                "Intermediate English or higher"
            };
            return Responsibilities;
        }
    }
}
