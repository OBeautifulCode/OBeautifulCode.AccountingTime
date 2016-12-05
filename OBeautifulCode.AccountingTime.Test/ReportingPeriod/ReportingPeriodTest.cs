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

    using FakeItEasy;

    using FluentAssertions;

    using Naos.Recipes.TupleInitializers;

    using Xunit;

    public static class ReportingPeriodTest
    {
        // ReSharper disable InconsistentNaming
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
                A.Dummy<ReportingPeriodInclusive<UnitOfTime>>(),
                A.Dummy<ReportingPeriodInclusive<CalendarUnitOfTime>>(),
                A.Dummy<ReportingPeriodInclusive<FiscalUnitOfTime>>(),
                A.Dummy<ReportingPeriodInclusive<GenericUnitOfTime>>(),
                A.Dummy<ReportingPeriodInclusive<CalendarDay>>(),
                A.Dummy<ReportingPeriodInclusive<CalendarMonth>>(),
                A.Dummy<ReportingPeriodInclusive<CalendarQuarter>>(),
                A.Dummy<ReportingPeriodInclusive<CalendarYear>>(),
                A.Dummy<ReportingPeriodInclusive<FiscalMonth>>(),
                A.Dummy<ReportingPeriodInclusive<FiscalQuarter>>(),
                A.Dummy<ReportingPeriodInclusive<FiscalYear>>(),
                A.Dummy<ReportingPeriodInclusive<GenericMonth>>(),
                A.Dummy<ReportingPeriodInclusive<GenericQuarter>>(),
                A.Dummy<ReportingPeriodInclusive<GenericYear>>(),
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
                { A.Dummy<ReportingPeriodInclusive<CalendarUnitOfTime>>(), typeof(ReportingPeriodInclusive<FiscalUnitOfTime>) },
                { A.Dummy<ReportingPeriodInclusive<FiscalUnitOfTime>>(), typeof(ReportingPeriodInclusive<GenericUnitOfTime>) },
                { A.Dummy<ReportingPeriodInclusive<GenericUnitOfTime>>(), typeof(ReportingPeriodInclusive<CalendarUnitOfTime>) },
                { A.Dummy<ReportingPeriodInclusive<CalendarDay>>(), typeof(ReportingPeriodInclusive<FiscalMonth>) },
                { A.Dummy<ReportingPeriodInclusive<CalendarMonth>>(), typeof(ReportingPeriodInclusive<CalendarDay>) },
                { A.Dummy<ReportingPeriodInclusive<CalendarQuarter>>(), typeof(ReportingPeriodInclusive<FiscalQuarter>) },
                { A.Dummy<ReportingPeriodInclusive<CalendarYear>>(), typeof(ReportingPeriodInclusive<GenericQuarter>) },
                { A.Dummy<ReportingPeriodInclusive<FiscalMonth>>(), typeof(ReportingPeriodInclusive<CalendarMonth>) },
                { A.Dummy<ReportingPeriodInclusive<FiscalQuarter>>(), typeof(ReportingPeriodInclusive<FiscalMonth>) },
                { A.Dummy<ReportingPeriodInclusive<FiscalYear>>(), typeof(ReportingPeriodInclusive<FiscalQuarter>) },
                { A.Dummy<ReportingPeriodInclusive<GenericMonth>>(), typeof(ReportingPeriodInclusive<GenericYear>) },
                { A.Dummy<ReportingPeriodInclusive<GenericQuarter>>(), typeof(ReportingPeriodInclusive<GenericYear>) },
                { A.Dummy<ReportingPeriodInclusive<GenericYear>>(), typeof(ReportingPeriodInclusive<FiscalYear>) }
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
            var reportingPeriod = A.Dummy<ReportingPeriodInclusive<CalendarDay>>();

            // Act
            var deserialized = new List<IReportingPeriod<UnitOfTime>>
            {
                reportingPeriod.Clone<IReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriodInclusive<UnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriodInclusive<UnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<CalendarUnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriodInclusive<CalendarUnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<CalendarUnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriodInclusive<CalendarUnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<CalendarDay>>(),
                reportingPeriod.Clone<IReportingPeriodInclusive<CalendarDay>>(),
                reportingPeriod.Clone<ReportingPeriod<CalendarDay>>(),
                reportingPeriod.Clone<ReportingPeriodInclusive<CalendarDay>>(),
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
            var reportingPeriod = A.Dummy<ReportingPeriodInclusive<CalendarMonth>>();

            // Act
            var deserialized = new List<IReportingPeriod<UnitOfTime>>
            {
                reportingPeriod.Clone<IReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriodInclusive<UnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriodInclusive<UnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<CalendarUnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriodInclusive<CalendarUnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<CalendarUnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriodInclusive<CalendarUnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<CalendarMonth>>(),
                reportingPeriod.Clone<IReportingPeriodInclusive<CalendarMonth>>(),
                reportingPeriod.Clone<ReportingPeriod<CalendarMonth>>(),
                reportingPeriod.Clone<ReportingPeriodInclusive<CalendarMonth>>(),
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
            var reportingPeriod = A.Dummy<ReportingPeriodInclusive<CalendarQuarter>>();

            // Act
            var deserialized = new List<IReportingPeriod<UnitOfTime>>
            {
                reportingPeriod.Clone<IReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriodInclusive<UnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriodInclusive<UnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<CalendarUnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriodInclusive<CalendarUnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<CalendarUnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriodInclusive<CalendarUnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<CalendarQuarter>>(),
                reportingPeriod.Clone<IReportingPeriodInclusive<CalendarQuarter>>(),
                reportingPeriod.Clone<ReportingPeriod<CalendarQuarter>>(),
                reportingPeriod.Clone<ReportingPeriodInclusive<CalendarQuarter>>(),
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
            var reportingPeriod = A.Dummy<ReportingPeriodInclusive<CalendarYear>>();

            // Act
            var deserialized = new List<IReportingPeriod<UnitOfTime>>
            {
                reportingPeriod.Clone<IReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriodInclusive<UnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriodInclusive<UnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<CalendarUnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriodInclusive<CalendarUnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<CalendarUnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriodInclusive<CalendarUnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<CalendarYear>>(),
                reportingPeriod.Clone<IReportingPeriodInclusive<CalendarYear>>(),
                reportingPeriod.Clone<ReportingPeriod<CalendarYear>>(),
                reportingPeriod.Clone<ReportingPeriodInclusive<CalendarYear>>(),
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
            var reportingPeriod = A.Dummy<ReportingPeriodInclusive<FiscalMonth>>();

            // Act
            var deserialized = new List<IReportingPeriod<UnitOfTime>>
            {
                reportingPeriod.Clone<IReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriodInclusive<UnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriodInclusive<UnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<FiscalUnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriodInclusive<FiscalUnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<FiscalUnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriodInclusive<FiscalUnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<FiscalMonth>>(),
                reportingPeriod.Clone<IReportingPeriodInclusive<FiscalMonth>>(),
                reportingPeriod.Clone<ReportingPeriod<FiscalMonth>>(),
                reportingPeriod.Clone<ReportingPeriodInclusive<FiscalMonth>>(),
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
            var reportingPeriod = A.Dummy<ReportingPeriodInclusive<FiscalQuarter>>();

            // Act
            var deserialized = new List<IReportingPeriod<UnitOfTime>>
            {
                reportingPeriod.Clone<IReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriodInclusive<UnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriodInclusive<UnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<FiscalUnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriodInclusive<FiscalUnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<FiscalUnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriodInclusive<FiscalUnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<FiscalQuarter>>(),
                reportingPeriod.Clone<IReportingPeriodInclusive<FiscalQuarter>>(),
                reportingPeriod.Clone<ReportingPeriod<FiscalQuarter>>(),
                reportingPeriod.Clone<ReportingPeriodInclusive<FiscalQuarter>>(),
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
            var reportingPeriod = A.Dummy<ReportingPeriodInclusive<FiscalYear>>();

            // Act
            var deserialized = new List<IReportingPeriod<UnitOfTime>>
            {
                reportingPeriod.Clone<IReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriodInclusive<UnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriodInclusive<UnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<FiscalUnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriodInclusive<FiscalUnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<FiscalUnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriodInclusive<FiscalUnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<FiscalYear>>(),
                reportingPeriod.Clone<IReportingPeriodInclusive<FiscalYear>>(),
                reportingPeriod.Clone<ReportingPeriod<FiscalYear>>(),
                reportingPeriod.Clone<ReportingPeriodInclusive<FiscalYear>>(),
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
            var reportingPeriod = A.Dummy<ReportingPeriodInclusive<GenericMonth>>();

            // Act
            var deserialized = new List<IReportingPeriod<UnitOfTime>>
            {
                reportingPeriod.Clone<IReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriodInclusive<UnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriodInclusive<UnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<GenericUnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriodInclusive<GenericUnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<GenericUnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriodInclusive<GenericUnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<GenericMonth>>(),
                reportingPeriod.Clone<IReportingPeriodInclusive<GenericMonth>>(),
                reportingPeriod.Clone<ReportingPeriod<GenericMonth>>(),
                reportingPeriod.Clone<ReportingPeriodInclusive<GenericMonth>>(),
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
            var reportingPeriod = A.Dummy<ReportingPeriodInclusive<GenericQuarter>>();

            // Act
            var deserialized = new List<IReportingPeriod<UnitOfTime>>
            {
                reportingPeriod.Clone<IReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriodInclusive<UnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriodInclusive<UnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<GenericUnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriodInclusive<GenericUnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<GenericUnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriodInclusive<GenericUnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<GenericQuarter>>(),
                reportingPeriod.Clone<IReportingPeriodInclusive<GenericQuarter>>(),
                reportingPeriod.Clone<ReportingPeriod<GenericQuarter>>(),
                reportingPeriod.Clone<ReportingPeriodInclusive<GenericQuarter>>(),
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
            var reportingPeriod = A.Dummy<ReportingPeriodInclusive<GenericYear>>();

            // Act
            var deserialized = new List<IReportingPeriod<UnitOfTime>>
            {
                reportingPeriod.Clone<IReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriodInclusive<UnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<UnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriodInclusive<UnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<GenericUnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriodInclusive<GenericUnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriod<GenericUnitOfTime>>(),
                reportingPeriod.Clone<ReportingPeriodInclusive<GenericUnitOfTime>>(),
                reportingPeriod.Clone<IReportingPeriod<GenericYear>>(),
                reportingPeriod.Clone<IReportingPeriodInclusive<GenericYear>>(),
                reportingPeriod.Clone<ReportingPeriod<GenericYear>>(),
                reportingPeriod.Clone<ReportingPeriodInclusive<GenericYear>>(),
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