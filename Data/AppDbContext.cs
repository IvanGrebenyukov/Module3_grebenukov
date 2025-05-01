using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WpfExamGrebenukov.Models;

namespace WpfExamGrebenukov.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MaterialType> MaterialTypes { get; set; }

    public virtual DbSet<Partner> Partners { get; set; }

    public virtual DbSet<PartnerProduct> PartnerProducts { get; set; }

    public virtual DbSet<PartnerType> PartnerTypes { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductType> ProductTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=examNewTestBdGrebenukov;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MaterialType>(entity =>
        {
            entity.HasKey(e => e.MaterialTypeId).HasName("PK_Material_type_import");

            entity.Property(e => e.MaterialTypeId)
                .ValueGeneratedNever()
                .HasColumnName("material_type_id");
            entity.Property(e => e.MaterialType1)
                .HasMaxLength(100)
                .HasColumnName("material_type");
            entity.Property(e => e.PercentageOfDefects)
                .HasMaxLength(100)
                .HasColumnName("percentage_of_defects");
        });

        modelBuilder.Entity<Partner>(entity =>
        {
            entity.HasKey(e => e.PartnerId).HasName("PK_Product_type_import");

            entity.Property(e => e.PartnerId)
                .UseIdentityColumn()
                .HasColumnName("partner_id");
            entity.Property(e => e.AddressPartner)
                .HasMaxLength(100)
                .HasColumnName("address_partner");
            entity.Property(e => e.Director)
                .HasMaxLength(100)
                .HasColumnName("director");
            entity.Property(e => e.EmailPartner)
                .HasMaxLength(100)
                .HasColumnName("email_partner");
            entity.Property(e => e.Inn).HasColumnName("inn");
            entity.Property(e => e.PartnerTypeId).HasColumnName("partner_type_id");
            entity.Property(e => e.PhonePartner)
                .HasMaxLength(100)
                .HasColumnName("phone_partner");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");

            entity.HasOne(d => d.PartnerTypes).WithMany(p => p.Partners)
                .HasForeignKey(d => d.PartnerTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Partner_import_partner_type_import");
        });

        modelBuilder.Entity<PartnerProduct>(entity =>
        {
            entity.HasKey(e => e.SalesId).HasName("PK_Partner_products_import");

            entity.Property(e => e.SalesId)
                .ValueGeneratedNever()
                .HasColumnName("sales_id");
            entity.Property(e => e.Count).HasColumnName("count");
            entity.Property(e => e.DataSales).HasColumnName("data_sales");
            entity.Property(e => e.PartnerId).HasColumnName("partner_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");

            entity.HasOne(d => d.Partner).WithMany(p => p.PartnerProducts)
                .HasForeignKey(d => d.PartnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Partner_products_import_Partner_import");

            entity.HasOne(d => d.Product).WithMany(p => p.PartnerProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Partner_products_import_Products_import");
        });

        modelBuilder.Entity<PartnerType>(entity =>
        {
            entity.HasKey(e => e.PartnerTypeId).HasName("PK_partner_type_import");

            entity.Property(e => e.PartnerTypeId)
                .ValueGeneratedNever()
                .HasColumnName("partner_type_id");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK_Products_import");

            entity.Property(e => e.ProductId)
                .ValueGeneratedNever()
                .HasColumnName("product_id");
            entity.Property(e => e.Article).HasColumnName("article");
            entity.Property(e => e.MaterialId).HasColumnName("material_id");
            entity.Property(e => e.MinCostForPartner)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("min_cost_for_partner");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
            entity.Property(e => e.TypeId).HasColumnName("type_id");

            entity.HasOne(d => d.MaterialTypes).WithMany(p => p.Products)
                .HasForeignKey(d => d.MaterialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_import_Material_type_import");

            entity.HasOne(d => d.ProductTypes).WithMany(p => p.Products)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_import_Product_type_import");
        });

        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.HasKey(e => e.TypeProductId).HasName("PK_Product_type_import_1");

            entity.Property(e => e.TypeProductId)
                .ValueGeneratedNever()
                .HasColumnName("type_product_id");
            entity.Property(e => e.CoefTypeProduct)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("coef_type_product");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
