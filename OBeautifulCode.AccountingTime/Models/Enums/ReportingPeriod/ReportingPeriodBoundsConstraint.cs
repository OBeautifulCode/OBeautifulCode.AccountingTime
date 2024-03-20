// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriodBoundsConstraint.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    /// <summary>
    /// A constraint on the bounds of a <see cref="ReportingPeriod"/>.
    /// </summary>
    public enum ReportingPeriodBoundsConstraint
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// No constraints.
        /// </summary>
        None,

        /// <summary>
        /// The start and the end of the reporting period must be bounded.
        /// </summary>
        MustBeFullyBounded,

        /// <summary>
        /// The start and the end of the reporting period must be unbounded.
        /// </summary>
        MustBeFullyUnbounded,

        /// <summary>
        /// The start or the end of the reporting period must be bounded, with no constraint on the other component.
        /// </summary>
        MustHaveBoundedComponent,

        /// <summary>
        /// The start or the end of the reporting period must be unbounded, with no constraint on the other component.
        /// </summary>
        MustHaveUnboundedComponent,

        /// <summary>
        /// The start of the reporting period must be bounded, with no constraint on the end.
        /// </summary>
        MustHaveBoundedStart,

        /// <summary>
        /// The start of the reporting period must be unbounded, with no constraint on the end.
        /// </summary>
        MustHaveUnboundedStart,

        /// <summary>
        /// The end of the reporting period must be bounded, with no constraint on the start.
        /// </summary>
        MustHaveBoundedEnd,

        /// <summary>
        /// The end of the reporting period must be unbounded, with no constraint on the start.
        /// </summary>
        MustHaveUnboundedEnd,

        /// <summary>
        /// The start of the reporting period must be bounded and the end must be unbounded.
        /// </summary>
        MustHaveBoundedStartAndUnboundedEnd,

        /// <summary>
        /// The start of the reporting period must be unbounded and the end must be bounded.
        /// </summary>
        MustHaveUnboundedStartAndBoundedEnd,
    }
}