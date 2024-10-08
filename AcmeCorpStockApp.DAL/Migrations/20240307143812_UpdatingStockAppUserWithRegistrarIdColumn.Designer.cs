﻿// <auto-generated />
using System;
using AcmeCorpStockApp.DAL;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Migrations
{
    [DbContext(typeof(AcmeCorpAppDBContext))]
    [Migration("20240307143812_UpdatingStockAppUserWithRegistrarIdColumn")]
    partial class UpdatingStockAppUserWithRegistrarIdColumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Models.Product", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime>("Manufactured")
                        .HasColumnType("Date");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(100)");

                    b.Property<float>("Price")
                        .HasColumnType("float(5)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Models.StockAppUser", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("RegistrarEmail")
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Email");

                    b.ToTable("StockAppUser");
                });
#pragma warning restore 612, 618
        }
    }
}
