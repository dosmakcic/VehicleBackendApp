﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project.Service.Data;

#nullable disable

namespace Project.Service.Migrations
{
    [DbContext(typeof(VehicleContext))]
    [Migration("20240805162256_UpdateVehicleMakeData")]
    partial class UpdateVehicleMakeData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Project.Service.Models.VehicleMake", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Abrv")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("VehicleMakes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Abrv = "BMW",
                            Name = "Bayerische Motoren Werke"
                        },
                        new
                        {
                            Id = 2,
                            Abrv = "Ford",
                            Name = "Ford"
                        },
                        new
                        {
                            Id = 3,
                            Abrv = "VW",
                            Name = "Volkswagen"
                        });
                });

            modelBuilder.Entity("Project.Service.Models.VehicleModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Abrv")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MakeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MakeId");

                    b.ToTable("VehicleModels");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Abrv = "X3",
                            MakeId = 1,
                            Name = "BMW X3"
                        },
                        new
                        {
                            Id = 3,
                            Abrv = "X5",
                            MakeId = 1,
                            Name = "X5"
                        },
                        new
                        {
                            Id = 4,
                            Abrv = "MST",
                            MakeId = 2,
                            Name = "Mustang"
                        },
                        new
                        {
                            Id = 5,
                            Abrv = "EXP",
                            MakeId = 2,
                            Name = "Explorer"
                        },
                        new
                        {
                            Id = 6,
                            Abrv = "GLF",
                            MakeId = 3,
                            Name = "Golf"
                        },
                        new
                        {
                            Id = 7,
                            Abrv = "PLO",
                            MakeId = 3,
                            Name = "Polo"
                        });
                });

            modelBuilder.Entity("Project.Service.Models.VehicleModel", b =>
                {
                    b.HasOne("Project.Service.Models.VehicleMake", "VehicleMake")
                        .WithMany("VehicleModels")
                        .HasForeignKey("MakeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("VehicleMake");
                });

            modelBuilder.Entity("Project.Service.Models.VehicleMake", b =>
                {
                    b.Navigation("VehicleModels");
                });
#pragma warning restore 612, 618
        }
    }
}