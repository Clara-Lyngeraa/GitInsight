using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GitInsight.Entities.Migrations
{
    public partial class newMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataCommits_AnalyzedRepos_AnalyzedRepoId",
                table: "DataCommits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataCommits",
                table: "DataCommits");

            migrationBuilder.DropIndex(
                name: "IX_DataCommits_AnalyzedRepoId",
                table: "DataCommits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnalyzedRepos",
                table: "AnalyzedRepos");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DataCommits");

            migrationBuilder.DropColumn(
                name: "AnalyzedRepoId",
                table: "DataCommits");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "DataCommits");

            migrationBuilder.DropColumn(
                name: "RepositoryId",
                table: "DataCommits");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AnalyzedRepos");

            migrationBuilder.AlterColumn<string>(
                name: "StringId",
                table: "DataCommits",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DataCommits",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "AnalyzedRepoRepositoryIdString",
                table: "DataCommits",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RepositoryIdString",
                table: "AnalyzedRepos",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataCommits",
                table: "DataCommits",
                column: "StringId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnalyzedRepos",
                table: "AnalyzedRepos",
                column: "RepositoryIdString");

            migrationBuilder.CreateIndex(
                name: "IX_DataCommits_AnalyzedRepoRepositoryIdString",
                table: "DataCommits",
                column: "AnalyzedRepoRepositoryIdString");

            migrationBuilder.AddForeignKey(
                name: "FK_DataCommits_AnalyzedRepos_AnalyzedRepoRepositoryIdString",
                table: "DataCommits",
                column: "AnalyzedRepoRepositoryIdString",
                principalTable: "AnalyzedRepos",
                principalColumn: "RepositoryIdString");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataCommits_AnalyzedRepos_AnalyzedRepoRepositoryIdString",
                table: "DataCommits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataCommits",
                table: "DataCommits");

            migrationBuilder.DropIndex(
                name: "IX_DataCommits_AnalyzedRepoRepositoryIdString",
                table: "DataCommits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnalyzedRepos",
                table: "AnalyzedRepos");

            migrationBuilder.DropColumn(
                name: "AnalyzedRepoRepositoryIdString",
                table: "DataCommits");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DataCommits",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StringId",
                table: "DataCommits",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "DataCommits",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "AnalyzedRepoId",
                table: "DataCommits",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "DataCommits",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RepositoryId",
                table: "DataCommits",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "RepositoryIdString",
                table: "AnalyzedRepos",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "AnalyzedRepos",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataCommits",
                table: "DataCommits",
                column: "Id");

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
    }
}
