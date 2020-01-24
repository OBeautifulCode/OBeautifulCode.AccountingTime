// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriod.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// Represents a range of time over which to report, inclusive of the endpoints.
    /// </summary>
    [Serializable]
    public class ReportingPeriod : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReportingPeriod"/> class.
        /// </summary>
        /// <param name="start">The start of the reporting period.</param>
        /// <param name="end">The end of the reporting period.</param>
        /// <exception cref="ArgumentNullException"><paramref name="start"/> or <paramref name="end"/> are null.</exception>
        /// <exception cref="ArgumentException"><paramref name="start"/> and <paramref name="end"/> are bounded and are different concrete types of units-of-time.</exception>
        /// <exception cref="ArgumentException"><paramref name="start"/> and/or <paramref name="end"/> is unbounded and are different kinds of units-of-time.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="start"/> is greater than <paramref name="end"/>.</exception>
        public ReportingPeriod(
            UnitOfTime start,
            UnitOfTime end)
        {
            new { start }.AsArg().Must().NotBeNull();
            new { end }.AsArg().Must().NotBeNull();

            if ((start is IAmUnboundedTime) || (end is IAmUnboundedTime))
            {
                if (start.UnitOfTimeKind != end.UnitOfTimeKind)
                {
                    throw new ArgumentException(Invariant($"{nameof(start)} and/or {nameof(end)} is unbounded and are different kinds of units-of-time."));
                }
            }
            else
            {
                if (start.GetType() != end.GetType())
                {
                    throw new ArgumentException(Invariant($"{nameof(start)} and {nameof(end)} are bounded and are different concrete types of units-of-time."));
                }

                if (start.CompareTo(end) == 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(start), Invariant($"{nameof(start)} is greater than {nameof(end)}."));
                }
            }

            this.Start = start;
            this.End = end;
        }

        /// <summary>
        ///  Gets the start of the reporting period.
        /// </summary>
        public UnitOfTime Start { get; private set; }

        /// <summary>
        ///  Gets the end of the reporting period.
        /// </summary>
        public UnitOfTime End { get; private set; }
    }
}
