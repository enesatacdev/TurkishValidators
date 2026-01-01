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
            var opts = options ?? new MaskingOptions { VisibleStart = 3, VisibleEnd = 2 };

            if (opts.Mode != Enums.MaskingMode.Custom)
            {
                ApplyModeSettings(opts, phone.Length);
            }
            
            if (phone.Length <= opts.VisibleStart + opts.VisibleEnd) return phone;
            
            string start = phone.Substring(0, opts.VisibleStart);
            string end = phone.Substring(phone.Length - opts.VisibleEnd);
            string middle = new string(opts.MaskChar, phone.Length - (opts.VisibleStart + opts.VisibleEnd));

            return start + middle + end;
        }

        public static string Mask(string? phone, Enums.MaskingMode mode)
        {
            return Mask(phone, new MaskingOptions { Mode = mode });
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

        public static string MaskFully(string? phone)
        {
            if (string.IsNullOrEmpty(phone)) return string.Empty;
            return new string('*', phone.Length);
        }
    }
}
