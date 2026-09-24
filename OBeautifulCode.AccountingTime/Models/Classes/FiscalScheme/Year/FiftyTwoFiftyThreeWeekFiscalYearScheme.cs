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
        public FiftyTwoFiftyThreeWeekFiscalYearScheme(
            DayOfWeek lastDayOfWeekInFiscalYear,
            MonthOfYear anchorMonth,
            FiftyTwoFiftyThreeWeekMethodology methodology)
        {
            if (anchorMonth == MonthOfYear.Invalid)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(anchorMonth)}' == '{MonthOfYear.Invalid}'"), (Exception)null);
            }

            if (methodology == FiftyTwoFiftyThreeWeekMethodology.Unknown)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(methodology)}' == '{FiftyTwoFiftyThreeWeekMethodology.Unknown}'"), (Exception)null);
            }

            this.LastDayOfWeekInFiscalYear = lastDayOfWeekInFiscalYear;
            this.AnchorMonth = anchorMonth;
            this.Methodology = methodology;
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

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException"><paramref name="fiscalYear"/> is null.</exception>
        [SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", MessageId = "year-1", Justification = "Overflow is impossible given constraint on year.")]
        public override ReportingPeriod GetCalendarDayPeriod(
            FiscalYear fiscalYear)
        {
            if (fiscalYear == null)
            {
                throw new ArgumentNullException(nameof(fiscalYear));
            }

            var firstDayInYear = this.GetAccountingYearEndDate(fiscalYear.Year - 1).AddDays(1).ToCalendarDay();

            var lastDayInYear = this.GetAccountingYearEndDate(fiscalYear.Year).ToCalendarDay();

            var result = new ReportingPeriod(firstDayInYear, lastDayInYear);

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