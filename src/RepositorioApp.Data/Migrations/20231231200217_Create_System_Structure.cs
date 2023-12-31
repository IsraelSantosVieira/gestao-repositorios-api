using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositorioApp.Data.Migrations
{
    public partial class Create_System_Structure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_educational_role_educational_role",
                schema: "public",
                table: "users");

            migrationBuilder.DropForeignKey(
                name: "FK_users_universities_university",
                schema: "public",
                table: "users");

            migrationBuilder.CreateTable(
                name: "resource_category",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "varchar", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_resource_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "varchar", nullable: false),
                    group = table.Column<string>(type: "varchar", nullable: false),
                    code = table.Column<string>(type: "varchar", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tags", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "educational_resources",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "varchar", nullable: false),
                    authors = table.Column<string>(type: "varchar", nullable: false),
                    description = table.Column<string>(type: "varchar", nullable: true),
                    category = table.Column<Guid>(type: "uuid", nullable: false),
                    repository_link = table.Column<string>(type: "varchar", nullable: false),
                    objectives = table.Column<string>(type: "varchar", nullable: false),
                    audience = table.Column<string>(type: "varchar", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_educational_resources", x => x.id);
                    table.ForeignKey(
                        name: "FK_educational_resources_resource_category_category",
                        column: x => x.category,
                        principalSchema: "public",
                        principalTable: "resource_category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "form_types",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "varchar", nullable: false),
                    code = table.Column<string>(type: "varchar", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    ResourceCategoryId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_form_types", x => x.id);
                    table.ForeignKey(
                        name: "FK_form_types_resource_category_ResourceCategoryId",
                        column: x => x.ResourceCategoryId,
                        principalSchema: "public",
                        principalTable: "resource_category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "articles",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    EducationalResourceId = table.Column<Guid>(type: "uuid", nullable: true),
                    title = table.Column<string>(type: "varchar", nullable: false),
                    authors = table.Column<string>(type: "varchar", nullable: false),
                    link = table.Column<string>(type: "varchar", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    EducationalResourceId1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_articles", x => x.id);
                    table.ForeignKey(
                        name: "FK_articles_educational_resources_EducationalResourceId",
                        column: x => x.EducationalResourceId,
                        principalSchema: "public",
                        principalTable: "educational_resources",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_articles_educational_resources_EducationalResourceId1",
                        column: x => x.EducationalResourceId1,
                        principalSchema: "public",
                        principalTable: "educational_resources",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "resource_materials",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    educational_resource = table.Column<Guid>(type: "uuid", nullable: false),
                    file_name = table.Column<string>(type: "varchar", nullable: false),
                    blob_url = table.Column<string>(type: "varchar", nullable: false),
                    EducationalResourceId1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_resource_materials", x => x.id);
                    table.ForeignKey(
                        name: "FK_resource_materials_educational_resources_educational_resour~",
                        column: x => x.educational_resource,
                        principalSchema: "public",
                        principalTable: "educational_resources",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_resource_materials_educational_resources_EducationalResourc~",
                        column: x => x.EducationalResourceId1,
                        principalSchema: "public",
                        principalTable: "educational_resources",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "user_experiences",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    owner = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "varchar", nullable: false),
                    profile = table.Column<string>(type: "varchar", nullable: true),
                    participants = table.Column<int>(type: "int", nullable: false),
                    form_type = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    expires_at = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_experiences", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_experiences_form_types_form_type",
                        column: x => x.form_type,
                        principalSchema: "public",
                        principalTable: "form_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_user_experiences_users_owner",
                        column: x => x.owner,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user_ratings",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user = table.Column<Guid>(type: "uuid", nullable: false),
                    user_experience = table.Column<Guid>(type: "uuid", nullable: false),
                    rating = table.Column<int>(type: "int", nullable: false),
                    feedback = table.Column<string>(type: "varchar", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UserExperienceId1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_ratings", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_ratings_user_experiences_user_experience",
                        column: x => x.user_experience,
                        principalSchema: "public",
                        principalTable: "user_experiences",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_ratings_user_experiences_UserExperienceId1",
                        column: x => x.UserExperienceId1,
                        principalSchema: "public",
                        principalTable: "user_experiences",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user_ratings_users_user",
                        column: x => x.user,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_articles_EducationalResourceId",
                schema: "public",
                table: "articles",
                column: "EducationalResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_articles_EducationalResourceId1",
                schema: "public",
                table: "articles",
                column: "EducationalResourceId1");

            migrationBuilder.CreateIndex(
                name: "IX_educational_resources_category",
                schema: "public",
                table: "educational_resources",
                column: "category");

            migrationBuilder.CreateIndex(
                name: "IX_form_types_ResourceCategoryId",
                schema: "public",
                table: "form_types",
                column: "ResourceCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_resource_materials_educational_resource",
                schema: "public",
                table: "resource_materials",
                column: "educational_resource");

            migrationBuilder.CreateIndex(
                name: "IX_resource_materials_EducationalResourceId1",
                schema: "public",
                table: "resource_materials",
                column: "EducationalResourceId1");

            migrationBuilder.CreateIndex(
                name: "IX_user_experiences_form_type",
                schema: "public",
                table: "user_experiences",
                column: "form_type");

            migrationBuilder.CreateIndex(
                name: "IX_user_experiences_owner",
                schema: "public",
                table: "user_experiences",
                column: "owner");

            migrationBuilder.CreateIndex(
                name: "IX_user_ratings_user",
                schema: "public",
                table: "user_ratings",
                column: "user");

            migrationBuilder.CreateIndex(
                name: "IX_user_ratings_user_experience",
                schema: "public",
                table: "user_ratings",
                column: "user_experience");

            migrationBuilder.CreateIndex(
                name: "IX_user_ratings_UserExperienceId1",
                schema: "public",
                table: "user_ratings",
                column: "UserExperienceId1");

            migrationBuilder.AddForeignKey(
                name: "FK_users_educational_role_educational_role",
                schema: "public",
                table: "users",
                column: "educational_role",
                principalSchema: "public",
                principalTable: "educational_role",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_users_universities_university",
                schema: "public",
                table: "users",
                column: "university",
                principalSchema: "public",
                principalTable: "universities",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_educational_role_educational_role",
                schema: "public",
                table: "users");

            migrationBuilder.DropForeignKey(
                name: "FK_users_universities_university",
                schema: "public",
                table: "users");

            migrationBuilder.DropTable(
                name: "articles",
                schema: "public");

            migrationBuilder.DropTable(
                name: "resource_materials",
                schema: "public");

            migrationBuilder.DropTable(
                name: "tags",
                schema: "public");

            migrationBuilder.DropTable(
                name: "user_ratings",
                schema: "public");

            migrationBuilder.DropTable(
                name: "educational_resources",
                schema: "public");

            migrationBuilder.DropTable(
                name: "user_experiences",
                schema: "public");

            migrationBuilder.DropTable(
                name: "form_types",
                schema: "public");

            migrationBuilder.DropTable(
                name: "resource_category",
                schema: "public");

            migrationBuilder.AddForeignKey(
                name: "FK_users_educational_role_educational_role",
                schema: "public",
                table: "users",
                column: "educational_role",
                principalSchema: "public",
                principalTable: "educational_role",
                principalColumn: "id",
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
    }
}
