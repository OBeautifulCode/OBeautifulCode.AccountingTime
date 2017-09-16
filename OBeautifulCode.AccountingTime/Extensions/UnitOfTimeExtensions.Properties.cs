// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfTimeExtensions.Properties.cs" company="OBeautifulCode">
//    Copyright (c) OBeautifulCode 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
        /// <exception cref="InvalidOperationException"><paramref name="unitOfTime"/> is unbounded.</exception>
        public static CalendarDay GetFirstCalendarDay(this CalendarUnitOfTime unitOfTime)
        {
            if (unitOfTime == null)
            {
                throw new ArgumentNullException(nameof(unitOfTime));
            }

            var unitOfTimeAsCalendarDay = unitOfTime as CalendarDay;
            if (unitOfTimeAsCalendarDay != null)
            {
                return unitOfTimeAsCalendarDay.GetFirstCalendarDay();
            }

            var unitOfTimeAsCalendarMonth = unitOfTime as CalendarMonth;
            if (unitOfTimeAsCalendarMonth != null)
            {
                return unitOfTimeAsCalendarMonth.GetFirstCalendarDay();
            }

            var unitOfTimeAsCalendarQuarter = unitOfTime as CalendarQuarter;
            if (unitOfTimeAsCalendarQuarter != null)
            {
                return unitOfTimeAsCalendarQuarter.GetFirstCalendarDay();
            }

            var unitOfTimeAsCalendarYear = unitOfTime as CalendarYear;
            if (unitOfTimeAsCalendarYear != null)
            {
                return unitOfTimeAsCalendarYear.GetFirstCalendarDay();
            }

            var unitOfTimeAsCalendarUnbounded = unitOfTime as CalendarUnbounded;
            if (unitOfTimeAsCalendarUnbounded != null)
            {
                throw new InvalidOperationException("There is no first day in unbounded time.");
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
        /// <exception cref="InvalidOperationException"><paramref name="unitOfTime"/> is unbounded.</exception>
        public static CalendarDay GetLastCalendarDay(this CalendarUnitOfTime unitOfTime)
        {
            if (unitOfTime == null)
            {
                throw new ArgumentNullException(nameof(unitOfTime));
            }

            var unitOfTimeAsCalendarDay = unitOfTime as CalendarDay;
            if (unitOfTimeAsCalendarDay != null)
            {
                return unitOfTimeAsCalendarDay.GetLastCalendarDay();
            }

            var unitOfTimeAsCalendarMonth = unitOfTime as CalendarMonth;
            if (unitOfTimeAsCalendarMonth != null)
            {
                return unitOfTimeAsCalendarMonth.GetLastCalendarDay();
            }

            var unitOfTimeAsCalendarQuarter = unitOfTime as CalendarQuarter;
            if (unitOfTimeAsCalendarQuarter != null)
            {
                return unitOfTimeAsCalendarQuarter.GetLastCalendarDay();
            }

            var unitOfTimeAsCalendarYear = unitOfTime as CalendarYear;
            if (unitOfTimeAsCalendarYear != null)
            {
                return unitOfTimeAsCalendarYear.GetLastCalendarDay();
            }

            var unitOfTimeAsCalendarUnbounded = unitOfTime as CalendarUnbounded;
            if (unitOfTimeAsCalendarUnbounded != null)
            {
                throw new InvalidOperationException("There is no last day in unbounded time.");
            }

            throw new NotSupportedException("this type of unit-of-time is not supported: " + unitOfTime.GetType());
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
        public static IList<T> GetUnitsToDate<T>(this T lastUnitOfTimeInYear)
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

        private static CalendarDay GetFirstCalendarDay(this CalendarDay day)
        {
            return day;
        }

        private static CalendarDay GetFirstCalendarDay(this CalendarMonth month)
        {
            var result = new CalendarDay(month.Year, month.MonthOfYear, DayOfMonth.One);
            return result;
        }

        private static CalendarDay GetFirstCalendarDay(this CalendarQuarter quarter)
        {
            var result = new CalendarDay(quarter.Year, (MonthOfYear)((((int)quarter.QuarterNumber - 1) * 3) + 1), DayOfMonth.One);
            return result;
        }

        private static CalendarDay GetFirstCalendarDay(this CalendarYear year)
        {
            var result = new CalendarDay(year.Year, MonthOfYear.January, DayOfMonth.One);
            return result;
        }

        private static CalendarDay GetLastCalendarDay(this CalendarDay day)
        {
            return day;
        }

        private static CalendarDay GetLastCalendarDay(this CalendarMonth month)
        {
            var daysInMonth = DateTime.DaysInMonth(month.Year, (int)month.MonthNumber);
            var result = new CalendarDay(month.Year, month.MonthOfYear, (DayOfMonth)daysInMonth);
            return result;
        }

        private static CalendarDay GetLastCalendarDay(this CalendarQuarter quarter)
        {
            var lastMonthInQuarter = new CalendarMonth(quarter.Year, (MonthOfYear)((int)quarter.QuarterNumber * 3));
            var result = lastMonthInQuarter.GetLastCalendarDay();
            return result;
        }

        private static CalendarDay GetLastCalendarDay(this CalendarYear year)
        {
            var result = new CalendarDay(year.Year, MonthOfYear.December, DayOfMonth.ThirtyOne);
            return result;
        }
    }
}

// ReSharper restore CheckNamespace