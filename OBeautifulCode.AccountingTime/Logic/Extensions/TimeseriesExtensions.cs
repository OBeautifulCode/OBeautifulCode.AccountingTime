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

        /// <summary>
        /// Determines whether or not the timeseries has a gap.
        /// </summary>
        /// <typeparam name="T">The type of value of the datapoints.</typeparam>
        /// <param name="timeseries">The timeseries.</param>
        /// <param name="timeseriesGapKind">The kind of gap to check for.</param>
        /// <param name="emptyTimeseriesHasGap">Specifies whether an empty timeseries is considered to have a gap.</param>
        /// <returns>
        /// true if the timeseries has a gap, otherwise false.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="timeseries"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="timeseriesGapKind"/> is <see cref="TimeseriesGapKind.Unknown"/>.</exception>
        public static bool HasGap<T>(
            this Timeseries<T> timeseries,
            TimeseriesGapKind timeseriesGapKind,
            bool emptyTimeseriesHasGap)
        {
            if (timeseries == null)
            {
                throw new ArgumentNullException(nameof(timeseries));
            }

            if (timeseriesGapKind == TimeseriesGapKind.Unknown)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(timeseriesGapKind)}' == '{TimeseriesGapKind.Unknown}'"), (Exception)null);
            }

            if (!timeseries.Datapoints.Any())
            {
                return emptyTimeseriesHasGap;
            }

            if ((timeseriesGapKind == TimeseriesGapKind.BetweenUnboundedStartAndLastDatapoint) ||
                 (timeseriesGapKind == TimeseriesGapKind.BetweenUnboundedStartAndUnboundedEnd))
            {
                if (timeseries.Datapoints.First().ReportingPeriod.Start.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded)
                {
                    return true;
                }
            }

            if ((timeseriesGapKind == TimeseriesGapKind.BetweenFirstDatapointAndUnboundedEnd) ||
                (timeseriesGapKind == TimeseriesGapKind.BetweenUnboundedStartAndUnboundedEnd))
            {
                if (timeseries.Datapoints.Last().ReportingPeriod.End.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded)
                {
                    return true;
                }
            }

            bool result;

            if ((timeseriesGapKind == TimeseriesGapKind.BetweenFirstAndLastDatapoint) ||
                (timeseriesGapKind == TimeseriesGapKind.BetweenFirstDatapointAndUnboundedEnd) ||
                (timeseriesGapKind == TimeseriesGapKind.BetweenUnboundedStartAndLastDatapoint) ||
                (timeseriesGapKind == TimeseriesGapKind.BetweenUnboundedStartAndUnboundedEnd))
            {
                result = !timeseries.Datapoints
                    .Zip(
                        timeseries.Datapoints.Skip(1),
                        (first, second) => new { First = first.ReportingPeriod, Second = second.ReportingPeriod })
                    .All(_ => _.Second.IsGreaterThanAndAdjacentTo(_.First));
            }
            else
            {
                throw new NotSupportedException(Invariant($"This {nameof(TimeseriesGapKind)} is not supported: {timeseriesGapKind}."));
            }

            return result;
        }
    }
}
