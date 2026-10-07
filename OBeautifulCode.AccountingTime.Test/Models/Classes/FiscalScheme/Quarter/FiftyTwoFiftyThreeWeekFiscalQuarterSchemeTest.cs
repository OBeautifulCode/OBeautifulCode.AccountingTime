// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FiftyTwoFiftyThreeWeekFiscalQuarterSchemeTest.cs" company="OBeautifulCode">
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
    public static partial class FiftyTwoFiftyThreeWeekFiscalQuarterSchemeTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static FiftyTwoFiftyThreeWeekFiscalQuarterSchemeTest()
        {
            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<FiftyTwoFiftyThreeWeekFiscalQuarterScheme>
                    {
                        Name = "constructor should throw ArgumentNullException when parameter 'fiscalYearScheme' is null scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<FiftyTwoFiftyThreeWeekFiscalQuarterScheme>();

                            var result = new FiftyTwoFiftyThreeWeekFiscalQuarterScheme(
                                                 null,
                                                 referenceObject.WeeksPerQuarter,
                                                 referenceObject.QuarterWithFiftyThirdWeek);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentNullException),
                        ExpectedExceptionMessageContains = new[] { "fiscalYearScheme", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<FiftyTwoFiftyThreeWeekFiscalQuarterScheme>
                    {
                        Name = "constructor should throw ArgumentNullException when parameter 'weeksPerQuarter' is null scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<FiftyTwoFiftyThreeWeekFiscalQuarterScheme>();

                            var result = new FiftyTwoFiftyThreeWeekFiscalQuarterScheme(
                                                 referenceObject.FiscalYearScheme,
                                                 null,
                                                 referenceObject.QuarterWithFiftyThirdWeek);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentNullException),
                        ExpectedExceptionMessageContains = new[] { "weeksPerQuarter", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<FiftyTwoFiftyThreeWeekFiscalQuarterScheme>
                    {
                        Name = "Constructor should throw ArgumentException when parameter 'weeksPerQuarter' contains fewer than 4 elements",
                        ConstructionFunc = () => new FiftyTwoFiftyThreeWeekFiscalQuarterScheme(A.Dummy<FiftyTwoFiftyThreeWeekFiscalYearScheme>(), new[] { 16, 16, 16 }, A.Dummy<QuarterNumber>()),
                        ExpectedExceptionMessageContains = new[] { "weeksPerQuarter", "must contain '4' elements" },
                        ExpectedExceptionType = typeof(ArgumentException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<FiftyTwoFiftyThreeWeekFiscalQuarterScheme>
                    {
                        Name = "Constructor should throw ArgumentException when parameter 'weeksPerQuarter' contains more than 4 elements",
                        ConstructionFunc = () => new FiftyTwoFiftyThreeWeekFiscalQuarterScheme(A.Dummy<FiftyTwoFiftyThreeWeekFiscalYearScheme>(), new[] { 12, 12, 12, 12, 12 }, A.Dummy<QuarterNumber>()),
                        ExpectedExceptionMessageContains = new[] { "weeksPerQuarter", "must contain '4' elements" },
                        ExpectedExceptionType = typeof(ArgumentException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<FiftyTwoFiftyThreeWeekFiscalQuarterScheme>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'weeksPerQuarter' contains an element that is < 12",
                        ConstructionFunc = () => new FiftyTwoFiftyThreeWeekFiscalQuarterScheme(A.Dummy<FiftyTwoFiftyThreeWeekFiscalYearScheme>(), new[] { 11, 13, 14, 14 }, A.Dummy<QuarterNumber>()),
                        ExpectedExceptionMessageContains = new[] { "weeksPerQuarter", "< '12'" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<FiftyTwoFiftyThreeWeekFiscalQuarterScheme>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'weeksPerQuarter' contains an element that is negative",
                        ConstructionFunc = () => new FiftyTwoFiftyThreeWeekFiscalQuarterScheme(A.Dummy<FiftyTwoFiftyThreeWeekFiscalYearScheme>(), new[] { -1, 16, 16, 16 }, A.Dummy<QuarterNumber>()),
                        ExpectedExceptionMessageContains = new[] { "weeksPerQuarter", "< '12'" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<FiftyTwoFiftyThreeWeekFiscalQuarterScheme>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'weeksPerQuarter' contains an element that is > 16",
                        ConstructionFunc = () => new FiftyTwoFiftyThreeWeekFiscalQuarterScheme(A.Dummy<FiftyTwoFiftyThreeWeekFiscalYearScheme>(), new[] { 17, 12, 12, 12 }, A.Dummy<QuarterNumber>()),
                        ExpectedExceptionMessageContains = new[] { "weeksPerQuarter", "> '16'" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<FiftyTwoFiftyThreeWeekFiscalQuarterScheme>
                    {
                        Name = "Constructor should throw ArgumentException when the elements of parameter 'weeksPerQuarter' do not sum to 52",
                        ConstructionFunc = () => new FiftyTwoFiftyThreeWeekFiscalQuarterScheme(A.Dummy<FiftyTwoFiftyThreeWeekFiscalYearScheme>(), new[] { 13, 13, 13, 14 }, A.Dummy<QuarterNumber>()),
                        ExpectedExceptionMessageContains = new[] { "weeksPerQuarter", "'53'", "must be '52'" },
                        ExpectedExceptionType = typeof(ArgumentException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<FiftyTwoFiftyThreeWeekFiscalQuarterScheme>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'quarterWithFiftyThirdWeek' is QuarterNumber.Invalid",
                        ConstructionFunc = () => new FiftyTwoFiftyThreeWeekFiscalQuarterScheme(A.Dummy<FiftyTwoFiftyThreeWeekFiscalYearScheme>(), new[] { 13, 13, 13, 13 }, QuarterNumber.Invalid),
                        ExpectedExceptionMessageContains = new[] { "quarterWithFiftyThirdWeek", "Invalid" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    });
        }

        [Fact]
        public static void GetCalendarDayPeriod___Should_throw_ArgumentNullException___When_parameter_fiscalQuarter_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<FiftyTwoFiftyThreeWeekFiscalQuarterScheme>();

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCalendarDayPeriod(null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("fiscalQuarter");
        }

        [Fact]
        public static void GetCalendarDayPeriod___Should_return_13_week_quarters_and_add_53rd_week_to_fourth_quarter___When_WeeksPerQuarter_is_13_13_13_13_and_QuarterWithFiftyThirdWeek_is_Q4()
        {
            // Arrange
            var systemUnderTest1 = new FiftyTwoFiftyThreeWeekFiscalQuarterScheme(
                new FiftyTwoFiftyThreeWeekFiscalYearScheme(DayOfWeek.Saturday, MonthOfYear.December, FiftyTwoFiftyThreeWeekMethodology.LastOccurrenceInAnchorMonth),
                new[] { 13, 13, 13, 13 },
                QuarterNumber.Q4);

            var systemUnderTest2 = new FiftyTwoFiftyThreeWeekFiscalQuarterScheme(
                new FiftyTwoFiftyThreeWeekFiscalYearScheme(DayOfWeek.Saturday, MonthOfYear.December, FiftyTwoFiftyThreeWeekMethodology.ClosestToLastDayOfAnchorMonth),
                new[] { 13, 13, 13, 13 },
                QuarterNumber.Q4);

            // 53-week year
            var expectedReportingPeriod1Q1 = new ReportingPeriod(26.December(2021).ToCalendarDay(), 26.March(2022).ToCalendarDay());
            var expectedReportingPeriod1Q2 = new ReportingPeriod(27.March(2022).ToCalendarDay(), 25.June(2022).ToCalendarDay());
            var expectedReportingPeriod1Q3 = new ReportingPeriod(26.June(2022).ToCalendarDay(), 24.September(2022).ToCalendarDay());
            var expectedReportingPeriod1Q4 = new ReportingPeriod(25.September(2022).ToCalendarDay(), 31.December(2022).ToCalendarDay());

            // 52-week year
            var expectedReportingPeriod2Q1 = new ReportingPeriod(1.January(2023).ToCalendarDay(), 1.April(2023).ToCalendarDay());
            var expectedReportingPeriod2Q2 = new ReportingPeriod(2.April(2023).ToCalendarDay(), 1.July(2023).ToCalendarDay());
            var expectedReportingPeriod2Q3 = new ReportingPeriod(2.July(2023).ToCalendarDay(), 30.September(2023).ToCalendarDay());
            var expectedReportingPeriod2Q4 = new ReportingPeriod(1.October(2023).ToCalendarDay(), 30.December(2023).ToCalendarDay());

            // 53-week year that ends in the calendar year after the anchor month
            var expectedReportingPeriod3Q1 = new ReportingPeriod(29.December(2024).ToCalendarDay(), 29.March(2025).ToCalendarDay());
            var expectedReportingPeriod3Q2 = new ReportingPeriod(30.March(2025).ToCalendarDay(), 28.June(2025).ToCalendarDay());
            var expectedReportingPeriod3Q3 = new ReportingPeriod(29.June(2025).ToCalendarDay(), 27.September(2025).ToCalendarDay());
            var expectedReportingPeriod3Q4 = new ReportingPeriod(28.September(2025).ToCalendarDay(), 3.January(2026).ToCalendarDay());

            // Act
            var actualReportingPeriod1Q1 = systemUnderTest1.GetCalendarDayPeriod(new FiscalQuarter(2022, QuarterNumber.Q1));
            var actualReportingPeriod1Q2 = systemUnderTest1.GetCalendarDayPeriod(new FiscalQuarter(2022, QuarterNumber.Q2));
            var actualReportingPeriod1Q3 = systemUnderTest1.GetCalendarDayPeriod(new FiscalQuarter(2022, QuarterNumber.Q3));
            var actualReportingPeriod1Q4 = systemUnderTest1.GetCalendarDayPeriod(new FiscalQuarter(2022, QuarterNumber.Q4));

            var actualReportingPeriod2Q1 = systemUnderTest1.GetCalendarDayPeriod(new FiscalQuarter(2023, QuarterNumber.Q1));
            var actualReportingPeriod2Q2 = systemUnderTest1.GetCalendarDayPeriod(new FiscalQuarter(2023, QuarterNumber.Q2));
            var actualReportingPeriod2Q3 = systemUnderTest1.GetCalendarDayPeriod(new FiscalQuarter(2023, QuarterNumber.Q3));
            var actualReportingPeriod2Q4 = systemUnderTest1.GetCalendarDayPeriod(new FiscalQuarter(2023, QuarterNumber.Q4));

            var actualReportingPeriod3Q1 = systemUnderTest2.GetCalendarDayPeriod(new FiscalQuarter(2025, QuarterNumber.Q1));
            var actualReportingPeriod3Q2 = systemUnderTest2.GetCalendarDayPeriod(new FiscalQuarter(2025, QuarterNumber.Q2));
            var actualReportingPeriod3Q3 = systemUnderTest2.GetCalendarDayPeriod(new FiscalQuarter(2025, QuarterNumber.Q3));
            var actualReportingPeriod3Q4 = systemUnderTest2.GetCalendarDayPeriod(new FiscalQuarter(2025, QuarterNumber.Q4));

            // Assert
            actualReportingPeriod1Q1.Should().Be(expectedReportingPeriod1Q1);
            actualReportingPeriod1Q2.Should().Be(expectedReportingPeriod1Q2);
            actualReportingPeriod1Q3.Should().Be(expectedReportingPeriod1Q3);
            actualReportingPeriod1Q4.Should().Be(expectedReportingPeriod1Q4);

            actualReportingPeriod2Q1.Should().Be(expectedReportingPeriod2Q1);
            actualReportingPeriod2Q2.Should().Be(expectedReportingPeriod2Q2);
            actualReportingPeriod2Q3.Should().Be(expectedReportingPeriod2Q3);
            actualReportingPeriod2Q4.Should().Be(expectedReportingPeriod2Q4);

            actualReportingPeriod3Q1.Should().Be(expectedReportingPeriod3Q1);
            actualReportingPeriod3Q2.Should().Be(expectedReportingPeriod3Q2);
            actualReportingPeriod3Q3.Should().Be(expectedReportingPeriod3Q3);
            actualReportingPeriod3Q4.Should().Be(expectedReportingPeriod3Q4);
        }

        [Fact]
        public static void GetCalendarDayPeriod___Should_add_53rd_week_to_first_quarter___When_QuarterWithFiftyThirdWeek_is_Q1()
        {
            // Arrange
            var systemUnderTest = new FiftyTwoFiftyThreeWeekFiscalQuarterScheme(
                new FiftyTwoFiftyThreeWeekFiscalYearScheme(DayOfWeek.Saturday, MonthOfYear.September, FiftyTwoFiftyThreeWeekMethodology.LastOccurrenceInAnchorMonth),
                new[] { 13, 13, 13, 13 },
                QuarterNumber.Q1);

            // 53-week year
            var expectedReportingPeriod1Q1 = new ReportingPeriod(25.September(2022).ToCalendarDay(), 31.December(2022).ToCalendarDay());
            var expectedReportingPeriod1Q2 = new ReportingPeriod(1.January(2023).ToCalendarDay(), 1.April(2023).ToCalendarDay());
            var expectedReportingPeriod1Q3 = new ReportingPeriod(2.April(2023).ToCalendarDay(), 1.July(2023).ToCalendarDay());
            var expectedReportingPeriod1Q4 = new ReportingPeriod(2.July(2023).ToCalendarDay(), 30.September(2023).ToCalendarDay());

            // 52-week year
            var expectedReportingPeriod2Q1 = new ReportingPeriod(1.October(2023).ToCalendarDay(), 30.December(2023).ToCalendarDay());
            var expectedReportingPeriod2Q2 = new ReportingPeriod(31.December(2023).ToCalendarDay(), 30.March(2024).ToCalendarDay());
            var expectedReportingPeriod2Q3 = new ReportingPeriod(31.March(2024).ToCalendarDay(), 29.June(2024).ToCalendarDay());
            var expectedReportingPeriod2Q4 = new ReportingPeriod(30.June(2024).ToCalendarDay(), 28.September(2024).ToCalendarDay());

            // Act
            var actualReportingPeriod1Q1 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(2023, QuarterNumber.Q1));
            var actualReportingPeriod1Q2 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(2023, QuarterNumber.Q2));
            var actualReportingPeriod1Q3 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(2023, QuarterNumber.Q3));
            var actualReportingPeriod1Q4 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(2023, QuarterNumber.Q4));

            var actualReportingPeriod2Q1 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(2024, QuarterNumber.Q1));
            var actualReportingPeriod2Q2 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(2024, QuarterNumber.Q2));
            var actualReportingPeriod2Q3 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(2024, QuarterNumber.Q3));
            var actualReportingPeriod2Q4 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(2024, QuarterNumber.Q4));

            // Assert
            actualReportingPeriod1Q1.Should().Be(expectedReportingPeriod1Q1);
            actualReportingPeriod1Q2.Should().Be(expectedReportingPeriod1Q2);
            actualReportingPeriod1Q3.Should().Be(expectedReportingPeriod1Q3);
            actualReportingPeriod1Q4.Should().Be(expectedReportingPeriod1Q4);

            actualReportingPeriod2Q1.Should().Be(expectedReportingPeriod2Q1);
            actualReportingPeriod2Q2.Should().Be(expectedReportingPeriod2Q2);
            actualReportingPeriod2Q3.Should().Be(expectedReportingPeriod2Q3);
            actualReportingPeriod2Q4.Should().Be(expectedReportingPeriod2Q4);
        }

        [Fact]
        public static void GetCalendarDayPeriod___Should_return_16_12_12_12_week_quarters_and_add_53rd_week_to_second_quarter___When_WeeksPerQuarter_is_16_12_12_12_and_QuarterWithFiftyThirdWeek_is_Q2()
        {
            // Arrange
            var systemUnderTest = new FiftyTwoFiftyThreeWeekFiscalQuarterScheme(
                new FiftyTwoFiftyThreeWeekFiscalYearScheme(DayOfWeek.Saturday, MonthOfYear.December, FiftyTwoFiftyThreeWeekMethodology.LastOccurrenceInAnchorMonth),
                new[] { 16, 12, 12, 12 },
                QuarterNumber.Q2);

            // 53-week year
            var expectedReportingPeriodQ1 = new ReportingPeriod(26.December(2021).ToCalendarDay(), 16.April(2022).ToCalendarDay());
            var expectedReportingPeriodQ2 = new ReportingPeriod(17.April(2022).ToCalendarDay(), 16.July(2022).ToCalendarDay());
            var expectedReportingPeriodQ3 = new ReportingPeriod(17.July(2022).ToCalendarDay(), 8.October(2022).ToCalendarDay());
            var expectedReportingPeriodQ4 = new ReportingPeriod(9.October(2022).ToCalendarDay(), 31.December(2022).ToCalendarDay());

            // Act
            var actualReportingPeriodQ1 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(2022, QuarterNumber.Q1));
            var actualReportingPeriodQ2 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(2022, QuarterNumber.Q2));
            var actualReportingPeriodQ3 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(2022, QuarterNumber.Q3));
            var actualReportingPeriodQ4 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(2022, QuarterNumber.Q4));

            // Assert
            actualReportingPeriodQ1.Should().Be(expectedReportingPeriodQ1);
            actualReportingPeriodQ2.Should().Be(expectedReportingPeriodQ2);
            actualReportingPeriodQ3.Should().Be(expectedReportingPeriodQ3);
            actualReportingPeriodQ4.Should().Be(expectedReportingPeriodQ4);
        }

        [Fact]
        public static void GetCalendarDayPeriod___Should_add_53rd_week_to_third_quarter___When_QuarterWithFiftyThirdWeek_is_Q3()
        {
            // Arrange
            var systemUnderTest = new FiftyTwoFiftyThreeWeekFiscalQuarterScheme(
                new FiftyTwoFiftyThreeWeekFiscalYearScheme(DayOfWeek.Saturday, MonthOfYear.March, FiftyTwoFiftyThreeWeekMethodology.LastOccurrenceInAnchorMonth),
                new[] { 13, 13, 13, 13 },
                QuarterNumber.Q3);

            // 53-week year
            var expectedReportingPeriod1Q1 = new ReportingPeriod(26.March(2023).ToCalendarDay(), 24.June(2023).ToCalendarDay());
            var expectedReportingPeriod1Q2 = new ReportingPeriod(25.June(2023).ToCalendarDay(), 23.September(2023).ToCalendarDay());
            var expectedReportingPeriod1Q3 = new ReportingPeriod(24.September(2023).ToCalendarDay(), 30.December(2023).ToCalendarDay());
            var expectedReportingPeriod1Q4 = new ReportingPeriod(31.December(2023).ToCalendarDay(), 30.March(2024).ToCalendarDay());

            // 52-week year
            var expectedReportingPeriod2Q1 = new ReportingPeriod(31.March(2024).ToCalendarDay(), 29.June(2024).ToCalendarDay());
            var expectedReportingPeriod2Q2 = new ReportingPeriod(30.June(2024).ToCalendarDay(), 28.September(2024).ToCalendarDay());
            var expectedReportingPeriod2Q3 = new ReportingPeriod(29.September(2024).ToCalendarDay(), 28.December(2024).ToCalendarDay());
            var expectedReportingPeriod2Q4 = new ReportingPeriod(29.December(2024).ToCalendarDay(), 29.March(2025).ToCalendarDay());

            // Act
            var actualReportingPeriod1Q1 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(2024, QuarterNumber.Q1));
            var actualReportingPeriod1Q2 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(2024, QuarterNumber.Q2));
            var actualReportingPeriod1Q3 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(2024, QuarterNumber.Q3));
            var actualReportingPeriod1Q4 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(2024, QuarterNumber.Q4));

            var actualReportingPeriod2Q1 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(2025, QuarterNumber.Q1));
            var actualReportingPeriod2Q2 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(2025, QuarterNumber.Q2));
            var actualReportingPeriod2Q3 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(2025, QuarterNumber.Q3));
            var actualReportingPeriod2Q4 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(2025, QuarterNumber.Q4));

            // Assert
            actualReportingPeriod1Q1.Should().Be(expectedReportingPeriod1Q1);
            actualReportingPeriod1Q2.Should().Be(expectedReportingPeriod1Q2);
            actualReportingPeriod1Q3.Should().Be(expectedReportingPeriod1Q3);
            actualReportingPeriod1Q4.Should().Be(expectedReportingPeriod1Q4);

            actualReportingPeriod2Q1.Should().Be(expectedReportingPeriod2Q1);
            actualReportingPeriod2Q2.Should().Be(expectedReportingPeriod2Q2);
            actualReportingPeriod2Q3.Should().Be(expectedReportingPeriod2Q3);
            actualReportingPeriod2Q4.Should().Be(expectedReportingPeriod2Q4);
        }

        [Fact]
        public static void GetCalendarDayPeriod___Should_return_12_12_12_16_week_quarters_and_add_53rd_week_to_fourth_quarter___When_WeeksPerQuarter_is_12_12_12_16_and_QuarterWithFiftyThirdWeek_is_Q4()
        {
            // Arrange
            var systemUnderTest = new FiftyTwoFiftyThreeWeekFiscalQuarterScheme(
                new FiftyTwoFiftyThreeWeekFiscalYearScheme(DayOfWeek.Saturday, MonthOfYear.December, FiftyTwoFiftyThreeWeekMethodology.LastOccurrenceInAnchorMonth),
                new[] { 12, 12, 12, 16 },
                QuarterNumber.Q4);

            // 53-week year
            var expectedReportingPeriod1Q1 = new ReportingPeriod(26.December(2021).ToCalendarDay(), 19.March(2022).ToCalendarDay());
            var expectedReportingPeriod1Q2 = new ReportingPeriod(20.March(2022).ToCalendarDay(), 11.June(2022).ToCalendarDay());
            var expectedReportingPeriod1Q3 = new ReportingPeriod(12.June(2022).ToCalendarDay(), 3.September(2022).ToCalendarDay());
            var expectedReportingPeriod1Q4 = new ReportingPeriod(4.September(2022).ToCalendarDay(), 31.December(2022).ToCalendarDay());

            // 52-week year
            var expectedReportingPeriod2Q1 = new ReportingPeriod(1.January(2023).ToCalendarDay(), 25.March(2023).ToCalendarDay());
            var expectedReportingPeriod2Q2 = new ReportingPeriod(26.March(2023).ToCalendarDay(), 17.June(2023).ToCalendarDay());
            var expectedReportingPeriod2Q3 = new ReportingPeriod(18.June(2023).ToCalendarDay(), 9.September(2023).ToCalendarDay());
            var expectedReportingPeriod2Q4 = new ReportingPeriod(10.September(2023).ToCalendarDay(), 30.December(2023).ToCalendarDay());

            // Act
            var actualReportingPeriod1Q1 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(2022, QuarterNumber.Q1));
            var actualReportingPeriod1Q2 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(2022, QuarterNumber.Q2));
            var actualReportingPeriod1Q3 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(2022, QuarterNumber.Q3));
            var actualReportingPeriod1Q4 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(2022, QuarterNumber.Q4));

            var actualReportingPeriod2Q1 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(2023, QuarterNumber.Q1));
            var actualReportingPeriod2Q2 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(2023, QuarterNumber.Q2));
            var actualReportingPeriod2Q3 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(2023, QuarterNumber.Q3));
            var actualReportingPeriod2Q4 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(2023, QuarterNumber.Q4));

            // Assert
            actualReportingPeriod1Q1.Should().Be(expectedReportingPeriod1Q1);
            actualReportingPeriod1Q2.Should().Be(expectedReportingPeriod1Q2);
            actualReportingPeriod1Q3.Should().Be(expectedReportingPeriod1Q3);
            actualReportingPeriod1Q4.Should().Be(expectedReportingPeriod1Q4);

            actualReportingPeriod2Q1.Should().Be(expectedReportingPeriod2Q1);
            actualReportingPeriod2Q2.Should().Be(expectedReportingPeriod2Q2);
            actualReportingPeriod2Q3.Should().Be(expectedReportingPeriod2Q3);
            actualReportingPeriod2Q4.Should().Be(expectedReportingPeriod2Q4);
        }

        [Fact]
        public static void GetCalendarDayPeriod___Should_return_12_12_16_12_week_quarters_and_add_53rd_week_to_fourth_quarter___When_WeeksPerQuarter_is_12_12_16_12_and_QuarterWithFiftyThirdWeek_is_Q4()
        {
            // Arrange
            var systemUnderTest = new FiftyTwoFiftyThreeWeekFiscalQuarterScheme(
                new FiftyTwoFiftyThreeWeekFiscalYearScheme(DayOfWeek.Sunday, MonthOfYear.January, FiftyTwoFiftyThreeWeekMethodology.ClosestToLastDayOfAnchorMonth),
                new[] { 12, 12, 16, 12 },
                QuarterNumber.Q4);

            // 53-week year
            var expectedReportingPeriod1Q1 = new ReportingPeriod(29.January(2024).ToCalendarDay(), 21.April(2024).ToCalendarDay());
            var expectedReportingPeriod1Q2 = new ReportingPeriod(22.April(2024).ToCalendarDay(), 14.July(2024).ToCalendarDay());
            var expectedReportingPeriod1Q3 = new ReportingPeriod(15.July(2024).ToCalendarDay(), 3.November(2024).ToCalendarDay());
            var expectedReportingPeriod1Q4 = new ReportingPeriod(4.November(2024).ToCalendarDay(), 2.February(2025).ToCalendarDay());

            // 52-week year
            var expectedReportingPeriod2Q1 = new ReportingPeriod(3.February(2025).ToCalendarDay(), 27.April(2025).ToCalendarDay());
            var expectedReportingPeriod2Q2 = new ReportingPeriod(28.April(2025).ToCalendarDay(), 20.July(2025).ToCalendarDay());
            var expectedReportingPeriod2Q3 = new ReportingPeriod(21.July(2025).ToCalendarDay(), 9.November(2025).ToCalendarDay());
            var expectedReportingPeriod2Q4 = new ReportingPeriod(10.November(2025).ToCalendarDay(), 1.February(2026).ToCalendarDay());

            // Act
            var actualReportingPeriod1Q1 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(2025, QuarterNumber.Q1));
            var actualReportingPeriod1Q2 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(2025, QuarterNumber.Q2));
            var actualReportingPeriod1Q3 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(2025, QuarterNumber.Q3));
            var actualReportingPeriod1Q4 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(2025, QuarterNumber.Q4));

            var actualReportingPeriod2Q1 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(2026, QuarterNumber.Q1));
            var actualReportingPeriod2Q2 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(2026, QuarterNumber.Q2));
            var actualReportingPeriod2Q3 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(2026, QuarterNumber.Q3));
            var actualReportingPeriod2Q4 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(2026, QuarterNumber.Q4));

            // Assert
            actualReportingPeriod1Q1.Should().Be(expectedReportingPeriod1Q1);
            actualReportingPeriod1Q2.Should().Be(expectedReportingPeriod1Q2);
            actualReportingPeriod1Q3.Should().Be(expectedReportingPeriod1Q3);
            actualReportingPeriod1Q4.Should().Be(expectedReportingPeriod1Q4);

            actualReportingPeriod2Q1.Should().Be(expectedReportingPeriod2Q1);
            actualReportingPeriod2Q2.Should().Be(expectedReportingPeriod2Q2);
            actualReportingPeriod2Q3.Should().Be(expectedReportingPeriod2Q3);
            actualReportingPeriod2Q4.Should().Be(expectedReportingPeriod2Q4);
        }

        [Fact]
        public static void GetCalendarDayPeriod___Should_return_quarters_of_the_fiscal_year_whose_anchor_month_is_in_the_calendar_year_after_the_fiscal_quarters_year___When_FiscalYearNumberingConvention_is_CalendarYearPriorToAnchorMonth()
        {
            // Arrange
            // Target: Saturday closest to the end of January; fiscal 2023 is a 53-week year that ends on Feb 3, 2024
            var systemUnderTest = new FiftyTwoFiftyThreeWeekFiscalQuarterScheme(
                new FiftyTwoFiftyThreeWeekFiscalYearScheme(DayOfWeek.Saturday, MonthOfYear.January, FiftyTwoFiftyThreeWeekMethodology.ClosestToLastDayOfAnchorMonth, FiftyTwoFiftyThreeWeekFiscalYearNumberingConvention.CalendarYearPriorToAnchorMonth),
                new[] { 13, 13, 13, 13 },
                QuarterNumber.Q4);

            var expectedQ1 = new ReportingPeriod(29.January(2023).ToCalendarDay(), 29.April(2023).ToCalendarDay());
            var expectedQ2 = new ReportingPeriod(30.April(2023).ToCalendarDay(), 29.July(2023).ToCalendarDay());
            var expectedQ3 = new ReportingPeriod(30.July(2023).ToCalendarDay(), 28.October(2023).ToCalendarDay());
            var expectedQ4 = new ReportingPeriod(29.October(2023).ToCalendarDay(), 3.February(2024).ToCalendarDay());

            // Act
            var actualQ1 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(2023, QuarterNumber.Q1));
            var actualQ2 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(2023, QuarterNumber.Q2));
            var actualQ3 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(2023, QuarterNumber.Q3));
            var actualQ4 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(2023, QuarterNumber.Q4));

            // Assert
            actualQ1.Should().Be(expectedQ1);
            actualQ2.Should().Be(expectedQ2);
            actualQ3.Should().Be(expectedQ3);
            actualQ4.Should().Be(expectedQ4);
        }
    }
}