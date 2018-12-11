using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace eMuseu.Models
{
    public class EMuseuContext : ApplicationDbContext
    {
        public EMuseuContext() //:base("name=DefaultConnection")
        {

        }

        //public DbSet<Utilizador> Utilizadores { get; set; }
        public DbSet<Peca> Pecas { get; set; }
        public DbSet<Tratamentos> Tratamentos { get; set; }
        public DbSet<EmprestimoPecas> EmprestimoPecas { get; set; }
        public DbSet<Rececao> Rececoes { get; set; }
        public DbSet<Emprestimo> emprestimos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        //public System.Data.Entity.DbSet<eMuseu.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}