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
    /// Property-related extension methods on <see cref="ReportingPeriod"/>.
    /// </summary>
    public static partial class ReportingPeriodExtensions
    {
        /// <summary>
        /// Gets the <see cref="Unit"/> of a <see cref="ReportingPeriod"/>.
        /// </summary>
        /// <param name="reportingPeriod">The reporting period.</param>
        /// <returns>
        /// The corresponding <see cref="Unit"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="reportingPeriod"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="reportingPeriod"/> <see cref="ReportingPeriod.Start"/> and <see cref="ReportingPeriod.End"/> has different granularity.</exception>
        public static Unit GetUnit(
            this ReportingPeriod reportingPeriod)
        {
            if (reportingPeriod == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod));
            }

            if (reportingPeriod.Start.UnitOfTimeGranularity != reportingPeriod.End.UnitOfTimeGranularity)
            {
                throw new ArgumentException(Invariant($"{nameof(reportingPeriod)} Start and End has different granularity."));
            }

            var result = reportingPeriod.Start.GetUnit();

            return result;
        }

        /// <summary>
        /// Gets the granularity of the unit-of-time used in a reporting period.
        /// </summary>
        /// <param name="reportingPeriod">The reporting period.</param>
        /// <returns>
        /// The granularity of the unit-of-time used in the specified reporting period.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="reportingPeriod"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="reportingPeriod"/> <see cref="ReportingPeriod.Start"/> and <see cref="ReportingPeriod.End"/> has different granularity.</exception>
        public static UnitOfTimeGranularity GetUnitOfTimeGranularity(
            this ReportingPeriod reportingPeriod)
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
        /// Gets the granularity of the unit-of-time used in a reporting period
        /// for the bound component OR if both components are <see cref="UnitOfTimeGranularity.Unbounded"/>
        /// then return <see cref="UnitOfTimeGranularity.Unbounded"/>.
        /// </summary>
        /// <param name="reportingPeriod">The reporting period.</param>
        /// <returns>
        /// The granularity of the bounded component unit-of-time used in the specified reporting period.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="reportingPeriod"/> is null.</exception>
        public static UnitOfTimeGranularity GetBoundedComponentGranularityOrUnbounded(
            this ReportingPeriod reportingPeriod)
        {
            if (reportingPeriod == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod));
            }

            UnitOfTimeGranularity result;

            if (reportingPeriod.Start.UnitOfTimeGranularity != reportingPeriod.End.UnitOfTimeGranularity)
            {
                result = reportingPeriod.Start.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded
                    ? reportingPeriod.End.UnitOfTimeGranularity
                    : reportingPeriod.Start.UnitOfTimeGranularity;
            }
            else
            {
                result = reportingPeriod.Start.UnitOfTimeGranularity;
            }

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
            this ReportingPeriod reportingPeriod)
        {
            if (reportingPeriod == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod));
            }

            var result = reportingPeriod.Start.UnitOfTimeKind;

            return result;
        }

        /// <summary>
        /// DEPRECATED.  Use <see cref="GetUnitsOfTimeWithin(ReportingPeriod)"/>.
        /// </summary>
        /// <param name="reportingPeriod">The reporting period.</param>
        /// <returns>
        /// DEPRECATED: Use <see cref="GetUnitsOfTimeWithin(ReportingPeriod)"/>.
        /// </returns>
        public static IReadOnlyList<UnitOfTime> GetUnitsWithin(
            this ReportingPeriod reportingPeriod)
        {
            var result = reportingPeriod.GetUnitsOfTimeWithin();

            return result;
        }

        /// <summary>
        /// Gets the distinct units-of-time contained within a specified reporting period.
        /// For example, a reporting period of 2Q2017-4Q2017, contains 2Q2017, 3Q2017, and 4Q2017.
        /// </summary>
        /// <remarks>
        /// The endpoints are considered units within the reporting period.
        /// </remarks>
        /// <param name="reportingPeriod">The reporting period.</param>
        /// <returns>
        /// The units-of-time contained within the specified reporting period.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="reportingPeriod"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="reportingPeriod"/> <see cref="ReportingPeriod.Start"/> and/or <see cref="ReportingPeriod.End"/> is unbounded.</exception>
        public static IReadOnlyList<UnitOfTime> GetUnitsOfTimeWithin(
            this ReportingPeriod reportingPeriod)
        {
            if (reportingPeriod == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod));
            }

            if (reportingPeriod.HasComponentWithUnboundedGranularity())
            {
                throw new ArgumentException(Invariant($"{nameof(reportingPeriod)} {nameof(reportingPeriod.Start)} and/or {nameof(reportingPeriod.End)} is unbounded."));
            }

            var allUnits = new List<UnitOfTime>();
            var currentUnit = reportingPeriod.Start;
            do
            {
                allUnits.Add(currentUnit);

                currentUnit = currentUnit.Plus(1);
            }
            while (currentUnit <= reportingPeriod.End);

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
            this ReportingPeriod reportingPeriod)
        {
            if (reportingPeriod == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod));
            }

            var result = (reportingPeriod.Start.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded) ||
                         (reportingPeriod.End.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded);

            return result;
        }

        /// <summary>
        /// Determines if the reporting period start and end have the same granularity.
        /// </summary>
        /// <param name="reportingPeriod">The reporting period.</param>
        /// <returns>
        /// true if the reporting period's start and end have the same granularity, otherwise false.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="reportingPeriod"/> is null.</exception>
        public static bool HasUniformGranularity(
            this ReportingPeriod reportingPeriod)
        {
            if (reportingPeriod == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod));
            }

            var result = reportingPeriod.Start.UnitOfTimeGranularity == reportingPeriod.End.UnitOfTimeGranularity;

            return result;
        }
    }
}