using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Test.Entity.Data;

public class Context : DbContext
{
        //Constructor calling the DbContext class constructor
        public Context()
        {
        }


        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }


        //Adding Domain Classes as DbSet
        public DbSet<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                if (optionsBuilder.IsConfigured == false)
                optionsBuilder.UseNpgsql("Host=localhost;Database=Test;Username=postgres;Password=Tatva@123");
            }


}
