// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountingTimeBsonConfigurationTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Serialization.Test
{
    using FakeItEasy;

    using FluentAssertions;

    using MongoDB.Bson;
    using Naos.Serialization.Bson;
    using Naos.Serialization.Domain;

    using OBeautifulCode.AccountingTime.Serialization.Bson;

    using Xunit;

    public class AccountingTimeBsonConfigurationTest
    {
        public AccountingTimeBsonConfigurationTest()
        {
            SerializationConfigurationManager.Configure<AccountingTimeBsonConfiguration>();
        }

        [Fact]
        public static void Register___Should_not_throw___When_called_multiple_times()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => SerializationConfigurationManager.Configure<AccountingTimeBsonConfiguration>());
            var ex2 = Record.Exception(() => SerializationConfigurationManager.Configure<AccountingTimeBsonConfiguration>());

            // Assert
            ex1.Should().BeNull();
            ex2.Should().BeNull();
        }

        [Fact]
        public void Deserialize___Should_roundtrip_a_serialized_AccountingPeriodSystemModel___When_model_contains_only_null_values()
        {
            // Arrange
            var expected = new AccountingPeriodSystemModel();
            var serializer = new NaosBsonSerializer();
            var serializedBytes = serializer.SerializeToBytes(expected);

            // Act
            var actual = serializer.Deserialize<AccountingPeriodSystemModel>(serializedBytes);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void Deserialize___Should_roundtrip_a_serialized_AccountingPeriodSystemModel___When_model_contains_non_null_values()
        {
            // Arrange
            var expected = A.Dummy<AccountingPeriodSystemModel>();
            var serializer = new NaosBsonSerializer();
            var serializedBytes = serializer.SerializeToBytes(expected);

            // Act
            var actual = serializer.Deserialize<AccountingPeriodSystemModel>(serializedBytes);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void Deserialize___Should_roundtrip_a_serialized_UnitOfTimeModel___When_model_contains_only_null_values()
        {
            // Arrange
            var expected = new UnitOfTimeModel();
            var serializer = new NaosBsonSerializer();
            var serializedBytes = serializer.SerializeToBytes(expected);

            // Act
            var actual = serializer.Deserialize<UnitOfTimeModel>(serializedBytes);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void Deserialize___Should_roundtrip_a_serialized_UnitOfTimeModel___When_model_contains_non_null_values()
        {
            // Arrange
            var expected = A.Dummy<UnitOfTimeModel>();
            var serializer = new NaosBsonSerializer();
            var serializedBytes = serializer.SerializeToBytes(expected);

            // Act
            var actual = serializer.Deserialize<UnitOfTimeModel>(serializedBytes);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void Deserialize___Should_roundtrip_a_serialized_ReportingPeriodModel___When_model_contains_only_null_values()
        {
            // Arrange
            var expected = new ReportingPeriodModel();
            var serializer = new NaosBsonSerializer();
            var serializedBytes = serializer.SerializeToBytes(expected);

            // Act
            var actual = serializer.Deserialize<ReportingPeriodModel>(serializedBytes);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void Deserialize___Should_roundtrip_a_serialized_ReportingPeriodModel___When_model_contains_non_null_values()
        {
            // Arrange
            var expected = A.Dummy<ReportingPeriodModel>();
            var serializer = new NaosBsonSerializer();
            var serializedBytes = serializer.SerializeToBytes(expected);

            // Act
            var actual = serializer.Deserialize<ReportingPeriodModel>(serializedBytes);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void Deserialize___Should_roundtrip_a_serialized_IReportingPeriodModel___When_model_contains_only_null_values()
        {
            // Arrange
            var expected = new IReportingPeriodModel();
            var serializer = new NaosBsonSerializer();
            var serializedBytes = serializer.SerializeToBytes(expected);

            // Act
            var actual = serializer.Deserialize<IReportingPeriodModel>(serializedBytes);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void Deserialize___Should_roundtrip_a_serialized_IReportingPeriodModel___When_model_contains_non_null_values()
        {
            // Arrange
            var expected = A.Dummy<IReportingPeriodModel>();
            var serializer = new NaosBsonSerializer();
            var serializedBytes = serializer.SerializeToBytes(expected);

            // Act
            var actual = serializer.Deserialize<IReportingPeriodModel>(serializedBytes);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void UnitOfTimeModel_without_nulls___Should_serialize_to_sortable_string_representation_of_UnitOfTime___When_using_custom_UnitOfTime_serializers()
        {
            // Arrange
            var model = A.Dummy<UnitOfTimeModel>();
            var expectedJson =
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
            var actualJson = model.ToJson();

            // Assert
            actualJson.Should().Contain(expectedJson);
        }

        [Fact]
        public void ReportingPeriodModel_without_nulls___Should_serialize_to_ReportingPeriodPersistenceModel_representation_of_ReportingPeriod___When_using_custom_serializers()
        {
            // Arrange
            var model1 = A.Dummy<ReportingPeriodModel>();
            var model2 = A.Dummy<IReportingPeriodModel>();

            var expectedJson1 =
                "\"" + nameof(model1.UnitOfTime) + "\" : { \"Start\" : \"" + model1.UnitOfTime.Start.SerializeToSortableString() + "\", \"End\" : \"" + model1.UnitOfTime.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model1.CalendarUnitOfTime) + "\" : { \"Start\" : \"" + model1.CalendarUnitOfTime.Start.SerializeToSortableString() + "\", \"End\" : \"" + model1.CalendarUnitOfTime.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model1.CalendarDay) + "\" : { \"Start\" : \"" + model1.CalendarDay.Start.SerializeToSortableString() + "\", \"End\" : \"" + model1.CalendarDay.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model1.CalendarMonth) + "\" : { \"Start\" : \"" + model1.CalendarMonth.Start.SerializeToSortableString() + "\", \"End\" : \"" + model1.CalendarMonth.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model1.CalendarQuarter) + "\" : { \"Start\" : \"" + model1.CalendarQuarter.Start.SerializeToSortableString() + "\", \"End\" : \"" + model1.CalendarQuarter.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model1.CalendarYear) + "\" : { \"Start\" : \"" + model1.CalendarYear.Start.SerializeToSortableString() + "\", \"End\" : \"" + model1.CalendarYear.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model1.CalendarUnbounded) + "\" : { \"Start\" : \"" + model1.CalendarUnbounded.Start.SerializeToSortableString() + "\", \"End\" : \"" + model1.CalendarUnbounded.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model1.FiscalUnitOfTime) + "\" : { \"Start\" : \"" + model1.FiscalUnitOfTime.Start.SerializeToSortableString() + "\", \"End\" : \"" + model1.FiscalUnitOfTime.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model1.FiscalMonth) + "\" : { \"Start\" : \"" + model1.FiscalMonth.Start.SerializeToSortableString() + "\", \"End\" : \"" + model1.FiscalMonth.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model1.FiscalQuarter) + "\" : { \"Start\" : \"" + model1.FiscalQuarter.Start.SerializeToSortableString() + "\", \"End\" : \"" + model1.FiscalQuarter.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model1.FiscalYear) + "\" : { \"Start\" : \"" + model1.FiscalYear.Start.SerializeToSortableString() + "\", \"End\" : \"" + model1.FiscalYear.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model1.FiscalUnbounded) + "\" : { \"Start\" : \"" + model1.FiscalUnbounded.Start.SerializeToSortableString() + "\", \"End\" : \"" + model1.FiscalUnbounded.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model1.GenericUnitOfTime) + "\" : { \"Start\" : \"" + model1.GenericUnitOfTime.Start.SerializeToSortableString() + "\", \"End\" : \"" + model1.GenericUnitOfTime.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model1.GenericMonth) + "\" : { \"Start\" : \"" + model1.GenericMonth.Start.SerializeToSortableString() + "\", \"End\" : \"" + model1.GenericMonth.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model1.GenericQuarter) + "\" : { \"Start\" : \"" + model1.GenericQuarter.Start.SerializeToSortableString() + "\", \"End\" : \"" + model1.GenericQuarter.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model1.GenericYear) + "\" : { \"Start\" : \"" + model1.GenericYear.Start.SerializeToSortableString() + "\", \"End\" : \"" + model1.GenericYear.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model1.GenericUnbounded) + "\" : { \"Start\" : \"" + model1.GenericUnbounded.Start.SerializeToSortableString() + "\", \"End\" : \"" + model1.GenericUnbounded.End.SerializeToSortableString() + "\" }";

            var expectedJson2 =
                "\"" + nameof(model2.UnitOfTime) + "\" : { \"Start\" : \"" + model2.UnitOfTime.Start.SerializeToSortableString() + "\", \"End\" : \"" + model2.UnitOfTime.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model2.CalendarUnitOfTime) + "\" : { \"Start\" : \"" + model2.CalendarUnitOfTime.Start.SerializeToSortableString() + "\", \"End\" : \"" + model2.CalendarUnitOfTime.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model2.CalendarDay) + "\" : { \"Start\" : \"" + model2.CalendarDay.Start.SerializeToSortableString() + "\", \"End\" : \"" + model2.CalendarDay.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model2.CalendarMonth) + "\" : { \"Start\" : \"" + model2.CalendarMonth.Start.SerializeToSortableString() + "\", \"End\" : \"" + model2.CalendarMonth.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model2.CalendarQuarter) + "\" : { \"Start\" : \"" + model2.CalendarQuarter.Start.SerializeToSortableString() + "\", \"End\" : \"" + model2.CalendarQuarter.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model2.CalendarYear) + "\" : { \"Start\" : \"" + model2.CalendarYear.Start.SerializeToSortableString() + "\", \"End\" : \"" + model2.CalendarYear.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model2.CalendarUnbounded) + "\" : { \"Start\" : \"" + model2.CalendarUnbounded.Start.SerializeToSortableString() + "\", \"End\" : \"" + model2.CalendarUnbounded.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model2.FiscalUnitOfTime) + "\" : { \"Start\" : \"" + model2.FiscalUnitOfTime.Start.SerializeToSortableString() + "\", \"End\" : \"" + model2.FiscalUnitOfTime.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model2.FiscalMonth) + "\" : { \"Start\" : \"" + model2.FiscalMonth.Start.SerializeToSortableString() + "\", \"End\" : \"" + model2.FiscalMonth.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model2.FiscalQuarter) + "\" : { \"Start\" : \"" + model2.FiscalQuarter.Start.SerializeToSortableString() + "\", \"End\" : \"" + model2.FiscalQuarter.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model2.FiscalYear) + "\" : { \"Start\" : \"" + model2.FiscalYear.Start.SerializeToSortableString() + "\", \"End\" : \"" + model2.FiscalYear.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model2.FiscalUnbounded) + "\" : { \"Start\" : \"" + model2.FiscalUnbounded.Start.SerializeToSortableString() + "\", \"End\" : \"" + model2.FiscalUnbounded.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model2.GenericUnitOfTime) + "\" : { \"Start\" : \"" + model2.GenericUnitOfTime.Start.SerializeToSortableString() + "\", \"End\" : \"" + model2.GenericUnitOfTime.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model2.GenericMonth) + "\" : { \"Start\" : \"" + model2.GenericMonth.Start.SerializeToSortableString() + "\", \"End\" : \"" + model2.GenericMonth.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model2.GenericQuarter) + "\" : { \"Start\" : \"" + model2.GenericQuarter.Start.SerializeToSortableString() + "\", \"End\" : \"" + model2.GenericQuarter.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model2.GenericYear) + "\" : { \"Start\" : \"" + model2.GenericYear.Start.SerializeToSortableString() + "\", \"End\" : \"" + model2.GenericYear.End.SerializeToSortableString() + "\" }, " +
                "\"" + nameof(model2.GenericUnbounded) + "\" : { \"Start\" : \"" + model2.GenericUnbounded.Start.SerializeToSortableString() + "\", \"End\" : \"" + model2.GenericUnbounded.End.SerializeToSortableString() + "\" }";

            // Act
            var actualJson1 = model1.ToJson();
            var actualJson2 = model2.ToJson();

            // Assert
            actualJson1.Should().Contain(expectedJson1);
            actualJson2.Should().Contain(expectedJson2);
        }
    }
}
