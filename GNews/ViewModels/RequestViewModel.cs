using GNews.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GNews.ViewModels
{
    public class RequestViewModel
    {
        public Request request { get; set; }

        public Employee employee { get; set; }

        public SelectList ListOfEmployees { get; set; }

        [Display(Name = "Empleado")]
        public int SelectedEmployee { get; set; }

    }
}