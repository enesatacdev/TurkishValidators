using TurkishValidators.TestData.Services;
using TurkishValidators.TestData.Models;
using TurkishValidators.Validators;
using Xunit;

namespace TurkishValidators.Tests.TestData
{
    public class TurkishDataProviderTests
    {
        private readonly TurkishDataProvider _generator = new TurkishDataProvider();

        [Fact]
        public void GenerateTcKimlikNo_ShouldGenerateValidTc()
        {
            var validator = new TcKimlikNoValidator();
            for (int i = 0; i < 100; i++)
            {
                string tc = _generator.GenerateTcKimlikNo();
                Assert.True(validator.Validate(tc).IsValid, $"Failed for TC: {tc}");
            }
        }

        [Fact]
        public void GenerateVergiNo_ShouldGenerateValidVkn()
        {
            var validator = new VergiNoValidator();
            for (int i = 0; i < 100; i++)
            {
                string vkn = _generator.GenerateVergiNo();
                // VergiNo generation relies on strict algorithm in Validator.
                // Our generator is simple. Let's ensure it passes.
                Assert.True(validator.Validate(vkn).IsValid, $"Failed for VKN: {vkn}");
            }
        }

        [Fact]
        public void GenerateTurkishIban_ShouldGenerateValidIban()
        {
            var validator = new IbanValidator();
            for (int i = 0; i < 100; i++)
            {
                string iban = _generator.GenerateTurkishIban();
                Assert.True(validator.Validate(iban).IsValid, $"Failed for IBAN: {iban}");
            }
        }

        [Fact]
        public void GenerateVehiclePlate_ShouldGenerateValidPlate()
        {
            var validator = new PlakaValidator();
            for (int i = 0; i < 100; i++)
            {
                string plate = _generator.GenerateVehiclePlate();
                Assert.True(validator.Validate(plate).IsValid, $"Failed for Plate: {plate}");
            }
        }

        [Fact]
        public void GenerateVehiclePlate_WithCity_ShouldGenerateCorrectCity()
        {
            string plate = _generator.GenerateVehiclePlate("Ankara");
            Assert.StartsWith("06", plate);
            
            var validator = new PlakaValidator();
            Assert.True(validator.Validate(plate).IsValid);
        }

        [Fact]
        public void GeneratePhoneGsm_ShouldGenerateValidGsm()
        {
            var validator = new TelefonNoValidator();
            for (int i = 0; i < 50; i++)
            {
                string phone = _generator.GeneratePhoneGsm();
                // GSM starts with 05...
                Assert.StartsWith("05", phone);
                Assert.True(validator.Validate(phone).IsValid, $"Failed for GSM: {phone}");
            }
        }

        [Fact]
        public void GeneratePhoneLandline_ShouldGenerateValidLandline()
        {
            var options = new TurkishValidators.Options.TelefonNoValidationOptions { AllowLandlines = true };
            var validator = new TelefonNoValidator(options);
            for (int i = 0; i < 50; i++)
            {
                string phone = _generator.GeneratePhoneLandline();
                // Landlines in TR start with 02, 03, 04... (Generator uses 02-04 range approx)
                Assert.True(validator.Validate(phone).IsValid, $"Failed for Landline: {phone}");
            }
        }

        [Fact]
        public void GeneratePostalCode_ShouldGenerateValidCode()
        {
            var validator = new PostaKoduValidator();
            for (int i = 0; i < 50; i++)
            {
                string code = _generator.GeneratePostalCode();
                Assert.True(validator.Validate(code).IsValid, $"Failed for PostalCode: {code}");
            }
        }

        [Fact]
        public void GenerateBulk_ShouldGenerateList()
        {
            var list = _generator.GenerateBulk(50);
            Assert.Equal(50, list.Count);
            
            var tcValidator = new TcKimlikNoValidator();
            var phoneValidator = new TelefonNoValidator();

            foreach (var item in list)
            {
                Assert.True(tcValidator.Validate(item.TcKimlikNo).IsValid);
                Assert.True(phoneValidator.Validate(item.Telefon).IsValid);
                Assert.False(string.IsNullOrEmpty(item.PostaKodu));
            }
        }
    }
}
