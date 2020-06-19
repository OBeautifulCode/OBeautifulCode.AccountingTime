// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalendarYearAccountingPeriodSystem.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Type;

    /// <summary>
    /// A calendar year is 12 consecutive months beginning on January 1st and ending on December 31st.
    /// </summary>
    public partial class CalendarYearAccountingPeriodSystem : AccountingPeriodSystem, IModelViaCodeGen
    {
       /// <inheritdoc />
        /// <exception cref="ArgumentNullException"><paramref name="fiscalYear"/> is null.</exception>
        public override ReportingPeriod GetReportingPeriodForFiscalYear(
            FiscalYear fiscalYear)
        {
            new { fiscalYear }.AsArg().Must().NotBeNull();

            var januaryFirst = new CalendarDay(fiscalYear.Year, MonthOfYear.January, DayOfMonth.One);

            var decemberThirtyFirst = new CalendarDay(fiscalYear.Year, MonthOfYear.December, DayOfMonth.ThirtyOne);

            var result = new ReportingPeriod(januaryFirst, decemberThirtyFirst);

            return result;
        }
    }
}
