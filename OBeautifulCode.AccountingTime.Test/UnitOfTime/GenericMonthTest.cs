// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GenericMonthTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Test
{
    using System;

    using FakeItEasy;

    using FluentAssertions;

    using OBeautifulCode.AutoFakeItEasy;

    using Xunit;

    public static class GenericMonthTest
    {
        private enum GenericMonthComponent
        {
            Month,

            Year,
        }

        [Fact]
        public static void Constructor___Should_throw_ArgumentOutOfRangeException___When_parameter_year_is_less_than_1()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => new GenericMonth(0, A.Dummy<MonthNumber>()));
            var ex2 = Record.Exception(() => new GenericMonth(A.Dummy<ZeroOrNegativeInteger>(), A.Dummy<MonthNumber>()));

            // Assert
            ex1.Should().BeOfType<ArgumentOutOfRangeException>();
            ex2.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void Constructor___Should_throw_ArgumentOutOfRangeException___When_parameter_year_is_greater_than_9999()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => new GenericMonth(10000, A.Dummy<MonthNumber>()));
            var ex2 = Record.Exception(() => new GenericMonth(A.Dummy<PositiveInteger>().ThatIs(y => y > 9999), A.Dummy<MonthNumber>()));

            // Assert
            ex1.Should().BeOfType<ArgumentOutOfRangeException>();
            ex2.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void Constructor___Should_throw_ArgumentException___When_parameter_monthNumber_is_Invalid()
        {
            // Arrange
            var validMonth = A.Dummy<GenericMonth>();

            // Act
            var ex = Record.Exception(() => new GenericMonth(validMonth.Year, MonthNumber.Invalid));

            // Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void Year___Should_return_same_year_passed_to_constructor___When_getting()
        {
            // Arrange
            var validMonth = A.Dummy<GenericMonth>();

            var year = validMonth.Year;
            var monthNumber = validMonth.MonthNumber;

            var systemUnderTest = new GenericMonth(year, monthNumber);

            // Act
            var actualYear = systemUnderTest.Year;

            // Assert
            actualYear.Should().Be(year);
        }

        [Fact]
        public static void MonthNumber___Should_return_same_monthNumber_passed_to_constructor___When_getting()
        {
            // Arrange
            var validMonth = A.Dummy<GenericMonth>();

            var year = validMonth.Year;
            var monthNumber = validMonth.MonthNumber;

            var systemUnderTest = new GenericMonth(year, monthNumber);

            // Act
            var actualMonthNumber = systemUnderTest.MonthNumber;

            // Assert
            actualMonthNumber.Should().Be(monthNumber);
        }

        [Fact]
        public static void EqualsOperator___Should_return_true___When_both_sides_of_operator_are_null()
        {
            // Arrange
            GenericMonth systemUnderTest1 = null;
            GenericMonth systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 == systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void EqualsOperator___Should_return_false___When_one_side_of_operator_is_null_and_the_other_side_is_not_null()
        {
            // Arrange
            GenericMonth systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<GenericMonth>();

            // Act
            var result1 = systemUnderTest1 == systemUnderTest2;
            var result2 = systemUnderTest2 == systemUnderTest1;

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
        }

        [Fact]
        public static void EqualsOperator___Should_return_true___When_same_object_is_on_both_sides_of_operator()
        {
            // Arrange
            var systemUnderTest = A.Dummy<GenericMonth>();

            // Act
#pragma warning disable CS1718 // Comparison made to same variable
            var result = systemUnderTest == systemUnderTest;
#pragma warning restore CS1718 // Comparison made to same variable

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void EqualsOperator___Should_return_false___When_objects_being_compared_have_different_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<GenericMonth>();
            var systemUnderTest1b = A.Dummy<GenericMonth>().ThatIsNot(systemUnderTest1a);

            var systemUnderTest2a = A.Dummy<GenericMonth>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfGenericMonth(GenericMonthComponent.Month);

            var systemUnderTest3a = A.Dummy<GenericMonth>();
            var systemUnderTest3b = systemUnderTest3a.TweakComponentOfGenericMonth(GenericMonthComponent.Year);

            // Act
            var result1 = systemUnderTest1a == systemUnderTest1b;
            var result2 = systemUnderTest2a == systemUnderTest2b;
            var result3 = systemUnderTest3a == systemUnderTest3b;

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
            result3.Should().BeFalse();
        }

        [Fact]
        public static void EqualsOperator___Should_return_true___When_objects_being_compared_have_same_property_values()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<GenericMonth>();
            var systemUnderTest2 = new GenericMonth(systemUnderTest1.Year, systemUnderTest1.MonthNumber);

            // Act
            var result = systemUnderTest1 == systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_false___When_both_sides_of_operator_are_null()
        {
            // Arrange
            GenericMonth systemUnderTest1 = null;
            GenericMonth systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 != systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_true___When_one_side_of_operator_is_null_and_the_other_side_is_not_null()
        {
            // Arrange
            GenericMonth systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<GenericMonth>();

            // Act
            var result1 = systemUnderTest1 != systemUnderTest2;
            var result2 = systemUnderTest2 != systemUnderTest1;

            // Assert
            result1.Should().BeTrue();
            result2.Should().BeTrue();
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_false___When_same_object_is_on_both_sides_of_operator()
        {
            // Arrange
            var systemUnderTest = A.Dummy<GenericMonth>();

            // Act
#pragma warning disable CS1718 // Comparison made to same variable
            var result = systemUnderTest != systemUnderTest;
#pragma warning restore CS1718 // Comparison made to same variable

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_true___When_objects_being_compared_have_different_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<GenericMonth>();
            var systemUnderTest1b = A.Dummy<GenericMonth>().ThatIsNot(systemUnderTest1a);

            var systemUnderTest2a = A.Dummy<GenericMonth>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfGenericMonth(GenericMonthComponent.Month);

            var systemUnderTest3a = A.Dummy<GenericMonth>();
            var systemUnderTest3b = systemUnderTest3a.TweakComponentOfGenericMonth(GenericMonthComponent.Year);

            // Act
            var result1 = systemUnderTest1a != systemUnderTest1b;
            var result2 = systemUnderTest2a != systemUnderTest2b;
            var result3 = systemUnderTest3a != systemUnderTest3b;

            // Assert
            result1.Should().BeTrue();
            result2.Should().BeTrue();
            result3.Should().BeTrue();
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_false___When_objects_being_compared_have_same_property_values()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<GenericMonth>();
            var systemUnderTest2 = new GenericMonth(systemUnderTest1.Year, systemUnderTest1.MonthNumber);

            // Act
            var result = systemUnderTest1 != systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void Equals___Should_return_false___When_calling_typed_overload_and_parameter_other_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<GenericMonth>();

            // Act
            var result = systemUnderTest.Equals(null);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void Equals___Should_return_true___When_calling_typed_overload_and_parameter_other_is_same_object()
        {
            // Arrange
            var systemUnderTest = A.Dummy<GenericMonth>();

            // Act
            var result = systemUnderTest.Equals(systemUnderTest);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Equals___Should_return_false___When_calling_typed_overload_and_objects_being_compared_have_different_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<GenericMonth>();
            var systemUnderTest1b = A.Dummy<GenericMonth>().ThatIsNot(systemUnderTest1a);

            var systemUnderTest2a = A.Dummy<GenericMonth>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfGenericMonth(GenericMonthComponent.Month);

            var systemUnderTest3a = A.Dummy<GenericMonth>();
            var systemUnderTest3b = systemUnderTest3a.TweakComponentOfGenericMonth(GenericMonthComponent.Year);

            // Act
            var result1 = systemUnderTest1a.Equals(systemUnderTest1b);
            var result2 = systemUnderTest2a.Equals(systemUnderTest2b);
            var result3 = systemUnderTest3a.Equals(systemUnderTest3b);

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
            result3.Should().BeFalse();
        }

        [Fact]
        public static void Equals___Should_return_true___When_calling_typed_overload_and_objects_being_compared_have_same_property_values()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<GenericMonth>();
            var systemUnderTest2 = new GenericMonth(systemUnderTest1.Year, systemUnderTest1.MonthNumber);

            // Act
            var result = systemUnderTest1.Equals(systemUnderTest2);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Equals___Should_return_false___When_calling_non_typed_overload_and_parameter_other_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<GenericMonth>();

            // Act
            var result = systemUnderTest.Equals(null);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void Equals___Should_return_false___When_calling_non_typed_overload_and_parameter_other_is_not_of_the_same_type()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<GenericMonth>();
            var systemUnderTest2 = A.Dummy<CalendarQuarter>();

            // Act
            var result = systemUnderTest1.Equals((object)systemUnderTest2);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void Equals___Should_return_true___When_calling_non_typed_overload_and_parameter_other_is_same_object()
        {
            // Arrange
            var systemUnderTest = A.Dummy<GenericMonth>();

            // Act
            var result = systemUnderTest.Equals((object)systemUnderTest);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Equals___Should_return_false___When_calling_non_typed_overload_and_objects_being_compared_have_different_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<GenericMonth>();
            var systemUnderTest1b = A.Dummy<GenericMonth>().ThatIsNot(systemUnderTest1a);

            var systemUnderTest2a = A.Dummy<GenericMonth>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfGenericMonth(GenericMonthComponent.Month);

            var systemUnderTest3a = A.Dummy<GenericMonth>();
            var systemUnderTest3b = systemUnderTest3a.TweakComponentOfGenericMonth(GenericMonthComponent.Year);

            // Act
            var result1 = systemUnderTest1a.Equals((object)systemUnderTest1b);
            var result2 = systemUnderTest2a.Equals((object)systemUnderTest2b);
            var result3 = systemUnderTest3a.Equals((object)systemUnderTest3b);

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
            result3.Should().BeFalse();
        }

        [Fact]
        public static void Equals___Should_return_true___When_calling_non_typed_overload_and_objects_being_compared_have_same_property_values()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<GenericMonth>();
            var systemUnderTest2 = new GenericMonth(systemUnderTest1.Year, systemUnderTest1.MonthNumber);

            // Act
            var result = systemUnderTest1.Equals((object)systemUnderTest2);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void GetHashCode___Should_not_be_equal_for_two_GenericMonths___When_both_objects_have_different_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<GenericMonth>();
            var systemUnderTest1b = A.Dummy<GenericMonth>().ThatIsNot(systemUnderTest1a);

            var systemUnderTest2a = A.Dummy<GenericMonth>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfGenericMonth(GenericMonthComponent.Month);

            var systemUnderTest3a = A.Dummy<GenericMonth>();
            var systemUnderTest3b = systemUnderTest3a.TweakComponentOfGenericMonth(GenericMonthComponent.Year);

            // Act
            var hash1a = systemUnderTest1a.GetHashCode();
            var hash1b = systemUnderTest1b.GetHashCode();

            var hash2a = systemUnderTest2a.GetHashCode();
            var hash2b = systemUnderTest2b.GetHashCode();

            var hash3a = systemUnderTest3a.GetHashCode();
            var hash3b = systemUnderTest3b.GetHashCode();

            // Assert
            hash1a.Should().NotBe(hash1b);
            hash2a.Should().NotBe(hash2b);
            hash3a.Should().NotBe(hash3b);
        }

        [Fact]
        public static void GetHashCode___Should_be_equal_for_two_GenericMonths___When_both_objects_have_the_same_property_values()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<GenericMonth>();
            var systemUnderTest2 = new GenericMonth(systemUnderTest1.Year, systemUnderTest1.MonthNumber);

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
            GenericMonth systemUnderTest1 = null;
            GenericMonth systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 < systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void LessThanOperator___Should_return_true___When_left_side_of_operator_is_null_and_right_side_is_not_null()
        {
            // Arrange
            GenericMonth systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<GenericMonth>();

            // Act
            var result = systemUnderTest1 < systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void LessThanOperator___Should_return_false___When_left_side_of_operator_is_not_null_and_right_side_is_null()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<GenericMonth>();
            GenericMonth systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 < systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void LessThanOperator___Should_return_false___When_left_side_of_operator_is_equal_to_right_side()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<GenericMonth>();
            var systemUnderTest2 = new GenericMonth(systemUnderTest1.Year, systemUnderTest1.MonthNumber);

            // Act
            var result = systemUnderTest1 < systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void LessThanOperator___Should_return_true___When_left_side_of_operator_is_less_than_right_side()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<GenericMonth>();
            var systemUnderTest1b = systemUnderTest1a.TweakComponentOfGenericMonthByAmount(1, GenericMonthComponent.Month);

            var systemUnderTest2a = A.Dummy<GenericMonth>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfGenericMonthByAmount(1, GenericMonthComponent.Year);

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
            var systemUnderTest1a = A.Dummy<GenericMonth>();
            var systemUnderTest1b = systemUnderTest1a.TweakComponentOfGenericMonthByAmount(-1, GenericMonthComponent.Month);

            var systemUnderTest2a = A.Dummy<GenericMonth>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfGenericMonthByAmount(-1, GenericMonthComponent.Year);

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
            GenericMonth systemUnderTest1 = null;
            GenericMonth systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 <= systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void LessThanOrEqualToOperator___Should_return_true___When_left_side_of_operator_is_null_and_right_side_is_not_null()
        {
            // Arrange
            GenericMonth systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<GenericMonth>();

            // Act
            var result = systemUnderTest1 <= systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void LessThanOrEqualToOperator___Should_return_false___When_left_side_of_operator_is_not_null_and_right_side_is_null()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<GenericMonth>();
            GenericMonth systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 <= systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void LessThanOrEqualToOperator___Should_return_true___When_left_side_of_operator_is_equal_to_right_side()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<GenericMonth>();
            var systemUnderTest2 = new GenericMonth(systemUnderTest1.Year, systemUnderTest1.MonthNumber);

            // Act
            var result = systemUnderTest1 <= systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void LessThanOrEqualToOperator___Should_return_true___When_left_side_of_operator_is_less_than_right_side()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<GenericMonth>();
            var systemUnderTest1b = systemUnderTest1a.TweakComponentOfGenericMonthByAmount(1, GenericMonthComponent.Month);

            var systemUnderTest2a = A.Dummy<GenericMonth>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfGenericMonthByAmount(1, GenericMonthComponent.Year);

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
            var systemUnderTest1a = A.Dummy<GenericMonth>();
            var systemUnderTest1b = systemUnderTest1a.TweakComponentOfGenericMonthByAmount(-1, GenericMonthComponent.Month);

            var systemUnderTest2a = A.Dummy<GenericMonth>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfGenericMonthByAmount(-1, GenericMonthComponent.Year);

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
            GenericMonth systemUnderTest1 = null;
            GenericMonth systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 > systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void GreaterThanOperator___Should_return_false___When_left_side_of_operator_is_null_and_right_side_is_not_null()
        {
            // Arrange
            GenericMonth systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<GenericMonth>();

            // Act
            var result = systemUnderTest1 > systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void GreaterThanOperator___Should_return_true___When_left_side_of_operator_is_not_null_and_right_side_is_null()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<GenericMonth>();
            GenericMonth systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 > systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void GreaterThanOperator___Should_return_false___When_left_side_of_operator_is_equal_to_right_side()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<GenericMonth>();
            var systemUnderTest2 = new GenericMonth(systemUnderTest1.Year, systemUnderTest1.MonthNumber);

            // Act
            var result = systemUnderTest1 > systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void GreaterThanOperator___Should_return_false___When_left_side_of_operator_is_less_than_right_side()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<GenericMonth>();
            var systemUnderTest1b = systemUnderTest1a.TweakComponentOfGenericMonthByAmount(1, GenericMonthComponent.Month);

            var systemUnderTest2a = A.Dummy<GenericMonth>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfGenericMonthByAmount(1, GenericMonthComponent.Year);

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
            var systemUnderTest1a = A.Dummy<GenericMonth>();
            var systemUnderTest1b = systemUnderTest1a.TweakComponentOfGenericMonthByAmount(-1, GenericMonthComponent.Month);

            var systemUnderTest2a = A.Dummy<GenericMonth>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfGenericMonthByAmount(-1, GenericMonthComponent.Year);

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
            GenericMonth systemUnderTest1 = null;
            GenericMonth systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 >= systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void GreaterThanOrEqualToOperator___Should_return_false___When_left_side_of_operator_is_null_and_right_side_is_not_null()
        {
            // Arrange
            GenericMonth systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<GenericMonth>();

            // Act
            var result = systemUnderTest1 >= systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void GreaterThanOrEqualToOperator___Should_return_true___When_left_side_of_operator_is_not_null_and_right_side_is_null()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<GenericMonth>();
            GenericMonth systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 >= systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void GreaterThanOrEqualToOperator___Should_return_true___When_left_side_of_operator_is_equal_to_right_side()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<GenericMonth>();
            var systemUnderTest2 = new GenericMonth(systemUnderTest1.Year, systemUnderTest1.MonthNumber);

            // Act
            var result = systemUnderTest1 >= systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void GreaterThanOrEqualToOperator___Should_return_false___When_left_side_of_operator_is_less_than_right_side()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<GenericMonth>();
            var systemUnderTest1b = systemUnderTest1a.TweakComponentOfGenericMonthByAmount(1, GenericMonthComponent.Month);

            var systemUnderTest2a = A.Dummy<GenericMonth>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfGenericMonthByAmount(1, GenericMonthComponent.Year);

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
            var systemUnderTest1a = A.Dummy<GenericMonth>();
            var systemUnderTest1b = systemUnderTest1a.TweakComponentOfGenericMonthByAmount(-1, GenericMonthComponent.Month);

            var systemUnderTest2a = A.Dummy<GenericMonth>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfGenericMonthByAmount(-1, GenericMonthComponent.Year);

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
            var systemUnderTest = A.Dummy<GenericMonth>();

            // Act
            var result = systemUnderTest.CompareTo(null);

            // Assert
            result.Should().Be(1);
        }

        [Fact]
        public static void CompareTo___Should_return_negative_1___When_calling_typed_overload_and_test_object_is_less_than_other_object()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<GenericMonth>();
            var systemUnderTest1b = systemUnderTest1a.TweakComponentOfGenericMonthByAmount(1, GenericMonthComponent.Month);

            var systemUnderTest2a = A.Dummy<GenericMonth>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfGenericMonthByAmount(1, GenericMonthComponent.Year);

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
            var systemUnderTest1a = A.Dummy<GenericMonth>();
            var systemUnderTest1b = systemUnderTest1a.TweakComponentOfGenericMonthByAmount(-1, GenericMonthComponent.Month);

            var systemUnderTest2a = A.Dummy<GenericMonth>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfGenericMonthByAmount(-1, GenericMonthComponent.Year);

            // Act
            var result1 = systemUnderTest1a.CompareTo(systemUnderTest1b);
            var result2 = systemUnderTest2a.CompareTo(systemUnderTest2b);

            // Assert
            result1.Should().Be(1);
            result2.Should().Be(1);
        }

        [Fact]
        public static void CompareTo___Should_return_0___When_calling_typed_overload_and_test_object_is_equal_to_other_object()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<GenericMonth>();
            var systemUnderTest2 = new GenericMonth(systemUnderTest1.Year, systemUnderTest1.MonthNumber);

            // Act
            var result = systemUnderTest1.CompareTo(systemUnderTest2);

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public static void CompareTo___Should_throw_ArgumentException___When_calling_non_typed_overload_and_other_object_is_not_of_same_type_as_test_object()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<GenericMonth>();
            var systemUnderTest2 = A.Dummy<CalendarQuarter>();

            // Act
            var ex = Record.Exception(() => systemUnderTest1.CompareTo((object)systemUnderTest2));

            // Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void CompareTo___Should_throw_ArgumentException___When_calling_non_typed_overload_and_other_object_is_null()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<GenericMonth>();
            GenericMonth systemUnderTest2 = null;

            // Act
            var ex = Record.Exception(() => systemUnderTest1.CompareTo((object)systemUnderTest2));

            // Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void CompareTo___Should_return_negative_1___When_calling_non_typed_overload_and_test_object_is_less_than_other_object()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<GenericMonth>();
            var systemUnderTest1b = systemUnderTest1a.TweakComponentOfGenericMonthByAmount(1, GenericMonthComponent.Month);

            var systemUnderTest2a = A.Dummy<GenericMonth>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfGenericMonthByAmount(1, GenericMonthComponent.Year);

            // Act
            var result1 = systemUnderTest1a.CompareTo((object)systemUnderTest1b);
            var result2 = systemUnderTest2a.CompareTo((object)systemUnderTest2b);

            // Assert
            result1.Should().Be(-1);
            result2.Should().Be(-1);
        }

        [Fact]
        public static void CompareTo___Should_return_1___When_calling_non_typed_overload_and_test_object_is_greater_than_other_object()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<GenericMonth>();
            var systemUnderTest1b = systemUnderTest1a.TweakComponentOfGenericMonthByAmount(-1, GenericMonthComponent.Month);

            var systemUnderTest2a = A.Dummy<GenericMonth>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfGenericMonthByAmount(-1, GenericMonthComponent.Year);

            // Act
            var result1 = systemUnderTest1a.CompareTo((object)systemUnderTest1b);
            var result2 = systemUnderTest2a.CompareTo((object)systemUnderTest2b);

            // Assert
            result1.Should().Be(1);
            result2.Should().Be(1);
        }

        [Fact]
        public static void CompareTo___Should_return_1___When_calling_non_typed_overload_and_test_object_is_equal_to_other_object()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<GenericMonth>();
            var systemUnderTest2 = new GenericMonth(systemUnderTest1.Year, systemUnderTest1.MonthNumber);

            // Act
            var result = systemUnderTest1.CompareTo((object)systemUnderTest2);

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public static void ToString___Should_return_friendly_string_representation_of_object___When_called()
        {
            // Arrange
            var systemUnderTest1 = new GenericMonth(2017, MonthNumber.One);
            var systemUnderTest2 = new GenericMonth(2017, MonthNumber.Two);
            var systemUnderTest3 = new GenericMonth(2017, MonthNumber.Three);
            var systemUnderTest4 = new GenericMonth(2017, MonthNumber.Four);
            var systemUnderTest5 = new GenericMonth(2017, MonthNumber.Five);
            var systemUnderTest6 = new GenericMonth(2017, MonthNumber.Six);
            var systemUnderTest7 = new GenericMonth(2017, MonthNumber.Seven);
            var systemUnderTest8 = new GenericMonth(2017, MonthNumber.Eight);
            var systemUnderTest9 = new GenericMonth(2017, MonthNumber.Nine);
            var systemUnderTest10 = new GenericMonth(2017, MonthNumber.Ten);
            var systemUnderTest11 = new GenericMonth(2017, MonthNumber.Eleven);
            var systemUnderTest12 = new GenericMonth(2017, MonthNumber.Twelve);

            // Act
            var toString1 = systemUnderTest1.ToString();
            var toString2 = systemUnderTest2.ToString();
            var toString3 = systemUnderTest3.ToString();
            var toString4 = systemUnderTest4.ToString();
            var toString5 = systemUnderTest5.ToString();
            var toString6 = systemUnderTest6.ToString();
            var toString7 = systemUnderTest7.ToString();
            var toString8 = systemUnderTest8.ToString();
            var toString9 = systemUnderTest9.ToString();
            var toString10 = systemUnderTest10.ToString();
            var toString11 = systemUnderTest11.ToString();
            var toString12 = systemUnderTest12.ToString();

            // Assert
            toString1.Should().Be("1st month of 2017");
            toString2.Should().Be("2nd month of 2017");
            toString3.Should().Be("3rd month of 2017");
            toString4.Should().Be("4th month of 2017");
            toString5.Should().Be("5th month of 2017");
            toString6.Should().Be("6th month of 2017");
            toString7.Should().Be("7th month of 2017");
            toString8.Should().Be("8th month of 2017");
            toString9.Should().Be("9th month of 2017");
            toString10.Should().Be("10th month of 2017");
            toString11.Should().Be("11th month of 2017");
            toString12.Should().Be("12th month of 2017");
        }

        [Fact]
        public static void Clone___Should_return_a_clone_of_the_object___When_called()
        {
            // Arrange
            var systemUnderTest = A.Dummy<GenericMonth>();

            // Act
            var clone = systemUnderTest.Clone();

            // Assert
            clone.Should().Be(systemUnderTest);
            clone.Should().NotBeSameAs(systemUnderTest);
        }

        [Fact]
        public static void UnitOfTimeKind__Should_return_Generic___When_called()
        {
            // Arrange
            var unitOfTime = A.Dummy<GenericMonth>();

            // Act
            var kind = unitOfTime.UnitOfTimeKind;

            // Assert
            kind.Should().Be(UnitOfTimeKind.Generic);
        }

        [Fact]
        public static void UnitOfTimeGranularity__Should_return_Month___When_called()
        {
            // Arrange
            var unitOfTime = A.Dummy<GenericMonth>();

            // Act
            var granularity = unitOfTime.UnitOfTimeGranularity;

            // Assert
            granularity.Should().Be(UnitOfTimeGranularity.Month);
        }

        private static GenericMonth TweakComponentOfGenericMonth(this GenericMonth genericMonth, GenericMonthComponent componentToTweak)
        {
            if (componentToTweak == GenericMonthComponent.Month)
            {
                var tweakedMonth = A.Dummy<MonthNumber>().ThatIsNot(genericMonth.MonthNumber);
                var result = new GenericMonth(genericMonth.Year, tweakedMonth);
                return result;
            }

            if (componentToTweak == GenericMonthComponent.Year)
            {
                var tweakedYear = A.Dummy<PositiveInteger>().ThatIs(y => y != genericMonth.Year && y <= 9999);
                var result = new GenericMonth(tweakedYear, genericMonth.MonthNumber);
                return result;
            }

            throw new NotSupportedException("this generic month component is not supported: " + componentToTweak);
        }

        private static GenericMonth TweakComponentOfGenericMonthByAmount(this GenericMonth genericMonth, int amount, GenericMonthComponent componentToTweak)
        {
            if (componentToTweak == GenericMonthComponent.Month)
            {
                var referenceMonth = new DateTime(genericMonth.Year, (int)genericMonth.MonthNumber, 1);
                var updatedMonth = referenceMonth.AddMonths(amount);
                var result = new GenericMonth(updatedMonth.Year, (MonthNumber)updatedMonth.Month);
                return result;
            }

            if (componentToTweak == GenericMonthComponent.Year)
            {
                var result = new GenericMonth(genericMonth.Year + amount, genericMonth.MonthNumber);
                return result;
            }

            throw new NotSupportedException("this generic month component is not supported: " + componentToTweak);
        }
    }
}