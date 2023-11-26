using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositorioApp.Data.Migrations
{
    public partial class Initial_Database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "logs",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    occurred_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    level = table.Column<string>(type: "varchar", nullable: true),
                    logger = table.Column<string>(type: "varchar", nullable: true),
                    message = table.Column<string>(type: "varchar", nullable: true),
                    exception = table.Column<string>(type: "varchar", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_logs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "parameter",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    transaction = table.Column<string>(type: "text", nullable: false),
                    Group = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false),
                    value = table.Column<string>(type: "text", nullable: false),
                    parameter_type = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parameter", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "security_keys",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    parameters = table.Column<string>(type: "varchar", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_security_keys", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "upload_files",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    upload_status = table.Column<short>(type: "smallint", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_upload_files", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    email = table.Column<string>(type: "varchar(1025)", nullable: false),
                    first_name = table.Column<string>(type: "varchar(255)", nullable: true),
                    last_name = table.Column<string>(type: "varchar(255)", nullable: true),
                    password = table.Column<string>(type: "varchar(1025)", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false),
                    avatar = table.Column<string>(type: "varchar", nullable: true),
                    phone = table.Column<string>(type: "varchar", nullable: false),
                    pending_information = table.Column<bool>(type: "boolean", nullable: false),
                    accepted_term = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    master = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "password_recover_requests",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "varchar(100)", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    expires_in = table.Column<DateTime>(type: "timestamp", nullable: false),
                    used_in = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_password_recover_requests", x => x.id);
                    table.ForeignKey(
                        name: "FK_password_recover_requests_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_password_recover_requests_user_id",
                schema: "public",
                table: "password_recover_requests",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                schema: "public",
                table: "users",
                columns: new[] { "email", "phone" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "logs",
                schema: "public");

            migrationBuilder.DropTable(
                name: "parameter",
                schema: "public");

            migrationBuilder.DropTable(
                name: "password_recover_requests",
                schema: "public");

            migrationBuilder.DropTable(
                name: "security_keys",
                schema: "public");

            migrationBuilder.DropTable(
                name: "upload_files",
                schema: "public");

            migrationBuilder.DropTable(
                name: "users",
                schema: "public");
        }
    }
}
