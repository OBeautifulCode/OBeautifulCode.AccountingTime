// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FiftyTwoFiftyThreeWeekAccountingPeriodSystem.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.DateTime.Recipes;
    using OBeautifulCode.Type;

    /// <summary>
    /// This system is used by companies that want that their accounting year always end on the same day of the week.
    /// </summary>
    public partial class FiftyTwoFiftyThreeWeekAccountingPeriodSystem : AccountingPeriodSystem, IModelViaCodeGen
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
            new { anchorMonth }.AsArg().Must().NotBeEqualTo(MonthOfYear.Invalid);
            new { fiftyTwoFiftyThreeWeekMethodology }.AsArg().Must().NotBeEqualTo(FiftyTwoFiftyThreeWeekMethodology.Unknown);

            this.LastDayOfWeekInAccountingYear = lastDayOfWeekInAccountingYear;
            this.AnchorMonth = anchorMonth;
            this.FiftyTwoFiftyThreeWeekMethodology = fiftyTwoFiftyThreeWeekMethodology;
        }

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

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException"><paramref name="fiscalYear"/> is null.</exception>
        [SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", MessageId = "year-1", Justification = "Overflow is impossible given constraint on year.")]
        public override ReportingPeriod GetReportingPeriodForFiscalYear(
            FiscalYear fiscalYear)
        {
            new { fiscalYear }.AsArg().Must().NotBeNull();

            var firstDayInYear = this.GetAccountingYearEndDate(fiscalYear.Year - 1).AddDays(1).ToCalendarDay();

            var lastDayInYear = this.GetAccountingYearEndDate(fiscalYear.Year).ToCalendarDay();

            var result = new ReportingPeriod(firstDayInYear, lastDayInYear);

            return result;
        }

        private DateTime GetAccountingYearEndDate(
            int year)
        {
            var result = new DateTime(year, (int)this.AnchorMonth, DateTime.DaysInMonth(year, (int)this.AnchorMonth));

            if (this.FiftyTwoFiftyThreeWeekMethodology == FiftyTwoFiftyThreeWeekMethodology.LastOccurrenceInAnchorMonth)
            {
                if (result.DayOfWeek != this.LastDayOfWeekInAccountingYear)
                {
                    result = result.Previous(this.LastDayOfWeekInAccountingYear);
                }
            }
            else if (this.FiftyTwoFiftyThreeWeekMethodology == FiftyTwoFiftyThreeWeekMethodology.ClosestToLastDayOfAnchorMonth)
            {
                if (result.DayOfWeek != this.LastDayOfWeekInAccountingYear)
                {
                    var prior = result.Previous(this.LastDayOfWeekInAccountingYear);
                    var next = result.Next(this.LastDayOfWeekInAccountingYear);
                    var priorDaysDifference = (result - prior).TotalDays;
                    var nextDaysDifference = (next - result).TotalDays;
                    result = priorDaysDifference < nextDaysDifference ? prior : next;
                }
            }
            else
            {
                throw new NotSupportedException("This methodology is not supported: " + this.FiftyTwoFiftyThreeWeekMethodology);
            }

            return result;
        }
    }
}