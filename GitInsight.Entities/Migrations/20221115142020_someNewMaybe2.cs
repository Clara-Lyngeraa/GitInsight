using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GitInsight.Entities.Migrations
{
    public partial class someNewMaybe2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataCommits_AnalyzedRepos_AnalyzedRepoPath",
                table: "DataCommits");

            migrationBuilder.DropIndex(
                name: "IX_DataCommits_AnalyzedRepoPath",
                table: "DataCommits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnalyzedRepos",
                table: "AnalyzedRepos");

            migrationBuilder.DropColumn(
                name: "AnalyzedRepoPath",
                table: "DataCommits");

            migrationBuilder.AddColumn<int>(
                name: "AnalyzedRepoId",
                table: "DataCommits",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "AnalyzedRepos",
                type: "character varying(48)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "AnalyzedRepos",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnalyzedRepos",
                table: "AnalyzedRepos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DataCommits_AnalyzedRepoId",
                table: "DataCommits",
                column: "AnalyzedRepoId");

            migrationBuilder.AddForeignKey(
                name: "FK_DataCommits_AnalyzedRepos_AnalyzedRepoId",
                table: "DataCommits",
                column: "AnalyzedRepoId",
                principalTable: "AnalyzedRepos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataCommits_AnalyzedRepos_AnalyzedRepoId",
                table: "DataCommits");

            migrationBuilder.DropIndex(
                name: "IX_DataCommits_AnalyzedRepoId",
                table: "DataCommits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnalyzedRepos",
                table: "AnalyzedRepos");

            migrationBuilder.DropColumn(
                name: "AnalyzedRepoId",
                table: "DataCommits");

            migrationBuilder.AddColumn<string>(
                name: "AnalyzedRepoPath",
                table: "DataCommits",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "State",
                table: "AnalyzedRepos",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(48)");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "AnalyzedRepos",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnalyzedRepos",
                table: "AnalyzedRepos",
                column: "Path");

            migrationBuilder.CreateIndex(
                name: "IX_DataCommits_AnalyzedRepoPath",
                table: "DataCommits",
                column: "AnalyzedRepoPath");

            migrationBuilder.AddForeignKey(
                name: "FK_DataCommits_AnalyzedRepos_AnalyzedRepoPath",
                table: "DataCommits",
                column: "AnalyzedRepoPath",
                principalTable: "AnalyzedRepos",
                principalColumn: "Path");
        }
    }
}
