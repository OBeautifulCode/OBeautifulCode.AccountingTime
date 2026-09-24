// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MonthEndFiscalQuarterScheme.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    /// <summary>
    /// A fiscal quarter scheme that defines the fiscal year using <see cref="FiscalYearScheme"/>
    /// and splits that year into 3 consecutive months.
    /// </summary>
    public partial class MonthEndFiscalQuarterScheme : FiscalQuarterSchemeBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MonthEndFiscalQuarterScheme"/> class.
        /// </summary>
        /// <param name="fiscalYearScheme">The fiscal year scheme.</param>
        public MonthEndFiscalQuarterScheme(
            MonthEndFiscalYearScheme fiscalYearScheme)
        {
            if (fiscalYearScheme == null)
            {
                throw new ArgumentNullException(nameof(fiscalYearScheme));
            }

            this.FiscalYearScheme = fiscalYearScheme;
        }

        /// <summary>
        /// Gets the fiscal year scheme.
        /// </summary>
        public MonthEndFiscalYearScheme FiscalYearScheme { get; private set; }

        /// <inheritdoc />
        public override ReportingPeriod GetCalendarDayPeriod(
            FiscalQuarter fiscalQuarter)
        {
            if (fiscalQuarter == null)
            {
                throw new ArgumentNullException(nameof(fiscalQuarter));
            }

            var lastMonth = new CalendarMonth(fiscalQuarter.Year, this.FiscalYearScheme.LastMonthInFiscalYear);

            var fiscalQuarterStartMonth = (CalendarMonth)lastMonth.Plus((3 * (int)fiscalQuarter.QuarterNumber) - 14);
            var fiscalQuarterEndMonth = (CalendarMonth)lastMonth.Plus((3 * (int)fiscalQuarter.QuarterNumber) - 12);

            var result = new ReportingPeriod(
                fiscalQuarterStartMonth.GetFirstCalendarDay(),
                fiscalQuarterEndMonth.GetLastCalendarDay());

            return result;
        }
    }
}
