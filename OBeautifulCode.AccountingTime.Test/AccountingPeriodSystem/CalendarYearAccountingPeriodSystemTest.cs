// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalendarYearAccountingPeriodSystemTest.cs" company="OBeautifulCode">
//    Copyright (c) OBeautifulCode 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Test
{
    using FakeItEasy;

    using FluentAssertions;

    using Xunit;

    public static class CalendarYearAccountingPeriodSystemTest
    {
        [Fact]
        public static void GetReportingPeriodForFiscalYear___Should_return_reporting_period_beginning_January_1_and_ending_December_31_of_specified_year___When_called()
        {
            // Arrange
            var fiscalYear = A.Dummy<FiscalYear>();
            var systemUnderTest = new CalendarYearAccountingPeriodSystem();
            var expectedReportingPeriod = new ReportingPeriod<CalendarDay>(
                1.January(fiscalYear.Year).ToCalendarDay(),
                31.December(fiscalYear.Year).ToCalendarDay());

            // Act
            var actualReportingPeriod = systemUnderTest.GetReportingPeriodForFiscalYear(fiscalYear);

            // Assert
            actualReportingPeriod.Should().Be(expectedReportingPeriod);
        }
    }
}