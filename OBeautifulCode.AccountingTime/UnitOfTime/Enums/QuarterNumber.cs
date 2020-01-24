// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QuarterNumber.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    /// <summary>
    /// A quarter number.
    /// </summary>
    [Serializable]
    public enum QuarterNumber
    {
        /// <summary>
        /// An invalid quarter.
        /// </summary>
        /// <remarks>
        /// This is required so that there is a default value for the enum.
        /// </remarks>
        Invalid = 0,

        /// <summary>
        /// First quarter.
        /// </summary>
        Q1 = 1,

        /// <summary>
        /// Second quarter.
        /// </summary>
        Q2 = 2,

        /// <summary>
        /// Third quarter.
        /// </summary>
        Q3 = 3,

        /// <summary>
        /// Fourth quarter.
        /// </summary>
        Q4 = 4,
    }
}
