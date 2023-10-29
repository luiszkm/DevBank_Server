using Bogus.Extensions.Brazil;
using CreateUser.DevBank.UnitTest.Domain.Common;

namespace CreateUser.DevBank.UnitTest.Domain.Entity.User;

[CollectionDefinition(nameof(UserTestFixture))]
public class UserTestFixtureCollection : ICollectionFixture<UserTestFixture>
{
}

public class UserTestFixture : UserFixture
{
    public DateTime GetInvalidBirthDate()
        => DateTime.Now.AddYears(-11);

    public DateTime GetValidBirthDateNow()
        => DateTime.Now.AddYears(-12);

    public DateTime GetInvalidBirthDateNow()
        => DateTime.Now.AddYears(-12).AddDays(+1);

    public string GetNewName()
        => Faker.Name.FullName();

    public string GetNewEmail()
        => Faker.Internet.Email();

    public string GetNewPhoneNumber()
        => Faker.Phone.PhoneNumber();

    public string GetNewPassword()
    => Faker.Internet.Password();

    public string GetNewValidCPF()
        => Faker.Person.Cpf(true);

    public DomainEntity.User GetValidUserWithoutCpf(string cpf)
        => new(
            GetValidName(),
            GetValidEmail(),
            GetValidPassword(),
            GetValidPhoneNumber(),
            cpf,
            GetValidBirthDate());
}