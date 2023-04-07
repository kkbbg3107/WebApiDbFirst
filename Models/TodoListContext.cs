using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApiDBFirst.Models;

public partial class TodoListContext : DbContext
{
    public TodoListContext()
    {
    }

    public TodoListContext(DbContextOptions<TodoListContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeDetail> EmployeeDetails { get; set; }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustId);

            entity.ToTable("Customer");

            entity.HasIndex(e => e.CustNo, "IX_Customer_NO").IsUnique();

            entity.Property(e => e.CustId).HasColumnName("cust_id");
            entity.Property(e => e.CustCode)
                .HasMaxLength(50)
                .HasColumnName("cust_code");
            entity.Property(e => e.CustName)
                .HasMaxLength(50)
                .HasColumnName("cust_name");
            entity.Property(e => e.CustNo)
                .HasMaxLength(50)
                .HasColumnName("cust_no");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee");
        });

        modelBuilder.Entity<EmployeeDetail>(entity =>
        {
            entity.HasKey(e => e.EmployeeId);

            entity.ToTable("EmployeeDetail");

            entity.Property(e => e.EmployeeId).ValueGeneratedNever();
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(10);
            entity.Property(e => e.Salary).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Employee).WithOne(p => p.EmployeeDetail)
                .HasForeignKey<EmployeeDetail>(d => d.EmployeeId)
                .HasConstraintName("FK_EmployeeDetail_Employee");
        });

        modelBuilder.Entity<Login>(entity =>
        {
            entity.ToTable("Login");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Account).HasMaxLength(50);
            entity.Property(e => e.MemberName).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.RowId);

            entity.ToTable("Order");

            entity.HasIndex(e => e.CustNo, "IX_Order");

            entity.Property(e => e.RowId).HasColumnName("row_id");
            entity.Property(e => e.CustNo)
                .HasMaxLength(50)
                .HasColumnName("cust_no");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("price");

            entity.HasOne(d => d.CustNoNavigation).WithMany(p => p.Orders)
                .HasPrincipalKey(p => p.CustNo)
                .HasForeignKey(d => d.CustNo)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Order_Customer");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.RoleId).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
