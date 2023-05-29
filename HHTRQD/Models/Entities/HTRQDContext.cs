using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HHTRQD.Models.Entities
{
    public partial class HTRQDContext : DbContext
    {
        public HTRQDContext()
        {
        }

        public HTRQDContext(DbContextOptions<HTRQDContext> options)
            : base(options)
        {
        }

        public virtual DbSet<LichSu> LichSu { get; set; }
        public virtual DbSet<LsngheNghiep> LsngheNghiep { get; set; }
        public virtual DbSet<LstieuChi> LstieuChi { get; set; }
        public virtual DbSet<LstrongSo> LstrongSo { get; set; }
        public virtual DbSet<NgheNghiep> NgheNghiep { get; set; }
        public virtual DbSet<TieuChi> TieuChi { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-5U8PRJL\\DUONG;Initial Catalog=HTRQD;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LichSu>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Ipmac)
                    .HasColumnName("IPMac")
                    .HasMaxLength(100);

                entity.Property(e => e.NgayTv)
                    .HasColumnName("NgayTV")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<LsngheNghiep>(entity =>
            {
                entity.ToTable("LSNgheNghiep");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Idls).HasColumnName("IDLS");

                entity.Property(e => e.Idnn).HasColumnName("IDNN");

                entity.HasOne(d => d.IdlsNavigation)
                    .WithMany(p => p.LsngheNghiep)
                    .HasForeignKey(d => d.Idls)
                    .HasConstraintName("FK_LSNgheNghiep_LichSu");

                entity.HasOne(d => d.IdnnNavigation)
                    .WithMany(p => p.LsngheNghiep)
                    .HasForeignKey(d => d.Idnn)
                    .HasConstraintName("FK_LSNgheNghiep_NgheNghiep");
            });

            modelBuilder.Entity<LstieuChi>(entity =>
            {
                entity.ToTable("LSTieuChi");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Idls).HasColumnName("IDLS");

                entity.Property(e => e.Idtc).HasColumnName("IDTC");

                entity.HasOne(d => d.IdlsNavigation)
                    .WithMany(p => p.LstieuChi)
                    .HasForeignKey(d => d.Idls)
                    .HasConstraintName("FK_LSTieuChi_LichSu");

                entity.HasOne(d => d.IdtcNavigation)
                    .WithMany(p => p.LstieuChi)
                    .HasForeignKey(d => d.Idtc)
                    .HasConstraintName("FK_LSTieuChi_TieuChi");
            });

            modelBuilder.Entity<LstrongSo>(entity =>
            {
                entity.ToTable("LSTrongSo");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Idls).HasColumnName("IDLS");

                entity.HasOne(d => d.IdlsNavigation)
                    .WithMany(p => p.LstrongSo)
                    .HasForeignKey(d => d.Idls)
                    .HasConstraintName("FK_LSTrongSo_LichSu");
            });

            modelBuilder.Entity<NgheNghiep>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.TcKngiaoTiep).HasColumnName("TC_KNGiaoTiep");

                entity.Property(e => e.TcKnlamViec).HasColumnName("TC_KNLamViec");

                entity.Property(e => e.TcNangLuc).HasColumnName("TC_NangLuc");

                entity.Property(e => e.TcThaiDoLamViec).HasColumnName("TC_ThaiDoLamViec");

                entity.Property(e => e.TcTuDuyLogic).HasColumnName("TC_TuDuyLogic");

                entity.Property(e => e.TenNn)
                    .HasColumnName("TenNN")
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<TieuChi>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MaTc)
                    .HasColumnName("MaTC")
                    .HasMaxLength(50);

                entity.Property(e => e.TenTc)
                    .HasColumnName("TenTC")
                    .HasMaxLength(200);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
