// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfTimeTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Test
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using FakeItEasy;

    using FluentAssertions;

    using OBeautifulCode.AutoFakeItEasy;

    using Xunit;

    public static class UnitOfTimeTest
    {
        [Fact]
        public static void EqualsOperator___Should_return_true___When_both_sides_of_operator_are_null()
        {
            // Arrange
            UnitOfTime systemUnderTest1 = null;
            UnitOfTime systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 == systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void EqualsOperator___Should_return_false___When_one_side_of_operator_is_null_and_the_other_side_is_not_null()
        {
            // Arrange
            UnitOfTime systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<UnitOfTime>();

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
            var systemUnderTest = A.Dummy<UnitOfTime>();

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
            var systemUnderTest1a = A.Dummy<UnitOfTime>().Whose(_ => _.UnitOfTimeGranularity != UnitOfTimeGranularity.Year);
            var systemUnderTest1b = A.Dummy<UnitOfTime>().Whose(_ => _.UnitOfTimeGranularity == UnitOfTimeGranularity.Year);

            var systemUnderTest2a = (UnitOfTime)A.Dummy<FiscalYear>();
            var systemUnderTest2b = (FiscalUnitOfTime)A.Dummy<FiscalYear>().ThatIs(_ => _.Year != ((FiscalYear)systemUnderTest2a).Year);

            var systemUnderTest3a = (FiscalUnitOfTime)A.Dummy<FiscalYear>();
            var systemUnderTest3b = A.Dummy<FiscalYear>().ThatIs(_ => _.Year != ((FiscalYear)systemUnderTest3a).Year);

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
        [SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "need to cast for this specific test")]
        public static void EqualsOperator___Should_return_true___When_objects_being_compared_have_same_property_values()
        {
            // Arrange
            var systemUnderTest1 = (FiscalUnitOfTime)A.Dummy<FiscalMonth>();
            var systemUnderTest2 = new FiscalMonth(((FiscalMonth)systemUnderTest1).Year, ((FiscalMonth)systemUnderTest1).MonthNumber);

            // Act
            var result = systemUnderTest1 == systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void EqualsOperator___Should_return_false___When_both_sides_of_operator_are_different_concrete_subclasses_of_UnitOfTime()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<UnitOfTime>();
            var systemUnderTest2 = A.Dummy<UnitOfTime>().Whose(_ => _.GetType() != systemUnderTest1.GetType());

            // Act
            var result = systemUnderTest1 == systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_false___When_both_sides_of_operator_are_null()
        {
            // Arrange
            UnitOfTime systemUnderTest1 = null;
            UnitOfTime systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 != systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_true___When_one_side_of_operator_is_null_and_the_other_side_is_not_null()
        {
            // Arrange
            UnitOfTime systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<UnitOfTime>();

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
            var systemUnderTest = A.Dummy<UnitOfTime>();

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
            var systemUnderTest1a = A.Dummy<UnitOfTime>().Whose(_ => _.UnitOfTimeGranularity != UnitOfTimeGranularity.Year);
            var systemUnderTest1b = A.Dummy<UnitOfTime>().Whose(_ => _.UnitOfTimeGranularity == UnitOfTimeGranularity.Year);

            var systemUnderTest2a = (UnitOfTime)A.Dummy<FiscalYear>();
            var systemUnderTest2b = (FiscalUnitOfTime)A.Dummy<FiscalYear>().ThatIs(_ => _.Year != ((FiscalYear)systemUnderTest2a).Year);

            var systemUnderTest3a = (FiscalUnitOfTime)A.Dummy<FiscalYear>();
            var systemUnderTest3b = A.Dummy<FiscalYear>().ThatIs(_ => _.Year != ((FiscalYear)systemUnderTest3a).Year);

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
        [SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "need to cast for this specific test")]
        public static void NotEqualsOperator___Should_return_false___When_objects_being_compared_have_same_property_values()
        {
            // Arrange
            var systemUnderTest1 = (FiscalUnitOfTime)A.Dummy<FiscalMonth>();
            var systemUnderTest2 = new FiscalMonth(((FiscalMonth)systemUnderTest1).Year, ((FiscalMonth)systemUnderTest1).MonthNumber);

            // Act
            var result = systemUnderTest1 != systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_true___When_both_sides_of_operator_are_different_concrete_subclasses_of_UnitOfTime()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<UnitOfTime>();
            var systemUnderTest2 = A.Dummy<UnitOfTime>().Whose(_ => _.GetType() != systemUnderTest1.GetType());

            // Act
            var result = systemUnderTest1 != systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Equals___Should_return_false___When_calling_typed_overload_and_parameter_other_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<UnitOfTime>();

            // Act
            var result = systemUnderTest.Equals(null);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void Equals___Should_return_true___When_calling_typed_overload_and_parameter_other_is_same_object()
        {
            // Arrange
            var systemUnderTest = A.Dummy<UnitOfTime>();

            // Act
            var result = systemUnderTest.Equals(systemUnderTest);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Equals___Should_return_false___When_calling_typed_overload_and_objects_being_compared_have_different_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<UnitOfTime>().Whose(_ => _.UnitOfTimeGranularity != UnitOfTimeGranularity.Year);
            var systemUnderTest1b = A.Dummy<UnitOfTime>().Whose(_ => _.UnitOfTimeGranularity == UnitOfTimeGranularity.Year);

            var systemUnderTest2a = (UnitOfTime)A.Dummy<FiscalYear>();
            var systemUnderTest2b = (FiscalUnitOfTime)A.Dummy<FiscalYear>().ThatIs(_ => _.Year != ((FiscalYear)systemUnderTest2a).Year);

            var systemUnderTest3a = (FiscalUnitOfTime)A.Dummy<FiscalYear>();
            var systemUnderTest3b = A.Dummy<FiscalYear>().ThatIs(_ => _.Year != ((FiscalYear)systemUnderTest3a).Year);

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
        [SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "need to cast for this specific test")]
        public static void Equals___Should_return_true___When_calling_typed_overload_and_objects_being_compared_have_same_property_values()
        {
            // Arrange
            var systemUnderTest1 = (FiscalUnitOfTime)A.Dummy<FiscalMonth>();
            var systemUnderTest2 = new FiscalMonth(((FiscalMonth)systemUnderTest1).Year, ((FiscalMonth)systemUnderTest1).MonthNumber);

            // Act
            var result = systemUnderTest1.Equals(systemUnderTest2);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Equals___Should_return_false___When_calling_typed_overload_and_both_sides_of_operator_are_different_concrete_subclasses_of_UnitOfTime()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<UnitOfTime>();
            var systemUnderTest2 = A.Dummy<UnitOfTime>().Whose(_ => _.GetType() != systemUnderTest1.GetType());

            // Act
            var result = systemUnderTest1.Equals(systemUnderTest2);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void LessThanOperator___Should_throw_ArgumentException___When_both_sides_of_operator_are_different_concrete_subclasses_of_UnitOfTime()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<UnitOfTime>();
            var systemUnderTest2 = A.Dummy<UnitOfTime>().Whose(_ => _.GetType() != systemUnderTest1.GetType());

            // Act
            var ex = Record.Exception(() => systemUnderTest1 < systemUnderTest2);

            // Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void LessThanOperator___Should_return_false___When_both_sides_of_operator_are_null()
        {
            // Arrange
            UnitOfTime systemUnderTest1 = null;
            UnitOfTime systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 < systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void LessThanOperator___Should_return_true___When_left_side_of_operator_is_null_and_right_side_is_not_null()
        {
            // Arrange
            UnitOfTime systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<UnitOfTime>();

            // Act
            var result = systemUnderTest1 < systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void LessThanOperator___Should_return_false___When_left_side_of_operator_is_not_null_and_right_side_is_null()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<UnitOfTime>();
            UnitOfTime systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 < systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void LessThanOperator___Should_return_false___When_left_side_of_operator_is_equal_to_right_side()
        {
            // Arrange
            var systemUnderTest1 = (FiscalUnitOfTime)A.Dummy<FiscalYear>();
            var systemUnderTest2 = (UnitOfTime)new FiscalYear(((FiscalYear)systemUnderTest1).Year);

            // Act
            var result = systemUnderTest1 < systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void LessThanOperator___Should_return_true___When_left_side_of_operator_is_less_than_right_side()
        {
            // Arrange
            var systemUnderTest1a = (UnitOfTime)A.Dummy<FiscalYear>();
            var systemUnderTest1b = (FiscalUnitOfTime)((FiscalYear)systemUnderTest1a).TweakYearByAmount(1);

            // Act
            var result = systemUnderTest1a < systemUnderTest1b;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void LessThanOperator___Should_return_false___When_left_side_of_operator_is_greater_than_right_side()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<FiscalYear>();
            var systemUnderTest1b = (FiscalUnitOfTime)systemUnderTest1a.TweakYearByAmount(-1);

            // Act
            var result = systemUnderTest1a < systemUnderTest1b;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void LessThanOrEqualToOperator___Should_throw_ArgumentException___When_both_sides_of_operator_are_different_concrete_subclasses_of_UnitOfTime()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<UnitOfTime>();
            var systemUnderTest2 = A.Dummy<UnitOfTime>().Whose(_ => _.GetType() != systemUnderTest1.GetType());

            // Act
            var ex = Record.Exception(() => systemUnderTest1 <= systemUnderTest2);

            // Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void LessThanOrEqualToOperator___Should_return_true___When_both_sides_of_operator_are_null()
        {
            // Arrange
            UnitOfTime systemUnderTest1 = null;
            UnitOfTime systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 <= systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void LessThanOrEqualToOperator___Should_return_true___When_left_side_of_operator_is_null_and_right_side_is_not_null()
        {
            // Arrange
            UnitOfTime systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<UnitOfTime>();

            // Act
            var result = systemUnderTest1 <= systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void LessThanOrEqualToOperator___Should_return_false___When_left_side_of_operator_is_not_null_and_right_side_is_null()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<UnitOfTime>();
            UnitOfTime systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 <= systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void LessThanOrEqualToOperator___Should_return_true___When_left_side_of_operator_is_equal_to_right_side()
        {
            // Arrange
            var systemUnderTest1 = (UnitOfTime)A.Dummy<FiscalYear>();
            var systemUnderTest2 = new FiscalYear(((FiscalYear)systemUnderTest1).Year);

            // Act
            var result = systemUnderTest1 <= systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void LessThanOrEqualToOperator___Should_return_true___When_left_side_of_operator_is_less_than_right_side()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<FiscalYear>();
            var systemUnderTest1b = (FiscalUnitOfTime)systemUnderTest1a.TweakYearByAmount(1);

            // Act
            var result = systemUnderTest1a <= systemUnderTest1b;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void LessThanOrEqualToOperator___Should_return_false___When_left_side_of_operator_is_greater_than_right_side()
        {
            // Arrange
            var systemUnderTest1a = (FiscalUnitOfTime)A.Dummy<FiscalYear>();
            var systemUnderTest1b = (UnitOfTime)((FiscalYear)systemUnderTest1a).TweakYearByAmount(-1);

            // Act
            var result = systemUnderTest1a <= systemUnderTest1b;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void GreaterThanOperator___Should_throw_ArgumentException___When_both_sides_of_operator_are_different_concrete_subclasses_of_UnitOfTime()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<UnitOfTime>();
            var systemUnderTest2 = A.Dummy<UnitOfTime>().Whose(_ => _.GetType() != systemUnderTest1.GetType());

            // Act
            var ex = Record.Exception(() => systemUnderTest1 > systemUnderTest2);

            // Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void GreaterThanOperator___Should_return_false___When_both_sides_of_operator_are_null()
        {
            // Arrange
            UnitOfTime systemUnderTest1 = null;
            UnitOfTime systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 > systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void GreaterThanOperator___Should_return_false___When_left_side_of_operator_is_null_and_right_side_is_not_null()
        {
            // Arrange
            UnitOfTime systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<UnitOfTime>();

            // Act
            var result = systemUnderTest1 > systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void GreaterThanOperator___Should_return_true___When_left_side_of_operator_is_not_null_and_right_side_is_null()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<UnitOfTime>();
            UnitOfTime systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 > systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void GreaterThanOperator___Should_return_false___When_left_side_of_operator_is_equal_to_right_side()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<FiscalYear>();
            var systemUnderTest2 = (UnitOfTime)new FiscalYear(systemUnderTest1.Year);

            // Act
            var result = systemUnderTest1 > systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void GreaterThanOperator___Should_return_false___When_left_side_of_operator_is_less_than_right_side()
        {
            // Arrange
            var systemUnderTest1a = (UnitOfTime)A.Dummy<FiscalYear>();
            var systemUnderTest1b = (FiscalUnitOfTime)((FiscalYear)systemUnderTest1a).TweakYearByAmount(1);

            // Act
            var result = systemUnderTest1a > systemUnderTest1b;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void GreaterThanOperator___Should_return_true___When_left_side_of_operator_is_greater_than_right_side()
        {
            // Arrange
            var systemUnderTest1a = (FiscalUnitOfTime)A.Dummy<FiscalYear>();
            var systemUnderTest1b = ((FiscalYear)systemUnderTest1a).TweakYearByAmount(-1);

            // Act
            var result = systemUnderTest1a > systemUnderTest1b;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void GreaterThanOrEqualToOperator___Should_throw_ArgumentException___When_both_sides_of_operator_are_different_concrete_subclasses_of_UnitOfTime()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<UnitOfTime>();
            var systemUnderTest2 = A.Dummy<UnitOfTime>().Whose(_ => _.GetType() != systemUnderTest1.GetType());

            // Act
            var ex = Record.Exception(() => systemUnderTest1 >= systemUnderTest2);

            // Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void GreaterThanOrEqualToOperator___Should_return_true___When_both_sides_of_operator_are_null()
        {
            // Arrange
            UnitOfTime systemUnderTest1 = null;
            UnitOfTime systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 >= systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void GreaterThanOrEqualToOperator___Should_return_false___When_left_side_of_operator_is_null_and_right_side_is_not_null()
        {
            // Arrange
            UnitOfTime systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<UnitOfTime>();

            // Act
            var result = systemUnderTest1 >= systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void GreaterThanOrEqualToOperator___Should_return_true___When_left_side_of_operator_is_not_null_and_right_side_is_null()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<UnitOfTime>();
            UnitOfTime systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 >= systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void GreaterThanOrEqualToOperator___Should_return_true___When_left_side_of_operator_is_equal_to_right_side()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<FiscalYear>();
            var systemUnderTest2 = (UnitOfTime)new FiscalYear(systemUnderTest1.Year);

            // Act
            var result = systemUnderTest1 >= systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void GreaterThanOrEqualToOperator___Should_return_false___When_left_side_of_operator_is_less_than_right_side()
        {
            // Arrange
            var systemUnderTest1a = (FiscalUnitOfTime)A.Dummy<FiscalYear>();
            var systemUnderTest1b = ((FiscalYear)systemUnderTest1a).TweakYearByAmount(1);

            // Act
            var result = systemUnderTest1a >= systemUnderTest1b;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void GreaterThanOrEqualToOperator___Should_return_true___When_left_side_of_operator_is_greater_than_right_side()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<FiscalYear>();
            var systemUnderTest1b = (FiscalUnitOfTime)systemUnderTest1a.TweakYearByAmount(-1);

            // Act
            var result = systemUnderTest1a >= systemUnderTest1b;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void CompareTo___Should_throw_ArgumentException___When_parameter_other_is_a_different_concrete_subclasses_of_UnitOfTime()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<UnitOfTime>();
            var systemUnderTest2 = A.Dummy<UnitOfTime>().Whose(_ => _.GetType() != systemUnderTest1.GetType());

            // Act
            var ex = Record.Exception(() => systemUnderTest1.CompareTo(systemUnderTest2));

            // Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void CompareTo___Should_return_1___When_calling_typed_overload_and_other_object_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<UnitOfTime>();

            // Act
            var result = systemUnderTest.CompareTo(null);

            // Assert
            result.Should().Be(1);
        }

        [Fact]
        public static void CompareTo___Should_return_negative_1___When_calling_typed_overload_and_test_object_is_less_than_other_object()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<FiscalYear>();
            var systemUnderTest1b = (UnitOfTime)systemUnderTest1a.TweakYearByAmount(1);

            // Act
            var result = systemUnderTest1a.CompareTo(systemUnderTest1b);

            // Assert
            result.Should().Be(-1);
        }

        [Fact]
        public static void CompareTo___Should_return_1___When_calling_typed_overload_and_test_object_is_greater_than_other_object()
        {
            // Arrange
            var systemUnderTest1a = (UnitOfTime)A.Dummy<FiscalYear>();
            var systemUnderTest1b = (FiscalUnitOfTime)((FiscalYear)systemUnderTest1a).TweakYearByAmount(-1);

            // Act
            var result = systemUnderTest1a.CompareTo(systemUnderTest1b);

            // Assert
            result.Should().Be(1);
        }

        [Fact]
        public static void CompareTo___Should_return_0___When_calling_typed_overload_and_test_object_is_equal_to_other_object()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<FiscalYear>();
            var systemUnderTest2 = (UnitOfTime)new FiscalYear(systemUnderTest1.Year);

            // Act
            var result = systemUnderTest1.CompareTo(systemUnderTest2);

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "This test is inherently complex.")]
        [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "This test is inherently complex.")]
        public static void Clone___Should_throw_InvalidOperationException___When_cloned_object_cannot_be_casted_to_generic_type_parameter()
        {
            // Arrange
            var unitsOfTime = new Dictionary<UnitOfTime, IEnumerable<Type>>
            {
                { A.Dummy<CalendarDay>(), Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(CalendarDay)) && (_ != typeof(CalendarUnitOfTime))) },
                { A.Dummy<CalendarMonth>(), Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(CalendarMonth)) && (_ != typeof(CalendarUnitOfTime))) },
                { A.Dummy<CalendarQuarter>(), Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(CalendarQuarter)) && (_ != typeof(CalendarUnitOfTime))) },
                { A.Dummy<CalendarYear>(), Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(CalendarYear)) && (_ != typeof(CalendarUnitOfTime))) },
                { A.Dummy<CalendarUnbounded>(),  Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(CalendarUnbounded)) && (_ != typeof(CalendarUnitOfTime))) },
                { A.Dummy<FiscalMonth>(), Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(FiscalMonth)) && (_ != typeof(FiscalUnitOfTime))) },
                { A.Dummy<FiscalQuarter>(), Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(FiscalQuarter)) && (_ != typeof(FiscalUnitOfTime))) },
                { A.Dummy<FiscalYear>(), Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(FiscalYear)) && (_ != typeof(FiscalUnitOfTime))) },
                { A.Dummy<FiscalUnbounded>(),  Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(FiscalUnbounded)) && (_ != typeof(FiscalUnitOfTime))) },
                { A.Dummy<GenericQuarter>(), Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(GenericQuarter)) && (_ != typeof(GenericUnitOfTime))) },
                { A.Dummy<GenericMonth>(), Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(GenericMonth)) && (_ != typeof(GenericUnitOfTime))) },
                { A.Dummy<GenericYear>(),  Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(GenericYear)) && (_ != typeof(GenericUnitOfTime))) },
                { A.Dummy<GenericUnbounded>(),  Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(GenericUnbounded)) && (_ != typeof(GenericUnitOfTime))) },
            };

            var cloneMethod = typeof(UnitOfTime).GetMethods().Single(_ => _.Name == nameof(UnitOfTime.Clone) && _.ContainsGenericParameters);

            // Act
            var exceptions = new List<Exception>();
            foreach (var unitOfTime in unitsOfTime.Keys)
            {
                foreach (var type in unitsOfTime[unitOfTime])
                {
                    var genericMethod = cloneMethod.MakeGenericMethod(type);
                    exceptions.Add(Record.Exception(() => genericMethod.Invoke(unitOfTime, null)).InnerException);
                }
            }

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "This test is inherently complex.")]
        public static void Clone___Should_return_cloned_object___When_cloned_object_can_be_casted_to_generic_type_parameter()
        {
            // Arrange
            var unitsOfTime = new Dictionary<UnitOfTime, IEnumerable<Type>>
            {
                { A.Dummy<CalendarDay>(), new[] { typeof(CalendarDay), typeof(CalendarUnitOfTime), typeof(UnitOfTime) } },
                { A.Dummy<CalendarMonth>(), new[] { typeof(CalendarMonth), typeof(CalendarUnitOfTime), typeof(UnitOfTime) } },
                { A.Dummy<CalendarQuarter>(), new[] { typeof(CalendarQuarter), typeof(CalendarUnitOfTime), typeof(UnitOfTime) } },
                { A.Dummy<CalendarYear>(), new[] { typeof(CalendarYear), typeof(CalendarUnitOfTime), typeof(UnitOfTime) } },
                { A.Dummy<CalendarUnbounded>(), new[] { typeof(CalendarUnbounded), typeof(CalendarUnitOfTime), typeof(UnitOfTime) } },
                { A.Dummy<FiscalMonth>(), new[] { typeof(FiscalMonth), typeof(FiscalUnitOfTime), typeof(UnitOfTime) } },
                { A.Dummy<FiscalQuarter>(), new[] { typeof(FiscalQuarter), typeof(FiscalUnitOfTime), typeof(UnitOfTime) } },
                { A.Dummy<FiscalYear>(), new[] { typeof(FiscalYear), typeof(FiscalUnitOfTime), typeof(UnitOfTime) } },
                { A.Dummy<FiscalUnbounded>(), new[] { typeof(FiscalUnbounded), typeof(FiscalUnitOfTime), typeof(UnitOfTime) } },
                { A.Dummy<GenericQuarter>(), new[] { typeof(GenericQuarter), typeof(GenericUnitOfTime), typeof(UnitOfTime) } },
                { A.Dummy<GenericMonth>(), new[] { typeof(GenericMonth), typeof(GenericUnitOfTime), typeof(UnitOfTime) } },
                { A.Dummy<GenericYear>(), new[] { typeof(GenericYear), typeof(GenericUnitOfTime), typeof(UnitOfTime) } },
                { A.Dummy<GenericUnbounded>(), new[] { typeof(GenericUnbounded), typeof(GenericUnitOfTime), typeof(UnitOfTime) } },
            };

            var cloneMethod = typeof(UnitOfTime).GetMethods().Single(_ => _.Name == nameof(UnitOfTime.Clone) && _.ContainsGenericParameters);

            // Act, Assert
            foreach (var unitOfTime in unitsOfTime.Keys)
            {
                foreach (var type in unitsOfTime[unitOfTime])
                {
                    var genericMethod = cloneMethod.MakeGenericMethod(type);
                    var result = (UnitOfTime)genericMethod.Invoke(unitOfTime, null);
                    unitOfTime.Should().Be(result);
                    unitOfTime.Should().NotBeSameAs(result);
                }
            }
        }

        [Fact]
        public static void GetHashCode___Should_not_be_equal_for_two_UnitOfKind_objects___When_they_are_of_different_kinds_but_have_the_same_granularity_and_same_property_values()
        {
            // Arrange
            var calendarUnbounded = A.Dummy<CalendarUnbounded>();
            var fiscalUnbounded = new FiscalUnbounded();
            var genericUnbounded = new GenericUnbounded();

            var calendarYear = A.Dummy<CalendarYear>();
            var fiscalYear = new FiscalYear(calendarYear.Year);
            var genericYear = new GenericYear(calendarYear.Year);

            var calendarQuarter = A.Dummy<CalendarQuarter>();
            var fiscalQuarter = new FiscalQuarter(calendarQuarter.Year, calendarQuarter.QuarterNumber);
            var genericQuarter = new GenericQuarter(calendarQuarter.Year, calendarQuarter.QuarterNumber);

            var calendarMonth = A.Dummy<CalendarMonth>();
            var fiscalMonth = new FiscalMonth(calendarMonth.Year, calendarMonth.MonthNumber);
            var genericMonth = new GenericMonth(calendarMonth.Year, calendarMonth.MonthNumber);

            // Act
            var calendarUnboundedHashCode = calendarUnbounded.GetHashCode();
            var fiscalUnboundedHashCode = fiscalUnbounded.GetHashCode();
            var genericUnboundedHashCode = genericUnbounded.GetHashCode();

            var calendarYearHashCode = calendarYear.GetHashCode();
            var fiscalYearHashCode = fiscalYear.GetHashCode();
            var genericYearHashCode = genericYear.GetHashCode();

            var calendarQuarterHashCode = calendarQuarter.GetHashCode();
            var fiscalQuarterHashCode = fiscalQuarter.GetHashCode();
            var genericQuarterHashCode = genericQuarter.GetHashCode();

            var calendarMonthHashCode = calendarMonth.GetHashCode();
            var fiscalMonthHashCode = fiscalMonth.GetHashCode();
            var genericMonthHashCode = genericMonth.GetHashCode();

            // Assert
            calendarUnboundedHashCode.Should().NotBe(fiscalUnboundedHashCode);
            calendarUnboundedHashCode.Should().NotBe(genericUnboundedHashCode);
            fiscalUnboundedHashCode.Should().NotBe(genericUnboundedHashCode);

            calendarYearHashCode.Should().NotBe(fiscalYearHashCode);
            calendarYearHashCode.Should().NotBe(genericYearHashCode);
            fiscalYearHashCode.Should().NotBe(genericYearHashCode);

            calendarQuarterHashCode.Should().NotBe(fiscalQuarterHashCode);
            calendarQuarterHashCode.Should().NotBe(genericQuarterHashCode);
            fiscalQuarterHashCode.Should().NotBe(genericQuarterHashCode);

            calendarMonthHashCode.Should().NotBe(fiscalMonthHashCode);
            calendarMonthHashCode.Should().NotBe(genericMonthHashCode);
            fiscalMonthHashCode.Should().NotBe(genericMonthHashCode);
        }
    }
}