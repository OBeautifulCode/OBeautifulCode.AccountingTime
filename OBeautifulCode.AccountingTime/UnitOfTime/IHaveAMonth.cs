// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHaveAMonth.cs" company="OBeautifulCode">
//    Copyright (c) OBeautifulCode 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    /// <summary>
    /// Exposes a month.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public interface IHaveAMonth : IHaveAYear
    {
        /// <summary>
        /// Gets the month number.
        /// </summary>
        MonthNumber MonthNumber { get; }
    }
}
