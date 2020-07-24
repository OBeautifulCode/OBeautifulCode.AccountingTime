// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FiscalMonth.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// Represents a fiscal month in a specified year.
    /// </summary>
    public partial class FiscalMonth : FiscalUnitOfTime, IAmAConcreteUnitOfTime, IAmBoundedTime, IHaveAMonth, IModelViaCodeGen, IComparableViaCodeGen, IDeclareCompareToForRelativeSortOrderMethod<FiscalMonth>, IDeclareToStringMethod
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FiscalMonth"/> class.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="monthNumber">The month number.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="year"/> is less than 1 or greater than 9999.</exception>
        /// <exception cref="ArgumentException"><paramref name="monthNumber"/> is invalid.</exception>
        public FiscalMonth(
            int year,
            MonthNumber monthNumber)
        {
            if (year < 1)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(year)}' < '{1}'"), (Exception)null);
            }

            if (year > 9999)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(year)}' > '{9999}'"), (Exception)null);
            }

            if (monthNumber == MonthNumber.Invalid)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(monthNumber)}' == '{MonthNumber.Invalid}'"), (Exception)null);
            }

            this.Year = year;
            this.MonthNumber = monthNumber;
        }

        /// <inheritdoc />
        public int Year { get; private set; }

        /// <inheritdoc />
        public MonthNumber MonthNumber { get; private set; }

        /// <inheritdoc />
        public override UnitOfTimeGranularity UnitOfTimeGranularity => UnitOfTimeGranularity.Month;

        /// <inheritdoc cref="IDeclareCompareToForRelativeSortOrderMethod{FiscalMonth}" />
        public RelativeSortOrder CompareToForRelativeSortOrder(
            FiscalMonth other)
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

            var result = Invariant($"{(int)this.MonthNumber}{monthNumberSuffix} month of FY{this.Year:D4}");

            return result;
        }
    }
}
