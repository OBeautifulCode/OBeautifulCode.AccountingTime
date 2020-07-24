// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalendarDay.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// Represents a calendar day.
    /// </summary>
    public partial class CalendarDay : CalendarUnitOfTime, IAmAConcreteUnitOfTime, IAmBoundedTime, IHaveAMonth, IFormattable, IModelViaCodeGen, IComparableViaCodeGen, IDeclareCompareToForRelativeSortOrderMethod<CalendarDay>, IDeclareToStringMethod
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CalendarDay"/> class.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="monthOfYear">The month of the year.</param>
        /// <param name="dayOfMonth">The day of the month.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="year"/> is less than 1 or greater than 9999.</exception>
        /// <exception cref="ArgumentException"><paramref name="monthOfYear"/> is invalid.</exception>
        /// <exception cref="ArgumentException"><paramref name="dayOfMonth"/> is invalid.</exception>
        /// <exception cref="ArgumentException"><paramref name="dayOfMonth"/> is not a valid day in the specified <paramref name="monthOfYear"/> and <paramref name="year"/>.</exception>
        public CalendarDay(
            int year,
            MonthOfYear monthOfYear,
            DayOfMonth dayOfMonth)
        {
            if (year < 1)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(year)}' < '{1}'"), (Exception)null);
            }

            if (year > 9999)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(year)}' > '{9999}'"), (Exception)null);
            }

            if (monthOfYear == MonthOfYear.Invalid)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(monthOfYear)}' == '{MonthOfYear.Invalid}'"), (Exception)null);
            }

            if (dayOfMonth == DayOfMonth.Invalid)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(dayOfMonth)}' == '{DayOfMonth.Invalid}'"), (Exception)null);
            }

            var totalDaysInMonth = DateTime.DaysInMonth(year, (int)monthOfYear);
            if ((int)dayOfMonth > totalDaysInMonth)
            {
                throw new ArgumentException(Invariant($"day ({dayOfMonth}) is not a valid day in the specified month ({monthOfYear}) and year ({year})."), nameof(dayOfMonth));
            }

            this.Year = year;
            this.MonthOfYear = monthOfYear;
            this.DayOfMonth = dayOfMonth;
        }

        /// <inheritdoc />
        public int Year { get; private set; }

        /// <summary>
        /// Gets the month of the year.
        /// </summary>
        public MonthOfYear MonthOfYear { get; private set; }

        /// <summary>
        /// Gets the day of the month.
        /// </summary>
        public DayOfMonth DayOfMonth { get; private set; }

        /// <inheritdoc />
        public MonthNumber MonthNumber => (MonthNumber)(int)this.MonthOfYear;

        /// <inheritdoc />
        public override UnitOfTimeGranularity UnitOfTimeGranularity => UnitOfTimeGranularity.Day;

        /// <inheritdoc cref="IDeclareCompareToForRelativeSortOrderMethod{CalendarDay}" />
        public RelativeSortOrder CompareToForRelativeSortOrder(
            CalendarDay other)
        {
            if (other == null)
            {
                return RelativeSortOrder.ThisInstanceFollowsTheOtherInstance;
            }
            else
            {
                var thisDateTime = this.ToDateTime();

                var otherDateTime = other.ToDateTime();

                if (thisDateTime < otherDateTime)
                {
                    return RelativeSortOrder.ThisInstancePrecedesTheOtherInstance;
                }
                else if (thisDateTime > otherDateTime)
                {
                    return RelativeSortOrder.ThisInstanceFollowsTheOtherInstance;
                }
                else
                {
                    return RelativeSortOrder.ThisInstanceOccursInTheSamePositionAsTheOtherInstance;
                }
            }
        }

        /// <inheritdoc cref="IDeclareToStringMethod.ToString" />
        public override string ToString()
        {
            var result = Invariant($"{this.Year:D4}-{(int)this.MonthNumber:D2}-{(int)this.DayOfMonth:D2}");

            return result;
        }

        /// <inheritdoc />
        public string ToString(
            string format,
            IFormatProvider formatProvider = null)
        {
            var dateTime = this.ToDateTime();

            var result = dateTime.ToString(format, formatProvider);

            return result;
        }

        /// <summary>
        /// Converts this calendar day to an object of type <see cref="DateTime"/>.
        /// </summary>
        /// <returns>
        /// Gets a <see cref="DateTime"/> version of this calendar day.
        /// </returns>
        public DateTime ToDateTime()
        {
            var result = new DateTime(this.Year, (int)this.MonthNumber, (int)this.DayOfMonth, 0, 0, 0, DateTimeKind.Unspecified);

            return result;
        }
    }
}
