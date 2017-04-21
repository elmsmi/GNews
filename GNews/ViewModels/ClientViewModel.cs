using GNews.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GNews.ViewModels
{
    public class ClientViewModel
    {
        public Client client { get; set; }

        public MultiSelectList ListOfEmployees { get; set; }

        public IEnumerable<int> SelectedEmployees { get; set; }
    }
}