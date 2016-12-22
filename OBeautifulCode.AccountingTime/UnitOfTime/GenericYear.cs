// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GenericYear.cs" company="OBeautifulCode">
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
    /// Represents a generic year.
    /// </summary>
    [Serializable]
    public class GenericYear : GenericUnitOfTime, IAmAConcreteUnitOfTime, IAmBoundedTime, IHaveAYear, IEquatable<GenericYear>, IComparable<GenericYear>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericYear"/> class.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="year"/> is less than 1 or greater than 9999.</exception>
        public GenericYear(int year)
        {
            if ((year < 1) || (year > 9999))
            {
                throw new ArgumentOutOfRangeException(nameof(year), "year is less than 1 or greater than 9999");
            }

            this.Year = year;
        }

        // ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

        /// <inheritdoc />
        public int Year { get; private set; }

        // ReSharper restore AutoPropertyCanBeMadeGetOnly.Local

        /// <inheritdoc />
        public override UnitOfTimeGranularity UnitOfTimeGranularity => UnitOfTimeGranularity.Year;

        /// <summary>
        /// Determines whether two objects of type <see cref="GenericYear" /> are equal.
        /// </summary>
        /// <param name="left">The first year to compare.</param>
        /// <param name="right">The second year to compare.</param>
        /// <returns>true if the two years are equal; false otherwise.</returns>
        public static bool operator ==(GenericYear left, GenericYear right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            {
                return false;
            }

            var result = left.Year == right.Year;
            return result;
        }

        /// <summary>
        /// Determines whether two objects of type <see cref="GenericYear" /> are not equal.
        /// </summary>
        /// <param name="left">The first year to compare.</param>
        /// <param name="right">The second year to compare.</param>
        /// <returns>true if the two years are not equal; false otherwise.</returns>
        public static bool operator !=(GenericYear left, GenericYear right) => !(left == right);

        /// <summary>
        /// Determines whether a year is less than another year.
        /// </summary>
        /// <param name="left">The left-hand year to compare.</param>
        /// <param name="right">The right-hand year to compare.</param>
        /// <returns>true if the the left-hand year is less than the right-hand year; false otherwise.</returns>
        public static bool operator <(GenericYear left, GenericYear right)
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
        /// Determines whether a year is greater than another year.
        /// </summary>
        /// <param name="left">The left-hand year to compare.</param>
        /// <param name="right">The right-hand year to compare.</param>
        /// <returns>true if the the left-hand year is greater than the right-hand year; false otherwise.</returns>
        public static bool operator >(GenericYear left, GenericYear right)
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
        /// Determines whether a year is less than or equal to than another year.
        /// </summary>
        /// <param name="left">The left-hand year to compare.</param>
        /// <param name="right">The right-hand year to compare.</param>
        /// <returns>true if the the left-hand year is less than or equal to the right-hand year; false otherwise.</returns>
        public static bool operator <=(GenericYear left, GenericYear right) => (left == right) || (left < right);

        /// <summary>
        /// Determines whether a year is greater than or equal to than another year.
        /// </summary>
        /// <param name="left">The left-hand year to compare.</param>
        /// <param name="right">The right-hand year to compare.</param>
        /// <returns>true if the the left-hand year is greater than or equal to the right-hand year; false otherwise.</returns>
        public static bool operator >=(GenericYear left, GenericYear right) => (left == right) || (left > right);

        /// <summary>
        /// Determines whether the specified <see cref="GenericYear"/> is equal to this one.
        /// </summary>
        /// <param name="other">The year to compare with this one.</param>
        /// <returns>
        /// true if this year is equal to the specified year; false otherwise.
        /// </returns>
        public bool Equals(GenericYear other) => this == other;

        /// <summary>
        /// Determines whether the specified object is equal to this one, as per <see cref="Equals(GenericYear)"/>.
        /// </summary>
        /// <param name="obj">The value to compare this one with.</param>
        /// <returns>
        /// true if the other object is a year equal to this one; false otherwise, consistent with <see cref="Equals(GenericYear)"/>
        /// </returns>
        public override bool Equals(object obj) => this == (obj as GenericYear);

        /// <summary>
        /// Compares the current instance with another object of the same type
        /// and returns an integer that indicates whether the current instance precedes,
        /// follows, or occurs in the same position in the sort order as the other object
        /// </summary>
        /// <param name="other">A <see cref="GenericYear"/> to compare to this instance.</param>
        /// <returns>
        /// -1 if the current instance is less than other.
        /// 0 if the current instance is equal to the other.
        /// 1 if the current instance is greater than the other.
        /// </returns>
        public int CompareTo(GenericYear other)
        {
            if (other == null)
            {
                return 1;
            }

            return this.Year.CompareTo(other.Year);
        }

        /// <summary>
        /// Compares the current instance with another object and returns an integer
        /// that indicates whether the current instance precedes,
        /// follows, or occurs in the same position in the sort order as the other object
        /// </summary>
        /// <param name="obj">An object to compare to this instance.</param>
        /// <returns>
        /// -1 if the current instance is less than other.
        /// 0 if the current instance is equal to the other.
        /// 1 if the current instance is greater than the other.
        /// </returns>
        /// <exception cref="ArgumentException"><paramref name="obj"/> is not of type <see cref="GenericYear"/>.</exception>
        public override int CompareTo(object obj)
        {
            var other = obj as GenericYear;
            if (other == null)
            {
                throw new ArgumentException("object is not a generic year");
            }

            return this.CompareTo(other);
        }

        /// <inheritdoc />
        public override int GetHashCode() =>
            HashCodeHelper.Initialize()
                // ReSharper disable NonReadonlyMemberInGetHashCode
                .Hash(this.Year)
                .Value;
                // ReSharper restore NonReadonlyMemberInGetHashCode

        /// <summary>
        /// Gets a friendly representation of this year.
        /// </summary>
        /// <returns>
        /// A friendly representation of this year (e.g. 2017)
        /// </returns>
        public override string ToString()
        {
            return Invariant($"{this.Year:D4}");
        }

        /// <inheritdoc />
        public override UnitOfTime Clone()
        {
            var clone = new GenericYear(this.Year);
            return clone;
        }
    }
}

// ReSharper restore CheckNamespace