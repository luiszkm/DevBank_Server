namespace User.DevBank.Domain.Domain.Validations
{
    public static class CpfValidations
    {
        public static bool IsValid(string cpf)
        {
            cpf = cpf.Replace(".", "").Replace("-", "").Trim();
            if (cpf.Length != 11)
                return false;

            if (cpf == "00000000000" ||
                cpf == "11111111111" ||
                cpf == "22222222222" ||
                cpf == "33333333333" ||
                cpf == "44444444444" ||
                cpf == "55555555555" ||
                cpf == "66666666666" ||
                cpf == "77777777777" ||
                cpf == "88888888888" ||
                cpf == "99999999999")
                return false;

            int[] multiplierOne = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplierTwo = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf;
            int sum;
            int remainder;

            tempCpf = cpf.Substring(0, 9);

            sum = 0;
            for (int i = 0; i < 9; i++)
                sum += int.Parse(tempCpf[i].ToString()) * multiplierOne[i];

            remainder = sum % 11;
            if (remainder < 2)
                remainder = 0;
            else
                remainder = 11 - remainder;

            if (cpf[9] != char.Parse(remainder.ToString()))
                return false;

            tempCpf = cpf.Substring(0, 10);

            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += int.Parse(tempCpf[i].ToString()) * multiplierTwo[i];

            remainder = sum % 11;
            if (remainder < 2)
                remainder = 0;
            else
                remainder = 11 - remainder;

            if (cpf[10] != char.Parse(remainder.ToString()))
                return false;

            return true;
        }
    }
}
