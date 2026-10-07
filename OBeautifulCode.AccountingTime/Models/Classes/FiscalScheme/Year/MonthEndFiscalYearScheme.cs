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
        /// <param name="fiscalYearNumberingConvention">OPTIONAL convention that determines which calendar year is used to number the fiscal year.  DEFAULT is to use the calendar year in which the fiscal year ends.</param>
        public MonthEndFiscalYearScheme(
            MonthOfYear lastMonthInFiscalYear,
            MonthEndFiscalYearNumberingConvention fiscalYearNumberingConvention = MonthEndFiscalYearNumberingConvention.CalendarYearInWhichFiscalYearEnds)
        {
            if (lastMonthInFiscalYear == MonthOfYear.Invalid)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(lastMonthInFiscalYear)}' == '{MonthOfYear.Invalid}'"), (Exception)null);
            }

            if (lastMonthInFiscalYear == MonthOfYear.December)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(lastMonthInFiscalYear)}' == '{MonthOfYear.December}'"), (Exception)null);
            }

            if (fiscalYearNumberingConvention == MonthEndFiscalYearNumberingConvention.Unknown)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(fiscalYearNumberingConvention)}' == '{MonthEndFiscalYearNumberingConvention.Unknown}'"), (Exception)null);
            }

            this.LastMonthInFiscalYear = lastMonthInFiscalYear;
            this.FiscalYearNumberingConvention = fiscalYearNumberingConvention;
        }

        /// <summary>
        /// Gets the last month of the fiscal year.
        /// </summary>
        public MonthOfYear LastMonthInFiscalYear { get; private set; }

        /// <summary>
        /// Gets the convention that determines which calendar year is used to number the fiscal year.
        /// </summary>
        public MonthEndFiscalYearNumberingConvention FiscalYearNumberingConvention { get; private set; }

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException"><paramref name="fiscalYear"/> is null.</exception>
        public override ReportingPeriod GetCalendarDayPeriod(
            FiscalYear fiscalYear)
        {
            if (fiscalYear == null)
            {
                throw new ArgumentNullException(nameof(fiscalYear));
            }

            var calendarYearInWhichFiscalYearEnds = this.GetCalendarYearInWhichFiscalYearEnds(fiscalYear.Year);

            var lastDayInEndingMonth = DateTime.DaysInMonth(calendarYearInWhichFiscalYearEnds, (int)this.LastMonthInFiscalYear);

            var lastDayInFiscalYear = new DateTime(calendarYearInWhichFiscalYearEnds, (int)this.LastMonthInFiscalYear, lastDayInEndingMonth);

            var firstDayInFiscalYear = lastDayInFiscalYear.AddDays(1).AddYears(-1);

            var result = new ReportingPeriod(firstDayInFiscalYear.ToCalendarDay(), lastDayInFiscalYear.ToCalendarDay());

            return result;
        }

        /// <summary>
        /// Gets the calendar year in which the specified fiscal year ends.
        /// </summary>
        /// <remarks>
        /// Because <see cref="LastMonthInFiscalYear"/> cannot be <see cref="MonthOfYear.December"/>,
        /// every fiscal year spans two calendar years: it begins in the calendar year prior to the one in which it ends.
        /// </remarks>
        /// <param name="fiscalYear">The fiscal year number.</param>
        /// <returns>
        /// The calendar year in which the specified fiscal year ends.
        /// </returns>
        internal int GetCalendarYearInWhichFiscalYearEnds(
            int fiscalYear)
        {
            int result;

            if (this.FiscalYearNumberingConvention == MonthEndFiscalYearNumberingConvention.CalendarYearInWhichFiscalYearEnds)
            {
                result = fiscalYear;
            }
            else if (this.FiscalYearNumberingConvention == MonthEndFiscalYearNumberingConvention.CalendarYearInWhichFiscalYearBegins)
            {
                result = fiscalYear + 1;
            }
            else
            {
                throw new NotSupportedException(Invariant($"This {nameof(this.FiscalYearNumberingConvention)} is not supported: {this.FiscalYearNumberingConvention}."));
            }

            return result;
        }
    }
}
