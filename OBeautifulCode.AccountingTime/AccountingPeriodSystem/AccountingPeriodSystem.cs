// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountingPeriodSystem.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// Represents a system for defining an entity's annual accounting period.
    /// </summary>
    [Serializable]
    public abstract class AccountingPeriodSystem : IEquatable<AccountingPeriodSystem>, IDeepCloneable<AccountingPeriodSystem>
    {
        /// <summary>
        /// Determines whether two objects of type <see cref="AccountingPeriodSystem"/> are equal.
        /// </summary>
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>True if the two items are equal; false otherwise.</returns>
        public static bool operator ==(
            AccountingPeriodSystem left,
            AccountingPeriodSystem right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            {
                return false;
            }

            var result = left.Equals((object)right);

            return result;
        }

        /// <summary>
        /// Determines whether two objects of type <see cref="AccountingPeriodSystem"/> are not equal.
        /// </summary>
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>True if the two items not equal; false otherwise.</returns>
        public static bool operator !=(
            AccountingPeriodSystem left,
            AccountingPeriodSystem right)
            => !(left == right);

        /// <summary>
        /// Gets the reporting period, in calendar days, for the specified fiscal year.
        /// </summary>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <returns>
        /// Returns the reporting period, in calendar days, for the specified fiscal year.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="fiscalYear"/> is null.</exception>
        public abstract ReportingPeriod<CalendarDay> GetReportingPeriodForFiscalYear(
            FiscalYear fiscalYear);

        /// <inheritdoc />
        public abstract override int GetHashCode();

        /// <inheritdoc />
        public abstract AccountingPeriodSystem DeepClone();

        /// <inheritdoc />
        public abstract override bool Equals(
            object obj);

        /// <inheritdoc />
        public bool Equals(
            AccountingPeriodSystem other)
            => this == other;

        /// <inheritdoc />
        public object Clone() => this.DeepClone();
    }
}
