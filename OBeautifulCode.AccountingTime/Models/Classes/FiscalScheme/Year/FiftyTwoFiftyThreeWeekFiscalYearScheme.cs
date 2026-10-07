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
        /// <param name="anchorMonth">
        /// The calendar month that determines the end of the fiscal year: the fiscal year ends on <paramref name="lastDayOfWeekInFiscalYear"/>,
        /// either where it last occurs in this month or where it falls nearest the last day of this month (see <paramref name="yearEndMethodology"/>).
        /// So the fiscal year can end a few days into the following month.
        /// </param>
        /// <param name="yearEndMethodology">The methodology used to identify the last day of the fiscal year.</param>
        /// <param name="fiscalYearNumberingConvention">OPTIONAL convention that determines which calendar year is used to number the fiscal year.  DEFAULT is to use the calendar year of the anchor month.</param>
        public FiftyTwoFiftyThreeWeekFiscalYearScheme(
            DayOfWeek lastDayOfWeekInFiscalYear,
            MonthOfYear anchorMonth,
            FiftyTwoFiftyThreeWeekYearEndMethodology yearEndMethodology,
            FiftyTwoFiftyThreeWeekFiscalYearNumberingConvention fiscalYearNumberingConvention = FiftyTwoFiftyThreeWeekFiscalYearNumberingConvention.CalendarYearOfAnchorMonth)
        {
            if (anchorMonth == MonthOfYear.Invalid)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(anchorMonth)}' == '{MonthOfYear.Invalid}'"), (Exception)null);
            }

            if (yearEndMethodology == FiftyTwoFiftyThreeWeekYearEndMethodology.Unknown)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(yearEndMethodology)}' == '{FiftyTwoFiftyThreeWeekYearEndMethodology.Unknown}'"), (Exception)null);
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
            this.YearEndMethodology = yearEndMethodology;
            this.FiscalYearNumberingConvention = fiscalYearNumberingConvention;
        }

        /// <summary>
        /// Gets the day of the week that the fiscal year always ends on.
        /// </summary>
        public DayOfWeek LastDayOfWeekInFiscalYear { get; private set; }

        /// <summary>
        /// Gets the calendar month that determines the end of the fiscal year: the fiscal year ends on <see cref="LastDayOfWeekInFiscalYear"/>,
        /// either where it last occurs in this month or where it falls nearest the last day of this month (see <see cref="YearEndMethodology"/>).
        /// So the fiscal year can end a few days into the following month.
        /// </summary>
        public MonthOfYear AnchorMonth { get; private set; }

        /// <summary>
        /// Gets the methodology used to identify the last day of the fiscal year.
        /// </summary>
        public FiftyTwoFiftyThreeWeekYearEndMethodology YearEndMethodology { get; private set; }

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

            if (this.YearEndMethodology == FiftyTwoFiftyThreeWeekYearEndMethodology.LastOccurrenceInAnchorMonth)
            {
                if (result.DayOfWeek != this.LastDayOfWeekInFiscalYear)
                {
                    result = result.Previous(this.LastDayOfWeekInFiscalYear);
                }
            }
            else if (this.YearEndMethodology == FiftyTwoFiftyThreeWeekYearEndMethodology.ClosestToLastDayOfAnchorMonth)
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
                throw new NotSupportedException("This year-end methodology is not supported: " + this.YearEndMethodology);
            }

            return result;
        }
    }
}