// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfTimeKind.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime
{
    /// <summary>
    /// The kind of unit-of-time.
    /// </summary>
    public enum UnitOfTimeKind
    {
        /// <summary>
        /// Represents a unit of time tied to the (gregorian) calendar.
        /// </summary>
        Calendar,

        /// <summary>
        /// Represents a unit of time in the context of some company's fiscal year.
        /// </summary>
        Fiscal,

        /// <summary>
        /// Represents a generic unit of time, without any context.
        /// </summary>
        Generic
    }
}

// ReSharper restore CheckNamespace