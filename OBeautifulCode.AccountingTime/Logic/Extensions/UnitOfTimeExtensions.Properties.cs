// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfTimeExtensions.Properties.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static System.FormattableString;

    /// <summary>
    /// Property-related extension methods on <see cref="UnitOfTime"/>.
    /// </summary>
    public static partial class UnitOfTimeExtensions
    {
        /// <summary>
        /// Gets the first calendar day in the specified calendar-based unit-of-time.
        /// </summary>
        /// <param name="unitOfTime">The unit-of-time.</param>
        /// <returns>
        /// The first calendar day in the specified calendar-based unit-of-time.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="unitOfTime"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="unitOfTime"/> is unbounded.</exception>
        public static CalendarDay GetFirstCalendarDay(
            this CalendarUnitOfTime unitOfTime)
        {
            if (unitOfTime == null)
            {
                throw new ArgumentNullException(nameof(unitOfTime));
            }

            if (unitOfTime.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded)
            {
                throw new ArgumentException(Invariant($"{nameof(unitOfTime)} is {nameof(UnitOfTimeGranularity.Unbounded)}."));
            }

            switch (unitOfTime)
            {
                case CalendarDay unitOfTimeAsCalendarDay:
                    return unitOfTimeAsCalendarDay.InternalGetFirstCalendarDay();
                case CalendarMonth unitOfTimeAsCalendarMonth:
                    return unitOfTimeAsCalendarMonth.InternalGetFirstCalendarDay();
                case CalendarQuarter unitOfTimeAsCalendarQuarter:
                    return unitOfTimeAsCalendarQuarter.InternalGetFirstCalendarDay();
                case CalendarYear unitOfTimeAsCalendarYear:
                    return unitOfTimeAsCalendarYear.InternalGetFirstCalendarDay();
                case CalendarUnbounded _:
                    throw new NotSupportedException(Invariant($"This unit-of-time is not supported: {unitOfTime}."));
            }

            throw new NotSupportedException("this type of unit-of-time is not supported: " + unitOfTime.GetType());
        }

        /// <summary>
        /// Gets the last calendar day in the specified calendar-based unit-of-time.
        /// </summary>
        /// <param name="unitOfTime">The unit-of-time.</param>
        /// <returns>
        /// The last calendar day in the specified calendar-based unit-of-time.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="unitOfTime"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="unitOfTime"/> is unbounded.</exception>
        public static CalendarDay GetLastCalendarDay(
            this CalendarUnitOfTime unitOfTime)
        {
            if (unitOfTime == null)
            {
                throw new ArgumentNullException(nameof(unitOfTime));
            }

            if (unitOfTime.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded)
            {
                throw new ArgumentException(Invariant($"{nameof(unitOfTime)} is {nameof(UnitOfTimeGranularity.Unbounded)}."));
            }

            switch (unitOfTime)
            {
                case CalendarDay unitOfTimeAsCalendarDay:
                    return unitOfTimeAsCalendarDay.InternalGetLastCalendarDay();
                case CalendarMonth unitOfTimeAsCalendarMonth:
                    return unitOfTimeAsCalendarMonth.InternalGetLastCalendarDay();
                case CalendarQuarter unitOfTimeAsCalendarQuarter:
                    return unitOfTimeAsCalendarQuarter.InternalGetLastCalendarDay();
                case CalendarYear unitOfTimeAsCalendarYear:
                    return unitOfTimeAsCalendarYear.InternalGetLastCalendarDay();
                case CalendarUnbounded _:
                    throw new NotSupportedException(Invariant($"This unit-of-time is not supported: {unitOfTime}."));
            }

            throw new NotSupportedException("this type of unit-of-time is not supported: " + unitOfTime.GetType());
        }

        /// <summary>
        /// Gets the <see cref="Unit"/> of a <see cref="UnitOfTime"/>.
        /// </summary>
        /// <param name="unitOfTime">The unit of time.</param>
        /// <returns>
        /// The corresponding <see cref="Unit"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="unitOfTime"/> is null.</exception>
        public static Unit GetUnit(
            this UnitOfTime unitOfTime)
        {
            if (unitOfTime == null)
            {
                throw new ArgumentNullException(nameof(unitOfTime));
            }

            var result = new Unit(unitOfTime.UnitOfTimeKind, unitOfTime.UnitOfTimeGranularity);

            return result;
        }

        /// <summary>
        /// Gets a list with the first <typeparamref name="T"/> in the same year as <paramref name="lastUnitOfTimeInYear"/>,
        /// up to and including <paramref name="lastUnitOfTimeInYear"/>, in sequential/ascending order.
        /// </summary>
        /// <param name="lastUnitOfTimeInYear">The last unit-of-time.</param>
        /// <typeparam name="T">The type of the unit-of-time.</typeparam>
        /// <returns>
        /// A list with the first <typeparamref name="T"/> in the same year as <paramref name="lastUnitOfTimeInYear"/>,
        /// up to and including <paramref name="lastUnitOfTimeInYear"/>, in sequential/ascending order.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="lastUnitOfTimeInYear"/> is null.</exception>
        public static IReadOnlyList<T> GetUnitsToDate<T>(
            this T lastUnitOfTimeInYear)
            where T : UnitOfTime, IHaveAYear
        {
            if (lastUnitOfTimeInYear == null)
            {
                throw new ArgumentNullException(nameof(lastUnitOfTimeInYear));
            }

            var unitsToDate = new Stack<T>();
            var thisYear = lastUnitOfTimeInYear.Year;
            while (lastUnitOfTimeInYear.Year == thisYear)
            {
                unitsToDate.Push(lastUnitOfTimeInYear);
                lastUnitOfTimeInYear = (T)lastUnitOfTimeInYear.Plus(-1);
            }

            var result = unitsToDate.ToList();

            return result;
        }

        private static CalendarDay InternalGetFirstCalendarDay(
            this CalendarDay day)
        {
            var result = day;

            return result;
        }

        private static CalendarDay InternalGetFirstCalendarDay(
            this CalendarMonth month)
        {
            var result = new CalendarDay(month.Year, month.MonthOfYear, DayOfMonth.One);

            return result;
        }

        private static CalendarDay InternalGetFirstCalendarDay(
            this CalendarQuarter quarter)
        {
            var result = new CalendarDay(quarter.Year, (MonthOfYear)((((int)quarter.QuarterNumber - 1) * 3) + 1), DayOfMonth.One);

            return result;
        }

        private static CalendarDay InternalGetFirstCalendarDay(
            this CalendarYear year)
        {
            var result = new CalendarDay(year.Year, MonthOfYear.January, DayOfMonth.One);

            return result;
        }

        private static CalendarDay InternalGetLastCalendarDay(
            this CalendarDay day)
        {
            var result = day;

            return result;
        }

        private static CalendarDay InternalGetLastCalendarDay(
            this CalendarMonth month)
        {
            var daysInMonth = DateTime.DaysInMonth(month.Year, (int)month.MonthNumber);

            var result = new CalendarDay(month.Year, month.MonthOfYear, (DayOfMonth)daysInMonth);

            return result;
        }

        private static CalendarDay InternalGetLastCalendarDay(
            this CalendarQuarter quarter)
        {
            var lastMonthInQuarter = new CalendarMonth(quarter.Year, (MonthOfYear)((int)quarter.QuarterNumber * 3));

            var result = lastMonthInQuarter.InternalGetLastCalendarDay();

            return result;
        }

        private static CalendarDay InternalGetLastCalendarDay(
            this CalendarYear year)
        {
            var result = new CalendarDay(year.Year, MonthOfYear.December, DayOfMonth.ThirtyOne);

            return result;
        }
    }
}
