// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalendarYearAccountingPeriodSystem.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime
{
    using System;

    /// <summary>
    /// A calendar year is 12 consecutive months beginning on January 1st and ending on December 31st.
    /// </summary>
    [Serializable]
    public class CalendarYearAccountingPeriodSystem : AccountingPeriodSystem
    {
        /// <inheritdoc />
        /// <exception cref="ArgumentNullException"><paramref name="fiscalYear"/> is null.</exception>
        public override ReportingPeriodInclusive<CalendarDay> GetReportingPeriodForFiscalYear(FiscalYear fiscalYear)
        {
            if (fiscalYear == null)
            {
                throw new ArgumentNullException(nameof(fiscalYear));
            }

            var januaryFirst = new CalendarDay(fiscalYear.Year, MonthOfYear.January, DayOfMonth.One);
            var decemberThirtyFirst = new CalendarDay(fiscalYear.Year, MonthOfYear.December, DayOfMonth.ThirtyOne);
            var result = new ReportingPeriodInclusive<CalendarDay>(januaryFirst, decemberThirtyFirst);
            return result;
        }
    }
}

// ReSharper restore CheckNamespace