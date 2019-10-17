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
    using OBeautifulCode.Equality.Recipes;

    /// <summary>
    /// This system is used by companies that want that their accounting year always end on the same day of the week.
    /// </summary>
    [Serializable]
    public class FiftyTwoFiftyThreeWeekAccountingPeriodSystem : AccountingPeriodSystem, IEquatable<FiftyTwoFiftyThreeWeekAccountingPeriodSystem>
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

        /// <summary>
        /// Determines whether two objects of type <see cref="FiftyTwoFiftyThreeWeekAccountingPeriodSystem"/> are equal.
        /// </summary>
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>True if the two items are equal; false otherwise.</returns>
        public static bool operator ==(
            FiftyTwoFiftyThreeWeekAccountingPeriodSystem left,
            FiftyTwoFiftyThreeWeekAccountingPeriodSystem right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            {
                return false;
            }

            var result =
                (left.LastDayOfWeekInAccountingYear == right.LastDayOfWeekInAccountingYear) &&
                (left.AnchorMonth == right.AnchorMonth) &&
                (left.FiftyTwoFiftyThreeWeekMethodology == right.FiftyTwoFiftyThreeWeekMethodology);

            return result;
        }

        /// <summary>
        /// Determines whether two objects of type <see cref="FiftyTwoFiftyThreeWeekAccountingPeriodSystem"/> are not equal.
        /// </summary>
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>True if the two items not equal; false otherwise.</returns>
        public static bool operator !=(
            FiftyTwoFiftyThreeWeekAccountingPeriodSystem left,
            FiftyTwoFiftyThreeWeekAccountingPeriodSystem right)
            => !(left == right);

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException"><paramref name="fiscalYear"/> is null.</exception>
        [SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", MessageId = "year-1", Justification = "Overflow is impossible given constraint on year.")]
        public override ReportingPeriod<CalendarDay> GetReportingPeriodForFiscalYear(
            FiscalYear fiscalYear)
        {
            new { fiscalYear }.AsArg().Must().NotBeNull();

            var firstDayInYear = this.GetAccountingYearEndDate(fiscalYear.Year - 1).AddDays(1).ToCalendarDay();
            var lastDayInYear = this.GetAccountingYearEndDate(fiscalYear.Year).ToCalendarDay();

            var result = new ReportingPeriod<CalendarDay>(firstDayInYear, lastDayInYear);

            return result;
        }

        /// <inheritdoc />
        public bool Equals(FiftyTwoFiftyThreeWeekAccountingPeriodSystem other) => this == other;

        /// <inheritdoc />
        public override bool Equals(object obj) => this == (obj as FiftyTwoFiftyThreeWeekAccountingPeriodSystem);

        /// <inheritdoc />
        public override int GetHashCode() =>
            HashCodeHelper.Initialize()
                .Hash(this.LastDayOfWeekInAccountingYear)
                .Hash(this.AnchorMonth)
                .Hash(this.FiftyTwoFiftyThreeWeekMethodology)
                .Value;

        /// <inheritdoc />
        public override AccountingPeriodSystem DeepClone()
        {
            var result = new FiftyTwoFiftyThreeWeekAccountingPeriodSystem(this.LastDayOfWeekInAccountingYear, this.AnchorMonth, this.FiftyTwoFiftyThreeWeekMethodology);

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