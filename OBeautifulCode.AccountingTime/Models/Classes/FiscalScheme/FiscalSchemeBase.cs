// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FiscalSchemeBase.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using OBeautifulCode.Type;

    /// <summary>
    /// Represents a scheme/convention for defining an entity's fiscal time.
    /// </summary>
    public abstract partial class FiscalSchemeBase : IModelViaCodeGen
    {
        /// <summary>
        /// Gets the granularity of the scheme.
        /// </summary>
        /// <returns>
        /// The granularity of the scheme.
        /// </returns>
        public abstract UnitOfTimeGranularity GetGranularity();
    }
}
