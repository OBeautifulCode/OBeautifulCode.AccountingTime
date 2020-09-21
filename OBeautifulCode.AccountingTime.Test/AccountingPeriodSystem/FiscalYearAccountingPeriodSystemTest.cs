// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FiscalYearAccountingPeriodSystemTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Test
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using FakeItEasy;

    using FluentAssertions;
    using FluentAssertions.Extensions;

    using OBeautifulCode.AutoFakeItEasy;
    using OBeautifulCode.CodeAnalysis.Recipes;
    using OBeautifulCode.CodeGen.ModelObject.Recipes;

    using Xunit;

    public static partial class FiscalYearAccountingPeriodSystemTest
    {
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static FiscalYearAccountingPeriodSystemTest()
        {
            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<FiscalYearAccountingPeriodSystem>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'lastMonthInFiscalYear' is MonthOfYear.Invalid",
                        ConstructionFunc = () => new FiscalYearAccountingPeriodSystem(MonthOfYear.Invalid),
                        ExpectedExceptionMessageContains = new[] { "lastMonthInFiscalYear", "Invalid" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<FiscalYearAccountingPeriodSystem>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'lastMonthInFiscalYear' is MonthOfYear.December",
                        ConstructionFunc = () => new FiscalYearAccountingPeriodSystem(MonthOfYear.December),
                        ExpectedExceptionMessageContains = new[] { "lastMonthInFiscalYear", "December" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    });
        }

        [Fact]
        public static void GetReportingPeriodForFiscalYear___Should_return_reporting_period_beginning_first_of_the_month_after_LastMonthInFiscalYear_in_the_prior_year_and_ending_last_day_of_LastMonthInFiscalYear___When_called()
        {
            // Arrange
            var nonLeapYear = A.Dummy<DateTime>().ThatIs(_ => !DateTime.IsLeapYear(_.Year)).Year;
            var leapYear = A.Dummy<DateTime>().ThatIs(_ => DateTime.IsLeapYear(_.Year)).Year;

            var systemUnderTest1 = new FiscalYearAccountingPeriodSystem(MonthOfYear.January);
            var systemUnderTest2 = new FiscalYearAccountingPeriodSystem(MonthOfYear.February);
            var systemUnderTest3 = new FiscalYearAccountingPeriodSystem(MonthOfYear.March);
            var systemUnderTest4 = new FiscalYearAccountingPeriodSystem(MonthOfYear.April);
            var systemUnderTest5 = new FiscalYearAccountingPeriodSystem(MonthOfYear.May);
            var systemUnderTest6 = new FiscalYearAccountingPeriodSystem(MonthOfYear.June);
            var systemUnderTest7 = new FiscalYearAccountingPeriodSystem(MonthOfYear.July);
            var systemUnderTest8 = new FiscalYearAccountingPeriodSystem(MonthOfYear.August);
            var systemUnderTest9 = new FiscalYearAccountingPeriodSystem(MonthOfYear.September);
            var systemUnderTest10 = new FiscalYearAccountingPeriodSystem(MonthOfYear.October);
            var systemUnderTest11 = new FiscalYearAccountingPeriodSystem(MonthOfYear.November);

            var expectedReportingPeriod1NonLeapYear = new ReportingPeriod(1.February(nonLeapYear - 1).ToCalendarDay(), 31.January(nonLeapYear).ToCalendarDay());
            var expectedReportingPeriod2NonLeapYear = new ReportingPeriod(1.March(nonLeapYear - 1).ToCalendarDay(), 28.February(nonLeapYear).ToCalendarDay());
            var expectedReportingPeriod3NonLeapYear = new ReportingPeriod(1.April(nonLeapYear - 1).ToCalendarDay(), 31.March(nonLeapYear).ToCalendarDay());
            var expectedReportingPeriod4NonLeapYear = new ReportingPeriod(1.May(nonLeapYear - 1).ToCalendarDay(), 30.April(nonLeapYear).ToCalendarDay());
            var expectedReportingPeriod5NonLeapYear = new ReportingPeriod(1.June(nonLeapYear - 1).ToCalendarDay(), 31.May(nonLeapYear).ToCalendarDay());
            var expectedReportingPeriod6NonLeapYear = new ReportingPeriod(1.July(nonLeapYear - 1).ToCalendarDay(), 30.June(nonLeapYear).ToCalendarDay());
            var expectedReportingPeriod7NonLeapYear = new ReportingPeriod(1.August(nonLeapYear - 1).ToCalendarDay(), 31.July(nonLeapYear).ToCalendarDay());
            var expectedReportingPeriod8NonLeapYear = new ReportingPeriod(1.September(nonLeapYear - 1).ToCalendarDay(), 31.August(nonLeapYear).ToCalendarDay());
            var expectedReportingPeriod9NonLeapYear = new ReportingPeriod(1.October(nonLeapYear - 1).ToCalendarDay(), 30.September(nonLeapYear).ToCalendarDay());
            var expectedReportingPeriod10NonLeapYear = new ReportingPeriod(1.November(nonLeapYear - 1).ToCalendarDay(), 31.October(nonLeapYear).ToCalendarDay());
            var expectedReportingPeriod11NonLeapYear = new ReportingPeriod(1.December(nonLeapYear - 1).ToCalendarDay(), 30.November(nonLeapYear).ToCalendarDay());

            var expectedReportingPeriod1LeapYear = new ReportingPeriod(1.February(leapYear - 1).ToCalendarDay(), 31.January(leapYear).ToCalendarDay());
            var expectedReportingPeriod2LeapYear = new ReportingPeriod(1.March(leapYear - 1).ToCalendarDay(), 29.February(leapYear).ToCalendarDay());
            var expectedReportingPeriod3LeapYear = new ReportingPeriod(1.April(leapYear - 1).ToCalendarDay(), 31.March(leapYear).ToCalendarDay());
            var expectedReportingPeriod4LeapYear = new ReportingPeriod(1.May(leapYear - 1).ToCalendarDay(), 30.April(leapYear).ToCalendarDay());
            var expectedReportingPeriod5LeapYear = new ReportingPeriod(1.June(leapYear - 1).ToCalendarDay(), 31.May(leapYear).ToCalendarDay());
            var expectedReportingPeriod6LeapYear = new ReportingPeriod(1.July(leapYear - 1).ToCalendarDay(), 30.June(leapYear).ToCalendarDay());
            var expectedReportingPeriod7LeapYear = new ReportingPeriod(1.August(leapYear - 1).ToCalendarDay(), 31.July(leapYear).ToCalendarDay());
            var expectedReportingPeriod8LeapYear = new ReportingPeriod(1.September(leapYear - 1).ToCalendarDay(), 31.August(leapYear).ToCalendarDay());
            var expectedReportingPeriod9LeapYear = new ReportingPeriod(1.October(leapYear - 1).ToCalendarDay(), 30.September(leapYear).ToCalendarDay());
            var expectedReportingPeriod10LeapYear = new ReportingPeriod(1.November(leapYear - 1).ToCalendarDay(), 31.October(leapYear).ToCalendarDay());
            var expectedReportingPeriod11LeapYear = new ReportingPeriod(1.December(leapYear - 1).ToCalendarDay(), 30.November(leapYear).ToCalendarDay());

            // Act
            var actualReportingPeriod1NonLeapYear = systemUnderTest1.GetReportingPeriodForFiscalYear(new FiscalYear(nonLeapYear));
            var actualReportingPeriod2NonLeapYear = systemUnderTest2.GetReportingPeriodForFiscalYear(new FiscalYear(nonLeapYear));
            var actualReportingPeriod3NonLeapYear = systemUnderTest3.GetReportingPeriodForFiscalYear(new FiscalYear(nonLeapYear));
            var actualReportingPeriod4NonLeapYear = systemUnderTest4.GetReportingPeriodForFiscalYear(new FiscalYear(nonLeapYear));
            var actualReportingPeriod5NonLeapYear = systemUnderTest5.GetReportingPeriodForFiscalYear(new FiscalYear(nonLeapYear));
            var actualReportingPeriod6NonLeapYear = systemUnderTest6.GetReportingPeriodForFiscalYear(new FiscalYear(nonLeapYear));
            var actualReportingPeriod7NonLeapYear = systemUnderTest7.GetReportingPeriodForFiscalYear(new FiscalYear(nonLeapYear));
            var actualReportingPeriod8NonLeapYear = systemUnderTest8.GetReportingPeriodForFiscalYear(new FiscalYear(nonLeapYear));
            var actualReportingPeriod9NonLeapYear = systemUnderTest9.GetReportingPeriodForFiscalYear(new FiscalYear(nonLeapYear));
            var actualReportingPeriod10NonLeapYear = systemUnderTest10.GetReportingPeriodForFiscalYear(new FiscalYear(nonLeapYear));
            var actualReportingPeriod11NonLeapYear = systemUnderTest11.GetReportingPeriodForFiscalYear(new FiscalYear(nonLeapYear));

            var actualReportingPeriod1LeapYear = systemUnderTest1.GetReportingPeriodForFiscalYear(new FiscalYear(leapYear));
            var actualReportingPeriod2LeapYear = systemUnderTest2.GetReportingPeriodForFiscalYear(new FiscalYear(leapYear));
            var actualReportingPeriod3LeapYear = systemUnderTest3.GetReportingPeriodForFiscalYear(new FiscalYear(leapYear));
            var actualReportingPeriod4LeapYear = systemUnderTest4.GetReportingPeriodForFiscalYear(new FiscalYear(leapYear));
            var actualReportingPeriod5LeapYear = systemUnderTest5.GetReportingPeriodForFiscalYear(new FiscalYear(leapYear));
            var actualReportingPeriod6LeapYear = systemUnderTest6.GetReportingPeriodForFiscalYear(new FiscalYear(leapYear));
            var actualReportingPeriod7LeapYear = systemUnderTest7.GetReportingPeriodForFiscalYear(new FiscalYear(leapYear));
            var actualReportingPeriod8LeapYear = systemUnderTest8.GetReportingPeriodForFiscalYear(new FiscalYear(leapYear));
            var actualReportingPeriod9LeapYear = systemUnderTest9.GetReportingPeriodForFiscalYear(new FiscalYear(leapYear));
            var actualReportingPeriod10LeapYear = systemUnderTest10.GetReportingPeriodForFiscalYear(new FiscalYear(leapYear));
            var actualReportingPeriod11LeapYear = systemUnderTest11.GetReportingPeriodForFiscalYear(new FiscalYear(leapYear));

            // Assert
            actualReportingPeriod1NonLeapYear.Should().Be(expectedReportingPeriod1NonLeapYear);
            actualReportingPeriod2NonLeapYear.Should().Be(expectedReportingPeriod2NonLeapYear);
            actualReportingPeriod3NonLeapYear.Should().Be(expectedReportingPeriod3NonLeapYear);
            actualReportingPeriod4NonLeapYear.Should().Be(expectedReportingPeriod4NonLeapYear);
            actualReportingPeriod5NonLeapYear.Should().Be(expectedReportingPeriod5NonLeapYear);
            actualReportingPeriod6NonLeapYear.Should().Be(expectedReportingPeriod6NonLeapYear);
            actualReportingPeriod7NonLeapYear.Should().Be(expectedReportingPeriod7NonLeapYear);
            actualReportingPeriod8NonLeapYear.Should().Be(expectedReportingPeriod8NonLeapYear);
            actualReportingPeriod9NonLeapYear.Should().Be(expectedReportingPeriod9NonLeapYear);
            actualReportingPeriod10NonLeapYear.Should().Be(expectedReportingPeriod10NonLeapYear);
            actualReportingPeriod11NonLeapYear.Should().Be(expectedReportingPeriod11NonLeapYear);

            actualReportingPeriod1LeapYear.Should().Be(expectedReportingPeriod1LeapYear);
            actualReportingPeriod2LeapYear.Should().Be(expectedReportingPeriod2LeapYear);
            actualReportingPeriod3LeapYear.Should().Be(expectedReportingPeriod3LeapYear);
            actualReportingPeriod4LeapYear.Should().Be(expectedReportingPeriod4LeapYear);
            actualReportingPeriod5LeapYear.Should().Be(expectedReportingPeriod5LeapYear);
            actualReportingPeriod6LeapYear.Should().Be(expectedReportingPeriod6LeapYear);
            actualReportingPeriod7LeapYear.Should().Be(expectedReportingPeriod7LeapYear);
            actualReportingPeriod8LeapYear.Should().Be(expectedReportingPeriod8LeapYear);
            actualReportingPeriod9LeapYear.Should().Be(expectedReportingPeriod9LeapYear);
            actualReportingPeriod10LeapYear.Should().Be(expectedReportingPeriod10LeapYear);
            actualReportingPeriod11LeapYear.Should().Be(expectedReportingPeriod11LeapYear);
        }
    }
}