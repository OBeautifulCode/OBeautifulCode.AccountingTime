// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfTimeExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using static System.FormattableString;

    /// <summary>
    /// Extension methods on <see cref="UnitOfTime"/>.
    /// </summary>
    public static class UnitOfTimeExtensions
    {
        private static readonly SerializationFormat[] SerializationFormatByType =
        {
            #pragma warning disable SA1025 // Code must not contain multiple whitespace in a row
            #pragma warning disable SA1009 // Closing parenthesis must be spaced correctly
            #pragma warning disable SA1001 // Commas must be spaced correctly
            new SerializationFormat { Type = typeof(CalendarDay)      , Regex = new Regex("^c-(\\d{4})-(\\d{2})-(\\d{2})$") },
            new SerializationFormat { Type = typeof(CalendarMonth)    , Regex = new Regex("^c-(\\d{4})-(\\d{2})$") },
            new SerializationFormat { Type = typeof(CalendarQuarter)  , Regex = new Regex("^c-(\\d{4})-Q(\\d)$") },
            new SerializationFormat { Type = typeof(CalendarYear)     , Regex = new Regex("^c-(\\d{4})$") },
            new SerializationFormat { Type = typeof(CalendarUnbounded), Regex = new Regex("^c-unbounded$") },
            new SerializationFormat { Type = typeof(FiscalMonth)      , Regex = new Regex("^f-(\\d{4})-(\\d{2})$") },
            new SerializationFormat { Type = typeof(FiscalQuarter)    , Regex = new Regex("^f-(\\d{4})-Q(\\d)$") },
            new SerializationFormat { Type = typeof(FiscalYear)       , Regex = new Regex("^f-(\\d{4})$") },
            new SerializationFormat { Type = typeof(FiscalUnbounded)  , Regex = new Regex("^f-unbounded$") },
            new SerializationFormat { Type = typeof(GenericMonth)     , Regex = new Regex("^g-(\\d{4})-(\\d{2})$") },
            new SerializationFormat { Type = typeof(GenericQuarter)   , Regex = new Regex("^g-(\\d{4})-Q(\\d)$") },
            new SerializationFormat { Type = typeof(GenericYear)      , Regex = new Regex("^g-(\\d{4})$") },
            new SerializationFormat { Type = typeof(GenericUnbounded) , Regex = new Regex("^g-unbounded$") }
            #pragma warning restore SA1001 // Commas must be spaced correctly
            #pragma warning restore SA1009 // Closing parenthesis must be spaced correctly
            #pragma warning restore SA1025 // Code must not contain multiple whitespace in a row
        };

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
            if (calendarQuarterThatIsFirstFiscalQuarter == QuarterNumber.Q4)
            {
                offset = 1;
            }
            else if (calendarQuarterThatIsFirstFiscalQuarter == QuarterNumber.Q3)
            {
                offset = 2;
            }
            else if (calendarQuarterThatIsFirstFiscalQuarter == QuarterNumber.Q2)
            {
                offset = 3;
            }
            else if (calendarQuarterThatIsFirstFiscalQuarter == QuarterNumber.Q1)
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
            if (calendarQuarterThatIsFirstFiscalQuarter == QuarterNumber.Q4)
            {
                offset = -1;
            }
            else if (calendarQuarterThatIsFirstFiscalQuarter == QuarterNumber.Q3)
            {
                offset = -2;
            }
            else if (calendarQuarterThatIsFirstFiscalQuarter == QuarterNumber.Q2)
            {
                offset = -3;
            }
            else if (calendarQuarterThatIsFirstFiscalQuarter == QuarterNumber.Q1)
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
        /// Adds the specified number of units to a unit-of-time.
        /// </summary>
        /// <param name="unitOfTime">The unit-of-time to add to.</param>
        /// <param name="unitsToAdd">The number of units to add (use negative numbers to subtract units).</param>
        /// <returns>
        /// The result of adding the specified number of units to the specified units-of-time.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="unitOfTime"/> is null.</exception>
        /// <exception cref="InvalidOperationException"><paramref name="unitOfTime"/> is unbounded.</exception>
        public static UnitOfTime Plus(this UnitOfTime unitOfTime, int unitsToAdd)
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

            var unitOfTimeAsCalendarUnbounded = unitOfTime as CalendarUnbounded;
            if (unitOfTimeAsCalendarUnbounded != null)
            {
                throw new InvalidOperationException("Cannot add to unbounded time.");
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

            var unitOfTimeAsFiscalUnbounded = unitOfTime as FiscalUnbounded;
            if (unitOfTimeAsFiscalUnbounded != null)
            {
                throw new InvalidOperationException("Cannot add to unbounded time.");
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

            var unitOfTimeAsGenericUnbounded = unitOfTime as GenericUnbounded;
            if (unitOfTimeAsGenericUnbounded != null)
            {
                throw new InvalidOperationException("Cannot add to unbounded time.");
            }

            throw new NotSupportedException("this type of unit-of-time is not supported: " + unitOfTime.GetType());
        }

        /// <summary>
        /// Adds the specified number of units of a specified granularity to a unit-of-time.
        /// </summary>
        /// <param name="unitOfTime">The unit-of-time to add to.</param>
        /// <param name="unitsToAdd">The number of units to add (use negative numbers to subtract units).</param>
        /// <param name="granularityOfUnitsToAdd">The granularity of the units to add.  Must be as or less granular than the <paramref name="unitOfTime"/> (e.g. can add CalendarYear to a CalendarQuarter, but not vice-versa)</param>
        /// <returns>
        /// The result of adding the specified number of units of the specified granularity to a unit-of-time.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="unitOfTime"/> is null.</exception>
        /// <exception cref="ArgumentException">Cannot add or subtract from a unit-of-time whose granularity is <see cref="UnitOfTimeGranularity.Unbounded"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="granularityOfUnitsToAdd"/> is <see cref="UnitOfTimeGranularity.Invalid"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="granularityOfUnitsToAdd"/> is <see cref="UnitOfTimeGranularity.Unbounded"/>.  Cannot add units of that granularity.</exception>
        /// <exception cref="ArgumentException"><paramref name="granularityOfUnitsToAdd"/> is more granular than the <paramref name="unitOfTime"/>.  Only units that are as granular or less granular than a unit-of-time can be added to that unit-of-time.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", MessageId = "3*unitsToAdd", Justification = "The user is doing something very wrong if they are adding very large numbers of units and it's OK for them to get an OverflowException at runtime.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", MessageId = "12*unitsToAdd", Justification = "The user is doing something very wrong if they are adding very large numbers of units and it's OK for them to get an OverflowException at runtime.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", MessageId = "4*unitsToAdd", Justification = "The user is doing something very wrong if they are adding very large numbers of units and it's OK for them to get an OverflowException at runtime.")]
        public static UnitOfTime Plus(this UnitOfTime unitOfTime, int unitsToAdd, UnitOfTimeGranularity granularityOfUnitsToAdd)
        {
            if (unitOfTime == null)
            {
                throw new ArgumentNullException(nameof(unitOfTime));
            }

            if (unitOfTime.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded)
            {
                throw new ArgumentException(Invariant($"Cannot add or subtract from a unit-of-time whose granuarlity is {nameof(UnitOfTimeGranularity.Unbounded)}"));
            }

            if (granularityOfUnitsToAdd == UnitOfTimeGranularity.Invalid)
            {
                throw new ArgumentException(Invariant($"{nameof(granularityOfUnitsToAdd)} is {nameof(UnitOfTimeGranularity.Invalid)}"));
            }

            if (granularityOfUnitsToAdd == UnitOfTimeGranularity.Unbounded)
            {
                throw new ArgumentException(Invariant($"{nameof(granularityOfUnitsToAdd)} is {nameof(UnitOfTimeGranularity.Unbounded)}.  Cannot add units of that granularity."));
            }

            if (granularityOfUnitsToAdd.IsMoreGranularThan(unitOfTime.UnitOfTimeGranularity))
            {
                throw new ArgumentException(Invariant($"{nameof(granularityOfUnitsToAdd)} is more granular than {nameof(unitOfTime)}.  Only units that are as granular or less granular than a unit-of-time can be added to that unit-of-time."));
            }

            if (unitOfTime.UnitOfTimeGranularity == granularityOfUnitsToAdd)
            {
                var result = unitOfTime.Plus(unitsToAdd);
                return result;
            }

            if (granularityOfUnitsToAdd == UnitOfTimeGranularity.Year)
            {
                if (unitOfTime.UnitOfTimeGranularity == UnitOfTimeGranularity.Quarter)
                {
                    var result = unitOfTime.Plus(4 * unitsToAdd);
                    return result;
                }

                if (unitOfTime.UnitOfTimeGranularity == UnitOfTimeGranularity.Month)
                {
                    var result = unitOfTime.Plus(12 * unitsToAdd);
                    return result;
                }

                if (unitOfTime.UnitOfTimeGranularity == UnitOfTimeGranularity.Day)
                {
                    throw new NotSupportedException("adjusting a unit-of-time with Day granularity is not supported");
                }

                throw new InvalidOperationException("should not get here");
            }

            if (granularityOfUnitsToAdd == UnitOfTimeGranularity.Quarter)
            {
                if (unitOfTime.UnitOfTimeGranularity == UnitOfTimeGranularity.Month)
                {
                    var result = unitOfTime.Plus(3 * unitsToAdd);
                    return result;
                }

                if (unitOfTime.UnitOfTimeGranularity == UnitOfTimeGranularity.Day)
                {
                    throw new NotSupportedException("adjusting a unit-of-time with Day granularity is not supported");
                }

                throw new InvalidOperationException("should not get here");
            }

            if (granularityOfUnitsToAdd == UnitOfTimeGranularity.Month)
            {
                if (unitOfTime.UnitOfTimeGranularity == UnitOfTimeGranularity.Day)
                {
                    throw new NotSupportedException("adjusting a unit-of-time with Day granularity is not supported");
                }

                throw new InvalidOperationException("should not get here");
            }

            throw new InvalidOperationException("should not get here");
        }

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

        /// <summary>
        /// Serializes a <see cref="UnitOfTime"/> to a sortable string.
        /// </summary>
        /// <param name="unitOfTime">The unit-of-time to serialize.</param>
        /// <returns>
        /// Gets a string representation of a unit-of-time that can be deserialized
        /// into the same unit-of-time and which sorts in the same way that the
        /// other unit-of-times (of the same type) would sort (earlier time first, later time last).
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="unitOfTime"/> is null.</exception>
        public static string SerializeToSortableString(this UnitOfTime unitOfTime)
        {
            if (unitOfTime == null)
            {
                throw new ArgumentNullException(nameof(unitOfTime));
            }

            var unitOfTimeType = unitOfTime.GetType();
            var unitOfTimeAsCalendarDay = unitOfTime as CalendarDay;
            if (unitOfTimeAsCalendarDay != null)
            {
                string result = Invariant($"c-{unitOfTimeAsCalendarDay.Year:D4}-{(int)unitOfTimeAsCalendarDay.MonthNumber:D2}-{(int)unitOfTimeAsCalendarDay.DayOfMonth:D2}");
                return result;
            }

            var unitOfTimeAsCalendarMonth = unitOfTime as CalendarMonth;
            if (unitOfTimeAsCalendarMonth != null)
            {
                string result = Invariant($"c-{unitOfTimeAsCalendarMonth.Year:D4}-{(int)unitOfTimeAsCalendarMonth.MonthNumber:D2}");
                return result;
            }

            var unitOfTimeAsCalendarQuarter = unitOfTime as CalendarQuarter;
            if (unitOfTimeAsCalendarQuarter != null)
            {
                string result = Invariant($"c-{unitOfTimeAsCalendarQuarter.Year:D4}-Q{(int)unitOfTimeAsCalendarQuarter.QuarterNumber}");
                return result;
            }

            var unitOfTimeAsCalendarYear = unitOfTime as CalendarYear;
            if (unitOfTimeAsCalendarYear != null)
            {
                string result = Invariant($"c-{unitOfTimeAsCalendarYear.Year:D4}");
                return result;
            }

            var unitOfTimeAsCalendarUnbounded = unitOfTime as CalendarUnbounded;
            if (unitOfTimeAsCalendarUnbounded != null)
            {
                return "c-unbounded";
            }

            var unitOfTimeAsFiscalMonth = unitOfTime as FiscalMonth;
            if (unitOfTimeAsFiscalMonth != null)
            {
                string result = Invariant($"f-{unitOfTimeAsFiscalMonth.Year:D4}-{(int)unitOfTimeAsFiscalMonth.MonthNumber:D2}");
                return result;
            }

            var unitOfTimeAsFiscalQuarter = unitOfTime as FiscalQuarter;
            if (unitOfTimeAsFiscalQuarter != null)
            {
                string result = Invariant($"f-{unitOfTimeAsFiscalQuarter.Year:D4}-Q{(int)unitOfTimeAsFiscalQuarter.QuarterNumber}");
                return result;
            }

            var unitOfTimeAsFiscalYear = unitOfTime as FiscalYear;
            if (unitOfTimeAsFiscalYear != null)
            {
                string result = Invariant($"f-{unitOfTimeAsFiscalYear.Year:D4}");
                return result;
            }

            var unitOfTimeAsFiscalUnbounded = unitOfTime as FiscalUnbounded;
            if (unitOfTimeAsFiscalUnbounded != null)
            {
                return "f-unbounded";
            }

            var unitOfTimeAsGenericMonth = unitOfTime as GenericMonth;
            if (unitOfTimeAsGenericMonth != null)
            {
                string result = Invariant($"g-{unitOfTimeAsGenericMonth.Year:D4}-{(int)unitOfTimeAsGenericMonth.MonthNumber:D2}");
                return result;
            }

            var unitOfTimeAsGenericQuarter = unitOfTime as GenericQuarter;
            if (unitOfTimeAsGenericQuarter != null)
            {
                string result = Invariant($"g-{unitOfTimeAsGenericQuarter.Year:D4}-Q{(int)unitOfTimeAsGenericQuarter.QuarterNumber}");
                return result;
            }

            var unitOfTimeAsGenericYear = unitOfTime as GenericYear;
            if (unitOfTimeAsGenericYear != null)
            {
                string result = Invariant($"g-{unitOfTimeAsGenericYear.Year:D4}");
                return result;
            }

            var unitOfTimeAsGenericUnbounded = unitOfTime as GenericUnbounded;
            if (unitOfTimeAsGenericUnbounded != null)
            {
                return "g-unbounded";
            }

            throw new NotSupportedException("this type of unit-of-time is not supported: " + unitOfTimeType);
        }

        /// <summary>
        /// Deserializes a <see cref="UnitOfTime"/> from a sortable string.
        /// </summary>
        /// <typeparam name="T">The type of unit-of-time.</typeparam>
        /// <param name="unitOfTime">The serialized, sortable unit-of-time string to deserialize.</param>
        /// <returns>
        /// Gets a unit-of-time deserialized from it's string representation.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="unitOfTime"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="unitOfTime"/> is whitespace.</exception>
        /// <exception cref="InvalidOperationException">Cannot deserialize string; it is not valid unit-of-time.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Deserializing is inheritently complex and requires lots of types.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Deserializing is inheritently complex and requires lots of types.")]
        public static T DeserializeFromSortableString<T>(this string unitOfTime)
            where T : UnitOfTime
        {
            if (unitOfTime == null)
            {
                throw new ArgumentNullException(nameof(unitOfTime));
            }

            if (string.IsNullOrWhiteSpace(unitOfTime))
            {
                throw new ArgumentException("unit-of-time string is whitespace", nameof(unitOfTime));
            }

            var serializationFormatMatch = SerializationFormatByType.Select(_ => new { Match = _.Regex.Match(unitOfTime), SerializationFormat = _ }).SingleOrDefault(_ => _.Match.Success);
            if (serializationFormatMatch == null)
            {
                throw new InvalidOperationException("Cannot deserialize string; it is not valid unit-of-time.");
            }

            var serializedType = serializationFormatMatch.SerializationFormat.Type;
            var returnType = typeof(T);
            if (!returnType.IsAssignableFrom(serializedType))
            {
                throw new InvalidOperationException(Invariant($"The unit-of-time appears to be a {serializedType.Name} which cannot be casted to a {returnType.Name}."));
            }

            string errorMessage = Invariant($"Cannot deserialize string;  it appears to be a {serializedType.Name} but it is malformed.");
            var tokens = serializationFormatMatch.SerializationFormat.Regex.GetGroupNames().Skip(1).Select(_ => serializationFormatMatch.Match.Groups[_].Value).ToArray();

            if (serializedType == typeof(CalendarDay))
            {
                var year = ParseIntOrThrow(tokens[0], errorMessage);
                var month = ParseEnumOrThrow<MonthOfYear>(tokens[1], errorMessage);
                var day = ParseEnumOrThrow<DayOfMonth>(tokens[2], errorMessage);

                try
                {
                    var deserialized = new CalendarDay(year, month, day);
                    return deserialized as T;
                }
                catch (ArgumentException)
                {
                    throw new InvalidOperationException(errorMessage);
                }
            }

            if (serializedType == typeof(CalendarMonth))
            {
                var year = ParseIntOrThrow(tokens[0], errorMessage);
                var month = ParseEnumOrThrow<MonthOfYear>(tokens[1], errorMessage);

                try
                {
                    var deserialized = new CalendarMonth(year, month);
                    return deserialized as T;
                }
                catch (ArgumentException)
                {
                    throw new InvalidOperationException(errorMessage);
                }
            }

            if (serializedType == typeof(CalendarQuarter))
            {
                var year = ParseIntOrThrow(tokens[0], errorMessage);
                var quarter = ParseEnumOrThrow<QuarterNumber>(tokens[1], errorMessage);

                try
                {
                    var deserialized = new CalendarQuarter(year, quarter);
                    return deserialized as T;
                }
                catch (ArgumentException)
                {
                    throw new InvalidOperationException(errorMessage);
                }
            }

            if (serializedType == typeof(CalendarYear))
            {
                var year = ParseIntOrThrow(tokens[0], errorMessage);

                try
                {
                    var deserialized = new CalendarYear(year);
                    return deserialized as T;
                }
                catch (ArgumentException)
                {
                    throw new InvalidOperationException(errorMessage);
                }
            }

            if (serializedType == typeof(CalendarUnbounded))
            {
                var deserialized = new CalendarUnbounded();
                return deserialized as T;
            }

            if (serializedType == typeof(FiscalMonth))
            {
                var year = ParseIntOrThrow(tokens[0], errorMessage);
                var month = ParseEnumOrThrow<MonthNumber>(tokens[1], errorMessage);

                try
                {
                    var deserialized = new FiscalMonth(year, month);
                    return deserialized as T;
                }
                catch (ArgumentException)
                {
                    throw new InvalidOperationException(errorMessage);
                }
            }

            if (serializedType == typeof(FiscalQuarter))
            {
                var year = ParseIntOrThrow(tokens[0], errorMessage);
                var quarter = ParseEnumOrThrow<QuarterNumber>(tokens[1], errorMessage);

                try
                {
                    var deserialized = new FiscalQuarter(year, quarter);
                    return deserialized as T;
                }
                catch (ArgumentException)
                {
                    throw new InvalidOperationException(errorMessage);
                }
            }

            if (serializedType == typeof(FiscalYear))
            {
                var year = ParseIntOrThrow(tokens[0], errorMessage);

                try
                {
                    var deserialized = new FiscalYear(year);
                    return deserialized as T;
                }
                catch (ArgumentException)
                {
                    throw new InvalidOperationException(errorMessage);
                }
            }

            if (serializedType == typeof(FiscalUnbounded))
            {
                var deserialized = new FiscalUnbounded();
                return deserialized as T;
            }

            if (serializedType == typeof(GenericMonth))
            {
                var year = ParseIntOrThrow(tokens[0], errorMessage);
                var month = ParseEnumOrThrow<MonthNumber>(tokens[1], errorMessage);

                try
                {
                    var deserialized = new GenericMonth(year, month);
                    return deserialized as T;
                }
                catch (ArgumentException)
                {
                    throw new InvalidOperationException(errorMessage);
                }
            }

            if (serializedType == typeof(GenericQuarter))
            {
                var year = ParseIntOrThrow(tokens[0], errorMessage);
                var quarter = ParseEnumOrThrow<QuarterNumber>(tokens[1], errorMessage);

                try
                {
                    var deserialized = new GenericQuarter(year, quarter);
                    return deserialized as T;
                }
                catch (ArgumentException)
                {
                    throw new InvalidOperationException(errorMessage);
                }
            }

            if (serializedType == typeof(GenericYear))
            {
                var year = ParseIntOrThrow(tokens[0], errorMessage);

                try
                {
                    var deserialized = new GenericYear(year);
                    return deserialized as T;
                }
                catch (ArgumentException)
                {
                    throw new InvalidOperationException(errorMessage);
                }
            }

            if (serializedType == typeof(GenericUnbounded))
            {
                var deserialized = new GenericUnbounded();
                return deserialized as T;
            }

            throw new NotSupportedException("this type of unit-of-time is not supported: " + serializedType);
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

        private static int ParseIntOrThrow(string token, string errorMessage)
        {
            int intValue;
            if (!int.TryParse(token, out intValue))
            {
                throw new InvalidOperationException(errorMessage);
            }

            return intValue;
        }

        private static T ParseEnumOrThrow<T>(string token, string errorMessage)
            where T : struct, IConvertible
        {
            int intValue = ParseIntOrThrow(token, errorMessage);
            var enumType = typeof(T);
            T enumValue = (T)Enum.ToObject(enumType, intValue);
            if (!Enum.IsDefined(enumType, enumValue))
            {
                throw new InvalidOperationException(errorMessage);
            }

            return enumValue;
        }

        private class SerializationFormat
        {
            public Type Type { get; set; }

            public Regex Regex { get; set; }
        }
    }
}

// ReSharper restore CheckNamespace