// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfTimeConverterTest.cs" company="OBeautifulCode">
//    Copyright (c) OBeautifulCode 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Serialization.Test
{
    using System;

    using FakeItEasy;

    using FluentAssertions;

    using Naos.Serialization.PropertyBag;

    using Newtonsoft.Json;


    using Xunit;

    public static class UnitOfTimeStringSerializerTest
    {
        [Fact]
        public static void UnitOfTimeModel_without_nulls___Should_roundtrip_to_json_and_back___When_using_UnitOfTimeConverter()
        {
            // Arrange
            var expectedModel = A.Dummy<UnitOfTimeModel>();

            // Act
            var json = new NaosPropertyBagSerializer().SerializeToString(expectedModel);
            var actualModel = new NaosPropertyBagSerializer().Deserialize<UnitOfTimeModel>(json);

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
            var json = new NaosPropertyBagSerializer().SerializeToString(expectedModel);
            var actualModel = new NaosPropertyBagSerializer().Deserialize<UnitOfTimeModel>(json);

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
                nameof(model.UnitOfTime) + "=" + model.UnitOfTime.SerializeToSortableString() + Environment.NewLine +
                nameof(model.CalendarUnitOfTime) + "=" + model.CalendarUnitOfTime.SerializeToSortableString() + Environment.NewLine +
                nameof(model.CalendarDay) + "=" + model.CalendarDay.SerializeToSortableString() + Environment.NewLine +
                nameof(model.CalendarMonth) + "=" + model.CalendarMonth.SerializeToSortableString() + Environment.NewLine +
                nameof(model.CalendarQuarter) + "=" + model.CalendarQuarter.SerializeToSortableString() + Environment.NewLine +
                nameof(model.CalendarYear) + "=" + model.CalendarYear.SerializeToSortableString() + Environment.NewLine +
                nameof(model.CalendarUnbounded) + "=" + model.CalendarUnbounded.SerializeToSortableString() + Environment.NewLine +
                nameof(model.FiscalUnitOfTime) + "=" + model.FiscalUnitOfTime.SerializeToSortableString() + Environment.NewLine +
                nameof(model.FiscalMonth) + "=" + model.FiscalMonth.SerializeToSortableString() + Environment.NewLine +
                nameof(model.FiscalQuarter) + "=" + model.FiscalQuarter.SerializeToSortableString() + Environment.NewLine +
                nameof(model.FiscalYear) + "=" + model.FiscalYear.SerializeToSortableString() + Environment.NewLine +
                nameof(model.FiscalUnbounded) + "=" + model.FiscalUnbounded.SerializeToSortableString() + Environment.NewLine +
                nameof(model.GenericUnitOfTime) + "=" + model.GenericUnitOfTime.SerializeToSortableString() + Environment.NewLine +
                nameof(model.GenericMonth) + "=" + model.GenericMonth.SerializeToSortableString() + Environment.NewLine +
                nameof(model.GenericQuarter) + "=" + model.GenericQuarter.SerializeToSortableString() + Environment.NewLine +
                nameof(model.GenericYear) + "=" + model.GenericYear.SerializeToSortableString() + Environment.NewLine +
                nameof(model.GenericUnbounded) + "=" + model.GenericUnbounded.SerializeToSortableString() + Environment.NewLine;

            // Act
            var actualJson = new NaosPropertyBagSerializer().SerializeToString(model);

            // Assert
            actualJson.Should().Contain(expectedJson);
        }

        private class UnitOfTimeModel
        {
            public UnitOfTime UnitOfTime { get; set; }

            public CalendarUnitOfTime CalendarUnitOfTime { get; set; }

            public CalendarDay CalendarDay { get; set; }

            public CalendarMonth CalendarMonth { get; set; }

            public CalendarQuarter CalendarQuarter { get; set; }

            public CalendarYear CalendarYear { get; set; }

            public CalendarUnbounded CalendarUnbounded { get; set; }

            public FiscalUnitOfTime FiscalUnitOfTime { get; set; }

            public FiscalMonth FiscalMonth { get; set; }

            public FiscalQuarter FiscalQuarter { get; set; }

            public FiscalYear FiscalYear { get; set; }

            public FiscalUnbounded FiscalUnbounded { get; set; }

            public GenericUnitOfTime GenericUnitOfTime { get; set; }

            public GenericMonth GenericMonth { get; set; }

            public GenericQuarter GenericQuarter { get; set; }

            public GenericYear GenericYear { get; set; }

            public GenericUnbounded GenericUnbounded { get; set; }

        }
    }
}
