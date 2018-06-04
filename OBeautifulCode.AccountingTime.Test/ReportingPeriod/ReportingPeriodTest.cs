// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriodTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using FakeItEasy;

    using FluentAssertions;

    using Naos.Recipes.TupleInitializers;

    using OBeautifulCode.AutoFakeItEasy;

    using Xunit;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "There are a lot of unit-of-time types.")]
    public static class ReportingPeriodTest
    {
        [Fact]
        public static void Constructor___Should_throw_ArgumentNullException___When_parameter_start_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => new ReportingPeriod<UnitOfTime>(null, A.Dummy<UnitOfTime>()));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void Constructor___Should_throw_ArgumentNullException___When_parameter_end_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => new ReportingPeriod<UnitOfTime>(A.Dummy<UnitOfTime>(), null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void Constructor___Should_throw_ArgumentException___When_parameters_start_and_end_bounded_and_not_of_the_same_concrete_type()
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
        public static void Constructor___Should_throw_ArgumentOutOfRangeException___When_parameters_start_and_end_are_bounded_and_start_is_greater_than_parameter_end()
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
        public static void Constructor___Should_throw_ArgumentException___When_parameters_start_and_or_end_is_unbounded_and_not_the_same_kind_of_unit_of_time()
        {
            // Arrange
            var start1 = A.Dummy<FiscalUnbounded>();
            var end1 = A.Dummy<CalendarYear>();

            var start2 = A.Dummy<FiscalMonth>();
            var end2 = A.Dummy<GenericUnbounded>();

            var start3 = A.Dummy<FiscalUnbounded>();
            var end3 = A.Dummy<GenericUnbounded>();

            var start4 = A.Dummy<GenericUnbounded>();
            var end4 = A.Dummy<CalendarUnbounded>();

            // Act
            var ex1 = Record.Exception(() => new ReportingPeriod<UnitOfTime>(start1, end1));
            var ex2 = Record.Exception(() => new ReportingPeriod<UnitOfTime>(start2, end2));
            var ex3 = Record.Exception(() => new ReportingPeriod<UnitOfTime>(start3, end3));
            var ex4 = Record.Exception(() => new ReportingPeriod<UnitOfTime>(start4, end4));

            // Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex2.Should().BeOfType<ArgumentException>();
            ex3.Should().BeOfType<ArgumentException>();
            ex4.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void Constructor___Should_not_throw___When_parameters_start_and_end_are_bounded_and_start_is_equal_to_parameter_end()
        {
            // Arrange
            var start = (UnitOfTime)A.Dummy<IAmBoundedTime>();

            // Act
            var ex = Record.Exception(() => new ReportingPeriod<UnitOfTime>(start, start));

            // Assert
            ex.Should().BeNull();
        }

        [Fact]
        public static void Constructor___Should_not_throw___When_parameters_start_and_or_end_are_unbounded_and_are_the_same_kind_of_unit_of_time()
        {
            // Arrange
            var start1 = A.Dummy<CalendarUnbounded>();
            var end1 = A.Dummy<CalendarMonth>();

            var start2 = A.Dummy<FiscalYear>();
            var end2 = A.Dummy<FiscalUnbounded>();

            var start3 = A.Dummy<GenericUnbounded>();
            var end3 = A.Dummy<GenericUnbounded>();

            // Act
            var ex1 = Record.Exception(() => new ReportingPeriod<CalendarUnitOfTime>(start1, end1));
            var ex2 = Record.Exception(() => new ReportingPeriod<FiscalUnitOfTime>(start2, end2));
            var ex3 = Record.Exception(() => new ReportingPeriod<GenericUnitOfTime>(start3, end3));

            // Assert
            ex1.Should().BeNull();
            ex2.Should().BeNull();
            ex3.Should().BeNull();
        }

        [Fact]
        public static void Start___Should_return_same_start_passed_to_constructor___When_getting()
        {
            // Arrange
            var start1 = A.Dummy<CalendarQuarter>();
            var end1 = A.Dummy<CalendarQuarter>().ThatIs(q => q >= start1);

            var start2 = A.Dummy<FiscalUnbounded>();
            var end2 = A.Dummy<FiscalMonth>();

            var start3 = A.Dummy<GenericUnbounded>();
            var end3 = A.Dummy<GenericUnbounded>();

            var start4 = A.Dummy<CalendarDay>();
            var end4 = A.Dummy<CalendarUnbounded>();

            var systemUnderTest1 = new ReportingPeriod<CalendarUnitOfTime>(start1, end1);
            var systemUnderTest2 = new ReportingPeriod<FiscalUnitOfTime>(start2, end2);
            var systemUnderTest3 = new ReportingPeriod<GenericUnitOfTime>(start3, end3);
            var systemUnderTest4 = new ReportingPeriod<CalendarUnitOfTime>(start4, end4);

            // Act
            var actualStart1 = systemUnderTest1.Start;
            var actualStart2 = systemUnderTest2.Start;
            var actualStart3 = systemUnderTest3.Start;
            var actualStart4 = systemUnderTest4.Start;

            // Assert
            actualStart1.Should().BeSameAs(start1);
            actualStart2.Should().BeSameAs(start2);
            actualStart3.Should().BeSameAs(start3);
            actualStart4.Should().BeSameAs(start4);
        }

        [Fact]
        public static void End___Should_return_same_end_passed_to_constructor___When_getting()
        {
            // Arrange
            var start1 = A.Dummy<CalendarQuarter>();
            var end1 = A.Dummy<CalendarQuarter>().ThatIs(q => q >= start1);

            var start2 = A.Dummy<FiscalUnbounded>();
            var end2 = A.Dummy<FiscalMonth>();

            var start3 = A.Dummy<GenericUnbounded>();
            var end3 = A.Dummy<GenericUnbounded>();

            var start4 = A.Dummy<CalendarDay>();
            var end4 = A.Dummy<CalendarUnbounded>();

            var systemUnderTest1 = new ReportingPeriod<CalendarUnitOfTime>(start1, end1);
            var systemUnderTest2 = new ReportingPeriod<FiscalUnitOfTime>(start2, end2);
            var systemUnderTest3 = new ReportingPeriod<GenericUnitOfTime>(start3, end3);
            var systemUnderTest4 = new ReportingPeriod<CalendarUnitOfTime>(start4, end4);

            // Act
            var actualEnd1 = systemUnderTest1.End;
            var actualEnd2 = systemUnderTest2.End;
            var actualEnd3 = systemUnderTest3.End;
            var actualEnd4 = systemUnderTest4.End;

            // Assert
            actualEnd1.Should().BeSameAs(end1);
            actualEnd2.Should().BeSameAs(end2);
            actualEnd3.Should().BeSameAs(end3);
            actualEnd4.Should().BeSameAs(end4);
        }

        [Fact]
        public static void EqualsOperator___Should_return_true___When_both_sides_of_operator_are_null()
        {
            // Arrange
            ReportingPeriod<UnitOfTime> systemUnderTest1 = null;
            ReportingPeriod<UnitOfTime> systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 == systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void EqualsOperator___Should_return_false___When_one_side_of_operator_is_null_and_the_other_side_is_not_null()
        {
            // Arrange
            ReportingPeriod<UnitOfTime> systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<ReportingPeriod<UnitOfTime>>();

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
            var systemUnderTest = A.Dummy<ReportingPeriod<UnitOfTime>>();

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
            var systemUnderTest1a = A.Dummy<ReportingPeriod<CalendarDay>>();
            var systemUnderTest1b = A.Dummy<ReportingPeriod<CalendarDay>>();

            var systemUnderTest2a = A.Dummy<ReportingPeriod<FiscalQuarter>>();
            var systemUnderTest2b = new ReportingPeriod<FiscalQuarter>(systemUnderTest2a.Start, A.Dummy<FiscalQuarter>().ThatIs(q => (q != systemUnderTest2a.End) && (q >= systemUnderTest2a.Start)));

            var systemUnderTest3a = A.Dummy<ReportingPeriod<CalendarMonth>>();
            var systemUnderTest3b = new ReportingPeriod<CalendarMonth>(A.Dummy<CalendarMonth>().ThatIs(m => (m != systemUnderTest3a.Start) && (m <= systemUnderTest3a.End)), systemUnderTest3a.End);

            var systemUnderTest4a = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarUnbounded>(), A.Dummy<CalendarDay>());
            var systemUnderTest4b = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarUnbounded>(), A.Dummy<CalendarDay>());

            var systemUnderTest5a = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarDay>(), A.Dummy<CalendarUnbounded>());
            var systemUnderTest5b = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarDay>(), A.Dummy<CalendarUnbounded>());

            // Act
            var result1 = systemUnderTest1a == systemUnderTest1b;
            var result2 = systemUnderTest2a == systemUnderTest2b;
            var result3 = systemUnderTest3a == systemUnderTest3b;
            var result4 = systemUnderTest4a == systemUnderTest4b;
            var result5 = systemUnderTest5a == systemUnderTest5b;

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
            result3.Should().BeFalse();
            result4.Should().BeFalse();
            result5.Should().BeFalse();
        }

        [Fact]
        public static void EqualsOperator___Should_return_true___When_objects_being_compared_have_same_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<ReportingPeriod<UnitOfTime>>();
            var systemUnderTest1b = new ReportingPeriod<UnitOfTime>(systemUnderTest1a.Start, systemUnderTest1a.End);

            var systemUnderTest2a = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarDay>(), A.Dummy<CalendarUnbounded>());
            var systemUnderTest2b = new ReportingPeriod<CalendarUnitOfTime>(systemUnderTest2a.Start, A.Dummy<CalendarUnbounded>());

            var systemUnderTest3a = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarUnbounded>(), A.Dummy<CalendarDay>());
            var systemUnderTest3b = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarUnbounded>(), systemUnderTest3a.End);

            var systemUnderTest4a = new ReportingPeriod<UnitOfTime>(A.Dummy<FiscalUnbounded>(), A.Dummy<FiscalUnbounded>());
            var systemUnderTest4b = new ReportingPeriod<UnitOfTime>(A.Dummy<FiscalUnbounded>(), A.Dummy<FiscalUnbounded>());

            // Act
            var result1 = systemUnderTest1a == systemUnderTest1b;
            var result2 = systemUnderTest2a == systemUnderTest2b;
            var result3 = systemUnderTest3a == systemUnderTest3b;
            var result4 = systemUnderTest4a == systemUnderTest4b;

            // Assert
            result1.Should().BeTrue();
            result2.Should().BeTrue();
            result3.Should().BeTrue();
            result4.Should().BeTrue();
        }

        [Fact]
        public static void EqualsOperator_with_IReportingPeriod___Should_return_true___When_both_sides_of_operator_are_null()
        {
            // Arrange
            IReportingPeriod<UnitOfTime> systemUnderTest1 = null;
            ReportingPeriod<UnitOfTime> systemUnderTest2 = null;

            // Act
            var result1 = systemUnderTest1 == systemUnderTest2;
            var result2 = systemUnderTest2 == systemUnderTest1;

            // Assert
            result1.Should().BeTrue();
            result2.Should().BeTrue();
        }

        [Fact]
        public static void EqualsOperator_with_IReportingPeriod___Should_return_false___When_one_side_of_operator_is_null_and_the_other_side_is_not_null()
        {
            // Arrange
            IReportingPeriod<UnitOfTime> systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<ReportingPeriod<UnitOfTime>>();

            // Act
            var result1 = systemUnderTest1 == systemUnderTest2;
            var result2 = systemUnderTest2 == systemUnderTest1;

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
        }

        [Fact]
        public static void EqualsOperator_with_IReportingPeriod___Should_return_true___When_same_object_is_on_both_sides_of_operator()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<ReportingPeriod<UnitOfTime>>();
            IReportingPeriod<UnitOfTime> systemUnderTest2 = systemUnderTest1;

            // Act
