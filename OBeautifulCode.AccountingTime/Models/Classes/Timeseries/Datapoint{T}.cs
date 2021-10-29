// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Datapoint{T}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// Information observed/recorded for a reporting period.
    /// </summary>
    /// <typeparam name="T">The type of value of the datapoint.</typeparam>
    public partial class Datapoint<T> : IDatapoint, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Datapoint{T}"/> class.
        /// </summary>
        /// <param name="reportingPeriod">The reporting period of the datapoint.</param>
        /// <param name="value">The value of the datapoint.</param>
        public Datapoint(
            ReportingPeriod reportingPeriod,
            T value)
        {
            if (reportingPeriod == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod));
            }

            this.ReportingPeriod = reportingPeriod;
            this.Value = value;
        }

        /// <summary>
        /// Gets the reporting period of the datapoint.
        /// </summary>
        public ReportingPeriod ReportingPeriod { get; private set; }

        /// <summary>
        /// Gets the value of the datapoint.
        /// </summary>
        public T Value { get; private set; }
    }
}
