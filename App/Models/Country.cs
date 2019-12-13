using System;
using System.Collections.Generic;

namespace App.Models
{
    public partial class Country
    {
        public Country()
        {
            Locations = new HashSet<Locations>();
        }

        public string CountryId { get; set; }
        public string CountryName { get; set; }
        public int RegionId { get; set; }

        public virtual Region Region { get; set; }
        public virtual ICollection<Locations> Locations { get; set; }
    }
}
