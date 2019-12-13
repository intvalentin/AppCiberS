using System;
using System.Collections.Generic;

namespace App.Models
{
    public partial class Angajati
    {
        public Angajati()
        {
            Departament = new HashSet<Departament>();
            Salariu = new HashSet<Salariu>();
        }

        public int Id { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Email { get; set; }
        public long NumarTelefon { get; set; }
        public int? IdDepartament { get; set; }
        public DateTime? DataAngajari { get; set; }
        public int? IdFunctia { get; set; }

        public virtual Departament IdDepartamentNavigation { get; set; }
        public virtual Functie IdFunctiaNavigation { get; set; }
        public virtual ICollection<Departament> Departament { get; set; }
        public virtual ICollection<Salariu> Salariu { get; set; }
    }
}
