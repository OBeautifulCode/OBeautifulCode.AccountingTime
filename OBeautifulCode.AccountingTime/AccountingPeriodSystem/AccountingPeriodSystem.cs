// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountingPeriodSystem.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Represents a system for defining an entity's annual accounting period.
    /// </summary>
    [Serializable]
    [Bindable(BindableSupport.Default)]
    public abstract class AccountingPeriodSystem
    {
        /// <summary>
        /// Gets the reporting period, in calendar days, for the specified fiscal year.
        /// </summary>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <returns>
        /// Returns the reporting period, in calendar days, for the specified fiscal year.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="fiscalYear"/> is null.</exception>
        public abstract ReportingPeriod<CalendarDay> GetReportingPeriodForFiscalYear(FiscalYear fiscalYear);
    }
}

// ReSharper restore CheckNamespace