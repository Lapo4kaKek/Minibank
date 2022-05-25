using System;

namespace Minibank.Core
{
    public class Calculator:ICalculator
    {
        public readonly IDatabase _database;

        public Calculator()
        {
        }

        public Calculator(IDatabase database)
        {
            _database = database;
        }
        public double ConvertCurrency(string currency, string input)
        {
            bool result = double.TryParse(input, out double balance);
            if (!result)
            {
                throw new UserFriendlyException("Некорректный ввод", input);
            }
            var value = _database.GetRubleCourse(currency);
            double sum = value * balance;
            if (sum < 0)
            {
                throw new UserFriendlyException("Отрицательный баланс", input);
            }
            return sum;
        }
        public double ConvertCurrency(double amount, string fromCurrency, string toCurrency, IDatabase database)
        {
            if (fromCurrency != "RUB")
            {
                amount = amount * database.GetRubleCourse(fromCurrency);
            }
            if (toCurrency == "RUB")
            {
                return amount;
            }
            double value2 = 1 / (database.GetRubleCourse(toCurrency) * 1.0);

            double sum = Math.Round(amount * value2, 2);
            if (sum < 0)
            {
                throw new UserFriendlyException("Отрицательный баланс", amount.ToString());
            }
            return sum;
        }
    }
}