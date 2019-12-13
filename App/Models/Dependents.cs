﻿using System;
using System.Collections.Generic;

namespace App.Models
{
    public partial class Dependents
    {
        public int DependentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Relationship { get; set; }
        public int EmployeeId { get; set; }

        public virtual Employe Employee { get; set; }
    }
}
