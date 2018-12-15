﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace eMuseu.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        //public enum TipoUser { administrador, especialista, registado }
        //public string UserName { get; set; }
        public string NomeP { get; set; }
        public string NomeU { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Cidade { get; set; }
        public string Morada { get; set; }
        //public TipoUser UserTipo { get; set; }
        public Boolean aprovado { get; set; }
        public string RoleName { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {

            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string roleName) : base(roleName) { }
       
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        //public DbSet<Utilizador> Utilizadores { get; set; }
        public DbSet<Peca> Pecas { get; set; }
        public DbSet<Tratamentos> Tratamentos { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }



        //public System.Data.Entity.DbSet<eMuseu.Models.RoleViewModel> RoleViewModels { get; set; }

        // public System.Data.Entity.DbSet<eMuseu.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}