// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfTime.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Represents a unit of time, such as a month, quarter, or year.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1036:OverrideMethodsOnComparableTypes", Justification = "Two abstract units-of-time cannot be compared.")]
    [Serializable]
    [Bindable(true, BindingDirection.TwoWay)]
    public abstract class UnitOfTime : IComparable
    {
        /// <summary>
        /// Gets the kind of the unit-of-time.
        /// </summary>
        public abstract UnitOfTimeKind UnitOfTimeKind { get; }

        /// <summary>
        /// Gets the granuarlity of the unit-of-time.
        /// </summary>
        public abstract UnitOfTimeGranularity UnitOfTimeGranularity { get; }

        /// <summary>
        /// <summary>
        /// Determines whether the specified object is equal to this one, as per <see cref="Equals(UnitOfTime)"/>.
        /// </summary>
        /// <param name="obj">The value to compare this one with.</param>
        /// <returns>
        /// true if the other object is a unit-of-time of the same concrete type and equal to this one; false otherwise, consistent with <see cref="Equals(UnitOfTime)"/>
        /// </returns>
        public abstract override bool Equals(object obj);

        /// <summary>
        /// Returns the hash code for this unit-of-time.
        /// </summary>
        /// <returns>
        /// The hash code for this unit-of-time.
        /// </returns>
        public abstract override int GetHashCode();

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
        /// <exception cref="ArgumentException"><paramref name="obj"/> is not of the same type as this object.</exception>
        public abstract int CompareTo(object obj);

        /// <summary>
        /// Deep clones a unit-of-time.
        /// </summary>
        /// <typeparam name="T">The type of unit-of-time to return.</typeparam>
        /// <returns>
        /// A deep clone of the specified unit-of-time.
        /// </returns>
        /// <exception cref="InvalidOperationException">The cloned object cannot be casted to the specified generic type.</exception>
        public T Clone<T>()
            where T : UnitOfTime
        {
            var clone = this.Clone();

            var cloneType = clone.GetType();
            var returnType = typeof(T);
            if (!returnType.IsAssignableFrom(cloneType))
            {
                throw new InvalidOperationException("The cloned object cannot be casted to the specified generic type.");
            }

            return clone as T;
        }

        /// <summary>
        /// Deep clones this unit-of-time.
        /// </summary>
        /// <returns>
        /// A deep clone of this unit-of-time.
        /// </returns>
        public abstract UnitOfTime Clone();

        /// <summary>
        /// Gets a friendly representation of this unit-of-time.
        /// </summary>
        /// <returns>
        /// A friendly representation of this unit-of-time.
        /// </returns>
        public abstract override string ToString();
    }
}

// ReSharper restore CheckNamespace