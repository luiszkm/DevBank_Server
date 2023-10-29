using DevBank.User.InfraData.EF.Configurations;
using Microsoft.EntityFrameworkCore;

namespace DevBank.User.InfraData.EF;
public class DevBankUserDbContext : DbContext
{
    public DbSet<DomainEntity.User> Users => Set<DomainEntity.User>();

    public DevBankUserDbContext(DbContextOptions<DevBankUserDbContext> options)
        : base(options) { }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserConfiguration());
    }
}
