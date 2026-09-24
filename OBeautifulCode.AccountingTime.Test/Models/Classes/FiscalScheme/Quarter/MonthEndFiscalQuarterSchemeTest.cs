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
        public static void GetCalendarDayPeriod___Should_return_calendar_quarters___When_called()
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
    }
}