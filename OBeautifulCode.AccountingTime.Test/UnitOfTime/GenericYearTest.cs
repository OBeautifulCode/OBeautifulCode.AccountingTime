// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GenericYearTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime.Test
{
    using System;

    using AutoFakeItEasy;

    using FakeItEasy;

    using FluentAssertions;

    using Newtonsoft.Json;

    using Spritely.Recipes;

    using Xunit;

    public static class GenericYearTest
    {
        // ReSharper disable InconsistentNaming
        [Fact]
        public static void Constructor___Should_throw_ArgumentOutOfRangeException___When_parameter_year_is_less_than_1()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => new GenericYear(0));
            var ex2 = Record.Exception(() => new GenericYear(A.Dummy<ZeroOrNegativeInteger>()));

            // Assert
            ex1.Should().BeOfType<ArgumentOutOfRangeException>();
            ex2.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void Constructor___Should_throw_ArgumentOutOfRangeException___When_parameter_year_is_greater_than_9999()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => new GenericYear(10000));
            var ex2 = Record.Exception(() => new GenericYear(A.Dummy<PositiveInteger>().ThatIs(y => y > 9999)));

            // Assert
            ex1.Should().BeOfType<ArgumentOutOfRangeException>();
            ex2.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void Year___Should_return_same_year_passed_to_constructor___When_getting()
        {
            // Arrange
            var validYear = A.Dummy<GenericYear>();
            var year = validYear.Year;
            var systemUnderTest = new GenericYear(year);

            // Act
            var actualYear = systemUnderTest.Year;

            // Assert
            actualYear.Should().Be(year);
        }

        [Fact]
        public static void EqualsOperator___Should_return_true___When_both_sides_of_operator_are_null()
        {
            // Arrange
            GenericYear systemUnderTest1 = null;
            GenericYear systemUnderTest2 = null;

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
            GenericYear systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<GenericYear>();

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
            var systemUnderTest = A.Dummy<GenericYear>();

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
        public static void EqualsOperator___Should_return_false___When_objects_being_compared_have_different_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<GenericYear>();
            var systemUnderTest1b = systemUnderTest1a.TweakYear();

            // Act
            var result = systemUnderTest1a == systemUnderTest1b;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void EqualsOperator___Should_return_true___When_objects_being_compared_have_same_property_values()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<GenericYear>();
            var systemUnderTest2 = new GenericYear(systemUnderTest1.Year);

            // Act
            var result = systemUnderTest1 == systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_false___When_both_sides_of_operator_are_null()
        {
            // Arrange
            GenericYear systemUnderTest1 = null;
            GenericYear systemUnderTest2 = null;

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
            GenericYear systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<GenericYear>();

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
            var systemUnderTest = A.Dummy<GenericYear>();

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
        public static void NotEqualsOperator___Should_return_true___When_objects_being_compared_have_different_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<GenericYear>();
            var systemUnderTest1b = systemUnderTest1a.TweakYear();

            // Act
            var result = systemUnderTest1a != systemUnderTest1b;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_false___When_objects_being_compared_have_same_property_values()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<GenericYear>();
            var systemUnderTest2 = new GenericYear(systemUnderTest1.Year);

            // Act
            var result = systemUnderTest1 != systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void Equals___Should_return_false___When_calling_typed_overload_and_parameter_other_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<GenericYear>();

            // Act
            var result = systemUnderTest.Equals(null);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void Equals___Should_return_true___When_calling_typed_overload_and_parameter_other_is_same_object()
        {
            // Arrange
            var systemUnderTest = A.Dummy<GenericYear>();

            // Act
            var result = systemUnderTest.Equals(systemUnderTest);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Equals___Should_return_false___When_calling_typed_overload_and_objects_being_compared_have_different_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<GenericYear>();
            var systemUnderTest1b = systemUnderTest1a.TweakYear();

            // Act
            var result = systemUnderTest1a.Equals(systemUnderTest1b);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void Equals___Should_return_true___When_calling_typed_overload_and_objects_being_compared_have_same_property_values()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<GenericYear>();
            var systemUnderTest2 = new GenericYear(systemUnderTest1.Year);

            // Act
            var result = systemUnderTest1.Equals(systemUnderTest2);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Equals___Should_return_false___When_calling_non_typed_overload_and_parameter_other_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<GenericYear>();

            // Act
            var result = systemUnderTest.Equals(null);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void Equals___Should_return_false___When_calling_non_typed_overload_and_parameter_other_is_not_of_the_same_type()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<GenericYear>();
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
            var systemUnderTest = A.Dummy<GenericYear>();

            // Act
            var result = systemUnderTest.Equals((object)systemUnderTest);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Equals___Should_return_false___When_calling_non_typed_overload_and_objects_being_compared_have_different_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<GenericYear>();
            var systemUnderTest1b = systemUnderTest1a.TweakYear();

            // Act
            var result = systemUnderTest1a.Equals((object)systemUnderTest1b);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void Equals___Should_return_true___When_calling_non_typed_overload_and_objects_being_compared_have_same_property_values()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<GenericYear>();
            var systemUnderTest2 = new GenericYear(systemUnderTest1.Year);

            // Act
            var result = systemUnderTest1.Equals((object)systemUnderTest2);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void GetHashCode___Should_not_be_equal_for_two_GenericYears___When_both_objects_have_different_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<GenericYear>();
            var systemUnderTest1b = systemUnderTest1a.TweakYear();

            // Act
            var hash1a = systemUnderTest1a.GetHashCode();
            var hash1b = systemUnderTest1b.GetHashCode();

            // Assert
            hash1a.Should().NotBe(hash1b);
        }

        [Fact]
        public static void GetHashCode___Should_be_equal_for_two_GenericYears___When_both_objects_have_the_same_property_values()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<GenericYear>();
            var systemUnderTest2 = new GenericYear(systemUnderTest1.Year);

            // Act
            var hash1 = systemUnderTest1.GetHashCode();
            var hash2 = systemUnderTest2.GetHashCode();

            // Assert
            hash1.Should().Be(hash2);
        }

        [Fact]
        public static void LessThanOperator___Should_return_false___When_both_sides_of_operator_are_null()
        {
            // Arrange
            GenericYear systemUnderTest1 = null;
            GenericYear systemUnderTest2 = null;

            // Act
            // ReSharper disable ExpressionIsAlwaysNull
            var result = systemUnderTest1 < systemUnderTest2;
            // ReSharper restore ExpressionIsAlwaysNull

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void LessThanOperator___Should_return_true___When_left_side_of_operator_is_null_and_right_side_is_not_null()
        {
            // Arrange
            GenericYear systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<GenericYear>();

            // Act
            // ReSharper disable ExpressionIsAlwaysNull
            var result = systemUnderTest1 < systemUnderTest2;
            // ReSharper restore ExpressionIsAlwaysNull

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void LessThanOperator___Should_return_false___When_left_side_of_operator_is_not_null_and_right_side_is_null()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<GenericYear>();
            GenericYear systemUnderTest2 = null;

            // Act
            // ReSharper disable ExpressionIsAlwaysNull
            var result = systemUnderTest1 < systemUnderTest2;
            // ReSharper restore ExpressionIsAlwaysNull

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void LessThanOperator___Should_return_false___When_left_side_of_operator_is_equal_to_right_side()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<GenericYear>();
            var systemUnderTest2 = new GenericYear(systemUnderTest1.Year);

            // Act
            var result = systemUnderTest1 < systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void LessThanOperator___Should_return_true___When_left_side_of_operator_is_less_than_right_side()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<GenericYear>();
            var systemUnderTest1b = systemUnderTest1a.TweakYearByAmount(1);

            // Act
            var result = systemUnderTest1a < systemUnderTest1b;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void LessThanOperator___Should_return_false___When_left_side_of_operator_is_greater_than_right_side()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<GenericYear>();
            var systemUnderTest1b = systemUnderTest1a.TweakYearByAmount(-1);

            // Act
            var result = systemUnderTest1a < systemUnderTest1b;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void LessThanOrEqualToOperator___Should_return_true___When_both_sides_of_operator_are_null()
        {
            // Arrange
            GenericYear systemUnderTest1 = null;
            GenericYear systemUnderTest2 = null;

            // Act
            // ReSharper disable ExpressionIsAlwaysNull
            var result = systemUnderTest1 <= systemUnderTest2;
            // ReSharper restore ExpressionIsAlwaysNull

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void LessThanOrEqualToOperator___Should_return_true___When_left_side_of_operator_is_null_and_right_side_is_not_null()
        {
            // Arrange
            GenericYear systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<GenericYear>();

            // Act
            // ReSharper disable ExpressionIsAlwaysNull
            var result = systemUnderTest1 <= systemUnderTest2;
            // ReSharper restore ExpressionIsAlwaysNull

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void LessThanOrEqualToOperator___Should_return_false___When_left_side_of_operator_is_not_null_and_right_side_is_null()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<GenericYear>();
            GenericYear systemUnderTest2 = null;

            // Act
            // ReSharper disable ExpressionIsAlwaysNull
            var result = systemUnderTest1 <= systemUnderTest2;
            // ReSharper restore ExpressionIsAlwaysNull

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void LessThanOrEqualToOperator___Should_return_true___When_left_side_of_operator_is_equal_to_right_side()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<GenericYear>();
            var systemUnderTest2 = new GenericYear(systemUnderTest1.Year);

            // Act
            var result = systemUnderTest1 <= systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void LessThanOrEqualToOperator___Should_return_true___When_left_side_of_operator_is_less_than_right_side()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<GenericYear>();
            var systemUnderTest1b = systemUnderTest1a.TweakYearByAmount(1);

            // Act
            var result = systemUnderTest1a <= systemUnderTest1b;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void LessThanOrEqualToOperator___Should_return_false___When_left_side_of_operator_is_greater_than_right_side()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<GenericYear>();
            var systemUnderTest1b = systemUnderTest1a.TweakYearByAmount(-1);

            // Act
            var result = systemUnderTest1a <= systemUnderTest1b;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void GreaterThanOperator___Should_return_false___When_both_sides_of_operator_are_null()
        {
            // Arrange
            GenericYear systemUnderTest1 = null;
            GenericYear systemUnderTest2 = null;

            // Act
            // ReSharper disable ExpressionIsAlwaysNull
            var result = systemUnderTest1 > systemUnderTest2;
            // ReSharper restore ExpressionIsAlwaysNull

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void GreaterThanOperator___Should_return_false___When_left_side_of_operator_is_null_and_right_side_is_not_null()
        {
            // Arrange
            GenericYear systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<GenericYear>();

            // Act
            // ReSharper disable ExpressionIsAlwaysNull
            var result = systemUnderTest1 > systemUnderTest2;
            // ReSharper restore ExpressionIsAlwaysNull

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void GreaterThanOperator___Should_return_true___When_left_side_of_operator_is_not_null_and_right_side_is_null()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<GenericYear>();
            GenericYear systemUnderTest2 = null;

            // Act
            // ReSharper disable ExpressionIsAlwaysNull
            var result = systemUnderTest1 > systemUnderTest2;
            // ReSharper restore ExpressionIsAlwaysNull

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void GreaterThanOperator___Should_return_false___When_left_side_of_operator_is_equal_to_right_side()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<GenericYear>();
            var systemUnderTest2 = new GenericYear(systemUnderTest1.Year);

            // Act
            var result = systemUnderTest1 > systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void GreaterThanOperator___Should_return_false___When_left_side_of_operator_is_less_than_right_side()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<GenericYear>();
            var systemUnderTest1b = systemUnderTest1a.TweakYearByAmount(1);

            // Act
            var result = systemUnderTest1a > systemUnderTest1b;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void GreaterThanOperator___Should_return_true___When_left_side_of_operator_is_greater_than_right_side()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<GenericYear>();
            var systemUnderTest1b = systemUnderTest1a.TweakYearByAmount(-1);

            // Act
            var result = systemUnderTest1a > systemUnderTest1b;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void GreaterThanOrEqualToOperator___Should_return_true___When_both_sides_of_operator_are_null()
        {
            // Arrange
            GenericYear systemUnderTest1 = null;
            GenericYear systemUnderTest2 = null;

            // Act
            // ReSharper disable ExpressionIsAlwaysNull
            var result = systemUnderTest1 >= systemUnderTest2;
            // ReSharper restore ExpressionIsAlwaysNull

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void GreaterThanOrEqualToOperator___Should_return_false___When_left_side_of_operator_is_null_and_right_side_is_not_null()
        {
            // Arrange
            GenericYear systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<GenericYear>();

            // Act
            // ReSharper disable ExpressionIsAlwaysNull
            var result = systemUnderTest1 >= systemUnderTest2;
            // ReSharper restore ExpressionIsAlwaysNull

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void GreaterThanOrEqualToOperator___Should_return_true___When_left_side_of_operator_is_not_null_and_right_side_is_null()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<GenericYear>();
            GenericYear systemUnderTest2 = null;

            // Act
            // ReSharper disable ExpressionIsAlwaysNull
            var result = systemUnderTest1 >= systemUnderTest2;
            // ReSharper restore ExpressionIsAlwaysNull

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void GreaterThanOrEqualToOperator___Should_return_true___When_left_side_of_operator_is_equal_to_right_side()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<GenericYear>();
            var systemUnderTest2 = new GenericYear(systemUnderTest1.Year);

            // Act
            var result = systemUnderTest1 >= systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void GreaterThanOrEqualToOperator___Should_return_false___When_left_side_of_operator_is_less_than_right_side()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<GenericYear>();
            var systemUnderTest1b = systemUnderTest1a.TweakYearByAmount(1);

            // Act
            var result = systemUnderTest1a >= systemUnderTest1b;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void GreaterThanOrEqualToOperator___Should_return_true___When_left_side_of_operator_is_greater_than_right_side()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<GenericYear>();
            var systemUnderTest1b = systemUnderTest1a.TweakYearByAmount(-1);

            // Act
            var result = systemUnderTest1a >= systemUnderTest1b;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void CompareTo___Should_return_1___When_calling_typed_overload_and_other_object_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<GenericYear>();

            // Act
            var result = systemUnderTest.CompareTo(null);

            // Assert
            result.Should().Be(1);
        }

        [Fact]
        public static void CompareTo___Should_return_negative_1___When_calling_typed_overload_and_test_object_is_less_than_other_object()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<GenericYear>();
            var systemUnderTest1b = systemUnderTest1a.TweakYearByAmount(1);

            // Act
            var result = systemUnderTest1a.CompareTo(systemUnderTest1b);

            // Assert
            result.Should().Be(-1);
        }

        [Fact]
        public static void CompareTo___Should_return_1___When_calling_typed_overload_and_test_object_is_greater_than_other_object()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<GenericYear>();
            var systemUnderTest1b = systemUnderTest1a.TweakYearByAmount(-1);

            // Act
            var result = systemUnderTest1a.CompareTo(systemUnderTest1b);

            // Assert
            result.Should().Be(1);
        }

        [Fact]
        public static void CompareTo___Should_return_0___When_calling_typed_overload_and_test_object_is_equal_to_other_object()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<GenericYear>();
            var systemUnderTest2 = new GenericYear(systemUnderTest1.Year);

            // Act
            var result = systemUnderTest1.CompareTo(systemUnderTest2);

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public static void CompareTo___Should_throw_ArgumentException___When_calling_non_typed_overload_and_other_object_is_not_of_same_type_as_test_object()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<GenericYear>();
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
            var systemUnderTest1 = A.Dummy<GenericYear>();
            GenericYear systemUnderTest2 = null;

            // Act
            // ReSharper disable ExpressionIsAlwaysNull
            var ex = Record.Exception(() => systemUnderTest1.CompareTo((object)systemUnderTest2));
            // ReSharper restore ExpressionIsAlwaysNull

            // Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void CompareTo___Should_return_negative_1___When_calling_non_typed_overload_and_test_object_is_less_than_other_object()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<GenericYear>();
            var systemUnderTest1b = systemUnderTest1a.TweakYearByAmount(1);

            // Act
            var result = systemUnderTest1a.CompareTo((object)systemUnderTest1b);

            // Assert
            result.Should().Be(-1);
        }

        [Fact]
        public static void CompareTo___Should_return_1___When_calling_non_typed_overload_and_test_object_is_greater_than_other_object()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<GenericYear>();
            var systemUnderTest1b = systemUnderTest1a.TweakYearByAmount(-1);

            // Act
            var result = systemUnderTest1a.CompareTo((object)systemUnderTest1b);

            // Assert
            result.Should().Be(1);
        }

        [Fact]
        public static void CompareTo___Should_return_0___When_calling_non_typed_overload_and_test_object_is_equal_to_other_object()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<GenericYear>();
            var systemUnderTest2 = new GenericYear(systemUnderTest1.Year);

            // Act
            var result = systemUnderTest1.CompareTo((object)systemUnderTest2);

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public static void ToString___Should_return_friendly_string_representation_of_object___When_called()
        {
            // Arrange
            var systemUnderTest = new GenericYear(2017);

            // Act
            var toString = systemUnderTest.ToString();

            // Assert
            toString.Should().Be("2017");
        }

        [Fact]
        public static void Clone___Should_return_a_clone_of_the_object___When_called()
        {
            // Arrange
            var systemUnderTest = A.Dummy<GenericYear>();

            // Act
            var clone = systemUnderTest.Clone();

            // Assert
            clone.Should().Be(systemUnderTest);
            clone.Should().NotBeSameAs(systemUnderTest);
        }

        [Fact]
        public static void Deserialize___Should_return_equivalent_object_of_type_GenericYear___When_an_object_of_that_type_is_serialized_to_json_and_deserialized_as_abstract_type()
        {
            // Arrange
            var settings = JsonConfiguration.DefaultSerializerSettings;
            var expectedUnitOfTime = A.Dummy<GenericYear>();
            var serializedJson = JsonConvert.SerializeObject(expectedUnitOfTime, settings);

            // Act
            var systemUnderTest1 = JsonConvert.DeserializeObject<UnitOfTime>(serializedJson, settings) as GenericYear;
            var systemUnderTest2 = JsonConvert.DeserializeObject<GenericUnitOfTime>(serializedJson, settings) as GenericYear;

            // Assert
            // ReSharper disable PossibleNullReferenceException
            systemUnderTest1.Should().Be(expectedUnitOfTime);
            systemUnderTest2.Should().Be(expectedUnitOfTime);
            // ReSharper restore PossibleNullReferenceException
        }

        [Fact]
        public static void UnitOfTimeKind__Should_return_Generic___When_called()
        {
            // Arrange
            var unitOfTime = A.Dummy<GenericYear>();

            // Act
            var kind = unitOfTime.UnitOfTimeKind;

            // Assert
            kind.Should().Be(UnitOfTimeKind.Generic);
        }

        [Fact]
        public static void UnitOfTimeGranularity__Should_return_Year___When_called()
        {
            // Arrange
            var unitOfTime = A.Dummy<GenericYear>();

            // Act
            var granularity = unitOfTime.UnitOfTimeGranularity;

            // Assert
            granularity.Should().Be(UnitOfTimeGranularity.Year);
        }

        private static GenericYear TweakYear(this GenericYear genericYear)
        {
            var tweakedYear = A.Dummy<PositiveInteger>().ThatIs(y => y != genericYear.Year && y <= 9999);
            var result = new GenericYear(tweakedYear);
            return result;
        }

        private static GenericYear TweakYearByAmount(this GenericYear genericYear, int amount)
        {
            var result = new GenericYear(genericYear.Year + amount);
            return result;
        }

        // ReSharper restore InconsistentNaming
    }
}

// ReSharper restore CheckNamespace