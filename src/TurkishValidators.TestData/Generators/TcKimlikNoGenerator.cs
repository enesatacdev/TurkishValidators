using System;

namespace TurkishValidators.TestData.Generators
{
    public class TcKimlikNoGenerator
    {
        private readonly Random _random;

        public TcKimlikNoGenerator(Random? random = null)
        {
            _random = random ?? new Random();
        }

        public string Generate()
        {
            int[] digits = new int[11];
            digits[0] = _random.Next(1, 10); // İlk hane 0 olamaz
            for (int i = 1; i < 9; i++) digits[i] = _random.Next(0, 10);

            // 10. hane hesaplama
            int oddSum = digits[0] + digits[2] + digits[4] + digits[6] + digits[8];
            int evenSum = digits[1] + digits[3] + digits[5] + digits[7];
            digits[9] = ((oddSum * 7) - evenSum) % 10;
            if (digits[9] < 0) digits[9] += 10;

            // 11. hane hesaplama
            int sumFirst10 = 0;
            for (int i = 0; i < 10; i++) sumFirst10 += digits[i];
            digits[10] = sumFirst10 % 10;

            return string.Join("", digits);
        }
    }
}
