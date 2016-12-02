// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriodExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using static System.FormattableString;

    /// <summary>
    /// Extension methods on <see cref="ReportingPeriod{T}"/>.
    /// </summary>
    public static class ReportingPeriodExtensions
    {
        /// <summary>
        /// Determines if a unit-of-time is contained within a reporting period.
        /// For example, 2Q2017 is contained within a reporting period of 1Q2017-4Q2017.
        /// </summary>
        /// <remarks>
        /// If the unit-of-time is equal to one of the endpoints of the reporting period,
        /// that unit-of-time is considered to be within the reporting period.
        /// </remarks>
        /// <param name="unitOfTime">The unit-of-time to check against a reporting period.</param>
        /// <param name="reportingPeriod">The reporting period.</param>
        /// <returns>
        /// true if the unit-of-time is contained within the reporting period; false otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="unitOfTime"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="reportingPeriod"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="unitOfTime"/> cannot be compared against <paramref name="reportingPeriod"/> because they represent different concrete subclasses of <see cref="UnitOfTime"/>.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "The logic is different based on the type of reporting period (inclusive, exclusive, etc.)")]
        public static bool IsInReportingPeriod(this UnitOfTime unitOfTime, IReportingPeriodInclusive<UnitOfTime> reportingPeriod)
        {
            if (unitOfTime == null)
            {
                throw new ArgumentNullException(nameof(unitOfTime));
            }

            if (reportingPeriod == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod));
            }

            // note: CompareTo will throw if the unit-of-time is a different concrete
            // sub-class of UnitOfTime than what is stored in the reporting period
            bool result;
            try
            {
                result = (unitOfTime.CompareTo(reportingPeriod.Start) >= 0) &&
                         (unitOfTime.CompareTo(reportingPeriod.End) <= 0);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("unitOfTime cannot be compared against reportingPeriod because they represent different concrete subclasses of UnitOfTime", ex);
            }

            return result;
        }

        /// <summary>
        /// Determines if two objects of type <see cref="IReportingPeriodInclusive{T}"/>, overlap.
        /// For example, the following reporting periods have an overlap: 1Q2017-3Q2017 and 3Q2017-4Q2017.
        /// </summary>
        /// <remarks>
        /// If the endpoint of one reporting period is the same as the endpoint
        /// of the second reporting period, the reporting periods are deemed to overlap.
        /// </remarks>
        /// <param name="reportingPeriod1">A reporting period.</param>
        /// <param name="reportingPeriod2">A second reporting period to check for overlap against the first reporting period.</param>
        /// <returns>
        /// true if there is an overlap between the reporting periods; false otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="reportingPeriod1"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="reportingPeriod2"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="reportingPeriod1"/> cannot be compared against <paramref name="reportingPeriod2"/> because they represent different concrete subclasses of <see cref="UnitOfTime"/>.</exception>
        public static bool HasOverlapWith(this IReportingPeriodInclusive<UnitOfTime> reportingPeriod1, IReportingPeriodInclusive<UnitOfTime> reportingPeriod2)
        {
            if (reportingPeriod1 == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod1));
            }

            if (reportingPeriod2 == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod2));
            }

            bool result;
            try
            {
                result = reportingPeriod2.Start.IsInReportingPeriod(reportingPeriod1) ||
                         reportingPeriod2.End.IsInReportingPeriod(reportingPeriod1) ||
                         reportingPeriod1.Start.IsInReportingPeriod(reportingPeriod2) ||
                         reportingPeriod1.End.IsInReportingPeriod(reportingPeriod2);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("reportingPeriod1 cannot be compared against reportingPeriod2 because they represent different concrete subclasses of UnitOfTime.", ex);
            }

            return result;
        }

        /// <summary>
        /// Gets the number of distinct units-of-time contained within a specified reporting period.
        /// For example, a reporting period of 2Q2017-4Q2017, contains 3 distinct quarters.
        /// </summary>
        /// <remarks>
        /// The endpoints are considered one unit each, unless they are the same, in which case
        /// there is a total of 1 unit within the reporting period.
        /// </remarks>
        /// <param name="reportingPeriod">The reporting period.</param>
        /// <returns>
        /// The number of units-of-time contained within the specified reporting period.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="reportingPeriod"/> is null.</exception>
        public static int NumberOfUnitsWithin(this IReportingPeriodInclusive<UnitOfTime> reportingPeriod)
        {
            var result = GetUnitsWithin(reportingPeriod).Count;
            return result;
        }

        /// <summary>
        /// Gets the distinct <typeparamref name="T"/> contained within a specified reporting period.
        /// For example, a reporting period of 2Q2017-4Q2017, contains 2Q2017, 3Q2017, and 4Q2017.
        /// </summary>
        /// <remarks>
        /// The endpoints are considered units within the reporting period.
        /// </remarks>
        /// <typeparam name="T">The unit-of-time of the reporting period.</typeparam>
        /// <param name="reportingPeriod">The reporting period.</param>
        /// <returns>
        /// The units-of-time contained within the specified reporting period.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="reportingPeriod"/> is null.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "The logic is different based on the type of reporting period (inclusive, exclusive, etc.)")]
        public static IList<T> GetUnitsWithin<T>(this IReportingPeriodInclusive<T> reportingPeriod)
            where T : UnitOfTime
        {
            if (reportingPeriod == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod));
            }

            var allUnits = new List<T>();
            var currentUnit = reportingPeriod.Start;
            do
            {
                allUnits.Add(currentUnit);
                currentUnit = (T)currentUnit.Plus(1);
            }
            while (currentUnit.CompareTo(reportingPeriod.End) <= 0);

            return allUnits;
        }

        /// <summary>
        /// Creates all permutations of reporting periods between the
        /// start and end of a specified reporting period, from 1 unit
        /// to the specified number of maximum number of units that a
        /// reporting period can contain.
        /// </summary>
        /// <typeparam name="T">The unit-of-time of the reporting period.</typeparam>
        /// <param name="reportingPeriod">The reporting period to permute.</param>
        /// <param name="maxUnitsInAnyReportingPeriod">Maximum number of units-of-time in each reporting period.</param>
        /// <returns>All possible reporting periods containing between 1 and <paramref name="maxUnitsInAnyReportingPeriod"/> units-of-time, contained within <paramref name="reportingPeriod"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="reportingPeriod"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="maxUnitsInAnyReportingPeriod"/> is less than or equal to 0.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is a perfectly fine usage of nesting generic types.")]
        public static ICollection<IReportingPeriodInclusive<T>> CreatePermutations<T>(this IReportingPeriodInclusive<T> reportingPeriod, int maxUnitsInAnyReportingPeriod)
            where T : UnitOfTime
        {
            if (reportingPeriod == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod));
            }

            if (maxUnitsInAnyReportingPeriod < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(maxUnitsInAnyReportingPeriod), "max units in any reporting period is <= 0");
            }

            var allUnits = reportingPeriod.GetUnitsWithin();
            var allReportingPeriods = new List<IReportingPeriodInclusive<T>>();
            for (int unitOfTimeIndex = 0; unitOfTimeIndex < allUnits.Count; unitOfTimeIndex++)
            {
                for (int numberOfUnits = 1; numberOfUnits <= maxUnitsInAnyReportingPeriod; numberOfUnits++)
                {
                    if (unitOfTimeIndex + numberOfUnits - 1 < allUnits.Count)
                    {
                        var subReportingPeriod = new ReportingPeriodInclusive<T>(allUnits[unitOfTimeIndex], allUnits[unitOfTimeIndex + numberOfUnits - 1]);
                        allReportingPeriods.Add(subReportingPeriod);
                    }
                }
            }

            return allReportingPeriods;
        }

        /// <summary>
        /// Serializes a <see cref="IReportingPeriodInclusive{UnitOfTime}"/> to a string.
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

            var reportingPeriodInclusive = reportingPeriod as IReportingPeriodInclusive<UnitOfTime>;
            if (reportingPeriodInclusive != null)
            {
                var result = Invariant($"rpi({reportingPeriod.Start.SerializeToSortableString()},{reportingPeriod.End.SerializeToSortableString()})");
                return result;
            }

            throw new NotSupportedException("this type of reporting period is not supported: " + reportingPeriod.GetType());
        }

        /// <summary>
        /// Deserializes an <see cref="IReportingPeriod{T}"/> from a string.
        /// </summary>
        /// <typeparam name="TReportingPeriod">The type of reporting period.</typeparam>
        /// <typeparam name="TReportingPeriodUnitOfTime">The unit-of-time used to define the start and end of the reporting period.</typeparam>
        /// <param name="reportingPeriod">The serialized reperiod period string to deserialize.</param>
        /// <returns>
        /// Gets a reporting period deserialized from it's string representation.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="reportingPeriod"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="reportingPeriod"/> is whitespace.</exception>
        /// <exception cref="InvalidOperationException">Cannot deserialize string; it is not valid reporting period.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Not possible to implement this, since we are trying to deserialize a string.")]
        public static TReportingPeriod DeserializeFromString<TReportingPeriod, TReportingPeriodUnitOfTime>(this string reportingPeriod)
            where TReportingPeriod : class, IReportingPeriod<TReportingPeriodUnitOfTime>
            where TReportingPeriodUnitOfTime : UnitOfTime
        {
            if (reportingPeriod == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod));
            }

            if (string.IsNullOrWhiteSpace(reportingPeriod))
            {
                throw new ArgumentException("reporting period string is whitespace", nameof(reportingPeriod));
            }

            string errorMessage = "Cannot deserialize string; it is not valid reporting period.";
            if (!reportingPeriod.EndsWith(")", StringComparison.Ordinal))
            {
                throw new InvalidOperationException(errorMessage);
            }

            reportingPeriod = reportingPeriod.Remove(reportingPeriod.Length - 1, 1);

            Type serializedType;
            if (reportingPeriod.StartsWith("rpi(",  StringComparison.Ordinal))
            {
                serializedType = typeof(ReportingPeriodInclusive<TReportingPeriodUnitOfTime>);
                reportingPeriod = reportingPeriod.Remove(0, 4);
            }
            else
            {
                throw new InvalidOperationException(errorMessage);
            }

            var returnType = typeof(TReportingPeriod);

            if (!returnType.IsAssignableFrom(serializedType))
            {
                throw new InvalidOperationException(Invariant($"The unit-of-time appears to be a {serializedType.Name} which cannot be casted to a {returnType.Name}."));
            }

            errorMessage = Invariant($"Cannot deserialize string;  it appears to be a {serializedType.Name} but it is malformed.");
            var tokens = reportingPeriod.Split(',');
            if (tokens.Length != 2)
            {
                throw new InvalidOperationException(errorMessage);
            }

            if (tokens.Any(string.IsNullOrWhiteSpace))
            {
                throw new InvalidOperationException(errorMessage);
            }

            TReportingPeriodUnitOfTime start;
            TReportingPeriodUnitOfTime end;
            try
            {
                start = tokens[0].DeserializeFromSortableString<TReportingPeriodUnitOfTime>();
                end = tokens[1].DeserializeFromSortableString<TReportingPeriodUnitOfTime>();
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException(errorMessage);
            }

            if (serializedType == typeof(ReportingPeriodInclusive<TReportingPeriodUnitOfTime>))
            {
                try
                {
                    var result = new ReportingPeriodInclusive<TReportingPeriodUnitOfTime>(start, end);
                    return result as TReportingPeriod;
                }
                catch (ArgumentException)
                {
                    throw new InvalidOperationException(errorMessage);
                }
            }

            throw new NotSupportedException("this type of reporting period is not supported: " + serializedType);
        }
    }
}

// ReSharper restore CheckNamespace