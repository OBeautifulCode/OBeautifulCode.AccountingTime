// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FiftyTwoFiftyThreeWeekFiscalYearSchemeTest.cs" company="OBeautifulCode">
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
    public static partial class FiftyTwoFiftyThreeWeekFiscalYearSchemeTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static FiftyTwoFiftyThreeWeekFiscalYearSchemeTest()
        {
            ConstructorArgumentValidationTestScenarios
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<FiftyTwoFiftyThreeWeekFiscalYearScheme>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'anchorMonth' is MonthOfYear.Invalid",
                        ConstructionFunc = () => new FiftyTwoFiftyThreeWeekFiscalYearScheme(A.Dummy<DayOfWeek>(), MonthOfYear.Invalid, A.Dummy<FiftyTwoFiftyThreeWeekMethodology>()),
                        ExpectedExceptionMessageContains = new[] { "anchorMonth", "Invalid" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<FiftyTwoFiftyThreeWeekFiscalYearScheme>
                    {
                        Name = "Constructor should throw ArgumentException when parameter 'fiscalYearNumberingConvention' is CalendarYearPriorToAnchorMonth and parameter 'anchorMonth' is MonthOfYear.December",
                        ConstructionFunc = () => new FiftyTwoFiftyThreeWeekFiscalYearScheme(A.Dummy<DayOfWeek>(), MonthOfYear.December, A.Dummy<FiftyTwoFiftyThreeWeekMethodology>(), FiftyTwoFiftyThreeWeekFiscalYearNumberingConvention.CalendarYearPriorToAnchorMonth),
                        ExpectedExceptionMessageContains = new[] { "fiscalYearNumberingConvention", "CalendarYearPriorToAnchorMonth", "anchorMonth", "December" },
                        ExpectedExceptionType = typeof(ArgumentException),
                    });

            // The code generated scenarios combine properties of different dummies, which can produce
            // the invalid combination of a December anchor month and CalendarYearPriorToAnchorMonth.
            DeepCloneWithTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<FiftyTwoFiftyThreeWeekFiscalYearScheme>
                    {
                        Name = "DeepCloneWithLastDayOfWeekInFiscalYear should deep clone object and replace LastDayOfWeekInFiscalYear with the provided lastDayOfWeekInFiscalYear",
                        WithPropertyName = "LastDayOfWeekInFiscalYear",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<FiftyTwoFiftyThreeWeekFiscalYearScheme>();

                            var result = new SystemUnderTestDeepCloneWithValue<FiftyTwoFiftyThreeWeekFiscalYearScheme>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = A.Dummy<DayOfWeek>().ThatIsNot(systemUnderTest.LastDayOfWeekInFiscalYear),
                            };

                            return result;
                        },
                    })
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<FiftyTwoFiftyThreeWeekFiscalYearScheme>
                    {
                        Name = "DeepCloneWithAnchorMonth should deep clone object and replace AnchorMonth with the provided anchorMonth",
                        WithPropertyName = "AnchorMonth",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<FiftyTwoFiftyThreeWeekFiscalYearScheme>();

                            var result = new SystemUnderTestDeepCloneWithValue<FiftyTwoFiftyThreeWeekFiscalYearScheme>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = A.Dummy<MonthOfYear>().ThatIs(_ => (_ != systemUnderTest.AnchorMonth) && IsValidCombination(_, systemUnderTest.FiscalYearNumberingConvention)),
                            };

                            return result;
                        },
                    })
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<FiftyTwoFiftyThreeWeekFiscalYearScheme>
                    {
                        Name = "DeepCloneWithMethodology should deep clone object and replace Methodology with the provided methodology",
                        WithPropertyName = "Methodology",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<FiftyTwoFiftyThreeWeekFiscalYearScheme>();

                            var result = new SystemUnderTestDeepCloneWithValue<FiftyTwoFiftyThreeWeekFiscalYearScheme>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = A.Dummy<FiftyTwoFiftyThreeWeekMethodology>().ThatIsNot(systemUnderTest.Methodology),
                            };

                            return result;
                        },
                    })
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<FiftyTwoFiftyThreeWeekFiscalYearScheme>
                    {
                        Name = "DeepCloneWithFiscalYearNumberingConvention should deep clone object and replace FiscalYearNumberingConvention with the provided fiscalYearNumberingConvention",
                        WithPropertyName = "FiscalYearNumberingConvention",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            // with a December anchor month, the only valid convention is CalendarYearOfAnchorMonth, so there is nothing to replace it with
                            var systemUnderTest = A.Dummy<FiftyTwoFiftyThreeWeekFiscalYearScheme>().ThatIs(_ => _.AnchorMonth != MonthOfYear.December);

                            var result = new SystemUnderTestDeepCloneWithValue<FiftyTwoFiftyThreeWeekFiscalYearScheme>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = A.Dummy<FiftyTwoFiftyThreeWeekFiscalYearNumberingConvention>().ThatIsNot(systemUnderTest.FiscalYearNumberingConvention),
                            };

                            return result;
                        },
                    });

            EquatableTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                {
                    var referenceObject = ReferenceObjectForEquatableTestScenarios;

                    var objectsThatAreNotEqualToReferenceObject = new List<FiftyTwoFiftyThreeWeekFiscalYearScheme>
                    {
                        new FiftyTwoFiftyThreeWeekFiscalYearScheme(
                            A.Dummy<DayOfWeek>().ThatIsNot(referenceObject.LastDayOfWeekInFiscalYear),
                            referenceObject.AnchorMonth,
                            referenceObject.Methodology,
                            referenceObject.FiscalYearNumberingConvention),
                        new FiftyTwoFiftyThreeWeekFiscalYearScheme(
                            referenceObject.LastDayOfWeekInFiscalYear,
                            A.Dummy<MonthOfYear>().ThatIs(_ => (_ != referenceObject.AnchorMonth) && IsValidCombination(_, referenceObject.FiscalYearNumberingConvention)),
                            referenceObject.Methodology,
                            referenceObject.FiscalYearNumberingConvention),
                        new FiftyTwoFiftyThreeWeekFiscalYearScheme(
                            referenceObject.LastDayOfWeekInFiscalYear,
                            referenceObject.AnchorMonth,
                            A.Dummy<FiftyTwoFiftyThreeWeekMethodology>().ThatIsNot(referenceObject.Methodology),
                            referenceObject.FiscalYearNumberingConvention),
                    };

                    // with a December anchor month, the only valid convention is CalendarYearOfAnchorMonth, so there is no other convention to compare against
                    if (referenceObject.AnchorMonth != MonthOfYear.December)
                    {
                        objectsThatAreNotEqualToReferenceObject.Add(
                            new FiftyTwoFiftyThreeWeekFiscalYearScheme(
                                referenceObject.LastDayOfWeekInFiscalYear,
                                referenceObject.AnchorMonth,
                                referenceObject.Methodology,
                                A.Dummy<FiftyTwoFiftyThreeWeekFiscalYearNumberingConvention>().ThatIsNot(referenceObject.FiscalYearNumberingConvention)));
                    }

                    var result = new EquatableTestScenario<FiftyTwoFiftyThreeWeekFiscalYearScheme>
                    {
                        Name = "Default Code Generated Scenario",
                        ReferenceObject = referenceObject,
                        ObjectsThatAreEqualToButNotTheSameAsReferenceObject = new[]
                        {
                            new FiftyTwoFiftyThreeWeekFiscalYearScheme(
                                referenceObject.LastDayOfWeekInFiscalYear,
                                referenceObject.AnchorMonth,
                                referenceObject.Methodology,
                                referenceObject.FiscalYearNumberingConvention),
                        },
                        ObjectsThatAreNotEqualToReferenceObject = objectsThatAreNotEqualToReferenceObject,
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
                            A.Dummy<MonthEndFiscalQuarterScheme>(),
                            A.Dummy<MonthEndFiscalYearScheme>(),
                        },
                    };

                    return result;
                });
        }

        [Fact]
        public static void GetCalendarDayPeriod___Should_throw_ArgumentNullException___When_parameter_fiscalYear_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<FiftyTwoFiftyThreeWeekFiscalYearScheme>();

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCalendarDayPeriod(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("fiscalYear");
        }

        [Fact]
        public static void GetCalendarDayPeriod___Should_return_reporting_period_that_ends_on_chosen_day_of_week_that_last_occurs_in_anchor_month_and_starts_on_day_after_last_years_ending_date___When_methodology_is_LastOccurrenceInAnchorMonth_and_FiscalYearNumberingConvention_is_CalendarYearOfAnchorMonth()
        {
            // Arrange
            var systemUnderTest = new FiftyTwoFiftyThreeWeekFiscalYearScheme(DayOfWeek.Saturday, MonthOfYear.August, FiftyTwoFiftyThreeWeekMethodology.LastOccurrenceInAnchorMonth);

            var expectedReportingPeriod1 = new ReportingPeriod(27.August(2006).ToCalendarDay(), 25.August(2007).ToCalendarDay());
            var expectedReportingPeriod2 = new ReportingPeriod(26.August(2007).ToCalendarDay(), 30.August(2008).ToCalendarDay());
            var expectedReportingPeriod3 = new ReportingPeriod(31.August(2008).ToCalendarDay(), 29.August(2009).ToCalendarDay());
            var expectedReportingPeriod4 = new ReportingPeriod(30.August(2009).ToCalendarDay(), 28.August(2010).ToCalendarDay());
            var expectedReportingPeriod5 = new ReportingPeriod(29.August(2010).ToCalendarDay(), 27.August(2011).ToCalendarDay());
            var expectedReportingPeriod6 = new ReportingPeriod(28.August(2011).ToCalendarDay(), 25.August(2012).ToCalendarDay());
            var expectedReportingPeriod7 = new ReportingPeriod(26.August(2012).ToCalendarDay(), 31.August(2013).ToCalendarDay());
            var expectedReportingPeriod8 = new ReportingPeriod(1.September(2013).ToCalendarDay(), 30.August(2014).ToCalendarDay());
            var expectedReportingPeriod9 = new ReportingPeriod(31.August(2014).ToCalendarDay(), 29.August(2015).ToCalendarDay());
            var expectedReportingPeriod10 = new ReportingPeriod(30.August(2015).ToCalendarDay(), 27.August(2016).ToCalendarDay());
            var expectedReportingPeriod11 = new ReportingPeriod(28.August(2016).ToCalendarDay(), 26.August(2017).ToCalendarDay());
            var expectedReportingPeriod12 = new ReportingPeriod(27.August(2017).ToCalendarDay(), 25.August(2018).ToCalendarDay());
            var expectedReportingPeriod13 = new ReportingPeriod(26.August(2018).ToCalendarDay(), 31.August(2019).ToCalendarDay());

            // Act
            var actualReportingPeriod1 = systemUnderTest.GetCalendarDayPeriod(new FiscalYear(2007));
            var actualReportingPeriod2 = systemUnderTest.GetCalendarDayPeriod(new FiscalYear(2008));
            var actualReportingPeriod3 = systemUnderTest.GetCalendarDayPeriod(new FiscalYear(2009));
            var actualReportingPeriod4 = systemUnderTest.GetCalendarDayPeriod(new FiscalYear(2010));
            var actualReportingPeriod5 = systemUnderTest.GetCalendarDayPeriod(new FiscalYear(2011));
            var actualReportingPeriod6 = systemUnderTest.GetCalendarDayPeriod(new FiscalYear(2012));
            var actualReportingPeriod7 = systemUnderTest.GetCalendarDayPeriod(new FiscalYear(2013));
            var actualReportingPeriod8 = systemUnderTest.GetCalendarDayPeriod(new FiscalYear(2014));
            var actualReportingPeriod9 = systemUnderTest.GetCalendarDayPeriod(new FiscalYear(2015));
            var actualReportingPeriod10 = systemUnderTest.GetCalendarDayPeriod(new FiscalYear(2016));
            var actualReportingPeriod11 = systemUnderTest.GetCalendarDayPeriod(new FiscalYear(2017));
            var actualReportingPeriod12 = systemUnderTest.GetCalendarDayPeriod(new FiscalYear(2018));
            var actualReportingPeriod13 = systemUnderTest.GetCalendarDayPeriod(new FiscalYear(2019));

            // Assert
            actualReportingPeriod1.Should().Be(expectedReportingPeriod1);
            actualReportingPeriod2.Should().Be(expectedReportingPeriod2);
            actualReportingPeriod3.Should().Be(expectedReportingPeriod3);
            actualReportingPeriod4.Should().Be(expectedReportingPeriod4);
            actualReportingPeriod5.Should().Be(expectedReportingPeriod5);
            actualReportingPeriod6.Should().Be(expectedReportingPeriod6);
            actualReportingPeriod7.Should().Be(expectedReportingPeriod7);
            actualReportingPeriod8.Should().Be(expectedReportingPeriod8);
            actualReportingPeriod9.Should().Be(expectedReportingPeriod9);
            actualReportingPeriod10.Should().Be(expectedReportingPeriod10);
            actualReportingPeriod11.Should().Be(expectedReportingPeriod11);
            actualReportingPeriod12.Should().Be(expectedReportingPeriod12);
            actualReportingPeriod13.Should().Be(expectedReportingPeriod13);
        }

        [Fact]
        public static void GetCalendarDayPeriod___Should_return_reporting_period_that_ends_on_chosen_day_of_week_that_is_nearest_to_last_day_of_anchor_month_and_starts_on_day_after_last_years_ending_date___When_methodology_is_ClosestToLastDayOfAnchorMonth_and_FiscalYearNumberingConvention_is_CalendarYearOfAnchorMonth()
        {
            // Arrange
            var systemUnderTest = new FiftyTwoFiftyThreeWeekFiscalYearScheme(DayOfWeek.Saturday, MonthOfYear.August, FiftyTwoFiftyThreeWeekMethodology.ClosestToLastDayOfAnchorMonth);

            var expectedReportingPeriod1 = new ReportingPeriod(3.September(2006).ToCalendarDay(), 1.September(2007).ToCalendarDay());
            var expectedReportingPeriod2 = new ReportingPeriod(2.September(2007).ToCalendarDay(), 30.August(2008).ToCalendarDay());
            var expectedReportingPeriod3 = new ReportingPeriod(31.August(2008).ToCalendarDay(), 29.August(2009).ToCalendarDay());
            var expectedReportingPeriod4 = new ReportingPeriod(30.August(2009).ToCalendarDay(), 28.August(2010).ToCalendarDay());
            var expectedReportingPeriod5 = new ReportingPeriod(29.August(2010).ToCalendarDay(), 3.September(2011).ToCalendarDay());
            var expectedReportingPeriod6 = new ReportingPeriod(4.September(2011).ToCalendarDay(), 1.September(2012).ToCalendarDay());
            var expectedReportingPeriod7 = new ReportingPeriod(2.September(2012).ToCalendarDay(), 31.August(2013).ToCalendarDay());
            var expectedReportingPeriod8 = new ReportingPeriod(1.September(2013).ToCalendarDay(), 30.August(2014).ToCalendarDay());
            var expectedReportingPeriod9 = new ReportingPeriod(31.August(2014).ToCalendarDay(), 29.August(2015).ToCalendarDay());
            var expectedReportingPeriod10 = new ReportingPeriod(30.August(2015).ToCalendarDay(), 3.September(2016).ToCalendarDay());
            var expectedReportingPeriod11 = new ReportingPeriod(4.September(2016).ToCalendarDay(), 2.September(2017).ToCalendarDay());
            var expectedReportingPeriod12 = new ReportingPeriod(3.September(2017).ToCalendarDay(), 1.September(2018).ToCalendarDay());
            var expectedReportingPeriod13 = new ReportingPeriod(2.September(2018).ToCalendarDay(), 31.August(2019).ToCalendarDay());

            // Act
            var actualReportingPeriod1 = systemUnderTest.GetCalendarDayPeriod(new FiscalYear(2007));
            var actualReportingPeriod2 = systemUnderTest.GetCalendarDayPeriod(new FiscalYear(2008));
            var actualReportingPeriod3 = systemUnderTest.GetCalendarDayPeriod(new FiscalYear(2009));
            var actualReportingPeriod4 = systemUnderTest.GetCalendarDayPeriod(new FiscalYear(2010));
            var actualReportingPeriod5 = systemUnderTest.GetCalendarDayPeriod(new FiscalYear(2011));
            var actualReportingPeriod6 = systemUnderTest.GetCalendarDayPeriod(new FiscalYear(2012));
            var actualReportingPeriod7 = systemUnderTest.GetCalendarDayPeriod(new FiscalYear(2013));
            var actualReportingPeriod8 = systemUnderTest.GetCalendarDayPeriod(new FiscalYear(2014));
            var actualReportingPeriod9 = systemUnderTest.GetCalendarDayPeriod(new FiscalYear(2015));
            var actualReportingPeriod10 = systemUnderTest.GetCalendarDayPeriod(new FiscalYear(2016));
            var actualReportingPeriod11 = systemUnderTest.GetCalendarDayPeriod(new FiscalYear(2017));
            var actualReportingPeriod12 = systemUnderTest.GetCalendarDayPeriod(new FiscalYear(2018));
            var actualReportingPeriod13 = systemUnderTest.GetCalendarDayPeriod(new FiscalYear(2019));

            // Assert
            actualReportingPeriod1.Should().Be(expectedReportingPeriod1);
            actualReportingPeriod2.Should().Be(expectedReportingPeriod2);
            actualReportingPeriod3.Should().Be(expectedReportingPeriod3);
            actualReportingPeriod4.Should().Be(expectedReportingPeriod4);
            actualReportingPeriod5.Should().Be(expectedReportingPeriod5);
            actualReportingPeriod6.Should().Be(expectedReportingPeriod6);
            actualReportingPeriod7.Should().Be(expectedReportingPeriod7);
            actualReportingPeriod8.Should().Be(expectedReportingPeriod8);
            actualReportingPeriod9.Should().Be(expectedReportingPeriod9);
            actualReportingPeriod10.Should().Be(expectedReportingPeriod10);
            actualReportingPeriod11.Should().Be(expectedReportingPeriod11);
            actualReportingPeriod12.Should().Be(expectedReportingPeriod12);
            actualReportingPeriod13.Should().Be(expectedReportingPeriod13);
        }

        [Fact]
        public static void GetCalendarDayPeriod___Should_return_reporting_period_whose_anchor_month_is_in_the_calendar_year_after_the_fiscal_year___When_FiscalYearNumberingConvention_is_CalendarYearPriorToAnchorMonth()
        {
            // Arrange
            var target = new FiftyTwoFiftyThreeWeekFiscalYearScheme(DayOfWeek.Saturday, MonthOfYear.January, FiftyTwoFiftyThreeWeekMethodology.ClosestToLastDayOfAnchorMonth, FiftyTwoFiftyThreeWeekFiscalYearNumberingConvention.CalendarYearPriorToAnchorMonth);

            var homeDepot = new FiftyTwoFiftyThreeWeekFiscalYearScheme(DayOfWeek.Sunday, MonthOfYear.January, FiftyTwoFiftyThreeWeekMethodology.ClosestToLastDayOfAnchorMonth, FiftyTwoFiftyThreeWeekFiscalYearNumberingConvention.CalendarYearPriorToAnchorMonth);

            var systemUnderTestAndExpected = new[]
            {
                // 53-week year
                new
                {
                    SystemUnderTest = target,
                    FiscalYear = new FiscalYear(2023),
                    Expected = new ReportingPeriod(29.January(2023).ToCalendarDay(), 3.February(2024).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = target,
                    FiscalYear = new FiscalYear(2024),
                    Expected = new ReportingPeriod(4.February(2024).ToCalendarDay(), 1.February(2025).ToCalendarDay()),
                },
                new
                {
                    SystemUnderTest = homeDepot,
                    FiscalYear = new FiscalYear(2023),
                    Expected = new ReportingPeriod(30.January(2023).ToCalendarDay(), 28.January(2024).ToCalendarDay()),
                },

                // 53-week year
                new
                {
                    SystemUnderTest = homeDepot,
                    FiscalYear = new FiscalYear(2024),
                    Expected = new ReportingPeriod(29.January(2024).ToCalendarDay(), 2.February(2025).ToCalendarDay()),
                },
            };

            var expected = systemUnderTestAndExpected.Select(_ => _.Expected).ToList();

            // Act
            var actual = systemUnderTestAndExpected.Select(_ => _.SystemUnderTest.GetCalendarDayPeriod(_.FiscalYear)).ToList();

            // Assert
            actual.Should().Equal(expected);
        }

        [Fact]
        public static void GetCalendarDayPeriod___Should_return_reporting_period_whose_anchor_month_is_in_the_same_calendar_year_as_the_fiscal_year___When_FiscalYearNumberingConvention_is_CalendarYearOfAnchorMonth()
        {
            // Arrange
            var bestBuy = new FiftyTwoFiftyThreeWeekFiscalYearScheme(DayOfWeek.Saturday, MonthOfYear.January, FiftyTwoFiftyThreeWeekMethodology.ClosestToLastDayOfAnchorMonth, FiftyTwoFiftyThreeWeekFiscalYearNumberingConvention.CalendarYearOfAnchorMonth);

            var expected = new ReportingPeriod(4.February(2024).ToCalendarDay(), 1.February(2025).ToCalendarDay());

            // Act
            var actual = bestBuy.GetCalendarDayPeriod(new FiscalYear(2025));

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public static void GetCalendarDayPeriod___Should_return_same_reporting_period_as_next_fiscal_year_numbered_by_CalendarYearOfAnchorMonth___When_FiscalYearNumberingConvention_is_CalendarYearPriorToAnchorMonth()
        {
            // Arrange
            var year = A.Dummy<DateTime>().ThatIs(_ => (_.Year > 1) && (_.Year < 9999)).Year;

            var lastDayOfWeekInFiscalYear = A.Dummy<DayOfWeek>();

            var anchorMonthsAndMethodologies = EnumExtensions.GetDefinedEnumValues<MonthOfYear>()
                .Where(_ => (_ != MonthOfYear.Invalid) && (_ != MonthOfYear.December))
                .SelectMany(_ => new[]
                {
                    new { AnchorMonth = _, Methodology = FiftyTwoFiftyThreeWeekMethodology.LastOccurrenceInAnchorMonth },
                    new { AnchorMonth = _, Methodology = FiftyTwoFiftyThreeWeekMethodology.ClosestToLastDayOfAnchorMonth },
                })
                .ToList();

            var expected = anchorMonthsAndMethodologies
                .Select(_ => new FiftyTwoFiftyThreeWeekFiscalYearScheme(lastDayOfWeekInFiscalYear, _.AnchorMonth, _.Methodology, FiftyTwoFiftyThreeWeekFiscalYearNumberingConvention.CalendarYearOfAnchorMonth).GetCalendarDayPeriod(new FiscalYear(year + 1)))
                .ToList();

            // Act
            var actual = anchorMonthsAndMethodologies
                .Select(_ => new FiftyTwoFiftyThreeWeekFiscalYearScheme(lastDayOfWeekInFiscalYear, _.AnchorMonth, _.Methodology, FiftyTwoFiftyThreeWeekFiscalYearNumberingConvention.CalendarYearPriorToAnchorMonth).GetCalendarDayPeriod(new FiscalYear(year)))
                .ToList();

            // Assert
            actual.Should().Equal(expected);
        }

        private static bool IsValidCombination(
            MonthOfYear anchorMonth,
            FiftyTwoFiftyThreeWeekFiscalYearNumberingConvention fiscalYearNumberingConvention)
        {
            var result = (anchorMonth != MonthOfYear.December) || (fiscalYearNumberingConvention != FiftyTwoFiftyThreeWeekFiscalYearNumberingConvention.CalendarYearPriorToAnchorMonth);

            return result;
        }
    }
}