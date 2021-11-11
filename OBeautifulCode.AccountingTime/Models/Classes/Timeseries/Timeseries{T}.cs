// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Timeseries{T}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using OBeautifulCode.CodeAnalysis.Recipes;
    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// A series of <see cref="Datapoint{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of value of the datapoints.</typeparam>
    public partial class Timeseries<T> : ITimeseries, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Timeseries{T}"/> class.
        /// </summary>
        /// <param name="datapoints">The datapoints.</param>
        public Timeseries(
            IReadOnlyList<Datapoint<T>> datapoints)
        {
            if (datapoints == null)
            {
                throw new ArgumentNullException(nameof(datapoints));
            }

            if (datapoints.Any(_ => _ == null))
            {
                throw new ArgumentException(Invariant($"{nameof(datapoints)} contains at least one null element."));
            }

            if (datapoints.Count > 1)
            {
                var mostGranularReportingPeriods = datapoints.Select(_ => _.ReportingPeriod.ToMostGranular()).ToList();

                if (mostGranularReportingPeriods.Select(_ => _.GetUnitOfTimeKind()).Distinct().Count() > 1)
                {
                    throw new ArgumentException(Invariant($"{nameof(datapoints)} contains reporting periods of different {nameof(UnitOfTimeKind)}s."));
                }

                for (var x = 1; x < datapoints.Count; x++)
                {
                    var endOfLeftSegment = mostGranularReportingPeriods[x - 1].End;

                    var startOfRightSegment = mostGranularReportingPeriods[x].Start;

                    if ((endOfLeftSegment.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded) ||
                        (startOfRightSegment.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded))
                    {
                        throw new ArgumentException(Invariant($"{nameof(datapoints)} are not in order and/or contain overlaps."));
                    }

                    if (endOfLeftSegment >= startOfRightSegment)
                    {
                        throw new ArgumentException(Invariant($"{nameof(datapoints)} are not in order and/or contain overlaps."));
                    }
                }
            }

            this.Datapoints = datapoints;
        }

        /// <summary>
        /// Gets an empty timeseries.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = ObcSuppressBecause.CA1000_DoNotDeclareStaticMembersOnGenericTypes_StaticPropertyReturnsInstanceOfContainingGenericClassAndIsConvenientAndMostDiscoverableWhereDeclared)]
        public static Timeseries<T> Empty => new Timeseries<T>(Array.Empty<Datapoint<T>>());

        /// <summary>
        /// Gets the datapoints.
        /// </summary>
        public IReadOnlyList<Datapoint<T>> Datapoints { get; private set; }
    }
}
