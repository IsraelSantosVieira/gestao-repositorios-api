using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositorioApp.Data.Migrations
{
    public partial class Create_Parameter_Group : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Group",
                schema: "public",
                table: "parameter",
                newName: "group");

            migrationBuilder.AlterColumn<string>(
                name: "group",
                schema: "public",
                table: "parameter",
                type: "varchar",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "group",
                schema: "public",
                table: "parameter",
                newName: "Group");

            migrationBuilder.AlterColumn<string>(
                name: "Group",
                schema: "public",
                table: "parameter",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldNullable: true);
        }
    }
}
