using System;

namespace TurkishValidators.TestData.Generators
{
    public class PostaKoduGenerator
    {
        private readonly Random _random;

        public PostaKoduGenerator(Random? random = null)
        {
            _random = random ?? new Random();
        }

        public string Generate()
        {
            int cityCode = _random.Next(1, 82);
            string cityPart = cityCode.ToString("D2");
            
            // Son 3 hane rastgele
            string districtPart = _random.Next(0, 1000).ToString("D3");
            
            return cityPart + districtPart;
        }
    }
}
