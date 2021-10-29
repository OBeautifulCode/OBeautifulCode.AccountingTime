// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FixedCutoff.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// A fixed cutoff.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class FixedCutoff : CutoffBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FixedCutoff"/> class.
        /// </summary>
        /// <param name="unitOfTime">The unit-of-time that is the cutoff.</param>
        public FixedCutoff(
            UnitOfTime unitOfTime)
        {
            if (unitOfTime == null)
            {
                throw new ArgumentNullException(nameof(unitOfTime));
            }

            this.UnitOfTime = unitOfTime;
        }

        /// <summary>
        /// Gets the unit-of-time that is the cutoff.
        /// </summary>
        public UnitOfTime UnitOfTime { get; private set; }
    }
}
