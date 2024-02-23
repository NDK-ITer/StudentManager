﻿// <auto-generated />
using System;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(UserDbContext))]
    [Migration("20240223101630_addIsChecked")]
    partial class addIsChecked
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Comment", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateComment")
                        .HasColumnType("datetime2");

                    b.Property<string>("PostId")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("Domain.Entities.Faculty", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsOpen")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Faculty");
                });

            modelBuilder.Entity("Domain.Entities.Post", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("DatePost")
                        .HasColumnType("datetime2");

                    b.Property<string>("FacultyId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<bool>("IsChecked")
                        .HasColumnType("bit");

                    b.Property<string>("LinkDocument")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ListImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("FacultyId");

                    b.HasIndex("UserId");

                    b.ToTable("Post", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Role", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("NormalizeName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Role", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "c8c17fbb-731c-405a-bed8-cb22868ef7ca",
                            Description = "",
                            Name = "ADMIN",
                            NormalizeName = "Admin"
                        },
                        new
                        {
                            Id = "76fda05c-b706-421c-94b7-a0a578c1b93b",
                            Description = "",
                            Name = "USER",
                            NormalizeName = "User"
                        });
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FacultyID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstEmail")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsLock")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("PresentEmail")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("TokenAccess")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("VerifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("FacultyID");

                    b.HasIndex("RoleId");

                    b.ToTable("User", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "2c75293b-f8e5-4862-9b13-5894a64895cd",
                            Avatar = "",
                            Birthday = new DateTime(2024, 2, 23, 17, 16, 28, 887, DateTimeKind.Local).AddTicks(1107),
                            CreatedDate = new DateTime(2024, 2, 23, 17, 16, 28, 887, DateTimeKind.Local).AddTicks(1117),
                            FirstEmail = "admin001@gmail.com",
                            FirstName = "Admin",
                            IsLock = false,
                            IsVerified = true,
                            LastName = "account",
                            PasswordHash = "OupciF/ZKYnt4U0xYizqoQ==",
                            PhoneNumber = "0123456789",
                            PresentEmail = "admin001@gmail.com",
                            RoleId = "c8c17fbb-731c-405a-bed8-cb22868ef7ca",
                            TokenAccess = "C09D185F3313EE93A4FA91598A0F124973C4AE6B7654C179FF13833D70FC5FF0B060F5A9FFAD733B86941B169F62880E6DB42B9C4F4F07C2958CA74BF4F373F7",
                            UserName = "adminVersion_0001",
                            VerifiedDate = new DateTime(2024, 2, 23, 17, 16, 28, 887, DateTimeKind.Local).AddTicks(1353)
                        });
                });

            modelBuilder.Entity("Domain.Entities.Comment", b =>
                {
                    b.HasOne("Domain.Entities.Post", "Post")
                        .WithMany("ListComent")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany("ListComment")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Post", b =>
                {
                    b.HasOne("Domain.Entities.Faculty", "Faculty")
                        .WithMany("ListPost")
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany("ListPost")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Faculty");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.HasOne("Domain.Entities.Faculty", "Faculty")
                        .WithMany("ListAdmin")
                        .HasForeignKey("FacultyID");

                    b.HasOne("Domain.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Faculty");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Domain.Entities.Faculty", b =>
                {
                    b.Navigation("ListAdmin");

                    b.Navigation("ListPost");
                });

            modelBuilder.Entity("Domain.Entities.Post", b =>
                {
                    b.Navigation("ListComent");
                });

            modelBuilder.Entity("Domain.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Navigation("ListComment");

                    b.Navigation("ListPost");
                });
#pragma warning restore 612, 618
        }
    }
}
