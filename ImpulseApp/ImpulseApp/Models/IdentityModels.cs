﻿using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace ImpulseApp.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
            
        }
        public DbSet<AdModels.SimpleAdModel> SimpleAds{get; set;}
    }
}