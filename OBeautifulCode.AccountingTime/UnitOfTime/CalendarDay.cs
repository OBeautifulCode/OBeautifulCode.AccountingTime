// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalendarDay.cs" company="OBeautifulCode">
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
    /// Represents a calendar day.
    /// </summary>
    [Serializable]
    public class CalendarDay : CalendarUnitOfTime, IAmAConcreteUnitOfTime, IHaveAMonth, IEquatable<CalendarDay>, IComparable<CalendarDay>, IFormattable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CalendarDay"/> class.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="monthOfYear">The month of the year.</param>
        /// <param name="dayOfMonth">The day of the month.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="year"/> is less than 1 or greater than 9999.</exception>
        /// <exception cref="ArgumentException"><paramref name="monthOfYear"/> is invalid.</exception>
        /// <exception cref="ArgumentException"><paramref name="dayOfMonth"/> is invalid.</exception>
        /// <exception cref="ArgumentException"><paramref name="dayOfMonth"/> is not a valid day in the specified <paramref name="monthOfYear"/> and <paramref name="year"/>.</exception>
        public CalendarDay(int year, MonthOfYear monthOfYear, DayOfMonth dayOfMonth)
        {
            if ((year < 1) || (year > 9999))
            {
                throw new ArgumentOutOfRangeException(nameof(year), "year is less than 1 or greater than 9999");
            }

            if (monthOfYear == MonthOfYear.Invalid)
            {
                throw new ArgumentException("month is invalid", nameof(monthOfYear));
            }

            if (dayOfMonth == DayOfMonth.Invalid)
            {
                throw new ArgumentException("day is invalid", nameof(dayOfMonth));
            }

            var totalDaysInMonth = DateTime.DaysInMonth(year, (int)monthOfYear);
            if ((int)dayOfMonth > totalDaysInMonth)
            {
                throw new ArgumentException("day is not a valid day in the specified month and year", nameof(dayOfMonth));
            }

            this.Year = year;
            this.MonthOfYear = monthOfYear;
            this.DayOfMonth = dayOfMonth;
        }

        // ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

        /// <inheritdoc />
        public int Year { get; private set; }

        /// <summary>
        /// Gets the month of the year.
        /// </summary>
        public MonthOfYear MonthOfYear { get; private set; }

        /// <summary>
        /// Gets the day of the month.
        /// </summary>
        public DayOfMonth DayOfMonth { get; private set; }

        // ReSharper restore AutoPropertyCanBeMadeGetOnly.Local

        /// <inheritdoc />
        public MonthNumber MonthNumber => (MonthNumber)(int)this.MonthOfYear;

        /// <inheritdoc />
        public override UnitOfTimeGranularity UnitOfTimeGranularity => UnitOfTimeGranularity.Day;

        /// <summary>
        /// Determines whether two objects of type <see cref="CalendarDay" /> are equal.
        /// </summary>
        /// <param name="left">The first day to compare.</param>
        /// <param name="right">The second day to compare.</param>
        /// <returns>true if the two days are equal; false otherwise.</returns>
        public static bool operator ==(CalendarDay left, CalendarDay right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            {
                return false;
            }

            var result =
                (left.DayOfMonth == right.DayOfMonth) &&
                (left.MonthOfYear == right.MonthOfYear) &&
                (left.Year == right.Year);
            return result;
        }

        /// <summary>
        /// Determines whether two objects of type <see cref="CalendarDay" /> are not equal.
        /// </summary>
        /// <param name="left">The first day to compare.</param>
        /// <param name="right">The second day to compare.</param>
        /// <returns>true if the two days are not equal; false otherwise.</returns>
        public static bool operator !=(CalendarDay left, CalendarDay right) => !(left == right);

        /// <summary>
        /// Determines whether a day is less than another day.
        /// </summary>
        /// <param name="left">The left-hand day to compare.</param>
        /// <param name="right">The right-hand day to compare.</param>
        /// <returns>true if the the left-hand day is less than the right-hand day; false otherwise.</returns>
        public static bool operator <(CalendarDay left, CalendarDay right)
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
        /// Determines whether a day is greater than another day.
        /// </summary>
        /// <param name="left">The left-hand day to compare.</param>
        /// <param name="right">The right-hand day to compare.</param>
        /// <returns>true if the the left-hand day is greater than the right-hand day; false otherwise.</returns>
        public static bool operator >(CalendarDay left, CalendarDay right)
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
        /// Determines whether a day is less than or equal to than another day.
        /// </summary>
        /// <param name="left">The left-hand day to compare.</param>
        /// <param name="right">The right-hand day to compare.</param>
        /// <returns>true if the the left-hand day is less than or equal to the right-hand day; false otherwise.</returns>
        public static bool operator <=(CalendarDay left, CalendarDay right) => (left == right) || (left < right);

        /// <summary>
        /// Determines whether a day is greater than or equal to than another day.
        /// </summary>
        /// <param name="left">The left-hand day to compare.</param>
        /// <param name="right">The right-hand day to compare.</param>
        /// <returns>true if the the left-hand day is greater than or equal to the right-hand day; false otherwise.</returns>
        public static bool operator >=(CalendarDay left, CalendarDay right) => (left == right) || (left > right);

        /// <summary>
        /// Determines whether the specified <see cref="CalendarDay"/> is equal to this one.
        /// </summary>
        /// <param name="other">The day to compare with this one.</param>
        /// <returns>
        /// true if this day is equal to the specified day; false otherwise.
        /// </returns>
        public bool Equals(CalendarDay other) => this == other;

        /// <summary>
        /// Determines whether the specified object is equal to this one, as per <see cref="Equals(CalendarDay)"/>.
        /// </summary>
        /// <param name="obj">The value to compare this one with.</param>
        /// <returns>
        /// true if the other object is a day equal to this one; false otherwise, consistent with <see cref="Equals(CalendarDay)"/>
        /// </returns>
        public override bool Equals(object obj) => this == (obj as CalendarDay);

        /// <summary>
        /// Compares the current instance with another object of the same type
        /// and returns an integer that indicates whether the current instance precedes,
        /// follows, or occurs in the same position in the sort order as the other object
        /// </summary>
        /// <param name="other">A <see cref="CalendarDay"/> to compare to this instance.</param>
        /// <returns>
        /// -1 if the current instance is less than other.
        /// 0 if the current instance is equal to the other.
        /// 1 if the current instance is greater than the other.
        /// </returns>
        public int CompareTo(CalendarDay other)
        {
            if (other == null)
            {
                return 1;
            }

            var thisDay = new DateTime(this.Year, (int)this.MonthOfYear, (int)this.DayOfMonth);
            var otherDay = new DateTime(other.Year, (int)other.MonthOfYear, (int)other.DayOfMonth);
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
        /// <exception cref="ArgumentException"><paramref name="obj"/> is not of type <see cref="CalendarDay"/>.</exception>
        public override int CompareTo(object obj)
        {
            var other = obj as CalendarDay;
            if (other == null)
            {
                throw new ArgumentException("object is not a calendar day", nameof(obj));
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
                .Hash(this.Year)
                .Hash(this.MonthOfYear)
                .Hash(this.DayOfMonth)
                .Value;
        // ReSharper restore NonReadonlyMemberInGetHashCode

        /// <summary>
        /// Converts this calendar day to an object of type <see cref="DateTime"/>.
        /// </summary>
        /// <returns>
        /// Gets a <see cref="DateTime"/> version of this calendar day.
        /// </returns>
        public DateTime ToDateTime()
        {
            var result = new DateTime(this.Year, (int)this.MonthNumber, (int)this.DayOfMonth, 0, 0, 0, DateTimeKind.Unspecified);
            return result;
        }

        /// <inheritdoc />
        public string ToString(string format, IFormatProvider formatProvider = null)
        {
            var dateTime = this.ToDateTime();
            var result = dateTime.ToString(format, formatProvider);
            return result;
        }

        /// <summary>
        /// Gets a friendly representation of this day.
        /// </summary>
        /// <returns>
        /// day in yyyy-MM-dd format, where yyyy is the year and MM is the month number, and dd is the day of the month (e.g. 2017-10-15)
        /// </returns>
        public override string ToString()
        {
            return Invariant($"{this.Year:D4}-{(int)this.MonthNumber:D2}-{(int)this.DayOfMonth:D2}");
        }

        /// <inheritdoc />
        public override UnitOfTime Clone()
        {
            var clone = new CalendarDay(this.Year, this.MonthOfYear, this.DayOfMonth);
            return clone;
        }
    }
}

// ReSharper restore CheckNamespace