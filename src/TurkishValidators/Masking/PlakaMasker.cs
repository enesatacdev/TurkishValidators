using System;
using TurkishValidators.Options;

namespace TurkishValidators.Masking
{
    /// <summary>
    /// Araç plakası maskeleme işlemlerini sağlar.
    /// </summary>
    public static class PlakaMasker
    {
        public static string Mask(string? plate, MaskingOptions? options = null)
        {
            if (string.IsNullOrEmpty(plate)) return string.Empty;

            // Varsayılan: İl kodu (ilk 2) görünür, kalanı maskelenir
            var opts = options ?? new MaskingOptions { VisibleStart = 2, VisibleEnd = 0 };

            if (opts.Mode != Enums.MaskingMode.Custom)
            {
                ApplyModeSettings(opts, plate.Length);
            }
            
            if (plate.Length <= opts.VisibleStart + opts.VisibleEnd) return plate;

            string start = plate.Substring(0, opts.VisibleStart);
            string end = plate.Substring(plate.Length - opts.VisibleEnd);
            string middle = new string(opts.MaskChar, plate.Length - (opts.VisibleStart + opts.VisibleEnd));

            return start + middle + end;
        }

        public static string Mask(string? plate, Enums.MaskingMode mode)
        {
            return Mask(plate, new MaskingOptions { Mode = mode });
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
    }
}
