using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositorioApp.Data.Migrations
{
    public partial class Update_Article_Structure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_articles_educational_resources_EducationalResourceId",
                schema: "public",
                table: "articles");

            migrationBuilder.RenameColumn(
                name: "EducationalResourceId",
                schema: "public",
                table: "articles",
                newName: "educational_resource");

            migrationBuilder.RenameIndex(
                name: "IX_articles_EducationalResourceId",
                schema: "public",
                table: "articles",
                newName: "IX_articles_educational_resource");

            migrationBuilder.AddForeignKey(
                name: "FK_articles_educational_resources_educational_resource",
                schema: "public",
                table: "articles",
                column: "educational_resource",
                principalSchema: "public",
                principalTable: "educational_resources",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_articles_educational_resources_educational_resource",
                schema: "public",
                table: "articles");

            migrationBuilder.RenameColumn(
                name: "educational_resource",
                schema: "public",
                table: "articles",
                newName: "EducationalResourceId");

            migrationBuilder.RenameIndex(
                name: "IX_articles_educational_resource",
                schema: "public",
                table: "articles",
                newName: "IX_articles_EducationalResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_articles_educational_resources_EducationalResourceId",
                schema: "public",
                table: "articles",
                column: "EducationalResourceId",
                principalSchema: "public",
                principalTable: "educational_resources",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
