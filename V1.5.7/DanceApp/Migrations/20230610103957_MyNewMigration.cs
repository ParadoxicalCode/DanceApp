using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DanceApp.Migrations
{
    /// <inheritdoc />
    public partial class MyNewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgeCategories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgeCategories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Dances",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SportsDiscipline = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    ShortName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dances", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Judges",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Character = table.Column<char>(type: "TEXT", nullable: false),
                    Surname = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Patronymic = table.Column<string>(type: "TEXT", nullable: true),
                    Club = table.Column<string>(type: "TEXT", nullable: true),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Judges", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tours",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tours", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Pairs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Number = table.Column<string>(type: "TEXT", nullable: true),
                    MaleSurname = table.Column<string>(type: "TEXT", nullable: true),
                    MaleName = table.Column<string>(type: "TEXT", nullable: true),
                    MalePatronymic = table.Column<string>(type: "TEXT", nullable: true),
                    MaleBirthday = table.Column<string>(type: "TEXT", nullable: true),
                    FemaleSurname = table.Column<string>(type: "TEXT", nullable: true),
                    FemaleName = table.Column<string>(type: "TEXT", nullable: true),
                    FemalePatronymic = table.Column<string>(type: "TEXT", nullable: true),
                    FemaleBirthday = table.Column<string>(type: "TEXT", nullable: true),
                    Club = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: false),
                    Trainer1 = table.Column<string>(type: "TEXT", nullable: false),
                    Trainer2 = table.Column<string>(type: "TEXT", nullable: true),
                    PerformanceType = table.Column<string>(type: "TEXT", nullable: false),
                    AgeCategoryID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pairs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Pairs_AgeCategories_AgeCategoryID",
                        column: x => x.AgeCategoryID,
                        principalTable: "AgeCategories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Competitions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Date = table.Column<string>(type: "TEXT", nullable: true),
                    Rank = table.Column<string>(type: "TEXT", nullable: true),
                    Manager = table.Column<string>(type: "TEXT", nullable: true),
                    City = table.Column<string>(type: "TEXT", nullable: true),
                    MainJudge = table.Column<string>(type: "TEXT", nullable: true),
                    CountingCommission = table.Column<string>(type: "TEXT", nullable: true),
                    SiteCapacity = table.Column<string>(type: "TEXT", nullable: true),
                    Fraction = table.Column<string>(type: "TEXT", nullable: true),
                    RegistrationStatus = table.Column<bool>(type: "INTEGER", nullable: false),
                    TourID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competitions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Competitions_Tours_TourID",
                        column: x => x.TourID,
                        principalTable: "Tours",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TourID = table.Column<int>(type: "INTEGER", nullable: false),
                    Number = table.Column<string>(type: "TEXT", nullable: true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    AgeCategory1 = table.Column<string>(type: "TEXT", nullable: false),
                    AgeCategory2 = table.Column<string>(type: "TEXT", nullable: true),
                    PerformanceType = table.Column<string>(type: "TEXT", nullable: false),
                    Program = table.Column<string>(type: "TEXT", nullable: false),
                    SportsDiscipline = table.Column<string>(type: "TEXT", nullable: false),
                    PairsCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Groups_Tours_TourID",
                        column: x => x.TourID,
                        principalTable: "Tours",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PairsInTour",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TourID = table.Column<int>(type: "INTEGER", nullable: false),
                    PairID = table.Column<int>(type: "INTEGER", nullable: false),
                    Select = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PairsInTour", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PairsInTour_Pairs_PairID",
                        column: x => x.PairID,
                        principalTable: "Pairs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PairsInTour_Tours_TourID",
                        column: x => x.TourID,
                        principalTable: "Tours",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DancesInGroup",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GroupID = table.Column<int>(type: "INTEGER", nullable: false),
                    DanceID = table.Column<int>(type: "INTEGER", nullable: false),
                    Select = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DancesInGroup", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DancesInGroup_Dances_DanceID",
                        column: x => x.DanceID,
                        principalTable: "Dances",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DancesInGroup_Groups_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Groups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JudgesInPerformance",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GroupID = table.Column<int>(type: "INTEGER", nullable: false),
                    DanceID = table.Column<int>(type: "INTEGER", nullable: false),
                    PerformanceNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    JudgeID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JudgesInPerformance", x => x.ID);
                    table.ForeignKey(
                        name: "FK_JudgesInPerformance_Dances_DanceID",
                        column: x => x.DanceID,
                        principalTable: "Dances",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JudgesInPerformance_Groups_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Groups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JudgesInPerformance_Judges_JudgeID",
                        column: x => x.JudgeID,
                        principalTable: "Judges",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PairsInGroup",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GroupID = table.Column<int>(type: "INTEGER", nullable: false),
                    PairID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PairsInGroup", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PairsInGroup_Groups_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Groups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PairsInGroup_Pairs_PairID",
                        column: x => x.PairID,
                        principalTable: "Pairs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PairsInPerformance",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GroupID = table.Column<int>(type: "INTEGER", nullable: false),
                    DanceID = table.Column<int>(type: "INTEGER", nullable: false),
                    PerformanceNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    PairID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PairsInPerformance", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PairsInPerformance_Dances_DanceID",
                        column: x => x.DanceID,
                        principalTable: "Dances",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PairsInPerformance_Groups_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Groups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PairsInPerformance_Pairs_PairID",
                        column: x => x.PairID,
                        principalTable: "Pairs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerformancesInDance",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DanceID = table.Column<int>(type: "INTEGER", nullable: false),
                    DanceInGroupID = table.Column<int>(type: "INTEGER", nullable: false),
                    PerformanceNumber = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformancesInDance", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PerformancesInDance_DancesInGroup_DanceInGroupID",
                        column: x => x.DanceInGroupID,
                        principalTable: "DancesInGroup",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PerformancesInDance_Dances_DanceID",
                        column: x => x.DanceID,
                        principalTable: "Dances",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AgeCategories",
                columns: new[] { "ID", "Title" },
                values: new object[,]
                {
                    { 1, "Дети 0" },
                    { 2, "Дети 1" },
                    { 3, "Дети 2" },
                    { 4, "Юниоры 1" },
                    { 5, "Юниоры 2" },
                    { 6, "Молодёжь" },
                    { 7, "Взрослые" },
                    { 8, "Сеньоры" }
                });

            migrationBuilder.InsertData(
                table: "Competitions",
                columns: new[] { "ID", "City", "CountingCommission", "Date", "Fraction", "MainJudge", "Manager", "Rank", "RegistrationStatus", "SiteCapacity", "Title", "TourID" },
                values: new object[] { 1, "Новосибирск", "Петров Михаил Борисович", "2023.06.14", "7/10", "Журавлёв Денис Тимурович", "Печурин Р.К.", "Межрегиональный турнир", true, "10", null, null });

            migrationBuilder.InsertData(
                table: "Dances",
                columns: new[] { "ID", "ShortName", "SportsDiscipline", "Title" },
                values: new object[,]
                {
                    { 1, "W", "Европейская программа", "Медленный вальс" },
                    { 2, "T", "Европейская программа", "Танго" },
                    { 3, "Vv", "Европейская программа", "Венский вальс" },
                    { 4, "SF", "Европейская программа", "Фокстрот" },
                    { 5, "Q", "Европейская программа", "Квикстеп" },
                    { 6, "S", "Латиноамериканская программа", "Самба" },
                    { 7, "Ch", "Латиноамериканская программа", "Ча-ча-ча" },
                    { 8, "R", "Латиноамериканская программа", "Румба" },
                    { 9, "Pd", "Латиноамериканская программа", "Пасодобль" },
                    { 10, "J", "Латиноамериканская программа", "Джайв" }
                });

            migrationBuilder.InsertData(
                table: "Judges",
                columns: new[] { "ID", "Character", "City", "Club", "Country", "Name", "Patronymic", "Surname" },
                values: new object[,]
                {
                    { 1, 'A', "Новосибирск", "Орион", "Россия", "Михаил", "Борисович", "Петров" },
                    { 2, 'B', "Новосибирск", "Орион", "Россия", "Варвара", "Сергеевна", "Зуева" },
                    { 3, 'C', "Новосибирск", "Орион", "Россия", "Михаил", "Сергеевич", "Семёнов" },
                    { 4, 'D', "Новосибирск", "Орион", "Россия", "Денис", "Тимурович", "Журавлёв" },
                    { 5, 'E', "Новосибирск", "Орион", "Россия", "Анна", "Егоровна", "Фролова" },
                    { 6, 'F', "Новосибирск", "Ночные лебеди", "Россия", "Екатерина", "Семёновна", "Новикова" },
                    { 7, 'G', "Новосибирск", "Ночные лебеди", "Россия", "Кирилл", "Фёдорович", "Николаев" },
                    { 8, 'H', "Новосибирск", "", "Россия", "Марк", "Романович", "Симонов" },
                    { 9, 'I', "Новосибирск", "Ночные лебеди", "Россия", "Василиса", "Николаевна", "Кузнецова" }
                });

            migrationBuilder.InsertData(
                table: "Pairs",
                columns: new[] { "ID", "AgeCategoryID", "City", "Club", "Country", "FemaleBirthday", "FemaleName", "FemalePatronymic", "FemaleSurname", "MaleBirthday", "MaleName", "MalePatronymic", "MaleSurname", "Number", "PerformanceType", "Trainer1", "Trainer2" },
                values: new object[,]
                {
                    { 1, 7, "Новосибирск", "Орион", "Россия", "2001.02.19", "Анастасия", "Руслановна", "Морозова", "", "", "", "", "31", "Соло", "Николаев К.Л.", "" },
                    { 2, 7, "Новосибирск", "Орион", "Россия", "2001.02.19", "Маргарита", "Михайловна", "Макеева", "", "", "", "", "39", "Соло", "Кузнецова Е.С.", "" },
                    { 3, 7, "Новосибирск", "Орион", "Россия", "2001.02.19", "Наталья", "Вадимовна", "Афанасьева", "2001.02.19", "Павел", "Михайлович", "Юдин", "47", "Пара", "Фролова А.В.", "" },
                    { 4, 7, "Новосибирск", "Орион", "Россия", "2001.02.19", "Алла", "Ильична", "Соловьёва", "2001.02.19", "Георгий", "Иванович", "Алешин", "21", "Пара", "Лобанова Д.С.", "" },
                    { 5, 7, "Новосибирск", "Алые розы", "Россия", "2001.02.19", "София", "Степановна", "Волкова", "2001.02.19", "Дмитрий", "Александрович", "Любимов", "4", "Пара", "Новикова И.Т", "" },
                    { 6, 7, "Новосибирск", "Алые розы", "Россия", "2001.02.19", "Диана", "Богдановна", "Куликова", "2001.02.19", "Виктор", "", "Семенов", "8", "Пара", "Кузнецова К.Р", "" },
                    { 7, 7, "Новосибирск", "Алые розы", "Россия", "2001.02.19", "Светлана", "Ильинична", "Колосова", "2001.02.19", "Станислав", "Львович", "Смирнов", "10", "Пара", "Симонов К.А.", "" },
                    { 8, 7, "Новосибирск", "Алые розы", "Россия", "2001.02.19", "Анастасия", "Григорьевна", "Фадеева", "2001.02.19", "Евгений", "Сергеевич", "Кузнецов", "13", "Пара", "Николаев К.Л.", "" },
                    { 9, 7, "Новосибирск", "Алые розы", "Россия", "2001.02.19", "Мария", "Марковна", "Синицина", "2001.02.19", "Борис", "Дмитриевич", "Карасев", "6", "Пара", "Лобанова Д.С.", "" },
                    { 10, 7, "Новосибирск", "Ночные лебеди", "Россия", "2001.02.19", "Анна", "Дмитриевна", "Захарова", "2001.02.19", "Максим", "Иванович", "Кириллов", "29", "Пара", "Симонов К.А.", "" },
                    { 11, 7, "Новосибирск", "Ночные лебеди", "Россия", "2001.02.19", "Дарья", "Дамировна", "Гуляева", "2001.02.19", "Фёдор", "Михайлович", "Попов", "34", "Пара", "Лобанова Д.С.", "" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Competitions_TourID",
                table: "Competitions",
                column: "TourID");

            migrationBuilder.CreateIndex(
                name: "IX_DancesInGroup_DanceID",
                table: "DancesInGroup",
                column: "DanceID");

            migrationBuilder.CreateIndex(
                name: "IX_DancesInGroup_GroupID",
                table: "DancesInGroup",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_TourID",
                table: "Groups",
                column: "TourID");

            migrationBuilder.CreateIndex(
                name: "IX_JudgesInPerformance_DanceID",
                table: "JudgesInPerformance",
                column: "DanceID");

            migrationBuilder.CreateIndex(
                name: "IX_JudgesInPerformance_GroupID",
                table: "JudgesInPerformance",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_JudgesInPerformance_JudgeID",
                table: "JudgesInPerformance",
                column: "JudgeID");

            migrationBuilder.CreateIndex(
                name: "IX_Pairs_AgeCategoryID",
                table: "Pairs",
                column: "AgeCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_PairsInGroup_GroupID",
                table: "PairsInGroup",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_PairsInGroup_PairID",
                table: "PairsInGroup",
                column: "PairID");

            migrationBuilder.CreateIndex(
                name: "IX_PairsInPerformance_DanceID",
                table: "PairsInPerformance",
                column: "DanceID");

            migrationBuilder.CreateIndex(
                name: "IX_PairsInPerformance_GroupID",
                table: "PairsInPerformance",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_PairsInPerformance_PairID",
                table: "PairsInPerformance",
                column: "PairID");

            migrationBuilder.CreateIndex(
                name: "IX_PairsInTour_PairID",
                table: "PairsInTour",
                column: "PairID");

            migrationBuilder.CreateIndex(
                name: "IX_PairsInTour_TourID",
                table: "PairsInTour",
                column: "TourID");

            migrationBuilder.CreateIndex(
                name: "IX_PerformancesInDance_DanceID",
                table: "PerformancesInDance",
                column: "DanceID");

            migrationBuilder.CreateIndex(
                name: "IX_PerformancesInDance_DanceInGroupID",
                table: "PerformancesInDance",
                column: "DanceInGroupID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Competitions");

            migrationBuilder.DropTable(
                name: "JudgesInPerformance");

            migrationBuilder.DropTable(
                name: "PairsInGroup");

            migrationBuilder.DropTable(
                name: "PairsInPerformance");

            migrationBuilder.DropTable(
                name: "PairsInTour");

            migrationBuilder.DropTable(
                name: "PerformancesInDance");

            migrationBuilder.DropTable(
                name: "Judges");

            migrationBuilder.DropTable(
                name: "Pairs");

            migrationBuilder.DropTable(
                name: "DancesInGroup");

            migrationBuilder.DropTable(
                name: "AgeCategories");

            migrationBuilder.DropTable(
                name: "Dances");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Tours");
        }
    }
}
