// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MonthEndFiscalQuarterSchemeTest.cs" company="OBeautifulCode">
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
    using FluentAssertions.Extensions;
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.AutoFakeItEasy;
    using OBeautifulCode.CodeAnalysis.Recipes;
    using OBeautifulCode.CodeGen.ModelObject.Recipes;
    using OBeautifulCode.Enum.Recipes;
    using OBeautifulCode.Math.Recipes;

    using Xunit;

    using static System.FormattableString;

    [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
    public static partial class MonthEndFiscalQuarterSchemeTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static MonthEndFiscalQuarterSchemeTest()
        {
        }

        [Fact]
        public static void GetCalendarDayPeriod___Should_throw_ArgumentNullException___When_parameter_fiscalQuarter_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<MonthEndFiscalQuarterScheme>();

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCalendarDayPeriod(null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("fiscalQuarter");
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "YearEnds", Justification = ObcSuppressBecause.CA1702_CompoundWordsShouldBeCasedCorrectly_AnalyzerIsIncorrectlyDetectingCompoundWords)]
        public static void GetCalendarDayPeriod___Should_return_calendar_quarters___When_FiscalYearNumberingConvention_is_CalendarYearInWhichFiscalYearEnds()
        {
            // Arrange
            var systemUnderTestAndExpected = new[]
            {
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.January)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q1),
                    Expected = new ReportingPeriod(
                        1.February(2024).ToCalendarDay(),
                        30.April(2024).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.January)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q2),
                    Expected = new ReportingPeriod(
                        1.May(2024).ToCalendarDay(),
                        31.July(2024).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.January)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q3),
                    Expected = new ReportingPeriod(
                        1.August(2024).ToCalendarDay(),
                        31.October(2024).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.January)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q4),
                    Expected = new ReportingPeriod(
                        1.November(2024).ToCalendarDay(),
                        31.January(2025).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.February)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q1),
                    Expected = new ReportingPeriod(
                        1.March(2024).ToCalendarDay(),
                        31.May(2024).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.February)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q2),
                    Expected = new ReportingPeriod(
                        1.June(2024).ToCalendarDay(),
                        31.August(2024).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.February)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q3),
                    Expected = new ReportingPeriod(
                        1.September(2024).ToCalendarDay(),
                        30.November(2024).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.February)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q4),
                    Expected = new ReportingPeriod(
                        1.December(2024).ToCalendarDay(),
                        28.February(2025).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.February)),
                    FiscalQuarter = new FiscalQuarter(2024, QuarterNumber.Q4),
                    Expected = new ReportingPeriod(
                        1.December(2023).ToCalendarDay(),
                        29.February(2024).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.March)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q1),
                    Expected = new ReportingPeriod(
                        1.April(2024).ToCalendarDay(),
                        30.June(2024).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.March)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q2),
                    Expected = new ReportingPeriod(
                        1.July(2024).ToCalendarDay(),
                        30.September(2024).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.March)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q3),
                    Expected = new ReportingPeriod(
                        1.October(2024).ToCalendarDay(),
                        31.December(2024).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.March)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q4),
                    Expected = new ReportingPeriod(
                        1.January(2025).ToCalendarDay(),
                        31.March(2025).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.April)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q1),
                    Expected = new ReportingPeriod(
                        1.May(2024).ToCalendarDay(),
                        31.July(2024).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.April)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q2),
                    Expected = new ReportingPeriod(
                        1.August(2024).ToCalendarDay(),
                        31.October(2024).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.April)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q3),
                    Expected = new ReportingPeriod(
                        1.November(2024).ToCalendarDay(),
                        31.January(2025).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.April)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q4),
                    Expected = new ReportingPeriod(
                        1.February(2025).ToCalendarDay(),
                        30.April(2025).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.May)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q1),
                    Expected = new ReportingPeriod(
                        1.June(2024).ToCalendarDay(),
                        31.August(2024).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.May)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q2),
                    Expected = new ReportingPeriod(
                        1.September(2024).ToCalendarDay(),
                        30.November(2024).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.May)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q3),
                    Expected = new ReportingPeriod(
                        1.December(2024).ToCalendarDay(),
                        28.February(2025).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.May)),
                    FiscalQuarter = new FiscalQuarter(2024, QuarterNumber.Q3),
                    Expected = new ReportingPeriod(
                        1.December(2023).ToCalendarDay(),
                        29.February(2024).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.May)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q4),
                    Expected = new ReportingPeriod(
                        1.March(2025).ToCalendarDay(),
                        31.May(2025).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.June)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q1),
                    Expected = new ReportingPeriod(
                        1.July(2024).ToCalendarDay(),
                        30.September(2024).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.June)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q2),
                    Expected = new ReportingPeriod(
                        1.October(2024).ToCalendarDay(),
                        31.December(2024).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.June)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q3),
                    Expected = new ReportingPeriod(
                        1.January(2025).ToCalendarDay(),
                        31.March(2025).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.June)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q4),
                    Expected = new ReportingPeriod(
                        1.April(2025).ToCalendarDay(),
                        30.June(2025).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.July)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q1),
                    Expected = new ReportingPeriod(
                        1.August(2024).ToCalendarDay(),
                        31.October(2024).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.July)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q2),
                    Expected = new ReportingPeriod(
                        1.November(2024).ToCalendarDay(),
                        31.January(2025).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.July)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q3),
                    Expected = new ReportingPeriod(
                        1.February(2025).ToCalendarDay(),
                        30.April(2025).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.July)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q4),
                    Expected = new ReportingPeriod(
                        1.May(2025).ToCalendarDay(),
                        31.July(2025).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.August)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q1),
                    Expected = new ReportingPeriod(
                        1.September(2024).ToCalendarDay(),
                        30.November(2024).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.August)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q2),
                    Expected = new ReportingPeriod(
                        1.December(2024).ToCalendarDay(),
                        28.February(2025).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.August)),
                    FiscalQuarter = new FiscalQuarter(2024, QuarterNumber.Q2),
                    Expected = new ReportingPeriod(
                        1.December(2023).ToCalendarDay(),
                        29.February(2024).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.August)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q3),
                    Expected = new ReportingPeriod(
                        1.March(2025).ToCalendarDay(),
                        31.May(2025).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.August)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q4),
                    Expected = new ReportingPeriod(
                        1.June(2025).ToCalendarDay(),
                        31.August(2025).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.September)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q1),
                    Expected = new ReportingPeriod(
                        1.October(2024).ToCalendarDay(),
                        31.December(2024).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.September)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q2),
                    Expected = new ReportingPeriod(
                        1.January(2025).ToCalendarDay(),
                        31.March(2025).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.September)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q3),
                    Expected = new ReportingPeriod(
                        1.April(2025).ToCalendarDay(),
                        30.June(2025).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.September)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q4),
                    Expected = new ReportingPeriod(
                        1.July(2025).ToCalendarDay(),
                        30.September(2025).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.October)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q1),
                    Expected = new ReportingPeriod(
                        1.November(2024).ToCalendarDay(),
                        31.January(2025).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.October)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q2),
                    Expected = new ReportingPeriod(
                        1.February(2025).ToCalendarDay(),
                        30.April(2025).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.October)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q3),
                    Expected = new ReportingPeriod(
                        1.May(2025).ToCalendarDay(),
                        31.July(2025).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.October)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q4),
                    Expected = new ReportingPeriod(
                        1.August(2025).ToCalendarDay(),
                        31.October(2025).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.November)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q1),
                    Expected = new ReportingPeriod(
                        1.December(2024).ToCalendarDay(),
                        28.February(2025).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.November)),
                    FiscalQuarter = new FiscalQuarter(2024, QuarterNumber.Q1),
                    Expected = new ReportingPeriod(
                        1.December(2023).ToCalendarDay(),
                        29.February(2024).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.November)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q2),
                    Expected = new ReportingPeriod(
                        1.March(2025).ToCalendarDay(),
                        31.May(2025).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.November)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q3),
                    Expected = new ReportingPeriod(
                        1.June(2025).ToCalendarDay(),
                        31.August(2025).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.November)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q4),
                    Expected = new ReportingPeriod(
                        1.September(2025).ToCalendarDay(),
                        30.November(2025).ToCalendarDay()),
                },
            };

            var expected = systemUnderTestAndExpected.Select(_ => _.Expected).ToList();

            // Act
            var actual = systemUnderTestAndExpected.Select(_ => _.SystemUnderTest.GetCalendarDayPeriod(_.FiscalQuarter)).ToList();

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void GetCalendarDayPeriod___Should_return_quarters_of_the_fiscal_year_that_begins_in_the_fiscal_quarters_year___When_FiscalYearNumberingConvention_is_CalendarYearInWhichFiscalYearBegins()
        {
            // Arrange
            var systemUnderTestAndExpected = new[]
            {
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.January, MonthEndFiscalYearNumberingConvention.CalendarYearInWhichFiscalYearBegins)),
                    FiscalQuarter = new FiscalQuarter(2026, QuarterNumber.Q1),
                    Expected = new ReportingPeriod(
                        1.February(2026).ToCalendarDay(),
                        30.April(2026).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.January, MonthEndFiscalYearNumberingConvention.CalendarYearInWhichFiscalYearBegins)),
                    FiscalQuarter = new FiscalQuarter(2026, QuarterNumber.Q2),
                    Expected = new ReportingPeriod(
                        1.May(2026).ToCalendarDay(),
                        31.July(2026).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.January, MonthEndFiscalYearNumberingConvention.CalendarYearInWhichFiscalYearBegins)),
                    FiscalQuarter = new FiscalQuarter(2026, QuarterNumber.Q3),
                    Expected = new ReportingPeriod(
                        1.August(2026).ToCalendarDay(),
                        31.October(2026).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.January, MonthEndFiscalYearNumberingConvention.CalendarYearInWhichFiscalYearBegins)),
                    FiscalQuarter = new FiscalQuarter(2026, QuarterNumber.Q4),
                    Expected = new ReportingPeriod(
                        1.November(2026).ToCalendarDay(),
                        31.January(2027).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.February, MonthEndFiscalYearNumberingConvention.CalendarYearInWhichFiscalYearBegins)),
                    FiscalQuarter = new FiscalQuarter(2023, QuarterNumber.Q4),
                    Expected = new ReportingPeriod(
                        1.December(2023).ToCalendarDay(),
                        29.February(2024).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.February, MonthEndFiscalYearNumberingConvention.CalendarYearInWhichFiscalYearBegins)),
                    FiscalQuarter = new FiscalQuarter(2024, QuarterNumber.Q4),
                    Expected = new ReportingPeriod(
                        1.December(2024).ToCalendarDay(),
                        28.February(2025).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.June, MonthEndFiscalYearNumberingConvention.CalendarYearInWhichFiscalYearBegins)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q1),
                    Expected = new ReportingPeriod(
                        1.July(2025).ToCalendarDay(),
                        30.September(2025).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.June, MonthEndFiscalYearNumberingConvention.CalendarYearInWhichFiscalYearBegins)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q3),
                    Expected = new ReportingPeriod(
                        1.January(2026).ToCalendarDay(),
                        31.March(2026).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.November, MonthEndFiscalYearNumberingConvention.CalendarYearInWhichFiscalYearBegins)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q1),
                    Expected = new ReportingPeriod(
                        1.December(2025).ToCalendarDay(),
                        28.February(2026).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new MonthEndFiscalQuarterScheme(new MonthEndFiscalYearScheme(MonthOfYear.November, MonthEndFiscalYearNumberingConvention.CalendarYearInWhichFiscalYearBegins)),
                    FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q4),
                    Expected = new ReportingPeriod(
                        1.September(2026).ToCalendarDay(),
                        30.November(2026).ToCalendarDay()),
                },
            };

            var expected = systemUnderTestAndExpected.Select(_ => _.Expected).ToList();

            // Act
            var actual = systemUnderTestAndExpected.Select(_ => _.SystemUnderTest.GetCalendarDayPeriod(_.FiscalQuarter)).ToList();

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void GetCalendarDayPeriod___Should_return_quarters_that_fall_within_the_fiscal_year___When_called()
        {
            // Arrange
            var fiscalYearSchemes = EnumExtensions.GetDefinedEnumValues<MonthOfYear>()
                .Where(_ => (_ != MonthOfYear.Invalid) && (_ != MonthOfYear.December))
                .SelectMany(_ => new[]
                {
                    new MonthEndFiscalYearScheme(_, MonthEndFiscalYearNumberingConvention.CalendarYearInWhichFiscalYearEnds),
                    new MonthEndFiscalYearScheme(_, MonthEndFiscalYearNumberingConvention.CalendarYearInWhichFiscalYearBegins),
                })
                .ToList();

            var year = A.Dummy<DateTime>().ThatIs(_ => (_.Year > 1) && (_.Year < 9999)).Year;

            var expected = fiscalYearSchemes
                .Select(_ => _.GetCalendarDayPeriod(new FiscalYear(year)))
                .ToList();

            // Act
            var actual = fiscalYearSchemes
                .Select(_ =>
                {
                    var systemUnderTest = new MonthEndFiscalQuarterScheme(_);

                    var q1 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(year, QuarterNumber.Q1));

                    var q4 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(year, QuarterNumber.Q4));

                    var result = new ReportingPeriod(q1.Start, q4.End);

                    return result;
                })
                .ToList();

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }
    }
}