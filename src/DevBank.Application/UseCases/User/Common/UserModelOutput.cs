

namespace User.DevBank.Application.UseCases.User.Common;
public class UserModelOutput
{
    public UserModelOutput(
        Guid id,
        string name,
        string email,
        string phone
        )
    {
        Id = id;
        Name = name;
        Email = email;
        Phone = phone;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public static UserModelOutput FromUser(DomainEntity.User user)
        => new UserModelOutput(
            user.Id,
            user.Name,
            user.Email,
            user.PhoneNumber);

}
