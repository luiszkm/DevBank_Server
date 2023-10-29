
using Bogus;
using Bogus.Extensions.Brazil;

public class BaseFixture
{
    public Faker Faker { get; set; }
    public BaseFixture()
        => Faker = new Faker("pt_BR");
    public bool GetRandomBoolean()
        => new Random().NextDouble() < 0.5;
    public string GetValidName()
        => Faker.Person.FullName;
    public string GetValidEmail()
    => Faker.Person.Email;



}