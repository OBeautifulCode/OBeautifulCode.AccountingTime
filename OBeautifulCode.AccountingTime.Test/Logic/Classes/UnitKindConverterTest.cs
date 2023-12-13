// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitKindConverterTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Test
{
    using System;
    using System.Linq;
    using FakeItEasy;
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.AutoFakeItEasy;
    using Xunit;

    public static class UnitKindConverterTest
    {
        [Fact]
        public static void Constructor___Should_throw_ArgumentNullException___When_parameter_unitKindAssociations_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => new UnitKindConverter(null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void Constructor___Should_throw_ArgumentException___When_parameter_unitKindAssociations_contains_a_null_element()
        {
            // Arrange, Act
            var actual = Record.Exception(() => new UnitKindConverter(
                new[]
                {
                    A.Dummy<UnitKindAssociation>(),
                    null,
                    A.Dummy<UnitKindAssociation>(),
                }));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentException>();
            actual.Message.AsTest().Must().ContainString("contains a null element");
        }

        [Fact]
        public static void Constructor___Should_throw_ArgumentException___When_unitKindAssociations_contains_a_contradictory_associations()
        {
            // Arrange, Act
            var actual = Record.Exception(() => new UnitKindConverter(
                new[]
                {
                    new UnitKindAssociation(
                        QuarterNumber.Q1.ToFiscal(2020).ToReportingPeriod(),
                        QuarterNumber.Q1.ToCalendar(2020).ToReportingPeriod()),
                    new UnitKindAssociation(
                        QuarterNumber.Q1.ToFiscal(2020).ToReportingPeriod(),
                        QuarterNumber.Q2.ToCalendar(2020).ToReportingPeriod()),
                }));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentException>();
            actual.Message.AsTest().Must().ContainString("Attempting to associate reportingPeriod1 (1Q2020 to 1Q2020) with reportingPeriod2 (2Q2020 to 2Q2020), however, reportingPeriod1 is already associated with a different reporting period (1Q2020 to 1Q2020) for the same Unit as reportingPeriod2 (Calendar Quarter)");
        }

        [Fact]
        public static void TryConvert___Should_throw_ArgumentNullException___When_parameter_reportingPeriod_is_null()
        {
            // Arrange,
            var systemUnderTest = new UnitKindConverter(new UnitKindAssociation[] { });
            var unit = A.Dummy<Unit>().Whose(_ => _.Granularity != UnitOfTimeGranularity.Unbounded);

            // Act
            var actual = Record.Exception(() => systemUnderTest.TryConvert(null, unit, out var result));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("reportingPeriod");
        }

        [Fact]
        public static void TryConvert___Should_throw_ArgumentException___When_parameter_reportingPeriod_has_component_with_Unbounded_granularity()
        {
            // Arrange,
            var systemUnderTest = new UnitKindConverter(new UnitKindAssociation[] { });
            var reportingPeriod = A.Dummy<ReportingPeriod>().Whose(_ => _.HasComponentWithUnboundedGranularity());
            var unit = A.Dummy<Unit>().Whose(_ => (_.Granularity != UnitOfTimeGranularity.Unbounded) && (_.Kind != reportingPeriod.GetUnitOfTimeKind()));

            // Act
            var actual = Record.Exception(() => systemUnderTest.TryConvert(reportingPeriod, unit, out var result));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentException>();
            actual.Message.AsTest().Must().ContainString("has a component with Unbounded UnitOfTimeGranularity");
        }

        [Fact]
        public static void TryConvert___Should_throw_ArgumentNullException___When_parameter_unit_is_null()
        {
            // Arrange,
            var systemUnderTest = new UnitKindConverter(new UnitKindAssociation[] { });
            var reportingPeriod = A.Dummy<ReportingPeriod>().Whose(_ => !_.HasComponentWithUnboundedGranularity());

            // Act
            var actual = Record.Exception(() => systemUnderTest.TryConvert(reportingPeriod, null, out var result));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("unit");
        }

        [Fact]
        public static void TryConvert___Should_throw_ArgumentException___When_parameter_unit_is_Unbounded()
        {
            // Arrange,
            var systemUnderTest = new UnitKindConverter(new UnitKindAssociation[] { });
            var reportingPeriod = A.Dummy<ReportingPeriod>().Whose(_ => !_.HasComponentWithUnboundedGranularity());
            var unit = A.Dummy<Unit>().Whose(_ => (_.Granularity == UnitOfTimeGranularity.Unbounded) && (_.Kind != reportingPeriod.GetUnitOfTimeKind()));

            // Act
            var actual = Record.Exception(() => systemUnderTest.TryConvert(reportingPeriod, unit, out var result));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentException>();
            actual.Message.AsTest().Must().ContainString("unit is Unbounded");
        }

        [Fact]
        public static void TryConvert___Should_throw_ArgumentException___When_parameter_unit_has_same_UnitOfTimeKind_as_parameter_reportingPeriod()
        {
            // Arrange,
            var systemUnderTest = new UnitKindConverter(new UnitKindAssociation[] { });
            var reportingPeriod = A.Dummy<ReportingPeriod>().Whose(_ => !_.HasComponentWithUnboundedGranularity());
            var unit = A.Dummy<Unit>().Whose(_ => (_.Granularity != UnitOfTimeGranularity.Unbounded) && (_.Kind == reportingPeriod.GetUnitOfTimeKind()));

            // Act
            var actual = Record.Exception(() => systemUnderTest.TryConvert(reportingPeriod, unit, out var result));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentException>();
            actual.Message.AsTest().Must().ContainString("unit has the same UnitOfTimeKind as the specified reportingPeriod");
        }

        [Fact]
        public static void TryConvert___Should_return_false_and_set_value_to_null___When_cannot_convert_because_reporting_period_never_associated()
        {
            // Arrange, Act
            var systemUnderTest = new UnitKindConverter(
                new[]
                {
                    new UnitKindAssociation(
                        QuarterNumber.Q1.ToFiscal(2020).ToReportingPeriod(),
                        QuarterNumber.Q1.ToCalendar(2020).ToReportingPeriod()),
                });

            var reportingPeriod = QuarterNumber.Q2.ToFiscal(2020).ToReportingPeriod();

            var unit = A.Dummy<Unit>().Whose(_ => (_.Granularity != UnitOfTimeGranularity.Unbounded) && (_.Kind != reportingPeriod.GetUnitOfTimeKind()));

            // Act
            var actual = systemUnderTest.TryConvert(reportingPeriod, unit, out var value);

            // Assert
            actual.AsTest().Must().BeFalse();
            value.AsTest().Must().BeNull();
        }

        [Fact]
        public static void TryConvert___Should_return_false_and_set_value_to_null___When_cannot_convert_because_unit_never_associated()
        {
            // Arrange, Act
            var systemUnderTest = new UnitKindConverter(
                new[]
                {
                    new UnitKindAssociation(
                        QuarterNumber.Q1.ToFiscal(2020).ToReportingPeriod(),
                        QuarterNumber.Q1.ToCalendar(2020).ToReportingPeriod()),
                });

            var reportingPeriod = QuarterNumber.Q1.ToFiscal(2020).ToReportingPeriod();

            var unit = new Unit(UnitOfTimeKind.Generic, UnitOfTimeGranularity.Quarter);

            // Act
            var actual = systemUnderTest.TryConvert(reportingPeriod, unit, out var value);

            // Assert
            actual.AsTest().Must().BeFalse();
            value.AsTest().Must().BeNull();
        }

        [Fact]
        public static void TryConvert___Should_return_false_and_set_value_to_null___When_cannot_convert_because_granularity_never_associated()
        {
            // Arrange, Act
            var systemUnderTest = new UnitKindConverter(
                new[]
                {
                    new UnitKindAssociation(
                        QuarterNumber.Q1.ToFiscal(2020).ToReportingPeriod(),
                        QuarterNumber.Q1.ToCalendar(2020).ToReportingPeriod()),
                });

            var reportingPeriod = QuarterNumber.Q1.ToFiscal(2020).ToReportingPeriod();

            var unit = new Unit(UnitOfTimeKind.Calendar, UnitOfTimeGranularity.Year);

            // Act
            var actual = systemUnderTest.TryConvert(reportingPeriod, unit, out var value);

            // Assert
            actual.AsTest().Must().BeFalse();
            value.AsTest().Must().BeNull();
        }

        [Fact]
        public static void TryConvert___Should_return_true_and_set_value_to_null___When_can_convert()
        {
            // Arrange, Act
            var systemUnderTest = new UnitKindConverter(
                new[]
                {
                    new UnitKindAssociation(
                        QuarterNumber.Q1.ToFiscal(2020).ToReportingPeriod(),
                        QuarterNumber.Q2.ToCalendar(2020).ToReportingPeriod()),
                    new UnitKindAssociation(
                        new ReportingPeriod(
                            new FiscalMonth(2020, MonthNumber.Four),
                            new FiscalMonth(2020, MonthNumber.Six)),
                        new ReportingPeriod(
                            new CalendarDay(2020, MonthOfYear.July, DayOfMonth.One),
                            new CalendarDay(2020, MonthOfYear.September, DayOfMonth.Thirty))),
                });

            var parametersAndExpected = new[]
            {
                new
                {
                    ReportingPeriod = QuarterNumber.Q1.ToFiscal(2020).ToReportingPeriod(),
                    Unit = new Unit(UnitOfTimeKind.Calendar, UnitOfTimeGranularity.Quarter),
                    ExpectedValue = QuarterNumber.Q2.ToCalendar(2020).ToReportingPeriod(),
                },
                new
                {
                    ReportingPeriod = QuarterNumber.Q1.ToFiscal(2020).ToReportingPeriod(),
                    Unit = new Unit(UnitOfTimeKind.Calendar, UnitOfTimeGranularity.Month),
                    ExpectedValue = new ReportingPeriod(
                        new CalendarMonth(2020, MonthOfYear.April),
                        new CalendarMonth(2020, MonthOfYear.June)),
                },
                new
                {
                    ReportingPeriod = QuarterNumber.Q1.ToFiscal(2020).ToReportingPeriod(),
                    Unit = new Unit(UnitOfTimeKind.Calendar, UnitOfTimeGranularity.Day),
                    ExpectedValue = new ReportingPeriod(
                        new CalendarDay(2020, MonthOfYear.April, DayOfMonth.One),
                        new CalendarDay(2020, MonthOfYear.June, DayOfMonth.Thirty)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalMonth(2020, MonthNumber.One),
                        new FiscalMonth(2020, MonthNumber.Three)),
                    Unit = new Unit(UnitOfTimeKind.Calendar, UnitOfTimeGranularity.Quarter),
                    ExpectedValue = QuarterNumber.Q2.ToCalendar(2020).ToReportingPeriod(),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalMonth(2020, MonthNumber.One),
                        new FiscalMonth(2020, MonthNumber.Three)),
                    Unit = new Unit(UnitOfTimeKind.Calendar, UnitOfTimeGranularity.Month),
                    ExpectedValue = new ReportingPeriod(
                        new CalendarMonth(2020, MonthOfYear.April),
                        new CalendarMonth(2020, MonthOfYear.June)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalMonth(2020, MonthNumber.One),
                        new FiscalMonth(2020, MonthNumber.Three)),
                    Unit = new Unit(UnitOfTimeKind.Calendar, UnitOfTimeGranularity.Day),
                    ExpectedValue = new ReportingPeriod(
                        new CalendarDay(2020, MonthOfYear.April, DayOfMonth.One),
                        new CalendarDay(2020, MonthOfYear.June, DayOfMonth.Thirty)),
                },
                new
                {
                    ReportingPeriod = QuarterNumber.Q2.ToCalendar(2020).ToReportingPeriod(),
                    Unit = new Unit(UnitOfTimeKind.Fiscal, UnitOfTimeGranularity.Quarter),
                    ExpectedValue = QuarterNumber.Q1.ToFiscal(2020).ToReportingPeriod(),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarMonth(2020, MonthOfYear.April),
                        new CalendarMonth(2020, MonthOfYear.June)),
                    Unit = new Unit(UnitOfTimeKind.Fiscal, UnitOfTimeGranularity.Quarter),
                    ExpectedValue = QuarterNumber.Q1.ToFiscal(2020).ToReportingPeriod(),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarDay(2020, MonthOfYear.April, DayOfMonth.One),
                        new CalendarDay(2020, MonthOfYear.June, DayOfMonth.Thirty)),
                    Unit = new Unit(UnitOfTimeKind.Fiscal, UnitOfTimeGranularity.Quarter),
                    ExpectedValue = QuarterNumber.Q1.ToFiscal(2020).ToReportingPeriod(),
                },
                new
                {
                    ReportingPeriod = QuarterNumber.Q2.ToCalendar(2020).ToReportingPeriod(),
                    Unit = new Unit(UnitOfTimeKind.Fiscal, UnitOfTimeGranularity.Month),
                    ExpectedValue = new ReportingPeriod(
                        new FiscalMonth(2020, MonthNumber.One),
                        new FiscalMonth(2020, MonthNumber.Three)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarMonth(2020, MonthOfYear.April),
                        new CalendarMonth(2020, MonthOfYear.June)),
                    Unit = new Unit(UnitOfTimeKind.Fiscal, UnitOfTimeGranularity.Month),
                    ExpectedValue = new ReportingPeriod(
                        new FiscalMonth(2020, MonthNumber.One),
                        new FiscalMonth(2020, MonthNumber.Three)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarDay(2020, MonthOfYear.April, DayOfMonth.One),
                        new CalendarDay(2020, MonthOfYear.June, DayOfMonth.Thirty)),
                    Unit = new Unit(UnitOfTimeKind.Fiscal, UnitOfTimeGranularity.Month),
                    ExpectedValue = new ReportingPeriod(
                        new FiscalMonth(2020, MonthNumber.One),
                        new FiscalMonth(2020, MonthNumber.Three)),
                },
                new
                {
                    ReportingPeriod = QuarterNumber.Q2.ToFiscal(2020).ToReportingPeriod(),
                    Unit = new Unit(UnitOfTimeKind.Calendar, UnitOfTimeGranularity.Quarter),
                    ExpectedValue = QuarterNumber.Q3.ToCalendar(2020).ToReportingPeriod(),
                },
                new
                {
                    ReportingPeriod = QuarterNumber.Q2.ToFiscal(2020).ToReportingPeriod(),
                    Unit = new Unit(UnitOfTimeKind.Calendar, UnitOfTimeGranularity.Month),
                    ExpectedValue = new ReportingPeriod(
                        new CalendarMonth(2020, MonthOfYear.July),
                        new CalendarMonth(2020, MonthOfYear.September)),
                },
                new
                {
                    ReportingPeriod = QuarterNumber.Q2.ToFiscal(2020).ToReportingPeriod(),
                    Unit = new Unit(UnitOfTimeKind.Calendar, UnitOfTimeGranularity.Day),
                    ExpectedValue = new ReportingPeriod(
                        new CalendarDay(2020, MonthOfYear.July, DayOfMonth.One),
                        new CalendarDay(2020, MonthOfYear.September, DayOfMonth.Thirty)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalMonth(2020, MonthNumber.Four),
                        new FiscalMonth(2020, MonthNumber.Six)),
                    Unit = new Unit(UnitOfTimeKind.Calendar, UnitOfTimeGranularity.Quarter),
                    ExpectedValue = QuarterNumber.Q3.ToCalendar(2020).ToReportingPeriod(),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalMonth(2020, MonthNumber.Four),
                        new FiscalMonth(2020, MonthNumber.Six)),
                    Unit = new Unit(UnitOfTimeKind.Calendar, UnitOfTimeGranularity.Month),
                    ExpectedValue = new ReportingPeriod(
                        new CalendarMonth(2020, MonthOfYear.July),
                        new CalendarMonth(2020, MonthOfYear.September)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalMonth(2020, MonthNumber.Four),
                        new FiscalMonth(2020, MonthNumber.Six)),
                    Unit = new Unit(UnitOfTimeKind.Calendar, UnitOfTimeGranularity.Day),
                    ExpectedValue = new ReportingPeriod(
                        new CalendarDay(2020, MonthOfYear.July, DayOfMonth.One),
                        new CalendarDay(2020, MonthOfYear.September, DayOfMonth.Thirty)),
                },
                new
                {
                    ReportingPeriod = QuarterNumber.Q3.ToCalendar(2020).ToReportingPeriod(),
                    Unit = new Unit(UnitOfTimeKind.Fiscal, UnitOfTimeGranularity.Quarter),
                    ExpectedValue = QuarterNumber.Q2.ToFiscal(2020).ToReportingPeriod(),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarMonth(2020, MonthOfYear.July),
                        new CalendarMonth(2020, MonthOfYear.September)),
                    Unit = new Unit(UnitOfTimeKind.Fiscal, UnitOfTimeGranularity.Quarter),
                    ExpectedValue = QuarterNumber.Q2.ToFiscal(2020).ToReportingPeriod(),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarDay(2020, MonthOfYear.July, DayOfMonth.One),
                        new CalendarDay(2020, MonthOfYear.September, DayOfMonth.Thirty)),
                    Unit = new Unit(UnitOfTimeKind.Fiscal, UnitOfTimeGranularity.Quarter),
                    ExpectedValue = QuarterNumber.Q2.ToFiscal(2020).ToReportingPeriod(),
                },
                new
                {
                    ReportingPeriod = QuarterNumber.Q3.ToCalendar(2020).ToReportingPeriod(),
                    Unit = new Unit(UnitOfTimeKind.Fiscal, UnitOfTimeGranularity.Month),
                    ExpectedValue = new ReportingPeriod(
                        new FiscalMonth(2020, MonthNumber.Four),
                        new FiscalMonth(2020, MonthNumber.Six)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarMonth(2020, MonthOfYear.July),
                        new CalendarMonth(2020, MonthOfYear.September)),
                    Unit = new Unit(UnitOfTimeKind.Fiscal, UnitOfTimeGranularity.Month),
                    ExpectedValue = new ReportingPeriod(
                        new FiscalMonth(2020, MonthNumber.Four),
                        new FiscalMonth(2020, MonthNumber.Six)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarDay(2020, MonthOfYear.July, DayOfMonth.One),
                        new CalendarDay(2020, MonthOfYear.September, DayOfMonth.Thirty)),
                    Unit = new Unit(UnitOfTimeKind.Fiscal, UnitOfTimeGranularity.Month),
                    ExpectedValue = new ReportingPeriod(
                        new FiscalMonth(2020, MonthNumber.Four),
                        new FiscalMonth(2020, MonthNumber.Six)),
                },
            };

            // Act
            var result = parametersAndExpected.Select(_ =>
            {
                var actual = systemUnderTest.TryConvert(_.ReportingPeriod, _.Unit, out var value);
                return new { Actual = actual, ActualValue = value, ExpectedValue = _.ExpectedValue };
            });

            // Assert
            result.Select(_ => _.Actual).AsTest().Must().Each().BeTrue();
            result.Select(_ => _.ActualValue.Equals(_.ExpectedValue)).Must().Each().BeTrue();
        }
    }
}
