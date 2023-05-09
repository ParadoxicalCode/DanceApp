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
            if (serialized != "ul")
            {
                string connectionString = serialized;
                optionsBuilder.UseSqlite(connectionString);
            }
        }

        public void UpdateDatabase()
        {
            Database.Migrate();
        }

        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Dance> Dances { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Judge> Judges { get; set; }
        public DbSet<JudgeInGroup> JudgeInGroups { get; set; }
        public DbSet<JudgeInPerformance> JudgeInPerformances { get; set; }
        public DbSet<Pair> Pairs { get; set; }
        public DbSet<PairInPerformance> PairInPerformances { get; set; }
        public DbSet<Performance> Performances { get; set; }
        public DbSet<ScoreInDance> ScoreInDances { get; set; }
        public DbSet<Tour> Tours { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JudgeInPerformance>().HasKey(u => new { u.PerformanceId, u.JudgeId });
            modelBuilder.Entity<JudgeInGroup>().HasKey(u => new { u.GroupId, u.JudgeId });

            modelBuilder.Entity<Competition>().HasData(
                new Competition { ID = 1, Title = "Зимний бал", StartDate = "2023.21.05", StartTime = "18:00", Manager = "Печурин Р.К.", Address = "ул. Максима Горького, д.16", RegistrationSwitch = true, UpdateGroups = true, SiteCapacity = 5 }
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

            modelBuilder.Entity<Tour>().HasData(
                new Tour { ID = 1, Name = "Финал" },
                new Tour { ID = 2, Name = "Полуфинал" },
                new Tour { ID = 3, Name = "1/4" },
                new Tour { ID = 4, Name = "1/8" }
            );

            modelBuilder.Entity<Judge>().HasData(
                new Judge { ID = 1,  Character = 'A', Surname = "Петров",    Name = "Михаил",    Patronymic = "Борисович",  Club = "Орион",         City = "Новосибирск", Country = "Россия" },
                new Judge { ID = 2,  Character = 'B', Surname = "Зуева",     Name = "Варвара",   Patronymic = "Сергеевна",  Club = "Орион",         City = "Новосибирск", Country = "Россия" },
                new Judge { ID = 3,  Character = 'C', Surname = "Семёнов",   Name = "Михаил",    Patronymic = "Сергеевич",  Club = "Орион",         City = "Новосибирск", Country = "Россия" },
                new Judge { ID = 4,  Character = 'D', Surname = "Журавлёв",  Name = "Денис",     Patronymic = "Тимурович",  Club = "Орион",         City = "Новосибирск", Country = "Россия" },
                new Judge { ID = 5,  Character = 'E', Surname = "Фролова",   Name = "Анна",      Patronymic = "Егоровна",   Club = "Орион",         City = "Новосибирск", Country = "Россия" },
                new Judge { ID = 6,  Character = 'F', Surname = "Новикова",  Name = "Екатерина", Patronymic = "Семёновна",  Club = "Ночные лебеди", City = "Новосибирск", Country = "Россия" },
                new Judge { ID = 7,  Character = 'G', Surname = "Николаев",  Name = "Кирилл",    Patronymic = "Фёдорович",  Club = "Ночные лебеди", City = "Новосибирск", Country = "Россия" },
                new Judge { ID = 8,  Character = 'H', Surname = "Симонов",   Name = "Марк",      Patronymic = "Романович",  Club = "",              City = "Новосибирск", Country = "Россия" },
                new Judge { ID = 9,  Character = 'I', Surname = "Кузнецова", Name = "Василиса",  Patronymic = "Николаевна", Club = "Ночные лебеди", City = "Новосибирск", Country = "Россия" }
            );

            modelBuilder.Entity<Pair>().HasData(
                new Pair { ID = 1,  Number = "31", MaleSurname = "",         MaleName = "",          MalePatronymic = "",              MaleBirthday = "",           FemaleSurname = "Морозова",  FemaleName = "Анастасия", FemalePatronymic = "Руслановна",  FemaleBirthday = "2001.02.19", PerformanceType = " (Соло)", Club = "Орион",        Country = "Россия", City = "Новосибирск", Trainer1 = "Николаев К.Л.",  Trainer2 = "", AgeCategory = "Взрослые" },
                new Pair { ID = 2,  Number = "39", MaleSurname = "",         MaleName = "",          MalePatronymic = "",              MaleBirthday = "",           FemaleSurname = "Макеева",   FemaleName = "Маргарита", FemalePatronymic = "Михайловна",  FemaleBirthday = "2001.02.19", PerformanceType = " (Соло)", Club = "Орион",        Country = "Россия", City = "Новосибирск", Trainer1 = "Кузнецова Е.С.", Trainer2 = "", AgeCategory = "Взрослые" },
                new Pair { ID = 3,  Number = "47", MaleSurname = "Юдин",     MaleName = "Павел",     MalePatronymic = "Михайлович",    MaleBirthday = "2001.02.19", FemaleSurname = "",          FemaleName = "",          FemalePatronymic = "",            FemaleBirthday = "",           PerformanceType = " (Соло)", Club = "Орион",        Country = "Россия", City = "Новосибирск", Trainer1 = "Фролова А.В.",   Trainer2 = "", AgeCategory = "Взрослые" },
                new Pair { ID = 4,  Number = "21", MaleSurname = "Алешин",   MaleName = "Георгий",   MalePatronymic = "Иванович",      MaleBirthday = "2001.02.19", FemaleSurname = "",          FemaleName = "",          FemalePatronymic = "",            FemaleBirthday = "",           PerformanceType = " (Соло)", Club = "Орион",        Country = "Россия", City = "Новосибирск", Trainer1 = "Лобанова Д.С.",  Trainer2 = "", AgeCategory = "Взрослые" },
                new Pair { ID = 5,  Number = "4",  MaleSurname = "Любимов",  MaleName = "Дмитрий",   MalePatronymic = "Александрович", MaleBirthday = "2001.02.19", FemaleSurname = "Волкова",   FemaleName = "София",     FemalePatronymic = "Степановна",  FemaleBirthday = "2001.02.19", PerformanceType = "",       Club = "Алые розы",     Country = "Россия", City = "Новосибирск", Trainer1 = "Новикова И.Т",   Trainer2 = "", AgeCategory = "Взрослые" },
                new Pair { ID = 6,  Number = "8",  MaleSurname = "Семенов",  MaleName = "Виктор",    MalePatronymic = "",              MaleBirthday = "2001.02.19", FemaleSurname = "Куликова",  FemaleName = "Диана",     FemalePatronymic = "Богдановна",  FemaleBirthday = "2001.02.19", PerformanceType = "",       Club = "Алые розы",     Country = "Россия", City = "Новосибирск", Trainer1 = "Кузнецова К.Р",  Trainer2 = "", AgeCategory = "Взрослые" },
                new Pair { ID = 7,  Number = "10", MaleSurname = "Смирнов",  MaleName = "Станислав", MalePatronymic = "Львович",       MaleBirthday = "2001.02.19", FemaleSurname = "Колосова",  FemaleName = "Светлана",  FemalePatronymic = "Ильинична",   FemaleBirthday = "2001.02.19", PerformanceType = "",       Club = "Алые розы",     Country = "Россия", City = "Новосибирск", Trainer1 = "Симонов К.А.",   Trainer2 = "", AgeCategory = "Взрослые" },
                new Pair { ID = 8,  Number = "13", MaleSurname = "Кузнецов", MaleName = "Евгений",   MalePatronymic = "Сергеевич",     MaleBirthday = "2001.02.19", FemaleSurname = "Фадеева",   FemaleName = "Анастасия", FemalePatronymic = "Григорьевна", FemaleBirthday = "2001.02.19", PerformanceType = "",       Club = "Алые розы",     Country = "Россия", City = "Новосибирск", Trainer1 = "Николаев К.Л.",  Trainer2 = "", AgeCategory = "Взрослые" },
                new Pair { ID = 9,  Number = "6",  MaleSurname = "Карасев",  MaleName = "Борис",     MalePatronymic = "Дмитриевич",    MaleBirthday = "2001.02.19", FemaleSurname = "Синицина",  FemaleName = "Мария",     FemalePatronymic = "Марковна",    FemaleBirthday = "2001.02.19", PerformanceType = "",       Club = "Алые розы",     Country = "Россия", City = "Новосибирск", Trainer1 = "Лобанова Д.С.",  Trainer2 = "", AgeCategory = "Взрослые" },
                new Pair { ID = 10, Number = "29", MaleSurname = "Кириллов", MaleName = "Максим",    MalePatronymic = "Иванович",      MaleBirthday = "2001.02.19", FemaleSurname = "Захарова",  FemaleName = "Анна",      FemalePatronymic = "Дмитриевна",  FemaleBirthday = "2001.02.19", PerformanceType = "",       Club = "Ночные лебеди", Country = "Россия", City = "Новосибирск", Trainer1 = "Симонов К.А.",   Trainer2 = "", AgeCategory = "Взрослые" },
                new Pair { ID = 11, Number = "34", MaleSurname = "Попов",    MaleName = "Фёдор",     MalePatronymic = "Михайлович",    MaleBirthday = "2001.02.19", FemaleSurname = "Гуляева",   FemaleName = "Дарья",     FemalePatronymic = "Дамировна",   FemaleBirthday = "2001.02.19", PerformanceType = "",       Club = "Ночные лебеди", Country = "Россия", City = "Новосибирск", Trainer1 = "Лобанова Д.С.",  Trainer2 = "", AgeCategory = "Взрослые" }
            );
        }
    }
}