using Microsoft.EntityFrameworkCore;
using PixieFit.Web.Business.Entities;
using Npgsql;

namespace PixieFit.Web.Business;

public class PFContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<CreditPackage> CreditPackages { get; set; }
    public DbSet<Resize> Resizes { get; set; }
    public DbSet<UserTransaction> UserTransactions { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Webhook> Webhooks { get; set; }

    public const string ConnectionString = 
        "Host=localhost; Port=5433; Database=PixieFit; Username=postgres; Password=5143322";

    public PFContext(DbContextOptions<PFContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseNpgsql(ConnectionString);
    }
}