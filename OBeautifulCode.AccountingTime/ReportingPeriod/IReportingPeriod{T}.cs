﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IReportingPeriod{T}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime
{
    /// <summary>
    /// Represents a range of time over which to report.
    /// </summary>
    /// <typeparam name="T">The unit-of-time used to define the start and end of the reporting period.</typeparam>
    public interface IReportingPeriod<out T>
        where T : UnitOfTime
    {
        /// <summary>
        ///  Gets the start of the reporting period.
        /// </summary>
        T Start { get; }

        /// <summary>
        ///  Gets the end of the reporting period.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "End", Justification = "This is a succinct and clear identifier for this property.")]
        T End { get; }

        /// <summary>
        /// Gets a clone of this reporting period.
        /// </summary>
        /// <returns>
        /// A clone of this reporting period.
        /// </returns>
        IReportingPeriod<T> Clone();
    }
}

// ReSharper restore CheckNamespace