using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eMuseu.Models
{
    [Table("Mensagem")]
    public class Mensagem
    {
        public int MensagemID { get; set; }
        public string OrigemID { get; set; } //OrigemID e DestinoID são do tipo string porque o ID dos users é do tipo string 
        public string DestinoID { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Endereço Destinatário")]
        public string EmailDest { get; set; }
        [Required]
        [Display(Name = "Mensagem")]
        [DataType(DataType.MultilineText)]
        public string Msg { get; set; }
    }
}