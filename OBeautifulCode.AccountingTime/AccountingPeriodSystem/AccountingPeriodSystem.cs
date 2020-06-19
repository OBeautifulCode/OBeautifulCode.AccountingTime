﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountingPeriodSystem.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// Represents a system for defining an entity's annual accounting period.
    /// </summary>
    public abstract partial class AccountingPeriodSystem : IModelViaCodeGen
    {
        /// <summary>
        /// Gets the reporting period, in calendar days, for the specified fiscal year.
        /// </summary>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <returns>
        /// Returns the reporting period, in calendar days, for the specified fiscal year.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="fiscalYear"/> is null.</exception>
        public abstract ReportingPeriod GetReportingPeriodForFiscalYear(
            FiscalYear fiscalYear);
    }
}
