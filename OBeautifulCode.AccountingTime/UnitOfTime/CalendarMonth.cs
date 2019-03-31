// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalendarMonth.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    using OBeautifulCode.Math.Recipes;

    using OBeautifulCode.Validation.Recipes;

    using static System.FormattableString;

    /// <summary>
    /// Represents a calendar month in a specified year.
    /// </summary>
    [Serializable]
    public class CalendarMonth : CalendarUnitOfTime, IAmAConcreteUnitOfTime, IAmBoundedTime, IHaveAMonth, IEquatable<CalendarMonth>, IComparable<CalendarMonth>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CalendarMonth"/> class.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="monthOfYear">The month of the year.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="year"/> is less than 1 or greater than 9999.</exception>
        /// <exception cref="ArgumentException"><paramref name="monthOfYear"/> is invalid.</exception>
        public CalendarMonth(
            int year,
            MonthOfYear monthOfYear)
        {
            new { year }.Must().BeGreaterThanOrEqualTo(1).And().BeLessThanOrEqualTo(9999);
            new { monthOfYear }.Must().NotBeEqualTo(MonthOfYear.Invalid);

            this.Year = year;
            this.MonthOfYear = monthOfYear;
        }

        /// <inheritdoc />
        public int Year { get; }

        /// <summary>
        /// Gets the month of the year.
        /// </summary>
        public MonthOfYear MonthOfYear { get; }

        /// <inheritdoc />
        public MonthNumber MonthNumber => (MonthNumber)(int)this.MonthOfYear;

        /// <inheritdoc />
        public override UnitOfTimeGranularity UnitOfTimeGranularity => UnitOfTimeGranularity.Month;

        /// <summary>
        /// Determines whether two objects of type <see cref="CalendarMonth" /> are equal.
        /// </summary>
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>true if the two months are equal; false otherwise.</returns>
        public static bool operator ==(
            CalendarMonth left,
            CalendarMonth right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            {
                return false;
            }

            var result = (left.MonthOfYear == right.MonthOfYear) && (left.Year == right.Year);

            return result;
        }

        /// <summary>
        /// Determines whether two objects of type <see cref="CalendarMonth" /> are not equal.
        /// </summary>
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>true if the two months are not equal; false otherwise.</returns>
        public static bool operator !=(
            CalendarMonth left,
            CalendarMonth right)
            => !(left == right);

        /// <summary>
        /// Determines whether a month is less than another month.
        /// </summary>
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>true if the the left-hand month is less than the right-hand month; false otherwise.</returns>
        public static bool operator <(
            CalendarMonth left,
            CalendarMonth right)
        {
            if (ReferenceEquals(left, right))
            {
                return false;
            }

            if (ReferenceEquals(left, null))
            {
                return true;
            }

            var result = left.CompareTo(right) < 0;

            return result;
        }

        /// <summary>
        /// Determines whether a month is greater than another month.
        /// </summary>
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>true if the the left-hand month is greater than the right-hand month; false otherwise.</returns>
        public static bool operator >(
            CalendarMonth left,
            CalendarMonth right)
        {
            if (ReferenceEquals(left, right))
            {
                return false;
            }

            if (ReferenceEquals(left, null))
            {
                return false;
            }

            var result = left.CompareTo(right) > 0;

            return result;
        }

        /// <summary>
        /// Determines whether a month is less than or equal to than another month.
        /// </summary>
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>true if the the left-hand month is less than or equal to the right-hand month; false otherwise.</returns>
        public static bool operator <=(
            CalendarMonth left,
            CalendarMonth right)
            => (left == right) || (left < right);

        /// <summary>
        /// Determines whether a month is greater than or equal to than another month.
        /// </summary>
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>true if the the left-hand month is greater than or equal to the right-hand month; false otherwise.</returns>
        public static bool operator >=(
            CalendarMonth left,
            CalendarMonth right)
            => (left == right) || (left > right);

        /// <inheritdoc />
        public bool Equals(
            CalendarMonth other)
            => this == other;

        /// <inheritdoc />
        public override bool Equals(
            object obj)
            => this == (obj as CalendarMonth);

        /// <inheritdoc />
        public int CompareTo(
            CalendarMonth other)
        {
            if (other == null)
            {
                return 1;
            }

            var thisDay = new DateTime(this.Year, (int)this.MonthOfYear, 1);
            var otherDay = new DateTime(other.Year, (int)other.MonthOfYear, 1);

            var result = thisDay.CompareTo(otherDay);

            return result;
        }

        /// <inheritdoc />
        public override int CompareTo(
            object obj)
        {
            var other = obj as CalendarMonth;
            if (other == null)
            {
                throw new ArgumentException("object is not a calendar month", nameof(obj));
            }

            var result = this.CompareTo(other);

            return result;
        }

        /// <inheritdoc />
        public override int GetHashCode() =>
            HashCodeHelper.Initialize()
                .Hash(this.UnitOfTimeKind)
                .Hash(this.UnitOfTimeGranularity)
                .Hash(this.MonthOfYear)
                .Hash(this.Year)
                .Value;

        /// <inheritdoc />
        public override UnitOfTime DeepClone()
        {
            var result = new CalendarMonth(this.Year, this.MonthOfYear);

            return result;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            var result = Invariant($"{this.Year:D4}-{(int)this.MonthOfYear:D2}");

            return result;
        }
    }
}
