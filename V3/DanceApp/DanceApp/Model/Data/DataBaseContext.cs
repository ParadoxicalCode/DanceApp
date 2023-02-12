using Microsoft.EntityFrameworkCore;

namespace DanceApp.Model.Data
{
    public class DataBaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Dancing.db");
        }

        public DbSet<AgeCategory> AgeCategories { get; set; }
        public DbSet<AgeCategoryInGroup> AgeCategoriesInGroup { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Dance> Dances { get; set; }
        public DbSet<Dancer> Dancers { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Judge> Judges { get; set; }
        public DbSet<JudgeInGroup> JudgeInGroups { get; set; }
        public DbSet<JudgeInPerformance> JudgeInPerformances { get; set; }
        public DbSet<Pair> Pairs { get; set; }
        public DbSet<PairInPerformance> PairInPerformances { get; set; }
        public DbSet<Performance> Performances { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<ScoreInDance> ScoreInDances { get; set; }
        public DbSet<Tour> Tours { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AgeCategoryInGroup>().HasKey(u => new { u.GroupId, u.AgeCategoryId });
            modelBuilder.Entity<JudgeInPerformance>().HasKey(u => new { u.PerformanceId, u.JudgeId });
            modelBuilder.Entity<JudgeInGroup>().HasKey(u => new { u.GroupId, u.JudgeId });

            modelBuilder.Entity<AgeCategory>().HasData(
                new AgeCategory { AgeCategoryId = 1, Name = "Дети 0", MinAge = 0, MaxAge = 6 },
                new AgeCategory { AgeCategoryId = 2, Name = "Дети 1", MinAge = 7, MaxAge = 9 },
                new AgeCategory { AgeCategoryId = 3, Name = "Дети 2", MinAge = 10, MaxAge = 11 },
                new AgeCategory { AgeCategoryId = 4, Name = "Юниоры 1", MinAge = 12, MaxAge = 13 },
                new AgeCategory { AgeCategoryId = 5, Name = "Юниоры 2", MinAge = 14, MaxAge = 15 },
                new AgeCategory { AgeCategoryId = 6, Name = "Молодёжь", MinAge = 16, MaxAge = 18 },
                new AgeCategory { AgeCategoryId = 7, Name = "Взрослые", MinAge = 19, MaxAge = 100 },
                new AgeCategory { AgeCategoryId = 8, Name = "Сеньоры", MinAge = 35, MaxAge = 100 },
                new AgeCategory { AgeCategoryId = 9, Name = "Гранд-сеньоры", MinAge = 61, MaxAge = 100 }
            );

            modelBuilder.Entity<City>().HasData(
                new City { CityId = 1, Title = "Новосибирск", CountryId = 1 },
                new City { CityId = 2, Title = "Москва", CountryId = 1 },
                new City { CityId = 3, Title = "Екатеринбург", CountryId = 1 },
                new City { CityId = 4, Title = "Казань", CountryId = 1 },
                new City { CityId = 5, Title = "Нижний Новгород", CountryId = 1 },
                new City { CityId = 6, Title = "Челябинск", CountryId = 1 },
                new City { CityId = 7, Title = "Красноярск", CountryId = 1 },
                new City { CityId = 8, Title = "Самара", CountryId = 1 },
                new City { CityId = 9, Title = "Уфа", CountryId = 1 },
                new City { CityId = 10, Title = "Ростов-на-Дону", CountryId = 1 },
                new City { CityId = 11, Title = "Омск", CountryId = 1 },
                new City { CityId = 12, Title = "Краснодар", CountryId = 1 },
                new City { CityId = 13, Title = "Воронеж", CountryId = 1 },
                new City { CityId = 14, Title = "Волгоград", CountryId = 1 },
                new City { CityId = 15, Title = "Пермь", CountryId = 1 }
            );

            modelBuilder.Entity<Country>().HasData(
                new Country { CountryId = 1, Title = "Россия" },
                new Country { CountryId = 2, Title = "Беларусь" },
                new Country { CountryId = 3, Title = "Казахстан" },
                new Country { CountryId = 4, Title = "Украина" },
                new Country { CountryId = 5, Title = "Грузия" },
                new Country { CountryId = 6, Title = "Таджикистан" },
                new Country { CountryId = 7, Title = "Азербайджан" },
                new Country { CountryId = 8, Title = "Туркменистан" },
                new Country { CountryId = 9, Title = "Киргизия" },
                new Country { CountryId = 10, Title = "Узбекистан" },
                new Country { CountryId = 11, Title = "Армения" },
                new Country { CountryId = 12, Title = "Молдавия" },
                new Country { CountryId = 13, Title = "Литва" },
                new Country { CountryId = 14, Title = "Латвия" },
                new Country { CountryId = 15, Title = "Эстония" }
            );

            modelBuilder.Entity<Dance>().HasData(
                new Dance { DanceId = 1, Name = "самба", ShortName = "S" },
                new Dance { DanceId = 2, Name = "ча-ча-ча", ShortName = "Ch" },
                new Dance { DanceId = 3, Name = "румба", ShortName = "R" },
                new Dance { DanceId = 4, Name = "пасодобль", ShortName = "Pd" },
                new Dance { DanceId = 5, Name = "джайв", ShortName = "J" },
                new Dance { DanceId = 6, Name = "медленный вальс", ShortName = "W" },
                new Dance { DanceId = 7, Name = "танго", ShortName = "T" },
                new Dance { DanceId = 8, Name = "венский вальс", ShortName = "Vv" },
                new Dance { DanceId = 9, Name = "медленный фокстрот", ShortName = "SF" },
                new Dance { DanceId = 10, Name = "квикстеп", ShortName = "Q" }
            );

            modelBuilder.Entity<Gender>().HasData(
                new Gender { GenderId = 1, Name = "Муж" },
                new Gender { GenderId = 2, Name = "Жен" }
            );

            modelBuilder.Entity<Position>().HasData(
                new Position { PositionId = 1, Name = "Главный судья" },
                new Position { PositionId = 2, Name = "Заместитель главного судьи" },
                new Position { PositionId = 3, Name = "Главный секретарь соревнований" },
                new Position { PositionId = 4, Name = "Линейный судья" },
                new Position { PositionId = 5, Name = "Судья при участниках" }
            );

            modelBuilder.Entity<Tour>().HasData(
                new Tour { TourId = 1, Name = "Финал" },
                new Tour { TourId = 2, Name = "Полуфинал" },
                new Tour { TourId = 3, Name = "1/4" },
                new Tour { TourId = 4, Name = "1/8" }
            );

            modelBuilder.Entity<Judge>().HasData(
                new Judge { JudgeId = 1, PositionId = 1, Surname = "Петров", Name = "Михаил", Patronymic = "Борисович", ClubId = 1 },
                new Judge { JudgeId = 2, PositionId = 2, Surname = "Зуева", Name = "Варвара", Patronymic = "Сергеевна", ClubId = 1 },
                new Judge { JudgeId = 3, PositionId = 3, Surname = "Семёнов", Name = "Михаил", Patronymic = "Сергеевич", ClubId = 1 },
                new Judge { JudgeId = 4, PositionId = 4, Surname = "Журавлёв", Name = "Денис", Patronymic = "Тимурович", ClubId = 1 },
                new Judge { JudgeId = 5, PositionId = 4, Surname = "Фролова", Name = "Анна", Patronymic = "Егоровна", ClubId = 1 },
                new Judge { JudgeId = 6, PositionId = 4, Surname = "Новикова", Name = "Екатерина", Patronymic = "Семёновна", ClubId = 1 },
                new Judge { JudgeId = 7, PositionId = 4, Surname = "Николаев", Name = "Кирилл", Patronymic = "Фёдорович", ClubId = 2 },
                new Judge { JudgeId = 8, PositionId = 5, Surname = "Симонов", Name = "Марк", Patronymic = "Романович", ClubId = 1 },
                new Judge { JudgeId = 9, PositionId = 5, Surname = "Кузнецова", Name = "Василиса", Patronymic = "Николаевна", ClubId = 1 },
                new Judge { JudgeId = 10, PositionId = 5, Surname = "Лобанова", Name = "Дарья", Patronymic = "Егоровна", ClubId = 2 }
            );

            modelBuilder.Entity<Club>().HasData(
                new Club { ClubId = 1, Title = "Сирень", CountryId = 1, CityId = 1},
                new Club { ClubId = 2, Title = "Василёк", CountryId = 1, CityId = 1 },
                new Club { ClubId = 3, Title = "Драцена", CountryId = 1, CityId = 1 },
                new Club { ClubId = 4, Title = "Жасмин", CountryId = 1, CityId = 1 },
                new Club { ClubId = 5, Title = "Ирис", CountryId = 1, CityId = 1 },
                new Club { ClubId = 6, Title = "Лаванда", CountryId = 1, CityId = 1 },
                new Club { ClubId = 7, Title = "Ромашка", CountryId = 1, CityId = 1 },
                new Club { ClubId = 8, Title = "Магнолия", CountryId = 1, CityId = 1 },
                new Club { ClubId = 9, Title = "Нарцисс", CountryId = 1, CityId = 1 },
                new Club { ClubId = 10, Title = "Петуния", CountryId = 1, CityId = 1 }
            );
        }
    }
}