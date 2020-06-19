// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FiscalQuarter.cs" company="OBeautifulCode">
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
    /// Represents a fiscal quarter of a specified year.
    /// </summary>
    public partial class FiscalQuarter : FiscalUnitOfTime, IAmAConcreteUnitOfTime, IAmBoundedTime, IHaveAQuarter, IModelViaCodeGen, IComparableViaCodeGen, IDeclareCompareToForRelativeSortOrderMethod<FiscalQuarter>, IDeclareToStringMethod
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FiscalQuarter"/> class.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="quarterNumber">The quarter number.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="year"/> is less than 1 or greater than 9999.</exception>
        /// <exception cref="ArgumentException"><paramref name="quarterNumber"/> is invalid.</exception>
        public FiscalQuarter(
            int year,
            QuarterNumber quarterNumber)
        {
            new { year }.AsArg().Must().BeGreaterThanOrEqualTo(1).And().BeLessThanOrEqualTo(9999);
            new { quarterNumber }.AsArg().Must().NotBeEqualTo(QuarterNumber.Invalid);

            this.Year = year;
            this.QuarterNumber = quarterNumber;
        }

        /// <inheritdoc />
        public QuarterNumber QuarterNumber { get; private set; }

        /// <inheritdoc />
        public int Year { get; private set; }

        /// <inheritdoc />
        public override UnitOfTimeGranularity UnitOfTimeGranularity => UnitOfTimeGranularity.Quarter;

        /// <inheritdoc cref="IDeclareCompareToForRelativeSortOrderMethod{FiscalQuarter}" />
        public RelativeSortOrder CompareToForRelativeSortOrder(
            FiscalQuarter other)
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
