﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RepositorioApp.Data;

#nullable disable

namespace RepositorioApp.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231231200849_Remove_Circular_References")]
    partial class Remove_Circular_References
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("public")
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RepositorioApp.Domain.Entities.Article", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Authors")
                        .IsRequired()
                        .HasColumnType("varchar")
                        .HasColumnName("authors");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("created_at");

                    b.Property<Guid?>("EducationalResourceId")
                        .HasColumnType("uuid")
                        .HasColumnName("educational_resource");

                    b.Property<string>("Link")
                        .HasColumnType("varchar")
                        .HasColumnName("link");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar")
                        .HasColumnName("title");

                    b.HasKey("Id");

                    b.HasIndex("EducationalResourceId");

                    b.ToTable("articles", "public");
                });

            modelBuilder.Entity("RepositorioApp.Domain.Entities.EducationalResource", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Audience")
                        .IsRequired()
                        .HasColumnType("varchar")
                        .HasColumnName("audience");

                    b.Property<string>("Authors")
                        .IsRequired()
                        .HasColumnType("varchar")
                        .HasColumnName("authors");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid")
                        .HasColumnName("category");

                    b.Property<string>("Description")
                        .HasColumnType("varchar")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar")
                        .HasColumnName("name");

                    b.Property<string>("Objectives")
                        .IsRequired()
                        .HasColumnType("varchar")
                        .HasColumnName("objectives");

                    b.Property<string>("RepositoryLink")
                        .IsRequired()
                        .HasColumnType("varchar")
                        .HasColumnName("repository_link");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("educational_resources", "public");
                });

            modelBuilder.Entity("RepositorioApp.Domain.Entities.EducationalRole", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("created_at");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("varchar")
                        .HasColumnName("role");

                    b.HasKey("Id");

                    b.ToTable("educational_role", "public");
                });

            modelBuilder.Entity("RepositorioApp.Domain.Entities.FormType", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("varchar")
                        .HasColumnName("code");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("created_at");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar")
                        .HasColumnName("name");

                    b.Property<Guid?>("ResourceCategoryId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ResourceCategoryId");

                    b.ToTable("form_types", "public");
                });

            modelBuilder.Entity("RepositorioApp.Domain.Entities.Log", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Exception")
                        .HasColumnType("varchar")
                        .HasColumnName("exception");

                    b.Property<string>("Level")
                        .HasColumnType("varchar")
                        .HasColumnName("level");

                    b.Property<string>("Logger")
                        .HasColumnType("varchar")
                        .HasColumnName("logger");

                    b.Property<string>("Message")
                        .HasColumnType("varchar")
                        .HasColumnName("message");

                    b.Property<DateTime?>("OccurredAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("occurred_at");

                    b.HasKey("Id");

                    b.ToTable("logs", "public");
                });

            modelBuilder.Entity("RepositorioApp.Domain.Entities.Parameter", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("Active")
                        .HasColumnType("boolean")
                        .HasColumnName("active");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Group")
                        .HasColumnType("text");

                    b.Property<string>("Transaction")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("transaction");

                    b.Property<short>("Type")
                        .HasColumnType("smallint")
                        .HasColumnName("parameter_type");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("Id");

                    b.ToTable("parameter", "public");
                });

            modelBuilder.Entity("RepositorioApp.Domain.Entities.PasswordRecoverRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("code");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("created_at");

                    b.Property<DateTime>("ExpiresIn")
                        .HasColumnType("timestamp")
                        .HasColumnName("expires_in");

                    b.Property<DateTime?>("UsedIn")
                        .HasColumnType("timestamp")
                        .HasColumnName("used_in");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("password_recover_requests", "public");
                });

            modelBuilder.Entity("RepositorioApp.Domain.Entities.ResourceCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("Active")
                        .HasColumnType("boolean")
                        .HasColumnName("active");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("resource_category", "public");
                });

            modelBuilder.Entity("RepositorioApp.Domain.Entities.ResourceMaterial", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("BlobUrl")
                        .IsRequired()
                        .HasColumnType("varchar")
                        .HasColumnName("blob_url");

                    b.Property<Guid>("EducationalResourceId")
                        .HasColumnType("uuid")
                        .HasColumnName("educational_resource");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("varchar")
                        .HasColumnName("file_name");

                    b.HasKey("Id");

                    b.HasIndex("EducationalResourceId");

                    b.ToTable("resource_materials", "public");
                });

            modelBuilder.Entity("RepositorioApp.Domain.Entities.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("varchar")
                        .HasColumnName("code");

                    b.Property<string>("Group")
                        .IsRequired()
                        .HasColumnType("varchar")
                        .HasColumnName("group");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("tags", "public");
                });

            modelBuilder.Entity("RepositorioApp.Domain.Entities.University", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Acronym")
                        .IsRequired()
                        .HasColumnType("varchar")
                        .HasColumnName("acronym");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar")
                        .HasColumnName("name");

                    b.Property<string>("Uf")
                        .IsRequired()
                        .HasColumnType("varchar")
                        .HasColumnName("uf");

                    b.HasKey("Id");

                    b.ToTable("universities", "public");
                });

            modelBuilder.Entity("RepositorioApp.Domain.Entities.UploadFile", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("created_at");

                    b.Property<short>("UploadStatus")
                        .HasColumnType("smallint")
                        .HasColumnName("upload_status");

                    b.HasKey("Id");

                    b.ToTable("upload_files", "public");
                });

            modelBuilder.Entity("RepositorioApp.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("AcceptedTerm")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("accepted_term");

                    b.Property<bool>("Active")
                        .HasColumnType("boolean")
                        .HasColumnName("active");

                    b.Property<string>("Avatar")
                        .HasColumnType("varchar")
                        .HasColumnName("avatar");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp")
                        .HasColumnName("birth_date");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("created_at");

                    b.Property<Guid?>("EducationalRoleId")
                        .HasColumnType("uuid")
                        .HasColumnName("educational_role");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(1025)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("last_name");

                    b.Property<bool>("Master")
                        .HasColumnType("boolean")
                        .HasColumnName("master");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(1025)")
                        .HasColumnName("password");

                    b.Property<bool>("PendingRegisterInformation")
                        .HasColumnType("boolean")
                        .HasColumnName("pending_information");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("varchar")
                        .HasColumnName("phone");

                    b.Property<Guid?>("UniversityId")
                        .HasColumnType("uuid")
                        .HasColumnName("university");

                    b.HasKey("Id");

                    b.HasIndex("EducationalRoleId");

                    b.HasIndex("UniversityId");

                    b.HasIndex("Email", "Phone")
                        .IsUnique()
                        .HasDatabaseName("ix_users_email");

                    b.ToTable("users", "public");
                });

            modelBuilder.Entity("RepositorioApp.Domain.Entities.UserExperience", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("created_at");

                    b.Property<DateTime>("ExpiresAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("expires_at");

                    b.Property<Guid>("FormTypeId")
                        .HasColumnType("uuid")
                        .HasColumnName("form_type");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid")
                        .HasColumnName("owner");

                    b.Property<int>("Participants")
                        .HasColumnType("int")
                        .HasColumnName("participants");

                    b.Property<string>("Profile")
                        .HasColumnType("varchar")
                        .HasColumnName("profile");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar")
                        .HasColumnName("title");

                    b.HasKey("Id");

                    b.HasIndex("FormTypeId");

                    b.HasIndex("OwnerId");

                    b.ToTable("user_experiences", "public");
                });

            modelBuilder.Entity("RepositorioApp.Domain.Entities.UserRating", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("created_at");

                    b.Property<string>("Feedback")
                        .HasColumnType("varchar")
                        .HasColumnName("feedback");

                    b.Property<int>("Rating")
                        .HasColumnType("int")
                        .HasColumnName("rating");

                    b.Property<Guid>("UserExperienceId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_experience");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user");

                    b.HasKey("Id");

                    b.HasIndex("UserExperienceId");

                    b.ToTable("user_ratings", "public");
                });

            modelBuilder.Entity("RepositorioApp.Jwt.Models.SecurityKey", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("created_at");

                    b.Property<string>("Parameters")
                        .IsRequired()
                        .HasColumnType("varchar")
                        .HasColumnName("parameters");

                    b.HasKey("Id");

                    b.ToTable("security_keys", "public");
                });

            modelBuilder.Entity("RepositorioApp.Domain.Entities.Article", b =>
                {
                    b.HasOne("RepositorioApp.Domain.Entities.EducationalResource", null)
                        .WithMany("Articles")
                        .HasForeignKey("EducationalResourceId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("RepositorioApp.Domain.Entities.EducationalResource", b =>
                {
                    b.HasOne("RepositorioApp.Domain.Entities.ResourceCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("RepositorioApp.Domain.Entities.FormType", b =>
                {
                    b.HasOne("RepositorioApp.Domain.Entities.ResourceCategory", null)
                        .WithMany("FormTypes")
                        .HasForeignKey("ResourceCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RepositorioApp.Domain.Entities.PasswordRecoverRequest", b =>
                {
                    b.HasOne("RepositorioApp.Domain.Entities.User", null)
                        .WithMany("PasswordRecoverRequests")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RepositorioApp.Domain.Entities.ResourceMaterial", b =>
                {
                    b.HasOne("RepositorioApp.Domain.Entities.EducationalResource", null)
                        .WithMany("ResourceMaterials")
                        .HasForeignKey("EducationalResourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RepositorioApp.Domain.Entities.User", b =>
                {
                    b.HasOne("RepositorioApp.Domain.Entities.EducationalRole", "EducationalRole")
                        .WithMany()
                        .HasForeignKey("EducationalRoleId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("RepositorioApp.Domain.Entities.University", "University")
                        .WithMany()
                        .HasForeignKey("UniversityId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("EducationalRole");

                    b.Navigation("University");
                });

            modelBuilder.Entity("RepositorioApp.Domain.Entities.UserExperience", b =>
                {
                    b.HasOne("RepositorioApp.Domain.Entities.FormType", "FormType")
                        .WithMany()
                        .HasForeignKey("FormTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RepositorioApp.Domain.Entities.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("FormType");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("RepositorioApp.Domain.Entities.UserRating", b =>
                {
                    b.HasOne("RepositorioApp.Domain.Entities.UserExperience", null)
                        .WithMany("Ratings")
                        .HasForeignKey("UserExperienceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RepositorioApp.Domain.Entities.EducationalResource", b =>
                {
                    b.Navigation("Articles");

                    b.Navigation("ResourceMaterials");
                });

            modelBuilder.Entity("RepositorioApp.Domain.Entities.ResourceCategory", b =>
                {
                    b.Navigation("FormTypes");
                });

            modelBuilder.Entity("RepositorioApp.Domain.Entities.User", b =>
                {
                    b.Navigation("PasswordRecoverRequests");
                });

            modelBuilder.Entity("RepositorioApp.Domain.Entities.UserExperience", b =>
                {
                    b.Navigation("Ratings");
                });
#pragma warning restore 612, 618
        }
    }
}
