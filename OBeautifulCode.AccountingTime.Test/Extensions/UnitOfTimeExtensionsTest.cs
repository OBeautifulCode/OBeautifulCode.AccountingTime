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
    using System.Linq;

    using AutoFakeItEasy;

    using FakeItEasy;

    using FluentAssertions;

    using Naos.Recipes.TupleInitializers;

    using Xunit;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "There are many kinds of units-of-time.")]
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
            var calQuarter2 = new CalendarQuarter(2013, QuarterNumber.Q2);
            var calQuarter1 = new CalendarQuarter(2013, QuarterNumber.Q1);
            var calQuarter3 = new CalendarQuarter(2013, QuarterNumber.Q3);
            var calQuarter4 = new CalendarQuarter(2013, QuarterNumber.Q4);

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

        [Fact]
        public static void GetFirstCalendarDay___Should_throw_ArgumentNullException___When_parameter_unitOfTime_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => UnitOfTimeExtensions.GetFirstCalendarDay(null));

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
            var quarter1 = new CalendarQuarter(2016, QuarterNumber.Q1);
            var quarter2 = new CalendarQuarter(2016, QuarterNumber.Q2);
            var quarter3 = new CalendarQuarter(2016, QuarterNumber.Q3);
            var quarter4 = new CalendarQuarter(2016, QuarterNumber.Q4);

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
        public static void GetFirstCalendarYear___Should_throw_InvalidOperationException___When_parameter_unitOfTime_is_of_type_CalendarUnbounded()
        {
            // Arrange
            var systemUnderTest = A.Dummy<CalendarUnbounded>();

            // Act
            var ex = Record.Exception(() => systemUnderTest.GetFirstCalendarDay());

            // Assert
            ex.Should().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void GetLastCalendarDay___Should_throw_ArgumentNullException___When_parameter_unitOfTime_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => UnitOfTimeExtensions.GetLastCalendarDay(null));

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
            var quarter1 = new CalendarQuarter(2016, QuarterNumber.Q1);
            var quarter2 = new CalendarQuarter(2016, QuarterNumber.Q2);
            var quarter3 = new CalendarQuarter(2016, QuarterNumber.Q3);
            var quarter4 = new CalendarQuarter(2016, QuarterNumber.Q4);

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
        public static void GetLastCalendarYear___Should_throw_InvalidOperationException___When_parameter_unitOfTime_is_of_type_CalendarUnbounded()
        {
            // Arrange
            var systemUnderTest = A.Dummy<CalendarUnbounded>();

            // Act
            var ex = Record.Exception(() => systemUnderTest.GetLastCalendarDay());

            // Assert
            ex.Should().BeOfType<InvalidOperationException>();
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
            var lastUnitOfTimeInYear1 = new CalendarQuarter(2016, QuarterNumber.Q1);
            var lastUnitOfTimeInYear2 = new CalendarQuarter(2016, QuarterNumber.Q2);
            var lastUnitOfTimeInYear3 = new CalendarQuarter(2016, QuarterNumber.Q3);
            var lastUnitOfTimeInYear4 = new CalendarQuarter(2016, QuarterNumber.Q4);

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
            var lastUnitOfTimeInYear1 = new FiscalQuarter(2016, QuarterNumber.Q1);
            var lastUnitOfTimeInYear2 = new FiscalQuarter(2016, QuarterNumber.Q2);
            var lastUnitOfTimeInYear3 = new FiscalQuarter(2016, QuarterNumber.Q3);
            var lastUnitOfTimeInYear4 = new FiscalQuarter(2016, QuarterNumber.Q4);

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
            var lastUnitOfTimeInYear1 = new GenericQuarter(2016, QuarterNumber.Q1);
            var lastUnitOfTimeInYear2 = new GenericQuarter(2016, QuarterNumber.Q2);
            var lastUnitOfTimeInYear3 = new GenericQuarter(2016, QuarterNumber.Q3);
            var lastUnitOfTimeInYear4 = new GenericQuarter(2016, QuarterNumber.Q4);

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

        [Fact]
        public static void SerializeToSortableString___Should_throw_ArgumentNullException___When_parameter_unitOfTime_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => UnitOfTimeExtensions.SerializeToSortableString(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void SerializeToSortableString___Should_return_expected_serialized_sortable_string_representation_of_unitOfTime___When_unitOfTime_is_a_CalendarDay()
        {
            // Arrange
            var calendarDaysBySerializedString = new Dictionary<string, UnitOfTime>
            {
                { "c-2017-01-03", new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Three) },
                { "c-2017-11-09", new CalendarDay(2017, MonthOfYear.November, DayOfMonth.Nine) },
                { "c-2017-07-21", new CalendarDay(2017, MonthOfYear.July, DayOfMonth.TwentyOne) },
                { "c-2017-10-08", new CalendarDay(2017, MonthOfYear.October, DayOfMonth.Eight) },
                { "c-2017-11-30",  new CalendarDay(2017, MonthOfYear.November, DayOfMonth.Thirty) }
            };

            // Act
            var results = calendarDaysBySerializedString.Select(_ => new { Expected = _.Key, Actual = _.Value.SerializeToSortableString() }).ToList();

            // Assert
            results.All(_ => _.Actual == _.Expected).Should().BeTrue();
        }

        [Fact]
        public static void SerializeToSortableString___Should_return_expected_serialized_sortable_string_representation_of_unitOfTime___When_unitOfTime_is_a_CalendarMonth()
        {
            // Arrange
            var calendarDaysBySerializedString = new Dictionary<string, UnitOfTime>
            {
                { "c-2017-01", new CalendarMonth(2017, MonthOfYear.January) },
                { "c-2017-07", new CalendarMonth(2017, MonthOfYear.July) },
                { "c-2017-11", new CalendarMonth(2017, MonthOfYear.November) }
            };

            // Act
            var results = calendarDaysBySerializedString.Select(_ => new { Expected = _.Key, Actual = _.Value.SerializeToSortableString() }).ToList();

            // Assert
            results.All(_ => _.Actual == _.Expected).Should().BeTrue();
        }

        [Fact]
        public static void SerializeToSortableString___Should_return_expected_serialized_sortable_string_representation_of_unitOfTime___When_unitOfTime_is_a_FiscalMonth()
        {
            // Arrange
            var calendarDaysBySerializedString = new Dictionary<string, UnitOfTime>
            {
                { "f-2017-01", new FiscalMonth(2017, MonthNumber.One) },
                { "f-2017-07", new FiscalMonth(2017, MonthNumber.Seven) },
                { "f-2017-11", new FiscalMonth(2017, MonthNumber.Eleven) }
            };

            // Act
            var results = calendarDaysBySerializedString.Select(_ => new { Expected = _.Key, Actual = _.Value.SerializeToSortableString() }).ToList();

            // Assert
            results.All(_ => _.Actual == _.Expected).Should().BeTrue();
        }

        [Fact]
        public static void SerializeToSortableString___Should_return_expected_serialized_sortable_string_representation_of_unitOfTime___When_unitOfTime_is_a_GenericMonth()
        {
            // Arrange
            var calendarDaysBySerializedString = new Dictionary<string, UnitOfTime>
            {
                { "g-2017-01", new GenericMonth(2017, MonthNumber.One) },
                { "g-2017-07", new GenericMonth(2017, MonthNumber.Seven) },
                { "g-2017-11", new GenericMonth(2017, MonthNumber.Eleven) }
            };

            // Act
            var results = calendarDaysBySerializedString.Select(_ => new { Expected = _.Key, Actual = _.Value.SerializeToSortableString() }).ToList();

            // Assert
            results.All(_ => _.Actual == _.Expected).Should().BeTrue();
        }

        [Fact]
        public static void SerializeToSortableString___Should_return_expected_serialized_sortable_string_representation_of_unitOfTime___When_unitOfTime_is_a_CalendarQuarter()
        {
            // Arrange
            var calendarDaysBySerializedString = new Dictionary<string, UnitOfTime>
            {
                { "c-2017-Q1", new CalendarQuarter(2017, QuarterNumber.Q1) },
                { "c-2017-Q2", new CalendarQuarter(2017, QuarterNumber.Q2) },
                { "c-2017-Q3", new CalendarQuarter(2017, QuarterNumber.Q3) },
                { "c-2017-Q4", new CalendarQuarter(2017, QuarterNumber.Q4) }
            };

            // Act
            var results = calendarDaysBySerializedString.Select(_ => new { Expected = _.Key, Actual = _.Value.SerializeToSortableString() }).ToList();

            // Assert
            results.All(_ => _.Actual == _.Expected).Should().BeTrue();
        }

        [Fact]
        public static void SerializeToSortableString___Should_return_expected_serialized_sortable_string_representation_of_unitOfTime___When_unitOfTime_is_a_FiscalQuarter()
        {
            // Arrange
            var calendarDaysBySerializedString = new Dictionary<string, UnitOfTime>
            {
                { "f-2017-Q1", new FiscalQuarter(2017, QuarterNumber.Q1) },
                { "f-2017-Q2", new FiscalQuarter(2017, QuarterNumber.Q2) },
                { "f-2017-Q3", new FiscalQuarter(2017, QuarterNumber.Q3) },
                { "f-2017-Q4", new FiscalQuarter(2017, QuarterNumber.Q4) }
            };

            // Act
            var results = calendarDaysBySerializedString.Select(_ => new { Expected = _.Key, Actual = _.Value.SerializeToSortableString() }).ToList();

            // Assert
            results.All(_ => _.Actual == _.Expected).Should().BeTrue();
        }

        [Fact]
        public static void SerializeToSortableString___Should_return_expected_serialized_sortable_string_representation_of_unitOfTime___When_unitOfTime_is_a_GenericQuarter()
        {
            // Arrange
            var calendarDaysBySerializedString = new Dictionary<string, UnitOfTime>
            {
                { "g-2017-Q1", new GenericQuarter(2017, QuarterNumber.Q1) },
                { "g-2017-Q2", new GenericQuarter(2017, QuarterNumber.Q2) },
                { "g-2017-Q3", new GenericQuarter(2017, QuarterNumber.Q3) },
                { "g-2017-Q4", new GenericQuarter(2017, QuarterNumber.Q4) }
            };

            // Act
            var results = calendarDaysBySerializedString.Select(_ => new { Expected = _.Key, Actual = _.Value.SerializeToSortableString() }).ToList();

            // Assert
            results.All(_ => _.Actual == _.Expected).Should().BeTrue();
        }

        [Fact]
        public static void SerializeToSortableString___Should_return_expected_serialized_sortable_string_representation_of_unitOfTime___When_unitOfTime_is_a_CalendarYear()
        {
            // Arrange
            var calendarDaysBySerializedString = new Dictionary<string, UnitOfTime>
            {
                { "c-2017", new CalendarYear(2017) },
                { "c-2009", new CalendarYear(2009) }
            };

            // Act
            var results = calendarDaysBySerializedString.Select(_ => new { Expected = _.Key, Actual = _.Value.SerializeToSortableString() }).ToList();

            // Assert
            results.All(_ => _.Actual == _.Expected).Should().BeTrue();
        }

        [Fact]
        public static void SerializeToSortableString___Should_return_expected_serialized_sortable_string_representation_of_unitOfTime___When_unitOfTime_is_a_FiscalYear()
        {
            // Arrange
            var calendarDaysBySerializedString = new Dictionary<string, UnitOfTime>
            {
                { "f-2017", new FiscalYear(2017) },
                { "f-2009", new FiscalYear(2009) }
            };

            // Act
            var results = calendarDaysBySerializedString.Select(_ => new { Expected = _.Key, Actual = _.Value.SerializeToSortableString() }).ToList();

            // Assert
            results.All(_ => _.Actual == _.Expected).Should().BeTrue();
        }

        [Fact]
        public static void SerializeToSortableString___Should_return_expected_serialized_sortable_string_representation_of_unitOfTime___When_unitOfTime_is_a_GenericYear()
        {
            // Arrange
            var calendarDaysBySerializedString = new Dictionary<string, UnitOfTime>
            {
                { "g-2017", new GenericYear(2017) },
                { "g-2009", new GenericYear(2009) }
            };

            // Act
            var results = calendarDaysBySerializedString.Select(_ => new { Expected = _.Key, Actual = _.Value.SerializeToSortableString() }).ToList();

            // Assert
            results.All(_ => _.Actual == _.Expected).Should().BeTrue();
        }

        [Fact]
        public static void SerializeToSortableString___Should_return_expected_serialized_sortable_string_representation_of_unitOfTime___When_unitOfTime_is_a_CalendarUnbounded()
        {
            // Arrange
            var calendarUnboundedBySerializedString = new Dictionary<string, UnitOfTime>
            {
                { "c-unbounded", new CalendarUnbounded() },
            };

            // Act
            var results = calendarUnboundedBySerializedString.Select(_ => new { Expected = _.Key, Actual = _.Value.SerializeToSortableString() }).ToList();

            // Assert
            results.All(_ => _.Actual == _.Expected).Should().BeTrue();
        }

        [Fact]
        public static void SerializeToSortableString___Should_return_expected_serialized_sortable_string_representation_of_unitOfTime___When_unitOfTime_is_a_FiscalUnbounded()
        {
            // Arrange
            var calendarUnboundedBySerializedString = new Dictionary<string, UnitOfTime>
            {
                { "f-unbounded", new FiscalUnbounded() },
            };

            // Act
            var results = calendarUnboundedBySerializedString.Select(_ => new { Expected = _.Key, Actual = _.Value.SerializeToSortableString() }).ToList();

            // Assert
            results.All(_ => _.Actual == _.Expected).Should().BeTrue();
        }

        [Fact]
        public static void SerializeToSortableString___Should_return_expected_serialized_sortable_string_representation_of_unitOfTime___When_unitOfTime_is_a_GenericUnbounded()
        {
            // Arrange
            var calendarUnboundedBySerializedString = new Dictionary<string, UnitOfTime>
            {
                { "g-unbounded", new GenericUnbounded() },
            };

            // Act
            var results = calendarUnboundedBySerializedString.Select(_ => new { Expected = _.Key, Actual = _.Value.SerializeToSortableString() }).ToList();

            // Assert
            results.All(_ => _.Actual == _.Expected).Should().BeTrue();
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_throw_ArgumentNullException___When_parameter_unitOfTime_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => UnitOfTimeExtensions.DeserializeFromSortableString<UnitOfTime>(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_throw_ArgumentException___When_parameter_unitOfTime_is_whitespace()
        {
            // Arrange
            var unitsOfTime = new[] { string.Empty, "  ", "  \r\n " };

            // Act
            var exceptions = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromSortableString<UnitOfTime>())).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<ArgumentException>());
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "This test is inherently complex.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "This test is inherently complex.")]
        public static void DeserializeFromSortableString___Should_throw_InvalidOperationException___When_the_kind_of_unit_of_time_encoded_cannot_be_casted_to_specified_generic_type_parameter()
        {
            // Arrange
            var unitsOfTime = new Dictionary<string, IEnumerable<Type>>
            {
                { "c-2015-11-11", Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(CalendarDay)) && (_ != typeof(CalendarUnitOfTime))) },
                { "c-2017-03", Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(CalendarMonth)) && (_ != typeof(CalendarUnitOfTime))) },
                { "c-2017-Q1", Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(CalendarQuarter)) && (_ != typeof(CalendarUnitOfTime))) },
                { "c-2017", Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(CalendarYear)) && (_ != typeof(CalendarUnitOfTime))) },
                { "c-unbounded", Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(CalendarUnbounded)) && (_ != typeof(CalendarUnitOfTime))) },
                { "f-2017-03", Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(FiscalMonth)) && (_ != typeof(FiscalUnitOfTime))) },
                { "f-2017-Q1", Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(FiscalQuarter)) && (_ != typeof(FiscalUnitOfTime))) },
                { "f-2017", Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(FiscalYear)) && (_ != typeof(FiscalUnitOfTime))) },
                { "f-unbounded", Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(FiscalUnbounded)) && (_ != typeof(FiscalUnitOfTime))) },
                { "g-2017-03", Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(GenericQuarter)) && (_ != typeof(GenericUnitOfTime))) },
                { "g-2017-Q1", Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(GenericQuarter)) && (_ != typeof(GenericUnitOfTime))) },
                { "g-2017",  Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(GenericYear)) && (_ != typeof(GenericUnitOfTime))) },
                { "g-unbounded", Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(GenericUnbounded)) && (_ != typeof(GenericUnitOfTime))) }
            };

            var deserializeFromSortableString = typeof(UnitOfTimeExtensions).GetMethod(nameof(UnitOfTimeExtensions.DeserializeFromSortableString));

            // Act
            var exceptions = new List<Exception>();
            foreach (var unitOfTime in unitsOfTime.Keys)
            {
                foreach (var type in unitsOfTime[unitOfTime])
                {
                    var genericMethod = deserializeFromSortableString.MakeGenericMethod(type);
                    // ReSharper disable PossibleNullReferenceException
                    exceptions.Add(Record.Exception(() => genericMethod.Invoke(null, new object[] { unitOfTime })).InnerException);
                    // ReSharper restore PossibleNullReferenceException
                }
            }

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_throw_InvalidOperationException___When_unitOfTime_is_a_malformed_CalendarDay()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                "c-201a-11-11",
                "c-xxxx-11-11",
                "c-10000-11-11",
                "c-T001-11-11",
                "c-0-11-11",
                "c-200-11-11",
                "c-0000-11-11",
                "c-999-11-11",
                "c-2007-1-11",
                "c-2007-9-11",
                "c-2007-13-11",
                "c-2007-99-11",
                "c-2007-00-11",
                "c-2007-001-11",
                "c-2007-012-11",
                "c-2007-11-1",
                "c-2007-11-9",
                "c-2007-11-32",
                "c-2007-11-31",
                "c-2015-02-29",
                "c-2015-03-00",
                "c-2015-03-001",
                "c-2015-03-030"
            };

            // Act
            var exceptions = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromSortableString<UnitOfTime>())).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_throw_InvalidOperationException___When_unitOfTime_is_a_malformed_CalendarMonth()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                "c-201a-11",
                "c-xxxx-11",
                "c-10000-11",
                "c-T001-11",
                "c-0-11",
                "c-200-11",
                "c-0000-11",
                "c-999-11",
                "c-2007-1",
                "c-2007-9",
                "c-2007-13",
                "c-2007-99",
                "c-2007-00",
                "c-2007-001",
                "c-2007-012"
            };

            // Act
            var exceptions = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromSortableString<UnitOfTime>())).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_throw_InvalidOperationException___When_unitOfTime_is_a_malformed_FiscalMonth()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                "f-201a-11",
                "f-xxxx-11",
                "f-10000-11",
                "f-T001-11",
                "f-0-11",
                "f-200-11",
                "f-0000-11",
                "f-999-11",
                "f-2007-1",
                "f-2007-9",
                "f-2007-13",
                "f-2007-99",
                "f-2007-00",
                "f-2007-001",
                "f-2007-012"
            };

            // Act
            var exceptions = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromSortableString<UnitOfTime>())).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_throw_InvalidOperationException___When_unitOfTime_is_a_malformed_GenericMonth()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                "g-201a-11",
                "g-xxxx-11",
                "g-10000-11",
                "g-T001-11",
                "g-0-11",
                "g-200-11",
                "g-0000-11",
                "g-999-11",
                "g-2007-1",
                "g-2007-9",
                "g-2007-13",
                "g-2007-99",
                "g-2007-00",
                "g-2007-001",
                "g-2007-012"
            };

            // Act
            var exceptions = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromSortableString<UnitOfTime>())).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_throw_InvalidOperationException___When_unitOfTime_is_a_malformed_CalendarQuarter()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                "c-201a-Q3",
                "c-xxxx-Q3",
                "c-10000-Q3",
                "c-T001-Q3",
                "c-0-Q3",
                "c-200-Q3",
                "c-0000-Q3",
                "c-999-Q3",
                "c-2007-Q01",
                "c-2007-Q00",
                "c-2007-Q004",
                "c-2007-Q5",
                "c-2007-Q31",
                "c-2007-1",
                "c-2007Q-1"
            };

            // Act
            var exceptions = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromSortableString<UnitOfTime>())).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_throw_InvalidOperationException___When_unitOfTime_is_a_malformed_FiscalQuarter()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                "f-201a-Q3",
                "f-xxxx-Q3",
                "f-10000-Q3",
                "f-T001-Q3",
                "f-0-Q3",
                "f-200-Q3",
                "f-0000-Q3",
                "f-999-Q3",
                "f-2007-Q01",
                "f-2007-Q00",
                "f-2007-Q004",
                "f-2007-Q5",
                "f-2007-Q31",
                "f-2007-1",
                "f-2007Q-1"
            };

            // Act
            var exceptions = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromSortableString<UnitOfTime>())).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_throw_InvalidOperationException___When_unitOfTime_is_a_malformed_GenericQuarter()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                "g-201a-Q3",
                "g-xxxx-Q3",
                "g-10000-Q3",
                "g-T001-Q3",
                "g-0-Q3",
                "g-200-Q3",
                "g-0000-Q3",
                "g-999-Q3",
                "g-2007-Q01",
                "g-2007-Q00",
                "g-2007-Q004",
                "g-2007-Q5",
                "g-2007-Q31",
                "g-2007-1",
                "g-2007Q-1"
            };

            // Act
            var exceptions = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromSortableString<UnitOfTime>())).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_throw_InvalidOperationException___When_unitOfTime_is_a_malformed_CalendarYear()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                "c-201a",
                "c-xxxx",
                "c-10000",
                "c-T001",
                "c-0",
                "c-200",
                "c-0000",
                "c-999"
            };

            // Act
            var exceptions = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromSortableString<UnitOfTime>())).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_throw_InvalidOperationException___When_unitOfTime_is_a_malformed_FiscalYear()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                "f-201a",
                "f-xxxx",
                "f-10000",
                "f-T001",
                "f-0",
                "f-200",
                "f-0000",
                "f-999"
            };

            // Act
            var exceptions = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromSortableString<UnitOfTime>())).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_throw_InvalidOperationException___When_unitOfTime_is_a_malformed_GenericYear()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                "g-201a",
                "g-xxxx",
                "g-10000",
                "g-T001",
                "g-0",
                "g-200",
                "g-0000",
                "g-999"
            };

            // Act
            var exceptions = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromSortableString<UnitOfTime>())).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_throw_InvalidOperationException___When_unitOfTime_is_a_malformed_CalendarUnbounded()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                "c-unbounded-",
                "c-unbounded--",
                "cunbounded",
                "cu-unbounded"
            };

            // Act
            var exceptions = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromSortableString<UnitOfTime>())).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_throw_InvalidOperationException___When_unitOfTime_is_a_malformed_FiscalUnbounded()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                "f-unbounded-",
                "f-unbounded--",
                "funbounded",
                "fu-unbounded"
            };

            // Act
            var exceptions = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromSortableString<UnitOfTime>())).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_throw_InvalidOperationException___When_unitOfTime_is_a_malformed_GenericUnbounded()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                "g-unbounded-",
                "g-unbounded--",
                "gunbounded",
                "gu-unbounded"
            };

            // Act
            var exceptions = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromSortableString<UnitOfTime>())).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_deserialize_a_CalendarDay___When_unitOfTime_is_a_well_formed_CalendarDay()
        {
            // Arrange
            var unitsOfTime = new Dictionary<string, CalendarDay>
            {
                { "c-2001-01-09", new CalendarDay(2001, MonthOfYear.January, DayOfMonth.Nine) },
                { "c-2016-02-29", new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine) },
                { "c-2001-11-04", new CalendarDay(2001, MonthOfYear.November, DayOfMonth.Four) },
                { "c-2001-12-30", new CalendarDay(2001, MonthOfYear.December, DayOfMonth.Thirty) }
            };

            // Act
            var deserialized1 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<UnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized2 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<CalendarUnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized3 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<CalendarDay>(), Expected = _.Value }).ToList();

            // Assert
            deserialized1.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized2.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized3.ForEach(_ => _.Actual.Should().Be(_.Expected));
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_deserialize_a_CalendarMonth___When_unitOfTime_is_a_well_formed_CalendarMonth()
        {
            // Arrange
            var unitsOfTime = new Dictionary<string, CalendarMonth>
            {
                { "c-2001-01", new CalendarMonth(2001, MonthOfYear.January) },
                { "c-2002-07", new CalendarMonth(2002, MonthOfYear.July) },
                { "c-2010-11", new CalendarMonth(2010, MonthOfYear.November) },
                { "c-2016-12", new CalendarMonth(2016, MonthOfYear.December) }
            };

            // Act
            var deserialized1 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<UnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized2 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<CalendarUnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized3 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<CalendarMonth>(), Expected = _.Value }).ToList();

            // Assert
            deserialized1.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized2.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized3.ForEach(_ => _.Actual.Should().Be(_.Expected));
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_deserialize_a_FiscalMonth___When_unitOfTime_is_a_well_formed_FiscalMonth()
        {
            // Arrange
            var unitsOfTime = new Dictionary<string, FiscalMonth>
            {
                { "f-2001-01", new FiscalMonth(2001, MonthNumber.One) },
                { "f-2002-07", new FiscalMonth(2002, MonthNumber.Seven) },
                { "f-2010-11", new FiscalMonth(2010, MonthNumber.Eleven) },
                { "f-2016-12", new FiscalMonth(2016, MonthNumber.Twelve) }
            };

            // Act
            var deserialized1 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<UnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized2 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<FiscalUnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized3 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<FiscalMonth>(), Expected = _.Value }).ToList();

            // Assert
            deserialized1.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized2.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized3.ForEach(_ => _.Actual.Should().Be(_.Expected));
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_deserialize_a_GenericMonth___When_unitOfTime_is_a_well_formed_GenericMonth()
        {
            // Arrange
            var unitsOfTime = new Dictionary<string, GenericMonth>
            {
                { "g-2001-01", new GenericMonth(2001, MonthNumber.One) },
                { "g-2002-07", new GenericMonth(2002, MonthNumber.Seven) },
                { "g-2010-11", new GenericMonth(2010, MonthNumber.Eleven) },
                { "g-2016-12", new GenericMonth(2016, MonthNumber.Twelve) }
            };

            // Act
            var deserialized1 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<UnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized2 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<GenericUnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized3 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<GenericMonth>(), Expected = _.Value }).ToList();

            // Assert
            deserialized1.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized2.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized3.ForEach(_ => _.Actual.Should().Be(_.Expected));
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_deserialize_a_CalendarQuarter___When_unitOfTime_is_a_well_formed_CalendarQuarter()
        {
            // Arrange
            var unitsOfTime = new Dictionary<string, CalendarQuarter>
            {
                { "c-2001-Q1", new CalendarQuarter(2001, QuarterNumber.Q1) },
                { "c-2002-Q2", new CalendarQuarter(2002, QuarterNumber.Q2) },
                { "c-2010-Q3", new CalendarQuarter(2010, QuarterNumber.Q3) },
                { "c-2016-Q4", new CalendarQuarter(2016, QuarterNumber.Q4) }
            };

            // Act
            var deserialized1 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<UnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized2 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<CalendarUnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized3 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<CalendarQuarter>(), Expected = _.Value }).ToList();

            // Assert
            deserialized1.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized2.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized3.ForEach(_ => _.Actual.Should().Be(_.Expected));
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_deserialize_a_FiscalQuarter___When_unitOfTime_is_a_well_formed_FiscalQuarter()
        {
            // Arrange
            var unitsOfTime = new Dictionary<string, FiscalQuarter>
            {
                { "f-2001-Q1", new FiscalQuarter(2001, QuarterNumber.Q1) },
                { "f-2002-Q2", new FiscalQuarter(2002, QuarterNumber.Q2) },
                { "f-2010-Q3", new FiscalQuarter(2010, QuarterNumber.Q3) },
                { "f-2016-Q4", new FiscalQuarter(2016, QuarterNumber.Q4) }
            };

            // Act
            var deserialized1 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<UnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized2 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<FiscalUnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized3 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<FiscalQuarter>(), Expected = _.Value }).ToList();

            // Assert
            deserialized1.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized2.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized3.ForEach(_ => _.Actual.Should().Be(_.Expected));
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_deserialize_a_GenericQuarter___When_unitOfTime_is_a_well_formed_GenericQuarter()
        {
            // Arrange
            var unitsOfTime = new Dictionary<string, GenericQuarter>
            {
                { "g-2001-Q1", new GenericQuarter(2001, QuarterNumber.Q1) },
                { "g-2002-Q2", new GenericQuarter(2002, QuarterNumber.Q2) },
                { "g-2010-Q3", new GenericQuarter(2010, QuarterNumber.Q3) },
                { "g-2016-Q4", new GenericQuarter(2016, QuarterNumber.Q4) }
            };

            // Act
            var deserialized1 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<UnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized2 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<GenericUnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized3 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<GenericQuarter>(), Expected = _.Value }).ToList();

            // Assert
            deserialized1.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized2.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized3.ForEach(_ => _.Actual.Should().Be(_.Expected));
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_deserialize_a_CalendarYear___When_unitOfTime_is_a_well_formed_CalendarYear()
        {
            // Arrange
            var unitsOfTime = new Dictionary<string, CalendarYear>
            {
                { "c-2001", new CalendarYear(2001) },
                { "c-2016", new CalendarYear(2016) }
            };

            // Act
            var deserialized1 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<UnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized2 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<CalendarUnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized3 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<CalendarYear>(), Expected = _.Value }).ToList();

            // Assert
            deserialized1.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized2.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized3.ForEach(_ => _.Actual.Should().Be(_.Expected));
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_deserialize_a_FiscalYear___When_unitOfTime_is_a_well_formed_FiscalYear()
        {
            // Arrange
            var unitsOfTime = new Dictionary<string, FiscalYear>
            {
                { "f-2001", new FiscalYear(2001) },
                { "f-2016", new FiscalYear(2016) }
            };

            // Act
            var deserialized1 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<UnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized2 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<FiscalUnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized3 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<FiscalYear>(), Expected = _.Value }).ToList();

            // Assert
            deserialized1.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized2.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized3.ForEach(_ => _.Actual.Should().Be(_.Expected));
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_deserialize_a_GenericYear___When_unitOfTime_is_a_well_formed_GenericYear()
        {
            // Arrange
            var unitsOfTime = new Dictionary<string, GenericYear>
            {
                { "g-2001", new GenericYear(2001) },
                { "g-2016", new GenericYear(2016) }
            };

            // Act
            var deserialized1 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<UnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized2 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<GenericUnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized3 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<GenericYear>(), Expected = _.Value }).ToList();

            // Assert
            deserialized1.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized2.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized3.ForEach(_ => _.Actual.Should().Be(_.Expected));
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_deserialize_a_CalendarUnbounded___When_unitOfTime_is_a_well_formed_CalendarUnbounded()
        {
            // Arrange
            var unitsOfTime = new Dictionary<string, CalendarUnbounded>
            {
                { "c-unbounded", new CalendarUnbounded() }
            };

            // Act
            var deserialized1 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<UnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized2 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<CalendarUnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized3 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<CalendarUnbounded>(), Expected = _.Value }).ToList();

            // Assert
            deserialized1.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized2.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized3.ForEach(_ => _.Actual.Should().Be(_.Expected));
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_deserialize_a_FiscalUnbounded___When_unitOfTime_is_a_well_formed_FiscalUnbounded()
        {
            // Arrange
            var unitsOfTime = new Dictionary<string, FiscalUnbounded>
            {
                { "f-unbounded", new FiscalUnbounded() }
            };

            // Act
            var deserialized1 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<UnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized2 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<FiscalUnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized3 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<FiscalUnbounded>(), Expected = _.Value }).ToList();

            // Assert
            deserialized1.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized2.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized3.ForEach(_ => _.Actual.Should().Be(_.Expected));
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_deserialize_a_GenericUnbounded___When_unitOfTime_is_a_well_formed_GenericUnbounded()
        {
            // Arrange
            var unitsOfTime = new Dictionary<string, GenericUnbounded>
            {
                { "g-unbounded", new GenericUnbounded() }
            };

            // Act
            var deserialized1 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<UnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized2 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<GenericUnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized3 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<GenericUnbounded>(), Expected = _.Value }).ToList();

            // Assert
            deserialized1.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized2.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized3.ForEach(_ => _.Actual.Should().Be(_.Expected));
        }

        // ReSharper restore InconsistentNaming
    }
}

// ReSharper restore CheckNamespace