// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalendarUnbounded.cs" company="OBeautifulCode">
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
    /// Represents an unbounded calendar unit-of-time.
    /// </summary>
    [Serializable]
    public class CalendarUnbounded : CalendarUnitOfTime, IAmAConcreteUnitOfTime, IAmUnboundedTime, IEquatable<CalendarUnbounded>, IComparable<CalendarUnbounded>
    {
        /// <inheritdoc />
        public override UnitOfTimeGranularity UnitOfTimeGranularity => UnitOfTimeGranularity.Unbounded;

        /// <summary>
        /// Determines whether two objects of type <see cref="CalendarUnbounded" /> are equal.
        /// </summary>
        /// <param name="left">The first unbounded time to compare.</param>
        /// <param name="right">The second unbounded time to compare.</param>
        /// <returns>true if the two unbounded times are equal; false otherwise.</returns>
        public static bool operator ==(CalendarUnbounded left, CalendarUnbounded right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Determines whether two objects of type <see cref="CalendarUnbounded" /> are not equal.
        /// </summary>
        /// <param name="left">The first unbounded time to compare.</param>
        /// <param name="right">The second unbounded time to compare.</param>
        /// <returns>true if the two unbounded times are not equal; false otherwise.</returns>
        public static bool operator !=(CalendarUnbounded left, CalendarUnbounded right) => !(left == right);

        /// <summary>
        /// Determines whether a unbounded is less than another unbounded.
        /// </summary>
        /// <param name="left">The left-hand unbounded to compare.</param>
        /// <param name="right">The right-hand unbounded to compare.</param>
        /// <returns>true if the the left-hand unbounded is less than the right-hand unbounded; false otherwise.</returns>
        public static bool operator <(CalendarUnbounded left, CalendarUnbounded right)
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
        /// Determines whether a unbounded is greater than another unbounded.
        /// </summary>
        /// <param name="left">The left-hand unbounded to compare.</param>
        /// <param name="right">The right-hand unbounded to compare.</param>
        /// <returns>true if the the left-hand unbounded is greater than the right-hand unbounded; false otherwise.</returns>
        public static bool operator >(CalendarUnbounded left, CalendarUnbounded right)
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
        /// Determines whether a unbounded is less than or equal to than another unbounded.
        /// </summary>
        /// <param name="left">The left-hand unbounded to compare.</param>
        /// <param name="right">The right-hand unbounded to compare.</param>
        /// <returns>true if the the left-hand unbounded is less than or equal to the right-hand unbounded; false otherwise.</returns>
        public static bool operator <=(CalendarUnbounded left, CalendarUnbounded right) => (left == right) || (left < right);

        /// <summary>
        /// Determines whether a unbounded is greater than or equal to than another unbounded.
        /// </summary>
        /// <param name="left">The left-hand unbounded to compare.</param>
        /// <param name="right">The right-hand unbounded to compare.</param>
        /// <returns>true if the the left-hand unbounded is greater than or equal to the right-hand unbounded; false otherwise.</returns>
        public static bool operator >=(CalendarUnbounded left, CalendarUnbounded right) => (left == right) || (left > right);

        /// <summary>
        /// Determines whether the specified <see cref="CalendarUnbounded"/> is equal to this one.
        /// </summary>
        /// <param name="other">The unbounded time to compare with this one.</param>
        /// <returns>
        /// true if this unbounded time is equal to the specified unbounded time; false otherwise.
        /// </returns>
        public bool Equals(CalendarUnbounded other) => this == other;

        /// <inheritdoc />
        public override bool Equals(object obj) => this == (obj as CalendarUnbounded);

        /// <summary>
        /// Compares the current instance with another object of the same type
        /// and returns an integer that indicates whether the current instance precedes,
        /// follows, or occurs in the same position in the sort order as the other object
        /// </summary>
        /// <param name="other">A <see cref="CalendarUnbounded"/> to compare to this instance.</param>
        /// <returns>
        /// 0 if the other instance is not null.
        /// 1 if the other instance is null.
        /// -1 is never returned.
        /// </returns>
        public int CompareTo(CalendarUnbounded other)
        {
            if (other == null)
            {
                return 1;
            }

            return 0;
        }

        /// <inheritdoc />
        public override int CompareTo(object obj)
        {
            var other = obj as CalendarUnbounded;
            if (other == null)
            {
                throw new ArgumentException("object is not an unbounded calendar time");
            }

            return 0;
        }

        /// <inheritdoc />
        public override int GetHashCode() =>
            HashCodeHelper.Initialize()
                .Hash(this.UnitOfTimeKind)
                .Hash(this.UnitOfTimeGranularity)
                .Value;

        /// <inheritdoc />
        public override UnitOfTime Clone()
        {
            var clone = new CalendarUnbounded();
            return clone;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Invariant($"calendar unbounded");
        }
    }
}

// ReSharper restore CheckNamespace