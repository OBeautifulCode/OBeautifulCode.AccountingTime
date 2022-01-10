// --------------------------------------------------------------------------------------------------------------------
// <copyright file="YearNumberExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    /// <summary>
    /// Extension methods on integer year number.
    /// </summary>
    public static class YearNumberExtensions
    {
        /// <summary>
        /// Constructs a calendar year from a year number.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns>
        /// A calendar year constructed from the specified year number.
        /// </returns>
        public static CalendarYear ToCalendar(
            this int year)
        {
            var result = new CalendarYear(year);

            return result;
        }

        /// <summary>
        /// Constructs a fiscal year from year number.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns>
        /// A fiscal year constructed from the specified year number.
        /// </returns>
        public static FiscalYear ToFiscal(
            this int year)
        {
            var result = new FiscalYear(year);

            return result;
        }

        /// <summary>
        /// Constructs a generic year from a year number.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns>
        /// A generic year constructed from the specified year number.
        /// </returns>
        public static GenericYear ToGeneric(
            this int year)
        {
            var result = new GenericYear(year);

            return result;
        }
    }
}