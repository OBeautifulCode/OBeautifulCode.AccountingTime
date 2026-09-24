// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalendarAlignedFiscalQuarterScheme.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    /// <summary>
    /// A fiscal quarter scheme in which each fiscal quarter mirrors the calendar quarter
    /// (e.g. Fiscal Q1 2026 => Calendar Q1 2026).
    /// </summary>
    public partial class CalendarAlignedFiscalQuarterScheme : FiscalQuarterSchemeBase
    {
        /// <inheritdoc />
        public override ReportingPeriod GetCalendarDayPeriod(
            FiscalQuarter fiscalQuarter)
        {
            if (fiscalQuarter == null)
            {
                throw new ArgumentNullException(nameof(fiscalQuarter));
            }

            var calendarQuarter = new CalendarQuarter(fiscalQuarter.Year, fiscalQuarter.QuarterNumber);

            var result = new ReportingPeriod(
                calendarQuarter.GetFirstCalendarDay(),
                calendarQuarter.GetLastCalendarDay());

            return result;
        }
    }
}
