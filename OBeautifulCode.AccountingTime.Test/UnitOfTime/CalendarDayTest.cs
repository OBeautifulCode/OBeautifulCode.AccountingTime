// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalendarDayTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Test
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;

    using FakeItEasy;

    using FluentAssertions;

    using OBeautifulCode.AutoFakeItEasy;

    using Xunit;

    public static class CalendarDayTest
    {
        private enum CalendarDayComponent
        {
            Day,

            Month,

            Year,
        }

        [Fact]
        public static void Constructor___Should_throw_ArgumentOutOfRangeException___When_parameter_year_is_less_than_1()
        {
            // Arrange
            var validDay = A.Dummy<CalendarDay>();

            // Act
            var ex1 = Record.Exception(() => new CalendarDay(0, validDay.MonthOfYear, validDay.DayOfMonth));
            var ex2 =
                Record.Exception(
                    () => new CalendarDay(A.Dummy<ZeroOrNegativeInteger>(), validDay.MonthOfYear, validDay.DayOfMonth));

            // Assert
            ex1.Should().BeOfType<ArgumentOutOfRangeException>();
            ex2.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void Constructor___Should_throw_ArgumentOutOfRangeException___When_parameter_year_is_greater_than_9999()
        {
            // Arrange
            var validDay = A.Dummy<CalendarDay>();

            // Act
            var ex1 = Record.Exception(() => new CalendarDay(10000, validDay.MonthOfYear, validDay.DayOfMonth));
            var ex2 = Record.Exception(() => new CalendarDay(A.Dummy<PositiveInteger>().ThatIs(y => y > 9999), validDay.MonthOfYear, validDay.DayOfMonth));

            // Assert
            ex1.Should().BeOfType<ArgumentOutOfRangeException>();
            ex2.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void Constructor___Should_throw_ArgumentOutOfRangeException___When_parameter_monthOfYear_is_Invalid()
        {
            // Arrange
            var validDay = A.Dummy<CalendarDay>();

            // Act
            var ex = Record.Exception(() => new CalendarDay(validDay.Year, MonthOfYear.Invalid, validDay.DayOfMonth));

            // Assert
            ex.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void Constructor___Should_throw_ArgumentOutOfRangeException___When_parameter_dayOfMonth_is_Invalid()
        {
            // Arrange
            var validDay = A.Dummy<CalendarDay>();

            // Act
            var ex = Record.Exception(() => new CalendarDay(validDay.Year, validDay.MonthOfYear, DayOfMonth.Invalid));

            // Assert
            ex.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void Constructor___Should_throw_ArgumentException___When_dayOfMonth_is_not_a_valid_day_in_the_specified_year_and_monthOfYear()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => new CalendarDay(2016, MonthOfYear.February, (DayOfMonth)30));
            var ex2 = Record.Exception(() => new CalendarDay(2016, MonthOfYear.February, (DayOfMonth)31));
            var ex3 = Record.Exception(() => new CalendarDay(2015, MonthOfYear.February, (DayOfMonth)29));
            var ex4 = Record.Exception(() => new CalendarDay(2015, MonthOfYear.February, (DayOfMonth)30));
            var ex5 = Record.Exception(() => new CalendarDay(2015, MonthOfYear.February, (DayOfMonth)31));
            var ex6 = Record.Exception(() => new CalendarDay(2016, MonthOfYear.November, (DayOfMonth)31));
            var ex7 = Record.Exception(() => new CalendarDay(2017, MonthOfYear.April, (DayOfMonth)31));

            // Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex2.Should().BeOfType<ArgumentException>();
            ex3.Should().BeOfType<ArgumentException>();
            ex4.Should().BeOfType<ArgumentException>();
            ex5.Should().BeOfType<ArgumentException>();
            ex6.Should().BeOfType<ArgumentException>();
            ex7.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void Year___Should_return_same_year_passed_to_constructor___When_getting()
        {
            // Arrange
            var validDay = A.Dummy<CalendarDay>();

            var year = validDay.Year;
            var monthOfYear = validDay.MonthOfYear;
            var dayOfMonth = validDay.DayOfMonth;

            var systemUnderTest = new CalendarDay(year, monthOfYear, dayOfMonth);

            // Act
            var actualYear = systemUnderTest.Year;

            // Assert
            actualYear.Should().Be(year);
        }

        [Fact]
        public static void MonthOfYear___Should_return_same_monthOfYear_passed_to_constructor___When_getting()
        {
            // Arrange
            var validDay = A.Dummy<CalendarDay>();

            var year = validDay.Year;
            var monthOfYear = validDay.MonthOfYear;
            var dayOfMonth = validDay.DayOfMonth;

            var systemUnderTest = new CalendarDay(year, monthOfYear, dayOfMonth);

            // Act
            var actualMonthOfYear = systemUnderTest.MonthOfYear;

            // Assert
            actualMonthOfYear.Should().Be(monthOfYear);
        }

        [Fact]
        public static void DayOfMonth___Should_return_same_dayOfMonth_passed_to_constructor___When_getting()
        {
            // Arrange
            var validDay = A.Dummy<CalendarDay>();

            var year = validDay.Year;
            var monthOfYear = validDay.MonthOfYear;
            var dayOfMonth = validDay.DayOfMonth;

            var systemUnderTest = new CalendarDay(year, monthOfYear, dayOfMonth);

            // Act
            var actualDayOfMonth = systemUnderTest.DayOfMonth;

            // Assert
            actualDayOfMonth.Should().Be(dayOfMonth);
        }

        [Fact]
        public static void MonthNumber___Should_return_month_number_of_monthOfYear_passed_to_constructor___When_getting()
        {
            // Arrange
            var validDay = A.Dummy<CalendarDay>();

            var year = validDay.Year;
            var monthOfYear = validDay.MonthOfYear;
            var dayOfMonth = validDay.DayOfMonth;

            var systemUnderTest = new CalendarDay(year, monthOfYear, dayOfMonth);

            // Act
            var actualMonthNumber = systemUnderTest.MonthNumber;

            // Assert
            actualMonthNumber.Should().Be((MonthNumber)monthOfYear);
        }

        [Fact]
        public static void EqualsOperator___Should_return_true___When_both_sides_of_operator_are_null()
        {
            // Arrange
            CalendarDay systemUnderTest1 = null;
            CalendarDay systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 == systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void EqualsOperator___Should_return_false___When_one_side_of_operator_is_null_and_the_other_side_is_not_null()
        {
            // Arrange
            CalendarDay systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<CalendarDay>();

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
            var systemUnderTest = A.Dummy<CalendarDay>();

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
            var systemUnderTest1a = A.Dummy<CalendarDay>();
            var systemUnderTest1b = A.Dummy<CalendarDay>();

            var systemUnderTest2a = A.Dummy<CalendarDay>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfCalendarDay(CalendarDayComponent.Day);

            var systemUnderTest3a = A.Dummy<CalendarDay>();
            var systemUnderTest3b = systemUnderTest3a.TweakComponentOfCalendarDay(CalendarDayComponent.Month);

            var systemUnderTest4a = A.Dummy<CalendarDay>();
            var systemUnderTest4b = systemUnderTest4a.TweakComponentOfCalendarDay(CalendarDayComponent.Year);

            // Act
            var result1 = systemUnderTest1a == systemUnderTest1b;
            var result2 = systemUnderTest2a == systemUnderTest2b;
            var result3 = systemUnderTest3a == systemUnderTest3b;
            var result4 = systemUnderTest4a == systemUnderTest4b;

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
            result3.Should().BeFalse();
            result4.Should().BeFalse();
        }

        [Fact]
        public static void EqualsOperator___Should_return_true___When_objects_being_compared_have_same_property_values()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<CalendarDay>();
            var systemUnderTest2 = new CalendarDay(systemUnderTest1.Year, systemUnderTest1.MonthOfYear, systemUnderTest1.DayOfMonth);

            // Act
            var result = systemUnderTest1 == systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_false___When_both_sides_of_operator_are_null()
        {
            // Arrange
            CalendarDay systemUnderTest1 = null;
            CalendarDay systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 != systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_true___When_one_side_of_operator_is_null_and_the_other_side_is_not_null()
        {
            // Arrange
            CalendarDay systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<CalendarDay>();

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
            var systemUnderTest = A.Dummy<CalendarDay>();

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
            var systemUnderTest1a = A.Dummy<CalendarDay>();
            var systemUnderTest1b = A.Dummy<CalendarDay>();

            var systemUnderTest2a = A.Dummy<CalendarDay>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfCalendarDay(CalendarDayComponent.Day);

            var systemUnderTest3a = A.Dummy<CalendarDay>();
            var systemUnderTest3b = systemUnderTest3a.TweakComponentOfCalendarDay(CalendarDayComponent.Month);

            var systemUnderTest4a = A.Dummy<CalendarDay>();
            var systemUnderTest4b = systemUnderTest4a.TweakComponentOfCalendarDay(CalendarDayComponent.Year);

            // Act
            var result1 = systemUnderTest1a != systemUnderTest1b;
            var result2 = systemUnderTest2a != systemUnderTest2b;
            var result3 = systemUnderTest3a != systemUnderTest3b;
            var result4 = systemUnderTest4a != systemUnderTest4b;

            // Assert
            result1.Should().BeTrue();
            result2.Should().BeTrue();
            result3.Should().BeTrue();
            result4.Should().BeTrue();
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_false___When_objects_being_compared_have_same_property_values()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<CalendarDay>();
            var systemUnderTest2 = new CalendarDay(systemUnderTest1.Year, systemUnderTest1.MonthOfYear, systemUnderTest1.DayOfMonth);

            // Act
            var result = systemUnderTest1 != systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void Equals___Should_return_false___When_calling_typed_overload_and_parameter_other_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<CalendarDay>();

            // Act
            var result = systemUnderTest.Equals(null);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void Equals___Should_return_true___When_calling_typed_overload_and_parameter_other_is_same_object()
        {
            // Arrange
            var systemUnderTest = A.Dummy<CalendarDay>();

            // Act
            var result = systemUnderTest.Equals(systemUnderTest);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Equals___Should_return_false___When_calling_typed_overload_and_objects_being_compared_have_different_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<CalendarDay>();
            var systemUnderTest1b = A.Dummy<CalendarDay>();

            var systemUnderTest2a = A.Dummy<CalendarDay>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfCalendarDay(CalendarDayComponent.Day);

            var systemUnderTest3a = A.Dummy<CalendarDay>();
            var systemUnderTest3b = systemUnderTest3a.TweakComponentOfCalendarDay(CalendarDayComponent.Month);

            var systemUnderTest4a = A.Dummy<CalendarDay>();
            var systemUnderTest4b = systemUnderTest4a.TweakComponentOfCalendarDay(CalendarDayComponent.Year);

            // Act
            var result1 = systemUnderTest1a.Equals(systemUnderTest1b);
            var result2 = systemUnderTest2a.Equals(systemUnderTest2b);
            var result3 = systemUnderTest3a.Equals(systemUnderTest3b);
            var result4 = systemUnderTest4a.Equals(systemUnderTest4b);

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
            result3.Should().BeFalse();
            result4.Should().BeFalse();
        }

        [Fact]
        public static void Equals___Should_return_true___When_calling_typed_overload_and_objects_being_compared_have_same_property_values()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<CalendarDay>();
            var systemUnderTest2 = new CalendarDay(systemUnderTest1.Year, systemUnderTest1.MonthOfYear, systemUnderTest1.DayOfMonth);

            // Act
            var result = systemUnderTest1.Equals(systemUnderTest2);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Equals___Should_return_false___When_calling_non_typed_overload_and_parameter_other_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<CalendarDay>();

            // Act
            var result = systemUnderTest.Equals(null);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void Equals___Should_return_false___When_calling_non_typed_overload_and_parameter_other_is_not_of_the_same_type()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<CalendarDay>();
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
            var systemUnderTest = A.Dummy<CalendarDay>();

            // Act
            var result = systemUnderTest.Equals((object)systemUnderTest);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Equals___Should_return_false___When_calling_non_typed_overload_and_objects_being_compared_have_different_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<CalendarDay>();
            var systemUnderTest1b = A.Dummy<CalendarDay>();

            var systemUnderTest2a = A.Dummy<CalendarDay>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfCalendarDay(CalendarDayComponent.Day);

            var systemUnderTest3a = A.Dummy<CalendarDay>();
            var systemUnderTest3b = systemUnderTest3a.TweakComponentOfCalendarDay(CalendarDayComponent.Month);

            var systemUnderTest4a = A.Dummy<CalendarDay>();
            var systemUnderTest4b = systemUnderTest4a.TweakComponentOfCalendarDay(CalendarDayComponent.Year);

            // Act
            var result1 = systemUnderTest1a.Equals((object)systemUnderTest1b);
            var result2 = systemUnderTest2a.Equals((object)systemUnderTest2b);
            var result3 = systemUnderTest3a.Equals((object)systemUnderTest3b);
            var result4 = systemUnderTest4a.Equals((object)systemUnderTest4b);

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
            result3.Should().BeFalse();
            result4.Should().BeFalse();
        }

        [Fact]
        public static void Equals___Should_return_true___When_calling_non_typed_overload_and_objects_being_compared_have_same_property_values()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<CalendarDay>();
            var systemUnderTest2 = new CalendarDay(systemUnderTest1.Year, systemUnderTest1.MonthOfYear, systemUnderTest1.DayOfMonth);

            // Act
            var result = systemUnderTest1.Equals((object)systemUnderTest2);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void GetHashCode___Should_not_be_equal_for_two_CalendarDays___When_both_objects_have_different_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<CalendarDay>();
            var systemUnderTest1b = A.Dummy<CalendarDay>();

            var systemUnderTest2a = A.Dummy<CalendarDay>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfCalendarDay(CalendarDayComponent.Day);

            var systemUnderTest3a = A.Dummy<CalendarDay>();
            var systemUnderTest3b = systemUnderTest3a.TweakComponentOfCalendarDay(CalendarDayComponent.Month);

            var systemUnderTest4a = A.Dummy<CalendarDay>();
            var systemUnderTest4b = systemUnderTest4a.TweakComponentOfCalendarDay(CalendarDayComponent.Year);

            // Act
            var hash1a = systemUnderTest1a.GetHashCode();
            var hash1b = systemUnderTest1b.GetHashCode();

            var hash2a = systemUnderTest2a.GetHashCode();
            var hash2b = systemUnderTest2b.GetHashCode();

            var hash3a = systemUnderTest3a.GetHashCode();
            var hash3b = systemUnderTest3b.GetHashCode();

            var hash4a = systemUnderTest4a.GetHashCode();
            var hash4b = systemUnderTest4b.GetHashCode();

            // Assert
            hash1a.Should().NotBe(hash1b);
            hash2a.Should().NotBe(hash2b);
            hash3a.Should().NotBe(hash3b);
            hash4a.Should().NotBe(hash4b);
        }

        [Fact]
        public static void GetHashCode___Should_be_equal_for_two_CalendarDays___When_both_objects_have_the_same_property_values()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<CalendarDay>();
            var systemUnderTest2 = new CalendarDay(systemUnderTest1.Year, systemUnderTest1.MonthOfYear, systemUnderTest1.DayOfMonth);

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
            CalendarDay systemUnderTest1 = null;
            CalendarDay systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 < systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void LessThanOperator___Should_return_true___When_left_side_of_operator_is_null_and_right_side_is_not_null()
        {
            // Arrange
            CalendarDay systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<CalendarDay>();

            // Act
            var result = systemUnderTest1 < systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void LessThanOperator___Should_return_false___When_left_side_of_operator_is_not_null_and_right_side_is_null()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<CalendarDay>();
            CalendarDay systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 < systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void LessThanOperator___Should_return_false___When_left_side_of_operator_is_equal_to_right_side()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<CalendarDay>();
            var systemUnderTest2 = new CalendarDay(systemUnderTest1.Year, systemUnderTest1.MonthOfYear, systemUnderTest1.DayOfMonth);

            // Act
            var result = systemUnderTest1 < systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void LessThanOperator___Should_return_true___When_left_side_of_operator_is_less_than_right_side()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<CalendarDay>();
            var systemUnderTest1b = systemUnderTest1a.TweakComponentOfCalendarDayByAmount(1, CalendarDayComponent.Day);

            var systemUnderTest2a = A.Dummy<CalendarDay>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfCalendarDayByAmount(1, CalendarDayComponent.Month);

            var systemUnderTest3a = A.Dummy<CalendarDay>();
            var systemUnderTest3b = systemUnderTest3a.TweakComponentOfCalendarDayByAmount(1, CalendarDayComponent.Year);

            // Act
            var result1 = systemUnderTest1a < systemUnderTest1b;
            var result2 = systemUnderTest2a < systemUnderTest2b;
            var result3 = systemUnderTest3a < systemUnderTest3b;

            // Assert
            result1.Should().BeTrue();
            result2.Should().BeTrue();
            result3.Should().BeTrue();
        }

        [Fact]
        public static void LessThanOperator___Should_return_false___When_left_side_of_operator_is_greater_than_right_side()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<CalendarDay>();
            var systemUnderTest1b = systemUnderTest1a.TweakComponentOfCalendarDayByAmount(-1, CalendarDayComponent.Day);

            var systemUnderTest2a = A.Dummy<CalendarDay>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfCalendarDayByAmount(-1, CalendarDayComponent.Month);

            var systemUnderTest3a = A.Dummy<CalendarDay>();
            var systemUnderTest3b = systemUnderTest3a.TweakComponentOfCalendarDayByAmount(-1, CalendarDayComponent.Year);

            // Act
            var result1 = systemUnderTest1a < systemUnderTest1b;
            var result2 = systemUnderTest2a < systemUnderTest2b;
            var result3 = systemUnderTest3a < systemUnderTest3b;

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
            result3.Should().BeFalse();
        }

        [Fact]
        public static void LessThanOrEqualToOperator___Should_return_true___When_both_sides_of_operator_are_null()
        {
            // Arrange
            CalendarDay systemUnderTest1 = null;
            CalendarDay systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 <= systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void LessThanOrEqualToOperator___Should_return_true___When_left_side_of_operator_is_null_and_right_side_is_not_null()
        {
            // Arrange
            CalendarDay systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<CalendarDay>();

            // Act
            var result = systemUnderTest1 <= systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void LessThanOrEqualToOperator___Should_return_false___When_left_side_of_operator_is_not_null_and_right_side_is_null()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<CalendarDay>();
            CalendarDay systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 <= systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void LessThanOrEqualToOperator___Should_return_true___When_left_side_of_operator_is_equal_to_right_side()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<CalendarDay>();
            var systemUnderTest2 = new CalendarDay(systemUnderTest1.Year, systemUnderTest1.MonthOfYear, systemUnderTest1.DayOfMonth);

            // Act
            var result = systemUnderTest1 <= systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void LessThanOrEqualToOperator___Should_return_true___When_left_side_of_operator_is_less_than_right_side()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<CalendarDay>();
            var systemUnderTest1b = systemUnderTest1a.TweakComponentOfCalendarDayByAmount(1, CalendarDayComponent.Day);

            var systemUnderTest2a = A.Dummy<CalendarDay>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfCalendarDayByAmount(1, CalendarDayComponent.Month);

            var systemUnderTest3a = A.Dummy<CalendarDay>();
            var systemUnderTest3b = systemUnderTest3a.TweakComponentOfCalendarDayByAmount(1, CalendarDayComponent.Year);

            // Act
            var result1 = systemUnderTest1a <= systemUnderTest1b;
            var result2 = systemUnderTest2a <= systemUnderTest2b;
            var result3 = systemUnderTest3a <= systemUnderTest3b;

            // Assert
            result1.Should().BeTrue();
            result2.Should().BeTrue();
            result3.Should().BeTrue();
        }

        [Fact]
        public static void LessThanOrEqualToOperator___Should_return_false___When_left_side_of_operator_is_greater_than_right_side()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<CalendarDay>();
            var systemUnderTest1b = systemUnderTest1a.TweakComponentOfCalendarDayByAmount(-1, CalendarDayComponent.Day);

            var systemUnderTest2a = A.Dummy<CalendarDay>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfCalendarDayByAmount(-1, CalendarDayComponent.Month);

            var systemUnderTest3a = A.Dummy<CalendarDay>();
            var systemUnderTest3b = systemUnderTest3a.TweakComponentOfCalendarDayByAmount(-1, CalendarDayComponent.Year);

            // Act
            var result1 = systemUnderTest1a <= systemUnderTest1b;
            var result2 = systemUnderTest2a <= systemUnderTest2b;
            var result3 = systemUnderTest3a <= systemUnderTest3b;

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
            result3.Should().BeFalse();
        }

        [Fact]
        public static void GreaterThanOperator___Should_return_false___When_both_sides_of_operator_are_null()
        {
            // Arrange
            CalendarDay systemUnderTest1 = null;
            CalendarDay systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 > systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void GreaterThanOperator___Should_return_false___When_left_side_of_operator_is_null_and_right_side_is_not_null()
        {
            // Arrange
            CalendarDay systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<CalendarDay>();

            // Act
            var result = systemUnderTest1 > systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void GreaterThanOperator___Should_return_true___When_left_side_of_operator_is_not_null_and_right_side_is_null()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<CalendarDay>();
            CalendarDay systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 > systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void GreaterThanOperator___Should_return_false___When_left_side_of_operator_is_equal_to_right_side()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<CalendarDay>();
            var systemUnderTest2 = new CalendarDay(systemUnderTest1.Year, systemUnderTest1.MonthOfYear, systemUnderTest1.DayOfMonth);

            // Act
            var result = systemUnderTest1 > systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void GreaterThanOperator___Should_return_false___When_left_side_of_operator_is_less_than_right_side()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<CalendarDay>();
            var systemUnderTest1b = systemUnderTest1a.TweakComponentOfCalendarDayByAmount(1, CalendarDayComponent.Day);

            var systemUnderTest2a = A.Dummy<CalendarDay>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfCalendarDayByAmount(1, CalendarDayComponent.Month);

            var systemUnderTest3a = A.Dummy<CalendarDay>();
            var systemUnderTest3b = systemUnderTest3a.TweakComponentOfCalendarDayByAmount(1, CalendarDayComponent.Year);

            // Act
            var result1 = systemUnderTest1a > systemUnderTest1b;
            var result2 = systemUnderTest2a > systemUnderTest2b;
            var result3 = systemUnderTest3a > systemUnderTest3b;

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
            result3.Should().BeFalse();
        }

        [Fact]
        public static void GreaterThanOperator___Should_return_true___When_left_side_of_operator_is_greater_than_right_side()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<CalendarDay>();
            var systemUnderTest1b = systemUnderTest1a.TweakComponentOfCalendarDayByAmount(-1, CalendarDayComponent.Day);

            var systemUnderTest2a = A.Dummy<CalendarDay>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfCalendarDayByAmount(-1, CalendarDayComponent.Month);

            var systemUnderTest3a = A.Dummy<CalendarDay>();
            var systemUnderTest3b = systemUnderTest3a.TweakComponentOfCalendarDayByAmount(-1, CalendarDayComponent.Year);

            // Act
            var result1 = systemUnderTest1a > systemUnderTest1b;
            var result2 = systemUnderTest2a > systemUnderTest2b;
            var result3 = systemUnderTest3a > systemUnderTest3b;

            // Assert
            result1.Should().BeTrue();
            result2.Should().BeTrue();
            result3.Should().BeTrue();
        }

        [Fact]
        public static void GreaterThanOrEqualToOperator___Should_return_true___When_both_sides_of_operator_are_null()
        {
            // Arrange
            CalendarDay systemUnderTest1 = null;
            CalendarDay systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 >= systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void GreaterThanOrEqualToOperator___Should_return_false___When_left_side_of_operator_is_null_and_right_side_is_not_null()
        {
            // Arrange
            CalendarDay systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<CalendarDay>();

            // Act
            var result = systemUnderTest1 >= systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void GreaterThanOrEqualToOperator___Should_return_true___When_left_side_of_operator_is_not_null_and_right_side_is_null()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<CalendarDay>();
            CalendarDay systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 >= systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void GreaterThanOrEqualToOperator___Should_return_true___When_left_side_of_operator_is_equal_to_right_side()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<CalendarDay>();
            var systemUnderTest2 = new CalendarDay(systemUnderTest1.Year, systemUnderTest1.MonthOfYear, systemUnderTest1.DayOfMonth);

            // Act
            var result = systemUnderTest1 >= systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void GreaterThanOrEqualToOperator___Should_return_false___When_left_side_of_operator_is_less_than_right_side()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<CalendarDay>();
            var systemUnderTest1b = systemUnderTest1a.TweakComponentOfCalendarDayByAmount(1, CalendarDayComponent.Day);

            var systemUnderTest2a = A.Dummy<CalendarDay>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfCalendarDayByAmount(1, CalendarDayComponent.Month);

            var systemUnderTest3a = A.Dummy<CalendarDay>();
            var systemUnderTest3b = systemUnderTest3a.TweakComponentOfCalendarDayByAmount(1, CalendarDayComponent.Year);

            // Act
            var result1 = systemUnderTest1a >= systemUnderTest1b;
            var result2 = systemUnderTest2a >= systemUnderTest2b;
            var result3 = systemUnderTest3a >= systemUnderTest3b;

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
            result3.Should().BeFalse();
        }

        [Fact]
        public static void GreaterThanOrEqualToOperator___Should_return_true___When_left_side_of_operator_is_greater_than_right_side()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<CalendarDay>();
            var systemUnderTest1b = systemUnderTest1a.TweakComponentOfCalendarDayByAmount(-1, CalendarDayComponent.Day);

            var systemUnderTest2a = A.Dummy<CalendarDay>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfCalendarDayByAmount(-1, CalendarDayComponent.Month);

            var systemUnderTest3a = A.Dummy<CalendarDay>();
            var systemUnderTest3b = systemUnderTest3a.TweakComponentOfCalendarDayByAmount(-1, CalendarDayComponent.Year);

            // Act
            var result1 = systemUnderTest1a >= systemUnderTest1b;
            var result2 = systemUnderTest2a >= systemUnderTest2b;
            var result3 = systemUnderTest3a >= systemUnderTest3b;

            // Assert
            result1.Should().BeTrue();
            result2.Should().BeTrue();
            result3.Should().BeTrue();
        }

        [Fact]
        public static void CompareTo___Should_return_1___When_calling_typed_overload_and_other_object_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<CalendarDay>();

            // Act
            var result = systemUnderTest.CompareTo(null);

            // Assert
            result.Should().Be(1);
        }

        [Fact]
        public static void CompareTo___Should_return_negative_1___When_calling_typed_overload_and_test_object_is_less_than_other_object()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<CalendarDay>();
            var systemUnderTest1b = systemUnderTest1a.TweakComponentOfCalendarDayByAmount(1, CalendarDayComponent.Day);

            var systemUnderTest2a = A.Dummy<CalendarDay>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfCalendarDayByAmount(1, CalendarDayComponent.Month);

            var systemUnderTest3a = A.Dummy<CalendarDay>();
            var systemUnderTest3b = systemUnderTest3a.TweakComponentOfCalendarDayByAmount(1, CalendarDayComponent.Year);

            // Act
            var result1 = systemUnderTest1a.CompareTo(systemUnderTest1b);
            var result2 = systemUnderTest2a.CompareTo(systemUnderTest2b);
            var result3 = systemUnderTest3a.CompareTo(systemUnderTest3b);

            // Assert
            result1.Should().Be(-1);
            result2.Should().Be(-1);
            result3.Should().Be(-1);
        }

        [Fact]
        public static void CompareTo___Should_return_1___When_calling_typed_overload_and_test_object_is_greater_than_other_object()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<CalendarDay>();
            var systemUnderTest1b = systemUnderTest1a.TweakComponentOfCalendarDayByAmount(-1, CalendarDayComponent.Day);

            var systemUnderTest2a = A.Dummy<CalendarDay>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfCalendarDayByAmount(-1, CalendarDayComponent.Month);

            var systemUnderTest3a = A.Dummy<CalendarDay>();
            var systemUnderTest3b = systemUnderTest3a.TweakComponentOfCalendarDayByAmount(-1, CalendarDayComponent.Year);

            // Act
            var result1 = systemUnderTest1a.CompareTo(systemUnderTest1b);
            var result2 = systemUnderTest2a.CompareTo(systemUnderTest2b);
            var result3 = systemUnderTest3a.CompareTo(systemUnderTest3b);

            // Assert
            result1.Should().Be(1);
            result2.Should().Be(1);
            result3.Should().Be(1);
        }

        [Fact]
        public static void CompareTo___Should_return_0___When_calling_typed_overload_and_test_object_is_equal_to_other_object()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<CalendarDay>();
            var systemUnderTest2 = new CalendarDay(systemUnderTest1.Year, systemUnderTest1.MonthOfYear, systemUnderTest1.DayOfMonth);

            // Act
            var result = systemUnderTest1.CompareTo(systemUnderTest2);

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public static void CompareTo___Should_throw_ArgumentException___When_calling_non_typed_overload_and_other_object_is_not_of_same_type_as_test_object()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<CalendarDay>();
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
            var systemUnderTest1 = A.Dummy<CalendarDay>();
            CalendarDay systemUnderTest2 = null;

            // Act
            var ex = Record.Exception(() => systemUnderTest1.CompareTo((object)systemUnderTest2));

            // Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void CompareTo___Should_return_negative_1___When_calling_non_typed_overload_and_test_object_is_less_than_other_object()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<CalendarDay>();
            var systemUnderTest1b = systemUnderTest1a.TweakComponentOfCalendarDayByAmount(1, CalendarDayComponent.Day);

            var systemUnderTest2a = A.Dummy<CalendarDay>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfCalendarDayByAmount(1, CalendarDayComponent.Month);

            var systemUnderTest3a = A.Dummy<CalendarDay>();
            var systemUnderTest3b = systemUnderTest3a.TweakComponentOfCalendarDayByAmount(1, CalendarDayComponent.Year);

            // Act
            var result1 = systemUnderTest1a.CompareTo((object)systemUnderTest1b);
            var result2 = systemUnderTest2a.CompareTo((object)systemUnderTest2b);
            var result3 = systemUnderTest3a.CompareTo((object)systemUnderTest3b);

            // Assert
            result1.Should().Be(-1);
            result2.Should().Be(-1);
            result3.Should().Be(-1);
        }

        [Fact]
        public static void CompareTo___Should_return_1___When_calling_non_typed_overload_and_test_object_is_greater_than_other_object()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<CalendarDay>();
            var systemUnderTest1b = systemUnderTest1a.TweakComponentOfCalendarDayByAmount(-1, CalendarDayComponent.Day);

            var systemUnderTest2a = A.Dummy<CalendarDay>();
            var systemUnderTest2b = systemUnderTest2a.TweakComponentOfCalendarDayByAmount(-1, CalendarDayComponent.Month);

            var systemUnderTest3a = A.Dummy<CalendarDay>();
            var systemUnderTest3b = systemUnderTest3a.TweakComponentOfCalendarDayByAmount(-1, CalendarDayComponent.Year);

            // Act
            var result1 = systemUnderTest1a.CompareTo((object)systemUnderTest1b);
            var result2 = systemUnderTest2a.CompareTo((object)systemUnderTest2b);
            var result3 = systemUnderTest3a.CompareTo((object)systemUnderTest3b);

            // Assert
            result1.Should().Be(1);
            result2.Should().Be(1);
            result3.Should().Be(1);
        }

        [Fact]
        public static void CompareTo___Should_return_0___When_calling_non_typed_overload_and_test_object_is_equal_to_other_object()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<CalendarDay>();
            var systemUnderTest2 = new CalendarDay(systemUnderTest1.Year, systemUnderTest1.MonthOfYear, systemUnderTest1.DayOfMonth);

            // Act
            var result = systemUnderTest1.CompareTo((object)systemUnderTest2);

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public static void ToDateTime___Should_return_DateTime_version_of_CalendarDay_with_DateTimeKind_Unspecified___When_called()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<CalendarDay>();
            var systemUnderTest2 = new CalendarDay(2017, MonthOfYear.November, DayOfMonth.Thirty);

            var expectedDateTime1 = new DateTime(systemUnderTest1.Year, (int)systemUnderTest1.MonthOfYear, (int)systemUnderTest1.DayOfMonth);
            var expectedDateTime2 = new DateTime(2017, 11, 30);

            // Act
            var dateTime1 = systemUnderTest1.ToDateTime();
            var dateTime2 = systemUnderTest2.ToDateTime();

            // Assert
            dateTime1.Should().Be(expectedDateTime1);
            dateTime1.Kind.Should().Be(DateTimeKind.Unspecified);

            dateTime2.Should().Be(expectedDateTime2);
            dateTime2.Kind.Should().Be(DateTimeKind.Unspecified);
        }

        [Fact]
        public static void ToString___Should_return_result_of_calling_ToString_on_DateTime_representation_of_object___When_calling_overload_with_formatting()
        {
            // Arrange
            var systemUnderTest1 = new CalendarDay(2017, MonthOfYear.November, DayOfMonth.Thirty);
            var systemUnderTest2 = new CalendarDay(2017, MonthOfYear.February, DayOfMonth.Three);

            // Act
            var toString1a = systemUnderTest1.ToString("MM yyyy dd");
            var toString1b = systemUnderTest1.ToString("MM yyyy dd", CultureInfo.CurrentCulture);
            var toString2a = systemUnderTest2.ToString("MM yyyy dd");
            var toString2b = systemUnderTest2.ToString("MM yyyy dd", CultureInfo.CurrentCulture);

            // Assert
            toString1a.Should().Be("11 2017 30");
            toString1b.Should().Be("11 2017 30");
            toString2a.Should().Be("02 2017 03");
            toString2b.Should().Be("02 2017 03");
        }

        [Fact]
        public static void ToString___Should_return_friendly_string_representation_of_object___When_calling_overload_without_formatting()
        {
            // Arrange
            var systemUnderTest1 = new CalendarDay(2017, MonthOfYear.November, DayOfMonth.Thirty);
            var systemUnderTest2 = new CalendarDay(2017, MonthOfYear.February, DayOfMonth.Three);

            // Act
            var toString1 = systemUnderTest1.ToString();
            var toString2 = systemUnderTest2.ToString();

            // Assert
            toString1.Should().Be("2017-11-30");
            toString2.Should().Be("2017-02-03");
        }

        [Fact]
        public static void Clone___Should_return_a_clone_of_the_object___When_called()
        {
            // Arrange
            var systemUnderTest = A.Dummy<CalendarDay>();

            // Act
            var clone = systemUnderTest.Clone();

            // Assert
            clone.Should().Be(systemUnderTest);
            clone.Should().NotBeSameAs(systemUnderTest);
        }

        [Fact]
        public static void UnitOfTimeKind__Should_return_Calendar___When_called()
        {
            // Arrange
            var unitOfTime = A.Dummy<CalendarDay>();

            // Act
            var kind = unitOfTime.UnitOfTimeKind;

            // Assert
            kind.Should().Be(UnitOfTimeKind.Calendar);
        }

        [Fact]
        public static void UnitOfTimeGranularity__Should_return_Day___When_called()
        {
            // Arrange
            var unitOfTime = A.Dummy<CalendarDay>();

            // Act
            var granularity = unitOfTime.UnitOfTimeGranularity;

            // Assert
            granularity.Should().Be(UnitOfTimeGranularity.Day);
        }

        [SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "OBeautifulCode.AccountingTime.CalendarDay", Justification = "In this case we are trying to determine if creating the object will throw.")]
        private static CalendarDay TweakComponentOfCalendarDay(this CalendarDay calendarDay, CalendarDayComponent componentToTweak)
        {
            if (componentToTweak == CalendarDayComponent.Day)
            {
                var tweakedDay = A.Dummy<DayOfMonth>().ThatIs(
                    d =>
                        d != calendarDay.DayOfMonth &&
                        DoesNotThrow(() => new CalendarDay(calendarDay.Year, calendarDay.MonthOfYear, d)));
                var result = new CalendarDay(calendarDay.Year, calendarDay.MonthOfYear, tweakedDay);
                return result;
            }

            if (componentToTweak == CalendarDayComponent.Month)
            {
                var tweakedMonth = A.Dummy<MonthOfYear>().ThatIs(
                    m =>
                        m != calendarDay.MonthOfYear &&
                        DoesNotThrow(() => new CalendarDay(calendarDay.Year, m, calendarDay.DayOfMonth)));
                var result = new CalendarDay(calendarDay.Year, tweakedMonth, calendarDay.DayOfMonth);
                return result;
            }

            if (componentToTweak == CalendarDayComponent.Year)
            {
                var tweakedYear = A.Dummy<PositiveInteger>().ThatIs(
                    y =>
                        y != calendarDay.Year &&
                        y <= 9999 &&
                        DoesNotThrow(() => new CalendarDay(y, calendarDay.MonthOfYear, calendarDay.DayOfMonth)));
                var result = new CalendarDay(tweakedYear, calendarDay.MonthOfYear, calendarDay.DayOfMonth);
                return result;
            }

            throw new NotSupportedException("this calendar day component is not supported: " + componentToTweak);
        }

        private static CalendarDay TweakComponentOfCalendarDayByAmount(this CalendarDay calendarDay, int amount, CalendarDayComponent componentToTweak)
        {
            if (componentToTweak == CalendarDayComponent.Day)
            {
                var updatedDateTime = calendarDay.ToDateTime().AddDays(amount);
                var result = new CalendarDay(updatedDateTime.Year, (MonthOfYear)updatedDateTime.Month, (DayOfMonth)updatedDateTime.Day);
                return result;
            }

            if (componentToTweak == CalendarDayComponent.Month)
            {
                var updatedDateTime = calendarDay.ToDateTime().AddMonths(amount);
                var result = new CalendarDay(updatedDateTime.Year, (MonthOfYear)updatedDateTime.Month, (DayOfMonth)updatedDateTime.Day);
                return result;
            }

            if (componentToTweak == CalendarDayComponent.Year)
            {
                var updatedDateTime = calendarDay.ToDateTime().AddYears(amount);
                var result = new CalendarDay(updatedDateTime.Year, (MonthOfYear)updatedDateTime.Month, (DayOfMonth)updatedDateTime.Day);
                return result;
            }

            throw new NotSupportedException("this calendar day component is not supported: " + componentToTweak);
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "The purpose of this method is to determine if any exception has been thrown.")]
        private static bool DoesNotThrow(Action action)
        {
            try
            {
                action();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}