// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FiftyTwoFiftyThreeWeekFiscalYearNumberingConvention.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    /// <summary>
    /// Determines which calendar year is used to number a fiscal year in a <see cref="FiftyTwoFiftyThreeWeekFiscalYearScheme"/>.
    /// </summary>
    /// <remarks>
    /// The number is determined by the anchor month, not by the actual first or last day of the fiscal year.
    /// A fiscal year can end a few days after its anchor month (e.g. on Feb 1 for a January anchor month),
    /// and for a December anchor month, that can be in the next calendar year; the fiscal year number is unaffected.
    /// </remarks>
    [Serializable]
    public enum FiftyTwoFiftyThreeWeekFiscalYearNumberingConvention
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// The fiscal year is numbered by the calendar year of the anchor month that the fiscal year ends in or near.
        /// For example, with a fiscal year that ends on the Saturday closest to the end of January,
        /// the fiscal year that runs from Feb 4, 2024 through Feb 1, 2025 is FY 2025 (Best Buy uses this convention).
        /// This is the most common convention.
        /// </summary>
        CalendarYearOfAnchorMonth,

        /// <summary>
        /// The fiscal year is numbered by the calendar year prior to that of the anchor month that the fiscal year ends in or near.
        /// For example, with a fiscal year that ends on the Saturday closest to the end of January,
        /// the fiscal year that runs from Feb 4, 2024 through Feb 1, 2025 is FY 2024 (Target uses this convention).
        /// This convention is common among retailers whose fiscal year ends in or near January.
        /// It cannot be used when the anchor month is December, because the fiscal year falls (almost) entirely in the anchor month's calendar year.
        /// </summary>
        CalendarYearPriorToAnchorMonth,
    }
}
