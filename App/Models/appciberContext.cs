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

        public virtual DbSet<Administrator> Administrator { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Job> Job { get; set; }
        public virtual DbSet<Locations> Locations { get; set; }
        public virtual DbSet<Region> Region { get; set; }

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
            modelBuilder.Entity<Administrator>(entity =>
            {
                entity.ToTable("administrator");

                entity.HasIndex(e => e.EmployeId)
                    .HasName("UQ__administ__C0B77A57F4FECC36")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EmployeId).HasColumnName("employe_id");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(60);

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasColumnName("salt")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.HasOne(d => d.Employe)
                    .WithOne(p => p.Administrator)
                    .HasForeignKey<Administrator>(d => d.EmployeId)
                    .HasConstraintName("FK__administr__emplo__45F365D3");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("country");

                entity.Property(e => e.CountryId)
                    .HasColumnName("country_id")
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CountryName)
                    .HasColumnName("country_name")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.RegionId).HasColumnName("region_id");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Country)
                    .HasForeignKey(d => d.RegionId)
                    .HasConstraintName("FK__country__region___286302EC");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("department");

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasColumnName("department_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Department)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__departmen__locat__35BCFE0A");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employee");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__employee__AB6E61649EC7114D")
                    .IsUnique();

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasColumnName("first_name")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.HireDate)
                    .HasColumnName("hire_date")
                    .HasColumnType("date");

                entity.Property(e => e.JobId).HasColumnName("job_id");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.ManagerId).HasColumnName("manager_id");

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("phone_number")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Salary)
                    .HasColumnName("salary")
                    .HasColumnType("decimal(8, 2)");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__employee__depart__3E52440B");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK__employee__job_id__3D5E1FD2");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.InverseManager)
                    .HasForeignKey(d => d.ManagerId)
                    .HasConstraintName("FK__employee__manage__3F466844");
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("job");

                entity.Property(e => e.JobId).HasColumnName("job_id");

                entity.Property(e => e.JobTitle)
                    .IsRequired()
                    .HasColumnName("job_title")
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.MaxSalary)
                    .HasColumnName("max_salary")
                    .HasColumnType("decimal(8, 2)");

                entity.Property(e => e.MinSalary)
                    .HasColumnName("min_salary")
                    .HasColumnType("decimal(8, 2)");
            });

            modelBuilder.Entity<Locations>(entity =>
            {
                entity.HasKey(e => e.LocationId)
                    .HasName("PK__location__771831EA5CA9E9EC");

                entity.ToTable("locations");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CountryId)
                    .IsRequired()
                    .HasColumnName("country_id")
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.PostalCode)
                    .HasColumnName("postal_code")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.StateProvince)
                    .HasColumnName("state_province")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.StreetAddress)
                    .HasColumnName("street_address")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK__locations__count__2E1BDC42");
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.ToTable("region");

                entity.Property(e => e.RegionId).HasColumnName("region_id");

                entity.Property(e => e.RegionName)
                    .HasColumnName("region_name")
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
