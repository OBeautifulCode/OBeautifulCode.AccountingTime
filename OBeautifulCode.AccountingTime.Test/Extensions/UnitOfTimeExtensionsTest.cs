// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfTimeExtensionsTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime.Test
{
    using System;
    using System.Collections.Generic;

    using FakeItEasy;

    using FluentAssertions;

    using Xunit;

    public static class UnitOfTimeExtensionsTest
    {
        // ReSharper disable InconsistentNaming
        [Fact]
        public static void ToGenericMonth___Should_throw_ArgumentNullException___When_parameter_month_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => UnitOfTimeExtensions.ToGenericMonth(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void ToGenericMonth___Should_return_IHaveAMonth_converted_to_a_GenericMonth___When_called()
        {
            // Arrange
            var systemUnderTest = A.Dummy<IHaveAMonth>();

            // Act
            var actualQuarter = systemUnderTest.ToGenericMonth();

            // Assert
            actualQuarter.Should().BeOfType<GenericMonth>();
            actualQuarter.MonthNumber.Should().Be(systemUnderTest.MonthNumber);
            actualQuarter.Year.Should().Be(systemUnderTest.Year);
        }

        [Fact]
        public static void ToGenericQuarter___Should_throw_ArgumentNullException___When_parameter_Quarter_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => UnitOfTimeExtensions.ToGenericQuarter(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void ToGenericQuarter___Should_return_IHaveAQuarter_converted_to_a_GenericQuarter___When_called()
        {
            // Arrange
            var systemUnderTest = A.Dummy<IHaveAQuarter>();

            // Act
            var actualQuarter = systemUnderTest.ToGenericQuarter();

            // Assert
            actualQuarter.Should().BeOfType<GenericQuarter>();
            actualQuarter.QuarterNumber.Should().Be(systemUnderTest.QuarterNumber);
            actualQuarter.Year.Should().Be(systemUnderTest.Year);
        }

        [Fact]
        public static void ToGenericYear___Should_throw_ArgumentNullException___When_parameter_Year_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => UnitOfTimeExtensions.ToGenericYear(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void ToGenericYear___Should_return_IHaveAYear_converted_to_a_GenericYear___When_called()
        {
            // Arrange
            var systemUnderTest = A.Dummy<IHaveAYear>();

            // Act
            var actualYear = systemUnderTest.ToGenericYear();

            // Assert
            actualYear.Should().BeOfType<GenericYear>();
            actualYear.Year.Should().Be(systemUnderTest.Year);
        }

        [Fact]
        public static void Plus___Should_throw_ArgumentNullException___When_parameter_unitOfTime_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => UnitOfTimeExtensions.Plus<UnitOfTime>(null, A.Dummy<int>()));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void Plus___Should_return_same_calendar_day___When_parameter_unitOfTime_is_a_CalendarDay_and_parameter_unitsToAdd_is_0()
        {
            // Arrange
            var expectedUnitOfTime = A.Dummy<CalendarDay>();

            // Act
            var actualUnitOfTime = expectedUnitOfTime.Plus(0);

            // Assert
            actualUnitOfTime.Should().Be(expectedUnitOfTime);
        }

        [Fact]
        public static void Plus___Should_add_calendar_days___When_parameter_unitOfTime_is_a_CalendarDay_and_parameter_unitsToAdd_is_positive()
        {
            // Arrange
            var systemUnderTest = new CalendarDay(2017, MonthOfYear.November, DayOfMonth.TwentyEight);

            var expectedUnitOfTime1 = new CalendarDay(2017, MonthOfYear.November, DayOfMonth.TwentyNine);
            var expectedUnitOfTime2 = new CalendarDay(2017, MonthOfYear.November, DayOfMonth.Thirty);
            var expectedUnitOfTime3 = new CalendarDay(2017, MonthOfYear.December, DayOfMonth.One);
            var expectedUnitOfTime4 = new CalendarDay(2017, MonthOfYear.December, DayOfMonth.Two);
            var expectedUnitOfTime5 = new CalendarDay(2017, MonthOfYear.December, DayOfMonth.ThirtyOne);
            var expectedUnitOfTime6 = new CalendarDay(2018, MonthOfYear.January, DayOfMonth.One);
            var expectedUnitOfTime7 = new CalendarDay(2018, MonthOfYear.January, DayOfMonth.Two);

            // Act
            var actualUnitOfTime1 = systemUnderTest.Plus(1);
            var actualUnitOfTime2 = systemUnderTest.Plus(2);
            var actualUnitOfTime3 = systemUnderTest.Plus(3);
            var actualUnitOfTime4 = systemUnderTest.Plus(4);
            var actualUnitOfTime5 = systemUnderTest.Plus(33);
            var actualUnitOfTime6 = systemUnderTest.Plus(34);
            var actualUnitOfTime7 = systemUnderTest.Plus(35);

            // Assert
            actualUnitOfTime1.Should().Be(expectedUnitOfTime1);
            actualUnitOfTime2.Should().Be(expectedUnitOfTime2);
            actualUnitOfTime3.Should().Be(expectedUnitOfTime3);
            actualUnitOfTime4.Should().Be(expectedUnitOfTime4);
            actualUnitOfTime5.Should().Be(expectedUnitOfTime5);
            actualUnitOfTime6.Should().Be(expectedUnitOfTime6);
            actualUnitOfTime7.Should().Be(expectedUnitOfTime7);
        }

        [Fact]
        public static void Plus___Should_subtract_calendar_days___When_parameter_unitOfTime_is_a_CalendarDay_and_parameter_unitsToAdd_is_negative()
        {
            // Arrange
            var systemUnderTest = new CalendarDay(2017, MonthOfYear.November, DayOfMonth.Two);

            var expectedUnitOfTime1 = new CalendarDay(2017, MonthOfYear.November, DayOfMonth.One);
            var expectedUnitOfTime2 = new CalendarDay(2017, MonthOfYear.October, DayOfMonth.ThirtyOne);
            var expectedUnitOfTime3 = new CalendarDay(2017, MonthOfYear.October, DayOfMonth.Thirty);
            var expectedUnitOfTime4 = new CalendarDay(2017, MonthOfYear.October, DayOfMonth.One);
            var expectedUnitOfTime5 = new CalendarDay(2017, MonthOfYear.September, DayOfMonth.Thirty);
            var expectedUnitOfTime6 = new CalendarDay(2017, MonthOfYear.September, DayOfMonth.TwentyNine);

            // Act
            var actualUnitOfTime1 = systemUnderTest.Plus(-1);
            var actualUnitOfTime2 = systemUnderTest.Plus(-2);
            var actualUnitOfTime3 = systemUnderTest.Plus(-3);
            var actualUnitOfTime4 = systemUnderTest.Plus(-32);
            var actualUnitOfTime5 = systemUnderTest.Plus(-33);
            var actualUnitOfTime6 = systemUnderTest.Plus(-34);

            // Assert
            actualUnitOfTime1.Should().Be(expectedUnitOfTime1);
            actualUnitOfTime2.Should().Be(expectedUnitOfTime2);
            actualUnitOfTime3.Should().Be(expectedUnitOfTime3);
            actualUnitOfTime4.Should().Be(expectedUnitOfTime4);
            actualUnitOfTime5.Should().Be(expectedUnitOfTime5);
            actualUnitOfTime6.Should().Be(expectedUnitOfTime6);
        }

        [Fact]
        public static void Plus___Should_return_same_calendar_month___When_parameter_unitOfTime_is_a_CalendarMonth_and_parameter_unitsToAdd_is_0()
        {
            // Arrange
            var expectedUnitOfTime = A.Dummy<CalendarMonth>();

            // Act
            var actualUnitOfTime = expectedUnitOfTime.Plus(0);

            // Assert
            actualUnitOfTime.Should().Be(expectedUnitOfTime);
        }

        [Fact]
        public static void Plus___Should_add_calendar_months___When_parameter_unitOfTime_is_a_CalendarMonth_and_parameter_unitsToAdd_is_positive()
        {
            // Arrange
            var systemUnderTest = new CalendarMonth(2016, MonthOfYear.November);

            var expectedUnitOfTime1 = new CalendarMonth(2016, MonthOfYear.December);
            var expectedUnitOfTime2 = new CalendarMonth(2017, MonthOfYear.January);
            var expectedUnitOfTime3 = new CalendarMonth(2017, MonthOfYear.May);
            var expectedUnitOfTime4 = new CalendarMonth(2018, MonthOfYear.February);

            // Act
            var actualUnitOfTime1 = systemUnderTest.Plus(1);
            var actualUnitOfTime2 = systemUnderTest.Plus(2);
            var actualUnitOfTime3 = systemUnderTest.Plus(6);
            var actualUnitOfTime4 = systemUnderTest.Plus(15);

            // Assert
            actualUnitOfTime1.Should().Be(expectedUnitOfTime1);
            actualUnitOfTime2.Should().Be(expectedUnitOfTime2);
            actualUnitOfTime3.Should().Be(expectedUnitOfTime3);
            actualUnitOfTime4.Should().Be(expectedUnitOfTime4);
        }

        [Fact]
        public static void Plus___Should_subtract_calendar_months___When_parameter_unitOfTime_is_a_CalendarMonth_and_parameter_unitsToAdd_is_negative()
        {
            // Arrange
            var systemUnderTest = new CalendarMonth(2016, MonthOfYear.November);

            var expectedUnitOfTime1 = new CalendarMonth(2016, MonthOfYear.October);
            var expectedUnitOfTime2 = new CalendarMonth(2016, MonthOfYear.September);
            var expectedUnitOfTime3 = new CalendarMonth(2015, MonthOfYear.December);
            var expectedUnitOfTime4 = new CalendarMonth(2015, MonthOfYear.August);

            // Act
            var actualUnitOfTime1 = systemUnderTest.Plus(-1);
            var actualUnitOfTime2 = systemUnderTest.Plus(-2);
            var actualUnitOfTime3 = systemUnderTest.Plus(-11);
            var actualUnitOfTime4 = systemUnderTest.Plus(-15);

            // Assert
            actualUnitOfTime1.Should().Be(expectedUnitOfTime1);
            actualUnitOfTime2.Should().Be(expectedUnitOfTime2);
            actualUnitOfTime3.Should().Be(expectedUnitOfTime3);
            actualUnitOfTime4.Should().Be(expectedUnitOfTime4);
        }

        [Fact]
        public static void Plus___Should_return_same_fiscal_month___When_parameter_unitOfTime_is_a_FiscalMonth_and_parameter_unitsToAdd_is_0()
        {
            // Arrange
            var expectedUnitOfTime = A.Dummy<FiscalMonth>();

            // Act
            var actualUnitOfTime = expectedUnitOfTime.Plus(0);

            // Assert
            actualUnitOfTime.Should().Be(expectedUnitOfTime);
        }

        [Fact]
        public static void Plus___Should_add_fiscal_months___When_parameter_unitOfTime_is_a_FiscalMonth_and_parameter_unitsToAdd_is_positive()
        {
            // Arrange
            var systemUnderTest = new FiscalMonth(2016, MonthNumber.Eleven);

            var expectedUnitOfTime1 = new FiscalMonth(2016, MonthNumber.Twelve);
            var expectedUnitOfTime2 = new FiscalMonth(2017, MonthNumber.One);
            var expectedUnitOfTime3 = new FiscalMonth(2017, MonthNumber.Five);
            var expectedUnitOfTime4 = new FiscalMonth(2018, MonthNumber.Two);

            // Act
            var actualUnitOfTime1 = systemUnderTest.Plus(1);
            var actualUnitOfTime2 = systemUnderTest.Plus(2);
            var actualUnitOfTime3 = systemUnderTest.Plus(6);
            var actualUnitOfTime4 = systemUnderTest.Plus(15);

            // Assert
            actualUnitOfTime1.Should().Be(expectedUnitOfTime1);
            actualUnitOfTime2.Should().Be(expectedUnitOfTime2);
            actualUnitOfTime3.Should().Be(expectedUnitOfTime3);
            actualUnitOfTime4.Should().Be(expectedUnitOfTime4);
        }

        [Fact]
        public static void Plus___Should_subtract_fiscal_months___When_parameter_unitOfTime_is_a_FiscalMonth_and_parameter_unitsToAdd_is_negative()
        {
            // Arrange
            var systemUnderTest = new FiscalMonth(2016, MonthNumber.Eleven);

            var expectedUnitOfTime1 = new FiscalMonth(2016, MonthNumber.Ten);
            var expectedUnitOfTime2 = new FiscalMonth(2016, MonthNumber.Nine);
            var expectedUnitOfTime3 = new FiscalMonth(2015, MonthNumber.Twelve);
            var expectedUnitOfTime4 = new FiscalMonth(2015, MonthNumber.Eight);

            // Act
            var actualUnitOfTime1 = systemUnderTest.Plus(-1);
            var actualUnitOfTime2 = systemUnderTest.Plus(-2);
            var actualUnitOfTime3 = systemUnderTest.Plus(-11);
            var actualUnitOfTime4 = systemUnderTest.Plus(-15);

            // Assert
            actualUnitOfTime1.Should().Be(expectedUnitOfTime1);
            actualUnitOfTime2.Should().Be(expectedUnitOfTime2);
            actualUnitOfTime3.Should().Be(expectedUnitOfTime3);
            actualUnitOfTime4.Should().Be(expectedUnitOfTime4);
        }

        [Fact]
        public static void Plus___Should_return_same_generic_month___When_parameter_unitOfTime_is_a_GenericMonth_and_parameter_unitsToAdd_is_0()
        {
            // Arrange
            var expectedUnitOfTime = A.Dummy<GenericMonth>();

            // Act
            var actualUnitOfTime = expectedUnitOfTime.Plus(0);

            // Assert
            actualUnitOfTime.Should().Be(expectedUnitOfTime);
        }

        [Fact]
        public static void Plus___Should_add_generic_months___When_parameter_unitOfTime_is_a_GenericMonth_and_parameter_unitsToAdd_is_positive()
        {
            // Arrange
            var systemUnderTest = new GenericMonth(2016, MonthNumber.Eleven);

            var expectedUnitOfTime1 = new GenericMonth(2016, MonthNumber.Twelve);
            var expectedUnitOfTime2 = new GenericMonth(2017, MonthNumber.One);
            var expectedUnitOfTime3 = new GenericMonth(2017, MonthNumber.Five);
            var expectedUnitOfTime4 = new GenericMonth(2018, MonthNumber.Two);

            // Act
            var actualUnitOfTime1 = systemUnderTest.Plus(1);
            var actualUnitOfTime2 = systemUnderTest.Plus(2);
            var actualUnitOfTime3 = systemUnderTest.Plus(6);
            var actualUnitOfTime4 = systemUnderTest.Plus(15);

            // Assert
            actualUnitOfTime1.Should().Be(expectedUnitOfTime1);
            actualUnitOfTime2.Should().Be(expectedUnitOfTime2);
            actualUnitOfTime3.Should().Be(expectedUnitOfTime3);
            actualUnitOfTime4.Should().Be(expectedUnitOfTime4);
        }

        [Fact]
        public static void Plus___Should_subtract_generic_months___When_parameter_unitOfTime_is_a_GenericMonth_and_parameter_unitsToAdd_is_negative()
        {
            // Arrange
            var systemUnderTest = new GenericMonth(2016, MonthNumber.Eleven);

            var expectedUnitOfTime1 = new GenericMonth(2016, MonthNumber.Ten);
            var expectedUnitOfTime2 = new GenericMonth(2016, MonthNumber.Nine);
            var expectedUnitOfTime3 = new GenericMonth(2015, MonthNumber.Twelve);
            var expectedUnitOfTime4 = new GenericMonth(2015, MonthNumber.Eight);

            // Act
            var actualUnitOfTime1 = systemUnderTest.Plus(-1);
            var actualUnitOfTime2 = systemUnderTest.Plus(-2);
            var actualUnitOfTime3 = systemUnderTest.Plus(-11);
            var actualUnitOfTime4 = systemUnderTest.Plus(-15);

            // Assert
            actualUnitOfTime1.Should().Be(expectedUnitOfTime1);
            actualUnitOfTime2.Should().Be(expectedUnitOfTime2);
            actualUnitOfTime3.Should().Be(expectedUnitOfTime3);
            actualUnitOfTime4.Should().Be(expectedUnitOfTime4);
        }

        [Fact]
        public static void Plus___Should_return_same_calendar_quarter___When_parameter_unitOfTime_is_a_CalendarQuarter_and_parameter_unitsToAdd_is_0()
        {
            // Arrange
            var expectedUnitOfTime = A.Dummy<CalendarQuarter>();

            // Act
            var actualUnitOfTime = expectedUnitOfTime.Plus(0);

            // Assert
            actualUnitOfTime.Should().Be(expectedUnitOfTime);
        }

        [Fact]
        public static void Plus___Should_add_calendar_quarters___When_parameter_unitOfTime_is_a_CalendarQuarter_and_parameter_unitsToAdd_is_positive()
        {
            // Arrange
            var systemUnderTest = new CalendarQuarter(2016, QuarterNumber.Second);

            var expectedUnitOfTime1 = new CalendarQuarter(2016, QuarterNumber.Third);
            var expectedUnitOfTime2 = new CalendarQuarter(2016, QuarterNumber.Fourth);
            var expectedUnitOfTime3 = new CalendarQuarter(2017, QuarterNumber.First);
            var expectedUnitOfTime4 = new CalendarQuarter(2017, QuarterNumber.Second);
            var expectedUnitOfTime5 = new CalendarQuarter(2017, QuarterNumber.Third);
            var expectedUnitOfTime6 = new CalendarQuarter(2017, QuarterNumber.Fourth);
            var expectedUnitOfTime7 = new CalendarQuarter(2018, QuarterNumber.First);
            var expectedUnitOfTime8 = new CalendarQuarter(2018, QuarterNumber.Second);

            // Act
            var actualUnitOfTime1 = systemUnderTest.Plus(1);
            var actualUnitOfTime2 = systemUnderTest.Plus(2);
            var actualUnitOfTime3 = systemUnderTest.Plus(3);
            var actualUnitOfTime4 = systemUnderTest.Plus(4);
            var actualUnitOfTime5 = systemUnderTest.Plus(5);
            var actualUnitOfTime6 = systemUnderTest.Plus(6);
            var actualUnitOfTime7 = systemUnderTest.Plus(7);
            var actualUnitOfTime8 = systemUnderTest.Plus(8);

            // Assert
            actualUnitOfTime1.Should().Be(expectedUnitOfTime1);
            actualUnitOfTime2.Should().Be(expectedUnitOfTime2);
            actualUnitOfTime3.Should().Be(expectedUnitOfTime3);
            actualUnitOfTime4.Should().Be(expectedUnitOfTime4);
            actualUnitOfTime5.Should().Be(expectedUnitOfTime5);
            actualUnitOfTime6.Should().Be(expectedUnitOfTime6);
            actualUnitOfTime7.Should().Be(expectedUnitOfTime7);
            actualUnitOfTime8.Should().Be(expectedUnitOfTime8);
        }

        [Fact]
        public static void Plus___Should_subtract_calendar_quarters___When_parameter_unitOfTime_is_a_CalendarQuarter_and_parameter_unitsToAdd_is_negative()
        {
            // Arrange
            var systemUnderTest = new CalendarQuarter(2016, QuarterNumber.Second);

            var expectedUnitOfTime1 = new CalendarQuarter(2016, QuarterNumber.First);
            var expectedUnitOfTime2 = new CalendarQuarter(2015, QuarterNumber.Fourth);
            var expectedUnitOfTime3 = new CalendarQuarter(2015, QuarterNumber.Third);
            var expectedUnitOfTime4 = new CalendarQuarter(2015, QuarterNumber.Second);
            var expectedUnitOfTime5 = new CalendarQuarter(2015, QuarterNumber.First);
            var expectedUnitOfTime6 = new CalendarQuarter(2014, QuarterNumber.Fourth);
            var expectedUnitOfTime7 = new CalendarQuarter(2014, QuarterNumber.Third);
            var expectedUnitOfTime8 = new CalendarQuarter(2014, QuarterNumber.Second);

            // Act
            var actualUnitOfTime1 = systemUnderTest.Plus(-1);
            var actualUnitOfTime2 = systemUnderTest.Plus(-2);
            var actualUnitOfTime3 = systemUnderTest.Plus(-3);
            var actualUnitOfTime4 = systemUnderTest.Plus(-4);
            var actualUnitOfTime5 = systemUnderTest.Plus(-5);
            var actualUnitOfTime6 = systemUnderTest.Plus(-6);
            var actualUnitOfTime7 = systemUnderTest.Plus(-7);
            var actualUnitOfTime8 = systemUnderTest.Plus(-8);

            // Assert
            actualUnitOfTime1.Should().Be(expectedUnitOfTime1);
            actualUnitOfTime2.Should().Be(expectedUnitOfTime2);
            actualUnitOfTime3.Should().Be(expectedUnitOfTime3);
            actualUnitOfTime4.Should().Be(expectedUnitOfTime4);
            actualUnitOfTime5.Should().Be(expectedUnitOfTime5);
            actualUnitOfTime6.Should().Be(expectedUnitOfTime6);
            actualUnitOfTime7.Should().Be(expectedUnitOfTime7);
            actualUnitOfTime8.Should().Be(expectedUnitOfTime8);
        }

        [Fact]
        public static void Plus___Should_return_same_fiscal_quarter___When_parameter_unitOfTime_is_a_FiscalQuarter_and_parameter_unitsToAdd_is_0()
        {
            // Arrange
            var expectedUnitOfTime = A.Dummy<FiscalQuarter>();

            // Act
            var actualUnitOfTime = expectedUnitOfTime.Plus(0);

            // Assert
            actualUnitOfTime.Should().Be(expectedUnitOfTime);
        }

        [Fact]
        public static void Plus___Should_add_fiscal_quarters___When_parameter_unitOfTime_is_a_FiscalQuarter_and_parameter_unitsToAdd_is_positive()
        {
            // Arrange
            var systemUnderTest = new FiscalQuarter(2016, QuarterNumber.Second);

            var expectedUnitOfTime1 = new FiscalQuarter(2016, QuarterNumber.Third);
            var expectedUnitOfTime2 = new FiscalQuarter(2016, QuarterNumber.Fourth);
            var expectedUnitOfTime3 = new FiscalQuarter(2017, QuarterNumber.First);
            var expectedUnitOfTime4 = new FiscalQuarter(2017, QuarterNumber.Second);
            var expectedUnitOfTime5 = new FiscalQuarter(2017, QuarterNumber.Third);
            var expectedUnitOfTime6 = new FiscalQuarter(2017, QuarterNumber.Fourth);
            var expectedUnitOfTime7 = new FiscalQuarter(2018, QuarterNumber.First);
            var expectedUnitOfTime8 = new FiscalQuarter(2018, QuarterNumber.Second);

            // Act
            var actualUnitOfTime1 = systemUnderTest.Plus(1);
            var actualUnitOfTime2 = systemUnderTest.Plus(2);
            var actualUnitOfTime3 = systemUnderTest.Plus(3);
            var actualUnitOfTime4 = systemUnderTest.Plus(4);
            var actualUnitOfTime5 = systemUnderTest.Plus(5);
            var actualUnitOfTime6 = systemUnderTest.Plus(6);
            var actualUnitOfTime7 = systemUnderTest.Plus(7);
            var actualUnitOfTime8 = systemUnderTest.Plus(8);

            // Assert
            actualUnitOfTime1.Should().Be(expectedUnitOfTime1);
            actualUnitOfTime2.Should().Be(expectedUnitOfTime2);
            actualUnitOfTime3.Should().Be(expectedUnitOfTime3);
            actualUnitOfTime4.Should().Be(expectedUnitOfTime4);
            actualUnitOfTime5.Should().Be(expectedUnitOfTime5);
            actualUnitOfTime6.Should().Be(expectedUnitOfTime6);
            actualUnitOfTime7.Should().Be(expectedUnitOfTime7);
            actualUnitOfTime8.Should().Be(expectedUnitOfTime8);
        }

        [Fact]
        public static void Plus___Should_subtract_fiscal_quarters___When_parameter_unitOfTime_is_a_FiscalQuarter_and_parameter_unitsToAdd_is_negative()
        {
            // Arrange
            var systemUnderTest = new FiscalQuarter(2016, QuarterNumber.Second);

            var expectedUnitOfTime1 = new FiscalQuarter(2016, QuarterNumber.First);
            var expectedUnitOfTime2 = new FiscalQuarter(2015, QuarterNumber.Fourth);
            var expectedUnitOfTime3 = new FiscalQuarter(2015, QuarterNumber.Third);
            var expectedUnitOfTime4 = new FiscalQuarter(2015, QuarterNumber.Second);
            var expectedUnitOfTime5 = new FiscalQuarter(2015, QuarterNumber.First);
            var expectedUnitOfTime6 = new FiscalQuarter(2014, QuarterNumber.Fourth);
            var expectedUnitOfTime7 = new FiscalQuarter(2014, QuarterNumber.Third);
            var expectedUnitOfTime8 = new FiscalQuarter(2014, QuarterNumber.Second);

            // Act
            var actualUnitOfTime1 = systemUnderTest.Plus(-1);
            var actualUnitOfTime2 = systemUnderTest.Plus(-2);
            var actualUnitOfTime3 = systemUnderTest.Plus(-3);
            var actualUnitOfTime4 = systemUnderTest.Plus(-4);
            var actualUnitOfTime5 = systemUnderTest.Plus(-5);
            var actualUnitOfTime6 = systemUnderTest.Plus(-6);
            var actualUnitOfTime7 = systemUnderTest.Plus(-7);
            var actualUnitOfTime8 = systemUnderTest.Plus(-8);

            // Assert
            actualUnitOfTime1.Should().Be(expectedUnitOfTime1);
            actualUnitOfTime2.Should().Be(expectedUnitOfTime2);
            actualUnitOfTime3.Should().Be(expectedUnitOfTime3);
            actualUnitOfTime4.Should().Be(expectedUnitOfTime4);
            actualUnitOfTime5.Should().Be(expectedUnitOfTime5);
            actualUnitOfTime6.Should().Be(expectedUnitOfTime6);
            actualUnitOfTime7.Should().Be(expectedUnitOfTime7);
            actualUnitOfTime8.Should().Be(expectedUnitOfTime8);
        }

        [Fact]
        public static void Plus___Should_return_same_generic_quarter___When_parameter_unitOfTime_is_a_GenericQuarter_and_parameter_unitsToAdd_is_0()
        {
            // Arrange
            var expectedUnitOfTime = A.Dummy<GenericQuarter>();

            // Act
            var actualUnitOfTime = expectedUnitOfTime.Plus(0);

            // Assert
            actualUnitOfTime.Should().Be(expectedUnitOfTime);
        }

        [Fact]
        public static void Plus___Should_add_generic_quarters___When_parameter_unitOfTime_is_a_GenericQuarter_and_parameter_unitsToAdd_is_positive()
        {
            // Arrange
            var systemUnderTest = new GenericQuarter(2016, QuarterNumber.Second);

            var expectedUnitOfTime1 = new GenericQuarter(2016, QuarterNumber.Third);
            var expectedUnitOfTime2 = new GenericQuarter(2016, QuarterNumber.Fourth);
            var expectedUnitOfTime3 = new GenericQuarter(2017, QuarterNumber.First);
            var expectedUnitOfTime4 = new GenericQuarter(2017, QuarterNumber.Second);
            var expectedUnitOfTime5 = new GenericQuarter(2017, QuarterNumber.Third);
            var expectedUnitOfTime6 = new GenericQuarter(2017, QuarterNumber.Fourth);
            var expectedUnitOfTime7 = new GenericQuarter(2018, QuarterNumber.First);
            var expectedUnitOfTime8 = new GenericQuarter(2018, QuarterNumber.Second);

            // Act
            var actualUnitOfTime1 = systemUnderTest.Plus(1);
            var actualUnitOfTime2 = systemUnderTest.Plus(2);
            var actualUnitOfTime3 = systemUnderTest.Plus(3);
            var actualUnitOfTime4 = systemUnderTest.Plus(4);
            var actualUnitOfTime5 = systemUnderTest.Plus(5);
            var actualUnitOfTime6 = systemUnderTest.Plus(6);
            var actualUnitOfTime7 = systemUnderTest.Plus(7);
            var actualUnitOfTime8 = systemUnderTest.Plus(8);

            // Assert
            actualUnitOfTime1.Should().Be(expectedUnitOfTime1);
            actualUnitOfTime2.Should().Be(expectedUnitOfTime2);
            actualUnitOfTime3.Should().Be(expectedUnitOfTime3);
            actualUnitOfTime4.Should().Be(expectedUnitOfTime4);
            actualUnitOfTime5.Should().Be(expectedUnitOfTime5);
            actualUnitOfTime6.Should().Be(expectedUnitOfTime6);
            actualUnitOfTime7.Should().Be(expectedUnitOfTime7);
            actualUnitOfTime8.Should().Be(expectedUnitOfTime8);
        }

        [Fact]
        public static void Plus___Should_subtract_generic_quarters___When_parameter_unitOfTime_is_a_GenericQuarter_and_parameter_unitsToAdd_is_negative()
        {
            // Arrange
            var systemUnderTest = new GenericQuarter(2016, QuarterNumber.Second);

            var expectedUnitOfTime1 = new GenericQuarter(2016, QuarterNumber.First);
            var expectedUnitOfTime2 = new GenericQuarter(2015, QuarterNumber.Fourth);
            var expectedUnitOfTime3 = new GenericQuarter(2015, QuarterNumber.Third);
            var expectedUnitOfTime4 = new GenericQuarter(2015, QuarterNumber.Second);
            var expectedUnitOfTime5 = new GenericQuarter(2015, QuarterNumber.First);
            var expectedUnitOfTime6 = new GenericQuarter(2014, QuarterNumber.Fourth);
            var expectedUnitOfTime7 = new GenericQuarter(2014, QuarterNumber.Third);
            var expectedUnitOfTime8 = new GenericQuarter(2014, QuarterNumber.Second);

            // Act
            var actualUnitOfTime1 = systemUnderTest.Plus(-1);
            var actualUnitOfTime2 = systemUnderTest.Plus(-2);
            var actualUnitOfTime3 = systemUnderTest.Plus(-3);
            var actualUnitOfTime4 = systemUnderTest.Plus(-4);
            var actualUnitOfTime5 = systemUnderTest.Plus(-5);
            var actualUnitOfTime6 = systemUnderTest.Plus(-6);
            var actualUnitOfTime7 = systemUnderTest.Plus(-7);
            var actualUnitOfTime8 = systemUnderTest.Plus(-8);

            // Assert
            actualUnitOfTime1.Should().Be(expectedUnitOfTime1);
            actualUnitOfTime2.Should().Be(expectedUnitOfTime2);
            actualUnitOfTime3.Should().Be(expectedUnitOfTime3);
            actualUnitOfTime4.Should().Be(expectedUnitOfTime4);
            actualUnitOfTime5.Should().Be(expectedUnitOfTime5);
            actualUnitOfTime6.Should().Be(expectedUnitOfTime6);
            actualUnitOfTime7.Should().Be(expectedUnitOfTime7);
            actualUnitOfTime8.Should().Be(expectedUnitOfTime8);
        }

        [Fact]
        public static void Plus___Should_return_same_calendar_year___When_parameter_unitOfTime_is_a_CalendarYear_and_parameter_unitsToAdd_is_0()
        {
            // Arrange
            var expectedUnitOfTime = A.Dummy<CalendarYear>();

            // Act
            var actualUnitOfTime = expectedUnitOfTime.Plus(0);

            // Assert
            actualUnitOfTime.Should().Be(expectedUnitOfTime);
        }

        [Fact]
        public static void Plus___Should_add_calendar_years___When_parameter_unitOfTime_is_a_CalendarYear_and_parameter_unitsToAdd_is_positive()
        {
            // Arrange
            var systemUnderTest = new CalendarYear(2016);

            var expectedUnitOfTime1 = new CalendarYear(2017);
            var expectedUnitOfTime2 = new CalendarYear(2018);
            var expectedUnitOfTime3 = new CalendarYear(2019);

            // Act
            var actualUnitOfTime1 = systemUnderTest.Plus(1);
            var actualUnitOfTime2 = systemUnderTest.Plus(2);
            var actualUnitOfTime3 = systemUnderTest.Plus(3);

            // Assert
            actualUnitOfTime1.Should().Be(expectedUnitOfTime1);
            actualUnitOfTime2.Should().Be(expectedUnitOfTime2);
            actualUnitOfTime3.Should().Be(expectedUnitOfTime3);
        }

        [Fact]
        public static void Plus___Should_subtract_calendar_years___When_parameter_unitOfTime_is_a_CalendarYear_and_parameter_unitsToAdd_is_negative()
        {
            // Arrange
            var systemUnderTest = new CalendarYear(2016);

            var expectedUnitOfTime1 = new CalendarYear(2015);
            var expectedUnitOfTime2 = new CalendarYear(2014);
            var expectedUnitOfTime3 = new CalendarYear(2013);

            // Act
            var actualUnitOfTime1 = systemUnderTest.Plus(-1);
            var actualUnitOfTime2 = systemUnderTest.Plus(-2);
            var actualUnitOfTime3 = systemUnderTest.Plus(-3);

            // Assert
            actualUnitOfTime1.Should().Be(expectedUnitOfTime1);
            actualUnitOfTime2.Should().Be(expectedUnitOfTime2);
            actualUnitOfTime3.Should().Be(expectedUnitOfTime3);
        }

        [Fact]
        public static void Plus___Should_return_same_fiscal_year___When_parameter_unitOfTime_is_a_FiscalYear_and_parameter_unitsToAdd_is_0()
        {
            // Arrange
            var expectedUnitOfTime = A.Dummy<FiscalYear>();

            // Act
            var actualUnitOfTime = expectedUnitOfTime.Plus(0);

            // Assert
            actualUnitOfTime.Should().Be(expectedUnitOfTime);
        }

        [Fact]
        public static void Plus___Should_add_fiscal_years___When_parameter_unitOfTime_is_a_FiscalYear_and_parameter_unitsToAdd_is_positive()
        {
            // Arrange
            var systemUnderTest = new FiscalYear(2016);

            var expectedUnitOfTime1 = new FiscalYear(2017);
            var expectedUnitOfTime2 = new FiscalYear(2018);
            var expectedUnitOfTime3 = new FiscalYear(2019);

            // Act
            var actualUnitOfTime1 = systemUnderTest.Plus(1);
            var actualUnitOfTime2 = systemUnderTest.Plus(2);
            var actualUnitOfTime3 = systemUnderTest.Plus(3);

            // Assert
            actualUnitOfTime1.Should().Be(expectedUnitOfTime1);
            actualUnitOfTime2.Should().Be(expectedUnitOfTime2);
            actualUnitOfTime3.Should().Be(expectedUnitOfTime3);
        }

        [Fact]
        public static void Plus___Should_subtract_fiscal_years___When_parameter_unitOfTime_is_a_FiscalYear_and_parameter_unitsToAdd_is_negative()
        {
            // Arrange
            var systemUnderTest = new FiscalYear(2016);

            var expectedUnitOfTime1 = new FiscalYear(2015);
            var expectedUnitOfTime2 = new FiscalYear(2014);
            var expectedUnitOfTime3 = new FiscalYear(2013);

            // Act
            var actualUnitOfTime1 = systemUnderTest.Plus(-1);
            var actualUnitOfTime2 = systemUnderTest.Plus(-2);
            var actualUnitOfTime3 = systemUnderTest.Plus(-3);

            // Assert
            actualUnitOfTime1.Should().Be(expectedUnitOfTime1);
            actualUnitOfTime2.Should().Be(expectedUnitOfTime2);
            actualUnitOfTime3.Should().Be(expectedUnitOfTime3);
        }

        [Fact]
        public static void Plus___Should_return_same_generic_year___When_parameter_unitOfTime_is_a_GenericYear_and_parameter_unitsToAdd_is_0()
        {
            // Arrange
            var expectedUnitOfTime = A.Dummy<GenericYear>();

            // Act
            var actualUnitOfTime = expectedUnitOfTime.Plus(0);

            // Assert
            actualUnitOfTime.Should().Be(expectedUnitOfTime);
        }

        [Fact]
        public static void Plus___Should_add_generic_years___When_parameter_unitOfTime_is_a_GenericYear_and_parameter_unitsToAdd_is_positive()
        {
            // Arrange
            var systemUnderTest = new GenericYear(2016);

            var expectedUnitOfTime1 = new GenericYear(2017);
            var expectedUnitOfTime2 = new GenericYear(2018);
            var expectedUnitOfTime3 = new GenericYear(2019);

            // Act
            var actualUnitOfTime1 = systemUnderTest.Plus(1);
            var actualUnitOfTime2 = systemUnderTest.Plus(2);
            var actualUnitOfTime3 = systemUnderTest.Plus(3);

            // Assert
            actualUnitOfTime1.Should().Be(expectedUnitOfTime1);
            actualUnitOfTime2.Should().Be(expectedUnitOfTime2);
            actualUnitOfTime3.Should().Be(expectedUnitOfTime3);
        }

        [Fact]
        public static void Plus___Should_subtract_generic_years___When_parameter_unitOfTime_is_a_GenericYear_and_parameter_unitsToAdd_is_negative()
        {
            // Arrange
            var systemUnderTest = new GenericYear(2016);

            var expectedUnitOfTime1 = new GenericYear(2015);
            var expectedUnitOfTime2 = new GenericYear(2014);
            var expectedUnitOfTime3 = new GenericYear(2013);

            // Act
            var actualUnitOfTime1 = systemUnderTest.Plus(-1);
            var actualUnitOfTime2 = systemUnderTest.Plus(-2);
            var actualUnitOfTime3 = systemUnderTest.Plus(-3);

            // Assert
            actualUnitOfTime1.Should().Be(expectedUnitOfTime1);
            actualUnitOfTime2.Should().Be(expectedUnitOfTime2);
            actualUnitOfTime3.Should().Be(expectedUnitOfTime3);
        }

        [Fact]
        public static void ToFiscalQuarter___Should_throw_ArgumentNullException___When_parameter_calendarQuarter_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => UnitOfTimeExtensions.ToFiscalQuarter(null, A.Dummy<QuarterNumber>()));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void ToFiscalQuarter___Should_throw_ArgumentException___When_parameter_calendarQuarterThatIsFirstFiscalQuarter_is_Invalid()
        {
            // Arrange, Act
            var ex = Record.Exception(() => A.Dummy<CalendarQuarter>().ToFiscalQuarter(QuarterNumber.Invalid));

            // Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void ToFiscalQuarter___Should_adjust_calendar_quarter_to_fiscal_quarter___When_called()
        {
            // Arrange
            // note: See green and yellow highlighted area in embedded spreadsheet FiscalQuarterToCalendarQuarter.xlsx
            var calQuarter2 = new CalendarQuarter(2013, QuarterNumber.Second);
            var calQuarter1 = new CalendarQuarter(2013, QuarterNumber.First);
            var calQuarter3 = new CalendarQuarter(2013, QuarterNumber.Third);
            var calQuarter4 = new CalendarQuarter(2013, QuarterNumber.Fourth);

            // Act
            var fiscalQuarter1a = calQuarter1.ToFiscalQuarter((QuarterNumber)4);
            var fiscalQuarter1b = calQuarter1.ToFiscalQuarter((QuarterNumber)3);
            var fiscalQuarter1c = calQuarter1.ToFiscalQuarter((QuarterNumber)2);
            var fiscalQuarter1d = calQuarter1.ToFiscalQuarter((QuarterNumber)1);

            var fiscalQuarter2a = calQuarter2.ToFiscalQuarter((QuarterNumber)4);
            var fiscalQuarter2b = calQuarter2.ToFiscalQuarter((QuarterNumber)3);
            var fiscalQuarter2c = calQuarter2.ToFiscalQuarter((QuarterNumber)2);
            var fiscalQuarter2d = calQuarter2.ToFiscalQuarter((QuarterNumber)1);

            var fiscalQuarter3a = calQuarter3.ToFiscalQuarter((QuarterNumber)4);
            var fiscalQuarter3b = calQuarter3.ToFiscalQuarter((QuarterNumber)3);
            var fiscalQuarter3c = calQuarter3.ToFiscalQuarter((QuarterNumber)2);
            var fiscalQuarter3d = calQuarter3.ToFiscalQuarter((QuarterNumber)1);

            var fiscalQuarter4a = calQuarter4.ToFiscalQuarter((QuarterNumber)4);
            var fiscalQuarter4b = calQuarter4.ToFiscalQuarter((QuarterNumber)3);
            var fiscalQuarter4c = calQuarter4.ToFiscalQuarter((QuarterNumber)2);
            var fiscalQuarter4d = calQuarter4.ToFiscalQuarter((QuarterNumber)1);

            // Assert
            fiscalQuarter1a.Should().Be(new FiscalQuarter(2013, (QuarterNumber)2));
            fiscalQuarter1b.Should().Be(new FiscalQuarter(2013, (QuarterNumber)3));
            fiscalQuarter1c.Should().Be(new FiscalQuarter(2013, (QuarterNumber)4));
            fiscalQuarter1d.Should().Be(new FiscalQuarter(2013, (QuarterNumber)1));
            fiscalQuarter2a.Should().Be(new FiscalQuarter(2013, (QuarterNumber)3));
            fiscalQuarter2b.Should().Be(new FiscalQuarter(2013, (QuarterNumber)4));
            fiscalQuarter2c.Should().Be(new FiscalQuarter(2014, (QuarterNumber)1));
            fiscalQuarter2d.Should().Be(new FiscalQuarter(2013, (QuarterNumber)2));
            fiscalQuarter3a.Should().Be(new FiscalQuarter(2013, (QuarterNumber)4));
            fiscalQuarter3b.Should().Be(new FiscalQuarter(2014, (QuarterNumber)1));
            fiscalQuarter3c.Should().Be(new FiscalQuarter(2014, (QuarterNumber)2));
            fiscalQuarter3d.Should().Be(new FiscalQuarter(2013, (QuarterNumber)3));
            fiscalQuarter4a.Should().Be(new FiscalQuarter(2014, (QuarterNumber)1));
            fiscalQuarter4b.Should().Be(new FiscalQuarter(2014, (QuarterNumber)2));
            fiscalQuarter4c.Should().Be(new FiscalQuarter(2014, (QuarterNumber)3));
            fiscalQuarter4d.Should().Be(new FiscalQuarter(2013, (QuarterNumber)4));
        }

        [Fact]
        public static void ToCalendarQuarter___Should_throw_ArgumentNullException___When_parameter_fiscalQuarter_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => UnitOfTimeExtensions.ToCalendarQuarter(null, A.Dummy<QuarterNumber>()));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void ToCalendarQuarter___Should_throw_ArgumentException___When_parameter_calendarQuarterThatIsFirstFiscalQuarter_is_Invalid()
        {
            // Arrange, Act
            var ex = Record.Exception(() => A.Dummy<FiscalQuarter>().ToCalendarQuarter(QuarterNumber.Invalid));

            // Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void ToCalendarQuarter___Should_adjust_fiscal_quarter_to_calendar_quarter___When_called()
        {
            // Arrange
            // note: See red and green highlighted area in embedded spreadsheet FiscalQuarterToCalendarQuarter.xlsx
            var expected1 = new CalendarQuarter(2013, (QuarterNumber)3);
            var expected2 = new CalendarQuarter(2013, (QuarterNumber)2);
            var expected3 = new CalendarQuarter(2013, (QuarterNumber)1);
            var expected4 = new CalendarQuarter(2012, (QuarterNumber)4);

            // Act
            var actual1a = new FiscalQuarter(2013, (QuarterNumber)4).ToCalendarQuarter((QuarterNumber)4);
            var actual1b = new FiscalQuarter(2014, (QuarterNumber)1).ToCalendarQuarter((QuarterNumber)3);
            var actual1c = new FiscalQuarter(2014, (QuarterNumber)2).ToCalendarQuarter((QuarterNumber)2);
            var actual1d = new FiscalQuarter(2013, (QuarterNumber)3).ToCalendarQuarter((QuarterNumber)1);

            var actual2a = new FiscalQuarter(2013, (QuarterNumber)3).ToCalendarQuarter((QuarterNumber)4);
            var actual2b = new FiscalQuarter(2013, (QuarterNumber)4).ToCalendarQuarter((QuarterNumber)3);
            var actual2c = new FiscalQuarter(2014, (QuarterNumber)1).ToCalendarQuarter((QuarterNumber)2);
            var actual2d = new FiscalQuarter(2013, (QuarterNumber)2).ToCalendarQuarter((QuarterNumber)1);

            var actual3a = new FiscalQuarter(2013, (QuarterNumber)2).ToCalendarQuarter((QuarterNumber)4);
            var actual3b = new FiscalQuarter(2013, (QuarterNumber)3).ToCalendarQuarter((QuarterNumber)3);
            var actual3c = new FiscalQuarter(2013, (QuarterNumber)4).ToCalendarQuarter((QuarterNumber)2);
            var actual3d = new FiscalQuarter(2013, (QuarterNumber)1).ToCalendarQuarter((QuarterNumber)1);

            var actual4a = new FiscalQuarter(2013, (QuarterNumber)1).ToCalendarQuarter((QuarterNumber)4);
            var actual4b = new FiscalQuarter(2013, (QuarterNumber)2).ToCalendarQuarter((QuarterNumber)3);
            var actual4c = new FiscalQuarter(2013, (QuarterNumber)3).ToCalendarQuarter((QuarterNumber)2);
            var actual4d = new FiscalQuarter(2012, (QuarterNumber)4).ToCalendarQuarter((QuarterNumber)1);

            // Assert
            actual1a.Should().Be(expected1);
            actual1b.Should().Be(expected1);
            actual1c.Should().Be(expected1);
            actual1d.Should().Be(expected1);
            actual2a.Should().Be(expected2);
            actual2b.Should().Be(expected2);
            actual2c.Should().Be(expected2);
            actual2d.Should().Be(expected2);
            actual3a.Should().Be(expected3);
            actual3b.Should().Be(expected3);
            actual3c.Should().Be(expected3);
            actual3d.Should().Be(expected3);
            actual4a.Should().Be(expected4);
            actual4b.Should().Be(expected4);
            actual4c.Should().Be(expected4);
            actual4d.Should().Be(expected4);
        }

        [Fact]
        public static void GetFirstCalendarDay___Should_throw_ArgumentNullException___When_parameter_unitOfTime_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => UnitOfTimeExtensions.GetFirstCalendarDay<CalendarUnitOfTime>(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void GetFirstCalendarDay___Should_return_same_unitOfTime___When_parameter_unitOfTime_is_of_type_CalendarDay()
        {
            // Arrange
            var expectedDay = A.Dummy<CalendarDay>();

            // Act
            var actualDay = expectedDay.GetFirstCalendarDay();

            // Assert
            actualDay.Should().Be(expectedDay);
        }

        [Fact]
        public static void GetFirstCalendarDay___Should_return_first_day_of_month___When_parameter_unitOfTime_is_of_type_CalendarMonth()
        {
            // Arrange
            var month = A.Dummy<CalendarMonth>();
            var expectedDay = new CalendarDay(month.Year, month.MonthOfYear, DayOfMonth.One);

            // Act
            var actualDay = month.GetFirstCalendarDay();

            // Assert
            actualDay.Should().Be(expectedDay);
        }

        [Fact]
        public static void GetFirstCalendarDay___Should_return_first_day_of_quarter___When_parameter_unitOfTime_is_of_type_CalendarQuarter()
        {
            // Arrange
            var quarter1 = new CalendarQuarter(2016, QuarterNumber.First);
            var quarter2 = new CalendarQuarter(2016, QuarterNumber.Second);
            var quarter3 = new CalendarQuarter(2016, QuarterNumber.Third);
            var quarter4 = new CalendarQuarter(2016, QuarterNumber.Fourth);

            var expectedDay1 = new CalendarDay(2016, MonthOfYear.January, DayOfMonth.One);
            var expectedDay2 = new CalendarDay(2016, MonthOfYear.April, DayOfMonth.One);
            var expectedDay3 = new CalendarDay(2016, MonthOfYear.July, DayOfMonth.One);
            var expectedDay4 = new CalendarDay(2016, MonthOfYear.October, DayOfMonth.One);

            // Act
            var actualDay1 = quarter1.GetFirstCalendarDay();
            var actualDay2 = quarter2.GetFirstCalendarDay();
            var actualDay3 = quarter3.GetFirstCalendarDay();
            var actualDay4 = quarter4.GetFirstCalendarDay();

            // Assert
            actualDay1.Should().Be(expectedDay1);
            actualDay2.Should().Be(expectedDay2);
            actualDay3.Should().Be(expectedDay3);
            actualDay4.Should().Be(expectedDay4);
        }

        [Fact]
        public static void GetFirstCalendarYear___Should_return_first_day_of_year___When_parameter_unitOfTime_is_of_type_CalendarYear()
        {
            // Arrange
            var year1 = new CalendarYear(2016);
            var year2 = new CalendarYear(2017);

            var expectedDay1 = new CalendarDay(2016, MonthOfYear.January, DayOfMonth.One);
            var expectedDay2 = new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One);

            // Act
            var actualDay1 = year1.GetFirstCalendarDay();
            var actualDay2 = year2.GetFirstCalendarDay();

            // Assert
            actualDay1.Should().Be(expectedDay1);
            actualDay2.Should().Be(expectedDay2);
        }

        [Fact]
        public static void GetLastCalendarDay___Should_throw_ArgumentNullException___When_parameter_unitOfTime_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => UnitOfTimeExtensions.GetLastCalendarDay<CalendarUnitOfTime>(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void GetLastCalendarDay___Should_return_same_unitOfTime___When_parameter_unitOfTime_is_of_type_CalendarDay()
        {
            // Arrange
            var expectedDay = A.Dummy<CalendarDay>();

            // Act
            var actualDay = expectedDay.GetLastCalendarDay();

            // Assert
            actualDay.Should().Be(expectedDay);
        }

        [Fact]
        public static void GetLastCalendarDay___Should_return_last_day_of_month___When_parameter_unitOfTime_is_of_type_CalendarMonth()
        {
            // Arrange
            var month1 = new CalendarMonth(2016, MonthOfYear.February);
            var month2 = new CalendarMonth(2016, MonthOfYear.November);
            var month3 = new CalendarMonth(2016, MonthOfYear.October);

            var expectedDay1 = new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine);
            var expectedDay2 = new CalendarDay(2016, MonthOfYear.November, DayOfMonth.Thirty);
            var expectedDay3 = new CalendarDay(2016, MonthOfYear.October, DayOfMonth.ThirtyOne);

            // Act
            var actualDay1 = month1.GetLastCalendarDay();
            var actualDay2 = month2.GetLastCalendarDay();
            var actualDay3 = month3.GetLastCalendarDay();

            // Assert
            actualDay1.Should().Be(expectedDay1);
            actualDay2.Should().Be(expectedDay2);
            actualDay3.Should().Be(expectedDay3);
        }

        [Fact]
        public static void GetLastCalendarDay___Should_return_last_day_of_quarter___When_parameter_unitOfTime_is_of_type_CalendarQuarter()
        {
            // Arrange
            var quarter1 = new CalendarQuarter(2016, QuarterNumber.First);
            var quarter2 = new CalendarQuarter(2016, QuarterNumber.Second);
            var quarter3 = new CalendarQuarter(2016, QuarterNumber.Third);
            var quarter4 = new CalendarQuarter(2016, QuarterNumber.Fourth);

            var expectedDay1 = new CalendarDay(2016, MonthOfYear.March, DayOfMonth.ThirtyOne);
            var expectedDay2 = new CalendarDay(2016, MonthOfYear.June, DayOfMonth.Thirty);
            var expectedDay3 = new CalendarDay(2016, MonthOfYear.September, DayOfMonth.Thirty);
            var expectedDay4 = new CalendarDay(2016, MonthOfYear.December, DayOfMonth.ThirtyOne);

            // Act
            var actualDay1 = quarter1.GetLastCalendarDay();
            var actualDay2 = quarter2.GetLastCalendarDay();
            var actualDay3 = quarter3.GetLastCalendarDay();
            var actualDay4 = quarter4.GetLastCalendarDay();

            // Assert
            actualDay1.Should().Be(expectedDay1);
            actualDay2.Should().Be(expectedDay2);
            actualDay3.Should().Be(expectedDay3);
            actualDay4.Should().Be(expectedDay4);
        }

        [Fact]
        public static void GetLastCalendarYear___Should_return_last_day_of_year___When_parameter_unitOfTime_is_of_type_CalendarYear()
        {
            // Arrange
            var year1 = new CalendarYear(2016);
            var year2 = new CalendarYear(2017);

            var expectedDay1 = new CalendarDay(2016, MonthOfYear.December, DayOfMonth.ThirtyOne);
            var expectedDay2 = new CalendarDay(2017, MonthOfYear.December, DayOfMonth.ThirtyOne);

            // Act
            var actualDay1 = year1.GetLastCalendarDay();
            var actualDay2 = year2.GetLastCalendarDay();

            // Assert
            actualDay1.Should().Be(expectedDay1);
            actualDay2.Should().Be(expectedDay2);
        }

        [Fact]
        public static void GetUnitsToDate___Should_throw_ArgumentNullException___When_parameter_lastUnitOfTimeInYear_is_null()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => UnitOfTimeExtensions.GetUnitsToDate<CalendarDay>(null));
            var ex2 = Record.Exception(() => UnitOfTimeExtensions.GetUnitsToDate<CalendarMonth>(null));
            var ex3 = Record.Exception(() => UnitOfTimeExtensions.GetUnitsToDate<CalendarQuarter>(null));
            var ex4 = Record.Exception(() => UnitOfTimeExtensions.GetUnitsToDate<CalendarYear>(null));

            var ex5 = Record.Exception(() => UnitOfTimeExtensions.GetUnitsToDate<FiscalMonth>(null));
            var ex6 = Record.Exception(() => UnitOfTimeExtensions.GetUnitsToDate<FiscalQuarter>(null));
            var ex7 = Record.Exception(() => UnitOfTimeExtensions.GetUnitsToDate<FiscalYear>(null));

            var ex8 = Record.Exception(() => UnitOfTimeExtensions.GetUnitsToDate<GenericMonth>(null));
            var ex9 = Record.Exception(() => UnitOfTimeExtensions.GetUnitsToDate<GenericQuarter>(null));
            var ex10 = Record.Exception(() => UnitOfTimeExtensions.GetUnitsToDate<GenericYear>(null));

            // Assert
            ex1.Should().BeOfType<ArgumentNullException>();
            ex2.Should().BeOfType<ArgumentNullException>();
            ex3.Should().BeOfType<ArgumentNullException>();
            ex4.Should().BeOfType<ArgumentNullException>();
            ex5.Should().BeOfType<ArgumentNullException>();
            ex6.Should().BeOfType<ArgumentNullException>();
            ex7.Should().BeOfType<ArgumentNullException>();
            ex8.Should().BeOfType<ArgumentNullException>();
            ex9.Should().BeOfType<ArgumentNullException>();
            ex10.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void GetUnitsToDate___Should_return_all_days_from_january_1_to_lastUnitOfTimeInYear___When_lastUnitOfTimeInYear_is_of_type_CalendarDay()
        {
            // Arrange
            var lastUnitOfTimeInYear1 = new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One);
            var lastUnitOfTimeInYear2 = new CalendarDay(2016, MonthOfYear.January, DayOfMonth.One);

            var expectedUnitsToDate1 = new List<CalendarDay>();
            for (var date = new DateTime(2016, 1, 1); date <= lastUnitOfTimeInYear1.ToDateTime(); date = date.AddDays(1))
            {
                expectedUnitsToDate1.Add(date.ToCalendarDay());
            }

            var expectedUnitsToDate2 = new List<CalendarDay> { lastUnitOfTimeInYear2 };

            // Act
            var actualUnitsToDate1 = lastUnitOfTimeInYear1.GetUnitsToDate();
            var actualUnitsToDate2 = lastUnitOfTimeInYear2.GetUnitsToDate();

            // Assert
            actualUnitsToDate1.Should().Equal(expectedUnitsToDate1);
            actualUnitsToDate2.Should().Equal(expectedUnitsToDate2);
        }

        [Fact]
        public static void GetUnitsToDate___Should_return_all_months_from_month_1_to_lastUnitOfTimeInYear___When_lastUnitOfTimeInYear_is_of_type_CalendarMonth()
        {
            // Arrange
            var lastUnitOfTimeInYear1 = new CalendarMonth(2016, (MonthOfYear)MonthNumber.One);
            var lastUnitOfTimeInYear2 = new CalendarMonth(2016, (MonthOfYear)MonthNumber.Three);
            var lastUnitOfTimeInYear3 = new CalendarMonth(2016, (MonthOfYear)MonthNumber.Twelve);

            var expectedUnitsToDate1 = new List<CalendarMonth> { lastUnitOfTimeInYear1 };
            var expectedUnitsToDate2 = new List<CalendarMonth> { new CalendarMonth(2016, (MonthOfYear)MonthNumber.One), new CalendarMonth(2016, (MonthOfYear)MonthNumber.Two), new CalendarMonth(2016, (MonthOfYear)MonthNumber.Three) };
            var expectedUnitsToDate3 = new List<CalendarMonth> { new CalendarMonth(2016, (MonthOfYear)MonthNumber.One), new CalendarMonth(2016, (MonthOfYear)MonthNumber.Two), new CalendarMonth(2016, (MonthOfYear)MonthNumber.Three), new CalendarMonth(2016, (MonthOfYear)MonthNumber.Four), new CalendarMonth(2016, (MonthOfYear)MonthNumber.Five), new CalendarMonth(2016, (MonthOfYear)MonthNumber.Six), new CalendarMonth(2016, (MonthOfYear)MonthNumber.Seven), new CalendarMonth(2016, (MonthOfYear)MonthNumber.Eight), new CalendarMonth(2016, (MonthOfYear)MonthNumber.Nine), new CalendarMonth(2016, (MonthOfYear)MonthNumber.Ten), new CalendarMonth(2016, (MonthOfYear)MonthNumber.Eleven), new CalendarMonth(2016, (MonthOfYear)MonthNumber.Twelve) };

            // Act
            var actualUnitsToDate1 = lastUnitOfTimeInYear1.GetUnitsToDate();
            var actualUnitsToDate2 = lastUnitOfTimeInYear2.GetUnitsToDate();
            var actualUnitsToDate3 = lastUnitOfTimeInYear3.GetUnitsToDate();

            // Assert
            actualUnitsToDate1.Should().Equal(expectedUnitsToDate1);
            actualUnitsToDate2.Should().Equal(expectedUnitsToDate2);
            actualUnitsToDate3.Should().Equal(expectedUnitsToDate3);
        }

        [Fact]
        public static void GetUnitsToDate___Should_return_all_months_from_month_1_to_lastUnitOfTimeInYear___When_lastUnitOfTimeInYear_is_of_type_FiscalMonth()
        {
            // Arrange
            var lastUnitOfTimeInYear1 = new FiscalMonth(2016, MonthNumber.One);
            var lastUnitOfTimeInYear2 = new FiscalMonth(2016, MonthNumber.Three);
            var lastUnitOfTimeInYear3 = new FiscalMonth(2016, MonthNumber.Twelve);

            var expectedUnitsToDate1 = new List<FiscalMonth> { lastUnitOfTimeInYear1 };
            var expectedUnitsToDate2 = new List<FiscalMonth> { new FiscalMonth(2016, MonthNumber.One), new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Three) };
            var expectedUnitsToDate3 = new List<FiscalMonth> { new FiscalMonth(2016, MonthNumber.One), new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Three), new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2016, MonthNumber.Five), new FiscalMonth(2016, MonthNumber.Six), new FiscalMonth(2016, MonthNumber.Seven), new FiscalMonth(2016, MonthNumber.Eight), new FiscalMonth(2016, MonthNumber.Nine), new FiscalMonth(2016, MonthNumber.Ten), new FiscalMonth(2016, MonthNumber.Eleven), new FiscalMonth(2016, MonthNumber.Twelve) };

            // Act
            var actualUnitsToDate1 = lastUnitOfTimeInYear1.GetUnitsToDate();
            var actualUnitsToDate2 = lastUnitOfTimeInYear2.GetUnitsToDate();
            var actualUnitsToDate3 = lastUnitOfTimeInYear3.GetUnitsToDate();

            // Assert
            actualUnitsToDate1.Should().Equal(expectedUnitsToDate1);
            actualUnitsToDate2.Should().Equal(expectedUnitsToDate2);
            actualUnitsToDate3.Should().Equal(expectedUnitsToDate3);
        }

        [Fact]
        public static void GetUnitsToDate___Should_return_all_months_from_month_1_to_lastUnitOfTimeInYear___When_lastUnitOfTimeInYear_is_of_type_GenericMonth()
        {
            // Arrange
            var lastUnitOfTimeInYear1 = new GenericMonth(2016, MonthNumber.One);
            var lastUnitOfTimeInYear2 = new GenericMonth(2016, MonthNumber.Three);
            var lastUnitOfTimeInYear3 = new GenericMonth(2016, MonthNumber.Twelve);

            var expectedUnitsToDate1 = new List<GenericMonth> { lastUnitOfTimeInYear1 };
            var expectedUnitsToDate2 = new List<GenericMonth> { new GenericMonth(2016, MonthNumber.One), new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Three) };
            var expectedUnitsToDate3 = new List<GenericMonth> { new GenericMonth(2016, MonthNumber.One), new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Three), new GenericMonth(2016, MonthNumber.Four), new GenericMonth(2016, MonthNumber.Five), new GenericMonth(2016, MonthNumber.Six), new GenericMonth(2016, MonthNumber.Seven), new GenericMonth(2016, MonthNumber.Eight), new GenericMonth(2016, MonthNumber.Nine), new GenericMonth(2016, MonthNumber.Ten), new GenericMonth(2016, MonthNumber.Eleven), new GenericMonth(2016, MonthNumber.Twelve) };

            // Act
            var actualUnitsToDate1 = lastUnitOfTimeInYear1.GetUnitsToDate();
            var actualUnitsToDate2 = lastUnitOfTimeInYear2.GetUnitsToDate();
            var actualUnitsToDate3 = lastUnitOfTimeInYear3.GetUnitsToDate();

            // Assert
            actualUnitsToDate1.Should().Equal(expectedUnitsToDate1);
            actualUnitsToDate2.Should().Equal(expectedUnitsToDate2);
            actualUnitsToDate3.Should().Equal(expectedUnitsToDate3);
        }

        [Fact]
        public static void GetUnitsToDate___Should_return_all_quarters_from_1Q_to_lastUnitOfTimeInYear___When_lastUnitOfTimeInYear_is_of_type_CalendarQuarter()
        {
            // Arrange
            var lastUnitOfTimeInYear1 = new CalendarQuarter(2016, QuarterNumber.First);
            var lastUnitOfTimeInYear2 = new CalendarQuarter(2016, QuarterNumber.Second);
            var lastUnitOfTimeInYear3 = new CalendarQuarter(2016, QuarterNumber.Third);
            var lastUnitOfTimeInYear4 = new CalendarQuarter(2016, QuarterNumber.Fourth);

            var expectedUnitsToDate1 = new List<CalendarQuarter> { lastUnitOfTimeInYear1 };
            var expectedUnitsToDate2 = new List<CalendarQuarter> { lastUnitOfTimeInYear1, lastUnitOfTimeInYear2 };
            var expectedUnitsToDate3 = new List<CalendarQuarter> { lastUnitOfTimeInYear1, lastUnitOfTimeInYear2, lastUnitOfTimeInYear3 };
            var expectedUnitsToDate4 = new List<CalendarQuarter> { lastUnitOfTimeInYear1, lastUnitOfTimeInYear2, lastUnitOfTimeInYear3, lastUnitOfTimeInYear4 };

            // Act
            var actualUnitsToDate1 = lastUnitOfTimeInYear1.GetUnitsToDate();
            var actualUnitsToDate2 = lastUnitOfTimeInYear2.GetUnitsToDate();
            var actualUnitsToDate3 = lastUnitOfTimeInYear3.GetUnitsToDate();
            var actualUnitsToDate4 = lastUnitOfTimeInYear4.GetUnitsToDate();

            // Assert
            actualUnitsToDate1.Should().Equal(expectedUnitsToDate1);
            actualUnitsToDate2.Should().Equal(expectedUnitsToDate2);
            actualUnitsToDate3.Should().Equal(expectedUnitsToDate3);
            actualUnitsToDate4.Should().Equal(expectedUnitsToDate4);
        }

        [Fact]
        public static void GetUnitsToDate___Should_return_all_quarters_from_1Q_to_lastUnitOfTimeInYear___When_lastUnitOfTimeInYear_is_of_type_FiscalQuarter()
        {
            // Arrange
            var lastUnitOfTimeInYear1 = new FiscalQuarter(2016, QuarterNumber.First);
            var lastUnitOfTimeInYear2 = new FiscalQuarter(2016, QuarterNumber.Second);
            var lastUnitOfTimeInYear3 = new FiscalQuarter(2016, QuarterNumber.Third);
            var lastUnitOfTimeInYear4 = new FiscalQuarter(2016, QuarterNumber.Fourth);

            var expectedUnitsToDate1 = new List<FiscalQuarter> { lastUnitOfTimeInYear1 };
            var expectedUnitsToDate2 = new List<FiscalQuarter> { lastUnitOfTimeInYear1, lastUnitOfTimeInYear2 };
            var expectedUnitsToDate3 = new List<FiscalQuarter> { lastUnitOfTimeInYear1, lastUnitOfTimeInYear2, lastUnitOfTimeInYear3 };
            var expectedUnitsToDate4 = new List<FiscalQuarter> { lastUnitOfTimeInYear1, lastUnitOfTimeInYear2, lastUnitOfTimeInYear3, lastUnitOfTimeInYear4 };

            // Act
            var actualUnitsToDate1 = lastUnitOfTimeInYear1.GetUnitsToDate();
            var actualUnitsToDate2 = lastUnitOfTimeInYear2.GetUnitsToDate();
            var actualUnitsToDate3 = lastUnitOfTimeInYear3.GetUnitsToDate();
            var actualUnitsToDate4 = lastUnitOfTimeInYear4.GetUnitsToDate();

            // Assert
            actualUnitsToDate1.Should().Equal(expectedUnitsToDate1);
            actualUnitsToDate2.Should().Equal(expectedUnitsToDate2);
            actualUnitsToDate3.Should().Equal(expectedUnitsToDate3);
            actualUnitsToDate4.Should().Equal(expectedUnitsToDate4);
        }

        [Fact]
        public static void GetUnitsToDate___Should_return_all_quarters_from_1Q_to_lastUnitOfTimeInYear___When_lastUnitOfTimeInYear_is_of_type_GenericQuarter()
        {
            // Arrange
            var lastUnitOfTimeInYear1 = new GenericQuarter(2016, QuarterNumber.First);
            var lastUnitOfTimeInYear2 = new GenericQuarter(2016, QuarterNumber.Second);
            var lastUnitOfTimeInYear3 = new GenericQuarter(2016, QuarterNumber.Third);
            var lastUnitOfTimeInYear4 = new GenericQuarter(2016, QuarterNumber.Fourth);

            var expectedUnitsToDate1 = new List<GenericQuarter> { lastUnitOfTimeInYear1 };
            var expectedUnitsToDate2 = new List<GenericQuarter> { lastUnitOfTimeInYear1, lastUnitOfTimeInYear2 };
            var expectedUnitsToDate3 = new List<GenericQuarter> { lastUnitOfTimeInYear1, lastUnitOfTimeInYear2, lastUnitOfTimeInYear3 };
            var expectedUnitsToDate4 = new List<GenericQuarter> { lastUnitOfTimeInYear1, lastUnitOfTimeInYear2, lastUnitOfTimeInYear3, lastUnitOfTimeInYear4 };

            // Act
            var actualUnitsToDate1 = lastUnitOfTimeInYear1.GetUnitsToDate();
            var actualUnitsToDate2 = lastUnitOfTimeInYear2.GetUnitsToDate();
            var actualUnitsToDate3 = lastUnitOfTimeInYear3.GetUnitsToDate();
            var actualUnitsToDate4 = lastUnitOfTimeInYear4.GetUnitsToDate();

            // Assert
            actualUnitsToDate1.Should().Equal(expectedUnitsToDate1);
            actualUnitsToDate2.Should().Equal(expectedUnitsToDate2);
            actualUnitsToDate3.Should().Equal(expectedUnitsToDate3);
            actualUnitsToDate4.Should().Equal(expectedUnitsToDate4);
        }

        [Fact]
        public static void GetUnitsToDate___Should_return_list_with_only_lastUnitOfTimeInYear___When_lastUnitOfTimeInYear_is_of_type_CalendarYear()
        {
            // Arrange
            var lastUnitOfTimeInYear = new CalendarYear(2016);
            var expectedUnitsToDate = new List<CalendarYear> { lastUnitOfTimeInYear };

            // Act
            var actualUnitsToDate = lastUnitOfTimeInYear.GetUnitsToDate();

            // Assert
            actualUnitsToDate.Should().Equal(expectedUnitsToDate);
        }

        [Fact]
        public static void GetUnitsToDate___Should_return_list_with_only_lastUnitOfTimeInYear___When_lastUnitOfTimeInYear_is_of_type_FiscalYear()
        {
            // Arrange
            var lastUnitOfTimeInYear = new FiscalYear(2016);
            var expectedUnitsToDate = new List<FiscalYear> { lastUnitOfTimeInYear };

            // Act
            var actualUnitsToDate = lastUnitOfTimeInYear.GetUnitsToDate();

            // Assert
            actualUnitsToDate.Should().Equal(expectedUnitsToDate);
        }

        [Fact]
        public static void GetUnitsToDate___Should_return_list_with_only_lastUnitOfTimeInYear___When_lastUnitOfTimeInYear_is_of_type_GenericYear()
        {
            // Arrange
            var lastUnitOfTimeInYear = new GenericYear(2016);
            var expectedUnitsToDate = new List<GenericYear> { lastUnitOfTimeInYear };

            // Act
            var actualUnitsToDate = lastUnitOfTimeInYear.GetUnitsToDate();

            // Assert
            actualUnitsToDate.Should().Equal(expectedUnitsToDate);
        }

        // ReSharper restore InconsistentNaming
    }
}

// ReSharper restore CheckNamespace