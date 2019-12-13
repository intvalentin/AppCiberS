using System;
using System.Collections.Generic;

namespace App.Models
{
    public partial class Department
    {
        public Department()
        {
            Employee = new HashSet<Employee>();
        }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int? LocationId { get; set; }

        public virtual Locations Location { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
    }
}
