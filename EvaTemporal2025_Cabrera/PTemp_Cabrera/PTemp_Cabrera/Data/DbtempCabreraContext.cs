using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PTemp_Cabrera.Models;

namespace PTemp_Cabrera.Data;
public partial class DbtempCabreraContext : DbContext
{
    public DbtempCabreraContext()
    {
    }

    public DbtempCabreraContext(DbContextOptions<DbtempCabreraContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CConsumidor> CConsumidors { get; set; }

    public virtual DbSet<CEmpleado> CEmpleados { get; set; }

    public virtual DbSet<CEstado> CEstados { get; set; }

    public virtual DbSet<TAsesorium> TAsesoria { get; set; }

    public virtual DbSet<TAviso> TAvisos { get; set; }

    public virtual DbSet<TReclamo> TReclamos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CConsumidor>(entity =>
        {
            entity.HasKey(e => e.IdConsumidor).HasName("PK__c_Consum__9F510D96EADA833D");

            entity.ToTable("c_Consumidor");

            entity.HasIndex(e => e.DuiConsumidor, "UQ__c_Consum__D8164B0FD76A8495").IsUnique();

            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.ApellidoConsumidor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellidoConsumidor");
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("correoElectronico");
            entity.Property(e => e.Direccion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.DuiConsumidor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("duiConsumidor");
            entity.Property(e => e.NombreConsumidor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombreConsumidor");
        });

        modelBuilder.Entity<CEmpleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__c_Emplea__CE6D8B9E236AAC52");

            entity.ToTable("c_Empleado");

            entity.HasIndex(e => e.Usuario, "UQ__c_Emplea__9AFF8FC6E69321B9").IsUnique();

            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellidos");
            entity.Property(e => e.Clave)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("clave");
            entity.Property(e => e.Nombres)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombres");
            entity.Property(e => e.Usuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usuario");
        });

        modelBuilder.Entity<CEstado>(entity =>
        {
            entity.HasKey(e => e.IdEstado).HasName("PK__c_Estado__FBB0EDC11838E10B");

            entity.ToTable("c_Estado");

            entity.Property(e => e.NombreEstado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombreEstado");
        });

        modelBuilder.Entity<TAsesorium>(entity =>
        {
            entity.HasKey(e => e.IdAsesoria).HasName("PK__t_Asesor__899503BABCE5ABAF");

            entity.ToTable("t_Asesoria");

            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.FechaIngreso)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaIngreso");
            entity.Property(e => e.MotivoAsesoria)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("motivoAsesoria");
            entity.Property(e => e.RespuestaAsesoria)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("respuestaAsesoria");

            entity.HasOne(d => d.IdReclamoNavigation).WithMany(p => p.TAsesoria)
                .HasForeignKey(d => d.IdReclamo)
                .HasConstraintName("FK__t_Asesori__IdRec__46E78A0C");
        });

        modelBuilder.Entity<TAviso>(entity =>
        {
            entity.HasKey(e => e.IdAviso).HasName("PK__t_Aviso__5CBDD9A742CD3BBA");

            entity.ToTable("t_Aviso");

            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.DetalleAviso)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("detalleAviso");
            entity.Property(e => e.FechaIngreso)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaIngreso");

            entity.HasOne(d => d.IdReclamoNavigation).WithMany(p => p.TAvisos)
                .HasForeignKey(d => d.IdReclamo)
                .HasConstraintName("FK__t_Aviso__IdRecla__4AB81AF0");
        });

        modelBuilder.Entity<TReclamo>(entity =>
        {
            entity.HasKey(e => e.IdReclamo).HasName("PK__t_Reclam__19682C66881749A0");

            entity.ToTable("t_Reclamo");

            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.DetalleReclamo)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("detalleReclamo");
            entity.Property(e => e.DireccionProveedor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("direccionProveedor");
            entity.Property(e => e.FechaIngreso)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaIngreso");
            entity.Property(e => e.FechaRevision)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRevision");
            entity.Property(e => e.MontoReclamo)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("montoReclamo");
            entity.Property(e => e.NombreProveedor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombreProveedor");
            entity.Property(e => e.TelefonoProveedor)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("telefonoProveedor");

            entity.HasOne(d => d.IdConsumidorNavigation).WithMany(p => p.TReclamos)
                .HasForeignKey(d => d.IdConsumidor)
                .HasConstraintName("FK__t_Reclamo__IdCon__4222D4EF");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.TReclamos)
                .HasForeignKey(d => d.IdEmpleado)
                .HasConstraintName("FK__t_Reclamo__IdEmp__412EB0B6");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.TReclamos)
                .HasForeignKey(d => d.IdEstado)
                .HasConstraintName("FK__t_Reclamo__IdEst__4316F928");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
