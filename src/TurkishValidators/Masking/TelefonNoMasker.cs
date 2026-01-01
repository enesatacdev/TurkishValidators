using System;
using TurkishValidators.Options;

namespace TurkishValidators.Masking
{
    /// <summary>
    /// Telefon numarası maskeleme işlemlerini sağlar.
    /// </summary>
    public static class TelefonNoMasker
    {
        public static string Mask(string? phone, MaskingOptions? options = null)
        {
            if (string.IsNullOrEmpty(phone)) return string.Empty;

            // Varsayılan: İlk 3 (alan kodu/prefix) ve son 2 hane görünür
            // Örn: 053*******67
            var opts = options ?? new MaskingOptions { VisibleStart = 3, VisibleEnd = 2 };
            
            if (phone.Length <= opts.VisibleStart + opts.VisibleEnd) return phone;
            
            string start = phone.Substring(0, opts.VisibleStart);
            string end = phone.Substring(phone.Length - opts.VisibleEnd);
            string middle = new string(opts.MaskChar, phone.Length - (opts.VisibleStart + opts.VisibleEnd));

            return start + middle + end;
        }

        public static string MaskFully(string? phone)
        {
            if (string.IsNullOrEmpty(phone)) return string.Empty;
            return new string('*', phone.Length);
        }
    }
}
