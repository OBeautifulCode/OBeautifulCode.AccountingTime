// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountingPeriodSystemHelpers.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    /// <summary>
    /// Heplper methods for objects of type <see cref="AccountingPeriodSystem"/>.
    /// </summary>
    internal static class AccountingPeriodSystemHelpers
    {
        /// <summary>
        /// Finds a specified day-of-week after a reference date.
        /// </summary>
        /// <param name="value">The reference date.</param>
        /// <param name="dayOfWeek">The next day-of-week to find.</param>
        /// <returns>
        /// Returns the specified day-of-week that falls after the reference date.
        /// </returns>
        public static DateTime Next(
            this DateTime value,
            DayOfWeek dayOfWeek)
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
        public static DateTime Previous(
            this DateTime value,
            DayOfWeek dayOfWeek)
        {
            int daysToAdd = (int)dayOfWeek - (int)value.DayOfWeek;
            if (value.DayOfWeek <= dayOfWeek)
            {
                daysToAdd -= 7;
            }

            return value.AddDays(daysToAdd);
        }
    }
}
