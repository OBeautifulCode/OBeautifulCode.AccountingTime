// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Common.cs" company="OBeautifulCode">
//    Copyright (c) OBeautifulCode 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Test
{
    using System;

    internal static class Common
    {
        public static readonly Type[] AllUnitOfTimeTypesExceptUnitOfTime =
        {
            typeof(CalendarUnitOfTime), typeof(CalendarDay), typeof(CalendarMonth), typeof(CalendarQuarter), typeof(CalendarYear), typeof(CalendarUnbounded),
            typeof(FiscalUnitOfTime), typeof(FiscalMonth), typeof(FiscalQuarter), typeof(FiscalYear), typeof(FiscalUnbounded),
            typeof(GenericUnitOfTime), typeof(GenericQuarter), typeof(GenericQuarter), typeof(GenericYear), typeof(GenericUnbounded)
        };

        public interface IReportingPeriodTest<out T> : IReportingPeriod<T>
            where T : UnitOfTime
        {
        }

        internal class ReportingPeriodTest<T> : IReportingPeriod<T>
            where T : UnitOfTime
        {
            public ReportingPeriodTest(T start, T end)
            {
                this.Start = start;
                this.End = end;
            }

            public T Start { get; }

            public T End { get; }

            public TReportingPeriod Clone<TReportingPeriod>()
                where TReportingPeriod : class, IReportingPeriod<UnitOfTime>
            {
                throw new NotImplementedException();
            }

            public IReportingPeriod<T> Clone()
            {
                throw new NotImplementedException();
            }
        }
    }
}
