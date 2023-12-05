﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriodExtensions.Manipulation.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using static System.FormattableString;

    /// <summary>
    /// Extension methods to shape and manipulate a <see cref="ReportingPeriod"/>.
    /// </summary>
    public static partial class ReportingPeriodExtensions
    {
        /// <summary>
        /// Clones a reporting period while adjusting the start or end of the reporting period, or both.
        /// </summary>
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
        /// <exception cref="InvalidOperationException">The adjustment has caused the <see cref="ReportingPeriod.Start"/> to be greater than <see cref="ReportingPeriod.End"/>.</exception>
        public static ReportingPeriod CloneWithAdjustment(
            this ReportingPeriod reportingPeriod,
            ReportingPeriodComponent component,
            int unitsToAdd,
            UnitOfTimeGranularity granularityOfUnitsToAdd)
        {
            if (reportingPeriod == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod));
            }

            if (component == ReportingPeriodComponent.Invalid)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(component)}' == '{ReportingPeriodComponent.Invalid}'"), (Exception)null);
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
                var result = new ReportingPeriod(start, end);

                return result;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new InvalidOperationException("The adjustment has caused the Start of the reporting period to be greater than the End of the reporting period.", ex);
            }
        }

        /// <summary>
        /// Creates all permutations of reporting periods between the
        /// start and end of a specified reporting period, from 1 unit
        /// to the specified number of maximum number of units that a
        /// reporting period can contain.
        /// </summary>
        /// <param name="reportingPeriod">The reporting period to permute.</param>
        /// <param name="maxUnitsInAnyReportingPeriod">Maximum number of units-of-time in each reporting period.</param>
        /// <returns>All possible reporting periods containing between 1 and <paramref name="maxUnitsInAnyReportingPeriod"/> units-of-time, contained within <paramref name="reportingPeriod"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="reportingPeriod"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="reportingPeriod"/> has an <see cref="UnitOfTimeGranularity.Unbounded"/> component.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="maxUnitsInAnyReportingPeriod"/> is less than or equal to 0.</exception>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is a perfectly fine usage of nesting generic types.")]
        public static IReadOnlyCollection<ReportingPeriod> CreatePermutations(
            this ReportingPeriod reportingPeriod,
            int maxUnitsInAnyReportingPeriod)
        {
            if (reportingPeriod == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod));
            }

            if (reportingPeriod.HasComponentWithUnboundedGranularity())
            {
                throw new ArgumentException(Invariant($"{nameof(reportingPeriod)} has an {nameof(UnitOfTimeGranularity.Unbounded)} component."));
            }

            if (maxUnitsInAnyReportingPeriod < 1)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(maxUnitsInAnyReportingPeriod)}' < '{1}'"), (Exception)null);
            }

            var allUnits = reportingPeriod.GetUnitsOfTimeWithin();

            var result = new List<ReportingPeriod>();

            for (int unitOfTimeIndex = 0; unitOfTimeIndex < allUnits.Count; unitOfTimeIndex++)
            {
                for (int numberOfUnits = 1; numberOfUnits <= maxUnitsInAnyReportingPeriod; numberOfUnits++)
                {
                    if (unitOfTimeIndex + numberOfUnits - 1 < allUnits.Count)
                    {
                        var subReportingPeriod = new ReportingPeriod(allUnits[unitOfTimeIndex], allUnits[unitOfTimeIndex + numberOfUnits - 1]);
                        result.Add(subReportingPeriod);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Splits a reporting period into units-of-time by a specified granularity.
        /// </summary>
        /// <param name="reportingPeriod">The reporting period to split.</param>
        /// <param name="granularity">The granularity to use when splitting.</param>
        /// <param name="overflowStrategy">
        /// OPTIONAL strategy to use when <paramref name="granularity"/> is less granular than
        /// the <paramref name="reportingPeriod"/> and, when splitting, the resulting units-of-time
        /// cannot be aligned with the start and end of the reporting period.
        /// For example, splitting March 2015 - February 2017 by year results in 2015,2016,2017,
        /// however only 2016 is fully contained within the reporting period.
        /// The reporting period is missing January 2015 - February 2015 and March 2017 to December 2017.
        /// DEFAULT is to throw when this happens.
        /// </param>
        /// <returns>
        /// Returns the units-of-time that split the specified reporting period by the specified granularity.
        /// The units-of-time will always be in the specified granularity, regardless of the granularity
        /// of the reporting period (e.g. splitting a fiscal month reporting period using yearly granularity
        /// will return <see cref="FiscalYear"/> objects).
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="reportingPeriod"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="reportingPeriod"/> has an <see cref="UnitOfTimeGranularity.Unbounded"/> component.</exception>
        /// <exception cref="ArgumentException"><paramref name="granularity"/> is <see cref="UnitOfTimeGranularity.Invalid"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="granularity"/> is <see cref="UnitOfTimeGranularity.Unbounded"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="overflowStrategy"/> is not <see cref="OverflowStrategy.ThrowOnOverflow"/>.</exception>
        /// <exception cref="InvalidOperationException">There was some overflow when splitting.</exception>
        public static IReadOnlyList<UnitOfTime> Split(
            this ReportingPeriod reportingPeriod,
            UnitOfTimeGranularity granularity,
            OverflowStrategy overflowStrategy = OverflowStrategy.ThrowOnOverflow)
        {
            if (reportingPeriod == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod));
            }

            if (reportingPeriod.HasComponentWithUnboundedGranularity())
            {
                throw new ArgumentException(Invariant($"{nameof(reportingPeriod)} has an {nameof(UnitOfTimeGranularity.Unbounded)} component."));
            }

            if (granularity == UnitOfTimeGranularity.Invalid)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(granularity)}' == '{UnitOfTimeGranularity.Invalid}'"), (Exception)null);
            }

            if (granularity == UnitOfTimeGranularity.Unbounded)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(granularity)}' == '{UnitOfTimeGranularity.Unbounded}'"), (Exception)null);
            }

            if ((overflowStrategy != OverflowStrategy.ThrowOnOverflow) && (overflowStrategy != OverflowStrategy.DiscardOverflow))
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(overflowStrategy)}' is not one of {{{OverflowStrategy.ThrowOnOverflow}, {OverflowStrategy.DiscardOverflow}}}."), (Exception)null);
            }

            var reportingPeriodGranularity = reportingPeriod.GetUnitOfTimeGranularity();

            IReadOnlyList<UnitOfTime> result;

            if (reportingPeriodGranularity == granularity)
            {
                result = reportingPeriod.GetUnitsOfTimeWithin();
            }
            else if (reportingPeriodGranularity.IsLessGranularThan(granularity))
            {
                result = reportingPeriod.MakeMoreGranular(granularity).GetUnitsOfTimeWithin();
            }
            else
            {
                var lessGranularReportingPeriod = reportingPeriod.MakeLessGranular(
                    granularity,
                    returnNullOnMisalignment: overflowStrategy == OverflowStrategy.ThrowOnOverflow);

                if (lessGranularReportingPeriod == null)
                {
                    throw new InvalidOperationException("There was some overflow when attempting to split.");
                }
                else
                {
                    result = lessGranularReportingPeriod.GetUnitsOfTimeWithin();

                    if (overflowStrategy == OverflowStrategy.DiscardOverflow)
                    {
                        result = result.Where(reportingPeriod.Contains).ToList();
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Converts the the specified reporting period into the most
        /// granular possible, but equivalent, reporting period.
        /// </summary>
        /// <param name="reportingPeriod">The reporting period to convert.</param>
        /// <returns>
        /// A reporting period that addresses the same set of time as <paramref name="reportingPeriod"/>,
        /// but is the most granular version possible of that reporting period.
        /// A reporting period whose start and end are both unbounded will be returned as-is.
        /// A reporting period with one unbounded and one bounded component will have it's bounded
        /// component converted (e.g. Unbounded to CalendarYear 2017 will be converted to Unbounded to 12/31/2017).
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="reportingPeriod"/> is null.</exception>
        public static ReportingPeriod ToMostGranular(
            this ReportingPeriod reportingPeriod)
        {
            if (reportingPeriod == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod));
            }

            var mostGranularStart = reportingPeriod.Start.ToMostGranular();
            var mostGranularEnd = reportingPeriod.End.ToMostGranular();

            var result = new ReportingPeriod(mostGranularStart.Start, mostGranularEnd.End);

            return result;
        }

        /// <summary>
        /// Converts the specified reporting period into the least
        /// granular possible, but equivalent, reporting period.
        /// </summary>
        /// <param name="reportingPeriod">The reporting period to convert.</param>
        /// <returns>
        /// A reporting period that addresses the same set of time as <paramref name="reportingPeriod"/>,
        /// but is the least granular version possible of that reporting period.
        /// A reporting period whose start and end are both unbounded will be returned as-is.
        /// A reporting period with one unbounded and one bounded component will be returned
        /// as-is (e.g. Unbounded to 12/31/2017 will NOT be converted to Unbounded to CalendarYear 2017).
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="reportingPeriod"/> is null.</exception>
        public static ReportingPeriod ToLeastGranular(
            this ReportingPeriod reportingPeriod)
        {
            if (reportingPeriod == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod));
            }

            var result = reportingPeriod.MakeOneNotchLessGranular();

            result = result == null ? reportingPeriod : result.ToLeastGranular();

            return result;
        }

        private static ReportingPeriod MakeMoreGranular(
            this ReportingPeriod reportingPeriod,
            UnitOfTimeGranularity granularity)
        {
            if (reportingPeriod == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod));
            }

            if (reportingPeriod.HasComponentWithUnboundedGranularity())
            {
                throw new ArgumentException(Invariant($"{nameof(reportingPeriod)} has an {nameof(UnitOfTimeGranularity.Unbounded)} component."));
            }

            if (granularity == UnitOfTimeGranularity.Invalid)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(granularity)}' == '{UnitOfTimeGranularity.Invalid}'"), (Exception)null);
            }

            if (granularity == UnitOfTimeGranularity.Unbounded)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(granularity)}' == '{UnitOfTimeGranularity.Unbounded}'"), (Exception)null);
            }

            if (reportingPeriod.GetUnitOfTimeGranularity().IsAsGranularOrMoreGranularThan(granularity))
            {
                throw new ArgumentException(Invariant($"{nameof(reportingPeriod)} is as granular or more granular than {nameof(granularity)}."));
            }

            var moreGranularStart = reportingPeriod.Start.MakeMoreGranular(granularity);
            var moreGranularEnd = reportingPeriod.End.MakeMoreGranular(granularity);

            var result = new ReportingPeriod(moreGranularStart.Start, moreGranularEnd.End);

            return result;
        }

        private static ReportingPeriod MakeMoreGranular(
            this UnitOfTime unitOfTime,
            UnitOfTimeGranularity granularity)
        {
            if (unitOfTime == null)
            {
                throw new ArgumentNullException(nameof(unitOfTime));
            }

            if (unitOfTime.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded)
            {
                throw new ArgumentException(Invariant($"{nameof(unitOfTime)} granularity is {nameof(UnitOfTimeGranularity.Unbounded)}."));
            }

            if (granularity == UnitOfTimeGranularity.Invalid)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(granularity)}' == '{UnitOfTimeGranularity.Invalid}'"), (Exception)null);
            }

            if (granularity == UnitOfTimeGranularity.Unbounded)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(granularity)}' == '{UnitOfTimeGranularity.Unbounded}'"), (Exception)null);
            }

            if (unitOfTime.UnitOfTimeGranularity.IsAsGranularOrMoreGranularThan(granularity))
            {
                throw new ArgumentException(Invariant($"{nameof(unitOfTime)} is as granular or more granular than {nameof(granularity)}."));
            }

            ReportingPeriod moreGranularReportingPeriod;
            if (unitOfTime.UnitOfTimeGranularity == UnitOfTimeGranularity.Year)
            {
                var unitOfTimeAsYear = unitOfTime as IHaveAYear;
                var startQuarter = QuarterNumber.Q1;
                var endQuarter = QuarterNumber.Q4;

                if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Calendar)
                {
                    // ReSharper disable once PossibleNullReferenceException
                    moreGranularReportingPeriod = new ReportingPeriod(new CalendarQuarter(unitOfTimeAsYear.Year, startQuarter), new CalendarQuarter(unitOfTimeAsYear.Year, endQuarter));
                }
                else if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Fiscal)
                {
                    // ReSharper disable once PossibleNullReferenceException
                    moreGranularReportingPeriod = new ReportingPeriod(new FiscalQuarter(unitOfTimeAsYear.Year, startQuarter), new FiscalQuarter(unitOfTimeAsYear.Year, endQuarter));
                }
                else if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Generic)
                {
                    // ReSharper disable once PossibleNullReferenceException
                    moreGranularReportingPeriod = new ReportingPeriod(new GenericQuarter(unitOfTimeAsYear.Year, startQuarter), new GenericQuarter(unitOfTimeAsYear.Year, endQuarter));
                }
                else
                {
                    throw new NotSupportedException("This kind of unit-of-time is not supported: " + unitOfTime.UnitOfTimeKind);
                }
            }
            else if (unitOfTime.UnitOfTimeGranularity == UnitOfTimeGranularity.Quarter)
            {
                var unitOfTimeAsQuarter = unitOfTime as IHaveAQuarter;

                // ReSharper disable once PossibleNullReferenceException
                var startMonth = ((((int)unitOfTimeAsQuarter.QuarterNumber) - 1) * 3) + 1;
                var endMonth = ((int)unitOfTimeAsQuarter.QuarterNumber) * 3;

                if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Calendar)
                {
                    moreGranularReportingPeriod = new ReportingPeriod(new CalendarMonth(unitOfTimeAsQuarter.Year, (MonthOfYear)startMonth), new CalendarMonth(unitOfTimeAsQuarter.Year, (MonthOfYear)endMonth));
                }
                else if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Fiscal)
                {
                    moreGranularReportingPeriod = new ReportingPeriod(new FiscalMonth(unitOfTimeAsQuarter.Year, (MonthNumber)startMonth), new FiscalMonth(unitOfTimeAsQuarter.Year, (MonthNumber)endMonth));
                }
                else if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Generic)
                {
                    moreGranularReportingPeriod = new ReportingPeriod(new GenericMonth(unitOfTimeAsQuarter.Year, (MonthNumber)startMonth), new GenericMonth(unitOfTimeAsQuarter.Year, (MonthNumber)endMonth));
                }
                else
                {
                    throw new NotSupportedException("This kind of unit-of-time is not supported: " + unitOfTime.UnitOfTimeKind);
                }
            }
            else if (unitOfTime.UnitOfTimeGranularity == UnitOfTimeGranularity.Month)
            {
                if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Calendar)
                {
                    var calendarUnitOfTime = unitOfTime as CalendarUnitOfTime;
                    moreGranularReportingPeriod = new ReportingPeriod(calendarUnitOfTime.GetFirstCalendarDay(), calendarUnitOfTime.GetLastCalendarDay());
                }
                else if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Fiscal)
                {
                    throw new NotSupportedException("The Fiscal kind cannot be made more granular than Month.");
                }
                else if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Generic)
                {
                    throw new NotSupportedException("The Generic kind cannot be made more granular than Month.");
                }
                else
                {
                    throw new NotSupportedException("This kind of unit-of-time is not supported: " + unitOfTime.UnitOfTimeKind);
                }
            }
            else
            {
                throw new NotSupportedException("This granularity is not supported: " + unitOfTime.UnitOfTimeGranularity);
            }

            if (moreGranularReportingPeriod.GetUnitOfTimeGranularity() == granularity)
            {
                return moreGranularReportingPeriod;
            }

            var result = moreGranularReportingPeriod.MakeMoreGranular(granularity);

            return result;
        }

        private static ReportingPeriod MakeOneNotchLessGranular(
            this ReportingPeriod reportingPeriod)
        {
            ReportingPeriod result = null;

            if (!reportingPeriod.HasComponentWithUnboundedGranularity())
            {
                var targetGranularity = reportingPeriod.GetUnitOfTimeGranularity().OneNotchLessGranular();

                result = targetGranularity == UnitOfTimeGranularity.Unbounded
                    ? null
                    : reportingPeriod.MakeLessGranular(targetGranularity);
            }

            return result;
        }

        [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Somewhat the nature of the problem.")]
        private static ReportingPeriod MakeLessGranular(
            this ReportingPeriod reportingPeriod,
            UnitOfTimeGranularity granularity,
            bool returnNullOnMisalignment = true)
        {
            if (reportingPeriod == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod));
            }

            if (reportingPeriod.HasComponentWithUnboundedGranularity())
            {
                throw new ArgumentException(Invariant($"{nameof(reportingPeriod)} has an {nameof(UnitOfTimeGranularity.Unbounded)} component."));
            }

            if (granularity == UnitOfTimeGranularity.Invalid)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(granularity)}' == '{UnitOfTimeGranularity.Invalid}'"), (Exception)null);
            }

            if (granularity == UnitOfTimeGranularity.Unbounded)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(granularity)}' == '{UnitOfTimeGranularity.Unbounded}'"), (Exception)null);
            }

            var reportingPeriodGranularity = reportingPeriod.GetUnitOfTimeGranularity();
            if (reportingPeriodGranularity.IsAsGranularOrLessGranularThan(granularity))
            {
                throw new ArgumentException(Invariant($"{nameof(reportingPeriod)} is as granular or less granular than {nameof(granularity)}."));
            }

            ReportingPeriod lessGranularReportingPeriod;
            var unitOfTimeKind = reportingPeriod.GetUnitOfTimeKind();
            if (reportingPeriodGranularity == UnitOfTimeGranularity.Day)
            {
                if (unitOfTimeKind == UnitOfTimeKind.Calendar)
                {
                    var startAsCalendarDay = reportingPeriod.Start as CalendarDay;

                    // ReSharper disable once PossibleNullReferenceException
                    var startMonth = new CalendarMonth(startAsCalendarDay.Year, startAsCalendarDay.MonthOfYear);
                    if ((startMonth.GetFirstCalendarDay() != startAsCalendarDay) && returnNullOnMisalignment)
                    {
                        return null;
                    }

                    var endAsCalendarDay = reportingPeriod.End as CalendarDay;

                    // ReSharper disable once PossibleNullReferenceException
                    var endMonth = new CalendarMonth(endAsCalendarDay.Year, endAsCalendarDay.MonthOfYear);
                    if ((endMonth.GetLastCalendarDay() != endAsCalendarDay) &&  returnNullOnMisalignment)
                    {
                        return null;
                    }

                    lessGranularReportingPeriod = new ReportingPeriod(startMonth, endMonth);
                }
                else
                {
                    throw new NotSupportedException("This kind of unit-of-time is not supported: " + unitOfTimeKind);
                }
            }
            else if (reportingPeriodGranularity == UnitOfTimeGranularity.Month)
            {
                var startAsMonth = reportingPeriod.Start as IHaveAMonth;
                var endAsMonth = reportingPeriod.End as IHaveAMonth;

                var validStartMonths = new HashSet<int> { 1, 4, 7, 10 };

                // ReSharper disable once PossibleNullReferenceException
                if ((!validStartMonths.Contains((int)startAsMonth.MonthNumber)) && returnNullOnMisalignment)
                {
                    return null;
                }

                var validEndMonths = new HashSet<int> { 3, 6, 9, 12 };

                // ReSharper disable once PossibleNullReferenceException
                if ((!validEndMonths.Contains((int)endAsMonth.MonthNumber)) && returnNullOnMisalignment)
                {
                    return null;
                }

                var monthNumberToQuarterMap = new Dictionary<int, QuarterNumber>
                {
                    { 1, QuarterNumber.Q1 },
                    { 2, QuarterNumber.Q1 },
                    { 3, QuarterNumber.Q1 },
                    { 4, QuarterNumber.Q2 },
                    { 5, QuarterNumber.Q2 },
                    { 6, QuarterNumber.Q2 },
                    { 7, QuarterNumber.Q3 },
                    { 8, QuarterNumber.Q3 },
                    { 9, QuarterNumber.Q3 },
                    { 10, QuarterNumber.Q4 },
                    { 11, QuarterNumber.Q4 },
                    { 12, QuarterNumber.Q4 },
                };

                if (unitOfTimeKind == UnitOfTimeKind.Calendar)
                {
                    var startQuarter = new CalendarQuarter(startAsMonth.Year, monthNumberToQuarterMap[(int)startAsMonth.MonthNumber]);

                    var endQuarter = new CalendarQuarter(endAsMonth.Year, monthNumberToQuarterMap[(int)endAsMonth.MonthNumber]);

                    lessGranularReportingPeriod = new ReportingPeriod(startQuarter, endQuarter);
                }
                else if (unitOfTimeKind == UnitOfTimeKind.Fiscal)
                {
                    var startQuarter = new FiscalQuarter(startAsMonth.Year, monthNumberToQuarterMap[(int)startAsMonth.MonthNumber]);

                    var endQuarter = new FiscalQuarter(endAsMonth.Year, monthNumberToQuarterMap[(int)endAsMonth.MonthNumber]);

                    lessGranularReportingPeriod = new ReportingPeriod(startQuarter, endQuarter);
                }
                else if (unitOfTimeKind == UnitOfTimeKind.Generic)
                {
                    var startQuarter = new GenericQuarter(startAsMonth.Year, monthNumberToQuarterMap[(int)startAsMonth.MonthNumber]);

                    var endQuarter = new GenericQuarter(endAsMonth.Year, monthNumberToQuarterMap[(int)endAsMonth.MonthNumber]);

                    lessGranularReportingPeriod = new ReportingPeriod(startQuarter, endQuarter);
                }
                else
                {
                    throw new NotSupportedException("This kind of unit-of-time is not supported: " + unitOfTimeKind);
                }
            }
            else if (reportingPeriodGranularity == UnitOfTimeGranularity.Quarter)
            {
                var startAsQuarter = reportingPeriod.Start as IHaveAQuarter;
                var endAsQuarter = reportingPeriod.End as IHaveAQuarter;

                // ReSharper disable once PossibleNullReferenceException
                if ((startAsQuarter.QuarterNumber != QuarterNumber.Q1) && returnNullOnMisalignment)
                {
                    return null;
                }

                // ReSharper disable once PossibleNullReferenceException
                if ((endAsQuarter.QuarterNumber != QuarterNumber.Q4) &&  returnNullOnMisalignment)
                {
                    return null;
                }

                if (unitOfTimeKind == UnitOfTimeKind.Calendar)
                {
                    var startYear = new CalendarYear(startAsQuarter.Year);

                    var endYear = new CalendarYear(endAsQuarter.Year);

                    lessGranularReportingPeriod = new ReportingPeriod(startYear, endYear);
                }
                else if (unitOfTimeKind == UnitOfTimeKind.Fiscal)
                {
                    var startYear = new FiscalYear(startAsQuarter.Year);

                    var endYear = new FiscalYear(endAsQuarter.Year);

                    lessGranularReportingPeriod = new ReportingPeriod(startYear, endYear);
                }
                else if (unitOfTimeKind == UnitOfTimeKind.Generic)
                {
                    var startYear = new GenericYear(startAsQuarter.Year);

                    var endYear = new GenericYear(endAsQuarter.Year);

                    lessGranularReportingPeriod = new ReportingPeriod(startYear, endYear);
                }
                else
                {
                    throw new NotSupportedException("This kind of unit-of-time is not supported: " + unitOfTimeKind);
                }
            }
            else
            {
                throw new NotSupportedException("This granularity is not supported: " + reportingPeriodGranularity);
            }

            if (lessGranularReportingPeriod.GetUnitOfTimeGranularity() == granularity)
            {
                return lessGranularReportingPeriod;
            }

            var result = MakeLessGranular(lessGranularReportingPeriod, granularity, returnNullOnMisalignment);

            return result;
        }
    }
}
