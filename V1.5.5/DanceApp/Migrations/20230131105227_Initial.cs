using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DanceApp.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgeCategories",
                columns: table => new
                {
                    AgeCategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    MinAge = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxAge = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgeCategories", x => x.AgeCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "Dances",
                columns: table => new
                {
                    DanceId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    ShortName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dances", x => x.DanceId);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    GenderId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.GenderId);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    PositionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.PositionId);
                });

            migrationBuilder.CreateTable(
                name: "Tours",
                columns: table => new
                {
                    TourId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tours", x => x.TourId);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    CityId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CountryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CityId);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clubs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CountryId = table.Column<int>(type: "INTEGER", nullable: false),
                    CityId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clubs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clubs_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Clubs_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Competitions",
                columns: table => new
                {
                    CompetitionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    StartDate = table.Column<string>(type: "TEXT", nullable: false),
                    Manager = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    ClubId = table.Column<int>(type: "INTEGER", nullable: true),
                    StartTime = table.Column<string>(type: "TEXT", nullable: true),
                    Selection = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competitions", x => x.CompetitionId);
                    table.ForeignKey(
                        name: "FK_Competitions_Clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompetitionId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Number = table.Column<string>(type: "TEXT", nullable: true),
                    TourId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.GroupId);
                    table.ForeignKey(
                        name: "FK_Groups_Competitions_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competitions",
                        principalColumn: "CompetitionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Groups_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "TourId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Judges",
                columns: table => new
                {
                    JudgeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompetitionId = table.Column<int>(type: "INTEGER", nullable: false),
                    Surname = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Patronymic = table.Column<string>(type: "TEXT", nullable: true),
                    PositionId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClubId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Judges", x => x.JudgeId);
                    table.ForeignKey(
                        name: "FK_Judges_Clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Judges_Competitions_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competitions",
                        principalColumn: "CompetitionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Judges_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "PositionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pairs",
                columns: table => new
                {
                    PairId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Number = table.Column<string>(type: "TEXT", nullable: false),
                    CompetitionId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pairs", x => x.PairId);
                    table.ForeignKey(
                        name: "FK_Pairs_Competitions_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competitions",
                        principalColumn: "CompetitionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AgeCategoriesInGroup",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "INTEGER", nullable: false),
                    AgeCategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgeCategoriesInGroup", x => new { x.GroupId, x.AgeCategoryId });
                    table.ForeignKey(
                        name: "FK_AgeCategoriesInGroup_AgeCategories_AgeCategoryId",
                        column: x => x.AgeCategoryId,
                        principalTable: "AgeCategories",
                        principalColumn: "AgeCategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgeCategoriesInGroup_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Performances",
                columns: table => new
                {
                    PerformanceId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompetitionId = table.Column<int>(type: "INTEGER", nullable: false),
                    GroupId = table.Column<int>(type: "INTEGER", nullable: false),
                    TourId = table.Column<int>(type: "INTEGER", nullable: false),
                    Number = table.Column<string>(type: "TEXT", nullable: false),
                    DanceId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Performances", x => x.PerformanceId);
                    table.ForeignKey(
                        name: "FK_Performances_Competitions_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competitions",
                        principalColumn: "CompetitionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Performances_Dances_DanceId",
                        column: x => x.DanceId,
                        principalTable: "Dances",
                        principalColumn: "DanceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Performances_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Performances_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "TourId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JudgeInGroups",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "INTEGER", nullable: false),
                    JudgeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JudgeInGroups", x => new { x.GroupId, x.JudgeId });
                    table.ForeignKey(
                        name: "FK_JudgeInGroups_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JudgeInGroups_Judges_JudgeId",
                        column: x => x.JudgeId,
                        principalTable: "Judges",
                        principalColumn: "JudgeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dancers",
                columns: table => new
                {
                    DancerId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompetitionId = table.Column<int>(type: "INTEGER", nullable: false),
                    Number = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Surname = table.Column<string>(type: "TEXT", nullable: false),
                    Patronymic = table.Column<string>(type: "TEXT", nullable: true),
                    Birthday = table.Column<string>(type: "TEXT", nullable: false),
                    GenderId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClubId = table.Column<int>(type: "INTEGER", nullable: false),
                    GroupId = table.Column<int>(type: "INTEGER", nullable: true),
                    PairId = table.Column<int>(type: "INTEGER", nullable: true),
                    Trainer = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dancers", x => x.DancerId);
                    table.ForeignKey(
                        name: "FK_Dancers_Clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dancers_Competitions_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competitions",
                        principalColumn: "CompetitionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dancers_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "GenderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dancers_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId");
                    table.ForeignKey(
                        name: "FK_Dancers_Pairs_PairId",
                        column: x => x.PairId,
                        principalTable: "Pairs",
                        principalColumn: "PairId");
                });

            migrationBuilder.CreateTable(
                name: "JudgeInPerformances",
                columns: table => new
                {
                    PerformanceId = table.Column<int>(type: "INTEGER", nullable: false),
                    JudgeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JudgeInPerformances", x => new { x.PerformanceId, x.JudgeId });
                    table.ForeignKey(
                        name: "FK_JudgeInPerformances_Judges_JudgeId",
                        column: x => x.JudgeId,
                        principalTable: "Judges",
                        principalColumn: "JudgeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JudgeInPerformances_Performances_PerformanceId",
                        column: x => x.PerformanceId,
                        principalTable: "Performances",
                        principalColumn: "PerformanceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PairInPerformances",
                columns: table => new
                {
                    PairInPerformanceId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PerformanceId = table.Column<int>(type: "INTEGER", nullable: false),
                    PairId = table.Column<int>(type: "INTEGER", nullable: false),
                    Disqualification = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PairInPerformances", x => x.PairInPerformanceId);
                    table.ForeignKey(
                        name: "FK_PairInPerformances_Pairs_PairId",
                        column: x => x.PairId,
                        principalTable: "Pairs",
                        principalColumn: "PairId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PairInPerformances_Performances_PerformanceId",
                        column: x => x.PerformanceId,
                        principalTable: "Performances",
                        principalColumn: "PerformanceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScoreInDances",
                columns: table => new
                {
                    ScoreInDanceId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PairId = table.Column<int>(type: "INTEGER", nullable: false),
                    CompetitionId = table.Column<int>(type: "INTEGER", nullable: false),
                    PerformanceId = table.Column<int>(type: "INTEGER", nullable: false),
                    DanceId = table.Column<int>(type: "INTEGER", nullable: false),
                    JudgeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Score = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoreInDances", x => x.ScoreInDanceId);
                    table.ForeignKey(
                        name: "FK_ScoreInDances_Competitions_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competitions",
                        principalColumn: "CompetitionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScoreInDances_Dances_DanceId",
                        column: x => x.DanceId,
                        principalTable: "Dances",
                        principalColumn: "DanceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScoreInDances_Judges_JudgeId",
                        column: x => x.JudgeId,
                        principalTable: "Judges",
                        principalColumn: "JudgeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScoreInDances_Pairs_PairId",
                        column: x => x.PairId,
                        principalTable: "Pairs",
                        principalColumn: "PairId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScoreInDances_Performances_PerformanceId",
                        column: x => x.PerformanceId,
                        principalTable: "Performances",
                        principalColumn: "PerformanceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AgeCategories",
                columns: new[] { "AgeCategoryId", "MaxAge", "MinAge", "Name" },
                values: new object[,]
                {
                    { 1, 6, 0, "Дети 0" },
                    { 2, 9, 7, "Дети 1" },
                    { 3, 11, 10, "Дети 2" },
                    { 4, 13, 12, "Юниоры 1" },
                    { 5, 15, 14, "Юниоры 2" },
                    { 6, 18, 16, "Молодёжь" },
                    { 7, 100, 19, "Взрослые" },
                    { 8, 100, 35, "Сеньоры" },
                    { 9, 100, 61, "Гранд-сеньоры" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryId", "Name" },
                values: new object[,]
                {
                    { 1, "Россия" },
                    { 2, "Беларусь" },
                    { 3, "Казахстан" },
                    { 4, "Украина" },
                    { 5, "Грузия" },
                    { 6, "Таджикистан" },
                    { 7, "Азербайджан" },
                    { 8, "Туркменистан" },
                    { 9, "Киргизия" },
                    { 10, "Узбекистан" },
                    { 11, "Армения" },
                    { 12, "Молдавия" },
                    { 13, "Литва" },
                    { 14, "Латвия" },
                    { 15, "Эстония" }
                });

            migrationBuilder.InsertData(
                table: "Dances",
                columns: new[] { "DanceId", "Name", "ShortName" },
                values: new object[,]
                {
                    { 1, "самба", "S" },
                    { 2, "ча-ча-ча", "Ch" },
                    { 3, "румба", "R" },
                    { 4, "пасодобль", "Pd" },
                    { 5, "джайв", "J" },
                    { 6, "медленный вальс", "W" },
                    { 7, "танго", "T" },
                    { 8, "венский вальс", "Vv" },
                    { 9, "медленный фокстрот", "SF" },
                    { 10, "квикстеп", "Q" }
                });

            migrationBuilder.InsertData(
                table: "Genders",
                columns: new[] { "GenderId", "Name" },
                values: new object[,]
                {
                    { 1, "Мужчина" },
                    { 2, "Женщина" }
                });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "PositionId", "Name" },
                values: new object[,]
                {
                    { 1, "Главный судья" },
                    { 2, "Заместитель главного судьи" },
                    { 3, "Главный секретарь соревнований" },
                    { 4, "Линейный судья" },
                    { 5, "Судья при участниках" }
                });

            migrationBuilder.InsertData(
                table: "Tours",
                columns: new[] { "TourId", "Name" },
                values: new object[,]
                {
                    { 1, "Финал" },
                    { 2, "Полуфинал" },
                    { 3, "1/4" },
                    { 4, "1/8" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "CityId", "CountryId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Новосибирск" },
                    { 2, 1, "Москва" },
                    { 3, 1, "Екатеринбург" },
                    { 4, 1, "Казань" },
                    { 5, 1, "Нижний Новгород" },
                    { 6, 1, "Челябинск" },
                    { 7, 1, "Красноярск" },
                    { 8, 1, "Самара" },
                    { 9, 1, "Уфа" },
                    { 10, 1, "Ростов-на-Дону" },
                    { 11, 1, "Омск" },
                    { 12, 1, "Краснодар" },
                    { 13, 1, "Воронеж" },
                    { 14, 1, "Волгоград" },
                    { 15, 1, "Пермь" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgeCategoriesInGroup_AgeCategoryId",
                table: "AgeCategoriesInGroup",
                column: "AgeCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                table: "Cities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Clubs_CityId",
                table: "Clubs",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Clubs_CountryId",
                table: "Clubs",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Competitions_ClubId",
                table: "Competitions",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_Dancers_ClubId",
                table: "Dancers",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_Dancers_CompetitionId",
                table: "Dancers",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Dancers_GenderId",
                table: "Dancers",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Dancers_GroupId",
                table: "Dancers",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Dancers_PairId",
                table: "Dancers",
                column: "PairId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_CompetitionId",
                table: "Groups",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_TourId",
                table: "Groups",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_JudgeInGroups_JudgeId",
                table: "JudgeInGroups",
                column: "JudgeId");

            migrationBuilder.CreateIndex(
                name: "IX_JudgeInPerformances_JudgeId",
                table: "JudgeInPerformances",
                column: "JudgeId");

            migrationBuilder.CreateIndex(
                name: "IX_Judges_ClubId",
                table: "Judges",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_Judges_CompetitionId",
                table: "Judges",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Judges_PositionId",
                table: "Judges",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_PairInPerformances_PairId",
                table: "PairInPerformances",
                column: "PairId");

            migrationBuilder.CreateIndex(
                name: "IX_PairInPerformances_PerformanceId",
                table: "PairInPerformances",
                column: "PerformanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Pairs_CompetitionId",
                table: "Pairs",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Performances_CompetitionId",
                table: "Performances",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Performances_DanceId",
                table: "Performances",
                column: "DanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Performances_GroupId",
                table: "Performances",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Performances_TourId",
                table: "Performances",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoreInDances_CompetitionId",
                table: "ScoreInDances",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoreInDances_DanceId",
                table: "ScoreInDances",
                column: "DanceId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoreInDances_JudgeId",
                table: "ScoreInDances",
                column: "JudgeId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoreInDances_PairId",
                table: "ScoreInDances",
                column: "PairId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoreInDances_PerformanceId",
                table: "ScoreInDances",
                column: "PerformanceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgeCategoriesInGroup");

            migrationBuilder.DropTable(
                name: "Dancers");

            migrationBuilder.DropTable(
                name: "JudgeInGroups");

            migrationBuilder.DropTable(
                name: "JudgeInPerformances");

            migrationBuilder.DropTable(
                name: "PairInPerformances");

            migrationBuilder.DropTable(
                name: "ScoreInDances");

            migrationBuilder.DropTable(
                name: "AgeCategories");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "Judges");

            migrationBuilder.DropTable(
                name: "Pairs");

            migrationBuilder.DropTable(
                name: "Performances");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Dances");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Competitions");

            migrationBuilder.DropTable(
                name: "Tours");

            migrationBuilder.DropTable(
                name: "Clubs");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
