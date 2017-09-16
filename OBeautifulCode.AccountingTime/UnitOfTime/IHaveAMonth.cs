// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHaveAMonth.cs" company="OBeautifulCode">
//    Copyright (c) OBeautifulCode 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime
{
    /// <summary>
    /// Exposes a month.
    /// </summary>
    public interface IHaveAMonth : IHaveAYear
    {
        /// <summary>
        /// Gets the month number.
        /// </summary>
        MonthNumber MonthNumber { get; }
    }
}

// ReSharper restore CheckNamespace