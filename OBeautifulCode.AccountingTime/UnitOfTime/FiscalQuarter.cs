// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FiscalQuarter.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime
{
    using System;

    using Math;

    /// <summary>
    /// Represents a fiscal quarter of a specified year.
    /// </summary>
    public class FiscalQuarter : FiscalUnitOfTime, IHaveAQuarter, IEquatable<FiscalQuarter>, IComparable<FiscalQuarter>, IComparable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FiscalQuarter"/> class.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="quarterNumber">The quarter number.</param>
        /// <exception cref="ArgumentException"><paramref name="quarterNumber"/> is invalid.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="year"/> is not between 1900 and 3000 (inclusive).</exception>
        public FiscalQuarter(int year, QuarterNumber quarterNumber)
        {
            // validate here
            // if ((quarter < 1) || (quarter > 4))
            // {
            //     throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Quarter must be between 1 and 4 (inclusive).  QuarterNumber provided was: {0}", quarter));
            // }

            this.Year = year;
            this.QuarterNumber = quarterNumber;
        }

        // ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

        /// <inheritdoc />
        public QuarterNumber QuarterNumber { get; private set; }

        /// <inheritdoc />
        public int Year { get; private set; }

        // ReSharper restore AutoPropertyCanBeMadeGetOnly.Local

        /// <summary>
        /// Determines whether two objects of type <see cref="FiscalQuarter" /> are equal.
        /// </summary>
        /// <param name="left">The first quarter to compare.</param>
        /// <param name="right">The second quarter to compare.</param>
        /// <returns>true if the two quarters are equal; false otherwise.</returns>
        public static bool operator ==(FiscalQuarter left, FiscalQuarter right)
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
        /// <param name="left">The first quarter to compare.</param>
        /// <param name="right">The second quarter to compare.</param>
        /// <returns>true if the two quarters are not equal; false otherwise.</returns>
        public static bool operator !=(FiscalQuarter left, FiscalQuarter right) => !(left == right);

        /// <summary>
        /// Determines whether a quarter is less than another quarter.
        /// </summary>
        /// <param name="left">The left-hand quarter to compare.</param>
        /// <param name="right">The right-hand quarter to compare.</param>
        /// <returns>true if the the left-hand quarter is less than the right-hand quarter; false otherwise.</returns>
        public static bool operator <(FiscalQuarter left, FiscalQuarter right) => left.CompareTo(right) < 0;

        /// <summary>
        /// Determines whether a quarter is greater than another quarter.
        /// </summary>
        /// <param name="left">The left-hand quarter to compare.</param>
        /// <param name="right">The right-hand quarter to compare.</param>
        /// <returns>true if the the left-hand quarter is greater than the right-hand quarter; false otherwise.</returns>
        public static bool operator >(FiscalQuarter left, FiscalQuarter right) => left.CompareTo(right) > 0;

        /// <summary>
        /// Determines whether a quarter is less than or equal to than another quarter.
        /// </summary>
        /// <param name="left">The left-hand quarter to compare.</param>
        /// <param name="right">The right-hand quarter to compare.</param>
        /// <returns>true if the the left-hand quarter is less than or equal to the right-hand quarter; false otherwise.</returns>
        public static bool operator <=(FiscalQuarter left, FiscalQuarter right) => (left == right) || (left < right);

        /// <summary>
        /// Determines whether a quarter is greater than or equal to than another quarter.
        /// </summary>
        /// <param name="left">The left-hand quarter to compare.</param>
        /// <param name="right">The right-hand quarter to compare.</param>
        /// <returns>true if the the left-hand quarter is greater than or equal to the right-hand quarter; false otherwise.</returns>
        public static bool operator >=(FiscalQuarter left, FiscalQuarter right) => (left == right) || (left > right);

        /// <summary>
        /// Determines whether the specified <see cref="FiscalQuarter"/> is equal to this one.
        /// </summary>
        /// <param name="other">The quarter to compare with this one.</param>
        /// <returns>
        /// true if this quarter is equal to the specified quarter; false otherwise.
        /// </returns>
        public bool Equals(FiscalQuarter other) => this == other;

        /// <summary>
        /// Determines whether the specified object is equal to this one, as per <see cref="Equals(FiscalQuarter)"/>.
        /// </summary>
        /// <param name="obj">The value to compare this one with.</param>
        /// <returns>
        /// true if the other object is a quarter equal to this one; false otherwise, consistent with <see cref="Equals(FiscalQuarter)"/>
        /// </returns>
        public override bool Equals(object obj) => this == (obj as FiscalQuarter);

        /// <summary>
        /// Compares the current instance with another object of the same type
        /// and returns an integer that indicates whether the current instance precedes,
        /// follows, or occurs in the same position in the sort order as the other object
        /// </summary>
        /// <param name="other">A <see cref="FiscalQuarter"/> to compare to this instance.</param>
        /// <returns>
        /// -1 if the current instance is less than other.
        /// 0 if the current instance is equal to the other.
        /// 1 if the current instance is greater than the other.
        /// </returns>
        public int CompareTo(FiscalQuarter other)
        {
            if (other == null)
            {
                return 1;
            }

            var thisDay = new DateTime(this.Year, (int)this.QuarterNumber, 1);
            var otherDay = new DateTime(other.Year, (int)other.QuarterNumber, 1);
            return thisDay.CompareTo(otherDay);
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
        /// <exception cref="ArgumentException"><paramref name="obj"/> is not of type <see cref="FiscalQuarter"/>.</exception>
        public int CompareTo(object obj)
        {
            var other = obj as FiscalQuarter;
            if (other == null)
            {
                throw new ArgumentException("the specified object is not a quarter");
            }

            return this.CompareTo(other);
        }

        /// <summary>
        /// Returns the hash code for this trigger.
        /// </summary>
        /// <returns>The hash code for this trigger.</returns>
        public override int GetHashCode() =>
            HashCodeHelper.Initialize()
                // ReSharper disable NonReadonlyMemberInGetHashCode
                .Hash(this.QuarterNumber)
                .Hash(this.Year)
                .Value;
        // ReSharper restore NonReadonlyMemberInGetHashCode

        /// <summary>
        /// Gets a friendly representation of this quarter.
        /// </summary>
        /// <returns>
        /// quarter in xQy format, where x is the quarter number and y is the year (e.g. 3Q2017)
        /// </returns>
        public override string ToString()
        {
            return $"{this.QuarterNumber}Q{this.Year}";
        }
    }
}

// ReSharper restore CheckNamespace