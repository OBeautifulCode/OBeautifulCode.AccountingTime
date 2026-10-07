// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FiftyTwoFiftyThreeWeekFiscalYearScheme.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using OBeautifulCode.DateTime.Recipes;
    using static System.FormattableString;

    /// <summary>
    /// A fiscal year scheme in which each fiscal year is 52 or 53 weeks long and always
    /// ends on the same day of the week.
    /// </summary>
    public partial class FiftyTwoFiftyThreeWeekFiscalYearScheme : FiscalYearSchemeBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FiftyTwoFiftyThreeWeekFiscalYearScheme"/> class.
        /// </summary>
        /// <param name="lastDayOfWeekInFiscalYear">The day of the week that the fiscal year always ends on.</param>
        /// <param name="anchorMonth">The month that the fiscal year end is anchored to.  See <see cref="Methodology"/>.</param>
        /// <param name="methodology">The methodology used to identify the last day of the accounting year.</param>
        /// <param name="fiscalYearNumberingConvention">OPTIONAL convention that determines which calendar year is used to number the fiscal year.  DEFAULT is to use the calendar year of the anchor month.</param>
        public FiftyTwoFiftyThreeWeekFiscalYearScheme(
            DayOfWeek lastDayOfWeekInFiscalYear,
            MonthOfYear anchorMonth,
            FiftyTwoFiftyThreeWeekMethodology methodology,
            FiftyTwoFiftyThreeWeekFiscalYearNumberingConvention fiscalYearNumberingConvention = FiftyTwoFiftyThreeWeekFiscalYearNumberingConvention.CalendarYearOfAnchorMonth)
        {
            if (anchorMonth == MonthOfYear.Invalid)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(anchorMonth)}' == '{MonthOfYear.Invalid}'"), (Exception)null);
            }

            if (methodology == FiftyTwoFiftyThreeWeekMethodology.Unknown)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(methodology)}' == '{FiftyTwoFiftyThreeWeekMethodology.Unknown}'"), (Exception)null);
            }

            if (fiscalYearNumberingConvention == FiftyTwoFiftyThreeWeekFiscalYearNumberingConvention.Unknown)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(fiscalYearNumberingConvention)}' == '{FiftyTwoFiftyThreeWeekFiscalYearNumberingConvention.Unknown}'"), (Exception)null);
            }

            if ((fiscalYearNumberingConvention == FiftyTwoFiftyThreeWeekFiscalYearNumberingConvention.CalendarYearPriorToAnchorMonth) && (anchorMonth == MonthOfYear.December))
            {
                throw new ArgumentException(Invariant($"'{nameof(fiscalYearNumberingConvention)}' == '{FiftyTwoFiftyThreeWeekFiscalYearNumberingConvention.CalendarYearPriorToAnchorMonth}' cannot be used when '{nameof(anchorMonth)}' == '{MonthOfYear.December}'."));
            }

            this.LastDayOfWeekInFiscalYear = lastDayOfWeekInFiscalYear;
            this.AnchorMonth = anchorMonth;
            this.Methodology = methodology;
            this.FiscalYearNumberingConvention = fiscalYearNumberingConvention;
        }

        /// <summary>
        /// Gets the day of the week that the fiscal year always ends on.
        /// </summary>
        public DayOfWeek LastDayOfWeekInFiscalYear { get; private set; }

        /// <summary>
        /// Gets the month that the fiscal year end is anchored to.  See <see cref="Methodology"/>.
        /// </summary>
        public MonthOfYear AnchorMonth { get; private set; }

        /// <summary>
        /// Gets the methodology used to identify the last day of the accounting year.
        /// </summary>
        public FiftyTwoFiftyThreeWeekMethodology Methodology { get; private set; }

        /// <summary>
        /// Gets the convention that determines which calendar year is used to number the fiscal year.
        /// </summary>
        public FiftyTwoFiftyThreeWeekFiscalYearNumberingConvention FiscalYearNumberingConvention { get; private set; }

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException"><paramref name="fiscalYear"/> is null.</exception>
        [SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", MessageId = "calendarYearOfAnchorMonth-1", Justification = "Overflow is impossible given constraint on year.")]
        public override ReportingPeriod GetCalendarDayPeriod(
            FiscalYear fiscalYear)
        {
            if (fiscalYear == null)
            {
                throw new ArgumentNullException(nameof(fiscalYear));
            }

            var calendarYearOfAnchorMonth = this.GetCalendarYearOfAnchorMonth(fiscalYear.Year);

            var firstDayInYear = this.GetAccountingYearEndDate(calendarYearOfAnchorMonth - 1).AddDays(1).ToCalendarDay();

            var lastDayInYear = this.GetAccountingYearEndDate(calendarYearOfAnchorMonth).ToCalendarDay();

            var result = new ReportingPeriod(firstDayInYear, lastDayInYear);

            return result;
        }

        private int GetCalendarYearOfAnchorMonth(
            int fiscalYear)
        {
            int result;

            if (this.FiscalYearNumberingConvention == FiftyTwoFiftyThreeWeekFiscalYearNumberingConvention.CalendarYearOfAnchorMonth)
            {
                result = fiscalYear;
            }
            else if (this.FiscalYearNumberingConvention == FiftyTwoFiftyThreeWeekFiscalYearNumberingConvention.CalendarYearPriorToAnchorMonth)
            {
                result = fiscalYear + 1;
            }
            else
            {
                throw new NotSupportedException(Invariant($"This {nameof(this.FiscalYearNumberingConvention)} is not supported: {this.FiscalYearNumberingConvention}."));
            }

            return result;
        }

        private DateTime GetAccountingYearEndDate(
            int year)
        {
            var result = new DateTime(year, (int)this.AnchorMonth, DateTime.DaysInMonth(year, (int)this.AnchorMonth));

            if (this.Methodology == FiftyTwoFiftyThreeWeekMethodology.LastOccurrenceInAnchorMonth)
            {
                if (result.DayOfWeek != this.LastDayOfWeekInFiscalYear)
                {
                    result = result.Previous(this.LastDayOfWeekInFiscalYear);
                }
            }
            else if (this.Methodology == FiftyTwoFiftyThreeWeekMethodology.ClosestToLastDayOfAnchorMonth)
            {
                if (result.DayOfWeek != this.LastDayOfWeekInFiscalYear)
                {
                    var prior = result.Previous(this.LastDayOfWeekInFiscalYear);
                    var next = result.Next(this.LastDayOfWeekInFiscalYear);
                    var priorDaysDifference = (result - prior).TotalDays;
                    var nextDaysDifference = (next - result).TotalDays;
                    result = priorDaysDifference < nextDaysDifference ? prior : next;
                }
            }
            else
            {
                throw new NotSupportedException("This methodology is not supported: " + this.Methodology);
            }

            return result;
        }
    }
}