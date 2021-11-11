// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DatapointExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System.Collections.Generic;

    /// <summary>
    /// Extension methods on <see cref="Datapoint{T}"/>.
    /// </summary>
    public static class DatapointExtensions
    {
        /// <summary>
        /// Creates a timeseries from the specified datapoints.
        /// </summary>
        /// <typeparam name="T">The type of value of the datapoints.</typeparam>
        /// <param name="datapoints">The datapoints.</param>
        /// <returns>
        /// The timeseries.
        /// </returns>
        public static Timeseries<T> ToTimeseries<T>(
            this IReadOnlyList<Datapoint<T>> datapoints)
        {
            var result = new Timeseries<T>(datapoints);

            return result;
        }
    }
}
