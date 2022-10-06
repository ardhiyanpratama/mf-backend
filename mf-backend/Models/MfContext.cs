using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace mf_backend.Models
{
    public partial class MfContext : DbContext
    {
        public MfContext()
        {
        }

        public MfContext(DbContextOptions<MfContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MsStorageLocation> MsStorageLocations { get; set; } = null!;
        public virtual DbSet<TrBpkb> TrBpkbs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MsStorageLocation>(entity =>
            {
                entity.HasKey(e => e.LocationId);

                entity.ToTable("ms_storage_location");

                entity.Property(e => e.LocationId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("location_id");

                entity.Property(e => e.LocationName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("location_name");
            });

            modelBuilder.Entity<TrBpkb>(entity =>
            {
                entity.HasKey(e => e.AgreementNumber);

                entity.ToTable("tr_bpkb");

                entity.Property(e => e.AgreementNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("agreement_number");

                entity.Property(e => e.BpkbDate)
                    .HasColumnType("datetime")
                    .HasColumnName("bpkb_date");

                entity.Property(e => e.BpkbDateIn)
                    .HasColumnType("datetime")
                    .HasColumnName("bpkb_date_in");

                entity.Property(e => e.BpkbNo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("bpkb_no");

                entity.Property(e => e.BranchId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("branch_id");

                entity.Property(e => e.FakturDate)
                    .HasColumnType("datetime")
                    .HasColumnName("faktur_date");

                entity.Property(e => e.FakturNo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("faktur_no");

                entity.Property(e => e.LocationId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("location_id");

                entity.Property(e => e.PoliceNo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("police_no");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.TrBpkbs)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_tr_bpkb_ms_storage_location");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
