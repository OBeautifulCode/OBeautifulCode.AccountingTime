// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OverflowStrategy.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime
{
    using System;

    /// <summary>
    /// A strategy for dealing with overflow when splitting a reporting period.
    /// </summary>
    [Serializable]
    public enum OverflowStrategy
    {
        /// <summary>
        /// Throw on any overflow.
        /// </summary>
        ThrowOnOverflow
    }
}

// ReSharper restore CheckNamespace