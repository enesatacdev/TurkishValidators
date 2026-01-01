using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TurkishValidators.Enums;
using TurkishValidators.Results;

namespace TurkishValidators.Validators
{
    /// <summary>
    /// Kredi kartı doğrulama ve BIN sorgulama işlemlerini sağlar.
    /// </summary>
    public class CreditCardValidator
    {
        // Örnek BIN listesi (Gerçek hayatta bu liste çok daha geniştir ve veritabanından gelmelidir)
        private static readonly Dictionary<string, string> _bankBins = new Dictionary<string, string>
        {
            // Ziraat
            { "469884", "Ziraat Bankası" }, { "589283", "Ziraat Bankası" }, { "6760", "Ziraat Bankası" },
            // Garanti
            { "554960", "Garanti BBVA" }, { "482856", "Garanti BBVA" }, { "5209", "Garanti BBVA" },
            // İş Bankası
            { "454360", "Türkiye İş Bankası" }, { "540668", "Türkiye İş Bankası" }, { "450803", "Türkiye İş Bankası" },
            // Akbank
            { "4740", "Akbank" }, { "5526", "Akbank" },
            // Yapı Kredi
            { "450634", "Yapı Kredi" }, { "5401", "Yapı Kredi" },
            // Troy (Deneme / Genel)
            { "9792", "Troy Kart" }, { "65", "Troy Kart" }
        };

        /// <summary>
        /// Kart numarasının Luhn algoritmasına göre geçerli olup olmadığını ve sayısal olduğunu kontrol eder.
        /// </summary>
        public static bool IsValid(string? cardNumber)
        {
            if (string.IsNullOrEmpty(cardNumber)) return false;

            // Sadece rakamları al
            var digitOnly = Regex.Replace(cardNumber, @"\D", "");
            if (digitOnly.Length < 13 || digitOnly.Length > 19) return false;

            return CheckLuhn(digitOnly);
        }

        /// <summary>
        /// Kart numarasının BIN (ilk 6 veya 8 hane) bilgisini analiz eder.
        /// </summary>
        public static CreditCardBinResult GetBinInfo(string? cardNumber)
        {
            var result = new CreditCardBinResult();
            
            if (!IsValid(cardNumber))
            {
                result.IsValid = false;
                result.ErrorMessage = "Geçersiz kart numarası.";
                return result;
            }

            var cleanCard = Regex.Replace(cardNumber!, @"\D", "");
            result.IsValid = true;
            result.CardType = IdentifyCardType(cleanCard);
            result.BankName = IdentifyBank(cleanCard);

            return result;
        }

        private static bool CheckLuhn(string digits)
        {
            int sum = 0;
            bool checkDigit = false;

            for (int i = digits.Length - 1; i >= 0; i--)
            {
                int digit = digits[i] - '0';

                if (checkDigit)
                {
                    digit *= 2;
                    if (digit > 9) digit -= 9;
                }

                sum += digit;
                checkDigit = !checkDigit;
            }

            return (sum % 10) == 0;
        }

        private static CardType IdentifyCardType(string number)
        {
            if (number.StartsWith("4")) return CardType.Visa;
            if (number.StartsWith("51") || number.StartsWith("52") || number.StartsWith("53") || number.StartsWith("54") || number.StartsWith("55")) return CardType.Mastercard;
            if (number.StartsWith("2221") || number.StartsWith("2720")) return CardType.Mastercard; // Yeni MC serileri
            if (number.StartsWith("34") || number.StartsWith("37")) return CardType.AmericanExpress;
            if (number.StartsWith("9792") || number.StartsWith("65")) return CardType.Troy;
            
            return CardType.Unknown;
        }

        private static string IdentifyBank(string number)
        {
            // Önce 6 haneli BIN kontrolü
            if (number.Length >= 6)
            {
                var prefix6 = number.Substring(0, 6);
                if (_bankBins.ContainsKey(prefix6)) return _bankBins[prefix6];
            }

            // 4 haneli geniş eşleşme kontrolü (Demo amaçlı)
            if (number.Length >= 4)
            {
                var prefix4 = number.Substring(0, 4);
                if (_bankBins.ContainsKey(prefix4)) return _bankBins[prefix4];
            }
            
            // 2 hane
            if (number.Length >= 2)
            {
                 var prefix2 = number.Substring(0, 2);
                 if (_bankBins.ContainsKey(prefix2)) return _bankBins[prefix2];
            }

            return "Bilinmeyen Banka";
        }
    }
}
