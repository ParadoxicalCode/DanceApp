﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Skating
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SkatingEntities : DbContext
    {
        public SkatingEntities()
            : base("name=SkatingEntities")
        {
        }

        private static SkatingEntities _context;
        public static SkatingEntities GetContext()
        {
            if (_context == null)
                _context = new SkatingEntities();
            return _context;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Clubs> Clubs { get; set; }
        public virtual DbSet<Competitions> Competitions { get; set; }
        public virtual DbSet<Dancers> Dancers { get; set; }
        public virtual DbSet<Dances> Dances { get; set; }
        public virtual DbSet<DanceScore> DanceScore { get; set; }
        public virtual DbSet<Gender> Gender { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<Judges> Judges { get; set; }
        public virtual DbSet<Pairs> Pairs { get; set; }
        public virtual DbSet<Programs> Programs { get; set; }
        public virtual DbSet<Results> Results { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Tours> Tours { get; set; }
        public virtual DbSet<TypesOfPerformance> TypesOfPerformance { get; set; }
    }
}