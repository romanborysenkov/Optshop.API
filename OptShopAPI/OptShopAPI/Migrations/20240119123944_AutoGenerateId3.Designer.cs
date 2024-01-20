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
    [Migration("20240119123944_AutoGenerateId3")]
    partial class AutoGenerateId3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.13");

            modelBuilder.Entity("OptShopAPI.Models.Customer", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("city")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("country")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("eircode")
                        .HasColumnType("TEXT");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("houseNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("mailbox")
                        .HasColumnType("TEXT");

                    b.Property<string>("orderIds")
                        .HasColumnType("TEXT");

                    b.Property<string>("phoneNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("plz")
                        .HasColumnType("TEXT");

                    b.Property<string>("postalCode")
                        .HasColumnType("TEXT");

                    b.Property<string>("province")
                        .HasColumnType("TEXT");

                    b.Property<string>("streetAddress")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("zip_code")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("customers");
                });

            modelBuilder.Entity("OptShopAPI.Models.Order", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("color")
                        .HasColumnType("TEXT");

                    b.Property<string>("description")
                        .HasColumnType("TEXT");

                    b.Property<int>("productCount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("productId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("status")
                        .HasColumnType("TEXT");

                    b.Property<int?>("totalPrice")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("orders");
                });

            modelBuilder.Entity("OptShopAPI.Models.Payment", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<int>("alreadyPaid")
                        .HasColumnType("INTEGER");

                    b.Property<string>("orderids")
                        .HasColumnType("TEXT");

                    b.Property<int>("totalPrice")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("payments");
                });

            modelBuilder.Entity("OptShopAPI.Models.Product", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("characters")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("color")
                        .HasColumnType("TEXT");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("minimalCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(200)
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
