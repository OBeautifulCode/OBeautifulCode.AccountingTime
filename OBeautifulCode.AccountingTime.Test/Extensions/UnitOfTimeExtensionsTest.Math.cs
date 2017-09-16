// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfTimeExtensionsTest.Math.cs" company="OBeautifulCode">
//    Copyright (c) OBeautifulCode 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoFakeItEasy;

    using FakeItEasy;

    using FluentAssertions;

    using Xunit;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "There are many kinds of units-of-time.")]
    public static partial class UnitOfTimeExtensionsTest
    {
        // ReSharper disable InconsistentNaming
        [Fact]
        public static void Plus___Should_throw_ArgumentNullException___When_parameter_unitOfTime_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => UnitOfTimeExtensions.Plus(null, A.Dummy<int>()));

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
            var systemUnderTest = new CalendarQuarter(2016, QuarterNumber.Q2);

            var expectedUnitOfTime1 = new CalendarQuarter(2016, QuarterNumber.Q3);
            var expectedUnitOfTime2 = new CalendarQuarter(2016, QuarterNumber.Q4);
            var expectedUnitOfTime3 = new CalendarQuarter(2017, QuarterNumber.Q1);
            var expectedUnitOfTime4 = new CalendarQuarter(2017, QuarterNumber.Q2);
            var expectedUnitOfTime5 = new CalendarQuarter(2017, QuarterNumber.Q3);
            var expectedUnitOfTime6 = new CalendarQuarter(2017, QuarterNumber.Q4);
            var expectedUnitOfTime7 = new CalendarQuarter(2018, QuarterNumber.Q1);
            var expectedUnitOfTime8 = new CalendarQuarter(2018, QuarterNumber.Q2);

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
            var systemUnderTest = new CalendarQuarter(2016, QuarterNumber.Q2);

            var expectedUnitOfTime1 = new CalendarQuarter(2016, QuarterNumber.Q1);
            var expectedUnitOfTime2 = new CalendarQuarter(2015, QuarterNumber.Q4);
            var expectedUnitOfTime3 = new CalendarQuarter(2015, QuarterNumber.Q3);
            var expectedUnitOfTime4 = new CalendarQuarter(2015, QuarterNumber.Q2);
            var expectedUnitOfTime5 = new CalendarQuarter(2015, QuarterNumber.Q1);
            var expectedUnitOfTime6 = new CalendarQuarter(2014, QuarterNumber.Q4);
            var expectedUnitOfTime7 = new CalendarQuarter(2014, QuarterNumber.Q3);
            var expectedUnitOfTime8 = new CalendarQuarter(2014, QuarterNumber.Q2);

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
            var systemUnderTest = new FiscalQuarter(2016, QuarterNumber.Q2);

            var expectedUnitOfTime1 = new FiscalQuarter(2016, QuarterNumber.Q3);
            var expectedUnitOfTime2 = new FiscalQuarter(2016, QuarterNumber.Q4);
            var expectedUnitOfTime3 = new FiscalQuarter(2017, QuarterNumber.Q1);
            var expectedUnitOfTime4 = new FiscalQuarter(2017, QuarterNumber.Q2);
            var expectedUnitOfTime5 = new FiscalQuarter(2017, QuarterNumber.Q3);
            var expectedUnitOfTime6 = new FiscalQuarter(2017, QuarterNumber.Q4);
            var expectedUnitOfTime7 = new FiscalQuarter(2018, QuarterNumber.Q1);
            var expectedUnitOfTime8 = new FiscalQuarter(2018, QuarterNumber.Q2);

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
            var systemUnderTest = new FiscalQuarter(2016, QuarterNumber.Q2);

            var expectedUnitOfTime1 = new FiscalQuarter(2016, QuarterNumber.Q1);
            var expectedUnitOfTime2 = new FiscalQuarter(2015, QuarterNumber.Q4);
            var expectedUnitOfTime3 = new FiscalQuarter(2015, QuarterNumber.Q3);
            var expectedUnitOfTime4 = new FiscalQuarter(2015, QuarterNumber.Q2);
            var expectedUnitOfTime5 = new FiscalQuarter(2015, QuarterNumber.Q1);
            var expectedUnitOfTime6 = new FiscalQuarter(2014, QuarterNumber.Q4);
            var expectedUnitOfTime7 = new FiscalQuarter(2014, QuarterNumber.Q3);
            var expectedUnitOfTime8 = new FiscalQuarter(2014, QuarterNumber.Q2);

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
            var systemUnderTest = new GenericQuarter(2016, QuarterNumber.Q2);

            var expectedUnitOfTime1 = new GenericQuarter(2016, QuarterNumber.Q3);
            var expectedUnitOfTime2 = new GenericQuarter(2016, QuarterNumber.Q4);
            var expectedUnitOfTime3 = new GenericQuarter(2017, QuarterNumber.Q1);
            var expectedUnitOfTime4 = new GenericQuarter(2017, QuarterNumber.Q2);
            var expectedUnitOfTime5 = new GenericQuarter(2017, QuarterNumber.Q3);
            var expectedUnitOfTime6 = new GenericQuarter(2017, QuarterNumber.Q4);
            var expectedUnitOfTime7 = new GenericQuarter(2018, QuarterNumber.Q1);
            var expectedUnitOfTime8 = new GenericQuarter(2018, QuarterNumber.Q2);

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
            var systemUnderTest = new GenericQuarter(2016, QuarterNumber.Q2);

            var expectedUnitOfTime1 = new GenericQuarter(2016, QuarterNumber.Q1);
            var expectedUnitOfTime2 = new GenericQuarter(2015, QuarterNumber.Q4);
            var expectedUnitOfTime3 = new GenericQuarter(2015, QuarterNumber.Q3);
            var expectedUnitOfTime4 = new GenericQuarter(2015, QuarterNumber.Q2);
            var expectedUnitOfTime5 = new GenericQuarter(2015, QuarterNumber.Q1);
            var expectedUnitOfTime6 = new GenericQuarter(2014, QuarterNumber.Q4);
            var expectedUnitOfTime7 = new GenericQuarter(2014, QuarterNumber.Q3);
            var expectedUnitOfTime8 = new GenericQuarter(2014, QuarterNumber.Q2);

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
        public static void Plus___Should_throw_InvalidOperationException___When_parameter_unitOfTime_is_CalendarUnbounded()
        {
            // Arrange
            var expectedUnitOfTime = A.Dummy<CalendarUnbounded>();

            // Act
            var ex = Record.Exception(() => expectedUnitOfTime.Plus(A.Dummy<int>()));

            // Assert
            ex.Should().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void Plus___Should_throw_InvalidOperationException___When_parameter_unitOfTime_is_FiscalUnbounded()
        {
            // Arrange
            var expectedUnitOfTime = A.Dummy<FiscalUnbounded>();

            // Act
            var ex = Record.Exception(() => expectedUnitOfTime.Plus(A.Dummy<int>()));

            // Assert
            ex.Should().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void Plus___Should_throw_InvalidOperationException___When_parameter_unitOfTime_is_GenericUnbounded()
        {
            // Arrange
            var expectedUnitOfTime = A.Dummy<GenericUnbounded>();

            // Act
            var ex = Record.Exception(() => expectedUnitOfTime.Plus(A.Dummy<int>()));

            // Assert
            ex.Should().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void Plus_with_granularityOfUnitsToAdd___Should_throw_ArgumentNullException___When_parameter_unitOfTime_is_null()
        {
            // Arrange
            var unitsToAdd = A.Dummy<int>();
            var granularityOfUnitsToAdd = A.Dummy<UnitOfTimeGranularity>().ThatIsNot(UnitOfTimeGranularity.Unbounded);

            // Act
            var ex = Record.Exception(() => UnitOfTimeExtensions.Plus(null, unitsToAdd, granularityOfUnitsToAdd));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void Plus_with_granularityOfUnitsToAdd___Should_throw_ArgumentException___When_parameter_unitOfTime_is_unbounded()
        {
            // Arrange
            var unitOfTime = A.Dummy<UnitOfTime>().ThatIs(_ => _.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded);
            var unitsToAdd = A.Dummy<int>();
            var granularityOfUnitsToAdd = A.Dummy<UnitOfTimeGranularity>().ThatIsNot(UnitOfTimeGranularity.Unbounded);

            // Act
            var ex = Record.Exception(() => unitOfTime.Plus(unitsToAdd, granularityOfUnitsToAdd));

            // Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void Plus_with_granularityOfUnitsToAdd___Should_throw_ArgumentException___When_parameter_granularityOfUnitsToAdd_is_Invalid()
        {
            // Arrange
            var unitOfTime = A.Dummy<UnitOfTime>().ThatIs(_ => _.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded);
            var unitsToAdd = A.Dummy<int>();

            // Act
            var ex = Record.Exception(() => unitOfTime.Plus(unitsToAdd, UnitOfTimeGranularity.Invalid));

            // Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void Plus_with_granularityOfUnitsToAdd___Should_throw_ArgumentException___When_parameter_granularityOfUnitsToAdd_is_Unbounded()
        {
            // Arrange
            var unitOfTime = A.Dummy<UnitOfTime>().ThatIs(_ => _.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded);
            var unitsToAdd = A.Dummy<int>();

            // Act
            var ex = Record.Exception(() => unitOfTime.Plus(unitsToAdd, UnitOfTimeGranularity.Unbounded));

            // Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void Plus_with_granularityOfUnitsToAdd___Should_throw_ArgumentException___When_parameter_granularityOfUnitsToAdd_is_more_granular_than_parameter_unitOfTime()
        {
            // Arrange
            var tests = new[]
            {
                new { UnitOfTime = (UnitOfTime)A.Dummy<CalendarDay>(), GranularityOfUnitsToAdd = new UnitOfTimeGranularity[] { } },
                new { UnitOfTime = (UnitOfTime)A.Dummy<CalendarMonth>(), GranularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Day } },
                new { UnitOfTime = (UnitOfTime)A.Dummy<CalendarQuarter>(), GranularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Day, UnitOfTimeGranularity.Month } },
                new { UnitOfTime = (UnitOfTime)A.Dummy<CalendarYear>(), GranularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Day, UnitOfTimeGranularity.Month, UnitOfTimeGranularity.Quarter } },
                new { UnitOfTime = (UnitOfTime)A.Dummy<FiscalMonth>(), GranularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Day } },
                new { UnitOfTime = (UnitOfTime)A.Dummy<FiscalQuarter>(), GranularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Day, UnitOfTimeGranularity.Month } },
                new { UnitOfTime = (UnitOfTime)A.Dummy<FiscalYear>(), GranularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Day, UnitOfTimeGranularity.Month, UnitOfTimeGranularity.Quarter } },
                new { UnitOfTime = (UnitOfTime)A.Dummy<GenericMonth>(), GranularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Day } },
                new { UnitOfTime = (UnitOfTime)A.Dummy<GenericQuarter>(), GranularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Day, UnitOfTimeGranularity.Month } },
                new { UnitOfTime = (UnitOfTime)A.Dummy<GenericYear>(), GranularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Day, UnitOfTimeGranularity.Month, UnitOfTimeGranularity.Quarter } }
            };

            // Act
            var exceptions = new List<Exception>();
            foreach (var test in tests)
            {
                foreach (var granularityOfUnitsToAdd in test.GranularityOfUnitsToAdd)
                {
                    exceptions.Add(Record.Exception(() => test.UnitOfTime.Plus(A.Dummy<int>(), granularityOfUnitsToAdd)));
                }
            }

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<ArgumentException>());
        }

        [Fact]
        public static void Plus_with_granularityOfUnitsToAdd___Should_return_same_result_as_Plus_method_without_parameter_granularityOfUnitsToAdd___When_parameter_granularityOfUnitsToAdd_is_as_granular_as_unitOfTime()
        {
            // Arrange
            int unitsToAdd = A.Dummy<PositiveInteger>().ThatIs(_ => _ < 100);
            int unitsToSubtract = A.Dummy<NegativeInteger>().ThatIs(_ => _ > -100);
            var unitsOfTime = new List<UnitOfTime>
            {
                A.Dummy<CalendarDay>(),
                A.Dummy<CalendarMonth>(),
                A.Dummy<CalendarQuarter>(),
                A.Dummy<CalendarYear>(),
                A.Dummy<FiscalMonth>(),
                A.Dummy<FiscalQuarter>(),
                A.Dummy<FiscalYear>(),
                A.Dummy<GenericMonth>(),
                A.Dummy<GenericQuarter>(),
                A.Dummy<GenericYear>(),
            };

            // Act
            var plusMethodsReturnSameResultsWhenUnitsToAddIsPositive = unitsOfTime.Select(_ => _.Plus(unitsToAdd) == _.Plus(unitsToAdd, _.UnitOfTimeGranularity)).ToList();
            var plusMethodsReturnSameResultsWhenUnitsToAddIsNegative = unitsOfTime.Select(_ => _.Plus(unitsToSubtract) == _.Plus(unitsToSubtract, _.UnitOfTimeGranularity)).ToList();

            // Assert
            plusMethodsReturnSameResultsWhenUnitsToAddIsPositive.ForEach(_ => _.Should().BeTrue());
            plusMethodsReturnSameResultsWhenUnitsToAddIsNegative.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Plus_with_granularityOfUnitsToAdd___Should_return_same_unitOfTime___When_unitsToAdd_is_0()
        {
            // Arrange
            var tests = new[]
            {
                new { Expected = (UnitOfTime)A.Dummy<CalendarMonth>(), GranularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Month, UnitOfTimeGranularity.Quarter, UnitOfTimeGranularity.Year } },
                new { Expected = (UnitOfTime)A.Dummy<CalendarQuarter>(), GranularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Quarter, UnitOfTimeGranularity.Year } },
                new { Expected = (UnitOfTime)A.Dummy<CalendarYear>(), GranularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Year } },
                new { Expected = (UnitOfTime)A.Dummy<FiscalMonth>(), GranularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Month, UnitOfTimeGranularity.Quarter, UnitOfTimeGranularity.Year } },
                new { Expected = (UnitOfTime)A.Dummy<FiscalQuarter>(), GranularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Quarter, UnitOfTimeGranularity.Year } },
                new { Expected = (UnitOfTime)A.Dummy<FiscalYear>(), GranularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Year } },
                new { Expected = (UnitOfTime)A.Dummy<GenericMonth>(), GranularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Month, UnitOfTimeGranularity.Quarter, UnitOfTimeGranularity.Year } },
                new { Expected = (UnitOfTime)A.Dummy<GenericQuarter>(), GranularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Quarter, UnitOfTimeGranularity.Year } },
                new { Expected = (UnitOfTime)A.Dummy<GenericYear>(), GranularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Year } }
            };

            // Act, Assert
            foreach (var test in tests)
            {
                foreach (var granularityOfUnitsToAdd in test.GranularityOfUnitsToAdd)
                {
                    var actual = test.Expected.Plus(0, granularityOfUnitsToAdd);
                    actual.Should().Be(test.Expected);
                }
            }
        }

        [Fact]
        public static void Plus_with_granularityOfUnitsToAdd___Should_adjust_unitOfTime___When_granularityOfUnitsToAdd_is_less_granular_than_granularity_of_unitOfTime()
        {
            // Arrange
            var tests = new[]
            {
                new { UnitOfTime = (UnitOfTime)new CalendarMonth(2016, MonthOfYear.February), GranularityOfUnitsToAdd = UnitOfTimeGranularity.Quarter, UnitsToAdd = 1, Expected = (UnitOfTime)new CalendarMonth(2016, MonthOfYear.May) },
                new { UnitOfTime = (UnitOfTime)new CalendarMonth(2016, MonthOfYear.December), GranularityOfUnitsToAdd = UnitOfTimeGranularity.Quarter, UnitsToAdd = 2, Expected = (UnitOfTime)new CalendarMonth(2017, MonthOfYear.June) },
                new { UnitOfTime = (UnitOfTime)new CalendarMonth(2016, MonthOfYear.January), GranularityOfUnitsToAdd = UnitOfTimeGranularity.Quarter, UnitsToAdd = -1, Expected = (UnitOfTime)new CalendarMonth(2015, MonthOfYear.October) },
                new { UnitOfTime = (UnitOfTime)new CalendarMonth(2016, MonthOfYear.December), GranularityOfUnitsToAdd = UnitOfTimeGranularity.Quarter, UnitsToAdd = -2, Expected = (UnitOfTime)new CalendarMonth(2016, MonthOfYear.June) },
                new { UnitOfTime = (UnitOfTime)new CalendarMonth(2016, MonthOfYear.September), GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year, UnitsToAdd = 1, Expected = (UnitOfTime)new CalendarMonth(2017, MonthOfYear.September) },
                new { UnitOfTime = (UnitOfTime)new CalendarMonth(2016, MonthOfYear.September), GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year, UnitsToAdd = 2, Expected = (UnitOfTime)new CalendarMonth(2018, MonthOfYear.September) },
                new { UnitOfTime = (UnitOfTime)new CalendarMonth(2016, MonthOfYear.September), GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year, UnitsToAdd = -1, Expected = (UnitOfTime)new CalendarMonth(2015, MonthOfYear.September) },
                new { UnitOfTime = (UnitOfTime)new CalendarMonth(2016, MonthOfYear.September), GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year, UnitsToAdd = -2, Expected = (UnitOfTime)new CalendarMonth(2014, MonthOfYear.September) },
                new { UnitOfTime = (UnitOfTime)new FiscalMonth(2016, MonthNumber.Five), GranularityOfUnitsToAdd = UnitOfTimeGranularity.Quarter, UnitsToAdd = 1, Expected = (UnitOfTime)new FiscalMonth(2016, MonthNumber.Eight) },
                new { UnitOfTime = (UnitOfTime)new FiscalMonth(2016, MonthNumber.Eleven), GranularityOfUnitsToAdd = UnitOfTimeGranularity.Quarter, UnitsToAdd = 2, Expected = (UnitOfTime)new FiscalMonth(2017, MonthNumber.Five) },
                new { UnitOfTime = (UnitOfTime)new FiscalMonth(2016, MonthNumber.Five), GranularityOfUnitsToAdd = UnitOfTimeGranularity.Quarter, UnitsToAdd = -1, Expected = (UnitOfTime)new FiscalMonth(2016, MonthNumber.Two) },
                new { UnitOfTime = (UnitOfTime)new FiscalMonth(2016, MonthNumber.Eleven), GranularityOfUnitsToAdd = UnitOfTimeGranularity.Quarter, UnitsToAdd = -2, Expected = (UnitOfTime)new FiscalMonth(2016, MonthNumber.Five) },
                new { UnitOfTime = (UnitOfTime)new FiscalMonth(2016, MonthNumber.Five), GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year, UnitsToAdd = 1, Expected = (UnitOfTime)new FiscalMonth(2017, MonthNumber.Five) },
                new { UnitOfTime = (UnitOfTime)new FiscalMonth(2016, MonthNumber.Eleven), GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year, UnitsToAdd = 2, Expected = (UnitOfTime)new FiscalMonth(2018, MonthNumber.Eleven) },
                new { UnitOfTime = (UnitOfTime)new FiscalMonth(2016, MonthNumber.Five), GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year, UnitsToAdd = -1, Expected = (UnitOfTime)new FiscalMonth(2015, MonthNumber.Five) },
                new { UnitOfTime = (UnitOfTime)new FiscalMonth(2016, MonthNumber.Eleven), GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year, UnitsToAdd = -2, Expected = (UnitOfTime)new FiscalMonth(2014, MonthNumber.Eleven) },
                new { UnitOfTime = (UnitOfTime)new GenericMonth(2016, MonthNumber.Five), GranularityOfUnitsToAdd = UnitOfTimeGranularity.Quarter, UnitsToAdd = 1, Expected = (UnitOfTime)new GenericMonth(2016, MonthNumber.Eight) },
                new { UnitOfTime = (UnitOfTime)new GenericMonth(2016, MonthNumber.Eleven), GranularityOfUnitsToAdd = UnitOfTimeGranularity.Quarter, UnitsToAdd = 2, Expected = (UnitOfTime)new GenericMonth(2017, MonthNumber.Five) },
                new { UnitOfTime = (UnitOfTime)new GenericMonth(2016, MonthNumber.Five), GranularityOfUnitsToAdd = UnitOfTimeGranularity.Quarter, UnitsToAdd = -1, Expected = (UnitOfTime)new GenericMonth(2016, MonthNumber.Two) },
                new { UnitOfTime = (UnitOfTime)new GenericMonth(2016, MonthNumber.Eleven), GranularityOfUnitsToAdd = UnitOfTimeGranularity.Quarter, UnitsToAdd = -2, Expected = (UnitOfTime)new GenericMonth(2016, MonthNumber.Five) },
                new { UnitOfTime = (UnitOfTime)new GenericMonth(2016, MonthNumber.Five), GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year, UnitsToAdd = 1, Expected = (UnitOfTime)new GenericMonth(2017, MonthNumber.Five) },
                new { UnitOfTime = (UnitOfTime)new GenericMonth(2016, MonthNumber.Eleven), GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year, UnitsToAdd = 2, Expected = (UnitOfTime)new GenericMonth(2018, MonthNumber.Eleven) },
                new { UnitOfTime = (UnitOfTime)new GenericMonth(2016, MonthNumber.Five), GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year, UnitsToAdd = -1, Expected = (UnitOfTime)new GenericMonth(2015, MonthNumber.Five) },
                new { UnitOfTime = (UnitOfTime)new GenericMonth(2016, MonthNumber.Eleven), GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year, UnitsToAdd = -2, Expected = (UnitOfTime)new GenericMonth(2014, MonthNumber.Eleven) },
                new { UnitOfTime = (UnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q3), GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year, UnitsToAdd = 1, Expected = (UnitOfTime)new CalendarQuarter(2017, QuarterNumber.Q3) },
                new { UnitOfTime = (UnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q3), GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year, UnitsToAdd = 2, Expected = (UnitOfTime)new CalendarQuarter(2018, QuarterNumber.Q3) },
                new { UnitOfTime = (UnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q2), GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year, UnitsToAdd = -1, Expected = (UnitOfTime)new CalendarQuarter(2015, QuarterNumber.Q2) },
                new { UnitOfTime = (UnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q1), GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year, UnitsToAdd = -2, Expected = (UnitOfTime)new CalendarQuarter(2014, QuarterNumber.Q1) },
                new { UnitOfTime = (UnitOfTime)new FiscalQuarter(2016, QuarterNumber.Q3), GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year, UnitsToAdd = 1, Expected = (UnitOfTime)new FiscalQuarter(2017, QuarterNumber.Q3) },
                new { UnitOfTime = (UnitOfTime)new FiscalQuarter(2016, QuarterNumber.Q3), GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year, UnitsToAdd = 2, Expected = (UnitOfTime)new FiscalQuarter(2018, QuarterNumber.Q3) },
                new { UnitOfTime = (UnitOfTime)new FiscalQuarter(2016, QuarterNumber.Q2), GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year, UnitsToAdd = -1, Expected = (UnitOfTime)new FiscalQuarter(2015, QuarterNumber.Q2) },
                new { UnitOfTime = (UnitOfTime)new FiscalQuarter(2016, QuarterNumber.Q1), GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year, UnitsToAdd = -2, Expected = (UnitOfTime)new FiscalQuarter(2014, QuarterNumber.Q1) },
                new { UnitOfTime = (UnitOfTime)new GenericQuarter(2016, QuarterNumber.Q3), GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year, UnitsToAdd = 1, Expected = (UnitOfTime)new GenericQuarter(2017, QuarterNumber.Q3) },
                new { UnitOfTime = (UnitOfTime)new GenericQuarter(2016, QuarterNumber.Q3), GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year, UnitsToAdd = 2, Expected = (UnitOfTime)new GenericQuarter(2018, QuarterNumber.Q3) },
                new { UnitOfTime = (UnitOfTime)new GenericQuarter(2016, QuarterNumber.Q2), GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year, UnitsToAdd = -1, Expected = (UnitOfTime)new GenericQuarter(2015, QuarterNumber.Q2) },
                new { UnitOfTime = (UnitOfTime)new GenericQuarter(2016, QuarterNumber.Q1), GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year, UnitsToAdd = -2, Expected = (UnitOfTime)new GenericQuarter(2014, QuarterNumber.Q1) },
            };

            // Act, Assert
            foreach (var test in tests)
            {
                var actual = test.UnitOfTime.Plus(test.UnitsToAdd, test.GranularityOfUnitsToAdd);
                actual.Should().Be(test.Expected);
            }
        }

        [Fact]
        public static void Plus_with_granularityOfUnitsToAdd___Should_throw_NotSupportedException___When_granularity_of_unitOfTime_is_Day_and_unitOfTimeGranularity_is_less_granular_than_Day()
        {
            // Arrange
            var granularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Month, UnitOfTimeGranularity.Quarter, UnitOfTimeGranularity.Year };

            // Act
            var exceptions = new List<Exception>();
            foreach (var granularityToAdd in granularityOfUnitsToAdd)
            {
                exceptions.Add(Record.Exception(() => A.Dummy<CalendarDay>().Plus(A.Dummy<int>(), granularityToAdd)));
            }

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<NotSupportedException>());
        }

        // ReSharper restore InconsistentNaming
    }
}

// ReSharper restore CheckNamespace