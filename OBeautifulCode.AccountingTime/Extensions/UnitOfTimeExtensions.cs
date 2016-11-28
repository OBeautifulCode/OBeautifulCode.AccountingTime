// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfTimeExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime
{
    using System;

    /// <summary>
    /// Extension methods on <see cref="UnitOfTime"/>.
    /// </summary>
    public static class UnitOfTimeExtensions
    {
        // need to support UnitOfTime, CalendarUnitOfTime?, IHaveAQuarter?

        /// <summary>
        /// Converts the specified <see cref="IHaveAMonth"/> to a <see cref="GenericMonth"/>.
        /// </summary>
        /// <param name="month">The month to convert.</param>
        /// <returns>
        /// A <see cref="GenericMonth"/> converted from an <see cref="IHaveAMonth"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="month"/> is null.</exception>
        public static GenericMonth ToGenericMonth(this IHaveAMonth month)
        {
            if (month == null)
            {
                throw new ArgumentNullException(nameof(month));
            }

            var result = new GenericMonth(month.Year, month.MonthNumber);
            return result;
        }

        /// <summary>
        /// Converts the specified <see cref="IHaveAQuarter"/> to a <see cref="GenericQuarter"/>.
        /// </summary>
        /// <param name="quarter">The quarter to convert.</param>
        /// <returns>
        /// A <see cref="GenericQuarter"/> converted from an <see cref="IHaveAQuarter"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="quarter"/> is null.</exception>
        public static GenericQuarter ToGenericQuarter(this IHaveAQuarter quarter)
        {
            if (quarter == null)
            {
                throw new ArgumentNullException(nameof(quarter));
            }

            var result = new GenericQuarter(quarter.Year, quarter.QuarterNumber);
            return result;
        }

        /// <summary>
        /// Converts the specified <see cref="IHaveAYear"/> to a <see cref="GenericYear"/>.
        /// </summary>
        /// <param name="year">The year to convert.</param>
        /// <returns>
        /// A <see cref="GenericYear"/> converted from an <see cref="IHaveAYear"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="year"/> is null.</exception>
        public static GenericYear ToGenericYear(this IHaveAYear year)
        {
            if (year == null)
            {
                throw new ArgumentNullException(nameof(year));
            }

            var result = new GenericYear(year.Year);
            return result;
        }

        /// <summary>
        /// Adds the specified number of units to a unit-of-time.
        /// </summary>
        /// <param name="unitOfTime">The unit-of-time to add to.</param>
        /// <param name="unitsToAdd">The number of units to add (use negative numbers to subtract units).</param>
        /// <typeparam name="T">The type of the unit-of-time.</typeparam>
        /// <returns>
        /// The result of adding the specified number of units to the specified units-of-time.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="unitOfTime"/> is null.</exception>
        public static UnitOfTime Plus<T>(this T unitOfTime, int unitsToAdd)
            where T : UnitOfTime
        {
            if (unitOfTime == null)
            {
                throw new ArgumentNullException(nameof(unitOfTime));
            }

            var unitOfTimeAsCalendarDay = unitOfTime as CalendarDay;
            if (unitOfTimeAsCalendarDay != null)
            {
                return unitOfTimeAsCalendarDay.Plus(unitsToAdd);
            }

            var unitOfTimeAsCalendarMonth = unitOfTime as CalendarMonth;
            if (unitOfTimeAsCalendarMonth != null)
            {
                return unitOfTimeAsCalendarMonth.Plus(unitsToAdd);
            }

            var unitOfTimeAsCalendarQuarter = unitOfTime as CalendarQuarter;
            if (unitOfTimeAsCalendarQuarter != null)
            {
                return unitOfTimeAsCalendarQuarter.Plus(unitsToAdd);
            }

            var unitOfTimeAsCalendarYear = unitOfTime as CalendarYear;
            if (unitOfTimeAsCalendarYear != null)
            {
                return unitOfTimeAsCalendarYear.Plus(unitsToAdd);
            }

            var unitOfTimeAsFiscalMonth = unitOfTime as FiscalMonth;
            if (unitOfTimeAsFiscalMonth != null)
            {
                return unitOfTimeAsFiscalMonth.Plus(unitsToAdd);
            }

            var unitOfTimeAsFiscalQuarter = unitOfTime as FiscalQuarter;
            if (unitOfTimeAsFiscalQuarter != null)
            {
                return unitOfTimeAsFiscalQuarter.Plus(unitsToAdd);
            }

            var unitOfTimeAsFiscalYear = unitOfTime as FiscalYear;
            if (unitOfTimeAsFiscalYear != null)
            {
                return unitOfTimeAsFiscalYear.Plus(unitsToAdd);
            }

            var unitOfTimeAsGenericMonth = unitOfTime as GenericMonth;
            if (unitOfTimeAsGenericMonth != null)
            {
                return unitOfTimeAsGenericMonth.Plus(unitsToAdd);
            }

            var unitOfTimeAsGenericQuarter = unitOfTime as GenericQuarter;
            if (unitOfTimeAsGenericQuarter != null)
            {
                return unitOfTimeAsGenericQuarter.Plus(unitsToAdd);
            }

            var unitOfTimeAsGenericYear = unitOfTime as GenericYear;
            if (unitOfTimeAsGenericYear != null)
            {
                return unitOfTimeAsGenericYear.Plus(unitsToAdd);
            }

            throw new NotSupportedException("this type of unit-of-time is not supported: " + unitOfTime.GetType());
        }

        /// <summary>
        /// Converts a <see cref="FiscalQuarter"/> to a <see cref="CalendarQuarter"/>.
        /// </summary>
        /// <param name="calendarQuarter">The calendar quarter to convert.</param>
        /// <param name="calendarQuarterThatIsFirstFiscalQuarter">The calendar quarter that is associated with the first fiscal quarter for the company.</param>
        /// <returns>
        /// The fiscal quarter associated with the specified calendar quarter.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="calendarQuarter"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="calendarQuarterThatIsFirstFiscalQuarter"/> is <see cref="QuarterNumber.Invalid"/></exception>
        public static FiscalQuarter ToFiscalQuarter(this CalendarQuarter calendarQuarter, QuarterNumber calendarQuarterThatIsFirstFiscalQuarter)
        {
            if (calendarQuarter == null)
            {
                throw new ArgumentNullException(nameof(calendarQuarter));
            }

            if (calendarQuarterThatIsFirstFiscalQuarter == QuarterNumber.Invalid)
            {
                throw new ArgumentException("calendar quarter that is first fiscal quarter is Invalid.", nameof(calendarQuarterThatIsFirstFiscalQuarter));
            }

            int offset;
            if (calendarQuarterThatIsFirstFiscalQuarter == QuarterNumber.Fourth)
            {
                offset = 1;
            }
            else if (calendarQuarterThatIsFirstFiscalQuarter == QuarterNumber.Third)
            {
                offset = 2;
            }
            else if (calendarQuarterThatIsFirstFiscalQuarter == QuarterNumber.Second)
            {
                offset = 3;
            }
            else if (calendarQuarterThatIsFirstFiscalQuarter == QuarterNumber.First)
            {
                offset = 0;
            }
            else
            {
                throw new NotSupportedException("This quarter number is not supported: " + calendarQuarterThatIsFirstFiscalQuarter);
            }

            var fiscalQuarter = new FiscalQuarter(calendarQuarter.Year, calendarQuarter.QuarterNumber);
            fiscalQuarter = fiscalQuarter.Plus(offset);
            return fiscalQuarter;
        }

        /// <summary>
        /// Converts a <see cref="CalendarQuarter"/> to a <see cref="FiscalQuarter"/>.
        /// </summary>
        /// <param name="fiscalQuarter">The fiscal quarter to convert.</param>
        /// <param name="calendarQuarterThatIsFirstFiscalQuarter">The calendar quarter that is associated with the first fiscal quarter for the company.</param>
        /// <returns>
        /// The calendar quarter associated with the specified fiscal quarter.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="fiscalQuarter"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="calendarQuarterThatIsFirstFiscalQuarter"/> is <see cref="QuarterNumber.Invalid"/></exception>
        public static CalendarQuarter ToCalendarQuarter(this FiscalQuarter fiscalQuarter, QuarterNumber calendarQuarterThatIsFirstFiscalQuarter)
        {
            if (fiscalQuarter == null)
            {
                throw new ArgumentNullException(nameof(fiscalQuarter));
            }

            if (calendarQuarterThatIsFirstFiscalQuarter == QuarterNumber.Invalid)
            {
                throw new ArgumentException("calendar quarter that is first fiscal quarter is Invalid.", nameof(calendarQuarterThatIsFirstFiscalQuarter));
            }

            int offset;
            if (calendarQuarterThatIsFirstFiscalQuarter == QuarterNumber.Fourth)
            {
                offset = -1;
            }
            else if (calendarQuarterThatIsFirstFiscalQuarter == QuarterNumber.Third)
            {
                offset = -2;
            }
            else if (calendarQuarterThatIsFirstFiscalQuarter == QuarterNumber.Second)
            {
                offset = -3;
            }
            else if (calendarQuarterThatIsFirstFiscalQuarter == QuarterNumber.First)
            {
                offset = 0;
            }
            else
            {
                throw new NotSupportedException("This quarter number is not supported: " + calendarQuarterThatIsFirstFiscalQuarter);
            }

            var calendarQuarter = new CalendarQuarter(fiscalQuarter.Year, fiscalQuarter.QuarterNumber);
            calendarQuarter = calendarQuarter.Plus(offset);
            return calendarQuarter;
        }

        /// <summary>
        /// Gets the first calendar day in the specified calendar-based unit-of-time.
        /// </summary>
        /// <param name="unitOfTime">The unit-of-time.</param>
        /// <typeparam name="T">The type of calendar-based unit-of-time.</typeparam>
        /// <returns>
        /// The first calendar day in the specified calendar-based unit-of-time.
        /// </returns>
        public static CalendarDay GetFirstCalendarDay<T>(this T unitOfTime)
            where T : CalendarUnitOfTime
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

            throw new NotSupportedException("this type of unit-of-time is not supported: " + unitOfTime.GetType());
        }

        /// <summary>
        /// Gets the last calendar day in the specified calendar-based unit-of-time.
        /// </summary>
        /// <param name="unitOfTime">The unit-of-time.</param>
        /// <typeparam name="T">The type of calendar-based unit-of-time.</typeparam>
        /// <returns>
        /// The last calendar day in the specified calendar-based unit-of-time.
        /// </returns>
        public static CalendarDay GetLastCalendarDay<T>(this T unitOfTime)
            where T : CalendarUnitOfTime
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

            throw new NotSupportedException("this type of unit-of-time is not supported: " + unitOfTime.GetType());
        }

        private static CalendarDay Plus(this CalendarDay unitOfTime, int unitsToAdd)
        {
            var dayAsDateTime = unitOfTime.ToDateTime().AddDays(unitsToAdd);
            var result = new CalendarDay(dayAsDateTime.Year, (MonthOfYear)dayAsDateTime.Month, (DayOfMonth)dayAsDateTime.Day);
            return result;
        }

        private static CalendarMonth Plus(this CalendarMonth unitOfTime, int unitsToAdd)
        {
            var genericMonth = unitOfTime.ToGenericMonth().Plus(unitsToAdd);
            var result = new CalendarMonth(genericMonth.Year, (MonthOfYear)genericMonth.MonthNumber);
            return result;
        }

        private static FiscalMonth Plus(this FiscalMonth unitOfTime, int unitsToAdd)
        {
            var genericMonth = unitOfTime.ToGenericMonth().Plus(unitsToAdd);
            var result = new FiscalMonth(genericMonth.Year, genericMonth.MonthNumber);
            return result;
        }

        private static GenericMonth Plus(this GenericMonth unitOfTime, int unitsToAdd)
        {
            var monthAsDateTime = new DateTime(unitOfTime.Year, (int)unitOfTime.MonthNumber, 1);
            monthAsDateTime = monthAsDateTime.AddMonths(unitsToAdd);
            var result = new GenericMonth(monthAsDateTime.Year, (MonthNumber)monthAsDateTime.Month);
            return result;
        }

        private static CalendarQuarter Plus(this CalendarQuarter unitOfTime, int unitsToAdd)
        {
            var genericQuarter = unitOfTime.ToGenericQuarter().Plus(unitsToAdd);
            var result = new CalendarQuarter(genericQuarter.Year, genericQuarter.QuarterNumber);
            return result;
        }

        private static FiscalQuarter Plus(this FiscalQuarter unitOfTime, int unitsToAdd)
        {
            var genericQuarter = unitOfTime.ToGenericQuarter().Plus(unitsToAdd);
            var result = new FiscalQuarter(genericQuarter.Year, genericQuarter.QuarterNumber);
            return result;
        }

        private static GenericQuarter Plus(this GenericQuarter unitOfTime, int unitsToAdd)
        {
            int year = unitOfTime.Year;
            int quarterNumber = (int)unitOfTime.QuarterNumber;

            year = year + (unitsToAdd / 4);
            quarterNumber = quarterNumber + (unitsToAdd % 4);

            if (quarterNumber > 4)
            {
                year++;
                quarterNumber = quarterNumber - 4;
            }

            if (quarterNumber < 1)
            {
                year--;
                quarterNumber = 4 + quarterNumber;
            }

            var quarter = new GenericQuarter(year, (QuarterNumber)quarterNumber);
            return quarter;
        }

        private static CalendarYear Plus(this CalendarYear unitOfTime, int unitsToAdd)
        {
            var genericYear = unitOfTime.ToGenericYear().Plus(unitsToAdd);
            var result = new CalendarYear(genericYear.Year);
            return result;
        }

        private static FiscalYear Plus(this FiscalYear unitOfTime, int unitsToAdd)
        {
            var genericYear = unitOfTime.ToGenericYear().Plus(unitsToAdd);
            var result = new FiscalYear(genericYear.Year);
            return result;
        }

        private static GenericYear Plus(this GenericYear unitOfTime, int unitsToAdd)
        {
            var result = new GenericYear(unitOfTime.Year + unitsToAdd);
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