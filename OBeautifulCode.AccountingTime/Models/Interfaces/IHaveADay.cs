// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHaveADay.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    /// <summary>
    /// Exposes a day.
    /// </summary>
    public interface IHaveADay : IHaveAMonth
    {
        /// <summary>
        /// Gets the day of the month.
        /// </summary>
        DayOfMonth DayOfMonth { get; }
    }
}
