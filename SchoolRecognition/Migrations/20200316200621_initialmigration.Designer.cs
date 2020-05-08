﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SchoolRecognition.Models;

namespace SchoolRecognition.Migrations
{
    [DbContext(typeof(SchoolRecognitionContext))]
    [Migration("20200316200621_initialmigration")]
    partial class initialmigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SchoolRecognition.Models.LocalGovernments", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(3)")
                        .HasMaxLength(3);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<Guid?>("StateId")
                        .HasColumnName("StateID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StateId");

                    b.ToTable("LocalGovernments");
                });

            modelBuilder.Entity("SchoolRecognition.Models.Offices", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("date");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<Guid?>("StateId")
                        .HasColumnName("StateID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Offices");
                });

            modelBuilder.Entity("SchoolRecognition.Models.PinHistories", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DateActive")
                        .HasColumnType("date");

                    b.Property<Guid?>("PinId")
                        .HasColumnName("PinID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SchoolId")
                        .HasColumnName("SchoolID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PinId");

                    b.HasIndex("SchoolId");

                    b.ToTable("PinHistories");
                });

            modelBuilder.Entity("SchoolRecognition.Models.Pins", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("date");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsInUse")
                        .HasColumnType("bit");

                    b.Property<Guid?>("RecognitionTypeId")
                        .HasColumnName("RecognitionTypeID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SerialPin")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("RecognitionTypeId");

                    b.ToTable("PINs");
                });

            modelBuilder.Entity("SchoolRecognition.Models.Ranks", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Ranks");
                });

            modelBuilder.Entity("SchoolRecognition.Models.RecognitionTypes", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasColumnType("varchar(3)")
                        .HasMaxLength(3)
                        .IsUnicode(false);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("RecognitionTypes");
                });

            modelBuilder.Entity("SchoolRecognition.Models.Roles", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSuperAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("SchoolRecognition.Models.SchoolCategories", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(2)")
                        .HasMaxLength(2);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("SchoolCategories");
                });

            modelBuilder.Entity("SchoolRecognition.Models.SchoolPayments", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal?>("Amount")
                        .HasColumnType("decimal(18, 0)");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("date");

                    b.Property<Guid?>("PinId")
                        .HasColumnName("PinID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("ReceiptImage")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ReceiptNo")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<Guid?>("SchoolId")
                        .HasColumnName("SchoolID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("PinId");

                    b.HasIndex("SchoolId");

                    b.ToTable("SchoolPayments");
                });

            modelBuilder.Entity("SchoolRecognition.Models.Schools", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<Guid?>("CategoryId")
                        .HasColumnName("CategoryID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<Guid?>("LgId")
                        .HasColumnName("LgID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<Guid?>("OfficeId")
                        .HasColumnName("OfficeID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PhoneNo")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<long?>("YearEstablished")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("LgId");

                    b.HasIndex("OfficeId");

                    b.ToTable("Schools");
                });

            modelBuilder.Entity("SchoolRecognition.Models.States", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(3)")
                        .HasMaxLength(3);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasName("IX_State")
                        .HasFilter("[Code] IS NOT NULL");

                    b.ToTable("States");
                });

            modelBuilder.Entity("SchoolRecognition.Models.Titles", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("ID")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Titles");
                });

            modelBuilder.Entity("SchoolRecognition.Models.Users", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("bit");

                    b.Property<string>("Lpno")
                        .HasColumnName("LPNO")
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("Others")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Password")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PhoneNo")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<Guid?>("RankId")
                        .HasColumnName("RankID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("RoleId")
                        .HasColumnName("RoleID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Signature")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RankId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SchoolRecognition.Models.LocalGovernments", b =>
                {
                    b.HasOne("SchoolRecognition.Models.States", "State")
                        .WithMany("LocalGovernments")
                        .HasForeignKey("StateId")
                        .HasConstraintName("FK_LocalGovernments_State");
                });

            modelBuilder.Entity("SchoolRecognition.Models.PinHistories", b =>
                {
                    b.HasOne("SchoolRecognition.Models.Pins", "Pin")
                        .WithMany("PinHistories")
                        .HasForeignKey("PinId")
                        .HasConstraintName("FK_PinHistory_PIN");

                    b.HasOne("SchoolRecognition.Models.Schools", "School")
                        .WithMany("PinHistories")
                        .HasForeignKey("SchoolId")
                        .HasConstraintName("FK_PinHistory_School");
                });

            modelBuilder.Entity("SchoolRecognition.Models.Pins", b =>
                {
                    b.HasOne("SchoolRecognition.Models.Users", "CreatedByNavigation")
                        .WithMany("Pins")
                        .HasForeignKey("CreatedBy")
                        .HasConstraintName("FK_PIN_User");

                    b.HasOne("SchoolRecognition.Models.RecognitionTypes", "RecognitionType")
                        .WithMany("Pins")
                        .HasForeignKey("RecognitionTypeId")
                        .HasConstraintName("FK_PIN_RecognitionType");
                });

            modelBuilder.Entity("SchoolRecognition.Models.SchoolPayments", b =>
                {
                    b.HasOne("SchoolRecognition.Models.Users", "CreatedByNavigation")
                        .WithMany("SchoolPayments")
                        .HasForeignKey("CreatedBy")
                        .HasConstraintName("FK_SchoolPayment_User");

                    b.HasOne("SchoolRecognition.Models.Pins", "Pin")
                        .WithMany("SchoolPayments")
                        .HasForeignKey("PinId")
                        .HasConstraintName("FK_SchoolPayment_PIN");

                    b.HasOne("SchoolRecognition.Models.Schools", "School")
                        .WithMany("SchoolPayments")
                        .HasForeignKey("SchoolId")
                        .HasConstraintName("FK_SchoolPayment_School");
                });

            modelBuilder.Entity("SchoolRecognition.Models.Schools", b =>
                {
                    b.HasOne("SchoolRecognition.Models.SchoolCategories", "Category")
                        .WithMany("Schools")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("FK_School_SchoolCategory");

                    b.HasOne("SchoolRecognition.Models.LocalGovernments", "Lg")
                        .WithMany("Schools")
                        .HasForeignKey("LgId")
                        .HasConstraintName("FK_School_LocalGovernment");

                    b.HasOne("SchoolRecognition.Models.Offices", "Office")
                        .WithMany("Schools")
                        .HasForeignKey("OfficeId")
                        .HasConstraintName("FK_School_Office");
                });

            modelBuilder.Entity("SchoolRecognition.Models.Users", b =>
                {
                    b.HasOne("SchoolRecognition.Models.Ranks", "Rank")
                        .WithMany("Users")
                        .HasForeignKey("RankId")
                        .HasConstraintName("FK_User_Rank");

                    b.HasOne("SchoolRecognition.Models.Roles", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_User_Role");
                });
#pragma warning restore 612, 618
        }
    }
}