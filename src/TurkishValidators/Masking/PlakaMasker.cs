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
            // 34 *******
            var opts = options ?? new MaskingOptions { VisibleStart = 2, VisibleEnd = 0 };
            
            // Eğer plaka boşluklu ise boşlukları korumak daha şık olabilir ama
            // basit maskeleme için düz mantık gidiyoruz.
            // Aradaki boşlukları da mask char ile değiştiriyoruz bu mantıkla.
            
            if (plate.Length <= opts.VisibleStart + opts.VisibleEnd) return plate;

            string start = plate.Substring(0, opts.VisibleStart);
            string end = plate.Substring(plate.Length - opts.VisibleEnd);
            string middle = new string(opts.MaskChar, plate.Length - (opts.VisibleStart + opts.VisibleEnd));

            return start + middle + end;
        }
    }
}
