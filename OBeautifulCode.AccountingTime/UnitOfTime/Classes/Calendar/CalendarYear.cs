// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalendarYear.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// Represents a calendar year.
    /// </summary>
    public partial class CalendarYear : CalendarUnitOfTime, IAmAConcreteUnitOfTime, IAmBoundedTime, IHaveAYear, IModelViaCodeGen, IComparableViaCodeGen, IDeclareCompareToForRelativeSortOrderMethod<CalendarYear>, IDeclareToStringMethod
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CalendarYear"/> class.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="year"/> is less than 1 or greater than 9999.</exception>
        public CalendarYear(
            int year)
        {
            new { year }.AsArg().Must().BeGreaterThanOrEqualTo(1).And().BeLessThanOrEqualTo(9999);

            this.Year = year;
        }

        /// <inheritdoc />
        public int Year { get; private set; }

        /// <inheritdoc />
        public override UnitOfTimeGranularity UnitOfTimeGranularity => UnitOfTimeGranularity.Year;

        /// <inheritdoc cref="IDeclareCompareToForRelativeSortOrderMethod{CalendarYear}" />
        public RelativeSortOrder CompareToForRelativeSortOrder(
            CalendarYear other)
        {
            if (other == null)
            {
                return RelativeSortOrder.ThisInstanceFollowsTheOtherInstance;
            }
            else
            {
                if (this.Year < other.Year)
                {
                    return RelativeSortOrder.ThisInstancePrecedesTheOtherInstance;
                }
                else if (this.Year > other.Year)
                {
                    return RelativeSortOrder.ThisInstanceFollowsTheOtherInstance;
                }
                else
                {
                    return RelativeSortOrder.ThisInstanceOccursInTheSamePositionAsTheOtherInstance;
                }
            }
        }

        /// <inheritdoc cref="IDeclareToStringMethod.ToString"/>
        public override string ToString()
        {
            var result = Invariant($"CY{this.Year:D4}");

            return result;
        }
    }
}
