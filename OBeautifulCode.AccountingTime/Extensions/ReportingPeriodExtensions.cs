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

            var result = new ReportingPeriod<UnitOfTime>(start, end).Clone<TReportingPeriod>();
            return result;
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
            if (unitOfTime == null)
            {
                throw new ArgumentNullException(nameof(unitOfTime));
            }

            if (reportingPeriod == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod));
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

            reportingPeriod1 = reportingPeriod1.ConvertToMostGranular();
            reportingPeriod2 = reportingPeriod2.ConvertToMostGranular();

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

            if ((reportingPeriod.Start.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded) || (reportingPeriod.End.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded))
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

            if ((reportingPeriod.Start.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded) || (reportingPeriod.End.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded))
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
        public static UnitOfTimeKind GetUnitOfTimeKind(this IReportingPeriod<UnitOfTime> reportingPeriod)
        {
            if (reportingPeriod == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod));
            }

            var result = reportingPeriod.Start.UnitOfTimeKind;
            return result;
        }

        private static IReportingPeriod<UnitOfTime> ConvertToMostGranular(this IReportingPeriod<UnitOfTime> reportingPeriod)
        {
            var moreGranularStart = ConvertToMostGranular(reportingPeriod.Start);
            var moreGranularEnd = ConvertToMostGranular(reportingPeriod.End);
            var result = new ReportingPeriod<UnitOfTime>(moreGranularStart.Start, moreGranularEnd.End);
            return result;
        }

        private static IReportingPeriod<UnitOfTime> ConvertToMostGranular(this UnitOfTime unitOfTime)
        {
            if (unitOfTime.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded)
            {
                var result = new ReportingPeriod<UnitOfTime>(unitOfTime, unitOfTime);
                return result;
            }

            if (unitOfTime.UnitOfTimeGranularity == UnitOfTimeGranularity.Year)
            {
                var year = unitOfTime as IHaveAYear;
                ReportingPeriod<UnitOfTime> reportingPeriodInQuarters;
                var startQuarter = QuarterNumber.Q1;
                var endQuarter = QuarterNumber.Q4;

                // ReSharper disable PossibleNullReferenceException
                if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Calendar)
                {
                    reportingPeriodInQuarters = new ReportingPeriod<UnitOfTime>(new CalendarQuarter(year.Year, startQuarter), new CalendarQuarter(year.Year, endQuarter));
                }
                else if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Fiscal)
                {
                    reportingPeriodInQuarters = new ReportingPeriod<UnitOfTime>(new FiscalQuarter(year.Year, startQuarter), new FiscalQuarter(year.Year, endQuarter));
                }
                else if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Generic)
                {
                    reportingPeriodInQuarters = new ReportingPeriod<UnitOfTime>(new GenericQuarter(year.Year, startQuarter), new GenericQuarter(year.Year, endQuarter));
                }
                else
                {
                    throw new NotSupportedException("This kind of unit-of-time is not supported: " + unitOfTime.UnitOfTimeKind);
                }

                // ReSharper restore PossibleNullReferenceException
                var result = ConvertToMostGranular(reportingPeriodInQuarters);
                return result;
            }

            if (unitOfTime.UnitOfTimeGranularity == UnitOfTimeGranularity.Quarter)
            {
                var quarter = unitOfTime as IHaveAQuarter;

                // ReSharper disable PossibleNullReferenceException
                var startMonth = (((int)quarter.QuarterNumber - 1) * 3) + 1;
                var endMonth = (int)quarter.QuarterNumber * 3;
                ReportingPeriod<UnitOfTime> reportingPeriodInMonths;

                if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Calendar)
                {
                    reportingPeriodInMonths = new ReportingPeriod<UnitOfTime>(new CalendarMonth(quarter.Year, (MonthOfYear)startMonth), new CalendarMonth(quarter.Year, (MonthOfYear)endMonth));
                }
                else if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Fiscal)
                {
                    reportingPeriodInMonths = new ReportingPeriod<UnitOfTime>(new FiscalMonth(quarter.Year, (MonthNumber)startMonth), new FiscalMonth(quarter.Year, (MonthNumber)endMonth));
                }
                else if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Generic)
                {
                    reportingPeriodInMonths = new ReportingPeriod<UnitOfTime>(new GenericMonth(quarter.Year, (MonthNumber)startMonth), new GenericMonth(quarter.Year, (MonthNumber)endMonth));
                }
                else
                {
                    throw new NotSupportedException("This kind of unit-of-time is not supported: " + unitOfTime.UnitOfTimeKind);
                }

                // ReSharper restore PossibleNullReferenceException
                var result = ConvertToMostGranular(reportingPeriodInMonths);
                return result;
            }

            if (unitOfTime.UnitOfTimeGranularity == UnitOfTimeGranularity.Month)
            {
                if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Calendar)
                {
                    var calendarMonth = unitOfTime as CalendarMonth;
                    var result = new ReportingPeriod<UnitOfTime>(calendarMonth.GetFirstCalendarDay(), calendarMonth.GetLastCalendarDay());
                    return result;
                }

                if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Fiscal)
                {
                    var result = new ReportingPeriod<UnitOfTime>(unitOfTime, unitOfTime);
                    return result;
                }

                if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Generic)
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
    }
}

// ReSharper restore CheckNamespace