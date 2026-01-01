using System;
using TurkishValidators.Options;

namespace TurkishValidators.Masking
{
    /// <summary>
    /// IBAN maskeleme işlemlerini sağlar.
    /// </summary>
    public static class IbanMasker
    {
        public static string Mask(string? iban, MaskingOptions? options = null)
        {
            if (string.IsNullOrEmpty(iban)) return string.Empty;

            // Varsayılan: TR ve son 4 hane görünür
            var opts = options ?? new MaskingOptions { VisibleStart = 2, VisibleEnd = 4 };

            if (opts.Mode != Enums.MaskingMode.Custom)
            {
                ApplyModeSettings(opts, iban.Length);
            }
            
            if (iban.Length <= opts.VisibleStart + opts.VisibleEnd) return iban;

            string start = iban.Substring(0, opts.VisibleStart);
            string end = iban.Substring(iban.Length - opts.VisibleEnd);
            string middle = new string(opts.MaskChar, iban.Length - (opts.VisibleStart + opts.VisibleEnd));

            return start + middle + end;
        }

        /// <summary>
        /// IBAN'ı belirli bir mod ile maskeler.
        /// </summary>
        public static string Mask(string? iban, Enums.MaskingMode mode)
        {
            return Mask(iban, new MaskingOptions { Mode = mode });
        }

        private static void ApplyModeSettings(MaskingOptions opts, int length)
        {
            switch (opts.Mode)
            {
                case Enums.MaskingMode.All:
                    opts.VisibleStart = 0;
                    opts.VisibleEnd = 0;
                    break;
                case Enums.MaskingMode.ShowFirstTwo:
                    opts.VisibleStart = 2;
                    opts.VisibleEnd = 0;
                    break;
                case Enums.MaskingMode.ShowFirstThree:
                    opts.VisibleStart = 3;
                    opts.VisibleEnd = 0;
                    break;
                case Enums.MaskingMode.ShowLastTwo:
                    opts.VisibleStart = 0;
                    opts.VisibleEnd = 2;
                    break;
                case Enums.MaskingMode.ShowLastThree:
                    opts.VisibleStart = 0;
                    opts.VisibleEnd = 3;
                    break;
                case Enums.MaskingMode.ShowLastFour:
                    opts.VisibleStart = 0;
                    opts.VisibleEnd = 4;
                    break;
            }
        }
        
        /// <summary>
        /// Formatlı gösterim ile maskeler (TR33 **** **** **** 1234 gibi).
        /// Not: Tam implementasyon için boşluk ekleme mantığı gerekir.
        /// </summary>
        public static string MaskFormatted(string? iban)
        {
             // Basit implementasyon
             if (string.IsNullOrEmpty(iban)) return string.Empty;
             if (iban.Length < 26) return Mask(iban);

             return iban.Substring(0, 4) + " **** **** **** **** **** " + iban.Substring(22);
        }
    }
}
