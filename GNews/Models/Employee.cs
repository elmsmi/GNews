using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GNews.Models
{
    public class Employee
    {
        public Employee()
        {
            Clients = new HashSet<Client>();
        }

        [Key]
        public int EmployeeID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Este Campo es obligatorio")]
        [Display(Name = "Empleado")]
        public string EmployeeName { get; set; }

        public virtual ICollection<Client> Clients { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}