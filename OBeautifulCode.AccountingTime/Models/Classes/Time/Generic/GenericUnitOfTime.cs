// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GenericUnitOfTime.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// Represents a generic unit of time, without any context.
    /// </summary>
    public abstract partial class GenericUnitOfTime : UnitOfTime, IModelViaCodeGen, IComparableViaCodeGen
    {
        /// <inheritdoc />
        public override UnitOfTimeKind UnitOfTimeKind => UnitOfTimeKind.Generic;
    }
}
