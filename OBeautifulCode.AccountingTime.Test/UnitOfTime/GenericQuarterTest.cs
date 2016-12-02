// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GenericQuarterTest.cs" company="OBeautifulCode">
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

    using Xunit;

    public static class GenericQuarterTest
    {
        // ReSharper disable InconsistentNaming
        private enum GenericQuarterComponent
        {
            Quarter,

            Year
        }

        [Fact]
        public static void Constructor___Should_throw_ArgumentOutOfRangeException___When_parameter_year_is_less_than_1()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => new GenericQuarter(0, A.Dummy<QuarterNumber>()));
            var ex2 = Record.Exception(() => new GenericQuarter(A.Dummy<ZeroOrNegativeInteger>(), A.Dummy<QuarterNumber>()));

            // Assert
            ex1.Should().BeOfType<ArgumentOutOfRangeException>();
            ex2.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void Constructor___Should_throw_ArgumentOutOfRangeException___When_parameter_year_is_greater_than_9999()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => new GenericQuarter(10000, A.Dummy<QuarterNumber>()));
            var ex2 = Record.Exception(() => new GenericQuarter(A.Dummy<PositiveInteger>().ThatIs(y => y > 9999), A.Dummy<QuarterNumber>()));

            // Assert
            ex1.Should().BeOfType<ArgumentOutOfRangeException>();
            ex2.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void Constructor___Should_throw_ArgumentException___When_parameter_quarterNumber_is_Invalid()
        {
            // Arrange
            var validQuarter = A.Dummy<GenericQuarter>();

            // Act
            var ex = Record.Exception(() => new GenericQuarter(validQuarter.Year, QuarterNumber.Invalid));

            // Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void Year___Should_return_same_year_passed_to_constructor___When_getting()
        {
            // Arrange
            var validQuarter = A.Dummy<GenericQuarter>();

            var year = validQuarter.Year;
            var quarter = validQuarter.QuarterNumber;

            var systemUnderTest = new GenericQuarter(year, quarter);

            // Act
            var actualYear = systemUnderTest.Year;

            // Assert
            actualYear.Should().Be(year);
        }

        [Fact]
        public static void QuarterNumber___Should_return_same_quarterNumber_passed_to_constructor___When_getting()
        {
            // Arrange
            var validQuarter = A.Dummy<GenericQuarter>();

            var year = validQuarter.Year;
            var quarter = validQuarter.QuarterNumber;

            var systemUnderTest = new GenericQuarter(year, quarter);

            // Act
            var actualQuarter = systemUnderTest.QuarterNumber;

            // Assert
            actualQuarter.Should().Be(quarter);
        }

        [Fact]
        public static void EqualsOperator___Should_return_true___When_both_sides_of_operator_are_null()
        {
            // Arrange
            GenericQuarter systemUnderTest1 = null;
            GenericQuarter systemUnderTest2 = null;

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
            GenericQuarter systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<GenericQuarter>();

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
            var systemUnderTest = A.Dummy<GenericQuarter>();

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
            var systemUnderTest2a = A.Dummy<GenericQuarter>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfGenericQuarter(GenericQuarterComponent.Quarter);

            var systemUnderTest3a = A.Dummy<GenericQuarter>();
            var systemUnderTest3b = systemUnderTest3a.TweakComponentOfGenericQuarter(GenericQuarterComponent.Year);

            // Act
            var result2 = systemUnderTest2a == systemUnderTest2b;
            var result3 = systemUnderTest3a == systemUnderTest3b;

            // Assert
            result2.Should().BeFalse();
            result3.Should().BeFalse();
        }

        [Fact]
        public static void EqualsOperator___Should_return_true___When_objects_being_compared_have_same_property_values()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<GenericQuarter>();
            var systemUnderTest2 = new GenericQuarter(systemUnderTest1.Year, systemUnderTest1.QuarterNumber);

            // Act
            var result = systemUnderTest1 == systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_false___When_both_sides_of_operator_are_null()
        {
            // Arrange
            GenericQuarter systemUnderTest1 = null;
            GenericQuarter systemUnderTest2 = null;

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
            GenericQuarter systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<GenericQuarter>();

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
            var systemUnderTest = A.Dummy<GenericQuarter>();

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
            var systemUnderTest2a = A.Dummy<GenericQuarter>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfGenericQuarter(GenericQuarterComponent.Quarter);

            var systemUnderTest3a = A.Dummy<GenericQuarter>();
            var systemUnderTest3b = systemUnderTest3a.TweakComponentOfGenericQuarter(GenericQuarterComponent.Year);

            // Act
            var result2 = systemUnderTest2a != systemUnderTest2b;
            var result3 = systemUnderTest3a != systemUnderTest3b;

            // Assert
            result2.Should().BeTrue();
            result3.Should().BeTrue();
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_false___When_objects_being_compared_have_same_property_values()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<GenericQuarter>();
            var systemUnderTest2 = new GenericQuarter(systemUnderTest1.Year, systemUnderTest1.QuarterNumber);

            // Act
            var result = systemUnderTest1 != systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void Equals___Should_return_false___When_calling_typed_overload_and_parameter_other_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<GenericQuarter>();

            // Act
            var result = systemUnderTest.Equals(null);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void Equals___Should_return_true___When_calling_typed_overload_and_parameter_other_is_same_object()
        {
            // Arrange
            var systemUnderTest = A.Dummy<GenericQuarter>();

            // Act
            var result = systemUnderTest.Equals(systemUnderTest);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Equals___Should_return_false___When_calling_typed_overload_and_objects_being_compared_have_different_property_values()
        {
            // Arrange
            var systemUnderTest2a = A.Dummy<GenericQuarter>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfGenericQuarter(GenericQuarterComponent.Quarter);

            var systemUnderTest3a = A.Dummy<GenericQuarter>();
            var systemUnderTest3b = systemUnderTest3a.TweakComponentOfGenericQuarter(GenericQuarterComponent.Year);

            // Act
            var result2 = systemUnderTest2a.Equals(systemUnderTest2b);
            var result3 = systemUnderTest3a.Equals(systemUnderTest3b);

            // Assert
            result2.Should().BeFalse();
            result3.Should().BeFalse();
        }

        [Fact]
        public static void Equals___Should_return_true___When_calling_typed_overload_and_objects_being_compared_have_same_property_values()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<GenericQuarter>();
            var systemUnderTest2 = new GenericQuarter(systemUnderTest1.Year, systemUnderTest1.QuarterNumber);

            // Act
            var result = systemUnderTest1.Equals(systemUnderTest2);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Equals___Should_return_false___When_calling_non_typed_overload_and_parameter_other_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<GenericQuarter>();

            // Act
            var result = systemUnderTest.Equals(null);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void Equals___Should_return_false___When_calling_non_typed_overload_and_parameter_other_is_not_of_the_same_type()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<GenericQuarter>();
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
            var systemUnderTest = A.Dummy<GenericQuarter>();

            // Act
            var result = systemUnderTest.Equals((object)systemUnderTest);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Equals___Should_return_false___When_calling_non_typed_overload_and_objects_being_compared_have_different_property_values()
        {
            // Arrange
            var systemUnderTest2a = A.Dummy<GenericQuarter>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfGenericQuarter(GenericQuarterComponent.Quarter);

            var systemUnderTest3a = A.Dummy<GenericQuarter>();
            var systemUnderTest3b = systemUnderTest3a.TweakComponentOfGenericQuarter(GenericQuarterComponent.Year);

            // Act
            var result2 = systemUnderTest2a.Equals((object)systemUnderTest2b);
            var result3 = systemUnderTest3a.Equals((object)systemUnderTest3b);

            // Assert
            result2.Should().BeFalse();
            result3.Should().BeFalse();
        }

        [Fact]
        public static void Equals___Should_return_true___When_calling_non_typed_overload_and_objects_being_compared_have_same_property_values()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<GenericQuarter>();
            var systemUnderTest2 = new GenericQuarter(systemUnderTest1.Year, systemUnderTest1.QuarterNumber);

            // Act
            var result = systemUnderTest1.Equals((object)systemUnderTest2);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void GetHashCode___Should_not_be_equal_for_two_GenericQuarters___When_both_objects_have_different_property_values()
        {
            // Arrange
            var systemUnderTest2a = A.Dummy<GenericQuarter>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfGenericQuarter(GenericQuarterComponent.Quarter);

            var systemUnderTest3a = A.Dummy<GenericQuarter>();
            var systemUnderTest3b = systemUnderTest3a.TweakComponentOfGenericQuarter(GenericQuarterComponent.Year);

            // Act
            var hash2a = systemUnderTest2a.GetHashCode();
            var hash2b = systemUnderTest2b.GetHashCode();

            var hash3a = systemUnderTest3a.GetHashCode();
            var hash3b = systemUnderTest3b.GetHashCode();

            // Assert
            hash2a.Should().NotBe(hash2b);
            hash3a.Should().NotBe(hash3b);
        }

        [Fact]
        public static void GetHashCode___Should_be_equal_for_two_GenericQuarters___When_both_objects_have_the_same_property_values()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<GenericQuarter>();
            var systemUnderTest2 = new GenericQuarter(systemUnderTest1.Year, systemUnderTest1.QuarterNumber);

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
            GenericQuarter systemUnderTest1 = null;
            GenericQuarter systemUnderTest2 = null;

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
            GenericQuarter systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<GenericQuarter>();

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
            var systemUnderTest1 = A.Dummy<GenericQuarter>();
            GenericQuarter systemUnderTest2 = null;

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
            var systemUnderTest1 = A.Dummy<GenericQuarter>();
            var systemUnderTest2 = new GenericQuarter(systemUnderTest1.Year, systemUnderTest1.QuarterNumber);

            // Act
            var result = systemUnderTest1 < systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void LessThanOperator___Should_return_true___When_left_side_of_operator_is_less_than_right_side()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<GenericQuarter>();
            var systemUnderTest1b = systemUnderTest1a.Plus(1, GenericQuarterComponent.Quarter);

            var systemUnderTest2a = A.Dummy<GenericQuarter>();
            var systemUnderTest2b = systemUnderTest2a.Plus(1, GenericQuarterComponent.Year);

            // Act
            var result1 = systemUnderTest1a < systemUnderTest1b;
            var result2 = systemUnderTest2a < systemUnderTest2b;

            // Assert
            result1.Should().BeTrue();
            result2.Should().BeTrue();
        }

        [Fact]
        public static void LessThanOperator___Should_return_false___When_left_side_of_operator_is_greater_than_right_side()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<GenericQuarter>();
            var systemUnderTest1b = systemUnderTest1a.Plus(-1, GenericQuarterComponent.Quarter);

            var systemUnderTest2a = A.Dummy<GenericQuarter>();
            var systemUnderTest2b = systemUnderTest2a.Plus(-1, GenericQuarterComponent.Year);

            // Act
            var result1 = systemUnderTest1a < systemUnderTest1b;
            var result2 = systemUnderTest2a < systemUnderTest2b;

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
        }

        [Fact]
        public static void LessThanOrEqualToOperator___Should_return_true___When_both_sides_of_operator_are_null()
        {
            // Arrange
            GenericQuarter systemUnderTest1 = null;
            GenericQuarter systemUnderTest2 = null;

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
            GenericQuarter systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<GenericQuarter>();

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
            var systemUnderTest1 = A.Dummy<GenericQuarter>();
            GenericQuarter systemUnderTest2 = null;

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
            var systemUnderTest1 = A.Dummy<GenericQuarter>();
            var systemUnderTest2 = new GenericQuarter(systemUnderTest1.Year, systemUnderTest1.QuarterNumber);

            // Act
            var result = systemUnderTest1 <= systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void LessThanOrEqualToOperator___Should_return_true___When_left_side_of_operator_is_less_than_right_side()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<GenericQuarter>();
            var systemUnderTest1b = systemUnderTest1a.Plus(1, GenericQuarterComponent.Quarter);

            var systemUnderTest2a = A.Dummy<GenericQuarter>();
            var systemUnderTest2b = systemUnderTest2a.Plus(1, GenericQuarterComponent.Year);

            // Act
            var result1 = systemUnderTest1a <= systemUnderTest1b;
            var result2 = systemUnderTest2a <= systemUnderTest2b;

            // Assert
            result1.Should().BeTrue();
            result2.Should().BeTrue();
        }

        [Fact]
        public static void LessThanOrEqualToOperator___Should_return_false___When_left_side_of_operator_is_greater_than_right_side()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<GenericQuarter>();
            var systemUnderTest1b = systemUnderTest1a.Plus(-1, GenericQuarterComponent.Quarter);

            var systemUnderTest2a = A.Dummy<GenericQuarter>();
            var systemUnderTest2b = systemUnderTest2a.Plus(-1, GenericQuarterComponent.Year);

            // Act
            var result1 = systemUnderTest1a <= systemUnderTest1b;
            var result2 = systemUnderTest2a <= systemUnderTest2b;

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
        }

        [Fact]
        public static void GreaterThanOperator___Should_return_false___When_both_sides_of_operator_are_null()
        {
            // Arrange
            GenericQuarter systemUnderTest1 = null;
            GenericQuarter systemUnderTest2 = null;

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
            GenericQuarter systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<GenericQuarter>();

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
            var systemUnderTest1 = A.Dummy<GenericQuarter>();
            GenericQuarter systemUnderTest2 = null;

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
            var systemUnderTest1 = A.Dummy<GenericQuarter>();
            var systemUnderTest2 = new GenericQuarter(systemUnderTest1.Year, systemUnderTest1.QuarterNumber);

            // Act
            var result = systemUnderTest1 > systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void GreaterThanOperator___Should_return_false___When_left_side_of_operator_is_less_than_right_side()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<GenericQuarter>();
            var systemUnderTest1b = systemUnderTest1a.Plus(1, GenericQuarterComponent.Quarter);

            var systemUnderTest2a = A.Dummy<GenericQuarter>();
            var systemUnderTest2b = systemUnderTest2a.Plus(1, GenericQuarterComponent.Year);

            // Act
            var result1 = systemUnderTest1a > systemUnderTest1b;
            var result2 = systemUnderTest2a > systemUnderTest2b;

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
        }

        [Fact]
        public static void GreaterThanOperator___Should_return_true___When_left_side_of_operator_is_greater_than_right_side()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<GenericQuarter>();
            var systemUnderTest1b = systemUnderTest1a.Plus(-1, GenericQuarterComponent.Quarter);

            var systemUnderTest2a = A.Dummy<GenericQuarter>();
            var systemUnderTest2b = systemUnderTest2a.Plus(-1, GenericQuarterComponent.Year);

            // Act
            var result1 = systemUnderTest1a > systemUnderTest1b;
            var result2 = systemUnderTest2a > systemUnderTest2b;

            // Assert
            result1.Should().BeTrue();
            result2.Should().BeTrue();
        }

        [Fact]
        public static void GreaterThanOrEqualToOperator___Should_return_true___When_both_sides_of_operator_are_null()
        {
            // Arrange
            GenericQuarter systemUnderTest1 = null;
            GenericQuarter systemUnderTest2 = null;

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
            GenericQuarter systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<GenericQuarter>();

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
            var systemUnderTest1 = A.Dummy<GenericQuarter>();
            GenericQuarter systemUnderTest2 = null;

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
            var systemUnderTest1 = A.Dummy<GenericQuarter>();
            var systemUnderTest2 = new GenericQuarter(systemUnderTest1.Year, systemUnderTest1.QuarterNumber);

            // Act
            var result = systemUnderTest1 >= systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void GreaterThanOrEqualToOperator___Should_return_false___When_left_side_of_operator_is_less_than_right_side()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<GenericQuarter>();
            var systemUnderTest1b = systemUnderTest1a.Plus(1, GenericQuarterComponent.Quarter);

            var systemUnderTest2a = A.Dummy<GenericQuarter>();
            var systemUnderTest2b = systemUnderTest2a.Plus(1, GenericQuarterComponent.Year);

            // Act
            var result1 = systemUnderTest1a >= systemUnderTest1b;
            var result2 = systemUnderTest2a >= systemUnderTest2b;

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
        }

        [Fact]
        public static void GreaterThanOrEqualToOperator___Should_return_true___When_left_side_of_operator_is_greater_than_right_side()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<GenericQuarter>();
            var systemUnderTest1b = systemUnderTest1a.Plus(-1, GenericQuarterComponent.Quarter);

            var systemUnderTest2a = A.Dummy<GenericQuarter>();
            var systemUnderTest2b = systemUnderTest2a.Plus(-1, GenericQuarterComponent.Year);

            // Act
            var result1 = systemUnderTest1a >= systemUnderTest1b;
            var result2 = systemUnderTest2a >= systemUnderTest2b;

            // Assert
            result1.Should().BeTrue();
            result2.Should().BeTrue();
        }

        [Fact]
        public static void CompareTo___Should_return_1___When_calling_typed_overload_and_other_object_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<GenericQuarter>();

            // Act
            var result = systemUnderTest.CompareTo(null);

            // Assert
            result.Should().Be(1);
        }

        [Fact]
        public static void CompareTo___Should_return_negative_1___When_calling_typed_overload_and_test_object_is_less_than_other_object()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<GenericQuarter>();
            var systemUnderTest1b = systemUnderTest1a.Plus(1, GenericQuarterComponent.Quarter);

            var systemUnderTest2a = A.Dummy<GenericQuarter>();
            var systemUnderTest2b = systemUnderTest2a.Plus(1, GenericQuarterComponent.Year);

            // Act
            var result1 = systemUnderTest1a.CompareTo(systemUnderTest1b);
            var result2 = systemUnderTest2a.CompareTo(systemUnderTest2b);

            // Assert
            result1.Should().Be(-1);
            result2.Should().Be(-1);
        }

        [Fact]
        public static void CompareTo___Should_return_1___When_calling_typed_overload_and_test_object_is_greater_than_other_object()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<GenericQuarter>();
            var systemUnderTest1b = systemUnderTest1a.Plus(-1, GenericQuarterComponent.Quarter);

            var systemUnderTest2a = A.Dummy<GenericQuarter>();
            var systemUnderTest2b = systemUnderTest2a.Plus(-1, GenericQuarterComponent.Year);

            // Act
            var result1 = systemUnderTest1a.CompareTo(systemUnderTest1b);
            var result2 = systemUnderTest2a.CompareTo(systemUnderTest2b);

            // Assert
            result1.Should().Be(1);
            result2.Should().Be(1);
        }

        [Fact]
        public static void CompareTo___Should_throw_ArgumentException___When_calling_non_typed_overload_and_other_object_is_not_of_same_type_as_test_object()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<GenericQuarter>();
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
            var systemUnderTest1 = A.Dummy<GenericQuarter>();
            GenericQuarter systemUnderTest2 = null;

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
            var systemUnderTest1a = A.Dummy<GenericQuarter>();
            var systemUnderTest1b = systemUnderTest1a.Plus(1, GenericQuarterComponent.Quarter);

            var systemUnderTest2a = A.Dummy<GenericQuarter>();
            var systemUnderTest2b = systemUnderTest2a.Plus(1, GenericQuarterComponent.Year);

            // Act
            var result1 = systemUnderTest1a.CompareTo((object)systemUnderTest1b);
            var result2 = systemUnderTest2a.CompareTo((object)systemUnderTest2b);

            // Assert
            result1.Should().Be(-1);
            result2.Should().Be(-1);
        }

        [Fact]
        public static void CompareTo___Should_return_1___When_calling_none_typed_overload_and_test_object_is_greater_than_other_object()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<GenericQuarter>();
            var systemUnderTest1b = systemUnderTest1a.Plus(-1, GenericQuarterComponent.Quarter);

            var systemUnderTest2a = A.Dummy<GenericQuarter>();
            var systemUnderTest2b = systemUnderTest2a.Plus(-1, GenericQuarterComponent.Year);

            // Act
            var result1 = systemUnderTest1a.CompareTo((object)systemUnderTest1b);
            var result2 = systemUnderTest2a.CompareTo((object)systemUnderTest2b);

            // Assert
            result1.Should().Be(1);
            result2.Should().Be(1);
        }

        [Fact]
        public static void ToString___Should_return_friendly_string_representation_of_object___When_called()
        {
            // Arrange
            var systemUnderTest1 = new GenericQuarter(2017, QuarterNumber.Q1);
            var systemUnderTest2 = new GenericQuarter(2017, QuarterNumber.Q4);

            // Act
            var toString1 = systemUnderTest1.ToString();
            var toString2 = systemUnderTest2.ToString();

            // Assert
            toString1.Should().Be("1Q2017");
            toString2.Should().Be("4Q2017");
        }

        private static GenericQuarter TweakComponentOfGenericQuarter(this GenericQuarter genericQuarter, GenericQuarterComponent componentToTweak)
        {
            if (componentToTweak == GenericQuarterComponent.Quarter)
            {
                var tweakedQuarter = A.Dummy<QuarterNumber>().ThatIsNot(genericQuarter.QuarterNumber);
                var result = new GenericQuarter(genericQuarter.Year, tweakedQuarter);
                return result;
            }

            if (componentToTweak == GenericQuarterComponent.Year)
            {
                var tweakedYear = A.Dummy<PositiveInteger>().ThatIs(y => y != genericQuarter.Year && y <= 9999);
                var result = new GenericQuarter(tweakedYear, genericQuarter.QuarterNumber);
                return result;
            }

            throw new NotSupportedException("this generic quarter component is not supported: " + componentToTweak);
        }

        private static GenericQuarter Plus(this GenericQuarter genericQuarter, int amount, GenericQuarterComponent componentToTweak)
        {
            if (componentToTweak == GenericQuarterComponent.Quarter)
            {
                var referenceMonth = new DateTime(genericQuarter.Year, (int)genericQuarter.QuarterNumber * 3, 1);
                var updatedMonth = referenceMonth.AddMonths(amount * 3);
                var result = new GenericQuarter(updatedMonth.Year, (QuarterNumber)(updatedMonth.Month / 3));
                return result;
            }

            if (componentToTweak == GenericQuarterComponent.Year)
            {
                var result = new GenericQuarter(genericQuarter.Year + amount, genericQuarter.QuarterNumber);
                return result;
            }

            throw new NotSupportedException("this generic quarter component is not supported: " + componentToTweak);
        }

        // ReSharper restore InconsistentNaming
    }
}

// ReSharper restore CheckNamespace