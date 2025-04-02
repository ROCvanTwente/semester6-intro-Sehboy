using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<CreditCard> CreditCards { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer("Server=.;Database=ASPSEC-02;Trusted_Connection=True;TrustServerCertificate=True;");
    }
}
