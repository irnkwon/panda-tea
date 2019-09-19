﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PandaTea.Models
{
    public partial class PandaTeaContext : DbContext
    {
        public PandaTeaContext()
        {
        }

        public PandaTeaContext(DbContextOptions<PandaTeaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ProductTbl> ProductTbl { get; set; }
        public virtual DbSet<ReviewTbl> ReviewTbl { get; set; }
        public virtual DbSet<StoreTbl> StoreTbl { get; set; }
        public virtual DbSet<TransactionTbl> TransactionTbl { get; set; }
        public virtual DbSet<UserTbl> UserTbl { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=pandateadb;Persist Security Info=True;User ID=sa;Password=Conestoga1");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductTbl>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.ToTable("productTbl");

                entity.Property(e => e.ProductId)
                    .HasColumnName("productId")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(6, 2)");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasColumnName("productName")
                    .HasMaxLength(50);

                entity.Property(e => e.Size)
                    .HasColumnName("size")
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<ReviewTbl>(entity =>
            {
                entity.HasKey(e => e.ReviewId);

                entity.ToTable("reviewTbl");

                entity.Property(e => e.ReviewId)
                    .HasColumnName("reviewId")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.DateReviewed)
                    .HasColumnName("dateReviewed")
                    .HasColumnType("date");

                entity.Property(e => e.ProductId)
                    .HasColumnName("productId")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Score)
                    .HasColumnName("score")
                    .HasColumnType("numeric(3, 1)");

                entity.Property(e => e.Text).HasColumnName("text");

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasColumnType("numeric(18, 0)");
            });

            modelBuilder.Entity<StoreTbl>(entity =>
            {
                entity.HasKey(e => e.StoreId);

                entity.ToTable("storeTbl");

                entity.Property(e => e.StoreId)
                    .HasColumnName("storeId")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(50);

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(50);

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasMaxLength(50);

                entity.Property(e => e.PostalCode)
                    .HasColumnName("postalCode")
                    .HasMaxLength(50);

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasMaxLength(50);

                entity.Property(e => e.StoreName)
                    .HasColumnName("storeName")
                    .HasMaxLength(50);

                entity.Property(e => e.Street)
                    .HasColumnName("street")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TransactionTbl>(entity =>
            {
                entity.HasKey(e => e.TransactionId);

                entity.ToTable("transactionTbl");

                entity.Property(e => e.TransactionId)
                    .HasColumnName("transactionId")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.DatePurchased)
                    .HasColumnName("datePurchased")
                    .HasColumnType("date");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(6, 2)");

                entity.Property(e => e.ProductId)
                    .HasColumnName("productId")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.StoreId)
                    .HasColumnName("storeId")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasColumnType("numeric(18, 0)");
            });

            modelBuilder.Entity<UserTbl>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("userTbl");

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.DateRegistered)
                    .HasColumnName("dateRegistered")
                    .HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .HasColumnName("firstName")
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .HasColumnName("lastName")
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("phoneNumber")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });
        }
    }
}