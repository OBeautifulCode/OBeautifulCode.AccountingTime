﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriodExtensions.Comparison.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime
{
    using System;

    using static System.FormattableString;

    /// <summary>
    /// Comparison-related extension methods on <see cref="IReportingPeriod{T}"/>.
    /// </summary>
    public static partial class ReportingPeriodExtensions
    {
        /// <summary>
        /// Determines if a unit-of-time is contained within a reporting period.
        /// For example, 2Q2017 is contained within a reporting period of 1Q2017-4Q2017.
        /// </summary>
        /// <remarks>
        /// If the unit-of-time is equal to one of the endpoints of the reporting period,
        /// that unit-of-time is considered to be within the reporting period.
        /// </remarks>
        /// <param name="reportingPeriod">The reporting period.</param>
        /// <param name="unitOfTime">The unit-of-time to check against a reporting period.</param>
        /// <returns>
        /// true if the unit-of-time is contained within the reporting period; false otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="unitOfTime"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="reportingPeriod"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="unitOfTime"/> cannot be compared against <paramref name="reportingPeriod"/> because they represent different <see cref="UnitOfTime.UnitOfTimeKind"/>.</exception>
        public static bool Contains(this IReportingPeriod<UnitOfTime> reportingPeriod, UnitOfTime unitOfTime)
        {
            if (reportingPeriod == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod));
            }

            if (unitOfTime == null)
            {
                throw new ArgumentNullException(nameof(unitOfTime));
            }

            if (unitOfTime.UnitOfTimeKind != reportingPeriod.GetUnitOfTimeKind())
            {
                throw new ArgumentException(Invariant($"{nameof(unitOfTime)} cannot be compared against {nameof(reportingPeriod)} because they represent different {nameof(UnitOfTimeKind)}"));
            }

            var result = reportingPeriod.Contains(new ReportingPeriod<UnitOfTime>(unitOfTime, unitOfTime));
            return result;
        }

        /// <summary>
        /// Determines if an <see cref="IReportingPeriod{T}"/> is contained within another <see cref="IReportingPeriod{T}"/>
        /// For example, 1Q2017-3Q2017 contains 2Q2017-3Q2017.
        /// </summary>
        /// <remarks>
        /// If an endpoint in the second reporting period equals an endpoint in the first reporting period, that endpoint
        /// is considered to be contained within the first reporting period.  Of course, both endpoints must be contained
        /// within the reporting period for this method to return true.
        /// </remarks>
        /// <param name="reportingPeriod1">A reporting period.</param>
        /// <param name="reportingPeriod2">A second reporting period to check for containment within the first reporting period.</param>
        /// <returns>
        /// true if the first reporting period contains the second one; false if not.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="reportingPeriod1"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="reportingPeriod2"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="reportingPeriod1"/> cannot be compared against <paramref name="reportingPeriod2"/> because they represent different <see cref="UnitOfTime.UnitOfTimeKind"/>.</exception>
        public static bool Contains(this IReportingPeriod<UnitOfTime> reportingPeriod1, IReportingPeriod<UnitOfTime> reportingPeriod2)
        {
            if (reportingPeriod1 == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod1));
            }

            if (reportingPeriod2 == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod2));
            }

            if (reportingPeriod1.GetUnitOfTimeKind() != reportingPeriod2.GetUnitOfTimeKind())
            {
                throw new ArgumentException(Invariant($"{nameof(reportingPeriod1)} cannot be compared against {nameof(reportingPeriod2)} because they represent different {nameof(UnitOfTimeKind)}"));
            }

            reportingPeriod1 = reportingPeriod1.ToMostGranular();
            reportingPeriod2 = reportingPeriod2.ToMostGranular();

            bool startIsContained;
            if (reportingPeriod1.Start.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded)
            {
                startIsContained = true;
            }
            else
            {
                if (reportingPeriod2.Start.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded)
                {
                    startIsContained = false;
                }
                else
                {
                    startIsContained = reportingPeriod1.Start.CompareTo(reportingPeriod2.Start) <= 0;
                }
            }

            bool endIsContained;
            if (reportingPeriod1.End.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded)
            {
                endIsContained = true;
            }
            else
            {
                if (reportingPeriod2.End.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded)
                {
                    endIsContained = false;
                }
                else
                {
                    endIsContained = reportingPeriod1.End.CompareTo(reportingPeriod2.End) >= 0;
                }
            }

            var result = startIsContained && endIsContained;
            return result;
        }

        /// <summary>
        /// Determines if two objects of type <see cref="IReportingPeriod{T}"/>, overlap.
        /// For example, the following reporting periods have an overlap: 1Q2017-3Q2017 and 3Q2017-4Q2017.
        /// </summary>
        /// <remarks>
        /// If the endpoint of one reporting period is the same as the endpoint
        /// of the second reporting period, the reporting periods are deemed to overlap.
        /// </remarks>
        /// <param name="reportingPeriod1">A reporting period.</param>
        /// <param name="reportingPeriod2">A second reporting period to check for overlap against the first reporting period.</param>
        /// <returns>
        /// true if there is an overlap between the reporting periods; false otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="reportingPeriod1"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="reportingPeriod2"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="reportingPeriod1"/> cannot be compared against <paramref name="reportingPeriod2"/> because they represent different <see cref="UnitOfTime.UnitOfTimeKind"/>.</exception>
        public static bool HasOverlapWith(this IReportingPeriod<UnitOfTime> reportingPeriod1, IReportingPeriod<UnitOfTime> reportingPeriod2)
        {
            if (reportingPeriod1 == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod1));
            }

            if (reportingPeriod2 == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod2));
            }

            if (reportingPeriod1.GetUnitOfTimeKind() != reportingPeriod2.GetUnitOfTimeKind())
            {
                throw new ArgumentException(Invariant($"{nameof(reportingPeriod1)} cannot be compared against {nameof(reportingPeriod2)} because they represent different {nameof(UnitOfTimeKind)}"));
            }

            bool result =
                reportingPeriod1.Contains(reportingPeriod2.Start) ||
                reportingPeriod1.Contains(reportingPeriod2.End) ||
                reportingPeriod2.Contains(reportingPeriod1.Start) ||
                reportingPeriod2.Contains(reportingPeriod1.End);
            return result;
        }
    }
}

// ReSharper restore CheckNamespace