// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GenericYear.cs" company="OBeautifulCode">
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
        public GenericYear(
            int year)
        {
            new { year }.Must().BeGreaterThanOrEqualTo(1).And().BeLessThanOrEqualTo(9999);

            this.Year = year;
        }

        /// <inheritdoc />
        public int Year { get; }

        /// <inheritdoc />
        public override UnitOfTimeGranularity UnitOfTimeGranularity => UnitOfTimeGranularity.Year;

        /// <summary>
        /// Determines whether two objects of type <see cref="GenericYear" /> are equal.
        /// </summary>
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>true if the two years are equal; false otherwise.</returns>
        public static bool operator ==(
            GenericYear left,
            GenericYear right)
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
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>true if the two years are not equal; false otherwise.</returns>
        public static bool operator !=(
            GenericYear left,
            GenericYear right)
            => !(left == right);

        /// <summary>
        /// Determines whether a year is less than another year.
        /// </summary>
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>true if the the left-hand year is less than the right-hand year; false otherwise.</returns>
        public static bool operator <(
            GenericYear left,
            GenericYear right)
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
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>true if the the left-hand year is greater than the right-hand year; false otherwise.</returns>
        public static bool operator >(
            GenericYear left,
            GenericYear right)
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
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>true if the the left-hand year is less than or equal to the right-hand year; false otherwise.</returns>
        public static bool operator <=(
            GenericYear left,
            GenericYear right)
            => (left == right) || (left < right);

        /// <summary>
        /// Determines whether a year is greater than or equal to than another year.
        /// </summary>
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>true if the the left-hand year is greater than or equal to the right-hand year; false otherwise.</returns>
        public static bool operator >=(
            GenericYear left,
            GenericYear right)
            => (left == right) || (left > right);

        /// <inheritdoc />
        public bool Equals(
            GenericYear other) => this == other;

        /// <inheritdoc />
        public override bool Equals(
            object obj) => this == (obj as GenericYear);

        /// <inheritdoc />
        public int CompareTo(
            GenericYear other)
        {
            if (other == null)
            {
                return 1;
            }

            var result = this.Year.CompareTo(other.Year);

            return result;
        }

        /// <inheritdoc />
        public override int CompareTo(
            object obj)
        {
            var other = obj as GenericYear;
            if (other == null)
            {
                throw new ArgumentException("object is not a generic year");
            }

            var result = this.CompareTo(other);

            return result;
        }

        /// <inheritdoc />
        public override int GetHashCode() =>
            HashCodeHelper.Initialize()
                .Hash(this.UnitOfTimeKind)
                .Hash(this.UnitOfTimeGranularity)
                .Hash(this.Year)
                .Value;

        /// <inheritdoc />
        public override UnitOfTime Clone()
        {
            var result = new GenericYear(this.Year);

            return result;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            var result = Invariant($"{this.Year:D4}");

            return result;
        }
    }
}
