using System;
using System.Collections.Generic;
using ClothingStoreApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ClothingStoreApi.DBContext
{
    public partial class ClothingStoreContext : DbContext
    {
        public ClothingStoreContext()
        {
        }

        public ClothingStoreContext(DbContextOptions<ClothingStoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Advertisement> Advertisements { get; set; } = null!;
        public virtual DbSet<AdvertisementAttribute> AdvertisementAttributes { get; set; } = null!;
        public virtual DbSet<Discount> Discounts { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Rating> Ratings { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-0D2M39V;Database=ClothingStore;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Advertisement>(entity =>
            {
                entity.HasKey(e => e.AdId)
                    .HasName("PK__Advertis__CAA4A62799049C04");

                entity.ToTable("Advertisement");

                entity.Property(e => e.AdId).HasColumnName("ad_id");

                entity.Property(e => e.AdvImage).HasColumnName("advImage");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("price");

                entity.Property(e => e.PublicationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("publicationDate");

                entity.Property(e => e.SellerId).HasColumnName("seller_id");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .HasColumnName("title");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.Advertisements)
                    .HasForeignKey(d => d.SellerId)
                    .HasConstraintName("FK__Advertise__selle__398D8EEE");
            });

            modelBuilder.Entity<AdvertisementAttribute>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PK__Advertis__D54EE9B4BB8816B1");

                entity.ToTable("Advertisement_Attribute");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.AdId).HasColumnName("ad_id");

                entity.Property(e => e.Brand)
                    .HasMaxLength(50)
                    .HasColumnName("brand");

                entity.Property(e => e.Category)
                    .HasMaxLength(50)
                    .HasColumnName("category");

                entity.Property(e => e.Color)
                    .HasMaxLength(50)
                    .HasColumnName("color");

                entity.Property(e => e.Size)
                    .HasMaxLength(20)
                    .HasColumnName("size");

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .HasColumnName("type");

                entity.HasOne(d => d.Ad)
                    .WithMany(p => p.AdvertisementAttributes)
                    .HasForeignKey(d => d.AdId)
                    .HasConstraintName("FK__Advertise__ad_id__3C69FB99");
            });

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.ToTable("Discount");

                entity.Property(e => e.DiscountId).HasColumnName("discount_id");

                entity.Property(e => e.AdId).HasColumnName("ad_id");

                entity.Property(e => e.DiscountPercentage)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("discountPercentage");

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("endDate");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("startDate");

                entity.HasOne(d => d.Ad)
                    .WithMany(p => p.Discounts)
                    .HasForeignKey(d => d.AdId)
                    .HasConstraintName("FK__Discount__ad_id__46E78A0C");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.AdId).HasColumnName("ad_id");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasColumnName("orderDate");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Ad)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.AdId)
                    .HasConstraintName("FK__Order__ad_id__403A8C7D");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Order__user_id__3F466844");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.ToTable("Rating");

                entity.Property(e => e.RatingId).HasColumnName("rating_id");

                entity.Property(e => e.AdId).HasColumnName("ad_id");

                entity.Property(e => e.RatingDate)
                    .HasColumnType("datetime")
                    .HasColumnName("ratingDate");

                entity.Property(e => e.RatingValue)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("ratingValue");

                entity.Property(e => e.Review).HasColumnName("review");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Ad)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.AdId)
                    .HasConstraintName("FK__Rating__ad_id__4316F928");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Rating__user_id__440B1D61");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.AccountStatus)
                    .HasMaxLength(50)
                    .HasColumnName("accountStatus");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasColumnName("address");

                entity.Property(e => e.Avatar).HasColumnName("avatar");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("date")
                    .HasColumnName("dateOfBirth");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .HasColumnName("firstName");

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .HasColumnName("lastName");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .HasColumnName("phoneNumber");

                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .HasColumnName("role");

                entity.Property(e => e.Sex)
                    .HasMaxLength(50)
                    .HasColumnName("sex");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
