using System;
using System.Collections.Generic;

namespace App.Models
{
    public partial class Departament
    {
        public Departament()
        {
            Angajati = new HashSet<Angajati>();
            Functie = new HashSet<Functie>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string NumeDepartament { get; set; }

        public virtual Angajati User { get; set; }
        public virtual ICollection<Angajati> Angajati { get; set; }
        public virtual ICollection<Functie> Functie { get; set; }
    }
}
