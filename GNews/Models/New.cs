using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GNews.Models
{
    public class New
    {
        [Key]
        public int NewID { get; set; }

        [Required]
        [AllowHtml]
        [DisplayFormat(NullDisplayText = "No hay noticias")]
        [Display(Name = "Noticia")]
        public string NewText { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha")]
        public DateTime Date { get; set; }

        [Display(Name = "Cliente")]
        public virtual Client Client { get; set; }
    }
}