using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PRN_Project2.Models
{
    public partial class PRN_ProjectContext : DbContext
    {
        public PRN_ProjectContext()
        {
        }

        public PRN_ProjectContext(DbContextOptions<PRN_ProjectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CommentHe160324> CommentHe160324s { get; set; } = null!;
        public virtual DbSet<OrderHe160324> OrderHe160324s { get; set; } = null!;
        public virtual DbSet<ProductHe160324> ProductHe160324s { get; set; } = null!;
        public virtual DbSet<UserHe160324> UserHe160324s { get; set; } = null!;
        public virtual DbSet<VerifyHe160324> VerifyHe160324s { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                              .SetBasePath(Directory.GetCurrentDirectory())
                              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("PRN_Project"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CommentHe160324>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Comment_HE160324");

                entity.Property(e => e.Comment)
                    .HasMaxLength(200)
                    .HasColumnName("comment");

                entity.Property(e => e.CommentDate)
                    .HasColumnType("date")
                    .HasColumnName("commentDate");

                entity.Property(e => e.CommentId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("commentID");

                entity.Property(e => e.ProductId)
                    .HasMaxLength(20)
                    .HasColumnName("productID");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .HasColumnName("userName");

                entity.HasOne(d => d.Product)
                    .WithMany()
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__Comment_H__produ__3C69FB99");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.UserName)
                    .HasConstraintName("FK__Comment_H__userN__3D5E1FD2");
            });

            modelBuilder.Entity<OrderHe160324>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__Order_HE__0809337D5FA5608E");

                entity.ToTable("Order_HE160324");

                entity.Property(e => e.OrderId).HasColumnName("orderID");

                entity.Property(e => e.Done).HasColumnName("done");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("date")
                    .HasColumnName("orderDate");

                entity.Property(e => e.PaymentMethod).HasColumnName("paymentMethod");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.ProductId)
                    .HasMaxLength(20)
                    .HasColumnName("productID");

                entity.Property(e => e.Receiver)
                    .HasMaxLength(50)
                    .HasColumnName("receiver");

                entity.Property(e => e.StatusDb).HasColumnName("statusDB");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .HasColumnName("userName");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderHe160324s)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__Order_HE1__produ__3E52440B");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.OrderHe160324s)
                    .HasForeignKey(d => d.UserName)
                    .HasConstraintName("FK__Order_HE1__userN__3F466844");
            });

            modelBuilder.Entity<ProductHe160324>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK__Product___2D10D14A33DC7080");

                entity.ToTable("Product_HE160324");

                entity.Property(e => e.ProductId)
                    .HasMaxLength(20)
                    .HasColumnName("productID");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .HasColumnName("description");

                entity.Property(e => e.Img)
                    .HasMaxLength(50)
                    .HasColumnName("img");

                entity.Property(e => e.PhonePrice).HasColumnName("phonePrice");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(100)
                    .HasColumnName("productName");

                entity.Property(e => e.Sale).HasColumnName("sale");

                entity.Property(e => e.ShikiName)
                    .HasMaxLength(50)
                    .HasColumnName("shikiName");

                entity.Property(e => e.Sold).HasColumnName("sold");
            });

            modelBuilder.Entity<UserHe160324>(entity =>
            {
                entity.HasKey(e => e.UserName)
                    .HasName("PK__User_HE1__66DCF95DE39F5E16");

                entity.ToTable("User_HE160324");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .HasColumnName("userName");

                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .HasColumnName("address");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.Facebook)
                    .HasMaxLength(100)
                    .HasColumnName("facebook");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.IngameId)
                    .HasMaxLength(50)
                    .HasColumnName("ingameID");

                entity.Property(e => e.IngameName)
                    .HasMaxLength(50)
                    .HasColumnName("ingameName");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.Roll).HasColumnName("roll");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UserId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("userID");
            });

            modelBuilder.Entity<VerifyHe160324>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Verify_HE160324");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .HasColumnName("userName");

                entity.Property(e => e.VerifyCode)
                    .HasMaxLength(50)
                    .HasColumnName("verifyCode");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.UserName)
                    .HasConstraintName("FK__Verify_HE__userN__403A8C7D");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
