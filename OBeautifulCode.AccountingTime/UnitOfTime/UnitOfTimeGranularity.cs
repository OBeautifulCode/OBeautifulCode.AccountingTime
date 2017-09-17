// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfTimeGranularity.cs" company="OBeautifulCode">
//    Copyright (c) OBeautifulCode 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    /// <summary>
    /// The granuarlity of a unit-of-time.
    /// </summary>
    public enum UnitOfTimeGranularity
    {
        /// <summary>
        /// Invalid granularity.
        /// </summary>
        /// <remarks>
        /// This is required so that there is a default value for the enum.
        /// </remarks>
        Invalid,

        /// <summary>
        /// Day-level granularity.
        /// </summary>
        Day,

        /// <summary>
        /// Month-level granularity.
        /// </summary>
        Month,

        /// <summary>
        /// Quarter-level granularity.
        /// </summary>
        Quarter,

        /// <summary>
        /// Year-level granularity.
        /// </summary>
        Year,

        /// <summary>
        /// Unbounded granularity.
        /// </summary>
        Unbounded
    }
}
