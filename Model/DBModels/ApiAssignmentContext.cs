﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Model.DBModels;

public partial class ApiAssignmentContext : DbContext
{
    public ApiAssignmentContext()
    {
    }

    public ApiAssignmentContext(DbContextOptions<ApiAssignmentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=ApiAssignment;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.MobileNumber).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
