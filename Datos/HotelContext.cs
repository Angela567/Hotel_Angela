﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Datos;

public partial class HotelContext : DbContext
{
    public HotelContext()
    {
    }

    public HotelContext(DbContextOptions<HotelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Disponibilidad> Disponibilidads { get; set; }

    public virtual DbSet<Fecha> Fechas { get; set; }

    public virtual DbSet<Habitacion> Habitacions { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Sede> Sedes { get; set; }

    public virtual DbSet<TipoHabitacion> TipoHabitacions { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOPANGELA\\SQLEXPRESS; Database=Hotel; Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Disponibilidad>(entity =>
        {
            entity.HasKey(e => e.IdDisponibilidad).HasName("PK__Disponib__D49C2B2C8F210387");

            entity.ToTable("Disponibilidad");

            entity.Property(e => e.IdDisponibilidad).HasColumnName("ID_Disponibilidad");
            entity.Property(e => e.Disponible).HasDefaultValue(true);
            entity.Property(e => e.IdFecha).HasColumnName("ID_Fecha");
            entity.Property(e => e.IdHabitacion).HasColumnName("ID_Habitacion");

            entity.HasOne(d => d.IdFechaNavigation).WithMany(p => p.Disponibilidads)
                .HasForeignKey(d => d.IdFecha)
                .HasConstraintName("FK__Disponibi__ID_Fe__34C8D9D1");

            entity.HasOne(d => d.IdHabitacionNavigation).WithMany(p => p.Disponibilidads)
                .HasForeignKey(d => d.IdHabitacion)
                .HasConstraintName("FK__Disponibi__ID_Ha__33D4B598");
        });

        modelBuilder.Entity<Fecha>(entity =>
        {
            entity.HasKey(e => e.IdFecha).HasName("PK__Fecha__8E9BDE521ED141E1");

            entity.ToTable("Fecha");

            entity.Property(e => e.IdFecha).HasColumnName("ID_Fecha");
            entity.Property(e => e.Fecha1).HasColumnName("Fecha");
        });

        modelBuilder.Entity<Habitacion>(entity =>
        {
            entity.HasKey(e => e.IdHabitacion).HasName("PK__Habitaci__9B683254B0824522");

            entity.ToTable("Habitacion");

            entity.Property(e => e.IdHabitacion).HasColumnName("ID_Habitacion");
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.IdSede).HasColumnName("ID_Sede");
            entity.Property(e => e.IdTipoHabitacion).HasColumnName("ID_Tipo_Habitacion");
            entity.Property(e => e.NumeroHabitacion)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Numero_Habitacion");

            entity.HasOne(d => d.IdSedeNavigation).WithMany(p => p.Habitacions)
                .HasForeignKey(d => d.IdSede)
                .HasConstraintName("FK__Habitacio__ID_Se__2E1BDC42");

            entity.HasOne(d => d.IdTipoHabitacionNavigation).WithMany(p => p.Habitacions)
                .HasForeignKey(d => d.IdTipoHabitacion)
                .HasConstraintName("FK__Habitacio__ID_Ti__2D27B809");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.IdReserva).HasName("PK__Reserva__12CAD9F47601FCD8");

            entity.ToTable("Reserva");

            entity.Property(e => e.IdReserva).HasColumnName("ID_Reserva");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Activa");
            entity.Property(e => e.IdFechaFin).HasColumnName("ID_FechaFin");
            entity.Property(e => e.IdFechaInicio).HasColumnName("ID_FechaInicio");
            entity.Property(e => e.IdHabitacion).HasColumnName("ID_Habitacion");
            entity.Property(e => e.IdUsuario).HasColumnName("ID_Usuario");

            entity.HasOne(d => d.IdFechaFinNavigation).WithMany(p => p.ReservaIdFechaFinNavigations)
                .HasForeignKey(d => d.IdFechaFin)
                .HasConstraintName("FK__Reserva__ID_Fech__3B75D760");

            entity.HasOne(d => d.IdFechaInicioNavigation).WithMany(p => p.ReservaIdFechaInicioNavigations)
                .HasForeignKey(d => d.IdFechaInicio)
                .HasConstraintName("FK__Reserva__ID_Fech__3A81B327");

            entity.HasOne(d => d.IdHabitacionNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdHabitacion)
                .HasConstraintName("FK__Reserva__ID_Habi__398D8EEE");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Reserva__ID_Usua__38996AB5");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__202AD220DF0BE046");

            entity.ToTable("Rol");

            entity.Property(e => e.IdRol).HasColumnName("ID_Rol");
            entity.Property(e => e.NombreRol)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Sede>(entity =>
        {
            entity.HasKey(e => e.IdSede).HasName("PK__Sede__EE7C3C6762BB3990");

            entity.ToTable("Sede");

            entity.Property(e => e.IdSede).HasColumnName("ID_Sede");
            entity.Property(e => e.Ciudad)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoHabitacion>(entity =>
        {
            entity.HasKey(e => e.IdTipoHabitacion).HasName("PK__Tipo_Hab__A4417D414C0AC741");

            entity.ToTable("Tipo_Habitacion");

            entity.Property(e => e.IdTipoHabitacion).HasColumnName("ID_Tipo_Habitacion");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.NombreTipo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__DE4431C52056179C");

            entity.ToTable("Usuario");

            entity.Property(e => e.IdUsuario).HasColumnName("ID_Usuario");
            entity.Property(e => e.Contraseña)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IdRol).HasColumnName("ID_Rol");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__Usuario__ID_Rol__25869641");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
