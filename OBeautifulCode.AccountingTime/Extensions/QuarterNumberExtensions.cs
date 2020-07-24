// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QuarterNumberExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    using static System.FormattableString;

    /// <summary>
    /// Extension methods on type <see cref="UnitOfTimeGranularity"/>.
    /// </summary>
    public static class QuarterNumberExtensions
    {
        /// <summary>
        /// Constructs a calendar quarter from a <see cref="QuarterNumber"/> and a year.
        /// </summary>
        /// <param name="quarterNumber">The quarter number.</param>
        /// <param name="year">The year.</param>
        /// <returns>
        /// A calendar quarter constructed from the specified <see cref="QuarterNumber"/> and year.
        /// </returns>
        /// <exception cref="ArgumentException"><paramref name="quarterNumber"/> is <see cref="QuarterNumber.Invalid"/>.</exception>
        public static CalendarQuarter ToCalendar(
            this QuarterNumber quarterNumber,
            int year)
        {
            if (quarterNumber == QuarterNumber.Invalid)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(quarterNumber)}' == '{QuarterNumber.Invalid}'"), (Exception)null);
            }

            var result = new CalendarQuarter(year, quarterNumber);

            return result;
        }

        /// <summary>
        /// Constructs a fiscal quarter from a <see cref="QuarterNumber"/> and a year.
        /// </summary>
        /// <param name="quarterNumber">The quarter number.</param>
        /// <param name="year">The year.</param>
        /// <returns>
        /// A fiscal quarter constructed from the specified <see cref="QuarterNumber"/> and year.
        /// </returns>
        /// <exception cref="ArgumentException"><paramref name="quarterNumber"/> is <see cref="QuarterNumber.Invalid"/>.</exception>
        public static FiscalQuarter ToFiscal(
            this QuarterNumber quarterNumber,
            int year)
        {
            if (quarterNumber == QuarterNumber.Invalid)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(quarterNumber)}' == '{QuarterNumber.Invalid}'"), (Exception)null);
            }

            var result = new FiscalQuarter(year, quarterNumber);

            return result;
        }

        /// <summary>
        /// Constructs a generic quarter from a <see cref="QuarterNumber"/> and a year.
        /// </summary>
        /// <param name="quarterNumber">The quarter number.</param>
        /// <param name="year">The year.</param>
        /// <returns>
        /// A generic quarter constructed from the specified <see cref="QuarterNumber"/> and year.
        /// </returns>
        /// <exception cref="ArgumentException"><paramref name="quarterNumber"/> is <see cref="QuarterNumber.Invalid"/>.</exception>
        public static GenericQuarter ToGeneric(
            this QuarterNumber quarterNumber,
            int year)
        {
            if (quarterNumber == QuarterNumber.Invalid)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(quarterNumber)}' == '{QuarterNumber.Invalid}'"), (Exception)null);
            }

            var result = new GenericQuarter(year, quarterNumber);

            return result;
        }
    }
}