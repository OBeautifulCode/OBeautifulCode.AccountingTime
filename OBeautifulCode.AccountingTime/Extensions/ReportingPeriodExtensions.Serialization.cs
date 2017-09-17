// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriodExtensions.Serialization.cs" company="OBeautifulCode">
//    Copyright (c) OBeautifulCode 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;
    using System.Linq;
    using System.Reflection;

    using static System.FormattableString;

    /// <summary>
    /// Serialization-related extension methods on <see cref="IReportingPeriod{T}"/>.
    /// </summary>
    public static partial class ReportingPeriodExtensions
    {
        /// <summary>
        /// Deserializes an <see cref="IReportingPeriod{T}"/> from a string.
        /// </summary>
        /// <typeparam name="TReportingPeriod">The type of reporting period to deserialize into.</typeparam>
        /// <param name="reportingPeriod">The serialized reporting period string to deserialize.</param>
        /// <returns>
        /// Gets a reporting period deserialized from it's string representation.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="reportingPeriod"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="reportingPeriod"/> is whitespace.</exception>
        /// <exception cref="InvalidOperationException">Cannot deserialize string; it is not valid reporting period.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Not possible to implement this, since we are trying to deserialize a string.")]
        public static TReportingPeriod DeserializeFromString<TReportingPeriod>(this string reportingPeriod)
            where TReportingPeriod : class, IReportingPeriod<UnitOfTime>
        {
            if (reportingPeriod == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod));
            }

            if (string.IsNullOrWhiteSpace(reportingPeriod))
            {
                throw new ArgumentException("reporting period string is whitespace", nameof(reportingPeriod));
            }

            var requestedType = typeof(TReportingPeriod);
            var result = reportingPeriod.DeserializeFromString(requestedType) as TReportingPeriod;
            return result;
        }

        /// <summary>
        /// Deserializes an <see cref="IReportingPeriod{T}"/> from a string.
        /// </summary>
        /// <param name="reportingPeriod">The serialized reporting period string to deserialize.</param>
        /// <param name="requestedType">The type of reporting period to deserialize into.</param>
        /// <returns>
        /// Gets a reporting period deserialized from it's string representation.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="reportingPeriod"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="reportingPeriod"/> is whitespace.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="requestedType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="requestedType"/> is not an <see cref="IReportingPeriod{UnitOfTime}"/>.</exception>
        /// <exception cref="InvalidOperationException">Cannot deserialize string; it is not valid reporting period.</exception>
        public static object DeserializeFromString(this string reportingPeriod, Type requestedType)
        {
            if (reportingPeriod == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod));
            }

            if (string.IsNullOrWhiteSpace(reportingPeriod))
            {
                throw new ArgumentException("reporting period string is whitespace", nameof(reportingPeriod));
            }

            if (requestedType == null)
            {
                throw new ArgumentNullException(nameof(requestedType));
            }

            if (!typeof(IReportingPeriod<UnitOfTime>).IsAssignableFrom(requestedType))
            {
                throw new ArgumentException(Invariant($"{nameof(requestedType)} is not an {nameof(IReportingPeriod<UnitOfTime>)}"), nameof(requestedType));
            }

            Type unboundGenericType = typeof(ReportingPeriod<>);
            string errorMessage = Invariant($"Cannot deserialize string;  it appears to be a {unboundGenericType.Name} but it is not assignable to type of reporting period requested.");
            Type requestedUnitOfTimeType = requestedType.GetGenericArguments()[0];
            var typeArgs = new[] { requestedUnitOfTimeType };
            var genericTypeToCreate = unboundGenericType.MakeGenericType(typeArgs);
            if (!requestedType.IsAssignableFrom(genericTypeToCreate))
            {
                throw new InvalidOperationException(errorMessage);
            }

            errorMessage = Invariant($"Cannot deserialize string;  it appears to be a {unboundGenericType.Name} but it is malformed.");
            var tokens = reportingPeriod.Split(',');
            if (tokens.Length != 2)
            {
                throw new InvalidOperationException(errorMessage);
            }

            if (tokens.Any(string.IsNullOrWhiteSpace))
            {
                throw new InvalidOperationException(errorMessage);
            }

            UnitOfTime start;
            UnitOfTime end;
            try
            {
                start = tokens[0].DeserializeFromSortableString<UnitOfTime>();
                end = tokens[1].DeserializeFromSortableString<UnitOfTime>();
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException(errorMessage);
            }

            errorMessage = Invariant($"Cannot deserialize string;  it appears to be a {unboundGenericType.Name} but the type of unit-of-time of the start and/or the end of the reporting period is not assignable to unit-of-time of the requested reporting period.");
            if (!requestedUnitOfTimeType.IsAssignableFrom(start.GetType()))
            {
                throw new InvalidOperationException(errorMessage);
            }

            if (!requestedUnitOfTimeType.IsAssignableFrom(end.GetType()))
            {
                throw new InvalidOperationException(errorMessage);
            }

            errorMessage = Invariant($"Cannot deserialize string;  it appears to be a {unboundGenericType.Name} but it is malformed.  The following error occured when attempting to create it: ");

            try
            {
                var result = Activator.CreateInstance(genericTypeToCreate, start, end);
                return result;
            }
            catch (TargetInvocationException ex)
            {
                throw new InvalidOperationException(errorMessage + ex.InnerException?.Message);
            }
        }

        /// <summary>
        /// Serializes a <see cref="IReportingPeriod{UnitOfTime}"/> to a string.
        /// </summary>
        /// <param name="reportingPeriod">The reporting period to serialize.</param>
        /// <returns>
        /// Gets a string representation of a reporting period that can be deserialized
        /// into the same reporting period.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="reportingPeriod"/> is null.</exception>
        public static string SerializeToString(this IReportingPeriod<UnitOfTime> reportingPeriod)
        {
            if (reportingPeriod == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod));
            }

            var result = Invariant($"{reportingPeriod.Start.SerializeToSortableString()},{reportingPeriod.End.SerializeToSortableString()}");
            return result;
        }
    }
}