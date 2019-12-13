using System;
using System.Collections.Generic;

namespace App.Models
{
    public partial class Salariu
    {
        public int Id { get; set; }
        public int? IdAngajat { get; set; }
        public int? Salariu1 { get; set; }

        public virtual Angajati IdAngajatNavigation { get; set; }
    }
}
