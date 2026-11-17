// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FixedDayFiscalYearScheme.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;
    using static System.FormattableString;

    /// <summary>
    /// A fiscal year scheme in which each fiscal year ends on the same fixed day of the same month (e.g. April 25, 2026)
    /// and begins on the day after that fixed day in the prior calendar year (e.g. April 26, 2025).
    /// </summary>
    /// <remarks>
    /// The fixed day must occur in the month every year, so February 29th is not allowed.
    /// The fixed day cannot always be the last day of the month (e.g. April 30th);
    /// use <see cref="MonthEndFiscalYearScheme"/> or <see cref="CalendarAlignedFiscalYearScheme"/> (for December 31st) for that.
    /// February 28th is allowed because it is not the last day of February in a leap year.
    /// With these constraints, every fiscal year spans two calendar years.
    /// </remarks>
    public partial class FixedDayFiscalYearScheme : FiscalYearSchemeBase
    {
        // A year that is not a leap year, used to determine the number of days that a month has in every year.
        private const int NonLeapYear = 2001;

        /// <summary>
        /// Initializes a new instance of the <see cref="FixedDayFiscalYearScheme"/> class.
        /// </summary>
        /// <param name="lastMonthInFiscalYear">The month in which the fiscal year ends.</param>
        /// <param name="lastDayInFiscalYear">The day of <paramref name="lastMonthInFiscalYear"/> on which the fiscal year ends.</param>
        /// <param name="fiscalYearNumberingConvention">OPTIONAL convention that determines which calendar year is used to number the fiscal year.  DEFAULT is to use the calendar year in which the fiscal year ends.</param>
        public FixedDayFiscalYearScheme(
            MonthOfYear lastMonthInFiscalYear,
            DayOfMonth lastDayInFiscalYear,
            FixedDayFiscalYearNumberingConvention fiscalYearNumberingConvention = FixedDayFiscalYearNumberingConvention.CalendarYearInWhichFiscalYearEnds)
        {
            if (lastMonthInFiscalYear == MonthOfYear.Invalid)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(lastMonthInFiscalYear)}' == '{MonthOfYear.Invalid}'"), (Exception)null);
            }

            if (lastDayInFiscalYear == DayOfMonth.Invalid)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(lastDayInFiscalYear)}' == '{DayOfMonth.Invalid}'"), (Exception)null);
            }

            if (fiscalYearNumberingConvention == FixedDayFiscalYearNumberingConvention.Unknown)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(fiscalYearNumberingConvention)}' == '{FixedDayFiscalYearNumberingConvention.Unknown}'"), (Exception)null);
            }

            if (!DayOccursInMonthEveryYear(lastDayInFiscalYear, lastMonthInFiscalYear))
            {
                throw new ArgumentException(Invariant($"'{nameof(lastDayInFiscalYear)}' == '{lastDayInFiscalYear}' does not occur every year in '{nameof(lastMonthInFiscalYear)}' == '{lastMonthInFiscalYear}'."));
            }

            if ((lastMonthInFiscalYear != MonthOfYear.February) && ((int)lastDayInFiscalYear == DateTime.DaysInMonth(NonLeapYear, (int)lastMonthInFiscalYear)))
            {
                var alternativeScheme = lastMonthInFiscalYear == MonthOfYear.December
                    ? nameof(CalendarAlignedFiscalYearScheme)
                    : nameof(MonthEndFiscalYearScheme);

                throw new ArgumentException(Invariant($"'{nameof(lastDayInFiscalYear)}' == '{lastDayInFiscalYear}' is always the last day of '{nameof(lastMonthInFiscalYear)}' == '{lastMonthInFiscalYear}'; use {alternativeScheme} instead."));
            }

            this.LastMonthInFiscalYear = lastMonthInFiscalYear;
            this.LastDayInFiscalYear = lastDayInFiscalYear;
            this.FiscalYearNumberingConvention = fiscalYearNumberingConvention;
        }

        /// <summary>
        /// Gets the month in which the fiscal year ends.
        /// </summary>
        public MonthOfYear LastMonthInFiscalYear { get; private set; }

        /// <summary>
        /// Gets the day of <see cref="LastMonthInFiscalYear"/> on which the fiscal year ends.
        /// </summary>
        public DayOfMonth LastDayInFiscalYear { get; private set; }

        /// <summary>
        /// Gets the convention that determines which calendar year is used to number the fiscal year.
        /// </summary>
        public FixedDayFiscalYearNumberingConvention FiscalYearNumberingConvention { get; private set; }

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException"><paramref name="fiscalYear"/> is null.</exception>
        public override ReportingPeriod GetCalendarDayPeriod(
            FiscalYear fiscalYear)
        {
            if (fiscalYear == null)
            {
                throw new ArgumentNullException(nameof(fiscalYear));
            }

            var lastDayInFiscalYear = this.GetLastDayInFiscalYear(fiscalYear.Year);

            // LastDayInFiscalYear occurs in LastMonthInFiscalYear every year, so subtracting a year never shifts the day.
            var firstDayInFiscalYear = lastDayInFiscalYear.AddYears(-1).AddDays(1);

            var result = new ReportingPeriod(firstDayInFiscalYear.ToCalendarDay(), lastDayInFiscalYear.ToCalendarDay());

            return result;
        }

        /// <summary>
        /// Determines if the specified day occurs in the specified month every year.
        /// </summary>
        /// <param name="day">The day.</param>
        /// <param name="month">The month.</param>
        /// <returns>
        /// true if the day occurs in the month every year, otherwise false.
        /// </returns>
        internal static bool DayOccursInMonthEveryYear(
            DayOfMonth day,
            MonthOfYear month)
        {
            var result = (int)day <= DateTime.DaysInMonth(NonLeapYear, (int)month);

            return result;
        }

        /// <summary>
        /// Gets the last day in the specified fiscal year.
        /// </summary>
        /// <param name="fiscalYear">The fiscal year number.</param>
        /// <returns>
        /// The last day in the specified fiscal year.
        /// </returns>
        internal DateTime GetLastDayInFiscalYear(
            int fiscalYear)
        {
            var calendarYearInWhichFiscalYearEnds = this.GetCalendarYearInWhichFiscalYearEnds(fiscalYear);

            var result = new DateTime(calendarYearInWhichFiscalYearEnds, (int)this.LastMonthInFiscalYear, (int)this.LastDayInFiscalYear);

            return result;
        }

        private int GetCalendarYearInWhichFiscalYearEnds(
            int fiscalYear)
        {
            int result;

            if (this.FiscalYearNumberingConvention == FixedDayFiscalYearNumberingConvention.CalendarYearInWhichFiscalYearEnds)
            {
                result = fiscalYear;
            }
            else if (this.FiscalYearNumberingConvention == FixedDayFiscalYearNumberingConvention.CalendarYearInWhichFiscalYearBegins)
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
