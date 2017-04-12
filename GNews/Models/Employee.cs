using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GNews.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Este Campo es obligatorio")]
        [Display(Name = "Empleado")]
        public string EmployeeName { get; set; }

        public virtual ICollection<Client> Clients { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}