using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.DBObjects;

namespace WebApplication1.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Comic> Comics { get; set; }

    public virtual DbSet<Franchise> Franchises { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.IdAuthor).HasName("PK__Author__7411B254A37544B1");

            entity.Property(e => e.IdAuthor).HasColumnName("id_author");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.AuthorName)
                .HasMaxLength(255)
                .HasColumnName("author_name");
            entity.Property(e => e.DateBirth).HasColumnName("date_birth");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
            entity.Property(e => e.Salary)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("salary");
        });

        modelBuilder.Entity<Comic>(entity =>
        {
            entity.HasKey(e => e.IdComic).HasName("PK__Comics__7F3656445A515673");

            entity.Property(e => e.IdComic).HasColumnName("id_comic");
            entity.Property(e => e.ComicName)
                .HasMaxLength(255)
                .HasColumnName("comic_name");
            entity.Property(e => e.IdAuthor).HasColumnName("id_author");
            entity.Property(e => e.IdFranchises).HasColumnName("id_franchises");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Rating)
                .HasColumnType("decimal(3, 2)")
                .HasColumnName("rating");
            entity.Property(e => e.ShortDescription)
                .HasMaxLength(1000)
                .HasColumnName("short_description");
            entity.Property(e => e.Stock).HasColumnName("stock");

            entity.HasOne(d => d.IdAuthorNavigation).WithMany(p => p.Comics)
                .HasForeignKey(d => d.IdAuthor)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Comics__id_autho__3D5E1FD2");

            entity.HasOne(d => d.IdFranchisesNavigation).WithMany(p => p.Comics)
                .HasForeignKey(d => d.IdFranchises)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Comics__id_franc__3E52440B");
        });

        modelBuilder.Entity<Franchise>(entity =>
        {
            entity.HasKey(e => e.IdFranchises).HasName("PK__Franchis__21DC623E3CF5F1F6");

            entity.Property(e => e.IdFranchises).HasColumnName("id_franchises");
            entity.Property(e => e.AverageRating)
                .HasDefaultValue(0.0m)
                .HasColumnType("decimal(3, 2)")
                .HasColumnName("average_rating");
            entity.Property(e => e.Budget)
                .HasColumnType("decimal(15, 2)")
                .HasColumnName("budget");
            entity.Property(e => e.DateFoundation).HasColumnName("date_foundation");
            entity.Property(e => e.FranchisesName)
                .HasMaxLength(255)
                .HasColumnName("franchises_name");
            entity.Property(e => e.Headquarters)
                .HasMaxLength(255)
                .HasColumnName("headquarters");
            entity.Property(e => e.IdAuthor).HasColumnName("id_author");

            entity.HasOne(d => d.IdAuthorNavigation).WithMany(p => p.Franchises)
                .HasForeignKey(d => d.IdAuthor)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Franchise__id_au__398D8EEE");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.IdOrder).HasName("PK__Orders__DD5B8F3F77026D0F");

            entity.Property(e => e.IdOrder).HasColumnName("id_order");
            entity.Property(e => e.IdComic).HasColumnName("id_comic");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("order_date");
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(50)
                .HasDefaultValue("processing")
                .HasColumnName("order_status");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .HasColumnName("payment_method");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.ShippingAddress)
                .HasMaxLength(255)
                .HasColumnName("shipping_address");
            entity.Property(e => e.ShippingMethod)
                .HasMaxLength(50)
                .HasColumnName("shipping_method");
            entity.Property(e => e.TransactionType)
                .HasMaxLength(10)
                .HasColumnName("transaction_type");

            entity.HasOne(d => d.IdComicNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdComic)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Orders__id_comic__4CA06362");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Orders__id_user__4BAC3F29");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK__Users__D2D1463755FCB5D9");

            entity.HasIndex(e => e.Email, "UQ__Users__AB6E61643125C7C0").IsUnique();

            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Budget)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("budget");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .HasDefaultValue("buyer")
                .HasColumnName("role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
