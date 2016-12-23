// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriodComponent.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime
{
    /// <summary>
    /// Represents the start, end, or both, of a reporting period.
    /// </summary>
    public enum ReportingPeriodComponent
    {
        /// <summary>
        /// Invalid reporting period component.
        /// </summary>
        /// <remarks>
        /// This is required so that there is a default value for the enum.
        /// </remarks>
        Invalid,

        /// <summary>
        /// The start of a reporting period.
        /// </summary>
        Start,

        /// <summary>
        /// The end of a reporting period.
        /// </summary>
        End,

        /// <summary>
        /// Both the start and the end of a reporting period.
        /// </summary>
        Both
    }
}

// ReSharper restore CheckNamespace