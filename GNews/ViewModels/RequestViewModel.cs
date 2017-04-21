using GNews.Models;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GNews.ViewModels
{
    public class RequestViewModel
    {
        public Request request { get; set; }

        public Employee employee { get; set; }

        public SelectList ListOfEmployees { get; set; }

        [Display(Name = "Empleado")]
        [Required(ErrorMessage = "El nombre del empleado es requerido")]
        public int SelectedEmployee { get; set; }

    }
}