using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FashionStore.DAL.Context
{
    public class Auth_Context : IdentityDbContext<AppUser>
    {
        public Auth_Context(DbContextOptions<Auth_Context> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.ApplyConfigurationsFromAssembly(typeof(Auth_Context).Assembly);
            builder.Entity<AppUser>().ToTable("Users");

            builder.Entity<AppUser>().Property(A => A.FirstName)
                   .HasMaxLength(255)
                   .IsRequired(false);

            builder.Entity<AppUser>().Property(A => A.LastName)
                   .HasMaxLength(255)
                   .IsRequired(false);

            builder.Entity<AppUser>().Property(A => A.Country)
                   .HasMaxLength(255)
                   .IsRequired(false);

            builder.Entity<AppUser>().Property(A => A.City)
                   .HasMaxLength(255)
                   .IsRequired(false);

            builder.Entity<AppUser>().Property(A => A.AddressDetails)
                   .HasMaxLength(255)
                   .IsRequired(false);
        }


    }
}
