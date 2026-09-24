// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MonthEndFiscalYearScheme.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;
    using static System.FormattableString;

    /// <summary>
    /// A fiscal year scheme in which each fiscal year is 12 consecutive months ending on
    /// the last day of a specified month (except December - use <see cref="CalendarAlignedFiscalYearScheme"/> for that).
    /// </summary>
    public partial class MonthEndFiscalYearScheme : FiscalYearSchemeBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MonthEndFiscalYearScheme"/> class.
        /// </summary>
        /// <param name="lastMonthInFiscalYear">The last month of the fiscal year.</param>
        /// <exception cref="ArgumentException"><paramref name="lastMonthInFiscalYear"/> is invalid.</exception>
        /// <exception cref="ArgumentException"><paramref name="lastMonthInFiscalYear"/> is <see cref="MonthOfYear.December"/>.</exception>
        public MonthEndFiscalYearScheme(
            MonthOfYear lastMonthInFiscalYear)
        {
            if (lastMonthInFiscalYear == MonthOfYear.Invalid)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(lastMonthInFiscalYear)}' == '{MonthOfYear.Invalid}'"), (Exception)null);
            }

            if (lastMonthInFiscalYear == MonthOfYear.December)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(lastMonthInFiscalYear)}' == '{MonthOfYear.December}'"), (Exception)null);
            }

            this.LastMonthInFiscalYear = lastMonthInFiscalYear;
        }

        /// <summary>
        /// Gets the last month of the fiscal year.
        /// </summary>
        public MonthOfYear LastMonthInFiscalYear { get; private set; }

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException"><paramref name="fiscalYear"/> is null.</exception>
        public override ReportingPeriod GetCalendarDayPeriod(
            FiscalYear fiscalYear)
        {
            if (fiscalYear == null)
            {
                throw new ArgumentNullException(nameof(fiscalYear));
            }

            var lastDayInEndingMonth = DateTime.DaysInMonth(fiscalYear.Year, (int)this.LastMonthInFiscalYear);

            var lastDayInFiscalYear = new DateTime(fiscalYear.Year, (int)this.LastMonthInFiscalYear, lastDayInEndingMonth);

            var firstDayInFiscalYear = lastDayInFiscalYear.AddDays(1).AddYears(-1);

            var result = new ReportingPeriod(firstDayInFiscalYear.ToCalendarDay(), lastDayInFiscalYear.ToCalendarDay());

            return result;
        }
    }
}
