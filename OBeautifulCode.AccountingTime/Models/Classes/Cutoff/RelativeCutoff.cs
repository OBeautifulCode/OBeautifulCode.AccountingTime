// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RelativeCutoff.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// A cutoff relative to some reporting period.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class RelativeCutoff : CutoffBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RelativeCutoff"/> class.
        /// </summary>
        /// <param name="duration">The duration relative to the reporting period.</param>
        /// <param name="beforeOrAfter">Specifies whether the duration is applied before or after the reporting period.</param>
        public RelativeCutoff(
            Duration duration,
            TimeComparison beforeOrAfter)
        {
            if (duration == null)
            {
                throw new ArgumentNullException(nameof(duration));
            }

            if ((beforeOrAfter != TimeComparison.Before) && (beforeOrAfter != TimeComparison.After))
            {
                throw new ArgumentOutOfRangeException(nameof(beforeOrAfter), Invariant($"{nameof(beforeOrAfter)} is neither {nameof(TimeComparison)}.{nameof(TimeComparison.Before)} nor {nameof(TimeComparison)}.{nameof(TimeComparison.After)}"));
            }

            this.Duration = duration;
            this.BeforeOrAfter = beforeOrAfter;
        }

        /// <summary>
        /// Gets the duration relative to the reporting period.
        /// </summary>
        public Duration Duration { get; private set; }

        /// <summary>
        /// Gets a value that specifies whether the duration is applied before or after the reporting period.
        /// </summary>
        public TimeComparison BeforeOrAfter { get; private set; }
    }
}
