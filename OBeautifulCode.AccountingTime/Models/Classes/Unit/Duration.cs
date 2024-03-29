﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Duration.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;
    using OBeautifulCode.Type;
    using static System.FormattableString;

    /// <summary>
    /// A specified quantity of a specified unit.
    /// </summary>
    public partial class Duration : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Duration"/> class.
        /// </summary>
        /// <param name="quantity">The quantity of the specified <paramref name="unit"/>.</param>
        /// <param name="unit">The unit.</param>
        public Duration(
            int quantity,
            Unit unit)
        {
            if (unit == null)
            {
                throw new ArgumentNullException(nameof(unit));
            }

            if (unit.Granularity == UnitOfTimeGranularity.Unbounded)
            {
                throw new ArgumentException(Invariant($"{nameof(unit)}.{nameof(unit.Granularity)} is {nameof(UnitOfTimeGranularity)}.{nameof(UnitOfTimeGranularity.Unbounded)}."));
            }

            this.Quantity = quantity;
            this.Unit = unit;
        }

        /// <summary>
        /// Gets the quantity of the specified <see cref="Unit"/>.
        /// </summary>
        public int Quantity { get; private set; }

        /// <summary>
        /// Gets the unit.
        /// </summary>
        public Unit Unit { get; private set; }
    }
}
