
using MediatR;
using User.DevBank.Application.UseCases.User.Common;

namespace User.DevBank.Application.UseCases.User.CreateUser.cs;
public interface ICreateUser : IRequestHandler<CreateUserInput, UserModelOutput>
{
}


