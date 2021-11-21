using System;
using System.Collections.Generic;

namespace HW._10.Task3.Models
{
    class JuniorDeveloper : Engineer
    {
        public JuniorDeveloper(string firstName, string lastName, int experience, string github)
        {
            Id = Guid.NewGuid();
            Title = "Junior Developer";
            FirstName = firstName;
            LastName = LastName;
            Experience = experience;
            Github = github;
            Responsibilities = DescribeResponsibilities();
        }
        public override List<string> DescribeResponsibilities()
        {
            Responsibilities = new()
            {
                "Strong knowledge of C#",
                "Basic concepts of working with a database(SQL/NOSQL)",
                "Basic understanding of HTML, CSS",
                "Understanding how the HTTP protocol works",
                "Pre-intermediate English or higher"
            };
            return Responsibilities;
        }
    }
}
