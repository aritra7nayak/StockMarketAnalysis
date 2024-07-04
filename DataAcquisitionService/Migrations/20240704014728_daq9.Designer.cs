﻿// <auto-generated />
using System;
using DataAcquisitionService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAcquisitionService.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240704014728_daq9")]
    partial class daq9
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DataAcquisitionService.Models.Price", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<decimal?>("Close")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Exchange")
                        .HasColumnType("int");

                    b.Property<decimal?>("High")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("LTP")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Low")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("Open")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("PriceRunID")
                        .HasColumnType("int");

                    b.Property<int?>("SecurityID")
                        .HasColumnType("int");

                    b.Property<decimal?>("TradedVolume")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ID");

                    b.HasIndex("PriceRunID");

                    b.HasIndex("SecurityID");

                    b.ToTable("Prices");
                });

            modelBuilder.Entity("DataAcquisitionService.Models.PriceRun", b =>
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

                    b.ToTable("PriceRuns");
                });

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

            modelBuilder.Entity("DataAcquisitionService.Models.Price", b =>
                {
                    b.HasOne("DataAcquisitionService.Models.PriceRun", "PriceRun")
                        .WithMany()
                        .HasForeignKey("PriceRunID");

                    b.HasOne("DataAcquisitionService.Models.Security", "Security")
                        .WithMany()
                        .HasForeignKey("SecurityID");

                    b.Navigation("PriceRun");

                    b.Navigation("Security");
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
