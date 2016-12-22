﻿// --------------------------------------------------------------------------------------------------------------------
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
    public class CalendarUnbounded : CalendarUnitOfTime, IAmAConcreteUnitOfTime, IAmUnboundedTime, IEquatable<CalendarUnbounded>
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
        /// Determines whether the specified <see cref="CalendarUnbounded"/> is equal to this one.
        /// </summary>
        /// <param name="other">The unbounded time to compare with this one.</param>
        /// <returns>
        /// true if this unbounded time is equal to the specified unbounded time; false otherwise.
        /// </returns>
        public bool Equals(CalendarUnbounded other) => this == other;

        /// <inheritdoc />
        public override bool Equals(object obj) => this == (obj as CalendarUnbounded);

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