
namespace User.DevBank.Domain.Domain.SeedWork;
public class BaseEntity
{
    public Guid Id { get; private set; }

    protected BaseEntity() => Id = Guid.NewGuid();
}
