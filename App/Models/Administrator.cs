namespace App.Models
{
    public partial class Administrator 
    {
        public int Id { get; set; }
        public int EmployeId { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

        public virtual Employee Employe { get; set; }
    }
}
