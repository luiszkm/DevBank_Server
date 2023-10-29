

using User.DevBank.Application.Interfaces;
using User.DevBank.Application.UseCases.User.Common;
using User.DevBank.Domain.Domain.Repository;

namespace User.DevBank.Application.UseCases.User.CreateUser.cs;
public class CreateUser : ICreateUser
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUser(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserModelOutput> Handle(
        CreateUserInput request,
        CancellationToken cancellationToken)
    {
        var userExists = await _userRepository.GetByCPF(request.CPF, cancellationToken);
        if (userExists != null)
        {
            return null;
        }

        var user = new DomainEntity.User(
                       request.Name,
                       request.Email,
                       request.Password,
                       request.Phone,
                       request.CPF,
                       request.BirthDate);

        await _userRepository.Create(user, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);
        return UserModelOutput.FromUser(user);

    }
}
