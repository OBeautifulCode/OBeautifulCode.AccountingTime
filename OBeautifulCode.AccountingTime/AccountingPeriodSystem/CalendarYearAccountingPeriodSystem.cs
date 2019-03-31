// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalendarYearAccountingPeriodSystem.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    using OBeautifulCode.Math.Recipes;
    using OBeautifulCode.Validation.Recipes;

    /// <summary>
    /// A calendar year is 12 consecutive months beginning on January 1st and ending on December 31st.
    /// </summary>
    [Serializable]
    public class CalendarYearAccountingPeriodSystem : AccountingPeriodSystem, IEquatable<CalendarYearAccountingPeriodSystem>
    {
        /// <summary>
        /// Determines whether two objects of type <see cref="CalendarYearAccountingPeriodSystem"/> are equal.
        /// </summary>
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>True if the two items are equal; false otherwise.</returns>
        public static bool operator ==(
            CalendarYearAccountingPeriodSystem left,
            CalendarYearAccountingPeriodSystem right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            {
                return false;
            }

            var result = true;

            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            return result;
        }

        /// <summary>
        /// Determines whether two objects of type <see cref="CalendarYearAccountingPeriodSystem"/> are not equal.
        /// </summary>
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>True if the two items not equal; false otherwise.</returns>
        public static bool operator !=(
            CalendarYearAccountingPeriodSystem left,
            CalendarYearAccountingPeriodSystem right)
            => !(left == right);

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException"><paramref name="fiscalYear"/> is null.</exception>
        public override ReportingPeriod<CalendarDay> GetReportingPeriodForFiscalYear(
            FiscalYear fiscalYear)
        {
            new { fiscalYear }.Must().NotBeNull();

            var januaryFirst = new CalendarDay(fiscalYear.Year, MonthOfYear.January, DayOfMonth.One);
            var decemberThirtyFirst = new CalendarDay(fiscalYear.Year, MonthOfYear.December, DayOfMonth.ThirtyOne);

            var result = new ReportingPeriod<CalendarDay>(januaryFirst, decemberThirtyFirst);

            return result;
        }

        /// <inheritdoc />
        public bool Equals(CalendarYearAccountingPeriodSystem other) => this == other;

        /// <inheritdoc />
        public override bool Equals(object obj) => this == (obj as CalendarYearAccountingPeriodSystem);

        /// <inheritdoc />
        public override int GetHashCode() =>
            HashCodeHelper.Initialize()
                .Value;

        /// <inheritdoc />
        public override AccountingPeriodSystem DeepClone()
        {
            var result = new CalendarYearAccountingPeriodSystem();

            return result;
        }
    }
}
