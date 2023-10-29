

namespace User.DevBank.Domain.Domain.Repository;
public interface IGenericRepository<TAggregate>
{
    public Task Create(TAggregate aggregate, CancellationToken cancellationToken);

    public Task Update(TAggregate aggregate, CancellationToken cancellationToken);

    public Task Delete(TAggregate aggregate, CancellationToken cancellationToken);
}
