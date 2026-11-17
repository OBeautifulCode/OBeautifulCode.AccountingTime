// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FixedDayFiscalYearSchemeTest.cs" company="OBeautifulCode">
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
    public static partial class FixedDayFiscalYearSchemeTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static FixedDayFiscalYearSchemeTest()
        {
            ConstructorArgumentValidationTestScenarios
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<FixedDayFiscalYearScheme>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'lastMonthInFiscalYear' is MonthOfYear.Invalid",
                        ConstructionFunc = () => new FixedDayFiscalYearScheme(MonthOfYear.Invalid, DayOfMonth.TwentyFive),
                        ExpectedExceptionMessageContains = new[] { "lastMonthInFiscalYear", "Invalid" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<FixedDayFiscalYearScheme>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'lastDayInFiscalYear' is DayOfMonth.Invalid",
                        ConstructionFunc = () => new FixedDayFiscalYearScheme(MonthOfYear.April, DayOfMonth.Invalid),
                        ExpectedExceptionMessageContains = new[] { "lastDayInFiscalYear", "Invalid" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<FixedDayFiscalYearScheme>
                    {
                        Name = "Constructor should throw ArgumentException when parameter 'lastDayInFiscalYear' is 29 and parameter 'lastMonthInFiscalYear' is February",
                        ConstructionFunc = () => new FixedDayFiscalYearScheme(MonthOfYear.February, DayOfMonth.TwentyNine),
                        ExpectedExceptionMessageContains = new[] { "lastDayInFiscalYear", "TwentyNine", "does not occur every year", "lastMonthInFiscalYear", "February" },
                        ExpectedExceptionType = typeof(ArgumentException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<FixedDayFiscalYearScheme>
                    {
                        Name = "Constructor should throw ArgumentException when parameter 'lastDayInFiscalYear' is 30 and parameter 'lastMonthInFiscalYear' is February",
                        ConstructionFunc = () => new FixedDayFiscalYearScheme(MonthOfYear.February, DayOfMonth.Thirty),
                        ExpectedExceptionMessageContains = new[] { "lastDayInFiscalYear", "Thirty", "does not occur every year", "lastMonthInFiscalYear", "February" },
                        ExpectedExceptionType = typeof(ArgumentException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<FixedDayFiscalYearScheme>
                    {
                        Name = "Constructor should throw ArgumentException when parameter 'lastDayInFiscalYear' is 31 and parameter 'lastMonthInFiscalYear' is April",
                        ConstructionFunc = () => new FixedDayFiscalYearScheme(MonthOfYear.April, DayOfMonth.ThirtyOne),
                        ExpectedExceptionMessageContains = new[] { "lastDayInFiscalYear", "ThirtyOne", "does not occur every year", "lastMonthInFiscalYear", "April" },
                        ExpectedExceptionType = typeof(ArgumentException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<FixedDayFiscalYearScheme>
                    {
                        Name = "Constructor should throw ArgumentException when parameter 'lastDayInFiscalYear' is 30 and parameter 'lastMonthInFiscalYear' is April",
                        ConstructionFunc = () => new FixedDayFiscalYearScheme(MonthOfYear.April, DayOfMonth.Thirty),
                        ExpectedExceptionMessageContains = new[] { "lastDayInFiscalYear", "Thirty", "always the last day", "lastMonthInFiscalYear", "April", "MonthEndFiscalYearScheme" },
                        ExpectedExceptionType = typeof(ArgumentException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<FixedDayFiscalYearScheme>
                    {
                        Name = "Constructor should throw ArgumentException when parameter 'lastDayInFiscalYear' is 31 and parameter 'lastMonthInFiscalYear' is January",
                        ConstructionFunc = () => new FixedDayFiscalYearScheme(MonthOfYear.January, DayOfMonth.ThirtyOne),
                        ExpectedExceptionMessageContains = new[] { "lastDayInFiscalYear", "ThirtyOne", "always the last day", "lastMonthInFiscalYear", "January", "MonthEndFiscalYearScheme" },
                        ExpectedExceptionType = typeof(ArgumentException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<FixedDayFiscalYearScheme>
                    {
                        Name = "Constructor should throw ArgumentException when parameter 'lastDayInFiscalYear' is 31 and parameter 'lastMonthInFiscalYear' is December",
                        ConstructionFunc = () => new FixedDayFiscalYearScheme(MonthOfYear.December, DayOfMonth.ThirtyOne),
                        ExpectedExceptionMessageContains = new[] { "lastDayInFiscalYear", "ThirtyOne", "always the last day", "lastMonthInFiscalYear", "December", "CalendarAlignedFiscalYearScheme" },
                        ExpectedExceptionType = typeof(ArgumentException),
                    });

            // The code generated scenarios combine properties of different dummies, which can produce
            // an invalid combination of month and day (e.g. a day of 30 and a month of February).
            DeepCloneWithTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<FixedDayFiscalYearScheme>
                    {
                        Name = "DeepCloneWithLastMonthInFiscalYear should deep clone object and replace LastMonthInFiscalYear with the provided lastMonthInFiscalYear",
                        WithPropertyName = "LastMonthInFiscalYear",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<FixedDayFiscalYearScheme>();

                            var result = new SystemUnderTestDeepCloneWithValue<FixedDayFiscalYearScheme>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = A.Dummy<MonthOfYear>().ThatIs(_ => (_ != systemUnderTest.LastMonthInFiscalYear) && IsValidCombination(_, systemUnderTest.LastDayInFiscalYear)),
                            };

                            return result;
                        },
                    })
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<FixedDayFiscalYearScheme>
                    {
                        Name = "DeepCloneWithLastDayInFiscalYear should deep clone object and replace LastDayInFiscalYear with the provided lastDayInFiscalYear",
                        WithPropertyName = "LastDayInFiscalYear",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<FixedDayFiscalYearScheme>();

                            var result = new SystemUnderTestDeepCloneWithValue<FixedDayFiscalYearScheme>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = A.Dummy<DayOfMonth>().ThatIs(_ => (_ != systemUnderTest.LastDayInFiscalYear) && IsValidCombination(systemUnderTest.LastMonthInFiscalYear, _)),
                            };

                            return result;
                        },
                    })
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<FixedDayFiscalYearScheme>
                    {
                        Name = "DeepCloneWithFiscalYearNumberingConvention should deep clone object and replace FiscalYearNumberingConvention with the provided fiscalYearNumberingConvention",
                        WithPropertyName = "FiscalYearNumberingConvention",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<FixedDayFiscalYearScheme>();

                            var result = new SystemUnderTestDeepCloneWithValue<FixedDayFiscalYearScheme>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = A.Dummy<FixedDayFiscalYearNumberingConvention>().ThatIsNot(systemUnderTest.FiscalYearNumberingConvention),
                            };

                            return result;
                        },
                    });

            EquatableTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                {
                    var referenceObject = ReferenceObjectForEquatableTestScenarios;

                    var result = new EquatableTestScenario<FixedDayFiscalYearScheme>
                    {
                        Name = "Default Code Generated Scenario",
                        ReferenceObject = referenceObject,
                        ObjectsThatAreEqualToButNotTheSameAsReferenceObject = new[]
                        {
                            new FixedDayFiscalYearScheme(
                                referenceObject.LastMonthInFiscalYear,
                                referenceObject.LastDayInFiscalYear,
                                referenceObject.FiscalYearNumberingConvention),
                        },
                        ObjectsThatAreNotEqualToReferenceObject = new[]
                        {
                            new FixedDayFiscalYearScheme(
                                A.Dummy<MonthOfYear>().ThatIs(_ => (_ != referenceObject.LastMonthInFiscalYear) && IsValidCombination(_, referenceObject.LastDayInFiscalYear)),
                                referenceObject.LastDayInFiscalYear,
                                referenceObject.FiscalYearNumberingConvention),
                            new FixedDayFiscalYearScheme(
                                referenceObject.LastMonthInFiscalYear,
                                A.Dummy<DayOfMonth>().ThatIs(_ => (_ != referenceObject.LastDayInFiscalYear) && IsValidCombination(referenceObject.LastMonthInFiscalYear, _)),
                                referenceObject.FiscalYearNumberingConvention),
                            new FixedDayFiscalYearScheme(
                                referenceObject.LastMonthInFiscalYear,
                                referenceObject.LastDayInFiscalYear,
                                A.Dummy<FixedDayFiscalYearNumberingConvention>().ThatIsNot(referenceObject.FiscalYearNumberingConvention)),
                        },
                        ObjectsThatAreNotOfTheSameTypeAsReferenceObject = new object[]
                        {
                            A.Dummy<object>(),
                            A.Dummy<string>(),
                            A.Dummy<int>(),
                            A.Dummy<int?>(),
                            A.Dummy<Guid>(),
                            A.Dummy<CalendarAlignedFiscalQuarterScheme>(),
                            A.Dummy<CalendarAlignedFiscalYearScheme>(),
                            A.Dummy<FiftyTwoFiftyThreeWeekFiscalQuarterScheme>(),
                            A.Dummy<FiftyTwoFiftyThreeWeekFiscalYearScheme>(),
                            A.Dummy<FixedDayFiscalQuarterScheme>(),
                            A.Dummy<MonthEndFiscalQuarterScheme>(),
                            A.Dummy<MonthEndFiscalYearScheme>(),
                        },
                    };

                    return result;
                });
        }

        [Fact]
        public static void Constructor___Should_not_throw___When_lastDayInFiscalYear_is_28_and_lastMonthInFiscalYear_is_February()
        {
            // Arrange, Act
            var actual = Record.Exception(() => new FixedDayFiscalYearScheme(MonthOfYear.February, DayOfMonth.TwentyEight));

            // Assert
            actual.Should().BeNull();
        }

        [Fact]
        public static void Constructor___Should_not_throw___When_lastDayInFiscalYear_occurs_in_lastMonthInFiscalYear_every_year_and_is_not_always_the_last_day_of_the_month()
        {
            // Arrange
            var monthAndDayCombinations = GetValidMonthAndDayCombinations();

            // Act
            var actual = monthAndDayCombinations
                .Select(_ => Record.Exception(() => new FixedDayFiscalYearScheme(_.Month, _.Day)))
                .ToList();

            // Assert
            // 7 months with 31 days (30 valid days each) + 4 months with 30 days (29 valid days each) + February (28 valid days)
            monthAndDayCombinations.Should().HaveCount((7 * 30) + (4 * 29) + 28);
            actual.Should().OnlyContain(_ => _ == null);
        }

        [Fact]
        public static void GetCalendarDayPeriod___Should_throw_ArgumentNullException___When_parameter_fiscalYear_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<FixedDayFiscalYearScheme>();

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCalendarDayPeriod(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("fiscalYear");
        }

        [Fact]
        public static void GetCalendarDayPeriod___Should_return_reporting_period_that_begins_on_the_day_after_the_fixed_day_in_the_prior_calendar_year_and_ends_on_the_fixed_day_in_the_fiscal_year___When_FiscalYearNumberingConvention_is_CalendarYearInWhichFiscalYearEnds()
        {
            // Arrange
            var systemUnderTestAndExpected = new[]
            {
                new
                {
                    SystemUnderTest = new FixedDayFiscalYearScheme(MonthOfYear.April, DayOfMonth.TwentyFive),
                    FiscalYear = new FiscalYear(2026),
                    Expected = new ReportingPeriod(26.April(2025).ToCalendarDay(), 25.April(2026).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new FixedDayFiscalYearScheme(MonthOfYear.January, DayOfMonth.TwentyFive, FixedDayFiscalYearNumberingConvention.CalendarYearInWhichFiscalYearEnds),
                    FiscalYear = new FiscalYear(2026),
                    Expected = new ReportingPeriod(26.January(2025).ToCalendarDay(), 25.January(2026).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new FixedDayFiscalYearScheme(MonthOfYear.January, DayOfMonth.One),
                    FiscalYear = new FiscalYear(2026),
                    Expected = new ReportingPeriod(2.January(2025).ToCalendarDay(), 1.January(2026).ToCalendarDay()),
                },
                new
                {
                    // February 29th is the last day of the fiscal year that ends in 2024, so FY 2024 has 366 days
                    SystemUnderTest = new FixedDayFiscalYearScheme(MonthOfYear.February, DayOfMonth.TwentyEight),
                    FiscalYear = new FiscalYear(2024),
                    Expected = new ReportingPeriod(1.March(2023).ToCalendarDay(), 28.February(2024).ToCalendarDay()),
                },
                new
                {
                    // February 29th is the first day of the fiscal year that ends in 2025
                    SystemUnderTest = new FixedDayFiscalYearScheme(MonthOfYear.February, DayOfMonth.TwentyEight),
                    FiscalYear = new FiscalYear(2025),
                    Expected = new ReportingPeriod(29.February(2024).ToCalendarDay(), 28.February(2025).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new FixedDayFiscalYearScheme(MonthOfYear.March, DayOfMonth.Thirty),
                    FiscalYear = new FiscalYear(2026),
                    Expected = new ReportingPeriod(31.March(2025).ToCalendarDay(), 30.March(2026).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new FixedDayFiscalYearScheme(MonthOfYear.June, DayOfMonth.TwentyNine),
                    FiscalYear = new FiscalYear(2026),
                    Expected = new ReportingPeriod(30.June(2025).ToCalendarDay(), 29.June(2026).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new FixedDayFiscalYearScheme(MonthOfYear.December, DayOfMonth.TwentyFive),
                    FiscalYear = new FiscalYear(2026),
                    Expected = new ReportingPeriod(26.December(2025).ToCalendarDay(), 25.December(2026).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new FixedDayFiscalYearScheme(MonthOfYear.December, DayOfMonth.Thirty),
                    FiscalYear = new FiscalYear(2026),
                    Expected = new ReportingPeriod(31.December(2025).ToCalendarDay(), 30.December(2026).ToCalendarDay()),
                },
            };

            var expected = systemUnderTestAndExpected.Select(_ => _.Expected).ToList();

            // Act
            var actual = systemUnderTestAndExpected.Select(_ => _.SystemUnderTest.GetCalendarDayPeriod(_.FiscalYear)).ToList();

            // Assert
            actual.Should().Equal(expected);
        }

        [Fact]
        public static void GetCalendarDayPeriod___Should_return_reporting_period_that_begins_on_the_day_after_the_fixed_day_in_the_fiscal_year_and_ends_on_the_fixed_day_in_the_next_calendar_year___When_FiscalYearNumberingConvention_is_CalendarYearInWhichFiscalYearBegins()
        {
            // Arrange
            var systemUnderTestAndExpected = new[]
            {
                new
                {
                    SystemUnderTest = new FixedDayFiscalYearScheme(MonthOfYear.January, DayOfMonth.TwentyFive, FixedDayFiscalYearNumberingConvention.CalendarYearInWhichFiscalYearBegins),
                    FiscalYear = new FiscalYear(2026),
                    Expected = new ReportingPeriod(26.January(2026).ToCalendarDay(), 25.January(2027).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new FixedDayFiscalYearScheme(MonthOfYear.April, DayOfMonth.TwentyFive, FixedDayFiscalYearNumberingConvention.CalendarYearInWhichFiscalYearBegins),
                    FiscalYear = new FiscalYear(2025),
                    Expected = new ReportingPeriod(26.April(2025).ToCalendarDay(), 25.April(2026).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new FixedDayFiscalYearScheme(MonthOfYear.February, DayOfMonth.TwentyEight, FixedDayFiscalYearNumberingConvention.CalendarYearInWhichFiscalYearBegins),
                    FiscalYear = new FiscalYear(2024),
                    Expected = new ReportingPeriod(29.February(2024).ToCalendarDay(), 28.February(2025).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = new FixedDayFiscalYearScheme(MonthOfYear.December, DayOfMonth.TwentyFive, FixedDayFiscalYearNumberingConvention.CalendarYearInWhichFiscalYearBegins),
                    FiscalYear = new FiscalYear(2025),
                    Expected = new ReportingPeriod(26.December(2025).ToCalendarDay(), 25.December(2026).ToCalendarDay()),
                },
            };

            var expected = systemUnderTestAndExpected.Select(_ => _.Expected).ToList();

            // Act
            var actual = systemUnderTestAndExpected.Select(_ => _.SystemUnderTest.GetCalendarDayPeriod(_.FiscalYear)).ToList();

            // Assert
            actual.Should().Equal(expected);
        }

        [Fact]
        public static void GetCalendarDayPeriod___Should_return_same_reporting_period_as_next_fiscal_year_numbered_by_CalendarYearInWhichFiscalYearEnds___When_FiscalYearNumberingConvention_is_CalendarYearInWhichFiscalYearBegins()
        {
            // Arrange
            var year = ThreadSafeRandom.Next(1950, 2051);

            var monthAndDayCombinations = GetValidMonthAndDayCombinations();

            var expected = monthAndDayCombinations
                .Select(_ => new FixedDayFiscalYearScheme(_.Month, _.Day, FixedDayFiscalYearNumberingConvention.CalendarYearInWhichFiscalYearEnds).GetCalendarDayPeriod(new FiscalYear(year + 1)))
                .ToList();

            // Act
            var actual = monthAndDayCombinations
                .Select(_ => new FixedDayFiscalYearScheme(_.Month, _.Day, FixedDayFiscalYearNumberingConvention.CalendarYearInWhichFiscalYearBegins).GetCalendarDayPeriod(new FiscalYear(year)))
                .ToList();

            // Assert
            actual.Should().Equal(expected);
        }

        [Fact]
        public static void GetCalendarDayPeriod___Should_return_contiguous_reporting_periods_that_each_end_on_the_fixed_day___When_called_for_consecutive_fiscal_years()
        {
            // Arrange
            var systemsUnderTest = GetValidMonthAndDayCombinations()
                .SelectMany(_ => new[]
                {
                    new FixedDayFiscalYearScheme(_.Month, _.Day, FixedDayFiscalYearNumberingConvention.CalendarYearInWhichFiscalYearEnds),
                    new FixedDayFiscalYearScheme(_.Month, _.Day, FixedDayFiscalYearNumberingConvention.CalendarYearInWhichFiscalYearBegins),
                })
                .ToList();

            // spans multiple leap years
            var fiscalYears = Enumerable.Range(2019, 11).Select(_ => new FiscalYear(_)).ToList();

            foreach (var systemUnderTest in systemsUnderTest)
            {
                // Act
                var actual = fiscalYears.Select(_ => systemUnderTest.GetCalendarDayPeriod(_)).ToList();

                // Assert
                foreach (var reportingPeriod in actual)
                {
                    var start = ((CalendarDay)reportingPeriod.Start).ToDateTime();
                    var end = ((CalendarDay)reportingPeriod.End).ToDateTime();

                    end.Month.Should().Be((int)systemUnderTest.LastMonthInFiscalYear);
                    end.Day.Should().Be((int)systemUnderTest.LastDayInFiscalYear);
                    start.Year.Should().Be(end.Year - 1);
                    ((end - start).Days + 1).Should().BeOneOf(365, 366);
                }

                for (var x = 1; x < actual.Count; x++)
                {
                    ((CalendarDay)actual[x].Start).ToDateTime().Should().Be(((CalendarDay)actual[x - 1].End).ToDateTime().AddDays(1));
                }
            }
        }

        private static bool IsValidCombination(
            MonthOfYear lastMonthInFiscalYear,
            DayOfMonth lastDayInFiscalYear)
        {
            // The day must occur in the month every year and must not always be the last day of the month.
            // The number of days in the month in a leap year, minus one, satisfies both constraints
            // (it's 28 for February and one less than the number of days in the month otherwise).
            var result = (int)lastDayInFiscalYear < DateTime.DaysInMonth(2000, (int)lastMonthInFiscalYear);

            return result;
        }

        private static IReadOnlyList<(MonthOfYear Month, DayOfMonth Day)> GetValidMonthAndDayCombinations()
        {
            var months = EnumExtensions.GetDefinedEnumValues<MonthOfYear>().Where(_ => _ != MonthOfYear.Invalid).ToList();

            var days = EnumExtensions.GetDefinedEnumValues<DayOfMonth>().Where(_ => _ != DayOfMonth.Invalid).ToList();

            var result = months
                .SelectMany(month => days.Select(day => (Month: month, Day: day)))
                .Where(_ => IsValidCombination(_.Month, _.Day))
                .ToList();

            return result;
        }
    }
}
