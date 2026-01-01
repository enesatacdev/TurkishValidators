using System;

namespace TurkishValidators.TestData.Generators
{
    public class IbanGenerator
    {
        private readonly Random _random;

        public IbanGenerator(Random? random = null)
        {
            _random = random ?? new Random();
        }

        public string Generate()
        {
            string countryCode = "TR";
            string bankCode = _random.Next(1, 1000).ToString("D5");
            string reserve = "0";
            string accountNo = "";
            for (int i = 0; i < 16; i++) accountNo += _random.Next(0, 10);

            string bban = bankCode + reserve + accountNo;
            
            string temp = bban + "2927" + "00"; // TR=2927
            
            decimal dTemp = 0;
            foreach (char c in temp)
            {
                int val = c - '0';
                dTemp = (dTemp * 10 + val) % 97;
            }
            
            int checkDigits = 98 - (int)dTemp;
            string checkDigitsStr = checkDigits.ToString("D2");

            return countryCode + checkDigitsStr + bban;
        }
    }
}
