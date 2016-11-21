// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FiscalYearAccountingPeriodSystem.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime
{
    using System;

    /// <summary>
    /// A fiscal year is 12 consecutive months ending on the last day of any month except December 31st.
    /// </summary>
    public class FiscalYearAccountingPeriodSystem : AccountingPeriodSystem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FiscalYearAccountingPeriodSystem"/> class.
        /// </summary>
        /// <param name="lastMonthInFiscalYear">The last month of the fiscal year.</param>
        public FiscalYearAccountingPeriodSystem(MonthOfYear lastMonthInFiscalYear)
        {
            // validate here
            // lastMonthInFiscalYear.Requires(nameof(lastMonthInFiscalYear)).IsNotEqualTo(Month.December);

            this.LastMonthInFiscalYear = lastMonthInFiscalYear;
        }

        // ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

        /// <summary>
        /// Gets the last month of the fiscal year.
        /// </summary>
        public MonthOfYear LastMonthInFiscalYear { get; private set; }

        // ReSharper restore AutoPropertyCanBeMadeGetOnly.Local

        /// <inheritdoc />
        public override ReportingPeriodInclusive<CalendarDay> GetReportingPeriodForFiscalYear(FiscalYear fiscalYear)
        {
            // validate here

            var lastDayInEndingMonth = DateTime.DaysInMonth(fiscalYear.Year, (int)this.LastMonthInFiscalYear);
            var lastDayInFiscalYear = new DateTime(fiscalYear.Year, (int)this.LastMonthInFiscalYear, lastDayInEndingMonth);
            var firstDayInFiscalYear = lastDayInFiscalYear.AddDays(1).AddYears(-1);
            var result = new ReportingPeriodInclusive<CalendarDay>(firstDayInFiscalYear.ToCalendarDay(), lastDayInFiscalYear.ToCalendarDay());
            return result;
        }
    }
}

// ReSharper restore CheckNamespace