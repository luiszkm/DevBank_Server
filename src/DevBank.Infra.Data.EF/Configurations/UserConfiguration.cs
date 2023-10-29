using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevBank.User.InfraData.EF.Configurations;
internal class UserConfiguration : IEntityTypeConfiguration<DomainEntity.User>
{
    public void Configure(EntityTypeBuilder<DomainEntity.User> builder)
    {
        builder.HasKey(user => user.Id);

    }
}
