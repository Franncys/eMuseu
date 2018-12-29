using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eMuseu.Models
{
    [Table("Rececoes")]
    public class Rececao
    {
        [Key]
        public int rececaoID { get; set; }
        //[ForeignKey("PecaID")]
        public Peca PecaID { get; set; }
        [Display(Name = "Descrição")]
        [DataType(DataType.MultilineText)]
        public String formulario { get; set; }
        [Display(Name = "Estado Anterior")]
        public String antes { get; set; }
        [Display(Name = "Estado Atual")]
        public String depois { get; set; }
        [Display(Name = "Cumprimento de Data")]
        public int cumprimento { get; set; }
    }
}