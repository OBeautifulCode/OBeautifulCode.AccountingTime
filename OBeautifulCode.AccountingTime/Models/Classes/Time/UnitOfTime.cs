// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfTime.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using OBeautifulCode.Type;

    /// <summary>
    /// Represents a unit of time, such as a calendar month, fiscal quarter, or general year.
    /// </summary>
    public abstract partial class UnitOfTime : IModelViaCodeGen, IComparableViaCodeGen
    {
        /// <summary>
        /// Gets the kind of the unit-of-time.
        /// </summary>
        public abstract UnitOfTimeKind UnitOfTimeKind { get; }

        /// <summary>
        /// Gets the granularity of the unit-of-time.
        /// </summary>
        public abstract UnitOfTimeGranularity UnitOfTimeGranularity { get; }
    }
}
