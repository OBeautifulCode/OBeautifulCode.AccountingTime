﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriodExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime
{
    using System;

    /// <summary>
    /// Extension methods on <see cref="ReportingPeriod{T}"/>.
    /// </summary>
    public static class ReportingPeriodExtensions
    {
        /// <summary>
        /// Determines if a unit-of-time is contained within a reporting period.
        /// For example, 2Q2017 is contained within a reporting period of 1Q2017-4Q2017.
        /// </summary>
        /// <remarks>
        /// If the unit-of-time is equal to one of the endpoints of the reporting period,
        /// that unit-of-time is considered to be within the reporting period.
        /// </remarks>
        /// <typeparam name="T">The type of unit-of-time/reporting period.</typeparam>
        /// <param name="unitOfTime">The unit-of-time to check against a reporting period.</param>
        /// <param name="reportingPeriod">The reporting period.</param>
        /// <returns>
        /// true if the unit-of-time is contained within the reporting period; false otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="unitOfTime"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="reportingPeriod"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="unitOfTime"/> cannot be compared against <paramref name="reportingPeriod"/> because they represent different concrete subclasses of <see cref="UnitOfTime"/>.</exception>
        public static bool IsInReportingPeriod<T>(this T unitOfTime, IReportingPeriodInclusive<T> reportingPeriod)
            where T : UnitOfTime
        {
            if (unitOfTime == null)
            {
                throw new ArgumentNullException(nameof(unitOfTime));
            }

            if (reportingPeriod == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod));
            }

            // note: CompareTo will throw if the unit-of-time is a different concrete
            // sub-class of UnitOfTime than what is stored in the reporting period
            bool result;
            try
            {
                result = (unitOfTime.CompareTo(reportingPeriod.Start) >= 0) &&
                         (unitOfTime.CompareTo(reportingPeriod.End) <= 0);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("unitOfTime cannot be compared against reportingPeriod because they represent different concrete subclasses of UnitOfTime", ex);
            }

            return result;
        }

        /// <summary>
        /// Determines if two objects of type <see cref="IReportingPeriodInclusive{T}"/>, overlap.
        /// For example, the following reporting periods have an overlap: 1Q2017-3Q2017 and 3Q2017-4Q2017.
        /// </summary>
        /// <remarks>
        /// If the endpoint of one reporting period is the same as the endpoint
        /// of the second reporting period, the reporting periods are deemed to overlap.
        /// </remarks>
        /// <typeparam name="T">The unit-of-time of the reporting period.</typeparam>
        /// <param name="reportingPeriod1">A reporting period.</param>
        /// <param name="reportingPeriod2">A second reporting period to check for overlap against the first reporting period.</param>
        /// <returns>
        /// true if there is an overlap between the reporting periods; false otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="reportingPeriod1"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="reportingPeriod2"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="reportingPeriod1"/> cannot be compared against <paramref name="reportingPeriod2"/> because they represent different concrete subclasses of <see cref="UnitOfTime"/>.</exception>
        public static bool HasOverlapWith<T>(this IReportingPeriodInclusive<T> reportingPeriod1, IReportingPeriodInclusive<T> reportingPeriod2)
            where T : UnitOfTime
        {
            if (reportingPeriod1 == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod1));
            }

            if (reportingPeriod2 == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod2));
            }

            bool result;
            try
            {
                result = reportingPeriod2.Start.IsInReportingPeriod(reportingPeriod1) ||
                         reportingPeriod2.End.IsInReportingPeriod(reportingPeriod1) ||
                         reportingPeriod1.Start.IsInReportingPeriod(reportingPeriod2) ||
                         reportingPeriod1.End.IsInReportingPeriod(reportingPeriod2);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("reportingPeriod1 cannot be compared against reportingPeriod2 because they represent different concrete subclasses of UnitOfTime.", ex);
            }

            return result;
        }

        /// <summary>
        /// Gets the number of distinct <typeparamref name="T"/> contained within a specified reporting period.
        /// For example, a reporting period of 2Q2017-4Q2017, contains 3 distinct quarters.
        /// </summary>
        /// <remarks>
        /// The endpoints are considered one unit each, unless they are the same, in which case
        /// there is a total of 1 unit within the reporting period.
        /// </remarks>
        /// <typeparam name="T">The unit-of-time of the reporting period.</typeparam>
        /// <param name="reportingPeriod">The reporting period.</param>
        /// <returns>
        /// The number of units-of-time contained within the specified reporting period.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="reportingPeriod"/> is null.</exception>
        public static int NumberOfUnitsWithin<T>(this IReportingPeriodInclusive<T> reportingPeriod)
            where T : UnitOfTime
        {
            if (reportingPeriod == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod));
            }

            var numberOfUnits = 1;
            var currentUnit = (UnitOfTime)reportingPeriod.Start;
            while (currentUnit.CompareTo(reportingPeriod.End) < 0)
            {
                numberOfUnits++;
                currentUnit = currentUnit.Plus(1);
            }

            return numberOfUnits;
        }
    }
}

// ReSharper restore CheckNamespace