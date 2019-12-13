using System;
using System.Collections.Generic;

namespace App.Models
{
    public partial class Administrator
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

        public virtual Employe UserEmailNavigation { get; set; }
    }
}
