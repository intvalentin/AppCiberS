using System;
using System.Collections.Generic;

namespace App.Models
{
    public partial class Locations
    {
        public Locations()
        {
            Department = new HashSet<Department>();
        }

        public int LocationId { get; set; }
        public string StreetAddress { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string CountryId { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<Department> Department { get; set; }
    }
}
