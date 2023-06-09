﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OptimizationLabs.API.Contexts;

#nullable disable

namespace OptimizationLabs.API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230423193642_BaseUpdate_1")]
    partial class BaseUpdate_1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.2.23128.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OptimizationLabs.Shared.Entities.Car", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CarName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("CarVolumeLimit")
                        .HasColumnType("float");

                    b.Property<double>("CarWeightLimit")
                        .HasColumnType("float");

                    b.Property<double>("DeliveryCost")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("OptimizationLabs.Shared.Entities.Delivery", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CarId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("ItemId");

                    b.ToTable("Deliveries");
                });

            modelBuilder.Entity("OptimizationLabs.Shared.Entities.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ItemPrice")
                        .HasColumnType("float");

                    b.Property<double>("ItemVolume")
                        .HasColumnType("float");

                    b.Property<double>("ItemWeight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("OptimizationLabs.Shared.Entities.Delivery", b =>
                {
                    b.HasOne("OptimizationLabs.Shared.Entities.Car", "Car")
                        .WithMany("Deliveries")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OptimizationLabs.Shared.Entities.Item", "Item")
                        .WithMany("Deliveries")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("OptimizationLabs.Shared.Entities.Car", b =>
                {
                    b.Navigation("Deliveries");
                });

            modelBuilder.Entity("OptimizationLabs.Shared.Entities.Item", b =>
                {
                    b.Navigation("Deliveries");
                });
#pragma warning restore 612, 618
        }
    }
}
