using System;
using Microsoft.EntityFrameworkCore.Migrations;
using RepositorioApp.Data.Imports;

#nullable disable

namespace RepositorioApp.Data.Migrations
{
    public partial class Create_Universities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                schema: "public",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "EducationalRoleId",
                schema: "public",
                table: "users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UniversityId",
                schema: "public",
                table: "users",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EducationalRole",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationalRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "universities",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "varchar", nullable: false),
                    acronym = table.Column<string>(type: "varchar", nullable: false),
                    uf = table.Column<string>(type: "varchar", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_universities", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_EducationalRoleId",
                schema: "public",
                table: "users",
                column: "EducationalRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_users_UniversityId",
                schema: "public",
                table: "users",
                column: "UniversityId");

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

            migrationBuilder.Sql(UniversitiesSqlGenerator.Sql());
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_EducationalRole_EducationalRoleId",
                schema: "public",
                table: "users");

            migrationBuilder.DropForeignKey(
                name: "FK_users_universities_UniversityId",
                schema: "public",
                table: "users");

            migrationBuilder.DropTable(
                name: "EducationalRole",
                schema: "public");

            migrationBuilder.DropTable(
                name: "universities",
                schema: "public");

            migrationBuilder.DropIndex(
                name: "IX_users_EducationalRoleId",
                schema: "public",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_UniversityId",
                schema: "public",
                table: "users");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                schema: "public",
                table: "users");

            migrationBuilder.DropColumn(
                name: "EducationalRoleId",
                schema: "public",
                table: "users");

            migrationBuilder.DropColumn(
                name: "UniversityId",
                schema: "public",
                table: "users");
        }
    }
}
