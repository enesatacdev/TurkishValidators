using TurkishValidators.Masking;
using TurkishValidators.Options;
using Xunit;

namespace TurkishValidators.Tests.Masking
{
    public class MaskingTests
    {
        [Fact]
        public void TcKimlikNoMasker_ShouldMaskCorrectly()
        {
            var result = TcKimlikNoMasker.Mask("12345678901");
            Assert.Equal("123******01", result);
        }

        [Fact]
        public void IbanMasker_ShouldMaskCorrectly()
        {
            var result = IbanMasker.Mask("TR330006100519786457841326");
            Assert.Equal("TR********************1326", result);
        }

        [Fact]
        public void TelefonNoMasker_ShouldMaskCorrectly()
        {
            var result = TelefonNoMasker.Mask("05321234567");
            Assert.Equal("053******67", result);
        }

        [Fact]
        public void PlakaMasker_ShouldMaskCorrectly()
        {
            var result = PlakaMasker.Mask("34ABC123");
            Assert.Equal("34******", result);
        }

        [Fact]
        public void VergiNoMasker_ShouldMaskCorrectly()
        {
            var result = VergiNoMasker.Mask("1234567890");
            Assert.Equal("123*****90", result);
        }

        [Fact]
        public void CustomMasking_ShouldWork()
        {
            var options = new MaskingOptions { VisibleStart = 0, VisibleEnd = 4, MaskChar = 'X' };
            var result = TcKimlikNoMasker.Mask("12345678901", options);
            Assert.Equal("XXXXXXX8901", result);
        }
    }
}
