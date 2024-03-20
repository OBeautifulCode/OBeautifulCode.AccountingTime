﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountingTimeDummyFactory.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in OBeautifulCode.AccountingTime.Test source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Test
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;

    using AutoFakeItEasy;

    using FakeItEasy;

    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Math.Recipes;
    using OBeautifulCode.Type;

    using static global::System.FormattableString;

    /// <summary>
    /// A Dummy Factory for types in <see cref="AccountingTime"/>.
    /// </summary>
#if !OBeautifulCodeAccountingTimeSolution
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [global::System.CodeDom.Compiler.GeneratedCode("OBeautifulCode.AccountingTime.Test", "See package version number")]
    internal
#else
    public
#endif
    class AccountingTimeDummyFactory : DefaultAccountingTimeDummyFactory
    {
        private const int MinYear = 1950;

        private const int MaxYear = 2050;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountingTimeDummyFactory"/> class.
        /// </summary>
        public AccountingTimeDummyFactory()
        {
            // ------------------------------------------------------------------------------------
            // ---------------------------  accounting period system ------------------------------
            // ------------------------------------------------------------------------------------
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(FiftyTwoFiftyThreeWeekMethodology.Unknown);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(ReportingPeriodBoundsConstraint.Unknown);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(ReportingPeriodSpanConstraint.Unknown);

            AutoFixtureBackedDummyFactory.UseRandomConcreteSubclassForDummy<AccountingPeriodSystem>();

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var result = new FiscalYearAccountingPeriodSystem(A.Dummy<MonthOfYear>().ThatIsNot(MonthOfYear.December));

                    return result;
                });

            // ------------------------------------------------------------------------------------
            // -------------------------------------  time ----------------------------------------
            // ------------------------------------------------------------------------------------
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(DayOfMonth.Invalid);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(MonthNumber.Invalid);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(MonthOfYear.Invalid);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(QuarterNumber.Invalid);

            AutoFixtureBackedDummyFactory.UseRandomConcreteSubclassForDummy<UnitOfTime>();
            AutoFixtureBackedDummyFactory.UseRandomConcreteSubclassForDummy<CalendarUnitOfTime>();
            AutoFixtureBackedDummyFactory.UseRandomConcreteSubclassForDummy<FiscalUnitOfTime>();
            AutoFixtureBackedDummyFactory.UseRandomConcreteSubclassForDummy<GenericUnitOfTime>();

            AutoFixtureBackedDummyFactory.UseRandomInterfaceImplementationForDummy<IHaveAMonth>();
            AutoFixtureBackedDummyFactory.UseRandomInterfaceImplementationForDummy<IHaveAQuarter>();
            AutoFixtureBackedDummyFactory.UseRandomInterfaceImplementationForDummy<IHaveAYear>();
            AutoFixtureBackedDummyFactory.UseRandomInterfaceImplementationForDummy<IAmBoundedTime>();
            AutoFixtureBackedDummyFactory.UseRandomInterfaceImplementationForDummy<IAmUnboundedTime>();

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var year = ThreadSafeRandom.Next(MinYear, MaxYear + 1);

                    var result = new GenericMonth(year, A.Dummy<MonthNumber>());

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var year = ThreadSafeRandom.Next(MinYear, MaxYear + 1);

                    var result = new GenericQuarter(year, A.Dummy<QuarterNumber>());

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var year = ThreadSafeRandom.Next(MinYear, MaxYear + 1);

                    var result = new GenericYear(year);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var year = ThreadSafeRandom.Next(MinYear, MaxYear + 1);

                    var result = new FiscalMonth(year, A.Dummy<MonthNumber>());

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var year = ThreadSafeRandom.Next(MinYear, MaxYear + 1);

                    var result = new FiscalQuarter(year, A.Dummy<QuarterNumber>());

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var year = ThreadSafeRandom.Next(MinYear, MaxYear + 1);

                    var result = new FiscalYear(year);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    while (true)
                    {
                        try
                        {
                            var date = A.Dummy<DateTime>();

                            var year = ThreadSafeRandom.Next(MinYear, MaxYear + 1);

                            var result = new CalendarDay(year, (MonthOfYear)date.Month, (DayOfMonth)date.Day);

                            return result;
                        }
                        catch (ArgumentException)
                        {
                        }
                    }
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var year = ThreadSafeRandom.Next(MinYear, MaxYear + 1);

                    var result = new CalendarMonth(year, A.Dummy<MonthOfYear>());

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var year = ThreadSafeRandom.Next(MinYear, MaxYear + 1);

                    var result = new CalendarQuarter(year, A.Dummy<QuarterNumber>());

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var year = ThreadSafeRandom.Next(MinYear, MaxYear + 1);

                    var result = new CalendarYear(year);

                    return result;
                });

            // ------------------------------------------------------------------------------------
            // ------------------------------  reporting period -----------------------------------
            // ------------------------------------------------------------------------------------
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(ReportingPeriodComponent.Invalid);

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var potentialTypes = new[] { typeof(GenericReportingPeriod), typeof(FiscalReportingPeriod), typeof(CalendarReportingPeriod) };

                    var result = GetRandomReportingPeriodWrapper(potentialTypes).ReportingPeriod;

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var scenarioNumber = ThreadSafeRandom.Next(0, 8);

                    ReportingPeriodCriteria result;

                    if (scenarioNumber == 0)
                    {
                        result = new ReportingPeriodCriteria();
                    }
                    else if (scenarioNumber == 1)
                    {
                        result = new ReportingPeriodCriteria(A.Dummy<Unit>());
                    }
                    else if (scenarioNumber == 2)
                    {
                        result = new ReportingPeriodCriteria(boundsConstraint: A.Dummy<ReportingPeriodBoundsConstraint>());
                    }
                    else if (scenarioNumber == 3)
                    {
                        result = new ReportingPeriodCriteria(spanConstraint: A.Dummy<ReportingPeriodSpanConstraint>());
                    }
                    else
                    {
                        while (true)
                        {
                            var unit = A.Dummy<Unit>();
                            var boundsConstraint = A.Dummy<ReportingPeriodBoundsConstraint>();
                            var spanConstraint = A.Dummy<ReportingPeriodSpanConstraint>();

                            try
                            {
                                result = new ReportingPeriodCriteria(unit, boundsConstraint, spanConstraint);
                                break;
                            }
                            catch (ArgumentException)
                            {
                            }
                        }
                    }

                    return result;
                });

            // ------------------------------------------------------------------------------------
            // -------------------------------------  unit ----------------------------------------
            // ------------------------------------------------------------------------------------
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(UnitOfTimeKind.Invalid);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(UnitOfTimeGranularity.Invalid);

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var unitOfTime = A.Dummy<UnitOfTime>();

                    var result = new Unit(unitOfTime.UnitOfTimeKind, unitOfTime.UnitOfTimeGranularity);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var unit = A.Dummy<Unit>().Whose(_ => _.Granularity != UnitOfTimeGranularity.Unbounded);

                    var result = new Duration(A.Dummy<PositiveInteger>(), unit);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var reportingPeriod1 = A.Dummy<ReportingPeriod>()
                        .Whose(_ => !_.HasComponentWithUnboundedGranularity());

                    var reportingPeriod2 = A.Dummy<ReportingPeriod>()
                        .Whose(_ => (!_.HasComponentWithUnboundedGranularity()) && (_.GetUnitOfTimeKind() != reportingPeriod1.GetUnitOfTimeKind()));

                    var result = new UnitKindAssociation(reportingPeriod1, reportingPeriod2, A.Dummy<string>());

                    return result;
                });

            // ------------------------------------------------------------------------------------
            // --------------------------  reporting period wrappers ------------------------------
            // ------------------------------------------------------------------------------------
            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var start = A.Dummy<GenericMonth>();
                    
                    var end = A.Dummy<GenericMonth>();

                    var result = end >= start
                        ? new GenericMonthReportingPeriod(start, end)
                        : new GenericMonthReportingPeriod(end, start);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var start = A.Dummy<GenericQuarter>();

                    var end = A.Dummy<GenericQuarter>();

                    var result = end >= start
                        ? new GenericQuarterReportingPeriod(start, end)
                        : new GenericQuarterReportingPeriod(end, start);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var start = A.Dummy<GenericYear>();

                    var end = A.Dummy<GenericYear>();

                    var result = end >= start
                        ? new GenericYearReportingPeriod(start, end)
                        : new GenericYearReportingPeriod(end, start);

                    return result;
                });
            
            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var start = A.Dummy<FiscalMonth>();

                    var end = A.Dummy<FiscalMonth>();

                    var result = end >= start
                        ? new FiscalMonthReportingPeriod(start, end)
                        : new FiscalMonthReportingPeriod(end, start);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var start = A.Dummy<FiscalQuarter>();

                    var end = A.Dummy<FiscalQuarter>();

                    var result = end >= start
                        ? new FiscalQuarterReportingPeriod(start, end)
                        : new FiscalQuarterReportingPeriod(end, start);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var start = A.Dummy<FiscalYear>();

                    var end = A.Dummy<FiscalYear>();

                    var result = end >= start
                        ? new FiscalYearReportingPeriod(start, end)
                        : new FiscalYearReportingPeriod(end, start);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var start = A.Dummy<CalendarDay>();

                    var end = A.Dummy<CalendarDay>();

                    var result = end >= start
                        ? new CalendarDayReportingPeriod(start, end)
                        : new CalendarDayReportingPeriod(end, start);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var start = A.Dummy<CalendarMonth>();

                    var end = A.Dummy<CalendarMonth>();

                    var result = end >= start
                        ? new CalendarMonthReportingPeriod(start, end)
                        : new CalendarMonthReportingPeriod(end, start);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var start = A.Dummy<CalendarQuarter>();

                    var end = A.Dummy<CalendarQuarter>();

                    var result = end >= start
                        ? new CalendarQuarterReportingPeriod(start, end)
                        : new CalendarQuarterReportingPeriod(end, start);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var start = A.Dummy<CalendarYear>();

                    var end = A.Dummy<CalendarYear>();

                    var result = end >= start
                        ? new CalendarYearReportingPeriod(start, end)
                        : new CalendarYearReportingPeriod(end, start);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var potentialTypes = new[] { typeof(BoundedGenericReportingPeriod), typeof(BoundedFiscalReportingPeriod), typeof(BoundedCalendarReportingPeriod) };

                    var reportingPeriod = GetRandomReportingPeriodWrapper(potentialTypes).ReportingPeriod;

                    var result = new BoundedReportingPeriod(reportingPeriod.Start, reportingPeriod.End);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var potentialTypes = new[] { typeof(GenericMonthReportingPeriod), typeof(GenericQuarterReportingPeriod), typeof(GenericYearReportingPeriod) };

                    var reportingPeriod = GetRandomReportingPeriodWrapper(potentialTypes).ReportingPeriod;

                    var result = new BoundedGenericReportingPeriod((GenericUnitOfTime)reportingPeriod.Start, (GenericUnitOfTime)reportingPeriod.End);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var potentialTypes = new[] { typeof(FiscalMonthReportingPeriod), typeof(FiscalQuarterReportingPeriod), typeof(FiscalYearReportingPeriod) };

                    var reportingPeriod = GetRandomReportingPeriodWrapper(potentialTypes).ReportingPeriod;

                    var result = new BoundedFiscalReportingPeriod((FiscalUnitOfTime)reportingPeriod.Start, (FiscalUnitOfTime)reportingPeriod.End);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var potentialTypes = new[] { typeof(CalendarDayReportingPeriod), typeof(CalendarMonthReportingPeriod), typeof(CalendarQuarterReportingPeriod), typeof(CalendarYearReportingPeriod) };

                    var reportingPeriod = GetRandomReportingPeriodWrapper(potentialTypes).ReportingPeriod;

                    var result = new BoundedCalendarReportingPeriod((CalendarUnitOfTime)reportingPeriod.Start, (CalendarUnitOfTime)reportingPeriod.End);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var boundedReportingPeriod = A.Dummy<BoundedGenericReportingPeriod>().ReportingPeriod;

                    var result = ThreadSafeRandom.Next(0, 2) == 0
                        ? new SemiBoundedGenericReportingPeriod((GenericUnitOfTime)boundedReportingPeriod.Start, new GenericUnbounded())
                        : new SemiBoundedGenericReportingPeriod(new GenericUnbounded(), (GenericUnitOfTime)boundedReportingPeriod.End);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var boundedReportingPeriod = A.Dummy<BoundedFiscalReportingPeriod>().ReportingPeriod;

                    var result = ThreadSafeRandom.Next(0, 2) == 0
                        ? new SemiBoundedFiscalReportingPeriod((FiscalUnitOfTime)boundedReportingPeriod.Start, new FiscalUnbounded())
                        : new SemiBoundedFiscalReportingPeriod(new FiscalUnbounded(), (FiscalUnitOfTime)boundedReportingPeriod.End);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var boundedReportingPeriod = A.Dummy<BoundedCalendarReportingPeriod>().ReportingPeriod;

                    var result = ThreadSafeRandom.Next(0, 2) == 0
                        ? new SemiBoundedCalendarReportingPeriod((CalendarUnitOfTime)boundedReportingPeriod.Start, new CalendarUnbounded())
                        : new SemiBoundedCalendarReportingPeriod(new CalendarUnbounded(), (CalendarUnitOfTime)boundedReportingPeriod.End);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var potentialTypes = new[] { typeof(GenericUnboundedReportingPeriod), typeof(SemiBoundedGenericReportingPeriod), typeof(BoundedGenericReportingPeriod) };

                    var reportingPeriod = GetRandomReportingPeriodWrapper(potentialTypes).ReportingPeriod;

                    var result = new GenericReportingPeriod((GenericUnitOfTime)reportingPeriod.Start, (GenericUnitOfTime)reportingPeriod.End);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var potentialTypes = new[] { typeof(FiscalUnboundedReportingPeriod), typeof(SemiBoundedFiscalReportingPeriod), typeof(BoundedFiscalReportingPeriod) };

                    var reportingPeriod = GetRandomReportingPeriodWrapper(potentialTypes).ReportingPeriod;

                    var result = new FiscalReportingPeriod((FiscalUnitOfTime)reportingPeriod.Start, (FiscalUnitOfTime)reportingPeriod.End);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var potentialTypes = new[] { typeof(CalendarUnboundedReportingPeriod), typeof(SemiBoundedCalendarReportingPeriod), typeof(BoundedCalendarReportingPeriod) };

                    var reportingPeriod = GetRandomReportingPeriodWrapper(potentialTypes).ReportingPeriod;

                    var result = new CalendarReportingPeriod((CalendarUnitOfTime)reportingPeriod.Start, (CalendarUnitOfTime)reportingPeriod.End);

                    return result;
                });

            // ------------------------------------------------------------------------------------
            // ------------------------------------  cutoff ---------------------------------------
            // ------------------------------------------------------------------------------------
            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var result = new RelativeCutoff(A.Dummy<Duration>(), A.Dummy<ReportingPeriodComponent>().ThatIsIn(new[] { ReportingPeriodComponent.Start, ReportingPeriodComponent.End }));

                    return result;
                });

            // ------------------------------------------------------------------------------------
            // ----------------------------------  timeseries -------------------------------------
            // ------------------------------------------------------------------------------------
            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var result = (IDatapoint)A.Dummy<Datapoint<Version>>();

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var result = (ITimeseries)A.Dummy<Timeseries<Version>>();

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var result = GetDummyTimeseries<Version>();

                    return result;
                });
        }

        /// <summary>
        /// Gets a dummy <see cref="Timeseries{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the value of the datapoints.</typeparam>
        /// <returns>
        /// The dummy timeseries.
        /// </returns>
        public static Timeseries<T> GetDummyTimeseries<T>()
        {
            // By constraining the year range, we are able to find adjacent
            // reporting periods later in the heuristic.
            var oneFourthYearRange = (MaxYear - MinYear) / 4;
            var minYear = MinYear + oneFourthYearRange;
            var maxYear = oneFourthYearRange - (MaxYear - MinYear) / 4;

            var reportingPeriod = A.Dummy<ReportingPeriod>().Whose(
                _ =>
                ((_.Start.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded) || (((IHaveAYear)_.Start).Year > minYear)) &&
                ((_.End.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded) || (((IHaveAYear)_.End).Year < maxYear)));

            IReadOnlyList<Datapoint<T>> datapoints = new List<Datapoint<T>>();

            // 1/4rd of time it will be an empty timeseries
            if (ThreadSafeRandom.Next(0, 4) > 0)
            {
                if (reportingPeriod.HasComponentWithUnboundedGranularity())
                {
                    datapoints = new List<Datapoint<T>>
                    {
                        new Datapoint<T>(reportingPeriod, A.Dummy<T>()),
                    };

                    // If completely unbounded, then there can only be one datapoint
                    if ((reportingPeriod.Start.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded) ||
                        (reportingPeriod.End.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded))
                    {
                        // Partially bound.
                        // 1/2 of the time use just the reporting period itself, otherwise tack on reporting periods to the bounded end
                        if (ThreadSafeRandom.Next(0, 2) == 0)
                        {
                            // One unbounded component, tack on reporting periods to the bounded end.
                            var timeComparison = reportingPeriod.Start.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded
                                ? TimeComparison.After
                                : TimeComparison.Before;

                            datapoints = AddDatapointsWithGapLikely(datapoints, reportingPeriod, timeComparison);
                        }
                    }
                }
                else
                {
                    // Completely bounded; get the units within
                    datapoints = reportingPeriod
                        .GetUnitsOfTimeWithin()
                        .Select(_ => _.ToReportingPeriod())
                        .Select(_ => new Datapoint<T>(_, A.Dummy<T>()))
                        .ToList();

                    // 1/2 chance of having a hole
                    if (ThreadSafeRandom.Next(0, 2) == 0)
                    {
                        var timeComparison = ThreadSafeRandom.Next(0, 1) == 0
                            ? TimeComparison.After
                            : TimeComparison.Before;

                        datapoints = AddDatapointsWithGapLikely(datapoints, reportingPeriod, timeComparison);
                    }
                }
            }

            var result = new Timeseries<T>(datapoints);

            return result;
        }

        private static ReportingPeriodWrapper GetRandomReportingPeriodWrapper(
            IReadOnlyCollection<Type> potentialTypes)
        {
            new { potentialTypes }.AsArg().Must().NotBeNullNorEmptyEnumerableNorContainAnyNulls();

            var randomIndex = ThreadSafeRandom.Next(0, potentialTypes.Count);

            var typeToCreate = potentialTypes.ElementAt(randomIndex);

            var result = (ReportingPeriodWrapper)AD.ummy(typeToCreate);

            return result;
        }

        private static ReportingPeriod GetAdjacentBoundedReportingPeriod(
            ReportingPeriod reportingPeriod,
            TimeComparison timeComparison)
        {
            ReportingPeriod result;

            if (timeComparison == TimeComparison.After)
            {
                result = A.Dummy<ReportingPeriod>()
                    .Whose(_ => (!_.HasComponentWithUnboundedGranularity()) &&
                                (_.GetUnitOfTimeKind() == reportingPeriod.GetUnitOfTimeKind()) &&
                                (_.Start.ToMostGranular().Start > reportingPeriod.End.ToMostGranular().End));
            }
            else if (timeComparison == TimeComparison.Before)
            {
                result = A.Dummy<ReportingPeriod>()
                    .Whose(_ => (!_.HasComponentWithUnboundedGranularity()) &&
                                (_.GetUnitOfTimeKind() == reportingPeriod.GetUnitOfTimeKind()) &&
                                (_.End.ToMostGranular().End < reportingPeriod.Start.ToMostGranular().Start));
            }
            else
            {
                throw new NotSupportedException(Invariant($"This {nameof(TimeComparison)} is not supported: {timeComparison}."));
            }

            return result;
        }

        private static IReadOnlyList<Datapoint<T>> AddDatapointsWithGapLikely<T>(
            IReadOnlyList<Datapoint<T>> datapoints,
            ReportingPeriod reportingPeriod,
            TimeComparison timeComparison)
        {
            var result = new List<Datapoint<T>>();

            var adjacentReportingPeriod = GetAdjacentBoundedReportingPeriod(reportingPeriod, timeComparison);

            // Most likely will result in a gap in the timeseries and also possibly datapoints in different granularity.
            var adjacentDatapoints = adjacentReportingPeriod
                .GetUnitsOfTimeWithin()
                .Select(_ => _.ToReportingPeriod())
                .Select(_ => new Datapoint<T>(_, A.Dummy<T>()))
                .ToList();

            if (timeComparison == TimeComparison.After)
            {
                result.AddRange(datapoints);

                result.AddRange(adjacentDatapoints);
            }
            else
            {
                result.AddRange(adjacentDatapoints);

                result.AddRange(datapoints);
            }

            return result;
        }
    }
}
