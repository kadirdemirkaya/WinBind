﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WinBind.Persistence.Data;

#nullable disable

namespace WinBind.Persistence.Migrations
{
    [DbContext(typeof(WinBindDbContext))]
    [Migration("20250124202613_auction_isactive_added")]
    partial class auction_isactive_added
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WinBind.Domain.Entities.Auction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AppUserId")
                        .HasColumnType("uuid");

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("StartingPrice")
                        .HasColumnType("numeric");

                    b.Property<DateTime?>("UpdatedAtUtc")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("ProductId");

                    b.ToTable("Auctions");
                });

            modelBuilder.Entity("WinBind.Domain.Entities.AuctionResult", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuctionId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ClosedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("FinalPrice")
                        .HasColumnType("numeric");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("UpdatedAtUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("WinningBid")
                        .HasColumnType("numeric");

                    b.Property<Guid>("WinningBidId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AuctionId")
                        .IsUnique();

                    b.HasIndex("WinningBidId");

                    b.ToTable("AuctionResults");
                });

            modelBuilder.Entity("WinBind.Domain.Entities.Basket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("UpdatedAtUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Baskets");
                });

            modelBuilder.Entity("WinBind.Domain.Entities.BasketItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("AddedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("BasketId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedAtUtc")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("BasketId");

                    b.HasIndex("ProductId");

                    b.ToTable("BasketItems");
                });

            modelBuilder.Entity("WinBind.Domain.Entities.Bid", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuctionId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("BidAmount")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("BidDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("UpdatedAtUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AuctionId");

                    b.HasIndex("UserId");

                    b.ToTable("Bids");
                });

            modelBuilder.Entity("WinBind.Domain.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAtUtc")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("WinBind.Domain.Entities.Identity.AppRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<DateTime?>("UpdatedAtUtc")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("WinBind.Domain.Entities.Identity.AppRoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("WinBind.Domain.Entities.Identity.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("UpdatedAtUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("WinBind.Domain.Entities.Identity.AppUserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("WinBind.Domain.Entities.Identity.AppUserLogin", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("WinBind.Domain.Entities.Identity.AppUserRole", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("WinBind.Domain.Entities.Identity.AppUserToken", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("WinBind.Domain.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BasketId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("numeric");

                    b.Property<DateTime?>("UpdatedAtUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("BasketId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("WinBind.Domain.Entities.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAtUtc")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("WinBind.Domain.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("BandColor")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CaseColor")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CaseShape")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DialColor")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsAuctionProduct")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<string>("SKU")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("StockCount")
                        .HasColumnType("integer");

                    b.Property<string>("Technology")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAtUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("WinBind.Domain.Entities.ProductImage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedAtUtc")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductImages");
                });

            modelBuilder.Entity("WinBind.Domain.Entities.Auction", b =>
                {
                    b.HasOne("WinBind.Domain.Entities.Identity.AppUser", "AppUser")
                        .WithMany("Auctions")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WinBind.Domain.Entities.Product", "Product")
                        .WithMany("Auctions")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("WinBind.Domain.Entities.AuctionResult", b =>
                {
                    b.HasOne("WinBind.Domain.Entities.Auction", "Auction")
                        .WithOne("AuctionResult")
                        .HasForeignKey("WinBind.Domain.Entities.AuctionResult", "AuctionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("WinBind.Domain.Entities.Bid", "WinningBidDetails")
                        .WithMany()
                        .HasForeignKey("WinningBidId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Auction");

                    b.Navigation("WinningBidDetails");
                });

            modelBuilder.Entity("WinBind.Domain.Entities.Basket", b =>
                {
                    b.HasOne("WinBind.Domain.Entities.Identity.AppUser", "AppUser")
                        .WithMany("Baskets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("WinBind.Domain.Entities.BasketItem", b =>
                {
                    b.HasOne("WinBind.Domain.Entities.Basket", "Basket")
                        .WithMany("BasketItems")
                        .HasForeignKey("BasketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WinBind.Domain.Entities.Product", "Product")
                        .WithMany("BasketItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Basket");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("WinBind.Domain.Entities.Bid", b =>
                {
                    b.HasOne("WinBind.Domain.Entities.Auction", "Auction")
                        .WithMany("Bids")
                        .HasForeignKey("AuctionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WinBind.Domain.Entities.Identity.AppUser", "AppUser")
                        .WithMany("Bids")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("Auction");
                });

            modelBuilder.Entity("WinBind.Domain.Entities.Identity.AppRoleClaim", b =>
                {
                    b.HasOne("WinBind.Domain.Entities.Identity.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WinBind.Domain.Entities.Identity.AppUserClaim", b =>
                {
                    b.HasOne("WinBind.Domain.Entities.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WinBind.Domain.Entities.Identity.AppUserLogin", b =>
                {
                    b.HasOne("WinBind.Domain.Entities.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WinBind.Domain.Entities.Identity.AppUserRole", b =>
                {
                    b.HasOne("WinBind.Domain.Entities.Identity.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WinBind.Domain.Entities.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WinBind.Domain.Entities.Identity.AppUserToken", b =>
                {
                    b.HasOne("WinBind.Domain.Entities.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WinBind.Domain.Entities.Order", b =>
                {
                    b.HasOne("WinBind.Domain.Entities.Basket", "Basket")
                        .WithMany()
                        .HasForeignKey("BasketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WinBind.Domain.Entities.Identity.AppUser", "AppUser")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("Basket");
                });

            modelBuilder.Entity("WinBind.Domain.Entities.Payment", b =>
                {
                    b.HasOne("WinBind.Domain.Entities.Order", "Order")
                        .WithMany("Payments")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("WinBind.Domain.Entities.Product", b =>
                {
                    b.HasOne("WinBind.Domain.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WinBind.Domain.Entities.Identity.AppUser", "AppUser")
                        .WithMany("Products")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("WinBind.Domain.Entities.ProductImage", b =>
                {
                    b.HasOne("WinBind.Domain.Entities.Product", "Product")
                        .WithMany("ProductImages")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("WinBind.Domain.Entities.Auction", b =>
                {
                    b.Navigation("AuctionResult")
                        .IsRequired();

                    b.Navigation("Bids");
                });

            modelBuilder.Entity("WinBind.Domain.Entities.Basket", b =>
                {
                    b.Navigation("BasketItems");
                });

            modelBuilder.Entity("WinBind.Domain.Entities.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("WinBind.Domain.Entities.Identity.AppUser", b =>
                {
                    b.Navigation("Auctions");

                    b.Navigation("Baskets");

                    b.Navigation("Bids");

                    b.Navigation("Orders");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("WinBind.Domain.Entities.Order", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("WinBind.Domain.Entities.Product", b =>
                {
                    b.Navigation("Auctions");

                    b.Navigation("BasketItems");

                    b.Navigation("ProductImages");
                });
#pragma warning restore 612, 618
        }
    }
}
