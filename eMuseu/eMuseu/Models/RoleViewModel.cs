using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace eMuseu.Models
{
    public class RoleViewModel
    {

        public RoleViewModel() {}

        public RoleViewModel(IdentityRole role)
        {
            Id = role.Id;
            Name = role.Name;
        }

        public string Id { get; set; }
        [Display(Name = "Nome")]
        public string Name { get; set; }
    }
}