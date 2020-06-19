// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GenericUnbounded.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// Represents an unbounded generic unit-of-time.
    /// </summary>
    public partial class GenericUnbounded : GenericUnitOfTime, IAmAConcreteUnitOfTime, IAmUnboundedTime, IModelViaCodeGen, IComparableViaCodeGen, IDeclareCompareToForRelativeSortOrderMethod<GenericUnbounded>, IDeclareToStringMethod
    {
        /// <inheritdoc />
        public override UnitOfTimeGranularity UnitOfTimeGranularity => UnitOfTimeGranularity.Unbounded;

        /// <inheritdoc cref="IDeclareCompareToForRelativeSortOrderMethod{GenericUnbounded}" />
        public RelativeSortOrder CompareToForRelativeSortOrder(
            GenericUnbounded other)
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

        /// <inheritdoc cref="IDeclareToStringMethod.ToString" />
        public override string ToString()
        {
            var result = Invariant($"generic unbounded");

            return result;
        }
    }
}
