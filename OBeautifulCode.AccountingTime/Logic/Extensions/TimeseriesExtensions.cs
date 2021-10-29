// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TimeseriesExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using static System.FormattableString;

    /// <summary>
    /// Extension methods on <see cref="Timeseries{T}"/>.
    /// </summary>
    public static class TimeseriesExtensions
    {
        /// <summary>
        /// Gets the matching datapoints from a timeseries.
        /// </summary>
        /// <typeparam name="T">The type of value of the datapoints.</typeparam>
        /// <param name="timeseries">The timeseries.</param>
        /// <param name="reportingPeriod">The reporting period to search for.</param>
        /// <param name="reportingPeriodComparison">OPTIONAL value that determines how the reporting period of the datapoints in <paramref name="timeseries"/> are compared to <paramref name="reportingPeriod"/>.  DEFAULT is to match when the reporting periods are equal, ignoring granularity.</param>
        /// <returns>
        /// The matching datapoints or an empty collection if there are none.
        /// If using <see cref="ReportingPeriodComparison.IsEqualToIgnoringGranularity"/> or <see cref="ReportingPeriodComparison.Contains"/>
        /// then a single datapoint will be returned at most.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="timeseries"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="reportingPeriod"/> is null.</exception>
        /// <exception cref="NotSupportedException"><paramref name="reportingPeriodComparison"/> is not supported.</exception>
        public static IReadOnlyList<Datapoint<T>> GetMatchingDatapoints<T>(
            this Timeseries<T> timeseries,
            ReportingPeriod reportingPeriod,
            ReportingPeriodComparison reportingPeriodComparison = ReportingPeriodComparison.IsEqualToIgnoringGranularity)
        {
            if (timeseries == null)
            {
                throw new ArgumentNullException(nameof(timeseries));
            }

            if (reportingPeriod == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod));
            }

            IReadOnlyList<Datapoint<T>> result;

            if (reportingPeriodComparison == ReportingPeriodComparison.IsEqualToIgnoringGranularity)
            {
                var mostGranularReportingPeriod = reportingPeriod.ToMostGranular();

                result = timeseries.Datapoints.Where(_ => _.ReportingPeriod.ToMostGranular().Equals(mostGranularReportingPeriod)).ToList();
            }
            else if (reportingPeriodComparison == ReportingPeriodComparison.Contains)
            {
                result = timeseries.Datapoints.Where(_ => (_.ReportingPeriod.GetUnitOfTimeKind() == reportingPeriod.GetUnitOfTimeKind()) && _.ReportingPeriod.Contains(reportingPeriod)).ToList();
            }
            else
            {
                throw new NotSupportedException(Invariant($"This {nameof(ReportingPeriodComparison)} is not supported: {reportingPeriodComparison}."));
            }

            return result;
        }
    }
}
