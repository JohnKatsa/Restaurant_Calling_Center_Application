using System;
using FinalProject.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FinalProject.Services.DataRetrieval
{
    public partial class AdventureWorks2017Context : DbContext
    {
        public AdventureWorks2017Context()
        {
        }

        public AdventureWorks2017Context(DbContextOptions<AdventureWorks2017Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<BusinessEntityAddress> BusinessEntityAddress { get; set; }
        public virtual DbSet<EmailAddress> EmailAddress { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<PersonPhone> PersonPhone { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=AdventureWorks2017;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("Address", "Person");

                entity.HasIndex(e => e.Rowguid)
                    .HasName("AK_Address_rowguid")
                    .IsUnique();

                entity.HasIndex(e => e.StateProvinceId);

                entity.HasIndex(e => new { e.AddressLine1, e.AddressLine2, e.City, e.StateProvinceId, e.PostalCode })
                    .IsUnique();

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.AddressLine1)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.AddressLine2).HasMaxLength(60);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.StateProvinceId).HasColumnName("StateProvinceID");
            });

            modelBuilder.Entity<BusinessEntityAddress>(entity =>
            {
                entity.HasKey(e => new { e.BusinessEntityId, e.AddressId, e.AddressTypeId })
                    .HasName("PK_BusinessEntityAddress_BusinessEntityID_AddressID_AddressTypeID");

                entity.ToTable("BusinessEntityAddress", "Person");

                entity.HasIndex(e => e.AddressId);

                entity.HasIndex(e => e.AddressTypeId);

                entity.HasIndex(e => e.Rowguid)
                    .HasName("AK_BusinessEntityAddress_rowguid")
                    .IsUnique();

                entity.Property(e => e.BusinessEntityId).HasColumnName("BusinessEntityID");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.AddressTypeId).HasColumnName("AddressTypeID");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.BusinessEntityAddress)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<EmailAddress>(entity =>
            {
                entity.HasKey(e => new { e.BusinessEntityId, e.EmailAddressId })
                    .HasName("PK_EmailAddress_BusinessEntityID_EmailAddressID");

                entity.ToTable("EmailAddress", "Person");

                entity.HasIndex(e => e.EmailAddress1);

                entity.Property(e => e.BusinessEntityId).HasColumnName("BusinessEntityID");

                entity.Property(e => e.EmailAddressId)
                    .HasColumnName("EmailAddressID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.EmailAddress1)
                    .HasColumnName("EmailAddress")
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.BusinessEntity)
                    .WithMany(p => p.EmailAddress)
                    .HasForeignKey(d => d.BusinessEntityId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.BusinessEntityId)
                    .HasName("PK_Employee_BusinessEntityID");

                entity.ToTable("Employee", "HumanResources");

                entity.HasIndex(e => e.LoginId)
                    .HasName("AK_Employee_LoginID")
                    .IsUnique();

                entity.HasIndex(e => e.NationalIdnumber)
                    .HasName("AK_Employee_NationalIDNumber")
                    .IsUnique();

                entity.HasIndex(e => e.Rowguid)
                    .HasName("AK_Employee_rowguid")
                    .IsUnique();

                entity.Property(e => e.BusinessEntityId)
                    .HasColumnName("BusinessEntityID")
                    .ValueGeneratedNever();

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.CurrentFlag)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(1);

                entity.Property(e => e.HireDate).HasColumnType("date");

                entity.Property(e => e.JobTitle)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LoginId)
                    .IsRequired()
                    .HasColumnName("LoginID")
                    .HasMaxLength(256);

                entity.Property(e => e.MaritalStatus)
                    .IsRequired()
                    .HasMaxLength(1);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NationalIdnumber)
                    .IsRequired()
                    .HasColumnName("NationalIDNumber")
                    .HasMaxLength(15);

                entity.Property(e => e.OrganizationLevel).HasComputedColumnSql("([OrganizationNode].[GetLevel]())");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.SalariedFlag)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.BusinessEntity)
                    .WithOne(p => p.Employee)
                    .HasForeignKey<Employee>(d => d.BusinessEntityId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.BusinessEntityId)
                    .HasName("PK_Person_BusinessEntityID");

                entity.ToTable("Person", "Person");

                entity.HasIndex(e => e.AdditionalContactInfo)
                    .HasName("PXML_Person_AddContact");

                entity.HasIndex(e => e.Demographics)
                    .HasName("XMLVALUE_Person_Demographics");

                entity.HasIndex(e => e.Rowguid)
                    .HasName("AK_Person_rowguid")
                    .IsUnique();

                entity.HasIndex(e => new { e.LastName, e.FirstName, e.MiddleName });

                entity.Property(e => e.BusinessEntityId)
                    .HasColumnName("BusinessEntityID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AdditionalContactInfo).HasColumnType("xml");

                entity.Property(e => e.Demographics).HasColumnType("xml");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MiddleName).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PersonType)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Suffix).HasMaxLength(10);

                entity.Property(e => e.Title).HasMaxLength(8);
            });

            modelBuilder.Entity<PersonPhone>(entity =>
            {
                entity.HasKey(e => new { e.BusinessEntityId, e.PhoneNumber, e.PhoneNumberTypeId })
                    .HasName("PK_PersonPhone_BusinessEntityID_PhoneNumber_PhoneNumberTypeID");

                entity.ToTable("PersonPhone", "Person");

                entity.HasIndex(e => e.PhoneNumber);

                entity.Property(e => e.BusinessEntityId).HasColumnName("BusinessEntityID");

                entity.Property(e => e.PhoneNumber).HasMaxLength(25);

                entity.Property(e => e.PhoneNumberTypeId).HasColumnName("PhoneNumberTypeID");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.BusinessEntity)
                    .WithMany(p => p.PersonPhone)
                    .HasForeignKey(d => d.BusinessEntityId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
        }
    }
}
