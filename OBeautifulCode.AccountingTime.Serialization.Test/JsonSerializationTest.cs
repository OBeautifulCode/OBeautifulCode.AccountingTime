// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JsonSerializationTest.cs" company="OBeautifulCode">
//    Copyright (c) OBeautifulCode 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Test
{
    using FakeItEasy;

    using FluentAssertions;

    using Naos.Serialization.Json;

    using OBeautifulCode.AutoFakeItEasy;

    using Xunit;

    public static class JsonSerializationTest
    {
        [Fact]
        public static void Deserialize___Should_return_object_of_type_CalendarYearAccountingPeriodSystem___When_an_object_of_that_type_is_serialized_to_json_and_deserialized_as_AccountingPeriodSystem()
        {
            // Arrange
            var serializer = new NaosJsonSerializer();
            var originalAccountingPeriodSystem = new CalendarYearAccountingPeriodSystem();
            var serializedJsonBytes = serializer.SerializeToBytes(originalAccountingPeriodSystem);
            var serializedJsonString = serializer.SerializeToString(originalAccountingPeriodSystem);

            // Act
            var systemUnderTest1 = serializer.Deserialize<AccountingPeriodSystem>(serializedJsonBytes);
            var systemUnderTest2 = serializer.Deserialize<AccountingPeriodSystem>(serializedJsonString);

            // Assert
            systemUnderTest1.Should().BeOfType<CalendarYearAccountingPeriodSystem>();
            systemUnderTest2.Should().BeOfType<CalendarYearAccountingPeriodSystem>();
        }

        [Fact]
        public static void Deserialize___Should_return_equivalent_object_of_type_FiftyTwoFiftyThreeWeekAccountingPeriodSystem___When_an_object_of_that_type_is_serialized_to_json_and_deserialized_as_AccountingPeriodSystem()
        {
            // Arrange
            var serializer = new NaosJsonSerializer();
            var expectedAccountingPeriodSystem = A.Dummy<FiftyTwoFiftyThreeWeekAccountingPeriodSystem>();
            var serializedJsonBytes = serializer.SerializeToBytes(expectedAccountingPeriodSystem);
            var serializedJsonString = serializer.SerializeToString(expectedAccountingPeriodSystem);

            // Act
            var systemUnderTest1 = serializer.Deserialize<AccountingPeriodSystem>(serializedJsonBytes) as FiftyTwoFiftyThreeWeekAccountingPeriodSystem;
            var systemUnderTest2 = serializer.Deserialize<AccountingPeriodSystem>(serializedJsonString) as FiftyTwoFiftyThreeWeekAccountingPeriodSystem;

            // Assert
            systemUnderTest1.Should().NotBeNull();
            systemUnderTest1.AnchorMonth.Should().Be(expectedAccountingPeriodSystem.AnchorMonth);
            systemUnderTest1.FiftyTwoFiftyThreeWeekMethodology.Should().Be(expectedAccountingPeriodSystem.FiftyTwoFiftyThreeWeekMethodology);
            systemUnderTest1.LastDayOfWeekInAccountingYear.Should().Be(expectedAccountingPeriodSystem.LastDayOfWeekInAccountingYear);

            systemUnderTest2.Should().NotBeNull();
            systemUnderTest2.AnchorMonth.Should().Be(expectedAccountingPeriodSystem.AnchorMonth);
            systemUnderTest2.FiftyTwoFiftyThreeWeekMethodology.Should().Be(expectedAccountingPeriodSystem.FiftyTwoFiftyThreeWeekMethodology);
            systemUnderTest2.LastDayOfWeekInAccountingYear.Should().Be(expectedAccountingPeriodSystem.LastDayOfWeekInAccountingYear);
        }

        [Fact]
        public static void Deserialize___Should_return_equivalent_object_of_type_FiscalYearAccountingPeriodSystem___When_an_object_of_that_type_is_serialized_to_json_and_deserialized_as_AccountingPeriodSystem()
        {
            // Arrange
            var serializer = new NaosJsonSerializer();
            var expectedAccountingPeriodSystem = new FiscalYearAccountingPeriodSystem(A.Dummy<MonthOfYear>().ThatIsNot(MonthOfYear.December));
            var serializedJsonBytes = serializer.SerializeToBytes(expectedAccountingPeriodSystem);
            var serializedJsonString = serializer.SerializeToString(expectedAccountingPeriodSystem);

            // Act
            var systemUnderTest1 = serializer.Deserialize<AccountingPeriodSystem>(serializedJsonBytes) as FiscalYearAccountingPeriodSystem;
            var systemUnderTest2 = serializer.Deserialize<AccountingPeriodSystem>(serializedJsonString) as FiscalYearAccountingPeriodSystem;

            // Assert
            systemUnderTest1.Should().NotBeNull();
            systemUnderTest1.LastMonthInFiscalYear.Should().Be(expectedAccountingPeriodSystem.LastMonthInFiscalYear);

            systemUnderTest2.Should().NotBeNull();
            systemUnderTest2.LastMonthInFiscalYear.Should().Be(expectedAccountingPeriodSystem.LastMonthInFiscalYear);
        }

        [Fact]
        public static void Deserialize___Should_return_equivalent_object_of_type_CalendarDay___When_an_object_of_that_type_is_serialized_to_json_and_deserialized_as_abstract_type()
        {
            // Arrange
            var serializer = new NaosJsonSerializer();
            var expectedUnitOfTime = A.Dummy<CalendarDay>();
            var serializedJsonBytes = serializer.SerializeToBytes(expectedUnitOfTime);
            var serializedJsonString = serializer.SerializeToBytes(expectedUnitOfTime);

            // Act
            var systemUnderTest1a = serializer.Deserialize<UnitOfTime>(serializedJsonBytes) as CalendarDay;
            var systemUnderTest1b = serializer.Deserialize<CalendarUnitOfTime>(serializedJsonBytes) as CalendarDay;

            var systemUnderTest2a = serializer.Deserialize<UnitOfTime>(serializedJsonString) as CalendarDay;
            var systemUnderTest2b = serializer.Deserialize<CalendarUnitOfTime>(serializedJsonString) as CalendarDay;

            // Assert
            systemUnderTest1a.Should().Be(expectedUnitOfTime);
            systemUnderTest1b.Should().Be(expectedUnitOfTime);

            systemUnderTest2a.Should().Be(expectedUnitOfTime);
            systemUnderTest2b.Should().Be(expectedUnitOfTime);
        }

        [Fact]
        public static void Deserialize___Should_return_equivalent_object_of_type_CalendarMonth___When_an_object_of_that_type_is_serialized_to_json_and_deserialized_as_abstract_type()
        {
            // Arrange
            var serializer = new NaosJsonSerializer();
            var expectedUnitOfTime = A.Dummy<CalendarMonth>();
            var serializedJsonBytes = serializer.SerializeToBytes(expectedUnitOfTime);
            var serializedJsonString = serializer.SerializeToBytes(expectedUnitOfTime);

            // Act
            var systemUnderTest1a = serializer.Deserialize<UnitOfTime>(serializedJsonBytes) as CalendarMonth;
            var systemUnderTest1b = serializer.Deserialize<CalendarUnitOfTime>(serializedJsonBytes) as CalendarMonth;

            var systemUnderTest2a = serializer.Deserialize<UnitOfTime>(serializedJsonString) as CalendarMonth;
            var systemUnderTest2b = serializer.Deserialize<CalendarUnitOfTime>(serializedJsonString) as CalendarMonth;

            // Assert
            systemUnderTest1a.Should().Be(expectedUnitOfTime);
            systemUnderTest1b.Should().Be(expectedUnitOfTime);

            systemUnderTest2a.Should().Be(expectedUnitOfTime);
            systemUnderTest2b.Should().Be(expectedUnitOfTime);
        }

        [Fact]
        public static void Deserialize___Should_return_equivalent_object_of_type_CalendarQuarter___When_an_object_of_that_type_is_serialized_to_json_and_deserialized_as_abstract_type()
        {
            // Arrange
            var serializer = new NaosJsonSerializer();
            var expectedUnitOfTime = A.Dummy<CalendarQuarter>();
            var serializedJsonBytes = serializer.SerializeToBytes(expectedUnitOfTime);
            var serializedJsonString = serializer.SerializeToBytes(expectedUnitOfTime);

            // Act
            var systemUnderTest1a = serializer.Deserialize<UnitOfTime>(serializedJsonBytes) as CalendarQuarter;
            var systemUnderTest1b = serializer.Deserialize<CalendarUnitOfTime>(serializedJsonBytes) as CalendarQuarter;

            var systemUnderTest2a = serializer.Deserialize<UnitOfTime>(serializedJsonString) as CalendarQuarter;
            var systemUnderTest2b = serializer.Deserialize<CalendarUnitOfTime>(serializedJsonString) as CalendarQuarter;

            // Assert
            systemUnderTest1a.Should().Be(expectedUnitOfTime);
            systemUnderTest1b.Should().Be(expectedUnitOfTime);

            systemUnderTest2a.Should().Be(expectedUnitOfTime);
            systemUnderTest2b.Should().Be(expectedUnitOfTime);
        }

        [Fact]
        public static void Deserialize___Should_return_equivalent_object_of_type_CalendarYear___When_an_object_of_that_type_is_serialized_to_json_and_deserialized_as_abstract_type()
        {
            // Arrange
            var serializer = new NaosJsonSerializer();
            var expectedUnitOfTime = A.Dummy<CalendarYear>();
            var serializedJsonBytes = serializer.SerializeToBytes(expectedUnitOfTime);
            var serializedJsonString = serializer.SerializeToBytes(expectedUnitOfTime);

            // Act
            var systemUnderTest1a = serializer.Deserialize<UnitOfTime>(serializedJsonBytes) as CalendarYear;
            var systemUnderTest1b = serializer.Deserialize<CalendarUnitOfTime>(serializedJsonBytes) as CalendarYear;

            var systemUnderTest2a = serializer.Deserialize<UnitOfTime>(serializedJsonString) as CalendarYear;
            var systemUnderTest2b = serializer.Deserialize<CalendarUnitOfTime>(serializedJsonString) as CalendarYear;

            // Assert
            systemUnderTest1a.Should().Be(expectedUnitOfTime);
            systemUnderTest1b.Should().Be(expectedUnitOfTime);

            systemUnderTest2a.Should().Be(expectedUnitOfTime);
            systemUnderTest2b.Should().Be(expectedUnitOfTime);
        }

        [Fact]
        public static void Deserialize___Should_return_equivalent_object_of_type_CalendarUnbounded___When_an_object_of_that_type_is_serialized_to_json_and_deserialized_as_abstract_type()
        {
            // Arrange
            var serializer = new NaosJsonSerializer();
            var expectedUnitOfTime = A.Dummy<CalendarUnbounded>();
            var serializedJsonBytes = serializer.SerializeToBytes(expectedUnitOfTime);
            var serializedJsonString = serializer.SerializeToBytes(expectedUnitOfTime);

            // Act
            var systemUnderTest1a = serializer.Deserialize<UnitOfTime>(serializedJsonBytes) as CalendarUnbounded;
            var systemUnderTest1b = serializer.Deserialize<CalendarUnitOfTime>(serializedJsonBytes) as CalendarUnbounded;

            var systemUnderTest2a = serializer.Deserialize<UnitOfTime>(serializedJsonString) as CalendarUnbounded;
            var systemUnderTest2b = serializer.Deserialize<CalendarUnitOfTime>(serializedJsonString) as CalendarUnbounded;

            // Assert
            systemUnderTest1a.Should().Be(expectedUnitOfTime);
            systemUnderTest1b.Should().Be(expectedUnitOfTime);

            systemUnderTest2a.Should().Be(expectedUnitOfTime);
            systemUnderTest2b.Should().Be(expectedUnitOfTime);
        }

        [Fact]
        public static void Deserialize___Should_return_equivalent_object_of_type_FiscalMonth___When_an_object_of_that_type_is_serialized_to_json_and_deserialized_as_abstract_type()
        {
            // Arrange
            var serializer = new NaosJsonSerializer();
            var expectedUnitOfTime = A.Dummy<FiscalMonth>();
            var serializedJsonBytes = serializer.SerializeToBytes(expectedUnitOfTime);
            var serializedJsonString = serializer.SerializeToBytes(expectedUnitOfTime);

            // Act
            var systemUnderTest1a = serializer.Deserialize<UnitOfTime>(serializedJsonBytes) as FiscalMonth;
            var systemUnderTest1b = serializer.Deserialize<FiscalUnitOfTime>(serializedJsonBytes) as FiscalMonth;

            var systemUnderTest2a = serializer.Deserialize<UnitOfTime>(serializedJsonString) as FiscalMonth;
            var systemUnderTest2b = serializer.Deserialize<FiscalUnitOfTime>(serializedJsonString) as FiscalMonth;

            // Assert
            systemUnderTest1a.Should().Be(expectedUnitOfTime);
            systemUnderTest1b.Should().Be(expectedUnitOfTime);

            systemUnderTest2a.Should().Be(expectedUnitOfTime);
            systemUnderTest2b.Should().Be(expectedUnitOfTime);
        }

        [Fact]
        public static void Deserialize___Should_return_equivalent_object_of_type_FiscalQuarter___When_an_object_of_that_type_is_serialized_to_json_and_deserialized_as_abstract_type()
        {
            // Arrange
            var serializer = new NaosJsonSerializer();
            var expectedUnitOfTime = A.Dummy<FiscalQuarter>();
            var serializedJsonBytes = serializer.SerializeToBytes(expectedUnitOfTime);
            var serializedJsonString = serializer.SerializeToBytes(expectedUnitOfTime);

            // Act
            var systemUnderTest1a = serializer.Deserialize<UnitOfTime>(serializedJsonBytes) as FiscalQuarter;
            var systemUnderTest1b = serializer.Deserialize<FiscalUnitOfTime>(serializedJsonBytes) as FiscalQuarter;

            var systemUnderTest2a = serializer.Deserialize<UnitOfTime>(serializedJsonString) as FiscalQuarter;
            var systemUnderTest2b = serializer.Deserialize<FiscalUnitOfTime>(serializedJsonString) as FiscalQuarter;

            // Assert
            systemUnderTest1a.Should().Be(expectedUnitOfTime);
            systemUnderTest1b.Should().Be(expectedUnitOfTime);

            systemUnderTest2a.Should().Be(expectedUnitOfTime);
            systemUnderTest2b.Should().Be(expectedUnitOfTime);
        }

        [Fact]
        public static void Deserialize___Should_return_equivalent_object_of_type_FiscalYear___When_an_object_of_that_type_is_serialized_to_json_and_deserialized_as_abstract_type()
        {
            // Arrange
            var serializer = new NaosJsonSerializer();
            var expectedUnitOfTime = A.Dummy<FiscalYear>();
            var serializedJsonBytes = serializer.SerializeToBytes(expectedUnitOfTime);
            var serializedJsonString = serializer.SerializeToBytes(expectedUnitOfTime);

            // Act
            var systemUnderTest1a = serializer.Deserialize<UnitOfTime>(serializedJsonBytes) as FiscalYear;
            var systemUnderTest1b = serializer.Deserialize<FiscalUnitOfTime>(serializedJsonBytes) as FiscalYear;

            var systemUnderTest2a = serializer.Deserialize<UnitOfTime>(serializedJsonString) as FiscalYear;
            var systemUnderTest2b = serializer.Deserialize<FiscalUnitOfTime>(serializedJsonString) as FiscalYear;

            // Assert
            systemUnderTest1a.Should().Be(expectedUnitOfTime);
            systemUnderTest1b.Should().Be(expectedUnitOfTime);

            systemUnderTest2a.Should().Be(expectedUnitOfTime);
            systemUnderTest2b.Should().Be(expectedUnitOfTime);
        }

        [Fact]
        public static void Deserialize___Should_return_equivalent_object_of_type_FiscalUnbounded___When_an_object_of_that_type_is_serialized_to_json_and_deserialized_as_abstract_type()
        {
            // Arrange
            var serializer = new NaosJsonSerializer();
            var expectedUnitOfTime = A.Dummy<FiscalUnbounded>();
            var serializedJsonBytes = serializer.SerializeToBytes(expectedUnitOfTime);
            var serializedJsonString = serializer.SerializeToBytes(expectedUnitOfTime);

            // Act
            var systemUnderTest1a = serializer.Deserialize<UnitOfTime>(serializedJsonBytes) as FiscalUnbounded;
            var systemUnderTest1b = serializer.Deserialize<FiscalUnitOfTime>(serializedJsonBytes) as FiscalUnbounded;

            var systemUnderTest2a = serializer.Deserialize<UnitOfTime>(serializedJsonString) as FiscalUnbounded;
            var systemUnderTest2b = serializer.Deserialize<FiscalUnitOfTime>(serializedJsonString) as FiscalUnbounded;

            // Assert
            systemUnderTest1a.Should().Be(expectedUnitOfTime);
            systemUnderTest1b.Should().Be(expectedUnitOfTime);

            systemUnderTest2a.Should().Be(expectedUnitOfTime);
            systemUnderTest2b.Should().Be(expectedUnitOfTime);
        }

        [Fact]
        public static void Deserialize___Should_return_equivalent_object_of_type_GenericMonth___When_an_object_of_that_type_is_serialized_to_json_and_deserialized_as_abstract_type()
        {
            // Arrange
            var serializer = new NaosJsonSerializer();
            var expectedUnitOfTime = A.Dummy<GenericMonth>();
            var serializedJsonBytes = serializer.SerializeToBytes(expectedUnitOfTime);
            var serializedJsonString = serializer.SerializeToBytes(expectedUnitOfTime);

            // Act
            var systemUnderTest1a = serializer.Deserialize<UnitOfTime>(serializedJsonBytes) as GenericMonth;
            var systemUnderTest1b = serializer.Deserialize<GenericUnitOfTime>(serializedJsonBytes) as GenericMonth;

            var systemUnderTest2a = serializer.Deserialize<UnitOfTime>(serializedJsonString) as GenericMonth;
            var systemUnderTest2b = serializer.Deserialize<GenericUnitOfTime>(serializedJsonString) as GenericMonth;

            // Assert
            systemUnderTest1a.Should().Be(expectedUnitOfTime);
            systemUnderTest1b.Should().Be(expectedUnitOfTime);

            systemUnderTest2a.Should().Be(expectedUnitOfTime);
            systemUnderTest2b.Should().Be(expectedUnitOfTime);
        }

        [Fact]
        public static void Deserialize___Should_return_equivalent_object_of_type_GenericQuarter___When_an_object_of_that_type_is_serialized_to_json_and_deserialized_as_abstract_type()
        {
            // Arrange
            var serializer = new NaosJsonSerializer();
            var expectedUnitOfTime = A.Dummy<GenericQuarter>();
            var serializedJsonBytes = serializer.SerializeToBytes(expectedUnitOfTime);
            var serializedJsonString = serializer.SerializeToBytes(expectedUnitOfTime);

            // Act
            var systemUnderTest1a = serializer.Deserialize<UnitOfTime>(serializedJsonBytes) as GenericQuarter;
            var systemUnderTest1b = serializer.Deserialize<GenericUnitOfTime>(serializedJsonBytes) as GenericQuarter;

            var systemUnderTest2a = serializer.Deserialize<UnitOfTime>(serializedJsonString) as GenericQuarter;
            var systemUnderTest2b = serializer.Deserialize<GenericUnitOfTime>(serializedJsonString) as GenericQuarter;

            // Assert
            systemUnderTest1a.Should().Be(expectedUnitOfTime);
            systemUnderTest1b.Should().Be(expectedUnitOfTime);

            systemUnderTest2a.Should().Be(expectedUnitOfTime);
            systemUnderTest2b.Should().Be(expectedUnitOfTime);
        }

        [Fact]
        public static void Deserialize___Should_return_equivalent_object_of_type_GenericYear___When_an_object_of_that_type_is_serialized_to_json_and_deserialized_as_abstract_type()
        {
            // Arrange
            var serializer = new NaosJsonSerializer();
            var expectedUnitOfTime = A.Dummy<GenericYear>();
            var serializedJsonBytes = serializer.SerializeToBytes(expectedUnitOfTime);
            var serializedJsonString = serializer.SerializeToBytes(expectedUnitOfTime);

            // Act
            var systemUnderTest1a = serializer.Deserialize<UnitOfTime>(serializedJsonBytes) as GenericYear;
            var systemUnderTest1b = serializer.Deserialize<GenericUnitOfTime>(serializedJsonBytes) as GenericYear;

            var systemUnderTest2a = serializer.Deserialize<UnitOfTime>(serializedJsonString) as GenericYear;
            var systemUnderTest2b = serializer.Deserialize<GenericUnitOfTime>(serializedJsonString) as GenericYear;

            // Assert
            systemUnderTest1a.Should().Be(expectedUnitOfTime);
            systemUnderTest1b.Should().Be(expectedUnitOfTime);

            systemUnderTest2a.Should().Be(expectedUnitOfTime);
            systemUnderTest2b.Should().Be(expectedUnitOfTime);
        }

        [Fact]
        public static void Deserialize___Should_return_equivalent_object_of_type_GenericUnbounded___When_an_object_of_that_type_is_serialized_to_json_and_deserialized_as_abstract_type()
        {
            // Arrange
            var serializer = new NaosJsonSerializer();
            var expectedUnitOfTime = A.Dummy<GenericUnbounded>();
            var serializedJsonBytes = serializer.SerializeToBytes(expectedUnitOfTime);
            var serializedJsonString = serializer.SerializeToBytes(expectedUnitOfTime);

            // Act
            var systemUnderTest1a = serializer.Deserialize<UnitOfTime>(serializedJsonBytes) as GenericUnbounded;
            var systemUnderTest1b = serializer.Deserialize<GenericUnitOfTime>(serializedJsonBytes) as GenericUnbounded;

            var systemUnderTest2a = serializer.Deserialize<UnitOfTime>(serializedJsonString) as GenericUnbounded;
            var systemUnderTest2b = serializer.Deserialize<GenericUnitOfTime>(serializedJsonString) as GenericUnbounded;

            // Assert
            systemUnderTest1a.Should().Be(expectedUnitOfTime);
            systemUnderTest1b.Should().Be(expectedUnitOfTime);

            systemUnderTest2a.Should().Be(expectedUnitOfTime);
            systemUnderTest2b.Should().Be(expectedUnitOfTime);
        }
    }
}