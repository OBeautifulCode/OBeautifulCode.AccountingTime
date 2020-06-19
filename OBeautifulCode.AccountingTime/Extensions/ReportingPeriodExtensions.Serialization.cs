// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriodExtensions.Serialization.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;
    using System.Linq;

    using OBeautifulCode.Assertion.Recipes;

    using static System.FormattableString;

    /// <summary>
    /// Serialization-related extension methods on <see cref="ReportingPeriod"/>.
    /// </summary>
    public static partial class ReportingPeriodExtensions
    {
        /// <summary>
        /// Deserializes a <see cref="ReportingPeriod"/> from a string.
        /// </summary>
        /// <param name="reportingPeriod">The serialized reporting period string to deserialize.</param>
        /// <returns>
        /// Gets a reporting period deserialized from it's string representation.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="reportingPeriod"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="reportingPeriod"/> is whitespace.</exception>
        /// <exception cref="InvalidOperationException">Cannot deserialize string; it is not valid reporting period.</exception>
        public static ReportingPeriod DeserializeFromString(
            this string reportingPeriod)
        {
            new { reportingPeriod }.AsArg().Must().NotBeNullNorWhiteSpace();

            var errorMessage = Invariant($"Cannot deserialize reporting period; the specified string is malformed: {reportingPeriod}.");

            var tokens = reportingPeriod.Split(',');
            if (tokens.Length != 2)
            {
                throw new InvalidOperationException(errorMessage);
            }

            if (tokens.Any(string.IsNullOrWhiteSpace))
            {
                throw new InvalidOperationException(errorMessage);
            }

            UnitOfTime start, end;

            try
            {
                start = tokens[0].DeserializeFromSortableString<UnitOfTime>();
                end = tokens[1].DeserializeFromSortableString<UnitOfTime>();
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException(errorMessage);
            }

            ReportingPeriod result;

            try
            {
                result = new ReportingPeriod(start, end);
            }
            catch (ArgumentException ex)
            {
                throw new InvalidOperationException(errorMessage, ex);
            }

            return result;
        }

        /// <summary>
        /// Serializes a <see cref="ReportingPeriod"/> to a string.
        /// </summary>
        /// <param name="reportingPeriod">The reporting period to serialize.</param>
        /// <returns>
        /// Gets a string representation of a reporting period that can be deserialized
        /// into the same reporting period.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="reportingPeriod"/> is null.</exception>
        public static string SerializeToString(
            this ReportingPeriod reportingPeriod)
        {
            new { reportingPeriod }.AsArg().Must().NotBeNull();

            var result = Invariant($"{reportingPeriod.Start.SerializeToSortableString()},{reportingPeriod.End.SerializeToSortableString()}");

            return result;
        }
    }
}