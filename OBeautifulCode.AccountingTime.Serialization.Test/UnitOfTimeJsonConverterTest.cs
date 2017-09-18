// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfTimeConverterTest.cs" company="OBeautifulCode">
//    Copyright (c) OBeautifulCode 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Serialization.Test
{
    using FakeItEasy;

    using FluentAssertions;

    using Newtonsoft.Json;

    using OBeautifulCode.AccountingTime.Serialization.Json;

    using Xunit;

    public static class UnitOfTimeJsonConverterTest
    {
        [Fact]
        public static void UnitOfTimeModel_without_nulls___Should_roundtrip_to_json_and_back___When_using_UnitOfTimeConverter()
        {
            // Arrange
            var expectedModel = A.Dummy<UnitOfTimeModel>();

            // Act
            var json = JsonConvert.SerializeObject(expectedModel);
            var actualModel = JsonConvert.DeserializeObject<UnitOfTimeModel>(json);

            // Assert
            actualModel.UnitOfTime.Should().Be(expectedModel.UnitOfTime);
            actualModel.CalendarUnitOfTime.Should().Be(expectedModel.CalendarUnitOfTime);
            actualModel.CalendarDay.Should().Be(expectedModel.CalendarDay);
            actualModel.CalendarMonth.Should().Be(expectedModel.CalendarMonth);
            actualModel.CalendarQuarter.Should().Be(expectedModel.CalendarQuarter);
            actualModel.CalendarYear.Should().Be(expectedModel.CalendarYear);
            actualModel.CalendarUnbounded.Should().Be(expectedModel.CalendarUnbounded);
            actualModel.FiscalUnitOfTime.Should().Be(expectedModel.FiscalUnitOfTime);
            actualModel.FiscalMonth.Should().Be(expectedModel.FiscalMonth);
            actualModel.FiscalQuarter.Should().Be(expectedModel.FiscalQuarter);
            actualModel.FiscalYear.Should().Be(expectedModel.FiscalYear);
            actualModel.FiscalUnbounded.Should().Be(expectedModel.FiscalUnbounded);
            actualModel.GenericUnitOfTime.Should().Be(expectedModel.GenericUnitOfTime);
            actualModel.GenericMonth.Should().Be(expectedModel.GenericMonth);
            actualModel.GenericQuarter.Should().Be(expectedModel.GenericQuarter);
            actualModel.GenericYear.Should().Be(expectedModel.GenericYear);
            actualModel.GenericUnbounded.Should().Be(expectedModel.GenericUnbounded);
        }

        [Fact]
        public static void UnitOfTimeModel_with_nulls___Should_roundtrip_to_json_and_back___When_using_UnitOfTimeConverter()
        {
            // Arrange
            var expectedModel = new UnitOfTimeModel();

            // Act
            var json = JsonConvert.SerializeObject(expectedModel);
            var actualModel = JsonConvert.DeserializeObject<UnitOfTimeModel>(json);

            // Assert
            actualModel.UnitOfTime.Should().BeNull();
            actualModel.CalendarUnitOfTime.Should().BeNull();
            actualModel.CalendarDay.Should().BeNull();
            actualModel.CalendarMonth.Should().BeNull();
            actualModel.CalendarQuarter.Should().BeNull();
            actualModel.CalendarYear.Should().BeNull();
            actualModel.CalendarUnbounded.Should().BeNull();
            actualModel.FiscalUnitOfTime.Should().BeNull();
            actualModel.FiscalMonth.Should().BeNull();
            actualModel.FiscalQuarter.Should().BeNull();
            actualModel.FiscalYear.Should().BeNull();
            actualModel.FiscalUnbounded.Should().BeNull();
            actualModel.GenericUnitOfTime.Should().BeNull();
            actualModel.GenericMonth.Should().BeNull();
            actualModel.GenericQuarter.Should().BeNull();
            actualModel.GenericYear.Should().BeNull();
            actualModel.GenericUnbounded.Should().BeNull();
        }

        [Fact]
        public static void UnitOfTimeModel_without_nulls___Should_serialize_to_sortable_string_representation_of_UnitOfTime___When_using_UnitOfTimeConverter()
        {
            // Arrange
            var model = A.Dummy<UnitOfTimeModel>();
            var expectedJson =
                "\"" + nameof(model.UnitOfTime) + "\":\"" + model.UnitOfTime.SerializeToSortableString() + "\"," +
                "\"" + nameof(model.CalendarUnitOfTime) + "\":\"" + model.CalendarUnitOfTime.SerializeToSortableString() + "\"," +
                "\"" + nameof(model.CalendarDay) + "\":\"" + model.CalendarDay.SerializeToSortableString() + "\"," +
                "\"" + nameof(model.CalendarMonth) + "\":\"" + model.CalendarMonth.SerializeToSortableString() + "\"," +
                "\"" + nameof(model.CalendarQuarter) + "\":\"" + model.CalendarQuarter.SerializeToSortableString() + "\"," +
                "\"" + nameof(model.CalendarYear) + "\":\"" + model.CalendarYear.SerializeToSortableString() + "\"," +
                "\"" + nameof(model.CalendarUnbounded) + "\":\"" + model.CalendarUnbounded.SerializeToSortableString() + "\"," +
                "\"" + nameof(model.FiscalUnitOfTime) + "\":\"" + model.FiscalUnitOfTime.SerializeToSortableString() + "\"," +
                "\"" + nameof(model.FiscalMonth) + "\":\"" + model.FiscalMonth.SerializeToSortableString() + "\"," +
                "\"" + nameof(model.FiscalQuarter) + "\":\"" + model.FiscalQuarter.SerializeToSortableString() + "\"," +
                "\"" + nameof(model.FiscalYear) + "\":\"" + model.FiscalYear.SerializeToSortableString() + "\"," +
                "\"" + nameof(model.FiscalUnbounded) + "\":\"" + model.FiscalUnbounded.SerializeToSortableString() + "\"," +
                "\"" + nameof(model.GenericUnitOfTime) + "\":\"" + model.GenericUnitOfTime.SerializeToSortableString() + "\"," +
                "\"" + nameof(model.GenericMonth) + "\":\"" + model.GenericMonth.SerializeToSortableString() + "\"," +
                "\"" + nameof(model.GenericQuarter) + "\":\"" + model.GenericQuarter.SerializeToSortableString() + "\"," +
                "\"" + nameof(model.GenericYear) + "\":\"" + model.GenericYear.SerializeToSortableString() + "\"," +
                "\"" + nameof(model.GenericUnbounded) + "\":\"" + model.GenericUnbounded.SerializeToSortableString() + "\"";

            // Act
            var actualJson = JsonConvert.SerializeObject(model);

            // Assert
            actualJson.Should().Contain(expectedJson);
        }

        private class UnitOfTimeModel
        {
            [JsonConverter(typeof(UnitOfTimeJsonConverter))]
            public UnitOfTime UnitOfTime { get; set; }

            [JsonConverter(typeof(UnitOfTimeJsonConverter))]
            public CalendarUnitOfTime CalendarUnitOfTime { get; set; }

            [JsonConverter(typeof(UnitOfTimeJsonConverter))]
            public CalendarDay CalendarDay { get; set; }

            [JsonConverter(typeof(UnitOfTimeJsonConverter))]
            public CalendarMonth CalendarMonth { get; set; }

            [JsonConverter(typeof(UnitOfTimeJsonConverter))]
            public CalendarQuarter CalendarQuarter { get; set; }

            [JsonConverter(typeof(UnitOfTimeJsonConverter))]
            public CalendarYear CalendarYear { get; set; }

            [JsonConverter(typeof(UnitOfTimeJsonConverter))]
            public CalendarUnbounded CalendarUnbounded { get; set; }

            [JsonConverter(typeof(UnitOfTimeJsonConverter))]
            public FiscalUnitOfTime FiscalUnitOfTime { get; set; }

            [JsonConverter(typeof(UnitOfTimeJsonConverter))]
            public FiscalMonth FiscalMonth { get; set; }

            [JsonConverter(typeof(UnitOfTimeJsonConverter))]
            public FiscalQuarter FiscalQuarter { get; set; }

            [JsonConverter(typeof(UnitOfTimeJsonConverter))]
            public FiscalYear FiscalYear { get; set; }

            [JsonConverter(typeof(UnitOfTimeJsonConverter))]
            public FiscalUnbounded FiscalUnbounded { get; set; }

            [JsonConverter(typeof(UnitOfTimeJsonConverter))]
            public GenericUnitOfTime GenericUnitOfTime { get; set; }

            [JsonConverter(typeof(UnitOfTimeJsonConverter))]
            public GenericMonth GenericMonth { get; set; }

            [JsonConverter(typeof(UnitOfTimeJsonConverter))]
            public GenericQuarter GenericQuarter { get; set; }

            [JsonConverter(typeof(UnitOfTimeJsonConverter))]
            public GenericYear GenericYear { get; set; }

            [JsonConverter(typeof(UnitOfTimeJsonConverter))]
            public GenericUnbounded GenericUnbounded { get; set; }

        }
    }
}
