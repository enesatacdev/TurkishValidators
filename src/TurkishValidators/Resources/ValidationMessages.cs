namespace TurkishValidators.Resources
{
    /// <summary>
    /// Doğrulama hata mesajlarını içerir.
    /// </summary>
    public class ValidationMessages
    {
        // TCKN
        public string TcKimlikNoEmpty { get; set; } = "TC Kimlik No boş olamaz.";
        public string TcKimlikNoLength { get; set; } = "TC Kimlik No 11 haneli olmalıdır.";
        public string TcKimlikNoNumeric { get; set; } = "TC Kimlik No sadece rakamlardan oluşmalıdır.";
        public string TcKimlikNoStartsWithZero { get; set; } = "TC Kimlik No 0 ile başlayamaz.";
        public string TcKimlikNoChecksum { get; set; } = "TC Kimlik No doğrulama algoritması başarısız.";
        
        // Vergi No
        public string VergiNoEmpty { get; set; } = "Vergi Numarası boş olamaz.";
        public string VergiNoLength { get; set; } = "Vergi Numarası 10 haneli olmalıdır.";
        public string VergiNoNumeric { get; set; } = "Vergi Numarası sadece rakamlardan oluşmalıdır.";
        public string VergiNoChecksum { get; set; } = "Vergi Numarası doğrulama algoritması başarısız.";

        // IBAN
        public string IbanEmpty { get; set; } = "IBAN boş olamaz.";
        public string IbanLength { get; set; } = "TR IBAN uzunluğu 26 karakter olmalıdır.";
        public string IbanCountryCode { get; set; } = "IBAN 'TR' ile başlamalıdır.";
        public string IbanChecksum { get; set; } = "IBAN kontrol hanesi hatalı.";

        // Telefon
        public string PhoneEmpty { get; set; } = "Telefon numarası boş olamaz.";
        public string PhoneLength { get; set; } = "Telefon numarası geçerli uzunlukta değil (Başında 0 olmadan 10 hane olmalı).";
        public string PhoneGsmNotAllowed { get; set; } = "GSM numaralarına izin verilmiyor.";
        public string PhoneLandlineNotAllowed { get; set; } = "Sabit hatlara izin verilmiyor.";
        public string PhoneInvalidAreaCode { get; set; } = "Geçersiz operatör/alan kodu.";

        // Plaka
        public string PlateEmpty { get; set; } = "Plaka boş olamaz.";
        public string PlateFormat { get; set; } = "Plaka formatı geçersiz.";
        public string PlateCityCode { get; set; } = "Geçersiz il kodu (01-81 arası olmalı).";

        // Posta Kodu
        public string PostalCodeEmpty { get; set; } = "Posta kodu boş olamaz.";
        public string PostalCodeLength { get; set; } = "Posta kodu 5 haneli olmalıdır.";
        public string PostalCodeNumeric { get; set; } = "Posta kodu sadece rakamlardan oluşmalıdır.";
        public string PostalCodeCityInvalid { get; set; } = "Geçersiz il kodu (01-81 arası olmalı).";

        /// <summary>
        /// Varsayılan Türkçe mesajları içeren yeni bir örnek döndürür.
        /// </summary>
        public static ValidationMessages CreateDefault()
        {
            return new ValidationMessages();
        }

        /// <summary>
        /// Varsayılan İngilizce mesajları içeren yeni bir örnek döndürür.
        /// </summary>
        public static ValidationMessages CreateEnglish()
        {
            return new ValidationMessages
            {
                TcKimlikNoEmpty = "Turkish Identity Number cannot be empty.",
                TcKimlikNoLength = "Turkish Identity Number must be 11 digits.",
                TcKimlikNoNumeric = "Turkish Identity Number must consist of digits only.",
                TcKimlikNoStartsWithZero = "Turkish Identity Number cannot start with 0.",
                TcKimlikNoChecksum = "Turkish Identity Number validation algorithm failed.",
                
                VergiNoEmpty = "Tax Number cannot be empty.",
                VergiNoLength = "Tax Number must be 10 digits.",
                VergiNoNumeric = "Tax Number must consist of digits only.",
                VergiNoChecksum = "Tax Number validation algorithm failed.",
                
                IbanEmpty = "IBAN cannot be empty.",
                IbanLength = "TR IBAN length must be 26 characters.",
                IbanCountryCode = "IBAN must start with 'TR'.",
                IbanChecksum = "IBAN checksum is invalid.",
                
                PhoneEmpty = "Phone number cannot be empty.",
                PhoneLength = "Phone number length is invalid (Must be 10 digits without leading 0).",
                PhoneGsmNotAllowed = "GSM numbers are not allowed.",
                PhoneLandlineNotAllowed = "Landlines are not allowed.",
                PhoneInvalidAreaCode = "Invalid operator/area code.",
                
                PlateEmpty = "License plate cannot be empty.",
                PlateFormat = "License plate format is invalid.",
                PlateCityCode = "Invalid city code (Must be between 01-81).",
                
                PostalCodeEmpty = "Postal code cannot be empty.",
                PostalCodeLength = "Postal code must be 5 digits.",
                PostalCodeNumeric = "Postal code must consist of digits only.",
                PostalCodeCityInvalid = "Invalid city code (Must be between 01-81)."
            };
        }
    }
}
