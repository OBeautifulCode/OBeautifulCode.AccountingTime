// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FiftyTwoFiftyThreeWeekYearEndMethodology.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    /// <summary>
    /// Determines the methodology used to identify the last day of the fiscal year in a <see cref="FiftyTwoFiftyThreeWeekFiscalYearScheme"/>.
    /// </summary>
    [Serializable]
    public enum FiftyTwoFiftyThreeWeekYearEndMethodology
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// Accounting year ends on whatever date the chosen day of the week last occurs in the anchor month.
        /// </summary>
        LastOccurrenceInAnchorMonth,

        /// <summary>
        /// Accounting year ends on the chosen day of week that is nearest to the last day of the anchor month.
        /// </summary>
        ClosestToLastDayOfAnchorMonth,
    }
}
