// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GenericMonth.cs" company="OBeautifulCode">
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
        public GenericMonth(int year, MonthNumber monthNumber)
        {
            if ((year < 1) || (year > 9999))
            {
                throw new ArgumentOutOfRangeException(nameof(year), "year is less than 1 or greater than 9999");
            }

            if (monthNumber == MonthNumber.Invalid)
            {
                throw new ArgumentException("month is invalid", nameof(monthNumber));
            }

            this.Year = year;
            this.MonthNumber = monthNumber;
        }

        // ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

        /// <inheritdoc />
        public int Year { get; private set; }

        /// <inheritdoc />
        public MonthNumber MonthNumber { get; private set; }

        // ReSharper restore AutoPropertyCanBeMadeGetOnly.Local

        /// <inheritdoc />
        public override UnitOfTimeGranularity UnitOfTimeGranularity => UnitOfTimeGranularity.Month;

        /// <summary>
        /// Determines whether two objects of type <see cref="GenericMonth" /> are equal.
        /// </summary>
        /// <param name="left">The first month to compare.</param>
        /// <param name="right">The second month to compare.</param>
        /// <returns>true if the two months are equal; false otherwise.</returns>
        public static bool operator ==(GenericMonth left, GenericMonth right)
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
        /// <param name="left">The first month to compare.</param>
        /// <param name="right">The second month to compare.</param>
        /// <returns>true if the two months are not equal; false otherwise.</returns>
        public static bool operator !=(GenericMonth left, GenericMonth right) => !(left == right);

        /// <summary>
        /// Determines whether a month is less than another month.
        /// </summary>
        /// <param name="left">The left-hand month to compare.</param>
        /// <param name="right">The right-hand month to compare.</param>
        /// <returns>true if the the left-hand month is less than the right-hand month; false otherwise.</returns>
        public static bool operator <(GenericMonth left, GenericMonth right)
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
        /// <param name="left">The left-hand month to compare.</param>
        /// <param name="right">The right-hand month to compare.</param>
        /// <returns>true if the the left-hand month is greater than the right-hand month; false otherwise.</returns>
        public static bool operator >(GenericMonth left, GenericMonth right)
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
        /// <param name="left">The left-hand month to compare.</param>
        /// <param name="right">The right-hand month to compare.</param>
        /// <returns>true if the the left-hand month is less than or equal to the right-hand month; false otherwise.</returns>
        public static bool operator <=(GenericMonth left, GenericMonth right) => (left == right) || (left < right);

        /// <summary>
        /// Determines whether a month is greater than or equal to than another month.
        /// </summary>
        /// <param name="left">The left-hand month to compare.</param>
        /// <param name="right">The right-hand month to compare.</param>
        /// <returns>true if the the left-hand month is greater than or equal to the right-hand month; false otherwise.</returns>
        public static bool operator >=(GenericMonth left, GenericMonth right) => (left == right) || (left > right);

        /// <summary>
        /// Determines whether the specified <see cref="GenericMonth"/> is equal to this one.
        /// </summary>
        /// <param name="other">The month to compare with this one.</param>
        /// <returns>
        /// true if this month is equal to the specified month; false otherwise.
        /// </returns>
        public bool Equals(GenericMonth other) => this == other;

        /// <inheritdoc />
        public override bool Equals(object obj) => this == (obj as GenericMonth);

        /// <summary>
        /// Compares the current instance with another object of the same type
        /// and returns an integer that indicates whether the current instance precedes,
        /// follows, or occurs in the same position in the sort order as the other object
        /// </summary>
        /// <param name="other">A <see cref="GenericMonth"/> to compare to this instance.</param>
        /// <returns>
        /// -1 if the current instance is less than other.
        /// 0 if the current instance is equal to the other.
        /// 1 if the current instance is greater than the other.
        /// </returns>
        public int CompareTo(GenericMonth other)
        {
            if (other == null)
            {
                return 1;
            }

            var thisDay = new DateTime(this.Year, (int)this.MonthNumber, 1);
            var otherDay = new DateTime(other.Year, (int)other.MonthNumber, 1);
            return thisDay.CompareTo(otherDay);
        }

        /// <inheritdoc />
        public override int CompareTo(object obj)
        {
            var other = obj as GenericMonth;
            if (other == null)
            {
                throw new ArgumentException("object is not a generic month");
            }

            return this.CompareTo(other);
        }

        /// <inheritdoc />
        public override int GetHashCode() =>
            HashCodeHelper.Initialize()
                // ReSharper disable NonReadonlyMemberInGetHashCode
                .Hash(this.MonthNumber)
                .Hash(this.Year)
                .Value;
                // ReSharper restore NonReadonlyMemberInGetHashCode

        /// <inheritdoc />
        public override UnitOfTime Clone()
        {
            var clone = new GenericMonth(this.Year, this.MonthNumber);
            return clone;
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

            return Invariant($"{(int)this.MonthNumber}{monthNumberSuffix} month of {this.Year:D4}");
        }
    }
}

// ReSharper restore CheckNamespace