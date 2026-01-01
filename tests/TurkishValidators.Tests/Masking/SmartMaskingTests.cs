using TurkishValidators.Enums;
using TurkishValidators.Masking;
using TurkishValidators.Options;
using Xunit;

namespace TurkishValidators.Tests.Masking
{
    public class SmartMaskingTests
    {
        [Fact]
        public void TcKimlikNo_ShouldRespect_ShowLastFour()
        {
            var tc = "12345678901";
            var result = TcKimlikNoMasker.Mask(tc, MaskingMode.ShowLastFour);
            
            // Expected: *******8901
            Assert.Equal("*******8901", result);
        }

        [Fact]
        public void Iban_ShouldRespect_ShowFirstTwo()
        {
            // TR330006100519786457841326
            var iban = "TR330006100519786457841326";
            var result = IbanMasker.Mask(iban, MaskingMode.ShowFirstTwo);

            // Expected: TR************************
            Assert.Equal("TR************************", result);
        }

        [Fact]
        public void TelefonNo_ShouldRespect_ShowLastThree()
        {
            var phone = "05321234567";
            var result = TelefonNoMasker.Mask(phone, MaskingMode.ShowLastThree);

            // Expected: ********567
            Assert.Equal("********567", result);
        }

        [Fact]
        public void Plaka_ShouldRespect_All()
        {
            var plate = "34ABC123";
            var result = PlakaMasker.Mask(plate, MaskingMode.All);

            // Expected: ********
            Assert.Equal("********", result);
        }

        [Fact]
        public void VergiNo_ShouldRespect_ShowFirstThree()
        {
            var vkn = "1234567890";
            var result = VergiNoMasker.Mask(vkn, MaskingMode.ShowFirstThree);

            // Expected: 123*******
            Assert.Equal("123*******", result);
        }

        [Fact]
        public void CustomMode_ShouldUse_VisibleStartEnd()
        {
            var tc = "12345678901";
            var options = new MaskingOptions
            {
                Mode = MaskingMode.Custom,
                VisibleStart = 1,
                VisibleEnd = 1,
                MaskChar = '#'
            };

            var result = TcKimlikNoMasker.Mask(tc, options);
            
            // Expected: 1#########1
            Assert.Equal("1#########1", result);
        }
        
        [Fact]
        public void Should_Return_Original_If_Length_Too_Short_For_Mode()
        {
            var shortText = "123";
            // ShowLastFour requires 4 chars visible. If text is 3, start=0, end=4. Length=3 < 0+4.
            // Result should be original text.
            var result = TcKimlikNoMasker.Mask(shortText, MaskingMode.ShowLastFour);

            Assert.Equal("123", result);
        }
    }
}
