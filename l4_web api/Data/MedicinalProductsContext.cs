using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using l4_web_api.Models;

namespace l4_web_api.Data;

public partial class MedicinalProductsContext : DbContext
{
    public MedicinalProductsContext()
    {
    }

    public MedicinalProductsContext(DbContextOptions<MedicinalProductsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CostMedicine> CostMedicines { get; set; }

    public virtual DbSet<Disease> Diseases { get; set; }

    public virtual DbSet<DiseasesAndSymptom> DiseasesAndSymptoms { get; set; }

    public virtual DbSet<FamilyMember> FamilyMembers { get; set; }

    public virtual DbSet<Medicine> Medicines { get; set; }

    public virtual DbSet<Prescription> Prescriptions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
            // Загружаем конфигурацию приложения
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory) // Указывает путь к исполняемому файлу
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // Загружаем appsettings.json
                .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CostMedicine>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CostMedi__3214EC071F0255F3");

            entity.Property(e => e.Manufacturer)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Medicines).WithMany(p => p.CostMedicines)
                .HasForeignKey(d => d.MedicinesId)
                .HasConstraintName("FK__CostMedic__Medic__4F7CD00D");
        });

        modelBuilder.Entity<Disease>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Diseases__3214EC071F6D1368");

            entity.Property(e => e.Consequences)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Duration)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Symptoms)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DiseasesAndSymptom>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Diseases__3214EC0726A88B5E");

            entity.Property(e => e.Dosage)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Diseases).WithMany(p => p.DiseasesAndSymptoms)
                .HasForeignKey(d => d.DiseasesId)
                .HasConstraintName("FK__DiseasesA__Disea__52593CB8");

            entity.HasOne(d => d.Medicines).WithMany(p => p.DiseasesAndSymptoms)
                .HasForeignKey(d => d.MedicinesId)
                .HasConstraintName("FK__DiseasesA__Medic__534D60F1");
        });

        modelBuilder.Entity<FamilyMember>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FamilyMe__3214EC0729C43A63");

            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Medicine>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Medicine__3214EC0765E9B951");

            entity.Property(e => e.Contraindications)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Indications)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Manufacturer)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Packaging)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Prescription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Prescrip__3214EC070760EAB1");

            entity.HasOne(d => d.Diseases).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.DiseasesId)
                .HasConstraintName("FK__Prescript__Disea__571DF1D5");

            entity.HasOne(d => d.FamilyMember).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.FamilyMemberId)
                .HasConstraintName("FK__Prescript__Famil__5629CD9C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
