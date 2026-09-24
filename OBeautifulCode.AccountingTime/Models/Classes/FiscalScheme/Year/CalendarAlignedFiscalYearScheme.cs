// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalendarAlignedFiscalYearScheme.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    /// <summary>
    /// A fiscal year scheme in which each fiscal year mirrors the calendar year;
    /// 12 consecutive months beginning on January 1st and ending on December 31st.
    /// </summary>
    public partial class CalendarAlignedFiscalYearScheme : FiscalYearSchemeBase
    {
       /// <inheritdoc />
        public override ReportingPeriod GetCalendarDayPeriod(
            FiscalYear fiscalYear)
        {
            if (fiscalYear == null)
            {
                throw new ArgumentNullException(nameof(fiscalYear));
            }

            var januaryFirst = new CalendarDay(fiscalYear.Year, MonthOfYear.January, DayOfMonth.One);

            var decemberThirtyFirst = new CalendarDay(fiscalYear.Year, MonthOfYear.December, DayOfMonth.ThirtyOne);

            var result = new ReportingPeriod(januaryFirst, decemberThirtyFirst);

            return result;
        }
    }
}
