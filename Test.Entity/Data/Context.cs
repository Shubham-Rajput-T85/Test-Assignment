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
        public virtual DbSet<User> Users { get; set; }

        public virtual  DbSet<Concert> Concerts { get; set; }

        public virtual DbSet<Ticket> Tickets { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                if (optionsBuilder.IsConfigured == false)
                optionsBuilder.UseNpgsql("Host=localhost;Database=Test;Username=postgres;Password=Tatva@123");
            }


}
