// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitKindConverter.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;
    using System.Collections.Generic;
    using static System.FormattableString;

    /// <summary>
    /// Converts a <see cref="ReportingPeriod"/> to a different <see cref="UnitOfTimeKind"/>.
    /// </summary>
    public class UnitKindConverter
    {
        private readonly Dictionary<ReportingPeriod, Dictionary<Unit, ReportingPeriod>> periodToUnitToAssociatedPeriodMap
                = new Dictionary<ReportingPeriod, Dictionary<Unit, ReportingPeriod>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitKindConverter"/> class.
        /// </summary>
        /// <param name="unitKindAssociations">The associations to use for conversion.</param>
        public UnitKindConverter(
            IReadOnlyCollection<UnitKindAssociation> unitKindAssociations)
        {
            if (unitKindAssociations == null)
            {
                throw new ArgumentNullException(nameof(unitKindAssociations));
            }

            foreach (var association in unitKindAssociations)
            {
                if (association == null)
                {
                    throw new ArgumentException(Invariant($"{nameof(unitKindAssociations)} contains a null element."));
                }

                var reportingPeriods1 = association.ReportingPeriod1.ToAllGranularities(includeSpecifiedReportingPeriodInResult: true);
                var reportingPeriods2 = association.ReportingPeriod2.ToAllGranularities(includeSpecifiedReportingPeriodInResult: true);

                foreach (var reportingPeriod1 in reportingPeriods1)
                {
                    foreach (var reportingPeriod2 in reportingPeriods2)
                    {
                        this.AddAssociation(reportingPeriod1, reportingPeriod2);
                        this.AddAssociation(reportingPeriod2, reportingPeriod1);
                    }
                }
            }
        }

        /// <summary>
        /// Attempts to convert a specified reporting period into a specified unit.
        /// </summary>
        /// <param name="reportingPeriod">The reporting period to convert.</param>
        /// <param name="unit">The unit to convert into.</param>
        /// <param name="value">When this method returns, contains the converted reporting period or else null if the conversion could not be performed.</param>
        /// <returns>
        /// true if the conversion was performed, otherwise false.
        /// </returns>
        public bool TryConvert(
            ReportingPeriod reportingPeriod,
            Unit unit,
            out ReportingPeriod value)
        {
            if (reportingPeriod == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod));
            }

            if (reportingPeriod.HasComponentWithUnboundedGranularity())
            {
                throw new ArgumentException(Invariant($"{nameof(reportingPeriod)} has a component with {nameof(UnitOfTimeGranularity.Unbounded)} {nameof(UnitOfTimeGranularity)}."));
            }

            if (unit == null)
            {
                throw new ArgumentNullException(nameof(unit));
            }

            if (unit.Granularity == UnitOfTimeGranularity.Unbounded)
            {
                throw new ArgumentException(Invariant($"{nameof(unit)} is {nameof(UnitOfTimeGranularity.Unbounded)}."));
            }

            if (unit.Kind == reportingPeriod.GetUnitOfTimeKind())
            {
                throw new ArgumentException(Invariant($"{nameof(unit)} has the same {nameof(UnitOfTimeKind)} as the specified {nameof(reportingPeriod)}."));
            }

            value = null;

            var result =
                this.periodToUnitToAssociatedPeriodMap.TryGetValue(reportingPeriod, out var unitToAssociatedPeriodMap)
                && unitToAssociatedPeriodMap.TryGetValue(unit, out value);

            return result;
        }

        private void AddAssociation(
            ReportingPeriod reportingPeriod1,
            ReportingPeriod reportingPeriod2)
        {
            if (!this.periodToUnitToAssociatedPeriodMap.TryGetValue(reportingPeriod1, out var unitToAssociatedPeriodMap))
            {
                unitToAssociatedPeriodMap = new Dictionary<Unit, ReportingPeriod>();

                this.periodToUnitToAssociatedPeriodMap.Add(reportingPeriod1, unitToAssociatedPeriodMap);
            }

            var reportingPeriod2Unit = reportingPeriod2.GetUnit();

            if (unitToAssociatedPeriodMap.TryGetValue(reportingPeriod2Unit, out var alreadyAssociatedReportingPeriod))
            {
                if (!reportingPeriod2.Equals(alreadyAssociatedReportingPeriod))
                {
                    throw new ArgumentException(Invariant($"Attempting to associate {nameof(reportingPeriod1)} ({reportingPeriod1}) with {nameof(reportingPeriod2)} ({reportingPeriod2}), however, {nameof(reportingPeriod1)} is already associated with a different reporting period ({alreadyAssociatedReportingPeriod}) for the same {nameof(Unit)} as {nameof(reportingPeriod2)} ({reportingPeriod2Unit})."));
                }
            }
            else
            {
                unitToAssociatedPeriodMap.Add(reportingPeriod2Unit, reportingPeriod2);
            }
        }
    }
}
