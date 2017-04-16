using GNews.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        public Employee employee { get; set; }
        public int SelectedClient { get; set; }
        public int SelectedEmployee { get; set; }
        public DateTime SelectedDate { get; set; }

    }
}