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
    /// Extension methods on type <see cref="QuarterNumber"/>.
    /// </summary>
    public static class QuarterNumberExtensions
    {
        /// <summary>
        /// Constructs a <see cref="CalendarQuarter"/> from a <see cref="QuarterNumber"/> and a year.
        /// </summary>
        /// <param name="quarterNumber">The quarter number.</param>
        /// <param name="year">The year.</param>
        /// <returns>
        /// The <see cref="CalendarQuarter"/>.
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
        /// Constructs a <see cref="FiscalQuarter"/> from a <see cref="QuarterNumber"/> and a year.
        /// </summary>
        /// <param name="quarterNumber">The quarter number.</param>
        /// <param name="year">The year.</param>
        /// <returns>
        /// The <see cref="FiscalQuarter"/>.
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
        /// Constructs a <see cref="GenericQuarter"/> from a <see cref="QuarterNumber"/> and a year.
        /// </summary>
        /// <param name="quarterNumber">The quarter number.</param>
        /// <param name="year">The year.</param>
        /// <returns>
        /// The <see cref="GenericQuarter"/>.
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

        /// <summary>
        /// Constructs a <see cref="UnitOfTime"/> whose <see cref="UnitOfTimeGranularity"/> is <see cref="UnitOfTimeGranularity.Quarter"/>
        /// from a <see cref="QuarterNumber"/>, year, and <see cref="UnitOfTimeKind"/>.
        /// </summary>
        /// <param name="quarterNumber">The quarter number.</param>
        /// <param name="year">The year.</param>
        /// <param name="unitOfTimeKind">The unit-of-time kind.</param>
        /// <returns>
        /// The <see cref="UnitOfTime"/>.
        /// </returns>
        /// <exception cref="ArgumentException"><paramref name="quarterNumber"/> is <see cref="QuarterNumber.Invalid"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="unitOfTimeKind"/> is <see cref="UnitOfTimeKind.Invalid"/>.</exception>
        public static UnitOfTime ToUnitOfTime(
            this QuarterNumber quarterNumber,
            int year,
            UnitOfTimeKind unitOfTimeKind)
        {
            if (quarterNumber == QuarterNumber.Invalid)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(quarterNumber)}' == '{QuarterNumber.Invalid}'"), (Exception)null);
            }

            if (unitOfTimeKind == UnitOfTimeKind.Invalid)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(unitOfTimeKind)}' == '{UnitOfTimeKind.Invalid}'"), (Exception)null);
            }

            UnitOfTime result;

            switch (unitOfTimeKind)
            {
                case UnitOfTimeKind.Calendar:
                    result = quarterNumber.ToCalendar(year);
                    break;
                case UnitOfTimeKind.Fiscal:
                    result = quarterNumber.ToFiscal(year);
                    break;
                case UnitOfTimeKind.Generic:
                    result = quarterNumber.ToGeneric(year);
                    break;
                default:
                    throw new NotSupportedException(Invariant($"This {nameof(UnitOfTimeKind)} is not supported: {unitOfTimeKind}"));
            }

            return result;
        }

        /// <summary>
        /// Converts the specified quarter to a string representation of it's ordinal indicator
        /// (i.e. "1st", "2nd", "3rd", or "4th").
        /// </summary>
        /// <param name="quarterNumber">The quarter number.</param>
        /// <returns>
        /// A string representation of the specified quarter number's ordinal indicator.
        /// </returns>
        /// <exception cref="ArgumentException"><paramref name="quarterNumber"/> is <see cref="QuarterNumber.Invalid"/>.</exception>
        public static string ToOrdinalIndicator(
            this QuarterNumber quarterNumber)
        {
            if (quarterNumber == QuarterNumber.Invalid)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(quarterNumber)}' == '{QuarterNumber.Invalid}'"), (Exception)null);
            }

            string result;

            switch ((int)quarterNumber)
            {
                case 1:
                    result = "1st";
                    break;
                case 2:
                    result = "2nd";
                    break;
                case 3:
                    result = "3rd";
                    break;
                case 4:
                    result = "4th";
                    break;
                default:
                    throw new NotSupportedException("This quarter is not supported: " + quarterNumber);
            }

            return result;
        }
    }
}