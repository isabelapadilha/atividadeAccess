using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=192.168.1.7;Database=bancolog;User Id=sa;Password=Isabela12;");
        }

        
        public DbSet<DTO> log { get; set; }
    }
}
