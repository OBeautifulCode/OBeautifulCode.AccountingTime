// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalendarUnitOfTime.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// Represents a unit of time tied to the (gregorian) calendar.
    /// </summary>
    public abstract partial class CalendarUnitOfTime : UnitOfTime, IModelViaCodeGen, IComparableViaCodeGen
    {
        /// <inheritdoc />
        public override UnitOfTimeKind UnitOfTimeKind => UnitOfTimeKind.Calendar;
    }
}
