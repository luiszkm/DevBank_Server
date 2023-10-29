using User.DevBank.Domain.Domain.SeedWork;

namespace CreateUser.DevBank.Domain.Entity;

  public class AccountBank :BaseEntity
{
    public AccountBank(Guid userId)
    {
        UserId = userId;
        CreateAccount();
    }
    public int Agency { get; private set; }
    public int AccountNumber { get; private set; }
    public AccountTypes AccountType { get; private set; }
    public string Bank { get; private set; }
    private decimal _balance { get; set; }
    private string Balance { get; set; }
    public Guid UserId { get; private set; }
    public Domain.Entity.User User { get; private set; }
    public bool IsActive { get; private set; }


    private void CreateAccount()
    {
        Agency = 0001;
        AccountNumber = int.Parse(GenerateRandomAccountNumber());
        AccountType = AccountTypes.Digital;
        Bank = "DevBank";
        _balance = 0;
        IsActive = true;
    }

    public enum PixTypes
    {
        CPF,
        Email,
        Phone,
        RandomKey
    }

    public enum AccountTypes
    {
        Corrente,
        Poupanca,
        Digital
    }

    private static string GenerateRandomAccountNumber()
    {
        Random random = new Random();
        string accountNumberWithoutCheckDigit = random.Next(0, 1000000000).ToString("D9");
        // Gere um n�mero de 9 d�gitos

        // Adicione um d�gito de verifica��o usando o algoritmo de Luhn.
        int checkDigit = CalculateLuhnCheckDigit(accountNumberWithoutCheckDigit + "0");

        // Retorne o n�mero da conta com o d�gito de verifica��o.
        string accountNumberWithCheckDigit = accountNumberWithoutCheckDigit + checkDigit.ToString();

        return accountNumberWithCheckDigit;
    }

    private static int CalculateLuhnCheckDigit(string accountNumber)
    {
        int sum = 0;
        bool isDouble = false;

        // Comece da direita para a esquerda (do �ltimo d�gito para o primeiro).
        for (int i = accountNumber.Length - 1; i >= 0; i--)
        {
            int digit = int.Parse(accountNumber[i].ToString());

            if (isDouble)
            {
                digit *= 2;
                if (digit > 9)
                {
                    digit -= 9;
                }
            }

            sum += digit;
            isDouble = !isDouble;
        }

        // Calcule o d�gito de verifica��o.
        int checkDigit = (10 - (sum % 10)) % 10;

        return checkDigit;
    }

}
