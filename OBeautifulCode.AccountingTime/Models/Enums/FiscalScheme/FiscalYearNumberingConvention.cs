// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FiscalYearNumberingConvention.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using OBeautifulCode.CodeAnalysis.Recipes;

    /// <summary>
    /// Determines which calendar year is used to number a fiscal year that spans two calendar years.
    /// </summary>
    [Serializable]
    public enum FiscalYearNumberingConvention
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// The fiscal year is numbered by the calendar year in which it ends.
        /// For example, a fiscal year that runs from Feb 1, 2026 through Jan 31, 2027 is FY 2027.
        /// This is the most common convention.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "YearEnds", Justification = ObcSuppressBecause.CA1702_CompoundWordsShouldBeCasedCorrectly_AnalyzerIsIncorrectlyDetectingCompoundWords)]
        CalendarYearInWhichFiscalYearEnds,

        /// <summary>
        /// The fiscal year is numbered by the calendar year in which it begins.
        /// For example, a fiscal year that runs from Feb 1, 2026 through Jan 31, 2027 is FY 2026.
        /// This convention is common among retailers whose fiscal year ends in January.
        /// </summary>
        CalendarYearInWhichFiscalYearBegins,
    }
}
