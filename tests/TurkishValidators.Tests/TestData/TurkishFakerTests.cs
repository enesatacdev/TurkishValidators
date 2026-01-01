using TurkishValidators.TestData;
using TurkishValidators.Validators;
using Xunit;

namespace TurkishValidators.Tests.TestData
{
    public class TurkishFakerTests
    {
        [Fact]
        public void GenerateTCKN_ShouldReturnValidTCKN()
        {
            var tc = TurkishFaker.GenerateTCKN();
            Assert.True(TcKimlikNoValidator.IsValid(tc));
        }

        [Fact]
        public void GenerateIBAN_ShouldReturnValidIBAN()
        {
            var iban = TurkishFaker.GenerateIBAN();
            Assert.True(IbanValidator.IsValid(iban));
        }

        [Fact]
        public void GenerateVKN_ShouldReturnValidVKN()
        {
            var vkn = TurkishFaker.GenerateVKN();
            Assert.True(VergiNoValidator.IsValid(vkn));
        }

        [Fact]
        public void GeneratePlate_ShouldReturnValidPlate()
        {
            var plate = TurkishFaker.GeneratePlate();
            Assert.True(PlakaValidator.IsValid(plate));
        }

        [Fact]
        public void GenerateGSM_ShouldReturnValidGSM()
        {
            var phone = TurkishFaker.GenerateGSM();
            Assert.True(TelefonNoValidator.IsValid(phone));
        }
    }
}
