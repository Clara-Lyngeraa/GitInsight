using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GitInsight.Entities.Migrations
{
    public partial class firstCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnalyzedRepos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RepositoryIdString = table.Column<string>(type: "text", nullable: true),
                    State = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyzedRepos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataCommits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StringId = table.Column<string>(type: "text", nullable: true),
                    RepositoryId = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    AnalyzedRepoId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataCommits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataCommits_AnalyzedRepos_AnalyzedRepoId",
                        column: x => x.AnalyzedRepoId,
                        principalTable: "AnalyzedRepos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataCommits_AnalyzedRepoId",
                table: "DataCommits",
                column: "AnalyzedRepoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataCommits");

            migrationBuilder.DropTable(
                name: "AnalyzedRepos");
        }
    }
}
