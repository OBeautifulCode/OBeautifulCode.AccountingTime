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

        internal interface IReportingPeriodTest<out T> : IReportingPeriod<T>
        where T : UnitOfTime
        {
        }

        internal class ReportingPeriodTest<T> : ReportingPeriod<T>, IReportingPeriodTest<T>
            where T : UnitOfTime
        {
            public ReportingPeriodTest(T start, T end)
            : base(start, end)
            {
            }

            public override IReportingPeriod<T> Clone()
            {
                var startClone = this.Start.Clone<T>();
                var endClone = this.End.Clone<T>();
                var result = new ReportingPeriodTest<T>(startClone, endClone);
                return result;
            }
        }
    }
}
