﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VehicleRegistration.WebApi.Infrastructure;

#nullable disable

namespace VehicleRegistration.WebApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BodyModel", b =>
                {
                    b.Property<int>("BodiesId")
                        .HasColumnType("int");

                    b.Property<int>("ModelsId")
                        .HasColumnType("int");

                    b.HasKey("BodiesId", "ModelsId");

                    b.HasIndex("ModelsId");

                    b.ToTable("BodyModel");
                });

            modelBuilder.Entity("EngineModel", b =>
                {
                    b.Property<int>("EnginesId")
                        .HasColumnType("int");

                    b.Property<int>("ModelsId")
                        .HasColumnType("int");

                    b.HasKey("EnginesId", "ModelsId");

                    b.HasIndex("ModelsId");

                    b.ToTable("EngineModel");
                });

            modelBuilder.Entity("VehicleRegistration.WebApi.Types.Body", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Bodies");
                });

            modelBuilder.Entity("VehicleRegistration.WebApi.Types.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Brand", (string)null);
                });

            modelBuilder.Entity("VehicleRegistration.WebApi.Types.Engine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("HorsePower")
                        .HasColumnType("float");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<double?>("Volume")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Engines");
                });

            modelBuilder.Entity("VehicleRegistration.WebApi.Types.Model", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<string>("ModelName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.ToTable("Models");
                });

            modelBuilder.Entity("BodyModel", b =>
                {
                    b.HasOne("VehicleRegistration.WebApi.Types.Body", null)
                        .WithMany()
                        .HasForeignKey("BodiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VehicleRegistration.WebApi.Types.Model", null)
                        .WithMany()
                        .HasForeignKey("ModelsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EngineModel", b =>
                {
                    b.HasOne("VehicleRegistration.WebApi.Types.Engine", null)
                        .WithMany()
                        .HasForeignKey("EnginesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VehicleRegistration.WebApi.Types.Model", null)
                        .WithMany()
                        .HasForeignKey("ModelsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VehicleRegistration.WebApi.Types.Model", b =>
                {
                    b.HasOne("VehicleRegistration.WebApi.Types.Brand", "Brand")
                        .WithMany("Models")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");
                });

            modelBuilder.Entity("VehicleRegistration.WebApi.Types.Brand", b =>
                {
                    b.Navigation("Models");
                });
#pragma warning restore 612, 618
        }
    }
}
