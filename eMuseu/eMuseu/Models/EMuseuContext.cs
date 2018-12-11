using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace eMuseu.Models
{
    public class EMuseuContext : DbContext
    {
        public EMuseuContext() : base("name=DefaultConnection")
        {

        }

        public DbSet<Utilizador> Utilizadores { get; set; }
        public DbSet<Peca> Pecas { get; set; }
        public DbSet<Tratamentos> Tratamentos { get; set; }
    }
}