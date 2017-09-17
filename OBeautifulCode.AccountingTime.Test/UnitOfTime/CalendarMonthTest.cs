// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalendarMonthTest.cs" company="OBeautifulCode">
//    Copyright (c) OBeautifulCode 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Test
{
    using System;

    using AutoFakeItEasy;

    using FakeItEasy;

    using FluentAssertions;

    using Newtonsoft.Json;

    using Spritely.Recipes;

    using Xunit;

    public static class CalendarMonthTest
    {
        private enum CalendarMonthComponent
        {
            Month,

            Year
        }

        [Fact]
        public static void Constructor___Should_throw_ArgumentOutOfRangeException___When_parameter_year_is_less_than_1()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => new CalendarMonth(0, A.Dummy<MonthOfYear>()));
            var ex2 = Record.Exception(() => new CalendarMonth(A.Dummy<ZeroOrNegativeInteger>(), A.Dummy<MonthOfYear>()));

            // Assert
            ex1.Should().BeOfType<ArgumentOutOfRangeException>();
            ex2.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void Constructor___Should_throw_ArgumentOutOfRangeException___When_parameter_year_is_greater_than_9999()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => new CalendarMonth(10000, A.Dummy<MonthOfYear>()));
            var ex2 = Record.Exception(() => new CalendarMonth(A.Dummy<PositiveInteger>().ThatIs(y => y > 9999), A.Dummy<MonthOfYear>()));

            // Assert
            ex1.Should().BeOfType<ArgumentOutOfRangeException>();
            ex2.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void Constructor___Should_throw_ArgumentException___When_parameter_monthOfYear_is_Invalid()
        {
            // Arrange
            var validMonth = A.Dummy<CalendarMonth>();

            // Act
            var ex = Record.Exception(() => new CalendarMonth(validMonth.Year, MonthOfYear.Invalid));

            // Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void Year___Should_return_same_year_passed_to_constructor___When_getting()
        {
            // Arrange
            var validMonth = A.Dummy<CalendarMonth>();

            var year = validMonth.Year;
            var monthOfYear = validMonth.MonthOfYear;

            var systemUnderTest = new CalendarMonth(year, monthOfYear);

            // Act
            var actualYear = systemUnderTest.Year;

            // Assert
            actualYear.Should().Be(year);
        }

        [Fact]
        public static void MonthOfYear___Should_return_same_monthOfYear_passed_to_constructor___When_getting()
        {
            // Arrange
            var validMonth = A.Dummy<CalendarMonth>();

            var year = validMonth.Year;
            var monthOfYear = validMonth.MonthOfYear;

            var systemUnderTest = new CalendarMonth(year, monthOfYear);

            // Act
            var actualMonthOfYear = systemUnderTest.MonthOfYear;

            // Assert
            actualMonthOfYear.Should().Be(monthOfYear);
        }

        [Fact]
        public static void MonthNumber___Should_return_month_number_of_monthOfYear_passed_to_constructor___When_getting()
        {
            // Arrange
            var validMonth = A.Dummy<CalendarMonth>();

            var year = validMonth.Year;
            var monthOfYear = validMonth.MonthOfYear;

            var systemUnderTest = new CalendarMonth(year, monthOfYear);

            // Act
            var actualMonthNumber = systemUnderTest.MonthNumber;

            // Assert
            actualMonthNumber.Should().Be((MonthNumber)monthOfYear);
        }

        [Fact]
        public static void EqualsOperator___Should_return_true___When_both_sides_of_operator_are_null()
        {
            // Arrange
            CalendarMonth systemUnderTest1 = null;
            CalendarMonth systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 == systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void EqualsOperator___Should_return_false___When_one_side_of_operator_is_null_and_the_other_side_is_not_null()
        {
            // Arrange
            CalendarMonth systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<CalendarMonth>();

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
            var systemUnderTest = A.Dummy<CalendarMonth>();

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
            var systemUnderTest1a = A.Dummy<CalendarMonth>();
            var systemUnderTest1b = A.Dummy<CalendarMonth>().ThatIsNot(systemUnderTest1a);

            var systemUnderTest2a = A.Dummy<CalendarMonth>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfCalendarMonth(CalendarMonthComponent.Month);

            var systemUnderTest3a = A.Dummy<CalendarMonth>();
            var systemUnderTest3b = systemUnderTest3a.TweakComponentOfCalendarMonth(CalendarMonthComponent.Year);

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
            var systemUnderTest1 = A.Dummy<CalendarMonth>();
            var systemUnderTest2 = new CalendarMonth(systemUnderTest1.Year, systemUnderTest1.MonthOfYear);

            // Act
            var result = systemUnderTest1 == systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_false___When_both_sides_of_operator_are_null()
        {
            // Arrange
            CalendarMonth systemUnderTest1 = null;
            CalendarMonth systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 != systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_true___When_one_side_of_operator_is_null_and_the_other_side_is_not_null()
        {
            // Arrange
            CalendarMonth systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<CalendarMonth>();

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
            var systemUnderTest = A.Dummy<CalendarMonth>();

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
            var systemUnderTest1a = A.Dummy<CalendarMonth>();
            var systemUnderTest1b = A.Dummy<CalendarMonth>().ThatIsNot(systemUnderTest1a);

            var systemUnderTest2a = A.Dummy<CalendarMonth>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfCalendarMonth(CalendarMonthComponent.Month);

            var systemUnderTest3a = A.Dummy<CalendarMonth>();
            var systemUnderTest3b = systemUnderTest3a.TweakComponentOfCalendarMonth(CalendarMonthComponent.Year);

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
            var systemUnderTest1 = A.Dummy<CalendarMonth>();
            var systemUnderTest2 = new CalendarMonth(systemUnderTest1.Year, systemUnderTest1.MonthOfYear);

            // Act
            var result = systemUnderTest1 != systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void Equals___Should_return_false___When_calling_typed_overload_and_parameter_other_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<CalendarMonth>();

            // Act
            var result = systemUnderTest.Equals(null);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void Equals___Should_return_true___When_calling_typed_overload_and_parameter_other_is_same_object()
        {
            // Arrange
            var systemUnderTest = A.Dummy<CalendarMonth>();

            // Act
            var result = systemUnderTest.Equals(systemUnderTest);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Equals___Should_return_false___When_calling_typed_overload_and_objects_being_compared_have_different_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<CalendarMonth>();
            var systemUnderTest1b = A.Dummy<CalendarMonth>().ThatIsNot(systemUnderTest1a);

            var systemUnderTest2a = A.Dummy<CalendarMonth>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfCalendarMonth(CalendarMonthComponent.Month);

            var systemUnderTest3a = A.Dummy<CalendarMonth>();
            var systemUnderTest3b = systemUnderTest3a.TweakComponentOfCalendarMonth(CalendarMonthComponent.Year);

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
            var systemUnderTest1 = A.Dummy<CalendarMonth>();
            var systemUnderTest2 = new CalendarMonth(systemUnderTest1.Year, systemUnderTest1.MonthOfYear);

            // Act
            var result = systemUnderTest1.Equals(systemUnderTest2);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Equals___Should_return_false___When_calling_non_typed_overload_and_parameter_other_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<CalendarMonth>();

            // Act
            var result = systemUnderTest.Equals(null);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void Equals___Should_return_false___When_calling_non_typed_overload_and_parameter_other_is_not_of_the_same_type()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<CalendarMonth>();
            var systemUnderTest2 = A.Dummy<FiscalMonth>();

            // Act
            var result = systemUnderTest1.Equals((object)systemUnderTest2);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void Equals___Should_return_true___When_calling_non_typed_overload_and_parameter_other_is_same_object()
        {
            // Arrange
            var systemUnderTest = A.Dummy<CalendarMonth>();

            // Act
            var result = systemUnderTest.Equals((object)systemUnderTest);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Equals___Should_return_false___When_calling_non_typed_overload_and_objects_being_compared_have_different_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<CalendarMonth>();
            var systemUnderTest1b = A.Dummy<CalendarMonth>().ThatIsNot(systemUnderTest1a);

            var systemUnderTest2a = A.Dummy<CalendarMonth>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfCalendarMonth(CalendarMonthComponent.Month);

            var systemUnderTest3a = A.Dummy<CalendarMonth>();
            var systemUnderTest3b = systemUnderTest3a.TweakComponentOfCalendarMonth(CalendarMonthComponent.Year);

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
            var systemUnderTest1 = A.Dummy<CalendarMonth>();
            var systemUnderTest2 = new CalendarMonth(systemUnderTest1.Year, systemUnderTest1.MonthOfYear);

            // Act
            var result = systemUnderTest1.Equals((object)systemUnderTest2);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void GetHashCode___Should_not_be_equal_for_two_CalendarMonths___When_both_objects_have_different_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<CalendarMonth>();
            var systemUnderTest1b = A.Dummy<CalendarMonth>().ThatIsNot(systemUnderTest1a);

            var systemUnderTest2a = A.Dummy<CalendarMonth>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfCalendarMonth(CalendarMonthComponent.Month);

            var systemUnderTest3a = A.Dummy<CalendarMonth>();
            var systemUnderTest3b = systemUnderTest3a.TweakComponentOfCalendarMonth(CalendarMonthComponent.Year);

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
        public static void GetHashCode___Should_be_equal_for_two_CalendarMonths___When_both_objects_have_the_same_property_values()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<CalendarMonth>();
            var systemUnderTest2 = new CalendarMonth(systemUnderTest1.Year, systemUnderTest1.MonthOfYear);

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
            CalendarMonth systemUnderTest1 = null;
            CalendarMonth systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 < systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void LessThanOperator___Should_return_true___When_left_side_of_operator_is_null_and_right_side_is_not_null()
        {
            // Arrange
            CalendarMonth systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<CalendarMonth>();

            // Act
            var result = systemUnderTest1 < systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void LessThanOperator___Should_return_false___When_left_side_of_operator_is_not_null_and_right_side_is_null()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<CalendarMonth>();
            CalendarMonth systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 < systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void LessThanOperator___Should_return_false___When_left_side_of_operator_is_equal_to_right_side()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<CalendarMonth>();
            var systemUnderTest2 = new CalendarMonth(systemUnderTest1.Year, systemUnderTest1.MonthOfYear);

            // Act
            var result = systemUnderTest1 < systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void LessThanOperator___Should_return_true___When_left_side_of_operator_is_less_than_right_side()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<CalendarMonth>();
            var systemUnderTest1b = systemUnderTest1a.TweakComponentOfCalendarMonthByAmount(1, CalendarMonthComponent.Month);

            var systemUnderTest2a = A.Dummy<CalendarMonth>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfCalendarMonthByAmount(1, CalendarMonthComponent.Year);

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
            var systemUnderTest1a = A.Dummy<CalendarMonth>();
            var systemUnderTest1b = systemUnderTest1a.TweakComponentOfCalendarMonthByAmount(-1, CalendarMonthComponent.Month);

            var systemUnderTest2a = A.Dummy<CalendarMonth>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfCalendarMonthByAmount(-1, CalendarMonthComponent.Year);

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
            CalendarMonth systemUnderTest1 = null;
            CalendarMonth systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 <= systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void LessThanOrEqualToOperator___Should_return_true___When_left_side_of_operator_is_null_and_right_side_is_not_null()
        {
            // Arrange
            CalendarMonth systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<CalendarMonth>();

            // Act
            var result = systemUnderTest1 <= systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void LessThanOrEqualToOperator___Should_return_false___When_left_side_of_operator_is_not_null_and_right_side_is_null()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<CalendarMonth>();
            CalendarMonth systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 <= systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void LessThanOrEqualToOperator___Should_return_true___When_left_side_of_operator_is_equal_to_right_side()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<CalendarMonth>();
            var systemUnderTest2 = new CalendarMonth(systemUnderTest1.Year, systemUnderTest1.MonthOfYear);

            // Act
            var result = systemUnderTest1 <= systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void LessThanOrEqualToOperator___Should_return_true___When_left_side_of_operator_is_less_than_right_side()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<CalendarMonth>();
            var systemUnderTest1b = systemUnderTest1a.TweakComponentOfCalendarMonthByAmount(1, CalendarMonthComponent.Month);

            var systemUnderTest2a = A.Dummy<CalendarMonth>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfCalendarMonthByAmount(1, CalendarMonthComponent.Year);

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
            var systemUnderTest1a = A.Dummy<CalendarMonth>();
            var systemUnderTest1b = systemUnderTest1a.TweakComponentOfCalendarMonthByAmount(-1, CalendarMonthComponent.Month);

            var systemUnderTest2a = A.Dummy<CalendarMonth>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfCalendarMonthByAmount(-1, CalendarMonthComponent.Year);

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
            CalendarMonth systemUnderTest1 = null;
            CalendarMonth systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 > systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void GreaterThanOperator___Should_return_false___When_left_side_of_operator_is_null_and_right_side_is_not_null()
        {
            // Arrange
            CalendarMonth systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<CalendarMonth>();

            // Act
            var result = systemUnderTest1 > systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void GreaterThanOperator___Should_return_true___When_left_side_of_operator_is_not_null_and_right_side_is_null()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<CalendarMonth>();
            CalendarMonth systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 > systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void GreaterThanOperator___Should_return_false___When_left_side_of_operator_is_equal_to_right_side()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<CalendarMonth>();
            var systemUnderTest2 = new CalendarMonth(systemUnderTest1.Year, systemUnderTest1.MonthOfYear);

            // Act
            var result = systemUnderTest1 > systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void GreaterThanOperator___Should_return_false___When_left_side_of_operator_is_less_than_right_side()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<CalendarMonth>();
            var systemUnderTest1b = systemUnderTest1a.TweakComponentOfCalendarMonthByAmount(1, CalendarMonthComponent.Month);

            var systemUnderTest2a = A.Dummy<CalendarMonth>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfCalendarMonthByAmount(1, CalendarMonthComponent.Year);

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
            var systemUnderTest1a = A.Dummy<CalendarMonth>();
            var systemUnderTest1b = systemUnderTest1a.TweakComponentOfCalendarMonthByAmount(-1, CalendarMonthComponent.Month);

            var systemUnderTest2a = A.Dummy<CalendarMonth>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfCalendarMonthByAmount(-1, CalendarMonthComponent.Year);

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
            CalendarMonth systemUnderTest1 = null;
            CalendarMonth systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 >= systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void GreaterThanOrEqualToOperator___Should_return_false___When_left_side_of_operator_is_null_and_right_side_is_not_null()
        {
            // Arrange
            CalendarMonth systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<CalendarMonth>();

            // Act
            var result = systemUnderTest1 >= systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void GreaterThanOrEqualToOperator___Should_return_true___When_left_side_of_operator_is_not_null_and_right_side_is_null()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<CalendarMonth>();
            CalendarMonth systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 >= systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void GreaterThanOrEqualToOperator___Should_return_true___When_left_side_of_operator_is_equal_to_right_side()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<CalendarMonth>();
            var systemUnderTest2 = new CalendarMonth(systemUnderTest1.Year, systemUnderTest1.MonthOfYear);

            // Act
            var result = systemUnderTest1 >= systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void GreaterThanOrEqualToOperator___Should_return_false___When_left_side_of_operator_is_less_than_right_side()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<CalendarMonth>();
            var systemUnderTest1b = systemUnderTest1a.TweakComponentOfCalendarMonthByAmount(1, CalendarMonthComponent.Month);

            var systemUnderTest2a = A.Dummy<CalendarMonth>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfCalendarMonthByAmount(1, CalendarMonthComponent.Year);

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
            var systemUnderTest1a = A.Dummy<CalendarMonth>();
            var systemUnderTest1b = systemUnderTest1a.TweakComponentOfCalendarMonthByAmount(-1, CalendarMonthComponent.Month);

            var systemUnderTest2a = A.Dummy<CalendarMonth>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfCalendarMonthByAmount(-1, CalendarMonthComponent.Year);

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
            var systemUnderTest = A.Dummy<CalendarMonth>();

            // Act
            var result = systemUnderTest.CompareTo(null);

            // Assert
            result.Should().Be(1);
        }

        [Fact]
        public static void CompareTo___Should_return_negative_1___When_calling_typed_overload_and_test_object_is_less_than_other_object()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<CalendarMonth>();
            var systemUnderTest1b = systemUnderTest1a.TweakComponentOfCalendarMonthByAmount(1, CalendarMonthComponent.Month);

            var systemUnderTest2a = A.Dummy<CalendarMonth>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfCalendarMonthByAmount(1, CalendarMonthComponent.Year);

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
            var systemUnderTest1a = A.Dummy<CalendarMonth>();
            var systemUnderTest1b = systemUnderTest1a.TweakComponentOfCalendarMonthByAmount(-1, CalendarMonthComponent.Month);

            var systemUnderTest2a = A.Dummy<CalendarMonth>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfCalendarMonthByAmount(-1, CalendarMonthComponent.Year);

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
            var systemUnderTest1 = A.Dummy<CalendarMonth>();
            var systemUnderTest2 = new CalendarMonth(systemUnderTest1.Year, systemUnderTest1.MonthOfYear);

            // Act
            var result = systemUnderTest1.CompareTo(systemUnderTest2);

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public static void CompareTo___Should_throw_ArgumentException___When_calling_non_typed_overload_and_other_object_is_not_of_same_type_as_test_object()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<CalendarMonth>();
            var systemUnderTest2 = A.Dummy<FiscalMonth>();

            // Act
            var ex = Record.Exception(() => systemUnderTest1.CompareTo((object)systemUnderTest2));

            // Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void CompareTo___Should_throw_ArgumentException___When_calling_non_typed_overload_and_other_object_is_null()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<CalendarMonth>();
            CalendarMonth systemUnderTest2 = null;

            // Act
            var ex = Record.Exception(() => systemUnderTest1.CompareTo((object)systemUnderTest2));

            // Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void CompareTo___Should_return_negative_1___When_calling_non_typed_overload_and_test_object_is_less_than_other_object()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<CalendarMonth>();
            var systemUnderTest1b = systemUnderTest1a.TweakComponentOfCalendarMonthByAmount(1, CalendarMonthComponent.Month);

            var systemUnderTest2a = A.Dummy<CalendarMonth>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfCalendarMonthByAmount(1, CalendarMonthComponent.Year);

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
            var systemUnderTest1a = A.Dummy<CalendarMonth>();
            var systemUnderTest1b = systemUnderTest1a.TweakComponentOfCalendarMonthByAmount(-1, CalendarMonthComponent.Month);

            var systemUnderTest2a = A.Dummy<CalendarMonth>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfCalendarMonthByAmount(-1, CalendarMonthComponent.Year);

            // Act
            var result1 = systemUnderTest1a.CompareTo((object)systemUnderTest1b);
            var result2 = systemUnderTest2a.CompareTo((object)systemUnderTest2b);

            // Assert
            result1.Should().Be(1);
            result2.Should().Be(1);
        }

        [Fact]
        public static void CompareTo___Should_return_0___When_calling_non_typed_overload_and_test_object_is_equal_to_other_object()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<CalendarMonth>();
            var systemUnderTest2 = new CalendarMonth(systemUnderTest1.Year, systemUnderTest1.MonthOfYear);

            // Act
            var result = systemUnderTest1.CompareTo((object)systemUnderTest2);

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public static void ToString___Should_return_friendly_string_representation_of_object___When_called()
        {
            // Arrange
            var systemUnderTest1 = new CalendarMonth(2017, MonthOfYear.November);
            var systemUnderTest2 = new CalendarMonth(2017, MonthOfYear.February);

            // Act
            var toString1 = systemUnderTest1.ToString();
            var toString2 = systemUnderTest2.ToString();

            // Assert
            toString1.Should().Be("2017-11");
            toString2.Should().Be("2017-02");
        }

        [Fact]
        public static void Clone___Should_return_a_clone_of_the_object___When_called()
        {
            // Arrange
            var systemUnderTest = A.Dummy<CalendarMonth>();

            // Act
            var clone = systemUnderTest.Clone();

            // Assert
            clone.Should().Be(systemUnderTest);
            clone.Should().NotBeSameAs(systemUnderTest);
        }

        [Fact]
        public static void Deserialize___Should_return_equivalent_object_of_type_CalendarMonth___When_an_object_of_that_type_is_serialized_to_json_and_deserialized_as_abstract_type()
        {
            // Arrange
            var settings = JsonConfiguration.DefaultSerializerSettings;
            var expectedUnitOfTime = A.Dummy<CalendarMonth>();
            var serializedJson = JsonConvert.SerializeObject(expectedUnitOfTime, settings);

            // Act
            var systemUnderTest1 = JsonConvert.DeserializeObject<UnitOfTime>(serializedJson, settings) as CalendarMonth;
            var systemUnderTest2 = JsonConvert.DeserializeObject<CalendarUnitOfTime>(serializedJson, settings) as CalendarMonth;

            // Assert
            systemUnderTest1.Should().Be(expectedUnitOfTime);
            systemUnderTest2.Should().Be(expectedUnitOfTime);
        }

        [Fact]
        public static void UnitOfTimeKind__Should_return_Calendar___When_called()
        {
            // Arrange
            var unitOfTime = A.Dummy<CalendarMonth>();

            // Act
            var kind = unitOfTime.UnitOfTimeKind;

            // Assert
            kind.Should().Be(UnitOfTimeKind.Calendar);
        }

        [Fact]
        public static void UnitOfTimeGranularity__Should_return_Month___When_called()
        {
            // Arrange
            var unitOfTime = A.Dummy<CalendarMonth>();

            // Act
            var granularity = unitOfTime.UnitOfTimeGranularity;

            // Assert
            granularity.Should().Be(UnitOfTimeGranularity.Month);
        }

        private static CalendarMonth TweakComponentOfCalendarMonth(this CalendarMonth calendarMonth, CalendarMonthComponent componentToTweak)
        {
            if (componentToTweak == CalendarMonthComponent.Month)
            {
                var tweakedMonth = A.Dummy<MonthOfYear>().ThatIsNot(calendarMonth.MonthOfYear);
                var result = new CalendarMonth(calendarMonth.Year, tweakedMonth);
                return result;
            }

            if (componentToTweak == CalendarMonthComponent.Year)
            {
                var tweakedYear = A.Dummy<PositiveInteger>().ThatIs(y => y != calendarMonth.Year && y <= 9999);
                var result = new CalendarMonth(tweakedYear, calendarMonth.MonthOfYear);
                return result;
            }

            throw new NotSupportedException("this calendar month component is not supported: " + componentToTweak);
        }

        private static CalendarMonth TweakComponentOfCalendarMonthByAmount(this CalendarMonth calendarMonth, int amount, CalendarMonthComponent componentToTweak)
        {
            if (componentToTweak == CalendarMonthComponent.Month)
            {
                var referenceMonth = new DateTime(calendarMonth.Year, (int)calendarMonth.MonthOfYear, 1);
                var updatedMonth = referenceMonth.AddMonths(amount);
                var result = new CalendarMonth(updatedMonth.Year, (MonthOfYear)updatedMonth.Month);
                return result;
            }

            if (componentToTweak == CalendarMonthComponent.Year)
            {
                var result = new CalendarMonth(calendarMonth.Year + amount, calendarMonth.MonthOfYear);
                return result;
            }

            throw new NotSupportedException("this calendar month component is not supported: " + componentToTweak);
        }
    }
}