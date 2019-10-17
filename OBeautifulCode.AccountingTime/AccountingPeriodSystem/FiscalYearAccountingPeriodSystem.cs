// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FiscalYearAccountingPeriodSystem.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Equality.Recipes;

    /// <summary>
    /// A fiscal year is 12 consecutive months ending on the last day of any month except December 31st.
    /// </summary>
    [Serializable]
    public class FiscalYearAccountingPeriodSystem : AccountingPeriodSystem, IEquatable<FiscalYearAccountingPeriodSystem>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FiscalYearAccountingPeriodSystem"/> class.
        /// </summary>
        /// <param name="lastMonthInFiscalYear">The last month of the fiscal year.</param>
        /// <exception cref="ArgumentException"><paramref name="lastMonthInFiscalYear"/> is invalid.</exception>
        /// <exception cref="ArgumentException"><paramref name="lastMonthInFiscalYear"/> is <see cref="MonthOfYear.December"/>.</exception>
        public FiscalYearAccountingPeriodSystem(
            MonthOfYear lastMonthInFiscalYear)
        {
            new { lastMonthInFiscalYear }.AsArg().Must().NotBeEqualTo(MonthOfYear.Invalid).And().NotBeEqualTo(MonthOfYear.December);

            this.LastMonthInFiscalYear = lastMonthInFiscalYear;
        }

        /// <summary>
        /// Gets the last month of the fiscal year.
        /// </summary>
        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        public MonthOfYear LastMonthInFiscalYear { get; private set; }

        /// <summary>
        /// Determines whether two objects of type <see cref="FiscalYearAccountingPeriodSystem"/> are equal.
        /// </summary>
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>True if the two items are equal; false otherwise.</returns>
        public static bool operator ==(
            FiscalYearAccountingPeriodSystem left,
            FiscalYearAccountingPeriodSystem right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            {
                return false;
            }

            var result = left.LastMonthInFiscalYear == right.LastMonthInFiscalYear;

            return result;
        }

        /// <summary>
        /// Determines whether two objects of type <see cref="FiscalYearAccountingPeriodSystem"/> are not equal.
        /// </summary>
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>True if the two items not equal; false otherwise.</returns>
        public static bool operator !=(
            FiscalYearAccountingPeriodSystem left,
            FiscalYearAccountingPeriodSystem right)
            => !(left == right);

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException"><paramref name="fiscalYear"/> is null.</exception>
        public override ReportingPeriod<CalendarDay> GetReportingPeriodForFiscalYear(
            FiscalYear fiscalYear)
        {
            new { fiscalYear }.AsArg().Must().NotBeNull();

            var lastDayInEndingMonth = DateTime.DaysInMonth(fiscalYear.Year, (int)this.LastMonthInFiscalYear);
            var lastDayInFiscalYear = new DateTime(fiscalYear.Year, (int)this.LastMonthInFiscalYear, lastDayInEndingMonth);
            var firstDayInFiscalYear = lastDayInFiscalYear.AddDays(1).AddYears(-1);

            var result = new ReportingPeriod<CalendarDay>(firstDayInFiscalYear.ToCalendarDay(), lastDayInFiscalYear.ToCalendarDay());

            return result;
        }

        /// <inheritdoc />
        public bool Equals(FiscalYearAccountingPeriodSystem other) => this == other;

        /// <inheritdoc />
        public override bool Equals(object obj) => this == (obj as FiscalYearAccountingPeriodSystem);

        /// <inheritdoc />
        public override int GetHashCode() =>
            HashCodeHelper.Initialize()
                .Hash(this.LastMonthInFiscalYear)
                .Value;

        /// <inheritdoc />
        public override AccountingPeriodSystem DeepClone()
        {
            var result = new FiscalYearAccountingPeriodSystem(this.LastMonthInFiscalYear);

            return result;
        }
    }
}
