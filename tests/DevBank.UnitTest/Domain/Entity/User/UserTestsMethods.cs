

using CreateUser.DevBank.Domain.Domain.Exceptions;
using User.DevBank.Domain.Domain.Exceptions;

namespace CreateUser.DevBank.UnitTest.Domain.Entity.User;

[Collection(nameof(UserTestFixture))]
public class UserTestsMethods
{
    private readonly UserTestFixture _fixture;

    public UserTestsMethods(UserTestFixture fixture)
    => _fixture = fixture;

    [Fact(DisplayName = nameof(Update))]
    [Trait("User", "Domain Instantiate")]

    public void Update()
    {
        var validUser = _fixture.GetValidUser();

        var newEmail = _fixture.GetNewEmail();
        var newPhone = _fixture.GetNewPhoneNumber();

        validUser.Update(newEmail, newPhone);
        validUser.Email.Should().Be(newEmail);
        validUser.PhoneNumber.Should().Be(newPhone);
        validUser.UpdatedAt.Should().BeAfter(validUser.CreatedAt);

    }

    [Fact(DisplayName = nameof(UpdateOnlyEmail))]
    [Trait("User", "Domain Instantiate")]

    public void UpdateOnlyEmail()
    {
        var validUser = _fixture.GetValidUser();
        var newEmail = _fixture.GetNewEmail();


        validUser.Update(newEmail);
        validUser.Email.Should().Be(newEmail);
        validUser.PhoneNumber.Should().Be(validUser.PhoneNumber);
        validUser.UpdatedAt.Should().BeAfter(validUser.CreatedAt);
    }

    [Fact(DisplayName = nameof(UpdateOnlyPhoneNumber))]
    [Trait("User", "Domain Instantiate")]

    public void UpdateOnlyPhoneNumber()
    {
        var validUser = _fixture.GetValidUser();
        var validPassword = _fixture.GetValidPassword();
        var ValidCPF = _fixture.GetValidCPF();

        var newPhone = _fixture.GetNewPhoneNumber();

        var user = new DomainEntity.User(
            validUser.Name,
            validUser.Email,
            validPassword,
            validUser.PhoneNumber,
            ValidCPF,
            validUser.BirthDate);
        var verifyUpdate = user.UpdatedAt > DateTime.Now
                           && user.UpdatedAt > user.CreatedAt;
        user.Update(phoneNumber: newPhone);
        user.Email.Should().Be(validUser.Email);
        user.PhoneNumber.Should().Be(newPhone);
        verifyUpdate.Should().BeTrue();
    }
    [Theory(DisplayName = nameof(ThrowWhenEmailIsInvalid))]
    [Trait("User", "Domain Instantiate")]
    [InlineData("test.com")]
    [InlineData("test@")]
    [InlineData("test")]
    [InlineData("test@.com")]

    public void ThrowWhenEmailIsInvalid(string invalidEmail)
    {
        var validUser = _fixture.GetValidUser();
        Action action = () => validUser.Update(email: invalidEmail);

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Email is invalid");
    }

    [Fact(DisplayName = nameof(UpdateAdmin))]
    [Trait("User", "Domain Instantiate")]

    public void UpdateAdmin()
    {
        var validUser = _fixture.GetValidUser();
        var oldCpf = validUser.GetCPF();
        var newName = _fixture.GetNewName();
        var newCpf = "710.281.360-05";
        var newBirthDate = _fixture.GetValidBirthDate();
        var verifyUpdate = validUser.UpdatedAt > DateTime.Now
                           && validUser.UpdatedAt > validUser.CreatedAt;

        validUser.UpdateAdmin(newName, newCpf, newBirthDate);
        validUser.Name.Should().Be(newName);
        validUser.GetCPF().Should().NotBe(oldCpf);
        validUser.BirthDate.Should().Be(newBirthDate);
        validUser.UpdatedAt.Should().BeAfter(validUser.CreatedAt);
        verifyUpdate.Should().BeTrue();
    }

    [Fact(DisplayName = nameof(UpdateAdminOnlyCPF))]
    [Trait("User", "Domain Instantiate")]

    public void UpdateAdminOnlyCPF()
    {
        var validUser = _fixture.GetValidUser();
        var oldCpf = validUser.GetCPF();
        var newCpf = "710.281.360-05";
        var verifyUpdate = validUser.UpdatedAt > DateTime.Now
                           && validUser.UpdatedAt > validUser.CreatedAt;

        validUser.UpdateAdmin(cpf: newCpf);
        validUser.Name.Should().Be(validUser.Name);
        validUser.GetCPF().Should().NotBe(oldCpf);
        validUser.BirthDate.Should().Be(validUser.BirthDate);
        validUser.UpdatedAt.Should().BeAfter(validUser.CreatedAt);
        verifyUpdate.Should().BeTrue();
    }
    [Fact(DisplayName = nameof(UpdateAdminOnlyName))]
    [Trait("User", "Domain Instantiate")]

