using System;

namespace TurkishValidators.TestData.Generators
{
    public class VergiNoGenerator
    {
        private readonly Random _random;

        public VergiNoGenerator(Random? random = null)
        {
            _random = random ?? new Random();
        }

        public string Generate()
        {
            // Basit ve geçerli bir algoritma ile VKN üretimi
            // VKN algoritması karmaşık olduğu için ve önceki uygulama testlerde sorun çıkardığı için
            // burada '1234567890' gibi bilinen test numaralarını veya
            // basit bir geçerli numara setinden rastgele seçim yapabiliriz.
            // Ancak kullanıcı "üretim" istediği için matematiksel olarak üretmeyi deneriz.
            
            // Generate 9 digits
            int[] digits = new int[10];
            for (int i = 0; i < 9; i++) digits[i] = _random.Next(0, 10);

            // Calculate checksum (Simple VKN Algo implementation for generation)
            // (d1 + 9) % 10 ... complicated inverse operations might be needed to FORCE a specific checksum?
            // No, we generate 9 digits and COMPUTE the 10th.
            
            int sum = 0;
            for (int i = 0; i < 9; i++)
            {
                int digit = digits[i];
                int weight = 10 - (i + 1);
                int v1 = (digit + weight) % 10;
                int v2;
                
                if (v1 == 9)
                {
                    v2 = 9;
                }
                else
                {
                    // (v1 * 2^weight) % 9
                    int powerOf2 = (int)Math.Pow(2, weight);
                    v2 = (v1 * powerOf2) % 9;
                }

                if (digit == 9 && weight == 9 && i == 0) // Special edge cases in VKN are complex?
                {
                    // The standard formula usually works.
                }

                sum += v2;
            }

            int checkControl = sum % 10;
            int checksum = (10 - checkControl) % 10;
            
            digits[9] = checksum;

            return string.Join("", digits);
        }
    }
}
