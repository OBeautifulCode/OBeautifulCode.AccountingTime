﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfTimeExtensions.Conversion.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    using static System.FormattableString;

    /// <summary>
    /// Conversion-related extension methods on <see cref="UnitOfTime"/>.
    /// </summary>
    public static partial class UnitOfTimeExtensions
    {
        /// <summary>
        /// Converts a <see cref="CalendarQuarter"/> to a <see cref="CalendarMonth"/>-based <see cref="ReportingPeriod"/>.
        /// </summary>
        /// <param name="calendarQuarter">The calendar quarter to convert.</param>
        /// <returns>
        /// The calendar month reporting period associated with the specified calendar quarter.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="calendarQuarter"/> is null.</exception>
        public static ReportingPeriod ToCalendarMonthReportingPeriod(
            this CalendarQuarter calendarQuarter)
        {
            if (calendarQuarter == null)
            {
                throw new ArgumentNullException(nameof(calendarQuarter));
            }

            ReportingPeriod result;

            var year = calendarQuarter.Year;

            if (calendarQuarter.QuarterNumber == QuarterNumber.Q1)
            {
                result = new ReportingPeriod(
                    new CalendarMonth(year, MonthOfYear.January),
                    new CalendarMonth(year, MonthOfYear.March));
            }
            else if (calendarQuarter.QuarterNumber == QuarterNumber.Q2)
            {
                result = new ReportingPeriod(
                    new CalendarMonth(year, MonthOfYear.April),
                    new CalendarMonth(year, MonthOfYear.June));
            }
            else if (calendarQuarter.QuarterNumber == QuarterNumber.Q3)
            {
                result = new ReportingPeriod(
                    new CalendarMonth(year, MonthOfYear.July),
                    new CalendarMonth(year, MonthOfYear.September));
            }
            else if (calendarQuarter.QuarterNumber == QuarterNumber.Q4)
            {
                result = new ReportingPeriod(
                    new CalendarMonth(year, MonthOfYear.October),
                    new CalendarMonth(year, MonthOfYear.December));
            }
            else
            {
                throw new NotSupportedException(Invariant($"This {nameof(QuarterNumber)} is not supported: {calendarQuarter.QuarterNumber}."));
            }

            return result;
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
        /// <exception cref="ArgumentException"><paramref name="calendarQuarterThatIsFirstFiscalQuarter"/> is <see cref="QuarterNumber.Invalid"/>.</exception>
        public static CalendarQuarter ToCalendarQuarter(
            this FiscalQuarter fiscalQuarter,
            QuarterNumber calendarQuarterThatIsFirstFiscalQuarter)
        {
            if (fiscalQuarter == null)
            {
                throw new ArgumentNullException(nameof(fiscalQuarter));
            }

            if (calendarQuarterThatIsFirstFiscalQuarter == QuarterNumber.Invalid)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(calendarQuarterThatIsFirstFiscalQuarter)}' == '{QuarterNumber.Invalid}'"), (Exception)null);
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

            var result = new CalendarQuarter(fiscalQuarter.Year, fiscalQuarter.QuarterNumber);

            result = result.InternalPlus(offset);

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
        /// <exception cref="ArgumentException"><paramref name="calendarQuarterThatIsFirstFiscalQuarter"/> is <see cref="QuarterNumber.Invalid"/>.</exception>
        public static FiscalQuarter ToFiscalQuarter(
            this CalendarQuarter calendarQuarter,
            QuarterNumber calendarQuarterThatIsFirstFiscalQuarter)
        {
            if (calendarQuarter == null)
            {
                throw new ArgumentNullException(nameof(calendarQuarter));
            }

            if (calendarQuarterThatIsFirstFiscalQuarter == QuarterNumber.Invalid)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(calendarQuarterThatIsFirstFiscalQuarter)}' == '{QuarterNumber.Invalid}'"), (Exception)null);
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

            var result = new FiscalQuarter(calendarQuarter.Year, calendarQuarter.QuarterNumber);

            result = result.InternalPlus(offset);

            return result;
        }

        /// <summary>
        /// Converts the specified <see cref="IHaveAMonth"/> to a <see cref="GenericMonth"/>.
        /// </summary>
        /// <param name="month">The month to convert.</param>
        /// <returns>
        /// A <see cref="GenericMonth"/> converted from an <see cref="IHaveAMonth"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="month"/> is null.</exception>
        public static GenericMonth ToGenericMonth(
            this IHaveAMonth month)
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
        public static GenericQuarter ToGenericQuarter(
            this IHaveAQuarter quarter)
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
        public static GenericYear ToGenericYear(
            this IHaveAYear year)
        {
            if (year == null)
            {
                throw new ArgumentNullException(nameof(year));
            }

            var result = new GenericYear(year.Year);

            return result;
        }

        /// <summary>
        /// Converts the the specified unit-of-time into the most
        /// granular possible, but equivalent, reporting period.
        /// </summary>
        /// <param name="unitOfTime">The unit-of-time to operate on.</param>
        /// <returns>
        /// A reporting period that addresses the same set of time as <paramref name="unitOfTime"/>,
        /// but is the most granular version possible of that unit-of-time.
        /// An unbounded unit of time is returned as a reporting period with an unbounded start and end.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="unitOfTime"/> is null.</exception>
        public static ReportingPeriod ToMostGranular(
            this UnitOfTime unitOfTime)
        {
            if (unitOfTime == null)
            {
                throw new ArgumentNullException(nameof(unitOfTime));
            }

            if (unitOfTime.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded)
            {
                var result = new ReportingPeriod(unitOfTime, unitOfTime);

                return result;
            }

            if (unitOfTime.UnitOfTimeGranularity == UnitOfTimeGranularity.Year)
            {
                if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Calendar)
                {
                    var calendarYear = unitOfTime as CalendarYear;

                    var result = new ReportingPeriod(calendarYear.InternalGetFirstCalendarDay(), calendarYear.InternalGetLastCalendarDay());

                    return result;
                }

                var year = unitOfTime as IHaveAYear;
                if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Fiscal)
                {
                    // ReSharper disable once PossibleNullReferenceException
                    var result = new ReportingPeriod(new FiscalMonth(year.Year, MonthNumber.One), new FiscalMonth(year.Year, MonthNumber.Twelve));

                    return result;
                }

                if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Generic)
                {
                    // ReSharper disable once PossibleNullReferenceException
                    var result = new ReportingPeriod(new GenericMonth(year.Year, MonthNumber.One), new GenericMonth(year.Year, MonthNumber.Twelve));

                    return result;
                }

                throw new NotSupportedException("This kind of unit-of-time is not supported: " + unitOfTime.UnitOfTimeKind);
            }

            if (unitOfTime.UnitOfTimeGranularity == UnitOfTimeGranularity.Quarter)
            {
                if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Calendar)
                {
                    var calendarQuarter = unitOfTime as CalendarQuarter;

                    var result = new ReportingPeriod(calendarQuarter.InternalGetFirstCalendarDay(), calendarQuarter.InternalGetLastCalendarDay());

                    return result;
                }

                var quarter = unitOfTime as IHaveAQuarter;

                // ReSharper disable once PossibleNullReferenceException
                var startMonth = (((int)quarter.QuarterNumber - 1) * 3) + 1;
                var endMonth = (int)quarter.QuarterNumber * 3;

                if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Fiscal)
                {
                    var result = new ReportingPeriod(new FiscalMonth(quarter.Year, (MonthNumber)startMonth), new FiscalMonth(quarter.Year, (MonthNumber)endMonth));

                    return result;
                }

                if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Generic)
                {
                    var result = new ReportingPeriod(new GenericMonth(quarter.Year, (MonthNumber)startMonth), new GenericMonth(quarter.Year, (MonthNumber)endMonth));

                    return result;
                }

                throw new NotSupportedException("This kind of unit-of-time is not supported: " + unitOfTime.UnitOfTimeKind);
            }

            if (unitOfTime.UnitOfTimeGranularity == UnitOfTimeGranularity.Month)
            {
                if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Calendar)
                {
                    var calendarMonth = unitOfTime as CalendarMonth;

                    var result = new ReportingPeriod(calendarMonth.InternalGetFirstCalendarDay(), calendarMonth.InternalGetLastCalendarDay());

                    return result;
                }

                if ((unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Fiscal) || (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Generic))
                {
                    var result = new ReportingPeriod(unitOfTime, unitOfTime);

                    return result;
                }

                throw new NotSupportedException("This kind of unit-of-time is not supported: " + unitOfTime.UnitOfTimeKind);
            }

            if (unitOfTime.UnitOfTimeGranularity == UnitOfTimeGranularity.Day)
            {
                if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Calendar)
                {
                    var result = new ReportingPeriod(unitOfTime, unitOfTime);

                    return result;
                }

                throw new NotSupportedException("This kind of unit-of-time is not supported: " + unitOfTime.UnitOfTimeKind);
            }

            throw new NotSupportedException("This granularity is not supported: " + unitOfTime.UnitOfTimeGranularity);
        }

        /// <summary>
        /// Creates a reporting period from a unit-of-time.
        /// </summary>
        /// <param name="unitOfTime">The unit-of-time to use in the reporting period.</param>
        /// <returns>
        /// A reporting period where the Start and End components are equal to the specified unit-of-time.
        /// </returns>
        public static ReportingPeriod ToReportingPeriod(
            this UnitOfTime unitOfTime)
        {
            if (unitOfTime == null)
            {
                throw new ArgumentNullException(nameof(unitOfTime));
            }

            var result = new ReportingPeriod(unitOfTime, unitOfTime);

            return result;
        }
    }
}
