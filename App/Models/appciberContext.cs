using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace App.Models
{
    public partial class appciberContext : DbContext
    {
        public appciberContext()
        {
        }

        public appciberContext(DbContextOptions<appciberContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Angajati> Angajati { get; set; }
        public virtual DbSet<Departament> Departament { get; set; }
        public virtual DbSet<Functie> Functie { get; set; }
        public virtual DbSet<Salariu> Salariu { get; set; }
        public virtual DbSet<Useri> Useri { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.; Database=appciber;User Id = admin; Password = password; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Angajati>(entity =>
            {
                entity.ToTable("angajati");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DataAngajari).HasColumnName("data_angajari");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(255);

                entity.Property(e => e.IdDepartament).HasColumnName("id_departament");

                entity.Property(e => e.IdFunctia).HasColumnName("id_functia");

                entity.Property(e => e.NumarTelefon).HasColumnName("numar_telefon");

                entity.Property(e => e.Nume)
                    .IsRequired()
                    .HasColumnName("nume")
                    .HasMaxLength(255);

                entity.Property(e => e.Prenume)
                    .IsRequired()
                    .HasColumnName("prenume")
                    .HasMaxLength(255);

                entity.HasOne(d => d.IdDepartamentNavigation)
                    .WithMany(p => p.Angajati)
                    .HasForeignKey(d => d.IdDepartament)
                    .HasConstraintName("FK__angajati__id_dep__2C3393D0");

                entity.HasOne(d => d.IdFunctiaNavigation)
                    .WithMany(p => p.Angajati)
                    .HasForeignKey(d => d.IdFunctia)
                    .HasConstraintName("FK__angajati__id_fun__2D27B809");
            });

            modelBuilder.Entity<Departament>(entity =>
            {
                entity.ToTable("departament");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.NumeDepartament)
                    .IsRequired()
                    .HasColumnName("nume_departament")
                    .HasMaxLength(255);

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Departament)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__departame__user___2E1BDC42");
            });

            modelBuilder.Entity<Functie>(entity =>
            {
                entity.ToTable("functie");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdDepartament).HasColumnName("id_departament");

                entity.HasOne(d => d.IdDepartamentNavigation)
                    .WithMany(p => p.Functie)
                    .HasForeignKey(d => d.IdDepartament)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__functie__id_depa__300424B4");
            });

            modelBuilder.Entity<Salariu>(entity =>
            {
                entity.ToTable("salariu");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdAngajat).HasColumnName("id_angajat");

                entity.Property(e => e.Salariu1).HasColumnName("salariu");

                entity.HasOne(d => d.IdAngajatNavigation)
                    .WithMany(p => p.Salariu)
                    .HasForeignKey(d => d.IdAngajat)
                    .HasConstraintName("FK__salariu__id_anga__2F10007B");
            });

            modelBuilder.Entity<Useri>(entity =>
            {
                entity.ToTable("useri");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Parola)
                    .IsRequired()
                    .HasColumnName("parola")
                    .HasMaxLength(255);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
