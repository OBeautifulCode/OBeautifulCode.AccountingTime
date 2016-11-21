// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime
{
    using System;

    /// <summary>
    /// Container for extension methods on object <see cref="DateTime"/>.
    /// </summary>
    internal static class DateTimeExtensions
    {
        /// <summary>
        /// Finds a specified day-of-week after a reference date.
        /// </summary>
        /// <param name="value">The reference date.</param>
        /// <param name="dayOfWeek">The next day-of-week to find.</param>
        /// <returns>
        /// Returns the specified day-of-week that falls after the reference date.
        /// </returns>
        public static DateTime Next(this DateTime value, DayOfWeek dayOfWeek)
        {
            int daysToAdd = (int)dayOfWeek - (int)value.DayOfWeek;
            if (value.DayOfWeek >= dayOfWeek)
            {
                daysToAdd += 7;
            }

            return value.AddDays(daysToAdd);
        }

        /// <summary>
        /// Finds a specified day-of-week prior to a reference date.
        /// </summary>
        /// <param name="value">The reference date.</param>
        /// <param name="dayOfWeek">The previous day-of-week to find.</param>
        /// <returns>
        /// Returns the specified day-of-week that falls prior to the reference date.
        /// </returns>
        public static DateTime Previous(this DateTime value, DayOfWeek dayOfWeek)
        {
            int daysToAdd = (int)dayOfWeek - (int)value.DayOfWeek;
            if (value.DayOfWeek <= dayOfWeek)
            {
                daysToAdd -= 7;
            }

            return value.AddDays(daysToAdd);
        }

        /// <summary>
        /// Converts a <see cref="DateTime"/> into a <see cref="CalendarDay"/>.
        /// </summary>
        /// <param name="value">The date/time to convert.</param>
        /// <returns>
        /// A <see cref="CalendarDay"/> converted from a <see cref="DateTime"/>.
        /// </returns>
        public static CalendarDay ToCalendarDay(this DateTime value)
        {
            var result = new CalendarDay(value.Year, (MonthOfYear)value.Month, (DayOfMonth)value.Day);
            return result;
        }
    }
}

// ReSharper restore CheckNamespace