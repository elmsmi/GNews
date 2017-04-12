using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GNews.Models
{
    public class Client
    {
        [Key]
        public int ClientID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Este Campo es obligatorio")]
        [MaxLength(100)]
        [Display(Name = "Cliente")]
        public string ClientName { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<New> News { get; set; }
    }
}