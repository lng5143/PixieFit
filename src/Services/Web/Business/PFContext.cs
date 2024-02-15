using Microsoft.EntityFrameworkCore;
using PixieFit.Web.Business.Entities;

namespace PixieFit.Web.Business;

public class PFContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<CreditPackage> CreditPackages { get; set; }
    public DbSet<Resize> Resizes { get; set; }
    public DbSet<UserTransaction> UserTransactions { get; set; }
}