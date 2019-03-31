// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountingTimeJsonConfigurationTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Serialization.Test
{
    using FakeItEasy;

    using FluentAssertions;

    using Naos.Serialization.Domain;
    using Naos.Serialization.Json;

    using OBeautifulCode.AccountingTime.Serialization.Json;

    using Xunit;

    public class AccountingTimeJsonConfigurationTest
    {
        public AccountingTimeJsonConfigurationTest()
        {
            SerializationConfigurationManager.Configure<AccountingTimeJsonConfiguration>();
        }

        [Fact]
        public static void Register___Should_not_throw___When_called_multiple_times()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => SerializationConfigurationManager.Configure<AccountingTimeJsonConfiguration>());
            var ex2 = Record.Exception(() => SerializationConfigurationManager.Configure<AccountingTimeJsonConfiguration>());

            // Assert
            ex1.Should().BeNull();
            ex2.Should().BeNull();
        }

        [Fact]
        public void Deserialize___Should_roundtrip_a_serialized_AccountingPeriodSystemModel___When_model_contains_only_null_values()
        {
            // Arrange
            var expected = new AccountingPeriodSystemModel();
            var serializer = new NaosJsonSerializer();
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
            var serializer = new NaosJsonSerializer();
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
            var serializer = new NaosJsonSerializer();
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
            var serializer = new NaosJsonSerializer();
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
            var serializer = new NaosJsonSerializer();
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
            var serializer = new NaosJsonSerializer();
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
            var serializer = new NaosJsonSerializer();
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
            var serializer = new NaosJsonSerializer();
            var serializedBytes = serializer.SerializeToBytes(expected);

            // Act
            var actual = serializer.Deserialize<IReportingPeriodModel>(serializedBytes);

            // Assert
            actual.Should().Be(expected);
        }
    }
}