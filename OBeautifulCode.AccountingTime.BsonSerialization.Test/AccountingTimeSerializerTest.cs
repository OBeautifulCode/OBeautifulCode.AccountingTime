// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountingTimeSerializerTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.BsonSerialization.Test
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using FakeItEasy;

    using FluentAssertions;

    using MongoDB.Bson;
    using MongoDB.Driver;

    using Xunit;

    public static class AccountingTimeSerializerTest
    {
        private const string DatabaseName = "test";

        private static readonly IMongoDatabase Database = new MongoClient().GetDatabase(DatabaseName);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = "This is calling a method.  No fields are initized in the constructor.")]
        static AccountingTimeSerializerTest()
        {
            AccountingTimeSerializer.Register();
        }

        [Fact]
        public static async Task UnitOfTimeModel_without_nulls___Should_roundtrip_to_Mongo_and_back___When_using_custom_UnitOfTime_serializers()
        {
            // Arrange
            var collection = Database.GetCollection<UnitOfTimeModel>(nameof(UnitOfTimeModel));
            var expectedUnitOfTimeModel = A.Dummy<UnitOfTimeModel>();

            // Act
            collection.InsertOne(expectedUnitOfTimeModel);
            var actualUnitOfTimeModel = (await collection.Find(_ => _.Id == expectedUnitOfTimeModel.Id).ToListAsync()).Single();

            // Assert
            actualUnitOfTimeModel.UnitOfTime.Should().Be(expectedUnitOfTimeModel.UnitOfTime);
            actualUnitOfTimeModel.CalendarUnitOfTime.Should().Be(expectedUnitOfTimeModel.CalendarUnitOfTime);
            actualUnitOfTimeModel.CalendarDay.Should().Be(expectedUnitOfTimeModel.CalendarDay);
            actualUnitOfTimeModel.CalendarMonth.Should().Be(expectedUnitOfTimeModel.CalendarMonth);
            actualUnitOfTimeModel.CalendarQuarter.Should().Be(expectedUnitOfTimeModel.CalendarQuarter);
            actualUnitOfTimeModel.CalendarYear.Should().Be(expectedUnitOfTimeModel.CalendarYear);
            actualUnitOfTimeModel.FiscalUnitOfTime.Should().Be(expectedUnitOfTimeModel.FiscalUnitOfTime);
            actualUnitOfTimeModel.FiscalMonth.Should().Be(expectedUnitOfTimeModel.FiscalMonth);
            actualUnitOfTimeModel.FiscalQuarter.Should().Be(expectedUnitOfTimeModel.FiscalQuarter);
            actualUnitOfTimeModel.FiscalYear.Should().Be(expectedUnitOfTimeModel.FiscalYear);
            actualUnitOfTimeModel.GenericUnitOfTime.Should().Be(expectedUnitOfTimeModel.GenericUnitOfTime);
            actualUnitOfTimeModel.GenericMonth.Should().Be(expectedUnitOfTimeModel.GenericMonth);
            actualUnitOfTimeModel.GenericQuarter.Should().Be(expectedUnitOfTimeModel.GenericQuarter);
            actualUnitOfTimeModel.GenericYear.Should().Be(expectedUnitOfTimeModel.GenericYear);
        }

        [Fact]
        public static async Task UnitOfTimeModel_with_nulls___Should_roundtrip_to_Mongo_and_back___When_using_custom_UnitOfTime_serializers()
        {
            // Arrange
            var collection = Database.GetCollection<UnitOfTimeModel>(nameof(UnitOfTimeModel));
            var expectedUnitOfTimeModel = new UnitOfTimeModel();

            // Act
            collection.InsertOne(expectedUnitOfTimeModel);
            var actualUnitOfTimeModel = (await collection.Find(_ => _.Id == expectedUnitOfTimeModel.Id).ToListAsync()).Single();

            // Assert
            actualUnitOfTimeModel.UnitOfTime.Should().BeNull();
            actualUnitOfTimeModel.CalendarUnitOfTime.Should().BeNull();
            actualUnitOfTimeModel.CalendarDay.Should().BeNull();
            actualUnitOfTimeModel.CalendarMonth.Should().BeNull();
            actualUnitOfTimeModel.CalendarQuarter.Should().BeNull();
            actualUnitOfTimeModel.CalendarYear.Should().BeNull();
            actualUnitOfTimeModel.FiscalUnitOfTime.Should().BeNull();
            actualUnitOfTimeModel.FiscalMonth.Should().BeNull();
            actualUnitOfTimeModel.FiscalQuarter.Should().BeNull();
            actualUnitOfTimeModel.FiscalYear.Should().BeNull();
            actualUnitOfTimeModel.GenericUnitOfTime.Should().BeNull();
            actualUnitOfTimeModel.GenericMonth.Should().BeNull();
            actualUnitOfTimeModel.GenericQuarter.Should().BeNull();
            actualUnitOfTimeModel.GenericYear.Should().BeNull();
        }

        [Fact]
        public static void UnitOfTimeModel_without_nulls___Should_serialize_to_sortable_string_representation_of_UnitOfTime___When_using_custom_UnitOfTime_serializers()
        {
            // Arrange
            var unitOfTimeModel = A.Dummy<UnitOfTimeModel>();
            var expectedJson =
                "\"" + nameof(unitOfTimeModel.UnitOfTime) + "\" : \"" + unitOfTimeModel.UnitOfTime.SerializeToSortableString() + "\", " +
                "\"" + nameof(unitOfTimeModel.CalendarUnitOfTime) + "\" : \"" + unitOfTimeModel.CalendarUnitOfTime.SerializeToSortableString() + "\", " +
                "\"" + nameof(unitOfTimeModel.CalendarDay) + "\" : \"" + unitOfTimeModel.CalendarDay.SerializeToSortableString() + "\", " +
                "\"" + nameof(unitOfTimeModel.CalendarMonth) + "\" : \"" + unitOfTimeModel.CalendarMonth.SerializeToSortableString() + "\", " +
                "\"" + nameof(unitOfTimeModel.CalendarQuarter) + "\" : \"" + unitOfTimeModel.CalendarQuarter.SerializeToSortableString() + "\", " +
                "\"" + nameof(unitOfTimeModel.CalendarYear) + "\" : \"" + unitOfTimeModel.CalendarYear.SerializeToSortableString() + "\", " +
                "\"" + nameof(unitOfTimeModel.FiscalUnitOfTime) + "\" : \"" + unitOfTimeModel.FiscalUnitOfTime.SerializeToSortableString() + "\", " +
                "\"" + nameof(unitOfTimeModel.FiscalMonth) + "\" : \"" + unitOfTimeModel.FiscalMonth.SerializeToSortableString() + "\", " +
                "\"" + nameof(unitOfTimeModel.FiscalQuarter) + "\" : \"" + unitOfTimeModel.FiscalQuarter.SerializeToSortableString() + "\", " +
                "\"" + nameof(unitOfTimeModel.FiscalYear) + "\" : \"" + unitOfTimeModel.FiscalYear.SerializeToSortableString() + "\", " +
                "\"" + nameof(unitOfTimeModel.GenericUnitOfTime) + "\" : \"" + unitOfTimeModel.GenericUnitOfTime.SerializeToSortableString() + "\", " +
                "\"" + nameof(unitOfTimeModel.GenericMonth) + "\" : \"" + unitOfTimeModel.GenericMonth.SerializeToSortableString() + "\", " +
                "\"" + nameof(unitOfTimeModel.GenericQuarter) + "\" : \"" + unitOfTimeModel.GenericQuarter.SerializeToSortableString() + "\", " +
                "\"" + nameof(unitOfTimeModel.GenericYear) + "\" : \"" + unitOfTimeModel.GenericYear.SerializeToSortableString() + "\"";

            // Act
            var actualJson = unitOfTimeModel.ToJson();

            // Assert
            actualJson.Should().Contain(expectedJson);
        }

        private class UnitOfTimeModel
        {
            // ReSharper disable UnusedAutoPropertyAccessor.Local
            // ReSharper disable MemberCanBePrivate.Local
            public Guid Id { get; set; }

            public UnitOfTime UnitOfTime { get; set; }

            public CalendarUnitOfTime CalendarUnitOfTime { get; set; }

            public CalendarDay CalendarDay { get; set; }

            public CalendarMonth CalendarMonth { get; set; }

            public CalendarQuarter CalendarQuarter { get; set; }

            public CalendarYear CalendarYear { get; set; }

            public FiscalUnitOfTime FiscalUnitOfTime { get; set; }

            public FiscalMonth FiscalMonth { get; set; }

            public FiscalQuarter FiscalQuarter { get; set; }

            public FiscalYear FiscalYear { get; set; }

            public GenericUnitOfTime GenericUnitOfTime { get; set; }

            public GenericMonth GenericMonth { get; set; }

            public GenericQuarter GenericQuarter { get; set; }

            public GenericYear GenericYear { get; set; }

            // ReSharper restore MemberCanBePrivate.Local
            // ReSharper restore UnusedAutoPropertyAccessor.Local
        }
    }
}
