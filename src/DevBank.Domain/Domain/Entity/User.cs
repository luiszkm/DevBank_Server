
using System.Security.Cryptography;
using System.Text;
using CreateUser.DevBank.Domain.Domain.Exceptions;
using CreateUser.DevBank.Domain.Domain.Validations;
using User.DevBank.Domain.Domain.Exceptions;
using User.DevBank.Domain.Domain.SeedWork;
using User.DevBank.Domain.Domain.Validations;

namespace CreateUser.DevBank.Domain.Domain.Entity;
public class User : AggregateRoot
{

    public User(
        string name,
        string email,
        string password,
        string phoneNumber,
        string cpf,
        DateTime birthDate)
    {

        Name = name;
        Email = email;
        SetPassword(password);
        PhoneNumber = phoneNumber;
        SetCPF(cpf);
        IsActive = true;
        SetBirthDate(birthDate);
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now.AddMilliseconds(10);
        Validate();

    }

    public string Name { get; private set; }
    public string Email { get; private set; }
    private string _password { get; set; }
    public string PhoneNumber { get; private set; }
    private string _cpf { get; set; }
    public bool IsActive { get; private set; }
    public DateTime BirthDate { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private void SetCPF(string? cpf)
    {
        if (cpf == null)
        {
            throw new EntityValidationException("CPF is invalid");
        }
        var cpfIsValid = CpfValidations.IsValid(cpf);

        if (!cpfIsValid)
        {
            throw new EntityValidationException("CPF is invalid");
        }
        cpf = cpf.Replace(".", "").Replace("-", "").Trim();
        _cpf = cpf;

    }
    public string GetCPF()
    {
        string cpf = _cpf;
        if (cpf.Length == 11)
        {
            cpf = "***." + cpf.Substring(3, 3) + ".***-" + cpf.Substring(9, 2);
        }
        Validate();
        return cpf;
    }

    public void Update(
        string? email = null,
        string? phoneNumber = null)
    {
        VerifyEmail(email);
        Validate();
        if (email is not null)
            Email = email;

        if (phoneNumber is not null)
            PhoneNumber = phoneNumber;

        UpdatedAt = DateTime.Now;
    }

    public void UpdateAdmin(
        string? name = null,
        string? cpf = null,
        DateTime? birthDate = null)
    {
        if (name is not null)
            Name = name;
        if (cpf is not null)
            SetCPF(cpf);
        if (birthDate is not null)
            SetBirthDate(birthDate.Value);
        UpdatedAt = DateTime.Now;
        Validate();

    }

    public void UpdatePassword(string oldPassword, string newPassword)
    {
        var verify = VerifyPassword(oldPassword);
        if (!verify)
        {
            throw new EntityCredentialsInvalid();
        }
        SetPassword(newPassword);
        Validate();
    }

    public void Deactivate()
    {
        this.IsActive = false;
        this.UpdatedAt = DateTime.Now;
        Validate();
    }

    public void Activate()
    {
        this.IsActive = true;
        this.UpdatedAt = DateTime.Now;
        Validate();
    }

    private void SetPassword(string password)
    {
        var passwordIsValid = PasswordValidations.IsValid(password);
        if (!passwordIsValid)
        {
            throw new EntityValidationException("Password not match the security policies");
        }
        _password = HashPassword(password);

    }

    public bool VerifyPassword(string password)
    {
        var passwordHash = HashPassword(password);
        Validate();
        bool valid = passwordHash.Equals(_password);
        return valid;
    }

    private string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }


    public void Validate()
    {
        Domainvalidation.NotNullOrEmpty(Name, nameof(Name));
        Domainvalidation.NotNullOrEmpty(Email, nameof(Email));
        Domainvalidation.MinLength(Email, 3, nameof(Email));
        Domainvalidation.MinLength(Name, 3, nameof(Name));
        Domainvalidation.MaxLength(Name, 255, nameof(Name));
        VerifyEmail(Email);

    }

    private void SetBirthDate(DateTime birthDate)
    {
        if (birthDate > DateTime.Now)
        {
            throw new EntityValidationException("BirthDate is invalid");

        }
        if (birthDate.AddYears(12) > DateTime.Now)
        {
            throw new EntityValidationException(
                "The user is not old enough, the minimum age is 12 years old"
            );
        }
        BirthDate = birthDate;
    }

    private void VerifyEmail(string email)
    {
        var emailIsValid = EmailValidator.IsValidEmail(email);
        if (!emailIsValid)
        {
            throw new EntityValidationException("Email is invalid");
        }
    }

}
