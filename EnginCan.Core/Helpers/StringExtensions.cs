using System.Collections.Generic;
using System.Linq;

namespace EnginCan.Core.Helpers
{
    public static class StringExtensions
    {
        public static string ToTrUpper(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return "";

            return value.Trim().Replace(" ", "")
                                .Replace("ç", "Ç")
                                .Replace("ğ", "Ğ")
                                .Replace("ı", "I")
                                .Replace("i", "İ")
                                .Replace("ö", "Ö")
                                .Replace("ş", "Ş")
                                .Replace("ü", "Ü").ToUpperInvariant();
        }

        public static string ToTrLower(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return "";

            return value.Trim().Replace(" ", "")
                                .Replace("Ç", "ç")
                                .Replace("Ğ", "ğ")
                                .Replace("I", "ı")
                                .Replace("İ", "i")
                                .Replace("Ö", "ö")
                                .Replace("Ş", "ş")
                                .Replace("Ü", "ü").ToLowerInvariant();
        }

        public static string ToTrCapitalize(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return "";

            var list = new List<string>();
            list.AddRange(value.Select(c => c.ToString()));

            var returnList = new List<string>();
            var capitalFirst = list[0].Replace(" ", "")
                               .Replace("ç", "Ç")
                               .Replace("ğ", "Ğ")
                               .Replace("ı", "I")
                               .Replace("i", "İ")
                               .Replace("ö", "Ö")
                               .Replace("ş", "Ş")
                               .Replace("ü", "Ü").ToUpperInvariant();
            
            returnList.Add(capitalFirst);

            for (int i = 1; i < list.Count; i++)
            {
                var charac = list[i].Replace(" ", "")
                                .Replace("Ç", "ç")
                                .Replace("Ğ", "ğ")
                                .Replace("I", "ı")
                                .Replace("İ", "i")
                                .Replace("Ö", "ö")
                                .Replace("Ş", "ş")
                                .Replace("Ü", "ü").ToLowerInvariant();

                returnList.Add(charac);
            }

            return string.Join(null, returnList);
        }

        public static string FromTrToEngLower(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return "";

            return value.Trim().Replace(" ", "")
                                .Replace("ç", "c")
                                .Replace("ğ", "g")
                                .Replace("ı", "i")
                                .Replace("i", "i")
                                .Replace("ö", "o")
                                .Replace("ş", "s")
                                .Replace("ü", "u")
                                .Replace("Ç", "c")
                                .Replace("Ğ", "g")
                                .Replace("I", "i")
                                .Replace("İ", "i")
                                .Replace("Ö", "o")
                                .Replace("Ş", "s")
                                .Replace("Ü", "u").ToLowerInvariant();
        }
    }
}