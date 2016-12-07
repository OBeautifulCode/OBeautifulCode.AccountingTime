// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GenericUnitOfTime.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime
{
    using System;

    /// <summary>
    /// Represents a generic unit of time, without any context.
    /// </summary>
    [Serializable]
    public abstract class GenericUnitOfTime : UnitOfTime
    {
        /// <inheritdoc />
        public override UnitOfTimeKind UnitOfTimeKind => UnitOfTimeKind.Generic;
    }
}

// ReSharper restore CheckNamespace