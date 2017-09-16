// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalendarUnitOfTime.cs" company="OBeautifulCode">
//    Copyright (c) OBeautifulCode 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime
{
    using System;

    /// <summary>
    /// Represents a unit of time tied to the (gregorian) calendar.
    /// </summary>
    [Serializable]
    public abstract class CalendarUnitOfTime : UnitOfTime
    {
        /// <inheritdoc />
        public override UnitOfTimeKind UnitOfTimeKind => UnitOfTimeKind.Calendar;
    }
}

// ReSharper restore CheckNamespace