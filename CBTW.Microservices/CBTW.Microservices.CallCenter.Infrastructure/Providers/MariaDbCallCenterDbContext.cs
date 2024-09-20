using CBTW.Microservices.CallCenter.Domain.CallCenter;
using Microsoft.EntityFrameworkCore;

namespace CBTW.Microservices.CallCenter.Infrastructure.Providers;

public partial class MariaDbCallCenterDbContext : DbContext
{
    public MariaDbCallCenterDbContext(DbContextOptions<MariaDbCallCenterDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CallCenterEntity> CallCenters { get; set; }

    public virtual DbSet<PhoneCCEntity> Phones { get; set; }

    public virtual DbSet<CountryEntity> Countries { get; set; }

    public virtual DbSet<CityEntity> Cities { get; set; }

    public virtual DbSet<DocumentTypeEntity> DocumentTypes { get; set; }

    public virtual DbSet<CustomerEntity> Customers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            throw new InvalidOperationException("DbContext not configured");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CallCenterEntity>(entity =>
        {
            entity.ToTable("CALLCENTER");

            entity.HasKey(e => e.Id)
                .HasName("PK_CALLCENTER");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .IsRequired();

            entity.Property(e => e.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(true);

            entity.Property(e => e.CreateDate)
                .HasColumnName("createDate")
                .HasColumnType("datetime")
                .IsRequired();

            entity.Property(e => e.UpdateDate)
                .HasColumnName("updateDate")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<PhoneCCEntity>(entity =>
        {
            entity.ToTable("PHONECC");

            entity.HasKey(e => e.Id)
                .HasName("PK_PHONECC");

            entity.HasOne(r => r.IdCallCenterNavigation)
                .WithMany(e => e.Phones)
                .HasForeignKey(r => r.IdCallCenter)
                .HasConstraintName("FK_PHONECC_CALLCENTER");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .IsRequired();

            entity.Property(e => e.CountryCode)
                .HasColumnName("countryCode")
                .IsRequired()
                .HasMaxLength(5)
                .IsUnicode(true);

            entity.Property(e => e.PhoneNumber)
                .HasColumnName("phoneNumber")
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(true);

            entity.Property(e => e.CreateDate)
                .HasColumnName("createDate")
                .HasColumnType("datetime")
                .IsRequired();

            entity.Property(e => e.UpdateDate)
                .HasColumnName("updateDate")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<CountryEntity>(entity =>
        {
            entity.ToTable("COUNTRY");

            entity.HasKey(e => e.Id)
                .HasName("PK_COUNTRY");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .IsRequired();

            entity.Property(e => e.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(true);

            entity.Property(e => e.CreateDate)
                .HasColumnName("createDate")
                .HasColumnType("datetime")
                .IsRequired();

            entity.Property(e => e.UpdateDate)
                .HasColumnName("updateDate")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<CityEntity>(entity =>
        {
            entity.ToTable("CITY");

            entity.HasKey(e => e.Id)
                .HasName("PK_CITY");

            entity.HasOne(r => r.IdCountryNavigation)
                .WithMany(e => e.Cities)
                .HasForeignKey(r => r.IdCountry)
                .HasConstraintName("FK_CITY_COUNTRY");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .IsRequired();

            entity.Property(e => e.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(true);

            entity.Property(e => e.CreateDate)
                .HasColumnName("createDate")
                .HasColumnType("datetime")
                .IsRequired();

            entity.Property(e => e.UpdateDate)
                .HasColumnName("updateDate")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<DocumentTypeEntity>(entity =>
        {
            entity.ToTable("DOCUMENTTYPE");

            entity.HasKey(e => e.Id)
                .HasName("PK_DOCUMENTTYPE");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .IsRequired();

            entity.Property(e => e.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(5)
                .IsUnicode(true);

            entity.Property(e => e.Description)
                .HasColumnName("description")
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(true);

            entity.Property(e => e.CreateDate)
                .HasColumnName("createDate")
                .HasColumnType("datetime")
                .IsRequired();

            entity.Property(e => e.UpdateDate)
                .HasColumnName("updateDate")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<CustomerEntity>(entity =>
        {
            entity.ToTable("CUSTOMER");

            entity.HasKey(e => e.Id)
                .HasName("PK_CUSTOMER");

            entity.HasOne(r => r.IdDocumentTypeNavigation)
                .WithMany(e => e.Customers)
                .HasForeignKey(r => r.IdDocumentType)
                .HasConstraintName("FK_CUSTOMER_DOCUMENTTYPE");

            entity.HasOne(r => r.IdCityNavigation)
                .WithMany(e => e.Customers)
                .HasForeignKey(r => r.IdCity)
                .HasConstraintName("FK_CUSTOMER_CITY");

            entity.HasOne(r => r.IdPhoneCCNavigation)
                .WithMany(e => e.Customers)
                .HasForeignKey(r => r.IdPhoneCC)
                .HasConstraintName("FK_CUSTOMER_PHONECC");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .IsRequired();

            entity.Property(e => e.Document)
                .HasColumnName("document")
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(true);

            entity.Property(e => e.FullName)
                .HasColumnName("fullName")
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(true);

            entity.Property(e => e.CountryCode)
                .HasColumnName("countryCode")
                .IsRequired()
                .HasMaxLength(5)
                .IsUnicode(true);

            entity.Property(e => e.PhoneNumber)
                .HasColumnName("phoneNumber")
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(true);

            entity.Property(e => e.DateOfBirth)
                .HasColumnName("dateOfBirth")
                .HasColumnType("date")
                .IsRequired();

            entity.Property(e => e.CreateDate)
                .HasColumnName("createDate")
                .HasColumnType("datetime")
                .IsRequired();

            entity.Property(e => e.UpdateDate)
                .HasColumnName("updateDate")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<PQREntity>(entity =>
        {
            entity.ToTable("PQR");

            entity.HasKey(e => e.Id)
                .HasName("PK_PQR");

            entity.HasOne(r => r.IdCustomerNavigation)
                .WithMany(e => e.PQRs)
                .HasForeignKey(r => r.IdCustomer)
                .HasConstraintName("FK_PQR_CUSTOMER");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .IsRequired();

            entity.Property(e => e.Subject)
                .HasColumnName("subject")
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(true);

            entity.Property(e => e.Description)
                .HasColumnName("description")
                .HasColumnType("longtext")
                .IsRequired()
                .IsUnicode(true);

            entity.Property(e => e.CreateDate)
                .HasColumnName("createDate")
                .HasColumnType("datetime")
                .IsRequired();

            entity.Property(e => e.UpdateDate)
                .HasColumnName("updateDate")
                .HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
