﻿// <auto-generated />
using System;
using DataAcquisitionService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAcquisitionService.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DataAcquisitionService.Models.Security", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int?>("BseCode")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ListingDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("MarketLot")
                        .HasColumnType("int");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SecurityRunID")
                        .HasColumnType("int");

                    b.Property<int?>("SecurityType")
                        .HasColumnType("int");

                    b.Property<string>("Series")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Symbol")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("SecurityRunID");

                    b.ToTable("Securities");
                });

            modelBuilder.Entity("DataAcquisitionService.Models.SecurityRun", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("ErrorFilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ErrorMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("InsertType")
                        .HasColumnType("int");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProcessType")
                        .HasColumnType("int");

                    b.Property<int?>("RowsAdded")
                        .HasColumnType("int");

                    b.Property<int?>("RowsDeleted")
                        .HasColumnType("int");

                    b.Property<int?>("RowsTotal")
                        .HasColumnType("int");

                    b.Property<int?>("RowsUpdated")
                        .HasColumnType("int");

                    b.Property<int?>("RowsWarning")
                        .HasColumnType("int");

                    b.Property<int>("SourceType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("SecurityRun");
                });

            modelBuilder.Entity("DataAcquisitionService.Models.Security", b =>
                {
                    b.HasOne("DataAcquisitionService.Models.SecurityRun", "SecurityRun")
                        .WithMany()
                        .HasForeignKey("SecurityRunID");

                    b.Navigation("SecurityRun");
                });
#pragma warning restore 612, 618
        }
    }
}
