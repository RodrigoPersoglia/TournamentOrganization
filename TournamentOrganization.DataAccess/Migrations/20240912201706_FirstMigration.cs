using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TournamentOrganization.DataAccess.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SkillLevel = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    Strength = table.Column<int>(type: "int", nullable: true),
                    Speed = table.Column<int>(type: "int", nullable: true),
                    ReactionTime = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tournaments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlayerGender = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Player1Id = table.Column<int>(type: "int", nullable: false),
                    Player2Id = table.Column<int>(type: "int", nullable: false),
                    TournamentId = table.Column<int>(type: "int", nullable: false),
                    WinnerId = table.Column<int>(type: "int", nullable: false),
                    Stage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matches_Players_Player1Id",
                        column: x => x.Player1Id,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Players_Player2Id",
                        column: x => x.Player2Id,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Players_WinnerId",
                        column: x => x.WinnerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerTournament",
                columns: table => new
                {
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    TournamentId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerTournament", x => new { x.PlayerId, x.TournamentId });
                    table.ForeignKey(
                        name: "FK_PlayerTournament_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerTournament_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Player1Id",
                table: "Matches",
                column: "Player1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Player2Id",
                table: "Matches",
                column: "Player2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TournamentId",
                table: "Matches",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_WinnerId",
                table: "Matches",
                column: "WinnerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerTournament_TournamentId",
                table: "PlayerTournament",
                column: "TournamentId");

            migrationBuilder.InsertData(
               table: "Players",
               columns: new[] { "Id", "FirstName", "LastName", "SkillLevel", "Gender", "Strength", "Speed", "ReactionTime" },
               values: new object[,]
               {
                    { 1, "Novak", "Djokovic", 99, "Male", 85, 90, null },
                    { 2, "Carlos", "Alcaraz", 98, "Male", 88, 92, null },
                    { 3, "Daniil", "Medvedev", 97, "Male", 87, 89, null },
                    { 4, "Rafael", "Nadal", 96, "Male", 86, 88, null },
                    { 5, "Stefanos", "Tsitsipas", 95, "Male", 84, 87, null },
                    { 6, "Alexander", "Zverev", 94, "Male", 83, 85, null },
                    { 7, "Casper", "Ruud", 93, "Male", 82, 84, null },
                    { 8, "Felix", "Auger-Aliassime", 92, "Male", 81, 83, null },
                    { 9, "Andrey", "Rublev", 91, "Male", 80, 82, null },
                    { 10, "Jannik", "Sinner", 90, "Male", 79, 81, null },
                    { 11, "Hubert", "Hurkacz", 89, "Male", 78, 80, null },
                    { 12, "Taylor", "Fritz", 88, "Male", 77, 79, null },
                    { 13, "Nicolas", "Basilashvili", 87, "Male", 76, 78, null },
                    { 14, "John", "Isner", 86, "Male", 75, 77, null },
                    { 15, "Diego", "Schwartzman", 85, "Male", 74, 76, null },
                    { 16, "Aslan", "Karatev", 84, "Male", 73, 75, null },
                    { 17, "Lorenzo", "Sonego", 83, "Male", 72, 74, null },
                    { 18, "Lorenzo", "Musetti", 82, "Male", 71, 73, null },
                    { 19, "Andy", "Murray", 81, "Male", 70, 72, null },
                    { 20, "Grigor", "Dimitrov", 80, "Male", 69, 71, null },
                    { 21, "Alexander", "Bublik", 79, "Male", 68, 70, null },
                    { 22, "Richard", "Gasquet", 78, "Male", 67, 69, null },
                    { 23, "David", "Goffin", 77, "Male", 66, 68, null },
                    { 24, "Filip", "Krajinovic", 76, "Male", 65, 67, null },
                    { 25, "Pedro", "Martinez", 75, "Male", 64, 66, null },
                    { 26, "Jiri", "Vesely", 74, "Male", 63, 65, null },
                    { 27, "Botic", "Van de Zandschulp", 73, "Male", 62, 64, null },
                    { 28, "Milos", "Raonic", 72, "Male", 61, 63, null },
                    { 29, "Stan", "Wawrinka", 71, "Male", 60, 62, null },
                    { 30, "Adrian", "Mannarino", 70, "Male", 59, 61, null },
                    { 31, "Ugo", "Humbert", 69, "Male", 58, 60, null },
                    { 32, "Joao", "Sousa", 68, "Male", 57, 59, null },
                    { 33, "Ashleigh", "Barty", 96, "Female", null, null, 95 },
                    { 34, "Iga", "Swiatek", 95, "Female", null, null, 94 },
                    { 35, "Naomi", "Osaka", 94, "Female", null, null, 93 },
                    { 36, "Maria", "Sakkari", 93, "Female", null, null, 92 },
                    { 37, "Aryna", "Sabalenka", 92, "Female", null, null, 91 },
                    { 38, "Elena", "Rybakina", 91, "Female", null, null, 90 },
                    { 39, "Emma", "Raducanu", 90, "Female", null, null, 89 },
                    { 40, "Jelena", "Ostapenko", 89, "Female", null, null, 88 },
                    { 41, "Bianca", "Andreescu", 88, "Female", null, null, 87 },
                    { 42, "Garbiñe", "Muguruza", 87, "Female", null, null, 86 },
                    { 43, "Sofia", "Kenin", 86, "Female", null, null, 85 },
                    { 44, "Petra", "Kvitova", 85, "Female", null, null, 84 },
                    { 45, "Karolína", "Plíšková", 84, "Female", null, null, 83 },
                    { 46, "Markéta", "Vondroušová", 83, "Female", null, null, 82 },
                    { 47, "Elina", "Svitolina", 82, "Female", null, null, 81 },
                    { 48, "Cori", "Gauff", 81, "Female", null, null, 80 },
                    { 49, "Zheng", "Qinwen", 80, "Female", null, null, 79 },
                    { 50, "Veronika", "Kudermetova", 79, "Female", null, null, 78 },
                    { 51, "Clara", "Burel", 78, "Female", null, null, 77 },
                    { 52, "Leylah", "Fernandez", 77, "Female", null, null, 76 },
                    { 53, "Jasmine", "Paolini", 76, "Female", null, null, 75 },
                    { 54, "Daria", "Kasatkina", 75, "Female", null, null, 74 },
                    { 55, "Yulia", "Putintseva", 74, "Female", null, null, 73 },
                    { 56, "Jule", "Niemeier", 73, "Female", null, null, 72 },
                    { 57, "Harmony", "Tan", 72, "Female", null, null, 71 },
                    { 58, "Katerina", "Siniakova", 71, "Female", null, null, 70 },
                    { 59, "Anett", "Kontaveit", 70, "Female", null, null, 69 },
                    { 60, "Aryna", "Sabalenka", 69, "Female", null, null, 68 },
                    { 61, "Elise", "Mertens", 68, "Female", null, null, 67 },
                    { 62, "Tamara", "Zidansek", 67, "Female", null, null, 66 }
               });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "PlayerTournament");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Tournaments");
        }
    }
}
