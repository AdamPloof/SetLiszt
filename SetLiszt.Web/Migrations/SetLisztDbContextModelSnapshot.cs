﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SetLiszt.Web.Data;

#nullable disable

namespace SetLiszt.Web.Migrations
{
    [DbContext(typeof(SetLisztDbContext))]
    partial class SetLisztDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GigProject", b =>
                {
                    b.Property<int>("GigsId")
                        .HasColumnType("integer");

                    b.Property<int>("ProjectsId")
                        .HasColumnType("integer");

                    b.HasKey("GigsId", "ProjectsId");

                    b.HasIndex("ProjectsId");

                    b.ToTable("GigProject");
                });

            modelBuilder.Entity("GigSet", b =>
                {
                    b.Property<int>("GigsId")
                        .HasColumnType("integer");

                    b.Property<int>("SetsId")
                        .HasColumnType("integer");

                    b.HasKey("GigsId", "SetsId");

                    b.HasIndex("SetsId");

                    b.ToTable("GigSet");
                });

            modelBuilder.Entity("ProjectSet", b =>
                {
                    b.Property<int>("ProjectsId")
                        .HasColumnType("integer");

                    b.Property<int>("SetsId")
                        .HasColumnType("integer");

                    b.HasKey("ProjectsId", "SetsId");

                    b.HasIndex("SetsId");

                    b.ToTable("ProjectSet");
                });

            modelBuilder.Entity("ProjectSong", b =>
                {
                    b.Property<int>("ProjectsId")
                        .HasColumnType("integer");

                    b.Property<int>("SongsId")
                        .HasColumnType("integer");

                    b.HasKey("ProjectsId", "SongsId");

                    b.HasIndex("SongsId");

                    b.ToTable("ProjectSong");
                });

            modelBuilder.Entity("SetLiszt.Web.Models.Gig", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("HitAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Location")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Gig", (string)null);
                });

            modelBuilder.Entity("SetLiszt.Web.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("SetLiszt.Web.Models.Set", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("Set", (string)null);
                });

            modelBuilder.Entity("SetLiszt.Web.Models.Song", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Artist")
                        .HasColumnType("text");

                    b.Property<string>("Filepath")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Song", (string)null);
                });

            modelBuilder.Entity("SetSong", b =>
                {
                    b.Property<int>("SetsId")
                        .HasColumnType("integer");

                    b.Property<int>("SongsId")
                        .HasColumnType("integer");

                    b.HasKey("SetsId", "SongsId");

                    b.HasIndex("SongsId");

                    b.ToTable("SetSong");
                });

            modelBuilder.Entity("GigProject", b =>
                {
                    b.HasOne("SetLiszt.Web.Models.Gig", null)
                        .WithMany()
                        .HasForeignKey("GigsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SetLiszt.Web.Models.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GigSet", b =>
                {
                    b.HasOne("SetLiszt.Web.Models.Gig", null)
                        .WithMany()
                        .HasForeignKey("GigsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SetLiszt.Web.Models.Set", null)
                        .WithMany()
                        .HasForeignKey("SetsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectSet", b =>
                {
                    b.HasOne("SetLiszt.Web.Models.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SetLiszt.Web.Models.Set", null)
                        .WithMany()
                        .HasForeignKey("SetsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectSong", b =>
                {
                    b.HasOne("SetLiszt.Web.Models.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SetLiszt.Web.Models.Song", null)
                        .WithMany()
                        .HasForeignKey("SongsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SetSong", b =>
                {
                    b.HasOne("SetLiszt.Web.Models.Set", null)
                        .WithMany()
                        .HasForeignKey("SetsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SetLiszt.Web.Models.Song", null)
                        .WithMany()
                        .HasForeignKey("SongsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
