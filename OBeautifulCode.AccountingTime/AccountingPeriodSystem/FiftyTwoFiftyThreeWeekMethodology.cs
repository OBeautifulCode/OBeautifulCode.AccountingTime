// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FiftyTwoFiftyThreeWeekMethodology.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime
{
    /// <summary>
    /// Determines the methodology used to identify the last day of the accounting year, in a 52-53 week accounting period system.
    /// </summary>
    public enum FiftyTwoFiftyThreeWeekMethodology
    {
        /// <summary>
        /// Accounting year ends on whatever date the chosen day of the week last occurs in the anchor month.
        /// </summary>
        LastOccuranceInAnchorMonth,

        /// <summary>
        /// Accounting year ends on the chosen day of week that is nearest to the last day of the anchor month.
        /// </summary>
        ClosestToLastDayOfAnchorMonth
    }
}

// ReSharper restore CheckNamespace