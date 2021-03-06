﻿// --------------------------------------------------------------------------------------------------------------------
// <auto-generated>
//   Generated using OBeautifulCode.CodeGen.ModelObject (1.0.153.0)
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Test
{
    using global::System;
    using global::System.CodeDom.Compiler;
    using global::System.Collections.Concurrent;
    using global::System.Collections.Generic;
    using global::System.Collections.ObjectModel;
    using global::System.Diagnostics.CodeAnalysis;

    using global::FakeItEasy;

    using global::OBeautifulCode.AccountingTime;
    using global::OBeautifulCode.AutoFakeItEasy;
    using global::OBeautifulCode.Math.Recipes;

    /// <summary>
    /// The default (code generated) Dummy Factory.
    /// Derive from this class to add any overriding or custom registrations.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [GeneratedCode("OBeautifulCode.CodeGen.ModelObject", "1.0.153.0")]
#if !OBeautifulCodeAccountingTimeSolution
    internal
#else
    public
#endif
    abstract class DefaultAccountingTimeDummyFactory : IDummyFactory
    {
        public DefaultAccountingTimeDummyFactory()
        {
            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var availableTypes = new[]
                    {
                        typeof(CalendarYearAccountingPeriodSystem),
                        typeof(FiftyTwoFiftyThreeWeekAccountingPeriodSystem),
                        typeof(FiscalYearAccountingPeriodSystem)
                    };

                    var randomIndex = ThreadSafeRandom.Next(0, availableTypes.Length);

                    var randomType = availableTypes[randomIndex];

                    var result = (AccountingPeriodSystem)AD.ummy(randomType);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new CalendarYearAccountingPeriodSystem());

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new FiftyTwoFiftyThreeWeekAccountingPeriodSystem(
                                 A.Dummy<DayOfWeek>(),
                                 A.Dummy<MonthOfYear>(),
                                 A.Dummy<FiftyTwoFiftyThreeWeekMethodology>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new FiscalYearAccountingPeriodSystem(
                                 A.Dummy<MonthOfYear>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new ReportingPeriod(
                                 A.Dummy<UnitOfTime>(),
                                 A.Dummy<UnitOfTime>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new CalendarDay(
                                 A.Dummy<int>(),
                                 A.Dummy<MonthOfYear>(),
                                 A.Dummy<DayOfMonth>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new CalendarMonth(
                                 A.Dummy<int>(),
                                 A.Dummy<MonthOfYear>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new CalendarQuarter(
                                 A.Dummy<int>(),
                                 A.Dummy<QuarterNumber>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new CalendarUnbounded());

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var availableTypes = new[]
                    {
                        typeof(CalendarDay),
                        typeof(CalendarMonth),
                        typeof(CalendarQuarter),
                        typeof(CalendarUnbounded),
                        typeof(CalendarYear)
                    };

                    var randomIndex = ThreadSafeRandom.Next(0, availableTypes.Length);

                    var randomType = availableTypes[randomIndex];

                    var result = (CalendarUnitOfTime)AD.ummy(randomType);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new CalendarYear(
                                 A.Dummy<int>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new FiscalMonth(
                                 A.Dummy<int>(),
                                 A.Dummy<MonthNumber>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new FiscalQuarter(
                                 A.Dummy<int>(),
                                 A.Dummy<QuarterNumber>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new FiscalUnbounded());

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var availableTypes = new[]
                    {
                        typeof(FiscalMonth),
                        typeof(FiscalQuarter),
                        typeof(FiscalUnbounded),
                        typeof(FiscalYear)
                    };

                    var randomIndex = ThreadSafeRandom.Next(0, availableTypes.Length);

                    var randomType = availableTypes[randomIndex];

                    var result = (FiscalUnitOfTime)AD.ummy(randomType);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new FiscalYear(
                                 A.Dummy<int>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new GenericMonth(
                                 A.Dummy<int>(),
                                 A.Dummy<MonthNumber>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new GenericQuarter(
                                 A.Dummy<int>(),
                                 A.Dummy<QuarterNumber>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new GenericUnbounded());

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var availableTypes = new[]
                    {
                        typeof(GenericMonth),
                        typeof(GenericQuarter),
                        typeof(GenericUnbounded),
                        typeof(GenericYear)
                    };

                    var randomIndex = ThreadSafeRandom.Next(0, availableTypes.Length);

                    var randomType = availableTypes[randomIndex];

                    var result = (GenericUnitOfTime)AD.ummy(randomType);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new GenericYear(
                                 A.Dummy<int>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var availableTypes = new[]
                    {
                        typeof(CalendarDay),
                        typeof(CalendarMonth),
                        typeof(CalendarQuarter),
                        typeof(CalendarUnbounded),
                        typeof(CalendarYear),
                        typeof(FiscalMonth),
                        typeof(FiscalQuarter),
                        typeof(FiscalUnbounded),
                        typeof(FiscalYear),
                        typeof(GenericMonth),
                        typeof(GenericQuarter),
                        typeof(GenericUnbounded),
                        typeof(GenericYear)
                    };

                    var randomIndex = ThreadSafeRandom.Next(0, availableTypes.Length);

                    var randomType = availableTypes[randomIndex];

                    var result = (UnitOfTime)AD.ummy(randomType);

                    return result;
                });
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
    }
}