// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriodCriteria.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;
    using OBeautifulCode.Type;
    using static System.FormattableString;

    /// <summary>
    /// Specifies the criteria for a reporting period.
    /// </summary>
    /// <remarks>
    /// In the future, other constraints could be added to this class (e.g. reporting period must span 2-4 months)
    /// or this class could be renamed to scope it to the kind of constraints provided and then new classes
    /// could be created to encapsulate other constraints.  Then perhaps multiple criteria could be specified in an
    /// AND or OR manner and each evaluated against some given reporting period.
    /// </remarks>
    public partial class ReportingPeriodCriteria : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReportingPeriodCriteria"/> class.
        /// </summary>
        /// <param name="unit">OPTIONAL unit.  DEFAULT is no unit.</param>
        /// <param name="boundsConstraint">OPTIONAL constraint on the bounds of the reporting period.  DEFAULT is no constraint.</param>
        /// <param name="spanConstraint">A constraint on the span of the reporting period.  DEFAULT is no constraint.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="boundsConstraint"/> is <see cref="ReportingPeriodBoundsConstraint.Unknown"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="spanConstraint"/> is <see cref="ReportingPeriodSpanConstraint.Unknown"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="unit"/> and <paramref name="boundsConstraint"/> result in a constraint that is logically impossible to meet.</exception>
        /// <exception cref="ArgumentException"><paramref name="unit"/> and <paramref name="spanConstraint"/> result in a constraint that is logically impossible to meet.</exception>
        /// <exception cref="ArgumentException"><paramref name="boundsConstraint"/> and <paramref name="spanConstraint"/> result in a constraint that is logically impossible to meet.</exception>
        public ReportingPeriodCriteria(
            Unit unit = null,
            ReportingPeriodBoundsConstraint boundsConstraint = ReportingPeriodBoundsConstraint.None,
            ReportingPeriodSpanConstraint spanConstraint = ReportingPeriodSpanConstraint.None)
        {
            if (boundsConstraint == ReportingPeriodBoundsConstraint.Unknown)
            {
                throw new ArgumentOutOfRangeException(nameof(boundsConstraint), Invariant($"{nameof(boundsConstraint)} is {ReportingPeriodBoundsConstraint.Unknown}"));
            }

            if (spanConstraint == ReportingPeriodSpanConstraint.Unknown)
            {
                throw new ArgumentOutOfRangeException(nameof(spanConstraint), Invariant($"{nameof(spanConstraint)} is {ReportingPeriodSpanConstraint.Unknown}"));
            }

            var malformedConstraints = false;

            if (unit != null)
            {
                switch (unit.Granularity)
                {
                    case UnitOfTimeGranularity.Unbounded:
                        malformedConstraints =
                            (boundsConstraint == ReportingPeriodBoundsConstraint.MustBeFullyBounded) ||
                            (boundsConstraint == ReportingPeriodBoundsConstraint.MustHaveBoundedComponent) ||
                            (boundsConstraint == ReportingPeriodBoundsConstraint.MustHaveBoundedStart) ||
                            (boundsConstraint == ReportingPeriodBoundsConstraint.MustHaveBoundedEnd) ||
                            (boundsConstraint == ReportingPeriodBoundsConstraint.MustHaveBoundedStartAndUnboundedEnd) ||
                            (boundsConstraint == ReportingPeriodBoundsConstraint.MustHaveUnboundedStartAndBoundedEnd);
                        break;
                    default:
                        malformedConstraints =
                            boundsConstraint == ReportingPeriodBoundsConstraint.MustBeFullyUnbounded;
                        break;
                }

                if (malformedConstraints)
                {
                    throw new ArgumentException(Invariant($"{nameof(unit)} is {unit} and {nameof(boundsConstraint)} is {boundsConstraint}, resulting in a constraint that is logically impossible to meet."));
                }

                switch (unit.Granularity)
                {
                    case UnitOfTimeGranularity.Unbounded:
                        malformedConstraints =
                            spanConstraint == ReportingPeriodSpanConstraint.MustHaveDifferentStartAndEnd;
                        break;
                }

                if (malformedConstraints)
                {
                    throw new ArgumentException(Invariant($"{nameof(unit)} is {unit} and {nameof(spanConstraint)} is {spanConstraint}, resulting in a constraint that is logically impossible to meet."));
                }
            }

            switch (spanConstraint)
            {
                case ReportingPeriodSpanConstraint.MustHaveDifferentStartAndEnd:
                    malformedConstraints =
                        boundsConstraint == ReportingPeriodBoundsConstraint.MustBeFullyUnbounded;
                    break;
                case ReportingPeriodSpanConstraint.MustHaveSameStartAndEnd:
                    malformedConstraints =
                        (boundsConstraint == ReportingPeriodBoundsConstraint.MustHaveUnboundedStartAndBoundedEnd) ||
                        (boundsConstraint == ReportingPeriodBoundsConstraint.MustHaveBoundedStartAndUnboundedEnd);
                    break;
            }

            if (malformedConstraints)
            {
                throw new ArgumentException(Invariant($"{nameof(boundsConstraint)} is {boundsConstraint} and {nameof(spanConstraint)} is {spanConstraint}, resulting in a constraint that is logically impossible to meet."));
            }

            this.Unit = unit;
            this.BoundsConstraint = boundsConstraint;
            this.SpanConstraint = spanConstraint;
        }

        /// <summary>
        /// Gets the unit.
        /// </summary>
        public Unit Unit { get; private set; }

        /// <summary>
        /// Gets a constraint on the bounds of the reporting period.
        /// </summary>
        public ReportingPeriodBoundsConstraint BoundsConstraint { get; private set; }

        /// <summary>
        /// Gets a constraint on the span of a reporting period.
        /// </summary>
        public ReportingPeriodSpanConstraint SpanConstraint { get; private set; }
    }
}
