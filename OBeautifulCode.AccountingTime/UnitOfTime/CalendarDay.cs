// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalendarDay.cs" company="OBeautifulCode">
//    Copyright (c) OBeautifulCode 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    using Math;

    using static System.FormattableString;

    /// <summary>
    /// Represents a calendar day.
    /// </summary>
    [Serializable]
    // ReSharper disable once InheritdocConsiderUsage
    public class CalendarDay : CalendarUnitOfTime, IAmAConcreteUnitOfTime, IAmBoundedTime, IHaveAMonth, IEquatable<CalendarDay>, IComparable<CalendarDay>, IFormattable
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
        public CalendarDay(
            int year,
            MonthOfYear monthOfYear,
            DayOfMonth dayOfMonth)
        {
            if ((year < 1) || (year > 9999))
            {
                throw new ArgumentOutOfRangeException(nameof(year), Invariant($"year ({year}) is less than 1 or greater than 9999"));
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
                throw new ArgumentException(Invariant($"day ({dayOfMonth}) is not a valid day in the specified month ({monthOfYear}) and year ({year})"), nameof(dayOfMonth));
            }

            this.Year = year;
            this.MonthOfYear = monthOfYear;
            this.DayOfMonth = dayOfMonth;
        }

        /// <inheritdoc />
        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        public int Year { get; private set; }

        /// <summary>
        /// Gets the month of the year.
        /// </summary>
        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        public MonthOfYear MonthOfYear { get; private set; }

        /// <summary>
        /// Gets the day of the month.
        /// </summary>
        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        public DayOfMonth DayOfMonth { get; private set; }

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
        public static bool operator ==(
            CalendarDay left,
            CalendarDay right)
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
        public static bool operator !=(
            CalendarDay left,
            CalendarDay right)
            => !(left == right);

        /// <summary>
        /// Determines whether a day is less than another day.
        /// </summary>
        /// <param name="left">The left-hand day to compare.</param>
        /// <param name="right">The right-hand day to compare.</param>
        /// <returns>true if the the left-hand day is less than the right-hand day; false otherwise.</returns>
        public static bool operator <(
            CalendarDay left,
            CalendarDay right)
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
        public static bool operator >(
            CalendarDay left,
            CalendarDay right)
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
        public static bool operator <=(
            CalendarDay left,
            CalendarDay right)
            => (left == right) || (left < right);

        /// <summary>
        /// Determines whether a day is greater than or equal to than another day.
        /// </summary>
        /// <param name="left">The left-hand day to compare.</param>
        /// <param name="right">The right-hand day to compare.</param>
        /// <returns>true if the the left-hand day is greater than or equal to the right-hand day; false otherwise.</returns>
        public static bool operator >=(
            CalendarDay left,
            CalendarDay right)
            => (left == right) || (left > right);

        /// <inheritdoc />
        public bool Equals(
            CalendarDay other)
            => this == other;

        /// <inheritdoc />
        public override bool Equals(
            object obj)
            => this == (obj as CalendarDay);

        /// <inheritdoc />
        public int CompareTo(
            CalendarDay other)
        {
            if (other == null)
            {
                return 1;
            }

            var thisDay = new DateTime(this.Year, (int)this.MonthOfYear, (int)this.DayOfMonth);
            var otherDay = new DateTime(other.Year, (int)other.MonthOfYear, (int)other.DayOfMonth);
            return thisDay.CompareTo(otherDay);
        }

        /// <inheritdoc />
        public override int CompareTo(
            object obj)
        {
            var other = obj as CalendarDay;
            if (other == null)
            {
                throw new ArgumentException("object is not a calendar day", nameof(obj));
            }

            return this.CompareTo(other);
        }

        /// <inheritdoc />
        public override int GetHashCode() =>
            HashCodeHelper.Initialize()
                .Hash(this.UnitOfTimeKind)
                .Hash(this.UnitOfTimeGranularity)
                .Hash(this.Year)
                .Hash(this.MonthOfYear)
                .Hash(this.DayOfMonth)
                .Value;

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
        public override UnitOfTime Clone()
        {
            var clone = new CalendarDay(this.Year, this.MonthOfYear, this.DayOfMonth);
            return clone;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Invariant($"{this.Year:D4}-{(int)this.MonthNumber:D2}-{(int)this.DayOfMonth:D2}");
        }

        /// <inheritdoc />
        public string ToString(
            string format,
            IFormatProvider formatProvider = null)
        {
            var dateTime = this.ToDateTime();
            var result = dateTime.ToString(format, formatProvider);
            return result;
        }
    }
}
