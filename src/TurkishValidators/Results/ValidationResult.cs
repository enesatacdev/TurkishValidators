using System.Collections.Generic;

namespace TurkishValidators.Results
{
    /// <summary>
    /// Doğrulama işleminin sonucunu temsil eder.
    /// </summary>
    public class ValidationResult
    {
        /// <summary>
        /// Doğrulamanın başarılı olup olmadığını belirtir.
        /// </summary>
        public bool IsValid { get; }

        /// <summary>
        /// Doğrulama başarısız olduğunda hata mesajını içerir.
        /// </summary>
        public string? ErrorMessage { get; }

        /// <summary>
        /// Doğrulama başarısız olduğunda hata kodunu içerir.
        /// </summary>
        public string? ErrorCode { get; }

        /// <summary>
        /// Doğrulama sonucuyla ilgili ek veriler (örn. Plaka doğrulamasında Şehir bilgisi).
        /// </summary>
        public Dictionary<string, object> Metadata { get; }

        private ValidationResult(bool isValid, string? errorMessage, string? errorCode)
        {
            IsValid = isValid;
            ErrorMessage = errorMessage;
            ErrorCode = errorCode;
            Metadata = new Dictionary<string, object>();
        }

        /// <summary>
        /// Başarılı bir doğrulama sonucu oluşturur.
        /// </summary>
        public static ValidationResult Success()
        {
            return new ValidationResult(true, null, null);
        }

        /// <summary>
        /// Başarısız bir doğrulama sonucu oluşturur.
        /// </summary>
        /// <param name="errorMessage">Hata mesajı.</param>
        /// <param name="errorCode">Hata kodu.</param>
        public static ValidationResult Failure(string errorMessage, string? errorCode = null)
        {
            return new ValidationResult(false, errorMessage, errorCode);
        }
    }
}
