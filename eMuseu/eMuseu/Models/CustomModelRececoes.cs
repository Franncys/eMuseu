using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eMuseu.Models
{
    public class CustomModelRececoes
    {
        public int EmprestimoID { get; set; }
        public string NomeP { get; set; }
        public string NomeU { get; set; }
        public System.DateTime? data_fim { get; set; }
        public System.DateTime? data_inicio { get; set; }
        public Boolean devolvido { get; set; }
    }
}