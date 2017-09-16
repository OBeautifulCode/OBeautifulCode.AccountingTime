// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriodExtensionsTest.Comparison.cs" company="OBeautifulCode">
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

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Testing this class requires lots of types because of the number of unit-of-time types intersected with the options for reporting period.")]
    public static partial class ReportingPeriodExtensionsTest
    {
        // ReSharper disable InconsistentNaming
        [Fact]
        public static void Contains_with_unitOfTime___Should_throw_ArgumentNullException___When_parameter_reportingPeriod_is_null()
        {
            // Arrange
            var unitOfTime = A.Dummy<UnitOfTime>();

            // Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.Contains(null, unitOfTime));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_throw_ArgumentNullException___When_parameter_unitOfTime_is_null()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod<UnitOfTime>>();
            UnitOfTime unitOfTime = null;

            // Act
            // ReSharper disable ExpressionIsAlwaysNull
            var ex = Record.Exception(() => reportingPeriod.Contains(unitOfTime));
            // ReSharper restore ExpressionIsAlwaysNull

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_throw_ArgumentException___When_parameter_unitOfTime_has_a_different_UnitOfTimeKind_than_reportingPeriod()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod<UnitOfTime>>();
            var unitOfTime = A.Dummy<UnitOfTime>().Whose(_ => _.UnitOfTimeKind != reportingPeriod.GetUnitOfTimeKind());

            // Act
            var ex = Record.Exception(() => reportingPeriod.Contains(unitOfTime));

            // Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_false___When_parameter_unitOfTime_is_not_contained_within_reportingPeriod_and_both_represent_CalendarDay()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Thirty));
            var reportingPeriod2 = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight));

            var unitOfTime1a = new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentySeven);
            var unitOfTime1b = new CalendarDay(2016, MonthOfYear.March, DayOfMonth.ThirtyOne);
            var unitOfTime1c = new CalendarDay(2016, MonthOfYear.February, DayOfMonth.Ten);
            var unitOfTime1d = new CalendarDay(2017, MonthOfYear.March, DayOfMonth.One);
            var unitOfTime1e = new CalendarDay(2015, MonthOfYear.March, DayOfMonth.One);

            var unitOfTime2a = new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentySeven);
            var unitOfTime2b = new CalendarDay(2016, MonthOfYear.March, DayOfMonth.ThirtyOne);
            var unitOfTime2c = new CalendarDay(2017, MonthOfYear.March, DayOfMonth.TwentyEight);
            var unitOfTime2d = new CalendarDay(2016, MonthOfYear.March, DayOfMonth.TwentyEight);

            // Act
            var contains1a = reportingPeriod1.Contains(unitOfTime1a);
            var contains1b = reportingPeriod1.Contains(unitOfTime1b);
            var contains1c = reportingPeriod1.Contains(unitOfTime1c);
            var contains1d = reportingPeriod1.Contains(unitOfTime1d);
            var contains1e = reportingPeriod1.Contains(unitOfTime1e);

            var contains2a = reportingPeriod2.Contains(unitOfTime2a);
            var contains2b = reportingPeriod2.Contains(unitOfTime2b);
            var contains2c = reportingPeriod2.Contains(unitOfTime2c);
            var contains2d = reportingPeriod2.Contains(unitOfTime2d);

            // Assert
            contains1a.Should().BeFalse();
            contains1b.Should().BeFalse();
            contains1c.Should().BeFalse();
            contains1d.Should().BeFalse();
            contains1e.Should().BeFalse();

            contains2a.Should().BeFalse();
            contains2b.Should().BeFalse();
            contains2c.Should().BeFalse();
            contains2d.Should().BeFalse();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_contained_within_reportingPeriod_and_both_represent_CalendarDay()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Thirty));
            var reportingPeriod2 = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight));

            var unitOfTime1a = new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight);
            var unitOfTime1b = new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Thirty);
            var unitOfTime1c = new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine);
            var unitOfTime1d = new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Ten);

            var unitOfTime2 = new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight);

            // Act
            var contains1a = reportingPeriod1.Contains(unitOfTime1a);
            var contains1b = reportingPeriod1.Contains(unitOfTime1b);
            var contains1c = reportingPeriod1.Contains(unitOfTime1c);
            var contains1d = reportingPeriod1.Contains(unitOfTime1d);

            var contains2 = reportingPeriod2.Contains(unitOfTime2);

            // Assert
            contains1a.Should().BeTrue();
            contains1b.Should().BeTrue();
            contains1c.Should().BeTrue();
            contains1d.Should().BeTrue();

            contains2.Should().BeTrue();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_false___When_parameter_unitOfTime_is_not_contained_within_reportingPeriod_and_both_represent_CalendarMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.September), new CalendarMonth(2017, MonthOfYear.February));
            var reportingPeriod2 = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.September), new CalendarMonth(2016, MonthOfYear.September));

            var unitOfTime1a = new CalendarMonth(2016, MonthOfYear.August);
            var unitOfTime1b = new CalendarMonth(2017, MonthOfYear.March);
            var unitOfTime1c = new CalendarMonth(2017, MonthOfYear.October);

            var unitOfTime2a = new CalendarMonth(2016, MonthOfYear.August);
            var unitOfTime2b = new CalendarMonth(2016, MonthOfYear.October);
            var unitOfTime2c = new CalendarMonth(2017, MonthOfYear.September);
            var unitOfTime2d = new CalendarMonth(2015, MonthOfYear.September);

            // Act
            var contains1a = reportingPeriod1.Contains(unitOfTime1a);
            var contains1b = reportingPeriod1.Contains(unitOfTime1b);
            var contains1c = reportingPeriod1.Contains(unitOfTime1c);
            var contains2a = reportingPeriod2.Contains(unitOfTime2a);
            var contains2b = reportingPeriod2.Contains(unitOfTime2b);
            var contains2c = reportingPeriod2.Contains(unitOfTime2c);
            var contains2d = reportingPeriod2.Contains(unitOfTime2d);

            // Assert
            contains1a.Should().BeFalse();
            contains1b.Should().BeFalse();
            contains1c.Should().BeFalse();

            contains2a.Should().BeFalse();
            contains2b.Should().BeFalse();
            contains2c.Should().BeFalse();
            contains2d.Should().BeFalse();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_contained_within_reportingPeriod_and_both_represent_CalendarMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.September), new CalendarMonth(2017, MonthOfYear.February));
            var reportingPeriod2 = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.September), new CalendarMonth(2016, MonthOfYear.September));

            var unitOfTime1a = new CalendarMonth(2016, MonthOfYear.September);
            var unitOfTime1b = new CalendarMonth(2017, MonthOfYear.February);
            var unitOfTime1c = new CalendarMonth(2016, MonthOfYear.December);

            var unitOfTime2 = new CalendarMonth(2016, MonthOfYear.September);

            // Act
            var contains1a = reportingPeriod1.Contains(unitOfTime1a);
            var contains1b = reportingPeriod1.Contains(unitOfTime1b);
            var contains1c = reportingPeriod1.Contains(unitOfTime1c);

            var contains2 = reportingPeriod2.Contains(unitOfTime2);

            // Assert
            contains1a.Should().BeTrue();
            contains1b.Should().BeTrue();
            contains1c.Should().BeTrue();

            contains2.Should().BeTrue();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_false___When_parameter_unitOfTime_is_not_contained_within_reportingPeriod_and_both_represent_FiscalMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Nine), new FiscalMonth(2017, MonthNumber.Two));
            var reportingPeriod2 = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Nine), new FiscalMonth(2016, MonthNumber.Nine));

            var unitOfTime1a = new FiscalMonth(2016, MonthNumber.Eight);
            var unitOfTime1b = new FiscalMonth(2017, MonthNumber.Three);
            var unitOfTime1c = new FiscalMonth(2017, MonthNumber.Ten);

            var unitOfTime2a = new FiscalMonth(2016, MonthNumber.Eight);
            var unitOfTime2b = new FiscalMonth(2016, MonthNumber.Ten);
            var unitOfTime2c = new FiscalMonth(2017, MonthNumber.Nine);
            var unitOfTime2d = new FiscalMonth(2015, MonthNumber.Nine);

            // Act
            var contains1a = reportingPeriod1.Contains(unitOfTime1a);
            var contains1b = reportingPeriod1.Contains(unitOfTime1b);
            var contains1c = reportingPeriod1.Contains(unitOfTime1c);

            var contains2a = reportingPeriod2.Contains(unitOfTime2a);
            var contains2b = reportingPeriod2.Contains(unitOfTime2b);
            var contains2c = reportingPeriod2.Contains(unitOfTime2c);
            var contains2d = reportingPeriod2.Contains(unitOfTime2d);

            // Assert
            contains1a.Should().BeFalse();
            contains1b.Should().BeFalse();
            contains1c.Should().BeFalse();

            contains2a.Should().BeFalse();
            contains2b.Should().BeFalse();
            contains2c.Should().BeFalse();
            contains2d.Should().BeFalse();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_contained_within_reportingPeriod_and_both_represent_FiscalMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Nine), new FiscalMonth(2017, MonthNumber.Two));
            var reportingPeriod2 = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Nine), new FiscalMonth(2016, MonthNumber.Nine));

            var unitOfTime1a = new FiscalMonth(2016, MonthNumber.Nine);
            var unitOfTime1b = new FiscalMonth(2017, MonthNumber.Two);
            var unitOfTime1c = new FiscalMonth(2016, MonthNumber.Twelve);

            var unitOfTime2 = new FiscalMonth(2016, MonthNumber.Nine);

            // Act
            var contains1a = reportingPeriod1.Contains(unitOfTime1a);
            var contains1b = reportingPeriod1.Contains(unitOfTime1b);
            var contains1c = reportingPeriod1.Contains(unitOfTime1c);

            var contains2 = reportingPeriod2.Contains(unitOfTime2);

            // Assert
            contains1a.Should().BeTrue();
            contains1b.Should().BeTrue();
            contains1c.Should().BeTrue();

            contains2.Should().BeTrue();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_false___When_parameter_unitOfTime_is_not_contained_within_reportingPeriod_and_both_represent_GenericMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Nine), new GenericMonth(2017, MonthNumber.Two));
            var reportingPeriod2 = new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Nine), new GenericMonth(2016, MonthNumber.Nine));

            var unitOfTime1a = new GenericMonth(2016, MonthNumber.Eight);
            var unitOfTime1b = new GenericMonth(2017, MonthNumber.Three);
            var unitOfTime1c = new GenericMonth(2017, MonthNumber.Ten);

            var unitOfTime2a = new GenericMonth(2016, MonthNumber.Eight);
            var unitOfTime2b = new GenericMonth(2016, MonthNumber.Ten);
            var unitOfTime2c = new GenericMonth(2017, MonthNumber.Nine);
            var unitOfTime2d = new GenericMonth(2015, MonthNumber.Nine);

            // Act
            var contains1a = reportingPeriod1.Contains(unitOfTime1a);
            var contains1b = reportingPeriod1.Contains(unitOfTime1b);
            var contains1c = reportingPeriod1.Contains(unitOfTime1c);
            var contains2a = reportingPeriod2.Contains(unitOfTime2a);
            var contains2b = reportingPeriod2.Contains(unitOfTime2b);
            var contains2c = reportingPeriod2.Contains(unitOfTime2c);
            var contains2d = reportingPeriod2.Contains(unitOfTime2d);

            // Assert
            contains1a.Should().BeFalse();
            contains1b.Should().BeFalse();
            contains1c.Should().BeFalse();

            contains2a.Should().BeFalse();
            contains2b.Should().BeFalse();
            contains2c.Should().BeFalse();
            contains2d.Should().BeFalse();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_contained_within_reportingPeriod_and_both_represent_GenericMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Nine), new GenericMonth(2017, MonthNumber.Two));
            var reportingPeriod2 = new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Nine), new GenericMonth(2016, MonthNumber.Nine));

            var unitOfTime1a = new GenericMonth(2016, MonthNumber.Nine);
            var unitOfTime1b = new GenericMonth(2017, MonthNumber.Two);
            var unitOfTime1c = new GenericMonth(2016, MonthNumber.Twelve);

            var unitOfTime2 = new GenericMonth(2016, MonthNumber.Nine);

            // Act
            var contains1a = reportingPeriod1.Contains(unitOfTime1a);
            var contains1b = reportingPeriod1.Contains(unitOfTime1b);
            var contains1c = reportingPeriod1.Contains(unitOfTime1c);

            var contains2 = reportingPeriod2.Contains(unitOfTime2);

            // Assert
            contains1a.Should().BeTrue();
            contains1b.Should().BeTrue();
            contains1c.Should().BeTrue();

            contains2.Should().BeTrue();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_false___When_parameter_unitOfTime_is_not_contained_within_reportingPeriod_and_both_represent_CalendarQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2017, QuarterNumber.Q3));
            var reportingPeriod2 = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q2));

            var unitOfTime1a = new CalendarQuarter(2016, QuarterNumber.Q1);
            var unitOfTime1b = new CalendarQuarter(2017, QuarterNumber.Q4);
            var unitOfTime1c = new CalendarQuarter(2015, QuarterNumber.Q2);

            var unitOfTime2a = new CalendarQuarter(2016, QuarterNumber.Q1);
            var unitOfTime2b = new CalendarQuarter(2016, QuarterNumber.Q3);
            var unitOfTime2c = new CalendarQuarter(2017, QuarterNumber.Q2);
            var unitOfTime2d = new CalendarQuarter(2015, QuarterNumber.Q2);

            // Act
            var contains1a = reportingPeriod1.Contains(unitOfTime1a);
            var contains1b = reportingPeriod1.Contains(unitOfTime1b);
            var contains1c = reportingPeriod1.Contains(unitOfTime1c);

            var contains2a = reportingPeriod2.Contains(unitOfTime2a);
            var contains2b = reportingPeriod2.Contains(unitOfTime2b);
            var contains2c = reportingPeriod2.Contains(unitOfTime2c);
            var contains2d = reportingPeriod2.Contains(unitOfTime2d);

            // Assert
            contains1a.Should().BeFalse();
            contains1b.Should().BeFalse();
            contains1c.Should().BeFalse();

            contains2a.Should().BeFalse();
            contains2b.Should().BeFalse();
            contains2c.Should().BeFalse();
            contains2d.Should().BeFalse();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_contained_within_reportingPeriod_and_both_represent_CalendarQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2017, QuarterNumber.Q3));
            var reportingPeriod2 = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q2));

            var unitOfTime1a = new CalendarQuarter(2016, QuarterNumber.Q2);
            var unitOfTime1b = new CalendarQuarter(2016, QuarterNumber.Q3);
            var unitOfTime1c = new CalendarQuarter(2016, QuarterNumber.Q4);
            var unitOfTime1d = new CalendarQuarter(2017, QuarterNumber.Q1);
            var unitOfTime1e = new CalendarQuarter(2017, QuarterNumber.Q2);
            var unitOfTime1f = new CalendarQuarter(2017, QuarterNumber.Q3);

            var unitOfTime2 = new CalendarQuarter(2016, QuarterNumber.Q2);

            // Act
            var contains1a = reportingPeriod1.Contains(unitOfTime1a);
            var contains1b = reportingPeriod1.Contains(unitOfTime1b);
            var contains1c = reportingPeriod1.Contains(unitOfTime1c);
            var contains1d = reportingPeriod1.Contains(unitOfTime1d);
            var contains1e = reportingPeriod1.Contains(unitOfTime1e);
            var contains1f = reportingPeriod1.Contains(unitOfTime1f);

            var contains2 = reportingPeriod2.Contains(unitOfTime2);

            // Assert
            contains1a.Should().BeTrue();
            contains1b.Should().BeTrue();
            contains1c.Should().BeTrue();
            contains1d.Should().BeTrue();
            contains1e.Should().BeTrue();
            contains1f.Should().BeTrue();

            contains2.Should().BeTrue();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_false___When_parameter_unitOfTime_is_not_contained_within_reportingPeriod_and_both_represent_FiscalQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2017, QuarterNumber.Q3));
            var reportingPeriod2 = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q2));

            var unitOfTime1a = new FiscalQuarter(2016, QuarterNumber.Q1);
            var unitOfTime1b = new FiscalQuarter(2017, QuarterNumber.Q4);
            var unitOfTime1c = new FiscalQuarter(2015, QuarterNumber.Q2);

            var unitOfTime2a = new FiscalQuarter(2016, QuarterNumber.Q1);
            var unitOfTime2b = new FiscalQuarter(2016, QuarterNumber.Q3);
            var unitOfTime2c = new FiscalQuarter(2017, QuarterNumber.Q2);
            var unitOfTime2d = new FiscalQuarter(2015, QuarterNumber.Q2);

            // Act
            var contains1a = reportingPeriod1.Contains(unitOfTime1a);
            var contains1b = reportingPeriod1.Contains(unitOfTime1b);
            var contains1c = reportingPeriod1.Contains(unitOfTime1c);

            var contains2a = reportingPeriod2.Contains(unitOfTime2a);
            var contains2b = reportingPeriod2.Contains(unitOfTime2b);
            var contains2c = reportingPeriod2.Contains(unitOfTime2c);
            var contains2d = reportingPeriod2.Contains(unitOfTime2d);

            // Assert
            contains1a.Should().BeFalse();
            contains1b.Should().BeFalse();
            contains1c.Should().BeFalse();

            contains2a.Should().BeFalse();
            contains2b.Should().BeFalse();
            contains2c.Should().BeFalse();
            contains2d.Should().BeFalse();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_contained_within_reportingPeriod_and_both_represent_FiscalQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2017, QuarterNumber.Q3));
            var reportingPeriod2 = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q2));

            var unitOfTime1a = new FiscalQuarter(2016, QuarterNumber.Q2);
            var unitOfTime1b = new FiscalQuarter(2016, QuarterNumber.Q3);
            var unitOfTime1c = new FiscalQuarter(2016, QuarterNumber.Q4);
            var unitOfTime1d = new FiscalQuarter(2017, QuarterNumber.Q1);
            var unitOfTime1e = new FiscalQuarter(2017, QuarterNumber.Q2);
            var unitOfTime1f = new FiscalQuarter(2017, QuarterNumber.Q3);

            var unitOfTime2 = new FiscalQuarter(2016, QuarterNumber.Q2);

            // Act
            var contains1a = reportingPeriod1.Contains(unitOfTime1a);
            var contains1b = reportingPeriod1.Contains(unitOfTime1b);
            var contains1c = reportingPeriod1.Contains(unitOfTime1c);
            var contains1d = reportingPeriod1.Contains(unitOfTime1d);
            var contains1e = reportingPeriod1.Contains(unitOfTime1e);
            var contains1f = reportingPeriod1.Contains(unitOfTime1f);

            var contains2 = reportingPeriod2.Contains(unitOfTime2);

            // Assert
            contains1a.Should().BeTrue();
            contains1b.Should().BeTrue();
            contains1c.Should().BeTrue();
            contains1d.Should().BeTrue();
            contains1e.Should().BeTrue();
            contains1f.Should().BeTrue();

            contains2.Should().BeTrue();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_false___When_parameter_unitOfTime_is_not_contained_within_reportingPeriod_and_both_represent_GenericQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2017, QuarterNumber.Q3));
            var reportingPeriod2 = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q2));

            var unitOfTime1a = new GenericQuarter(2016, QuarterNumber.Q1);
            var unitOfTime1b = new GenericQuarter(2017, QuarterNumber.Q4);
            var unitOfTime1c = new GenericQuarter(2015, QuarterNumber.Q2);

            var unitOfTime2a = new GenericQuarter(2016, QuarterNumber.Q1);
            var unitOfTime2b = new GenericQuarter(2016, QuarterNumber.Q3);
            var unitOfTime2c = new GenericQuarter(2017, QuarterNumber.Q2);
            var unitOfTime2d = new GenericQuarter(2015, QuarterNumber.Q2);

            // Act
            var contains1a = reportingPeriod1.Contains(unitOfTime1a);
            var contains1b = reportingPeriod1.Contains(unitOfTime1b);
            var contains1c = reportingPeriod1.Contains(unitOfTime1c);

            var contains2a = reportingPeriod2.Contains(unitOfTime2a);
            var contains2b = reportingPeriod2.Contains(unitOfTime2b);
            var contains2c = reportingPeriod2.Contains(unitOfTime2c);
            var contains2d = reportingPeriod2.Contains(unitOfTime2d);

            // Assert
            contains1a.Should().BeFalse();
            contains1b.Should().BeFalse();
            contains1c.Should().BeFalse();

            contains2a.Should().BeFalse();
            contains2b.Should().BeFalse();
            contains2c.Should().BeFalse();
            contains2d.Should().BeFalse();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_contained_within_reportingPeriod_and_both_represent_GenericQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2017, QuarterNumber.Q3));
            var reportingPeriod2 = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q2));

            var unitOfTime1a = new GenericQuarter(2016, QuarterNumber.Q2);
            var unitOfTime1b = new GenericQuarter(2016, QuarterNumber.Q3);
            var unitOfTime1c = new GenericQuarter(2016, QuarterNumber.Q4);
            var unitOfTime1d = new GenericQuarter(2017, QuarterNumber.Q1);
            var unitOfTime1e = new GenericQuarter(2017, QuarterNumber.Q2);
            var unitOfTime1f = new GenericQuarter(2017, QuarterNumber.Q3);

            var unitOfTime2 = new GenericQuarter(2016, QuarterNumber.Q2);

            // Act
            var contains1a = reportingPeriod1.Contains(unitOfTime1a);
            var contains1b = reportingPeriod1.Contains(unitOfTime1b);
            var contains1c = reportingPeriod1.Contains(unitOfTime1c);
            var contains1d = reportingPeriod1.Contains(unitOfTime1d);
            var contains1e = reportingPeriod1.Contains(unitOfTime1e);
            var contains1f = reportingPeriod1.Contains(unitOfTime1f);

            var contains2 = reportingPeriod2.Contains(unitOfTime2);

            // Assert
            contains1a.Should().BeTrue();
            contains1b.Should().BeTrue();
            contains1c.Should().BeTrue();
            contains1d.Should().BeTrue();
            contains1e.Should().BeTrue();
            contains1f.Should().BeTrue();

            contains2.Should().BeTrue();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_false___When_parameter_unitOfTime_is_not_contained_within_reportingPeriod_and_both_represent_CalendarYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2018));
            var reportingPeriod2 = new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2016));

            var unitOfTime1a = new CalendarYear(2015);
            var unitOfTime1b = new CalendarYear(2019);

            var unitOfTime2a = new CalendarYear(2015);
            var unitOfTime2b = new CalendarYear(2017);

            // Act
            var contains1a = reportingPeriod1.Contains(unitOfTime1a);
            var contains1b = reportingPeriod1.Contains(unitOfTime1b);

            var contains2a = reportingPeriod2.Contains(unitOfTime2a);
            var contains2b = reportingPeriod2.Contains(unitOfTime2b);

            // Assert
            contains1a.Should().BeFalse();
            contains1b.Should().BeFalse();

            contains2a.Should().BeFalse();
            contains2b.Should().BeFalse();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_contained_within_reportingPeriod_and_both_represent_CalendarYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2018));
            var reportingPeriod2 = new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2016));

            var unitOfTime1a = new CalendarYear(2016);
            var unitOfTime1b = new CalendarYear(2017);
            var unitOfTime1c = new CalendarYear(2018);

            var unitOfTime2 = new CalendarYear(2016);

            // Act
            var contains1a = reportingPeriod1.Contains(unitOfTime1a);
            var contains1b = reportingPeriod1.Contains(unitOfTime1b);
            var contains1c = reportingPeriod1.Contains(unitOfTime1c);

            var contains2 = reportingPeriod2.Contains(unitOfTime2);

            // Assert
            contains1a.Should().BeTrue();
            contains1b.Should().BeTrue();
            contains1c.Should().BeTrue();

            contains2.Should().BeTrue();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_false___When_parameter_unitOfTime_is_not_contained_within_reportingPeriod_and_both_represent_FiscalYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2018));
            var reportingPeriod2 = new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2016));

            var unitOfTime1a = new FiscalYear(2015);
            var unitOfTime1b = new FiscalYear(2019);

            var unitOfTime2a = new FiscalYear(2015);
            var unitOfTime2b = new FiscalYear(2017);

            // Act
            var contains1a = reportingPeriod1.Contains(unitOfTime1a);
            var contains1b = reportingPeriod1.Contains(unitOfTime1b);
            var contains2a = reportingPeriod2.Contains(unitOfTime2a);
            var contains2b = reportingPeriod2.Contains(unitOfTime2b);

            // Assert
            contains1a.Should().BeFalse();
            contains1b.Should().BeFalse();

            contains2a.Should().BeFalse();
            contains2b.Should().BeFalse();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_contained_within_reportingPeriod_and_both_represent_FiscalYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2018));
            var reportingPeriod2 = new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2016));

            var unitOfTime1a = new FiscalYear(2016);
            var unitOfTime1b = new FiscalYear(2017);
            var unitOfTime1c = new FiscalYear(2018);

            var unitOfTime2 = new FiscalYear(2016);

            // Act
            var contains1a = reportingPeriod1.Contains(unitOfTime1a);
            var contains1b = reportingPeriod1.Contains(unitOfTime1b);
            var contains1c = reportingPeriod1.Contains(unitOfTime1c);

            var contains2 = reportingPeriod2.Contains(unitOfTime2);

            // Assert
            contains1a.Should().BeTrue();
            contains1b.Should().BeTrue();
            contains1c.Should().BeTrue();

            contains2.Should().BeTrue();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_false___When_parameter_unitOfTime_is_not_contained_within_reportingPeriod_and_both_represent_GenericYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2018));
            var reportingPeriod2 = new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2016));

            var unitOfTime1a = new GenericYear(2015);
            var unitOfTime1b = new GenericYear(2019);

            var unitOfTime2a = new GenericYear(2015);
            var unitOfTime2b = new GenericYear(2017);

            // Act
            var contains1a = reportingPeriod1.Contains(unitOfTime1a);
            var contains1b = reportingPeriod1.Contains(unitOfTime1b);

            var contains2a = reportingPeriod2.Contains(unitOfTime2a);
            var contains2b = reportingPeriod2.Contains(unitOfTime2b);

            // Assert
            contains1a.Should().BeFalse();
            contains1b.Should().BeFalse();

            contains2a.Should().BeFalse();
            contains2b.Should().BeFalse();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_contained_within_reportingPeriod_and_both_represent_GenericYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2018));
            var reportingPeriod2 = new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2016));

            var unitOfTime1a = new GenericYear(2016);
            var unitOfTime1b = new GenericYear(2017);
            var unitOfTime1c = new GenericYear(2018);

            var unitOfTime2 = new GenericYear(2016);

            // Act
            var contains1a = reportingPeriod1.Contains(unitOfTime1a);
            var contains1b = reportingPeriod1.Contains(unitOfTime1b);
            var contains1c = reportingPeriod1.Contains(unitOfTime1c);

            var contains2 = reportingPeriod2.Contains(unitOfTime2);

            // Assert
            contains1a.Should().BeTrue();
            contains1b.Should().BeTrue();
            contains1c.Should().BeTrue();

            contains2.Should().BeTrue();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_not_contained_within_reportingPeriod_and_both_represent_CalendarUnbounded()
        {
            // Arrange
            var reportingPeriod = new ReportingPeriod<CalendarUnbounded>(new CalendarUnbounded(), new CalendarUnbounded());
            var unitOfTime = new CalendarUnbounded();

            // Act
            var result = reportingPeriod.Contains(unitOfTime);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_not_contained_within_reportingPeriod_and_both_represent_FiscalUnbounded()
        {
            // Arrange
            var reportingPeriod = new ReportingPeriod<FiscalUnbounded>(new FiscalUnbounded(), new FiscalUnbounded());
            var unitOfTime = new FiscalUnbounded();

            // Act
            var result = reportingPeriod.Contains(unitOfTime);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_not_contained_within_reportingPeriod_and_both_represent_GenericUnbounded()
        {
            // Arrange
            var reportingPeriod = new ReportingPeriod<GenericUnbounded>(new GenericUnbounded(), new GenericUnbounded());
            var unitOfTime = new GenericUnbounded();

            // Act
            var result = reportingPeriod.Contains(unitOfTime);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_false___When_parameter_unitOfTime_is_not_contained_within_a_CalendarDay_reportingPeriod_and_unitOfTime_is_not_a_CalendarDay()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.February, DayOfMonth.Two),
                        new CalendarDay(2016, MonthOfYear.March, DayOfMonth.ThirtyOne)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2016, MonthOfYear.February)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.February, DayOfMonth.One),
                        new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Thirty)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2016, MonthOfYear.March)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.February, DayOfMonth.One),
                        new CalendarDay(2016, MonthOfYear.March, DayOfMonth.ThirtyOne)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2016, MonthOfYear.January)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.February, DayOfMonth.One),
                        new CalendarDay(2016, MonthOfYear.March, DayOfMonth.ThirtyOne)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2016, MonthOfYear.April)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Thirty)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2016, MonthOfYear.March)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Thirty)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2016, MonthOfYear.April)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Thirty)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2017, MonthOfYear.January)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Two),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2016, MonthOfYear.March)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2016, MonthOfYear.January)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2015, MonthOfYear.March)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.January, DayOfMonth.Two),
                        new CalendarDay(2016, MonthOfYear.September, DayOfMonth.Thirty)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.January, DayOfMonth.One),
                        new CalendarDay(2016, MonthOfYear.September, DayOfMonth.TwentyNine)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q3)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.January, DayOfMonth.One),
                        new CalendarDay(2016, MonthOfYear.September, DayOfMonth.Thirty)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2015, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.January, DayOfMonth.One),
                        new CalendarDay(2016, MonthOfYear.September, DayOfMonth.Thirty)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarDay(2016, MonthOfYear.September, DayOfMonth.TwentyNine)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q3)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarDay(2016, MonthOfYear.September, DayOfMonth.Thirty)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarDay(2016, MonthOfYear.September, DayOfMonth.Thirty)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2017, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.October, DayOfMonth.Two),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.October, DayOfMonth.One),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q3)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.October, DayOfMonth.One),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2015, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.January, DayOfMonth.One),
                        new CalendarDay(2017, MonthOfYear.December, DayOfMonth.Thirty)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.January, DayOfMonth.Two),
                        new CalendarDay(2017, MonthOfYear.December, DayOfMonth.ThirtyOne)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.January, DayOfMonth.One),
                        new CalendarDay(2017, MonthOfYear.December, DayOfMonth.ThirtyOne)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2015)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.January, DayOfMonth.One),
                        new CalendarDay(2017, MonthOfYear.December, DayOfMonth.ThirtyOne)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2018)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarDay(2017, MonthOfYear.December, DayOfMonth.Thirty)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarDay(2017, MonthOfYear.December, DayOfMonth.ThirtyOne)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2018)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Two),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2017, MonthOfYear.December, DayOfMonth.ThirtyOne),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.January, DayOfMonth.One),
                        new CalendarDay(2017, MonthOfYear.December, DayOfMonth.ThirtyOne)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarUnbounded()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarDay(2017, MonthOfYear.December, DayOfMonth.ThirtyOne)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarUnbounded()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2017, MonthOfYear.December, DayOfMonth.ThirtyOne),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarUnbounded()
                }
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_contained_within_a_CalendarDay_reportingPeriod_and_unitOfTime_is_not_a_CalendarDay()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.February, DayOfMonth.Two),
                        new CalendarDay(2016, MonthOfYear.March, DayOfMonth.ThirtyOne)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2016, MonthOfYear.March)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.February, DayOfMonth.One),
                        new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Thirty)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2016, MonthOfYear.February)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarDay(2016, MonthOfYear.March, DayOfMonth.ThirtyOne)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2016, MonthOfYear.March)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarDay(2016, MonthOfYear.March, DayOfMonth.ThirtyOne)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2016, MonthOfYear.January)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarDay(2016, MonthOfYear.March, DayOfMonth.ThirtyOne)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2015, MonthOfYear.March)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2016, MonthOfYear.March)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2016, MonthOfYear.April)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2017, MonthOfYear.January)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.January, DayOfMonth.One),
                        new CalendarDay(2016, MonthOfYear.September, DayOfMonth.TwentyNine)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.January, DayOfMonth.Two),
                        new CalendarDay(2016, MonthOfYear.September, DayOfMonth.TwentyNine)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.January, DayOfMonth.One),
                        new CalendarDay(2016, MonthOfYear.September, DayOfMonth.Thirty)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q3)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarDay(2016, MonthOfYear.September, DayOfMonth.Thirty)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q3)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarDay(2016, MonthOfYear.September, DayOfMonth.Thirty)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarDay(2016, MonthOfYear.September, DayOfMonth.Thirty)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2015, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.October, DayOfMonth.One),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.October, DayOfMonth.One),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2017, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.October, DayOfMonth.One),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2017, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.January, DayOfMonth.One),
                        new CalendarDay(2017, MonthOfYear.December, DayOfMonth.Thirty)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.January, DayOfMonth.Two),
                        new CalendarDay(2017, MonthOfYear.December, DayOfMonth.ThirtyOne)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarDay(2017, MonthOfYear.December, DayOfMonth.ThirtyOne)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarDay(2017, MonthOfYear.December, DayOfMonth.ThirtyOne)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2018)
                }
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_false___When_parameter_unitOfTime_is_not_contained_within_a_CalendarMonth_reportingPeriod_and_unitOfTime_is_not_a_CalendarMonth()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.February),
                        new CalendarMonth(2016, MonthOfYear.March)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2016, MonthOfYear.January, DayOfMonth.ThirtyOne)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.February),
                        new CalendarMonth(2016, MonthOfYear.March)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2016, MonthOfYear.April, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.February),
                        new CalendarMonth(2016, MonthOfYear.March)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2017, MonthOfYear.February, DayOfMonth.Ten)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.February),
                        new CalendarMonth(2016, MonthOfYear.March)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2015, MonthOfYear.February, DayOfMonth.Ten)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarMonth(2016, MonthOfYear.March)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2016, MonthOfYear.April, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarMonth(2016, MonthOfYear.March)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2017, MonthOfYear.March, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.March),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.March),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2015, MonthOfYear.March, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.February),
                        new CalendarMonth(2016, MonthOfYear.June)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.January),
                        new CalendarMonth(2016, MonthOfYear.May)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.January),
                        new CalendarMonth(2016, MonthOfYear.June)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q3)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.January),
                        new CalendarMonth(2016, MonthOfYear.June)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2015, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.January),
                        new CalendarMonth(2016, MonthOfYear.June)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2015, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.January),
                        new CalendarMonth(2016, MonthOfYear.June)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2017, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarMonth(2016, MonthOfYear.June)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q3)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarMonth(2016, MonthOfYear.June)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2017, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.April),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.April),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2015, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.January),
                        new CalendarMonth(2017, MonthOfYear.November)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.February),
                        new CalendarMonth(2017, MonthOfYear.December)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.January),
                        new CalendarMonth(2017, MonthOfYear.December)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2015)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.January),
                        new CalendarMonth(2017, MonthOfYear.December)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2018)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarMonth(2017, MonthOfYear.December)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2018)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.January),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2015)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarMonth(2017, MonthOfYear.December)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarUnbounded()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2017, MonthOfYear.December),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarUnbounded()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.January),
                        new CalendarMonth(2017, MonthOfYear.December)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarUnbounded()
                },
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_contained_within_a_CalendarMonth_reportingPeriod_and_unitOfTime_is_not_a_CalendarMonth()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.February),
                        new CalendarMonth(2016, MonthOfYear.March)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2016, MonthOfYear.February, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.February),
                        new CalendarMonth(2016, MonthOfYear.March)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2016, MonthOfYear.March, DayOfMonth.ThirtyOne)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.February),
                        new CalendarMonth(2016, MonthOfYear.March)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarMonth(2016, MonthOfYear.March)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2016, MonthOfYear.March, DayOfMonth.ThirtyOne)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarMonth(2016, MonthOfYear.March)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarMonth(2016, MonthOfYear.March)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2015, MonthOfYear.December, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.March),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.March),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2016, MonthOfYear.April, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.March),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.February),
                        new CalendarMonth(2016, MonthOfYear.June)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.January),
                        new CalendarMonth(2016, MonthOfYear.May)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.February),
                        new CalendarMonth(2016, MonthOfYear.August)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarMonth(2016, MonthOfYear.August)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarMonth(2016, MonthOfYear.August)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarMonth(2016, MonthOfYear.August)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2015, QuarterNumber.Q3)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.May),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q3)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.May),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.May),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2017, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.January),
                        new CalendarMonth(2017, MonthOfYear.November)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.February),
                        new CalendarMonth(2017, MonthOfYear.December)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarMonth(2017, MonthOfYear.November)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarMonth(2017, MonthOfYear.December)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.January),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.March),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2017)
                },
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_false___When_parameter_unitOfTime_is_not_contained_within_a_CalendarQuarter_reportingPeriod_and_unitOfTime_is_not_a_CalendarQuarter()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Thirty)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2017, MonthOfYear.October, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2018, MonthOfYear.April, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2015, MonthOfYear.April, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2017, MonthOfYear.October, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2018, MonthOfYear.July, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2017, QuarterNumber.Q3),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2017, MonthOfYear.June, DayOfMonth.Thirty)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2017, QuarterNumber.Q3),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2016, MonthOfYear.August, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2016, MonthOfYear.March)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2017, MonthOfYear.October)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2018, MonthOfYear.July)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2015, MonthOfYear.July)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2017, MonthOfYear.October)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2018, MonthOfYear.July)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2016, MonthOfYear.March)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2015, MonthOfYear.June)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2018)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2018)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2019)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2015)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarUnbounded()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarUnbounded()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarUnbounded()
                },
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_contained_within_a_CalendarQuarter_reportingPeriod_and_unitOfTime_is_not_a_CalendarQuarter()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2016, MonthOfYear.April, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2017, MonthOfYear.September, DayOfMonth.Thirty)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2017, MonthOfYear.April, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2017, MonthOfYear.September, DayOfMonth.Thirty)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2017, MonthOfYear.June, DayOfMonth.Thirty)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2017, QuarterNumber.Q3),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2017, MonthOfYear.July, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2017, QuarterNumber.Q3),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2017, MonthOfYear.October, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2016, MonthOfYear.April)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2017, MonthOfYear.September)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2016, MonthOfYear.December)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2017, MonthOfYear.September)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2017, MonthOfYear.April)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2016, MonthOfYear.April)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2016, MonthOfYear.August)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q1),
                        new CalendarQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarQuarter(2018, QuarterNumber.Q4)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2018)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q1),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2016)
                }
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_false___When_parameter_unitOfTime_is_not_contained_within_a_CalendarYear_reportingPeriod_and_unitOfTime_is_not_a_CalendarYear()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2016),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2015, MonthOfYear.December, DayOfMonth.ThirtyOne)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2016),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2019, MonthOfYear.January, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2019, MonthOfYear.January, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2018),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2017, MonthOfYear.December, DayOfMonth.ThirtyOne)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2016),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2019, MonthOfYear.January)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2016),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2015, MonthOfYear.December)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2019, MonthOfYear.January)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2018),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2017, MonthOfYear.December)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2016),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2015, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2016),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2019, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2019, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2018),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2017, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2016),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarUnbounded()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarUnbounded()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2016),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarUnbounded()
                }
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_contained_within_a_CalendarYear_reportingPeriod_and_unitOfTime_is_not_a_CalendarYear()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2016),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2016, MonthOfYear.January, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2016),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2018, MonthOfYear.December, DayOfMonth.ThirtyOne)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2016),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2017, MonthOfYear.June, DayOfMonth.Fifteen)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2018, MonthOfYear.December, DayOfMonth.ThirtyOne)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2017, MonthOfYear.June, DayOfMonth.Fifteen)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2018),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2018, MonthOfYear.January, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2018),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2019, MonthOfYear.June, DayOfMonth.Fifteen)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2016),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2016, MonthOfYear.January)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2016),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2018, MonthOfYear.December)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2016),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2017, MonthOfYear.June)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2018, MonthOfYear.December)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2018, MonthOfYear.January)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2017, MonthOfYear.June)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2018),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2018, MonthOfYear.December)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2018),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2018, MonthOfYear.January)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2018),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2019, MonthOfYear.June)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2016),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2016),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2018, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2016),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2017, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2018, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2018, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2017, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2018),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2018, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2018),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2018, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2018),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2019, QuarterNumber.Q2)
                }
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_reportingPeriod_has_CalendarUnbounded_Start_and_End()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)A.Dummy<CalendarDay>()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)A.Dummy<CalendarMonth>()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)A.Dummy<CalendarQuarter>()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)A.Dummy<CalendarYear>()
                }
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_false___When_parameter_unitOfTime_is_not_contained_within_a_FiscalMonth_reportingPeriod_and_unitOfTime_is_not_a_FiscalMonth()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.Two),
                        new FiscalMonth(2016, MonthNumber.Six)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2016, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.One),
                        new FiscalMonth(2016, MonthNumber.Five)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2016, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.One),
                        new FiscalMonth(2016, MonthNumber.Six)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2016, QuarterNumber.Q3)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.One),
                        new FiscalMonth(2016, MonthNumber.Six)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2015, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.One),
                        new FiscalMonth(2016, MonthNumber.Six)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2015, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.One),
                        new FiscalMonth(2016, MonthNumber.Six)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2017, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalMonth(2016, MonthNumber.Six)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2016, QuarterNumber.Q3)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalMonth(2016, MonthNumber.Six)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2017, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.Four),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2016, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.Four),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2015, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.One),
                        new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.Two),
                        new FiscalMonth(2017, MonthNumber.Twelve)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.One),
                        new FiscalMonth(2017, MonthNumber.Twelve)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2015)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.One),
                        new FiscalMonth(2017, MonthNumber.Twelve)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2018)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalMonth(2017, MonthNumber.Twelve)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2018)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.One),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2015)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalMonth(2017, MonthNumber.Twelve)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalUnbounded()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2017, MonthNumber.Twelve),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalUnbounded()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.One),
                        new FiscalMonth(2017, MonthNumber.Twelve)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalUnbounded()
                },
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_contained_within_a_FiscalMonth_reportingPeriod_and_unitOfTime_is_not_a_FiscalMonth()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.Two),
                        new FiscalMonth(2016, MonthNumber.Six)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2016, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.One),
                        new FiscalMonth(2016, MonthNumber.Five)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2016, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.Two),
                        new FiscalMonth(2016, MonthNumber.Eight)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2016, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalMonth(2016, MonthNumber.Eight)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2016, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalMonth(2016, MonthNumber.Eight)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2016, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalMonth(2016, MonthNumber.Eight)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2015, QuarterNumber.Q3)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.Five),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2016, QuarterNumber.Q3)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.Five),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2016, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.Five),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2017, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.One),
                        new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.Two),
                        new FiscalMonth(2017, MonthNumber.Twelve)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalMonth(2017, MonthNumber.Twelve)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.One),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.Three),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2017)
                },
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_false___When_parameter_unitOfTime_is_not_contained_within_a_FiscalQuarter_reportingPeriod_and_unitOfTime_is_not_a_FiscalQuarter()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalQuarter(2016, QuarterNumber.Q2),
                        new FiscalQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2016, MonthNumber.Three)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalQuarter(2016, QuarterNumber.Q2),
                        new FiscalQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2017, MonthNumber.Ten)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalQuarter(2016, QuarterNumber.Q2),
                        new FiscalQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2018, MonthNumber.Seven)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalQuarter(2016, QuarterNumber.Q2),
                        new FiscalQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2015, MonthNumber.Seven)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2017, MonthNumber.Ten)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2018, MonthNumber.Seven)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalQuarter(2016, QuarterNumber.Q2),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2016, MonthNumber.Three)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalQuarter(2016, QuarterNumber.Q2),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2015, MonthNumber.Six)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalQuarter(2016, QuarterNumber.Q2),
                        new FiscalQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalQuarter(2016, QuarterNumber.Q2),
                        new FiscalQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2018)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2018)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2019)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalQuarter(2016, QuarterNumber.Q2),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalQuarter(2016, QuarterNumber.Q2),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2015)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalQuarter(2016, QuarterNumber.Q2),
                        new FiscalQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalUnbounded()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalUnbounded()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalQuarter(2016, QuarterNumber.Q2),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalUnbounded()
                },
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_contained_within_a_FiscalQuarter_reportingPeriod_and_unitOfTime_is_not_a_FiscalQuarter()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalQuarter(2016, QuarterNumber.Q2),
                        new FiscalQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2016, MonthNumber.Four)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalQuarter(2016, QuarterNumber.Q2),
                        new FiscalQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2017, MonthNumber.Nine)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalQuarter(2016, QuarterNumber.Q2),
                        new FiscalQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2016, MonthNumber.Twelve)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2017, MonthNumber.Nine)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2017, MonthNumber.Four)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalQuarter(2016, QuarterNumber.Q2),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2016, MonthNumber.Four)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalQuarter(2016, QuarterNumber.Q2),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2016, MonthNumber.Eight)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalQuarter(2016, QuarterNumber.Q2),
                        new FiscalQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalQuarter(2016, QuarterNumber.Q1),
                        new FiscalQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalQuarter(2018, QuarterNumber.Q4)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2018)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalQuarter(2016, QuarterNumber.Q2),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalQuarter(2016, QuarterNumber.Q1),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2016)
                }
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_false___When_parameter_unitOfTime_is_not_contained_within_a_FiscalYear_reportingPeriod_and_unitOfTime_is_not_a_FiscalYear()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalYear(2016),
                        new FiscalYear(2018)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2019, MonthNumber.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalYear(2016),
                        new FiscalYear(2018)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2015, MonthNumber.Twelve)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalYear(2018)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2019, MonthNumber.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalYear(2018),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2017, MonthNumber.Twelve)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalYear(2016),
                        new FiscalYear(2018)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2015, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalYear(2016),
                        new FiscalYear(2018)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2019, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalYear(2018)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2019, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalYear(2018),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2017, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalYear(2016),
                        new FiscalYear(2018)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalUnbounded()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalYear(2018)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalUnbounded()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalYear(2016),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalUnbounded()
                }
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_contained_within_a_FiscalYear_reportingPeriod_and_unitOfTime_is_not_a_FiscalYear()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalYear(2016),
                        new FiscalYear(2018)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2016, MonthNumber.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalYear(2016),
                        new FiscalYear(2018)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2018, MonthNumber.Twelve)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalYear(2016),
                        new FiscalYear(2018)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2017, MonthNumber.Six)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalYear(2018)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2018, MonthNumber.Twelve)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalYear(2018)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2018, MonthNumber.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalYear(2018)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2017, MonthNumber.Six)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalYear(2018),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2018, MonthNumber.Twelve)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalYear(2018),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2018, MonthNumber.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalYear(2018),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2019, MonthNumber.Six)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalYear(2016),
                        new FiscalYear(2018)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2016, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalYear(2016),
                        new FiscalYear(2018)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2018, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalYear(2016),
                        new FiscalYear(2018)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2017, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalYear(2018)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2018, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalYear(2018)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2018, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalYear(2018)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2017, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalYear(2018),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2018, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalYear(2018),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2018, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalYear(2018),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2019, QuarterNumber.Q2)
                }
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_reportingPeriod_has_FiscalUnbounded_Start_and_End()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)A.Dummy<FiscalMonth>()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)A.Dummy<FiscalQuarter>()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)A.Dummy<FiscalYear>()
                }
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_false___When_parameter_unitOfTime_is_not_contained_within_a_GenericMonth_reportingPeriod_and_unitOfTime_is_not_a_GenericMonth()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.Two),
                        new GenericMonth(2016, MonthNumber.Six)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2016, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.One),
                        new GenericMonth(2016, MonthNumber.Five)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2016, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.One),
                        new GenericMonth(2016, MonthNumber.Six)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2016, QuarterNumber.Q3)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.One),
                        new GenericMonth(2016, MonthNumber.Six)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2015, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.One),
                        new GenericMonth(2016, MonthNumber.Six)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2015, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.One),
                        new GenericMonth(2016, MonthNumber.Six)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2017, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericMonth(2016, MonthNumber.Six)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2016, QuarterNumber.Q3)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericMonth(2016, MonthNumber.Six)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2017, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.Four),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2016, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.Four),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2015, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.One),
                        new GenericMonth(2017, MonthNumber.Eleven)),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.Two),
                        new GenericMonth(2017, MonthNumber.Twelve)),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.One),
                        new GenericMonth(2017, MonthNumber.Twelve)),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2015)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.One),
                        new GenericMonth(2017, MonthNumber.Twelve)),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2018)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericMonth(2017, MonthNumber.Twelve)),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2018)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.One),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2015)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericMonth(2017, MonthNumber.Twelve)),
                    UnitOfTime = (GenericUnitOfTime)new GenericUnbounded()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2017, MonthNumber.Twelve),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericUnbounded()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.One),
                        new GenericMonth(2017, MonthNumber.Twelve)),
                    UnitOfTime = (GenericUnitOfTime)new GenericUnbounded()
                },
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_contained_within_a_GenericMonth_reportingPeriod_and_unitOfTime_is_not_a_GenericMonth()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.Two),
                        new GenericMonth(2016, MonthNumber.Six)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2016, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.One),
                        new GenericMonth(2016, MonthNumber.Five)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2016, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.Two),
                        new GenericMonth(2016, MonthNumber.Eight)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2016, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericMonth(2016, MonthNumber.Eight)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2016, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericMonth(2016, MonthNumber.Eight)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2016, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericMonth(2016, MonthNumber.Eight)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2015, QuarterNumber.Q3)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.Five),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2016, QuarterNumber.Q3)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.Five),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2016, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.Five),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2017, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.One),
                        new GenericMonth(2017, MonthNumber.Eleven)),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.Two),
                        new GenericMonth(2017, MonthNumber.Twelve)),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericMonth(2017, MonthNumber.Eleven)),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericMonth(2017, MonthNumber.Twelve)),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.One),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.Three),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2017)
                },
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_false___When_parameter_unitOfTime_is_not_contained_within_a_GenericQuarter_reportingPeriod_and_unitOfTime_is_not_a_GenericQuarter()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericQuarter(2016, QuarterNumber.Q2),
                        new GenericQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2016, MonthNumber.Three)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericQuarter(2016, QuarterNumber.Q2),
                        new GenericQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2017, MonthNumber.Ten)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericQuarter(2016, QuarterNumber.Q2),
                        new GenericQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2018, MonthNumber.Seven)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericQuarter(2016, QuarterNumber.Q2),
                        new GenericQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2015, MonthNumber.Seven)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2017, MonthNumber.Ten)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2018, MonthNumber.Seven)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericQuarter(2016, QuarterNumber.Q2),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2016, MonthNumber.Three)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericQuarter(2016, QuarterNumber.Q2),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2015, MonthNumber.Six)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericQuarter(2016, QuarterNumber.Q2),
                        new GenericQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericQuarter(2016, QuarterNumber.Q2),
                        new GenericQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2018)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2018)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2019)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericQuarter(2016, QuarterNumber.Q2),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericQuarter(2016, QuarterNumber.Q2),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2015)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericQuarter(2016, QuarterNumber.Q2),
                        new GenericQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (GenericUnitOfTime)new GenericUnbounded()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (GenericUnitOfTime)new GenericUnbounded()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericQuarter(2016, QuarterNumber.Q2),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericUnbounded()
                },
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_contained_within_a_GenericQuarter_reportingPeriod_and_unitOfTime_is_not_a_GenericQuarter()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericQuarter(2016, QuarterNumber.Q2),
                        new GenericQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2016, MonthNumber.Four)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericQuarter(2016, QuarterNumber.Q2),
                        new GenericQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2017, MonthNumber.Nine)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericQuarter(2016, QuarterNumber.Q2),
                        new GenericQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2016, MonthNumber.Twelve)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2017, MonthNumber.Nine)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2017, MonthNumber.Four)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericQuarter(2016, QuarterNumber.Q2),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2016, MonthNumber.Four)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericQuarter(2016, QuarterNumber.Q2),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2016, MonthNumber.Eight)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericQuarter(2016, QuarterNumber.Q2),
                        new GenericQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericQuarter(2016, QuarterNumber.Q1),
                        new GenericQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericQuarter(2018, QuarterNumber.Q4)),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2018)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericQuarter(2016, QuarterNumber.Q2),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericQuarter(2016, QuarterNumber.Q1),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2016)
                }
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_false___When_parameter_unitOfTime_is_not_contained_within_a_GenericYear_reportingPeriod_and_unitOfTime_is_not_a_GenericYear()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericYear(2016),
                        new GenericYear(2018)),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2019, MonthNumber.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericYear(2016),
                        new GenericYear(2018)),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2015, MonthNumber.Twelve)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericYear(2018)),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2019, MonthNumber.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericYear(2018),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2017, MonthNumber.Twelve)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericYear(2016),
                        new GenericYear(2018)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2015, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericYear(2016),
                        new GenericYear(2018)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2019, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericYear(2018)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2019, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericYear(2018),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2017, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericYear(2016),
                        new GenericYear(2018)),
                    UnitOfTime = (GenericUnitOfTime)new GenericUnbounded()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericYear(2018)),
                    UnitOfTime = (GenericUnitOfTime)new GenericUnbounded()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericYear(2016),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericUnbounded()
                }
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_contained_within_a_GenericYear_reportingPeriod_and_unitOfTime_is_not_a_GenericYear()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericYear(2016),
                        new GenericYear(2018)),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2016, MonthNumber.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericYear(2016),
                        new GenericYear(2018)),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2018, MonthNumber.Twelve)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericYear(2016),
                        new GenericYear(2018)),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2017, MonthNumber.Six)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericYear(2018)),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2018, MonthNumber.Twelve)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericYear(2018)),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2018, MonthNumber.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericYear(2018)),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2017, MonthNumber.Six)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericYear(2018),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2018, MonthNumber.Twelve)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericYear(2018),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2018, MonthNumber.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericYear(2018),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2019, MonthNumber.Six)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericYear(2016),
                        new GenericYear(2018)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2016, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericYear(2016),
                        new GenericYear(2018)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2018, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericYear(2016),
                        new GenericYear(2018)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2017, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericYear(2018)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2018, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericYear(2018)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2018, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericYear(2018)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2017, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericYear(2018),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2018, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericYear(2018),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2018, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericYear(2018),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2019, QuarterNumber.Q2)
                }
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_reportingPeriod_has_GenericUnbounded_Start_and_End()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)A.Dummy<GenericMonth>()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)A.Dummy<GenericQuarter>()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)A.Dummy<GenericYear>()
                }
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_throw_ArgumentNullException___When_parameter_reportingPeriod1_is_null()
        {
            // Arrange
            var reportingPeriod2 = A.Dummy<ReportingPeriod<UnitOfTime>>();

            // Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.Contains(null, reportingPeriod2));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_throw_ArgumentNullException___When_parameter_reportingPeriod2_is_null()
        {
            // Arrange
            var reportingPeriod1 = A.Dummy<ReportingPeriod<UnitOfTime>>();
            IReportingPeriod<UnitOfTime> reportingPeriod2 = null;

            // Act
            // ReSharper disable ExpressionIsAlwaysNull
            var ex = Record.Exception(() => reportingPeriod1.Contains(reportingPeriod2));
            // ReSharper restore ExpressionIsAlwaysNull

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_throw_ArgumentException___When_reporting_periods_have_different_UnitOfTimeKind()
        {
            // Arrange
            var reportingPeriod1 = A.Dummy<ReportingPeriod<UnitOfTime>>();
            var reportingPeriod2 = A.Dummy<ReportingPeriod<UnitOfTime>>().Whose(_ => _.GetUnitOfTimeKind() != reportingPeriod1.GetUnitOfTimeKind());

            // Act
            // ReSharper disable ExpressionIsAlwaysNull
            var ex = Record.Exception(() => reportingPeriod1.Contains(reportingPeriod2));
            // ReSharper restore ExpressionIsAlwaysNull

            // Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_false___When_reportingPeriod1_does_not_contain_reportingPeriod2_an_both_are_of_type_CalendarDay()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Thirty));
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.One), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentySeven)),
                    new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.One), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight)),
                    new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.One), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine)),
                    new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.One), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Thirty)),
                    new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.ThirtyOne)),
                    new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.ThirtyOne)),
                    new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Thirty), new CalendarDay(2016, MonthOfYear.April, DayOfMonth.Ten)),
                    new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.April, DayOfMonth.One), new CalendarDay(2016, MonthOfYear.April, DayOfMonth.Ten))
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_contain_reportingPeriod2_an_both_are_of_type_CalendarDay()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Thirty));
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.TwentyNine)),
                    new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.TwentyNine)),
                    new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Thirty)),
                    new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Thirty))
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_false___When_reportingPeriod1_does_not_contain_reportingPeriod2_an_both_are_of_type_CalendarMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2015, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.May));
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<CalendarMonth>(new CalendarMonth(2014, MonthOfYear.January), new CalendarMonth(2015, MonthOfYear.January)),
                    new ReportingPeriod<CalendarMonth>(new CalendarMonth(2015, MonthOfYear.January), new CalendarMonth(2015, MonthOfYear.February)),
                    new ReportingPeriod<CalendarMonth>(new CalendarMonth(2015, MonthOfYear.January), new CalendarMonth(2016, MonthOfYear.January)),
                    new ReportingPeriod<CalendarMonth>(new CalendarMonth(2015, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.June)),
                    new ReportingPeriod<CalendarMonth>(new CalendarMonth(2015, MonthOfYear.March), new CalendarMonth(2016, MonthOfYear.June)),
                    new ReportingPeriod<CalendarMonth>(new CalendarMonth(2015, MonthOfYear.March), new CalendarMonth(2016, MonthOfYear.June)),
                    new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.May), new CalendarMonth(2016, MonthOfYear.June)),
                    new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.June), new CalendarMonth(2016, MonthOfYear.September))
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_contain_reportingPeriod2_an_both_are_of_type_CalendarMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2015, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.May));
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<CalendarMonth>(new CalendarMonth(2015, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.May)),
                    new ReportingPeriod<CalendarMonth>(new CalendarMonth(2015, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.April)),
                    new ReportingPeriod<CalendarMonth>(new CalendarMonth(2015, MonthOfYear.April), new CalendarMonth(2016, MonthOfYear.May)),
                    new ReportingPeriod<CalendarMonth>(new CalendarMonth(2015, MonthOfYear.April), new CalendarMonth(2016, MonthOfYear.April)),
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_false___When_reportingPeriod1_does_not_contain_reportingPeriod2_an_both_are_of_type_CalendarQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2015, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q3));
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2014, QuarterNumber.Q3), new CalendarQuarter(2015, QuarterNumber.Q1)),
                    new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2015, QuarterNumber.Q1), new CalendarQuarter(2015, QuarterNumber.Q2)),
                    new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2015, QuarterNumber.Q1), new CalendarQuarter(2016, QuarterNumber.Q1)),
                    new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2015, QuarterNumber.Q1), new CalendarQuarter(2016, QuarterNumber.Q3)),
                    new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2015, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q4)),
                    new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2015, QuarterNumber.Q3), new CalendarQuarter(2016, QuarterNumber.Q4)),
                    new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q4)),
                    new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q4), new CalendarQuarter(2017, QuarterNumber.Q1))
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_contain_reportingPeriod2_an_both_are_of_type_CalendarQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2015, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q3));
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2015, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q3)),
                    new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2015, QuarterNumber.Q3), new CalendarQuarter(2016, QuarterNumber.Q3)),
                    new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2015, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q2)),
                    new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2015, QuarterNumber.Q3), new CalendarQuarter(2016, QuarterNumber.Q2))
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_false___When_reportingPeriod1_does_not_contain_reportingPeriod2_an_both_are_of_type_CalendarYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarYear>(new CalendarYear(2015), new CalendarYear(2017));
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<CalendarYear>(new CalendarYear(2012), new CalendarYear(2014)),
                    new ReportingPeriod<CalendarYear>(new CalendarYear(2012), new CalendarYear(2015)),
                    new ReportingPeriod<CalendarYear>(new CalendarYear(2012), new CalendarYear(2016)),
                    new ReportingPeriod<CalendarYear>(new CalendarYear(2012), new CalendarYear(2017)),
                    new ReportingPeriod<CalendarYear>(new CalendarYear(2015), new CalendarYear(2018)),
                    new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2018)),
                    new ReportingPeriod<CalendarYear>(new CalendarYear(2017), new CalendarYear(2018)),
                    new ReportingPeriod<CalendarYear>(new CalendarYear(2018), new CalendarYear(2019))
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_contain_reportingPeriod2_an_both_are_of_type_CalendarYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarYear>(new CalendarYear(2015), new CalendarYear(2017));
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<CalendarYear>(new CalendarYear(2015), new CalendarYear(2017)),
                    new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2017)),
                    new ReportingPeriod<CalendarYear>(new CalendarYear(2015), new CalendarYear(2016)),
                    new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2016))
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_does_not_contain_reportingPeriod2_an_both_are_of_type_CalendarUnbounded()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarUnbounded>(new CalendarUnbounded(), new CalendarUnbounded());
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<CalendarUnbounded>(new CalendarUnbounded(), new CalendarUnbounded())
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_false___When_reportingPeriod1_does_not_contain_reportingPeriod2_an_both_are_of_type_FiscalMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2015, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Five));
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<FiscalMonth>(new FiscalMonth(2014, MonthNumber.One), new FiscalMonth(2015, MonthNumber.One)),
                    new ReportingPeriod<FiscalMonth>(new FiscalMonth(2015, MonthNumber.One), new FiscalMonth(2015, MonthNumber.Two)),
                    new ReportingPeriod<FiscalMonth>(new FiscalMonth(2015, MonthNumber.One), new FiscalMonth(2016, MonthNumber.One)),
                    new ReportingPeriod<FiscalMonth>(new FiscalMonth(2015, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Six)),
                    new ReportingPeriod<FiscalMonth>(new FiscalMonth(2015, MonthNumber.Three), new FiscalMonth(2016, MonthNumber.Six)),
                    new ReportingPeriod<FiscalMonth>(new FiscalMonth(2015, MonthNumber.Three), new FiscalMonth(2016, MonthNumber.Six)),
                    new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Five), new FiscalMonth(2016, MonthNumber.Six)),
                    new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Six), new FiscalMonth(2016, MonthNumber.Nine))
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_contain_reportingPeriod2_an_both_are_of_type_FiscalMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2015, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Five));
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<FiscalMonth>(new FiscalMonth(2015, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Five)),
                    new ReportingPeriod<FiscalMonth>(new FiscalMonth(2015, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Four)),
                    new ReportingPeriod<FiscalMonth>(new FiscalMonth(2015, MonthNumber.Four), new FiscalMonth(2016, MonthNumber.Five)),
                    new ReportingPeriod<FiscalMonth>(new FiscalMonth(2015, MonthNumber.Four), new FiscalMonth(2016, MonthNumber.Four)),
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_false___When_reportingPeriod1_does_not_contain_reportingPeriod2_an_both_are_of_type_FiscalQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2015, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q3));
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2014, QuarterNumber.Q3), new FiscalQuarter(2015, QuarterNumber.Q1)),
                    new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2015, QuarterNumber.Q1), new FiscalQuarter(2015, QuarterNumber.Q2)),
                    new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2015, QuarterNumber.Q1), new FiscalQuarter(2016, QuarterNumber.Q1)),
                    new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2015, QuarterNumber.Q1), new FiscalQuarter(2016, QuarterNumber.Q3)),
                    new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2015, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q4)),
                    new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2015, QuarterNumber.Q3), new FiscalQuarter(2016, QuarterNumber.Q4)),
                    new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q4)),
                    new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q4), new FiscalQuarter(2017, QuarterNumber.Q1))
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_contain_reportingPeriod2_an_both_are_of_type_FiscalQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2015, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q3));
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2015, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q3)),
                    new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2015, QuarterNumber.Q3), new FiscalQuarter(2016, QuarterNumber.Q3)),
                    new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2015, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q2)),
                    new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2015, QuarterNumber.Q3), new FiscalQuarter(2016, QuarterNumber.Q2))
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_false___When_reportingPeriod1_does_not_contain_reportingPeriod2_an_both_are_of_type_FiscalYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalYear>(new FiscalYear(2015), new FiscalYear(2017));
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<FiscalYear>(new FiscalYear(2012), new FiscalYear(2014)),
                    new ReportingPeriod<FiscalYear>(new FiscalYear(2012), new FiscalYear(2015)),
                    new ReportingPeriod<FiscalYear>(new FiscalYear(2012), new FiscalYear(2016)),
                    new ReportingPeriod<FiscalYear>(new FiscalYear(2012), new FiscalYear(2017)),
                    new ReportingPeriod<FiscalYear>(new FiscalYear(2015), new FiscalYear(2018)),
                    new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2018)),
                    new ReportingPeriod<FiscalYear>(new FiscalYear(2017), new FiscalYear(2018)),
                    new ReportingPeriod<FiscalYear>(new FiscalYear(2018), new FiscalYear(2019))
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_contain_reportingPeriod2_an_both_are_of_type_FiscalYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalYear>(new FiscalYear(2015), new FiscalYear(2017));
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<FiscalYear>(new FiscalYear(2015), new FiscalYear(2017)),
                    new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2017)),
                    new ReportingPeriod<FiscalYear>(new FiscalYear(2015), new FiscalYear(2016)),
                    new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2016))
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_does_not_contain_reportingPeriod2_an_both_are_of_type_FiscalUnbounded()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalUnbounded>(new FiscalUnbounded(), new FiscalUnbounded());
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<FiscalUnbounded>(new FiscalUnbounded(), new FiscalUnbounded())
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_false___When_reportingPeriod1_does_not_contain_reportingPeriod2_an_both_are_of_type_GenericMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericMonth>(new GenericMonth(2015, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Five));
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<GenericMonth>(new GenericMonth(2014, MonthNumber.One), new GenericMonth(2015, MonthNumber.One)),
                    new ReportingPeriod<GenericMonth>(new GenericMonth(2015, MonthNumber.One), new GenericMonth(2015, MonthNumber.Two)),
                    new ReportingPeriod<GenericMonth>(new GenericMonth(2015, MonthNumber.One), new GenericMonth(2016, MonthNumber.One)),
                    new ReportingPeriod<GenericMonth>(new GenericMonth(2015, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Six)),
                    new ReportingPeriod<GenericMonth>(new GenericMonth(2015, MonthNumber.Three), new GenericMonth(2016, MonthNumber.Six)),
                    new ReportingPeriod<GenericMonth>(new GenericMonth(2015, MonthNumber.Three), new GenericMonth(2016, MonthNumber.Six)),
                    new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Five), new GenericMonth(2016, MonthNumber.Six)),
                    new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Six), new GenericMonth(2016, MonthNumber.Nine))
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_contain_reportingPeriod2_an_both_are_of_type_GenericMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericMonth>(new GenericMonth(2015, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Five));
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<GenericMonth>(new GenericMonth(2015, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Five)),
                    new ReportingPeriod<GenericMonth>(new GenericMonth(2015, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Four)),
                    new ReportingPeriod<GenericMonth>(new GenericMonth(2015, MonthNumber.Four), new GenericMonth(2016, MonthNumber.Five)),
                    new ReportingPeriod<GenericMonth>(new GenericMonth(2015, MonthNumber.Four), new GenericMonth(2016, MonthNumber.Four)),
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_false___When_reportingPeriod1_does_not_contain_reportingPeriod2_an_both_are_of_type_GenericQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2015, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q3));
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<GenericQuarter>(new GenericQuarter(2014, QuarterNumber.Q3), new GenericQuarter(2015, QuarterNumber.Q1)),
                    new ReportingPeriod<GenericQuarter>(new GenericQuarter(2015, QuarterNumber.Q1), new GenericQuarter(2015, QuarterNumber.Q2)),
                    new ReportingPeriod<GenericQuarter>(new GenericQuarter(2015, QuarterNumber.Q1), new GenericQuarter(2016, QuarterNumber.Q1)),
                    new ReportingPeriod<GenericQuarter>(new GenericQuarter(2015, QuarterNumber.Q1), new GenericQuarter(2016, QuarterNumber.Q3)),
                    new ReportingPeriod<GenericQuarter>(new GenericQuarter(2015, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q4)),
                    new ReportingPeriod<GenericQuarter>(new GenericQuarter(2015, QuarterNumber.Q3), new GenericQuarter(2016, QuarterNumber.Q4)),
                    new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q4)),
                    new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q4), new GenericQuarter(2017, QuarterNumber.Q1))
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_contain_reportingPeriod2_an_both_are_of_type_GenericQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2015, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q3));
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<GenericQuarter>(new GenericQuarter(2015, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q3)),
                    new ReportingPeriod<GenericQuarter>(new GenericQuarter(2015, QuarterNumber.Q3), new GenericQuarter(2016, QuarterNumber.Q3)),
                    new ReportingPeriod<GenericQuarter>(new GenericQuarter(2015, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q2)),
                    new ReportingPeriod<GenericQuarter>(new GenericQuarter(2015, QuarterNumber.Q3), new GenericQuarter(2016, QuarterNumber.Q2))
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_false___When_reportingPeriod1_does_not_contain_reportingPeriod2_an_both_are_of_type_GenericYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericYear>(new GenericYear(2015), new GenericYear(2017));
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<GenericYear>(new GenericYear(2012), new GenericYear(2014)),
                    new ReportingPeriod<GenericYear>(new GenericYear(2012), new GenericYear(2015)),
                    new ReportingPeriod<GenericYear>(new GenericYear(2012), new GenericYear(2016)),
                    new ReportingPeriod<GenericYear>(new GenericYear(2012), new GenericYear(2017)),
                    new ReportingPeriod<GenericYear>(new GenericYear(2015), new GenericYear(2018)),
                    new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2018)),
                    new ReportingPeriod<GenericYear>(new GenericYear(2017), new GenericYear(2018)),
                    new ReportingPeriod<GenericYear>(new GenericYear(2018), new GenericYear(2019))
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_contain_reportingPeriod2_an_both_are_of_type_GenericYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericYear>(new GenericYear(2015), new GenericYear(2017));
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<GenericYear>(new GenericYear(2015), new GenericYear(2017)),
                    new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2017)),
                    new ReportingPeriod<GenericYear>(new GenericYear(2015), new GenericYear(2016)),
                    new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2016))
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_does_not_contain_reportingPeriod2_an_both_are_of_type_GenericUnbounded()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericUnbounded>(new GenericUnbounded(), new GenericUnbounded());
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<GenericUnbounded>(new GenericUnbounded(), new GenericUnbounded())
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_false___When_reportingPeriod1_uses_CalendarDay_and_does_not_contain_reportingPeriod2_which_does_not_use_CalendarDay()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_uses_CalendarDay_and_contains_reportingPeriod2_which_does_not_use_CalendarDay()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_false___When_reportingPeriod1_uses_CalendarMonth_and_does_not_contain_reportingPeriod2_which_does_not_use_CalendarMonth()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_uses_CalendarMonth_and_contains_reportingPeriod2_which_does_not_use_CalendarMonth()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_false___When_reportingPeriod1_uses_CalendarQuarter_and_does_not_contain_reportingPeriod2_which_does_not_use_CalendarQuarter()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_uses_CalendarQuarter_and_contains_reportingPeriod2_which_does_not_use_CalendarQuarter()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_false___When_reportingPeriod1_uses_CalendarYear_and_does_not_contain_reportingPeriod2_which_does_not_use_CalendarYear()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_uses_CalendarYear_and_contains_reportingPeriod2_which_does_not_use_CalendarYear()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_uses_CalendarUnbounded_and_contains_reportingPeriod2_which_does_not_use_CalendarYear()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_false___When_reportingPeriod1_uses_FiscalMonth_and_does_not_contain_reportingPeriod2_which_does_not_use_FiscalMonth()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_uses_FiscalMonth_and_contains_reportingPeriod2_which_does_not_use_FiscalMonth()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_false___When_reportingPeriod1_uses_FiscalQuarter_and_does_not_contain_reportingPeriod2_which_does_not_use_FiscalQuarter()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_uses_FiscalQuarter_and_contains_reportingPeriod2_which_does_not_use_FiscalQuarter()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_false___When_reportingPeriod1_uses_FiscalYear_and_does_not_contain_reportingPeriod2_which_does_not_use_FiscalYear()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_uses_FiscalYear_and_contains_reportingPeriod2_which_does_not_use_FiscalYear()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_uses_FiscalUnbounded_and_contains_reportingPeriod2_which_does_not_use_FiscalYear()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_false___When_reportingPeriod1_uses_GenericMonth_and_does_not_contain_reportingPeriod2_which_does_not_use_GenericMonth()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_uses_GenericMonth_and_contains_reportingPeriod2_which_does_not_use_GenericMonth()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_false___When_reportingPeriod1_uses_GenericQuarter_and_does_not_contain_reportingPeriod2_which_does_not_use_GenericQuarter()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_uses_GenericQuarter_and_contains_reportingPeriod2_which_does_not_use_GenericQuarter()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_false___When_reportingPeriod1_uses_GenericYear_and_does_not_contain_reportingPeriod2_which_does_not_use_GenericYear()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_uses_GenericYear_and_contains_reportingPeriod2_which_does_not_use_GenericYear()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_uses_GenericUnbounded_and_contains_reportingPeriod2_which_does_not_use_GenericYear()
        {
        }

        [Fact]
        public static void HasOverlapWith___Should_throw_ArgumentNullException___When_parameter_reportingPeriod1_is_null()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod<UnitOfTime>>();

            // Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.HasOverlapWith(null, reportingPeriod));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void HasOverlapWith___Should_throw_ArgumentNullException___When_parameter_reportingPeriod2_is_null()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod<UnitOfTime>>();

            // Act
            var ex = Record.Exception(() => reportingPeriod.HasOverlapWith(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void HasOverlapWith___Should_throw_ArgumentException___When_reporting_periods_have_different_UnitOfTimeKind()
        {
            // Arrange
            var reportingPeriod1 = A.Dummy<ReportingPeriod<UnitOfTime>>();
            var reportingPeriod2 = A.Dummy<ReportingPeriod<UnitOfTime>>().Whose(_ => _.GetUnitOfTimeKind() != reportingPeriod1.GetUnitOfTimeKind());

            // Act
            // ReSharper disable ExpressionIsAlwaysNull
            var ex = Record.Exception(() => reportingPeriod1.HasOverlapWith(reportingPeriod2));
            // ReSharper restore ExpressionIsAlwaysNull

            // Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_false___When_parameters_reportingPeriod1_and_reportingPeriod2_do_not_overlap_and_are_of_type_CalendarDay()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Thirty));

            var reportingPeriod2a = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.One), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentySeven));
            var reportingPeriod2b = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.March, DayOfMonth.ThirtyOne), new CalendarDay(2016, MonthOfYear.April, DayOfMonth.Five));

            // Act
            var hasOverlap1a = reportingPeriod1.HasOverlapWith(reportingPeriod2a);
            var hasOverlap1b = reportingPeriod2a.HasOverlapWith(reportingPeriod1);

            var hasOverlap2a = reportingPeriod1.HasOverlapWith(reportingPeriod2b);
            var hasOverlap2b = reportingPeriod2b.HasOverlapWith(reportingPeriod1);

            // Assert
            hasOverlap1a.Should().BeFalse();
            hasOverlap1b.Should().BeFalse();
            hasOverlap2a.Should().BeFalse();
            hasOverlap2b.Should().BeFalse();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_true___When_parameters_reportingPeriod1_and_reportingPeriod2_overlap_and_are_of_type_CalendarDay()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Thirty));

            var reportingPeriod2a = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.One), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight));
            var reportingPeriod2b = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.One), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine));
            var reportingPeriod2c = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.TwentyNine));
            var reportingPeriod2d = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.TwentyNine));
            var reportingPeriod2e = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Thirty));
            var reportingPeriod2f = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.ThirtyOne));
            var reportingPeriod2g = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Thirty), new CalendarDay(2016, MonthOfYear.April, DayOfMonth.Ten));

            // Act
            var hasOverlap1a = reportingPeriod1.HasOverlapWith(reportingPeriod2a);
            var hasOverlap1b = reportingPeriod2a.HasOverlapWith(reportingPeriod1);

            var hasOverlap2a = reportingPeriod1.HasOverlapWith(reportingPeriod2b);
            var hasOverlap2b = reportingPeriod2b.HasOverlapWith(reportingPeriod1);

            var hasOverlap3a = reportingPeriod1.HasOverlapWith(reportingPeriod2c);
            var hasOverlap3b = reportingPeriod2c.HasOverlapWith(reportingPeriod1);

            var hasOverlap4a = reportingPeriod1.HasOverlapWith(reportingPeriod2d);
            var hasOverlap4b = reportingPeriod2d.HasOverlapWith(reportingPeriod1);

            var hasOverlap5a = reportingPeriod1.HasOverlapWith(reportingPeriod2e);
            var hasOverlap5b = reportingPeriod2e.HasOverlapWith(reportingPeriod1);

            var hasOverlap6a = reportingPeriod1.HasOverlapWith(reportingPeriod2f);
            var hasOverlap6b = reportingPeriod2f.HasOverlapWith(reportingPeriod1);

            var hasOverlap7a = reportingPeriod1.HasOverlapWith(reportingPeriod2g);
            var hasOverlap7b = reportingPeriod2g.HasOverlapWith(reportingPeriod1);

            // Assert
            hasOverlap1a.Should().BeTrue();
            hasOverlap1b.Should().BeTrue();
            hasOverlap2a.Should().BeTrue();
            hasOverlap2b.Should().BeTrue();
            hasOverlap3a.Should().BeTrue();
            hasOverlap3b.Should().BeTrue();
            hasOverlap4a.Should().BeTrue();
            hasOverlap4b.Should().BeTrue();
            hasOverlap5a.Should().BeTrue();
            hasOverlap5b.Should().BeTrue();
            hasOverlap6a.Should().BeTrue();
            hasOverlap6b.Should().BeTrue();
            hasOverlap7a.Should().BeTrue();
            hasOverlap7b.Should().BeTrue();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_false___When_parameters_reportingPeriod1_and_reportingPeriod2_do_not_overlap_and_are_of_type_CalendarMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.April));

            var reportingPeriod2a = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2014, MonthOfYear.January), new CalendarMonth(2016, MonthOfYear.January));
            var reportingPeriod2b = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.May), new CalendarMonth(2017, MonthOfYear.July));

            // Act
            var hasOverlap1a = reportingPeriod1.HasOverlapWith(reportingPeriod2a);
            var hasOverlap1b = reportingPeriod2a.HasOverlapWith(reportingPeriod1);

            var hasOverlap2a = reportingPeriod1.HasOverlapWith(reportingPeriod2b);
            var hasOverlap2b = reportingPeriod2b.HasOverlapWith(reportingPeriod1);

            // Assert
            hasOverlap1a.Should().BeFalse();
            hasOverlap1b.Should().BeFalse();
            hasOverlap2a.Should().BeFalse();
            hasOverlap2b.Should().BeFalse();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_true___When_parameters_reportingPeriod1_and_reportingPeriod2_overlap_and_are_of_type_CalendarMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.April));

            var reportingPeriod2a = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2015, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.February));
            var reportingPeriod2b = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2015, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.March));
            var reportingPeriod2c = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.March));
            var reportingPeriod2d = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.March), new CalendarMonth(2016, MonthOfYear.March));
            var reportingPeriod2e = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.March), new CalendarMonth(2016, MonthOfYear.April));
            var reportingPeriod2f = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.March), new CalendarMonth(2017, MonthOfYear.June));
            var reportingPeriod2g = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.April), new CalendarMonth(2017, MonthOfYear.June));

            // Act
            var hasOverlap1a = reportingPeriod1.HasOverlapWith(reportingPeriod2a);
            var hasOverlap1b = reportingPeriod2a.HasOverlapWith(reportingPeriod1);

            var hasOverlap2a = reportingPeriod1.HasOverlapWith(reportingPeriod2b);
            var hasOverlap2b = reportingPeriod2b.HasOverlapWith(reportingPeriod1);

            var hasOverlap3a = reportingPeriod1.HasOverlapWith(reportingPeriod2c);
            var hasOverlap3b = reportingPeriod2c.HasOverlapWith(reportingPeriod1);

            var hasOverlap4a = reportingPeriod1.HasOverlapWith(reportingPeriod2d);
            var hasOverlap4b = reportingPeriod2d.HasOverlapWith(reportingPeriod1);

            var hasOverlap5a = reportingPeriod1.HasOverlapWith(reportingPeriod2e);
            var hasOverlap5b = reportingPeriod2e.HasOverlapWith(reportingPeriod1);

            var hasOverlap6a = reportingPeriod1.HasOverlapWith(reportingPeriod2f);
            var hasOverlap6b = reportingPeriod2f.HasOverlapWith(reportingPeriod1);

            var hasOverlap7a = reportingPeriod1.HasOverlapWith(reportingPeriod2g);
            var hasOverlap7b = reportingPeriod2g.HasOverlapWith(reportingPeriod1);

            // Assert
            hasOverlap1a.Should().BeTrue();
            hasOverlap1b.Should().BeTrue();
            hasOverlap2a.Should().BeTrue();
            hasOverlap2b.Should().BeTrue();
            hasOverlap3a.Should().BeTrue();
            hasOverlap3b.Should().BeTrue();
            hasOverlap4a.Should().BeTrue();
            hasOverlap4b.Should().BeTrue();
            hasOverlap5a.Should().BeTrue();
            hasOverlap5b.Should().BeTrue();
            hasOverlap6a.Should().BeTrue();
            hasOverlap6b.Should().BeTrue();
            hasOverlap7a.Should().BeTrue();
            hasOverlap7b.Should().BeTrue();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_false___When_parameters_reportingPeriod1_and_reportingPeriod2_do_not_overlap_and_are_of_type_FiscalMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Four));

            var reportingPeriod2a = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2014, MonthNumber.One), new FiscalMonth(2016, MonthNumber.One));
            var reportingPeriod2b = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Five), new FiscalMonth(2017, MonthNumber.Seven));

            // Act
            var hasOverlap1a = reportingPeriod1.HasOverlapWith(reportingPeriod2a);
            var hasOverlap1b = reportingPeriod2a.HasOverlapWith(reportingPeriod1);

            var hasOverlap2a = reportingPeriod1.HasOverlapWith(reportingPeriod2b);
            var hasOverlap2b = reportingPeriod2b.HasOverlapWith(reportingPeriod1);

            // Assert
            hasOverlap1a.Should().BeFalse();
            hasOverlap1b.Should().BeFalse();
            hasOverlap2a.Should().BeFalse();
            hasOverlap2b.Should().BeFalse();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_true___When_parameters_reportingPeriod1_and_reportingPeriod2_overlap_and_are_of_type_FiscalMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Four));

            var reportingPeriod2a = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2015, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Two));
            var reportingPeriod2b = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2015, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Three));
            var reportingPeriod2c = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Three));
            var reportingPeriod2d = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Three), new FiscalMonth(2016, MonthNumber.Three));
            var reportingPeriod2e = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Three), new FiscalMonth(2016, MonthNumber.Four));
            var reportingPeriod2f = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Three), new FiscalMonth(2017, MonthNumber.Six));
            var reportingPeriod2g = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Six));

            // Act
            var hasOverlap1a = reportingPeriod1.HasOverlapWith(reportingPeriod2a);
            var hasOverlap1b = reportingPeriod2a.HasOverlapWith(reportingPeriod1);

            var hasOverlap2a = reportingPeriod1.HasOverlapWith(reportingPeriod2b);
            var hasOverlap2b = reportingPeriod2b.HasOverlapWith(reportingPeriod1);

            var hasOverlap3a = reportingPeriod1.HasOverlapWith(reportingPeriod2c);
            var hasOverlap3b = reportingPeriod2c.HasOverlapWith(reportingPeriod1);

            var hasOverlap4a = reportingPeriod1.HasOverlapWith(reportingPeriod2d);
            var hasOverlap4b = reportingPeriod2d.HasOverlapWith(reportingPeriod1);

            var hasOverlap5a = reportingPeriod1.HasOverlapWith(reportingPeriod2e);
            var hasOverlap5b = reportingPeriod2e.HasOverlapWith(reportingPeriod1);

            var hasOverlap6a = reportingPeriod1.HasOverlapWith(reportingPeriod2f);
            var hasOverlap6b = reportingPeriod2f.HasOverlapWith(reportingPeriod1);

            var hasOverlap7a = reportingPeriod1.HasOverlapWith(reportingPeriod2g);
            var hasOverlap7b = reportingPeriod2g.HasOverlapWith(reportingPeriod1);

            // Assert
            hasOverlap1a.Should().BeTrue();
            hasOverlap1b.Should().BeTrue();
            hasOverlap2a.Should().BeTrue();
            hasOverlap2b.Should().BeTrue();
            hasOverlap3a.Should().BeTrue();
            hasOverlap3b.Should().BeTrue();
            hasOverlap4a.Should().BeTrue();
            hasOverlap4b.Should().BeTrue();
            hasOverlap5a.Should().BeTrue();
            hasOverlap5b.Should().BeTrue();
            hasOverlap6a.Should().BeTrue();
            hasOverlap6b.Should().BeTrue();
            hasOverlap7a.Should().BeTrue();
            hasOverlap7b.Should().BeTrue();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_false___When_parameters_reportingPeriod1_and_reportingPeriod2_do_not_overlap_and_are_of_type_GenericMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Four));

            var reportingPeriod2a = new ReportingPeriod<GenericMonth>(new GenericMonth(2014, MonthNumber.One), new GenericMonth(2016, MonthNumber.One));
            var reportingPeriod2b = new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Five), new GenericMonth(2017, MonthNumber.Seven));

            // Act
            var hasOverlap1a = reportingPeriod1.HasOverlapWith(reportingPeriod2a);
            var hasOverlap1b = reportingPeriod2a.HasOverlapWith(reportingPeriod1);

            var hasOverlap2a = reportingPeriod1.HasOverlapWith(reportingPeriod2b);
            var hasOverlap2b = reportingPeriod2b.HasOverlapWith(reportingPeriod1);

            // Assert
            hasOverlap1a.Should().BeFalse();
            hasOverlap1b.Should().BeFalse();
            hasOverlap2a.Should().BeFalse();
            hasOverlap2b.Should().BeFalse();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_true___When_parameters_reportingPeriod1_and_reportingPeriod2_overlap_and_are_of_type_GenericMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Four));

            var reportingPeriod2a = new ReportingPeriod<GenericMonth>(new GenericMonth(2015, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Two));
            var reportingPeriod2b = new ReportingPeriod<GenericMonth>(new GenericMonth(2015, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Three));
            var reportingPeriod2c = new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Three));
            var reportingPeriod2d = new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Three), new GenericMonth(2016, MonthNumber.Three));
            var reportingPeriod2e = new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Three), new GenericMonth(2016, MonthNumber.Four));
            var reportingPeriod2f = new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Three), new GenericMonth(2017, MonthNumber.Six));
            var reportingPeriod2g = new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Four), new GenericMonth(2017, MonthNumber.Six));

            // Act
            var hasOverlap1a = reportingPeriod1.HasOverlapWith(reportingPeriod2a);
            var hasOverlap1b = reportingPeriod2a.HasOverlapWith(reportingPeriod1);

            var hasOverlap2a = reportingPeriod1.HasOverlapWith(reportingPeriod2b);
            var hasOverlap2b = reportingPeriod2b.HasOverlapWith(reportingPeriod1);

            var hasOverlap3a = reportingPeriod1.HasOverlapWith(reportingPeriod2c);
            var hasOverlap3b = reportingPeriod2c.HasOverlapWith(reportingPeriod1);

            var hasOverlap4a = reportingPeriod1.HasOverlapWith(reportingPeriod2d);
            var hasOverlap4b = reportingPeriod2d.HasOverlapWith(reportingPeriod1);

            var hasOverlap5a = reportingPeriod1.HasOverlapWith(reportingPeriod2e);
            var hasOverlap5b = reportingPeriod2e.HasOverlapWith(reportingPeriod1);

            var hasOverlap6a = reportingPeriod1.HasOverlapWith(reportingPeriod2f);
            var hasOverlap6b = reportingPeriod2f.HasOverlapWith(reportingPeriod1);

            var hasOverlap7a = reportingPeriod1.HasOverlapWith(reportingPeriod2g);
            var hasOverlap7b = reportingPeriod2g.HasOverlapWith(reportingPeriod1);

            // Assert
            hasOverlap1a.Should().BeTrue();
            hasOverlap1b.Should().BeTrue();
            hasOverlap2a.Should().BeTrue();
            hasOverlap2b.Should().BeTrue();
            hasOverlap3a.Should().BeTrue();
            hasOverlap3b.Should().BeTrue();
            hasOverlap4a.Should().BeTrue();
            hasOverlap4b.Should().BeTrue();
            hasOverlap5a.Should().BeTrue();
            hasOverlap5b.Should().BeTrue();
            hasOverlap6a.Should().BeTrue();
            hasOverlap6b.Should().BeTrue();
            hasOverlap7a.Should().BeTrue();
            hasOverlap7b.Should().BeTrue();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_false___When_parameters_reportingPeriod1_and_reportingPeriod2_do_not_overlap_and_are_of_type_CalendarQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q4));

            var reportingPeriod2a = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2014, QuarterNumber.Q1), new CalendarQuarter(2016, QuarterNumber.Q1));
            var reportingPeriod2b = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2017, QuarterNumber.Q1), new CalendarQuarter(2018, QuarterNumber.Q1));

            // Act
            var hasOverlap1a = reportingPeriod1.HasOverlapWith(reportingPeriod2a);
            var hasOverlap1b = reportingPeriod2a.HasOverlapWith(reportingPeriod1);

            var hasOverlap2a = reportingPeriod1.HasOverlapWith(reportingPeriod2b);
            var hasOverlap2b = reportingPeriod2b.HasOverlapWith(reportingPeriod1);

            // Assert
            hasOverlap1a.Should().BeFalse();
            hasOverlap1b.Should().BeFalse();
            hasOverlap2a.Should().BeFalse();
            hasOverlap2b.Should().BeFalse();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_true___When_parameters_reportingPeriod1_and_reportingPeriod2_overlap_and_are_of_type_CalendarQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q4));

            var reportingPeriod2a = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2015, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q2));
            var reportingPeriod2b = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2015, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q3));
            var reportingPeriod2c = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q3));
            var reportingPeriod2d = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q2));
            var reportingPeriod2e = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q3), new CalendarQuarter(2016, QuarterNumber.Q4));
            var reportingPeriod2f = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q3), new CalendarQuarter(2017, QuarterNumber.Q1));
            var reportingPeriod2g = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q4), new CalendarQuarter(2017, QuarterNumber.Q1));

            // Act
            var hasOverlap1a = reportingPeriod1.HasOverlapWith(reportingPeriod2a);
            var hasOverlap1b = reportingPeriod2a.HasOverlapWith(reportingPeriod1);

            var hasOverlap2a = reportingPeriod1.HasOverlapWith(reportingPeriod2b);
            var hasOverlap2b = reportingPeriod2b.HasOverlapWith(reportingPeriod1);

            var hasOverlap3a = reportingPeriod1.HasOverlapWith(reportingPeriod2c);
            var hasOverlap3b = reportingPeriod2c.HasOverlapWith(reportingPeriod1);

            var hasOverlap4a = reportingPeriod1.HasOverlapWith(reportingPeriod2d);
            var hasOverlap4b = reportingPeriod2d.HasOverlapWith(reportingPeriod1);

            var hasOverlap5a = reportingPeriod1.HasOverlapWith(reportingPeriod2e);
            var hasOverlap5b = reportingPeriod2e.HasOverlapWith(reportingPeriod1);

            var hasOverlap6a = reportingPeriod1.HasOverlapWith(reportingPeriod2f);
            var hasOverlap6b = reportingPeriod2f.HasOverlapWith(reportingPeriod1);

            var hasOverlap7a = reportingPeriod1.HasOverlapWith(reportingPeriod2g);
            var hasOverlap7b = reportingPeriod2g.HasOverlapWith(reportingPeriod1);

            // Assert
            hasOverlap1a.Should().BeTrue();
            hasOverlap1b.Should().BeTrue();
            hasOverlap2a.Should().BeTrue();
            hasOverlap2b.Should().BeTrue();
            hasOverlap3a.Should().BeTrue();
            hasOverlap3b.Should().BeTrue();
            hasOverlap4a.Should().BeTrue();
            hasOverlap4b.Should().BeTrue();
            hasOverlap5a.Should().BeTrue();
            hasOverlap5b.Should().BeTrue();
            hasOverlap6a.Should().BeTrue();
            hasOverlap6b.Should().BeTrue();
            hasOverlap7a.Should().BeTrue();
            hasOverlap7b.Should().BeTrue();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_false___When_parameters_reportingPeriod1_and_reportingPeriod2_do_not_overlap_and_are_of_type_FiscalQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q4));

            var reportingPeriod2a = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2014, QuarterNumber.Q1), new FiscalQuarter(2016, QuarterNumber.Q1));
            var reportingPeriod2b = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2017, QuarterNumber.Q1), new FiscalQuarter(2018, QuarterNumber.Q1));

            // Act
            var hasOverlap1a = reportingPeriod1.HasOverlapWith(reportingPeriod2a);
            var hasOverlap1b = reportingPeriod2a.HasOverlapWith(reportingPeriod1);

            var hasOverlap2a = reportingPeriod1.HasOverlapWith(reportingPeriod2b);
            var hasOverlap2b = reportingPeriod2b.HasOverlapWith(reportingPeriod1);

            // Assert
            hasOverlap1a.Should().BeFalse();
            hasOverlap1b.Should().BeFalse();
            hasOverlap2a.Should().BeFalse();
            hasOverlap2b.Should().BeFalse();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_true___When_parameters_reportingPeriod1_and_reportingPeriod2_overlap_and_are_of_type_FiscalQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q4));

            var reportingPeriod2a = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2015, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q2));
            var reportingPeriod2b = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2015, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q3));
            var reportingPeriod2c = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q3));
            var reportingPeriod2d = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q2));
            var reportingPeriod2e = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q3), new FiscalQuarter(2016, QuarterNumber.Q4));
            var reportingPeriod2f = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q3), new FiscalQuarter(2017, QuarterNumber.Q1));
            var reportingPeriod2g = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q4), new FiscalQuarter(2017, QuarterNumber.Q1));

            // Act
            var hasOverlap1a = reportingPeriod1.HasOverlapWith(reportingPeriod2a);
            var hasOverlap1b = reportingPeriod2a.HasOverlapWith(reportingPeriod1);

            var hasOverlap2a = reportingPeriod1.HasOverlapWith(reportingPeriod2b);
            var hasOverlap2b = reportingPeriod2b.HasOverlapWith(reportingPeriod1);

            var hasOverlap3a = reportingPeriod1.HasOverlapWith(reportingPeriod2c);
            var hasOverlap3b = reportingPeriod2c.HasOverlapWith(reportingPeriod1);

            var hasOverlap4a = reportingPeriod1.HasOverlapWith(reportingPeriod2d);
            var hasOverlap4b = reportingPeriod2d.HasOverlapWith(reportingPeriod1);

            var hasOverlap5a = reportingPeriod1.HasOverlapWith(reportingPeriod2e);
            var hasOverlap5b = reportingPeriod2e.HasOverlapWith(reportingPeriod1);

            var hasOverlap6a = reportingPeriod1.HasOverlapWith(reportingPeriod2f);
            var hasOverlap6b = reportingPeriod2f.HasOverlapWith(reportingPeriod1);

            var hasOverlap7a = reportingPeriod1.HasOverlapWith(reportingPeriod2g);
            var hasOverlap7b = reportingPeriod2g.HasOverlapWith(reportingPeriod1);

            // Assert
            hasOverlap1a.Should().BeTrue();
            hasOverlap1b.Should().BeTrue();
            hasOverlap2a.Should().BeTrue();
            hasOverlap2b.Should().BeTrue();
            hasOverlap3a.Should().BeTrue();
            hasOverlap3b.Should().BeTrue();
            hasOverlap4a.Should().BeTrue();
            hasOverlap4b.Should().BeTrue();
            hasOverlap5a.Should().BeTrue();
            hasOverlap5b.Should().BeTrue();
            hasOverlap6a.Should().BeTrue();
            hasOverlap6b.Should().BeTrue();
            hasOverlap7a.Should().BeTrue();
            hasOverlap7b.Should().BeTrue();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_false___When_parameters_reportingPeriod1_and_reportingPeriod2_do_not_overlap_and_are_of_type_GenericQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q4));

            var reportingPeriod2a = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2014, QuarterNumber.Q1), new GenericQuarter(2016, QuarterNumber.Q1));
            var reportingPeriod2b = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2017, QuarterNumber.Q1), new GenericQuarter(2018, QuarterNumber.Q1));

            // Act
            var hasOverlap1a = reportingPeriod1.HasOverlapWith(reportingPeriod2a);
            var hasOverlap1b = reportingPeriod2a.HasOverlapWith(reportingPeriod1);

            var hasOverlap2a = reportingPeriod1.HasOverlapWith(reportingPeriod2b);
            var hasOverlap2b = reportingPeriod2b.HasOverlapWith(reportingPeriod1);

            // Assert
            hasOverlap1a.Should().BeFalse();
            hasOverlap1b.Should().BeFalse();
            hasOverlap2a.Should().BeFalse();
            hasOverlap2b.Should().BeFalse();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_true___When_parameters_reportingPeriod1_and_reportingPeriod2_overlap_and_are_of_type_GenericQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q4));

            var reportingPeriod2a = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2015, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q2));
            var reportingPeriod2b = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2015, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q3));
            var reportingPeriod2c = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q3));
            var reportingPeriod2d = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q2));
            var reportingPeriod2e = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q3), new GenericQuarter(2016, QuarterNumber.Q4));
            var reportingPeriod2f = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q3), new GenericQuarter(2017, QuarterNumber.Q1));
            var reportingPeriod2g = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q4), new GenericQuarter(2017, QuarterNumber.Q1));

            // Act
            var hasOverlap1a = reportingPeriod1.HasOverlapWith(reportingPeriod2a);
            var hasOverlap1b = reportingPeriod2a.HasOverlapWith(reportingPeriod1);

            var hasOverlap2a = reportingPeriod1.HasOverlapWith(reportingPeriod2b);
            var hasOverlap2b = reportingPeriod2b.HasOverlapWith(reportingPeriod1);

            var hasOverlap3a = reportingPeriod1.HasOverlapWith(reportingPeriod2c);
            var hasOverlap3b = reportingPeriod2c.HasOverlapWith(reportingPeriod1);

            var hasOverlap4a = reportingPeriod1.HasOverlapWith(reportingPeriod2d);
            var hasOverlap4b = reportingPeriod2d.HasOverlapWith(reportingPeriod1);

            var hasOverlap5a = reportingPeriod1.HasOverlapWith(reportingPeriod2e);
            var hasOverlap5b = reportingPeriod2e.HasOverlapWith(reportingPeriod1);

            var hasOverlap6a = reportingPeriod1.HasOverlapWith(reportingPeriod2f);
            var hasOverlap6b = reportingPeriod2f.HasOverlapWith(reportingPeriod1);

            var hasOverlap7a = reportingPeriod1.HasOverlapWith(reportingPeriod2g);
            var hasOverlap7b = reportingPeriod2g.HasOverlapWith(reportingPeriod1);

            // Assert
            hasOverlap1a.Should().BeTrue();
            hasOverlap1b.Should().BeTrue();
            hasOverlap2a.Should().BeTrue();
            hasOverlap2b.Should().BeTrue();
            hasOverlap3a.Should().BeTrue();
            hasOverlap3b.Should().BeTrue();
            hasOverlap4a.Should().BeTrue();
            hasOverlap4b.Should().BeTrue();
            hasOverlap5a.Should().BeTrue();
            hasOverlap5b.Should().BeTrue();
            hasOverlap6a.Should().BeTrue();
            hasOverlap6b.Should().BeTrue();
            hasOverlap7a.Should().BeTrue();
            hasOverlap7b.Should().BeTrue();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_false___When_parameters_reportingPeriod1_and_reportingPeriod2_do_not_overlap_and_are_of_type_CalendarYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarYear>(new CalendarYear(2011), new CalendarYear(2014));

            var reportingPeriod2a = new ReportingPeriod<CalendarYear>(new CalendarYear(2009), new CalendarYear(2010));
            var reportingPeriod2b = new ReportingPeriod<CalendarYear>(new CalendarYear(2015), new CalendarYear(2018));

            // Act
            var hasOverlap1a = reportingPeriod1.HasOverlapWith(reportingPeriod2a);
            var hasOverlap1b = reportingPeriod2a.HasOverlapWith(reportingPeriod1);

            var hasOverlap2a = reportingPeriod1.HasOverlapWith(reportingPeriod2b);
            var hasOverlap2b = reportingPeriod2b.HasOverlapWith(reportingPeriod1);

            // Assert
            hasOverlap1a.Should().BeFalse();
            hasOverlap1b.Should().BeFalse();
            hasOverlap2a.Should().BeFalse();
            hasOverlap2b.Should().BeFalse();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_true___When_parameters_reportingPeriod1_and_reportingPeriod2_overlap_and_are_of_type_CalendarYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarYear>(new CalendarYear(2011), new CalendarYear(2014));

            var reportingPeriod2a = new ReportingPeriod<CalendarYear>(new CalendarYear(2009), new CalendarYear(2011));
            var reportingPeriod2b = new ReportingPeriod<CalendarYear>(new CalendarYear(2009), new CalendarYear(2012));
            var reportingPeriod2c = new ReportingPeriod<CalendarYear>(new CalendarYear(2011), new CalendarYear(2013));
            var reportingPeriod2d = new ReportingPeriod<CalendarYear>(new CalendarYear(2012), new CalendarYear(2013));
            var reportingPeriod2e = new ReportingPeriod<CalendarYear>(new CalendarYear(2012), new CalendarYear(2014));
            var reportingPeriod2f = new ReportingPeriod<CalendarYear>(new CalendarYear(2012), new CalendarYear(2016));
            var reportingPeriod2g = new ReportingPeriod<CalendarYear>(new CalendarYear(2014), new CalendarYear(2017));

            // Act
            var hasOverlap1a = reportingPeriod1.HasOverlapWith(reportingPeriod2a);
            var hasOverlap1b = reportingPeriod2a.HasOverlapWith(reportingPeriod1);

            var hasOverlap2a = reportingPeriod1.HasOverlapWith(reportingPeriod2b);
            var hasOverlap2b = reportingPeriod2b.HasOverlapWith(reportingPeriod1);

            var hasOverlap3a = reportingPeriod1.HasOverlapWith(reportingPeriod2c);
            var hasOverlap3b = reportingPeriod2c.HasOverlapWith(reportingPeriod1);

            var hasOverlap4a = reportingPeriod1.HasOverlapWith(reportingPeriod2d);
            var hasOverlap4b = reportingPeriod2d.HasOverlapWith(reportingPeriod1);

            var hasOverlap5a = reportingPeriod1.HasOverlapWith(reportingPeriod2e);
            var hasOverlap5b = reportingPeriod2e.HasOverlapWith(reportingPeriod1);

            var hasOverlap6a = reportingPeriod1.HasOverlapWith(reportingPeriod2f);
            var hasOverlap6b = reportingPeriod2f.HasOverlapWith(reportingPeriod1);

            var hasOverlap7a = reportingPeriod1.HasOverlapWith(reportingPeriod2g);
            var hasOverlap7b = reportingPeriod2g.HasOverlapWith(reportingPeriod1);

            // Assert
            hasOverlap1a.Should().BeTrue();
            hasOverlap1b.Should().BeTrue();
            hasOverlap2a.Should().BeTrue();
            hasOverlap2b.Should().BeTrue();
            hasOverlap3a.Should().BeTrue();
            hasOverlap3b.Should().BeTrue();
            hasOverlap4a.Should().BeTrue();
            hasOverlap4b.Should().BeTrue();
            hasOverlap5a.Should().BeTrue();
            hasOverlap5b.Should().BeTrue();
            hasOverlap6a.Should().BeTrue();
            hasOverlap6b.Should().BeTrue();
            hasOverlap7a.Should().BeTrue();
            hasOverlap7b.Should().BeTrue();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_false___When_parameters_reportingPeriod1_and_reportingPeriod2_do_not_overlap_and_are_of_type_FiscalYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalYear>(new FiscalYear(2011), new FiscalYear(2014));

            var reportingPeriod2a = new ReportingPeriod<FiscalYear>(new FiscalYear(2009), new FiscalYear(2010));
            var reportingPeriod2b = new ReportingPeriod<FiscalYear>(new FiscalYear(2015), new FiscalYear(2018));

            // Act
            var hasOverlap1a = reportingPeriod1.HasOverlapWith(reportingPeriod2a);
            var hasOverlap1b = reportingPeriod2a.HasOverlapWith(reportingPeriod1);

            var hasOverlap2a = reportingPeriod1.HasOverlapWith(reportingPeriod2b);
            var hasOverlap2b = reportingPeriod2b.HasOverlapWith(reportingPeriod1);

            // Assert
            hasOverlap1a.Should().BeFalse();
            hasOverlap1b.Should().BeFalse();
            hasOverlap2a.Should().BeFalse();
            hasOverlap2b.Should().BeFalse();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_true___When_parameters_reportingPeriod1_and_reportingPeriod2_overlap_and_are_of_type_FiscalYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalYear>(new FiscalYear(2011), new FiscalYear(2014));

            var reportingPeriod2a = new ReportingPeriod<FiscalYear>(new FiscalYear(2009), new FiscalYear(2011));
            var reportingPeriod2b = new ReportingPeriod<FiscalYear>(new FiscalYear(2009), new FiscalYear(2012));
            var reportingPeriod2c = new ReportingPeriod<FiscalYear>(new FiscalYear(2011), new FiscalYear(2013));
            var reportingPeriod2d = new ReportingPeriod<FiscalYear>(new FiscalYear(2012), new FiscalYear(2013));
            var reportingPeriod2e = new ReportingPeriod<FiscalYear>(new FiscalYear(2012), new FiscalYear(2014));
            var reportingPeriod2f = new ReportingPeriod<FiscalYear>(new FiscalYear(2012), new FiscalYear(2016));
            var reportingPeriod2g = new ReportingPeriod<FiscalYear>(new FiscalYear(2014), new FiscalYear(2017));

            // Act
            var hasOverlap1a = reportingPeriod1.HasOverlapWith(reportingPeriod2a);
            var hasOverlap1b = reportingPeriod2a.HasOverlapWith(reportingPeriod1);

            var hasOverlap2a = reportingPeriod1.HasOverlapWith(reportingPeriod2b);
            var hasOverlap2b = reportingPeriod2b.HasOverlapWith(reportingPeriod1);

            var hasOverlap3a = reportingPeriod1.HasOverlapWith(reportingPeriod2c);
            var hasOverlap3b = reportingPeriod2c.HasOverlapWith(reportingPeriod1);

            var hasOverlap4a = reportingPeriod1.HasOverlapWith(reportingPeriod2d);
            var hasOverlap4b = reportingPeriod2d.HasOverlapWith(reportingPeriod1);

            var hasOverlap5a = reportingPeriod1.HasOverlapWith(reportingPeriod2e);
            var hasOverlap5b = reportingPeriod2e.HasOverlapWith(reportingPeriod1);

            var hasOverlap6a = reportingPeriod1.HasOverlapWith(reportingPeriod2f);
            var hasOverlap6b = reportingPeriod2f.HasOverlapWith(reportingPeriod1);

            var hasOverlap7a = reportingPeriod1.HasOverlapWith(reportingPeriod2g);
            var hasOverlap7b = reportingPeriod2g.HasOverlapWith(reportingPeriod1);

            // Assert
            hasOverlap1a.Should().BeTrue();
            hasOverlap1b.Should().BeTrue();
            hasOverlap2a.Should().BeTrue();
            hasOverlap2b.Should().BeTrue();
            hasOverlap3a.Should().BeTrue();
            hasOverlap3b.Should().BeTrue();
            hasOverlap4a.Should().BeTrue();
            hasOverlap4b.Should().BeTrue();
            hasOverlap5a.Should().BeTrue();
            hasOverlap5b.Should().BeTrue();
            hasOverlap6a.Should().BeTrue();
            hasOverlap6b.Should().BeTrue();
            hasOverlap7a.Should().BeTrue();
            hasOverlap7b.Should().BeTrue();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_false___When_parameters_reportingPeriod1_and_reportingPeriod2_do_not_overlap_and_are_of_type_GenericYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericYear>(new GenericYear(2011), new GenericYear(2014));

            var reportingPeriod2a = new ReportingPeriod<GenericYear>(new GenericYear(2009), new GenericYear(2010));
            var reportingPeriod2b = new ReportingPeriod<GenericYear>(new GenericYear(2015), new GenericYear(2018));

            // Act
            var hasOverlap1a = reportingPeriod1.HasOverlapWith(reportingPeriod2a);
            var hasOverlap1b = reportingPeriod2a.HasOverlapWith(reportingPeriod1);

            var hasOverlap2a = reportingPeriod1.HasOverlapWith(reportingPeriod2b);
            var hasOverlap2b = reportingPeriod2b.HasOverlapWith(reportingPeriod1);

            // Assert
            hasOverlap1a.Should().BeFalse();
            hasOverlap1b.Should().BeFalse();
            hasOverlap2a.Should().BeFalse();
            hasOverlap2b.Should().BeFalse();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_true___When_parameters_reportingPeriod1_and_reportingPeriod2_overlap_and_are_of_type_GenericYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericYear>(new GenericYear(2011), new GenericYear(2014));

            var reportingPeriod2a = new ReportingPeriod<GenericYear>(new GenericYear(2009), new GenericYear(2011));
            var reportingPeriod2b = new ReportingPeriod<GenericYear>(new GenericYear(2009), new GenericYear(2012));
            var reportingPeriod2c = new ReportingPeriod<GenericYear>(new GenericYear(2011), new GenericYear(2013));
            var reportingPeriod2d = new ReportingPeriod<GenericYear>(new GenericYear(2012), new GenericYear(2013));
            var reportingPeriod2e = new ReportingPeriod<GenericYear>(new GenericYear(2012), new GenericYear(2014));
            var reportingPeriod2f = new ReportingPeriod<GenericYear>(new GenericYear(2012), new GenericYear(2016));
            var reportingPeriod2g = new ReportingPeriod<GenericYear>(new GenericYear(2014), new GenericYear(2017));

            // Act
            var hasOverlap1a = reportingPeriod1.HasOverlapWith(reportingPeriod2a);
            var hasOverlap1b = reportingPeriod2a.HasOverlapWith(reportingPeriod1);

            var hasOverlap2a = reportingPeriod1.HasOverlapWith(reportingPeriod2b);
            var hasOverlap2b = reportingPeriod2b.HasOverlapWith(reportingPeriod1);

            var hasOverlap3a = reportingPeriod1.HasOverlapWith(reportingPeriod2c);
            var hasOverlap3b = reportingPeriod2c.HasOverlapWith(reportingPeriod1);

            var hasOverlap4a = reportingPeriod1.HasOverlapWith(reportingPeriod2d);
            var hasOverlap4b = reportingPeriod2d.HasOverlapWith(reportingPeriod1);

            var hasOverlap5a = reportingPeriod1.HasOverlapWith(reportingPeriod2e);
            var hasOverlap5b = reportingPeriod2e.HasOverlapWith(reportingPeriod1);

            var hasOverlap6a = reportingPeriod1.HasOverlapWith(reportingPeriod2f);
            var hasOverlap6b = reportingPeriod2f.HasOverlapWith(reportingPeriod1);

            var hasOverlap7a = reportingPeriod1.HasOverlapWith(reportingPeriod2g);
            var hasOverlap7b = reportingPeriod2g.HasOverlapWith(reportingPeriod1);

            // Assert
            hasOverlap1a.Should().BeTrue();
            hasOverlap1b.Should().BeTrue();
            hasOverlap2a.Should().BeTrue();
            hasOverlap2b.Should().BeTrue();
            hasOverlap3a.Should().BeTrue();
            hasOverlap3b.Should().BeTrue();
            hasOverlap4a.Should().BeTrue();
            hasOverlap4b.Should().BeTrue();
            hasOverlap5a.Should().BeTrue();
            hasOverlap5b.Should().BeTrue();
            hasOverlap6a.Should().BeTrue();
            hasOverlap6b.Should().BeTrue();
            hasOverlap7a.Should().BeTrue();
            hasOverlap7b.Should().BeTrue();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_true___When_parameters_reportingPeriod1_and_reportingPeriod2_overlap_and_are_of_type_CalendarUnbounded()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarUnbounded>(new CalendarUnbounded(), new CalendarUnbounded());
            var reportingPeriod2 = new ReportingPeriod<CalendarUnbounded>(new CalendarUnbounded(), new CalendarUnbounded());

            // Act
            var hasOverlap = reportingPeriod1.HasOverlapWith(reportingPeriod2);

            // Assert
            hasOverlap.Should().BeTrue();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_true___When_parameters_reportingPeriod1_and_reportingPeriod2_overlap_and_are_of_type_FiscalUnbounded()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalUnbounded>(new FiscalUnbounded(), new FiscalUnbounded());
            var reportingPeriod2 = new ReportingPeriod<FiscalUnbounded>(new FiscalUnbounded(), new FiscalUnbounded());

            // Act
            var hasOverlap = reportingPeriod1.HasOverlapWith(reportingPeriod2);

            // Assert
            hasOverlap.Should().BeTrue();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_true___When_parameters_reportingPeriod1_and_reportingPeriod2_overlap_and_are_of_type_GenericUnbounded()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericUnbounded>(new GenericUnbounded(), new GenericUnbounded());
            var reportingPeriod2 = new ReportingPeriod<GenericUnbounded>(new GenericUnbounded(), new GenericUnbounded());

            // Act
            var hasOverlap = reportingPeriod1.HasOverlapWith(reportingPeriod2);

            // Assert
            hasOverlap.Should().BeTrue();
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_false___When_reportingPeriod1_uses_CalendarDay_and_does_not_contain_reportingPeriod2_which_does_not_use_CalendarDay()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_true___When_reportingPeriod1_uses_CalendarDay_and_contains_reportingPeriod2_which_does_not_use_CalendarDay()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_false___When_reportingPeriod1_uses_CalendarMonth_and_does_not_contain_reportingPeriod2_which_does_not_use_CalendarMonth()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_true___When_reportingPeriod1_uses_CalendarMonth_and_contains_reportingPeriod2_which_does_not_use_CalendarMonth()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_false___When_reportingPeriod1_uses_CalendarQuarter_and_does_not_contain_reportingPeriod2_which_does_not_use_CalendarQuarter()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_true___When_reportingPeriod1_uses_CalendarQuarter_and_contains_reportingPeriod2_which_does_not_use_CalendarQuarter()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_false___When_reportingPeriod1_uses_CalendarYear_and_does_not_contain_reportingPeriod2_which_does_not_use_CalendarYear()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_true___When_reportingPeriod1_uses_CalendarYear_and_contains_reportingPeriod2_which_does_not_use_CalendarYear()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_true___When_reportingPeriod1_uses_CalendarUnbounded_and_contains_reportingPeriod2_which_does_not_use_CalendarYear()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_false___When_reportingPeriod1_uses_FiscalMonth_and_does_not_contain_reportingPeriod2_which_does_not_use_FiscalMonth()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_true___When_reportingPeriod1_uses_FiscalMonth_and_contains_reportingPeriod2_which_does_not_use_FiscalMonth()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_false___When_reportingPeriod1_uses_FiscalQuarter_and_does_not_contain_reportingPeriod2_which_does_not_use_FiscalQuarter()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_true___When_reportingPeriod1_uses_FiscalQuarter_and_contains_reportingPeriod2_which_does_not_use_FiscalQuarter()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_false___When_reportingPeriod1_uses_FiscalYear_and_does_not_contain_reportingPeriod2_which_does_not_use_FiscalYear()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_true___When_reportingPeriod1_uses_FiscalYear_and_contains_reportingPeriod2_which_does_not_use_FiscalYear()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_true___When_reportingPeriod1_uses_FiscalUnbounded_and_contains_reportingPeriod2_which_does_not_use_FiscalYear()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_false___When_reportingPeriod1_uses_GenericMonth_and_does_not_contain_reportingPeriod2_which_does_not_use_GenericMonth()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_true___When_reportingPeriod1_uses_GenericMonth_and_contains_reportingPeriod2_which_does_not_use_GenericMonth()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_false___When_reportingPeriod1_uses_GenericQuarter_and_does_not_contain_reportingPeriod2_which_does_not_use_GenericQuarter()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_true___When_reportingPeriod1_uses_GenericQuarter_and_contains_reportingPeriod2_which_does_not_use_GenericQuarter()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_false___When_reportingPeriod1_uses_GenericYear_and_does_not_contain_reportingPeriod2_which_does_not_use_GenericYear()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_true___When_reportingPeriod1_uses_GenericYear_and_contains_reportingPeriod2_which_does_not_use_GenericYear()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_true___When_reportingPeriod1_uses_GenericUnbounded_and_contains_reportingPeriod2_which_does_not_use_GenericYear()
        {
        }

        [Fact]
        public static void IsGreaterThanAndAdjacentTo___Should_throw_ArgumentNullException___When_parameter_reportingPeriod1_is_null()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod<UnitOfTime>>();

            // Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.IsGreaterThanAndAdjacentTo(null, reportingPeriod));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void IsGreaterThanAndAdjacentTo___Should_throw_ArgumentNullException___When_parameter_reportingPeriod2_is_null()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod<UnitOfTime>>();

            // Act
            var ex = Record.Exception(() => reportingPeriod.IsGreaterThanAndAdjacentTo(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void IsGreaterThanAndAdjacentTo___Should_throw_ArgumentException___When_reporting_periods_have_different_UnitOfTimeKind()
        {
            // Arrange
            var reportingPeriod1 = A.Dummy<ReportingPeriod<UnitOfTime>>();
            var reportingPeriod2 = A.Dummy<ReportingPeriod<UnitOfTime>>().Whose(_ => _.GetUnitOfTimeKind() != reportingPeriod1.GetUnitOfTimeKind());

            // Act
            // ReSharper disable ExpressionIsAlwaysNull
            var ex = Record.Exception(() => reportingPeriod1.IsGreaterThanAndAdjacentTo(reportingPeriod2));
            // ReSharper restore ExpressionIsAlwaysNull

            // Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_false___When_reportingPeriod1_is_less_than_reportingPeriod2()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.One), new FiscalMonth(2016, MonthNumber.Five));
            var reportingPeriod2 = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Six), new FiscalMonth(2016, MonthNumber.Twelve));

            // Act
            var actual = reportingPeriod1.IsGreaterThanAndAdjacentTo(reportingPeriod2);

            // Assert
            actual.Should().BeFalse();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_false___When_reportingPeriod1_and_reportingPeriod2_overlap()
        {
            // Arrange
            var reportingPeriod1a = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Six), new FiscalMonth(2016, MonthNumber.Twelve));
            var reportingPeriod1b = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.One), new FiscalMonth(2016, MonthNumber.Six));

            var reportingPeriod2a = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.One), new FiscalMonth(2016, MonthNumber.Six));
            var reportingPeriod2b = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.One), new FiscalMonth(2016, MonthNumber.Six));

            var reportingPeriod3a = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Five), new FiscalMonth(2016, MonthNumber.Ten));
            var reportingPeriod3b = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.One), new FiscalMonth(2016, MonthNumber.Six));

            var reportingPeriod4a = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Five), new FiscalMonth(2016, MonthNumber.Ten));
            var reportingPeriod4b = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2016, MonthNumber.Six));

            // Act
            var actual1 = reportingPeriod1a.IsGreaterThanAndAdjacentTo(reportingPeriod1b);
            var actual2 = reportingPeriod2a.IsGreaterThanAndAdjacentTo(reportingPeriod2b);
            var actual3 = reportingPeriod3a.IsGreaterThanAndAdjacentTo(reportingPeriod3b);
            var actual4 = reportingPeriod4a.IsGreaterThanAndAdjacentTo(reportingPeriod4b);

            // Assert
            actual1.Should().BeFalse();
            actual2.Should().BeFalse();
            actual3.Should().BeFalse();
            actual4.Should().BeFalse();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_false___When_reportingPeriod1_is_greater_than_but_not_adjacent_to_reportingPeriod2()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Eight), new FiscalMonth(2016, MonthNumber.Twelve));
            var reportingPeriod2 = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.One), new FiscalMonth(2016, MonthNumber.Six));

            // Act
            var actual = reportingPeriod1.IsGreaterThanAndAdjacentTo(reportingPeriod2);

            // Assert
            actual.Should().BeFalse();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_true___When_reportingPeriod1_is_greater_than_and_adjacent_to_reportingPeriod2()
        {
            // Arrange
            var reportingPeriod1a = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Seven), new FiscalMonth(2016, MonthNumber.Twelve));
            var reportingPeriod1b = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.One), new FiscalMonth(2016, MonthNumber.Six));

            var reportingPeriod2a = new ReportingPeriod<FiscalQuarter>(QuarterNumber.Q2.ToFiscal(2015), QuarterNumber.Q3.ToFiscal(2015));
            var reportingPeriod2b = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2015, MonthNumber.One), new FiscalMonth(2015, MonthNumber.Three));

            var reportingPeriod3a = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2015, MonthNumber.Seven), new FiscalMonth(2015, MonthNumber.Eight));
            var reportingPeriod3b = new ReportingPeriod<FiscalQuarter>(QuarterNumber.Q1.ToFiscal(2015), QuarterNumber.Q2.ToFiscal(2015));

            // Act
            var actual1 = reportingPeriod1a.IsGreaterThanAndAdjacentTo(reportingPeriod1b);
            var actual2 = reportingPeriod2a.IsGreaterThanAndAdjacentTo(reportingPeriod2b);
            var actual3 = reportingPeriod3a.IsGreaterThanAndAdjacentTo(reportingPeriod3b);

            // Assert
            actual1.Should().BeTrue();
            actual2.Should().BeTrue();
            actual3.Should().BeTrue();
        }

        // ReSharper restore InconsistentNaming
    }
}

// ReSharper restore CheckNamespace