// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FiscalUnboundedTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime.Test
{
    using System;

    using FakeItEasy;

    using FluentAssertions;

    using Newtonsoft.Json;

    using Spritely.Recipes;

    using Xunit;

    public static class FiscalUnboundedTest
    {
        // ReSharper disable InconsistentNaming
        [Fact]
        public static void EqualsOperator___Should_return_true___When_both_sides_of_operator_are_null()
        {
            // Arrange
            FiscalUnbounded systemUnderTest1 = null;
            FiscalUnbounded systemUnderTest2 = null;

            // Act
            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            var result = systemUnderTest1 == systemUnderTest2;

            // Assert
            result.Should().BeTrue();
            // ReSharper restore ConditionIsAlwaysTrueOrFalse
        }

        [Fact]
        public static void EqualsOperator___Should_return_false___When_one_side_of_operator_is_null_and_the_other_side_is_not_null()
        {
            // Arrange
            FiscalUnbounded systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<FiscalUnbounded>();

            // Act
            // ReSharper disable ExpressionIsAlwaysNull
            var result1 = systemUnderTest1 == systemUnderTest2;
            var result2 = systemUnderTest2 == systemUnderTest1;

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [Fact]
        public static void EqualsOperator___Should_return_true___When_same_object_is_on_both_sides_of_operator()
        {
            // Arrange
            var systemUnderTest = A.Dummy<FiscalUnbounded>();

            // Act
            // ReSharper disable EqualExpressionComparison
#pragma warning disable CS1718 // Comparison made to same variable
            var result = systemUnderTest == systemUnderTest;
#pragma warning restore CS1718 // Comparison made to same variable

            // Assert
            result.Should().BeTrue();
            // ReSharper restore EqualExpressionComparison
        }

        [Fact]
        public static void EqualsOperator___Should_return_true___When_different_objects_on_both_sides_of_operator()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<FiscalUnbounded>();
            var systemUnderTest2 = A.Dummy<FiscalUnbounded>();

            // Act
            var result = systemUnderTest1 == systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_false___When_both_sides_of_operator_are_null()
        {
            // Arrange
            FiscalUnbounded systemUnderTest1 = null;
            FiscalUnbounded systemUnderTest2 = null;

            // Act
            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            var result = systemUnderTest1 != systemUnderTest2;

            // Assert
            result.Should().BeFalse();
            // ReSharper restore ConditionIsAlwaysTrueOrFalse
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_true___When_one_side_of_operator_is_null_and_the_other_side_is_not_null()
        {
            // Arrange
            FiscalUnbounded systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<FiscalUnbounded>();

            // Act
            // ReSharper disable ExpressionIsAlwaysNull
            var result1 = systemUnderTest1 != systemUnderTest2;
            var result2 = systemUnderTest2 != systemUnderTest1;

            // Assert
            result1.Should().BeTrue();
            result2.Should().BeTrue();
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_false___When_same_object_is_on_both_sides_of_operator()
        {
            // Arrange
            var systemUnderTest = A.Dummy<FiscalUnbounded>();

            // Act
            // ReSharper disable EqualExpressionComparison
#pragma warning disable CS1718 // Comparison made to same variable
            var result = systemUnderTest != systemUnderTest;
#pragma warning restore CS1718 // Comparison made to same variable

            // Assert
            result.Should().BeFalse();
            // ReSharper restore EqualExpressionComparison
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_false___When_different_objects_on_both_sides_of_operator()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<FiscalUnbounded>();
            var systemUnderTest2 = A.Dummy<FiscalUnbounded>();

            // Act
            var result = systemUnderTest1 != systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void Equals___Should_return_false___When_calling_typed_overload_and_parameter_other_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<FiscalUnbounded>();

            // Act
            var result = systemUnderTest.Equals(null);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void Equals___Should_return_true___When_calling_typed_overload_and_parameter_other_is_same_object()
        {
            // Arrange
            var systemUnderTest = A.Dummy<FiscalUnbounded>();

            // Act
            var result = systemUnderTest.Equals(systemUnderTest);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Equals___Should_return_true___When_calling_typed_overload_and_parameter_other_is_different_object()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<FiscalUnbounded>();
            var systemUnderTest2 = A.Dummy<FiscalUnbounded>();

            // Act
            var result = systemUnderTest1.Equals(systemUnderTest2);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Equals___Should_return_false___When_calling_non_typed_overload_and_parameter_other_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<FiscalUnbounded>();

            // Act
            var result = systemUnderTest.Equals(null);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void Equals___Should_return_false___When_calling_non_typed_overload_and_parameter_other_is_not_of_the_same_type()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<FiscalUnbounded>();
            var systemUnderTest2 = A.Dummy<FiscalMonth>();

            // Act
            // ReSharper disable SuspiciousTypeConversion.Global
            // ReSharper disable RedundantCast
            var result = systemUnderTest1.Equals((object)systemUnderTest2);
            // ReSharper restore RedundantCast
            // ReSharper restore SuspiciousTypeConversion.Global

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void Equals___Should_return_true___When_calling_non_typed_overload_and_parameter_other_is_same_object()
        {
            // Arrange
            var systemUnderTest = A.Dummy<FiscalUnbounded>();

            // Act
            var result = systemUnderTest.Equals((object)systemUnderTest);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Equals___Should_return_true___When_calling_non_typed_overload_and_parameter_other_is_different_object()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<FiscalUnbounded>();
            var systemUnderTest2 = A.Dummy<FiscalUnbounded>();

            // Act
            var result = systemUnderTest1.Equals((object)systemUnderTest2);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void GetHashCode___Should_be_equal_for_two_FiscalUnbounded___When_called()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<FiscalUnbounded>();
            var systemUnderTest2 = A.Dummy<FiscalUnbounded>();

            // Act
            var hash1 = systemUnderTest1.GetHashCode();
            var hash2 = systemUnderTest2.GetHashCode();

            // Assert
            hash1.Should().Be(hash2);
        }

        [Fact]
        public static void CompareTo___Should_throw_ArgumentException___When_calling_non_typed_overload_and_other_object_is_not_of_same_type_as_test_object()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<FiscalUnbounded>();
            var systemUnderTest2 = A.Dummy<FiscalMonth>();

            // Act
            // ReSharper disable RedundantCast
            var ex = Record.Exception(() => systemUnderTest1.CompareTo((object)systemUnderTest2));
            // ReSharper restore RedundantCast

            // Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void CompareTo___Should_throw_ArgumentException___When_calling_non_typed_overload_and_other_object_is_null()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<FiscalUnbounded>();
            FiscalUnbounded systemUnderTest2 = null;

            // Act
            // ReSharper disable ExpressionIsAlwaysNull
            // ReSharper disable RedundantCast
            var ex = Record.Exception(() => systemUnderTest1.CompareTo((object)systemUnderTest2));
            // ReSharper restore RedundantCast
            // ReSharper restore ExpressionIsAlwaysNull

            // Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void CompareTo___Should_return_0___When_calling_non_typed_overload_and_other_object_is_not_null()
        {
            // Arrange
            var systemUnderTest1 = new FiscalUnbounded();
            var systemUnderTest2 = new FiscalUnbounded();

            // Act
            // ReSharper disable RedundantCast
            var result = systemUnderTest1.CompareTo((object)systemUnderTest2);
            // ReSharper restore RedundantCast

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public static void ToString___Should_return_friendly_string_representation_of_object___When_called()
        {
            // Arrange
            var systemUnderTest = new FiscalUnbounded();

            // Act
            var toString = systemUnderTest.ToString();

            // Assert
            toString.Should().Be("fiscal unbounded");
        }

        [Fact]
        public static void Clone___Should_return_a_clone_of_the_object___When_called()
        {
            // Arrange
            var systemUnderTest = A.Dummy<FiscalUnbounded>();

            // Act
            var clone = systemUnderTest.Clone();

            // Assert
            clone.Should().Be(systemUnderTest);
            clone.Should().NotBeSameAs(systemUnderTest);
        }

        [Fact]
        public static void Deserialize___Should_return_equivalent_object_of_type_FiscalUnbounded___When_an_object_of_that_type_is_serialized_to_json_and_deserialized_as_abstract_type()
        {
            // Arrange
            var settings = JsonConfiguration.DefaultSerializerSettings;
            var expectedUnitOfTime = A.Dummy<FiscalUnbounded>();
            var serializedJson = JsonConvert.SerializeObject(expectedUnitOfTime, settings);

            // Act
            var systemUnderTest1 = JsonConvert.DeserializeObject<UnitOfTime>(serializedJson, settings) as FiscalUnbounded;
            var systemUnderTest2 = JsonConvert.DeserializeObject<FiscalUnitOfTime>(serializedJson, settings) as FiscalUnbounded;

            // Assert
            // ReSharper disable PossibleNullReferenceException
            systemUnderTest1.Should().Be(expectedUnitOfTime);
            systemUnderTest2.Should().Be(expectedUnitOfTime);
            // ReSharper restore PossibleNullReferenceException
        }

        [Fact]
        public static void UnitOfTimeKind__Should_return_Fiscal___When_called()
        {
            // Arrange
            var unitOfTime = A.Dummy<FiscalUnbounded>();

            // Act
            var kind = unitOfTime.UnitOfTimeKind;

            // Assert
            kind.Should().Be(UnitOfTimeKind.Fiscal);
        }

        [Fact]
        public static void UnitOfTimeGranularity__Should_return_Unbounded___When_called()
        {
            // Arrange
            var unitOfTime = A.Dummy<FiscalUnbounded>();

            // Act
            var granularity = unitOfTime.UnitOfTimeGranularity;

            // Assert
            granularity.Should().Be(UnitOfTimeGranularity.Unbounded);
        }

        // ReSharper restore InconsistentNaming
    }
}

// ReSharper restore CheckNamespace