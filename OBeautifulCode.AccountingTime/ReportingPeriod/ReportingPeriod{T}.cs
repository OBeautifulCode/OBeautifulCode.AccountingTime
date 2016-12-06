// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriod{T}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime
{
    using System;
    using System.ComponentModel;

    /// <inheritdoc />
    [Serializable]
    [Bindable(true, BindingDirection.TwoWay)]
    public abstract class ReportingPeriod<T> : IReportingPeriod<T>
        where T : UnitOfTime
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReportingPeriod{T}"/> class.
        /// </summary>
        /// <param name="start">The start of the reporting period.</param>
        /// <param name="end">The end of the reporting period.</param>
        /// <exception cref="ArgumentNullException"><paramref name="start"/> or <paramref name="end"/> are null.</exception>
        /// <exception cref="ArgumentException"><paramref name="start"/> and <paramref name="end"/> are different kinds of units-of-time.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="start"/> is greater than <paramref name="end"/>.</exception>
        protected ReportingPeriod(T start, T end)
        {
            if (start == null)
            {
                throw new ArgumentNullException(nameof(start));
            }

            if (end == null)
            {
                throw new ArgumentNullException(nameof(end));
            }

            if (start.GetType() != end.GetType())
            {
                throw new ArgumentException("start and end are different kinds of units-of-time");
            }

            if (start.CompareTo(end) == 1)
            {
                throw new ArgumentOutOfRangeException(nameof(start), "start is greater than end");
            }

            this.Start = start;
            this.End = end;
        }

        // ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

        /// <inheritdoc />
        public T Start { get; private set; }

        /// <inheritdoc />
        public T End { get; private set; }

        // ReSharper restore AutoPropertyCanBeMadeGetOnly.Local

        /// <inheritdoc />
        public TReportingPeriod Clone<TReportingPeriod>()
            where TReportingPeriod : class, IReportingPeriod<UnitOfTime>
        {
            var requestedType = typeof(TReportingPeriod);
            Type requestedUnitOfTimeType = requestedType.GetGenericArguments()[0];

            var thisUnboundGenericType = this.GetType().GetGenericTypeDefinition();
            var typeArgs = new[] { requestedUnitOfTimeType };
            var genericTypeToCreate = thisUnboundGenericType.MakeGenericType(typeArgs);

            string errorMessage = "A clone of this reporting-period is not assignable to the type of reporting period requested.";
            if (!requestedType.IsAssignableFrom(genericTypeToCreate))
            {
                throw new InvalidOperationException(errorMessage);
            }

            // ReSharper disable UseMethodIsInstanceOfType
            if (!requestedUnitOfTimeType.IsAssignableFrom(this.Start.GetType()))
            {
                throw new InvalidOperationException(errorMessage);
            }

            if (!requestedUnitOfTimeType.IsAssignableFrom(this.End.GetType()))
            {
                throw new InvalidOperationException(errorMessage);
            }

            var result = Activator.CreateInstance(genericTypeToCreate, this.Start.Clone(), this.End.Clone());
            return result as TReportingPeriod;
        }

        /// <inheritdoc />
        public abstract IReportingPeriod<T> Clone();
    }
}

// ReSharper restore CheckNamespace