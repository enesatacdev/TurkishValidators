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
            
            if (iban.Length <= opts.VisibleStart + opts.VisibleEnd) return iban;

            string start = iban.Substring(0, opts.VisibleStart);
            string end = iban.Substring(iban.Length - opts.VisibleEnd);
            string middle = new string(opts.MaskChar, iban.Length - (opts.VisibleStart + opts.VisibleEnd));

            return start + middle + end;
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
