﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GenericUnbounded.cs" company="OBeautifulCode">
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
    /// Represents an unbounded generic unit-of-time.
    /// </summary>
    [Serializable]
    public class GenericUnbounded : GenericUnitOfTime, IAmAConcreteUnitOfTime, IAmUnboundedTime, IEquatable<GenericUnbounded>
    {
        /// <inheritdoc />
        public override UnitOfTimeGranularity UnitOfTimeGranularity => UnitOfTimeGranularity.Unbounded;

        /// <summary>
        /// Determines whether two objects of type <see cref="GenericUnbounded" /> are equal.
        /// </summary>
        /// <param name="left">The first unbounded time to compare.</param>
        /// <param name="right">The second unbounded time to compare.</param>
        /// <returns>true if the two unbounded times are equal; false otherwise.</returns>
        public static bool operator ==(GenericUnbounded left, GenericUnbounded right)
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
        /// Determines whether two objects of type <see cref="GenericUnbounded" /> are not equal.
        /// </summary>
        /// <param name="left">The first unbounded time to compare.</param>
        /// <param name="right">The second unbounded time to compare.</param>
        /// <returns>true if the two unbounded times are not equal; false otherwise.</returns>
        public static bool operator !=(GenericUnbounded left, GenericUnbounded right) => !(left == right);

        /// <summary>
        /// Determines whether the specified <see cref="GenericUnbounded"/> is equal to this one.
        /// </summary>
        /// <param name="other">The unbounded time to compare with this one.</param>
        /// <returns>
        /// true if this unbounded time is equal to the specified unbounded time; false otherwise.
        /// </returns>
        public bool Equals(GenericUnbounded other) => this == other;

        /// <summary>
        /// Determines whether the specified object is equal to this one, as per <see cref="Equals(GenericUnbounded)"/>.
        /// </summary>
        /// <param name="obj">The value to compare this one with.</param>
        /// <returns>
        /// true if the other object is a unbounded time equal to this one; false otherwise, consistent with <see cref="Equals(GenericUnbounded)"/>
        /// </returns>
        public override bool Equals(object obj) => this == (obj as GenericUnbounded);

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
        /// <exception cref="ArgumentException"><paramref name="obj"/> is not of type <see cref="GenericUnbounded"/>.</exception>
        public override int CompareTo(object obj)
        {
            var other = obj as GenericUnbounded;
            if (other == null)
            {
                throw new ArgumentException("object is not an unbounded generic time");
            }

            return 0;
        }

        /// <inheritdoc />
        public override int GetHashCode() =>
            HashCodeHelper.Initialize()
                .Value;

        /// <summary>
        /// Gets a friendly representation of this unbounded time.
        /// </summary>
        /// <returns>
        /// A friendly representation of this unbounded time (e.g. CY2017)
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations", Justification = "This exception will never get thrown, it's there purely for safety.")]
        public override string ToString()
        {
            return Invariant($"generic unbounded");
        }

        /// <inheritdoc />
        public override UnitOfTime Clone()
        {
            var clone = new GenericUnbounded();
            return clone;
        }
    }
}

// ReSharper restore CheckNamespace