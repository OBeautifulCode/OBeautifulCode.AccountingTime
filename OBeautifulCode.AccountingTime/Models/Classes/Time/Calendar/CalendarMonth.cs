// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalendarMonth.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// Represents a calendar month in a specified year.
    /// </summary>
    public partial class CalendarMonth : CalendarUnitOfTime, IAmAConcreteUnitOfTime, IAmBoundedTime, IHaveAMonth, IModelViaCodeGen, IComparableViaCodeGen, IDeclareCompareToForRelativeSortOrderMethod<CalendarMonth>, IDeclareToStringMethod
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CalendarMonth"/> class.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="monthOfYear">The month of the year.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="year"/> is less than 1 or greater than 9999.</exception>
        /// <exception cref="ArgumentException"><paramref name="monthOfYear"/> is invalid.</exception>
        public CalendarMonth(
            int year,
            MonthOfYear monthOfYear)
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

            this.Year = year;
            this.MonthOfYear = monthOfYear;
        }

        /// <inheritdoc />
        public int Year { get; private set; }

        /// <summary>
        /// Gets the month of the year.
        /// </summary>
        public MonthOfYear MonthOfYear { get; private set; }

        /// <inheritdoc />
        public MonthNumber MonthNumber => (MonthNumber)(int)this.MonthOfYear;

        /// <inheritdoc />
        public override UnitOfTimeGranularity UnitOfTimeGranularity => UnitOfTimeGranularity.Month;

        /// <inheritdoc cref="IDeclareCompareToForRelativeSortOrderMethod{CalendarMonth}" />
        public RelativeSortOrder CompareToForRelativeSortOrder(
            CalendarMonth other)
        {
            if (other == null)
            {
                return RelativeSortOrder.ThisInstanceFollowsTheOtherInstance;
            }
            else
            {
                var thisDateTime = new DateTime(this.Year, (int)this.MonthOfYear, 1);

                var otherDateTime = new DateTime(other.Year, (int)other.MonthOfYear, 1);

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
            var result = Invariant($"{this.Year:D4}-{(int)this.MonthOfYear:D2}");

            return result;
        }
    }
}
