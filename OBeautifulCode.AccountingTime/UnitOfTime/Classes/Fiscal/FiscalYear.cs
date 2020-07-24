// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FiscalYear.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// Represents a fiscal year.
    /// </summary>
    public partial class FiscalYear : FiscalUnitOfTime, IAmAConcreteUnitOfTime, IAmBoundedTime, IHaveAYear, IModelViaCodeGen, IComparableViaCodeGen, IDeclareCompareToForRelativeSortOrderMethod<FiscalYear>, IDeclareToStringMethod
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FiscalYear"/> class.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="year"/> is less than 1 or greater than 9999.</exception>
        public FiscalYear(
            int year)
        {
            if (year < 1)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(year)}' < '{1}'"), (Exception)null);
            }

            if (year > 9999)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(year)}' > '{9999}'"), (Exception)null);
            }

            this.Year = year;
        }

        /// <inheritdoc />
        public int Year { get; private set; }

        /// <inheritdoc />
        public override UnitOfTimeGranularity UnitOfTimeGranularity => UnitOfTimeGranularity.Year;

        /// <inheritdoc cref="IDeclareCompareToForRelativeSortOrderMethod{FiscalYear}" />
        public RelativeSortOrder CompareToForRelativeSortOrder(
            FiscalYear other)
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
            var result = Invariant($"FY{this.Year:D4}");

            return result;
        }
    }
}
