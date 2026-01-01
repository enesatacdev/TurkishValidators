using System;

namespace TurkishValidators.TestData.Generators
{
    public class TelefonNoGenerator
    {
        private readonly Random _random;
        
        // Yaygın GSM prefixleri
        private static readonly string[] GsmPrefixes = { "530", "531", "532", "533", "534", "535", "536", "537", "538", "539", "540", "541", "542", "543", "544", "545", "546", "547", "548", "549", "551", "552", "553", "554", "555", "559", "501", "505", "506", "507" };

        public TelefonNoGenerator(Random? random = null)
        {
            _random = random ?? new Random();
        }

        /// <summary>
        /// Geçerli bir GSM numarası üretir.
        /// </summary>
        public string GenerateGsm()
        {
            string prefix = GsmPrefixes[_random.Next(GsmPrefixes.Length)];
            string number = "";
            for (int i = 0; i < 7; i++) number += _random.Next(0, 10);
            return "0" + prefix + number;
        }

        /// <summary>
        /// Geçerli bir sabit hat numarası üretir (Örn: 0212...)
        /// </summary>
        public string GenerateLandline()
        {
            // Alan kodları (Örnek: 212, 216, 312 vs.)
            // Türkiye'de alan kodları 2, 3, 4 ile başlar.
            // Basitlik için rastgele geçerli görünümlü bir alan kodu üretelim.
            // 200-499 arası.
            int areaCode = _random.Next(200, 499);
            string number = "";
            for (int i = 0; i < 7; i++) number += _random.Next(0, 10);
            return "0" + areaCode + number;
        }
    }
}
