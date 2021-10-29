// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriodComparison.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    /// <summary>
    /// Specifies how to compare two reporting periods.
    /// </summary>
    public enum ReportingPeriodComparison
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// The reporting periods must be equal, ignoring granularity
        /// (e.g. Calendar 1Q2020 == January 2020 thru March 2020).
        /// </summary>
        IsEqualToIgnoringGranularity,

        /// <summary>
        /// The subject reporting period must contain the comparison reporting period.
        /// </summary>
        Contains,

        /////// <summary>
        /////// The subject reporting period must be contained by the comparison reporting period.
        /////// </summary>
        ////IsContainedBy,
        /////// <summary>
        ///////
        /////// </summary>
        ////Overlap,
    }
}
