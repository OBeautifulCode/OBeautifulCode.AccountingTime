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