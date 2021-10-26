// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FiscalUnitOfTime.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// Represents a unit of time in the context of some company's fiscal year.
    /// </summary>
    public abstract partial class FiscalUnitOfTime : UnitOfTime, IModelViaCodeGen, IComparableViaCodeGen
    {
        /// <inheritdoc />
        public override UnitOfTimeKind UnitOfTimeKind => UnitOfTimeKind.Fiscal;
    }
}
