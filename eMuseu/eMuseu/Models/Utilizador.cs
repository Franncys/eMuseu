using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eMuseu.Models
{
    [Table("Utilizadores")]
    public class Utilizador
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        public String Nome { get; set; }
        [Required]
        [Display(Name = "Tipo  de Utilizador")]
        public TipoUser UserTipo { get; set; }
        [Required]
        public String Morada { get; set; }
        [Required]
        public String Cidade { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime DataNascimento { get; set; }
        public int aprovado { get; set; }
        public enum TipoUser
        {
            Registado,
            Especialista,
            Administrador
        }

        public Utilizador()
        {
            aprovado = 0;
        }
    }
}