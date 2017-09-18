// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalendarQuarter.cs" company="OBeautifulCode">
//    Copyright (c) OBeautifulCode 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    using Math;

    using static System.FormattableString;

    /// <summary>
    /// Represents a calendar quarter of a specified year.
    /// </summary>
    [Serializable]
    // ReSharper disable once InheritdocConsiderUsage
    public class CalendarQuarter : CalendarUnitOfTime, IAmAConcreteUnitOfTime, IAmBoundedTime, IHaveAQuarter, IEquatable<CalendarQuarter>, IComparable<CalendarQuarter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CalendarQuarter"/> class.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="quarterNumber">The quarter number.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="year"/> is less than 1 or greater than 9999.</exception>
        /// <exception cref="ArgumentException"><paramref name="quarterNumber"/> is invalid.</exception>
        public CalendarQuarter(
            int year,
            QuarterNumber quarterNumber)
        {
            if ((year < 1) || (year > 9999))
            {
                throw new ArgumentOutOfRangeException(nameof(year), Invariant($"year ({year}) is less than 1 or greater than 9999"));
            }

            if (quarterNumber == QuarterNumber.Invalid)
            {
                throw new ArgumentException("quarter is invalid", nameof(quarterNumber));
            }

            this.Year = year;
            this.QuarterNumber = quarterNumber;
        }

        /// <inheritdoc />
        public QuarterNumber QuarterNumber { get; }

        /// <inheritdoc />
        public int Year { get; }

        /// <inheritdoc />
        public override UnitOfTimeGranularity UnitOfTimeGranularity => UnitOfTimeGranularity.Quarter;

        /// <summary>
        /// Determines whether two objects of type <see cref="CalendarQuarter" /> are equal.
        /// </summary>
        /// <param name="left">The first quarter to compare.</param>
        /// <param name="right">The second quarter to compare.</param>
        /// <returns>true if the two quarters are equal; false otherwise.</returns>
        public static bool operator ==(
            CalendarQuarter left,
            CalendarQuarter right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            {
                return false;
            }

            var result = (left.QuarterNumber == right.QuarterNumber) && (left.Year == right.Year);
            return result;
        }

        /// <summary>
        /// Determines whether two objects of type <see cref="CalendarQuarter" /> are not equal.
        /// </summary>
        /// <param name="left">The first quarter to compare.</param>
        /// <param name="right">The second quarter to compare.</param>
        /// <returns>true if the two quarters are not equal; false otherwise.</returns>
        public static bool operator !=(
            CalendarQuarter left,
            CalendarQuarter right)
            => !(left == right);

        /// <summary>
        /// Determines whether a quarter is less than another quarter.
        /// </summary>
        /// <param name="left">The left-hand quarter to compare.</param>
        /// <param name="right">The right-hand quarter to compare.</param>
        /// <returns>true if the the left-hand quarter is less than the right-hand quarter; false otherwise.</returns>
        public static bool operator <(
            CalendarQuarter left,
            CalendarQuarter right)
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
        /// Determines whether a quarter is greater than another quarter.
        /// </summary>
        /// <param name="left">The left-hand quarter to compare.</param>
        /// <param name="right">The right-hand quarter to compare.</param>
        /// <returns>true if the the left-hand quarter is greater than the right-hand quarter; false otherwise.</returns>
        public static bool operator >(
            CalendarQuarter left,
            CalendarQuarter right)
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
        /// Determines whether a quarter is less than or equal to than another quarter.
        /// </summary>
        /// <param name="left">The left-hand quarter to compare.</param>
        /// <param name="right">The right-hand quarter to compare.</param>
        /// <returns>true if the the left-hand quarter is less than or equal to the right-hand quarter; false otherwise.</returns>
        public static bool operator <=(
            CalendarQuarter left,
            CalendarQuarter right)
            => (left == right) || (left < right);

        /// <summary>
        /// Determines whether a quarter is greater than or equal to than another quarter.
        /// </summary>
        /// <param name="left">The left-hand quarter to compare.</param>
        /// <param name="right">The right-hand quarter to compare.</param>
        /// <returns>true if the the left-hand quarter is greater than or equal to the right-hand quarter; false otherwise.</returns>
        public static bool operator >=(
            CalendarQuarter left,
            CalendarQuarter right)
            => (left == right) || (left > right);

        /// <inheritdoc />
        public bool Equals(
            CalendarQuarter other)
            => this == other;

        /// <inheritdoc />
        public override bool Equals(
            object obj)
            => this == (obj as CalendarQuarter);

        /// <inheritdoc />
        public int CompareTo(
            CalendarQuarter other)
        {
            if (other == null)
            {
                return 1;
            }

            var thisDay = new DateTime(this.Year, (int)this.QuarterNumber, 1);
            var otherDay = new DateTime(other.Year, (int)other.QuarterNumber, 1);
            return thisDay.CompareTo(otherDay);
        }

        /// <inheritdoc />
        public override int CompareTo(
            object obj)
        {
            var other = obj as CalendarQuarter;
            if (other == null)
            {
                throw new ArgumentException("obj is not a calendar quarter");
            }

            return this.CompareTo(other);
        }

        /// <inheritdoc />
        public override int GetHashCode() =>
            HashCodeHelper.Initialize()
                .Hash(this.UnitOfTimeKind)
                .Hash(this.UnitOfTimeGranularity)
                .Hash(this.QuarterNumber)
                .Hash(this.Year)
                .Value;

        /// <inheritdoc />
        public override UnitOfTime Clone()
        {
            var clone = new CalendarQuarter(this.Year, this.QuarterNumber);
            return clone;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Invariant($"{(int)this.QuarterNumber}Q{this.Year:D4}");
        }
    }
}
