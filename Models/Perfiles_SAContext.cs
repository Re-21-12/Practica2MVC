using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Perfiles_SA.Models
{
    public partial class Perfiles_SAContext : DbContext
    {
        public Perfiles_SAContext()
        {
        }

        public Perfiles_SAContext(DbContextOptions<Perfiles_SAContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Empleado> Empleados { get; set; } = null!;
            
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(e => e.Dpi)
                    .HasName("PK__Empleado__C035891E7325733A");

                entity.ToTable("Empleado");

                entity.Property(e => e.Dpi)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("DPI");

                entity.Property(e => e.DepartamentoAsignado).HasMaxLength(50);

                entity.Property(e => e.Direccion).HasMaxLength(255);

                entity.Property(e => e.Edad).HasComputedColumnSql("([dbo].[CalcularEdadFunc]([FechaNacimiento]))", false);

                entity.Property(e => e.FechaIngresoEmpresa).HasColumnType("datetime");

                entity.Property(e => e.FechaNacimiento).HasColumnType("datetime");

                entity.Property(e => e.Genero)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Nit)
                    .HasMaxLength(20)
                    .HasColumnName("NIT");

                entity.Property(e => e.Nombres)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
