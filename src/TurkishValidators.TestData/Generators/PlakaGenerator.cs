using System;
using TurkishValidators.Resources;

namespace TurkishValidators.TestData.Generators
{
    public class PlakaGenerator
    {
        private readonly Random _random;

        public PlakaGenerator(Random? random = null)
        {
            _random = random ?? new Random();
        }

        public string Generate(string? cityName = null)
        {
            int cityCode;
            if (!string.IsNullOrEmpty(cityName))
            {
                int? code = CityData.GetPlateCode(cityName);
                if (!code.HasValue) throw new ArgumentException("Geçersiz şehir ismi.", nameof(cityName));
                cityCode = code.Value;
            }
            else
            {
                cityCode = _random.Next(1, 82);
            }

            string cityCodeStr = cityCode.ToString("D2");
            
            // Random pattern
            int pattern = _random.Next(0, 3);
            string letters = "";
            string numbers = "";

            if (pattern == 0) // Lit: 2, Num: 3 (AA 999)
            {
                 letters = GetRandomLetters(2);
                 numbers = GetRandomNumbers(3);
            }
            else if (pattern == 1) // Lit: 1, Num: 4 (A 9999)
            {
                letters = GetRandomLetters(1);
                 numbers = GetRandomNumbers(4);
            }
            else // Lit: 3, Num: 2 (ABC 99)
            {
                letters = GetRandomLetters(3);
                numbers = GetRandomNumbers(2);
            }

            return $"{cityCodeStr} {letters} {numbers}";
        }

        private string GetRandomLetters(int count)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char[] buffer = new char[count];
            for (int i = 0; i < count; i++) buffer[i] = chars[_random.Next(chars.Length)];
            return new string(buffer);
        }

        private string GetRandomNumbers(int count)
        {
            string result = "";
            for (int i = 0; i < count; i++) result += _random.Next(0, 10);
            return result;
        }
    }
}