#pragma warning disable CS1718 // Comparison made to same variable
            var result1 = systemUnderTest1 == systemUnderTest2;
            var result2 = systemUnderTest2 == systemUnderTest1;
#pragma warning restore CS1718 // Comparison made to same variable

            // Assert
            result1.Should().BeTrue();
            result2.Should().BeTrue();
        }

        [Fact]
        public static void EqualsOperator_with_IReportingPeriod___Should_return_false___When_objects_being_compared_are_different_kinds_of_ReportingPeriod_with_same_unit_of_time()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<ReportingPeriod<CalendarDay>>();
            var systemUnderTest1b = new Common.ReportingPeriodTest<CalendarDay>(systemUnderTest1a.Start, systemUnderTest1a.End);

            var systemUnderTest2a = A.Dummy<ReportingPeriod<FiscalQuarter>>();
            var systemUnderTest2b = (IReportingPeriod<FiscalQuarter>)new Common.ReportingPeriodTest<FiscalQuarter>(systemUnderTest2a.Start, systemUnderTest2a.End);

            var systemUnderTest3a = new ReportingPeriod<GenericUnitOfTime>(A.Dummy<GenericUnbounded>(), A.Dummy<GenericUnbounded>());
            var systemUnderTest3b = (IReportingPeriod<GenericUnitOfTime>)new Common.ReportingPeriodTest<GenericUnitOfTime>(A.Dummy<GenericUnbounded>(), A.Dummy<GenericUnbounded>());

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
        public static void EqualsOperator_with_IReportingPeriod___Should_return_false___When_objects_being_compared_have_same_kind_of_unit_of_time_and_have_different_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<IReportingPeriod<CalendarDay>>();
            var systemUnderTest1b = A.Dummy<ReportingPeriod<CalendarDay>>();

            var systemUnderTest2a = A.Dummy<IReportingPeriod<FiscalQuarter>>();
            var systemUnderTest2b = new ReportingPeriod<FiscalQuarter>(systemUnderTest2a.Start, A.Dummy<FiscalQuarter>().ThatIs(q => (q != systemUnderTest2a.End) && (q >= systemUnderTest2a.Start)));

            var systemUnderTest3a = A.Dummy<IReportingPeriod<CalendarMonth>>();
            var systemUnderTest3b = new ReportingPeriod<CalendarMonth>(A.Dummy<CalendarMonth>().ThatIs(m => (m != systemUnderTest3a.Start) && (m <= systemUnderTest3a.End)), systemUnderTest3a.End);

            var systemUnderTest4a = (IReportingPeriod<CalendarUnitOfTime>)new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarUnbounded>(), A.Dummy<CalendarDay>());
            var systemUnderTest4b = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarUnbounded>(), A.Dummy<CalendarDay>());

            var systemUnderTest5a = (IReportingPeriod<CalendarUnitOfTime>)new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarDay>(), A.Dummy<CalendarUnbounded>());
            var systemUnderTest5b = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarDay>(), A.Dummy<CalendarUnbounded>());

            // Act
            var result1a = systemUnderTest1a == systemUnderTest1b;
            var result1b = systemUnderTest1b == systemUnderTest1a;

            var result2a = systemUnderTest2a == systemUnderTest2b;
            var result2b = systemUnderTest2b == systemUnderTest2a;

            var result3a = systemUnderTest3a == systemUnderTest3b;
            var result3b = systemUnderTest3b == systemUnderTest3a;

            var result4a = systemUnderTest4a == systemUnderTest4b;
            var result4b = systemUnderTest4b == systemUnderTest4a;

            var result5a = systemUnderTest5a == systemUnderTest5b;
            var result5b = systemUnderTest5b == systemUnderTest5a;

            // Assert
            result1a.Should().BeFalse();
            result1b.Should().BeFalse();

            result2a.Should().BeFalse();
            result2b.Should().BeFalse();

            result3a.Should().BeFalse();
            result3b.Should().BeFalse();

            result4a.Should().BeFalse();
            result4b.Should().BeFalse();

            result5a.Should().BeFalse();
            result5b.Should().BeFalse();
        }

        [Fact]
        public static void EqualsOperator_with_IReportingPeriod___Should_return_false___When_objects_being_compared_have_different_but_comparable_kinds_of_units_of_time_and_have_different_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<IReportingPeriod<FiscalQuarter>>();
            var systemUnderTest1b = new ReportingPeriod<FiscalUnitOfTime>(systemUnderTest1a.Start, A.Dummy<FiscalQuarter>().ThatIs(q => (q != systemUnderTest1a.End) && (q >= systemUnderTest1a.Start)));

            var systemUnderTest2a = A.Dummy<ReportingPeriod<CalendarMonth>>();
            var systemUnderTest2b = (IReportingPeriod<UnitOfTime>)new ReportingPeriod<UnitOfTime>(A.Dummy<CalendarMonth>().ThatIs(m => (m != systemUnderTest2a.Start) && (m <= systemUnderTest2a.End)), systemUnderTest2a.End);

            var systemUnderTest3a = (IReportingPeriod<CalendarUnitOfTime>)new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarUnbounded>(), A.Dummy<CalendarDay>());
            var systemUnderTest3b = new ReportingPeriod<UnitOfTime>(A.Dummy<CalendarUnbounded>(), A.Dummy<CalendarDay>());

            var systemUnderTest4a = (IReportingPeriod<UnitOfTime>)new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarDay>(), A.Dummy<CalendarUnbounded>());
            var systemUnderTest4b = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarDay>(), A.Dummy<CalendarUnbounded>());

            // Act
            var result1a = systemUnderTest1a == systemUnderTest1b;
            var result1b = systemUnderTest1b == systemUnderTest1a;

            var result2a = systemUnderTest2a == systemUnderTest2b;
            var result2b = systemUnderTest2b == systemUnderTest2a;

            var result3a = systemUnderTest3a == systemUnderTest3b;
            var result3b = systemUnderTest3b == systemUnderTest3a;

            var result4a = systemUnderTest4a == systemUnderTest4b;
            var result4b = systemUnderTest4b == systemUnderTest4a;

            // Assert
            result1a.Should().BeFalse();
            result1b.Should().BeFalse();

            result2a.Should().BeFalse();
            result2b.Should().BeFalse();

            result3a.Should().BeFalse();
            result3b.Should().BeFalse();

            result4a.Should().BeFalse();
            result4b.Should().BeFalse();
        }

        [Fact]
        public static void EqualsOperator_with_IReportingPeriod___Should_return_false___When_objects_being_compared_have_different_and_not_comparable_units_of_time()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<IReportingPeriod<FiscalQuarter>>();
            var systemUnderTest1b = new ReportingPeriod<GenericQuarter>(systemUnderTest1a.Start.ToGenericQuarter(), systemUnderTest1a.End.ToGenericQuarter());

            var systemUnderTest2a = A.Dummy<ReportingPeriod<CalendarMonth>>();
            var systemUnderTest2b = (IReportingPeriod<GenericUnitOfTime>)new ReportingPeriod<GenericUnitOfTime>(systemUnderTest2a.Start.ToGenericMonth(), systemUnderTest2a.End.ToGenericMonth());

            var systemUnderTest3a = (IReportingPeriod<CalendarUnitOfTime>)new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarUnbounded>(), A.Dummy<CalendarDay>());
            var systemUnderTest3b = new ReportingPeriod<GenericUnitOfTime>(A.Dummy<GenericUnbounded>(), A.Dummy<GenericMonth>());

            var systemUnderTest4a = (IReportingPeriod<UnitOfTime>)new ReportingPeriod<GenericUnitOfTime>(A.Dummy<GenericMonth>(), A.Dummy<GenericUnbounded>());
            var systemUnderTest4b = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarDay>(), A.Dummy<CalendarUnbounded>());

            // Act
            var result1a = systemUnderTest1a == systemUnderTest1b;
            var result1b = systemUnderTest1b == systemUnderTest1a;

            var result2a = systemUnderTest2a == systemUnderTest2b;
            var result2b = systemUnderTest2b == systemUnderTest2a;

            var result3a = systemUnderTest3a == systemUnderTest3b;
            var result3b = systemUnderTest3b == systemUnderTest3a;

            var result4a = systemUnderTest4a == systemUnderTest4b;
            var result4b = systemUnderTest4b == systemUnderTest4a;

            // Assert
            result1a.Should().BeFalse();
            result1b.Should().BeFalse();

            result2a.Should().BeFalse();
            result2b.Should().BeFalse();

            result3a.Should().BeFalse();
            result3b.Should().BeFalse();

            result4a.Should().BeFalse();
            result4b.Should().BeFalse();
        }

        [Fact]
        public static void EqualsOperator_with_IReportingPeriod___Should_return_true___When_objects_being_compared_have_same_kind_of_unit_of_time_and_same_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<IReportingPeriod<UnitOfTime>>();
            var systemUnderTest1b = new ReportingPeriod<UnitOfTime>(systemUnderTest1a.Start, systemUnderTest1a.End);

            var systemUnderTest2a = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarDay>(), A.Dummy<CalendarUnbounded>());
            var systemUnderTest2b = (IReportingPeriod<CalendarUnitOfTime>)new ReportingPeriod<CalendarUnitOfTime>(systemUnderTest2a.Start, A.Dummy<CalendarUnbounded>());

            var systemUnderTest3a = (IReportingPeriod<CalendarUnitOfTime>)new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarUnbounded>(), A.Dummy<CalendarDay>());
            var systemUnderTest3b = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarUnbounded>(), systemUnderTest3a.End);

            var systemUnderTest4a = new ReportingPeriod<UnitOfTime>(A.Dummy<FiscalUnbounded>(), A.Dummy<FiscalUnbounded>());
            var systemUnderTest4b = (IReportingPeriod<UnitOfTime>)new ReportingPeriod<UnitOfTime>(A.Dummy<FiscalUnbounded>(), A.Dummy<FiscalUnbounded>());

            // Act
            var result1a = systemUnderTest1a == systemUnderTest1b;
            var result1b = systemUnderTest1b == systemUnderTest1a;

            var result2a = systemUnderTest2a == systemUnderTest2b;
            var result2b = systemUnderTest2b == systemUnderTest2a;

            var result3a = systemUnderTest3a == systemUnderTest3b;
            var result3b = systemUnderTest3b == systemUnderTest3a;

            var result4a = systemUnderTest4a == systemUnderTest4b;
            var result4b = systemUnderTest4b == systemUnderTest4a;

            // Assert
            result1a.Should().BeTrue();
            result1b.Should().BeTrue();

            result2a.Should().BeTrue();
            result2b.Should().BeTrue();

            result3a.Should().BeTrue();
            result3b.Should().BeTrue();

            result4a.Should().BeTrue();
            result4b.Should().BeTrue();
        }

        [Fact]
        public static void EqualsOperator_with_IReportingPeriod___Should_return_true___When_objects_being_compared_have_different_but_comparable_kinds_of_unit_of_time_and_same_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<IReportingPeriod<CalendarDay>>();
            var systemUnderTest1b = new ReportingPeriod<CalendarUnitOfTime>(systemUnderTest1a.Start, systemUnderTest1a.End);

            var systemUnderTest2a = A.Dummy<ReportingPeriod<GenericQuarter>>();
            var systemUnderTest2b = (IReportingPeriod<GenericUnitOfTime>)new ReportingPeriod<GenericQuarter>(systemUnderTest2a.Start, systemUnderTest2a.End);

            var systemUnderTest3a = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarDay>(), A.Dummy<CalendarUnbounded>());
            var systemUnderTest3b = (IReportingPeriod<UnitOfTime>)new ReportingPeriod<CalendarUnitOfTime>(systemUnderTest3a.Start, A.Dummy<CalendarUnbounded>());

            var systemUnderTest4a = (IReportingPeriod<CalendarUnitOfTime>)new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarUnbounded>(), A.Dummy<CalendarDay>());
            var systemUnderTest4b = new ReportingPeriod<UnitOfTime>(A.Dummy<CalendarUnbounded>(), systemUnderTest4a.End);

            var systemUnderTest5a = new ReportingPeriod<FiscalUnbounded>(A.Dummy<FiscalUnbounded>(), A.Dummy<FiscalUnbounded>());
            var systemUnderTest5b = (IReportingPeriod<FiscalUnitOfTime>)new ReportingPeriod<FiscalUnitOfTime>(A.Dummy<FiscalUnbounded>(), A.Dummy<FiscalUnbounded>());

            // Act
            var result1a = systemUnderTest1a == systemUnderTest1b;
            var result1b = systemUnderTest1b == systemUnderTest1a;

            var result2a = systemUnderTest2a == systemUnderTest2b;
            var result2b = systemUnderTest2b == systemUnderTest2a;

            var result3a = systemUnderTest3a == systemUnderTest3b;
            var result3b = systemUnderTest3b == systemUnderTest3a;

            var result4a = systemUnderTest4a == systemUnderTest4b;
            var result4b = systemUnderTest4b == systemUnderTest4a;

            var result5a = systemUnderTest5a == systemUnderTest5b;
            var result5b = systemUnderTest5b == systemUnderTest5a;

            // Assert
            result1a.Should().BeTrue();
            result1b.Should().BeTrue();

            result2a.Should().BeTrue();
            result2b.Should().BeTrue();

            result3a.Should().BeTrue();
            result3b.Should().BeTrue();

            result4a.Should().BeTrue();
            result4b.Should().BeTrue();

            result5a.Should().BeTrue();
            result5b.Should().BeTrue();
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_false___When_both_sides_of_operator_are_null()
        {
            // Arrange
            ReportingPeriod<UnitOfTime> systemUnderTest1 = null;
            ReportingPeriod<UnitOfTime> systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 != systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_true___When_one_side_of_operator_is_null_and_the_other_side_is_not_null()
        {
            // Arrange
            ReportingPeriod<UnitOfTime> systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<ReportingPeriod<UnitOfTime>>();

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
            var systemUnderTest = A.Dummy<ReportingPeriod<UnitOfTime>>();

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
            var systemUnderTest1a = A.Dummy<ReportingPeriod<CalendarDay>>();
            var systemUnderTest1b = A.Dummy<ReportingPeriod<CalendarDay>>();

            var systemUnderTest2a = A.Dummy<ReportingPeriod<FiscalQuarter>>();
            var systemUnderTest2b = new ReportingPeriod<FiscalQuarter>(systemUnderTest2a.Start, A.Dummy<FiscalQuarter>().ThatIs(q => (q != systemUnderTest2a.End) && (q >= systemUnderTest2a.Start)));

            var systemUnderTest3a = A.Dummy<ReportingPeriod<CalendarMonth>>();
            var systemUnderTest3b = new ReportingPeriod<CalendarMonth>(A.Dummy<CalendarMonth>().ThatIs(m => (m != systemUnderTest3a.Start) && (m <= systemUnderTest3a.End)), systemUnderTest3a.End);

            var systemUnderTest4a = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarUnbounded>(), A.Dummy<CalendarDay>());
            var systemUnderTest4b = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarUnbounded>(), A.Dummy<CalendarDay>());

            var systemUnderTest5a = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarDay>(), A.Dummy<CalendarUnbounded>());
            var systemUnderTest5b = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarDay>(), A.Dummy<CalendarUnbounded>());

            // Act
            var result1 = systemUnderTest1a != systemUnderTest1b;
            var result2 = systemUnderTest2a != systemUnderTest2b;
            var result3 = systemUnderTest3a != systemUnderTest3b;
            var result4 = systemUnderTest4a != systemUnderTest4b;
            var result5 = systemUnderTest5a != systemUnderTest5b;

            // Assert
            result1.Should().BeTrue();
            result2.Should().BeTrue();
            result3.Should().BeTrue();
            result4.Should().BeTrue();
            result5.Should().BeTrue();
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_false___When_objects_being_compared_have_same_property_values()
        {
            var systemUnderTest1a = A.Dummy<ReportingPeriod<UnitOfTime>>();
            var systemUnderTest1b = new ReportingPeriod<UnitOfTime>(systemUnderTest1a.Start, systemUnderTest1a.End);

            var systemUnderTest2a = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarDay>(), A.Dummy<CalendarUnbounded>());
            var systemUnderTest2b = new ReportingPeriod<CalendarUnitOfTime>(systemUnderTest2a.Start, A.Dummy<CalendarUnbounded>());

            var systemUnderTest3a = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarUnbounded>(), A.Dummy<CalendarDay>());
            var systemUnderTest3b = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarUnbounded>(), systemUnderTest3a.End);

            var systemUnderTest4a = new ReportingPeriod<UnitOfTime>(A.Dummy<FiscalUnbounded>(), A.Dummy<FiscalUnbounded>());
            var systemUnderTest4b = new ReportingPeriod<UnitOfTime>(A.Dummy<FiscalUnbounded>(), A.Dummy<FiscalUnbounded>());

            // Act
            var result1 = systemUnderTest1a != systemUnderTest1b;
            var result2 = systemUnderTest2a != systemUnderTest2b;
            var result3 = systemUnderTest3a != systemUnderTest3b;
            var result4 = systemUnderTest4a != systemUnderTest4b;

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
            result3.Should().BeFalse();
            result4.Should().BeFalse();
        }

        [Fact]
        public static void NotEqualsOperator_with_IReportingPeriod___Should_return_false___When_both_sides_of_operator_are_null()
        {
            // Arrange
            IReportingPeriod<UnitOfTime> systemUnderTest1 = null;
            ReportingPeriod<UnitOfTime> systemUnderTest2 = null;

            // Act
            var result1 = systemUnderTest1 != systemUnderTest2;
            var result2 = systemUnderTest2 != systemUnderTest1;

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
        }

        [Fact]
        public static void NotEqualsOperator_with_IReportingPeriod___Should_return_true___When_one_side_of_operator_is_null_and_the_other_side_is_not_null()
        {
            // Arrange
            IReportingPeriod<UnitOfTime> systemUnderTest1 = null;
            var systemUnderTest2 = A.Dummy<ReportingPeriod<UnitOfTime>>();

            // Act
            var result1 = systemUnderTest1 != systemUnderTest2;
            var result2 = systemUnderTest2 != systemUnderTest1;

            // Assert
            result1.Should().BeTrue();
            result2.Should().BeTrue();
        }

        [Fact]
        public static void NotEqualsOperator_with_IReportingPeriod___Should_return_false___When_same_object_is_on_both_sides_of_operator()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<ReportingPeriod<UnitOfTime>>();
            IReportingPeriod<UnitOfTime> systemUnderTest2 = systemUnderTest1;

            // Act
