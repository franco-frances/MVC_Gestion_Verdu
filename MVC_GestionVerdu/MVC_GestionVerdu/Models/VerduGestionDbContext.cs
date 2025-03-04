using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MVC_GestionVerdu.Models;

public partial class VerduGestionDbContext : DbContext
{
    public VerduGestionDbContext()
    {
    }

    public VerduGestionDbContext(DbContextOptions<VerduGestionDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<DetallesVenta> DetallesVentas { get; set; }

    public virtual DbSet<Gastos> Gastos { get; set; }

    public virtual DbSet<MetodoPago> MetodoPagos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { 
    
    
    
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__CB90334991DD9F7C");

            entity.Property(e => e.IdCategoria).HasColumnName("Id_Categoria");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DetallesVenta>(entity =>
        {
            entity.HasKey(e => e.IdDetalleVenta).HasName("PK__Detalles__ABDF352651BCC546");

            entity.Property(e => e.IdDetalleVenta).HasColumnName("Id_DetalleVenta");
            entity.Property(e => e.Concepto)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.MetodoPagoId).HasColumnName("MetodoPago_Id");
            entity.Property(e => e.Monto).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UsuarioId).HasColumnName("Usuario_Id");

            entity.HasOne(d => d.MetodoPago).WithMany(p => p.DetallesVenta)
                .HasForeignKey(d => d.MetodoPagoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetallesVentas_MetodosPago");

            entity.HasOne(d => d.Usuario).WithMany(p => p.DetallesVenta)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Producto_DetallesVentas");
        });

        modelBuilder.Entity<Gastos>(entity =>
        {
            entity.HasKey(e => e.IdGasto).HasName("PK__Gastos__A25A4A913809F841");

            entity.Property(e => e.IdGasto).HasColumnName("Id_Gasto");
            entity.Property(e => e.Concepto)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.Monto).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.UsuarioId).HasColumnName("Usuario_Id");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Gastos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Producto_Gastos");
        });

        modelBuilder.Entity<MetodoPago>(entity =>
        {
            entity.HasKey(e => e.IdMetodoPago).HasName("PK__MetodoPa__657893474FE45848");

            entity.Property(e => e.IdMetodoPago).HasColumnName("Id_MetodoPago");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProductos).HasName("PK__Producto__66A0BD699719D29C");

            entity.Property(e => e.IdProductos).HasColumnName("Id_Productos");
            entity.Property(e => e.CategoriaId).HasColumnName("Categoria_id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.MargenGanancia).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.PesoCajon).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PrecioCajon).HasColumnType("decimal(10, 2)");

            entity.Property(e => e.PrecioCosto).HasColumnType("decimal(10,2)");


            entity.Property(e => e.PrecioFinal).HasColumnType("numeric(36, 20)");

            entity.Property(e => e.UsuarioId).HasColumnName("Usuario_Id");

            entity.HasOne(d => d.Categoria).WithMany(p => p.Productos)
                .HasForeignKey(d => d.CategoriaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productos_Categorias");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Productos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Producto_Usuario");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_USERS");

            entity.Property(e => e.Email)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.NickName)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
