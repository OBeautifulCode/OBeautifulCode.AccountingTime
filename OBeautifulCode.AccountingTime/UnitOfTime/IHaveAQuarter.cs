// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHaveAQuarter.cs" company="OBeautifulCode">
//    Copyright (c) OBeautifulCode 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime
{
    /// <summary>
    /// Exposes a quarter.
    /// </summary>
    public interface IHaveAQuarter : IHaveAYear
    {
        /// <summary>
        /// Gets the quarter number.
        /// </summary>
        QuarterNumber QuarterNumber { get; }
    }
}

// ReSharper restore CheckNamespace