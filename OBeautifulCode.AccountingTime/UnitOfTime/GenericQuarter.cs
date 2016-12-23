// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GenericQuarter.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime
{
    using System;

    using Math;

    using static System.FormattableString;

    /// <summary>
    /// Represents a generic quarter of a specified year.
    /// </summary>
    [Serializable]
    public class GenericQuarter : GenericUnitOfTime, IAmAConcreteUnitOfTime, IAmBoundedTime, IHaveAQuarter, IEquatable<GenericQuarter>, IComparable<GenericQuarter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericQuarter"/> class.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="quarterNumber">The quarter number.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="year"/> is less than 1 or greater than 9999.</exception>
        /// <exception cref="ArgumentException"><paramref name="quarterNumber"/> is invalid.</exception>
        public GenericQuarter(int year, QuarterNumber quarterNumber)
        {
            if ((year < 1) || (year > 9999))
            {
                throw new ArgumentOutOfRangeException(nameof(year), "year is less than 1 or greater than 9999");
            }

            if (quarterNumber == QuarterNumber.Invalid)
            {
                throw new ArgumentException("quarter is invalid", nameof(quarterNumber));
            }

            this.Year = year;
            this.QuarterNumber = quarterNumber;
        }

        // ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

        /// <inheritdoc />
        public QuarterNumber QuarterNumber { get; private set; }

        /// <inheritdoc />
        public int Year { get; private set; }

        // ReSharper restore AutoPropertyCanBeMadeGetOnly.Local

        /// <inheritdoc />
        public override UnitOfTimeGranularity UnitOfTimeGranularity => UnitOfTimeGranularity.Quarter;

        /// <summary>
        /// Determines whether two objects of type <see cref="GenericQuarter" /> are equal.
        /// </summary>
        /// <param name="left">The first quarter to compare.</param>
        /// <param name="right">The second quarter to compare.</param>
        /// <returns>true if the two quarters are equal; false otherwise.</returns>
        public static bool operator ==(GenericQuarter left, GenericQuarter right)
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
        /// Determines whether two objects of type <see cref="GenericQuarter" /> are not equal.
        /// </summary>
        /// <param name="left">The first quarter to compare.</param>
        /// <param name="right">The second quarter to compare.</param>
        /// <returns>true if the two quarters are not equal; false otherwise.</returns>
        public static bool operator !=(GenericQuarter left, GenericQuarter right) => !(left == right);

        /// <summary>
        /// Determines whether a quarter is less than another quarter.
        /// </summary>
        /// <param name="left">The left-hand quarter to compare.</param>
        /// <param name="right">The right-hand quarter to compare.</param>
        /// <returns>true if the the left-hand quarter is less than the right-hand quarter; false otherwise.</returns>
        public static bool operator <(GenericQuarter left, GenericQuarter right)
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
        public static bool operator >(GenericQuarter left, GenericQuarter right)
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
        public static bool operator <=(GenericQuarter left, GenericQuarter right) => (left == right) || (left < right);

        /// <summary>
        /// Determines whether a quarter is greater than or equal to than another quarter.
        /// </summary>
        /// <param name="left">The left-hand quarter to compare.</param>
        /// <param name="right">The right-hand quarter to compare.</param>
        /// <returns>true if the the left-hand quarter is greater than or equal to the right-hand quarter; false otherwise.</returns>
        public static bool operator >=(GenericQuarter left, GenericQuarter right) => (left == right) || (left > right);

        /// <summary>
        /// Determines whether the specified <see cref="GenericQuarter"/> is equal to this one.
        /// </summary>
        /// <param name="other">The quarter to compare with this one.</param>
        /// <returns>
        /// true if this quarter is equal to the specified quarter; false otherwise.
        /// </returns>
        public bool Equals(GenericQuarter other) => this == other;

        /// <inheritdoc />
        public override bool Equals(object obj) => this == (obj as GenericQuarter);

        /// <summary>
        /// Compares the current instance with another object of the same type
        /// and returns an integer that indicates whether the current instance precedes,
        /// follows, or occurs in the same position in the sort order as the other object
        /// </summary>
        /// <param name="other">A <see cref="GenericQuarter"/> to compare to this instance.</param>
        /// <returns>
        /// -1 if the current instance is less than other.
        /// 0 if the current instance is equal to the other.
        /// 1 if the current instance is greater than the other.
        /// </returns>
        public int CompareTo(GenericQuarter other)
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
        public override int CompareTo(object obj)
        {
            var other = obj as GenericQuarter;
            if (other == null)
            {
                throw new ArgumentException("object is not a generic quarter");
            }

            return this.CompareTo(other);
        }

        /// <inheritdoc />
        public override int GetHashCode() =>
            HashCodeHelper.Initialize()
                // ReSharper disable NonReadonlyMemberInGetHashCode
                .Hash(this.QuarterNumber)
                .Hash(this.Year)
                .Value;
                // ReSharper restore NonReadonlyMemberInGetHashCode

        /// <inheritdoc />
        public override UnitOfTime Clone()
        {
            var clone = new GenericQuarter(this.Year, this.QuarterNumber);
            return clone;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Invariant($"{(int)this.QuarterNumber}Q{this.Year:D4}");
        }
    }
}

// ReSharper restore CheckNamespace