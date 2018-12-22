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
        //Inserir Campos Para Formulario
        public String formulario { get; set; }
        public String antes { get; set; }
        public String depois { get; set; }
        public int cumprimento { get; set; }
    }
}