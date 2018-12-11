using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eMuseu.Models
{
    [Table("Emprestimos_Pecas")]
    public class EmprestimoPecas
    {
        [Key, Column(Order = 0)]
        public int EmprestimoID { get; set; }

        [Key, Column(Order = 1)]
        public int PecaID { get; set; }

        public virtual Peca peca { get; set; }
        
        public virtual Emprestimo emprestimo { get; set; }

        public String deterioracao { get; set; }

        public EmprestimoPecas()
        {

        }
    }
}