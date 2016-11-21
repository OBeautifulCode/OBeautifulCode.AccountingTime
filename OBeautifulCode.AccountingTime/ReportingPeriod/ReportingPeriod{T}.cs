// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriod{T}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime
{
    using System;

    /// <summary>
    /// Represents a range of time over which to report.
    /// </summary>
    /// <typeparam name="T">The unit-of-time used to define the start and end of the reporting period.</typeparam>
    public abstract class ReportingPeriod<T>
        where T : UnitOfTime
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReportingPeriod{T}"/> class.
        /// </summary>
        /// <param name="start">The start of the reporting period.</param>
        /// <param name="end">The end of the reporting period.</param>
        protected ReportingPeriod(T start, T end)
        {
            if (start.GetType() != end.GetType())
            {
                throw new ArgumentException("description here");
            }

            // validate here
            // if ((dynamic)start < (dynamic)end)
            // {
            //     throw new ArgumentException
            // }

            this.Start = start;
            this.End = end;
        }

        /// <summary>
        ///  Gets the start of the reporting period.
        /// </summary>
        public T Start { get; private set; }

        /// <summary>
        ///  Gets the end of the reporting period.
        /// </summary>
        public T End { get; private set; }
    }
}

// ReSharper restore CheckNamespace