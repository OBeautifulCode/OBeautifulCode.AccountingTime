// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfTimeGranularityExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime
{
    using System;

    using static System.FormattableString;

    /// <summary>
    /// Extension methods on type <see cref="UnitOfTimeGranularity"/>.
    /// </summary>
    public static class UnitOfTimeGranularityExtensions
    {
        /// <summary>
        /// Determines if a <see cref="UnitOfTimeGranularity"/> is less granular than another <see cref="UnitOfTimeGranularity"/>.
        /// </summary>
        /// <param name="granularity1">The first granularity to compare.</param>
        /// <param name="granularity2">The second granularity to compare.</param>
        /// <returns>
        /// true if the first <see cref="UnitOfTimeGranularity"/> is less granular than the second <see cref="UnitOfTimeGranularity"/>; false otherwise.
        /// </returns>
        public static bool IsLessGranularThan(this UnitOfTimeGranularity granularity1, UnitOfTimeGranularity granularity2)
        {
            var result = !granularity1.IsAsGranularOrMoreGranularThan(granularity2);
            return result;
        }

        /// <summary>
        /// Determines if a <see cref="UnitOfTimeGranularity"/> is as granular or less granular than another <see cref="UnitOfTimeGranularity"/>.
        /// </summary>
        /// <param name="granularity1">The first granularity to compare.</param>
        /// <param name="granularity2">The second granularity to compare.</param>
        /// <returns>
        /// true if the first <see cref="UnitOfTimeGranularity"/> is as granular or less granular than the second <see cref="UnitOfTimeGranularity"/>; false otherwise.
        /// </returns>
        public static bool IsAsGranularOrLessGranularThan(this UnitOfTimeGranularity granularity1, UnitOfTimeGranularity granularity2)
        {
            var result = !granularity1.IsMoreGranularThan(granularity2);
            return result;
        }

        /// <summary>
        /// Determines if a <see cref="UnitOfTimeGranularity"/> is more granular than another <see cref="UnitOfTimeGranularity"/>.
        /// </summary>
        /// <param name="granularity1">The first granularity to compare.</param>
        /// <param name="granularity2">The second granularity to compare.</param>
        /// <returns>
        /// true if the first <see cref="UnitOfTimeGranularity"/> is more granular than the second <see cref="UnitOfTimeGranularity"/>; false otherwise.
        /// </returns>
        public static bool IsMoreGranularThan(this UnitOfTimeGranularity granularity1, UnitOfTimeGranularity granularity2)
        {
            int granularityScore1 = GetGranularityScore(granularity1);
            int granularityScore2 = GetGranularityScore(granularity2);
            var result = granularityScore1 < granularityScore2;
            return result;
        }

        /// <summary>
        /// Determines if a <see cref="UnitOfTimeGranularity"/> is as granular or more granular than another <see cref="UnitOfTimeGranularity"/>.
        /// </summary>
        /// <param name="granularity1">The first granularity to compare.</param>
        /// <param name="granularity2">The second granularity to compare.</param>
        /// <returns>
        /// true if the first <see cref="UnitOfTimeGranularity"/> is as granular or more granular than the second <see cref="UnitOfTimeGranularity"/>; false otherwise.
        /// </returns>
        public static bool IsAsGranularOrMoreGranularThan(this UnitOfTimeGranularity granularity1, UnitOfTimeGranularity granularity2)
        {
            var isMoreGranular = granularity1.IsMoreGranularThan(granularity2);
            var result = isMoreGranular || (granularity1 == granularity2);
            return result;
        }

        private static int GetGranularityScore(UnitOfTimeGranularity granularity)
        {
            if (granularity == UnitOfTimeGranularity.Invalid)
            {
                throw new ArgumentException(Invariant($"{nameof(granularity)} is {nameof(UnitOfTimeGranularity.Invalid)}"));
            }

            switch (granularity)
            {
                case UnitOfTimeGranularity.Day:
                    return 1;
                case UnitOfTimeGranularity.Month:
                    return 2;
                case UnitOfTimeGranularity.Quarter:
                    return 3;
                case UnitOfTimeGranularity.Year:
                    return 4;
                case UnitOfTimeGranularity.Unbounded:
                    return 5;
                default:
                    throw new NotSupportedException(Invariant($"This {nameof(UnitOfTimeGranularity)} is not supported: {granularity}"));
            }
        }
    }
}

// ReSharper restore CheckNamespace