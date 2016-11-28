// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IReportingPeriodInclusive{T}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime
{
    /// <summary>
    /// Represents a reporting period, inclusive of the endpoints.
    /// </summary>
    /// <typeparam name="T">The unit-of-time used to define the start and end of the reporting period.</typeparam>
    public interface IReportingPeriodInclusive<out T> : IReportingPeriod<T>
        where T : UnitOfTime
    {
    }
}

// ReSharper restore CheckNamespace