using Microsoft.EntityFrameworkCore;
using User.DevBank.Domain.Domain.Repository;

namespace DevBank.User.InfraData.EF.Repositories;
public class UserRepository : IUserRepository
{
    private readonly DevBankUserDbContext _dbContext;

    public UserRepository(DevBankUserDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    private DbSet<DomainEntity.User> _users => _dbContext.Users;
    public async Task Create(DomainEntity.User aggregate, CancellationToken cancellationToken)
    => await _users.AddAsync(aggregate, cancellationToken);

    public Task Update(DomainEntity.User aggregate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task Delete(DomainEntity.User aggregate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<DomainEntity.User> GetByCPF(string cpf, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
