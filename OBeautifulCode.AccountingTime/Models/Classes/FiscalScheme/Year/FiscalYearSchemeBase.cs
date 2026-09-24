// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FiscalYearSchemeBase.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    /// <summary>
    /// Represents a scheme/convention for defining an entity's fiscal year.
    /// </summary>
    public abstract partial class FiscalYearSchemeBase : FiscalSchemeBase
    {
        /// <summary>
        /// Gets the calendar day period for the specified fiscal year.
        /// </summary>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <returns>
        /// The calendar day period for the specified fiscal year.
        /// </returns>
        public abstract ReportingPeriod GetCalendarDayPeriod(
            FiscalYear fiscalYear);
    }
}