#pragma warning disable CS1718 // Comparison made to same variable
            var result1 = systemUnderTest1 != systemUnderTest2;
            var result2 = systemUnderTest2 != systemUnderTest1;
#pragma warning restore CS1718 // Comparison made to same variable

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
        }

        [Fact]
        public static void NotEqualsOperator_with_IReportingPeriod___Should_return_true___When_objects_being_compared_are_different_kinds_of_ReportingPeriod_with_same_unit_of_time()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<ReportingPeriod<CalendarDay>>();
            var systemUnderTest1b = new Common.ReportingPeriodTest<CalendarDay>(systemUnderTest1a.Start, systemUnderTest1a.End);

            var systemUnderTest2a = A.Dummy<ReportingPeriod<FiscalQuarter>>();
            var systemUnderTest2b = (IReportingPeriod<FiscalQuarter>)new Common.ReportingPeriodTest<FiscalQuarter>(systemUnderTest2a.Start, systemUnderTest2a.End);

            var systemUnderTest3a = new ReportingPeriod<GenericUnitOfTime>(A.Dummy<GenericUnbounded>(), A.Dummy<GenericUnbounded>());
            var systemUnderTest3b = (IReportingPeriod<GenericUnitOfTime>)new Common.ReportingPeriodTest<GenericUnitOfTime>(A.Dummy<GenericUnbounded>(), A.Dummy<GenericUnbounded>());

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
        public static void NotEqualsOperator_with_IReportingPeriod___Should_return_true___When_objects_being_compared_have_same_kind_of_unit_of_time_and_have_different_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<IReportingPeriod<CalendarDay>>();
            var systemUnderTest1b = A.Dummy<ReportingPeriod<CalendarDay>>();

            var systemUnderTest2a = A.Dummy<IReportingPeriod<FiscalQuarter>>();
            var systemUnderTest2b = new ReportingPeriod<FiscalQuarter>(systemUnderTest2a.Start, A.Dummy<FiscalQuarter>().ThatIs(q => (q != systemUnderTest2a.End) && (q >= systemUnderTest2a.Start)));

            var systemUnderTest3a = A.Dummy<IReportingPeriod<CalendarMonth>>();
            var systemUnderTest3b = new ReportingPeriod<CalendarMonth>(A.Dummy<CalendarMonth>().ThatIs(m => (m != systemUnderTest3a.Start) && (m <= systemUnderTest3a.End)), systemUnderTest3a.End);

            var systemUnderTest4a = (IReportingPeriod<CalendarUnitOfTime>)new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarUnbounded>(), A.Dummy<CalendarDay>());
            var systemUnderTest4b = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarUnbounded>(), A.Dummy<CalendarDay>());

            var systemUnderTest5a = (IReportingPeriod<CalendarUnitOfTime>)new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarDay>(), A.Dummy<CalendarUnbounded>());
            var systemUnderTest5b = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarDay>(), A.Dummy<CalendarUnbounded>());

            // Act
            var result1a = systemUnderTest1a != systemUnderTest1b;
            var result1b = systemUnderTest1b != systemUnderTest1a;

            var result2a = systemUnderTest2a != systemUnderTest2b;
            var result2b = systemUnderTest2b != systemUnderTest2a;

            var result3a = systemUnderTest3a != systemUnderTest3b;
            var result3b = systemUnderTest3b != systemUnderTest3a;

            var result4a = systemUnderTest4a != systemUnderTest4b;
            var result4b = systemUnderTest4b != systemUnderTest4a;

            var result5a = systemUnderTest5a != systemUnderTest5b;
            var result5b = systemUnderTest5b != systemUnderTest5a;

            // Assert
            result1a.Should().BeTrue();
            result1b.Should().BeTrue();

            result2a.Should().BeTrue();
            result2b.Should().BeTrue();

            result3a.Should().BeTrue();
            result3b.Should().BeTrue();

            result4a.Should().BeTrue();
            result4b.Should().BeTrue();

            result5a.Should().BeTrue();
            result5b.Should().BeTrue();
        }

        [Fact]
        public static void NotEqualsOperator_with_IReportingPeriod___Should_return_true___When_objects_being_compared_have_different_but_comparable_kinds_of_units_of_time_and_have_different_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<IReportingPeriod<FiscalQuarter>>();
            var systemUnderTest1b = new ReportingPeriod<FiscalUnitOfTime>(systemUnderTest1a.Start, A.Dummy<FiscalQuarter>().ThatIs(q => (q != systemUnderTest1a.End) && (q >= systemUnderTest1a.Start)));

            var systemUnderTest2a = A.Dummy<ReportingPeriod<CalendarMonth>>();
            var systemUnderTest2b = (IReportingPeriod<UnitOfTime>)new ReportingPeriod<UnitOfTime>(A.Dummy<CalendarMonth>().ThatIs(m => (m != systemUnderTest2a.Start) && (m <= systemUnderTest2a.End)), systemUnderTest2a.End);

            var systemUnderTest3a = (IReportingPeriod<CalendarUnitOfTime>)new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarUnbounded>(), A.Dummy<CalendarDay>());
            var systemUnderTest3b = new ReportingPeriod<UnitOfTime>(A.Dummy<CalendarUnbounded>(), A.Dummy<CalendarDay>());

            var systemUnderTest4a = (IReportingPeriod<UnitOfTime>)new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarDay>(), A.Dummy<CalendarUnbounded>());
            var systemUnderTest4b = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarDay>(), A.Dummy<CalendarUnbounded>());

            // Act
            var result1a = systemUnderTest1a != systemUnderTest1b;
            var result1b = systemUnderTest1b != systemUnderTest1a;

            var result2a = systemUnderTest2a != systemUnderTest2b;
            var result2b = systemUnderTest2b != systemUnderTest2a;

            var result3a = systemUnderTest3a != systemUnderTest3b;
            var result3b = systemUnderTest3b != systemUnderTest3a;

            var result4a = systemUnderTest4a != systemUnderTest4b;
            var result4b = systemUnderTest4b != systemUnderTest4a;

            // Assert
            result1a.Should().BeTrue();
            result1b.Should().BeTrue();

            result2a.Should().BeTrue();
            result2b.Should().BeTrue();

            result3a.Should().BeTrue();
            result3b.Should().BeTrue();

            result4a.Should().BeTrue();
            result4b.Should().BeTrue();
        }

        [Fact]
        public static void NotEqualsOperator_with_IReportingPeriod___Should_return_true___When_objects_being_compared_have_different_and_not_comparable_units_of_time()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<IReportingPeriod<FiscalQuarter>>();
            var systemUnderTest1b = new ReportingPeriod<GenericQuarter>(systemUnderTest1a.Start.ToGenericQuarter(), systemUnderTest1a.End.ToGenericQuarter());

            var systemUnderTest2a = A.Dummy<ReportingPeriod<CalendarMonth>>();
            var systemUnderTest2b = (IReportingPeriod<GenericUnitOfTime>)new ReportingPeriod<GenericUnitOfTime>(systemUnderTest2a.Start.ToGenericMonth(), systemUnderTest2a.End.ToGenericMonth());

            var systemUnderTest3a = (IReportingPeriod<CalendarUnitOfTime>)new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarUnbounded>(), A.Dummy<CalendarDay>());
            var systemUnderTest3b = new ReportingPeriod<GenericUnitOfTime>(A.Dummy<GenericUnbounded>(), A.Dummy<GenericMonth>());

            var systemUnderTest4a = (IReportingPeriod<UnitOfTime>)new ReportingPeriod<GenericUnitOfTime>(A.Dummy<GenericMonth>(), A.Dummy<GenericUnbounded>());
            var systemUnderTest4b = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarDay>(), A.Dummy<CalendarUnbounded>());

            // Act
            var result1a = systemUnderTest1a != systemUnderTest1b;
            var result1b = systemUnderTest1b != systemUnderTest1a;

            var result2a = systemUnderTest2a != systemUnderTest2b;
            var result2b = systemUnderTest2b != systemUnderTest2a;

            var result3a = systemUnderTest3a != systemUnderTest3b;
            var result3b = systemUnderTest3b != systemUnderTest3a;

            var result4a = systemUnderTest4a != systemUnderTest4b;
            var result4b = systemUnderTest4b != systemUnderTest4a;

            // Assert
            result1a.Should().BeTrue();
            result1b.Should().BeTrue();

            result2a.Should().BeTrue();
            result2b.Should().BeTrue();

            result3a.Should().BeTrue();
            result3b.Should().BeTrue();

            result4a.Should().BeTrue();
            result4b.Should().BeTrue();
        }

        [Fact]
        public static void NotEqualsOperator_with_IReportingPeriod___Should_return_false___When_objects_being_compared_have_same_kind_of_unit_of_time_and_same_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<IReportingPeriod<UnitOfTime>>();
            var systemUnderTest1b = new ReportingPeriod<UnitOfTime>(systemUnderTest1a.Start, systemUnderTest1a.End);

            var systemUnderTest2a = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarDay>(), A.Dummy<CalendarUnbounded>());
            var systemUnderTest2b = (IReportingPeriod<CalendarUnitOfTime>)new ReportingPeriod<CalendarUnitOfTime>(systemUnderTest2a.Start, A.Dummy<CalendarUnbounded>());

            var systemUnderTest3a = (IReportingPeriod<CalendarUnitOfTime>)new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarUnbounded>(), A.Dummy<CalendarDay>());
            var systemUnderTest3b = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarUnbounded>(), systemUnderTest3a.End);

            var systemUnderTest4a = new ReportingPeriod<UnitOfTime>(A.Dummy<FiscalUnbounded>(), A.Dummy<FiscalUnbounded>());
            var systemUnderTest4b = (IReportingPeriod<UnitOfTime>)new ReportingPeriod<UnitOfTime>(A.Dummy<FiscalUnbounded>(), A.Dummy<FiscalUnbounded>());

            // Act
            var result1a = systemUnderTest1a != systemUnderTest1b;
            var result1b = systemUnderTest1b != systemUnderTest1a;

            var result2a = systemUnderTest2a != systemUnderTest2b;
            var result2b = systemUnderTest2b != systemUnderTest2a;

            var result3a = systemUnderTest3a != systemUnderTest3b;
            var result3b = systemUnderTest3b != systemUnderTest3a;

            var result4a = systemUnderTest4a != systemUnderTest4b;
            var result4b = systemUnderTest4b != systemUnderTest4a;

            // Assert
            result1a.Should().BeFalse();
            result1b.Should().BeFalse();

            result2a.Should().BeFalse();
            result2b.Should().BeFalse();

            result3a.Should().BeFalse();
            result3b.Should().BeFalse();

            result4a.Should().BeFalse();
            result4b.Should().BeFalse();
        }

        [Fact]
        public static void NotEqualsOperator_with_IReportingPeriod___Should_return_false___When_objects_being_compared_have_different_but_comparable_kinds_of_unit_of_time_and_same_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<IReportingPeriod<CalendarDay>>();
            var systemUnderTest1b = new ReportingPeriod<CalendarUnitOfTime>(systemUnderTest1a.Start, systemUnderTest1a.End);

            var systemUnderTest2a = A.Dummy<ReportingPeriod<GenericQuarter>>();
            var systemUnderTest2b = (IReportingPeriod<GenericUnitOfTime>)new ReportingPeriod<GenericQuarter>(systemUnderTest2a.Start, systemUnderTest2a.End);

            var systemUnderTest3a = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarDay>(), A.Dummy<CalendarUnbounded>());
            var systemUnderTest3b = (IReportingPeriod<UnitOfTime>)new ReportingPeriod<CalendarUnitOfTime>(systemUnderTest3a.Start, A.Dummy<CalendarUnbounded>());

            var systemUnderTest4a = (IReportingPeriod<CalendarUnitOfTime>)new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarUnbounded>(), A.Dummy<CalendarDay>());
            var systemUnderTest4b = new ReportingPeriod<UnitOfTime>(A.Dummy<CalendarUnbounded>(), systemUnderTest4a.End);

            var systemUnderTest5a = new ReportingPeriod<FiscalUnbounded>(A.Dummy<FiscalUnbounded>(), A.Dummy<FiscalUnbounded>());
            var systemUnderTest5b = (IReportingPeriod<FiscalUnitOfTime>)new ReportingPeriod<FiscalUnitOfTime>(A.Dummy<FiscalUnbounded>(), A.Dummy<FiscalUnbounded>());

            // Act
            var result1a = systemUnderTest1a != systemUnderTest1b;
            var result1b = systemUnderTest1b != systemUnderTest1a;

            var result2a = systemUnderTest2a != systemUnderTest2b;
            var result2b = systemUnderTest2b != systemUnderTest2a;

            var result3a = systemUnderTest3a != systemUnderTest3b;
            var result3b = systemUnderTest3b != systemUnderTest3a;

            var result4a = systemUnderTest4a != systemUnderTest4b;
            var result4b = systemUnderTest4b != systemUnderTest4a;

            var result5a = systemUnderTest5a != systemUnderTest5b;
            var result5b = systemUnderTest5b != systemUnderTest5a;

            // Assert
            result1a.Should().BeFalse();
            result1b.Should().BeFalse();

            result2a.Should().BeFalse();
            result2b.Should().BeFalse();

            result3a.Should().BeFalse();
            result3b.Should().BeFalse();

            result4a.Should().BeFalse();
            result4b.Should().BeFalse();

            result5a.Should().BeFalse();
            result5b.Should().BeFalse();
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

            var systemUnderTest4a = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarUnbounded>(), A.Dummy<CalendarDay>());
            var systemUnderTest4b = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarUnbounded>(), A.Dummy<CalendarDay>());

            var systemUnderTest5a = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarDay>(), A.Dummy<CalendarUnbounded>());
            var systemUnderTest5b = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarDay>(), A.Dummy<CalendarUnbounded>());

            // Act
            var result1 = systemUnderTest1a.Equals(systemUnderTest1b);
            var result2 = systemUnderTest2a.Equals(systemUnderTest2b);
            var result3 = systemUnderTest3a.Equals(systemUnderTest3b);
            var result4 = systemUnderTest4a.Equals(systemUnderTest4b);
            var result5 = systemUnderTest5a.Equals(systemUnderTest5b);

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
            result3.Should().BeFalse();
            result4.Should().BeFalse();
            result5.Should().BeFalse();
        }

        [Fact]
        public static void Equals_with_ReportingPeriod___Should_return_true___When_objects_being_compared_have_same_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<ReportingPeriod<UnitOfTime>>();
            var systemUnderTest1b = new ReportingPeriod<UnitOfTime>(systemUnderTest1a.Start, systemUnderTest1a.End);

            var systemUnderTest2a = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarDay>(), A.Dummy<CalendarUnbounded>());
            var systemUnderTest2b = new ReportingPeriod<CalendarUnitOfTime>(systemUnderTest2a.Start, A.Dummy<CalendarUnbounded>());

            var systemUnderTest3a = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarUnbounded>(), A.Dummy<CalendarDay>());
            var systemUnderTest3b = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarUnbounded>(), systemUnderTest3a.End);

            var systemUnderTest4a = new ReportingPeriod<UnitOfTime>(A.Dummy<FiscalUnbounded>(), A.Dummy<FiscalUnbounded>());
            var systemUnderTest4b = new ReportingPeriod<UnitOfTime>(A.Dummy<FiscalUnbounded>(), A.Dummy<FiscalUnbounded>());

            // Act
            var result1 = systemUnderTest1a.Equals(systemUnderTest1b);
            var result2 = systemUnderTest2a.Equals(systemUnderTest2b);
            var result3 = systemUnderTest3a.Equals(systemUnderTest3b);
            var result4 = systemUnderTest4a.Equals(systemUnderTest4b);

            // Assert
            result1.Should().BeTrue();
            result2.Should().BeTrue();
            result3.Should().BeTrue();
            result4.Should().BeTrue();
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
            var result = systemUnderTest.Equals(systemUnderTest);

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

            var systemUnderTest3a = new ReportingPeriod<GenericUnitOfTime>(A.Dummy<GenericUnbounded>(), A.Dummy<GenericUnbounded>());
            var systemUnderTest3b = (IReportingPeriod<GenericUnitOfTime>)new Common.ReportingPeriodTest<GenericUnitOfTime>(A.Dummy<GenericUnbounded>(), A.Dummy<GenericUnbounded>());

            // Act
            var result1a = systemUnderTest1a.Equals(systemUnderTest1b);
            var result1b = systemUnderTest1b.Equals(systemUnderTest1a);

            var result2a = systemUnderTest2a.Equals(systemUnderTest2b);
            var result2b = systemUnderTest2b.Equals(systemUnderTest2a);

            var result3a = systemUnderTest3a.Equals(systemUnderTest3b);
            var result3b = systemUnderTest3b.Equals(systemUnderTest3a);

            // Assert
            result1a.Should().BeFalse();
            result1b.Should().BeFalse();

            result2a.Should().BeFalse();
            result2b.Should().BeFalse();

            result3a.Should().BeFalse();
            result3b.Should().BeFalse();
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

            var systemUnderTest4a = (IReportingPeriod<CalendarUnitOfTime>)new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarUnbounded>(), A.Dummy<CalendarDay>());
            var systemUnderTest4b = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarUnbounded>(), A.Dummy<CalendarDay>());

            var systemUnderTest5a = (IReportingPeriod<CalendarUnitOfTime>)new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarDay>(), A.Dummy<CalendarUnbounded>());
            var systemUnderTest5b = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarDay>(), A.Dummy<CalendarUnbounded>());

            // Act
            var result1a = systemUnderTest1a.Equals(systemUnderTest1b);
            var result1b = systemUnderTest1b.Equals(systemUnderTest1a);

            var result2a = systemUnderTest2a.Equals(systemUnderTest2b);
            var result2b = systemUnderTest2b.Equals(systemUnderTest2a);

            var result3a = systemUnderTest3a.Equals(systemUnderTest3b);
            var result3b = systemUnderTest3b.Equals(systemUnderTest3a);

            var result4a = systemUnderTest4a.Equals(systemUnderTest4b);
            var result4b = systemUnderTest4b.Equals(systemUnderTest4a);

            var result5a = systemUnderTest5a.Equals(systemUnderTest5b);
            var result5b = systemUnderTest5b.Equals(systemUnderTest5a);

            // Assert
            result1a.Should().BeFalse();
            result1b.Should().BeFalse();

            result2a.Should().BeFalse();
            result2b.Should().BeFalse();

            result3a.Should().BeFalse();
            result3b.Should().BeFalse();

            result4a.Should().BeFalse();
            result4b.Should().BeFalse();

            result5a.Should().BeFalse();
            result5b.Should().BeFalse();
        }

        [Fact]
        public static void Equals_with_IReportingPeriod___Should_return_false___When_and_objects_being_compared_have_different_but_comparable_kinds_of_units_of_time_and_have_different_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<IReportingPeriod<FiscalQuarter>>();
            var systemUnderTest1b = new ReportingPeriod<FiscalUnitOfTime>(systemUnderTest1a.Start, A.Dummy<FiscalQuarter>().ThatIs(q => (q != systemUnderTest1a.End) && (q >= systemUnderTest1a.Start)));

            var systemUnderTest2a = A.Dummy<ReportingPeriod<CalendarMonth>>();
            var systemUnderTest2b = (IReportingPeriod<UnitOfTime>)new ReportingPeriod<UnitOfTime>(A.Dummy<CalendarMonth>().ThatIs(m => (m != systemUnderTest2a.Start) && (m <= systemUnderTest2a.End)), systemUnderTest2a.End);

            var systemUnderTest3a = (IReportingPeriod<CalendarUnitOfTime>)new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarUnbounded>(), A.Dummy<CalendarDay>());
            var systemUnderTest3b = new ReportingPeriod<UnitOfTime>(A.Dummy<CalendarUnbounded>(), A.Dummy<CalendarDay>());

            var systemUnderTest4a = (IReportingPeriod<UnitOfTime>)new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarDay>(), A.Dummy<CalendarUnbounded>());
            var systemUnderTest4b = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarDay>(), A.Dummy<CalendarUnbounded>());

            // Act
            var result1a = systemUnderTest1a.Equals(systemUnderTest1b);
            var result1b = systemUnderTest1b.Equals(systemUnderTest1a);

            var result2a = systemUnderTest2a.Equals(systemUnderTest2b);
            var result2b = systemUnderTest2b.Equals(systemUnderTest2a);

            var result3a = systemUnderTest3a.Equals(systemUnderTest3b);
            var result3b = systemUnderTest3b.Equals(systemUnderTest3a);

            var result4a = systemUnderTest4a.Equals(systemUnderTest4b);
            var result4b = systemUnderTest4b.Equals(systemUnderTest4a);

            // Assert
            result1a.Should().BeFalse();
            result1b.Should().BeFalse();

            result2a.Should().BeFalse();
            result2b.Should().BeFalse();

            result3a.Should().BeFalse();
            result3b.Should().BeFalse();

            result4a.Should().BeFalse();
            result4b.Should().BeFalse();
        }

        [Fact]
        public static void Equals_with_IReportingPeriod___Should_return_false___When_and_objects_being_compared_have_different_and_not_comparable_units_of_time()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<IReportingPeriod<FiscalQuarter>>();
            var systemUnderTest1b = new ReportingPeriod<GenericQuarter>(systemUnderTest1a.Start.ToGenericQuarter(), systemUnderTest1a.End.ToGenericQuarter());

            var systemUnderTest2a = A.Dummy<ReportingPeriod<CalendarMonth>>();
            var systemUnderTest2b = (IReportingPeriod<GenericUnitOfTime>)new ReportingPeriod<GenericUnitOfTime>(systemUnderTest2a.Start.ToGenericMonth(), systemUnderTest2a.End.ToGenericMonth());

            var systemUnderTest3a = (IReportingPeriod<CalendarUnitOfTime>)new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarUnbounded>(), A.Dummy<CalendarDay>());
            var systemUnderTest3b = new ReportingPeriod<GenericUnitOfTime>(A.Dummy<GenericUnbounded>(), A.Dummy<GenericMonth>());

            var systemUnderTest4a = (IReportingPeriod<UnitOfTime>)new ReportingPeriod<GenericUnitOfTime>(A.Dummy<GenericMonth>(), A.Dummy<GenericUnbounded>());
            var systemUnderTest4b = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarDay>(), A.Dummy<CalendarUnbounded>());

            // Act
            var result1a = systemUnderTest1a.Equals(systemUnderTest1b);
            var result1b = systemUnderTest1b.Equals(systemUnderTest1a);

            var result2a = systemUnderTest2a.Equals(systemUnderTest2b);
            var result2b = systemUnderTest2b.Equals(systemUnderTest2a);

            var result3a = systemUnderTest3a.Equals(systemUnderTest3b);
            var result3b = systemUnderTest3b.Equals(systemUnderTest3a);

            var result4a = systemUnderTest4a.Equals(systemUnderTest4b);
            var result4b = systemUnderTest4b.Equals(systemUnderTest4a);

            // Assert
            result1a.Should().BeFalse();
            result1b.Should().BeFalse();

            result2a.Should().BeFalse();
            result2b.Should().BeFalse();

            result3a.Should().BeFalse();
            result3b.Should().BeFalse();

            result4a.Should().BeFalse();
            result4b.Should().BeFalse();
        }

        [Fact]
        public static void Equals_with_IReportingPeriod___Should_return_true___When_objects_being_compared_have_same_kind_of_unit_of_time_and_same_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<IReportingPeriod<UnitOfTime>>();
            var systemUnderTest1b = new ReportingPeriod<UnitOfTime>(systemUnderTest1a.Start, systemUnderTest1a.End);

            var systemUnderTest2a = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarDay>(), A.Dummy<CalendarUnbounded>());
            var systemUnderTest2b = (IReportingPeriod<CalendarUnitOfTime>)new ReportingPeriod<CalendarUnitOfTime>(systemUnderTest2a.Start, A.Dummy<CalendarUnbounded>());

            var systemUnderTest3a = (IReportingPeriod<CalendarUnitOfTime>)new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarUnbounded>(), A.Dummy<CalendarDay>());
            var systemUnderTest3b = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarUnbounded>(), systemUnderTest3a.End);

            var systemUnderTest4a = new ReportingPeriod<UnitOfTime>(A.Dummy<FiscalUnbounded>(), A.Dummy<FiscalUnbounded>());
            var systemUnderTest4b = (IReportingPeriod<UnitOfTime>)new ReportingPeriod<UnitOfTime>(A.Dummy<FiscalUnbounded>(), A.Dummy<FiscalUnbounded>());

            // Act
            var result1a = systemUnderTest1a.Equals(systemUnderTest1b);
            var result1b = systemUnderTest1b.Equals(systemUnderTest1a);

            var result2a = systemUnderTest2a.Equals(systemUnderTest2b);
            var result2b = systemUnderTest2b.Equals(systemUnderTest2a);

            var result3a = systemUnderTest3a.Equals(systemUnderTest3b);
            var result3b = systemUnderTest3b.Equals(systemUnderTest3a);

            var result4a = systemUnderTest4a.Equals(systemUnderTest4b);
            var result4b = systemUnderTest4b.Equals(systemUnderTest4a);

            // Assert
            result1a.Should().BeTrue();
            result1b.Should().BeTrue();

            result2a.Should().BeTrue();
            result2b.Should().BeTrue();

            result3a.Should().BeTrue();
            result3b.Should().BeTrue();

            result4a.Should().BeTrue();
            result4b.Should().BeTrue();
        }

        [Fact]
        public static void Equals_with_IReportingPeriod___Should_return_true___When_objects_being_compared_have_different_but_comparable_kinds_of_unit_of_time_and_same_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<IReportingPeriod<CalendarDay>>();
            var systemUnderTest1b = new ReportingPeriod<CalendarUnitOfTime>(systemUnderTest1a.Start, systemUnderTest1a.End);

            var systemUnderTest2a = A.Dummy<ReportingPeriod<GenericQuarter>>();
            var systemUnderTest2b = (IReportingPeriod<GenericUnitOfTime>)new ReportingPeriod<GenericQuarter>(systemUnderTest2a.Start, systemUnderTest2a.End);

            var systemUnderTest3a = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarDay>(), A.Dummy<CalendarUnbounded>());
            var systemUnderTest3b = (IReportingPeriod<UnitOfTime>)new ReportingPeriod<CalendarUnitOfTime>(systemUnderTest3a.Start, A.Dummy<CalendarUnbounded>());

            var systemUnderTest4a = (IReportingPeriod<CalendarUnitOfTime>)new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarUnbounded>(), A.Dummy<CalendarDay>());
            var systemUnderTest4b = new ReportingPeriod<UnitOfTime>(A.Dummy<CalendarUnbounded>(), systemUnderTest4a.End);

            var systemUnderTest5a = new ReportingPeriod<FiscalUnbounded>(A.Dummy<FiscalUnbounded>(), A.Dummy<FiscalUnbounded>());
            var systemUnderTest5b = (IReportingPeriod<FiscalUnitOfTime>)new ReportingPeriod<FiscalUnitOfTime>(A.Dummy<FiscalUnbounded>(), A.Dummy<FiscalUnbounded>());

            // Act
            var result1a = systemUnderTest1a.Equals(systemUnderTest1b);
            var result1b = systemUnderTest1b.Equals(systemUnderTest1a);

            var result2a = systemUnderTest2a.Equals(systemUnderTest2b);
            var result2b = systemUnderTest2b.Equals(systemUnderTest2a);

            var result3a = systemUnderTest3a.Equals(systemUnderTest3b);
            var result3b = systemUnderTest3b.Equals(systemUnderTest3a);

            var result4a = systemUnderTest4a.Equals(systemUnderTest4b);
            var result4b = systemUnderTest4b.Equals(systemUnderTest4a);

            var result5a = systemUnderTest5a.Equals(systemUnderTest5b);
            var result5b = systemUnderTest5b.Equals(systemUnderTest5a);

            // Assert
            result1a.Should().BeTrue();
            result1b.Should().BeTrue();

            result2a.Should().BeTrue();
            result2b.Should().BeTrue();

            result3a.Should().BeTrue();
            result3b.Should().BeTrue();

            result4a.Should().BeTrue();
            result4b.Should().BeTrue();

            result5a.Should().BeTrue();
            result5b.Should().BeTrue();
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
            var result = systemUnderTest1.Equals((object)systemUnderTest2);

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

            var result2a = systemUnderTest2a.Equals((object)systemUnderTest2b);
            var result2b = systemUnderTest2b.Equals((object)systemUnderTest2a);

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

            var systemUnderTest3a = new ReportingPeriod<GenericUnitOfTime>(A.Dummy<GenericUnbounded>(), A.Dummy<GenericUnbounded>());
            var systemUnderTest3b = (IReportingPeriod<GenericUnitOfTime>)new Common.ReportingPeriodTest<GenericUnitOfTime>(A.Dummy<GenericUnbounded>(), A.Dummy<GenericUnbounded>());

            // Act
            var result1a = systemUnderTest1a.Equals((object)systemUnderTest1b);
            var result1b = systemUnderTest1b.Equals((object)systemUnderTest1a);

            var result2a = systemUnderTest2a.Equals((object)systemUnderTest2b);
            var result2b = systemUnderTest2b.Equals((object)systemUnderTest2a);

            var result3a = systemUnderTest3a.Equals((object)systemUnderTest3b);
            var result3b = systemUnderTest3b.Equals((object)systemUnderTest3a);

            // Assert
            result1a.Should().BeFalse();
            result1b.Should().BeFalse();

            result2a.Should().BeFalse();
            result2b.Should().BeFalse();

            result3a.Should().BeFalse();
            result3b.Should().BeFalse();
        }

        [Fact]
        public static void Equals_with_object___Should_return_false___When_objects_being_compared_have_the_same_kind_of_unit_of_time_but_different_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<IReportingPeriod<CalendarDay>>();
            var systemUnderTest1b = A.Dummy<ReportingPeriod<CalendarDay>>();

            var systemUnderTest2a = A.Dummy<IReportingPeriod<FiscalQuarter>>();
            var systemUnderTest2b = new ReportingPeriod<FiscalQuarter>(systemUnderTest2a.Start, A.Dummy<FiscalQuarter>().ThatIs(q => (q != systemUnderTest2a.End) && (q >= systemUnderTest2a.Start)));

            var systemUnderTest3a = A.Dummy<IReportingPeriod<CalendarMonth>>();
            var systemUnderTest3b = new ReportingPeriod<CalendarMonth>(A.Dummy<CalendarMonth>().ThatIs(m => (m != systemUnderTest3a.Start) && (m <= systemUnderTest3a.End)), systemUnderTest3a.End);

            var systemUnderTest4a = (IReportingPeriod<CalendarUnitOfTime>)new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarUnbounded>(), A.Dummy<CalendarDay>());
            var systemUnderTest4b = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarUnbounded>(), A.Dummy<CalendarDay>());

            var systemUnderTest5a = (IReportingPeriod<CalendarUnitOfTime>)new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarDay>(), A.Dummy<CalendarUnbounded>());
            var systemUnderTest5b = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarDay>(), A.Dummy<CalendarUnbounded>());

            // Act
            var result1a = systemUnderTest1a.Equals((object)systemUnderTest1b);
            var result1b = systemUnderTest1b.Equals((object)systemUnderTest1a);

            var result2a = systemUnderTest2a.Equals((object)systemUnderTest2b);
            var result2b = systemUnderTest2b.Equals((object)systemUnderTest2a);

            var result3a = systemUnderTest3a.Equals((object)systemUnderTest3b);
            var result3b = systemUnderTest3b.Equals((object)systemUnderTest3a);

            var result4a = systemUnderTest4a.Equals((object)systemUnderTest4b);
            var result4b = systemUnderTest4b.Equals((object)systemUnderTest4a);

            var result5a = systemUnderTest5a.Equals((object)systemUnderTest5b);
            var result5b = systemUnderTest5b.Equals((object)systemUnderTest5a);

            // Assert
            result1a.Should().BeFalse();
            result1b.Should().BeFalse();

            result2a.Should().BeFalse();
            result2b.Should().BeFalse();

            result3a.Should().BeFalse();
            result3b.Should().BeFalse();

            result4a.Should().BeFalse();
            result4b.Should().BeFalse();

            result5a.Should().BeFalse();
            result5b.Should().BeFalse();
        }

        [Fact]
        public static void Equals_with_object___Should_return_false___When_objects_being_compared_have_different_but_comparable_kinds_of_unit_of_time_and_different_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<IReportingPeriod<FiscalQuarter>>();
            var systemUnderTest1b = new ReportingPeriod<FiscalUnitOfTime>(systemUnderTest1a.Start, A.Dummy<FiscalQuarter>().ThatIs(q => (q != systemUnderTest1a.End) && (q >= systemUnderTest1a.Start)));

            var systemUnderTest2a = A.Dummy<ReportingPeriod<CalendarMonth>>();
            var systemUnderTest2b = (IReportingPeriod<UnitOfTime>)new ReportingPeriod<UnitOfTime>(A.Dummy<CalendarMonth>().ThatIs(m => (m != systemUnderTest2a.Start) && (m <= systemUnderTest2a.End)), systemUnderTest2a.End);

            var systemUnderTest3a = (IReportingPeriod<CalendarUnitOfTime>)new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarUnbounded>(), A.Dummy<CalendarDay>());
            var systemUnderTest3b = new ReportingPeriod<UnitOfTime>(A.Dummy<CalendarUnbounded>(), A.Dummy<CalendarDay>());

            var systemUnderTest4a = (IReportingPeriod<UnitOfTime>)new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarDay>(), A.Dummy<CalendarUnbounded>());
            var systemUnderTest4b = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarDay>(), A.Dummy<CalendarUnbounded>());

            // Act
            var result1a = systemUnderTest1a.Equals((object)systemUnderTest1b);
            var result1b = systemUnderTest1b.Equals((object)systemUnderTest1a);

            var result2a = systemUnderTest2a.Equals((object)systemUnderTest2b);
            var result2b = systemUnderTest2b.Equals((object)systemUnderTest2a);

            var result3a = systemUnderTest3a.Equals((object)systemUnderTest3b);
            var result3b = systemUnderTest3b.Equals((object)systemUnderTest3a);

            var result4a = systemUnderTest4a.Equals((object)systemUnderTest4b);
            var result4b = systemUnderTest4b.Equals((object)systemUnderTest4a);

            // Assert
            result1a.Should().BeFalse();
            result1b.Should().BeFalse();

            result2a.Should().BeFalse();
            result2b.Should().BeFalse();

            result3a.Should().BeFalse();
            result3b.Should().BeFalse();

            result4a.Should().BeFalse();
            result4b.Should().BeFalse();
        }

        [Fact]
        public static void Equals_with_object___Should_return_false___When_objects_being_compared_have_different_and_not_comparable_units_of_time()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<IReportingPeriod<FiscalQuarter>>();
            var systemUnderTest1b = new ReportingPeriod<GenericQuarter>(systemUnderTest1a.Start.ToGenericQuarter(), systemUnderTest1a.End.ToGenericQuarter());

            var systemUnderTest2a = A.Dummy<ReportingPeriod<CalendarMonth>>();
            var systemUnderTest2b = (IReportingPeriod<GenericUnitOfTime>)new ReportingPeriod<GenericUnitOfTime>(systemUnderTest2a.Start.ToGenericMonth(), systemUnderTest2a.End.ToGenericMonth());

            var systemUnderTest3a = (IReportingPeriod<CalendarUnitOfTime>)new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarUnbounded>(), A.Dummy<CalendarDay>());
            var systemUnderTest3b = new ReportingPeriod<GenericUnitOfTime>(A.Dummy<GenericUnbounded>(), A.Dummy<GenericMonth>());

            var systemUnderTest4a = (IReportingPeriod<UnitOfTime>)new ReportingPeriod<GenericUnitOfTime>(A.Dummy<GenericMonth>(), A.Dummy<GenericUnbounded>());
            var systemUnderTest4b = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarDay>(), A.Dummy<CalendarUnbounded>());

            // Act
            var result1a = systemUnderTest1a.Equals((object)systemUnderTest1b);
            var result1b = systemUnderTest1b.Equals((object)systemUnderTest1a);

            var result2a = systemUnderTest2a.Equals((object)systemUnderTest2b);
            var result2b = systemUnderTest2b.Equals((object)systemUnderTest2a);

            var result3a = systemUnderTest3a.Equals((object)systemUnderTest3b);
            var result3b = systemUnderTest3b.Equals((object)systemUnderTest3a);

            var result4a = systemUnderTest4a.Equals((object)systemUnderTest4b);
            var result4b = systemUnderTest4b.Equals((object)systemUnderTest4a);

            // Assert
            result1a.Should().BeFalse();
            result1b.Should().BeFalse();

            result2a.Should().BeFalse();
            result2b.Should().BeFalse();

            result3a.Should().BeFalse();
            result3b.Should().BeFalse();

            result4a.Should().BeFalse();
            result4b.Should().BeFalse();
        }

        [Fact]
        public static void Equals_with_object___Should_return_true___When_objects_being_compared_have_the_same_kind_of_unit_of_time_and_same_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<IReportingPeriod<UnitOfTime>>();
            var systemUnderTest1b = new ReportingPeriod<UnitOfTime>(systemUnderTest1a.Start, systemUnderTest1a.End);

            var systemUnderTest2a = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarDay>(), A.Dummy<CalendarUnbounded>());
            var systemUnderTest2b = (IReportingPeriod<CalendarUnitOfTime>)new ReportingPeriod<CalendarUnitOfTime>(systemUnderTest2a.Start, A.Dummy<CalendarUnbounded>());

            var systemUnderTest3a = (IReportingPeriod<CalendarUnitOfTime>)new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarUnbounded>(), A.Dummy<CalendarDay>());
            var systemUnderTest3b = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarUnbounded>(), systemUnderTest3a.End);

            var systemUnderTest4a = new ReportingPeriod<UnitOfTime>(A.Dummy<FiscalUnbounded>(), A.Dummy<FiscalUnbounded>());
            var systemUnderTest4b = (IReportingPeriod<UnitOfTime>)new ReportingPeriod<UnitOfTime>(A.Dummy<FiscalUnbounded>(), A.Dummy<FiscalUnbounded>());

            // Act
            var result1a = systemUnderTest1a.Equals((object)systemUnderTest1b);
            var result1b = systemUnderTest1b.Equals((object)systemUnderTest1a);

            var result2a = systemUnderTest2a.Equals((object)systemUnderTest2b);
            var result2b = systemUnderTest2b.Equals((object)systemUnderTest2a);

            var result3a = systemUnderTest3a.Equals((object)systemUnderTest3b);
            var result3b = systemUnderTest3b.Equals((object)systemUnderTest3a);

            var result4a = systemUnderTest4a.Equals((object)systemUnderTest4b);
            var result4b = systemUnderTest4b.Equals((object)systemUnderTest4a);

            // Assert
            result1a.Should().BeTrue();
            result1b.Should().BeTrue();

            result2a.Should().BeTrue();
            result2b.Should().BeTrue();

            result3a.Should().BeTrue();
            result3b.Should().BeTrue();

            result4a.Should().BeTrue();
            result4b.Should().BeTrue();
        }

        [Fact]
        public static void Equals_with_object___Should_return_true___When_When_objects_being_compared_have_different_but_comparable_kinds_of_unit_of_time_and_same_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<IReportingPeriod<CalendarDay>>();
            var systemUnderTest1b = new ReportingPeriod<CalendarUnitOfTime>(systemUnderTest1a.Start, systemUnderTest1a.End);

            var systemUnderTest2a = A.Dummy<ReportingPeriod<GenericQuarter>>();
            var systemUnderTest2b = (IReportingPeriod<GenericUnitOfTime>)new ReportingPeriod<GenericQuarter>(systemUnderTest2a.Start, systemUnderTest2a.End);

            var systemUnderTest3a = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarDay>(), A.Dummy<CalendarUnbounded>());
            var systemUnderTest3b = (IReportingPeriod<UnitOfTime>)new ReportingPeriod<CalendarUnitOfTime>(systemUnderTest3a.Start, A.Dummy<CalendarUnbounded>());

            var systemUnderTest4a = (IReportingPeriod<CalendarUnitOfTime>)new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarUnbounded>(), A.Dummy<CalendarDay>());
            var systemUnderTest4b = new ReportingPeriod<UnitOfTime>(A.Dummy<CalendarUnbounded>(), systemUnderTest4a.End);

            var systemUnderTest5a = new ReportingPeriod<FiscalUnbounded>(A.Dummy<FiscalUnbounded>(), A.Dummy<FiscalUnbounded>());
            var systemUnderTest5b = (IReportingPeriod<FiscalUnitOfTime>)new ReportingPeriod<FiscalUnitOfTime>(A.Dummy<FiscalUnbounded>(), A.Dummy<FiscalUnbounded>());

            // Act
            var result1a = systemUnderTest1a.Equals((object)systemUnderTest1b);
            var result1b = systemUnderTest1b.Equals((object)systemUnderTest1a);

            var result2a = systemUnderTest2a.Equals((object)systemUnderTest2b);
            var result2b = systemUnderTest2b.Equals((object)systemUnderTest2a);

            var result3a = systemUnderTest3a.Equals((object)systemUnderTest3b);
            var result3b = systemUnderTest3b.Equals((object)systemUnderTest3a);

            var result4a = systemUnderTest4a.Equals((object)systemUnderTest4b);
            var result4b = systemUnderTest4b.Equals((object)systemUnderTest4a);

            var result5a = systemUnderTest5a.Equals((object)systemUnderTest5b);
            var result5b = systemUnderTest5b.Equals((object)systemUnderTest5a);

            // Assert
            result1a.Should().BeTrue();
            result1b.Should().BeTrue();

            result2a.Should().BeTrue();
            result2b.Should().BeTrue();

            result3a.Should().BeTrue();
            result3b.Should().BeTrue();

            result4a.Should().BeTrue();
            result4b.Should().BeTrue();

            result5a.Should().BeTrue();
            result5b.Should().BeTrue();
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

            var systemUnderTest4a = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarUnbounded>(), A.Dummy<CalendarDay>());
            var systemUnderTest4b = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarUnbounded>(), A.Dummy<CalendarDay>());

            var systemUnderTest5a = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarDay>(), A.Dummy<CalendarUnbounded>());
            var systemUnderTest5b = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarDay>(), A.Dummy<CalendarUnbounded>());

            // Act
            var hash1a = systemUnderTest1a.GetHashCode();
            var hash1b = systemUnderTest1b.GetHashCode();

            var hash2a = systemUnderTest2a.GetHashCode();
            var hash2b = systemUnderTest2b.GetHashCode();

            var hash3a = systemUnderTest3a.GetHashCode();
            var hash3b = systemUnderTest3b.GetHashCode();

            var hash4a = systemUnderTest4a.GetHashCode();
            var hash4b = systemUnderTest4b.GetHashCode();

            var hash5a = systemUnderTest5a.GetHashCode();
            var hash5b = systemUnderTest5b.GetHashCode();

            // Assert
            hash1a.Should().NotBe(hash1b);
            hash2a.Should().NotBe(hash2b);
            hash3a.Should().NotBe(hash3b);
            hash4a.Should().NotBe(hash4b);
            hash5a.Should().NotBe(hash5b);
        }

        [Fact]
        public static void GetHashCode___Should_be_equal_for_two_ReportingPeriod___When_both_objects_have_the_same_property_values()
        {
            // Arrange
            var systemUnderTest1a = A.Dummy<ReportingPeriod<UnitOfTime>>();
            var systemUnderTest1b = new ReportingPeriod<UnitOfTime>(systemUnderTest1a.Start, systemUnderTest1a.End);

            var systemUnderTest2a = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarDay>(), A.Dummy<CalendarUnbounded>());
            var systemUnderTest2b = new ReportingPeriod<CalendarUnitOfTime>(systemUnderTest2a.Start, A.Dummy<CalendarUnbounded>());

            var systemUnderTest3a = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarUnbounded>(), A.Dummy<CalendarDay>());
            var systemUnderTest3b = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarUnbounded>(), systemUnderTest3a.End);

            var systemUnderTest4a = new ReportingPeriod<UnitOfTime>(A.Dummy<FiscalUnbounded>(), A.Dummy<FiscalUnbounded>());
            var systemUnderTest4b = new ReportingPeriod<UnitOfTime>(A.Dummy<FiscalUnbounded>(), A.Dummy<FiscalUnbounded>());

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
            hash1a.Should().Be(hash1b);
            hash2a.Should().Be(hash2b);
            hash3a.Should().Be(hash3b);
            hash4a.Should().Be(hash4b);
        }

        [Fact]
        public static void GetHashCode___Should_not_be_equal_for_two_ReportingPeriod___When_they_are_of_different_kinds_but_have_the_same_granularity_and_same_property_values()
        {
            // Arrange
            var calendarUnbounded = A.Dummy<ReportingPeriod<CalendarUnbounded>>();
            var fiscalUnbounded = A.Dummy<ReportingPeriod<FiscalUnbounded>>();
            var genericUnbounded = A.Dummy<ReportingPeriod<GenericUnbounded>>();

            var calendarYear = A.Dummy<ReportingPeriod<CalendarYear>>();
            var fiscalYear = new ReportingPeriod<FiscalYear>(new FiscalYear(calendarYear.Start.Year), new FiscalYear(calendarYear.End.Year));
            var genericYear = new ReportingPeriod<GenericYear>(new GenericYear(calendarYear.Start.Year), new GenericYear(calendarYear.End.Year));

            var calendarQuarter = A.Dummy<ReportingPeriod<CalendarQuarter>>();
            var fiscalQuarter = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(calendarQuarter.Start.Year, calendarQuarter.Start.QuarterNumber), new FiscalQuarter(calendarQuarter.End.Year, calendarQuarter.End.QuarterNumber));
            var genericQuarter = new ReportingPeriod<GenericQuarter>(new GenericQuarter(calendarQuarter.Start.Year, calendarQuarter.Start.QuarterNumber), new GenericQuarter(calendarQuarter.End.Year, calendarQuarter.End.QuarterNumber));

            var calendarMonth = A.Dummy<ReportingPeriod<CalendarMonth>>();
            var fiscalMonth = new ReportingPeriod<FiscalMonth>(new FiscalMonth(calendarMonth.Start.Year, calendarMonth.Start.MonthNumber), new FiscalMonth(calendarMonth.End.Year, calendarMonth.End.MonthNumber));
            var genericMonth = new ReportingPeriod<GenericMonth>(new GenericMonth(calendarMonth.Start.Year, calendarMonth.Start.MonthNumber), new GenericMonth(calendarMonth.End.Year, calendarMonth.End.MonthNumber));

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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Need to test all units-of-time")]
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
                A.Dummy<ReportingPeriod<CalendarUnbounded>>(),
                A.Dummy<ReportingPeriod<FiscalMonth>>(),
                A.Dummy<ReportingPeriod<FiscalQuarter>>(),
                A.Dummy<ReportingPeriod<FiscalYear>>(),
                A.Dummy<ReportingPeriod<FiscalUnbounded>>(),
                A.Dummy<ReportingPeriod<GenericMonth>>(),
                A.Dummy<ReportingPeriod<GenericQuarter>>(),
                A.Dummy<ReportingPeriod<GenericYear>>(),
                A.Dummy<ReportingPeriod<GenericUnbounded>>(),
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
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<CalendarUnbounded>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<CalendarUnbounded>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<FiscalMonth>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<FiscalMonth>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<FiscalQuarter>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<FiscalQuarter>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<FiscalYear>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<FiscalYear>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<FiscalUnbounded>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<FiscalUnbounded>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<GenericMonth>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<GenericMonth>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<GenericQuarter>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<GenericQuarter>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<GenericYear>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<GenericYear>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<GenericUnbounded>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<GenericUnbounded>) },
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
                A.Dummy<ReportingPeriod<CalendarUnbounded>>(),
                A.Dummy<ReportingPeriod<FiscalMonth>>(),
                A.Dummy<ReportingPeriod<FiscalQuarter>>(),
                A.Dummy<ReportingPeriod<FiscalYear>>(),
                A.Dummy<ReportingPeriod<FiscalUnbounded>>(),
                A.Dummy<ReportingPeriod<GenericMonth>>(),
                A.Dummy<ReportingPeriod<GenericQuarter>>(),
                A.Dummy<ReportingPeriod<GenericYear>>(),
                A.Dummy<ReportingPeriod<GenericUnbounded>>(),
            };

            // Act
            var exceptions = new List<Exception>();
            foreach (var reportingPeriod in reportingPeriods)
            {
                foreach (var type in allTypes)
                {
                    var cloneMethod = reportingPeriod.GetType().GetMethods().Single(_ => (_.Name == nameof(ReportingPeriod<UnitOfTime>.Clone)) && _.IsGenericMethod);
                    var genericMethod = cloneMethod.MakeGenericMethod(type.ReportingPeriodType);
                    exceptions.Add(Record.Exception(() => genericMethod.Invoke(reportingPeriod, null)).InnerException);
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
                { A.Dummy<ReportingPeriod<GenericYear>>(), typeof(ReportingPeriod<FiscalYear>) },
                { A.Dummy<ReportingPeriod<GenericUnbounded>>(), typeof(ReportingPeriod<FiscalUnbounded>) },
                { A.Dummy<ReportingPeriod<CalendarUnitOfTime>>(), typeof(ReportingPeriod<GenericUnbounded>) },
                { A.Dummy<ReportingPeriod<CalendarMonth>>(), typeof(ReportingPeriod<GenericUnbounded>) },
                { A.Dummy<ReportingPeriod<CalendarUnbounded>>(), typeof(ReportingPeriod<FiscalQuarter>) },
                { A.Dummy<ReportingPeriod<CalendarUnbounded>>(), typeof(ReportingPeriod<FiscalUnitOfTime>) },
            };

            // Act
            var exceptions = new List<Exception>();
            foreach (var reportingPeriod in reportingPeriods)
            {
                var cloneMethod = reportingPeriod.Item1.GetType().GetMethods().Single(_ => (_.Name == nameof(ReportingPeriod<UnitOfTime>.Clone)) && _.IsGenericMethod);
                var genericMethod = cloneMethod.MakeGenericMethod(reportingPeriod.Item2);
                exceptions.Add(Record.Exception(() => genericMethod.Invoke(reportingPeriod.Item1, null)).InnerException);
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
                reportingPeriod.Clone<ReportingPeriod<CalendarMonth>>(),
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
                reportingPeriod.Clone<ReportingPeriod<CalendarQuarter>>(),
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
                reportingPeriod.Clone<ReportingPeriod<CalendarYear>>(),
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
        public static void Clone_with_type_parameter___Should_deep_clone_into_various_flavors_of_IReportingPeriod___When_reporting_period_is_a_CalendarUnbounded()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod<CalendarUnbounded>>();

            // Act
            var deserialized = new List<IReportingPeriod<UnitOfTime>>
            {
                reportingPeriod.Clone<IReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<CalendarUnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<CalendarUnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<CalendarUnbounded>>(),
                reportingPeriod.Clone<ReportingPeriod<CalendarUnbounded>>(),
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
        public static void Clone_with_type_parameter___Should_deep_clone_into_various_flavors_of_IReportingPeriod___When_reporting_period_is_a_CalendarUnitOfTime()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarUnitOfTime>(new CalendarUnbounded(), A.Dummy<CalendarQuarter>());
            var reportingPeriod2 = new ReportingPeriod<CalendarUnitOfTime>(A.Dummy<CalendarDay>(), new CalendarUnbounded());
            var reportingPeriod3 = new ReportingPeriod<CalendarUnitOfTime>(new CalendarUnbounded(), new CalendarUnbounded());

            // Act
            var deserialized1 = new List<IReportingPeriod<UnitOfTime>>
            {
                reportingPeriod1.Clone<IReportingPeriod<UnitOfTime>>(),
                reportingPeriod1.Clone<ReportingPeriod<UnitOfTime>>(),
                reportingPeriod1.Clone<IReportingPeriod<CalendarUnitOfTime>>(),
                reportingPeriod1.Clone<ReportingPeriod<CalendarUnitOfTime>>(),
            };

            var deserialized2 = new List<IReportingPeriod<UnitOfTime>>
            {
                reportingPeriod2.Clone<IReportingPeriod<UnitOfTime>>(),
                reportingPeriod2.Clone<ReportingPeriod<UnitOfTime>>(),
                reportingPeriod2.Clone<IReportingPeriod<CalendarUnitOfTime>>(),
                reportingPeriod2.Clone<ReportingPeriod<CalendarUnitOfTime>>(),
            };

            var deserialized3 = new List<IReportingPeriod<UnitOfTime>>
            {
                reportingPeriod3.Clone<IReportingPeriod<UnitOfTime>>(),
                reportingPeriod3.Clone<ReportingPeriod<UnitOfTime>>(),
                reportingPeriod3.Clone<IReportingPeriod<CalendarUnitOfTime>>(),
                reportingPeriod3.Clone<ReportingPeriod<CalendarUnitOfTime>>(),
            };

            // Assert
            deserialized1.ForEach(_ =>
            {
                _.Should().Be(reportingPeriod1).And.NotBeSameAs(reportingPeriod1);
                _.Start.Should().NotBeSameAs(reportingPeriod1.Start);
                _.End.Should().NotBeSameAs(reportingPeriod1.End);
            });

            deserialized2.ForEach(_ =>
            {
                _.Should().Be(reportingPeriod2).And.NotBeSameAs(reportingPeriod2);
                _.Start.Should().NotBeSameAs(reportingPeriod2.Start);
                _.End.Should().NotBeSameAs(reportingPeriod2.End);
            });

            deserialized3.ForEach(_ =>
            {
                _.Should().Be(reportingPeriod3).And.NotBeSameAs(reportingPeriod3);
                _.Start.Should().NotBeSameAs(reportingPeriod3.Start);
                _.End.Should().NotBeSameAs(reportingPeriod3.End);
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
                reportingPeriod.Clone<ReportingPeriod<FiscalQuarter>>(),
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
                reportingPeriod.Clone<ReportingPeriod<FiscalYear>>(),
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
        public static void Clone_with_type_parameter___Should_deep_clone_into_various_flavors_of_IReportingPeriod___When_reporting_period_is_a_FiscalUnbounded()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod<FiscalUnbounded>>();

            // Act
            var deserialized = new List<IReportingPeriod<UnitOfTime>>
            {
                reportingPeriod.Clone<IReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<FiscalUnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<FiscalUnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<FiscalUnbounded>>(),
                reportingPeriod.Clone<ReportingPeriod<FiscalUnbounded>>(),
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
        public static void Clone_with_type_parameter___Should_deep_clone_into_various_flavors_of_IReportingPeriod___When_reporting_period_is_a_FiscalUnitOfTime()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalUnitOfTime>(new FiscalUnbounded(), A.Dummy<FiscalQuarter>());
            var reportingPeriod2 = new ReportingPeriod<FiscalUnitOfTime>(A.Dummy<FiscalMonth>(), new FiscalUnbounded());
            var reportingPeriod3 = new ReportingPeriod<FiscalUnitOfTime>(new FiscalUnbounded(), new FiscalUnbounded());

            // Act
            var deserialized1 = new List<IReportingPeriod<UnitOfTime>>
            {
                reportingPeriod1.Clone<IReportingPeriod<UnitOfTime>>(),
                reportingPeriod1.Clone<ReportingPeriod<UnitOfTime>>(),
                reportingPeriod1.Clone<IReportingPeriod<FiscalUnitOfTime>>(),
                reportingPeriod1.Clone<ReportingPeriod<FiscalUnitOfTime>>(),
            };

            var deserialized2 = new List<IReportingPeriod<UnitOfTime>>
            {
                reportingPeriod2.Clone<IReportingPeriod<UnitOfTime>>(),
                reportingPeriod2.Clone<ReportingPeriod<UnitOfTime>>(),
                reportingPeriod2.Clone<IReportingPeriod<FiscalUnitOfTime>>(),
                reportingPeriod2.Clone<ReportingPeriod<FiscalUnitOfTime>>(),
            };

            var deserialized3 = new List<IReportingPeriod<UnitOfTime>>
            {
                reportingPeriod3.Clone<IReportingPeriod<UnitOfTime>>(),
                reportingPeriod3.Clone<ReportingPeriod<UnitOfTime>>(),
                reportingPeriod3.Clone<IReportingPeriod<FiscalUnitOfTime>>(),
                reportingPeriod3.Clone<ReportingPeriod<FiscalUnitOfTime>>(),
            };

            // Assert
            deserialized1.ForEach(_ =>
            {
                _.Should().Be(reportingPeriod1).And.NotBeSameAs(reportingPeriod1);
                _.Start.Should().NotBeSameAs(reportingPeriod1.Start);
                _.End.Should().NotBeSameAs(reportingPeriod1.End);
            });

            deserialized2.ForEach(_ =>
            {
                _.Should().Be(reportingPeriod2).And.NotBeSameAs(reportingPeriod2);
                _.Start.Should().NotBeSameAs(reportingPeriod2.Start);
                _.End.Should().NotBeSameAs(reportingPeriod2.End);
            });

            deserialized3.ForEach(_ =>
            {
                _.Should().Be(reportingPeriod3).And.NotBeSameAs(reportingPeriod3);
                _.Start.Should().NotBeSameAs(reportingPeriod3.Start);
                _.End.Should().NotBeSameAs(reportingPeriod3.End);
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
                reportingPeriod.Clone<ReportingPeriod<GenericMonth>>(),
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
                reportingPeriod.Clone<ReportingPeriod<GenericQuarter>>(),
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
                reportingPeriod.Clone<ReportingPeriod<GenericYear>>(),
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
        public static void Clone_with_type_parameter___Should_deep_clone_into_various_flavors_of_IReportingPeriod___When_reporting_period_is_a_GenericUnbounded()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod<GenericUnbounded>>();

            // Act
            var deserialized = new List<IReportingPeriod<UnitOfTime>>
            {
                reportingPeriod.Clone<IReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<GenericUnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<GenericUnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<GenericUnbounded>>(),
                reportingPeriod.Clone<ReportingPeriod<GenericUnbounded>>(),
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
        public static void Clone_with_type_parameter___Should_deep_clone_into_various_flavors_of_IReportingPeriod___When_reporting_period_is_a_GenericUnitOfTime()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericUnitOfTime>(new GenericUnbounded(), A.Dummy<GenericQuarter>());
            var reportingPeriod2 = new ReportingPeriod<GenericUnitOfTime>(A.Dummy<GenericYear>(), new GenericUnbounded());
            var reportingPeriod3 = new ReportingPeriod<GenericUnitOfTime>(new GenericUnbounded(), new GenericUnbounded());

            // Act
            var deserialized1 = new List<IReportingPeriod<UnitOfTime>>
            {
                reportingPeriod1.Clone<IReportingPeriod<UnitOfTime>>(),
                reportingPeriod1.Clone<ReportingPeriod<UnitOfTime>>(),
                reportingPeriod1.Clone<IReportingPeriod<GenericUnitOfTime>>(),
                reportingPeriod1.Clone<ReportingPeriod<GenericUnitOfTime>>(),
            };

            var deserialized2 = new List<IReportingPeriod<UnitOfTime>>
            {
                reportingPeriod2.Clone<IReportingPeriod<UnitOfTime>>(),
                reportingPeriod2.Clone<ReportingPeriod<UnitOfTime>>(),
                reportingPeriod2.Clone<IReportingPeriod<GenericUnitOfTime>>(),
                reportingPeriod2.Clone<ReportingPeriod<GenericUnitOfTime>>(),
            };

            var deserialized3 = new List<IReportingPeriod<UnitOfTime>>
            {
                reportingPeriod3.Clone<IReportingPeriod<UnitOfTime>>(),
                reportingPeriod3.Clone<ReportingPeriod<UnitOfTime>>(),
                reportingPeriod3.Clone<IReportingPeriod<GenericUnitOfTime>>(),
                reportingPeriod3.Clone<ReportingPeriod<GenericUnitOfTime>>(),
            };

            // Assert
            deserialized1.ForEach(_ =>
            {
                _.Should().Be(reportingPeriod1).And.NotBeSameAs(reportingPeriod1);
                _.Start.Should().NotBeSameAs(reportingPeriod1.Start);
                _.End.Should().NotBeSameAs(reportingPeriod1.End);
            });

            deserialized2.ForEach(_ =>
            {
                _.Should().Be(reportingPeriod2).And.NotBeSameAs(reportingPeriod2);
                _.Start.Should().NotBeSameAs(reportingPeriod2.Start);
                _.End.Should().NotBeSameAs(reportingPeriod2.End);
            });

            deserialized3.ForEach(_ =>
            {
                _.Should().Be(reportingPeriod3).And.NotBeSameAs(reportingPeriod3);
                _.Start.Should().NotBeSameAs(reportingPeriod3.Start);
                _.End.Should().NotBeSameAs(reportingPeriod3.End);
            });
        }
    }
}
