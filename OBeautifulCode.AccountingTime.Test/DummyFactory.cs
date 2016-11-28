// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DummyFactory.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Test
{
    using System;

    using AutoFakeItEasy;

    using FakeItEasy;

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

            AutoFixtureBackedDummyFactory.UseRandomConcreteSubclassForDummy<UnitOfTime>();

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

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var start = A.Dummy<UnitOfTime>();
                    var end = A.Dummy<UnitOfTime>().ThatIs(u => u.GetType() == start.GetType());
                    if ((dynamic)start <= (dynamic)end)
                    {
                        return new ReportingPeriodInclusive<UnitOfTime>(start, end);
                    }

                    return new ReportingPeriodInclusive<UnitOfTime>(end, start);
                });

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

        private static void AddDummyCreatorForReportingPeriodInclusive<T>()
            where T : UnitOfTime
        {
            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var start = A.Dummy<T>();
                    var end = A.Dummy<T>();
                    if ((dynamic)start <= (dynamic)end)
                    {
                        return new ReportingPeriodInclusive<T>(start, end);
                    }

                    return new ReportingPeriodInclusive<T>(end, start);
                });
        }
    }
}
