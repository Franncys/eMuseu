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
        public String Nome { get; set; }
        public TipoUser UserTipo { get; set; }
        public String Morada { get; set; }
        public String Cidade { get; set; }
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