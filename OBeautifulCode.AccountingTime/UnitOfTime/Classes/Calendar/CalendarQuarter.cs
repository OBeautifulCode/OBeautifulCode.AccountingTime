// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalendarQuarter.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// Represents a calendar quarter of a specified year.
    /// </summary>
    public partial class CalendarQuarter : CalendarUnitOfTime, IAmAConcreteUnitOfTime, IAmBoundedTime, IHaveAQuarter, IModelViaCodeGen, IComparableViaCodeGen, IDeclareCompareToForRelativeSortOrderMethod<CalendarQuarter>, IDeclareToStringMethod
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CalendarQuarter"/> class.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="quarterNumber">The quarter number.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="year"/> is less than 1 or greater than 9999.</exception>
        /// <exception cref="ArgumentException"><paramref name="quarterNumber"/> is invalid.</exception>
        public CalendarQuarter(
            int year,
            QuarterNumber quarterNumber)
        {
            if (year < 1)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(year)}' < '{1}'"), (Exception)null);
            }

            if (year > 9999)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(year)}' > '{9999}'"), (Exception)null);
            }

            if (quarterNumber == QuarterNumber.Invalid)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(quarterNumber)}' == '{QuarterNumber.Invalid}'"), (Exception)null);
            }

            this.Year = year;
            this.QuarterNumber = quarterNumber;
        }

        /// <inheritdoc />
        public QuarterNumber QuarterNumber { get; private set; }

        /// <inheritdoc />
        public int Year { get; private set; }

        /// <inheritdoc />
        public override UnitOfTimeGranularity UnitOfTimeGranularity => UnitOfTimeGranularity.Quarter;

        /// <inheritdoc cref="IDeclareCompareToForRelativeSortOrderMethod{CalendarQuarter}" />
        public RelativeSortOrder CompareToForRelativeSortOrder(
            CalendarQuarter other)
        {
            if (other == null)
            {
                return RelativeSortOrder.ThisInstanceFollowsTheOtherInstance;
            }
            else
            {
                var thisDateTime = new DateTime(this.Year, (int)this.QuarterNumber, 1);

                var otherDateTime = new DateTime(other.Year, (int)other.QuarterNumber, 1);

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
            var result = Invariant($"{(int)this.QuarterNumber}Q{this.Year:D4}");

            return result;
        }
    }
}
