using Bogus.Extensions.Brazil;

namespace CreateUser.DevBank.UnitTest.Domain.Common;
public class UserFixture : BaseFixture
{
    public string GetValidPassword()
        => Faker.Internet.Password(8, false, "", "@1Ab_");

    public string GetValidPhoneNumber()
        => Faker.Person.Phone;

    public string GetValidCPF()
        => Faker.Person.Cpf(true);

    public DateTime GetValidBirthDate()
        => Faker.Person.DateOfBirth.AddYears(-12);

    public DomainEntity.User GetValidUser()
        => new(
            GetValidName(),
            GetValidEmail(),
            GetValidPassword(),
            GetValidPhoneNumber(),
            GetValidCPF(),
            GetValidBirthDate()
        );
    public DomainEntity.User GetValidUser(string password)
        => new(
            GetValidName(),
            GetValidEmail(),
            password,
            GetValidPhoneNumber(),
            GetValidCPF(),
            GetValidBirthDate()
        );
}
