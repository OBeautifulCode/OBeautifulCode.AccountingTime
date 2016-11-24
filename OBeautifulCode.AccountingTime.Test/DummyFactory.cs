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
        public DummyFactory()
        {
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
                    var result = new FiscalMonth(date.Year, A.Dummy<MonthNumber>());
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

            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(DayOfMonth.Invalid);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(MonthNumber.Invalid);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(MonthOfYear.Invalid);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(QuarterNumber.Invalid);
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
