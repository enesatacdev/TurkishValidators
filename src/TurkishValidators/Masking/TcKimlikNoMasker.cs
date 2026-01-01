using System;
using TurkishValidators.Options;

namespace TurkishValidators.Masking
{
    /// <summary>
    /// TC Kimlik Numarası maskeleme işlemlerini sağlar.
    /// </summary>
    public static class TcKimlikNoMasker
    {
        /// <summary>
        /// TC Kimlik Numarasını maskeler.
        /// </summary>
        /// <param name="tcNo">Maskelenecek TC No.</param>
        /// <param name="options">Maskeleme seçenekleri.</param>
        public static string Mask(string? tcNo, MaskingOptions? options = null)
        {
            if (string.IsNullOrEmpty(tcNo)) return string.Empty;

            var opts = options ?? new MaskingOptions { VisibleStart = 3, VisibleEnd = 2 };

            // Apply Mode if not Custom
            if (opts.Mode != Enums.MaskingMode.Custom)
            {
                ApplyModeSettings(opts, tcNo.Length);
            }
            
            if (tcNo.Length <= opts.VisibleStart + opts.VisibleEnd)
            {
                return tcNo; 
            }

            string start = tcNo.Substring(0, opts.VisibleStart);
            string end = tcNo.Substring(tcNo.Length - opts.VisibleEnd);
            string middle = new string(opts.MaskChar, tcNo.Length - (opts.VisibleStart + opts.VisibleEnd));

            return start + middle + end;
        }

        /// <summary>
        /// TC Kimlik Numarasını belirli bir mod ile maskeler.
        /// </summary>
        public static string Mask(string? tcNo, Enums.MaskingMode mode)
        {
            return Mask(tcNo, new MaskingOptions { Mode = mode });
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
        /// TC Kimlik Numarasını tamamen maskeler.
        /// </summary>
        public static string MaskFully(string? tcNo)
        {
            if (string.IsNullOrEmpty(tcNo)) return string.Empty;
            return new string('*', tcNo.Length);
        }
    }
}
