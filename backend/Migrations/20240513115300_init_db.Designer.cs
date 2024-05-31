﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using backend.Data;

#nullable disable

namespace backend.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240513115300_init_db")]
    partial class init_db
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.24")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("backend.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("KeepLoginIn")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<string>("Role")
                        .HasColumnType("longtext");

                    b.Property<string>("SaltPassword")
                        .HasColumnType("longtext");

                    b.Property<string>("Token")
                        .HasColumnType("longtext");

                    b.Property<string>("UserEmail")
                        .HasColumnType("longtext");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("backend.Models.Cycle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Libele")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Cycles");
                });

            modelBuilder.Entity("backend.Models.Doc", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AnneeAcademique")
                        .HasColumnType("longtext");

                    b.Property<int?>("AnneeSortie")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("CycleId")
                        .HasColumnType("int");

                    b.Property<string>("DateSign")
                        .HasColumnType("longtext");

                    b.Property<int?>("Fichier")
                        .HasColumnType("int");

                    b.Property<int?>("FiliereId")
                        .HasColumnType("int");

                    b.Property<string>("Numero")
                        .HasColumnType("longtext");

                    b.Property<string>("Objet")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Promotion")
                        .HasColumnType("longtext");

                    b.Property<string>("Session")
                        .HasColumnType("longtext");

                    b.Property<string>("Source")
                        .HasColumnType("longtext");

                    b.Property<string>("TypeDoc")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("CycleId");

                    b.HasIndex("FiliereId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("backend.Models.Filiere", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Libele")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Filieres");
                });

            modelBuilder.Entity("backend.Models.Doc", b =>
                {
                    b.HasOne("backend.Models.Cycle", "Cycle")
                        .WithMany("Documents")
                        .HasForeignKey("CycleId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("backend.Models.Filiere", "Filiere")
                        .WithMany("Documents")
                        .HasForeignKey("FiliereId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Cycle");

                    b.Navigation("Filiere");
                });

            modelBuilder.Entity("backend.Models.Cycle", b =>
                {
                    b.Navigation("Documents");
                });

            modelBuilder.Entity("backend.Models.Filiere", b =>
                {
                    b.Navigation("Documents");
                });
#pragma warning restore 612, 618
        }
    }
}
