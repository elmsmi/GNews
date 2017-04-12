using GNews.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GNews.ViewModels
{
    public class EmployeeViewModel
    {
        public Employee employee { get; set; }

        public MultiSelectList ListOfClients { get; set; }

        public IEnumerable<int> SelectedClients { get; set; }
    }
}