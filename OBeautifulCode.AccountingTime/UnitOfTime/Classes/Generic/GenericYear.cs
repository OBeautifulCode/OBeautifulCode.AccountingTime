// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GenericYear.cs" company="OBeautifulCode">
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
    /// Represents a generic year.
    /// </summary>
    public partial class GenericYear : GenericUnitOfTime, IAmAConcreteUnitOfTime, IAmBoundedTime, IHaveAYear, IModelViaCodeGen, IComparableViaCodeGen, IDeclareCompareToForRelativeSortOrderMethod<GenericYear>, IDeclareToStringMethod
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericYear"/> class.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="year"/> is less than 1 or greater than 9999.</exception>
        public GenericYear(
            int year)
        {
            new { year }.AsArg().Must().BeGreaterThanOrEqualTo(1).And().BeLessThanOrEqualTo(9999);

            this.Year = year;
        }

        /// <inheritdoc />
        public int Year { get; private set; }

        /// <inheritdoc />
        public override UnitOfTimeGranularity UnitOfTimeGranularity => UnitOfTimeGranularity.Year;

        /// <inheritdoc cref="IDeclareCompareToForRelativeSortOrderMethod{GenericYear}" />
        public RelativeSortOrder CompareToForRelativeSortOrder(
            GenericYear other)
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

        /// <inheritdoc cref="IDeclareToStringMethod.ToString" />
        public override string ToString()
        {
            var result = Invariant($"{this.Year:D4}");

            return result;
        }
    }
}
