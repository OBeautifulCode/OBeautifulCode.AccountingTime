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
        /// Gets the first unit-of-time in the same year as the specified unit-of-time.
        /// </summary>
        /// <param name="unitOfTime">The unit-of-time.</param>
        /// <returns>
        /// The first unit-of-time in the same year as <paramref name="unitOfTime"/>.
        /// For example:
        /// - If November 10, 2023 is specified then January 1, 2023 is returned.
        /// - If Q2 2023 is specified then Q1 2023 is returned.
        /// </returns>
        public static UnitOfTime GetFirstInSameYear(
            this UnitOfTime unitOfTime)
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
                    return unitOfTimeAsCalendarDay.InternalGetFirstInSameYear();
                case CalendarMonth unitOfTimeAsCalendarMonth:
                    return unitOfTimeAsCalendarMonth.InternalGetFirstInSameYear();
                case CalendarQuarter unitOfTimeAsCalendarQuarter:
                    return unitOfTimeAsCalendarQuarter.InternalGetFirstInSameYear();
                case CalendarYear unitOfTimeAsCalendarYear:
                    return unitOfTimeAsCalendarYear.InternalGetFirstInSameYear();
                case FiscalMonth unitOfTimeAsFiscalMonth:
                    return unitOfTimeAsFiscalMonth.InternalGetFirstInSameYear();
                case FiscalQuarter unitOfTimeAsFiscalQuarter:
                    return unitOfTimeAsFiscalQuarter.InternalGetFirstInSameYear();
                case FiscalYear unitOfTimeAsFiscalYear:
                    return unitOfTimeAsFiscalYear.InternalGetFirstInSameYear();
                case GenericMonth unitOfTimeAsGenericMonth:
                    return unitOfTimeAsGenericMonth.InternalGetFirstInSameYear();
                case GenericQuarter unitOfTimeAsGenericQuarter:
                    return unitOfTimeAsGenericQuarter.InternalGetFirstInSameYear();
                case GenericYear unitOfTimeAsGenericYear:
                    return unitOfTimeAsGenericYear.InternalGetFirstInSameYear();
                default:
                    throw new NotSupportedException(Invariant($"This unit-of-time is not supported: {unitOfTime}."));
            }
        }

        /// <summary>
        /// Gets the last unit-of-time in the same year as the specified unit-of-time.
        /// </summary>
        /// <param name="unitOfTime">The unit-of-time.</param>
        /// <returns>
        /// The last unit-of-time in the same year as <paramref name="unitOfTime"/>.
        /// For example:
        /// - If November 10, 2023 is specified then December 31, 2023 is returned.
        /// - If Q2 2023 is specified then Q4 2023 is returned.
        /// </returns>
        public static UnitOfTime GetLastInSameYear(
            this UnitOfTime unitOfTime)
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
                    return unitOfTimeAsCalendarDay.InternalGetLastInSameYear();
                case CalendarMonth unitOfTimeAsCalendarMonth:
                    return unitOfTimeAsCalendarMonth.InternalGetLastInSameYear();
                case CalendarQuarter unitOfTimeAsCalendarQuarter:
                    return unitOfTimeAsCalendarQuarter.InternalGetLastInSameYear();
                case CalendarYear unitOfTimeAsCalendarYear:
                    return unitOfTimeAsCalendarYear.InternalGetLastInSameYear();
                case FiscalMonth unitOfTimeAsFiscalMonth:
                    return unitOfTimeAsFiscalMonth.InternalGetLastInSameYear();
                case FiscalQuarter unitOfTimeAsFiscalQuarter:
                    return unitOfTimeAsFiscalQuarter.InternalGetLastInSameYear();
                case FiscalYear unitOfTimeAsFiscalYear:
                    return unitOfTimeAsFiscalYear.InternalGetLastInSameYear();
                case GenericMonth unitOfTimeAsGenericMonth:
                    return unitOfTimeAsGenericMonth.InternalGetLastInSameYear();
                case GenericQuarter unitOfTimeAsGenericQuarter:
                    return unitOfTimeAsGenericQuarter.InternalGetLastInSameYear();
                case GenericYear unitOfTimeAsGenericYear:
                    return unitOfTimeAsGenericYear.InternalGetLastInSameYear();
                default:
                    throw new NotSupportedException(Invariant($"This unit-of-time is not supported: {unitOfTime}."));
            }
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

        private static CalendarDay InternalGetFirstInSameYear(
            this CalendarDay unitOfTime)
        {
            var result = new CalendarDay(unitOfTime.Year, MonthOfYear.January, DayOfMonth.One);

            return result;
        }

        private static CalendarMonth InternalGetFirstInSameYear(
            this CalendarMonth unitOfTime)
        {
            var result = new CalendarMonth(unitOfTime.Year, MonthOfYear.January);

            return result;
        }

        private static CalendarQuarter InternalGetFirstInSameYear(
            this CalendarQuarter unitOfTime)
        {
            var result = new CalendarQuarter(unitOfTime.Year, QuarterNumber.Q1);

            return result;
        }

        private static CalendarYear InternalGetFirstInSameYear(
            this CalendarYear unitOfTime)
        {
            var result = new CalendarYear(unitOfTime.Year);

            return result;
        }

        private static FiscalMonth InternalGetFirstInSameYear(
            this FiscalMonth unitOfTime)
        {
            var result = new FiscalMonth(unitOfTime.Year, MonthNumber.One);

            return result;
        }

        private static FiscalQuarter InternalGetFirstInSameYear(
            this FiscalQuarter unitOfTime)
        {
            var result = new FiscalQuarter(unitOfTime.Year, QuarterNumber.Q1);

            return result;
        }

        private static FiscalYear InternalGetFirstInSameYear(
            this FiscalYear unitOfTime)
        {
            var result = new FiscalYear(unitOfTime.Year);

            return result;
        }

        private static GenericMonth InternalGetFirstInSameYear(
            this GenericMonth unitOfTime)
        {
            var result = new GenericMonth(unitOfTime.Year, MonthNumber.One);

            return result;
        }

        private static GenericQuarter InternalGetFirstInSameYear(
            this GenericQuarter unitOfTime)
        {
            var result = new GenericQuarter(unitOfTime.Year, QuarterNumber.Q1);

            return result;
        }

        private static GenericYear InternalGetFirstInSameYear(
            this GenericYear unitOfTime)
        {
            var result = new GenericYear(unitOfTime.Year);

            return result;
        }

        private static CalendarDay InternalGetLastInSameYear(
            this CalendarDay unitOfTime)
        {
            var result = new CalendarDay(unitOfTime.Year, MonthOfYear.December, DayOfMonth.ThirtyOne);

            return result;
        }

        private static CalendarMonth InternalGetLastInSameYear(
            this CalendarMonth unitOfTime)
        {
            var result = new CalendarMonth(unitOfTime.Year, MonthOfYear.December);

            return result;
        }

        private static CalendarQuarter InternalGetLastInSameYear(
            this CalendarQuarter unitOfTime)
        {
            var result = new CalendarQuarter(unitOfTime.Year, QuarterNumber.Q4);

            return result;
        }

        private static CalendarYear InternalGetLastInSameYear(
            this CalendarYear unitOfTime)
        {
            var result = new CalendarYear(unitOfTime.Year);

            return result;
        }

        private static FiscalMonth InternalGetLastInSameYear(
            this FiscalMonth unitOfTime)
        {
            var result = new FiscalMonth(unitOfTime.Year, MonthNumber.Twelve);

            return result;
        }

        private static FiscalQuarter InternalGetLastInSameYear(
            this FiscalQuarter unitOfTime)
        {
            var result = new FiscalQuarter(unitOfTime.Year, QuarterNumber.Q4);

            return result;
        }

        private static FiscalYear InternalGetLastInSameYear(
            this FiscalYear unitOfTime)
        {
            var result = new FiscalYear(unitOfTime.Year);

            return result;
        }

        private static GenericMonth InternalGetLastInSameYear(
            this GenericMonth unitOfTime)
        {
            var result = new GenericMonth(unitOfTime.Year, MonthNumber.Twelve);

            return result;
        }

        private static GenericQuarter InternalGetLastInSameYear(
            this GenericQuarter unitOfTime)
        {
            var result = new GenericQuarter(unitOfTime.Year, QuarterNumber.Q4);

            return result;
        }

        private static GenericYear InternalGetLastInSameYear(
            this GenericYear unitOfTime)
        {
            var result = new GenericYear(unitOfTime.Year);

            return result;
        }
    }
}
