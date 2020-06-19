// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHaveAQuarter.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

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
