﻿// <auto-generated />
using System;
using Caber.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Caber.Migrations
{
    [DbContext(typeof(CaberContext))]
    partial class CaberContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Caber.Models.Cab", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DriverId")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Make")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegistrationNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("SeatingCapacity")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DriverId");

                    b.HasIndex("RegistrationNumber")
                        .IsUnique();

                    b.ToTable("Cabs");
                });

            modelBuilder.Entity("Caber.Models.Driver", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("LicenseExpiryDate")
                        .HasColumnType("date");

                    b.Property<string>("LicenseNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("drivers", (string)null);
                });

            modelBuilder.Entity("Caber.Models.DriverRating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DriverId")
                        .HasColumnType("int");

                    b.Property<int>("PassengerId")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DriverId");

                    b.HasIndex("PassengerId");

                    b.ToTable("DriverRatings");
                });

            modelBuilder.Entity("Caber.Models.FavoritePlaces", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PassengerId")
                        .HasColumnType("int");

                    b.Property<string>("PlaceAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlaceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PassengerId");

                    b.ToTable("FavoritePlaces");
                });

            modelBuilder.Entity("Caber.Models.Passenger", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("passengers", (string)null);
                });

            modelBuilder.Entity("Caber.Models.Ride", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CabId")
                        .HasColumnType("int");

                    b.Property<string>("EndLocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<double?>("Fare")
                        .HasColumnType("float");

                    b.Property<string>("PassengerComment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PassengerId")
                        .HasColumnType("int");

                    b.Property<int?>("PassengerRating")
                        .HasColumnType("int");

                    b.Property<DateTime>("RideDate")
                        .HasColumnType("datetime2");

                    b.Property<float?>("RideDistance")
                        .HasColumnType("real");

                    b.Property<int>("RideStatus")
                        .HasColumnType("int");

                    b.Property<string>("StartLocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CabId");

                    b.HasIndex("PassengerId");

                    b.ToTable("Rides");
                });

            modelBuilder.Entity("Caber.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordHashKey")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Phone")
                        .IsUnique()
                        .HasFilter("[Phone] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Caber.Models.Cab", b =>
                {
                    b.HasOne("Caber.Models.Driver", "Driver")
                        .WithMany("OwnedCabs")
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Driver");
                });

            modelBuilder.Entity("Caber.Models.Driver", b =>
                {
                    b.HasOne("Caber.Models.User", "User")
                        .WithOne("Driver")
                        .HasForeignKey("Caber.Models.Driver", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Caber.Models.DriverRating", b =>
                {
                    b.HasOne("Caber.Models.Driver", "Driver")
                        .WithMany("DriverRatings")
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Caber.Models.Passenger", "Passenger")
                        .WithMany("DriverRatings")
                        .HasForeignKey("PassengerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Driver");

                    b.Navigation("Passenger");
                });

            modelBuilder.Entity("Caber.Models.FavoritePlaces", b =>
                {
                    b.HasOne("Caber.Models.Passenger", "Passenger")
                        .WithMany("FavoritePlaces")
                        .HasForeignKey("PassengerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Passenger");
                });

            modelBuilder.Entity("Caber.Models.Passenger", b =>
                {
                    b.HasOne("Caber.Models.User", "User")
                        .WithOne("Passenger")
                        .HasForeignKey("Caber.Models.Passenger", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Caber.Models.Ride", b =>
                {
                    b.HasOne("Caber.Models.Cab", "Cab")
                        .WithMany("Rides")
                        .HasForeignKey("CabId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Caber.Models.Passenger", "Passenger")
                        .WithMany("Rides")
                        .HasForeignKey("PassengerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Cab");

                    b.Navigation("Passenger");
                });

            modelBuilder.Entity("Caber.Models.Cab", b =>
                {
                    b.Navigation("Rides");
                });

            modelBuilder.Entity("Caber.Models.Driver", b =>
                {
                    b.Navigation("DriverRatings");

                    b.Navigation("OwnedCabs");
                });

            modelBuilder.Entity("Caber.Models.Passenger", b =>
                {
                    b.Navigation("DriverRatings");

                    b.Navigation("FavoritePlaces");

                    b.Navigation("Rides");
                });

            modelBuilder.Entity("Caber.Models.User", b =>
                {
                    b.Navigation("Driver")
                        .IsRequired();

                    b.Navigation("Passenger")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
