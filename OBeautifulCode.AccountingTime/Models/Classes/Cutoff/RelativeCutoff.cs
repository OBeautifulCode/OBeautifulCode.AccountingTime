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
        /// <param name="startOrEnd">Specifies whether the duration is applied to the start or end of the reporting period.</param>
        public RelativeCutoff(
            Duration duration,
            ReportingPeriodComponent startOrEnd)
        {
            if (duration == null)
            {
                throw new ArgumentNullException(nameof(duration));
            }

            if ((startOrEnd != ReportingPeriodComponent.Start) && (startOrEnd != ReportingPeriodComponent.End))
            {
                throw new ArgumentOutOfRangeException(nameof(startOrEnd), Invariant($"{nameof(startOrEnd)} is neither {nameof(ReportingPeriodComponent)}.{nameof(ReportingPeriodComponent.Start)} nor {nameof(ReportingPeriodComponent)}.{nameof(ReportingPeriodComponent.End)}"));
            }

            this.Duration = duration;
            this.StartOrEnd = startOrEnd;
        }

        /// <summary>
        /// Gets the duration relative to the reporting period.
        /// </summary>
        public Duration Duration { get; private set; }

        /// <summary>
        /// Gets a value that specifies whether the duration is applied to the start or end of the reporting period.
        /// </summary>
        public ReportingPeriodComponent StartOrEnd { get; private set; }
    }
}
