using GNews.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace GNews.ViewModels
{
    public class NewViewModel
    {
        public New New { get; set; }
        public SelectList ListOfEmployees { get; set; }
        public SelectList ListOfClients { get; set; }
        public SelectList ListOfDates { get; set; }
        public IEnumerable<New> ListOfNews { get; set; }

        [Required(ErrorMessage = "El Cliente es requerido")]
        public int? ClientForCreating { get; set; }
        public int? SelectedClient { get; set; }
        public int? SelectedEmployee { get; set; }
        public string fecha { get; set; }
    }
}