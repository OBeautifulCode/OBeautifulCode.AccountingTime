// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfTime.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    using System.Diagnostics.CodeAnalysis;

    using OBeautifulCode.Type;

    /// <summary>
    /// Represents a unit of time, such as a month, quarter, or year.
    /// </summary>
    [Serializable]
    [SuppressMessage("Microsoft.Design", "CA1036:OverrideMethodsOnComparableTypes", Justification = "Two abstract units-of-time cannot be compared.")]
    public abstract class UnitOfTime : IModelViaCodeGen, IComparable, IComparable<UnitOfTime>
    {
        /// <summary>
        /// Gets the kind of the unit-of-time.
        /// </summary>
        public abstract UnitOfTimeKind UnitOfTimeKind { get; }

        /// <summary>
        /// Gets the granularity of the unit-of-time.
        /// </summary>
        public abstract UnitOfTimeGranularity UnitOfTimeGranularity { get; }

        /// <summary>
        /// Determines whether a unit-of-time is less than another unit-of-time.
        /// </summary>
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>true if the the left-hand unit-of-time is less than the right-hand unit-of-time; false otherwise.</returns>
        public static bool operator <(
            UnitOfTime left,
            UnitOfTime right)
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
        /// Determines whether a unit-of-time is greater than another unit-of-time.
        /// </summary>
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>true if the the left-hand unit-of-time is greater than the right-hand unit-of-time; false otherwise.</returns>
        public static bool operator >(
            UnitOfTime left,
            UnitOfTime right)
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
        /// Determines whether a unit-of-time is less than or equal to than another unit-of-time.
        /// </summary>
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>true if the the left-hand unit-of-time is less than or equal to the right-hand unit-of-time; false otherwise.</returns>
        public static bool operator <=(
            UnitOfTime left,
            UnitOfTime right)
            => (left == right) || (left < right);

        /// <summary>
        /// Determines whether a unit-of-time is greater than or equal to than another unit-of-time.
        /// </summary>
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>true if the the left-hand unit-of-time is greater than or equal to the right-hand unit-of-time; false otherwise.</returns>
        public static bool operator >=(
            UnitOfTime left,
            UnitOfTime right)
            => (left == right) || (left > right);

        /// <inheritdoc />
        public int CompareTo(UnitOfTime other)
        {
            if (other == null)
            {
                return 1;
            }

            var result = this.CompareTo((object)other);

            return result;
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentException"><paramref name="obj"/> is not of the same type as this object.</exception>
        public abstract int CompareTo(
            object obj);
    }
}
