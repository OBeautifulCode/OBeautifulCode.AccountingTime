// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FiscalUnbounded.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    using static System.FormattableString;

    /// <summary>
    /// Represents an unbounded fiscal unit-of-time.
    /// </summary>
    [Serializable]
    public class FiscalUnbounded : FiscalUnitOfTime, IAmAConcreteUnitOfTime, IAmUnboundedTime, IComparable<FiscalUnbounded>
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
            FiscalUnbounded left,
            FiscalUnbounded right)
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
            FiscalUnbounded left,
            FiscalUnbounded right)
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
            FiscalUnbounded left,
            FiscalUnbounded right)
            => (left == right) || (left < right);

        /// <summary>
        /// Determines whether a unbounded is greater than or equal to than another unbounded.
        /// </summary>
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>true if the the left-hand unbounded is greater than or equal to the right-hand unbounded; false otherwise.</returns>
        public static bool operator >=(
            FiscalUnbounded left,
            FiscalUnbounded right)
            => (left == right) || (left > right);

        /// <inheritdoc />
        public int CompareTo(
            FiscalUnbounded other)
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
            var other = obj as FiscalUnbounded;

            if (other == null)
            {
                throw new ArgumentException("object is not an unbounded fiscal time");
            }

            return 0;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            var result = Invariant($"fiscal unbounded");

            return result;
        }
    }
}
