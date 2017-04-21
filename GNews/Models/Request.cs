using System.ComponentModel.DataAnnotations;

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

        [Required(ErrorMessage = "La petición es requerida")]
        [Display(Name = "Petición")]
        [StringLength(300, ErrorMessage = "El tamaño maximo para una petición es 300 characteres")]
        public string RequestText { get; set; }

        [Required(ErrorMessage = "El nombre del empleado es requerido")]
        [Display(Name = "Empleado")]
        public virtual Employee Employee { get; set; }

        [Display(Name = "Resuelta")]
        public bool Resolved { get; set; }
    }
}