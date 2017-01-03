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
    using System.Reflection;

    using static System.FormattableString;

    /// <summary>
    /// Extension methods on <see cref="ReportingPeriod{T}"/>.
    /// </summary>
    public static class ReportingPeriodExtensions
    {
        /// <summary>
        /// Clones a reporting period while adjusting the start or end of the reporting period, or both.
        /// </summary>
        /// <typeparam name="TReportingPeriod">The type of reporting period to return.</typeparam>
        /// <param name="reportingPeriod">The reporting period to clone.</param>
        /// <param name="component">The component(s) of the reporting period to adjust.</param>
        /// <param name="unitsToAdd">The number of units to add when adjusting the reporting period component.  Use negative numbers to subtract units.</param>
        /// <param name="granularityOfUnitsToAdd">The granularity of the units to add to the specified reporting period component(s).  Must be as or less granular than the reporting period component (e.g. can add CalendarYear to a CalendarQuarter, but not vice-versa).</param>
        /// <returns>A clone of the specified reporting period with the specified adjustment made to the start or end of the reporting period, or both.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="reportingPeriod"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="component"/> is <see cref="ReportingPeriodComponent.Invalid"/>.</exception>
        /// <exception cref="ArgumentException">Cannot add or subtract from a unit-of-time whose granularity is <see cref="UnitOfTimeGranularity.Unbounded"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="granularityOfUnitsToAdd"/> is <see cref="UnitOfTimeGranularity.Invalid"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="granularityOfUnitsToAdd"/> is <see cref="UnitOfTimeGranularity.Unbounded"/>.  Cannot add units of that granularity.</exception>
        /// <exception cref="ArgumentException"><paramref name="granularityOfUnitsToAdd"/> is more granular than the reporting period component.  Only units that are as granular or less granular than a unit-of-time can be added to that unit-of-time.</exception>
        /// <exception cref="InvalidOperationException">The adjustment has caused the <see cref="ReportingPeriod{T}.Start"/> to be greater than <see cref="ReportingPeriod{T}.End"/></exception>
        /// <exception cref="InvalidOperationException">The adjusted reporting period cannot be converted into a <typeparamref name="TReportingPeriod"/></exception>
        public static TReportingPeriod CloneWithAdjustment<TReportingPeriod>(this IReportingPeriod<UnitOfTime> reportingPeriod, ReportingPeriodComponent component, int unitsToAdd, UnitOfTimeGranularity granularityOfUnitsToAdd)
            where TReportingPeriod : class, IReportingPeriod<UnitOfTime>
        {
            if (reportingPeriod == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod));
            }

            if (component == ReportingPeriodComponent.Invalid)
            {
                throw new ArgumentException(Invariant($"{nameof(component)} is {nameof(ReportingPeriodComponent.Invalid)}"));
            }

            var start = reportingPeriod.Start;
            var end = reportingPeriod.End;
            if ((component == ReportingPeriodComponent.Start) || (component == ReportingPeriodComponent.Both))
            {
                start = start.Plus(unitsToAdd, granularityOfUnitsToAdd);
            }

            if ((component == ReportingPeriodComponent.End) || (component == ReportingPeriodComponent.Both))
            {
                end = end.Plus(unitsToAdd, granularityOfUnitsToAdd);
            }

            try
            {
                var result = new ReportingPeriod<UnitOfTime>(start, end).Clone<TReportingPeriod>();
                return result;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new InvalidOperationException("The adjustment has caused the Start of the reporting period to be greater than the End of the reporting period.", ex);
            }
        }

        /// <summary>
        /// Determines if a unit-of-time is contained within a reporting period.
        /// For example, 2Q2017 is contained within a reporting period of 1Q2017-4Q2017.
        /// </summary>
        /// <remarks>
        /// If the unit-of-time is equal to one of the endpoints of the reporting period,
        /// that unit-of-time is considered to be within the reporting period.
        /// </remarks>
        /// <param name="reportingPeriod">The reporting period.</param>
        /// <param name="unitOfTime">The unit-of-time to check against a reporting period.</param>
        /// <returns>
        /// true if the unit-of-time is contained within the reporting period; false otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="unitOfTime"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="reportingPeriod"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="unitOfTime"/> cannot be compared against <paramref name="reportingPeriod"/> because they represent different <see cref="UnitOfTime.UnitOfTimeKind"/>.</exception>
        public static bool Contains(this IReportingPeriod<UnitOfTime> reportingPeriod, UnitOfTime unitOfTime)
        {
            if (reportingPeriod == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod));
            }

            if (unitOfTime == null)
            {
                throw new ArgumentNullException(nameof(unitOfTime));
            }

            if (unitOfTime.UnitOfTimeKind != reportingPeriod.GetUnitOfTimeKind())
            {
                throw new ArgumentException(Invariant($"{nameof(unitOfTime)} cannot be compared against {nameof(reportingPeriod)} because they represent different {nameof(UnitOfTimeKind)}"));
            }

            var result = reportingPeriod.Contains(new ReportingPeriod<UnitOfTime>(unitOfTime, unitOfTime));
            return result;
        }

        /// <summary>
        /// Determines if an <see cref="IReportingPeriod{T}"/> is contained within another <see cref="IReportingPeriod{T}"/>
        /// For example, 1Q2017-3Q2017 contains 2Q2017-3Q2017.
        /// </summary>
        /// <remarks>
        /// If an endpoint in the second reporting period equals an endpoint in the first reporting period, that endpoint
        /// is considered to be contained within the first reporting period.  Of course, both endpoints must be contained
        /// within the reporting period for this method to return true.
        /// </remarks>
        /// <param name="reportingPeriod1">A reporting period.</param>
        /// <param name="reportingPeriod2">A second reporting period to check for containment within the first reporting period.</param>
        /// <returns>
        /// true if the first reporting period contains the second one; false if not.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="reportingPeriod1"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="reportingPeriod2"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="reportingPeriod1"/> cannot be compared against <paramref name="reportingPeriod2"/> because they represent different <see cref="UnitOfTime.UnitOfTimeKind"/>.</exception>
        public static bool Contains(this IReportingPeriod<UnitOfTime> reportingPeriod1, IReportingPeriod<UnitOfTime> reportingPeriod2)
        {
            if (reportingPeriod1 == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod1));
            }

            if (reportingPeriod2 == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod2));
            }

            if (reportingPeriod1.GetUnitOfTimeKind() != reportingPeriod2.GetUnitOfTimeKind())
            {
                throw new ArgumentException(Invariant($"{nameof(reportingPeriod1)} cannot be compared against {nameof(reportingPeriod2)} because they represent different {nameof(UnitOfTimeKind)}"));
            }

            reportingPeriod1 = reportingPeriod1.MakeMostGranular();
            reportingPeriod2 = reportingPeriod2.MakeMostGranular();

            bool startIsContained;
            if (reportingPeriod1.Start.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded)
            {
                startIsContained = true;
            }
            else
            {
                if (reportingPeriod2.Start.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded)
                {
                    startIsContained = false;
                }
                else
                {
                    startIsContained = reportingPeriod1.Start.CompareTo(reportingPeriod2.Start) <= 0;
                }
            }

            bool endIsContained;
            if (reportingPeriod1.End.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded)
            {
                endIsContained = true;
            }
            else
            {
                if (reportingPeriod2.End.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded)
                {
                    endIsContained = false;
                }
                else
                {
                    endIsContained = reportingPeriod1.End.CompareTo(reportingPeriod2.End) >= 0;
                }
            }

            var result = startIsContained && endIsContained;
            return result;
        }

        /// <summary>
        /// Determines if two objects of type <see cref="IReportingPeriod{T}"/>, overlap.
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
        /// <exception cref="ArgumentException"><paramref name="reportingPeriod1"/> cannot be compared against <paramref name="reportingPeriod2"/> because they represent different <see cref="UnitOfTime.UnitOfTimeKind"/>.</exception>
        public static bool HasOverlapWith(this IReportingPeriod<UnitOfTime> reportingPeriod1, IReportingPeriod<UnitOfTime> reportingPeriod2)
        {
            if (reportingPeriod1 == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod1));
            }

            if (reportingPeriod2 == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod2));
            }

            if (reportingPeriod1.GetUnitOfTimeKind() != reportingPeriod2.GetUnitOfTimeKind())
            {
                throw new ArgumentException(Invariant($"{nameof(reportingPeriod1)} cannot be compared against {nameof(reportingPeriod2)} because they represent different {nameof(UnitOfTimeKind)}"));
            }

            bool result =
                reportingPeriod1.Contains(reportingPeriod2.Start) ||
                reportingPeriod1.Contains(reportingPeriod2.End) ||
                reportingPeriod2.Contains(reportingPeriod1.Start) ||
                reportingPeriod2.Contains(reportingPeriod1.End);
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
        /// <exception cref="ArgumentException"><paramref name="reportingPeriod"/> <see cref="IReportingPeriod{T}.Start"/> and/or <see cref="IReportingPeriod{T}.End"/> is unbounded.</exception>
        public static int NumberOfUnitsWithin(this IReportingPeriod<UnitOfTime> reportingPeriod)
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
        /// <exception cref="ArgumentException"><paramref name="reportingPeriod"/> <see cref="IReportingPeriod{T}.Start"/> and/or <see cref="IReportingPeriod{T}.End"/> is unbounded.</exception>
        public static IList<T> GetUnitsWithin<T>(this IReportingPeriod<T> reportingPeriod)
            where T : UnitOfTime
        {
            if (reportingPeriod == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod));
            }

            if (reportingPeriod.HasComponentWithUnboundedGranularity())
            {
                throw new ArgumentException(Invariant($"{nameof(reportingPeriod)} {nameof(reportingPeriod.Start)} and/or {nameof(reportingPeriod.End)} is unbounded"));
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
        /// <exception cref="ArgumentException"><paramref name="reportingPeriod"/> <see cref="IReportingPeriod{T}.Start"/> and/or <see cref="IReportingPeriod{T}.End"/> is unbounded.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="maxUnitsInAnyReportingPeriod"/> is less than or equal to 0.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is a perfectly fine usage of nesting generic types.")]
        public static ICollection<IReportingPeriod<T>> CreatePermutations<T>(this IReportingPeriod<T> reportingPeriod, int maxUnitsInAnyReportingPeriod)
            where T : UnitOfTime
        {
            if (reportingPeriod == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod));
            }

            if (reportingPeriod.HasComponentWithUnboundedGranularity())
            {
                throw new ArgumentException(Invariant($"{nameof(reportingPeriod)} {nameof(reportingPeriod.Start)} and/or {nameof(reportingPeriod.End)} is unbounded"));
            }

            if (maxUnitsInAnyReportingPeriod < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(maxUnitsInAnyReportingPeriod), "max units in any reporting period is <= 0");
            }

            var allUnits = reportingPeriod.GetUnitsWithin();
            var allReportingPeriods = new List<IReportingPeriod<T>>();
            for (int unitOfTimeIndex = 0; unitOfTimeIndex < allUnits.Count; unitOfTimeIndex++)
            {
                for (int numberOfUnits = 1; numberOfUnits <= maxUnitsInAnyReportingPeriod; numberOfUnits++)
                {
                    if (unitOfTimeIndex + numberOfUnits - 1 < allUnits.Count)
                    {
                        var subReportingPeriod = new ReportingPeriod<T>(allUnits[unitOfTimeIndex], allUnits[unitOfTimeIndex + numberOfUnits - 1]);
                        allReportingPeriods.Add(subReportingPeriod);
                    }
                }
            }

            return allReportingPeriods;
        }

        /// <summary>
        /// Splits a reporting period into units-of-time by a specified granularity.
        /// </summary>
        /// <param name="reportingPeriod">The reporting period to split.</param>
        /// <param name="granularity">The granularity to use when splitting.</param>
        /// <param name="overflowStrategy">
        /// The strategy to use when <paramref name="granularity"/> is less granular than
        /// the <paramref name="reportingPeriod"/> and, when splitting, the resulting units-of-time
        /// cannot be aligned with the start and end of the reporting period.  For example,
        /// splitting Mar2015-Feb2017 by year results in 2015,2016,2017, however only 2016 is
        /// fully contained within the reporting period.  The reporting period is missing Jan2015-Feb2015
        /// and March2017-Dec2017.
        /// </param>
        /// <returns>
        /// Returns the units-of-time that split the specified reporting period by the specified granularity.
        /// The units-of-time will always be in the specified granularity, regardless of the granularity
        /// of the reporting period (e.g. splitting a fiscal month reporting period using yearly granularity
        /// will return <see cref="FiscalYear"/> objects).
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="reportingPeriod"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="reportingPeriod"/> has a component that is <see cref="UnitOfTimeGranularity.Unbounded"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="granularity"/> is <see cref="UnitOfTimeGranularity.Invalid"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="overflowStrategy"/> is not <see cref="OverflowStrategy.ThrowOnOverflow"/>.</exception>
        /// <exception cref="InvalidOperationException">There was some overflow when splitting.</exception>
        public static IList<UnitOfTime> Split(this IReportingPeriod<UnitOfTime> reportingPeriod, UnitOfTimeGranularity granularity, OverflowStrategy overflowStrategy = OverflowStrategy.ThrowOnOverflow)
        {
            if (reportingPeriod == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod));
            }

            if (reportingPeriod.HasComponentWithUnboundedGranularity())
            {
                throw new ArgumentException(Invariant($"{nameof(reportingPeriod)} {nameof(reportingPeriod.Start)} and/or {nameof(reportingPeriod.End)} is {nameof(UnitOfTimeGranularity.Unbounded)}"));
            }

            if (granularity == UnitOfTimeGranularity.Invalid)
            {
                throw new ArgumentException(Invariant($"{nameof(granularity)} is {nameof(UnitOfTimeGranularity.Invalid)}"));
            }

            if (overflowStrategy != OverflowStrategy.ThrowOnOverflow)
            {
                throw new ArgumentException(Invariant($"{nameof(overflowStrategy)} is not {nameof(OverflowStrategy.ThrowOnOverflow)}"));
            }

            if (reportingPeriod.GetUnitOfTimeGranularity().IsAsGranularOrLessGranularThan(granularity))
            {
                var result = reportingPeriod.MakeMoreGranular(granularity).GetUnitsWithin();
                return result;
            }
            else
            {
                var result = reportingPeriod.MakeLessGranular(granularity).GetUnitsWithin();
                return result;
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

        /// <summary>
        /// Deserializes an <see cref="IReportingPeriod{T}"/> from a string.
        /// </summary>
        /// <typeparam name="TReportingPeriod">The type of reporting period.</typeparam>
        /// <param name="reportingPeriod">The serialized reperiod period string to deserialize.</param>
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

            Type unboundGenericType = typeof(ReportingPeriod<>);
            string errorMessage = Invariant($"Cannot deserialize string;  it appears to be a {unboundGenericType.Name} but it is not assignable to type of reporting period requested.");
            var requestedType = typeof(TReportingPeriod);
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

            // ReSharper disable UseMethodIsInstanceOfType
            errorMessage = Invariant($"Cannot deserialize string;  it appears to be a {unboundGenericType.Name} but the type of unit-of-time of the start and/or the end of the reporting period is not assignable to unit-of-time of the requested reporting period.");
            if (!requestedUnitOfTimeType.IsAssignableFrom(start.GetType()))
            {
                throw new InvalidOperationException(errorMessage);
            }

            if (!requestedUnitOfTimeType.IsAssignableFrom(end.GetType()))
            {
                throw new InvalidOperationException(errorMessage);
            }

            // ReSharper restore UseMethodIsInstanceOfType
            errorMessage = Invariant($"Cannot deserialize string;  it appears to be a {unboundGenericType.Name} but it is malformed.  The following error occured when attempting to create it: ");

            try
            {
                var result = Activator.CreateInstance(genericTypeToCreate, start, end);
                return result as TReportingPeriod;
            }
            catch (TargetInvocationException ex)
            {
                throw new InvalidOperationException(errorMessage + ex.InnerException?.Message);
            }
        }

        /// <summary>
        /// Gets the kind of the unit-of-time used in a reporting period.
        /// </summary>
        /// <param name="reportingPeriod">The reporting period.</param>
        /// <returns>
        /// The kind of the unit-of-time used in the specified reporting period.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="reportingPeriod"/> is null.</exception>
        public static UnitOfTimeKind GetUnitOfTimeKind(this IReportingPeriod<UnitOfTime> reportingPeriod)
        {
            if (reportingPeriod == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod));
            }

            var result = reportingPeriod.Start.UnitOfTimeKind;
            return result;
        }

        /// <summary>
        /// Gets the granularity of the unit-of-time used in a reporting period.
        /// </summary>
        /// <param name="reportingPeriod">The reporting period.</param>
        /// <returns>
        /// The granularity of the unit-of-time used in the specified reporting period.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="reportingPeriod"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="reportingPeriod"/> <see cref="ReportingPeriod{T}.Start"/> and <see cref="ReportingPeriod{T}.End"/> has different granularity.</exception>
        public static UnitOfTimeGranularity GetUnitOfTimeGranularity(this IReportingPeriod<UnitOfTime> reportingPeriod)
        {
            if (reportingPeriod == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod));
            }

            if (reportingPeriod.Start.UnitOfTimeGranularity != reportingPeriod.End.UnitOfTimeGranularity)
            {
                throw new ArgumentException(Invariant($"{nameof(reportingPeriod)} Start and End has different granularity."));
            }

            var result = reportingPeriod.Start.UnitOfTimeGranularity;
            return result;
        }

        /// <summary>
        /// Determines if the reporting period has a component with unbounded granularity.
        /// </summary>
        /// <param name="reportingPeriod">The reporting period.</param>
        /// <returns>
        /// true if one or both components of the reporting period has unbounded granularity; otherwise false.
        /// </returns>
        public static bool HasComponentWithUnboundedGranularity(this IReportingPeriod<UnitOfTime> reportingPeriod)
        {
            if (reportingPeriod == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod));
            }

            var result = (reportingPeriod.Start.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded) ||
                         (reportingPeriod.End.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded);
            return result;
        }

        private static IReportingPeriod<UnitOfTime> MakeMostGranular(this IReportingPeriod<UnitOfTime> reportingPeriod)
        {
            var mostGranularStart = reportingPeriod.Start.MakeMostGranular();
            var mostGranularEnd = reportingPeriod.End.MakeMostGranular();
            var result = new ReportingPeriod<UnitOfTime>(mostGranularStart.Start, mostGranularEnd.End);
            return result;
        }

        private static IReportingPeriod<UnitOfTime> MakeMostGranular(this UnitOfTime unitOfTime)
        {
            if (unitOfTime.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded)
            {
                var result = new ReportingPeriod<UnitOfTime>(unitOfTime, unitOfTime);
                return result;
            }

            if (unitOfTime.UnitOfTimeGranularity == UnitOfTimeGranularity.Year)
            {
                // ReSharper disable PossibleNullReferenceException
                if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Calendar)
                {
                    var calendarYear = unitOfTime as CalendarYear;
                    var result = new ReportingPeriod<UnitOfTime>(calendarYear.GetFirstCalendarDay(), calendarYear.GetLastCalendarDay());
                    return result;
                }

                var year = unitOfTime as IHaveAYear;
                if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Fiscal)
                {
                    var result = new ReportingPeriod<UnitOfTime>(new FiscalMonth(year.Year, MonthNumber.One), new FiscalMonth(year.Year, MonthNumber.Twelve));
                    return result;
                }

                if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Generic)
                {
                    var result = new ReportingPeriod<UnitOfTime>(new GenericMonth(year.Year, MonthNumber.One), new GenericMonth(year.Year, MonthNumber.Twelve));
                    return result;
                }

                // ReSharper restore PossibleNullReferenceException
                throw new NotSupportedException("This kind of unit-of-time is not supported: " + unitOfTime.UnitOfTimeKind);
            }

            if (unitOfTime.UnitOfTimeGranularity == UnitOfTimeGranularity.Quarter)
            {
                if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Calendar)
                {
                    var calendarQuarter = unitOfTime as CalendarQuarter;
                    var result = new ReportingPeriod<UnitOfTime>(calendarQuarter.GetFirstCalendarDay(), calendarQuarter.GetLastCalendarDay());
                    return result;
                }

                // ReSharper disable PossibleNullReferenceException
                var quarter = unitOfTime as IHaveAQuarter;
                var startMonth = (((int)quarter.QuarterNumber - 1) * 3) + 1;
                var endMonth = (int)quarter.QuarterNumber * 3;

                if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Fiscal)
                {
                    var result = new ReportingPeriod<UnitOfTime>(new FiscalMonth(quarter.Year, (MonthNumber)startMonth), new FiscalMonth(quarter.Year, (MonthNumber)endMonth));
                    return result;
                }

                if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Generic)
                {
                    var result = new ReportingPeriod<UnitOfTime>(new GenericMonth(quarter.Year, (MonthNumber)startMonth), new GenericMonth(quarter.Year, (MonthNumber)endMonth));
                    return result;
                }

                // ReSharper restore PossibleNullReferenceException
                throw new NotSupportedException("This kind of unit-of-time is not supported: " + unitOfTime.UnitOfTimeKind);
            }

            if (unitOfTime.UnitOfTimeGranularity == UnitOfTimeGranularity.Month)
            {
                if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Calendar)
                {
                    var calendarMonth = unitOfTime as CalendarMonth;
                    var result = new ReportingPeriod<UnitOfTime>(calendarMonth.GetFirstCalendarDay(), calendarMonth.GetLastCalendarDay());
                    return result;
                }

                if ((unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Fiscal) || (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Generic))
                {
                    var result = new ReportingPeriod<UnitOfTime>(unitOfTime, unitOfTime);
                    return result;
                }

                throw new NotSupportedException("This kind of unit-of-time is not supported: " + unitOfTime.UnitOfTimeKind);
            }

            if (unitOfTime.UnitOfTimeGranularity == UnitOfTimeGranularity.Day)
            {
                if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Calendar)
                {
                    var result = new ReportingPeriod<UnitOfTime>(unitOfTime, unitOfTime);
                    return result;
                }

                throw new NotSupportedException("This kind of unit-of-time is not supported: " + unitOfTime.UnitOfTimeKind);
            }

            throw new NotSupportedException("This granularity is not supported: " + unitOfTime.UnitOfTimeGranularity);
        }

        private static IReportingPeriod<UnitOfTime> MakeMoreGranular(this IReportingPeriod<UnitOfTime> reportingPeriod, UnitOfTimeGranularity granularity)
        {
            if (reportingPeriod.GetUnitOfTimeGranularity().IsMoreGranularThan(granularity))
            {
                throw new ArgumentException(Invariant($"{nameof(reportingPeriod)} is more granular than {nameof(granularity)}"));
            }

            var moreGranularStart = reportingPeriod.Start.MakeMoreGranular(granularity);
            var moreGranularEnd = reportingPeriod.End.MakeMoreGranular(granularity);
            var result = new ReportingPeriod<UnitOfTime>(moreGranularStart.Start, moreGranularEnd.End);
            return result;
        }

        private static IReportingPeriod<UnitOfTime> MakeMoreGranular(this UnitOfTime unitOfTime, UnitOfTimeGranularity granularity)
        {
            if (unitOfTime.UnitOfTimeGranularity.IsMoreGranularThan(granularity))
            {
                throw new ArgumentException(Invariant($"{nameof(unitOfTime)} is more granular than {nameof(granularity)}"));
            }

            if (unitOfTime.UnitOfTimeGranularity == granularity)
            {
                var result = new ReportingPeriod<UnitOfTime>(unitOfTime, unitOfTime);
                return result;
            }

            if (unitOfTime.UnitOfTimeGranularity == UnitOfTimeGranularity.Year)
            {
                // ReSharper disable PossibleNullReferenceException
                var unitOfTimeAsYear = unitOfTime as IHaveAYear;
                var startQuarter = QuarterNumber.Q1;
                var endQuarter = QuarterNumber.Q4;

                if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Calendar)
                {
                    var moreGranularReportingPeriod = new ReportingPeriod<UnitOfTime>(new CalendarQuarter(unitOfTimeAsYear.Year, startQuarter), new CalendarQuarter(unitOfTimeAsYear.Year, endQuarter));
                    var result = moreGranularReportingPeriod.MakeMoreGranular(granularity);
                    return result;
                }

                if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Fiscal)
                {
                    var moreGranularReportingPeriod = new ReportingPeriod<UnitOfTime>(new FiscalQuarter(unitOfTimeAsYear.Year, startQuarter), new FiscalQuarter(unitOfTimeAsYear.Year, endQuarter));
                    var result = moreGranularReportingPeriod.MakeMoreGranular(granularity);
                    return result;
                }

                if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Generic)
                {
                    var moreGranularReportingPeriod = new ReportingPeriod<UnitOfTime>(new GenericQuarter(unitOfTimeAsYear.Year, startQuarter), new GenericQuarter(unitOfTimeAsYear.Year, endQuarter));
                    var result = moreGranularReportingPeriod.MakeMoreGranular(granularity);
                    return result;
                }

                // ReSharper restore PossibleNullReferenceException
                throw new NotSupportedException("This kind of unit-of-time is not supported: " + unitOfTime.UnitOfTimeKind);
            }

            if (unitOfTime.UnitOfTimeGranularity == UnitOfTimeGranularity.Quarter)
            {
                // ReSharper disable PossibleNullReferenceException
                var unitOfTimeAsQuarter = unitOfTime as IHaveAQuarter;
                var startMonth = ((((int)unitOfTimeAsQuarter.QuarterNumber) - 1) * 3) + 1;
                var endMonth = ((int)unitOfTimeAsQuarter.QuarterNumber) * 3;

                if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Calendar)
                {
                    var moreGranularReportingPeriod = new ReportingPeriod<UnitOfTime>(new CalendarMonth(unitOfTimeAsQuarter.Year, (MonthOfYear)startMonth), new CalendarMonth(unitOfTimeAsQuarter.Year, (MonthOfYear)endMonth));
                    var result = moreGranularReportingPeriod.MakeMoreGranular(granularity);
                    return result;
                }

                if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Fiscal)
                {
                    var moreGranularReportingPeriod = new ReportingPeriod<UnitOfTime>(new FiscalMonth(unitOfTimeAsQuarter.Year, (MonthNumber)startMonth), new FiscalMonth(unitOfTimeAsQuarter.Year, (MonthNumber)endMonth));
                    var result = moreGranularReportingPeriod.MakeMoreGranular(granularity);
                    return result;
                }

                if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Generic)
                {
                    var moreGranularReportingPeriod = new ReportingPeriod<UnitOfTime>(new GenericMonth(unitOfTimeAsQuarter.Year, (MonthNumber)startMonth), new GenericMonth(unitOfTimeAsQuarter.Year, (MonthNumber)endMonth));
                    var result = moreGranularReportingPeriod.MakeMoreGranular(granularity);
                    return result;
                }

                // ReSharper restore PossibleNullReferenceException
                throw new NotSupportedException("This kind of unit-of-time is not supported: " + unitOfTime.UnitOfTimeKind);
            }

            if (unitOfTime.UnitOfTimeGranularity == UnitOfTimeGranularity.Month)
            {
                if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Calendar)
                {
                    var calendarUnitOfTime = unitOfTime as CalendarUnitOfTime;
                    var moreGranularReportingPeriod = new ReportingPeriod<UnitOfTime>(calendarUnitOfTime.GetFirstCalendarDay(), calendarUnitOfTime.GetLastCalendarDay());
                    var result = moreGranularReportingPeriod.MakeMoreGranular(granularity);
                    return result;
                }

                if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Fiscal)
                {
                    throw new NotSupportedException("The Fiscal kind cannot be made more granular than Month.");
                }

                if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Generic)
                {
                    throw new NotSupportedException("The Generic kind cannot be made more granular than Month.");
                }

                throw new NotSupportedException("This kind of unit-of-time is not supported: " + unitOfTime.UnitOfTimeKind);
            }

            throw new NotSupportedException("This granularity is not supported: " + unitOfTime.UnitOfTimeGranularity);
        }

        private static IReportingPeriod<UnitOfTime> MakeLessGranular(this IReportingPeriod<UnitOfTime> reportingPeriod, UnitOfTimeGranularity granularity)
        {
            var reportingPeriodGranularity = reportingPeriod.GetUnitOfTimeGranularity();
            if (reportingPeriodGranularity.IsLessGranularThan(granularity))
            {
                throw new ArgumentException(Invariant($"{nameof(reportingPeriod)} is less granular than {nameof(granularity)}"));
            }

            if (reportingPeriodGranularity == granularity)
            {
                return reportingPeriod;
            }

            var unitOfTimeKind = reportingPeriod.GetUnitOfTimeKind();
            if (reportingPeriodGranularity == UnitOfTimeGranularity.Day)
            {
                // ReSharper disable PossibleNullReferenceException
                if (unitOfTimeKind == UnitOfTimeKind.Calendar)
                {
                    var startAsCalendarDay = reportingPeriod.Start as CalendarDay;
                    var startMonth = new CalendarMonth(startAsCalendarDay.Year, startAsCalendarDay.MonthOfYear);
                    if (startMonth.GetFirstCalendarDay() != startAsCalendarDay)
                    {
                        throw new InvalidOperationException("Cannot convert a calendar day reporting period to a calendar month reporting period when the reporting period start time is not the first day of a month.");
                    }

                    var endAsCalendarDay = reportingPeriod.End as CalendarDay;
                    var endMonth = new CalendarMonth(endAsCalendarDay.Year, endAsCalendarDay.MonthOfYear);
                    if (endMonth.GetLastCalendarDay() != endAsCalendarDay)
                    {
                        throw new InvalidOperationException("Cannot convert a calendar day reporting period to a calendar month reporting period when the reporting period end time is not the last day of a month.");
                    }

                    var lessGranularReportingPeriod = new ReportingPeriod<CalendarMonth>(startMonth, endMonth);
                    var result = MakeLessGranular(lessGranularReportingPeriod, granularity);
                    return result;
                }

                // ReSharper restore PossibleNullReferenceException
                throw new NotSupportedException("This kind of unit-of-time is not supported: " + unitOfTimeKind);
            }

            if (reportingPeriodGranularity == UnitOfTimeGranularity.Month)
            {
                // ReSharper disable PossibleNullReferenceException
                var startAsMonth = reportingPeriod.Start as IHaveAMonth;
                var endAsMonth = reportingPeriod.End as IHaveAMonth;

                var quarterByStartMonth = new Dictionary<int, QuarterNumber>
                {
                    { 1, QuarterNumber.Q1 },
                    { 4, QuarterNumber.Q2 },
                    { 7, QuarterNumber.Q3 },
                    { 10, QuarterNumber.Q4 }
                };

                var quarterByEndMonth = new Dictionary<int, QuarterNumber>
                {
                    { 3, QuarterNumber.Q1 },
                    { 6, QuarterNumber.Q2 },
                    { 9, QuarterNumber.Q3 },
                    { 12, QuarterNumber.Q4 }
                };

                if (!quarterByStartMonth.ContainsKey((int)startAsMonth.MonthNumber))
                {
                    throw new InvalidOperationException("Cannot convert a monthly reporting period to a quarterly reporting period when the reporting period start time is not the first month of a recognized quarter.");
                }

                if (!quarterByEndMonth.ContainsKey((int)endAsMonth.MonthNumber))
                {
                    throw new InvalidOperationException("Cannot convert a monthly reporting period to a quarterly reporting period when the reporting period end time is not the last month of a recognized quarter.");
                }

                if (unitOfTimeKind == UnitOfTimeKind.Calendar)
                {
                    var startQuarter = new CalendarQuarter(startAsMonth.Year, quarterByStartMonth[(int)startAsMonth.MonthNumber]);
                    var endQuarter = new CalendarQuarter(endAsMonth.Year, quarterByEndMonth[(int)endAsMonth.MonthNumber]);
                    var lessGranularReportingPeriod = new ReportingPeriod<CalendarQuarter>(startQuarter, endQuarter);
                    var result = MakeLessGranular(lessGranularReportingPeriod, granularity);
                    return result;
                }

                if (unitOfTimeKind == UnitOfTimeKind.Fiscal)
                {
                    var startQuarter = new FiscalQuarter(startAsMonth.Year, quarterByStartMonth[(int)startAsMonth.MonthNumber]);
                    var endQuarter = new FiscalQuarter(endAsMonth.Year, quarterByEndMonth[(int)endAsMonth.MonthNumber]);
                    var lessGranularReportingPeriod = new ReportingPeriod<FiscalQuarter>(startQuarter, endQuarter);
                    var result = MakeLessGranular(lessGranularReportingPeriod, granularity);
                    return result;
                }

                if (unitOfTimeKind == UnitOfTimeKind.Generic)
                {
                    var startQuarter = new GenericQuarter(startAsMonth.Year, quarterByStartMonth[(int)startAsMonth.MonthNumber]);
                    var endQuarter = new GenericQuarter(endAsMonth.Year, quarterByEndMonth[(int)endAsMonth.MonthNumber]);
                    var lessGranularReportingPeriod = new ReportingPeriod<GenericQuarter>(startQuarter, endQuarter);
                    var result = MakeLessGranular(lessGranularReportingPeriod, granularity);
                    return result;
                }

                // ReSharper restore PossibleNullReferenceException
                throw new NotSupportedException("This kind of unit-of-time is not supported: " + unitOfTimeKind);
            }

            if (reportingPeriodGranularity == UnitOfTimeGranularity.Quarter)
            {
                // ReSharper disable PossibleNullReferenceException
                var startAsQuarter = reportingPeriod.Start as IHaveAQuarter;
                var endAsQuarter = reportingPeriod.End as IHaveAQuarter;

                if (startAsQuarter.QuarterNumber != QuarterNumber.Q1)
                {
                    throw new InvalidOperationException("Cannot convert a quarterly reporting period to a yearly reporting period when the reporting period start time is not Q1.");
                }

                if (endAsQuarter.QuarterNumber != QuarterNumber.Q4)
                {
                    throw new InvalidOperationException("Cannot convert a quarterly reporting period to a yearly reporting period when the reporting period end is not Q4.");
                }

                if (unitOfTimeKind == UnitOfTimeKind.Calendar)
                {
                    var startYear = new CalendarYear(startAsQuarter.Year);
                    var endYear = new CalendarYear(endAsQuarter.Year);
                    var lessGranularReportingPeriod = new ReportingPeriod<CalendarYear>(startYear, endYear);
                    var result = MakeLessGranular(lessGranularReportingPeriod, granularity);
                    return result;
                }

                if (unitOfTimeKind == UnitOfTimeKind.Fiscal)
                {
                    var startYear = new FiscalYear(startAsQuarter.Year);
                    var endYear = new FiscalYear(endAsQuarter.Year);
                    var lessGranularReportingPeriod = new ReportingPeriod<FiscalYear>(startYear, endYear);
                    var result = MakeLessGranular(lessGranularReportingPeriod, granularity);
                    return result;
                }

                if (unitOfTimeKind == UnitOfTimeKind.Generic)
                {
                    var startYear = new GenericYear(startAsQuarter.Year);
                    var endYear = new GenericYear(endAsQuarter.Year);
                    var lessGranularReportingPeriod = new ReportingPeriod<GenericYear>(startYear, endYear);
                    var result = MakeLessGranular(lessGranularReportingPeriod, granularity);
                    return result;
                }

                // ReSharper restore PossibleNullReferenceException
                throw new NotSupportedException("This kind of unit-of-time is not supported: " + unitOfTimeKind);
            }

            throw new NotSupportedException("This granularity is not supported: " + reportingPeriodGranularity);
        }
    }
}

// ReSharper restore CheckNamespace