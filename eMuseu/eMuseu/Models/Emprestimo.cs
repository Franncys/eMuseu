using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eMuseu.Models
{
    [Table("Emprestimos")]
    public class Emprestimo
    {
        [Key]
        public int EmprestimoID { get; set; }
        [Required]
        [Display(Name = "Data de Inicio")]
        [DataType(DataType.Date)]
        public System.DateTime data_inicio { get; set; }
        [Required]
        [Display(Name = "Fim de Emprestimo")]
        [DataType(DataType.Date)]
        public System.DateTime data_fim { get; set; }
        public Boolean validado { get; set; }
        public Boolean devolvido { get; set; }
        public virtual ICollection<Emp_Peca> Emp_Peca { get; set; }
    }
}