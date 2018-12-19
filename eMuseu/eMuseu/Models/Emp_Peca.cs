using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eMuseu.Models
{
    public class Emp_Peca
    {
        public int PecaID { get; set; }
        public int EmprestimoID { get; set; }
        public string Estado { get; set; }
        public virtual Peca Peca { get; set; }
        public virtual Emprestimo Emprestimo { get; set; }
    }
}