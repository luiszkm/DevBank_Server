using DomainEntity = CreateUser.DevBank.Domain.Domain.Entity;

namespace User.DevBank.Domain.Domain.Repository;
public interface IUserRepository : IGenericRepository<DomainEntity.User>
{
    public Task<DomainEntity.User> GetByCPF(string cpf, CancellationToken cancellationToken);
}
