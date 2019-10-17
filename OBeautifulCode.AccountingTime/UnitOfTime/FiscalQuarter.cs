﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FiscalQuarter.cs" company="OBeautifulCode">
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
    /// Represents a fiscal quarter of a specified year.
    /// </summary>
    [Serializable]
    public class FiscalQuarter : FiscalUnitOfTime, IAmAConcreteUnitOfTime, IAmBoundedTime, IHaveAQuarter, IEquatable<FiscalQuarter>, IComparable<FiscalQuarter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FiscalQuarter"/> class.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="quarterNumber">The quarter number.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="year"/> is less than 1 or greater than 9999.</exception>
        /// <exception cref="ArgumentException"><paramref name="quarterNumber"/> is invalid.</exception>
        public FiscalQuarter(
            int year,
            QuarterNumber quarterNumber)
        {
            new { year }.AsArg().Must().BeGreaterThanOrEqualTo(1).And().BeLessThanOrEqualTo(9999);
            new { quarterNumber }.AsArg().Must().NotBeEqualTo(QuarterNumber.Invalid);

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
        /// Determines whether two objects of type <see cref="FiscalQuarter" /> are equal.
        /// </summary>
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>true if the two quarters are equal; false otherwise.</returns>
        public static bool operator ==(
            FiscalQuarter left,
            FiscalQuarter right)
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
        /// Determines whether two objects of type <see cref="FiscalQuarter" /> are not equal.
        /// </summary>
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>true if the two quarters are not equal; false otherwise.</returns>
        public static bool operator !=(
            FiscalQuarter left,
            FiscalQuarter right)
            => !(left == right);

        /// <summary>
        /// Determines whether a quarter is less than another quarter.
        /// </summary>
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>true if the the left-hand quarter is less than the right-hand quarter; false otherwise.</returns>
        public static bool operator <(
            FiscalQuarter left,
            FiscalQuarter right)
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
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>true if the the left-hand quarter is greater than the right-hand quarter; false otherwise.</returns>
        public static bool operator >(
            FiscalQuarter left,
            FiscalQuarter right)
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
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>true if the the left-hand quarter is less than or equal to the right-hand quarter; false otherwise.</returns>
        public static bool operator <=(
            FiscalQuarter left,
            FiscalQuarter right)
            => (left == right) || (left < right);

        /// <summary>
        /// Determines whether a quarter is greater than or equal to than another quarter.
        /// </summary>
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>true if the the left-hand quarter is greater than or equal to the right-hand quarter; false otherwise.</returns>
        public static bool operator >=(
            FiscalQuarter left,
            FiscalQuarter right)
            => (left == right) || (left > right);

        /// <inheritdoc />
        public bool Equals(
            FiscalQuarter other) => this == other;

        /// <inheritdoc />
        public override bool Equals(
            object obj) => this == (obj as FiscalQuarter);

        /// <inheritdoc />
        public int CompareTo(
            FiscalQuarter other)
        {
            if (other == null)
            {
                return 1;
            }

            var thisDay = new DateTime(this.Year, (int)this.QuarterNumber, 1);
            var otherDay = new DateTime(other.Year, (int)other.QuarterNumber, 1);

            var result = thisDay.CompareTo(otherDay);

            return result;
        }

        /// <inheritdoc />
        public override int CompareTo(
            object obj)
        {
            var other = obj as FiscalQuarter;
            if (other == null)
            {
                throw new ArgumentException("object is not a fiscal quarter");
            }

            var result = this.CompareTo(other);

            return result;
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
        public override UnitOfTime DeepClone()
        {
            var result = new FiscalQuarter(this.Year, this.QuarterNumber);

            return result;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            var result = Invariant($"{(int)this.QuarterNumber}Q{this.Year:D4}");

            return result;
        }
    }
}
