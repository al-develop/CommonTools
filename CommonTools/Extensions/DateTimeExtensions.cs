using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommonTools.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime FirstDayOfWeek(this DateTime dateTime)
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            DayOfWeek firstDayOfWeek = currentCulture.DateTimeFormat.FirstDayOfWeek;
            int offset = dateTime.DayOfWeek - firstDayOfWeek < 0 ? 7 : 0;
            int numberOfDaysSinceBeginningOfTheWeek = dateTime.DayOfWeek + offset - firstDayOfWeek;

            return dateTime.AddDays(-numberOfDaysSinceBeginningOfTheWeek);
        }

        public static DateTime LastDayOfWeek(this DateTime dateTime) 
            => dateTime.FirstDayOfWeek().AddDays(6);

        public static DateTime FirstDayOfMonth(this DateTime dateTime) 
            => dateTime.SetDay(1);

        public static DateTime LastDayOfMonth(this DateTime dateTime) 
            => dateTime.AddMonths(1).FirstDayOfMonth().AddDays(-1);

        public static DateTime Next(this DateTime start, DayOfWeek day)
        {
            do
            {
                start = start + TimeSpan.FromDays(1);
            }
            while (start.DayOfWeek != day);

            return start;
        }

        public static DateTime SelfOrNext(this DateTime dateTime, DayOfWeek day, int number)
        {
            DateTime nextDate = dateTime.DayOfWeek == day ? dateTime : dateTime.Next(day);

            for (int i = 1; i < number; i++)
            {
                nextDate = nextDate.Next(day);
            }

            return nextDate;
        }

        public static int CountOfDaysInMonth(this DateTime dateTime, DayOfWeek day)
        {
            int dayCount = 0;

            DateTime startOfMonth = dateTime.FirstDayOfMonth();
            while (startOfMonth.Month == dateTime.Month)
            {
                if (startOfMonth.DayOfWeek == DayOfWeek.Friday)
                    dayCount++;

                startOfMonth = startOfMonth.AddDays(1);
            }

            return dayCount;
        }

        public static DateTime SetDay(this DateTime dateTime, int day)
        {
            day = Math.Min(DateTime.DaysInMonth(dateTime.Year, dateTime.Month), day);
            return new DateTime(dateTime.Year, dateTime.Month, day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond, dateTime.Kind);
        }

        public static DateTime StartOfDay(this DateTime dateTime)
            => dateTime.Date;

        public static DateTime EndOfDay(this DateTime dateTime) 
            => dateTime.Date.AddDays(1).AddSeconds(-1);

        public static DateTime AddBusinessDays(this DateTime date, int days)
        {
            if (days < 0)
            {
                throw new ArgumentException("days cannot be negative", nameof(days));
            }

            if (days == 0) return date;

            if (date.DayOfWeek == DayOfWeek.Saturday)
            {
                date = date.AddDays(2);
                days -= 1;
            }
            else if (date.DayOfWeek == DayOfWeek.Sunday)
            {
                date = date.AddDays(1);
                days -= 1;
            }

            date = date.AddDays(days / 5 * 7);
            int extraDays = days % 5;

            if ((int)date.DayOfWeek + extraDays > 5)
            {
                extraDays += 2;
            }

            return date.AddDays(extraDays);
        }
    }
}
