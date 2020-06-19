﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FiscalYearAccountingPeriodSystem.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Type;

    /// <summary>
    /// A fiscal year is 12 consecutive months ending on the last day of any month except December 31st.
    /// </summary>
    public partial class FiscalYearAccountingPeriodSystem : AccountingPeriodSystem, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FiscalYearAccountingPeriodSystem"/> class.
        /// </summary>
        /// <param name="lastMonthInFiscalYear">The last month of the fiscal year.</param>
        /// <exception cref="ArgumentException"><paramref name="lastMonthInFiscalYear"/> is invalid.</exception>
        /// <exception cref="ArgumentException"><paramref name="lastMonthInFiscalYear"/> is <see cref="MonthOfYear.December"/>.</exception>
        public FiscalYearAccountingPeriodSystem(
            MonthOfYear lastMonthInFiscalYear)
        {
            new { lastMonthInFiscalYear }.AsArg().Must().NotBeEqualTo(MonthOfYear.Invalid).And().NotBeEqualTo(MonthOfYear.December);

            this.LastMonthInFiscalYear = lastMonthInFiscalYear;
        }

        /// <summary>
        /// Gets the last month of the fiscal year.
        /// </summary>
        public MonthOfYear LastMonthInFiscalYear { get; private set; }

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException"><paramref name="fiscalYear"/> is null.</exception>
        public override ReportingPeriod GetReportingPeriodForFiscalYear(
            FiscalYear fiscalYear)
        {
            new { fiscalYear }.AsArg().Must().NotBeNull();

            var lastDayInEndingMonth = DateTime.DaysInMonth(fiscalYear.Year, (int)this.LastMonthInFiscalYear);

            var lastDayInFiscalYear = new DateTime(fiscalYear.Year, (int)this.LastMonthInFiscalYear, lastDayInEndingMonth);

            var firstDayInFiscalYear = lastDayInFiscalYear.AddDays(1).AddYears(-1);

            var result = new ReportingPeriod(firstDayInFiscalYear.ToCalendarDay(), lastDayInFiscalYear.ToCalendarDay());

            return result;
        }
    }
}
