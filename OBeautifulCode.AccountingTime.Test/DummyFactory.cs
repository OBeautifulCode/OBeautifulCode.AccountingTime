// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DummyFactory.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Test
{
    using System;
    using System.Linq;

    using AutoFakeItEasy;

    using FakeItEasy;

    using Math;

    public class DummyFactory : IDummyFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DummyFactory"/> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "This is not excessively complex.  Dummy factories typically wire-up many types.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "This is not excessively complex.  Dummy factories typically wire-up many types.")]
        public DummyFactory()
        {
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(DayOfMonth.Invalid);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(MonthNumber.Invalid);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(MonthOfYear.Invalid);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(QuarterNumber.Invalid);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(UnitOfTimeKind.Invalid);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(UnitOfTimeGranularity.Invalid);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(ReportingPeriodComponent.Invalid);

            AutoFixtureBackedDummyFactory.UseRandomConcreteSubclassForDummy<UnitOfTime>();
            AutoFixtureBackedDummyFactory.UseRandomConcreteSubclassForDummy<CalendarUnitOfTime>();
            AutoFixtureBackedDummyFactory.UseRandomConcreteSubclassForDummy<FiscalUnitOfTime>();
            AutoFixtureBackedDummyFactory.UseRandomConcreteSubclassForDummy<GenericUnitOfTime>();

            AutoFixtureBackedDummyFactory.UseRandomInterfaceImplementationForDummy<IHaveAMonth>();
            AutoFixtureBackedDummyFactory.UseRandomInterfaceImplementationForDummy<IHaveAQuarter>();
            AutoFixtureBackedDummyFactory.UseRandomInterfaceImplementationForDummy<IHaveAYear>();

            // note: this customization is required because AutoFixture doesn't use A.Dummy<>
            // and thus will, from time-to-time, try to create this type with MonthOfYear.Invalid
            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var result = new FiscalYearAccountingPeriodSystem(A.Dummy<MonthOfYear>());
                    return result;
                });

            // note: this customization is required because AutoFixture doesn't use A.Dummy<>
            // and thus will, from time-to-time, try to create this type with MonthOfYear.Invalid
            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var result = new FiftyTwoFiftyThreeWeekAccountingPeriodSystem(A.Dummy<DayOfWeek>(), A.Dummy<MonthOfYear>(), A.Dummy<FiftyTwoFiftyThreeWeekMethodology>());
                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var date = A.Dummy<DateTime>();
                    var result = new GenericMonth(date.Year, A.Dummy<MonthNumber>());
                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var date = A.Dummy<DateTime>();
                    var result = new GenericQuarter(date.Year, A.Dummy<QuarterNumber>());
                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var date = A.Dummy<DateTime>();
                    var result = new GenericYear(date.Year);
                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var date = A.Dummy<DateTime>();
                    var result = new FiscalMonth(date.Year, A.Dummy<MonthNumber>());
                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var date = A.Dummy<DateTime>();
                    var result = new FiscalQuarter(date.Year, A.Dummy<QuarterNumber>());
                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var date = A.Dummy<DateTime>();
                    var result = new FiscalYear(date.Year);
                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var date = A.Dummy<DateTime>();
                    var result = new CalendarDay(date.Year, (MonthOfYear)date.Month, (DayOfMonth)date.Day);
                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var date = A.Dummy<DateTime>();
                    var result = new CalendarMonth(date.Year, A.Dummy<MonthOfYear>());
                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var date = A.Dummy<DateTime>();
                    var result = new CalendarQuarter(date.Year, A.Dummy<QuarterNumber>());
                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var date = A.Dummy<DateTime>();
                    var result = new CalendarYear(date.Year);
                    return result;
                });

            AddDummyCreatorForReportingPeriod<UnitOfTime>();
            AddDummyCreatorForReportingPeriod<CalendarUnitOfTime>();
            AddDummyCreatorForReportingPeriod<FiscalUnitOfTime>();
            AddDummyCreatorForReportingPeriod<GenericUnitOfTime>();
            AddDummyCreatorForReportingPeriod<CalendarDay>();
            AddDummyCreatorForReportingPeriod<CalendarQuarter>();
            AddDummyCreatorForReportingPeriod<CalendarMonth>();
            AddDummyCreatorForReportingPeriod<CalendarYear>();
            AddDummyCreatorForReportingPeriod<FiscalMonth>();
            AddDummyCreatorForReportingPeriod<FiscalQuarter>();
            AddDummyCreatorForReportingPeriod<FiscalYear>();
            AddDummyCreatorForReportingPeriod<GenericMonth>();
            AddDummyCreatorForReportingPeriod<GenericQuarter>();
            AddDummyCreatorForReportingPeriod<GenericYear>();

            AddDummyCreatorForReportingPeriodInclusive<UnitOfTime>();
            AddDummyCreatorForReportingPeriodInclusive<CalendarUnitOfTime>();
            AddDummyCreatorForReportingPeriodInclusive<FiscalUnitOfTime>();
            AddDummyCreatorForReportingPeriodInclusive<GenericUnitOfTime>();
            AddDummyCreatorForReportingPeriodInclusive<CalendarDay>();
            AddDummyCreatorForReportingPeriodInclusive<CalendarQuarter>();
            AddDummyCreatorForReportingPeriodInclusive<CalendarMonth>();
            AddDummyCreatorForReportingPeriodInclusive<CalendarYear>();
            AddDummyCreatorForReportingPeriodInclusive<FiscalMonth>();
            AddDummyCreatorForReportingPeriodInclusive<FiscalQuarter>();
            AddDummyCreatorForReportingPeriodInclusive<FiscalYear>();
            AddDummyCreatorForReportingPeriodInclusive<GenericMonth>();
            AddDummyCreatorForReportingPeriodInclusive<GenericQuarter>();
            AddDummyCreatorForReportingPeriodInclusive<GenericYear>();
        }

        /// <inheritdoc />
        public Priority Priority => new FakeItEasy.Priority(1);

        /// <inheritdoc />
        public bool CanCreate(Type type)
        {
            return false;
        }

        /// <inheritdoc />
        public object Create(Type type)
        {
            return null;
        }

        private static void AddDummyCreatorForReportingPeriod<T>()
           where T : UnitOfTime
        {
            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var reportingPeriodType = typeof(ReportingPeriod<T>);
                    var reportingPeriodConcreteTypes =
                        reportingPeriodType.Assembly.GetTypes()
                            .Where(type => type.IsGenericType)
                            .Select(type => type.MakeGenericType(typeof(T)))
                            .Where(type => type.IsSubclassOf(reportingPeriodType))
                            .ToList();

                    var typeToCreate = reportingPeriodConcreteTypes[ThreadSafeRandom.Next(0, reportingPeriodConcreteTypes.Count)];
                    var result = AD.ummy(typeToCreate);
                    return result as ReportingPeriod<T>;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator<IReportingPeriod<T>>(
                () =>
                {
                    var result = A.Dummy<ReportingPeriod<T>>();
                    return result;
                });
        }

        private static void AddDummyCreatorForReportingPeriodInclusive<T>()
            where T : UnitOfTime
        {
            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var start = A.Dummy<T>();
                    var end = A.Dummy<T>().ThatIs(u => u.GetType() == start.GetType());
                    if ((dynamic)start <= (dynamic)end)
                    {
                        return new ReportingPeriodInclusive<T>(start, end);
                    }

                    return new ReportingPeriodInclusive<T>(end, start);
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator<IReportingPeriodInclusive<T>>(
                () =>
                {
                    var result = A.Dummy<ReportingPeriodInclusive<T>>();
                    return result;
                });
        }
    }
}
