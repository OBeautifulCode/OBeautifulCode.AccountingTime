// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalendarYearAccountingPeriodSystem.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime
{
    /// <summary>
    /// A calendar year is 12 consecutive months beginning on January 1st and ending on December 31st.
    /// </summary>
    public class CalendarYearAccountingPeriodSystem : AccountingPeriodSystem
    {
        /// <inheritdoc />
        public override ReportingPeriodInclusive<CalendarDay> GetReportingPeriodForFiscalYear(FiscalYear fiscalYear)
        {
            var januaryFirst = new CalendarDay(fiscalYear.Year, MonthOfYear.January, DayOfMonth.One);
            var decemberThirtyFirst = new CalendarDay(fiscalYear.Year, MonthOfYear.December, DayOfMonth.ThirtyOne);
            var result = new ReportingPeriodInclusive<CalendarDay>(januaryFirst, decemberThirtyFirst);
            return result;
        }
    }
}

// ReSharper restore CheckNamespace