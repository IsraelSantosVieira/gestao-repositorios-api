using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositorioApp.Data.Migrations
{
    public partial class Create_Educational_Role : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_EducationalRole_educational_role",
                schema: "public",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EducationalRole",
                schema: "public",
                table: "EducationalRole");

            migrationBuilder.RenameTable(
                name: "EducationalRole",
                schema: "public",
                newName: "educational_role",
                newSchema: "public");

            migrationBuilder.RenameColumn(
                name: "Role",
                schema: "public",
                table: "educational_role",
                newName: "role");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "public",
                table: "educational_role",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                schema: "public",
                table: "educational_role",
                newName: "created_at");

            migrationBuilder.AlterColumn<string>(
                name: "role",
                schema: "public",
                table: "educational_role",
                type: "varchar",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                schema: "public",
                table: "educational_role",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_educational_role",
                schema: "public",
                table: "educational_role",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_users_educational_role_educational_role",
                schema: "public",
                table: "users",
                column: "educational_role",
                principalSchema: "public",
                principalTable: "educational_role",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_educational_role_educational_role",
                schema: "public",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_educational_role",
                schema: "public",
                table: "educational_role");

            migrationBuilder.RenameTable(
                name: "educational_role",
                schema: "public",
                newName: "EducationalRole",
                newSchema: "public");

            migrationBuilder.RenameColumn(
                name: "role",
                schema: "public",
                table: "EducationalRole",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "public",
                table: "EducationalRole",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "created_at",
                schema: "public",
                table: "EducationalRole",
                newName: "CreatedAt");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                schema: "public",
                table: "EducationalRole",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "public",
                table: "EducationalRole",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EducationalRole",
                schema: "public",
                table: "EducationalRole",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_users_EducationalRole_educational_role",
                schema: "public",
                table: "users",
                column: "educational_role",
                principalSchema: "public",
                principalTable: "EducationalRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
