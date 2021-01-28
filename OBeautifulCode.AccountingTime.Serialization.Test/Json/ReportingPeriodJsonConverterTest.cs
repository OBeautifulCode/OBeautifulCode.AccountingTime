// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriodJsonConverterTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Serialization.Test
{
    using NewtonsoftFork.Json;

    using OBeautifulCode.Assertion.Recipes;

    using Xunit;

    public static class ReportingPeriodJsonConverterTest
    {
        [Fact]
        public static void ReportingPeriodModelWrapper_with_nulls___Should_roundtrip_to_json_and_back___When_using_ReportingPeriodStringSerializerBackedJsonConverter()
        {
            // Arrange
            var expected = new ReportingPeriodModelWrapper();

            var json = JsonConvert.SerializeObject(expected);

            // Act
            var actual = JsonConvert.DeserializeObject<ReportingPeriodModelWrapper>(json);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void ReportingPeriodModelWrapper_without_nulls___Should_roundtrip_to_json_and_back___When_using_ReportingPeriodStringSerializerBackedJsonConverter()
        {
            // Arrange
            var expected = TestModels.GetDummyReportingPeriodModelWrapper();

            var json = JsonConvert.SerializeObject(expected);

            // Act
            var actual = JsonConvert.DeserializeObject<ReportingPeriodModelWrapper>(json);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void ReportingPeriodModelWrapper_without_nulls___Should_serialize_to_string_representation_of_ReportingPeriod___When_using_ReportingPeriodStringSerializerBackedJsonConverter()
        {
            // Arrange
            var model = TestModels.GetDummyReportingPeriodModelWrapper();

            var expected =
                "\"" + nameof(model.UnitOfTime) + "\":\"" + model.UnitOfTime.SerializeToString() + "\"," +
                "\"" + nameof(model.CalendarUnitOfTime) + "\":\"" + model.CalendarUnitOfTime.SerializeToString() + "\"," +
                "\"" + nameof(model.CalendarDay) + "\":\"" + model.CalendarDay.SerializeToString() + "\"," +
                "\"" + nameof(model.CalendarMonth) + "\":\"" + model.CalendarMonth.SerializeToString() + "\"," +
                "\"" + nameof(model.CalendarQuarter) + "\":\"" + model.CalendarQuarter.SerializeToString() + "\"," +
                "\"" + nameof(model.CalendarYear) + "\":\"" + model.CalendarYear.SerializeToString() + "\"," +
                "\"" + nameof(model.CalendarUnbounded) + "\":\"" + model.CalendarUnbounded.SerializeToString() + "\"," +
                "\"" + nameof(model.FiscalUnitOfTime) + "\":\"" + model.FiscalUnitOfTime.SerializeToString() + "\"," +
                "\"" + nameof(model.FiscalMonth) + "\":\"" + model.FiscalMonth.SerializeToString() + "\"," +
                "\"" + nameof(model.FiscalQuarter) + "\":\"" + model.FiscalQuarter.SerializeToString() + "\"," +
                "\"" + nameof(model.FiscalYear) + "\":\"" + model.FiscalYear.SerializeToString() + "\"," +
                "\"" + nameof(model.FiscalUnbounded) + "\":\"" + model.FiscalUnbounded.SerializeToString() + "\"," +
                "\"" + nameof(model.GenericUnitOfTime) + "\":\"" + model.GenericUnitOfTime.SerializeToString() + "\"," +
                "\"" + nameof(model.GenericMonth) + "\":\"" + model.GenericMonth.SerializeToString() + "\"," +
                "\"" + nameof(model.GenericQuarter) + "\":\"" + model.GenericQuarter.SerializeToString() + "\"," +
                "\"" + nameof(model.GenericYear) + "\":\"" + model.GenericYear.SerializeToString() + "\"," +
                "\"" + nameof(model.GenericUnbounded) + "\":\"" + model.GenericUnbounded.SerializeToString() + "\"}";

            // Act
            var actual = JsonConvert.SerializeObject(model);

            // Assert
            actual.AsTest().Must().ContainString(expected);
        }
    }
}
