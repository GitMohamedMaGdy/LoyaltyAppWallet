﻿// <auto-generated />
using System;
using Loyalty.DataAccess.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Loyalty.Services.Migrations
{
    [DbContext(typeof(LoyaltyContext))]
    [Migration("20220406021305_deleteIdFromRegi")]
    partial class deleteIdFromRegi
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Loyalty.Domain.Models.Device", b =>
                {
                    b.Property<string>("DeviceLibraryIdentifier")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PushToken")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DeviceLibraryIdentifier");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("Loyalty.Domain.Models.Pass", b =>
                {
                    b.Property<string>("PassTypeIdentifier")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AuthenticationToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("LastUpdatedTag")
                        .HasColumnType("datetime2");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PassTypeIdentifier");

                    b.HasAlternateKey("PassTypeIdentifier", "SerialNumber")
                        .HasName("IX_UniquePassIndex");

                    b.ToTable("Passes");
                });

            modelBuilder.Entity("Loyalty.Domain.Models.Registration", b =>
                {
                    b.Property<string>("DeviceLibraryIdentifier")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PassTypeIdentifier")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PushToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SerialNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DeviceLibraryIdentifier", "PassTypeIdentifier");

                    b.HasIndex("PassTypeIdentifier");

                    b.ToTable("Registrations");
                });

            modelBuilder.Entity("Loyalty.Domain.Models.Registration", b =>
                {
                    b.HasOne("Loyalty.Domain.Models.Device", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceLibraryIdentifier")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Loyalty.Domain.Models.Pass", "Pass")
                        .WithMany()
                        .HasForeignKey("PassTypeIdentifier")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
