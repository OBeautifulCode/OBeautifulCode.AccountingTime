// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriodTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoFakeItEasy;

    using FakeItEasy;

    using FluentAssertions;

    using Naos.Recipes.TupleInitializers;

    using Xunit;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "There are a lot of unit-of-time types.")]
    public static class ReportingPeriodTest
    {
        // ReSharper disable InconsistentNaming
        [Fact]
        public static void Constructor___Should_throw_ArgumentNullException___When_parameter_start_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => new ReportingPeriod<CalendarQuarter>(null, A.Dummy<CalendarQuarter>()));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void Constructor___Should_throw_ArgumentNullException___When_parameter_end_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => new ReportingPeriod<CalendarQuarter>(A.Dummy<CalendarQuarter>(), null));

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
            var ex1 = Record.Exception(() => new ReportingPeriod<CalendarUnitOfTime>(start1, end1));
            var ex2 = Record.Exception(() => new ReportingPeriod<UnitOfTime>(start2, end2));

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
            var ex = Record.Exception(() => new ReportingPeriod<CalendarUnitOfTime>(start, end));

            // Assert
            ex.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void Constructor___Should_not_throw___When_parameter_start_is_equal_to_parameter_end()
        {
            // Arrange
            var start = A.Dummy<UnitOfTime>();

            // Act
            var ex = Record.Exception(() => new ReportingPeriod<UnitOfTime>(start, start));

            // Assert
            ex.Should().BeNull();
        }

        [Fact]
        public static void Start___Should_return_same_start_passed_to_constructor___When_getting()
        {
            // Arrange
            var start = A.Dummy<CalendarQuarter>();
            var end = A.Dummy<CalendarQuarter>().ThatIs(q => q >= start);
            var systemUnderTest = new ReportingPeriod<CalendarUnitOfTime>(start, end);

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
            var systemUnderTest = new ReportingPeriod<CalendarUnitOfTime>(start, end);

            // Act
            var actualEnd = systemUnderTest.End;

            // Assert
            actualEnd.Should().BeSameAs(end);
        }

        [Fact]
        public static void EqualsOperator___Should_return_true___When_both_sides_of_operator_are_null()
        {
            // Arrange
            ReportingPeriod<UnitOfTime> systemUnderTest1 = null;
            ReportingPeriod<UnitOfTime> systemUnderTest2 = null;

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
            ReportingPeriod<UnitOfTime> systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<ReportingPeriod<UnitOfTime>>();

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
            var systemUnderTest = A.Dummy<ReportingPeriod<UnitOfTime>>();

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
            var systemUnderTest1a = A.Dummy<ReportingPeriod<CalendarDay>>();
            var systemUnderTest1b = A.Dummy<ReportingPeriod<CalendarDay>>();

            var systemUnderTest2a = A.Dummy<ReportingPeriod<FiscalQuarter>>();
            var systemUnderTest2b = new ReportingPeriod<FiscalQuarter>(systemUnderTest2a.Start, A.Dummy<FiscalQuarter>().ThatIs(q => (q != systemUnderTest2a.End) && (q >= systemUnderTest2a.Start)));

            var systemUnderTest3a = A.Dummy<ReportingPeriod<CalendarMonth>>();
            var systemUnderTest3b = new ReportingPeriod<CalendarMonth>(A.Dummy<CalendarMonth>().ThatIs(m => (m != systemUnderTest3a.Start) && (m <= systemUnderTest3a.End)), systemUnderTest3a.End);

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
            var systemUnderTest1 = A.Dummy<ReportingPeriod<UnitOfTime>>();
            var systemUnderTest2 = new ReportingPeriod<UnitOfTime>(systemUnderTest1.Start, systemUnderTest1.End);

            // Act
            var result = systemUnderTest1 == systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void EqualsOperator_with_IReportingPeriod___Should_return_true___When_both_sides_of_operator_are_null()
        {
            // Arrange
            IReportingPeriod<UnitOfTime> systemUnderTest1 = null;
            ReportingPeriod<UnitOfTime> systemUnderTest2 = null;

            // Act
            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            var result1 = systemUnderTest1 == systemUnderTest2;
            var result2 = systemUnderTest2 == systemUnderTest1;

            // Assert
            result1.Should().BeTrue();
            result2.Should().BeTrue();
            // ReSharper restore ConditionIsAlwaysTrueOrFalse
        }

        [Fact]
        public static void EqualsOperator_with_IReportingPeriod___Should_return_false___When_one_side_of_operator_is_null_and_the_other_side_is_not_null()
        {
            // Arrange
            IReportingPeriod<UnitOfTime> systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<ReportingPeriod<UnitOfTime>>();

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
        public static void EqualsOperator_with_IReportingPeriod___Should_return_true___When_same_object_is_on_both_sides_of_operator()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<ReportingPeriod<UnitOfTime>>();
            IReportingPeriod<UnitOfTime> systemUnderTest2 = systemUnderTest1;

            // Act
            // ReSharper disable EqualExpressionComparison
#pragma warning disable CS1718 // Comparison made to same variable
            var result1 = systemUnderTest1 == systemUnderTest2;
            var result2 = systemUnderTest2 == systemUnderTest1;
#pragma warning restore CS1718 // Comparison made to same variable

            // Assert
            result1.Should().BeTrue();
            result2.Should().BeTrue();
            // ReSharper restore EqualExpressionComparison
        }

        [Fact]
        public static void EqualsOperator_with_IReportingPeriod___Should_return_false___When_objects_being_compared_are_different_kinds_of_ReportingPeriod_with_same_unit_of_time()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<ReportingPeriod<CalendarDay>>();
            var systemUnderTest1b = new Common.ReportingPeriodTest<CalendarDay>(systemUnderTest1a.Start, systemUnderTest1a.End);

            var systemUnderTest2a = A.Dummy<ReportingPeriod<FiscalQuarter>>();
            var systemUnderTest2b = (IReportingPeriod<FiscalQuarter>)new Common.ReportingPeriodTest<FiscalQuarter>(systemUnderTest2a.Start, systemUnderTest2a.End);

            // Act
            var result1a = systemUnderTest1a == systemUnderTest1b;
            var result1b = systemUnderTest1b == systemUnderTest1a;

            var result2a = systemUnderTest2a == systemUnderTest2b;
            var result2b = systemUnderTest2b == systemUnderTest2a;

            // Assert
            result1a.Should().BeFalse();
            result1b.Should().BeFalse();

            result2a.Should().BeFalse();
            result2b.Should().BeFalse();
        }

        [Fact]
        public static void EqualsOperator_with_IReportingPeriod___Should_return_false___When_objects_being_compared_have_same_kind_of_unit_of_time_and_have_different_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<IReportingPeriod<CalendarDay>>();
            var systemUnderTest1b = A.Dummy<ReportingPeriod<CalendarDay>>();

            var systemUnderTest2a = A.Dummy<IReportingPeriod<FiscalQuarter>>();
            var systemUnderTest2b = new ReportingPeriod<FiscalQuarter>(systemUnderTest2a.Start, A.Dummy<FiscalQuarter>().ThatIs(q => (q != systemUnderTest2a.End) && (q >= systemUnderTest2a.Start)));

            var systemUnderTest3a = A.Dummy<IReportingPeriod<CalendarMonth>>();
            var systemUnderTest3b = new ReportingPeriod<CalendarMonth>(A.Dummy<CalendarMonth>().ThatIs(m => (m != systemUnderTest3a.Start) && (m <= systemUnderTest3a.End)), systemUnderTest3a.End);

            // Act
            var result1a = systemUnderTest1a == systemUnderTest1b;
            var result1b = systemUnderTest1b == systemUnderTest1a;

            var result2a = systemUnderTest2a == systemUnderTest2b;
            var result2b = systemUnderTest2b == systemUnderTest2a;

            var result3a = systemUnderTest3a == systemUnderTest3b;
            var result3b = systemUnderTest3b == systemUnderTest3a;

            // Assert
            result1a.Should().BeFalse();
            result1b.Should().BeFalse();

            result2a.Should().BeFalse();
            result2b.Should().BeFalse();

            result3a.Should().BeFalse();
            result3b.Should().BeFalse();
        }

        [Fact]
        public static void EqualsOperator_with_IReportingPeriod___Should_return_false___When_objects_being_compared_have_different_but_comparable_kinds_of_units_of_time_and_have_different_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<IReportingPeriod<FiscalQuarter>>();
            var systemUnderTest1b = new ReportingPeriod<FiscalUnitOfTime>(systemUnderTest1a.Start, A.Dummy<FiscalQuarter>().ThatIs(q => (q != systemUnderTest1a.End) && (q >= systemUnderTest1a.Start)));

            var systemUnderTest2a = A.Dummy<ReportingPeriod<CalendarMonth>>();
            var systemUnderTest2b = (IReportingPeriod<UnitOfTime>)new ReportingPeriod<UnitOfTime>(A.Dummy<CalendarMonth>().ThatIs(m => (m != systemUnderTest2a.Start) && (m <= systemUnderTest2a.End)), systemUnderTest2a.End);

            // Act
            var result1a = systemUnderTest1a == systemUnderTest1b;
            var result1b = systemUnderTest1b == systemUnderTest1a;

            var result2a = systemUnderTest2a == systemUnderTest2b;
            var result2b = systemUnderTest2b == systemUnderTest2a;

            // Assert
            result1a.Should().BeFalse();
            result1b.Should().BeFalse();

            result2a.Should().BeFalse();
            result2b.Should().BeFalse();
        }

        [Fact]
        public static void EqualsOperator_with_IReportingPeriod___Should_return_false___When_objects_being_compared_have_different_and_not_comparable_units_of_time()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<IReportingPeriod<FiscalQuarter>>();
            var systemUnderTest1b = new ReportingPeriod<GenericQuarter>(systemUnderTest1a.Start.ToGenericQuarter(), systemUnderTest1a.End.ToGenericQuarter());

            var systemUnderTest2a = A.Dummy<ReportingPeriod<CalendarMonth>>();
            var systemUnderTest2b = (IReportingPeriod<GenericUnitOfTime>)new ReportingPeriod<GenericUnitOfTime>(systemUnderTest2a.Start.ToGenericMonth(), systemUnderTest2a.End.ToGenericMonth());

            // Act
            var result1a = systemUnderTest1a == systemUnderTest1b;
            var result1b = systemUnderTest1b == systemUnderTest1a;

            var result2a = systemUnderTest2a == systemUnderTest2b;
            var result2b = systemUnderTest2b == systemUnderTest2a;

            // Assert
            result1a.Should().BeFalse();
            result1b.Should().BeFalse();
            result2a.Should().BeFalse();
            result2b.Should().BeFalse();
        }

        [Fact]
        public static void EqualsOperator_with_IReportingPeriod___Should_return_true___When_objects_being_compared_have_same_kind_of_unit_of_time_and_same_property_values()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<IReportingPeriod<UnitOfTime>>();
            var systemUnderTest2 = new ReportingPeriod<UnitOfTime>(systemUnderTest1.Start, systemUnderTest1.End);

            // Act
            var result1 = systemUnderTest1 == systemUnderTest2;
            var result2 = systemUnderTest2 == systemUnderTest1;

            // Assert
            result1.Should().BeTrue();
            result2.Should().BeTrue();
        }

        [Fact]
        public static void EqualsOperator_with_IReportingPeriod___Should_return_true___When_objects_being_compared_have_different_but_comparable_kinds_of_unit_of_time_and_same_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<IReportingPeriod<CalendarDay>>();
            var systemUnderTest1b = new ReportingPeriod<CalendarUnitOfTime>(systemUnderTest1a.Start, systemUnderTest1a.End);

            var systemUnderTest2a = A.Dummy<ReportingPeriod<GenericQuarter>>();
            var systemUnderTest2b = (IReportingPeriod<GenericUnitOfTime>)new ReportingPeriod<GenericQuarter>(systemUnderTest2a.Start, systemUnderTest2a.End);

            // Act
            var result1a = systemUnderTest1a == systemUnderTest1b;
            var result1b = systemUnderTest1b == systemUnderTest1a;

            var result2a = systemUnderTest2a == systemUnderTest2b;
            var result2b = systemUnderTest2b == systemUnderTest2a;

            // Assert
            result1a.Should().BeTrue();
            result1b.Should().BeTrue();
            result2a.Should().BeTrue();
            result2b.Should().BeTrue();
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_false___When_both_sides_of_operator_are_null()
        {
            // Arrange
            ReportingPeriod<UnitOfTime> systemUnderTest1 = null;
            ReportingPeriod<UnitOfTime> systemUnderTest2 = null;

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
            ReportingPeriod<UnitOfTime> systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<ReportingPeriod<UnitOfTime>>();

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
            var systemUnderTest = A.Dummy<ReportingPeriod<UnitOfTime>>();

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
            var systemUnderTest1a = A.Dummy<ReportingPeriod<CalendarDay>>();
            var systemUnderTest1b = A.Dummy<ReportingPeriod<CalendarDay>>();

            var systemUnderTest2a = A.Dummy<ReportingPeriod<FiscalQuarter>>();
            var systemUnderTest2b = new ReportingPeriod<FiscalQuarter>(systemUnderTest2a.Start, A.Dummy<FiscalQuarter>().ThatIs(q => (q != systemUnderTest2a.End) && (q >= systemUnderTest2a.Start)));

            var systemUnderTest3a = A.Dummy<ReportingPeriod<CalendarMonth>>();
            var systemUnderTest3b = new ReportingPeriod<CalendarMonth>(A.Dummy<CalendarMonth>().ThatIs(m => (m != systemUnderTest3a.Start) && (m <= systemUnderTest3a.End)), systemUnderTest3a.End);

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
            var systemUnderTest1 = A.Dummy<ReportingPeriod<UnitOfTime>>();
            var systemUnderTest2 = new ReportingPeriod<UnitOfTime>(systemUnderTest1.Start, systemUnderTest1.End);

            // Act
            var result = systemUnderTest1 != systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void NotEqualsOperator_with_IReportingPeriod___Should_return_false___When_both_sides_of_operator_are_null()
        {
            // Arrange
            IReportingPeriod<UnitOfTime> systemUnderTest1 = null;
            ReportingPeriod<UnitOfTime> systemUnderTest2 = null;

            // Act
            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            var result1 = systemUnderTest1 != systemUnderTest2;
            var result2 = systemUnderTest2 != systemUnderTest1;

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
            // ReSharper restore ConditionIsAlwaysTrueOrFalse
        }

        [Fact]
        public static void NotEqualsOperator_with_IReportingPeriod___Should_return_true___When_one_side_of_operator_is_null_and_the_other_side_is_not_null()
        {
            // Arrange
            IReportingPeriod<UnitOfTime> systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<ReportingPeriod<UnitOfTime>>();

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
        public static void NotEqualsOperator_with_IReportingPeriod___Should_return_false___When_same_object_is_on_both_sides_of_operator()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<ReportingPeriod<UnitOfTime>>();
            IReportingPeriod<UnitOfTime> systemUnderTest2 = systemUnderTest1;

            // Act
            // ReSharper disable EqualExpressionComparison
#pragma warning disable CS1718 // Comparison made to same variable
            var result1 = systemUnderTest1 != systemUnderTest2;
            var result2 = systemUnderTest2 != systemUnderTest1;
#pragma warning restore CS1718 // Comparison made to same variable

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
            // ReSharper restore EqualExpressionComparison
        }

        [Fact]
        public static void NotEqualsOperator_with_IReportingPeriod___Should_return_true___When_objects_being_compared_are_different_kinds_of_ReportingPeriod_with_same_unit_of_time()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<ReportingPeriod<CalendarDay>>();
            var systemUnderTest1b = new Common.ReportingPeriodTest<CalendarDay>(systemUnderTest1a.Start, systemUnderTest1a.End);

            var systemUnderTest2a = A.Dummy<ReportingPeriod<FiscalQuarter>>();
            var systemUnderTest2b = (IReportingPeriod<FiscalQuarter>)new Common.ReportingPeriodTest<FiscalQuarter>(systemUnderTest2a.Start, systemUnderTest2a.End);

            // Act
            var result1a = systemUnderTest1a != systemUnderTest1b;
            var result1b = systemUnderTest1b != systemUnderTest1a;

            var result2a = systemUnderTest2a != systemUnderTest2b;
            var result2b = systemUnderTest2b != systemUnderTest2a;

            // Assert
            result1a.Should().BeTrue();
            result1b.Should().BeTrue();

            result2a.Should().BeTrue();
            result2b.Should().BeTrue();
        }

        [Fact]
        public static void NotEqualsOperator_with_IReportingPeriod___Should_return_true___When_objects_being_compared_have_same_kind_of_unit_of_time_and_have_different_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<IReportingPeriod<CalendarDay>>();
            var systemUnderTest1b = A.Dummy<ReportingPeriod<CalendarDay>>();

            var systemUnderTest2a = A.Dummy<IReportingPeriod<FiscalQuarter>>();
            var systemUnderTest2b = new ReportingPeriod<FiscalQuarter>(systemUnderTest2a.Start, A.Dummy<FiscalQuarter>().ThatIs(q => (q != systemUnderTest2a.End) && (q >= systemUnderTest2a.Start)));

            var systemUnderTest3a = A.Dummy<IReportingPeriod<CalendarMonth>>();
            var systemUnderTest3b = new ReportingPeriod<CalendarMonth>(A.Dummy<CalendarMonth>().ThatIs(m => (m != systemUnderTest3a.Start) && (m <= systemUnderTest3a.End)), systemUnderTest3a.End);

            // Act
            var result1a = systemUnderTest1a != systemUnderTest1b;
            var result1b = systemUnderTest1b != systemUnderTest1a;

            var result2a = systemUnderTest2a != systemUnderTest2b;
            var result2b = systemUnderTest2b != systemUnderTest2a;

            var result3a = systemUnderTest3a != systemUnderTest3b;
            var result3b = systemUnderTest3b != systemUnderTest3a;

            // Assert
            result1a.Should().BeTrue();
            result1b.Should().BeTrue();

            result2a.Should().BeTrue();
            result2b.Should().BeTrue();

            result3a.Should().BeTrue();
            result3b.Should().BeTrue();
        }

        [Fact]
        public static void NotEqualsOperator_with_IReportingPeriod___Should_return_true___When_objects_being_compared_have_different_but_comparable_kinds_of_units_of_time_and_have_different_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<IReportingPeriod<FiscalQuarter>>();
            var systemUnderTest1b = new ReportingPeriod<FiscalUnitOfTime>(systemUnderTest1a.Start, A.Dummy<FiscalQuarter>().ThatIs(q => (q != systemUnderTest1a.End) && (q >= systemUnderTest1a.Start)));

            var systemUnderTest2a = A.Dummy<ReportingPeriod<CalendarMonth>>();
            var systemUnderTest2b = (IReportingPeriod<UnitOfTime>)new ReportingPeriod<UnitOfTime>(A.Dummy<CalendarMonth>().ThatIs(m => (m != systemUnderTest2a.Start) && (m <= systemUnderTest2a.End)), systemUnderTest2a.End);

            // Act
            var result1a = systemUnderTest1a != systemUnderTest1b;
            var result1b = systemUnderTest1b != systemUnderTest1a;

            var result2a = systemUnderTest2a != systemUnderTest2b;
            var result2b = systemUnderTest2b != systemUnderTest2a;

            // Assert
            result1a.Should().BeTrue();
            result1b.Should().BeTrue();

            result2a.Should().BeTrue();
            result2b.Should().BeTrue();
        }

        [Fact]
        public static void NotEqualsOperator_with_IReportingPeriod___Should_return_true___When_objects_being_compared_have_different_and_not_comparable_units_of_time()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<IReportingPeriod<FiscalQuarter>>();
            var systemUnderTest1b = new ReportingPeriod<GenericQuarter>(systemUnderTest1a.Start.ToGenericQuarter(), systemUnderTest1a.End.ToGenericQuarter());

            var systemUnderTest2a = A.Dummy<ReportingPeriod<CalendarMonth>>();
            var systemUnderTest2b = (IReportingPeriod<GenericUnitOfTime>)new ReportingPeriod<GenericUnitOfTime>(systemUnderTest2a.Start.ToGenericMonth(), systemUnderTest2a.End.ToGenericMonth());

            // Act
            var result1a = systemUnderTest1a != systemUnderTest1b;
            var result1b = systemUnderTest1b != systemUnderTest1a;

            var result2a = systemUnderTest2a != systemUnderTest2b;
            var result2b = systemUnderTest2b != systemUnderTest2a;

            // Assert
            result1a.Should().BeTrue();
            result1b.Should().BeTrue();
            result2a.Should().BeTrue();
            result2b.Should().BeTrue();
        }

        [Fact]
        public static void NotEqualsOperator_with_IReportingPeriod___Should_return_false___When_objects_being_compared_have_same_kind_of_unit_of_time_and_same_property_values()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<IReportingPeriod<UnitOfTime>>();
            var systemUnderTest2 = new ReportingPeriod<UnitOfTime>(systemUnderTest1.Start, systemUnderTest1.End);

            // Act
            var result1 = systemUnderTest1 != systemUnderTest2;
            var result2 = systemUnderTest2 != systemUnderTest1;

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
        }

        [Fact]
        public static void NotEqualsOperator_with_IReportingPeriod___Should_return_false___When_objects_being_compared_have_different_but_comparable_kinds_of_unit_of_time_and_same_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<IReportingPeriod<CalendarDay>>();
            var systemUnderTest1b = new ReportingPeriod<CalendarUnitOfTime>(systemUnderTest1a.Start, systemUnderTest1a.End);

            var systemUnderTest2a = A.Dummy<ReportingPeriod<GenericQuarter>>();
            var systemUnderTest2b = (IReportingPeriod<GenericUnitOfTime>)new ReportingPeriod<GenericQuarter>(systemUnderTest2a.Start, systemUnderTest2a.End);

            // Act
            var result1a = systemUnderTest1a != systemUnderTest1b;
            var result1b = systemUnderTest1b != systemUnderTest1a;

            var result2a = systemUnderTest2a != systemUnderTest2b;
            var result2b = systemUnderTest2b != systemUnderTest2a;

            // Assert
            result1a.Should().BeFalse();
            result1b.Should().BeFalse();
            result2a.Should().BeFalse();
            result2b.Should().BeFalse();
        }

        [Fact]
        public static void Equals_with_ReportingPeriod___Should_return_false___When_parameter_other_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<ReportingPeriod<UnitOfTime>>();

            // Act
            var result = systemUnderTest.Equals(null);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void Equals_with_ReportingPeriod___Should_return_true___When_parameter_other_is_same_object()
        {
            // Arrange
            var systemUnderTest = A.Dummy<ReportingPeriod<UnitOfTime>>();

            // Act
            var result = systemUnderTest.Equals(systemUnderTest);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Equals_with_ReportingPeriod___Should_return_false___When_objects_being_compared_have_different_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<ReportingPeriod<CalendarDay>>();
            var systemUnderTest1b = A.Dummy<ReportingPeriod<CalendarDay>>();

            var systemUnderTest2a = A.Dummy<ReportingPeriod<FiscalQuarter>>();
            var systemUnderTest2b = new ReportingPeriod<FiscalQuarter>(systemUnderTest2a.Start, A.Dummy<FiscalQuarter>().ThatIs(q => (q != systemUnderTest2a.End) && (q >= systemUnderTest2a.Start)));

            var systemUnderTest3a = A.Dummy<ReportingPeriod<CalendarMonth>>();
            var systemUnderTest3b = new ReportingPeriod<CalendarMonth>(A.Dummy<CalendarMonth>().ThatIs(m => (m != systemUnderTest3a.Start) && (m <= systemUnderTest3a.End)), systemUnderTest3a.End);

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
        public static void Equals_with_ReportingPeriod___Should_return_true___When_objects_being_compared_have_same_property_values()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<ReportingPeriod<UnitOfTime>>();
            var systemUnderTest2 = new ReportingPeriod<UnitOfTime>(systemUnderTest1.Start, systemUnderTest1.End);

            // Act
            var result = systemUnderTest1.Equals(systemUnderTest2);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Equals_with_IReportingPeriod___Should_return_false___When_parameter_other_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<IReportingPeriod<UnitOfTime>>();

            // Act
            var result = systemUnderTest.Equals(null);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void Equals_with_IReportingPeriod___Should_return_true___When_parameter_other_is_same_object()
        {
            // Arrange
            var systemUnderTest = A.Dummy<IReportingPeriod<UnitOfTime>>();

            // Act
            // ReSharper disable EqualExpressionComparison
            var result = systemUnderTest.Equals(systemUnderTest);
            // ReSharper restore EqualExpressionComparison

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Equals_with_IReportingPeriod___Should_return_false___When_objects_being_compared_are_different_kinds_of_ReportingPeriod_with_same_unit_of_time()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<ReportingPeriod<CalendarDay>>();
            var systemUnderTest1b = new Common.ReportingPeriodTest<CalendarDay>(systemUnderTest1a.Start, systemUnderTest1a.End);

            var systemUnderTest2a = A.Dummy<ReportingPeriod<FiscalQuarter>>();
            var systemUnderTest2b = (IReportingPeriod<FiscalQuarter>)new Common.ReportingPeriodTest<FiscalQuarter>(systemUnderTest2a.Start, systemUnderTest2a.End);

            // Act
            var result1 = systemUnderTest1a.Equals(systemUnderTest1b);
            var result2 = systemUnderTest2a.Equals(systemUnderTest2b);

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
        }

        [Fact]
        public static void Equals_with_IReportingPeriod___Should_return_false___When_objects_being_compared_have_same_kind_of_unit_of_time_and_have_different_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<IReportingPeriod<CalendarDay>>();
            var systemUnderTest1b = A.Dummy<ReportingPeriod<CalendarDay>>();

            var systemUnderTest2a = A.Dummy<IReportingPeriod<FiscalQuarter>>();
            var systemUnderTest2b = new ReportingPeriod<FiscalQuarter>(systemUnderTest2a.Start, A.Dummy<FiscalQuarter>().ThatIs(q => (q != systemUnderTest2a.End) && (q >= systemUnderTest2a.Start)));

            var systemUnderTest3a = A.Dummy<IReportingPeriod<CalendarMonth>>();
            var systemUnderTest3b = new ReportingPeriod<CalendarMonth>(A.Dummy<CalendarMonth>().ThatIs(m => (m != systemUnderTest3a.Start) && (m <= systemUnderTest3a.End)), systemUnderTest3a.End);

            // Act
            var result1 = systemUnderTest1b.Equals(systemUnderTest1a);
            var result2 = systemUnderTest2b.Equals(systemUnderTest2a);
            var result3 = systemUnderTest3b.Equals(systemUnderTest3a);

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
            result3.Should().BeFalse();
        }

        [Fact]
        public static void Equals_with_IReportingPeriod___Should_return_false___When_and_objects_being_compared_have_different_but_comparable_kinds_of_units_of_time_and_have_different_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<IReportingPeriod<FiscalQuarter>>();
            var systemUnderTest1b = new ReportingPeriod<FiscalUnitOfTime>(systemUnderTest1a.Start, A.Dummy<FiscalQuarter>().ThatIs(q => (q != systemUnderTest1a.End) && (q >= systemUnderTest1a.Start)));

            var systemUnderTest2a = A.Dummy<ReportingPeriod<CalendarMonth>>();
            var systemUnderTest2b = (IReportingPeriod<UnitOfTime>)new ReportingPeriod<UnitOfTime>(A.Dummy<CalendarMonth>().ThatIs(m => (m != systemUnderTest2a.Start) && (m <= systemUnderTest2a.End)), systemUnderTest2a.End);

            // Act
            var result1 = systemUnderTest1b.Equals(systemUnderTest1a);
            var result2 = systemUnderTest2a.Equals(systemUnderTest2b);

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
        }

        [Fact]
        public static void Equals_with_IReportingPeriod___Should_return_false___When_and_objects_being_compared_have_different_and_not_comparable_units_of_time()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<IReportingPeriod<FiscalQuarter>>();
            var systemUnderTest1b = new ReportingPeriod<GenericQuarter>(systemUnderTest1a.Start.ToGenericQuarter(), systemUnderTest1a.End.ToGenericQuarter());

            var systemUnderTest2a = A.Dummy<ReportingPeriod<CalendarMonth>>();
            var systemUnderTest2b = (IReportingPeriod<GenericUnitOfTime>)new ReportingPeriod<GenericUnitOfTime>(systemUnderTest2a.Start.ToGenericMonth(), systemUnderTest2a.End.ToGenericMonth());

            // Act
            var result1 = systemUnderTest1b.Equals(systemUnderTest1a);
            var result2 = systemUnderTest2a.Equals(systemUnderTest2b);

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
        }

        [Fact]
        public static void Equals_with_IReportingPeriod___Should_return_true___When_objects_being_compared_have_same_kind_of_unit_of_time_and_same_property_values()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<IReportingPeriod<UnitOfTime>>();
            var systemUnderTest2 = new ReportingPeriod<UnitOfTime>(systemUnderTest1.Start, systemUnderTest1.End);

            // Act
            var result = systemUnderTest2.Equals(systemUnderTest1);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Equals_with_IReportingPeriod___Should_return_true___When_objects_being_compared_have_different_but_comparable_kinds_of_unit_of_time_and_same_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<IReportingPeriod<CalendarDay>>();
            var systemUnderTest1b = new ReportingPeriod<CalendarUnitOfTime>(systemUnderTest1a.Start, systemUnderTest1a.End);

            var systemUnderTest2a = A.Dummy<ReportingPeriod<GenericQuarter>>();
            var systemUnderTest2b = (IReportingPeriod<GenericUnitOfTime>)new ReportingPeriod<GenericQuarter>(systemUnderTest2a.Start, systemUnderTest2a.End);

            // Act
            var result1 = systemUnderTest1b.Equals(systemUnderTest1a);
            var result2 = systemUnderTest2a.Equals(systemUnderTest2b);

            // Assert
            result1.Should().BeTrue();
            result2.Should().BeTrue();
        }

        [Fact]
        public static void Equals_with_object___Should_return_false___When_parameter_other_is_null()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<ReportingPeriod<UnitOfTime>>();
            var systemUnderTest2 = A.Dummy<IReportingPeriod<UnitOfTime>>();

            // Act
            var result1 = systemUnderTest1.Equals(null);
            var result2 = systemUnderTest2.Equals(null);

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
        }

        [Fact]
        public static void Equals_with_object___Should_return_false___When_parameter_other_is_not_of_the_same_type()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<ReportingPeriod<UnitOfTime>>();
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
        public static void Equals_with_object___Should_return_true___When_parameter_other_is_same_object()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<ReportingPeriod<UnitOfTime>>();

            var systemUnderTest2a = A.Dummy<ReportingPeriod<UnitOfTime>>();
            IReportingPeriod<UnitOfTime> systemUnderTest2b = systemUnderTest2a;

            // Act
            var result1 = systemUnderTest1.Equals((object)systemUnderTest1);

            // ReSharper disable RedundantCast
            var result2a = systemUnderTest2a.Equals((object)systemUnderTest2b);
            var result2b = systemUnderTest2b.Equals((object)systemUnderTest2a);
            // ReSharper restore RedundantCast

            // Assert
            result1.Should().BeTrue();

            result2a.Should().BeTrue();
            result2b.Should().BeTrue();
        }

        [Fact]
        public static void Equals_with_object___Should_return_false___When_objects_being_compared_are_different_kinds_of_ReportingPeriod_with_same_unit_of_time()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<ReportingPeriod<CalendarDay>>();
            var systemUnderTest1b = new Common.ReportingPeriodTest<CalendarDay>(systemUnderTest1a.Start, systemUnderTest1a.End);

            var systemUnderTest2a = A.Dummy<ReportingPeriod<FiscalQuarter>>();
            var systemUnderTest2b = (IReportingPeriod<FiscalQuarter>)new Common.ReportingPeriodTest<FiscalQuarter>(systemUnderTest2a.Start, systemUnderTest2a.End);

            // Act
            // ReSharper disable SuspiciousTypeConversion.Global
            var result1 = systemUnderTest1a.Equals((object)systemUnderTest1b);
            var result2 = systemUnderTest2a.Equals((object)systemUnderTest2b);
            // ReSharper restore SuspiciousTypeConversion.Global

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
        }

        [Fact]
        public static void Equals_with_object___Should_return_false___When_objects_being_compared_have_the_same_kind_of_unit_of_time_but_different_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<ReportingPeriod<CalendarDay>>();
            var systemUnderTest1b = A.Dummy<ReportingPeriod<CalendarDay>>();

            var systemUnderTest2a = A.Dummy<ReportingPeriod<FiscalQuarter>>();
            var systemUnderTest2b = new ReportingPeriod<FiscalQuarter>(systemUnderTest2a.Start, A.Dummy<FiscalQuarter>().ThatIs(q => (q != systemUnderTest2a.End) && (q >= systemUnderTest2a.Start)));

            var systemUnderTest3a = A.Dummy<ReportingPeriod<CalendarMonth>>();
            var systemUnderTest3b = new ReportingPeriod<CalendarMonth>(A.Dummy<CalendarMonth>().ThatIs(m => (m != systemUnderTest3a.Start) && (m <= systemUnderTest3a.End)), systemUnderTest3a.End);

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
        public static void Equals_with_object___Should_return_false___When_objects_being_compared_have_different_but_comparable_kinds_of_unit_of_time_and_different_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<IReportingPeriod<FiscalQuarter>>();
            var systemUnderTest1b = new ReportingPeriod<FiscalUnitOfTime>(systemUnderTest1a.Start, A.Dummy<FiscalQuarter>().ThatIs(q => (q != systemUnderTest1a.End) && (q >= systemUnderTest1a.Start)));

            var systemUnderTest2a = A.Dummy<ReportingPeriod<CalendarMonth>>();
            var systemUnderTest2b = (IReportingPeriod<UnitOfTime>)new ReportingPeriod<UnitOfTime>(A.Dummy<CalendarMonth>().ThatIs(m => (m != systemUnderTest2a.Start) && (m <= systemUnderTest2a.End)), systemUnderTest2a.End);

            // Act
            // ReSharper disable SuspiciousTypeConversion.Global
            var result1 = systemUnderTest1b.Equals((object)systemUnderTest1a);
            var result2 = systemUnderTest2a.Equals((object)systemUnderTest2b);
            // ReSharper restore SuspiciousTypeConversion.Global

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
        }

        [Fact]
        public static void Equals_with_object___Should_return_false___When_objects_being_compared_have_different_and_not_comparable_units_of_time()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<IReportingPeriod<FiscalQuarter>>();
            var systemUnderTest1b = new ReportingPeriod<GenericQuarter>(systemUnderTest1a.Start.ToGenericQuarter(), systemUnderTest1a.End.ToGenericQuarter());

            var systemUnderTest2a = A.Dummy<ReportingPeriod<CalendarMonth>>();
            var systemUnderTest2b = (IReportingPeriod<GenericUnitOfTime>)new ReportingPeriod<GenericUnitOfTime>(systemUnderTest2a.Start.ToGenericMonth(), systemUnderTest2a.End.ToGenericMonth());

            // Act
            // ReSharper disable SuspiciousTypeConversion.Global
            var result1 = systemUnderTest1b.Equals((object)systemUnderTest1a);
            var result2 = systemUnderTest2a.Equals((object)systemUnderTest2b);
            // ReSharper restore SuspiciousTypeConversion.Global

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
        }

        [Fact]
        public static void Equals_with_object___Should_return_true___When_objects_being_compared_have_the_same_kind_of_unit_of_time_and_same_property_values()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<ReportingPeriod<UnitOfTime>>();
            var systemUnderTest2 = new ReportingPeriod<UnitOfTime>(systemUnderTest1.Start, systemUnderTest1.End);

            // Act
            var result = systemUnderTest1.Equals((object)systemUnderTest2);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Equals_with_object___Should_return_true___When_When_objects_being_compared_have_different_but_comparable_kinds_of_unit_of_time_and_same_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<IReportingPeriod<CalendarDay>>();
            var systemUnderTest1b = new ReportingPeriod<CalendarUnitOfTime>(systemUnderTest1a.Start, systemUnderTest1a.End);

            var systemUnderTest2a = A.Dummy<ReportingPeriod<GenericQuarter>>();
            var systemUnderTest2b = (IReportingPeriod<GenericUnitOfTime>)new ReportingPeriod<GenericQuarter>(systemUnderTest2a.Start, systemUnderTest2a.End);

            // Act
            // ReSharper disable SuspiciousTypeConversion.Global
            var result1 = systemUnderTest1b.Equals((object)systemUnderTest1a);
            var result2 = systemUnderTest2a.Equals((object)systemUnderTest2b);
            // ReSharper restore SuspiciousTypeConversion.Global

            // Assert
            result1.Should().BeTrue();
            result2.Should().BeTrue();
        }

        [Fact]
        public static void GetHashCode___Should_not_be_equal_for_two_ReportingPeriod___When_both_objects_have_different_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<ReportingPeriod<CalendarDay>>();
            var systemUnderTest1b = A.Dummy<ReportingPeriod<CalendarDay>>();

            var systemUnderTest2a = A.Dummy<ReportingPeriod<FiscalQuarter>>();
            var systemUnderTest2b = new ReportingPeriod<FiscalQuarter>(systemUnderTest2a.Start, A.Dummy<FiscalQuarter>().ThatIs(q => (q != systemUnderTest2a.End) && (q >= systemUnderTest2a.Start)));

            var systemUnderTest3a = A.Dummy<ReportingPeriod<CalendarMonth>>();
            var systemUnderTest3b = new ReportingPeriod<CalendarMonth>(A.Dummy<CalendarMonth>().ThatIs(m => (m != systemUnderTest3a.Start) && (m <= systemUnderTest3a.End)), systemUnderTest3a.End);

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
        public static void GetHashCode___Should_be_equal_for_two_ReportingPeriod___When_both_objects_have_the_same_property_values()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<ReportingPeriod<UnitOfTime>>();
            var systemUnderTest2 = new ReportingPeriod<UnitOfTime>(systemUnderTest1.Start, systemUnderTest1.End);

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

            var systemUnderTest1 = new ReportingPeriod<CalendarDay>(start, end);
            var systemUnderTest2 = new ReportingPeriod<UnitOfTime>(start, end);

            // Act
            var toString1 = systemUnderTest1.ToString();
            var toString2 = systemUnderTest2.ToString();

            // Assert
            toString1.Should().Be("2017-11-30 to 2018-03-24");
            toString2.Should().Be("2017-11-30 to 2018-03-24");
        }

        [Fact]
        public static void Clone___Should_return_deep_clone_of_reporting_period___When_called()
        {
            // Arrange
            var reportingPeriods = new List<IReportingPeriod<UnitOfTime>>
            {
                A.Dummy<ReportingPeriod<UnitOfTime>>(),
                A.Dummy<ReportingPeriod<CalendarUnitOfTime>>(),
                A.Dummy<ReportingPeriod<FiscalUnitOfTime>>(),
                A.Dummy<ReportingPeriod<GenericUnitOfTime>>(),
                A.Dummy<ReportingPeriod<CalendarDay>>(),
                A.Dummy<ReportingPeriod<CalendarMonth>>(),
                A.Dummy<ReportingPeriod<CalendarQuarter>>(),
                A.Dummy<ReportingPeriod<CalendarYear>>(),
                A.Dummy<ReportingPeriod<FiscalMonth>>(),
                A.Dummy<ReportingPeriod<FiscalQuarter>>(),
                A.Dummy<ReportingPeriod<FiscalYear>>(),
                A.Dummy<ReportingPeriod<GenericMonth>>(),
                A.Dummy<ReportingPeriod<GenericQuarter>>(),
                A.Dummy<ReportingPeriod<GenericYear>>(),
            };

            // Act
            var clones = reportingPeriods.Select(_ => new { Original = _, Clone = _.Clone() }).ToList();

            // Assert
            clones.ForEach(_ => _.Clone.Should().Be(_.Original));
            clones.ForEach(_ => _.Clone.Should().BeOfType(_.Original.GetType()));
            clones.ForEach(_ => _.Clone.Should().NotBeSameAs(_.Original));
            clones.ForEach(_ => _.Clone.Start.Should().NotBeSameAs(_.Original.Start));
            clones.ForEach(_ => _.Clone.End.Should().NotBeSameAs(_.Original.End));
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Thoroughly checking this test-case requires lots of types.")]
        public static void Clone_with_type_parameter___Should_throw_InvalidOperationException___When_the_cloned_reporting_period_cannot_be_assigned_to_the_return_type()
        {
            // Arrange
            var allTypes = new[]
            {
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<UnitOfTime>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<UnitOfTime>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<CalendarUnitOfTime>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<CalendarUnitOfTime>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<FiscalUnitOfTime>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<FiscalUnitOfTime>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<GenericUnitOfTime>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<GenericUnitOfTime>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<CalendarDay>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<CalendarDay>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<CalendarMonth>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<CalendarMonth>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<CalendarQuarter>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<CalendarQuarter>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<CalendarYear>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<CalendarYear>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<FiscalMonth>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<FiscalMonth>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<FiscalQuarter>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<FiscalQuarter>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<FiscalYear>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<FiscalYear>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<GenericMonth>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<GenericMonth>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<GenericQuarter>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<GenericQuarter>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<GenericYear>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<GenericYear>) }
            };

            var reportingPeriods = new List<IReportingPeriod<UnitOfTime>>
            {
                A.Dummy<ReportingPeriod<UnitOfTime>>(),
                A.Dummy<ReportingPeriod<CalendarUnitOfTime>>(),
                A.Dummy<ReportingPeriod<FiscalUnitOfTime>>(),
                A.Dummy<ReportingPeriod<GenericUnitOfTime>>(),
                A.Dummy<ReportingPeriod<CalendarDay>>(),
                A.Dummy<ReportingPeriod<CalendarMonth>>(),
                A.Dummy<ReportingPeriod<CalendarQuarter>>(),
                A.Dummy<ReportingPeriod<CalendarYear>>(),
                A.Dummy<ReportingPeriod<FiscalMonth>>(),
                A.Dummy<ReportingPeriod<FiscalQuarter>>(),
                A.Dummy<ReportingPeriod<FiscalYear>>(),
                A.Dummy<ReportingPeriod<GenericMonth>>(),
                A.Dummy<ReportingPeriod<GenericQuarter>>(),
                A.Dummy<ReportingPeriod<GenericYear>>(),
            };

            // Act
            var exceptions = new List<Exception>();
            foreach (var reportingPeriod in reportingPeriods)
            {
                foreach (var type in allTypes)
                {
                    var cloneMethod = reportingPeriod.GetType().GetMethods().Single(_ => (_.Name == nameof(ReportingPeriod<UnitOfTime>.Clone)) && _.IsGenericMethod);
                    var genericMethod = cloneMethod.MakeGenericMethod(type.ReportingPeriodType);
                    // ReSharper disable PossibleNullReferenceException
                    exceptions.Add(Record.Exception(() => genericMethod.Invoke(reportingPeriod, null)).InnerException);
                    // ReSharper restore PossibleNullReferenceException
                }
            }

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Thoroughly checking this test-case requires lots of types.")]
        public static void Clone_with_type_parameter___Should_throw_InvalidOperationException___When_the_kind_of_unit_of_times_cloned_cannot_be_assigned_to_the_return_types_unit_of_time()
        {
            // Arrange
            var reportingPeriods = new List<Tuple<IReportingPeriod<UnitOfTime>, Type>>
            {
                { A.Dummy<ReportingPeriod<CalendarUnitOfTime>>(), typeof(ReportingPeriod<FiscalUnitOfTime>) },
                { A.Dummy<ReportingPeriod<FiscalUnitOfTime>>(), typeof(ReportingPeriod<GenericUnitOfTime>) },
                { A.Dummy<ReportingPeriod<GenericUnitOfTime>>(), typeof(ReportingPeriod<CalendarUnitOfTime>) },
                { A.Dummy<ReportingPeriod<CalendarDay>>(), typeof(ReportingPeriod<FiscalMonth>) },
                { A.Dummy<ReportingPeriod<CalendarMonth>>(), typeof(ReportingPeriod<CalendarDay>) },
                { A.Dummy<ReportingPeriod<CalendarQuarter>>(), typeof(ReportingPeriod<FiscalQuarter>) },
                { A.Dummy<ReportingPeriod<CalendarYear>>(), typeof(ReportingPeriod<GenericQuarter>) },
                { A.Dummy<ReportingPeriod<FiscalMonth>>(), typeof(ReportingPeriod<CalendarMonth>) },
                { A.Dummy<ReportingPeriod<FiscalQuarter>>(), typeof(ReportingPeriod<FiscalMonth>) },
                { A.Dummy<ReportingPeriod<FiscalYear>>(), typeof(ReportingPeriod<FiscalQuarter>) },
                { A.Dummy<ReportingPeriod<GenericMonth>>(), typeof(ReportingPeriod<GenericYear>) },
                { A.Dummy<ReportingPeriod<GenericQuarter>>(), typeof(ReportingPeriod<GenericYear>) },
                { A.Dummy<ReportingPeriod<GenericYear>>(), typeof(ReportingPeriod<FiscalYear>) }
            };

            // Act
            var exceptions = new List<Exception>();
            foreach (var reportingPeriod in reportingPeriods)
            {
                var cloneMethod = reportingPeriod.Item1.GetType().GetMethods().Single(_ => (_.Name == nameof(ReportingPeriod<UnitOfTime>.Clone)) && _.IsGenericMethod);
                var genericMethod = cloneMethod.MakeGenericMethod(reportingPeriod.Item2);
                // ReSharper disable PossibleNullReferenceException
                exceptions.Add(Record.Exception(() => genericMethod.Invoke(reportingPeriod.Item1, null)).InnerException);
                // ReSharper restore PossibleNullReferenceException
            }

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void Clone_with_type_parameter___Should_deep_clone_into_various_flavors_of_IReportingPeriod___When_reporting_period_is_a_CalendarDay()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod<CalendarDay>>();

            // Act
            var deserialized = new List<IReportingPeriod<UnitOfTime>>
            {
                reportingPeriod.Clone<IReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<CalendarUnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<CalendarUnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<CalendarDay>>(),
                reportingPeriod.Clone<ReportingPeriod<CalendarDay>>(),
            };

            // Assert
            deserialized.ForEach(_ =>
            {
                _.Should().Be(reportingPeriod).And.NotBeSameAs(reportingPeriod);
                _.Start.Should().NotBeSameAs(reportingPeriod.Start);
                _.End.Should().NotBeSameAs(reportingPeriod.End);
            });
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void Clone_with_type_parameter___Should_deep_clone_into_various_flavors_of_IReportingPeriod___When_reporting_period_is_a_CalendarMonth()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod<CalendarMonth>>();

            // Act
            var deserialized = new List<IReportingPeriod<UnitOfTime>>
            {
                reportingPeriod.Clone<IReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<CalendarUnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<CalendarUnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<CalendarMonth>>(),
                reportingPeriod.Clone<ReportingPeriod<CalendarMonth>>()
            };

            // Assert
            deserialized.ForEach(_ =>
            {
                _.Should().Be(reportingPeriod).And.NotBeSameAs(reportingPeriod);
                _.Start.Should().NotBeSameAs(reportingPeriod.Start);
                _.End.Should().NotBeSameAs(reportingPeriod.End);
            });
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void Clone_with_type_parameter___Should_deep_clone_into_various_flavors_of_IReportingPeriod___When_reporting_period_is_a_CalendarQuarter()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod<CalendarQuarter>>();

            // Act
            var deserialized = new List<IReportingPeriod<UnitOfTime>>
            {
                reportingPeriod.Clone<IReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<CalendarUnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<CalendarUnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<CalendarQuarter>>(),
                reportingPeriod.Clone<ReportingPeriod<CalendarQuarter>>()
            };

            // Assert
            deserialized.ForEach(_ =>
            {
                _.Should().Be(reportingPeriod).And.NotBeSameAs(reportingPeriod);
                _.Start.Should().NotBeSameAs(reportingPeriod.Start);
                _.End.Should().NotBeSameAs(reportingPeriod.End);
            });
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void Clone_with_type_parameter___Should_deep_clone_into_various_flavors_of_IReportingPeriod___When_reporting_period_is_a_CalendarYear()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod<CalendarYear>>();

            // Act
            var deserialized = new List<IReportingPeriod<UnitOfTime>>
            {
                reportingPeriod.Clone<IReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<CalendarUnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<CalendarUnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<CalendarYear>>(),
                reportingPeriod.Clone<ReportingPeriod<CalendarYear>>()
            };

            // Assert
            deserialized.ForEach(_ =>
            {
                _.Should().Be(reportingPeriod).And.NotBeSameAs(reportingPeriod);
                _.Start.Should().NotBeSameAs(reportingPeriod.Start);
                _.End.Should().NotBeSameAs(reportingPeriod.End);
            });
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void Clone_with_type_parameter___Should_deep_clone_into_various_flavors_of_IReportingPeriod___When_reporting_period_is_a_FiscalMonth()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod<FiscalMonth>>();

            // Act
            var deserialized = new List<IReportingPeriod<UnitOfTime>>
            {
                reportingPeriod.Clone<IReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<FiscalUnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<FiscalUnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<FiscalMonth>>(),
                reportingPeriod.Clone<ReportingPeriod<FiscalMonth>>(),
            };

            // Assert
            deserialized.ForEach(_ =>
            {
                _.Should().Be(reportingPeriod).And.NotBeSameAs(reportingPeriod);
                _.Start.Should().NotBeSameAs(reportingPeriod.Start);
                _.End.Should().NotBeSameAs(reportingPeriod.End);
            });
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void Clone_with_type_parameter___Should_deep_clone_into_various_flavors_of_IReportingPeriod___When_reporting_period_is_a_FiscalQuarter()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod<FiscalQuarter>>();

            // Act
            var deserialized = new List<IReportingPeriod<UnitOfTime>>
            {
                reportingPeriod.Clone<IReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<FiscalUnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<FiscalUnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<FiscalQuarter>>(),
                reportingPeriod.Clone<ReportingPeriod<FiscalQuarter>>()
            };

            // Assert
            deserialized.ForEach(_ =>
            {
                _.Should().Be(reportingPeriod).And.NotBeSameAs(reportingPeriod);
                _.Start.Should().NotBeSameAs(reportingPeriod.Start);
                _.End.Should().NotBeSameAs(reportingPeriod.End);
            });
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void Clone_with_type_parameter___Should_deep_clone_into_various_flavors_of_IReportingPeriod___When_reporting_period_is_a_FiscalYear()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod<FiscalYear>>();

            // Act
            var deserialized = new List<IReportingPeriod<UnitOfTime>>
            {
                reportingPeriod.Clone<IReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<FiscalUnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<FiscalUnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<FiscalYear>>(),
                reportingPeriod.Clone<ReportingPeriod<FiscalYear>>()
            };

            // Assert
            deserialized.ForEach(_ =>
            {
                _.Should().Be(reportingPeriod).And.NotBeSameAs(reportingPeriod);
                _.Start.Should().NotBeSameAs(reportingPeriod.Start);
                _.End.Should().NotBeSameAs(reportingPeriod.End);
            });
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void Clone_with_type_parameter___Should_deep_clone_into_various_flavors_of_IReportingPeriod___When_reporting_period_is_a_GenericMonth()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod<GenericMonth>>();

            // Act
            var deserialized = new List<IReportingPeriod<UnitOfTime>>
            {
                reportingPeriod.Clone<IReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<GenericUnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<GenericUnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<GenericMonth>>(),
                reportingPeriod.Clone<ReportingPeriod<GenericMonth>>()
            };

            // Assert
            deserialized.ForEach(_ =>
            {
                _.Should().Be(reportingPeriod).And.NotBeSameAs(reportingPeriod);
                _.Start.Should().NotBeSameAs(reportingPeriod.Start);
                _.End.Should().NotBeSameAs(reportingPeriod.End);
            });
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void Clone_with_type_parameter___Should_deep_clone_into_various_flavors_of_IReportingPeriod___When_reporting_period_is_a_GenericQuarter()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod<GenericQuarter>>();

            // Act
            var deserialized = new List<IReportingPeriod<UnitOfTime>>
            {
                reportingPeriod.Clone<IReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<GenericUnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<GenericUnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<GenericQuarter>>(),
                reportingPeriod.Clone<ReportingPeriod<GenericQuarter>>()
            };

            // Assert
            deserialized.ForEach(_ =>
            {
                _.Should().Be(reportingPeriod).And.NotBeSameAs(reportingPeriod);
                _.Start.Should().NotBeSameAs(reportingPeriod.Start);
                _.End.Should().NotBeSameAs(reportingPeriod.End);
            });
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void Clone_with_type_parameter___Should_deep_clone_into_various_flavors_of_IReportingPeriod___When_reporting_period_is_a_GenericYear()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod<GenericYear>>();

            // Act
            var deserialized = new List<IReportingPeriod<UnitOfTime>>
            {
                reportingPeriod.Clone<IReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<GenericUnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<GenericUnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<GenericYear>>(),
                reportingPeriod.Clone<ReportingPeriod<GenericYear>>()
            };

            // Assert
            deserialized.ForEach(_ =>
            {
                _.Should().Be(reportingPeriod).And.NotBeSameAs(reportingPeriod);
                _.Start.Should().NotBeSameAs(reportingPeriod.Start);
                _.End.Should().NotBeSameAs(reportingPeriod.End);
            });
        }

        // ReSharper restore InconsistentNaming
    }
}

// ReSharper restore CheckNamespace