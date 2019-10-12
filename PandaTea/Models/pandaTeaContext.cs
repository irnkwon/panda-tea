﻿using Microsoft.EntityFrameworkCore;

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

        public virtual DbSet<MenuModel> MenuModel { get; set; }
        public virtual DbSet<ProductModel> ProductModel { get; set; }
        public virtual DbSet<ReviewModel> ReviewModel { get; set; }
        public virtual DbSet<StoreModel> StoreModel { get; set; }
        public virtual DbSet<OrderModel> OrderModel { get; set; }
        public virtual DbSet<UserModel> UserModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MenuModel>(entity =>
            {
                entity.HasKey(e => e.MenuId);

                entity.ToTable("menuTbl");

                entity.Property(e => e.MenuId)
                    .HasColumnName("menuId")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Calories).HasColumnName("calories");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(6, 2)");

                entity.Property(e => e.ProductId)
                    .HasColumnName("productId")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Size)
                    .HasColumnName("size")
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<ProductModel>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.ToTable("productTbl");

                entity.Property(e => e.ProductId)
                    .HasColumnName("productId")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasColumnName("productName")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ReviewModel>(entity =>
            {
                entity.HasKey(e => e.ReviewId);

                entity.ToTable("reviewTbl");

                entity.Property(e => e.ReviewId)
                    .HasColumnName("reviewId")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.DateReviewed)
                    .HasColumnName("dateReviewed")
                    .HasColumnType("datetime");

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

            modelBuilder.Entity<StoreModel>(entity =>
            {
                entity.HasKey(e => e.StoreId);

                entity.ToTable("storeTbl");

                entity.Property(e => e.StoreId)
                    .HasColumnName("storeId")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

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

            modelBuilder.Entity<OrderModel>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.ToTable("orderTbl");

                entity.Property(e => e.OrderId)
                    .HasColumnName("orderId")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.DatePurchased)
                    .HasColumnName("datePurchased")
                    .HasColumnType("datetime");

                entity.Property(e => e.MenutId)
                    .HasColumnName("menutId")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.StoreId)
                    .HasColumnName("storeId")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasColumnType("numeric(18, 0)");
            });

            modelBuilder.Entity<UserModel>(entity =>
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

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("phoneNumber")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });
        }
    }
}
