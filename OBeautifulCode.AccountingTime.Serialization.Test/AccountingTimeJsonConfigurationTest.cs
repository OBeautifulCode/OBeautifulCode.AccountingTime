// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountingTimeJsonConfigurationTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Serialization.Test
{
    using System;
    using System.Collections.Generic;

    using FakeItEasy;

    using FluentAssertions;

    using OBeautifulCode.AccountingTime.Serialization.Json;
    using OBeautifulCode.Serialization.Json;

    using Xunit;

    public class AccountingTimeJsonConfigurationTest
    {
        private static readonly ObcJsonSerializer Serializer = new ObcJsonSerializer(typeof(AccountingTimeTestJsonConfiguration));

        [Fact]
        public void Deserialize___Should_roundtrip_a_serialized_AccountingPeriodSystemModel___When_model_contains_only_null_values()
        {
            // Arrange
            var expected = new AccountingPeriodSystemModel();

            var serializedBytes = Serializer.SerializeToBytes(expected);

            // Act
            var actual = Serializer.Deserialize<AccountingPeriodSystemModel>(serializedBytes);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void Deserialize___Should_roundtrip_a_serialized_AccountingPeriodSystemModel___When_model_contains_non_null_values()
        {
            // Arrange
            var expected = A.Dummy<AccountingPeriodSystemModel>();

            var serializedBytes = Serializer.SerializeToBytes(expected);

            // Act
            var actual = Serializer.Deserialize<AccountingPeriodSystemModel>(serializedBytes);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void Deserialize___Should_roundtrip_a_serialized_UnitOfTimeModel___When_model_contains_only_null_values()
        {
            // Arrange
            var expected = new UnitOfTimeModel();

            var serializedBytes = Serializer.SerializeToBytes(expected);

            // Act
            var actual = Serializer.Deserialize<UnitOfTimeModel>(serializedBytes);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void Deserialize___Should_roundtrip_a_serialized_UnitOfTimeModel___When_model_contains_non_null_values()
        {
            // Arrange
            var expected = A.Dummy<UnitOfTimeModel>();

            var serializedBytes = Serializer.SerializeToBytes(expected);

            // Act
            var actual = Serializer.Deserialize<UnitOfTimeModel>(serializedBytes);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void Deserialize___Should_roundtrip_a_serialized_ReportingPeriodModel___When_model_contains_only_null_values()
        {
            // Arrange
            var expected = new ReportingPeriodModel();

            var serializedBytes = Serializer.SerializeToBytes(expected);

            // Act
            var actual = Serializer.Deserialize<ReportingPeriodModel>(serializedBytes);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void Deserialize___Should_roundtrip_a_serialized_ReportingPeriodModel___When_model_contains_non_null_values()
        {
            // Arrange
            var expected = A.Dummy<ReportingPeriodModel>();

            var serializedBytes = Serializer.SerializeToBytes(expected);

            // Act
            var actual = Serializer.Deserialize<ReportingPeriodModel>(serializedBytes);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void Deserialize___Should_roundtrip_a_serialized_IReportingPeriodModel___When_model_contains_only_null_values()
        {
            // Arrange
            var expected = new IReportingPeriodModel();

            var serializedBytes = Serializer.SerializeToBytes(expected);

            // Act
            var actual = Serializer.Deserialize<IReportingPeriodModel>(serializedBytes);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void Deserialize___Should_roundtrip_a_serialized_IReportingPeriodModel___When_model_contains_non_null_values()
        {
            // Arrange
            var expected = A.Dummy<IReportingPeriodModel>();

            var serializedBytes = Serializer.SerializeToBytes(expected);

            // Act
            var actual = Serializer.Deserialize<IReportingPeriodModel>(serializedBytes);

            // Assert
            actual.Should().Be(expected);
        }

        private class AccountingTimeTestJsonConfiguration : JsonConfigurationBase
        {
            public override IReadOnlyCollection<Type> DependentConfigurationTypes => new[] { typeof(AccountingTimeJsonConfiguration) };

            protected override IReadOnlyCollection<Type> TypesToAutoRegister => new[] { typeof(AccountingPeriodSystemModel), typeof(UnitOfTimeModel), typeof(IReportingPeriodModel), typeof(ReportingPeriodModel) };
        }
    }
}