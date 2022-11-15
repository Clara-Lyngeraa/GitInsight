using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GitInsight.Entities.Migrations
{
    public partial class someNewMaybe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataCommits_AnalyzedRepos_AnalyzedRepoRepositoryIdString",
                table: "DataCommits");

            migrationBuilder.RenameColumn(
                name: "AnalyzedRepoRepositoryIdString",
                table: "DataCommits",
                newName: "AnalyzedRepoPath");

            migrationBuilder.RenameIndex(
                name: "IX_DataCommits_AnalyzedRepoRepositoryIdString",
                table: "DataCommits",
                newName: "IX_DataCommits_AnalyzedRepoPath");

            migrationBuilder.RenameColumn(
                name: "RepositoryIdString",
                table: "AnalyzedRepos",
                newName: "Path");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "AnalyzedRepos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_DataCommits_AnalyzedRepos_AnalyzedRepoPath",
                table: "DataCommits",
                column: "AnalyzedRepoPath",
                principalTable: "AnalyzedRepos",
                principalColumn: "Path");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataCommits_AnalyzedRepos_AnalyzedRepoPath",
                table: "DataCommits");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AnalyzedRepos");

            migrationBuilder.RenameColumn(
                name: "AnalyzedRepoPath",
                table: "DataCommits",
                newName: "AnalyzedRepoRepositoryIdString");

            migrationBuilder.RenameIndex(
                name: "IX_DataCommits_AnalyzedRepoPath",
                table: "DataCommits",
                newName: "IX_DataCommits_AnalyzedRepoRepositoryIdString");

            migrationBuilder.RenameColumn(
                name: "Path",
                table: "AnalyzedRepos",
                newName: "RepositoryIdString");

            migrationBuilder.AddForeignKey(
                name: "FK_DataCommits_AnalyzedRepos_AnalyzedRepoRepositoryIdString",
                table: "DataCommits",
                column: "AnalyzedRepoRepositoryIdString",
                principalTable: "AnalyzedRepos",
                principalColumn: "RepositoryIdString");
        }
    }
}
