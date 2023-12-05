// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriodExtensionsTest.Properties.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Test
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using FakeItEasy;

    using FluentAssertions;

    using OBeautifulCode.AutoFakeItEasy;

    using Xunit;

    [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Testing this class requires lots of types because of the number of unit-of-time types intersected with the options for reporting period.")]
    public static partial class ReportingPeriodExtensionsTest
    {
        [Fact]
        public static void GetUnit___Should_throw_ArgumentNullException___When_parameter_reportingPeriod_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.GetUnit(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Need to test all flavors of unit-of-time")]
        public static void GetUnit___Should_throw_ArgumentException___When_reportingPeriod_Start_and_End_has_different_granularity()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                A.Dummy<ReportingPeriod>().Whose(_ => (_.GetUnitOfTimeKind() == UnitOfTimeKind.Calendar) && (_.Start.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded) && (_.End.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded)),
                A.Dummy<ReportingPeriod>().Whose(_ => (_.GetUnitOfTimeKind() == UnitOfTimeKind.Calendar) && (_.Start.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded) && (_.End.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded)),
                A.Dummy<ReportingPeriod>().Whose(_ => (_.GetUnitOfTimeKind() == UnitOfTimeKind.Fiscal) && (_.Start.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded) && (_.End.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded)),
                A.Dummy<ReportingPeriod>().Whose(_ => (_.GetUnitOfTimeKind() == UnitOfTimeKind.Fiscal) && (_.Start.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded) && (_.End.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded)),
                A.Dummy<ReportingPeriod>().Whose(_ => (_.GetUnitOfTimeKind() == UnitOfTimeKind.Generic) && (_.Start.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded) && (_.End.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded)),
                A.Dummy<ReportingPeriod>().Whose(_ => (_.GetUnitOfTimeKind() == UnitOfTimeKind.Generic) && (_.Start.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded) && (_.End.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded)),
            };

            // Act
            var unitOfTimeGranularity = reportingPeriods.Select(_ => Record.Exception(() => _.GetUnit())).ToList();

            // Assert
            unitOfTimeGranularity.ForEach(_ => _.Should().BeOfType<ArgumentException>());
        }

        [Fact]
        [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Need to test all flavors of unit-of-time")]
        public static void GetUnit___Should_return_the_unit_of_the_reporting_period___When_called()
        {
            // Arrange
            var reportingPeriods = new Dictionary<ReportingPeriod, Unit>
            {
                { A.Dummy<CalendarDayReportingPeriod>(), new Unit(UnitOfTimeKind.Calendar, UnitOfTimeGranularity.Day) },
                { A.Dummy<CalendarMonthReportingPeriod>(), new Unit(UnitOfTimeKind.Calendar, UnitOfTimeGranularity.Month) },
                { A.Dummy<CalendarQuarterReportingPeriod>(), new Unit(UnitOfTimeKind.Calendar, UnitOfTimeGranularity.Quarter) },
                { A.Dummy<CalendarYearReportingPeriod>(), new Unit(UnitOfTimeKind.Calendar, UnitOfTimeGranularity.Year) },
                { A.Dummy<CalendarUnboundedReportingPeriod>(), new Unit(UnitOfTimeKind.Calendar, UnitOfTimeGranularity.Unbounded) },
                { A.Dummy<FiscalMonthReportingPeriod>(), new Unit(UnitOfTimeKind.Fiscal, UnitOfTimeGranularity.Month) },
                { A.Dummy<FiscalQuarterReportingPeriod>(), new Unit(UnitOfTimeKind.Fiscal, UnitOfTimeGranularity.Quarter) },
                { A.Dummy<FiscalYearReportingPeriod>(), new Unit(UnitOfTimeKind.Fiscal, UnitOfTimeGranularity.Year) },
                { A.Dummy<FiscalUnboundedReportingPeriod>(), new Unit(UnitOfTimeKind.Fiscal, UnitOfTimeGranularity.Unbounded) },
                { A.Dummy<GenericMonthReportingPeriod>(), new Unit(UnitOfTimeKind.Generic, UnitOfTimeGranularity.Month) },
                { A.Dummy<GenericQuarterReportingPeriod>(), new Unit(UnitOfTimeKind.Generic, UnitOfTimeGranularity.Quarter) },
                { A.Dummy<GenericYearReportingPeriod>(), new Unit(UnitOfTimeKind.Generic, UnitOfTimeGranularity.Year) },
                { A.Dummy<GenericUnboundedReportingPeriod>(), new Unit(UnitOfTimeKind.Generic, UnitOfTimeGranularity.Unbounded) },
            };

            // Act
            var unitOfTimeGranularity = reportingPeriods.Select(_ => new { Actual = _.Key.GetUnit(), Expected = _.Value }).ToList();

            // Assert
            unitOfTimeGranularity.ForEach(_ => _.Actual.Should().Be(_.Expected));
        }

        [Fact]
        public static void GetUnitOfTimeGranularity___Should_throw_ArgumentNullException___When_parameter_reportingPeriod_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.GetUnitOfTimeGranularity(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Need to test all flavors of unit-of-time")]
        public static void GetUnitOfTimeGranularity___Should_throw_ArgumentException___When_reportingPeriod_Start_and_End_has_different_granularity()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                A.Dummy<ReportingPeriod>().Whose(_ => (_.GetUnitOfTimeKind() == UnitOfTimeKind.Calendar) && (_.Start.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded) && (_.End.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded)),
                A.Dummy<ReportingPeriod>().Whose(_ => (_.GetUnitOfTimeKind() == UnitOfTimeKind.Calendar) && (_.Start.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded) && (_.End.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded)),
                A.Dummy<ReportingPeriod>().Whose(_ => (_.GetUnitOfTimeKind() == UnitOfTimeKind.Fiscal) && (_.Start.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded) && (_.End.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded)),
                A.Dummy<ReportingPeriod>().Whose(_ => (_.GetUnitOfTimeKind() == UnitOfTimeKind.Fiscal) && (_.Start.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded) && (_.End.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded)),
                A.Dummy<ReportingPeriod>().Whose(_ => (_.GetUnitOfTimeKind() == UnitOfTimeKind.Generic) && (_.Start.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded) && (_.End.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded)),
                A.Dummy<ReportingPeriod>().Whose(_ => (_.GetUnitOfTimeKind() == UnitOfTimeKind.Generic) && (_.Start.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded) && (_.End.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded)),
            };

            // Act
            var unitOfTimeGranularity = reportingPeriods.Select(_ => Record.Exception(() => _.GetUnitOfTimeGranularity())).ToList();

            // Assert
            unitOfTimeGranularity.ForEach(_ => _.Should().BeOfType<ArgumentException>());
        }

        [Fact]
        [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Need to test all flavors of unit-of-time")]
        public static void GetUnitOfTimeGranularity___Should_return_the_granularity_of_unit_of_time_used_in_the_reporting_period___When_called()
        {
            // Arrange
            var reportingPeriods = new Dictionary<ReportingPeriod, UnitOfTimeGranularity>
            {
                { A.Dummy<CalendarDayReportingPeriod>(), UnitOfTimeGranularity.Day },
                { A.Dummy<CalendarMonthReportingPeriod>(), UnitOfTimeGranularity.Month },
                { A.Dummy<CalendarQuarterReportingPeriod>(), UnitOfTimeGranularity.Quarter },
                { A.Dummy<CalendarYearReportingPeriod>(), UnitOfTimeGranularity.Year },
                { A.Dummy<CalendarUnboundedReportingPeriod>(), UnitOfTimeGranularity.Unbounded },
                { A.Dummy<FiscalMonthReportingPeriod>(), UnitOfTimeGranularity.Month },
                { A.Dummy<FiscalQuarterReportingPeriod>(), UnitOfTimeGranularity.Quarter },
                { A.Dummy<FiscalYearReportingPeriod>(), UnitOfTimeGranularity.Year },
                { A.Dummy<FiscalUnboundedReportingPeriod>(), UnitOfTimeGranularity.Unbounded },
                { A.Dummy<GenericMonthReportingPeriod>(), UnitOfTimeGranularity.Month },
                { A.Dummy<GenericQuarterReportingPeriod>(), UnitOfTimeGranularity.Quarter },
                { A.Dummy<GenericYearReportingPeriod>(), UnitOfTimeGranularity.Year },
                { A.Dummy<GenericUnboundedReportingPeriod>(), UnitOfTimeGranularity.Unbounded },
            };

            // Act
            var unitOfTimeGranularity = reportingPeriods.Select(_ => new { Actual = _.Key.GetUnitOfTimeGranularity(), Expected = _.Value }).ToList();

            // Assert
            unitOfTimeGranularity.ForEach(_ => _.Actual.Should().Be(_.Expected));
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
        [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Need to test all flavors of unit-of-time")]
        public static void GetUnitOfTimeKind___Should_return_the_kind_of_unit_of_time_used_in_the_reporting_period___When_called()
        {
            // Arrange
            Dictionary<ReportingPeriod, UnitOfTimeKind> reportingPeriods;

            while (true)
            {
                try
                {
                    // need this loop to avoid duplicate key exception
                    reportingPeriods = new Dictionary<ReportingPeriod, UnitOfTimeKind>
                    {
                        { A.Dummy<CalendarReportingPeriod>(), UnitOfTimeKind.Calendar },
                        { A.Dummy<CalendarDayReportingPeriod>(), UnitOfTimeKind.Calendar },
                        { A.Dummy<CalendarMonthReportingPeriod>(), UnitOfTimeKind.Calendar },
                        { A.Dummy<CalendarQuarterReportingPeriod>(), UnitOfTimeKind.Calendar },
                        { A.Dummy<CalendarYearReportingPeriod>(), UnitOfTimeKind.Calendar },
                        { A.Dummy<CalendarUnboundedReportingPeriod>(), UnitOfTimeKind.Calendar },
                        { A.Dummy<FiscalReportingPeriod>(), UnitOfTimeKind.Fiscal },
                        { A.Dummy<FiscalMonthReportingPeriod>(), UnitOfTimeKind.Fiscal },
                        { A.Dummy<FiscalQuarterReportingPeriod>(), UnitOfTimeKind.Fiscal },
                        { A.Dummy<FiscalYearReportingPeriod>(), UnitOfTimeKind.Fiscal },
                        { A.Dummy<FiscalUnboundedReportingPeriod>(), UnitOfTimeKind.Fiscal },
                        { A.Dummy<GenericReportingPeriod>(), UnitOfTimeKind.Generic },
                        { A.Dummy<GenericMonthReportingPeriod>(), UnitOfTimeKind.Generic },
                        { A.Dummy<GenericQuarterReportingPeriod>(), UnitOfTimeKind.Generic },
                        { A.Dummy<GenericYearReportingPeriod>(), UnitOfTimeKind.Generic },
                        { A.Dummy<GenericUnboundedReportingPeriod>(), UnitOfTimeKind.Generic },
                    };

                    break;
                }
                catch (Exception)
                {
                }
            }

            // Act
            var unitOfTimeKinds = reportingPeriods.Select(_ => new { Actual = _.Key.GetUnitOfTimeKind(), Expected = _.Value }).ToList();

            // Assert
            unitOfTimeKinds.ForEach(_ => _.Actual.Should().Be(_.Expected));
        }

        [Fact]
        public static void GetUnitsOfTimeWithin___Should_throw_ArgumentNullException___When_parameter_reportingPeriod_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.GetUnitsOfTimeWithin(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void GetUnitsOfTimeWithin___Should_throw_ArgumentException___When_parameter_reportingPeriod_Start_and_or_End_is_unbounded()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod(new CalendarUnbounded(), A.Dummy<CalendarUnitOfTime>());
            var reportingPeriod2 = new ReportingPeriod(A.Dummy<GenericUnitOfTime>(), new GenericUnbounded());
            var reportingPeriod3 = new ReportingPeriod(new FiscalUnbounded(), new FiscalUnbounded());

            // Act
            var ex1 = Record.Exception(() => reportingPeriod1.GetUnitsOfTimeWithin());
            var ex2 = Record.Exception(() => reportingPeriod2.GetUnitsOfTimeWithin());
            var ex3 = Record.Exception(() => reportingPeriod3.GetUnitsOfTimeWithin());

            // Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex2.Should().BeOfType<ArgumentException>();
            ex3.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void GetUnitsOfTimeWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_CalendarDay()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight));
            var reportingPeriod2 = new ReportingPeriod(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Three));

            // Act
            var actualUnits1 = reportingPeriod1.GetUnitsOfTimeWithin();
            var actualUnits2 = reportingPeriod2.GetUnitsOfTimeWithin();

            // Assert
            actualUnits1.Should().Equal(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight));
            actualUnits2.Should().Equal(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Two), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Three));
        }

        [Fact]
        public static void GetUnitsOfTimeWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_CalendarMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.February));
            var reportingPeriod2 = new ReportingPeriod(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.April));
            var reportingPeriod3 = new ReportingPeriod(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2017, MonthOfYear.January));

            // Act
            var actualUnits1 = reportingPeriod1.GetUnitsOfTimeWithin();
            var actualUnits2 = reportingPeriod2.GetUnitsOfTimeWithin();
            var actualUnits3 = reportingPeriod3.GetUnitsOfTimeWithin();

            // Assert
            actualUnits1.Should().Equal(new CalendarMonth(2016, MonthOfYear.February));
            actualUnits2.Should().Equal(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.March), new CalendarMonth(2016, MonthOfYear.April));
            actualUnits3.Should().Equal(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.March), new CalendarMonth(2016, MonthOfYear.April), new CalendarMonth(2016, MonthOfYear.May), new CalendarMonth(2016, MonthOfYear.June), new CalendarMonth(2016, MonthOfYear.July), new CalendarMonth(2016, MonthOfYear.August), new CalendarMonth(2016, MonthOfYear.September), new CalendarMonth(2016, MonthOfYear.October), new CalendarMonth(2016, MonthOfYear.November), new CalendarMonth(2016, MonthOfYear.December), new CalendarMonth(2017, MonthOfYear.January));
        }

        [Fact]
        public static void GetUnitsOfTimeWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_FiscalMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Two));
            var reportingPeriod2 = new ReportingPeriod(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Three));
            var reportingPeriod3 = new ReportingPeriod(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2017, MonthNumber.One));

            // Act
            var actualUnits1 = reportingPeriod1.GetUnitsOfTimeWithin();
            var actualUnits2 = reportingPeriod2.GetUnitsOfTimeWithin();
            var actualUnits3 = reportingPeriod3.GetUnitsOfTimeWithin();

            // Assert
            actualUnits1.Should().Equal(new FiscalMonth(2016, MonthNumber.Two));
            actualUnits2.Should().Equal(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Three));
            actualUnits3.Should().Equal(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Three), new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2016, MonthNumber.Five), new FiscalMonth(2016, MonthNumber.Six), new FiscalMonth(2016, MonthNumber.Seven), new FiscalMonth(2016, MonthNumber.Eight), new FiscalMonth(2016, MonthNumber.Nine), new FiscalMonth(2016, MonthNumber.Ten), new FiscalMonth(2016, MonthNumber.Eleven), new FiscalMonth(2016, MonthNumber.Twelve), new FiscalMonth(2017, MonthNumber.One));
        }

        [Fact]
        public static void GetUnitsOfTimeWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_GenericMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Two));
            var reportingPeriod2 = new ReportingPeriod(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Three));
            var reportingPeriod3 = new ReportingPeriod(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2017, MonthNumber.One));

            // Act
            var actualUnits1 = reportingPeriod1.GetUnitsOfTimeWithin();
            var actualUnits2 = reportingPeriod2.GetUnitsOfTimeWithin();
            var actualUnits3 = reportingPeriod3.GetUnitsOfTimeWithin();

            // Assert
            actualUnits1.Should().Equal(new GenericMonth(2016, MonthNumber.Two));
            actualUnits2.Should().Equal(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Three));
            actualUnits3.Should().Equal(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Three), new GenericMonth(2016, MonthNumber.Four), new GenericMonth(2016, MonthNumber.Five), new GenericMonth(2016, MonthNumber.Six), new GenericMonth(2016, MonthNumber.Seven), new GenericMonth(2016, MonthNumber.Eight), new GenericMonth(2016, MonthNumber.Nine), new GenericMonth(2016, MonthNumber.Ten), new GenericMonth(2016, MonthNumber.Eleven), new GenericMonth(2016, MonthNumber.Twelve), new GenericMonth(2017, MonthNumber.One));
        }

        [Fact]
        public static void GetUnitsOfTimeWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_CalendarQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q2));
            var reportingPeriod2 = new ReportingPeriod(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q4));
            var reportingPeriod3 = new ReportingPeriod(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2017, QuarterNumber.Q3));

            // Act
            var actualUnits1 = reportingPeriod1.GetUnitsOfTimeWithin();
            var actualUnits2 = reportingPeriod2.GetUnitsOfTimeWithin();
            var actualUnits3 = reportingPeriod3.GetUnitsOfTimeWithin();

            // Assert
            actualUnits1.Should().Equal(new CalendarQuarter(2016, QuarterNumber.Q2));
            actualUnits2.Should().Equal(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q3), new CalendarQuarter(2016, QuarterNumber.Q4));
            actualUnits3.Should().Equal(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q3), new CalendarQuarter(2016, QuarterNumber.Q4), new CalendarQuarter(2017, QuarterNumber.Q1), new CalendarQuarter(2017, QuarterNumber.Q2), new CalendarQuarter(2017, QuarterNumber.Q3));
        }

        [Fact]
        public static void GetUnitsOfTimeWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_FiscalQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q2));
            var reportingPeriod2 = new ReportingPeriod(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q4));
            var reportingPeriod3 = new ReportingPeriod(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2017, QuarterNumber.Q3));

            // Act
            var actualUnits1 = reportingPeriod1.GetUnitsOfTimeWithin();
            var actualUnits2 = reportingPeriod2.GetUnitsOfTimeWithin();
            var actualUnits3 = reportingPeriod3.GetUnitsOfTimeWithin();

            // Assert
            actualUnits1.Should().Equal(new FiscalQuarter(2016, QuarterNumber.Q2));
            actualUnits2.Should().Equal(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q3), new FiscalQuarter(2016, QuarterNumber.Q4));
            actualUnits3.Should().Equal(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q3), new FiscalQuarter(2016, QuarterNumber.Q4), new FiscalQuarter(2017, QuarterNumber.Q1), new FiscalQuarter(2017, QuarterNumber.Q2), new FiscalQuarter(2017, QuarterNumber.Q3));
        }

        [Fact]
        public static void GetUnitsOfTimeWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_GenericQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q2));
            var reportingPeriod2 = new ReportingPeriod(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q4));
            var reportingPeriod3 = new ReportingPeriod(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2017, QuarterNumber.Q3));

            // Act
            var actualUnits1 = reportingPeriod1.GetUnitsOfTimeWithin();
            var actualUnits2 = reportingPeriod2.GetUnitsOfTimeWithin();
            var actualUnits3 = reportingPeriod3.GetUnitsOfTimeWithin();

            // Assert
            actualUnits1.Should().Equal(new GenericQuarter(2016, QuarterNumber.Q2));
            actualUnits2.Should().Equal(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q3), new GenericQuarter(2016, QuarterNumber.Q4));
            actualUnits3.Should().Equal(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q3), new GenericQuarter(2016, QuarterNumber.Q4), new GenericQuarter(2017, QuarterNumber.Q1), new GenericQuarter(2017, QuarterNumber.Q2), new GenericQuarter(2017, QuarterNumber.Q3));
        }

        [Fact]
        public static void GetUnitsOfTimeWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_CalendarYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod(new CalendarYear(2016), new CalendarYear(2016));
            var reportingPeriod2 = new ReportingPeriod(new CalendarYear(2016), new CalendarYear(2017));
            var reportingPeriod3 = new ReportingPeriod(new CalendarYear(2016), new CalendarYear(2018));

            // Act
            var actualUnits1 = reportingPeriod1.GetUnitsOfTimeWithin();
            var actualUnits2 = reportingPeriod2.GetUnitsOfTimeWithin();
            var actualUnits3 = reportingPeriod3.GetUnitsOfTimeWithin();

            // Assert
            actualUnits1.Should().Equal(new CalendarYear(2016));
            actualUnits2.Should().Equal(new CalendarYear(2016), new CalendarYear(2017));
            actualUnits3.Should().Equal(new CalendarYear(2016), new CalendarYear(2017), new CalendarYear(2018));
        }

        [Fact]
        public static void GetUnitsOfTimeWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_FiscalYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod(new FiscalYear(2016), new FiscalYear(2016));
            var reportingPeriod2 = new ReportingPeriod(new FiscalYear(2016), new FiscalYear(2017));
            var reportingPeriod3 = new ReportingPeriod(new FiscalYear(2016), new FiscalYear(2018));

            // Act
            var actualUnits1 = reportingPeriod1.GetUnitsOfTimeWithin();
            var actualUnits2 = reportingPeriod2.GetUnitsOfTimeWithin();
            var actualUnits3 = reportingPeriod3.GetUnitsOfTimeWithin();

            // Assert
            actualUnits1.Should().Equal(new FiscalYear(2016));
            actualUnits2.Should().Equal(new FiscalYear(2016), new FiscalYear(2017));
            actualUnits3.Should().Equal(new FiscalYear(2016), new FiscalYear(2017), new FiscalYear(2018));
        }

        [Fact]
        public static void GetUnitsOfTimeWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_GenericYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod(new GenericYear(2016), new GenericYear(2016));
            var reportingPeriod2 = new ReportingPeriod(new GenericYear(2016), new GenericYear(2017));
            var reportingPeriod3 = new ReportingPeriod(new GenericYear(2016), new GenericYear(2018));

            // Act
            var actualUnits1 = reportingPeriod1.GetUnitsOfTimeWithin();
            var actualUnits2 = reportingPeriod2.GetUnitsOfTimeWithin();
            var actualUnits3 = reportingPeriod3.GetUnitsOfTimeWithin();

            // Assert
            actualUnits1.Should().Equal(new GenericYear(2016));
            actualUnits2.Should().Equal(new GenericYear(2016), new GenericYear(2017));
            actualUnits3.Should().Equal(new GenericYear(2016), new GenericYear(2017), new GenericYear(2018));
        }

        [Fact]
        public static void HasComponentWithUnboundedGranularity___Should_throw_ArgumentNullException___When_parameter_reportingPeriod_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.HasComponentWithUnboundedGranularity(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void HasComponentWithUnboundedGranularity___Should_return_false___When_neither_the_Start_nor_End_component_has_an_Unbounded_UnitOfTimeGranularity()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod>().Whose(_ => (_.Start.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded) && (_.End.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded));

            // Act
            var result = reportingPeriod.HasComponentWithUnboundedGranularity();

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void HasComponentWithUnboundedGranularity___Should_return_true___When_either_the_Start_or_End_or_both_components_has_an_Unbounded_UnitOfTimeGranularity()
        {
            // Arrange
            var reportingPeriod1 = A.Dummy<ReportingPeriod>().Whose(_ => (_.Start.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded) && (_.End.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded));
            var reportingPeriod2 = A.Dummy<ReportingPeriod>().Whose(_ => (_.Start.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded) && (_.End.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded));
            var reportingPeriod3 = A.Dummy<ReportingPeriod>().Whose(_ => (_.Start.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded) && (_.End.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded));

            // Act
            var result1 = reportingPeriod1.HasComponentWithUnboundedGranularity();
            var result2 = reportingPeriod2.HasComponentWithUnboundedGranularity();
            var result3 = reportingPeriod3.HasComponentWithUnboundedGranularity();

            // Assert
            result1.Should().BeTrue();
            result2.Should().BeTrue();
            result3.Should().BeTrue();
        }
    }
}
