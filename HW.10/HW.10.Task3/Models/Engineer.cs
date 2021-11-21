using System;
using System.Collections.Generic;


namespace HW._10.Task3.Models
{
    abstract class Engineer
    {
        public const string company = "SaM Solutions";
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Experience { get; set; }
        public List<string> Responsibilities { get; set; }
        public List<string> Technologies { get; set; }
        public string Github { get; set; }
        public string Title { get; set; }
        public int BaseSalary { get; set; } = 500;
        public virtual int FindSalary()
        {
            return BaseSalary;
        }
        abstract public List<string> DescribeResponsibilities();
    }
}
