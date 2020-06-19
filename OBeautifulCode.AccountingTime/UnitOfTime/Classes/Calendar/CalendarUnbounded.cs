// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalendarUnbounded.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// Represents an unbounded calendar unit-of-time.
    /// </summary>
    public partial class CalendarUnbounded : CalendarUnitOfTime, IAmAConcreteUnitOfTime, IAmUnboundedTime, IModelViaCodeGen, IComparableViaCodeGen, IDeclareCompareToForRelativeSortOrderMethod<CalendarUnbounded>, IDeclareToStringMethod
    {
        /// <inheritdoc />
        public override UnitOfTimeGranularity UnitOfTimeGranularity => UnitOfTimeGranularity.Unbounded;

        /// <inheritdoc cref="IDeclareCompareToForRelativeSortOrderMethod{CalendarUnbounded}" />
        public RelativeSortOrder CompareToForRelativeSortOrder(
            CalendarUnbounded other)
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
            var result = Invariant($"calendar unbounded");

            return result;
        }
    }
}
