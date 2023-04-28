using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.IO;
using System.Windows.Controls;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data.SqlTypes;
using System.Windows;
using System.Drawing;

#nullable disable
namespace DanceApp.Model.Data
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Читаем строку подключения из json.
            string json = File.ReadAllText("AppSettings.json");
            Json deserialized = JsonConvert.DeserializeObject<Json>(json);
            string serialized = JsonConvert.SerializeObject(deserialized);
            serialized = serialized.Substring(21);
            serialized = serialized.Substring(0, serialized.Length - 2);

            // Узнаём путь до базы данных и её имя.
            string appDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            string[] files; files = new string[1]; files[0] = "";

            bool fileExists = false;
            string connectionString = "Data Source=Default.db";
            if (serialized != "ul")
            {
                string title = Path.GetFileName(serialized.Substring(12));

                // Проверка базы данных на существование.
                fileExists = System.IO.File.Exists(System.IO.Path.Combine(appDirectory, title));
                if (fileExists == false) { connectionString = serialized; }
            }

            optionsBuilder.UseSqlite(connectionString);

            // Проверить json файл. Если он пуст или базы данных не существует, то создать
        }

        public void UpdateDatabase()
        {
            Database.Migrate();
        }

        public DbSet<AgeCategory> AgeCategories { get; set; }
        public DbSet<AgeCategoryInGroup> AgeCategoriesInGroup { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Dance> Dances { get; set; }
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
        public DbSet<TypeOfPerformance> TypesOfPerformance { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AgeCategoryInGroup>().HasKey(u => new { u.ID, u.AgeCategoryId });
            modelBuilder.Entity<JudgeInPerformance>().HasKey(u => new { u.PerformanceId, u.JudgeId });
            modelBuilder.Entity<JudgeInGroup>().HasKey(u => new { u.GroupId, u.JudgeId });

            modelBuilder.Entity<AgeCategory>().HasData(
                new AgeCategory { ID = 1, Name = "Дети 0",        MinAge = 0,  MaxAge = 6 },
                new AgeCategory { ID = 2, Name = "Дети 1",        MinAge = 7,  MaxAge = 9 },
                new AgeCategory { ID = 3, Name = "Дети 2",        MinAge = 10, MaxAge = 11 },
                new AgeCategory { ID = 4, Name = "Юниоры 1",      MinAge = 12, MaxAge = 13 },
                new AgeCategory { ID = 5, Name = "Юниоры 2",      MinAge = 14, MaxAge = 15 },
                new AgeCategory { ID = 6, Name = "Молодёжь",      MinAge = 16, MaxAge = 18 },
                new AgeCategory { ID = 7, Name = "Взрослые",      MinAge = 19, MaxAge = 100 },
                new AgeCategory { ID = 8, Name = "Сеньоры",       MinAge = 35, MaxAge = 100 },
                new AgeCategory { ID = 9, Name = "Гранд-сеньоры", MinAge = 61, MaxAge = 100 }
            );

            modelBuilder.Entity<Dance>().HasData(
                new Dance { ID = 1,  Title = "самба",              ShortName = "S" },
                new Dance { ID = 2,  Title = "ча-ча-ча",           ShortName = "Ch" },
                new Dance { ID = 3,  Title = "румба",              ShortName = "R" },
                new Dance { ID = 4,  Title = "пасодобль",          ShortName = "Pd" },
                new Dance { ID = 5,  Title = "джайв",              ShortName = "J" },
                new Dance { ID = 6,  Title = "медленный вальс",    ShortName = "W" },
                new Dance { ID = 7,  Title = "танго",              ShortName = "T" },
                new Dance { ID = 8,  Title = "венский вальс",      ShortName = "Vv" },
                new Dance { ID = 9,  Title = "медленный фокстрот", ShortName = "SF" },
                new Dance { ID = 10, Title = "квикстеп",           ShortName = "Q" }
            );

            modelBuilder.Entity<Position>().HasData(
                new Position { ID = 1, Title = "Главный судья" },
                new Position { ID = 2, Title = "Заместитель главного судьи" },
                new Position { ID = 3, Title = "Главный секретарь соревнований" },
                new Position { ID = 4, Title = "Линейный судья" },
                new Position { ID = 5, Title = "Судья при участниках" }
            );

            modelBuilder.Entity<Tour>().HasData(
                new Tour { ID = 1, Name = "Финал" },
                new Tour { ID = 2, Name = "Полуфинал" },
                new Tour { ID = 3, Name = "1/4" },
                new Tour { ID = 4, Name = "1/8" }
            );

            modelBuilder.Entity<Judge>().HasData(
                new Judge { ID = 1,  PositionId = 1, Surname = "Петров",    Name = "Михаил",    Patronymic = "Борисович", Club = "Орион"},
                new Judge { ID = 2,  PositionId = 2, Surname = "Зуева",     Name = "Варвара",   Patronymic = "Сергеевна", Club = "Орион" },
                new Judge { ID = 3,  PositionId = 3, Surname = "Семёнов",   Name = "Михаил",    Patronymic = "Сергеевич", Club = "Орион" },
                new Judge { ID = 4,  PositionId = 4, Surname = "Журавлёв",  Name = "Денис",     Patronymic = "Тимурович", Club = "Орион" },
                new Judge { ID = 5,  PositionId = 4, Surname = "Фролова",   Name = "Анна",      Patronymic = "Егоровна", Club = "Орион" },
                new Judge { ID = 6,  PositionId = 4, Surname = "Новикова",  Name = "Екатерина", Patronymic = "Семёновна", Club = "Ночные лебеди" },
                new Judge { ID = 7,  PositionId = 4, Surname = "Николаев",  Name = "Кирилл",    Patronymic = "Фёдорович", Club = "Ночные лебеди" },
                new Judge { ID = 8,  PositionId = 5, Surname = "Симонов",   Name = "Марк",      Patronymic = "Романович", Club = "Ночные лебеди" },
                new Judge { ID = 9,  PositionId = 5, Surname = "Кузнецова", Name = "Василиса",  Patronymic = "Николаевна", Club = "Ночные лебеди" },
                new Judge { ID = 10, PositionId = 5, Surname = "Лобанова",  Name = "Дарья",     Patronymic = "Егоровна", Club = "Ночные лебеди" }
            );

            modelBuilder.Entity<Competition>().HasData(
                new Competition { ID = 1, Title = "Зимний бал", StartDate = "21.05.2023", StartTime="18:00", Manager = "Печурин Р.К.", Address = "ул. Максима Горького, д.16", Club = "Ночная звезда", SettingsToJudges = 0, PairsToGroups = 0 }
            );

            modelBuilder.Entity<Group>().HasData(
                new Group { ID = 1, Number = "65", Title ="Дети 1 + Дети 0",      Program="2 танца",   NumberOfOutputs="54",   DancersCount="54",   TypeOfPerformanceId=2 },
                new Group { ID = 2, Number = "12", Title = "Юниоры 1 + Юниоры 2", Program = "2 танца", NumberOfOutputs = "11", DancersCount = "22", TypeOfPerformanceId = 1 }
            );

            modelBuilder.Entity<TypeOfPerformance>().HasData(
                new TypeOfPerformance { ID = 1, Title = "Пара" },
                new TypeOfPerformance { ID = 2, Title = "Соло" }
            );

            modelBuilder.Entity<Pair>().HasData(
                new Pair { ID = 1,  Number = "31", MaleSurname = "Чернышев", MaleName = "Артём",     MalePatronymic = "Фёдорович",     MaleBirthday = "19.02.2001", FemaleSurname = "Морозова",  FemaleName = "Анастасия", FemalePatronymic = "Руслановна",  FemaleBirthday = "19.02.2001", Club = "Орион", City = "Новосибирск", Country = "Россия", Trainer1 = "Николаев К.Л.", Trainer2 = "" },
                new Pair { ID = 2,  Number = "39", MaleSurname = "Осипов",   MaleName = "Тимур",     MalePatronymic = "Тимофеевич",    MaleBirthday = "19.02.2001", FemaleSurname = "Макеева",   FemaleName = "Маргарита", FemalePatronymic = "Михайловна",  FemaleBirthday = "19.02.2001", Club = "Орион", City = "Новосибирск", Country = "Россия", Trainer1 = "Кузнецова Е.С.", Trainer2 = "" },
                new Pair { ID = 3,  Number = "47", MaleSurname = "Юдин",     MaleName = "Павел",     MalePatronymic = "Михайлович",    MaleBirthday = "19.02.2001", FemaleSurname = "Журавлева", FemaleName = "Елизавета", FemalePatronymic = "",            FemaleBirthday = "19.02.2001", Club = "Орион", City = "Новосибирск", Country = "Россия", Trainer1 = "Фролова А.В.", Trainer2 = "" },
                new Pair { ID = 4,  Number = "21", MaleSurname = "Алешин",   MaleName = "Георгий",   MalePatronymic = "Иванович",      MaleBirthday = "19.02.2001", FemaleSurname = "Савина",    FemaleName = "Варвара",   FemalePatronymic = "Артёмовна",   FemaleBirthday = "19.02.2001", Club = "Орион", City = "Новосибирск", Country = "Россия", Trainer1 = "Лобанова Д.С.", Trainer2 = "" },
                new Pair { ID = 5,  Number = "4",  MaleSurname = "Любимов",  MaleName = "Дмитрий",   MalePatronymic = "Александрович", MaleBirthday = "19.02.2001", FemaleSurname = "Волкова",   FemaleName = "София",     FemalePatronymic = "Степановна",  FemaleBirthday = "19.02.2001", Club = "Алые розы", City = "Новосибирск", Country = "Россия", Trainer1 = "Новикова И.Т", Trainer2 = "" },
                new Pair { ID = 6,  Number = "8",  MaleSurname = "Семенов",  MaleName = "Виктор",    MalePatronymic = "",              MaleBirthday = "19.02.2001", FemaleSurname = "Куликова",  FemaleName = "Диана",     FemalePatronymic = "Богдановна",  FemaleBirthday = "19.02.2001", Club = "Алые розы", City = "Новосибирск", Country = "Россия", Trainer1 = "Кузнецова К.Р", Trainer2 = "" },
                new Pair { ID = 7,  Number = "10", MaleSurname = "Смирнов",  MaleName = "Станислав", MalePatronymic = "Львович",       MaleBirthday = "19.02.2001", FemaleSurname = "Колосова",  FemaleName = "Светлана",  FemalePatronymic = "Ильинична",   FemaleBirthday = "19.02.2001", Club = "Алые розы", City = "Новосибирск", Country = "Россия", Trainer1 = "Симонов К.А.", Trainer2 = "" },
                new Pair { ID = 8,  Number = "13", MaleSurname = "Кузнецов", MaleName = "Евгений",   MalePatronymic = "Сергеевич",     MaleBirthday = "19.02.2001", FemaleSurname = "Фадеева",   FemaleName = "Анастасия", FemalePatronymic = "Григорьевна", FemaleBirthday = "19.02.2001", Club = "Алые розы", City = "Новосибирск", Country = "Россия", Trainer1 = "Николаев К.Л.", Trainer2 = "" },
                new Pair { ID = 9,  Number = "6",  MaleSurname = "Карасев",  MaleName = "Борис",     MalePatronymic = "Дмитриевич",    MaleBirthday = "19.02.2001", FemaleSurname = "Синицина",  FemaleName = "Мария",     FemalePatronymic = "Марковна",    FemaleBirthday = "19.02.2001", Club = "Алые розы", City = "Новосибирск", Country = "Россия", Trainer1 = "Лобанова Д.С.", Trainer2 = "" },
                new Pair { ID = 10, Number = "29", MaleSurname = "Кириллов", MaleName = "Максим",    MalePatronymic = "Иванович",      MaleBirthday = "19.02.2001", FemaleSurname = "Захарова",  FemaleName = "Анна",      FemalePatronymic = "Дмитриевна",  FemaleBirthday = "19.02.2001", Club = "Ночные лебеди", City = "Новосибирск", Country = "Россия", Trainer1 = "Симонов К.А.", Trainer2 = "" },
                new Pair { ID = 11, Number = "34", MaleSurname = "Попов",    MaleName = "Фёдор",     MalePatronymic = "Михайлович",    MaleBirthday = "19.02.2001", FemaleSurname = "Гуляева",   FemaleName = "Дарья",     FemalePatronymic = "Дамировна",   FemaleBirthday = "19.02.2001", Club = "Ночные лебеди", City = "Новосибирск", Country = "Россия", Trainer1 = "Лобанова Д.С.", Trainer2 = "" }
            );
        }
    }
}