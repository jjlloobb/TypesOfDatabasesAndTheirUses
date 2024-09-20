using CBTW.Microservices.CallCenter.Domain.CallCenter;
using Microsoft.EntityFrameworkCore;

namespace CBTW.Microservices.CallCenter.Infrastructure.Providers;

public partial class PostgreSqlCallCenterDbContext : DbContext
{
	public PostgreSqlCallCenterDbContext(DbContextOptions<PostgreSqlCallCenterDbContext> options)
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
			entity.ToTable("callcenter", "public");

			entity.HasKey(e => e.Id)
				.HasName("pk_callcenter");

			entity.Property(e => e.Id)
				.HasColumnName("id")
				.IsRequired();

			entity.Property(e => e.Name)
				.HasColumnName("name")
				.IsRequired()
				.HasMaxLength(200)
				.IsUnicode(true);

			entity.Property(e => e.CreateDate)
				.HasColumnName("createdate")
				.HasColumnType("timestamp(3)")
				.IsRequired();

			entity.Property(e => e.UpdateDate)
				.HasColumnName("updatedate")
				.HasColumnType("timestamp(3)");
		});

		modelBuilder.Entity<PhoneCCEntity>(entity =>
		{
			entity.ToTable("phonecc", "public");

			entity.HasKey(e => e.Id)
				.HasName("pk_phonecc");

			entity.HasOne(r => r.IdCallCenterNavigation)
				.WithMany(e => e.Phones)
				.HasForeignKey(r => r.IdCallCenter)
				.HasConstraintName("fk_phonecc_callcenter");

			entity.Property(e => e.Id)
				.HasColumnName("id")
				.IsRequired();

			entity.Property(e => e.IdCallCenter)
				.HasColumnName("idcallcenter")
				.IsRequired();

			entity.Property(e => e.CountryCode)
				.HasColumnName("countrycode")
				.IsRequired()
				.HasMaxLength(5)
				.IsUnicode(true);

			entity.Property(e => e.PhoneNumber)
				.HasColumnName("phonenumber")
				.IsRequired()
				.HasMaxLength(15)
				.IsUnicode(true);

			entity.Property(e => e.CreateDate)
				.HasColumnName("createdate")
				.HasColumnType("timestamp(3)")
				.IsRequired();

			entity.Property(e => e.UpdateDate)
				.HasColumnName("updatedate")
				.HasColumnType("timestamp(3)");
		});

		modelBuilder.Entity<CountryEntity>(entity =>
		{
			entity.ToTable("country", "public");

			entity.HasKey(e => e.Id)
				.HasName("pk_country");

			entity.Property(e => e.Id)
				.HasColumnName("id")
				.IsRequired();

			entity.Property(e => e.Name)
				.HasColumnName("name")
				.IsRequired()
				.HasMaxLength(200)
				.IsUnicode(true);

			entity.Property(e => e.CreateDate)
				.HasColumnName("createdate")
				.HasColumnType("timestamp(3)")
				.IsRequired();

			entity.Property(e => e.UpdateDate)
				.HasColumnName("updatedate")
				.HasColumnType("timestamp(3)");
		});

		modelBuilder.Entity<CityEntity>(entity =>
		{
			entity.ToTable("city", "public");

			entity.HasKey(e => e.Id)
				.HasName("pk_city");

			entity.HasOne(r => r.IdCountryNavigation)
				.WithMany(e => e.Cities)
				.HasForeignKey(r => r.IdCountry)
				.HasConstraintName("fk_city_country");

			entity.Property(e => e.Id)
				.HasColumnName("id")
				.IsRequired();

			entity.Property(e => e.IdCountry)
				.HasColumnName("idcountry")
				.IsRequired();

			entity.Property(e => e.Name)
				.HasColumnName("name")
				.IsRequired()
				.HasMaxLength(200)
				.IsUnicode(true);

			entity.Property(e => e.CreateDate)
				.HasColumnName("createdate")
				.HasColumnType("timestamp(3)")
				.IsRequired();

			entity.Property(e => e.UpdateDate)
				.HasColumnName("updatedate")
				.HasColumnType("timestamp(3)");
		});

		modelBuilder.Entity<DocumentTypeEntity>(entity =>
		{
			entity.ToTable("documenttype", "public");

			entity.HasKey(e => e.Id)
				.HasName("pk_documenttype");

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
				.HasColumnName("createdate")
				.HasColumnType("timestamp(3)")
				.IsRequired();

			entity.Property(e => e.UpdateDate)
				.HasColumnName("updatedate")
				.HasColumnType("timestamp(3)");
		});

		modelBuilder.Entity<CustomerEntity>(entity =>
		{
			entity.ToTable("customer", "public");

			entity.HasKey(e => e.Id)
				.HasName("pk_customer");

			entity.HasOne(r => r.IdDocumentTypeNavigation)
				.WithMany(e => e.Customers)
				.HasForeignKey(r => r.IdDocumentType)
				.HasConstraintName("fk_customer_documenttype");

			entity.HasOne(r => r.IdCityNavigation)
				.WithMany(e => e.Customers)
				.HasForeignKey(r => r.IdCity)
				.HasConstraintName("fk_customer_city");

			entity.HasOne(r => r.IdPhoneCCNavigation)
				.WithMany(e => e.Customers)
				.HasForeignKey(r => r.IdPhoneCC)
				.HasConstraintName("fk_customer_phonecc");

			entity.Property(e => e.Id)
				.HasColumnName("id")
				.IsRequired();

			entity.Property(e => e.IdDocumentType)
				.HasColumnName("iddocumenttype")
				.IsRequired();

			entity.Property(e => e.IdCity)
				.HasColumnName("idcity")
				.IsRequired();

			entity.Property(e => e.IdPhoneCC)
				.HasColumnName("idphonecc")
				.IsRequired();


			entity.Property(e => e.Document)
				.HasColumnName("document")
				.IsRequired()
				.HasMaxLength(20)
				.IsUnicode(true);

			entity.Property(e => e.FullName)
				.HasColumnName("fullname")
				.IsRequired()
				.HasMaxLength(200)
				.IsUnicode(true);

			entity.Property(e => e.CountryCode)
				.HasColumnName("countrycode")
				.IsRequired()
				.HasMaxLength(5)
				.IsUnicode(true);

			entity.Property(e => e.PhoneNumber)
				.HasColumnName("phonenumber")
				.IsRequired()
				.HasMaxLength(15)
				.IsUnicode(true);

			entity.Property(e => e.DateOfBirth)
				.HasColumnName("dateofbirth")
				.HasColumnType("date")
				.IsRequired();

			entity.Property(e => e.CreateDate)
				.HasColumnName("createdate")
				.HasColumnType("timestamp(3)")
				.IsRequired();

			entity.Property(e => e.UpdateDate)
				.HasColumnName("updatedate")
				.HasColumnType("timestamp(3)");
		});

		modelBuilder.Entity<PQREntity>(entity =>
		{
			entity.ToTable("pqr", "public");

			entity.HasKey(e => e.Id)
				.HasName("pk_pqr");

			entity.HasOne(r => r.IdCustomerNavigation)
				.WithMany(e => e.PQRs)
				.HasForeignKey(r => r.IdCustomer)
				.HasConstraintName("fk_pqr_customer");

			entity.Property(e => e.Id)
				.HasColumnName("id")
				.IsRequired();

			entity.Property(e => e.IdCustomer)
				.HasColumnName("idcustomer")
				.IsRequired();

			entity.Property(e => e.Subject)
				.HasColumnName("subject")
				.IsRequired()
				.HasMaxLength(200)
				.IsUnicode(true);

			entity.Property(e => e.Description)
				.HasColumnName("description")
				.HasColumnType("TEXT")
				.IsRequired()
				.IsUnicode(true);

			entity.Property(e => e.CreateDate)
				.HasColumnName("createdate")
				.HasColumnType("timestamp(3)")
				.IsRequired();

			entity.Property(e => e.UpdateDate)
				.HasColumnName("updatedate")
				.HasColumnType("timestamp(3)");
		});

		OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
