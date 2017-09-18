// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeExtensions.cs" company="OBeautifulCode">
//    Copyright (c) OBeautifulCode 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

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
    }
}
