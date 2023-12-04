// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitKindAssociation.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;
    using OBeautifulCode.Type;
    using static System.FormattableString;

    /// <summary>
    /// Associates two <see cref="ReportingPeriod"/>s having different <see cref="UnitOfTimeKind"/>.
    /// </summary>
    public partial class UnitKindAssociation : IHaveStringId, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitKindAssociation"/> class.
        /// </summary>
        /// <param name="reportingPeriod1">The first reporting period.</param>
        /// <param name="reportingPeriod2">The second reporting period.</param>
        /// <param name="id">OPTIONAL identifier for the association.</param>
        public UnitKindAssociation(
            ReportingPeriod reportingPeriod1,
            ReportingPeriod reportingPeriod2,
            string id = null)
        {
            if (reportingPeriod1 == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod1));
            }

            if (reportingPeriod2 == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod2));
            }

            if (reportingPeriod1.HasComponentWithUnboundedGranularity())
            {
                throw new ArgumentException(Invariant($"{nameof(reportingPeriod1)} has a component whose {nameof(UnitOfTimeGranularity)} is {UnitOfTimeGranularity.Unbounded}."));
            }

            if (reportingPeriod2.HasComponentWithUnboundedGranularity())
            {
                throw new ArgumentException(Invariant($"{nameof(reportingPeriod2)} has a component whose {nameof(UnitOfTimeGranularity)} is {UnitOfTimeGranularity.Unbounded}."));
            }

            if (reportingPeriod1.GetUnitOfTimeKind() == reportingPeriod2.GetUnitOfTimeKind())
            {
                throw new ArgumentException(Invariant($"The specified reporting periods have the same {nameof(UnitOfTimeKind)}: {reportingPeriod1.GetUnitOfTimeKind()}."));
            }

            this.ReportingPeriod1 = reportingPeriod1;
            this.ReportingPeriod2 = reportingPeriod2;
            this.Id = id;
        }

        /// <summary>
        /// Gets the first reporting period.
        /// </summary>
        public ReportingPeriod ReportingPeriod1 { get; private set; }

        /// <summary>
        /// Gets the second reporting period.
        /// </summary>
        public ReportingPeriod ReportingPeriod2 { get; private set; }

        /// <summary>
        /// Gets the identifier for the association.
        /// </summary>
        public string Id { get; private set; }
    }
}
