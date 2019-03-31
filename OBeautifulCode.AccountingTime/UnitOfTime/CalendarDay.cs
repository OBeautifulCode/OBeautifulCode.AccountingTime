// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalendarDay.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    using OBeautifulCode.Math.Recipes;
    using OBeautifulCode.Validation.Recipes;

    using static System.FormattableString;

    /// <summary>
    /// Represents a calendar day.
    /// </summary>
    [Serializable]
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
            new { year }.Must().BeGreaterThanOrEqualTo(1).And().BeLessThanOrEqualTo(9999);
            new { monthOfYear }.Must().NotBeEqualTo(MonthOfYear.Invalid);
            new { dayOfMonth }.Must().NotBeEqualTo(DayOfMonth.Invalid);

            var totalDaysInMonth = DateTime.DaysInMonth(year, (int)monthOfYear);
            if ((int)dayOfMonth > totalDaysInMonth)
            {
                throw new ArgumentException(Invariant($"day ({dayOfMonth}) is not a valid day in the specified month ({monthOfYear}) and year ({year})."), nameof(dayOfMonth));
            }

            this.Year = year;
            this.MonthOfYear = monthOfYear;
            this.DayOfMonth = dayOfMonth;
        }

        /// <inheritdoc />
        public int Year { get; }

        /// <summary>
        /// Gets the month of the year.
        /// </summary>
        public MonthOfYear MonthOfYear { get; }

        /// <summary>
        /// Gets the day of the month.
        /// </summary>
        public DayOfMonth DayOfMonth { get; }

        /// <inheritdoc />
        public MonthNumber MonthNumber => (MonthNumber)(int)this.MonthOfYear;

        /// <inheritdoc />
        public override UnitOfTimeGranularity UnitOfTimeGranularity => UnitOfTimeGranularity.Day;

        /// <summary>
        /// Determines whether two objects of type <see cref="CalendarDay" /> are equal.
        /// </summary>
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
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
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>true if the two days are not equal; false otherwise.</returns>
        public static bool operator !=(
            CalendarDay left,
            CalendarDay right)
            => !(left == right);

        /// <summary>
        /// Determines whether a day is less than another day.
        /// </summary>
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
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
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
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
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>true if the the left-hand day is less than or equal to the right-hand day; false otherwise.</returns>
        public static bool operator <=(
            CalendarDay left,
            CalendarDay right)
            => (left == right) || (left < right);

        /// <summary>
        /// Determines whether a day is greater than or equal to than another day.
        /// </summary>
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
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

            var result = thisDay.CompareTo(otherDay);

            return result;
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

            var result = this.CompareTo(other);

            return result;
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
            var result = Invariant($"{this.Year:D4}-{(int)this.MonthNumber:D2}-{(int)this.DayOfMonth:D2}");

            return result;
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
