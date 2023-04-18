// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Unit.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// Specifies a unit of accounting time.
    /// </summary>
    public partial class Unit : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Unit"/> class.
        /// </summary>
        /// <param name="kind">The kind of unit.</param>
        /// <param name="granularity">The granularity of the unit.</param>
        public Unit(
            UnitOfTimeKind kind,
            UnitOfTimeGranularity granularity)
        {
            if (kind == UnitOfTimeKind.Invalid)
            {
                throw new ArgumentOutOfRangeException(nameof(kind), Invariant($"{nameof(kind)} is {nameof(UnitOfTimeKind)}.{nameof(UnitOfTimeKind.Invalid)}."));
            }

            if (granularity == UnitOfTimeGranularity.Invalid)
            {
                throw new ArgumentOutOfRangeException(nameof(granularity), Invariant($"{nameof(granularity)} is {nameof(UnitOfTimeGranularity)}.{nameof(UnitOfTimeGranularity.Invalid)}."));
            }

            if ((granularity == UnitOfTimeGranularity.Day) && (kind != UnitOfTimeKind.Calendar))
            {
                throw new ArgumentException(Invariant($"{nameof(granularity)} of {nameof(UnitOfTimeGranularity)}.{nameof(UnitOfTimeGranularity.Day)} is only applicable when {nameof(kind)} is {nameof(UnitOfTimeKind)}.{nameof(UnitOfTimeKind.Calendar)}; specified value is {kind}."));
            }

            this.Kind = kind;
            this.Granularity = granularity;
        }

        /// <summary>
        /// Gets the kind of unit.
        /// </summary>
        public UnitOfTimeKind Kind { get; private set; }

        /// <summary>
        /// Gets the granularity of the unit.
        /// </summary>
        public UnitOfTimeGranularity Granularity { get; private set; }
    }
}
