// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FiscalUnbounded.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// Represents an unbounded fiscal unit-of-time.
    /// </summary>
    public partial class FiscalUnbounded : FiscalUnitOfTime, IAmAConcreteUnitOfTime, IAmUnboundedTime, IModelViaCodeGen, IComparableViaCodeGen, IDeclareCompareToForRelativeSortOrderMethod<FiscalUnbounded>, IDeclareToStringMethod
    {
        /// <inheritdoc />
        public override UnitOfTimeGranularity UnitOfTimeGranularity => UnitOfTimeGranularity.Unbounded;

        /// <inheritdoc cref="IDeclareCompareToForRelativeSortOrderMethod{FiscalUnbounded}" />
        public RelativeSortOrder CompareToForRelativeSortOrder(
            FiscalUnbounded other)
        {
            if (other == null)
            {
                return RelativeSortOrder.ThisInstanceFollowsTheOtherInstance;
            }
            else
            {
                return RelativeSortOrder.ThisInstanceOccursInTheSamePositionAsTheOtherInstance;
            }
        }

        /// <inheritdoc cref="IDeclareToStringMethod.ToString"/>
        public override string ToString()
        {
            var result = Invariant($"fiscal unbounded");

            return result;
        }
    }
}
