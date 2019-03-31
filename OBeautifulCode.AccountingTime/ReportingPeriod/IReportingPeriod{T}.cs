// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IReportingPeriod{T}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Represents a range of time over which to report, inclusive of the endpoints.
    /// </summary>
    /// <typeparam name="T">The unit-of-time used to define the start and end of the reporting period.</typeparam>
    public interface IReportingPeriod<out T> : ICloneable
        where T : UnitOfTime
    {
        /// <summary>
        ///  Gets the start of the reporting period.
        /// </summary>
        T Start { get; }

        /// <summary>
        ///  Gets the end of the reporting period.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "End", Justification = "This is a succinct and clear identifier for this property.")]
        T End { get; }

        /// <summary>
        /// Deep clones this reporting period.
        /// </summary>
        /// <typeparam name="TReportingPeriod">The type of reporting-period to return.</typeparam>
        /// <returns>
        /// A deep clone of this reporting period.
        /// </returns>
        /// <exception cref="InvalidOperationException">A clone of this reporting-period cannot be assigned to the specified type.</exception>
        TReportingPeriod DeepClone<TReportingPeriod>()
            where TReportingPeriod : class, IReportingPeriod<UnitOfTime>;

        /// <summary>
        /// Deep clones this reporting period.
        /// </summary>
        /// <returns>
        /// A deep clone of this reporting period.
        /// </returns>
        IReportingPeriod<T> DeepClone();
    }
}
