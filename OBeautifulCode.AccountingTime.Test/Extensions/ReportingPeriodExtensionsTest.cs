// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriodExtensionsTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime.Test
{
    using System;

    using FakeItEasy;

    using FluentAssertions;

    using Xunit;

    public static class ReportingPeriodExtensionsTest
    {
        // ReSharper disable InconsistentNaming
        [Fact]
        public static void IsInReportingPeriod___Should_throw_ArgumentNullException___When_parameter_unitOfTime_is_null()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriodInclusive<UnitOfTime>>();

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
            var reportingPeriod1 = A.Dummy<ReportingPeriodInclusive<CalendarMonth>>();

            var unitOfTime2 = A.Dummy<FiscalQuarter>();
            var reportingPeriod2 = A.Dummy<ReportingPeriodInclusive<GenericQuarter>>();

            // Act
            var ex1 = Record.Exception(() => unitOfTime1.IsInReportingPeriod<CalendarUnitOfTime>(reportingPeriod1));
            var ex2 = Record.Exception(() => unitOfTime2.IsInReportingPeriod<UnitOfTime>(reportingPeriod2));

            // Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex2.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void IsInReportingPeriod___Should_return_false___When_parameter_unitOfTime_is_in_reportingPeriod_and_both_represent_CalendarDay()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriodInclusive<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Thirty));
            var reportingPeriod2 = new ReportingPeriodInclusive<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight));

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
            var reportingPeriod1 = new ReportingPeriodInclusive<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Thirty));
            var reportingPeriod2 = new ReportingPeriodInclusive<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight));

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
            var reportingPeriod1 = new ReportingPeriodInclusive<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.September), new CalendarMonth(2017, MonthOfYear.February));
            var reportingPeriod2 = new ReportingPeriodInclusive<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.September), new CalendarMonth(2016, MonthOfYear.September));

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
            var reportingPeriod1 = new ReportingPeriodInclusive<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.September), new CalendarMonth(2017, MonthOfYear.February));
            var reportingPeriod2 = new ReportingPeriodInclusive<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.September), new CalendarMonth(2016, MonthOfYear.September));

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
            var reportingPeriod1 = new ReportingPeriodInclusive<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Nine), new FiscalMonth(2017, MonthNumber.Two));
            var reportingPeriod2 = new ReportingPeriodInclusive<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Nine), new FiscalMonth(2016, MonthNumber.Nine));

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
            var reportingPeriod1 = new ReportingPeriodInclusive<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Nine), new FiscalMonth(2017, MonthNumber.Two));
            var reportingPeriod2 = new ReportingPeriodInclusive<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Nine), new FiscalMonth(2016, MonthNumber.Nine));

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
            var reportingPeriod1 = new ReportingPeriodInclusive<GenericMonth>(new GenericMonth(2016, MonthNumber.Nine), new GenericMonth(2017, MonthNumber.Two));
            var reportingPeriod2 = new ReportingPeriodInclusive<GenericMonth>(new GenericMonth(2016, MonthNumber.Nine), new GenericMonth(2016, MonthNumber.Nine));

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
            var reportingPeriod1 = new ReportingPeriodInclusive<GenericMonth>(new GenericMonth(2016, MonthNumber.Nine), new GenericMonth(2017, MonthNumber.Two));
            var reportingPeriod2 = new ReportingPeriodInclusive<GenericMonth>(new GenericMonth(2016, MonthNumber.Nine), new GenericMonth(2016, MonthNumber.Nine));

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
            var reportingPeriod1 = new ReportingPeriodInclusive<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Second), new CalendarQuarter(2017, QuarterNumber.Third));
            var reportingPeriod2 = new ReportingPeriodInclusive<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Second), new CalendarQuarter(2016, QuarterNumber.Second));

            var unitOfTime1a = new CalendarQuarter(2016, QuarterNumber.First);
            var unitOfTime1b = new CalendarQuarter(2017, QuarterNumber.Fourth);
            var unitOfTime1c = new CalendarQuarter(2015, QuarterNumber.Second);

            var unitOfTime2a = new CalendarQuarter(2016, QuarterNumber.First);
            var unitOfTime2b = new CalendarQuarter(2016, QuarterNumber.Third);
            var unitOfTime2c = new CalendarQuarter(2017, QuarterNumber.Second);
            var unitOfTime2d = new CalendarQuarter(2015, QuarterNumber.Second);

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
            var reportingPeriod1 = new ReportingPeriodInclusive<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Second), new CalendarQuarter(2017, QuarterNumber.Third));
            var reportingPeriod2 = new ReportingPeriodInclusive<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Second), new CalendarQuarter(2016, QuarterNumber.Second));

            var unitOfTime1a = new CalendarQuarter(2016, QuarterNumber.Second);
            var unitOfTime1b = new CalendarQuarter(2016, QuarterNumber.Third);
            var unitOfTime1c = new CalendarQuarter(2016, QuarterNumber.Fourth);
            var unitOfTime1d = new CalendarQuarter(2017, QuarterNumber.First);
            var unitOfTime1e = new CalendarQuarter(2017, QuarterNumber.Second);
            var unitOfTime1f = new CalendarQuarter(2017, QuarterNumber.Third);

            var unitOfTime2 = new CalendarQuarter(2016, QuarterNumber.Second);

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
            var reportingPeriod1 = new ReportingPeriodInclusive<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Second), new FiscalQuarter(2017, QuarterNumber.Third));
            var reportingPeriod2 = new ReportingPeriodInclusive<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Second), new FiscalQuarter(2016, QuarterNumber.Second));

            var unitOfTime1a = new FiscalQuarter(2016, QuarterNumber.First);
            var unitOfTime1b = new FiscalQuarter(2017, QuarterNumber.Fourth);
            var unitOfTime1c = new FiscalQuarter(2015, QuarterNumber.Second);

            var unitOfTime2a = new FiscalQuarter(2016, QuarterNumber.First);
            var unitOfTime2b = new FiscalQuarter(2016, QuarterNumber.Third);
            var unitOfTime2c = new FiscalQuarter(2017, QuarterNumber.Second);
            var unitOfTime2d = new FiscalQuarter(2015, QuarterNumber.Second);

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
            var reportingPeriod1 = new ReportingPeriodInclusive<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Second), new FiscalQuarter(2017, QuarterNumber.Third));
            var reportingPeriod2 = new ReportingPeriodInclusive<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Second), new FiscalQuarter(2016, QuarterNumber.Second));

            var unitOfTime1a = new FiscalQuarter(2016, QuarterNumber.Second);
            var unitOfTime1b = new FiscalQuarter(2016, QuarterNumber.Third);
            var unitOfTime1c = new FiscalQuarter(2016, QuarterNumber.Fourth);
            var unitOfTime1d = new FiscalQuarter(2017, QuarterNumber.First);
            var unitOfTime1e = new FiscalQuarter(2017, QuarterNumber.Second);
            var unitOfTime1f = new FiscalQuarter(2017, QuarterNumber.Third);

            var unitOfTime2 = new FiscalQuarter(2016, QuarterNumber.Second);

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
            var reportingPeriod1 = new ReportingPeriodInclusive<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Second), new GenericQuarter(2017, QuarterNumber.Third));
            var reportingPeriod2 = new ReportingPeriodInclusive<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Second), new GenericQuarter(2016, QuarterNumber.Second));

            var unitOfTime1a = new GenericQuarter(2016, QuarterNumber.First);
            var unitOfTime1b = new GenericQuarter(2017, QuarterNumber.Fourth);
            var unitOfTime1c = new GenericQuarter(2015, QuarterNumber.Second);

            var unitOfTime2a = new GenericQuarter(2016, QuarterNumber.First);
            var unitOfTime2b = new GenericQuarter(2016, QuarterNumber.Third);
            var unitOfTime2c = new GenericQuarter(2017, QuarterNumber.Second);
            var unitOfTime2d = new GenericQuarter(2015, QuarterNumber.Second);

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
            var reportingPeriod1 = new ReportingPeriodInclusive<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Second), new GenericQuarter(2017, QuarterNumber.Third));
            var reportingPeriod2 = new ReportingPeriodInclusive<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Second), new GenericQuarter(2016, QuarterNumber.Second));

            var unitOfTime1a = new GenericQuarter(2016, QuarterNumber.Second);
            var unitOfTime1b = new GenericQuarter(2016, QuarterNumber.Third);
            var unitOfTime1c = new GenericQuarter(2016, QuarterNumber.Fourth);
            var unitOfTime1d = new GenericQuarter(2017, QuarterNumber.First);
            var unitOfTime1e = new GenericQuarter(2017, QuarterNumber.Second);
            var unitOfTime1f = new GenericQuarter(2017, QuarterNumber.Third);

            var unitOfTime2 = new GenericQuarter(2016, QuarterNumber.Second);

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
            var reportingPeriod1 = new ReportingPeriodInclusive<CalendarYear>(new CalendarYear(2016), new CalendarYear(2018));
            var reportingPeriod2 = new ReportingPeriodInclusive<CalendarYear>(new CalendarYear(2016), new CalendarYear(2016));

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
            var reportingPeriod1 = new ReportingPeriodInclusive<CalendarYear>(new CalendarYear(2016), new CalendarYear(2018));
            var reportingPeriod2 = new ReportingPeriodInclusive<CalendarYear>(new CalendarYear(2016), new CalendarYear(2016));

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
            var reportingPeriod1 = new ReportingPeriodInclusive<FiscalYear>(new FiscalYear(2016), new FiscalYear(2018));
            var reportingPeriod2 = new ReportingPeriodInclusive<FiscalYear>(new FiscalYear(2016), new FiscalYear(2016));

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
            var reportingPeriod1 = new ReportingPeriodInclusive<FiscalYear>(new FiscalYear(2016), new FiscalYear(2018));
            var reportingPeriod2 = new ReportingPeriodInclusive<FiscalYear>(new FiscalYear(2016), new FiscalYear(2016));

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
            var reportingPeriod1 = new ReportingPeriodInclusive<GenericYear>(new GenericYear(2016), new GenericYear(2018));
            var reportingPeriod2 = new ReportingPeriodInclusive<GenericYear>(new GenericYear(2016), new GenericYear(2016));

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
            var reportingPeriod1 = new ReportingPeriodInclusive<GenericYear>(new GenericYear(2016), new GenericYear(2018));
            var reportingPeriod2 = new ReportingPeriodInclusive<GenericYear>(new GenericYear(2016), new GenericYear(2016));

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

        // ReSharper restore InconsistentNaming
    }
}

// ReSharper restore CheckNamespace