// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GenericMonth.cs" company="OBeautifulCode">
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
    /// Represents a generic month in a specified year.
    /// </summary>
    public partial class GenericMonth : GenericUnitOfTime, IAmAConcreteUnitOfTime, IAmBoundedTime, IHaveAMonth, IModelViaCodeGen, IComparableViaCodeGen, IDeclareCompareToForRelativeSortOrderMethod<GenericMonth>, IDeclareToStringMethod
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericMonth"/> class.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="monthNumber">The month number.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="year"/> is less than 1 or greater than 9999.</exception>
        /// <exception cref="ArgumentException"><paramref name="monthNumber"/> is invalid.</exception>
        public GenericMonth(
            int year,
            MonthNumber monthNumber)
        {
            new { year }.AsArg().Must().BeGreaterThanOrEqualTo(1).And().BeLessThanOrEqualTo(9999);
            new { monthNumber }.AsArg().Must().NotBeEqualTo(MonthNumber.Invalid);

            this.Year = year;
            this.MonthNumber = monthNumber;
        }

        /// <inheritdoc />
        public int Year { get; private set; }

        /// <inheritdoc />
        public MonthNumber MonthNumber { get; private set; }

        /// <inheritdoc />
        public override UnitOfTimeGranularity UnitOfTimeGranularity => UnitOfTimeGranularity.Month;

        /// <inheritdoc cref="IDeclareCompareToForRelativeSortOrderMethod{GenericMonth}" />
        public RelativeSortOrder CompareToForRelativeSortOrder(
            GenericMonth other)
        {
            if (other == null)
            {
                return RelativeSortOrder.ThisInstanceFollowsTheOtherInstance;
            }
            else
            {
                var thisDateTime = new DateTime(this.Year, (int)this.MonthNumber, 1);

                var otherDateTime = new DateTime(other.Year, (int)other.MonthNumber, 1);

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
            string monthNumberSuffix;
            switch (this.MonthNumber)
            {
                case MonthNumber.One:
                    monthNumberSuffix = "st";
                    break;
                case MonthNumber.Two:
                    monthNumberSuffix = "nd";
                    break;
                case MonthNumber.Three:
                    monthNumberSuffix = "rd";
                    break;
                default:
                    monthNumberSuffix = "th";
                    break;
            }

            var result = Invariant($"{(int)this.MonthNumber}{monthNumberSuffix} month of {this.Year:D4}");

            return result;
        }
    }
}
