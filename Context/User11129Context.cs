using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using demo21111.Models;

namespace demo21111.Context;

public partial class User11129Context : DbContext
{
    public User11129Context()
    {
    }

    public User11129Context(DbContextOptions<User11129Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Prod> Prods { get; set; }

    public virtual DbSet<Punkt> Punkts { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Zakaz> Zakazs { get; set; }

    public virtual DbSet<ZakazPr> ZakazPrs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=192.168.7.159;Port=5432;Database=user11129;Username=user11129;Password=user11129");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdClient).HasName("client_pk");

            entity.ToTable("client");

            entity.Property(e => e.IdClient)
                .ValueGeneratedNever()
                .HasColumnName("id_client");
            entity.Property(e => e.IdRole).HasColumnName("id_role");
            entity.Property(e => e.Lastname)
                .HasColumnType("character varying")
                .HasColumnName("lastname");
            entity.Property(e => e.Login)
                .HasColumnType("character varying")
                .HasColumnName("login");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Passwd)
                .HasColumnType("character varying")
                .HasColumnName("passwd");
            entity.Property(e => e.Surname)
                .HasColumnType("character varying")
                .HasColumnName("surname");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Clients)
                .HasForeignKey(d => d.IdRole)
                .HasConstraintName("client_role_fk");
        });

        modelBuilder.Entity<Prod>(entity =>
        {
            entity.HasKey(e => e.IdProd).HasName("prod_pk");

            entity.ToTable("prod");

            entity.Property(e => e.IdProd)
                .ValueGeneratedNever()
                .HasColumnName("id_prod");
            entity.Property(e => e.Articul)
                .HasColumnType("character varying")
                .HasColumnName("articul");
            entity.Property(e => e.Categ)
                .HasColumnType("character varying")
                .HasColumnName("categ");
            entity.Property(e => e.Cost).HasColumnName("cost");
            entity.Property(e => e.Currdisc).HasColumnName("currdisc");
            entity.Property(e => e.Descr)
                .HasColumnType("character varying")
                .HasColumnName("descr");
            entity.Property(e => e.Edizm)
                .HasColumnType("character varying")
                .HasColumnName("edizm");
            entity.Property(e => e.Image)
                .HasColumnType("character varying")
                .HasColumnName("image");
            entity.Property(e => e.Manuf)
                .HasColumnType("character varying")
                .HasColumnName("manuf");
            entity.Property(e => e.Maxdisc).HasColumnName("maxdisc");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Quanskl).HasColumnName("quanskl");
            entity.Property(e => e.Suppl)
                .HasColumnType("character varying")
                .HasColumnName("suppl");
        });

        modelBuilder.Entity<Punkt>(entity =>
        {
            entity.HasKey(e => e.IdPunkt).HasName("punkt_pk");

            entity.ToTable("punkt");

            entity.Property(e => e.IdPunkt)
                .ValueGeneratedNever()
                .HasColumnName("id_punkt");
            entity.Property(e => e.Punkt1)
                .HasColumnType("character varying")
                .HasColumnName("punkt");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("role_pk");

            entity.ToTable("role");

            entity.Property(e => e.IdRole)
                .ValueGeneratedNever()
                .HasColumnName("id_role");
            entity.Property(e => e.Role1)
                .HasColumnType("character varying")
                .HasColumnName("role");
        });

        modelBuilder.Entity<Zakaz>(entity =>
        {
            entity.HasKey(e => e.IdZakaz).HasName("zakaz_pk");

            entity.ToTable("zakaz");

            entity.Property(e => e.IdZakaz)
                .ValueGeneratedNever()
                .HasColumnName("id_zakaz");
            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.Datecr).HasColumnName("datecr");
            entity.Property(e => e.Datedel).HasColumnName("datedel");
            entity.Property(e => e.IdPunkt).HasColumnName("id_punkt");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Status)
                .HasColumnType("character varying")
                .HasColumnName("status");

            entity.HasOne(d => d.IdPunktNavigation).WithMany(p => p.Zakazs)
                .HasForeignKey(d => d.IdPunkt)
                .HasConstraintName("zakaz_punkt_fk");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Zakazs)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("zakaz_client_fk");
        });

        modelBuilder.Entity<ZakazPr>(entity =>
        {
            entity.HasKey(e => new { e.IdZakaz, e.IdProd }).HasName("zakaz_pr_pk");

            entity.ToTable("zakaz_pr");

            entity.Property(e => e.IdZakaz).HasColumnName("id_zakaz");
            entity.Property(e => e.IdProd).HasColumnName("id_prod");
            entity.Property(e => e.Amount).HasColumnName("amount");

            entity.HasOne(d => d.IdProdNavigation).WithMany(p => p.ZakazPrs)
                .HasForeignKey(d => d.IdProd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("zakaz_pr_prod_fk");

            entity.HasOne(d => d.IdZakazNavigation).WithMany(p => p.ZakazPrs)
                .HasForeignKey(d => d.IdZakaz)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("zakaz_pr_zakaz_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
