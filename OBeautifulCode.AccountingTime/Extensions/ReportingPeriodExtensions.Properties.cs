// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriodExtensions.Properties.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;
    using System.Collections.Generic;

    using static System.FormattableString;

    /// <summary>
    /// Property-related extension methods on <see cref="IReportingPeriod{T}"/>.
    /// </summary>
    public static partial class ReportingPeriodExtensions
    {
        /// <summary>
        /// Gets the granularity of the unit-of-time used in a reporting period.
        /// </summary>
        /// <param name="reportingPeriod">The reporting period.</param>
        /// <returns>
        /// The granularity of the unit-of-time used in the specified reporting period.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="reportingPeriod"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="reportingPeriod"/> <see cref="ReportingPeriod{T}.Start"/> and <see cref="ReportingPeriod{T}.End"/> has different granularity.</exception>
        public static UnitOfTimeGranularity GetUnitOfTimeGranularity(
            this IReportingPeriod<UnitOfTime> reportingPeriod)
        {
            if (reportingPeriod == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod));
            }

            if (reportingPeriod.Start.UnitOfTimeGranularity != reportingPeriod.End.UnitOfTimeGranularity)
            {
                throw new ArgumentException(Invariant($"{nameof(reportingPeriod)} Start and End has different granularity."));
            }

            var result = reportingPeriod.Start.UnitOfTimeGranularity;
            return result;
        }

        /// <summary>
        /// Gets the kind of the unit-of-time used in a reporting period.
        /// </summary>
        /// <param name="reportingPeriod">The reporting period.</param>
        /// <returns>
        /// The kind of the unit-of-time used in the specified reporting period.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="reportingPeriod"/> is null.</exception>
        public static UnitOfTimeKind GetUnitOfTimeKind(
            this IReportingPeriod<UnitOfTime> reportingPeriod)
        {
            if (reportingPeriod == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod));
            }

            var result = reportingPeriod.Start.UnitOfTimeKind;
            return result;
        }

        /// <summary>
        /// Gets the distinct <typeparamref name="T"/> contained within a specified reporting period.
        /// For example, a reporting period of 2Q2017-4Q2017, contains 2Q2017, 3Q2017, and 4Q2017.
        /// </summary>
        /// <remarks>
        /// The endpoints are considered units within the reporting period.
        /// </remarks>
        /// <typeparam name="T">The unit-of-time of the reporting period.</typeparam>
        /// <param name="reportingPeriod">The reporting period.</param>
        /// <returns>
        /// The units-of-time contained within the specified reporting period.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="reportingPeriod"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="reportingPeriod"/> <see cref="IReportingPeriod{T}.Start"/> and/or <see cref="IReportingPeriod{T}.End"/> is unbounded.</exception>
        public static IList<T> GetUnitsWithin<T>(
            this IReportingPeriod<T> reportingPeriod)
            where T : UnitOfTime
        {
            if (reportingPeriod == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod));
            }

            if (reportingPeriod.HasComponentWithUnboundedGranularity())
            {
                throw new ArgumentException(Invariant($"{nameof(reportingPeriod)} {nameof(reportingPeriod.Start)} and/or {nameof(reportingPeriod.End)} is unbounded"));
            }

            var allUnits = new List<T>();
            var currentUnit = reportingPeriod.Start;
            do
            {
                allUnits.Add(currentUnit);
                currentUnit = (T)currentUnit.Plus(1);
            }
            while (currentUnit.CompareTo(reportingPeriod.End) <= 0);

            return allUnits;
        }

        /// <summary>
        /// Determines if the reporting period has a component with unbounded granularity.
        /// </summary>
        /// <param name="reportingPeriod">The reporting period.</param>
        /// <returns>
        /// true if one or both components of the reporting period has unbounded granularity; otherwise false.
        /// </returns>
        public static bool HasComponentWithUnboundedGranularity(
            this IReportingPeriod<UnitOfTime> reportingPeriod)
        {
            if (reportingPeriod == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod));
            }

            var result = (reportingPeriod.Start.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded) ||
                         (reportingPeriod.End.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded);
            return result;
        }
    }
}