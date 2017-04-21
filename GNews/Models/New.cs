using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GNews.Models
{
    public class New
    {
        [Key]
        public int NewID { get; set; }

        [Required(ErrorMessage = "La noticia es requerida")]
        [AllowHtml]
        [Display(Name = "Noticia")]
        public string NewText { get; set; }

        [Required(ErrorMessage = "La fecha es requerida")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha")]
        public DateTime Date { get; set; }

        [Display(Name = "Cliente")]
        public virtual Client Client { get; set; }
    }
}