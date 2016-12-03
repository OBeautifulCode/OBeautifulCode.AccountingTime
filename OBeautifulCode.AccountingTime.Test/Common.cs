// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Common.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Test
{
    using System;

    internal static class Common
    {
        public static readonly Type[] AllUnitOfTimeTypesExceptUnitOfTime =
        {
            typeof(CalendarUnitOfTime), typeof(CalendarDay), typeof(CalendarMonth), typeof(CalendarQuarter), typeof(CalendarYear),
            typeof(FiscalUnitOfTime), typeof(FiscalMonth), typeof(FiscalQuarter), typeof(FiscalYear),
            typeof(GenericUnitOfTime), typeof(GenericQuarter), typeof(GenericQuarter), typeof(GenericYear)
        };
    }
}
