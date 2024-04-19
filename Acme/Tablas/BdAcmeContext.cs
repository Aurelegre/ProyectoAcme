using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Acme.Tablas;

public partial class BdAcmeContext : DbContext
{
    public BdAcmeContext()
    {
    }

    public BdAcmeContext(DbContextOptions<BdAcmeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Campo> Campos { get; set; }

    public virtual DbSet<Encuestum> Encuesta { get; set; }

    public virtual DbSet<Link> Links { get; set; }

    public virtual DbSet<Respuestum> Respuesta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=conexion");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Campo>(entity =>
        {
            entity.HasKey(e => e.Idcampo).HasName("PK__campo__02D54CF5E41CCB59");

            entity.ToTable("campo");

            entity.Property(e => e.Idcampo).HasColumnName("idcampo");
            entity.Property(e => e.IdEncuesta).HasColumnName("idEncuesta");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Requerido)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("requerido");
            entity.Property(e => e.Tipo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Titulo)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEncuestaNavigation).WithMany(p => p.Campos)
                .HasForeignKey(d => d.IdEncuesta)
                .HasConstraintName("fk_encuesta")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Encuestum>(entity =>
        {
            entity.HasKey(e => e.IdEncuesta).HasName("PK__Encuesta__C03F98577E5253C0");

            entity.Property(e => e.IdEncuesta).HasColumnName("idEncuesta");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(80)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Link>(entity =>
        {
            entity.HasKey(e => e.Idlink).HasName("PK__link__EAF1A68570C0D3EC");

            entity.ToTable("link");

            entity.Property(e => e.Idlink).HasColumnName("idlink");
            entity.Property(e => e.Idencuesta).HasColumnName("idencuesta");
            entity.Property(e => e.Link1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("link");

            entity.HasOne(d => d.IdencuestaNavigation).WithMany(p => p.Links)
                .HasForeignKey(d => d.Idencuesta)
                .HasConstraintName("fk_encuestaLink")
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Respuestum>(entity =>
        {
            entity.HasKey(e => e.IdRespuesta).HasName("PK__respuest__8AB5BFC8910A85B6");

            entity.ToTable("respuesta");

            entity.Property(e => e.IdRespuesta).HasColumnName("idRespuesta");
            entity.Property(e => e.Contenido)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("contenido");
            entity.Property(e => e.Idcampo).HasColumnName("idcampo");

            entity.HasOne(d => d.IdcampoNavigation).WithMany(p => p.Respuesta)
                .HasForeignKey(d => d.Idcampo)
                .HasConstraintName("fk_campo")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Usuario>().HasKey(g => g.id);
        modelBuilder.Entity<Usuario>().Property(e  => e.Nombre).HasMaxLength(40);
        modelBuilder.Entity<Usuario>().Property(e => e.Contraseña).HasMaxLength(20);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
