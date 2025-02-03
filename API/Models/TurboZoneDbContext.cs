using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Domain.Entity.Vehicles;

namespace API.Models;

public partial class TurboZoneDbContext : DbContext
{
    public TurboZoneDbContext()
    {
    }

    public TurboZoneDbContext(DbContextOptions<TurboZoneDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.Property(e => e.VehicleId).HasColumnName("Vehicle_ID");
            entity.Property(e => e.Brand)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Condition)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.ExteriorColor)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("Exterior_Color");
            entity.Property(e => e.FuelType)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("Fuel_Type");
            entity.Property(e => e.InteriorColor)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("Interior_Color");
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Motor)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Transmission)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
