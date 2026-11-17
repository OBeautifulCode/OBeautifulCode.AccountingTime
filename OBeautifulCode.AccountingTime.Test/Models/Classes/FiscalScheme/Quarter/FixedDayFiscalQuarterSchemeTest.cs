// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FixedDayFiscalQuarterSchemeTest.cs" company="OBeautifulCode">
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
    using OBeautifulCode.AutoFakeItEasy;
    using OBeautifulCode.CodeAnalysis.Recipes;
    using OBeautifulCode.CodeGen.ModelObject.Recipes;
    using OBeautifulCode.Enum.Recipes;
    using OBeautifulCode.Math.Recipes;

    using Xunit;

    using static System.FormattableString;

    [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
    public static partial class FixedDayFiscalQuarterSchemeTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static FixedDayFiscalQuarterSchemeTest()
        {
            ConstructorArgumentValidationTestScenarios
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<FixedDayFiscalQuarterScheme>
                    {
                        Name = "Constructor should throw ArgumentException when parameter 'fiscalYearScheme' ends on May 30th, because February 30th does not exist",
                        ConstructionFunc = () => new FixedDayFiscalQuarterScheme(new FixedDayFiscalYearScheme(MonthOfYear.May, DayOfMonth.Thirty)),
                        ExpectedExceptionMessageContains = new[] { "fiscalYearScheme", "LastDayInFiscalYear", "Thirty", "does not occur every year", "February" },
                        ExpectedExceptionType = typeof(ArgumentException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<FixedDayFiscalQuarterScheme>
                    {
                        Name = "Constructor should throw ArgumentException when parameter 'fiscalYearScheme' ends on August 30th, because February 30th does not exist",
                        ConstructionFunc = () => new FixedDayFiscalQuarterScheme(new FixedDayFiscalYearScheme(MonthOfYear.August, DayOfMonth.Thirty)),
                        ExpectedExceptionMessageContains = new[] { "fiscalYearScheme", "LastDayInFiscalYear", "Thirty", "does not occur every year", "February" },
                        ExpectedExceptionType = typeof(ArgumentException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<FixedDayFiscalQuarterScheme>
                    {
                        Name = "Constructor should throw ArgumentException when parameter 'fiscalYearScheme' ends on August 29th, because February 29th does not occur every year",
                        ConstructionFunc = () => new FixedDayFiscalQuarterScheme(new FixedDayFiscalYearScheme(MonthOfYear.August, DayOfMonth.TwentyNine)),
                        ExpectedExceptionMessageContains = new[] { "fiscalYearScheme", "LastDayInFiscalYear", "TwentyNine", "does not occur every year", "February" },
                        ExpectedExceptionType = typeof(ArgumentException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<FixedDayFiscalQuarterScheme>
                    {
                        Name = "Constructor should throw ArgumentException when parameter 'fiscalYearScheme' ends on November 29th, because February 29th does not occur every year",
                        ConstructionFunc = () => new FixedDayFiscalQuarterScheme(new FixedDayFiscalYearScheme(MonthOfYear.November, DayOfMonth.TwentyNine, FixedDayFiscalYearNumberingConvention.CalendarYearInWhichFiscalYearBegins)),
                        ExpectedExceptionMessageContains = new[] { "fiscalYearScheme", "LastDayInFiscalYear", "TwentyNine", "does not occur every year", "February" },
                        ExpectedExceptionType = typeof(ArgumentException),
                    });
        }

        [Fact]
        public static void Constructor___Should_throw_ArgumentException___When_fiscalYearScheme_LastDayInFiscalYear_does_not_occur_every_year_in_some_month_in_which_a_fiscal_quarter_ends()
        {
            // Arrange
            var fiscalYearSchemes = GetFiscalYearSchemes()
                .Where(_ => !LastDayInFiscalYearOccursEveryYearInAllQuarterEndMonths(_))
                .ToList();

            // Act
            var actual = fiscalYearSchemes.Select(_ => Record.Exception(() => new FixedDayFiscalQuarterScheme(_))).ToList();

            // Assert
            // February is a quarter-end month when the fiscal year ends in May, August, or November.
            // The invalid days are May 29th and 30th, August 29th and 30th, and November 29th
            // (November 30th is rejected by the fiscal year scheme), each with 2 numbering conventions.
            fiscalYearSchemes.Should().HaveCount(5 * 2);
            actual.Should().OnlyContain(_ => (_ != null) && (_.GetType() == typeof(ArgumentException)));
        }

        [Fact]
        public static void Constructor___Should_not_throw___When_fiscalYearScheme_LastDayInFiscalYear_occurs_every_year_in_all_months_in_which_a_fiscal_quarter_ends()
        {
            // Arrange
            var fiscalYearSchemes = GetFiscalYearSchemes()
                .Where(LastDayInFiscalYearOccursEveryYearInAllQuarterEndMonths)
                .ToList();

            // Act
            var actual = fiscalYearSchemes.Select(_ => Record.Exception(() => new FixedDayFiscalQuarterScheme(_))).ToList();

            // Assert
            actual.Should().OnlyContain(_ => _ == null);
        }

        [Fact]
        public static void GetCalendarDayPeriod___Should_throw_ArgumentNullException___When_parameter_fiscalQuarter_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<FixedDayFiscalQuarterScheme>();

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCalendarDayPeriod(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("fiscalQuarter");
        }

        [Fact]
        public static void GetCalendarDayPeriod___Should_return_quarters_that_end_on_the_fixed_day_every_three_months___When_FiscalYearNumberingConvention_is_CalendarYearInWhichFiscalYearEnds()
        {
            // Arrange
            var april25 = new FixedDayFiscalQuarterScheme(new FixedDayFiscalYearScheme(MonthOfYear.April, DayOfMonth.TwentyFive));
            var february28 = new FixedDayFiscalQuarterScheme(new FixedDayFiscalYearScheme(MonthOfYear.February, DayOfMonth.TwentyEight));
            var november28 = new FixedDayFiscalQuarterScheme(new FixedDayFiscalYearScheme(MonthOfYear.November, DayOfMonth.TwentyEight));
            var january30 = new FixedDayFiscalQuarterScheme(new FixedDayFiscalYearScheme(MonthOfYear.January, DayOfMonth.Thirty));
            var december30 = new FixedDayFiscalQuarterScheme(new FixedDayFiscalYearScheme(MonthOfYear.December, DayOfMonth.Thirty));

            var systemUnderTestAndExpected = new[]
            {
                new { SystemUnderTest = april25, FiscalQuarter = new FiscalQuarter(2026, QuarterNumber.Q1), Expected = new ReportingPeriod(26.April(2025).ToCalendarDay(), 25.July(2025).ToCalendarDay()) },
                new { SystemUnderTest = april25, FiscalQuarter = new FiscalQuarter(2026, QuarterNumber.Q2), Expected = new ReportingPeriod(26.July(2025).ToCalendarDay(), 25.October(2025).ToCalendarDay()) },
                new { SystemUnderTest = april25, FiscalQuarter = new FiscalQuarter(2026, QuarterNumber.Q3), Expected = new ReportingPeriod(26.October(2025).ToCalendarDay(), 25.January(2026).ToCalendarDay()) },
                new { SystemUnderTest = april25, FiscalQuarter = new FiscalQuarter(2026, QuarterNumber.Q4), Expected = new ReportingPeriod(26.January(2026).ToCalendarDay(), 25.April(2026).ToCalendarDay()) },

                // February 29th is the first day of FY 2025
                new { SystemUnderTest = february28, FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q1), Expected = new ReportingPeriod(29.February(2024).ToCalendarDay(), 28.May(2024).ToCalendarDay()) },
                new { SystemUnderTest = february28, FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q2), Expected = new ReportingPeriod(29.May(2024).ToCalendarDay(), 28.August(2024).ToCalendarDay()) },
                new { SystemUnderTest = february28, FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q3), Expected = new ReportingPeriod(29.August(2024).ToCalendarDay(), 28.November(2024).ToCalendarDay()) },
                new { SystemUnderTest = february28, FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q4), Expected = new ReportingPeriod(29.November(2024).ToCalendarDay(), 28.February(2025).ToCalendarDay()) },

                // February 29th is the first day of Q2 of FY 2024
                new { SystemUnderTest = november28, FiscalQuarter = new FiscalQuarter(2024, QuarterNumber.Q1), Expected = new ReportingPeriod(29.November(2023).ToCalendarDay(), 28.February(2024).ToCalendarDay()) },
                new { SystemUnderTest = november28, FiscalQuarter = new FiscalQuarter(2024, QuarterNumber.Q2), Expected = new ReportingPeriod(29.February(2024).ToCalendarDay(), 28.May(2024).ToCalendarDay()) },
                new { SystemUnderTest = november28, FiscalQuarter = new FiscalQuarter(2024, QuarterNumber.Q3), Expected = new ReportingPeriod(29.May(2024).ToCalendarDay(), 28.August(2024).ToCalendarDay()) },
                new { SystemUnderTest = november28, FiscalQuarter = new FiscalQuarter(2024, QuarterNumber.Q4), Expected = new ReportingPeriod(29.August(2024).ToCalendarDay(), 28.November(2024).ToCalendarDay()) },

                // April 30th is the last day of Q1, so Q2 begins on May 1st
                new { SystemUnderTest = january30, FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q1), Expected = new ReportingPeriod(31.January(2024).ToCalendarDay(), 30.April(2024).ToCalendarDay()) },
                new { SystemUnderTest = january30, FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q2), Expected = new ReportingPeriod(1.May(2024).ToCalendarDay(), 30.July(2024).ToCalendarDay()) },
                new { SystemUnderTest = january30, FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q3), Expected = new ReportingPeriod(31.July(2024).ToCalendarDay(), 30.October(2024).ToCalendarDay()) },
                new { SystemUnderTest = january30, FiscalQuarter = new FiscalQuarter(2025, QuarterNumber.Q4), Expected = new ReportingPeriod(31.October(2024).ToCalendarDay(), 30.January(2025).ToCalendarDay()) },

                new { SystemUnderTest = december30, FiscalQuarter = new FiscalQuarter(2026, QuarterNumber.Q1), Expected = new ReportingPeriod(31.December(2025).ToCalendarDay(), 30.March(2026).ToCalendarDay()) },
                new { SystemUnderTest = december30, FiscalQuarter = new FiscalQuarter(2026, QuarterNumber.Q2), Expected = new ReportingPeriod(31.March(2026).ToCalendarDay(), 30.June(2026).ToCalendarDay()) },
                new { SystemUnderTest = december30, FiscalQuarter = new FiscalQuarter(2026, QuarterNumber.Q3), Expected = new ReportingPeriod(1.July(2026).ToCalendarDay(), 30.September(2026).ToCalendarDay()) },
                new { SystemUnderTest = december30, FiscalQuarter = new FiscalQuarter(2026, QuarterNumber.Q4), Expected = new ReportingPeriod(1.October(2026).ToCalendarDay(), 30.December(2026).ToCalendarDay()) },
            };

            var expected = systemUnderTestAndExpected.Select(_ => _.Expected).ToList();

            // Act
            var actual = systemUnderTestAndExpected.Select(_ => _.SystemUnderTest.GetCalendarDayPeriod(_.FiscalQuarter)).ToList();

            // Assert
            actual.Should().Equal(expected);
        }

        [Fact]
        public static void GetCalendarDayPeriod___Should_return_quarters_that_end_on_the_fixed_day_every_three_months___When_FiscalYearNumberingConvention_is_CalendarYearInWhichFiscalYearBegins()
        {
            // Arrange
            var january25 = new FixedDayFiscalQuarterScheme(new FixedDayFiscalYearScheme(MonthOfYear.January, DayOfMonth.TwentyFive, FixedDayFiscalYearNumberingConvention.CalendarYearInWhichFiscalYearBegins));

            var systemUnderTestAndExpected = new[]
            {
                new { SystemUnderTest = january25, FiscalQuarter = new FiscalQuarter(2026, QuarterNumber.Q1), Expected = new ReportingPeriod(26.January(2026).ToCalendarDay(), 25.April(2026).ToCalendarDay()) },
                new { SystemUnderTest = january25, FiscalQuarter = new FiscalQuarter(2026, QuarterNumber.Q2), Expected = new ReportingPeriod(26.April(2026).ToCalendarDay(), 25.July(2026).ToCalendarDay()) },
                new { SystemUnderTest = january25, FiscalQuarter = new FiscalQuarter(2026, QuarterNumber.Q3), Expected = new ReportingPeriod(26.July(2026).ToCalendarDay(), 25.October(2026).ToCalendarDay()) },
                new { SystemUnderTest = january25, FiscalQuarter = new FiscalQuarter(2026, QuarterNumber.Q4), Expected = new ReportingPeriod(26.October(2026).ToCalendarDay(), 25.January(2027).ToCalendarDay()) },
            };

            var expected = systemUnderTestAndExpected.Select(_ => _.Expected).ToList();

            // Act
            var actual = systemUnderTestAndExpected.Select(_ => _.SystemUnderTest.GetCalendarDayPeriod(_.FiscalQuarter)).ToList();

            // Assert
            actual.Should().Equal(expected);
        }

        [Fact]
        public static void GetCalendarDayPeriod___Should_return_contiguous_quarters_that_span_the_fiscal_year_and_each_end_on_the_fixed_day___When_called()
        {
            // Arrange
            var systemsUnderTest = GetFiscalYearSchemes()
                .Where(LastDayInFiscalYearOccursEveryYearInAllQuarterEndMonths)
                .Select(_ => new FixedDayFiscalQuarterScheme(_))
                .ToList();

            // spans multiple leap years
            var years = Enumerable.Range(2019, 11).ToList();

            var quarterNumbers = new[] { QuarterNumber.Q1, QuarterNumber.Q2, QuarterNumber.Q3, QuarterNumber.Q4 };

            foreach (var systemUnderTest in systemsUnderTest)
            {
                foreach (var year in years)
                {
                    // Act
                    var actual = quarterNumbers.Select(_ => systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(year, _))).ToList();

                    // Assert
                    var fiscalYearReportingPeriod = systemUnderTest.FiscalYearScheme.GetCalendarDayPeriod(new FiscalYear(year));

                    actual.First().Start.Should().Be(fiscalYearReportingPeriod.Start);
                    actual.Last().End.Should().Be(fiscalYearReportingPeriod.End);

                    foreach (var reportingPeriod in actual)
                    {
                        var start = ((CalendarDay)reportingPeriod.Start).ToDateTime();
                        var end = ((CalendarDay)reportingPeriod.End).ToDateTime();

                        end.Day.Should().Be((int)systemUnderTest.FiscalYearScheme.LastDayInFiscalYear);
                        ((end - start).Days + 1).Should().BeInRange(89, 92);
                    }

                    for (var x = 1; x < actual.Count; x++)
                    {
                        ((CalendarDay)actual[x].Start).ToDateTime().Should().Be(((CalendarDay)actual[x - 1].End).ToDateTime().AddDays(1));
                    }
                }
            }
        }

        private static IReadOnlyList<FixedDayFiscalYearScheme> GetFiscalYearSchemes()
        {
            var months = EnumExtensions.GetDefinedEnumValues<MonthOfYear>().Where(_ => _ != MonthOfYear.Invalid).ToList();

            var days = EnumExtensions.GetDefinedEnumValues<DayOfMonth>().Where(_ => _ != DayOfMonth.Invalid).ToList();

            var fiscalYearNumberingConventions = EnumExtensions.GetDefinedEnumValues<FixedDayFiscalYearNumberingConvention>().Where(_ => _ != FixedDayFiscalYearNumberingConvention.Unknown).ToList();

            // The day must occur in the month every year and must not always be the last day of the month.
            // The number of days in the month in a leap year, minus one, satisfies both constraints
            // (it's 28 for February and one less than the number of days in the month otherwise).
            var result = months
                .SelectMany(month => days.Where(day => (int)day < DateTime.DaysInMonth(2000, (int)month)).Select(day => new { Month = month, Day = day }))
                .SelectMany(_ => fiscalYearNumberingConventions.Select(convention => new FixedDayFiscalYearScheme(_.Month, _.Day, convention)))
                .ToList();

            return result;
        }

        private static bool LastDayInFiscalYearOccursEveryYearInAllQuarterEndMonths(
            FixedDayFiscalYearScheme fiscalYearScheme)
        {
            // 2001 is not a leap year, so February has 28 days
            var result = Enumerable.Range(0, 4)
                .Select(_ => ((((int)fiscalYearScheme.LastMonthInFiscalYear - 1) + (3 * _)) % 12) + 1)
                .All(month => (int)fiscalYearScheme.LastDayInFiscalYear <= DateTime.DaysInMonth(2001, month));

            return result;
        }
    }
}
