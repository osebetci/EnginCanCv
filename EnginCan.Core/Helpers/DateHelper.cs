using System;

namespace EnginCan.Core.Helpers
{
    public static class DateHelper
    {
        public static int MonthDifference(this DateTime lValue, DateTime rValue)
        {
            return (lValue.Month - rValue.Month) + 12 * (lValue.Year - rValue.Year);
        }

        public static string DateIntegerToString(int month)
        {
            string monthName = "";
            switch (month)
            {
                case 1:
                    monthName = "Ocak";
                    break;

                case 2:
                    monthName = "Şubat";
                    break;

                case 3:
                    monthName = "Mart";
                    break;

                case 4:
                    monthName = "Nisan";
                    break;

                case 5:
                    monthName = "Mayıs";
                    break;

                case 6:
                    monthName = "Haziran";
                    break;

                case 7:
                    monthName = "Temmuz";
                    break;

                case 8:
                    monthName = "Ağustos";
                    break;

                case 9:
                    monthName = "Eylül";
                    break;

                case 10:
                    monthName = "Ekim";
                    break;

                case 11:
                    monthName = "Kasım";
                    break;

                case 12:
                    monthName = "Aralık";
                    break;

                default:
                    break;
            }
            return monthName;
        }

        /// <summary>
        /// Parametre olarak verilen günle eşleşen ilk geçmiş tarihi döndürür
        /// </summary>
        /// <param name="dt">Mevcut tarih</param>
        /// <param name="previousDay">Aranılacak gün</param>
        /// <returns></returns>
        public static DateTime PreviousDay(this DateTime dt, DayOfWeek previousDay)
        {
            int diff = (7 + (dt.DayOfWeek - previousDay)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }

        /// <summary>
        /// Parametre olarak verilen günle eşleşen ilk gelecek tarihi döndürür
        /// </summary>
        /// <param name="dt">Mevcut tarih</param>
        /// <param name="nextDay">Aranılacak gün</param>
        /// <returns></returns>
        public static DateTime NextDay(this DateTime dt, DayOfWeek nextDay)
        {
            int diff = (7 - (dt.DayOfWeek - nextDay)) % 7;
            return dt.AddDays(1 * diff).Date;
        }

        /// <summary>
        /// Ayın ilk gününü döndürür
        /// </summary>
        /// <param name="currentDate">Mevcut tarih</param>
        /// <returns></returns>
        public static DateTime GetFirstDateOfMonth(this DateTime currentDate)
        {
            return currentDate.AddDays((-1) * currentDate.Day + 1);
        }

        /// <summary>
        /// Ayın son gününü döndürür
        /// </summary>
        /// <param name="currentDate"></param>
        /// <returns></returns>
        public static DateTime GetLastDateOfMonth(this DateTime currentDate)
        {
            return currentDate.AddMonths(1).GetFirstDateOfMonth().AddDays(-1);
        }

        /// <summary>
        /// İki tarih aralığındaki günleri döndürür.
        /// </summary>
        /// <param name="fromDate">Başlangıç tarihi</param>
        /// <param name="toDate">Bitiş tarihi</param>
        /// <returns></returns>
        public static DateTime[] GetDatesArray(this DateTime fromDate, DateTime toDate)
        {
            int days = (toDate - fromDate).Days;
            var dates = new DateTime[days];

            for (int i = 0; i < days; i++)
            {
                dates[i] = fromDate.AddDays(i);
            }

            return dates;
        }

        /// <summary>
        /// Günün Türkçe ismini döndürür
        /// </summary>
        /// <returns></returns>
        public static DayOfWeek GetTurkishName(this DayOfWeek day, out string dayName)
        {
            dayName = "";
            switch (day)
            {
                case DayOfWeek.Friday:
                    dayName = "Cuma";
                    break;

                case DayOfWeek.Monday:
                    dayName = "Pazartesi";
                    break;

                case DayOfWeek.Saturday:
                    dayName = "Cumartesi";
                    break;

                case DayOfWeek.Sunday:
                    dayName = "Pazar";
                    break;

                case DayOfWeek.Thursday:
                    dayName = "Perşembe";
                    break;

                case DayOfWeek.Tuesday:
                    dayName = "Salı";
                    break;

                case DayOfWeek.Wednesday:
                    dayName = "Çarşamba";
                    break;
            }
            return day;
        }
    }
}