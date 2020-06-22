// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfTimeExtensionsTest.Conversion.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Test
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using FakeItEasy;

    using FluentAssertions;

    using Xunit;

    [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "There are many kinds of units-of-time.")]
    public static partial class UnitOfTimeExtensionsTest
    {
        [Fact]
        public static void ToCalendarQuarter___Should_throw_ArgumentNullException___When_parameter_fiscalQuarter_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => UnitOfTimeExtensions.ToCalendarQuarter(null, A.Dummy<QuarterNumber>()));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void ToCalendarQuarter___Should_throw_ArgumentOutOfRangeException___When_parameter_calendarQuarterThatIsFirstFiscalQuarter_is_Invalid()
        {
            // Arrange, Act
            var ex = Record.Exception(() => A.Dummy<FiscalQuarter>().ToCalendarQuarter(QuarterNumber.Invalid));

            // Assert
            ex.Should().BeOfType<ArgumentOutOfRangeException>();
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
        public static void ToFiscalQuarter___Should_throw_ArgumentNullException___When_parameter_calendarQuarter_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => UnitOfTimeExtensions.ToFiscalQuarter(null, A.Dummy<QuarterNumber>()));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void ToFiscalQuarter___Should_throw_ArgumentOutOfRangeException___When_parameter_calendarQuarterThatIsFirstFiscalQuarter_is_Invalid()
        {
            // Arrange, Act
            var ex = Record.Exception(() => A.Dummy<CalendarQuarter>().ToFiscalQuarter(QuarterNumber.Invalid));

            // Assert
            ex.Should().BeOfType<ArgumentOutOfRangeException>();
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
        public static void ToMostGranular___Should_throw_ArgumentNullException___When_parameter_unitOfTime_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => UnitOfTimeExtensions.ToMostGranular(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void ToMostGranular___Should_return_reporting_period_of_CalendarUnbounded___When_unitOfTime_is_a_CalendarUnbounded()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                new { UnitOfTime = new CalendarUnbounded(), ReportingPeriod = new ReportingPeriod(new CalendarUnbounded(), new CalendarUnbounded()) },
            };

            // Act
            var reportingPeriods = unitsOfTime.Select(_ => new { Actual = _.UnitOfTime.ToMostGranular(), Expected = _.ReportingPeriod }).ToList();

            // Assert
            reportingPeriods.ForEach(_ => _.Expected.Should().Be(_.Actual));
        }

        [Fact]
        public static void ToMostGranular___Should_return_reporting_period_of_CalendarDay___When_unitOfTime_is_a_CalendarYear()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                new { UnitOfTime = new CalendarYear(2017), ReportingPeriod = new ReportingPeriod(new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One), new CalendarDay(2017, MonthOfYear.December, DayOfMonth.ThirtyOne)) },
            };

            // Act
            var reportingPeriods = unitsOfTime.Select(_ => new { Actual = _.UnitOfTime.ToMostGranular(), Expected = _.ReportingPeriod }).ToList();

            // Assert
            reportingPeriods.ForEach(_ => _.Expected.Should().Be(_.Actual));
        }

        [Fact]
        public static void ToMostGranular___Should_return_reporting_period_of_CalendarDay___When_unitOfTime_is_a_CalendarQuarter()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                new { UnitOfTime = new CalendarQuarter(2017, QuarterNumber.Q1), ReportingPeriod = new ReportingPeriod(new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One), new CalendarDay(2017, MonthOfYear.March, DayOfMonth.ThirtyOne)) },
                new { UnitOfTime = new CalendarQuarter(2017, QuarterNumber.Q2), ReportingPeriod = new ReportingPeriod(new CalendarDay(2017, MonthOfYear.April, DayOfMonth.One), new CalendarDay(2017, MonthOfYear.June, DayOfMonth.Thirty)) },
                new { UnitOfTime = new CalendarQuarter(2017, QuarterNumber.Q3), ReportingPeriod = new ReportingPeriod(new CalendarDay(2017, MonthOfYear.July, DayOfMonth.One), new CalendarDay(2017, MonthOfYear.September, DayOfMonth.Thirty)) },
                new { UnitOfTime = new CalendarQuarter(2017, QuarterNumber.Q4), ReportingPeriod = new ReportingPeriod(new CalendarDay(2017, MonthOfYear.October, DayOfMonth.One), new CalendarDay(2017, MonthOfYear.December, DayOfMonth.ThirtyOne)) },
            };

            // Act
            var reportingPeriods = unitsOfTime.Select(_ => new { Actual = _.UnitOfTime.ToMostGranular(), Expected = _.ReportingPeriod }).ToList();

            // Assert
            reportingPeriods.ForEach(_ => _.Expected.Should().Be(_.Actual));
        }

        [Fact]
        public static void ToMostGranular___Should_return_reporting_period_of_CalendarDay___When_unitOfTime_is_a_CalendarMonth()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                new { UnitOfTime = new CalendarMonth(2016, MonthOfYear.February), ReportingPeriod = new ReportingPeriod(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.One), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine)) },
            };

            // Act
            var reportingPeriods = unitsOfTime.Select(_ => new { Actual = _.UnitOfTime.ToMostGranular(), Expected = _.ReportingPeriod }).ToList();

            // Assert
            reportingPeriods.ForEach(_ => _.Expected.Should().Be(_.Actual));
        }

        [Fact]
        public static void ToMostGranular___Should_return_reporting_period_of_CalendarDay___When_unitOfTime_is_a_CalendarDay()
        {
            // Arrange
            var calendarDay = A.Dummy<CalendarDay>();
            var unitsOfTime = new[]
            {
                new { UnitOfTime = calendarDay, ReportingPeriod = new ReportingPeriod(calendarDay, calendarDay) },
            };

            // Act
            var reportingPeriods = unitsOfTime.Select(_ => new { Actual = _.UnitOfTime.ToMostGranular(), Expected = _.ReportingPeriod }).ToList();

            // Assert
            reportingPeriods.ForEach(_ => _.Expected.Should().Be(_.Actual));
        }

        [Fact]
        public static void ToMostGranular___Should_return_reporting_period_of_FiscalUnbounded___When_unitOfTime_is_a_FiscalUnbounded()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                new { UnitOfTime = new FiscalUnbounded(), ReportingPeriod = new ReportingPeriod(new FiscalUnbounded(), new FiscalUnbounded()) },
            };

            // Act
            var reportingPeriods = unitsOfTime.Select(_ => new { Actual = _.UnitOfTime.ToMostGranular(), Expected = _.ReportingPeriod }).ToList();

            // Assert
            reportingPeriods.ForEach(_ => _.Expected.Should().Be(_.Actual));
        }

        [Fact]
        public static void ToMostGranular___Should_return_reporting_period_of_FiscalMonth___When_unitOfTime_is_a_FiscalYear()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                new { UnitOfTime = new FiscalYear(2017), ReportingPeriod = new ReportingPeriod(new FiscalMonth(2017, MonthNumber.One), new FiscalMonth(2017, MonthNumber.Twelve)) },
            };

            // Act
            var reportingPeriods = unitsOfTime.Select(_ => new { Actual = _.UnitOfTime.ToMostGranular(), Expected = _.ReportingPeriod }).ToList();

            // Assert
            reportingPeriods.ForEach(_ => _.Expected.Should().Be(_.Actual));
        }

        [Fact]
        public static void ToMostGranular___Should_return_reporting_period_of_FiscalMonth___When_unitOfTime_is_a_FiscalQuarter()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                new { UnitOfTime = new FiscalQuarter(2017, QuarterNumber.Q1), ReportingPeriod = new ReportingPeriod(new FiscalMonth(2017, MonthNumber.One), new FiscalMonth(2017, MonthNumber.Three)) },
                new { UnitOfTime = new FiscalQuarter(2017, QuarterNumber.Q2), ReportingPeriod = new ReportingPeriod(new FiscalMonth(2017, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Six)) },
                new { UnitOfTime = new FiscalQuarter(2017, QuarterNumber.Q3), ReportingPeriod = new ReportingPeriod(new FiscalMonth(2017, MonthNumber.Seven), new FiscalMonth(2017, MonthNumber.Nine)) },
                new { UnitOfTime = new FiscalQuarter(2017, QuarterNumber.Q4), ReportingPeriod = new ReportingPeriod(new FiscalMonth(2017, MonthNumber.Ten), new FiscalMonth(2017, MonthNumber.Twelve)) },
            };

            // Act
            var reportingPeriods = unitsOfTime.Select(_ => new { Actual = _.UnitOfTime.ToMostGranular(), Expected = _.ReportingPeriod }).ToList();

            // Assert
            reportingPeriods.ForEach(_ => _.Expected.Should().Be(_.Actual));
        }

        [Fact]
        public static void ToMostGranular___Should_return_reporting_period_of_FiscalMonth___When_unitOfTime_is_a_FiscalMonth()
        {
            // Arrange
            var month = A.Dummy<FiscalMonth>();
            var unitsOfTime = new[]
            {
                new { UnitOfTime = month, ReportingPeriod = new ReportingPeriod(month, month) },
            };

            // Act
            var reportingPeriods = unitsOfTime.Select(_ => new { Actual = _.UnitOfTime.ToMostGranular(), Expected = _.ReportingPeriod }).ToList();

            // Assert
            reportingPeriods.ForEach(_ => _.Expected.Should().Be(_.Actual));
        }

        [Fact]
        public static void ToMostGranular___Should_return_reporting_period_of_GenericUnbounded___When_unitOfTime_is_a_GenericUnbounded()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                new { UnitOfTime = new GenericUnbounded(), ReportingPeriod = new ReportingPeriod(new GenericUnbounded(), new GenericUnbounded()) },
            };

            // Act
            var reportingPeriods = unitsOfTime.Select(_ => new { Actual = _.UnitOfTime.ToMostGranular(), Expected = _.ReportingPeriod }).ToList();

            // Assert
            reportingPeriods.ForEach(_ => _.Expected.Should().Be(_.Actual));
        }

        [Fact]
        public static void ToMostGranular___Should_return_reporting_period_of_GenericMonth___When_unitOfTime_is_a_GenericYear()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                new { UnitOfTime = new GenericYear(2017), ReportingPeriod = new ReportingPeriod(new GenericMonth(2017, MonthNumber.One), new GenericMonth(2017, MonthNumber.Twelve)) },
            };

            // Act
            var reportingPeriods = unitsOfTime.Select(_ => new { Actual = _.UnitOfTime.ToMostGranular(), Expected = _.ReportingPeriod }).ToList();

            // Assert
            reportingPeriods.ForEach(_ => _.Expected.Should().Be(_.Actual));
        }

        [Fact]
        public static void ToMostGranular___Should_return_reporting_period_of_GenericMonth___When_unitOfTime_is_a_GenericQuarter()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                new { UnitOfTime = new GenericQuarter(2017, QuarterNumber.Q1), ReportingPeriod = new ReportingPeriod(new GenericMonth(2017, MonthNumber.One), new GenericMonth(2017, MonthNumber.Three)) },
                new { UnitOfTime = new GenericQuarter(2017, QuarterNumber.Q2), ReportingPeriod = new ReportingPeriod(new GenericMonth(2017, MonthNumber.Four), new GenericMonth(2017, MonthNumber.Six)) },
                new { UnitOfTime = new GenericQuarter(2017, QuarterNumber.Q3), ReportingPeriod = new ReportingPeriod(new GenericMonth(2017, MonthNumber.Seven), new GenericMonth(2017, MonthNumber.Nine)) },
                new { UnitOfTime = new GenericQuarter(2017, QuarterNumber.Q4), ReportingPeriod = new ReportingPeriod(new GenericMonth(2017, MonthNumber.Ten), new GenericMonth(2017, MonthNumber.Twelve)) },
            };

            // Act
            var reportingPeriods = unitsOfTime.Select(_ => new { Actual = _.UnitOfTime.ToMostGranular(), Expected = _.ReportingPeriod }).ToList();

            // Assert
            reportingPeriods.ForEach(_ => _.Expected.Should().Be(_.Actual));
        }

        [Fact]
        public static void ToMostGranular___Should_return_reporting_period_of_GenericMonth___When_unitOfTime_is_a_GenericMonth()
        {
            // Arrange
            var month = A.Dummy<GenericMonth>();
            var unitsOfTime = new[]
            {
                new { UnitOfTime = month, ReportingPeriod = new ReportingPeriod(month, month) },
            };

            // Act
            var reportingPeriods = unitsOfTime.Select(_ => new { Actual = _.UnitOfTime.ToMostGranular(), Expected = _.ReportingPeriod }).ToList();

            // Assert
            reportingPeriods.ForEach(_ => _.Expected.Should().Be(_.Actual));
        }

        [Fact]
        public static void ToReportingPeriod___Should_throw_ArgumentNullException___When_parameter_unitOfTime_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => UnitOfTimeExtensions.ToReportingPeriod(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void ToReportingPeriod___Should_return_a_reporting_period_with_Start_and_End_equal_to_unitOfTime___When_called()
        {
            // Arrange,
            var unitOfTime = A.Dummy<UnitOfTime>();

            // Act
            var reportingPeriod = unitOfTime.ToReportingPeriod();

            // Assert
            reportingPeriod.Start.Should().Be(unitOfTime);
            reportingPeriod.End.Should().Be(unitOfTime);
        }
    }
}
