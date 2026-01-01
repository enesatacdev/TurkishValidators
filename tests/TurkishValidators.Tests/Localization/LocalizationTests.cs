using System.Globalization;
using TurkishValidators.Config;
using TurkishValidators.Resources;
using TurkishValidators.Validators;
using Xunit;

namespace TurkishValidators.Tests.Localization
{
    public class LocalizationTests
    {
        [Fact]
        public void RegisterMessages_ShouldUseCustomMessagesForNewLanguage()
        {
            // Almanca için özel mesaj seti oluşturalım
            var germanMessages = new ValidationMessages
            {
                TcKimlikNoEmpty = "Die TC-Identitätsnummer darf nicht leer sein.",
                TcKimlikNoLength = "Die TC-Identitätsnummer muss 11 Ziffern lang sein."
            };

            // "de-DE" için kaydet
            TurkishValidatorConfig.RegisterMessages("de-DE", germanMessages);

            // Validator'ı Almanca kültürüyle test et
            var validator = new TcKimlikNoValidator(new TurkishValidators.Options.TcKimlikNoValidationOptions
            {
                Culture = new CultureInfo("de-DE")
            });

            var result = validator.Validate(""); // Boş gönder
            Assert.False(result.IsValid);
            Assert.Equal("Die TC-Identitätsnummer darf nicht leer sein.", result.ErrorMessage);
        }

        [Fact]
        public void RegisterMessages_ShouldOverrideExistingLanguage()
        {
            // Türkçe için özel bir mesaj set edelim
            var customTr = ValidationMessages.CreateDefault();
            customTr.TcKimlikNoEmpty = "Lütfen TCKN giriniz (Özel)!";

            TurkishValidatorConfig.RegisterMessages("tr-TR", customTr);

            var validator = new TcKimlikNoValidator(); // Varsayılan tr-TR
            // Not: Varsayılan davranış Config'deki static state'e bağlı olduğu için 
            // diğer testleri etkileyebilir. Bu yüzden test sonunda temizlemek gerekebilir veya 
            // Culture parametresiyle explicit çağırmak daha güvenlidir.
            
            // Explicit tr-TR ile test edelim
            var validatorTr = new TcKimlikNoValidator(new TurkishValidators.Options.TcKimlikNoValidationOptions 
            { 
                Culture = new CultureInfo("tr-TR") 
            });
            var result = validatorTr.Validate("");
            
            Assert.Equal("Lütfen TCKN giriniz (Özel)!", result.ErrorMessage);
        }
        
        [Fact]
        public void GetMessages_ShouldFallbackToTwoLetterIso_WhenSpecificCultureNotFound()
        {
             // "fr" (Fransızca) genel kaydı yapalım
            var frenchMessages = new ValidationMessages
            {
                TcKimlikNoEmpty = "Le numéro d'identité ne peut pas être vide."
            };
            TurkishValidatorConfig.RegisterMessages("fr", frenchMessages);

            // "fr-CA" (Kanada Fransızcası) isteyelim
            var culture = new CultureInfo("fr-CA");
            var messages = TurkishValidatorConfig.GetMessages(culture);

            Assert.Equal(frenchMessages.TcKimlikNoEmpty, messages.TcKimlikNoEmpty);
        }
    }
}
