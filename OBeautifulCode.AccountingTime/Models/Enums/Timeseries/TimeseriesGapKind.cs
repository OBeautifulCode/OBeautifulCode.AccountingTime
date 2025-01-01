// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TimeseriesGapKind.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    /// <summary>
    /// Specifies the kind of gap in timeseries.
    /// </summary>
    public enum TimeseriesGapKind
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// There is a gap between the first and last datapoint.
        /// </summary>
        BetweenFirstAndLastDatapoint,

        /// <summary>
        /// There is a gap between the first datapoint and unbounded end.
        /// </summary>
        BetweenFirstDatapointAndUnboundedEnd,

        /// <summary>
        /// There is a gap between unbounded start and the last datapoint.
        /// </summary>
        BetweenUnboundedStartAndLastDatapoint,

        /// <summary>
        /// There is a gap between unbounded start and unbounded end.
        /// </summary>
        BetweenUnboundedStartAndUnboundedEnd,
    }
}
