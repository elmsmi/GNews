using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GNews.Models
{
    public class Request
    {

        public Request()
        {
            Employee = new Employee();
        }

        [Key]
        public int RequestID { get; set; }

        [Required]
        [Display(Name = "Petición")]
        public string RequestText { get; set; }

        [Required]
        [Display(Name = "Empleado")]
        public virtual Employee Employee { get; set; }

        [Display(Name = "Resuelta")]
        public bool Resolved { get; set; }
    }
}