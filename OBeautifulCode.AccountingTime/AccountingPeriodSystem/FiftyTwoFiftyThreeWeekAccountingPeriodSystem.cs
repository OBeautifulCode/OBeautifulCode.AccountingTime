// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FiftyTwoFiftyThreeWeekAccountingPeriodSystem.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    /// <summary>
    /// This system is used by companies that want that their accounting year always end on the same day of the week.
    /// </summary>
    [Serializable]
    public class FiftyTwoFiftyThreeWeekAccountingPeriodSystem : AccountingPeriodSystem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FiftyTwoFiftyThreeWeekAccountingPeriodSystem"/> class.
        /// </summary>
        /// <param name="lastDayOfWeekInAccountingYear">The day of the week that the fiscal year always ends on.</param>
        /// <param name="anchorMonth">The month that the fiscal year end is anchored to.  See <see cref="FiftyTwoFiftyThreeWeekMethodology"/>.</param>
        /// <param name="fiftyTwoFiftyThreeWeekMethodology">The methodology used to identify the last day of the accounting year.</param>
        /// <exception cref="ArgumentException"><paramref name="anchorMonth"/> is invalid.</exception>
        public FiftyTwoFiftyThreeWeekAccountingPeriodSystem(
            DayOfWeek lastDayOfWeekInAccountingYear,
            MonthOfYear anchorMonth,
            FiftyTwoFiftyThreeWeekMethodology fiftyTwoFiftyThreeWeekMethodology)
        {
            if (anchorMonth == MonthOfYear.Invalid)
            {
                throw new ArgumentException("anchor month is invalid", nameof(anchorMonth));
            }

            this.LastDayOfWeekInAccountingYear = lastDayOfWeekInAccountingYear;
            this.AnchorMonth = anchorMonth;
            this.FiftyTwoFiftyThreeWeekMethodology = fiftyTwoFiftyThreeWeekMethodology;
        }

        /// <summary>
        /// Gets the day of the week that the fiscal year always ends on.
        /// </summary>
        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        public DayOfWeek LastDayOfWeekInAccountingYear { get; private set; }

        /// <summary>
        /// Gets the month that the fiscal year end is anchored to.
        /// See <see cref="FiftyTwoFiftyThreeWeekMethodology"/>.
        /// </summary>
        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        public MonthOfYear AnchorMonth { get; private set; }

        /// <summary>
        /// Gets the methodology used to identify the last day of the accounting year.
        /// </summary>
        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        public FiftyTwoFiftyThreeWeekMethodology FiftyTwoFiftyThreeWeekMethodology { get; private set; }

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException"><paramref name="fiscalYear"/> is null.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", MessageId = "year-1", Justification = "Overflow is impossible given constraint on year.")]
        public override ReportingPeriod<CalendarDay> GetReportingPeriodForFiscalYear(
            FiscalYear fiscalYear)
        {
            if (fiscalYear == null)
            {
                throw new ArgumentNullException(nameof(fiscalYear));
            }

            var firstDayInYear = this.GetAccountingYearEndDate(fiscalYear.Year - 1).AddDays(1).ToCalendarDay();
            var lastDayInYear = this.GetAccountingYearEndDate(fiscalYear.Year).ToCalendarDay();
            var result = new ReportingPeriod<CalendarDay>(firstDayInYear, lastDayInYear);
            return result;
        }

        private DateTime GetAccountingYearEndDate(
            int year)
        {
            var lastDayInYear = new DateTime(year, (int)this.AnchorMonth, DateTime.DaysInMonth(year, (int)this.AnchorMonth));
            if (this.FiftyTwoFiftyThreeWeekMethodology == FiftyTwoFiftyThreeWeekMethodology.LastOccurrenceInAnchorMonth)
            {
                if (lastDayInYear.DayOfWeek != this.LastDayOfWeekInAccountingYear)
                {
                    lastDayInYear = lastDayInYear.Previous(this.LastDayOfWeekInAccountingYear);
                }
            }
            else if (this.FiftyTwoFiftyThreeWeekMethodology == FiftyTwoFiftyThreeWeekMethodology.ClosestToLastDayOfAnchorMonth)
            {
                if (lastDayInYear.DayOfWeek != this.LastDayOfWeekInAccountingYear)
                {
                    var prior = lastDayInYear.Previous(this.LastDayOfWeekInAccountingYear);
                    var next = lastDayInYear.Next(this.LastDayOfWeekInAccountingYear);
                    var priorDaysDifference = (lastDayInYear - prior).TotalDays;
                    var nextDaysDifference = (next - lastDayInYear).TotalDays;
                    lastDayInYear = priorDaysDifference < nextDaysDifference ? prior : next;
                }
            }
            else
            {
                throw new NotSupportedException("This methodology is not supported: " + this.FiftyTwoFiftyThreeWeekMethodology);
            }

            return lastDayInYear;
        }
    }
}