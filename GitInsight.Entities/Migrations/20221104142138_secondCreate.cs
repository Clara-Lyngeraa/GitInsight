using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GitInsight.Entities.Migrations
{
    public partial class secondCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnalyzedRepoId",
                table: "Signatures",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RepositoryId",
                table: "Signatures",
                type: "text",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.CreateIndex(
                name: "IX_Signatures_AnalyzedRepoId",
                table: "Signatures",
                column: "AnalyzedRepoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Signatures_AnalyzedRepos_AnalyzedRepoId",
                table: "Signatures",
                column: "AnalyzedRepoId",
                principalTable: "AnalyzedRepos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Signatures_AnalyzedRepos_AnalyzedRepoId",
                table: "Signatures");

            migrationBuilder.DropTable(
                name: "AnalyzedRepos");

            migrationBuilder.DropIndex(
                name: "IX_Signatures_AnalyzedRepoId",
                table: "Signatures");

            migrationBuilder.DropColumn(
                name: "AnalyzedRepoId",
                table: "Signatures");

            migrationBuilder.DropColumn(
                name: "RepositoryId",
                table: "Signatures");
        }
    }
}
