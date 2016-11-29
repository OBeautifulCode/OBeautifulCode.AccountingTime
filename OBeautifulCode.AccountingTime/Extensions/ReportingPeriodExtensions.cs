// --------------------------------------------------------------------------------------------------------------------
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
        /// </summary>
        /// <typeparam name="T">The type unit-of-time/reporting period.</typeparam>
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

    }
}

// ReSharper restore CheckNamespace