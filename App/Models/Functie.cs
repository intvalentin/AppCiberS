using System;
using System.Collections.Generic;

namespace App.Models
{
    public partial class Functie
    {
        public Functie()
        {
            Angajati = new HashSet<Angajati>();
        }

        public int Id { get; set; }
        public int IdDepartament { get; set; }

        public virtual Departament IdDepartamentNavigation { get; set; }
        public virtual ICollection<Angajati> Angajati { get; set; }
    }
}
