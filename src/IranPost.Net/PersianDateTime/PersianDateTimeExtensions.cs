using System;

namespace IranPost.Net.PersianDateTime
{
    internal static class PersianDateTimeExtensions
    {
        public static PersianDate ToPersianDateTime(
            this DateTime dateTime
        ) => new PersianDate(dateTime);

        public static PersianDate ToPersianDateTime(
            this DateTimeOffset dateTime
        ) => new PersianDate(dateTime);

        public static DateTime ToGregorianDateTime(
            this PersianDate dateTime
        ) => PersianDateConverter.ToGregorianDateTime(dateTime);

        #region Short

        #region Short DateTime

        public static string ToPersianShortDateTimeString(
            this DateTime dateTime
        )
            => dateTime.ToPersianDateTime().ToString("g");

        public static string ToPersianShortDateTimeString(
            this DateTimeOffset dateTime
        )
            => dateTime.ToPersianDateTime().ToString("g");

        public static string ToShortDateTimeString(
            this PersianDate dateTime
        )
            => dateTime.ToString("g");

        #endregion

        #region Short Date

        public static string ToPersianShortDateString(
            this DateTime dateTime
        )
            => dateTime.ToPersianDateTime().ToString("d");

        public static string ToPersianShortDateString(
            this DateTimeOffset dateTime
        )
            => dateTime.ToPersianDateTime().ToString("d");

        public static string ToShortDateString(
            this PersianDate dateTime
        )
            => dateTime.ToString("d");

        #endregion

        #endregion

        #region Long

        #region Long DateTime

        public static string ToPersianLongDateTimeString(
            this DateTime dateTime
        )
            => dateTime.ToPersianDateTime().ToString("f");

        public static string ToPersianLongDateTimeString(
            this DateTimeOffset dateTime
        )
            => dateTime.ToPersianDateTime().ToString("f");

        public static string ToLongDateTimeString(
            this PersianDate dateTime
        )
            => dateTime.ToString("f");

        #endregion

        #region Long Date

        public static string ToPersianLongDateString(
            this DateTime dateTime
        )
            => dateTime.ToPersianDateTime().ToString("D");

        public static string ToPersianLongDateString(
            this DateTimeOffset dateTime
        )
            => dateTime.ToPersianDateTime().ToString("D");

        public static string ToLongDateString(
            this PersianDate dateTime
        )
            => dateTime.ToString("D");

        #endregion

        #endregion
    }
}