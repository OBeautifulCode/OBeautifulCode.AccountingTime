// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FixedDayFiscalYearNumberingConvention.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    /// <summary>
    /// Determines which calendar year is used to number a fiscal year in a <see cref="FixedDayFiscalYearScheme"/>,
    /// given that the fiscal year always spans two calendar years.
    /// </summary>
    [Serializable]
    public enum FixedDayFiscalYearNumberingConvention
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// The fiscal year is numbered by the calendar year in which it ends.
        /// For example, with a fiscal year that ends on April 25th, the fiscal year that runs from Apr 26, 2025 through Apr 25, 2026 is FY 2026.
        /// This is the most common convention.
        /// </summary>
        CalendarYearInWhichFiscalYearEnds,

        /// <summary>
        /// The fiscal year is numbered by the calendar year in which it begins.
        /// For example, with a fiscal year that ends on January 25th, the fiscal year that runs from Jan 26, 2026 through Jan 25, 2027 is FY 2026.
        /// This convention is common when the fiscal year ends early in the calendar year, such that most of the fiscal year falls in the calendar year in which it begins.
        /// </summary>
        CalendarYearInWhichFiscalYearBegins,
    }
}
