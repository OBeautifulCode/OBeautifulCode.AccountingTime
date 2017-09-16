﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfTimeExtensions.Math.cs" company="OBeautifulCode">
//    Copyright (c) OBeautifulCode 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime
{
    using System;

    using static System.FormattableString;

    /// <summary>
    /// Math-related extension methods on <see cref="UnitOfTime"/>.
    /// </summary>
    public static partial class UnitOfTimeExtensions
    {
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
    }
}

// ReSharper restore CheckNamespace