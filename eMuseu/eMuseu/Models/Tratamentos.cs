using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eMuseu.Models
{
    [Table("Tratamentos")]
    public class Tratamentos
    {
        [Key]
        public int TratamentoID { get; set; }
        public String NomeTratamento { get; set; }
        [ForeignKey("Peca")]
        public IList<Peca> PecaID { get; set; }

        public Tratamentos()
        {

        }
    }
}