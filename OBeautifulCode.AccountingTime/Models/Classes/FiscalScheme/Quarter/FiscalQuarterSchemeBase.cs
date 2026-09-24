// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FiscalQuarterSchemeBase.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    /// <summary>
    /// Represents a scheme/convention for defining an entity's fiscal quarter.
    /// </summary>
    public abstract partial class FiscalQuarterSchemeBase : FiscalSchemeBase
    {
        /// <summary>
        /// Gets the calendar day period for the specified fiscal quarter.
        /// </summary>
        /// <param name="fiscalQuarter">The fiscal quarter.</param>
        /// <returns>
        /// The calendar day period for the specified fiscal quarter.
        /// </returns>
        public abstract ReportingPeriod GetCalendarDayPeriod(
            FiscalQuarter fiscalQuarter);

        /// <inheritdoc />
        public override UnitOfTimeGranularity GetGranularity()
        {
            var result = UnitOfTimeGranularity.Quarter;

            return result;
        }
    }
}
