// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfTimeKindExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    using static System.FormattableString;

    /// <summary>
    /// Extension methods on type <see cref="UnitOfTimeKind"/>.
    /// </summary>
    public static class UnitOfTimeKindExtensions
    {
        /// <summary>
        /// Constructs an unbounded unit-of-time of the specified kind.
        /// </summary>
        /// <param name="unitOfTimeKind">The kind of unit-of-time.</param>
        /// <returns>
        /// The unbounded unit-of-time.
        /// </returns>
        /// <exception cref="ArgumentException"><paramref name="unitOfTimeKind"/> is <see cref="UnitOfTimeKind.Invalid"/>.</exception>
        public static UnitOfTime ToUnbounded(
            this UnitOfTimeKind unitOfTimeKind)
        {
            if (unitOfTimeKind == UnitOfTimeKind.Invalid)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(unitOfTimeKind)}' == '{UnitOfTimeKind.Invalid}'"), (Exception)null);
            }

            UnitOfTime result;

            switch (unitOfTimeKind)
            {
                case UnitOfTimeKind.Calendar:
                    result = new CalendarUnbounded();
                    break;
                case UnitOfTimeKind.Fiscal:
                    result = new FiscalUnbounded();
                    break;
                case UnitOfTimeKind.Generic:
                    result = new GenericUnbounded();
                    break;
                default:
                    throw new NotSupportedException(Invariant($"This {nameof(UnitOfTimeKind)} is not supported: {unitOfTimeKind}."));
            }

            return result;
        }
    }
}