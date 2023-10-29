using CreateUser.DevBank.Domain.Domain.Exceptions;
using CreateUser.DevBank.UnitTest.Domain.Common;

namespace CreateUser.DevBank.UnitTest.Domain.Entity.User;
[Collection(nameof(UserTestFixture))]
public class UserTest
{

    private readonly UserTestFixture _fixture;

    public UserTest(UserTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(Instantiate))]
    [Trait("User", "Domain Instantiate")]
    public void Instantiate()
    {
        //Arrange
        var validUser = _fixture.GetValidUser();
        var validPassword = _fixture.GetValidPassword();
        var ValidCPF = _fixture.GetValidCPF();
        //Act
        var user = new DomainEntity.User(
            validUser.Name,
            validUser.Email,
            validPassword,
            validUser.PhoneNumber,
            ValidCPF,
            validUser.BirthDate);
        //Assert

        user.Should().NotBeNull();
        user.Name.Should().Be(validUser.Name);
        user.Email.Should().Be(validUser.Email);
        user.PhoneNumber.Should().Be(validUser.PhoneNumber);
        user.BirthDate.Should().Be(validUser.BirthDate);
        user.VerifyPassword(validPassword).Should().BeTrue();

    }
    [Theory(DisplayName = nameof(InstantiateWithInvalidName))]
    [Trait("User", "Domain Instantiate")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("   ")]
    public void InstantiateWithInvalidName(string? invalidName)
    {
        //Arrange
        var validUser = _fixture.GetValidUser();
        var validPassword = _fixture.GetValidPassword();
        var ValidCPF = _fixture.GetValidCPF();
        //Act
        Action action =
           () => new DomainEntity.User(
               invalidName,
               validUser.Email,
               validPassword,
               validUser.PhoneNumber,
               ValidCPF,
               validUser.BirthDate);
        //Assert

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Name should not be empty or null");
    }

    [Theory(DisplayName = nameof(InstantiateWhitEmptyOrNullEmail))]
    [Trait("User", "Domain Instantiate")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("   ")]

    public void InstantiateWhitEmptyOrNullEmail(string? invalidEmail)
    {
        //Arrange
        var validUser = _fixture.GetValidUser();
        var validPassword = _fixture.GetValidPassword();
        var ValidCPF = _fixture.GetValidCPF();
        //Act
        Action action =
           () => new DomainEntity.User(
               validUser.Name,
               invalidEmail,
               validPassword,
               validUser.PhoneNumber,
               ValidCPF,
               validUser.BirthDate);
        //Assert

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Email should not be empty or null");
    }
    [Theory(DisplayName = nameof(InstantiateWhitInvalidEmail))]
    [Trait("User", "Domain Instantiate")]
    [InlineData("test")]
    [InlineData("test.com")]
    [InlineData("test@")]
    [InlineData("test@.com")]

    public void InstantiateWhitInvalidEmail(string? invalidEmail)
    {
        //Arrange
        var validUser = _fixture.GetValidUser();
        var validPassword = _fixture.GetValidPassword();
        var ValidCPF = _fixture.GetValidCPF();
        //Act
        Action action =
            () => new DomainEntity.User(
                validUser.Name,
                invalidEmail,
                validPassword,
                validUser.PhoneNumber,
                ValidCPF,
                validUser.BirthDate);
        //Assert

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Email is invalid");
    }

    [Theory(DisplayName = nameof(InsatiateWithInvalidPassword))]
    [Trait("User", "Domain Instantiate")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("   ")]
    [InlineData("1234567")]
    public void InsatiateWithInvalidPassword(string? invalidPassword)
    {
        //Arrange
        var validUser = _fixture.GetValidUser();
        var ValidCPF = _fixture.GetValidCPF();
        //Act
        Action action =
           () => new DomainEntity.User(
               validUser.Name,
               validUser.Email,
               invalidPassword,
               validUser.PhoneNumber,
               ValidCPF,
               validUser.BirthDate);
        //Assert

        action.Should().Throw<EntityValidationException>()
        .WithMessage("Password not match the security policies");

    }

    [Theory(DisplayName = nameof(InstantiateWhitCPFInvalid))]
    [Trait("User", "Domain Instantiate")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("   ")]
    [InlineData("1234567")]
    [InlineData("0123456789101112")]
    public void InstantiateWhitCPFInvalid(string? invalidCPF)
    {
        //Arrange
        var validUser = _fixture.GetValidUser();
        var validPassword = _fixture.GetValidPassword();
        //Act
        Action action =
           () => new DomainEntity.User(
               validUser.Name,
               validUser.Email,
               validPassword,
               validUser.PhoneNumber,
               invalidCPF,
               validUser.BirthDate);
        //Assert

        action.Should().Throw<EntityValidationException>()
            .WithMessage("CPF is invalid");

    }

    [Fact(DisplayName = nameof(InstantiateWhitBirthDateInvalid))]
    [Trait("User", "Domain Instantiate")]

    public void InstantiateWhitBirthDateInvalid()
    {
        //Arrange
        var validUser = _fixture.GetValidUser();
        var validPassword = _fixture.GetValidPassword();
        var ValidCPF = _fixture.GetValidCPF();
        DateTime invalidBirthDate = DateTime.Now.AddHours(10);
        //Act
        Action action =
           () => new DomainEntity.User(validUser.Name,
               validUser.Email,
               validPassword,
               validUser.PhoneNumber,
               ValidCPF,
               invalidBirthDate);
        //Assert

        action.Should().Throw<EntityValidationException>()
            .WithMessage("BirthDate is invalid");

    }

    [Fact(DisplayName = nameof(ThrowWhenDoesNotReachTheMinAge))]
    [Trait("User", "Domain Instantiate")]

    public void ThrowWhenDoesNotReachTheMinAge()
    {
        //Arrange
        var validUser = _fixture.GetValidUser();
        var validPassword = _fixture.GetValidPassword();
        var ValidCPF = _fixture.GetValidCPF();
        DateTime invalidBirthDate = _fixture.GetInvalidBirthDate();
        //Act
        Action action =
            () => new DomainEntity.User(validUser.Name,
                validUser.Email,
                validPassword,
                validUser.PhoneNumber,
                ValidCPF,
                invalidBirthDate);
        //Assert

        action.Should().Throw<EntityValidationException>()
            .WithMessage("The user is not old enough, the minimum age is 12 years old");
    }

    [Fact(DisplayName = nameof(ThrowWhenYourBirthDayIsJustAFewDaysAway))]
    [Trait("User", "Domain Instantiate")]

    public void ThrowWhenYourBirthDayIsJustAFewDaysAway()
    {
        //Arrange
        var validUser = _fixture.GetValidUser();
        var validPassword = _fixture.GetValidPassword();
        var ValidCPF = _fixture.GetValidCPF();
        DateTime invalidBirthDate = _fixture.GetInvalidBirthDateNow();
        //Act
        Action action =
            () => new DomainEntity.User(validUser.Name,
                validUser.Email,
                validPassword,
                validUser.PhoneNumber,
                ValidCPF,
                invalidBirthDate);
        //Assert
        var test = DateTime.Now > invalidBirthDate;

        action.Should().Throw<EntityValidationException>()
            .WithMessage("The user is not old enough, the minimum age is 12 years old");

    }

    [Fact(DisplayName = nameof(ThrowWhenDoesNotReachTheMinAge))]
    [Trait("User", "Domain Instantiate")]

    public void InstantiateWithUserHasABirthDayToday()
    {

        //Arrange
        DateTime birthDayToday = _fixture.GetValidBirthDateNow();
        var validUser = _fixture.GetValidUser();
        var validPassword = _fixture.GetValidPassword();
        var ValidCPF = _fixture.GetValidCPF();
        //Act
        var user = new DomainEntity.User(
            validUser.Name,
            validUser.Email,
            validPassword,
            validUser.PhoneNumber,
            ValidCPF,
            birthDayToday);
        //Assert

        user.Should().NotBeNull();
        user.Name.Should().Be(validUser.Name);
        user.Email.Should().Be(validUser.Email);
        user.PhoneNumber.Should().Be(validUser.PhoneNumber);
        user.BirthDate.Should().Be(birthDayToday);
        user.VerifyPassword(validPassword).Should().BeTrue();




    }
}
