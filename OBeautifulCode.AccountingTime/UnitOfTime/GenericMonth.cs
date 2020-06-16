// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GenericMonth.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Equality.Recipes;

    using static System.FormattableString;

    /// <summary>
    /// Represents a generic month in a specified year.
    /// </summary>
    [Serializable]
    public class GenericMonth : GenericUnitOfTime, IAmAConcreteUnitOfTime, IAmBoundedTime, IHaveAMonth, IEquatable<GenericMonth>, IComparable<GenericMonth>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericMonth"/> class.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="monthNumber">The month number.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="year"/> is less than 1 or greater than 9999.</exception>
        /// <exception cref="ArgumentException"><paramref name="monthNumber"/> is invalid.</exception>
        public GenericMonth(
            int year,
            MonthNumber monthNumber)
        {
            new { year }.AsArg().Must().BeGreaterThanOrEqualTo(1).And().BeLessThanOrEqualTo(9999);
            new { monthNumber }.AsArg().Must().NotBeEqualTo(MonthNumber.Invalid);

            this.Year = year;
            this.MonthNumber = monthNumber;
        }

        /// <inheritdoc />
        public int Year { get; }

        /// <inheritdoc />
        public MonthNumber MonthNumber { get; }

        /// <inheritdoc />
        public override UnitOfTimeGranularity UnitOfTimeGranularity => UnitOfTimeGranularity.Month;

        /// <summary>
        /// Determines whether two objects of type <see cref="GenericMonth" /> are equal.
        /// </summary>
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>true if the two months are equal; false otherwise.</returns>
        public static bool operator ==(
            GenericMonth left,
            GenericMonth right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            {
                return false;
            }

            var result = (left.MonthNumber == right.MonthNumber) && (left.Year == right.Year);

            return result;
        }

        /// <summary>
        /// Determines whether two objects of type <see cref="GenericMonth" /> are not equal.
        /// </summary>
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>true if the two months are not equal; false otherwise.</returns>
        public static bool operator !=(
            GenericMonth left,
            GenericMonth right)
            => !(left == right);

        /// <summary>
        /// Determines whether a month is less than another month.
        /// </summary>
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>true if the the left-hand month is less than the right-hand month; false otherwise.</returns>
        public static bool operator <(
            GenericMonth left,
            GenericMonth right)
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
            GenericMonth left,
            GenericMonth right)
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
            GenericMonth left,
            GenericMonth right)
            => (left == right) || (left < right);

        /// <summary>
        /// Determines whether a month is greater than or equal to than another month.
        /// </summary>
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>true if the the left-hand month is greater than or equal to the right-hand month; false otherwise.</returns>
        public static bool operator >=(
            GenericMonth left,
            GenericMonth right)
            => (left == right) || (left > right);

        /// <inheritdoc />
        public bool Equals(
            GenericMonth other) => this == other;

        /// <inheritdoc />
        public override bool Equals(
            object obj) => this == (obj as GenericMonth);

        /// <inheritdoc />
        public int CompareTo(
            GenericMonth other)
        {
            if (other == null)
            {
                return 1;
            }

            var thisDay = new DateTime(this.Year, (int)this.MonthNumber, 1);
            var otherDay = new DateTime(other.Year, (int)other.MonthNumber, 1);

            var result = thisDay.CompareTo(otherDay);

            return result;
        }

        /// <inheritdoc />
        public override int CompareTo(
            object obj)
        {
            var other = obj as GenericMonth;
            if (other == null)
            {
                throw new ArgumentException("object is not a generic month");
            }

            var result = this.CompareTo(other);

            return result;
        }

        /// <inheritdoc />
        public override int GetHashCode() =>
            HashCodeHelper.Initialize()
                .Hash(this.UnitOfTimeKind)
                .Hash(this.UnitOfTimeGranularity)
                .Hash(this.MonthNumber)
                .Hash(this.Year)
                .Value;

        /// <inheritdoc />
        public override UnitOfTime DeepClone()
        {
            var result = new GenericMonth(this.Year, this.MonthNumber);

            return result;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            string monthNumberSuffix;
            switch (this.MonthNumber)
            {
                case MonthNumber.One:
                    monthNumberSuffix = "st";
                    break;
                case MonthNumber.Two:
                    monthNumberSuffix = "nd";
                    break;
                case MonthNumber.Three:
                    monthNumberSuffix = "rd";
                    break;
                default:
                    monthNumberSuffix = "th";
                    break;
            }

            var result = Invariant($"{(int)this.MonthNumber}{monthNumberSuffix} month of {this.Year:D4}");

            return result;
        }
    }
}
