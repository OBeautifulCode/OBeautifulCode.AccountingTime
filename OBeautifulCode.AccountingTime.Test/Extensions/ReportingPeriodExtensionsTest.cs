// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriodExtensionsTest.cs" company="OBeautifulCode">
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

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Testing this class requires lots of types because of the number of unit-of-time types intersected with the options for reporting period.")]
    public static class ReportingPeriodExtensionsTest
    {
        // ReSharper disable InconsistentNaming
        [Fact]
        public static void IsInReportingPeriod___Should_throw_ArgumentNullException___When_parameter_unitOfTime_is_null()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod<UnitOfTime>>();

            // Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.IsInReportingPeriod(null, reportingPeriod));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void IsInReportingPeriod___Should_throw_ArgumentNullException___When_parameter_reportingPeriod_is_null()
        {
            // Arrange
            var unitOfTime = A.Dummy<UnitOfTime>();

            // Act
            var ex = Record.Exception(() => unitOfTime.IsInReportingPeriod(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void IsInReportingPeriod___Should_throw_ArgumentException___When_parameter_unitOfTime_and_reportingPeriod_represent_different_concrete_subclasses_of_UnitOfTime()
        {
            // Arrange
            var unitOfTime1 = A.Dummy<CalendarDay>();
            var reportingPeriod1 = A.Dummy<ReportingPeriod<CalendarMonth>>();

            var unitOfTime2 = A.Dummy<FiscalQuarter>();
            var reportingPeriod2 = A.Dummy<ReportingPeriod<GenericQuarter>>();

            // Act
            var ex1 = Record.Exception(() => unitOfTime1.IsInReportingPeriod(reportingPeriod1));
            var ex2 = Record.Exception(() => unitOfTime2.IsInReportingPeriod(reportingPeriod2));

            // Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex2.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void IsInReportingPeriod___Should_return_false___When_parameter_unitOfTime_is_in_reportingPeriod_and_both_represent_CalendarDay()
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
            var isInReportingPeriod1a = unitOfTime1a.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1b = unitOfTime1b.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1c = unitOfTime1c.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1d = unitOfTime1d.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1e = unitOfTime1e.IsInReportingPeriod(reportingPeriod1);

            var isInReportingPeriod2a = unitOfTime2a.IsInReportingPeriod(reportingPeriod2);
            var isInReportingPeriod2b = unitOfTime2b.IsInReportingPeriod(reportingPeriod2);
            var isInReportingPeriod2c = unitOfTime2c.IsInReportingPeriod(reportingPeriod2);
            var isInReportingPeriod2d = unitOfTime2d.IsInReportingPeriod(reportingPeriod2);

            // Assert
            isInReportingPeriod1a.Should().BeFalse();
            isInReportingPeriod1b.Should().BeFalse();
            isInReportingPeriod1c.Should().BeFalse();
            isInReportingPeriod1d.Should().BeFalse();
            isInReportingPeriod1e.Should().BeFalse();

            isInReportingPeriod2a.Should().BeFalse();
            isInReportingPeriod2b.Should().BeFalse();
            isInReportingPeriod2c.Should().BeFalse();
            isInReportingPeriod2d.Should().BeFalse();
        }

        [Fact]
        public static void IsInReportingPeriod___Should_return_true___When_parameter_unitOfTime_is_in_reportingPeriod_and_both_represent_CalendarDay()
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
            var isInReportingPeriod1a = unitOfTime1a.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1b = unitOfTime1b.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1c = unitOfTime1c.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1d = unitOfTime1d.IsInReportingPeriod(reportingPeriod1);

            var isInReportingPeriod2 = unitOfTime2.IsInReportingPeriod(reportingPeriod2);

            // Assert
            isInReportingPeriod1a.Should().BeTrue();
            isInReportingPeriod1b.Should().BeTrue();
            isInReportingPeriod1c.Should().BeTrue();
            isInReportingPeriod1d.Should().BeTrue();

            isInReportingPeriod2.Should().BeTrue();
        }

        [Fact]
        public static void IsInReportingPeriod___Should_return_false___When_parameter_unitOfTime_is_in_reportingPeriod_and_both_represent_CalendarMonth()
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
            var isInReportingPeriod1a = unitOfTime1a.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1b = unitOfTime1b.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1c = unitOfTime1c.IsInReportingPeriod(reportingPeriod1);

            var isInReportingPeriod2a = unitOfTime2a.IsInReportingPeriod(reportingPeriod2);
            var isInReportingPeriod2b = unitOfTime2b.IsInReportingPeriod(reportingPeriod2);
            var isInReportingPeriod2c = unitOfTime2c.IsInReportingPeriod(reportingPeriod2);
            var isInReportingPeriod2d = unitOfTime2d.IsInReportingPeriod(reportingPeriod2);

            // Assert
            isInReportingPeriod1a.Should().BeFalse();
            isInReportingPeriod1b.Should().BeFalse();
            isInReportingPeriod1c.Should().BeFalse();

            isInReportingPeriod2a.Should().BeFalse();
            isInReportingPeriod2b.Should().BeFalse();
            isInReportingPeriod2c.Should().BeFalse();
            isInReportingPeriod2d.Should().BeFalse();
        }

        [Fact]
        public static void IsInReportingPeriod___Should_return_true___When_parameter_unitOfTime_is_in_reportingPeriod_and_both_represent_CalendarMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.September), new CalendarMonth(2017, MonthOfYear.February));
            var reportingPeriod2 = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.September), new CalendarMonth(2016, MonthOfYear.September));

            var unitOfTime1a = new CalendarMonth(2016, MonthOfYear.September);
            var unitOfTime1b = new CalendarMonth(2017, MonthOfYear.February);
            var unitOfTime1c = new CalendarMonth(2016, MonthOfYear.December);

            var unitOfTime2 = new CalendarMonth(2016, MonthOfYear.September);

            // Act
            var isInReportingPeriod1a = unitOfTime1a.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1b = unitOfTime1b.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1c = unitOfTime1c.IsInReportingPeriod(reportingPeriod1);

            var isInReportingPeriod2 = unitOfTime2.IsInReportingPeriod(reportingPeriod2);

            // Assert
            isInReportingPeriod1a.Should().BeTrue();
            isInReportingPeriod1b.Should().BeTrue();
            isInReportingPeriod1c.Should().BeTrue();

            isInReportingPeriod2.Should().BeTrue();
        }

        [Fact]
        public static void IsInReportingPeriod___Should_return_false___When_parameter_unitOfTime_is_in_reportingPeriod_and_both_represent_FiscalMonth()
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
            var isInReportingPeriod1a = unitOfTime1a.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1b = unitOfTime1b.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1c = unitOfTime1c.IsInReportingPeriod(reportingPeriod1);

            var isInReportingPeriod2a = unitOfTime2a.IsInReportingPeriod(reportingPeriod2);
            var isInReportingPeriod2b = unitOfTime2b.IsInReportingPeriod(reportingPeriod2);
            var isInReportingPeriod2c = unitOfTime2c.IsInReportingPeriod(reportingPeriod2);
            var isInReportingPeriod2d = unitOfTime2d.IsInReportingPeriod(reportingPeriod2);

            // Assert
            isInReportingPeriod1a.Should().BeFalse();
            isInReportingPeriod1b.Should().BeFalse();
            isInReportingPeriod1c.Should().BeFalse();

            isInReportingPeriod2a.Should().BeFalse();
            isInReportingPeriod2b.Should().BeFalse();
            isInReportingPeriod2c.Should().BeFalse();
            isInReportingPeriod2d.Should().BeFalse();
        }

        [Fact]
        public static void IsInReportingPeriod___Should_return_true___When_parameter_unitOfTime_is_in_reportingPeriod_and_both_represent_FiscalMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Nine), new FiscalMonth(2017, MonthNumber.Two));
            var reportingPeriod2 = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Nine), new FiscalMonth(2016, MonthNumber.Nine));

            var unitOfTime1a = new FiscalMonth(2016, MonthNumber.Nine);
            var unitOfTime1b = new FiscalMonth(2017, MonthNumber.Two);
            var unitOfTime1c = new FiscalMonth(2016, MonthNumber.Twelve);

            var unitOfTime2 = new FiscalMonth(2016, MonthNumber.Nine);

            // Act
            var isInReportingPeriod1a = unitOfTime1a.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1b = unitOfTime1b.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1c = unitOfTime1c.IsInReportingPeriod(reportingPeriod1);

            var isInReportingPeriod2 = unitOfTime2.IsInReportingPeriod(reportingPeriod2);

            // Assert
            isInReportingPeriod1a.Should().BeTrue();
            isInReportingPeriod1b.Should().BeTrue();
            isInReportingPeriod1c.Should().BeTrue();

            isInReportingPeriod2.Should().BeTrue();
        }

        [Fact]
        public static void IsInReportingPeriod___Should_return_false___When_parameter_unitOfTime_is_in_reportingPeriod_and_both_represent_GenericMonth()
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
            var isInReportingPeriod1a = unitOfTime1a.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1b = unitOfTime1b.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1c = unitOfTime1c.IsInReportingPeriod(reportingPeriod1);

            var isInReportingPeriod2a = unitOfTime2a.IsInReportingPeriod(reportingPeriod2);
            var isInReportingPeriod2b = unitOfTime2b.IsInReportingPeriod(reportingPeriod2);
            var isInReportingPeriod2c = unitOfTime2c.IsInReportingPeriod(reportingPeriod2);
            var isInReportingPeriod2d = unitOfTime2d.IsInReportingPeriod(reportingPeriod2);

            // Assert
            isInReportingPeriod1a.Should().BeFalse();
            isInReportingPeriod1b.Should().BeFalse();
            isInReportingPeriod1c.Should().BeFalse();

            isInReportingPeriod2a.Should().BeFalse();
            isInReportingPeriod2b.Should().BeFalse();
            isInReportingPeriod2c.Should().BeFalse();
            isInReportingPeriod2d.Should().BeFalse();
        }

        [Fact]
        public static void IsInReportingPeriod___Should_return_true___When_parameter_unitOfTime_is_in_reportingPeriod_and_both_represent_GenericMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Nine), new GenericMonth(2017, MonthNumber.Two));
            var reportingPeriod2 = new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Nine), new GenericMonth(2016, MonthNumber.Nine));

            var unitOfTime1a = new GenericMonth(2016, MonthNumber.Nine);
            var unitOfTime1b = new GenericMonth(2017, MonthNumber.Two);
            var unitOfTime1c = new GenericMonth(2016, MonthNumber.Twelve);

            var unitOfTime2 = new GenericMonth(2016, MonthNumber.Nine);

            // Act
            var isInReportingPeriod1a = unitOfTime1a.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1b = unitOfTime1b.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1c = unitOfTime1c.IsInReportingPeriod(reportingPeriod1);

            var isInReportingPeriod2 = unitOfTime2.IsInReportingPeriod(reportingPeriod2);

            // Assert
            isInReportingPeriod1a.Should().BeTrue();
            isInReportingPeriod1b.Should().BeTrue();
            isInReportingPeriod1c.Should().BeTrue();

            isInReportingPeriod2.Should().BeTrue();
        }

        [Fact]
        public static void IsInReportingPeriod___Should_return_false___When_parameter_unitOfTime_is_in_reportingPeriod_and_both_represent_CalendarQuarter()
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
            var isInReportingPeriod1a = unitOfTime1a.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1b = unitOfTime1b.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1c = unitOfTime1c.IsInReportingPeriod(reportingPeriod1);

            var isInReportingPeriod2a = unitOfTime2a.IsInReportingPeriod(reportingPeriod2);
            var isInReportingPeriod2b = unitOfTime2b.IsInReportingPeriod(reportingPeriod2);
            var isInReportingPeriod2c = unitOfTime2c.IsInReportingPeriod(reportingPeriod2);
            var isInReportingPeriod2d = unitOfTime2d.IsInReportingPeriod(reportingPeriod2);

            // Assert
            isInReportingPeriod1a.Should().BeFalse();
            isInReportingPeriod1b.Should().BeFalse();
            isInReportingPeriod1c.Should().BeFalse();

            isInReportingPeriod2a.Should().BeFalse();
            isInReportingPeriod2b.Should().BeFalse();
            isInReportingPeriod2c.Should().BeFalse();
            isInReportingPeriod2d.Should().BeFalse();
        }

        [Fact]
        public static void IsInReportingPeriod___Should_return_true___When_parameter_unitOfTime_is_in_reportingPeriod_and_both_represent_CalendarQuarter()
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
            var isInReportingPeriod1a = unitOfTime1a.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1b = unitOfTime1b.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1c = unitOfTime1c.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1d = unitOfTime1d.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1e = unitOfTime1e.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1f = unitOfTime1f.IsInReportingPeriod(reportingPeriod1);

            var isInReportingPeriod2 = unitOfTime2.IsInReportingPeriod(reportingPeriod2);

            // Assert
            isInReportingPeriod1a.Should().BeTrue();
            isInReportingPeriod1b.Should().BeTrue();
            isInReportingPeriod1c.Should().BeTrue();
            isInReportingPeriod1d.Should().BeTrue();
            isInReportingPeriod1e.Should().BeTrue();
            isInReportingPeriod1f.Should().BeTrue();

            isInReportingPeriod2.Should().BeTrue();
        }

        [Fact]
        public static void IsInReportingPeriod___Should_return_false___When_parameter_unitOfTime_is_in_reportingPeriod_and_both_represent_FiscalQuarter()
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
            var isInReportingPeriod1a = unitOfTime1a.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1b = unitOfTime1b.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1c = unitOfTime1c.IsInReportingPeriod(reportingPeriod1);

            var isInReportingPeriod2a = unitOfTime2a.IsInReportingPeriod(reportingPeriod2);
            var isInReportingPeriod2b = unitOfTime2b.IsInReportingPeriod(reportingPeriod2);
            var isInReportingPeriod2c = unitOfTime2c.IsInReportingPeriod(reportingPeriod2);
            var isInReportingPeriod2d = unitOfTime2d.IsInReportingPeriod(reportingPeriod2);

            // Assert
            isInReportingPeriod1a.Should().BeFalse();
            isInReportingPeriod1b.Should().BeFalse();
            isInReportingPeriod1c.Should().BeFalse();

            isInReportingPeriod2a.Should().BeFalse();
            isInReportingPeriod2b.Should().BeFalse();
            isInReportingPeriod2c.Should().BeFalse();
            isInReportingPeriod2d.Should().BeFalse();
        }

        [Fact]
        public static void IsInReportingPeriod___Should_return_true___When_parameter_unitOfTime_is_in_reportingPeriod_and_both_represent_FiscalQuarter()
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
            var isInReportingPeriod1a = unitOfTime1a.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1b = unitOfTime1b.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1c = unitOfTime1c.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1d = unitOfTime1d.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1e = unitOfTime1e.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1f = unitOfTime1f.IsInReportingPeriod(reportingPeriod1);

            var isInReportingPeriod2 = unitOfTime2.IsInReportingPeriod(reportingPeriod2);

            // Assert
            isInReportingPeriod1a.Should().BeTrue();
            isInReportingPeriod1b.Should().BeTrue();
            isInReportingPeriod1c.Should().BeTrue();
            isInReportingPeriod1d.Should().BeTrue();
            isInReportingPeriod1e.Should().BeTrue();
            isInReportingPeriod1f.Should().BeTrue();

            isInReportingPeriod2.Should().BeTrue();
        }

        [Fact]
        public static void IsInReportingPeriod___Should_return_false___When_parameter_unitOfTime_is_in_reportingPeriod_and_both_represent_GenericQuarter()
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
            var isInReportingPeriod1a = unitOfTime1a.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1b = unitOfTime1b.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1c = unitOfTime1c.IsInReportingPeriod(reportingPeriod1);

            var isInReportingPeriod2a = unitOfTime2a.IsInReportingPeriod(reportingPeriod2);
            var isInReportingPeriod2b = unitOfTime2b.IsInReportingPeriod(reportingPeriod2);
            var isInReportingPeriod2c = unitOfTime2c.IsInReportingPeriod(reportingPeriod2);
            var isInReportingPeriod2d = unitOfTime2d.IsInReportingPeriod(reportingPeriod2);

            // Assert
            isInReportingPeriod1a.Should().BeFalse();
            isInReportingPeriod1b.Should().BeFalse();
            isInReportingPeriod1c.Should().BeFalse();

            isInReportingPeriod2a.Should().BeFalse();
            isInReportingPeriod2b.Should().BeFalse();
            isInReportingPeriod2c.Should().BeFalse();
            isInReportingPeriod2d.Should().BeFalse();
        }

        [Fact]
        public static void IsInReportingPeriod___Should_return_true___When_parameter_unitOfTime_is_in_reportingPeriod_and_both_represent_GenericQuarter()
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
            var isInReportingPeriod1a = unitOfTime1a.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1b = unitOfTime1b.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1c = unitOfTime1c.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1d = unitOfTime1d.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1e = unitOfTime1e.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1f = unitOfTime1f.IsInReportingPeriod(reportingPeriod1);

            var isInReportingPeriod2 = unitOfTime2.IsInReportingPeriod(reportingPeriod2);

            // Assert
            isInReportingPeriod1a.Should().BeTrue();
            isInReportingPeriod1b.Should().BeTrue();
            isInReportingPeriod1c.Should().BeTrue();
            isInReportingPeriod1d.Should().BeTrue();
            isInReportingPeriod1e.Should().BeTrue();
            isInReportingPeriod1f.Should().BeTrue();

            isInReportingPeriod2.Should().BeTrue();
        }

        [Fact]
        public static void IsInReportingPeriod___Should_return_false___When_parameter_unitOfTime_is_in_reportingPeriod_and_both_represent_CalendarYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2018));
            var reportingPeriod2 = new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2016));

            var unitOfTime1a = new CalendarYear(2015);
            var unitOfTime1b = new CalendarYear(2019);

            var unitOfTime2a = new CalendarYear(2015);
            var unitOfTime2b = new CalendarYear(2017);

            // Act
            var isInReportingPeriod1a = unitOfTime1a.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1b = unitOfTime1b.IsInReportingPeriod(reportingPeriod1);

            var isInReportingPeriod2a = unitOfTime2a.IsInReportingPeriod(reportingPeriod2);
            var isInReportingPeriod2b = unitOfTime2b.IsInReportingPeriod(reportingPeriod2);

            // Assert
            isInReportingPeriod1a.Should().BeFalse();
            isInReportingPeriod1b.Should().BeFalse();

            isInReportingPeriod2a.Should().BeFalse();
            isInReportingPeriod2b.Should().BeFalse();
        }

        [Fact]
        public static void IsInReportingPeriod___Should_return_true___When_parameter_unitOfTime_is_in_reportingPeriod_and_both_represent_CalendarYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2018));
            var reportingPeriod2 = new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2016));

            var unitOfTime1a = new CalendarYear(2016);
            var unitOfTime1b = new CalendarYear(2017);
            var unitOfTime1c = new CalendarYear(2018);

            var unitOfTime2 = new CalendarYear(2016);

            // Act
            var isInReportingPeriod1a = unitOfTime1a.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1b = unitOfTime1b.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1c = unitOfTime1c.IsInReportingPeriod(reportingPeriod1);

            var isInReportingPeriod2 = unitOfTime2.IsInReportingPeriod(reportingPeriod2);

            // Assert
            isInReportingPeriod1a.Should().BeTrue();
            isInReportingPeriod1b.Should().BeTrue();
            isInReportingPeriod1c.Should().BeTrue();

            isInReportingPeriod2.Should().BeTrue();
        }

        [Fact]
        public static void IsInReportingPeriod___Should_return_false___When_parameter_unitOfTime_is_in_reportingPeriod_and_both_represent_FiscalYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2018));
            var reportingPeriod2 = new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2016));

            var unitOfTime1a = new FiscalYear(2015);
            var unitOfTime1b = new FiscalYear(2019);

            var unitOfTime2a = new FiscalYear(2015);
            var unitOfTime2b = new FiscalYear(2017);

            // Act
            var isInReportingPeriod1a = unitOfTime1a.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1b = unitOfTime1b.IsInReportingPeriod(reportingPeriod1);

            var isInReportingPeriod2a = unitOfTime2a.IsInReportingPeriod(reportingPeriod2);
            var isInReportingPeriod2b = unitOfTime2b.IsInReportingPeriod(reportingPeriod2);

            // Assert
            isInReportingPeriod1a.Should().BeFalse();
            isInReportingPeriod1b.Should().BeFalse();

            isInReportingPeriod2a.Should().BeFalse();
            isInReportingPeriod2b.Should().BeFalse();
        }

        [Fact]
        public static void IsInReportingPeriod___Should_return_true___When_parameter_unitOfTime_is_in_reportingPeriod_and_both_represent_FiscalYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2018));
            var reportingPeriod2 = new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2016));

            var unitOfTime1a = new FiscalYear(2016);
            var unitOfTime1b = new FiscalYear(2017);
            var unitOfTime1c = new FiscalYear(2018);

            var unitOfTime2 = new FiscalYear(2016);

            // Act
            var isInReportingPeriod1a = unitOfTime1a.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1b = unitOfTime1b.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1c = unitOfTime1c.IsInReportingPeriod(reportingPeriod1);

            var isInReportingPeriod2 = unitOfTime2.IsInReportingPeriod(reportingPeriod2);

            // Assert
            isInReportingPeriod1a.Should().BeTrue();
            isInReportingPeriod1b.Should().BeTrue();
            isInReportingPeriod1c.Should().BeTrue();

            isInReportingPeriod2.Should().BeTrue();
        }

        [Fact]
        public static void IsInReportingPeriod___Should_return_false___When_parameter_unitOfTime_is_in_reportingPeriod_and_both_represent_GenericYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2018));
            var reportingPeriod2 = new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2016));

            var unitOfTime1a = new GenericYear(2015);
            var unitOfTime1b = new GenericYear(2019);

            var unitOfTime2a = new GenericYear(2015);
            var unitOfTime2b = new GenericYear(2017);

            // Act
            var isInReportingPeriod1a = unitOfTime1a.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1b = unitOfTime1b.IsInReportingPeriod(reportingPeriod1);

            var isInReportingPeriod2a = unitOfTime2a.IsInReportingPeriod(reportingPeriod2);
            var isInReportingPeriod2b = unitOfTime2b.IsInReportingPeriod(reportingPeriod2);

            // Assert
            isInReportingPeriod1a.Should().BeFalse();
            isInReportingPeriod1b.Should().BeFalse();

            isInReportingPeriod2a.Should().BeFalse();
            isInReportingPeriod2b.Should().BeFalse();
        }

        [Fact]
        public static void IsInReportingPeriod___Should_return_true___When_parameter_unitOfTime_is_in_reportingPeriod_and_both_represent_GenericYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2018));
            var reportingPeriod2 = new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2016));

            var unitOfTime1a = new GenericYear(2016);
            var unitOfTime1b = new GenericYear(2017);
            var unitOfTime1c = new GenericYear(2018);

            var unitOfTime2 = new GenericYear(2016);

            // Act
            var isInReportingPeriod1a = unitOfTime1a.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1b = unitOfTime1b.IsInReportingPeriod(reportingPeriod1);
            var isInReportingPeriod1c = unitOfTime1c.IsInReportingPeriod(reportingPeriod1);

            var isInReportingPeriod2 = unitOfTime2.IsInReportingPeriod(reportingPeriod2);

            // Assert
            isInReportingPeriod1a.Should().BeTrue();
            isInReportingPeriod1b.Should().BeTrue();
            isInReportingPeriod1c.Should().BeTrue();

            isInReportingPeriod2.Should().BeTrue();
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
        public static void HasOverlapWith___Should_throw_ArgumentException___When_parameters_reportingPeriod1_and_reportingPeriod1_represent_different_concrete_subclasses_of_UnitOfTime()
        {
            // Arrange
            var reportingPeriod1a = A.Dummy<ReportingPeriod<FiscalQuarter>>();
            var reportingPeriod1b = A.Dummy<ReportingPeriod<FiscalYear>>();

            var reportingPeriod2a = A.Dummy<ReportingPeriod<CalendarQuarter>>();
            var reportingPeriod2b = A.Dummy<ReportingPeriod<GenericQuarter>>();

            // Act
            var ex1 = Record.Exception(() => reportingPeriod1a.HasOverlapWith(reportingPeriod1b));
            var ex2 = Record.Exception(() => reportingPeriod2a.HasOverlapWith(reportingPeriod2b));

            // Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex2.Should().BeOfType<ArgumentException>();
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
        public static void NumberOfUnitsWithin___Should_throw_ArgumentNullException___When_parameter_reportingPeriod_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.NumberOfUnitsWithin(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void NumberOfUnitsWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_CalendarDay()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight));
            var reportingPeriod2 = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Three));
            var reportingPeriod3 = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Three), new CalendarDay(2017, MonthOfYear.March, DayOfMonth.Two));

            // Act
            var actualUnits1 = reportingPeriod1.NumberOfUnitsWithin();
            var actualUnits2 = reportingPeriod2.NumberOfUnitsWithin();
            var actualUnits3 = reportingPeriod3.NumberOfUnitsWithin();

            // Assert
            actualUnits1.Should().Be(1);
            actualUnits2.Should().Be(5);
            actualUnits3.Should().Be(365);
        }

        [Fact]
        public static void NumberOfUnitsWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_CalendarMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.February));
            var reportingPeriod2 = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.April));
            var reportingPeriod3 = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2017, MonthOfYear.January));

            // Act
            var actualUnits1 = reportingPeriod1.NumberOfUnitsWithin();
            var actualUnits2 = reportingPeriod2.NumberOfUnitsWithin();
            var actualUnits3 = reportingPeriod3.NumberOfUnitsWithin();

            // Assert
            actualUnits1.Should().Be(1);
            actualUnits2.Should().Be(3);
            actualUnits3.Should().Be(12);
        }

        [Fact]
        public static void NumberOfUnitsWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_FiscalMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Two));
            var reportingPeriod2 = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Three));
            var reportingPeriod3 = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2017, MonthNumber.One));

            // Act
            var actualUnits1 = reportingPeriod1.NumberOfUnitsWithin();
            var actualUnits2 = reportingPeriod2.NumberOfUnitsWithin();
            var actualUnits3 = reportingPeriod3.NumberOfUnitsWithin();

            // Assert
            actualUnits1.Should().Be(1);
            actualUnits2.Should().Be(2);
            actualUnits3.Should().Be(12);
        }

        [Fact]
        public static void NumberOfUnitsWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_GenericMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Two));
            var reportingPeriod2 = new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Three));
            var reportingPeriod3 = new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2017, MonthNumber.One));

            // Act
            var actualUnits1 = reportingPeriod1.NumberOfUnitsWithin();
            var actualUnits2 = reportingPeriod2.NumberOfUnitsWithin();
            var actualUnits3 = reportingPeriod3.NumberOfUnitsWithin();

            // Assert
            actualUnits1.Should().Be(1);
            actualUnits2.Should().Be(2);
            actualUnits3.Should().Be(12);
        }

        [Fact]
        public static void NumberOfUnitsWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_CalendarQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q2));
            var reportingPeriod2 = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q4));
            var reportingPeriod3 = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2017, QuarterNumber.Q3));

            // Act
            var actualUnits1 = reportingPeriod1.NumberOfUnitsWithin();
            var actualUnits2 = reportingPeriod2.NumberOfUnitsWithin();
            var actualUnits3 = reportingPeriod3.NumberOfUnitsWithin();

            // Assert
            actualUnits1.Should().Be(1);
            actualUnits2.Should().Be(3);
            actualUnits3.Should().Be(6);
        }

        [Fact]
        public static void NumberOfUnitsWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_FiscalQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q2));
            var reportingPeriod2 = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q4));
            var reportingPeriod3 = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2017, QuarterNumber.Q3));

            // Act
            var actualUnits1 = reportingPeriod1.NumberOfUnitsWithin();
            var actualUnits2 = reportingPeriod2.NumberOfUnitsWithin();
            var actualUnits3 = reportingPeriod3.NumberOfUnitsWithin();

            // Assert
            actualUnits1.Should().Be(1);
            actualUnits2.Should().Be(3);
            actualUnits3.Should().Be(6);
        }

        [Fact]
        public static void NumberOfUnitsWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_GenericQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q2));
            var reportingPeriod2 = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q4));
            var reportingPeriod3 = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2017, QuarterNumber.Q3));

            // Act
            var actualUnits1 = reportingPeriod1.NumberOfUnitsWithin();
            var actualUnits2 = reportingPeriod2.NumberOfUnitsWithin();
            var actualUnits3 = reportingPeriod3.NumberOfUnitsWithin();

            // Assert
            actualUnits1.Should().Be(1);
            actualUnits2.Should().Be(3);
            actualUnits3.Should().Be(6);
        }

        [Fact]
        public static void NumberOfUnitsWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_CalendarYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2016));
            var reportingPeriod2 = new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2017));
            var reportingPeriod3 = new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2018));

            // Act
            var actualUnits1 = reportingPeriod1.NumberOfUnitsWithin();
            var actualUnits2 = reportingPeriod2.NumberOfUnitsWithin();
            var actualUnits3 = reportingPeriod3.NumberOfUnitsWithin();

            // Assert
            actualUnits1.Should().Be(1);
            actualUnits2.Should().Be(2);
            actualUnits3.Should().Be(3);
        }

        [Fact]
        public static void NumberOfUnitsWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_FiscalYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2016));
            var reportingPeriod2 = new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2017));
            var reportingPeriod3 = new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2018));

            // Act
            var actualUnits1 = reportingPeriod1.NumberOfUnitsWithin();
            var actualUnits2 = reportingPeriod2.NumberOfUnitsWithin();
            var actualUnits3 = reportingPeriod3.NumberOfUnitsWithin();

            // Assert
            actualUnits1.Should().Be(1);
            actualUnits2.Should().Be(2);
            actualUnits3.Should().Be(3);
        }

        [Fact]
        public static void NumberOfUnitsWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_GenericYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2016));
            var reportingPeriod2 = new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2017));
            var reportingPeriod3 = new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2018));

            // Act
            var actualUnits1 = reportingPeriod1.NumberOfUnitsWithin();
            var actualUnits2 = reportingPeriod2.NumberOfUnitsWithin();
            var actualUnits3 = reportingPeriod3.NumberOfUnitsWithin();

            // Assert
            actualUnits1.Should().Be(1);
            actualUnits2.Should().Be(2);
            actualUnits3.Should().Be(3);
        }

        [Fact]
        public static void GetUnitsWithin___Should_throw_ArgumentNullException___When_parameter_reportingPeriod_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.GetUnitsWithin<UnitOfTime>(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void GetUnitsWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_CalendarDay()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight));
            var reportingPeriod2 = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Three));

            // Act
            var actualUnits1 = reportingPeriod1.GetUnitsWithin<UnitOfTime>();
            var actualUnits2 = reportingPeriod2.GetUnitsWithin<UnitOfTime>();

            // Assert
            actualUnits1.Should().Equal(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight));
            actualUnits2.Should().Equal(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Two), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Three));
        }

        [Fact]
        public static void GetUnitsWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_CalendarMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.February));
            var reportingPeriod2 = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.April));
            var reportingPeriod3 = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2017, MonthOfYear.January));

            // Act
            var actualUnits1 = reportingPeriod1.GetUnitsWithin<UnitOfTime>();
            var actualUnits2 = reportingPeriod2.GetUnitsWithin<UnitOfTime>();
            var actualUnits3 = reportingPeriod3.GetUnitsWithin<UnitOfTime>();

            // Assert
            actualUnits1.Should().Equal(new CalendarMonth(2016, MonthOfYear.February));
            actualUnits2.Should().Equal(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.March), new CalendarMonth(2016, MonthOfYear.April));
            actualUnits3.Should().Equal(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.March), new CalendarMonth(2016, MonthOfYear.April), new CalendarMonth(2016, MonthOfYear.May), new CalendarMonth(2016, MonthOfYear.June), new CalendarMonth(2016, MonthOfYear.July), new CalendarMonth(2016, MonthOfYear.August), new CalendarMonth(2016, MonthOfYear.September), new CalendarMonth(2016, MonthOfYear.October), new CalendarMonth(2016, MonthOfYear.November), new CalendarMonth(2016, MonthOfYear.December), new CalendarMonth(2017, MonthOfYear.January));
        }

        [Fact]
        public static void GetUnitsWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_FiscalMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Two));
            var reportingPeriod2 = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Three));
            var reportingPeriod3 = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2017, MonthNumber.One));

            // Act
            var actualUnits1 = reportingPeriod1.GetUnitsWithin<UnitOfTime>();
            var actualUnits2 = reportingPeriod2.GetUnitsWithin<UnitOfTime>();
            var actualUnits3 = reportingPeriod3.GetUnitsWithin<UnitOfTime>();

            // Assert
            actualUnits1.Should().Equal(new FiscalMonth(2016, MonthNumber.Two));
            actualUnits2.Should().Equal(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Three));
            actualUnits3.Should().Equal(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Three), new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2016, MonthNumber.Five), new FiscalMonth(2016, MonthNumber.Six), new FiscalMonth(2016, MonthNumber.Seven), new FiscalMonth(2016, MonthNumber.Eight), new FiscalMonth(2016, MonthNumber.Nine), new FiscalMonth(2016, MonthNumber.Ten), new FiscalMonth(2016, MonthNumber.Eleven), new FiscalMonth(2016, MonthNumber.Twelve), new FiscalMonth(2017, MonthNumber.One));
        }

        [Fact]
        public static void GetUnitsWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_GenericMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Two));
            var reportingPeriod2 = new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Three));
            var reportingPeriod3 = new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2017, MonthNumber.One));

            // Act
            var actualUnits1 = reportingPeriod1.GetUnitsWithin<UnitOfTime>();
            var actualUnits2 = reportingPeriod2.GetUnitsWithin<UnitOfTime>();
            var actualUnits3 = reportingPeriod3.GetUnitsWithin<UnitOfTime>();

            // Assert
            actualUnits1.Should().Equal(new GenericMonth(2016, MonthNumber.Two));
            actualUnits2.Should().Equal(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Three));
            actualUnits3.Should().Equal(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Three), new GenericMonth(2016, MonthNumber.Four), new GenericMonth(2016, MonthNumber.Five), new GenericMonth(2016, MonthNumber.Six), new GenericMonth(2016, MonthNumber.Seven), new GenericMonth(2016, MonthNumber.Eight), new GenericMonth(2016, MonthNumber.Nine), new GenericMonth(2016, MonthNumber.Ten), new GenericMonth(2016, MonthNumber.Eleven), new GenericMonth(2016, MonthNumber.Twelve), new GenericMonth(2017, MonthNumber.One));
        }

        [Fact]
        public static void GetUnitsWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_CalendarQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q2));
            var reportingPeriod2 = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q4));
            var reportingPeriod3 = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2017, QuarterNumber.Q3));

            // Act
            var actualUnits1 = reportingPeriod1.GetUnitsWithin<UnitOfTime>();
            var actualUnits2 = reportingPeriod2.GetUnitsWithin<UnitOfTime>();
            var actualUnits3 = reportingPeriod3.GetUnitsWithin<UnitOfTime>();

            // Assert
            actualUnits1.Should().Equal(new CalendarQuarter(2016, QuarterNumber.Q2));
            actualUnits2.Should().Equal(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q3), new CalendarQuarter(2016, QuarterNumber.Q4));
            actualUnits3.Should().Equal(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q3), new CalendarQuarter(2016, QuarterNumber.Q4), new CalendarQuarter(2017, QuarterNumber.Q1), new CalendarQuarter(2017, QuarterNumber.Q2), new CalendarQuarter(2017, QuarterNumber.Q3));
        }

        [Fact]
        public static void GetUnitsWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_FiscalQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q2));
            var reportingPeriod2 = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q4));
            var reportingPeriod3 = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2017, QuarterNumber.Q3));

            // Act
            var actualUnits1 = reportingPeriod1.GetUnitsWithin<UnitOfTime>();
            var actualUnits2 = reportingPeriod2.GetUnitsWithin<UnitOfTime>();
            var actualUnits3 = reportingPeriod3.GetUnitsWithin<UnitOfTime>();

            // Assert
            actualUnits1.Should().Equal(new FiscalQuarter(2016, QuarterNumber.Q2));
            actualUnits2.Should().Equal(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q3), new FiscalQuarter(2016, QuarterNumber.Q4));
            actualUnits3.Should().Equal(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q3), new FiscalQuarter(2016, QuarterNumber.Q4), new FiscalQuarter(2017, QuarterNumber.Q1), new FiscalQuarter(2017, QuarterNumber.Q2), new FiscalQuarter(2017, QuarterNumber.Q3));
        }

        [Fact]
        public static void GetUnitsWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_GenericQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q2));
            var reportingPeriod2 = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q4));
            var reportingPeriod3 = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2017, QuarterNumber.Q3));

            // Act
            var actualUnits1 = reportingPeriod1.GetUnitsWithin<UnitOfTime>();
            var actualUnits2 = reportingPeriod2.GetUnitsWithin<UnitOfTime>();
            var actualUnits3 = reportingPeriod3.GetUnitsWithin<UnitOfTime>();

            // Assert
            actualUnits1.Should().Equal(new GenericQuarter(2016, QuarterNumber.Q2));
            actualUnits2.Should().Equal(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q3), new GenericQuarter(2016, QuarterNumber.Q4));
            actualUnits3.Should().Equal(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q3), new GenericQuarter(2016, QuarterNumber.Q4), new GenericQuarter(2017, QuarterNumber.Q1), new GenericQuarter(2017, QuarterNumber.Q2), new GenericQuarter(2017, QuarterNumber.Q3));
        }

        [Fact]
        public static void GetUnitsWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_CalendarYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2016));
            var reportingPeriod2 = new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2017));
            var reportingPeriod3 = new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2018));

            // Act
            var actualUnits1 = reportingPeriod1.GetUnitsWithin<UnitOfTime>();
            var actualUnits2 = reportingPeriod2.GetUnitsWithin<UnitOfTime>();
            var actualUnits3 = reportingPeriod3.GetUnitsWithin<UnitOfTime>();

            // Assert
            actualUnits1.Should().Equal(new CalendarYear(2016));
            actualUnits2.Should().Equal(new CalendarYear(2016), new CalendarYear(2017));
            actualUnits3.Should().Equal(new CalendarYear(2016), new CalendarYear(2017), new CalendarYear(2018));
        }

        [Fact]
        public static void GetUnitsWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_FiscalYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2016));
            var reportingPeriod2 = new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2017));
            var reportingPeriod3 = new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2018));

            // Act
            var actualUnits1 = reportingPeriod1.GetUnitsWithin<UnitOfTime>();
            var actualUnits2 = reportingPeriod2.GetUnitsWithin<UnitOfTime>();
            var actualUnits3 = reportingPeriod3.GetUnitsWithin<UnitOfTime>();

            // Assert
            actualUnits1.Should().Equal(new FiscalYear(2016));
            actualUnits2.Should().Equal(new FiscalYear(2016), new FiscalYear(2017));
            actualUnits3.Should().Equal(new FiscalYear(2016), new FiscalYear(2017), new FiscalYear(2018));
        }

        [Fact]
        public static void GetUnitsWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_GenericYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2016));
            var reportingPeriod2 = new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2017));
            var reportingPeriod3 = new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2018));

            // Act
            var actualUnits1 = reportingPeriod1.GetUnitsWithin<UnitOfTime>();
            var actualUnits2 = reportingPeriod2.GetUnitsWithin<UnitOfTime>();
            var actualUnits3 = reportingPeriod3.GetUnitsWithin<UnitOfTime>();

            // Assert
            actualUnits1.Should().Equal(new GenericYear(2016));
            actualUnits2.Should().Equal(new GenericYear(2016), new GenericYear(2017));
            actualUnits3.Should().Equal(new GenericYear(2016), new GenericYear(2017), new GenericYear(2018));
        }

        [Fact]
        public static void CreatePermutations___Should_throw_ArgumentNullException___When_parameter_reportingPeriod_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.CreatePermutations<UnitOfTime>(null, A.Dummy<PositiveInteger>()));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void CreatePermutations___Should_throw_ArgumentOutOfRangeException___When_parameter_maxUnitsInAnyReportingPeriod_is_0_or_less()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod<UnitOfTime>>();

            // Act
            var ex1 = Record.Exception(() => reportingPeriod.CreatePermutations(0));
            var ex2 = Record.Exception(() => reportingPeriod.CreatePermutations(A.Dummy<NegativeInteger>()));

            // Assert
            ex1.Should().BeOfType<ArgumentOutOfRangeException>();
            ex2.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void CreatePermutations___Should_return_permutations___When_called_for_reporting_period_of_CalendarDay()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight));
            var reportingPeriod2 = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Three));

            // Act
            var permutations1a = reportingPeriod1.CreatePermutations(1);
            var permutations1b = reportingPeriod1.CreatePermutations(5);

            var permutations2a = reportingPeriod2.CreatePermutations(1);
            var permutations2b = reportingPeriod2.CreatePermutations(3);

            // Assert
            permutations1a.Should().Equal(reportingPeriod1);
            permutations1b.Should().Equal(reportingPeriod1);

            permutations2a.Should().Equal(
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight)),
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine)),
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One)),
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Two), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Two)),
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Three), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Three)));

            permutations2b.Should().Equal(
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight)),
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine)),
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One)),
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine)),
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One)),
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Two)),
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One)),
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Two)),
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Three)),
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Two), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Two)),
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Two), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Three)),
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Three), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Three)));
        }

        [Fact]
        public static void CreatePermutations___Should_return_permutations___When_called_for_reporting_period_of_CalendarMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.February));
            var reportingPeriod2 = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.June));

            // Act
            var permutations1a = reportingPeriod1.CreatePermutations(1);
            var permutations1b = reportingPeriod1.CreatePermutations(5);

            var permutations2a = reportingPeriod2.CreatePermutations(1);
            var permutations2b = reportingPeriod2.CreatePermutations(3);

            // Assert
            permutations1a.Should().Equal(reportingPeriod1);
            permutations1b.Should().Equal(reportingPeriod1);

            permutations2a.Should().Equal(
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.February)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.March), new CalendarMonth(2016, MonthOfYear.March)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.April), new CalendarMonth(2016, MonthOfYear.April)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.May), new CalendarMonth(2016, MonthOfYear.May)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.June), new CalendarMonth(2016, MonthOfYear.June)));

            permutations2b.Should().Equal(
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.February)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.March)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.April)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.March), new CalendarMonth(2016, MonthOfYear.March)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.March), new CalendarMonth(2016, MonthOfYear.April)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.March), new CalendarMonth(2016, MonthOfYear.May)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.April), new CalendarMonth(2016, MonthOfYear.April)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.April), new CalendarMonth(2016, MonthOfYear.May)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.April), new CalendarMonth(2016, MonthOfYear.June)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.May), new CalendarMonth(2016, MonthOfYear.May)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.May), new CalendarMonth(2016, MonthOfYear.June)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.June), new CalendarMonth(2016, MonthOfYear.June)));
        }

        [Fact]
        public static void CreatePermutations___Should_return_permutations___When_called_for_reporting_period_of_FiscalMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Two));
            var reportingPeriod2 = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Six));

            // Act
            var permutations1a = reportingPeriod1.CreatePermutations(1);
            var permutations1b = reportingPeriod1.CreatePermutations(5);

            var permutations2a = reportingPeriod2.CreatePermutations(1);
            var permutations2b = reportingPeriod2.CreatePermutations(3);

            // Assert
            permutations1a.Should().Equal(reportingPeriod1);
            permutations1b.Should().Equal(reportingPeriod1);

            permutations2a.Should().Equal(
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Two)),
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Three), new FiscalMonth(2016, MonthNumber.Three)),
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2016, MonthNumber.Four)),
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Five), new FiscalMonth(2016, MonthNumber.Five)),
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Six), new FiscalMonth(2016, MonthNumber.Six)));

            permutations2b.Should().Equal(
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Two)),
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Three)),
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Four)),
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Three), new FiscalMonth(2016, MonthNumber.Three)),
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Three), new FiscalMonth(2016, MonthNumber.Four)),
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Three), new FiscalMonth(2016, MonthNumber.Five)),
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2016, MonthNumber.Four)),
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2016, MonthNumber.Five)),
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2016, MonthNumber.Six)),
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Five), new FiscalMonth(2016, MonthNumber.Five)),
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Five), new FiscalMonth(2016, MonthNumber.Six)),
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Six), new FiscalMonth(2016, MonthNumber.Six)));
        }

        [Fact]
        public static void CreatePermutations___Should_return_permutations___When_called_for_reporting_period_of_GenericMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Two));
            var reportingPeriod2 = new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Six));

            // Act
            var permutations1a = reportingPeriod1.CreatePermutations(1);
            var permutations1b = reportingPeriod1.CreatePermutations(5);

            var permutations2a = reportingPeriod2.CreatePermutations(1);
            var permutations2b = reportingPeriod2.CreatePermutations(3);

            // Assert
            permutations1a.Should().Equal(reportingPeriod1);
            permutations1b.Should().Equal(reportingPeriod1);

            permutations2a.Should().Equal(
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Two)),
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Three), new GenericMonth(2016, MonthNumber.Three)),
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Four), new GenericMonth(2016, MonthNumber.Four)),
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Five), new GenericMonth(2016, MonthNumber.Five)),
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Six), new GenericMonth(2016, MonthNumber.Six)));

            permutations2b.Should().Equal(
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Two)),
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Three)),
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Four)),
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Three), new GenericMonth(2016, MonthNumber.Three)),
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Three), new GenericMonth(2016, MonthNumber.Four)),
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Three), new GenericMonth(2016, MonthNumber.Five)),
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Four), new GenericMonth(2016, MonthNumber.Four)),
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Four), new GenericMonth(2016, MonthNumber.Five)),
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Four), new GenericMonth(2016, MonthNumber.Six)),
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Five), new GenericMonth(2016, MonthNumber.Five)),
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Five), new GenericMonth(2016, MonthNumber.Six)),
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Six), new GenericMonth(2016, MonthNumber.Six)));
        }

        [Fact]
        public static void CreatePermutations___Should_return_permutations___When_called_for_reporting_period_of_CalendarQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q2));
            var reportingPeriod2 = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2017, QuarterNumber.Q2));

            // Act
            var permutations1a = reportingPeriod1.CreatePermutations(1);
            var permutations1b = reportingPeriod1.CreatePermutations(5);

            var permutations2a = reportingPeriod2.CreatePermutations(1);
            var permutations2b = reportingPeriod2.CreatePermutations(3);

            // Assert
            permutations1a.Should().Equal(reportingPeriod1);
            permutations1b.Should().Equal(reportingPeriod1);

            permutations2a.Should().Equal(
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q2)),
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q3), new CalendarQuarter(2016, QuarterNumber.Q3)),
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q4), new CalendarQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2017, QuarterNumber.Q1), new CalendarQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2017, QuarterNumber.Q2), new CalendarQuarter(2017, QuarterNumber.Q2)));

            permutations2b.Should().Equal(
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q2)),
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q3)),
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q3), new CalendarQuarter(2016, QuarterNumber.Q3)),
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q3), new CalendarQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q3), new CalendarQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q4), new CalendarQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q4), new CalendarQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q4), new CalendarQuarter(2017, QuarterNumber.Q2)),
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2017, QuarterNumber.Q1), new CalendarQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2017, QuarterNumber.Q1), new CalendarQuarter(2017, QuarterNumber.Q2)),
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2017, QuarterNumber.Q2), new CalendarQuarter(2017, QuarterNumber.Q2)));
        }

        [Fact]
        public static void CreatePermutations___Should_return_permutations___When_called_for_reporting_period_of_FiscalQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q2));
            var reportingPeriod2 = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2017, QuarterNumber.Q2));

            // Act
            var permutations1a = reportingPeriod1.CreatePermutations(1);
            var permutations1b = reportingPeriod1.CreatePermutations(5);

            var permutations2a = reportingPeriod2.CreatePermutations(1);
            var permutations2b = reportingPeriod2.CreatePermutations(3);

            // Assert
            permutations1a.Should().Equal(reportingPeriod1);
            permutations1b.Should().Equal(reportingPeriod1);

            permutations2a.Should().Equal(
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q2)),
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q3), new FiscalQuarter(2016, QuarterNumber.Q3)),
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q4), new FiscalQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2017, QuarterNumber.Q1), new FiscalQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2017, QuarterNumber.Q2), new FiscalQuarter(2017, QuarterNumber.Q2)));

            permutations2b.Should().Equal(
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q2)),
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q3)),
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q3), new FiscalQuarter(2016, QuarterNumber.Q3)),
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q3), new FiscalQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q3), new FiscalQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q4), new FiscalQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q4), new FiscalQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q4), new FiscalQuarter(2017, QuarterNumber.Q2)),
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2017, QuarterNumber.Q1), new FiscalQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2017, QuarterNumber.Q1), new FiscalQuarter(2017, QuarterNumber.Q2)),
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2017, QuarterNumber.Q2), new FiscalQuarter(2017, QuarterNumber.Q2)));
        }

        [Fact]
        public static void CreatePermutations___Should_return_permutations___When_called_for_reporting_period_of_GenericQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q2));
            var reportingPeriod2 = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2017, QuarterNumber.Q2));

            // Act
            var permutations1a = reportingPeriod1.CreatePermutations(1);
            var permutations1b = reportingPeriod1.CreatePermutations(5);

            var permutations2a = reportingPeriod2.CreatePermutations(1);
            var permutations2b = reportingPeriod2.CreatePermutations(3);

            // Assert
            permutations1a.Should().Equal(reportingPeriod1);
            permutations1b.Should().Equal(reportingPeriod1);

            permutations2a.Should().Equal(
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q2)),
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q3), new GenericQuarter(2016, QuarterNumber.Q3)),
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q4), new GenericQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2017, QuarterNumber.Q1), new GenericQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2017, QuarterNumber.Q2), new GenericQuarter(2017, QuarterNumber.Q2)));

            permutations2b.Should().Equal(
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q2)),
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q3)),
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q3), new GenericQuarter(2016, QuarterNumber.Q3)),
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q3), new GenericQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q3), new GenericQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q4), new GenericQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q4), new GenericQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q4), new GenericQuarter(2017, QuarterNumber.Q2)),
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2017, QuarterNumber.Q1), new GenericQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2017, QuarterNumber.Q1), new GenericQuarter(2017, QuarterNumber.Q2)),
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2017, QuarterNumber.Q2), new GenericQuarter(2017, QuarterNumber.Q2)));
        }

        [Fact]
        public static void CreatePermutations___Should_return_permutations___When_called_for_reporting_period_of_CalendarYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2016));
            var reportingPeriod2 = new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2019));

            // Act
            var permutations1a = reportingPeriod1.CreatePermutations(1);
            var permutations1b = reportingPeriod1.CreatePermutations(5);

            var permutations2a = reportingPeriod2.CreatePermutations(1);
            var permutations2b = reportingPeriod2.CreatePermutations(3);

            // Assert
            permutations1a.Should().Equal(reportingPeriod1);
            permutations1b.Should().Equal(reportingPeriod1);

            permutations2a.Should().Equal(
                new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2016)),
                new ReportingPeriod<CalendarYear>(new CalendarYear(2017), new CalendarYear(2017)),
                new ReportingPeriod<CalendarYear>(new CalendarYear(2018), new CalendarYear(2018)),
                new ReportingPeriod<CalendarYear>(new CalendarYear(2019), new CalendarYear(2019)));

            permutations2b.Should().Equal(
                new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2016)),
                new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2017)),
                new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2018)),
                new ReportingPeriod<CalendarYear>(new CalendarYear(2017), new CalendarYear(2017)),
                new ReportingPeriod<CalendarYear>(new CalendarYear(2017), new CalendarYear(2018)),
                new ReportingPeriod<CalendarYear>(new CalendarYear(2017), new CalendarYear(2019)),
                new ReportingPeriod<CalendarYear>(new CalendarYear(2018), new CalendarYear(2018)),
                new ReportingPeriod<CalendarYear>(new CalendarYear(2018), new CalendarYear(2019)),
                new ReportingPeriod<CalendarYear>(new CalendarYear(2019), new CalendarYear(2019)));
        }

        [Fact]
        public static void CreatePermutations___Should_return_permutations___When_called_for_reporting_period_of_FiscalYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2016));
            var reportingPeriod2 = new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2019));

            // Act
            var permutations1a = reportingPeriod1.CreatePermutations(1);
            var permutations1b = reportingPeriod1.CreatePermutations(5);

            var permutations2a = reportingPeriod2.CreatePermutations(1);
            var permutations2b = reportingPeriod2.CreatePermutations(3);

            // Assert
            permutations1a.Should().Equal(reportingPeriod1);
            permutations1b.Should().Equal(reportingPeriod1);

            permutations2a.Should().Equal(
                new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2016)),
                new ReportingPeriod<FiscalYear>(new FiscalYear(2017), new FiscalYear(2017)),
                new ReportingPeriod<FiscalYear>(new FiscalYear(2018), new FiscalYear(2018)),
                new ReportingPeriod<FiscalYear>(new FiscalYear(2019), new FiscalYear(2019)));

            permutations2b.Should().Equal(
                new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2016)),
                new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2017)),
                new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2018)),
                new ReportingPeriod<FiscalYear>(new FiscalYear(2017), new FiscalYear(2017)),
                new ReportingPeriod<FiscalYear>(new FiscalYear(2017), new FiscalYear(2018)),
                new ReportingPeriod<FiscalYear>(new FiscalYear(2017), new FiscalYear(2019)),
                new ReportingPeriod<FiscalYear>(new FiscalYear(2018), new FiscalYear(2018)),
                new ReportingPeriod<FiscalYear>(new FiscalYear(2018), new FiscalYear(2019)),
                new ReportingPeriod<FiscalYear>(new FiscalYear(2019), new FiscalYear(2019)));
        }

        [Fact]
        public static void CreatePermutations___Should_return_permutations___When_called_for_reporting_period_of_GenericYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2016));
            var reportingPeriod2 = new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2019));

            // Act
            var permutations1a = reportingPeriod1.CreatePermutations(1);
            var permutations1b = reportingPeriod1.CreatePermutations(5);

            var permutations2a = reportingPeriod2.CreatePermutations(1);
            var permutations2b = reportingPeriod2.CreatePermutations(3);

            // Assert
            permutations1a.Should().Equal(reportingPeriod1);
            permutations1b.Should().Equal(reportingPeriod1);

            permutations2a.Should().Equal(
                new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2016)),
                new ReportingPeriod<GenericYear>(new GenericYear(2017), new GenericYear(2017)),
                new ReportingPeriod<GenericYear>(new GenericYear(2018), new GenericYear(2018)),
                new ReportingPeriod<GenericYear>(new GenericYear(2019), new GenericYear(2019)));

            permutations2b.Should().Equal(
                new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2016)),
                new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2017)),
                new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2018)),
                new ReportingPeriod<GenericYear>(new GenericYear(2017), new GenericYear(2017)),
                new ReportingPeriod<GenericYear>(new GenericYear(2017), new GenericYear(2018)),
                new ReportingPeriod<GenericYear>(new GenericYear(2017), new GenericYear(2019)),
                new ReportingPeriod<GenericYear>(new GenericYear(2018), new GenericYear(2018)),
                new ReportingPeriod<GenericYear>(new GenericYear(2018), new GenericYear(2019)),
                new ReportingPeriod<GenericYear>(new GenericYear(2019), new GenericYear(2019)));
        }

        [Fact]
        public static void SerializeToString__Should_throw_ArgumentNullException___When_parameter_reportingPeriod_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.SerializeToString(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void SerializeToString__Should_return_expected_serialized_string_representation_of_reportingPeriod___When_reportingPeriod_is_a_IReportingPeriod()
        {
            // Arrange
            var reportingPeriods = new Dictionary<string, IReportingPeriod<UnitOfTime>>
            {
                { "cd-2017-05-17,cd-2018-12-09", new ReportingPeriod<CalendarDay>(new CalendarDay(2017, MonthOfYear.May, DayOfMonth.Seventeen), new CalendarDay(2018, MonthOfYear.December, DayOfMonth.Nine)) },
                { "cm-2017-05,cm-2018-12", new ReportingPeriod<CalendarMonth>(new CalendarMonth(2017, MonthOfYear.May), new CalendarMonth(2018, MonthOfYear.December)) },
                { "fm-2017-05,fm-2018-12", new ReportingPeriod<FiscalMonth>(new FiscalMonth(2017, MonthNumber.Five), new FiscalMonth(2018, MonthNumber.Twelve)) },
                { "gm-2017-05,gm-2018-12", new ReportingPeriod<GenericMonth>(new GenericMonth(2017, MonthNumber.Five), new GenericMonth(2018, MonthNumber.Twelve)) },
                { "cq-2017-2,cq-2018-4", new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2017, QuarterNumber.Q2), new CalendarQuarter(2018, QuarterNumber.Q4)) },
                { "fq-2017-2,fq-2018-4", new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2017, QuarterNumber.Q2), new FiscalQuarter(2018, QuarterNumber.Q4)) },
                { "gq-2017-2,gq-2018-4", new ReportingPeriod<GenericQuarter>(new GenericQuarter(2017, QuarterNumber.Q2), new GenericQuarter(2018, QuarterNumber.Q4)) },
                { "cy-2017,cy-2018", new ReportingPeriod<CalendarYear>(new CalendarYear(2017), new CalendarYear(2018)) },
                { "fy-2017,fy-2018", new ReportingPeriod<FiscalYear>(new FiscalYear(2017), new FiscalYear(2018)) },
                { "gy-2017,gy-2018", new ReportingPeriod<GenericYear>(new GenericYear(2017), new GenericYear(2018)) }
            };

            // Act
            var serialized = reportingPeriods.Select(_ => new { Actual = _.Value.SerializeToString(), Expected = _.Key }).ToList();

            // Assert
            serialized.ForEach(_ => _.Actual.Should().Be(_.Expected));
        }

        [Fact]
        public static void DeserializeFromString___Should_throw_ArgumentNullException___When_parameter_unitOfTime_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.DeserializeFromString<IReportingPeriod<UnitOfTime>>(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void DeserializeFromString___Should_throw_ArgumentException___When_parameter_unitOfTime_is_whitespace()
        {
            // Arrange
            var reportingPeriods = new[] { string.Empty, "  ", "  \r\n " };

            // Act
            var exceptions = reportingPeriods.Select(_ => Record.Exception(() => _.DeserializeFromString<IReportingPeriod<UnitOfTime>>())).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<ArgumentException>());
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Thoroughly checking this test-case requires lots of types.")]
        public static void DeserializeFromString___Should_throw_InvalidOperationException___When_the_return_type_cannot_be_assigned_to_a_ReportingPeriod()
        {
            // Arrange
            var allTypes = new[]
            {
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<UnitOfTime>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<UnitOfTime>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<CalendarUnitOfTime>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<CalendarUnitOfTime>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<FiscalUnitOfTime>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<FiscalUnitOfTime>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<GenericUnitOfTime>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<GenericUnitOfTime>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<CalendarDay>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<CalendarDay>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<CalendarMonth>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<CalendarMonth>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<CalendarQuarter>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<CalendarQuarter>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<CalendarYear>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<CalendarYear>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<FiscalMonth>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<FiscalMonth>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<FiscalQuarter>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<FiscalQuarter>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<FiscalYear>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<FiscalYear>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<GenericMonth>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<GenericMonth>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<GenericQuarter>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<GenericQuarter>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<GenericYear>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<GenericYear>) }
            };

            var reportingPeriods = new[]
            {
                "cd-2015-11-11,cd-2016-11-11",
                "cm-2017-03,cm-2017-04",
                "fm-2017-03,fm-2017-04",
                "gm-2017-03,gm-2017-04",
                "cq-2017-3,cq-2017-4",
                "fq-2017-3,fq-2017-4",
                "gq-2017-3,gq-2017-4",
                "cy-2017,cy-2018",
                "fy-2017,fy-2018",
                "gy-2017,gy-2018"
            };

            var deserializeFromString = typeof(ReportingPeriodExtensions).GetMethod(nameof(ReportingPeriodExtensions.DeserializeFromString));

            // Act
            var exceptions = new List<Exception>();
            foreach (var reportingPeriod in reportingPeriods)
            {
                foreach (var type in allTypes)
                {
                    var genericMethod = deserializeFromString.MakeGenericMethod(type.ReportingPeriodType);
                    // ReSharper disable PossibleNullReferenceException
                    exceptions.Add(Record.Exception(() => genericMethod.Invoke(null, new object[] { reportingPeriod })).InnerException);
                    // ReSharper restore PossibleNullReferenceException
                }
            }

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromString___Should_throw_InvalidOperationException___When_the_kind_of_unit_of_times_encoded_cannot_be_assigned_to_the_return_types_unit_of_time()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new { ReportingPeriod = "cd-2015-11-11,cd-2016-11-11", ReportingPeriodType = typeof(ReportingPeriod<FiscalMonth>) },
                new { ReportingPeriod = "cm-2017-03,cm-2017-04", ReportingPeriodType = typeof(ReportingPeriod<FiscalMonth>) },
                new { ReportingPeriod = "fm-2017-03,fm-2017-04", ReportingPeriodType = typeof(ReportingPeriod<CalendarUnitOfTime>) },
                new { ReportingPeriod = "gm-2017-03,gm-2017-04", ReportingPeriodType = typeof(ReportingPeriod<GenericYear>) },
                new { ReportingPeriod = "cq-2017-3,cq-2017-4", ReportingPeriodType = typeof(ReportingPeriod<FiscalUnitOfTime>) },
                new { ReportingPeriod = "fq-2017-3,fq-2017-4", ReportingPeriodType = typeof(ReportingPeriod<GenericQuarter>) },
                new { ReportingPeriod = "gq-2017-3,gq-2017-4", ReportingPeriodType = typeof(ReportingPeriod<CalendarDay>) },
                new { ReportingPeriod = "cy-2017,cy-2018", ReportingPeriodType = typeof(ReportingPeriod<GenericUnitOfTime>) },
                new { ReportingPeriod = "fy-2017,fy-2018", ReportingPeriodType = typeof(ReportingPeriod<FiscalMonth>) },
                new { ReportingPeriod = "gy-2017,gy-2018", ReportingPeriodType = typeof(ReportingPeriod<GenericQuarter>) },
                new { ReportingPeriod = "cd-2015-11-11,fm-2017-04", ReportingPeriodType = typeof(ReportingPeriod<FiscalMonth>) },
                new { ReportingPeriod = "fm-2017-04,fy-2018", ReportingPeriodType = typeof(ReportingPeriod<FiscalMonth>) },
                new { ReportingPeriod = "cq-2017-3,fm-2017-04", ReportingPeriodType = typeof(ReportingPeriod<CalendarUnitOfTime>) },
                new { ReportingPeriod = "gm-2017-03,gy-2018", ReportingPeriodType = typeof(ReportingPeriod<GenericYear>) },
                new { ReportingPeriod = "fq-2017-3,cq-2017-4", ReportingPeriodType = typeof(ReportingPeriod<FiscalUnitOfTime>) },
                new { ReportingPeriod = "fq-2017-3,gq-2017-4", ReportingPeriodType = typeof(ReportingPeriod<GenericQuarter>) },
                new { ReportingPeriod = "cd-2015-11-11,gq-2017-4", ReportingPeriodType = typeof(ReportingPeriod<CalendarDay>) },
                new { ReportingPeriod = "gy-2018,cy-2018", ReportingPeriodType = typeof(ReportingPeriod<GenericUnitOfTime>) },
                new { ReportingPeriod = "fm-2017-04,fy-2018", ReportingPeriodType = typeof(ReportingPeriod<FiscalMonth>) },
                new { ReportingPeriod = "gy-2017,gq-2017-4", ReportingPeriodType = typeof(ReportingPeriod<GenericQuarter>) },
            };

            var deserializeFromString = typeof(ReportingPeriodExtensions).GetMethod(nameof(ReportingPeriodExtensions.DeserializeFromString));

            // Act
            var exceptions = new List<Exception>();
            foreach (var reportingPeriod in reportingPeriods)
            {
                var genericMethod = deserializeFromString.MakeGenericMethod(reportingPeriod.ReportingPeriodType);
                // ReSharper disable PossibleNullReferenceException
                exceptions.Add(Record.Exception(() => genericMethod.Invoke(null, new object[] { reportingPeriod.ReportingPeriod })).InnerException);
                // ReSharper restore PossibleNullReferenceException
            }

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromString___Should_throw_InvalidOperationException___When_reportingPeriod_has_the_wrong_number_of_tokens()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                ",",
                "cm-2017-04",
                ",cm-2017-04",
                "cm-2017-04,",
                "cm-2017-03,,",
                ",cm-2017-03,",
                "cm-2017-03,cm-2017-04,",
                "cm-2017-03,cm-2017-04,cm-2017-04"
            };

            // Act
            var exceptions = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromString<IReportingPeriod<UnitOfTime>>())).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromString___Should_throw_InvalidOperationException___When_token_representing_start_of_reporting_period_is_malformed()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                "cm-201a-11,cm-2017-10",
                "cm-xxxx-11,cm-2017-10",
                "cm-10000-11,cm-2017-10",
                "cm-T001-11,cm-2017-10",
                "cm-0-11,cm-2017-10",
                "cm-200-11,cm-2017-10",
                "cm-0000-11,cm-2017-10",
                "cm-999-11,cm-2017-10",
                "cm-2007-1,cm-2017-10",
                "cm-2007-9,cm-2017-10",
                "cm-2007-13,cm-2017-10",
                "cm-2007-99,cm-2017-10",
                "cm-2007-00,cm-2017-10",
                "cm-2007-001,cm-2017-10",
                "cm-2007-012,cm-2017-10"
            };

            // Act
            var exceptions = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromString<IReportingPeriod<UnitOfTime>>())).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromString___Should_throw_InvalidOperationException___When_token_representing_end_of_reporting_period_is_malformed()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                "cm-2017-04,cm-201a-11",
                "cm-2017-04,cm-xxxx-11",
                "cm-2017-04,cm-10000-11",
                "cm-2017-04,cm-T001-11",
                "cm-2017-04,cm-0-11",
                "cm-2017-04,cm-200-11",
                "cm-2017-04,cm-0000-11",
                "cm-2017-04,cm-999-11",
                "cm-2017-04,cm-2007-1",
                "cm-2017-04,cm-2007-9",
                "cm-2017-04,cm-2007-13",
                "cm-2017-04,cm-2007-99",
                "cm-2017-04,cm-2007-00",
                "cm-2017-04,cm-2007-001",
                "cm-2017-04,cm-2007-012"
            };

            // Act
            var exceptions = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromString<IReportingPeriod<UnitOfTime>>())).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromString___Should_throw_InvalidOperationException___When_token_representing_start_and_end_of_reporting_period_do_not_deserialize_into_same_concrete_type()
        {
            // Arrange
            var unitsOfTime1 = new[]
            {
                "cm-2017-04,cd-2017-04-11",
                "fq-2017-4,gq-2018-1",
                "cy-2017,fm-2018-05"
            };

            var unitsOfTime2 = new[]
            {
                "cm-2017-04,cd-2017-04-11",
                "cy-2017,cm-2018-05"
            };

            // Act
            var exceptions1 = unitsOfTime1.Select(_ => Record.Exception(() => _.DeserializeFromString<IReportingPeriod<UnitOfTime>>())).ToList();
            var exceptions2 = unitsOfTime2.Select(_ => Record.Exception(() => _.DeserializeFromString<IReportingPeriod<CalendarUnitOfTime>>())).ToList();

            // Assert
            exceptions1.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
            exceptions2.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromString___Should_throw_InvalidOperationException___When_token_representing_start_of_reporting_period_is_greater_than_token_representing_end()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                "cm-2017-04,cm-2016-04",
                "cq-2017-3,cq-2017-2",
                "cd-2017-03-04,cd-2017-03-01",
                "fy-2017,fy-2016"
            };

            // Act
            var exceptions = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromString<IReportingPeriod<UnitOfTime>>())).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_CalendarDay_string()
        {
            // Arrange
            var reportingPeriod = "cd-2001-01-10,cd-2016-02-29";
            var expected = new ReportingPeriod<CalendarDay>(new CalendarDay(2001, MonthOfYear.January, DayOfMonth.Ten), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine));

            // Act
            var deserialized1 = reportingPeriod.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized2 = reportingPeriod.DeserializeFromString<ReportingPeriod<UnitOfTime>>();

            var deserialized3 = reportingPeriod.DeserializeFromString<IReportingPeriod<CalendarUnitOfTime>>();
            var deserialized4 = reportingPeriod.DeserializeFromString<ReportingPeriod<CalendarUnitOfTime>>();

            var deserialized5 = reportingPeriod.DeserializeFromString<IReportingPeriod<CalendarDay>>();
            var deserialized6 = reportingPeriod.DeserializeFromString<ReportingPeriod<CalendarDay>>();

            // Assert
            deserialized1.Should().Be(expected);
            deserialized2.Should().Be(expected);
            deserialized3.Should().Be(expected);
            deserialized4.Should().Be(expected);
            deserialized5.Should().Be(expected);
            deserialized6.Should().Be(expected);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_CalendarMonth_string()
        {
            // Arrange
            var reportingPeriod = "cm-2001-01,cm-2001-02";
            var expected = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2001, MonthOfYear.January), new CalendarMonth(2001, MonthOfYear.February));

            // Act
            var deserialized1 = reportingPeriod.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized2 = reportingPeriod.DeserializeFromString<ReportingPeriod<UnitOfTime>>();

            var deserialized3 = reportingPeriod.DeserializeFromString<IReportingPeriod<CalendarUnitOfTime>>();
            var deserialized4 = reportingPeriod.DeserializeFromString<ReportingPeriod<CalendarUnitOfTime>>();

            var deserialized5 = reportingPeriod.DeserializeFromString<IReportingPeriod<CalendarMonth>>();
            var deserialized6 = reportingPeriod.DeserializeFromString<ReportingPeriod<CalendarMonth>>();

            // Assert
            deserialized1.Should().Be(expected);
            deserialized2.Should().Be(expected);
            deserialized3.Should().Be(expected);
            deserialized4.Should().Be(expected);
            deserialized5.Should().Be(expected);
            deserialized6.Should().Be(expected);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_FiscalMonth_string()
        {
            // Arrange
            var reportingPeriod = "fm-2001-01,fm-2001-02";
            var expected = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2001, MonthNumber.One), new FiscalMonth(2001, MonthNumber.Two));

            // Act
            var deserialized1 = reportingPeriod.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized2 = reportingPeriod.DeserializeFromString<ReportingPeriod<UnitOfTime>>();

            var deserialized3 = reportingPeriod.DeserializeFromString<IReportingPeriod<FiscalUnitOfTime>>();
            var deserialized4 = reportingPeriod.DeserializeFromString<ReportingPeriod<FiscalUnitOfTime>>();

            var deserialized5 = reportingPeriod.DeserializeFromString<IReportingPeriod<FiscalMonth>>();
            var deserialized6 = reportingPeriod.DeserializeFromString<ReportingPeriod<FiscalMonth>>();

            // Assert
            deserialized1.Should().Be(expected);
            deserialized2.Should().Be(expected);
            deserialized3.Should().Be(expected);
            deserialized4.Should().Be(expected);
            deserialized5.Should().Be(expected);
            deserialized6.Should().Be(expected);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_GenericMonth_string()
        {
            // Arrange
            var reportingPeriod = "gm-2001-01,gm-2001-02";
            var expected = new ReportingPeriod<GenericMonth>(new GenericMonth(2001, MonthNumber.One), new GenericMonth(2001, MonthNumber.Two));

            // Act
            var deserialized1 = reportingPeriod.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized2 = reportingPeriod.DeserializeFromString<ReportingPeriod<UnitOfTime>>();

            var deserialized3 = reportingPeriod.DeserializeFromString<IReportingPeriod<GenericUnitOfTime>>();
            var deserialized4 = reportingPeriod.DeserializeFromString<ReportingPeriod<GenericUnitOfTime>>();

            var deserialized5 = reportingPeriod.DeserializeFromString<IReportingPeriod<GenericMonth>>();
            var deserialized6 = reportingPeriod.DeserializeFromString<ReportingPeriod<GenericMonth>>();

            // Assert
            deserialized1.Should().Be(expected);
            deserialized2.Should().Be(expected);
            deserialized3.Should().Be(expected);
            deserialized4.Should().Be(expected);
            deserialized5.Should().Be(expected);
            deserialized6.Should().Be(expected);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_CalendarQuarter_string()
        {
            // Arrange
            var reportingPeriod = "cq-2001-1,cq-2001-2";
            var expected = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2001, QuarterNumber.Q1), new CalendarQuarter(2001, QuarterNumber.Q2));

            // Act
            var deserialized1 = reportingPeriod.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized2 = reportingPeriod.DeserializeFromString<ReportingPeriod<UnitOfTime>>();

            var deserialized3 = reportingPeriod.DeserializeFromString<IReportingPeriod<CalendarUnitOfTime>>();
            var deserialized4 = reportingPeriod.DeserializeFromString<ReportingPeriod<CalendarUnitOfTime>>();

            var deserialized5 = reportingPeriod.DeserializeFromString<IReportingPeriod<CalendarQuarter>>();
            var deserialized6 = reportingPeriod.DeserializeFromString<ReportingPeriod<CalendarQuarter>>();

            // Assert
            deserialized1.Should().Be(expected);
            deserialized2.Should().Be(expected);
            deserialized3.Should().Be(expected);
            deserialized4.Should().Be(expected);
            deserialized5.Should().Be(expected);
            deserialized6.Should().Be(expected);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_FiscalQuarter_string()
        {
            // Arrange
            var reportingPeriod = "fq-2001-1,fq-2001-2";
            var expected = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2001, QuarterNumber.Q1), new FiscalQuarter(2001, QuarterNumber.Q2));

            // Act
            var deserialized1 = reportingPeriod.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized2 = reportingPeriod.DeserializeFromString<ReportingPeriod<UnitOfTime>>();

            var deserialized3 = reportingPeriod.DeserializeFromString<IReportingPeriod<FiscalUnitOfTime>>();
            var deserialized4 = reportingPeriod.DeserializeFromString<ReportingPeriod<FiscalUnitOfTime>>();

            var deserialized5 = reportingPeriod.DeserializeFromString<IReportingPeriod<FiscalQuarter>>();
            var deserialized6 = reportingPeriod.DeserializeFromString<ReportingPeriod<FiscalQuarter>>();

            // Assert
            deserialized1.Should().Be(expected);
            deserialized2.Should().Be(expected);
            deserialized3.Should().Be(expected);
            deserialized4.Should().Be(expected);
            deserialized5.Should().Be(expected);
            deserialized6.Should().Be(expected);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_GenericQuarter_string()
        {
            // Arrange
            var reportingPeriod = "gq-2001-1,gq-2001-2";
            var expected = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2001, QuarterNumber.Q1), new GenericQuarter(2001, QuarterNumber.Q2));

            // Act
            var deserialized1 = reportingPeriod.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized2 = reportingPeriod.DeserializeFromString<ReportingPeriod<UnitOfTime>>();

            var deserialized3 = reportingPeriod.DeserializeFromString<IReportingPeriod<GenericUnitOfTime>>();
            var deserialized4 = reportingPeriod.DeserializeFromString<ReportingPeriod<GenericUnitOfTime>>();

            var deserialized5 = reportingPeriod.DeserializeFromString<IReportingPeriod<GenericQuarter>>();
            var deserialized6 = reportingPeriod.DeserializeFromString<ReportingPeriod<GenericQuarter>>();

            // Assert
            deserialized1.Should().Be(expected);
            deserialized2.Should().Be(expected);
            deserialized3.Should().Be(expected);
            deserialized4.Should().Be(expected);
            deserialized5.Should().Be(expected);
            deserialized6.Should().Be(expected);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_CalendarYear_string()
        {
            // Arrange
            var reportingPeriod = "cy-2001,cy-2002";
            var expected = new ReportingPeriod<CalendarYear>(new CalendarYear(2001), new CalendarYear(2002));

            // Act
            var deserialized1 = reportingPeriod.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized2 = reportingPeriod.DeserializeFromString<ReportingPeriod<UnitOfTime>>();

            var deserialized3 = reportingPeriod.DeserializeFromString<IReportingPeriod<CalendarUnitOfTime>>();
            var deserialized4 = reportingPeriod.DeserializeFromString<ReportingPeriod<CalendarUnitOfTime>>();

            var deserialized5 = reportingPeriod.DeserializeFromString<IReportingPeriod<CalendarYear>>();
            var deserialized6 = reportingPeriod.DeserializeFromString<ReportingPeriod<CalendarYear>>();

            // Assert
            deserialized1.Should().Be(expected);
            deserialized2.Should().Be(expected);
            deserialized3.Should().Be(expected);
            deserialized4.Should().Be(expected);
            deserialized5.Should().Be(expected);
            deserialized6.Should().Be(expected);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_FiscalYear_string()
        {
            // Arrange
            var reportingPeriod = "fy-2001,fy-2002";
            var expected = new ReportingPeriod<FiscalYear>(new FiscalYear(2001), new FiscalYear(2002));

            // Act
            var deserialized1 = reportingPeriod.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized2 = reportingPeriod.DeserializeFromString<ReportingPeriod<UnitOfTime>>();

            var deserialized3 = reportingPeriod.DeserializeFromString<IReportingPeriod<FiscalUnitOfTime>>();
            var deserialized4 = reportingPeriod.DeserializeFromString<ReportingPeriod<FiscalUnitOfTime>>();

            var deserialized5 = reportingPeriod.DeserializeFromString<IReportingPeriod<FiscalYear>>();
            var deserialized6 = reportingPeriod.DeserializeFromString<ReportingPeriod<FiscalYear>>();

            // Assert
            deserialized1.Should().Be(expected);
            deserialized2.Should().Be(expected);
            deserialized3.Should().Be(expected);
            deserialized4.Should().Be(expected);
            deserialized5.Should().Be(expected);
            deserialized6.Should().Be(expected);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_GenericYear_string()
        {
            // Arrange
            var reportingPeriod = "gy-2001,gy-2002";
            var expected = new ReportingPeriod<GenericYear>(new GenericYear(2001), new GenericYear(2002));

            // Act
            var deserialized1 = reportingPeriod.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized2 = reportingPeriod.DeserializeFromString<ReportingPeriod<UnitOfTime>>();

            var deserialized3 = reportingPeriod.DeserializeFromString<IReportingPeriod<GenericUnitOfTime>>();
            var deserialized4 = reportingPeriod.DeserializeFromString<ReportingPeriod<GenericUnitOfTime>>();

            var deserialized5 = reportingPeriod.DeserializeFromString<IReportingPeriod<GenericYear>>();
            var deserialized6 = reportingPeriod.DeserializeFromString<ReportingPeriod<GenericYear>>();

            // Assert
            deserialized1.Should().Be(expected);
            deserialized2.Should().Be(expected);
            deserialized3.Should().Be(expected);
            deserialized4.Should().Be(expected);
            deserialized5.Should().Be(expected);
            deserialized6.Should().Be(expected);
        }

        [Fact]
        public static void GetUnitOfTimeKind___Should_throw_ArgumentNullException___When_parameter_reportingPeriod_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.GetUnitOfTimeKind(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void GetUnitOfTimeKind___Should_return_the_kind_of_unit_of_time_used_in_the_reporting_period___When_called()
        {
            // Arrange
            var reportingPeriods = new List<Tuple<IReportingPeriod<UnitOfTime>, UnitOfTimeKind>>
            {
                { A.Dummy<IReportingPeriod<CalendarUnitOfTime>>(), UnitOfTimeKind.Calendar },
                { A.Dummy<IReportingPeriod<CalendarDay>>(), UnitOfTimeKind.Calendar },
                { A.Dummy<IReportingPeriod<CalendarMonth>>(), UnitOfTimeKind.Calendar },
                { A.Dummy<IReportingPeriod<CalendarQuarter>>(), UnitOfTimeKind.Calendar },
                { A.Dummy<IReportingPeriod<CalendarYear>>(), UnitOfTimeKind.Calendar },
                { A.Dummy<IReportingPeriod<FiscalUnitOfTime>>(), UnitOfTimeKind.Fiscal },
                { A.Dummy<IReportingPeriod<FiscalMonth>>(), UnitOfTimeKind.Fiscal },
                { A.Dummy<IReportingPeriod<FiscalQuarter>>(), UnitOfTimeKind.Fiscal },
                { A.Dummy<IReportingPeriod<FiscalYear>>(), UnitOfTimeKind.Fiscal },
                { A.Dummy<IReportingPeriod<GenericUnitOfTime>>(), UnitOfTimeKind.Generic },
                { A.Dummy<IReportingPeriod<GenericMonth>>(), UnitOfTimeKind.Generic },
                { A.Dummy<IReportingPeriod<GenericQuarter>>(), UnitOfTimeKind.Generic },
                { A.Dummy<IReportingPeriod<GenericYear>>(), UnitOfTimeKind.Generic }
            };

            // Act
            var unitOfTimeKinds = reportingPeriods.Select(_ => new { Actual = _.Item1.GetUnitOfTimeKind(), Expected = _.Item2 }).ToList();

            // Assert
            unitOfTimeKinds.ForEach(_ => _.Actual.Should().Be(_.Expected));
        }

        [Fact]
        public static void GetUnitOfTimeGranularity___Should_throw_ArgumentNullException___When_parameter_reportingPeriod_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.GetUnitOfTimeGranularity(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        public static void GetUnitOfTimeGranularity__Should_return_the_granularity_of_the_unit_of_time_used_in_the_reporting_period___When_called()
        {
            // Arrange
            var reportingPeriods = new List<Tuple<IReportingPeriod<UnitOfTime>, UnitOfTimeGranularity>>
            {
                { A.Dummy<IReportingPeriod<CalendarDay>>(), UnitOfTimeGranularity.Day },
                { A.Dummy<IReportingPeriod<CalendarMonth>>(), UnitOfTimeGranularity.Month },
                { A.Dummy<IReportingPeriod<CalendarQuarter>>(), UnitOfTimeGranularity.Quarter },
                { A.Dummy<IReportingPeriod<CalendarYear>>(), UnitOfTimeGranularity.Year },
                { A.Dummy<IReportingPeriod<FiscalMonth>>(), UnitOfTimeGranularity.Month },
                { A.Dummy<IReportingPeriod<FiscalQuarter>>(), UnitOfTimeGranularity.Quarter },
                { A.Dummy<IReportingPeriod<FiscalYear>>(), UnitOfTimeGranularity.Year },
                { A.Dummy<IReportingPeriod<GenericMonth>>(), UnitOfTimeGranularity.Month },
                { A.Dummy<IReportingPeriod<GenericQuarter>>(), UnitOfTimeGranularity.Quarter },
                { A.Dummy<IReportingPeriod<GenericYear>>(), UnitOfTimeGranularity.Year }
            };

            // Act
            var unitOfTimeKinds = reportingPeriods.Select(_ => new { Actual = _.Item1.GetUnitOfTimeKind(), Expected = _.Item2 }).ToList();

            // Assert
            unitOfTimeKinds.ForEach(_ => _.Actual.Should().Be(_.Expected));
        }

        // ReSharper restore InconsistentNaming
    }
}

// ReSharper restore CheckNamespace