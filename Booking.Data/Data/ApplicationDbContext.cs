﻿
using Booking.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Booking.Data.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        //Med DbSet kan vi sedan köra Q mot det Dbsettet (tabellen)
        public DbSet<GymClass> GymClasses => Set<GymClass>();
        public DbSet<ApplicationUserGymClass> ConnectionTableUserGyms => Set<ApplicationUserGymClass>();
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

       
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUserGymClass>()
                .HasKey(a => new {  a.ApplicationUserId, a.GymClassId });
        }
    }
}