using Microsoft.EntityFrameworkCore;
using shopDAL_EF;
using System;
using System.Collections.Generic;

namespace shopDAL.Entities
{
    public partial class TradingDbContext : DbContext
    {
        public TradingDbContext()
        {
        }

        public TradingDbContext(DbContextOptions<TradingDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblBankCard> TblBankCards { get; set; }
        public virtual DbSet<TblBrand> TblBrands { get; set; }
        public virtual DbSet<TblCategory> TblCategories { get; set; }
        public virtual DbSet<TblProduct> TblProducts { get; set; }
        public virtual DbSet<TblSupplier> TblSuppliers { get; set; }
        public virtual DbSet<TblUser> TblUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // На майбутнє цей рядок краще винести в appsettings.json WPF проєкту
                optionsBuilder.UseSqlServer("Server=localhost;Database=TradingCompanyDB;User Id=sa;Password=ka21;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblBankCard>(entity =>
            {
                entity.HasKey(e => e.CardId);
                entity.ToTable("tblBankCard");
                entity.Property(e => e.CardId).HasColumnName("cardId");
                entity.Property(e => e.Number)
                    .HasMaxLength(16)
                    .HasColumnName("number");
                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User).WithMany(p => p.TblBankCards)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_tblBankCard_tblUser");
            });

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
                entity.HasIndex(e => e.CategoryId, "IX_tblProduct");

                entity.Property(e => e.ProductId).HasColumnName("productId");
                entity.Property(e => e.BrandId).HasColumnName("brandId");
                entity.Property(e => e.CategoryId).HasColumnName("categoryId");
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("createdAt");
                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("price");
                entity.Property(e => e.ProductName)
                    .HasMaxLength(200)
                    .HasColumnName("productName");
                entity.Property(e => e.QuantityInStock).HasColumnName("quantityInStock");
                entity.Property(e => e.SupplierId).HasColumnName("supplierId");
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

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.ToTable("tblUser");
                entity.HasIndex(e => e.Login, "UQ_tblUser_Login").IsUnique();

                entity.Property(e => e.UserId).HasColumnName("userId");
                entity.Property(e => e.Login)
                    .HasMaxLength(50)
                    .HasColumnName("login");
                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(64)
                    .HasColumnName("passwordHash");
                entity.Property(e => e.PasswordSalt).HasColumnName("passwordSalt");
                entity.Property(e => e.Role)
                    .HasMaxLength(20)
                    .HasDefaultValue("Guest");

                entity.Property(e => e.FirstName)
        .HasMaxLength(100)
        .HasColumnName("firstName");

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .HasColumnName("lastName");

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .HasColumnName("email");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .HasColumnName("phone");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasColumnName("address");

                entity.Property(e => e.Gender)
                    .HasMaxLength(20)
                    .HasColumnName("gender");

                entity.Property(e => e.SecretWord)
                    .HasMaxLength(100)
                    .HasColumnName("secretWord");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}