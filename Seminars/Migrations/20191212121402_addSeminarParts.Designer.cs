﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Seminars.Repositories;

namespace Seminars.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20191212121402_addSeminarParts")]
    partial class addSeminarParts
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Seminars.Models.FileModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Path");

                    b.HasKey("Id");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("Seminars.Models.Seminar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<string>("Excerpt");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Slug");

                    b.HasKey("Id");

                    b.ToTable("Seminars");
                });

            modelBuilder.Entity("Seminars.Models.SeminarPart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("ParentPartId");

                    b.Property<int>("SeminarId");

                    b.HasKey("Id");

                    b.HasIndex("ParentPartId");

                    b.HasIndex("SeminarId");

                    b.ToTable("SeminarParts");
                });

            modelBuilder.Entity("Seminars.Models.SeminarPart", b =>
                {
                    b.HasOne("Seminars.Models.SeminarPart", "Part")
                        .WithMany("Parts")
                        .HasForeignKey("ParentPartId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Seminars.Models.Seminar", "Seminar")
                        .WithMany("Parts")
                        .HasForeignKey("SeminarId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
