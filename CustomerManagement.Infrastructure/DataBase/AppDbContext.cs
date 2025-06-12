using CustomerManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Infrastructure.DataBase;
public sealed class AppDbContext : DbContext
{
    #region Properties And Constructor

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Company> Companies { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<CompanyContact> CompanyContacts { get; set; }
    public DbSet<CustomAttribute> CustomAttributes { get; set; }
    #endregion

    #region Methods

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>()
            .HasIndex(c => c.Name)
            .HasMethod("GIN")
            .HasAnnotation("Npgsql:TsVectorConfig", "english")
            .HasAnnotation("Npgsql:TsVectorProperties", new[] { "Name" });

        modelBuilder.Entity<Contact>()
            .HasIndex(c => c.Name)
            .HasMethod("GIN")
            .HasAnnotation("Npgsql:TsVectorConfig", "english")
            .HasAnnotation("Npgsql:TsVectorProperties", new[] { "Name" });

        modelBuilder.Entity<CustomAttribute>()
            .HasIndex(c => new { c.Name, c.Value })
            .HasMethod("GIN")
            .HasAnnotation("Npgsql:TsVectorConfig", "english")
            .HasAnnotation("Npgsql:TsVectorProperties", new[] { "Name", "Value" });

        modelBuilder.Entity<CustomAttribute>()
            .HasOne(a => a.Company)
            .WithMany(c => c.CustomAttributes)
            .HasForeignKey(a => a.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CustomAttribute>()
            .HasOne(a => a.Contact)
            .WithMany(c => c.CustomAttributes)
            .HasForeignKey(a => a.ContactId)
            .OnDelete(DeleteBehavior.Cascade);
    }
    #endregion
}
