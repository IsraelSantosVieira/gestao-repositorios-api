using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositorioApp.Data.Migrations
{
    public partial class Remove_Circular_References : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_articles_educational_resources_EducationalResourceId1",
                schema: "public",
                table: "articles");

            migrationBuilder.DropForeignKey(
                name: "FK_resource_materials_educational_resources_educational_resour~",
                schema: "public",
                table: "resource_materials");

            migrationBuilder.DropForeignKey(
                name: "FK_resource_materials_educational_resources_EducationalResourc~",
                schema: "public",
                table: "resource_materials");

            migrationBuilder.DropForeignKey(
                name: "FK_user_ratings_user_experiences_UserExperienceId1",
                schema: "public",
                table: "user_ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_user_ratings_users_user",
                schema: "public",
                table: "user_ratings");

            migrationBuilder.DropIndex(
                name: "IX_user_ratings_user",
                schema: "public",
                table: "user_ratings");

            migrationBuilder.DropIndex(
                name: "IX_user_ratings_UserExperienceId1",
                schema: "public",
                table: "user_ratings");

            migrationBuilder.DropIndex(
                name: "IX_resource_materials_EducationalResourceId1",
                schema: "public",
                table: "resource_materials");

            migrationBuilder.DropIndex(
                name: "IX_articles_EducationalResourceId1",
                schema: "public",
                table: "articles");

            migrationBuilder.DropColumn(
                name: "UserExperienceId1",
                schema: "public",
                table: "user_ratings");

            migrationBuilder.DropColumn(
                name: "EducationalResourceId1",
                schema: "public",
                table: "resource_materials");

            migrationBuilder.DropColumn(
                name: "EducationalResourceId1",
                schema: "public",
                table: "articles");

            migrationBuilder.AddForeignKey(
                name: "FK_resource_materials_educational_resources_educational_resour~",
                schema: "public",
                table: "resource_materials",
                column: "educational_resource",
                principalSchema: "public",
                principalTable: "educational_resources",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_resource_materials_educational_resources_educational_resour~",
                schema: "public",
                table: "resource_materials");

            migrationBuilder.AddColumn<Guid>(
                name: "UserExperienceId1",
                schema: "public",
                table: "user_ratings",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EducationalResourceId1",
                schema: "public",
                table: "resource_materials",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EducationalResourceId1",
                schema: "public",
                table: "articles",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_ratings_user",
                schema: "public",
                table: "user_ratings",
                column: "user");

            migrationBuilder.CreateIndex(
                name: "IX_user_ratings_UserExperienceId1",
                schema: "public",
                table: "user_ratings",
                column: "UserExperienceId1");

            migrationBuilder.CreateIndex(
                name: "IX_resource_materials_EducationalResourceId1",
                schema: "public",
                table: "resource_materials",
                column: "EducationalResourceId1");

            migrationBuilder.CreateIndex(
                name: "IX_articles_EducationalResourceId1",
                schema: "public",
                table: "articles",
                column: "EducationalResourceId1");

            migrationBuilder.AddForeignKey(
                name: "FK_articles_educational_resources_EducationalResourceId1",
                schema: "public",
                table: "articles",
                column: "EducationalResourceId1",
                principalSchema: "public",
                principalTable: "educational_resources",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_resource_materials_educational_resources_educational_resour~",
                schema: "public",
                table: "resource_materials",
                column: "educational_resource",
                principalSchema: "public",
                principalTable: "educational_resources",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_resource_materials_educational_resources_EducationalResourc~",
                schema: "public",
                table: "resource_materials",
                column: "EducationalResourceId1",
                principalSchema: "public",
                principalTable: "educational_resources",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_ratings_user_experiences_UserExperienceId1",
                schema: "public",
                table: "user_ratings",
                column: "UserExperienceId1",
                principalSchema: "public",
                principalTable: "user_experiences",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_ratings_users_user",
                schema: "public",
                table: "user_ratings",
                column: "user",
                principalSchema: "public",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