    public void UpdateAdminOnlyName()
    {
        var validUser = _fixture.GetValidUser();
        var newName = _fixture.GetNewName();
        var verifyUpdate = validUser.UpdatedAt > DateTime.Now
                           && validUser.UpdatedAt > validUser.CreatedAt;

        validUser.UpdateAdmin(name: newName);
        validUser.Name.Should().Be(newName);
        validUser.BirthDate.Should().Be(validUser.BirthDate);
        validUser.UpdatedAt.Should().BeAfter(validUser.CreatedAt);
        verifyUpdate.Should().BeTrue();
    }

    [Fact(DisplayName = nameof(UpdateAdminOnlyBirthDate))]
    [Trait("User", "Domain Instantiate")]

    public void UpdateAdminOnlyBirthDate()
    {
        var validUser = _fixture.GetValidUser();
        var verifyUpdate = validUser.UpdatedAt > DateTime.Now
                           && validUser.UpdatedAt > validUser.CreatedAt;
        var newBirthDate = _fixture.GetValidBirthDate();

        validUser.UpdateAdmin(birthDate: newBirthDate);
        validUser.Name.Should().Be(validUser.Name);
        validUser.BirthDate.Should().Be(newBirthDate);
        validUser.UpdatedAt.Should().BeAfter(validUser.CreatedAt);
        verifyUpdate.Should().BeTrue();
    }


    [Theory(DisplayName = nameof(UpdateAdminWithInvalidName))]
    [Trait("User", "Domain Instantiate")]
    [InlineData("")]
    [InlineData(" ")]

    public void UpdateAdminWithInvalidName(string? name)
    {
        var validUser = _fixture.GetValidUser();

        Action action = () => validUser.UpdateAdmin(name: name);
        action.Should().Throw<EntityValidationException>()
            .WithMessage("Name should not be empty or null");

    }
    [Theory(DisplayName = nameof(UpdateAdminWithInvalidCpf))]
    [Trait("User", "Domain Instantiate")]
    [InlineData("122222222")]
    [InlineData("12345678910")]

    public void UpdateAdminWithInvalidCpf(string? cpf)
    {
        var validUser = _fixture.GetValidUser();

        Action action = () => validUser.UpdateAdmin(cpf: cpf);
        action.Should().Throw<EntityValidationException>()
            .WithMessage("Cpf is invalid");

    }

    [Fact(DisplayName = nameof(UpdateAdminWithInvalidBirthDate))]
    [Trait("User", "Domain Instantiate")]

    public void UpdateAdminWithInvalidBirthDate()
    {
        var validUser = _fixture.GetValidUser();
        var invalidBirthDate = DateTime.Now.AddDays(1);
        Action action = () => validUser.UpdateAdmin(birthDate: invalidBirthDate);
        action.Should().Throw<EntityValidationException>()
            .WithMessage("BirthDate is invalid");

    }

    [Fact(DisplayName = nameof(UpdatePassword))]
    [Trait("User", "Domain Instantiate")]

    public void UpdatePassword()
    {
        var oldPassword = "Test@2023";
        var validUser = _fixture.GetValidUser(oldPassword);
        var newPassword = _fixture.GetValidPassword();

        validUser.UpdatePassword(oldPassword, newPassword);

        var passwordIsValid = validUser.VerifyPassword(newPassword);
        passwordIsValid.Should().BeTrue();
        validUser.UpdatedAt.Should().BeAfter(validUser.CreatedAt);
    }
    [Fact(DisplayName = nameof(ThrowWhenPassWordNotMatch))]
    [Trait("User", "Domain Instantiate")]

    public void ThrowWhenPassWordNotMatch()
    {
        var oldPassword = "Test@2023";
        var validUser = _fixture.GetValidUser();
        var newPassword = _fixture.GetValidPassword();

        Action action = () => validUser.UpdatePassword(oldPassword, newPassword);
        action.Should().Throw<EntityCredentialsInvalid>()
            .WithMessage("Invalid credentials");
    }
    [Fact(DisplayName = nameof(ThrowWhenPassWordNotMatch))]
    [Trait("User", "Domain Instantiate")]

    public void ThrowWhenPasswordIsInvalid()
    {
        var oldPassword = "Test@2023";
        var validUser = _fixture.GetValidUser(oldPassword);
        var newPassword = "12345678";

        Action action = () => validUser.UpdatePassword(oldPassword, newPassword);
        action.Should().Throw<EntityValidationException>()
            .WithMessage("Password not match the security policies");
    }

    [Fact(DisplayName = nameof(ActivateUser))]
    [Trait("User", "Domain Instantiate")]

    public void ActivateUser()
    {

        var validUser = _fixture.GetValidUser();
        validUser.Activate();
        validUser.IsActive.Should().BeTrue();

    }

    [Fact(DisplayName = nameof(Deactivate))]
    [Trait("User", "Domain Instantiate")]

    public void Deactivate()
    {

        var validUser = _fixture.GetValidUser();
        validUser.Deactivate();
        validUser.IsActive.Should().BeFalse();

    }


    [Fact(DisplayName = nameof(GetCPF))]
    [Trait("User", "Domain Instantiate")]

    public void GetCPF()
    {
        var validCpf = "40320668509";
        var validUser = _fixture.GetValidUserWithoutCpf(cpf: validCpf);
        validCpf = "***." + validCpf.Substring(3, 3) + ".***-" + validCpf.Substring(9, 2);

        var cpf = validUser.GetCPF();
        cpf.Should().Be(validCpf);

    }

}
