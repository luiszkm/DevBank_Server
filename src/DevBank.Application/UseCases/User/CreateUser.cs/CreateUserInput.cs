

using MediatR;
using User.DevBank.Application.UseCases.User.Common;

namespace User.DevBank.Application.UseCases.User.CreateUser.cs;
public class CreateUserInput : IRequest<UserModelOutput>
{
    public CreateUserInput(
        string name,
        string email,
        string password,
        string phoneNumber,
        string cpf,
        DateTime birthDate
    )
    {
        Name = name;
        Email = email;
        Password = password;
        Phone = phoneNumber;
        CPF = cpf;
        BirthDate = birthDate;
    }

    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public string CPF { get; set; }
    public DateTime BirthDate { get; set; }
}
