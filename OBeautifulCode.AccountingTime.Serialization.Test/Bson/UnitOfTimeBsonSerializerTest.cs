// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfTimeBsonSerializerTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Serialization.Test
{
    using FakeItEasy;

    using MongoDB.Bson;
    using MongoDB.Bson.Serialization;

    using OBeautifulCode.Assertion.Recipes;

    using Xunit;

    public static class UnitOfTimeBsonSerializerTest
    {
        [Fact]
        public static void UnitOfTimeModelWrapper_with_nulls___Should_roundtrip_to_bson_and_back___When_using_UnitOfTimeStringSerializedBackedRegisteredTypeBsonSerializer_of_TValue()
        {
            // Arrange
            var expected = new UnitOfTimeModelWrapper();

            var bson = expected.ToBsonDocument();

            // Act
            var actual = BsonSerializer.Deserialize<UnitOfTimeModelWrapper>(bson);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void UnitOfTimeModelWrapper_without_nulls___Should_roundtrip_to_bson_and_back___When_using_UnitOfTimeStringSerializedBackedRegisteredTypeBsonSerializer_of_TValue()
        {
            // Arrange
            var expected = A.Dummy<UnitOfTimeModelWrapper>();

            var bson = expected.ToBsonDocument();

            // Act
            var actual = BsonSerializer.Deserialize<UnitOfTimeModelWrapper>(bson);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void UnitOfTimeModelWrapper_without_nulls___Should_serialize_to_sortable_string_representation_of_UnitOfTime___When_using_UnitOfTimeStringSerializedBackedRegisteredTypeBsonSerializer_of_TValue()
        {
            // Arrange
            var model = A.Dummy<UnitOfTimeModelWrapper>();

            var expected =
                "\"" + nameof(model.UnitOfTime) + "\" : \"" + model.UnitOfTime.SerializeToSortableString() + "\", " +
                "\"" + nameof(model.CalendarUnitOfTime) + "\" : \"" + model.CalendarUnitOfTime.SerializeToSortableString() + "\", " +
                "\"" + nameof(model.CalendarDay) + "\" : \"" + model.CalendarDay.SerializeToSortableString() + "\", " +
                "\"" + nameof(model.CalendarMonth) + "\" : \"" + model.CalendarMonth.SerializeToSortableString() + "\", " +
                "\"" + nameof(model.CalendarQuarter) + "\" : \"" + model.CalendarQuarter.SerializeToSortableString() + "\", " +
                "\"" + nameof(model.CalendarYear) + "\" : \"" + model.CalendarYear.SerializeToSortableString() + "\", " +
                "\"" + nameof(model.CalendarUnbounded) + "\" : \"" + model.CalendarUnbounded.SerializeToSortableString() + "\", " +
                "\"" + nameof(model.FiscalUnitOfTime) + "\" : \"" + model.FiscalUnitOfTime.SerializeToSortableString() + "\", " +
                "\"" + nameof(model.FiscalMonth) + "\" : \"" + model.FiscalMonth.SerializeToSortableString() + "\", " +
                "\"" + nameof(model.FiscalQuarter) + "\" : \"" + model.FiscalQuarter.SerializeToSortableString() + "\", " +
                "\"" + nameof(model.FiscalYear) + "\" : \"" + model.FiscalYear.SerializeToSortableString() + "\", " +
                "\"" + nameof(model.FiscalUnbounded) + "\" : \"" + model.FiscalUnbounded.SerializeToSortableString() + "\", " +
                "\"" + nameof(model.GenericUnitOfTime) + "\" : \"" + model.GenericUnitOfTime.SerializeToSortableString() + "\", " +
                "\"" + nameof(model.GenericMonth) + "\" : \"" + model.GenericMonth.SerializeToSortableString() + "\", " +
                "\"" + nameof(model.GenericQuarter) + "\" : \"" + model.GenericQuarter.SerializeToSortableString() + "\", " +
                "\"" + nameof(model.GenericYear) + "\" : \"" + model.GenericYear.SerializeToSortableString() + "\", " +
                "\"" + nameof(model.GenericUnbounded) + "\" : \"" + model.GenericUnbounded.SerializeToSortableString() + "\"";

            // Act
            var actual = model.ToJson();

            // Assert
            actual.AsTest().Must().ContainString(expected);
        }
    }
}
