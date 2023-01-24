using Microsoft.EntityFrameworkCore;

namespace DanceApp.Model.Data
{
    public class ApplicationContext: DbContext
    {
        // Сущность.
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Competition> Competitions { get; set; }

        // Конструктор (инициализатор).
        public ApplicationContext()
        {
            // Если БД не существует, то создать БД.
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        // Подключение к БД.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DanceDB;Trusted_Connection=True;");
        }
    }
}
