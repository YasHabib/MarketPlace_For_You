﻿// <auto-generated />
using System;
using MarketPlaceForYou.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MarketPlaceForYou.Repositories.Migrations
{
    [DbContext(typeof(MKPFYDbContext))]
    [Migration("20230212001620_changedColumnTypeToInt")]
    partial class changedColumnTypeToInt
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MarketPlaceForYou.Models.Entities.FAQ", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("FAQs");
                });

            modelBuilder.Entity("MarketPlaceForYou.Models.Entities.Listing", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("BuyerID")
                        .HasColumnType("text");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<int>("City")
                        .HasColumnType("integer");

                    b.Property<int>("Condition")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<string>("ProdName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Purchased")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BuyerID");

                    b.HasIndex("UserId");

                    b.ToTable("Listings");
                });

            modelBuilder.Entity("MarketPlaceForYou.Models.Entities.Notification", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsRead")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("SentDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Notification");
                });

            modelBuilder.Entity("MarketPlaceForYou.Models.Entities.SearchInput", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("SearchString")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("SearchedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("SearchInputs");
                });

            modelBuilder.Entity("MarketPlaceForYou.Models.Entities.Upload", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ListingId")
                        .HasColumnType("uuid");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ListingId");

                    b.ToTable("Uploads");
                });

            modelBuilder.Entity("MarketPlaceForYou.Models.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MarketPlaceForYou.Models.Entities.Listing", b =>
                {
                    b.HasOne("MarketPlaceForYou.Models.Entities.User", null)
                        .WithMany("Purchases")
                        .HasForeignKey("BuyerID");

                    b.HasOne("MarketPlaceForYou.Models.Entities.User", "User")
                        .WithMany("Listings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MarketPlaceForYou.Models.Entities.Upload", b =>
                {
                    b.HasOne("MarketPlaceForYou.Models.Entities.Listing", null)
                        .WithMany("Uploads")
                        .HasForeignKey("ListingId");
                });

            modelBuilder.Entity("MarketPlaceForYou.Models.Entities.Listing", b =>
                {
                    b.Navigation("Uploads");
                });

            modelBuilder.Entity("MarketPlaceForYou.Models.Entities.User", b =>
                {
                    b.Navigation("Listings");

                    b.Navigation("Purchases");
                });
#pragma warning restore 612, 618
        }
    }
}
