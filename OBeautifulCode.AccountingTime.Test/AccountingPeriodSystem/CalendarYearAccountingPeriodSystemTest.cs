// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalendarYearAccountingPeriodSystemTest.cs" company="OBeautifulCode">
//    Copyright (c) OBeautifulCode 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Test
{
    using FakeItEasy;

    using FluentAssertions;

    using Newtonsoft.Json;

    using Spritely.Recipes;

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

        [Fact]
        public static void Deserialize___Should_return_object_of_type_CalendarYearAccountingPeriodSystem___When_an_object_of_that_type_is_serialized_to_json_and_deserialized_as_AccountingPeriodSystem()
        {
            // Arrange
            var settings = JsonConfiguration.DefaultSerializerSettings;
            var originalAccountingPeriodSystem = new CalendarYearAccountingPeriodSystem();
            var serializedJson = JsonConvert.SerializeObject(originalAccountingPeriodSystem, settings);

            // Act
            var systemUnderTest = JsonConvert.DeserializeObject<AccountingPeriodSystem>(serializedJson, settings);

            // Assert
            systemUnderTest.Should().BeOfType<CalendarYearAccountingPeriodSystem>();
        }
    }
}