using TurkishValidators.Enums;

namespace TurkishValidators.Results
{
    /// <summary>
    /// Kredi kartı BIN sorgulama sonucu.
    /// </summary>
    public class CreditCardBinResult
    {
        public bool IsValid { get; set; }
        public CardType CardType { get; set; }
        public string BankName { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
