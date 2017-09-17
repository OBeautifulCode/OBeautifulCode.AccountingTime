// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MonthOfYear.cs" company="OBeautifulCode">
//    Copyright (c) OBeautifulCode 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    /// <summary>
    /// Represents a month.
    /// </summary>
    [Serializable]
    public enum MonthOfYear
    {
        /// <summary>
        /// Invalid month.
        /// </summary>
        /// <remarks>
        /// This is required so that there is a default value for the enum.
        /// </remarks>
        Invalid = 0,

        /// <summary>
        /// Month of January.
        /// </summary>
        January = 1,

        /// <summary>
        /// Month of February.
        /// </summary>
        February = 2,

        /// <summary>
        /// Month of March.
        /// </summary>
        March = 3,

        /// <summary>
        /// Month of April.
        /// </summary>
        April = 4,

        /// <summary>
        /// Month of May.
        /// </summary>
        May = 5,

        /// <summary>
        /// Month of June.
        /// </summary>
        June = 6,

        /// <summary>
        /// Month of July.
        /// </summary>
        July = 7,

        /// <summary>
        /// Month of August.
        /// </summary>
        August = 8,

        /// <summary>
        /// Month of September.
        /// </summary>
        September = 9,

        /// <summary>
        /// Month of October.
        /// </summary>
        October = 10,

        /// <summary>
        /// Month of November.
        /// </summary>
        November = 11,

        /// <summary>
        /// Month of December.
        /// </summary>
        December = 12
    }
}
