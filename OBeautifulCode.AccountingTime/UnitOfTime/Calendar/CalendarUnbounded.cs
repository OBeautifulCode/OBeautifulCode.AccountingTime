// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalendarUnbounded.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    using static System.FormattableString;

    /// <summary>
    /// Represents an unbounded calendar unit-of-time.
    /// </summary>
    [Serializable]
    public class CalendarUnbounded : CalendarUnitOfTime, IAmAConcreteUnitOfTime, IAmUnboundedTime, IComparable<CalendarUnbounded>
    {
        /// <inheritdoc />
        public override UnitOfTimeGranularity UnitOfTimeGranularity => UnitOfTimeGranularity.Unbounded;

        /// <summary>
        /// Determines whether a unbounded is less than another unbounded.
        /// </summary>
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>true if the the left-hand unbounded is less than the right-hand unbounded; false otherwise.</returns>
        public static bool operator <(
            CalendarUnbounded left,
            CalendarUnbounded right)
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
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>true if the the left-hand unbounded is greater than the right-hand unbounded; false otherwise.</returns>
        public static bool operator >(
            CalendarUnbounded left,
            CalendarUnbounded right)
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
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>true if the the left-hand unbounded is less than or equal to the right-hand unbounded; false otherwise.</returns>
        public static bool operator <=(
            CalendarUnbounded left,
            CalendarUnbounded right)
            => (left == right) || (left < right);

        /// <summary>
        /// Determines whether a unbounded is greater than or equal to than another unbounded.
        /// </summary>
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>true if the the left-hand unbounded is greater than or equal to the right-hand unbounded; false otherwise.</returns>
        public static bool operator >=(
            CalendarUnbounded left,
            CalendarUnbounded right)
            => (left == right) || (left > right);

        /// <inheritdoc />
        public int CompareTo(
            CalendarUnbounded other)
        {
            if (other == null)
            {
                return 1;
            }

            return 0;
        }

        /// <inheritdoc />
        public override int CompareTo(
            object obj)
        {
            var other = obj as CalendarUnbounded;

            if (other == null)
            {
                throw new ArgumentException("object is not an unbounded calendar time");
            }

            return 0;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            var result = Invariant($"calendar unbounded");

            return result;
        }
    }
}
