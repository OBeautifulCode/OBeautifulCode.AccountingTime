// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfTime.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Represents a unit of time, such as a month, quarter, or year.
    /// </summary>
    [Serializable]
    [SuppressMessage("Microsoft.Design", "CA1036:OverrideMethodsOnComparableTypes", Justification = "Two abstract units-of-time cannot be compared.")]
    public abstract class UnitOfTime : ICloneable, IComparable, IEquatable<UnitOfTime>, IComparable<UnitOfTime>
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
        /// Determines whether two objects of type <see cref="UnitOfTime" /> are equal.
        /// </summary>
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>true if the two units-of-time are equal; false otherwise.</returns>
        public static bool operator ==(
            UnitOfTime left,
            UnitOfTime right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            {
                return false;
            }

            var result = left.Equals((object)right);

            return result;
        }

        /// <summary>
        /// Determines whether two objects of type <see cref="UnitOfTime" /> are not equal.
        /// </summary>
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>true if the two units-of-time are not equal; false otherwise.</returns>
        public static bool operator !=(
            UnitOfTime left,
            UnitOfTime right)
            => !(left == right);

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
        public bool Equals(
            UnitOfTime other)
            => this == other;

        /// <summary>
        /// Determines whether the specified object is equal to this one, as per <see cref="Equals(UnitOfTime)"/>.
        /// </summary>
        /// <param name="obj">The value to compare this one with.</param>
        /// <returns>
        /// true if the other object is a unit-of-time of the same concrete type and equal to this one; false otherwise, consistent with <see cref="Equals(UnitOfTime)"/>.
        /// </returns>
        public abstract override bool Equals(
            object obj);

        /// <summary>
        /// Returns the hash code for this unit-of-time.
        /// </summary>
        /// <returns>
        /// The hash code for this unit-of-time.
        /// </returns>
        public abstract override int GetHashCode();

        /// <inheritdoc />
        public object Clone() => this.DeepClone();

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

        /// <summary>
        /// Deep clones a unit-of-time.
        /// </summary>
        /// <typeparam name="T">The type of unit-of-time to return.</typeparam>
        /// <returns>
        /// A deep clone of the specified unit-of-time.
        /// </returns>
        /// <exception cref="InvalidOperationException">The cloned object cannot be casted to the specified generic type.</exception>
        public T DeepClone<T>()
            where T : UnitOfTime
        {
            var clone = this.DeepClone();

            var cloneType = clone.GetType();
            var returnType = typeof(T);
            if (!returnType.IsAssignableFrom(cloneType))
            {
                throw new InvalidOperationException("The cloned object cannot be cast to the specified generic type.");
            }

            var result = clone as T;

            return result;
        }

        /// <summary>
        /// Deep clones this unit-of-time.
        /// </summary>
        /// <returns>
        /// A deep clone of this unit-of-time.
        /// </returns>
        public abstract UnitOfTime DeepClone();

        /// <summary>
        /// Gets a friendly representation of this unit-of-time.
        /// </summary>
        /// <returns>
        /// A friendly representation of this unit-of-time.
        /// </returns>
        public abstract override string ToString();
    }
}
