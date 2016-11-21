// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FiftyTwoFiftyThreeWeekAccountingPeriodSystem.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime
{
    using System;

    /// <summary>
    /// This system is used by companies that want that their accounting year always end on the same day of the week.
    /// </summary>
    public class FiftyTwoFiftyThreeWeekAccountingPeriodSystem : AccountingPeriodSystem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FiftyTwoFiftyThreeWeekAccountingPeriodSystem"/> class.
        /// </summary>
        /// <param name="lastDayOfWeekInAccountingYear">The day of the week that the fiscal year always ends on.</param>
        /// <param name="anchorMonth">The month that the fiscal year end is anchored to.  See <see cref="FiftyTwoFiftyThreeWeekMethodology"/>.</param>
        /// <param name="fiftyTwoFiftyThreeWeekMethodology">The methodology used to identify the last day of the accounting year.</param>
        public FiftyTwoFiftyThreeWeekAccountingPeriodSystem(DayOfWeek lastDayOfWeekInAccountingYear, MonthOfYear anchorMonth, FiftyTwoFiftyThreeWeekMethodology fiftyTwoFiftyThreeWeekMethodology)
        {
            this.LastDayOfWeekInAccountingYear = lastDayOfWeekInAccountingYear;
            this.AnchorMonth = anchorMonth;
            this.FiftyTwoFiftyThreeWeekMethodology = fiftyTwoFiftyThreeWeekMethodology;
        }

        // ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

        /// <summary>
        /// Gets the day of the week that the fiscal year always ends on.
        /// </summary>
        public DayOfWeek LastDayOfWeekInAccountingYear { get; private set; }

        /// <summary>
        /// Gets the month that the fiscal year end is anchored to.
        /// See <see cref="FiftyTwoFiftyThreeWeekMethodology"/>.
        /// </summary>
        public MonthOfYear AnchorMonth { get; private set; }

        /// <summary>
        /// Gets the methodology used to identify the last day of the accounting year.
        /// </summary>
        public FiftyTwoFiftyThreeWeekMethodology FiftyTwoFiftyThreeWeekMethodology { get; private set; }

        // ReSharper restore AutoPropertyCanBeMadeGetOnly.Local

        /// <inheritdoc />
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", MessageId = "year-1", Justification = "Overflow is impossible given constraint on year.")]
        public override ReportingPeriodInclusive<CalendarDay> GetReportingPeriodForFiscalYear(FiscalYear fiscalYear)
        {
            // validate here
            // year.Requires(nameof(year)).IsGreaterOrEqual(1900).IsLessOrEqual(3000);

            var firstDayInYear = this.GetAccountingYearEndDate(fiscalYear.Year - 1).AddDays(1).ToCalendarDay();
            var lastDayInYear = this.GetAccountingYearEndDate(fiscalYear.Year).ToCalendarDay();
            var result = new ReportingPeriodInclusive<CalendarDay>(firstDayInYear, lastDayInYear);
            return result;
        }

        private DateTime GetAccountingYearEndDate(int year)
        {
            var lastDayInYear = new DateTime(year, (int)this.AnchorMonth, DateTime.DaysInMonth(year, (int)this.AnchorMonth));
            if (this.FiftyTwoFiftyThreeWeekMethodology == FiftyTwoFiftyThreeWeekMethodology.LastOccuranceInAnchorMonth)
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

// ReSharper restore CheckNamespace
