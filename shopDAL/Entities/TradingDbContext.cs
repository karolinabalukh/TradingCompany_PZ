using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace shopDAL.Entities;

public partial class TradingDbContext : DbContext
{
    public TradingDbContext(DbContextOptions<TradingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblBrand> TblBrands { get; set; }

    public virtual DbSet<TblCategory> TblCategories { get; set; }

    public virtual DbSet<TblProduct> TblProducts { get; set; }

    public virtual DbSet<TblSupplier> TblSuppliers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblBrand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("PK__tblBrand__06B772997F487293");

            entity.ToTable("tblBrand");

            entity.Property(e => e.BrandId).HasColumnName("brandId");
            entity.Property(e => e.BrandName)
                .HasMaxLength(100)
                .HasColumnName("brandName");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .HasColumnName("country");
        });

        modelBuilder.Entity<TblCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__tblCateg__23CAF1D820A6EE58");

            entity.ToTable("tblCategory");

            entity.Property(e => e.CategoryId).HasColumnName("categoryId");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(100)
                .HasColumnName("categoryName");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
        });

        modelBuilder.Entity<TblProduct>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__tblProdu__2D10D16ABEACB140");

            entity.ToTable("tblProduct");

            entity.Property(e => e.ProductId).HasColumnName("productId");
            entity.Property(e => e.BrandId).HasColumnName("brandId");
            entity.Property(e => e.CategoryId).HasColumnName("categoryId");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("price");
            entity.Property(e => e.ProductName)
                .HasMaxLength(200)
                .HasColumnName("productName");
            entity.Property(e => e.QuantityInStock).HasColumnName("quantityInStock");
            entity.Property(e => e.SupplierId).HasColumnName("supplierId");

            entity.Property(e => e.CreatedAt)
                 .HasColumnType("datetime")
                 .HasColumnName("createdAt");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Brand).WithMany(p => p.TblProducts)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Brand");

            entity.HasOne(d => d.Category).WithMany(p => p.TblProducts)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Category");

            entity.HasOne(d => d.Supplier).WithMany(p => p.TblProducts)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Supplier");
        });

        modelBuilder.Entity<TblSupplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PK__tblSuppl__DB8E62EDEB8DCD40");

            entity.ToTable("tblSupplier");

            entity.Property(e => e.SupplierId).HasColumnName("supplierId");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(150)
                .HasColumnName("companyName");
            entity.Property(e => e.ContactPhone)
                .HasMaxLength(20)
                .HasColumnName("contactPhone");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
