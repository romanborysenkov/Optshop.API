﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OptShopAPI.Data;

#nullable disable

namespace OptShopAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231201145115_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.13");

            modelBuilder.Entity("OptShopAPI.Models.Product", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("characters")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("photoName")
                        .HasColumnType("TEXT");

                    b.Property<string>("photoSrc")
                        .HasColumnType("TEXT");

                    b.Property<int?>("price")
                        .IsRequired()
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.ToTable("products");
                });
#pragma warning restore 612, 618
        }
    }
}
