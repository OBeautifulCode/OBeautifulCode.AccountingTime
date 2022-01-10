// --------------------------------------------------------------------------------------------------------------------
// <copyright file="YearNumberExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;
    using static System.FormattableString;

    /// <summary>
    /// Extension methods on integer year number.
    /// </summary>
    public static class YearNumberExtensions
    {
        /// <summary>
        /// Constructs a <see cref="CalendarYear"/> from a year number.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns>
        /// The <see cref="CalendarYear"/>.
        /// </returns>
        public static CalendarYear ToCalendar(
            this int year)
        {
            var result = new CalendarYear(year);

            return result;
        }

        /// <summary>
        /// Constructs a <see cref="FiscalYear"/> from year number.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns>
        /// The <see cref="FiscalYear"/>.
        /// </returns>
        public static FiscalYear ToFiscal(
            this int year)
        {
            var result = new FiscalYear(year);

            return result;
        }

        /// <summary>
        /// Constructs a <see cref="GenericYear"/> from a year number.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns>
        /// The <see cref="GenericYear"/>.
        /// </returns>
        public static GenericYear ToGeneric(
            this int year)
        {
            var result = new GenericYear(year);

            return result;
        }

        /// <summary>
        /// Constructs a <see cref="UnitOfTime"/> whose <see cref="UnitOfTimeGranularity"/> is <see cref="UnitOfTimeGranularity.Year"/>
        /// from a year number and <see cref="UnitOfTimeKind"/>.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="unitOfTimeKind">The unit-of-time kind.</param>
        /// <returns>
        /// The <see cref="UnitOfTime"/>.
        /// </returns>
        /// <exception cref="ArgumentException"><paramref name="unitOfTimeKind"/> is <see cref="UnitOfTimeKind.Invalid"/>.</exception>
        public static UnitOfTime ToUnitOfTime(
            this int year,
            UnitOfTimeKind unitOfTimeKind)
        {
            if (unitOfTimeKind == UnitOfTimeKind.Invalid)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(unitOfTimeKind)}' == '{UnitOfTimeKind.Invalid}'"), (Exception)null);
            }

            UnitOfTime result;

            switch (unitOfTimeKind)
            {
                case UnitOfTimeKind.Calendar:
                    result = year.ToCalendar();
                    break;
                case UnitOfTimeKind.Fiscal:
                    result = year.ToFiscal();
                    break;
                case UnitOfTimeKind.Generic:
                    result = year.ToGeneric();
                    break;
                default:
                    throw new NotSupportedException(Invariant($"This {nameof(UnitOfTimeKind)} is not supported: {unitOfTimeKind}"));
            }

            return result;
        }
    }
}