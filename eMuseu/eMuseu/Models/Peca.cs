﻿using System;
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
        [Display(Name = "Nome da Peça")]
        public String nomePeca { get; set; }
        public int Periodo { get; set; }
        public String Zona { get; set; }
        public String Estado { get; set; }
        [Display(Name = "Tipo de Peça")]
        public TipoPeca PecaTipo { get; set; }
        public virtual ICollection<Emp_Peca> Emp_Peca { get; set; }

        public enum TipoPeca
        {
            Quadro,
            Rádio,
            Instrumento,
            Arma,
            Louça,
            Móvel,
            Veiculo
            
            
        }

        public Peca()
        {

        }

    }
}