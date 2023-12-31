using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositorioApp.Data.Migrations
{
    public partial class Update_User_References : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_EducationalRole_EducationalRoleId",
                schema: "public",
                table: "users");

            migrationBuilder.DropForeignKey(
                name: "FK_users_universities_UniversityId",
                schema: "public",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "UniversityId",
                schema: "public",
                table: "users",
                newName: "university");

            migrationBuilder.RenameColumn(
                name: "EducationalRoleId",
                schema: "public",
                table: "users",
                newName: "educational_role");

            migrationBuilder.RenameColumn(
                name: "BirthDate",
                schema: "public",
                table: "users",
                newName: "birth_date");

            migrationBuilder.RenameIndex(
                name: "IX_users_UniversityId",
                schema: "public",
                table: "users",
                newName: "IX_users_university");

            migrationBuilder.RenameIndex(
                name: "IX_users_EducationalRoleId",
                schema: "public",
                table: "users",
                newName: "IX_users_educational_role");

            migrationBuilder.AlterColumn<DateTime>(
                name: "birth_date",
                schema: "public",
                table: "users",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddForeignKey(
                name: "FK_users_EducationalRole_educational_role",
                schema: "public",
                table: "users",
                column: "educational_role",
                principalSchema: "public",
                principalTable: "EducationalRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_users_universities_university",
                schema: "public",
                table: "users",
                column: "university",
                principalSchema: "public",
                principalTable: "universities",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_EducationalRole_educational_role",
                schema: "public",
                table: "users");

            migrationBuilder.DropForeignKey(
                name: "FK_users_universities_university",
                schema: "public",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "university",
                schema: "public",
                table: "users",
                newName: "UniversityId");

            migrationBuilder.RenameColumn(
                name: "educational_role",
                schema: "public",
                table: "users",
                newName: "EducationalRoleId");

            migrationBuilder.RenameColumn(
                name: "birth_date",
                schema: "public",
                table: "users",
                newName: "BirthDate");

            migrationBuilder.RenameIndex(
                name: "IX_users_university",
                schema: "public",
                table: "users",
                newName: "IX_users_UniversityId");

            migrationBuilder.RenameIndex(
                name: "IX_users_educational_role",
                schema: "public",
                table: "users",
                newName: "IX_users_EducationalRoleId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                schema: "public",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.AddForeignKey(
                name: "FK_users_EducationalRole_EducationalRoleId",
                schema: "public",
                table: "users",
                column: "EducationalRoleId",
                principalSchema: "public",
                principalTable: "EducationalRole",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_users_universities_UniversityId",
                schema: "public",
                table: "users",
                column: "UniversityId",
                principalSchema: "public",
                principalTable: "universities",
                principalColumn: "id");
        }
    }
}
