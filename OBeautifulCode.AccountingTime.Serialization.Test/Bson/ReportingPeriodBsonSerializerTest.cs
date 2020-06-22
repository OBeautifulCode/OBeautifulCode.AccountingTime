// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriodBsonSerializerTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Serialization.Test
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization;

    using OBeautifulCode.Assertion.Recipes;

    using Xunit;

    public static class ReportingPeriodBsonSerializerTest
    {
        [Fact]
        public static void ReportingPeriodModelWrapper_with_nulls___Should_roundtrip_to_bson_and_back___When_using_ReportingPeriodBsonSerializer()
        {
            // Arrange
            var expected = new ReportingPeriodModelWrapper();

            var bson = expected.ToBsonDocument();

            // Act
            var actual = BsonSerializer.Deserialize<ReportingPeriodModelWrapper>(bson);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void ReportingPeriodModelWrapper_without_nulls___Should_roundtrip_to_bson_and_back___When_using_ReportingPeriodBsonSerializer()
        {
            // Arrange
            var expected = TestModels.GetDummyReportingPeriodModelWrapper();

            var bson = expected.ToBsonDocument();

            // Act
            var actual = BsonSerializer.Deserialize<ReportingPeriodModelWrapper>(bson);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void ReportingPeriodModelWrapper_without_nulls___Should_serialize_to_ReportingPeriodPersistenceModel_representation_of_ReportingPeriod___When_using_ReportingPeriodBsonSerializer()
        {
            // Arrange
            var model = TestModels.GetDummyReportingPeriodModelWrapper();

            var expected =
                "\"" + nameof(model.UnitOfTime) + "\" : { \"Start\" : \"" + model.UnitOfTime.Start.SerializeToSortableString() + "\", \"End\" : \"" + model.UnitOfTime.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model.CalendarUnitOfTime) + "\" : { \"Start\" : \"" + model.CalendarUnitOfTime.Start.SerializeToSortableString() + "\", \"End\" : \"" + model.CalendarUnitOfTime.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model.CalendarDay) + "\" : { \"Start\" : \"" + model.CalendarDay.Start.SerializeToSortableString() + "\", \"End\" : \"" + model.CalendarDay.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model.CalendarMonth) + "\" : { \"Start\" : \"" + model.CalendarMonth.Start.SerializeToSortableString() + "\", \"End\" : \"" + model.CalendarMonth.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model.CalendarQuarter) + "\" : { \"Start\" : \"" + model.CalendarQuarter.Start.SerializeToSortableString() + "\", \"End\" : \"" + model.CalendarQuarter.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model.CalendarYear) + "\" : { \"Start\" : \"" + model.CalendarYear.Start.SerializeToSortableString() + "\", \"End\" : \"" + model.CalendarYear.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model.CalendarUnbounded) + "\" : { \"Start\" : \"" + model.CalendarUnbounded.Start.SerializeToSortableString() + "\", \"End\" : \"" + model.CalendarUnbounded.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model.FiscalUnitOfTime) + "\" : { \"Start\" : \"" + model.FiscalUnitOfTime.Start.SerializeToSortableString() + "\", \"End\" : \"" + model.FiscalUnitOfTime.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model.FiscalMonth) + "\" : { \"Start\" : \"" + model.FiscalMonth.Start.SerializeToSortableString() + "\", \"End\" : \"" + model.FiscalMonth.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model.FiscalQuarter) + "\" : { \"Start\" : \"" + model.FiscalQuarter.Start.SerializeToSortableString() + "\", \"End\" : \"" + model.FiscalQuarter.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model.FiscalYear) + "\" : { \"Start\" : \"" + model.FiscalYear.Start.SerializeToSortableString() + "\", \"End\" : \"" + model.FiscalYear.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model.FiscalUnbounded) + "\" : { \"Start\" : \"" + model.FiscalUnbounded.Start.SerializeToSortableString() + "\", \"End\" : \"" + model.FiscalUnbounded.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model.GenericUnitOfTime) + "\" : { \"Start\" : \"" + model.GenericUnitOfTime.Start.SerializeToSortableString() + "\", \"End\" : \"" + model.GenericUnitOfTime.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model.GenericMonth) + "\" : { \"Start\" : \"" + model.GenericMonth.Start.SerializeToSortableString() + "\", \"End\" : \"" + model.GenericMonth.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model.GenericQuarter) + "\" : { \"Start\" : \"" + model.GenericQuarter.Start.SerializeToSortableString() + "\", \"End\" : \"" + model.GenericQuarter.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model.GenericYear) + "\" : { \"Start\" : \"" + model.GenericYear.Start.SerializeToSortableString() + "\", \"End\" : \"" + model.GenericYear.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model.GenericUnbounded) + "\" : { \"Start\" : \"" + model.GenericUnbounded.Start.SerializeToSortableString() + "\", \"End\" : \"" + model.GenericUnbounded.End.SerializeToSortableString() + "\" }";

            // Act
            var actual = model.ToJson();

            // Assert
            actual.AsTest().Must().ContainString(expected);
        }
    }
}
