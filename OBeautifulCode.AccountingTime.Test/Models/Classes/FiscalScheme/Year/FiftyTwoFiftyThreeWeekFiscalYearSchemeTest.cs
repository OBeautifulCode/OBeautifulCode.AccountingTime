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
        public static void GetCalendarDayPeriod___Should_return_reporting_period_that_ends_on_chosen_day_of_week_that_last_occurs_in_anchor_month_and_starts_on_day_after_last_years_ending_date___When_methodology_is_LastOccurrenceInAnchorMonth()
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
        public static void GetCalendarDayPeriod___Should_return_reporting_period_that_ends_on_chosen_day_of_week_that_is_nearest_to_last_day_of_anchor_month_and_starts_on_day_after_last_years_ending_date___When_methodology_is_ClosestToLastDayOfAnchorMonth()
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
    }
}