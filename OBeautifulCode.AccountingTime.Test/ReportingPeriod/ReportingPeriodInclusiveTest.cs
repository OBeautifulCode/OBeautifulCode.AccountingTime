// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriodInclusiveTest.cs" company="OBeautifulCode">
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

    public static class ReportingPeriodInclusiveTest
    {
        // ReSharper disable InconsistentNaming
        [Fact]
        public static void Constructor___Should_throw_ArgumentNullException___When_parameter_start_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => new ReportingPeriodInclusive<CalendarQuarter>(null, A.Dummy<CalendarQuarter>()));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void Constructor___Should_throw_ArgumentNullException___When_parameter_end_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => new ReportingPeriodInclusive<CalendarQuarter>(A.Dummy<CalendarQuarter>(), null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void Constructor___Should_throw_ArgumentException___When_parameter_start_and_end_are_not_of_the_same_concrete_type()
        {
            // Arrange
            var start1 = A.Dummy<CalendarQuarter>();
            var end1 = A.Dummy<CalendarDay>();

            var start2 = A.Dummy<FiscalQuarter>();
            var end2 = A.Dummy<CalendarQuarter>();

            // Act
            var ex1 = Record.Exception(() => new ReportingPeriodInclusive<CalendarUnitOfTime>(start1, end1));
            var ex2 = Record.Exception(() => new ReportingPeriodInclusive<UnitOfTime>(start2, end2));

            // Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex2.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void Constructor___Should_throw_ArgumentOutOfRangeException___When_parameter_start_is_greater_than_parameter_end()
        {
            // Arrange
            var start = A.Dummy<CalendarQuarter>();
            var end = A.Dummy<CalendarQuarter>().ThatIs(q => q < start);

            // Act
            var ex = Record.Exception(() => new ReportingPeriodInclusive<CalendarUnitOfTime>(start, end));

            // Assert
            ex.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void Start___Should_return_same_start_passed_to_constructor___When_getting()
        {
            // Arrange
            var start = A.Dummy<CalendarQuarter>();
            var end = A.Dummy<CalendarQuarter>().ThatIs(q => q >= start);
            var systemUnderTest = new ReportingPeriodInclusive<CalendarUnitOfTime>(start, end);

            // Act
            var actualStart = systemUnderTest.Start;

            // Assert
            actualStart.Should().BeSameAs(start);
        }

        [Fact]
        public static void End___Should_return_same_end_passed_to_constructor___When_getting()
        {
            // Arrange
            var start = A.Dummy<CalendarQuarter>();
            var end = A.Dummy<CalendarQuarter>().ThatIs(q => q >= start);
            var systemUnderTest = new ReportingPeriodInclusive<CalendarUnitOfTime>(start, end);

            // Act
            var actualEnd = systemUnderTest.End;

            // Assert
            actualEnd.Should().BeSameAs(end);
        }

        [Fact]
        public static void EqualsOperator___Should_return_true___When_both_sides_of_operator_are_null()
        {
            // Arrange
            ReportingPeriodInclusive<UnitOfTime> systemUnderTest1 = null;
            ReportingPeriodInclusive<UnitOfTime> systemUnderTest2 = null;

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
            ReportingPeriodInclusive<UnitOfTime> systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<ReportingPeriodInclusive<UnitOfTime>>();

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
            var systemUnderTest = A.Dummy<ReportingPeriodInclusive<UnitOfTime>>();

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
            var systemUnderTest1a = A.Dummy<ReportingPeriodInclusive<CalendarDay>>();
            var systemUnderTest1b = A.Dummy<ReportingPeriodInclusive<CalendarDay>>();

            var systemUnderTest2a = A.Dummy<ReportingPeriodInclusive<FiscalQuarter>>();
            var systemUnderTest2b = new ReportingPeriodInclusive<FiscalQuarter>(systemUnderTest2a.Start, A.Dummy<FiscalQuarter>().ThatIs(q => (q != systemUnderTest2a.End) && (q >= systemUnderTest2a.Start)));

            var systemUnderTest3a = A.Dummy<ReportingPeriodInclusive<CalendarMonth>>();
            var systemUnderTest3b = new ReportingPeriodInclusive<CalendarMonth>(A.Dummy<CalendarMonth>().ThatIs(m => (m != systemUnderTest3a.Start) && (m <= systemUnderTest3a.End)), systemUnderTest3a.End);

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
            var systemUnderTest1 = A.Dummy<ReportingPeriodInclusive<UnitOfTime>>();
            var systemUnderTest2 = new ReportingPeriodInclusive<UnitOfTime>(systemUnderTest1.Start, systemUnderTest1.End);

            // Act
            var result = systemUnderTest1 == systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_false___When_both_sides_of_operator_are_null()
        {
            // Arrange
            ReportingPeriodInclusive<UnitOfTime> systemUnderTest1 = null;
            ReportingPeriodInclusive<UnitOfTime> systemUnderTest2 = null;

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
            ReportingPeriodInclusive<UnitOfTime> systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<ReportingPeriodInclusive<UnitOfTime>>();

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
            var systemUnderTest = A.Dummy<ReportingPeriodInclusive<UnitOfTime>>();

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
            var systemUnderTest1a = A.Dummy<ReportingPeriodInclusive<CalendarDay>>();
            var systemUnderTest1b = A.Dummy<ReportingPeriodInclusive<CalendarDay>>();

            var systemUnderTest2a = A.Dummy<ReportingPeriodInclusive<FiscalQuarter>>();
            var systemUnderTest2b = new ReportingPeriodInclusive<FiscalQuarter>(systemUnderTest2a.Start, A.Dummy<FiscalQuarter>().ThatIs(q => (q != systemUnderTest2a.End) && (q >= systemUnderTest2a.Start)));

            var systemUnderTest3a = A.Dummy<ReportingPeriodInclusive<CalendarMonth>>();
            var systemUnderTest3b = new ReportingPeriodInclusive<CalendarMonth>(A.Dummy<CalendarMonth>().ThatIs(m => (m != systemUnderTest3a.Start) && (m <= systemUnderTest3a.End)), systemUnderTest3a.End);

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
            var systemUnderTest1 = A.Dummy<ReportingPeriodInclusive<UnitOfTime>>();
            var systemUnderTest2 = new ReportingPeriodInclusive<UnitOfTime>(systemUnderTest1.Start, systemUnderTest1.End);

            // Act
            var result = systemUnderTest1 != systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void Equals___Should_return_false___When_calling_typed_overload_and_parameter_other_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<ReportingPeriodInclusive<UnitOfTime>>();

            // Act
            var result = systemUnderTest.Equals(null);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void Equals___Should_return_true___When_calling_typed_overload_and_parameter_other_is_same_object()
        {
            // Arrange
            var systemUnderTest = A.Dummy<ReportingPeriodInclusive<UnitOfTime>>();

            // Act
            var result = systemUnderTest.Equals(systemUnderTest);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Equals___Should_return_false___When_calling_typed_overload_and_objects_being_compared_have_different_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<ReportingPeriodInclusive<CalendarDay>>();
            var systemUnderTest1b = A.Dummy<ReportingPeriodInclusive<CalendarDay>>();

            var systemUnderTest2a = A.Dummy<ReportingPeriodInclusive<FiscalQuarter>>();
            var systemUnderTest2b = new ReportingPeriodInclusive<FiscalQuarter>(systemUnderTest2a.Start, A.Dummy<FiscalQuarter>().ThatIs(q => (q != systemUnderTest2a.End) && (q >= systemUnderTest2a.Start)));

            var systemUnderTest3a = A.Dummy<ReportingPeriodInclusive<CalendarMonth>>();
            var systemUnderTest3b = new ReportingPeriodInclusive<CalendarMonth>(A.Dummy<CalendarMonth>().ThatIs(m => (m != systemUnderTest3a.Start) && (m <= systemUnderTest3a.End)), systemUnderTest3a.End);

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
            var systemUnderTest1 = A.Dummy<ReportingPeriodInclusive<UnitOfTime>>();
            var systemUnderTest2 = new ReportingPeriodInclusive<UnitOfTime>(systemUnderTest1.Start, systemUnderTest1.End);

            // Act
            var result = systemUnderTest1.Equals(systemUnderTest2);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Equals___Should_return_false___When_calling_non_typed_overload_and_parameter_other_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<ReportingPeriodInclusive<UnitOfTime>>();

            // Act
            var result = systemUnderTest.Equals(null);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void Equals___Should_return_false___When_calling_non_typed_overload_and_parameter_other_is_not_of_the_same_type()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<ReportingPeriodInclusive<UnitOfTime>>();
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
            var systemUnderTest = A.Dummy<ReportingPeriodInclusive<UnitOfTime>>();

            // Act
            var result = systemUnderTest.Equals((object)systemUnderTest);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Equals___Should_return_false___When_calling_non_typed_overload_and_objects_being_compared_have_different_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<ReportingPeriodInclusive<CalendarDay>>();
            var systemUnderTest1b = A.Dummy<ReportingPeriodInclusive<CalendarDay>>();

            var systemUnderTest2a = A.Dummy<ReportingPeriodInclusive<FiscalQuarter>>();
            var systemUnderTest2b = new ReportingPeriodInclusive<FiscalQuarter>(systemUnderTest2a.Start, A.Dummy<FiscalQuarter>().ThatIs(q => (q != systemUnderTest2a.End) && (q >= systemUnderTest2a.Start)));

            var systemUnderTest3a = A.Dummy<ReportingPeriodInclusive<CalendarMonth>>();
            var systemUnderTest3b = new ReportingPeriodInclusive<CalendarMonth>(A.Dummy<CalendarMonth>().ThatIs(m => (m != systemUnderTest3a.Start) && (m <= systemUnderTest3a.End)), systemUnderTest3a.End);

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
            var systemUnderTest1 = A.Dummy<ReportingPeriodInclusive<UnitOfTime>>();
            var systemUnderTest2 = new ReportingPeriodInclusive<UnitOfTime>(systemUnderTest1.Start, systemUnderTest1.End);

            // Act
            var result = systemUnderTest1.Equals((object)systemUnderTest2);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void GetHashCode___Should_not_be_equal_for_two_ReportingPeriodInclusive___When_both_objects_have_different_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<ReportingPeriodInclusive<CalendarDay>>();
            var systemUnderTest1b = A.Dummy<ReportingPeriodInclusive<CalendarDay>>();

            var systemUnderTest2a = A.Dummy<ReportingPeriodInclusive<FiscalQuarter>>();
            var systemUnderTest2b = new ReportingPeriodInclusive<FiscalQuarter>(systemUnderTest2a.Start, A.Dummy<FiscalQuarter>().ThatIs(q => (q != systemUnderTest2a.End) && (q >= systemUnderTest2a.Start)));

            var systemUnderTest3a = A.Dummy<ReportingPeriodInclusive<CalendarMonth>>();
            var systemUnderTest3b = new ReportingPeriodInclusive<CalendarMonth>(A.Dummy<CalendarMonth>().ThatIs(m => (m != systemUnderTest3a.Start) && (m <= systemUnderTest3a.End)), systemUnderTest3a.End);

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
        public static void GetHashCode___Should_be_equal_for_two_ReportingPeriodInclusive___When_both_objects_have_the_same_property_values()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<ReportingPeriodInclusive<UnitOfTime>>();
            var systemUnderTest2 = new ReportingPeriodInclusive<UnitOfTime>(systemUnderTest1.Start, systemUnderTest1.End);

            // Act
            var hash1 = systemUnderTest1.GetHashCode();
            var hash2 = systemUnderTest2.GetHashCode();

            // Assert
            hash1.Should().Be(hash2);
        }

        [Fact]
        public static void ToString___Should_return_friendly_string_representation_of_object___When_called()
        {
            // Arrange
            var start = new CalendarDay(2017, MonthOfYear.November, DayOfMonth.Thirty);
            var end = new CalendarDay(2018, MonthOfYear.March, DayOfMonth.TwentyFour);

            var systemUnderTest1 = new ReportingPeriodInclusive<CalendarDay>(start, end);
            var systemUnderTest2 = new ReportingPeriodInclusive<UnitOfTime>(start, end);

            // Act
            var toString1 = systemUnderTest1.ToString();
            var toString2 = systemUnderTest2.ToString();

            // Assert
            toString1.Should().Be("2017-11-30 to 2018-03-24");
            toString2.Should().Be("2017-11-30 to 2018-03-24");
        }

        // ReSharper restore InconsistentNaming
    }
}

// ReSharper restore CheckNamespace