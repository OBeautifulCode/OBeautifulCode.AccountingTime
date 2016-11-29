// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QuarterNumber.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime
{
    /// <summary>
    /// A quarter number.
    /// </summary>
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
        First = 1,

        /// <summary>
        /// Second quarter.
        /// </summary>
        Second = 2,

        /// <summary>
        /// Third quarter.
        /// </summary>
        Third = 3,

        /// <summary>
        /// Fourth quarter.
        /// </summary>
        Fourth = 4
    }
}

// ReSharper restore CheckNamespace