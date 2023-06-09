﻿using System;
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

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Login>(entity =>
        {
            entity.ToTable("Login");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Account).HasMaxLength(50);
            entity.Property(e => e.MemberName).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
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
