using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace SQLite_Probe
{
    public class BloggingContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Club> Clubs { get; set; }

        public BloggingContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Skating.db");
        }
    }

    public class City
    {
        
        public int CityId { get; set; }
        public string Name { get; set; }

        public List<Club> Clubs { get; } = new();
    }

    public class Club
    {
        public int ClubId { get; set; }
        public string Name { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }
    }
}












