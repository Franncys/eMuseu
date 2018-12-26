using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eMuseu.Models
{
    [Table("Pecas")]
    public class Peca
    {
        [Key]
        public int PecaID { get; set; }
        public String nomePeca { get; set; }
        public int Periodo { get; set; }
        public String Zona { get; set; }
        public String Estado { get; set; }
        public TipoPeca PecaTipo { get; set; }
        [ForeignKey("TratamentoID")]
        public IList<Tratamentos> TratamentoID { get; set; }
       /* [ForeignKey("EmprestimoID")]
        public IList<Emprestimo> EmprestimoID { get; set; }*/
        public virtual ICollection<Emp_Peca> Emp_Peca { get; set; }

        public enum TipoPeca
        {
            tipo1,
            tipo2
        }

        public Peca()
        {

        }

    }
}