// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriodSpanConstraint.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    /// <summary>
    /// A constraint on the span of a reporting period.
    /// </summary>
    public enum ReportingPeriodSpanConstraint
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// No constraint.
        /// </summary>
        None,

        /// <summary>
        /// The start must equal the end.
        /// </summary>
        MustHaveSameStartAndEnd,

        /// <summary>
        /// The start cannot equal the end.
        /// </summary>
        MustHaveDifferentStartAndEnd,
    }
}