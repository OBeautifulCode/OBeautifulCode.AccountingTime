// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;
    using static System.FormattableString;

    /// <summary>
    /// Extension methods on <see cref="DateTime"/>.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Converts a <see cref="DateTime"/> into a <see cref="CalendarDay"/>.
        /// </summary>
        /// <param name="value">The date/time to convert.</param>
        /// <returns>
        /// A <see cref="CalendarDay"/> converted from a <see cref="DateTime"/>.
        /// </returns>
        public static CalendarDay ToCalendarDay(
            this DateTime value)
        {
            var result = new CalendarDay(value.Year, (MonthOfYear)value.Month, (DayOfMonth)value.Day);

            return result;
        }

        /// <summary>
        /// Finds a specified <see cref="CalendarUnitOfTime"/> prior to a reference date.
        /// </summary>
        /// <param name="value">The reference date.</param>
        /// <param name="granularity">The granularity of the previous period.</param>
        /// <returns>
        /// Returns the <see cref="CalendarUnitOfTime"/> in the specified <paramref name="granularity"/>
        /// that falls prior to the reference date.
        /// </returns>
        /// <exception cref="ArgumentException"><paramref name="granularity"/> is <see cref="UnitOfTimeGranularity.Unbounded"/>.</exception>
        public static CalendarUnitOfTime Previous(
            this DateTime value,
            UnitOfTimeGranularity granularity)
        {
            if (granularity == UnitOfTimeGranularity.Unbounded)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(granularity)}' == '{UnitOfTimeGranularity.Unbounded}'"), (Exception)null);
            }

            CalendarUnitOfTime result;

            if (granularity == UnitOfTimeGranularity.Year)
            {
                result = new CalendarYear(value.Year - 1);
            }
            else if (granularity == UnitOfTimeGranularity.Quarter)
            {
                result = value.Month <= 3
                    ? new CalendarQuarter(value.Year - 1, QuarterNumber.Q4)
                    : new CalendarQuarter(value.Year, (QuarterNumber)((value.Month - 1) / 3));
            }
            else if (granularity == UnitOfTimeGranularity.Month)
            {
                result = value.Month == 1
                    ? new CalendarMonth(value.Year - 1, MonthOfYear.December)
                    : new CalendarMonth(value.Year, (MonthOfYear)(value.Month - 1));
            }
            else if (granularity == UnitOfTimeGranularity.Day)
            {
                result = (CalendarDay)value.Date.ToCalendarDay().Plus(-1);
            }
            else
            {
                throw new NotSupportedException(Invariant($"This granularity is not supported: {granularity}."));
            }

            return result;
        }
    }
}
