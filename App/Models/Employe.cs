using System;
using System.Collections.Generic;

namespace App.Models
{
    public partial class Employe
    {
        public Employe()
        {
            Dependents = new HashSet<Dependents>();
            InverseManager = new HashSet<Employe>();
        }

        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public int JobId { get; set; }
        public decimal Salary { get; set; }
        public int? ManagerId { get; set; }
        public int? DepartmentId { get; set; }

        public virtual Department Department { get; set; }
        public virtual Job Job { get; set; }
        public virtual Employe Manager { get; set; }
        public virtual Administrator Administrator { get; set; }
        public virtual ICollection<Dependents> Dependents { get; set; }
        public virtual ICollection<Employe> InverseManager { get; set; }
    }
}
