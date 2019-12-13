using System;
using System.Collections.Generic;

namespace App.Models
{
    public partial class Job
    {
        public Job()
        {
            Employee = new HashSet<Employee>();
        }

        public int JobId { get; set; }
        public string JobTitle { get; set; }
        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }
    }
}
