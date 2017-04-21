using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GNews.Models
{
    public class Client
    {
        public Client()
        {
            Employees = new HashSet<Employee>();
        }

        [Key]
        public int ClientID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Este Campo es obligatorio")]
        [MaxLength(100)]
        [Display(Name = "Cliente")]
        public string ClientName { get; set; }

        [Display(Name = "Empleados")]
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<New> News { get; set; }
    }
}