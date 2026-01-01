using System;
using TurkishValidators.TestData.Services;

namespace TurkishValidators.TestData
{
    /// <summary>
    /// Test verisi üretimi için statik erişim noktası.
    /// TurkishDataProvider sınıfının metotlarını statik olarak sunar.
    /// </summary>
    public static class TurkishFaker
    {
        private static readonly TurkishDataProvider _provider = new TurkishDataProvider();

        /// <summary>
        /// Geçerli bir TC Kimlik Numarası üretir.
        /// </summary>
        public static string GenerateTCKN() => _provider.GenerateTcKimlikNo();

        /// <summary>
        /// Geçerli bir Vergi Kimlik Numarası üretir.
        /// </summary>
        public static string GenerateVKN() => _provider.GenerateVergiNo();

        /// <summary>
        /// Geçerli bir Türk IBAN numarası üretir.
        /// </summary>
        public static string GenerateIBAN() => _provider.GenerateTurkishIban();

        /// <summary>
        /// Geçerli bir Plaka kodu üretir (Örn: 34 ABC 123).
        /// </summary>
        /// <param name="cityName">Opsiyonel il adı (Örn: "Istanbul" veya "34").</param>
        public static string GeneratePlate(string? cityName = null) => _provider.GenerateVehiclePlate(cityName);

        /// <summary>
        /// Geçerli bir GSM numarası üretir (Örn: 05321234567).
        /// </summary>
        public static string GenerateGSM() => _provider.GeneratePhoneGsm();

        /// <summary>
        /// Geçerli bir Sabit hat numarası üretir (Örn: 02121234567).
        /// </summary>
        public static string GenerateLandline() => _provider.GeneratePhoneLandline();
        
        /// <summary>
        /// Geçerli bir Posta Kodu üretir.
        /// </summary>
        public static string GeneratePostalCode() => _provider.GeneratePostalCode();
    }
}
