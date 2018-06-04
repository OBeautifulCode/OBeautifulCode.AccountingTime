// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriodExtensionsTest.Serialization.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Xunit;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Testing this class requires lots of types because of the number of unit-of-time types intersected with the options for reporting period.")]
    public static partial class ReportingPeriodExtensionsTest
    {
        [Fact]
        public static void DeserializeFromString_TReportingPeriod___Should_throw_ArgumentNullException___When_parameter_unitOfTime_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.DeserializeFromString<IReportingPeriod<UnitOfTime>>(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void DeserializeFromString_TReportingPeriod___Should_throw_ArgumentException___When_parameter_unitOfTime_is_white_space()
        {
            // Arrange
            var reportingPeriods = new[] { string.Empty, "  ", "  \r\n " };

            // Act
            var exceptions = reportingPeriods.Select(_ => Record.Exception(() => _.DeserializeFromString<IReportingPeriod<UnitOfTime>>())).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<ArgumentException>());
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Thoroughly checking this test-case requires lots of types.")]
        public static void DeserializeFromString_TReportingPeriod___Should_throw_InvalidOperationException___When_the_return_type_cannot_be_assigned_to_a_ReportingPeriod()
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

            var reportingPeriods = new[]
            {
                "c-2015-11-11,c-2016-11-11",
                "c-2017-03,c-2017-04",
                "f-2017-03,f-2017-04",
                "g-2017-03,g-2017-04",
                "c-2017-Q3,c-2017-Q4",
                "f-2017-Q3,f-2017-Q4",
                "g-2017-Q3,g-2017-Q4",
                "c-2017,c-2018",
                "f-2017,f-2018",
                "g-2017,g-2018",
                "c-unbounded,c-unbounded",
                "c-unbounded,c-2018",
                "c-2017-Q3,c-unbounded",
                "f-unbounded,f-unbounded",
                "f-unbounded,f-2018",
                "f-2017-Q3,f-unbounded",
                "g-unbounded,g-unbounded",
                "g-unbounded,g-2018",
                "g-2017-Q3,g-unbounded",
            };

            var deserializeFromString = typeof(ReportingPeriodExtensions).GetMethods().Single(_ => _.Name == nameof(ReportingPeriodExtensions.DeserializeFromString) && _.ContainsGenericParameters);

            // Act
            var exceptions = new List<Exception>();
            foreach (var reportingPeriod in reportingPeriods)
            {
                foreach (var type in allTypes)
                {
                    var genericMethod = deserializeFromString.MakeGenericMethod(type.ReportingPeriodType);
                    exceptions.Add(Record.Exception(() => genericMethod.Invoke(null, new object[] { reportingPeriod })).InnerException);
                }
            }

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Need to test various flavors of unit-of-time.")]
        public static void DeserializeFromString_TReportingPeriod___Should_throw_InvalidOperationException___When_the_kind_of_unit_of_times_encoded_cannot_be_assigned_to_the_return_types_unit_of_time()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new { ReportingPeriod = "c-2015-11-11,c-2016-11-11", ReportingPeriodType = typeof(ReportingPeriod<FiscalMonth>) },
                new { ReportingPeriod = "c-2017-03,c-2017-04", ReportingPeriodType = typeof(ReportingPeriod<FiscalMonth>) },
                new { ReportingPeriod = "f-2017-03,f-2017-04", ReportingPeriodType = typeof(ReportingPeriod<CalendarUnitOfTime>) },
                new { ReportingPeriod = "g-2017-03,g-2017-04", ReportingPeriodType = typeof(ReportingPeriod<GenericYear>) },
                new { ReportingPeriod = "c-2017-Q3,c-2017-Q4", ReportingPeriodType = typeof(ReportingPeriod<FiscalUnitOfTime>) },
                new { ReportingPeriod = "f-2017-Q3,f-2017-Q4", ReportingPeriodType = typeof(ReportingPeriod<GenericQuarter>) },
                new { ReportingPeriod = "g-2017-Q3,g-2017-Q4", ReportingPeriodType = typeof(ReportingPeriod<CalendarDay>) },
                new { ReportingPeriod = "c-2017,c-2018", ReportingPeriodType = typeof(ReportingPeriod<GenericUnitOfTime>) },
                new { ReportingPeriod = "f-2017,f-2018", ReportingPeriodType = typeof(ReportingPeriod<FiscalMonth>) },
                new { ReportingPeriod = "g-2017,g-2018", ReportingPeriodType = typeof(ReportingPeriod<GenericQuarter>) },
                new { ReportingPeriod = "c-2015-11-11,f-2017-04", ReportingPeriodType = typeof(ReportingPeriod<FiscalMonth>) },
                new { ReportingPeriod = "f-2017-04,f-2018", ReportingPeriodType = typeof(ReportingPeriod<FiscalMonth>) },
                new { ReportingPeriod = "c-2017-Q3,f-2017-04", ReportingPeriodType = typeof(ReportingPeriod<CalendarUnitOfTime>) },
                new { ReportingPeriod = "g-2017-03,g-2018", ReportingPeriodType = typeof(ReportingPeriod<GenericYear>) },
                new { ReportingPeriod = "f-2017-Q3,c-2017-Q4", ReportingPeriodType = typeof(ReportingPeriod<FiscalUnitOfTime>) },
                new { ReportingPeriod = "f-2017-Q3,g-2017-Q4", ReportingPeriodType = typeof(ReportingPeriod<GenericQuarter>) },
                new { ReportingPeriod = "c-2015-11-11,g-2017-Q4", ReportingPeriodType = typeof(ReportingPeriod<CalendarDay>) },
                new { ReportingPeriod = "g-2018,c-2018", ReportingPeriodType = typeof(ReportingPeriod<GenericUnitOfTime>) },
                new { ReportingPeriod = "f-2017-04,f-2018", ReportingPeriodType = typeof(ReportingPeriod<FiscalMonth>) },
                new { ReportingPeriod = "g-2017,g-2017-Q4", ReportingPeriodType = typeof(ReportingPeriod<GenericQuarter>) },
                new { ReportingPeriod = "c-unbounded,c-2017-Q4", ReportingPeriodType = typeof(ReportingPeriod<GenericUnitOfTime>) },
                new { ReportingPeriod = "c-2017-Q4,c-unbounded", ReportingPeriodType = typeof(ReportingPeriod<CalendarQuarter>) },
                new { ReportingPeriod = "c-unbounded,c-unbounded", ReportingPeriodType = typeof(ReportingPeriod<CalendarDay>) },
                new { ReportingPeriod = "f-unbounded,f-2017-Q4", ReportingPeriodType = typeof(ReportingPeriod<FiscalQuarter>) },
                new { ReportingPeriod = "f-2017-Q4,f-unbounded", ReportingPeriodType = typeof(ReportingPeriod<CalendarUnitOfTime>) },
                new { ReportingPeriod = "f-unbounded,f-unbounded", ReportingPeriodType = typeof(ReportingPeriod<FiscalYear>) },
                new { ReportingPeriod = "g-unbounded,g-2017-10", ReportingPeriodType = typeof(ReportingPeriod<GenericMonth>) },
                new { ReportingPeriod = "g-2017-10,g-unbounded", ReportingPeriodType = typeof(ReportingPeriod<GenericQuarter>) },
                new { ReportingPeriod = "g-unbounded,g-unbounded", ReportingPeriodType = typeof(ReportingPeriod<CalendarUnitOfTime>) },
            };

            var deserializeFromString = typeof(ReportingPeriodExtensions).GetMethods().Single(_ => _.Name == nameof(ReportingPeriodExtensions.DeserializeFromString) && _.ContainsGenericParameters);

            // Act
            var exceptions = new List<Exception>();
            foreach (var reportingPeriod in reportingPeriods)
            {
                var genericMethod = deserializeFromString.MakeGenericMethod(reportingPeriod.ReportingPeriodType);
                exceptions.Add(Record.Exception(() => genericMethod.Invoke(null, new object[] { reportingPeriod.ReportingPeriod })).InnerException);
            }

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromString_TReportingPeriod___Should_throw_InvalidOperationException___When_reportingPeriod_has_the_wrong_number_of_tokens()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                ",",
                "c-2017-04",
                ",c-2017-04",
                "c-2017-04,",
                "c-2017-03,,",
                ",c-2017-03,",
                "c-2017-03,c-2017-04,",
                "c-2017-03,c-2017-04,c-2017-04",
            };

            // Act
            var exceptions = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromString<IReportingPeriod<UnitOfTime>>())).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromString_TReportingPeriod___Should_throw_InvalidOperationException___When_token_representing_start_of_reporting_period_is_malformed()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                "c-201a-11,c-2017-10",
                "c-xxxx-11,c-2017-10",
                "c-10000-11,c-2017-10",
                "c-T001-11,c-2017-10",
                "c-0-11,c-2017-10",
                "c-200-11,c-2017-10",
                "c-0000-11,c-2017-10",
                "c-999-11,c-2017-10",
                "c-2007-1,c-2017-10",
                "c-2007-9,c-2017-10",
                "c-2007-13,c-2017-10",
                "c-2007-99,c-2017-10",
                "c-2007-00,c-2017-10",
                "c-2007-001,c-2017-10",
                "c-2007-012,c-2017-10",
            };

            // Act
            var exceptions = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromString<IReportingPeriod<UnitOfTime>>())).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromString_TReportingPeriod___Should_throw_InvalidOperationException___When_token_representing_end_of_reporting_period_is_malformed()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                "c-2017-04,c-201a-11",
                "c-2017-04,c-xxxx-11",
                "c-2017-04,c-10000-11",
                "c-2017-04,c-T001-11",
                "c-2017-04,c-0-11",
                "c-2017-04,c-200-11",
                "c-2017-04,c-0000-11",
                "c-2017-04,c-999-11",
                "c-2017-04,c-2007-1",
                "c-2017-04,c-2007-9",
                "c-2017-04,c-2007-13",
                "c-2017-04,c-2007-99",
                "c-2017-04,c-2007-00",
                "c-2017-04,c-2007-001",
                "c-2017-04,c-2007-012",
            };

            // Act
            var exceptions = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromString<IReportingPeriod<UnitOfTime>>())).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromString_TReportingPeriod___Should_throw_InvalidOperationException___When_tokens_representing_start_and_end_of_reporting_period_are_bounded_and_do_not_deserialize_into_same_concrete_type()
        {
            // Arrange
            var unitsOfTime1 = new[]
            {
                "c-2017-04,c-2017-04-11",
                "f-2017-Q4,g-2018-Q1",
                "c-2017,f-2018-05",
            };

            var unitsOfTime2 = new[]
            {
                "c-2017-04,c-2017-04-11",
                "c-2017,c-2018-05",
            };

            // Act
            var exceptions1 = unitsOfTime1.Select(_ => Record.Exception(() => _.DeserializeFromString<IReportingPeriod<UnitOfTime>>())).ToList();
            var exceptions2 = unitsOfTime2.Select(_ => Record.Exception(() => _.DeserializeFromString<IReportingPeriod<CalendarUnitOfTime>>())).ToList();

            // Assert
            exceptions1.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
            exceptions2.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromString_TReportingPeriod___Should_throw_InvalidOperationException___When_tokens_representing_start_and_end_of_reporting_period_are_bounded_and_start_is_greater_than_end()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                "c-2017-04,c-2016-04",
                "c-2017-Q3,c-2017-Q2",
                "c-2017-03-04,c-2017-03-01",
                "f-2017,f-2016",
            };

            // Act
            var exceptions = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromString<IReportingPeriod<UnitOfTime>>())).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromString_TReportingPeriod___Should_throw_InvalidOperationException___When_one_or_both_tokens_representing_start_and_end_of_reporting_period_are_unbounded_and_are_different_kinds_of_unit_of_time()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                "g-unbounded,c-2016-04",
                "c-2017-Q3,g-unbounded",
                "c-unbounded,g-unbounded",
                "f-unbounded,c-unbounded",
            };

            // Act
            var exceptions1 = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromString<IReportingPeriod<UnitOfTime>>())).ToList();
            var exceptions2 = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromString<IReportingPeriod<CalendarUnitOfTime>>())).ToList();

            // Assert
            exceptions1.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
            exceptions2.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString_TReportingPeriod___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_CalendarDay_string()
        {
            // Arrange
            var reportingPeriod = "c-2001-01-10,c-2016-02-29";
            var expected = new ReportingPeriod<CalendarDay>(new CalendarDay(2001, MonthOfYear.January, DayOfMonth.Ten), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine));

            // Act
            var deserialized1 = reportingPeriod.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized2 = reportingPeriod.DeserializeFromString<ReportingPeriod<UnitOfTime>>();

            var deserialized3 = reportingPeriod.DeserializeFromString<IReportingPeriod<CalendarUnitOfTime>>();
            var deserialized4 = reportingPeriod.DeserializeFromString<ReportingPeriod<CalendarUnitOfTime>>();

            var deserialized5 = reportingPeriod.DeserializeFromString<IReportingPeriod<CalendarDay>>();
            var deserialized6 = reportingPeriod.DeserializeFromString<ReportingPeriod<CalendarDay>>();

            // Assert
            deserialized1.Should().Be(expected);
            deserialized2.Should().Be(expected);
            deserialized3.Should().Be(expected);
            deserialized4.Should().Be(expected);
            deserialized5.Should().Be(expected);
            deserialized6.Should().Be(expected);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString_TReportingPeriod___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_CalendarMonth_string()
        {
            // Arrange
            var reportingPeriod = "c-2001-01,c-2001-02";
            var expected = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2001, MonthOfYear.January), new CalendarMonth(2001, MonthOfYear.February));

            // Act
            var deserialized1 = reportingPeriod.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized2 = reportingPeriod.DeserializeFromString<ReportingPeriod<UnitOfTime>>();

            var deserialized3 = reportingPeriod.DeserializeFromString<IReportingPeriod<CalendarUnitOfTime>>();
            var deserialized4 = reportingPeriod.DeserializeFromString<ReportingPeriod<CalendarUnitOfTime>>();

            var deserialized5 = reportingPeriod.DeserializeFromString<IReportingPeriod<CalendarMonth>>();
            var deserialized6 = reportingPeriod.DeserializeFromString<ReportingPeriod<CalendarMonth>>();

            // Assert
            deserialized1.Should().Be(expected);
            deserialized2.Should().Be(expected);
            deserialized3.Should().Be(expected);
            deserialized4.Should().Be(expected);
            deserialized5.Should().Be(expected);
            deserialized6.Should().Be(expected);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString_TReportingPeriod___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_FiscalMonth_string()
        {
            // Arrange
            var reportingPeriod = "f-2001-01,f-2001-02";
            var expected = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2001, MonthNumber.One), new FiscalMonth(2001, MonthNumber.Two));

            // Act
            var deserialized1 = reportingPeriod.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized2 = reportingPeriod.DeserializeFromString<ReportingPeriod<UnitOfTime>>();

            var deserialized3 = reportingPeriod.DeserializeFromString<IReportingPeriod<FiscalUnitOfTime>>();
            var deserialized4 = reportingPeriod.DeserializeFromString<ReportingPeriod<FiscalUnitOfTime>>();

            var deserialized5 = reportingPeriod.DeserializeFromString<IReportingPeriod<FiscalMonth>>();
            var deserialized6 = reportingPeriod.DeserializeFromString<ReportingPeriod<FiscalMonth>>();

            // Assert
            deserialized1.Should().Be(expected);
            deserialized2.Should().Be(expected);
            deserialized3.Should().Be(expected);
            deserialized4.Should().Be(expected);
            deserialized5.Should().Be(expected);
            deserialized6.Should().Be(expected);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString_TReportingPeriod___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_GenericMonth_string()
        {
            // Arrange
            var reportingPeriod = "g-2001-01,g-2001-02";
            var expected = new ReportingPeriod<GenericMonth>(new GenericMonth(2001, MonthNumber.One), new GenericMonth(2001, MonthNumber.Two));

            // Act
            var deserialized1 = reportingPeriod.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized2 = reportingPeriod.DeserializeFromString<ReportingPeriod<UnitOfTime>>();

            var deserialized3 = reportingPeriod.DeserializeFromString<IReportingPeriod<GenericUnitOfTime>>();
            var deserialized4 = reportingPeriod.DeserializeFromString<ReportingPeriod<GenericUnitOfTime>>();

            var deserialized5 = reportingPeriod.DeserializeFromString<IReportingPeriod<GenericMonth>>();
            var deserialized6 = reportingPeriod.DeserializeFromString<ReportingPeriod<GenericMonth>>();

            // Assert
            deserialized1.Should().Be(expected);
            deserialized2.Should().Be(expected);
            deserialized3.Should().Be(expected);
            deserialized4.Should().Be(expected);
            deserialized5.Should().Be(expected);
            deserialized6.Should().Be(expected);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString_TReportingPeriod___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_CalendarQuarter_string()
        {
            // Arrange
            var reportingPeriod = "c-2001-Q1,c-2001-Q2";
            var expected = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2001, QuarterNumber.Q1), new CalendarQuarter(2001, QuarterNumber.Q2));

            // Act
            var deserialized1 = reportingPeriod.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized2 = reportingPeriod.DeserializeFromString<ReportingPeriod<UnitOfTime>>();

            var deserialized3 = reportingPeriod.DeserializeFromString<IReportingPeriod<CalendarUnitOfTime>>();
            var deserialized4 = reportingPeriod.DeserializeFromString<ReportingPeriod<CalendarUnitOfTime>>();

            var deserialized5 = reportingPeriod.DeserializeFromString<IReportingPeriod<CalendarQuarter>>();
            var deserialized6 = reportingPeriod.DeserializeFromString<ReportingPeriod<CalendarQuarter>>();

            // Assert
            deserialized1.Should().Be(expected);
            deserialized2.Should().Be(expected);
            deserialized3.Should().Be(expected);
            deserialized4.Should().Be(expected);
            deserialized5.Should().Be(expected);
            deserialized6.Should().Be(expected);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString_TReportingPeriod___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_FiscalQuarter_string()
        {
            // Arrange
            var reportingPeriod = "f-2001-Q1,f-2001-Q2";
            var expected = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2001, QuarterNumber.Q1), new FiscalQuarter(2001, QuarterNumber.Q2));

            // Act
            var deserialized1 = reportingPeriod.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized2 = reportingPeriod.DeserializeFromString<ReportingPeriod<UnitOfTime>>();

            var deserialized3 = reportingPeriod.DeserializeFromString<IReportingPeriod<FiscalUnitOfTime>>();
            var deserialized4 = reportingPeriod.DeserializeFromString<ReportingPeriod<FiscalUnitOfTime>>();

            var deserialized5 = reportingPeriod.DeserializeFromString<IReportingPeriod<FiscalQuarter>>();
            var deserialized6 = reportingPeriod.DeserializeFromString<ReportingPeriod<FiscalQuarter>>();

            // Assert
            deserialized1.Should().Be(expected);
            deserialized2.Should().Be(expected);
            deserialized3.Should().Be(expected);
            deserialized4.Should().Be(expected);
            deserialized5.Should().Be(expected);
            deserialized6.Should().Be(expected);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString_TReportingPeriod___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_GenericQuarter_string()
        {
            // Arrange
            var reportingPeriod = "g-2001-Q1,g-2001-Q2";
            var expected = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2001, QuarterNumber.Q1), new GenericQuarter(2001, QuarterNumber.Q2));

            // Act
            var deserialized1 = reportingPeriod.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized2 = reportingPeriod.DeserializeFromString<ReportingPeriod<UnitOfTime>>();

            var deserialized3 = reportingPeriod.DeserializeFromString<IReportingPeriod<GenericUnitOfTime>>();
            var deserialized4 = reportingPeriod.DeserializeFromString<ReportingPeriod<GenericUnitOfTime>>();

            var deserialized5 = reportingPeriod.DeserializeFromString<IReportingPeriod<GenericQuarter>>();
            var deserialized6 = reportingPeriod.DeserializeFromString<ReportingPeriod<GenericQuarter>>();

            // Assert
            deserialized1.Should().Be(expected);
            deserialized2.Should().Be(expected);
            deserialized3.Should().Be(expected);
            deserialized4.Should().Be(expected);
            deserialized5.Should().Be(expected);
            deserialized6.Should().Be(expected);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString_TReportingPeriod___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_CalendarYear_string()
        {
            // Arrange
            var reportingPeriod = "c-2001,c-2002";
            var expected = new ReportingPeriod<CalendarYear>(new CalendarYear(2001), new CalendarYear(2002));

            // Act
            var deserialized1 = reportingPeriod.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized2 = reportingPeriod.DeserializeFromString<ReportingPeriod<UnitOfTime>>();

            var deserialized3 = reportingPeriod.DeserializeFromString<IReportingPeriod<CalendarUnitOfTime>>();
            var deserialized4 = reportingPeriod.DeserializeFromString<ReportingPeriod<CalendarUnitOfTime>>();

            var deserialized5 = reportingPeriod.DeserializeFromString<IReportingPeriod<CalendarYear>>();
            var deserialized6 = reportingPeriod.DeserializeFromString<ReportingPeriod<CalendarYear>>();

            // Assert
            deserialized1.Should().Be(expected);
            deserialized2.Should().Be(expected);
            deserialized3.Should().Be(expected);
            deserialized4.Should().Be(expected);
            deserialized5.Should().Be(expected);
            deserialized6.Should().Be(expected);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString_TReportingPeriod___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_FiscalYear_string()
        {
            // Arrange
            var reportingPeriod = "f-2001,f-2002";
            var expected = new ReportingPeriod<FiscalYear>(new FiscalYear(2001), new FiscalYear(2002));

            // Act
            var deserialized1 = reportingPeriod.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized2 = reportingPeriod.DeserializeFromString<ReportingPeriod<UnitOfTime>>();

            var deserialized3 = reportingPeriod.DeserializeFromString<IReportingPeriod<FiscalUnitOfTime>>();
            var deserialized4 = reportingPeriod.DeserializeFromString<ReportingPeriod<FiscalUnitOfTime>>();

            var deserialized5 = reportingPeriod.DeserializeFromString<IReportingPeriod<FiscalYear>>();
            var deserialized6 = reportingPeriod.DeserializeFromString<ReportingPeriod<FiscalYear>>();

            // Assert
            deserialized1.Should().Be(expected);
            deserialized2.Should().Be(expected);
            deserialized3.Should().Be(expected);
            deserialized4.Should().Be(expected);
            deserialized5.Should().Be(expected);
            deserialized6.Should().Be(expected);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString_TReportingPeriod___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_GenericYear_string()
        {
            // Arrange
            var reportingPeriod = "g-2001,g-2002";
            var expected = new ReportingPeriod<GenericYear>(new GenericYear(2001), new GenericYear(2002));

            // Act
            var deserialized1 = reportingPeriod.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized2 = reportingPeriod.DeserializeFromString<ReportingPeriod<UnitOfTime>>();

            var deserialized3 = reportingPeriod.DeserializeFromString<IReportingPeriod<GenericUnitOfTime>>();
            var deserialized4 = reportingPeriod.DeserializeFromString<ReportingPeriod<GenericUnitOfTime>>();

            var deserialized5 = reportingPeriod.DeserializeFromString<IReportingPeriod<GenericYear>>();
            var deserialized6 = reportingPeriod.DeserializeFromString<ReportingPeriod<GenericYear>>();

            // Assert
            deserialized1.Should().Be(expected);
            deserialized2.Should().Be(expected);
            deserialized3.Should().Be(expected);
            deserialized4.Should().Be(expected);
            deserialized5.Should().Be(expected);
            deserialized6.Should().Be(expected);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString_TReportingPeriod___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_CalendarUnbounded_string()
        {
            // Arrange
            var reportingPeriod1 = "c-unbounded,c-2002";
            var reportingPeriod2 = "c-2002,c-unbounded";
            var reportingPeriod3 = "c-unbounded,c-unbounded";

            var expected1 = new ReportingPeriod<CalendarUnitOfTime>(new CalendarUnbounded(), new CalendarYear(2002));
            var expected2 = new ReportingPeriod<CalendarUnitOfTime>(new CalendarYear(2002), new CalendarUnbounded());
            var expected3 = new ReportingPeriod<CalendarUnbounded>(new CalendarUnbounded(), new CalendarUnbounded());

            // Act
            var deserialized1a = reportingPeriod1.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized1b = reportingPeriod1.DeserializeFromString<ReportingPeriod<UnitOfTime>>();
            var deserialized1c = reportingPeriod1.DeserializeFromString<IReportingPeriod<CalendarUnitOfTime>>();
            var deserialized1d = reportingPeriod1.DeserializeFromString<ReportingPeriod<CalendarUnitOfTime>>();

            var deserialized2a = reportingPeriod2.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized2b = reportingPeriod2.DeserializeFromString<ReportingPeriod<UnitOfTime>>();
            var deserialized2c = reportingPeriod2.DeserializeFromString<IReportingPeriod<CalendarUnitOfTime>>();
            var deserialized2d = reportingPeriod2.DeserializeFromString<ReportingPeriod<CalendarUnitOfTime>>();

            var deserialized3a = reportingPeriod3.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized3b = reportingPeriod3.DeserializeFromString<ReportingPeriod<UnitOfTime>>();
            var deserialized3c = reportingPeriod3.DeserializeFromString<IReportingPeriod<CalendarUnitOfTime>>();
            var deserialized3d = reportingPeriod3.DeserializeFromString<ReportingPeriod<CalendarUnitOfTime>>();
            var deserialized3e = reportingPeriod3.DeserializeFromString<IReportingPeriod<CalendarUnbounded>>();
            var deserialized3f = reportingPeriod3.DeserializeFromString<ReportingPeriod<CalendarUnbounded>>();

            // Assert
            deserialized1a.Should().Be(expected1);
            deserialized1b.Should().Be(expected1);
            deserialized1c.Should().Be(expected1);
            deserialized1d.Should().Be(expected1);

            deserialized2a.Should().Be(expected2);
            deserialized2b.Should().Be(expected2);
            deserialized2c.Should().Be(expected2);
            deserialized2d.Should().Be(expected2);

            deserialized3a.Should().Be(expected3);
            deserialized3b.Should().Be(expected3);
            deserialized3c.Should().Be(expected3);
            deserialized3d.Should().Be(expected3);
            deserialized3e.Should().Be(expected3);
            deserialized3f.Should().Be(expected3);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString_TReportingPeriod___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_FiscalUnbounded_string()
        {
            // Arrange
            var reportingPeriod1 = "f-unbounded,f-2002";
            var reportingPeriod2 = "f-2002,f-unbounded";
            var reportingPeriod3 = "f-unbounded,f-unbounded";

            var expected1 = new ReportingPeriod<FiscalUnitOfTime>(new FiscalUnbounded(), new FiscalYear(2002));
            var expected2 = new ReportingPeriod<FiscalUnitOfTime>(new FiscalYear(2002), new FiscalUnbounded());
            var expected3 = new ReportingPeriod<FiscalUnbounded>(new FiscalUnbounded(), new FiscalUnbounded());

            // Act
            var deserialized1a = reportingPeriod1.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized1b = reportingPeriod1.DeserializeFromString<ReportingPeriod<UnitOfTime>>();
            var deserialized1c = reportingPeriod1.DeserializeFromString<IReportingPeriod<FiscalUnitOfTime>>();
            var deserialized1d = reportingPeriod1.DeserializeFromString<ReportingPeriod<FiscalUnitOfTime>>();

            var deserialized2a = reportingPeriod2.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized2b = reportingPeriod2.DeserializeFromString<ReportingPeriod<UnitOfTime>>();
            var deserialized2c = reportingPeriod2.DeserializeFromString<IReportingPeriod<FiscalUnitOfTime>>();
            var deserialized2d = reportingPeriod2.DeserializeFromString<ReportingPeriod<FiscalUnitOfTime>>();

            var deserialized3a = reportingPeriod3.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized3b = reportingPeriod3.DeserializeFromString<ReportingPeriod<UnitOfTime>>();
            var deserialized3c = reportingPeriod3.DeserializeFromString<IReportingPeriod<FiscalUnitOfTime>>();
            var deserialized3d = reportingPeriod3.DeserializeFromString<ReportingPeriod<FiscalUnitOfTime>>();
            var deserialized3e = reportingPeriod3.DeserializeFromString<IReportingPeriod<FiscalUnbounded>>();
            var deserialized3f = reportingPeriod3.DeserializeFromString<ReportingPeriod<FiscalUnbounded>>();

            // Assert
            deserialized1a.Should().Be(expected1);
            deserialized1b.Should().Be(expected1);
            deserialized1c.Should().Be(expected1);
            deserialized1d.Should().Be(expected1);

            deserialized2a.Should().Be(expected2);
            deserialized2b.Should().Be(expected2);
            deserialized2c.Should().Be(expected2);
            deserialized2d.Should().Be(expected2);

            deserialized3a.Should().Be(expected3);
            deserialized3b.Should().Be(expected3);
            deserialized3c.Should().Be(expected3);
            deserialized3d.Should().Be(expected3);
            deserialized3e.Should().Be(expected3);
            deserialized3f.Should().Be(expected3);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString_TReportingPeriod___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_GenericUnbounded_string()
        {
            // Arrange
            var reportingPeriod1 = "g-unbounded,g-2002";
            var reportingPeriod2 = "g-2002,g-unbounded";
            var reportingPeriod3 = "g-unbounded,g-unbounded";

            var expected1 = new ReportingPeriod<GenericUnitOfTime>(new GenericUnbounded(), new GenericYear(2002));
            var expected2 = new ReportingPeriod<GenericUnitOfTime>(new GenericYear(2002), new GenericUnbounded());
            var expected3 = new ReportingPeriod<GenericUnbounded>(new GenericUnbounded(), new GenericUnbounded());

            // Act
            var deserialized1a = reportingPeriod1.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized1b = reportingPeriod1.DeserializeFromString<ReportingPeriod<UnitOfTime>>();
            var deserialized1c = reportingPeriod1.DeserializeFromString<IReportingPeriod<GenericUnitOfTime>>();
            var deserialized1d = reportingPeriod1.DeserializeFromString<ReportingPeriod<GenericUnitOfTime>>();

            var deserialized2a = reportingPeriod2.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized2b = reportingPeriod2.DeserializeFromString<ReportingPeriod<UnitOfTime>>();
            var deserialized2c = reportingPeriod2.DeserializeFromString<IReportingPeriod<GenericUnitOfTime>>();
            var deserialized2d = reportingPeriod2.DeserializeFromString<ReportingPeriod<GenericUnitOfTime>>();

            var deserialized3a = reportingPeriod3.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized3b = reportingPeriod3.DeserializeFromString<ReportingPeriod<UnitOfTime>>();
            var deserialized3c = reportingPeriod3.DeserializeFromString<IReportingPeriod<GenericUnitOfTime>>();
            var deserialized3d = reportingPeriod3.DeserializeFromString<ReportingPeriod<GenericUnitOfTime>>();
            var deserialized3e = reportingPeriod3.DeserializeFromString<IReportingPeriod<GenericUnbounded>>();
            var deserialized3f = reportingPeriod3.DeserializeFromString<ReportingPeriod<GenericUnbounded>>();

            // Assert
            deserialized1a.Should().Be(expected1);
            deserialized1b.Should().Be(expected1);
            deserialized1c.Should().Be(expected1);
            deserialized1d.Should().Be(expected1);

            deserialized2a.Should().Be(expected2);
            deserialized2b.Should().Be(expected2);
            deserialized2c.Should().Be(expected2);
            deserialized2d.Should().Be(expected2);

            deserialized3a.Should().Be(expected3);
            deserialized3b.Should().Be(expected3);
            deserialized3c.Should().Be(expected3);
            deserialized3d.Should().Be(expected3);
            deserialized3e.Should().Be(expected3);
            deserialized3f.Should().Be(expected3);
        }

        [Fact]
        public static void DeserializeFromString___Should_throw_ArgumentNullException___When_parameter_unitOfTime_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.DeserializeFromString(null, typeof(IReportingPeriod<UnitOfTime>)));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void DeserializeFromString___Should_throw_ArgumentException___When_parameter_unitOfTime_is_white_space()
        {
            // Arrange
            var reportingPeriods = new[] { string.Empty, "  ", "  \r\n " };

            // Act
            var exceptions = reportingPeriods.Select(_ => Record.Exception(() => _.DeserializeFromString(typeof(IReportingPeriod<UnitOfTime>)))).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<ArgumentException>());
        }

        [Fact]
        public static void DeserializeFromString___Should_throw_ArgumentNullException___When_parameter_requestedType_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => "g-2001-Q1,g-2001-Q2".DeserializeFromString(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void DeserializeFromString___Should_throw_ArgumentException___When_parameter_requestedType_is_not_an_IReportingPeriod_of_UnitOfTime()
        {
            // Arrange, Act
            var ex = Record.Exception(() => "g-2001-Q1,g-2001-Q2".DeserializeFromString(typeof(string)));

            // Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Thoroughly checking this test-case requires lots of types.")]
        public static void DeserializeFromString___Should_throw_InvalidOperationException___When_the_return_type_cannot_be_assigned_to_a_ReportingPeriod()
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

            var reportingPeriods = new[]
            {
                "c-2015-11-11,c-2016-11-11",
                "c-2017-03,c-2017-04",
                "f-2017-03,f-2017-04",
                "g-2017-03,g-2017-04",
                "c-2017-Q3,c-2017-Q4",
                "f-2017-Q3,f-2017-Q4",
                "g-2017-Q3,g-2017-Q4",
                "c-2017,c-2018",
                "f-2017,f-2018",
                "g-2017,g-2018",
                "c-unbounded,c-unbounded",
                "c-unbounded,c-2018",
                "c-2017-Q3,c-unbounded",
                "f-unbounded,f-unbounded",
                "f-unbounded,f-2018",
                "f-2017-Q3,f-unbounded",
                "g-unbounded,g-unbounded",
                "g-unbounded,g-2018",
                "g-2017-Q3,g-unbounded",
            };

            // Act
            var exceptions = new List<Exception>();
            foreach (var reportingPeriod in reportingPeriods)
            {
                foreach (var type in allTypes)
                {
                    exceptions.Add(Record.Exception(() => reportingPeriod.DeserializeFromString(type.ReportingPeriodType)));
                }
            }

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Need to test various flavors of unit-of-time.")]
        public static void DeserializeFromString___Should_throw_InvalidOperationException___When_the_kind_of_unit_of_times_encoded_cannot_be_assigned_to_the_return_types_unit_of_time()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new { ReportingPeriod = "c-2015-11-11,c-2016-11-11", ReportingPeriodType = typeof(ReportingPeriod<FiscalMonth>) },
                new { ReportingPeriod = "c-2017-03,c-2017-04", ReportingPeriodType = typeof(ReportingPeriod<FiscalMonth>) },
                new { ReportingPeriod = "f-2017-03,f-2017-04", ReportingPeriodType = typeof(ReportingPeriod<CalendarUnitOfTime>) },
                new { ReportingPeriod = "g-2017-03,g-2017-04", ReportingPeriodType = typeof(ReportingPeriod<GenericYear>) },
                new { ReportingPeriod = "c-2017-Q3,c-2017-Q4", ReportingPeriodType = typeof(ReportingPeriod<FiscalUnitOfTime>) },
                new { ReportingPeriod = "f-2017-Q3,f-2017-Q4", ReportingPeriodType = typeof(ReportingPeriod<GenericQuarter>) },
                new { ReportingPeriod = "g-2017-Q3,g-2017-Q4", ReportingPeriodType = typeof(ReportingPeriod<CalendarDay>) },
                new { ReportingPeriod = "c-2017,c-2018", ReportingPeriodType = typeof(ReportingPeriod<GenericUnitOfTime>) },
                new { ReportingPeriod = "f-2017,f-2018", ReportingPeriodType = typeof(ReportingPeriod<FiscalMonth>) },
                new { ReportingPeriod = "g-2017,g-2018", ReportingPeriodType = typeof(ReportingPeriod<GenericQuarter>) },
                new { ReportingPeriod = "c-2015-11-11,f-2017-04", ReportingPeriodType = typeof(ReportingPeriod<FiscalMonth>) },
                new { ReportingPeriod = "f-2017-04,f-2018", ReportingPeriodType = typeof(ReportingPeriod<FiscalMonth>) },
                new { ReportingPeriod = "c-2017-Q3,f-2017-04", ReportingPeriodType = typeof(ReportingPeriod<CalendarUnitOfTime>) },
                new { ReportingPeriod = "g-2017-03,g-2018", ReportingPeriodType = typeof(ReportingPeriod<GenericYear>) },
                new { ReportingPeriod = "f-2017-Q3,c-2017-Q4", ReportingPeriodType = typeof(ReportingPeriod<FiscalUnitOfTime>) },
                new { ReportingPeriod = "f-2017-Q3,g-2017-Q4", ReportingPeriodType = typeof(ReportingPeriod<GenericQuarter>) },
                new { ReportingPeriod = "c-2015-11-11,g-2017-Q4", ReportingPeriodType = typeof(ReportingPeriod<CalendarDay>) },
                new { ReportingPeriod = "g-2018,c-2018", ReportingPeriodType = typeof(ReportingPeriod<GenericUnitOfTime>) },
                new { ReportingPeriod = "f-2017-04,f-2018", ReportingPeriodType = typeof(ReportingPeriod<FiscalMonth>) },
                new { ReportingPeriod = "g-2017,g-2017-Q4", ReportingPeriodType = typeof(ReportingPeriod<GenericQuarter>) },
                new { ReportingPeriod = "c-unbounded,c-2017-Q4", ReportingPeriodType = typeof(ReportingPeriod<GenericUnitOfTime>) },
                new { ReportingPeriod = "c-2017-Q4,c-unbounded", ReportingPeriodType = typeof(ReportingPeriod<CalendarQuarter>) },
                new { ReportingPeriod = "c-unbounded,c-unbounded", ReportingPeriodType = typeof(ReportingPeriod<CalendarDay>) },
                new { ReportingPeriod = "f-unbounded,f-2017-Q4", ReportingPeriodType = typeof(ReportingPeriod<FiscalQuarter>) },
                new { ReportingPeriod = "f-2017-Q4,f-unbounded", ReportingPeriodType = typeof(ReportingPeriod<CalendarUnitOfTime>) },
                new { ReportingPeriod = "f-unbounded,f-unbounded", ReportingPeriodType = typeof(ReportingPeriod<FiscalYear>) },
                new { ReportingPeriod = "g-unbounded,g-2017-10", ReportingPeriodType = typeof(ReportingPeriod<GenericMonth>) },
                new { ReportingPeriod = "g-2017-10,g-unbounded", ReportingPeriodType = typeof(ReportingPeriod<GenericQuarter>) },
                new { ReportingPeriod = "g-unbounded,g-unbounded", ReportingPeriodType = typeof(ReportingPeriod<CalendarUnitOfTime>) },
            };

            // Act
            var exceptions = new List<Exception>();
            foreach (var reportingPeriod in reportingPeriods)
            {
                exceptions.Add(Record.Exception(() => reportingPeriod.ReportingPeriod.DeserializeFromString(reportingPeriod.ReportingPeriodType)));
            }

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromString___Should_throw_InvalidOperationException___When_reportingPeriod_has_the_wrong_number_of_tokens()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                ",",
                "c-2017-04",
                ",c-2017-04",
                "c-2017-04,",
                "c-2017-03,,",
                ",c-2017-03,",
                "c-2017-03,c-2017-04,",
                "c-2017-03,c-2017-04,c-2017-04",
            };

            // Act
            var exceptions = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromString(typeof(IReportingPeriod<UnitOfTime>)))).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromString___Should_throw_InvalidOperationException___When_token_representing_start_of_reporting_period_is_malformed()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                "c-201a-11,c-2017-10",
                "c-xxxx-11,c-2017-10",
                "c-10000-11,c-2017-10",
                "c-T001-11,c-2017-10",
                "c-0-11,c-2017-10",
                "c-200-11,c-2017-10",
                "c-0000-11,c-2017-10",
                "c-999-11,c-2017-10",
                "c-2007-1,c-2017-10",
                "c-2007-9,c-2017-10",
                "c-2007-13,c-2017-10",
                "c-2007-99,c-2017-10",
                "c-2007-00,c-2017-10",
                "c-2007-001,c-2017-10",
                "c-2007-012,c-2017-10",
            };

            // Act
            var exceptions = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromString(typeof(IReportingPeriod<UnitOfTime>)))).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromString___Should_throw_InvalidOperationException___When_token_representing_end_of_reporting_period_is_malformed()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                "c-2017-04,c-201a-11",
                "c-2017-04,c-xxxx-11",
                "c-2017-04,c-10000-11",
                "c-2017-04,c-T001-11",
                "c-2017-04,c-0-11",
                "c-2017-04,c-200-11",
                "c-2017-04,c-0000-11",
                "c-2017-04,c-999-11",
                "c-2017-04,c-2007-1",
                "c-2017-04,c-2007-9",
                "c-2017-04,c-2007-13",
                "c-2017-04,c-2007-99",
                "c-2017-04,c-2007-00",
                "c-2017-04,c-2007-001",
                "c-2017-04,c-2007-012",
            };

            // Act
            var exceptions = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromString(typeof(IReportingPeriod<UnitOfTime>)))).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromString___Should_throw_InvalidOperationException___When_tokens_representing_start_and_end_of_reporting_period_are_bounded_and_do_not_deserialize_into_same_concrete_type()
        {
            // Arrange
            var unitsOfTime1 = new[]
            {
                "c-2017-04,c-2017-04-11",
                "f-2017-Q4,g-2018-Q1",
                "c-2017,f-2018-05",
            };

            var unitsOfTime2 = new[]
            {
                "c-2017-04,c-2017-04-11",
                "c-2017,c-2018-05",
            };

            // Act
            var exceptions1 = unitsOfTime1.Select(_ => Record.Exception(() => _.DeserializeFromString(typeof(IReportingPeriod<UnitOfTime>)))).ToList();
            var exceptions2 = unitsOfTime2.Select(_ => Record.Exception(() => _.DeserializeFromString(typeof(IReportingPeriod<CalendarUnitOfTime>)))).ToList();

            // Assert
            exceptions1.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
            exceptions2.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromString___Should_throw_InvalidOperationException___When_tokens_representing_start_and_end_of_reporting_period_are_bounded_and_start_is_greater_than_end()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                "c-2017-04,c-2016-04",
                "c-2017-Q3,c-2017-Q2",
                "c-2017-03-04,c-2017-03-01",
                "f-2017,f-2016",
            };

            // Act
            var exceptions = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromString(typeof(IReportingPeriod<UnitOfTime>)))).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromString___Should_throw_InvalidOperationException___When_one_or_both_tokens_representing_start_and_end_of_reporting_period_are_unbounded_and_are_different_kinds_of_unit_of_time()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                "g-unbounded,c-2016-04",
                "c-2017-Q3,g-unbounded",
                "c-unbounded,g-unbounded",
                "f-unbounded,c-unbounded",
            };

            // Act
            var exceptions1 = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromString(typeof(IReportingPeriod<UnitOfTime>)))).ToList();
            var exceptions2 = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromString(typeof(IReportingPeriod<CalendarUnitOfTime>)))).ToList();

            // Assert
            exceptions1.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
            exceptions2.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_CalendarDay_string()
        {
            // Arrange
            var reportingPeriod = "c-2001-01-10,c-2016-02-29";
            var expected = new ReportingPeriod<CalendarDay>(new CalendarDay(2001, MonthOfYear.January, DayOfMonth.Ten), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine));

            // Act
            var deserialized1 = reportingPeriod.DeserializeFromString(typeof(IReportingPeriod<UnitOfTime>));
            var deserialized2 = reportingPeriod.DeserializeFromString(typeof(ReportingPeriod<UnitOfTime>));

            var deserialized3 = reportingPeriod.DeserializeFromString(typeof(IReportingPeriod<CalendarUnitOfTime>));
            var deserialized4 = reportingPeriod.DeserializeFromString(typeof(ReportingPeriod<CalendarUnitOfTime>));

            var deserialized5 = reportingPeriod.DeserializeFromString(typeof(IReportingPeriod<CalendarDay>));
            var deserialized6 = reportingPeriod.DeserializeFromString(typeof(ReportingPeriod<CalendarDay>));

            // Assert
            deserialized1.Should().Be(expected);
            deserialized2.Should().Be(expected);
            deserialized3.Should().Be(expected);
            deserialized4.Should().Be(expected);
            deserialized5.Should().Be(expected);
            deserialized6.Should().Be(expected);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_CalendarMonth_string()
        {
            // Arrange
            var reportingPeriod = "c-2001-01,c-2001-02";
            var expected = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2001, MonthOfYear.January), new CalendarMonth(2001, MonthOfYear.February));

            // Act
            var deserialized1 = reportingPeriod.DeserializeFromString(typeof(IReportingPeriod<UnitOfTime>));
            var deserialized2 = reportingPeriod.DeserializeFromString(typeof(ReportingPeriod<UnitOfTime>));

            var deserialized3 = reportingPeriod.DeserializeFromString(typeof(IReportingPeriod<CalendarUnitOfTime>));
            var deserialized4 = reportingPeriod.DeserializeFromString(typeof(ReportingPeriod<CalendarUnitOfTime>));

            var deserialized5 = reportingPeriod.DeserializeFromString(typeof(IReportingPeriod<CalendarMonth>));
            var deserialized6 = reportingPeriod.DeserializeFromString(typeof(ReportingPeriod<CalendarMonth>));

            // Assert
            deserialized1.Should().Be(expected);
            deserialized2.Should().Be(expected);
            deserialized3.Should().Be(expected);
            deserialized4.Should().Be(expected);
            deserialized5.Should().Be(expected);
            deserialized6.Should().Be(expected);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_FiscalMonth_string()
        {
            // Arrange
            var reportingPeriod = "f-2001-01,f-2001-02";
            var expected = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2001, MonthNumber.One), new FiscalMonth(2001, MonthNumber.Two));

            // Act
            var deserialized1 = reportingPeriod.DeserializeFromString(typeof(IReportingPeriod<UnitOfTime>));
            var deserialized2 = reportingPeriod.DeserializeFromString(typeof(ReportingPeriod<UnitOfTime>));

            var deserialized3 = reportingPeriod.DeserializeFromString(typeof(IReportingPeriod<FiscalUnitOfTime>));
            var deserialized4 = reportingPeriod.DeserializeFromString(typeof(ReportingPeriod<FiscalUnitOfTime>));

            var deserialized5 = reportingPeriod.DeserializeFromString(typeof(IReportingPeriod<FiscalMonth>));
            var deserialized6 = reportingPeriod.DeserializeFromString(typeof(ReportingPeriod<FiscalMonth>));

            // Assert
            deserialized1.Should().Be(expected);
            deserialized2.Should().Be(expected);
            deserialized3.Should().Be(expected);
            deserialized4.Should().Be(expected);
            deserialized5.Should().Be(expected);
            deserialized6.Should().Be(expected);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_GenericMonth_string()
        {
            // Arrange
            var reportingPeriod = "g-2001-01,g-2001-02";
            var expected = new ReportingPeriod<GenericMonth>(new GenericMonth(2001, MonthNumber.One), new GenericMonth(2001, MonthNumber.Two));

            // Act
            var deserialized1 = reportingPeriod.DeserializeFromString(typeof(IReportingPeriod<UnitOfTime>));
            var deserialized2 = reportingPeriod.DeserializeFromString(typeof(ReportingPeriod<UnitOfTime>));

            var deserialized3 = reportingPeriod.DeserializeFromString(typeof(IReportingPeriod<GenericUnitOfTime>));
            var deserialized4 = reportingPeriod.DeserializeFromString(typeof(ReportingPeriod<GenericUnitOfTime>));

            var deserialized5 = reportingPeriod.DeserializeFromString(typeof(IReportingPeriod<GenericMonth>));
            var deserialized6 = reportingPeriod.DeserializeFromString(typeof(ReportingPeriod<GenericMonth>));

            // Assert
            deserialized1.Should().Be(expected);
            deserialized2.Should().Be(expected);
            deserialized3.Should().Be(expected);
            deserialized4.Should().Be(expected);
            deserialized5.Should().Be(expected);
            deserialized6.Should().Be(expected);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_CalendarQuarter_string()
        {
            // Arrange
            var reportingPeriod = "c-2001-Q1,c-2001-Q2";
            var expected = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2001, QuarterNumber.Q1), new CalendarQuarter(2001, QuarterNumber.Q2));

            // Act
            var deserialized1 = reportingPeriod.DeserializeFromString(typeof(IReportingPeriod<UnitOfTime>));
            var deserialized2 = reportingPeriod.DeserializeFromString(typeof(ReportingPeriod<UnitOfTime>));

            var deserialized3 = reportingPeriod.DeserializeFromString(typeof(IReportingPeriod<CalendarUnitOfTime>));
            var deserialized4 = reportingPeriod.DeserializeFromString(typeof(ReportingPeriod<CalendarUnitOfTime>));

            var deserialized5 = reportingPeriod.DeserializeFromString(typeof(IReportingPeriod<CalendarQuarter>));
            var deserialized6 = reportingPeriod.DeserializeFromString(typeof(ReportingPeriod<CalendarQuarter>));

            // Assert
            deserialized1.Should().Be(expected);
            deserialized2.Should().Be(expected);
            deserialized3.Should().Be(expected);
            deserialized4.Should().Be(expected);
            deserialized5.Should().Be(expected);
            deserialized6.Should().Be(expected);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_FiscalQuarter_string()
        {
            // Arrange
            var reportingPeriod = "f-2001-Q1,f-2001-Q2";
            var expected = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2001, QuarterNumber.Q1), new FiscalQuarter(2001, QuarterNumber.Q2));

            // Act
            var deserialized1 = reportingPeriod.DeserializeFromString(typeof(IReportingPeriod<UnitOfTime>));
            var deserialized2 = reportingPeriod.DeserializeFromString(typeof(ReportingPeriod<UnitOfTime>));

            var deserialized3 = reportingPeriod.DeserializeFromString(typeof(IReportingPeriod<FiscalUnitOfTime>));
            var deserialized4 = reportingPeriod.DeserializeFromString(typeof(ReportingPeriod<FiscalUnitOfTime>));

            var deserialized5 = reportingPeriod.DeserializeFromString(typeof(IReportingPeriod<FiscalQuarter>));
            var deserialized6 = reportingPeriod.DeserializeFromString(typeof(ReportingPeriod<FiscalQuarter>));

            // Assert
            deserialized1.Should().Be(expected);
            deserialized2.Should().Be(expected);
            deserialized3.Should().Be(expected);
            deserialized4.Should().Be(expected);
            deserialized5.Should().Be(expected);
            deserialized6.Should().Be(expected);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_GenericQuarter_string()
        {
            // Arrange
            var reportingPeriod = "g-2001-Q1,g-2001-Q2";
            var expected = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2001, QuarterNumber.Q1), new GenericQuarter(2001, QuarterNumber.Q2));

            // Act
            var deserialized1 = reportingPeriod.DeserializeFromString(typeof(IReportingPeriod<UnitOfTime>));
            var deserialized2 = reportingPeriod.DeserializeFromString(typeof(ReportingPeriod<UnitOfTime>));

            var deserialized3 = reportingPeriod.DeserializeFromString(typeof(IReportingPeriod<GenericUnitOfTime>));
            var deserialized4 = reportingPeriod.DeserializeFromString(typeof(ReportingPeriod<GenericUnitOfTime>));

            var deserialized5 = reportingPeriod.DeserializeFromString(typeof(IReportingPeriod<GenericQuarter>));
            var deserialized6 = reportingPeriod.DeserializeFromString(typeof(ReportingPeriod<GenericQuarter>));

            // Assert
            deserialized1.Should().Be(expected);
            deserialized2.Should().Be(expected);
            deserialized3.Should().Be(expected);
            deserialized4.Should().Be(expected);
            deserialized5.Should().Be(expected);
            deserialized6.Should().Be(expected);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_CalendarYear_string()
        {
            // Arrange
            var reportingPeriod = "c-2001,c-2002";
            var expected = new ReportingPeriod<CalendarYear>(new CalendarYear(2001), new CalendarYear(2002));

            // Act
            var deserialized1 = reportingPeriod.DeserializeFromString(typeof(IReportingPeriod<UnitOfTime>));
            var deserialized2 = reportingPeriod.DeserializeFromString(typeof(ReportingPeriod<UnitOfTime>));

            var deserialized3 = reportingPeriod.DeserializeFromString(typeof(IReportingPeriod<CalendarUnitOfTime>));
            var deserialized4 = reportingPeriod.DeserializeFromString(typeof(ReportingPeriod<CalendarUnitOfTime>));

            var deserialized5 = reportingPeriod.DeserializeFromString(typeof(IReportingPeriod<CalendarYear>));
            var deserialized6 = reportingPeriod.DeserializeFromString(typeof(ReportingPeriod<CalendarYear>));

            // Assert
            deserialized1.Should().Be(expected);
            deserialized2.Should().Be(expected);
            deserialized3.Should().Be(expected);
            deserialized4.Should().Be(expected);
            deserialized5.Should().Be(expected);
            deserialized6.Should().Be(expected);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_FiscalYear_string()
        {
            // Arrange
            var reportingPeriod = "f-2001,f-2002";
            var expected = new ReportingPeriod<FiscalYear>(new FiscalYear(2001), new FiscalYear(2002));

            // Act
            var deserialized1 = reportingPeriod.DeserializeFromString(typeof(IReportingPeriod<UnitOfTime>));
            var deserialized2 = reportingPeriod.DeserializeFromString(typeof(ReportingPeriod<UnitOfTime>));

            var deserialized3 = reportingPeriod.DeserializeFromString(typeof(IReportingPeriod<FiscalUnitOfTime>));
            var deserialized4 = reportingPeriod.DeserializeFromString(typeof(ReportingPeriod<FiscalUnitOfTime>));

            var deserialized5 = reportingPeriod.DeserializeFromString(typeof(IReportingPeriod<FiscalYear>));
            var deserialized6 = reportingPeriod.DeserializeFromString(typeof(ReportingPeriod<FiscalYear>));

            // Assert
            deserialized1.Should().Be(expected);
            deserialized2.Should().Be(expected);
            deserialized3.Should().Be(expected);
            deserialized4.Should().Be(expected);
            deserialized5.Should().Be(expected);
            deserialized6.Should().Be(expected);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_GenericYear_string()
        {
            // Arrange
            var reportingPeriod = "g-2001,g-2002";
            var expected = new ReportingPeriod<GenericYear>(new GenericYear(2001), new GenericYear(2002));

            // Act
            var deserialized1 = reportingPeriod.DeserializeFromString(typeof(IReportingPeriod<UnitOfTime>));
            var deserialized2 = reportingPeriod.DeserializeFromString(typeof(ReportingPeriod<UnitOfTime>));

            var deserialized3 = reportingPeriod.DeserializeFromString(typeof(IReportingPeriod<GenericUnitOfTime>));
            var deserialized4 = reportingPeriod.DeserializeFromString(typeof(ReportingPeriod<GenericUnitOfTime>));

            var deserialized5 = reportingPeriod.DeserializeFromString(typeof(IReportingPeriod<GenericYear>));
            var deserialized6 = reportingPeriod.DeserializeFromString(typeof(ReportingPeriod<GenericYear>));

            // Assert
            deserialized1.Should().Be(expected);
            deserialized2.Should().Be(expected);
            deserialized3.Should().Be(expected);
            deserialized4.Should().Be(expected);
            deserialized5.Should().Be(expected);
            deserialized6.Should().Be(expected);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_CalendarUnbounded_string()
        {
            // Arrange
            var reportingPeriod1 = "c-unbounded,c-2002";
            var reportingPeriod2 = "c-2002,c-unbounded";
            var reportingPeriod3 = "c-unbounded,c-unbounded";

            var expected1 = new ReportingPeriod<CalendarUnitOfTime>(new CalendarUnbounded(), new CalendarYear(2002));
            var expected2 = new ReportingPeriod<CalendarUnitOfTime>(new CalendarYear(2002), new CalendarUnbounded());
            var expected3 = new ReportingPeriod<CalendarUnbounded>(new CalendarUnbounded(), new CalendarUnbounded());

            // Act
            var deserialized1a = reportingPeriod1.DeserializeFromString(typeof(IReportingPeriod<UnitOfTime>));
            var deserialized1b = reportingPeriod1.DeserializeFromString(typeof(ReportingPeriod<UnitOfTime>));
            var deserialized1c = reportingPeriod1.DeserializeFromString(typeof(IReportingPeriod<CalendarUnitOfTime>));
            var deserialized1d = reportingPeriod1.DeserializeFromString(typeof(ReportingPeriod<CalendarUnitOfTime>));

            var deserialized2a = reportingPeriod2.DeserializeFromString(typeof(IReportingPeriod<UnitOfTime>));
            var deserialized2b = reportingPeriod2.DeserializeFromString(typeof(ReportingPeriod<UnitOfTime>));
            var deserialized2c = reportingPeriod2.DeserializeFromString(typeof(IReportingPeriod<CalendarUnitOfTime>));
            var deserialized2d = reportingPeriod2.DeserializeFromString(typeof(ReportingPeriod<CalendarUnitOfTime>));

            var deserialized3a = reportingPeriod3.DeserializeFromString(typeof(IReportingPeriod<UnitOfTime>));
            var deserialized3b = reportingPeriod3.DeserializeFromString(typeof(ReportingPeriod<UnitOfTime>));
            var deserialized3c = reportingPeriod3.DeserializeFromString(typeof(IReportingPeriod<CalendarUnitOfTime>));
            var deserialized3d = reportingPeriod3.DeserializeFromString(typeof(ReportingPeriod<CalendarUnitOfTime>));
            var deserialized3e = reportingPeriod3.DeserializeFromString(typeof(IReportingPeriod<CalendarUnbounded>));
            var deserialized3f = reportingPeriod3.DeserializeFromString(typeof(ReportingPeriod<CalendarUnbounded>));

            // Assert
            deserialized1a.Should().Be(expected1);
            deserialized1b.Should().Be(expected1);
            deserialized1c.Should().Be(expected1);
            deserialized1d.Should().Be(expected1);

            deserialized2a.Should().Be(expected2);
            deserialized2b.Should().Be(expected2);
            deserialized2c.Should().Be(expected2);
            deserialized2d.Should().Be(expected2);

            deserialized3a.Should().Be(expected3);
            deserialized3b.Should().Be(expected3);
            deserialized3c.Should().Be(expected3);
            deserialized3d.Should().Be(expected3);
            deserialized3e.Should().Be(expected3);
            deserialized3f.Should().Be(expected3);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_FiscalUnbounded_string()
        {
            // Arrange
            var reportingPeriod1 = "f-unbounded,f-2002";
            var reportingPeriod2 = "f-2002,f-unbounded";
            var reportingPeriod3 = "f-unbounded,f-unbounded";

            var expected1 = new ReportingPeriod<FiscalUnitOfTime>(new FiscalUnbounded(), new FiscalYear(2002));
            var expected2 = new ReportingPeriod<FiscalUnitOfTime>(new FiscalYear(2002), new FiscalUnbounded());
            var expected3 = new ReportingPeriod<FiscalUnbounded>(new FiscalUnbounded(), new FiscalUnbounded());

            // Act
            var deserialized1a = reportingPeriod1.DeserializeFromString(typeof(IReportingPeriod<UnitOfTime>));
            var deserialized1b = reportingPeriod1.DeserializeFromString(typeof(ReportingPeriod<UnitOfTime>));
            var deserialized1c = reportingPeriod1.DeserializeFromString(typeof(IReportingPeriod<FiscalUnitOfTime>));
            var deserialized1d = reportingPeriod1.DeserializeFromString(typeof(ReportingPeriod<FiscalUnitOfTime>));

            var deserialized2a = reportingPeriod2.DeserializeFromString(typeof(IReportingPeriod<UnitOfTime>));
            var deserialized2b = reportingPeriod2.DeserializeFromString(typeof(ReportingPeriod<UnitOfTime>));
            var deserialized2c = reportingPeriod2.DeserializeFromString(typeof(IReportingPeriod<FiscalUnitOfTime>));
            var deserialized2d = reportingPeriod2.DeserializeFromString(typeof(ReportingPeriod<FiscalUnitOfTime>));

            var deserialized3a = reportingPeriod3.DeserializeFromString(typeof(IReportingPeriod<UnitOfTime>));
            var deserialized3b = reportingPeriod3.DeserializeFromString(typeof(ReportingPeriod<UnitOfTime>));
            var deserialized3c = reportingPeriod3.DeserializeFromString(typeof(IReportingPeriod<FiscalUnitOfTime>));
            var deserialized3d = reportingPeriod3.DeserializeFromString(typeof(ReportingPeriod<FiscalUnitOfTime>));
            var deserialized3e = reportingPeriod3.DeserializeFromString(typeof(IReportingPeriod<FiscalUnbounded>));
            var deserialized3f = reportingPeriod3.DeserializeFromString(typeof(ReportingPeriod<FiscalUnbounded>));

            // Assert
            deserialized1a.Should().Be(expected1);
            deserialized1b.Should().Be(expected1);
            deserialized1c.Should().Be(expected1);
            deserialized1d.Should().Be(expected1);

            deserialized2a.Should().Be(expected2);
            deserialized2b.Should().Be(expected2);
            deserialized2c.Should().Be(expected2);
            deserialized2d.Should().Be(expected2);

            deserialized3a.Should().Be(expected3);
            deserialized3b.Should().Be(expected3);
            deserialized3c.Should().Be(expected3);
            deserialized3d.Should().Be(expected3);
            deserialized3e.Should().Be(expected3);
            deserialized3f.Should().Be(expected3);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_GenericUnbounded_string()
        {
            // Arrange
            var reportingPeriod1 = "g-unbounded,g-2002";
            var reportingPeriod2 = "g-2002,g-unbounded";
            var reportingPeriod3 = "g-unbounded,g-unbounded";

            var expected1 = new ReportingPeriod<GenericUnitOfTime>(new GenericUnbounded(), new GenericYear(2002));
            var expected2 = new ReportingPeriod<GenericUnitOfTime>(new GenericYear(2002), new GenericUnbounded());
            var expected3 = new ReportingPeriod<GenericUnbounded>(new GenericUnbounded(), new GenericUnbounded());

            // Act
            var deserialized1a = reportingPeriod1.DeserializeFromString(typeof(IReportingPeriod<UnitOfTime>));
            var deserialized1b = reportingPeriod1.DeserializeFromString(typeof(ReportingPeriod<UnitOfTime>));
            var deserialized1c = reportingPeriod1.DeserializeFromString(typeof(IReportingPeriod<GenericUnitOfTime>));
            var deserialized1d = reportingPeriod1.DeserializeFromString(typeof(ReportingPeriod<GenericUnitOfTime>));

            var deserialized2a = reportingPeriod2.DeserializeFromString(typeof(IReportingPeriod<UnitOfTime>));
            var deserialized2b = reportingPeriod2.DeserializeFromString(typeof(ReportingPeriod<UnitOfTime>));
            var deserialized2c = reportingPeriod2.DeserializeFromString(typeof(IReportingPeriod<GenericUnitOfTime>));
            var deserialized2d = reportingPeriod2.DeserializeFromString(typeof(ReportingPeriod<GenericUnitOfTime>));

            var deserialized3a = reportingPeriod3.DeserializeFromString(typeof(IReportingPeriod<UnitOfTime>));
            var deserialized3b = reportingPeriod3.DeserializeFromString(typeof(ReportingPeriod<UnitOfTime>));
            var deserialized3c = reportingPeriod3.DeserializeFromString(typeof(IReportingPeriod<GenericUnitOfTime>));
            var deserialized3d = reportingPeriod3.DeserializeFromString(typeof(ReportingPeriod<GenericUnitOfTime>));
            var deserialized3e = reportingPeriod3.DeserializeFromString(typeof(IReportingPeriod<GenericUnbounded>));
            var deserialized3f = reportingPeriod3.DeserializeFromString(typeof(ReportingPeriod<GenericUnbounded>));

            // Assert
            deserialized1a.Should().Be(expected1);
            deserialized1b.Should().Be(expected1);
            deserialized1c.Should().Be(expected1);
            deserialized1d.Should().Be(expected1);

            deserialized2a.Should().Be(expected2);
            deserialized2b.Should().Be(expected2);
            deserialized2c.Should().Be(expected2);
            deserialized2d.Should().Be(expected2);

            deserialized3a.Should().Be(expected3);
            deserialized3b.Should().Be(expected3);
            deserialized3c.Should().Be(expected3);
            deserialized3d.Should().Be(expected3);
            deserialized3e.Should().Be(expected3);
            deserialized3f.Should().Be(expected3);
        }

        [Fact]
        public static void SerializeToString__Should_throw_ArgumentNullException___When_parameter_reportingPeriod_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.SerializeToString(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Need to test various flavors of unit-of-time.")]
        public static void SerializeToString__Should_return_expected_serialized_string_representation_of_reportingPeriod___When_reportingPeriod_is_a_IReportingPeriod()
        {
            // Arrange
            var reportingPeriods = new Dictionary<string, IReportingPeriod<UnitOfTime>>
            {
                { "c-2017-05-17,c-2018-12-09", new ReportingPeriod<CalendarDay>(new CalendarDay(2017, MonthOfYear.May, DayOfMonth.Seventeen), new CalendarDay(2018, MonthOfYear.December, DayOfMonth.Nine)) },
                { "c-2017-05,c-2018-12", new ReportingPeriod<CalendarMonth>(new CalendarMonth(2017, MonthOfYear.May), new CalendarMonth(2018, MonthOfYear.December)) },
                { "f-2017-05,f-2018-12", new ReportingPeriod<FiscalMonth>(new FiscalMonth(2017, MonthNumber.Five), new FiscalMonth(2018, MonthNumber.Twelve)) },
                { "g-2017-05,g-2018-12", new ReportingPeriod<GenericMonth>(new GenericMonth(2017, MonthNumber.Five), new GenericMonth(2018, MonthNumber.Twelve)) },
                { "c-2017-Q2,c-2018-Q4", new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2017, QuarterNumber.Q2), new CalendarQuarter(2018, QuarterNumber.Q4)) },
                { "f-2017-Q2,f-2018-Q4", new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2017, QuarterNumber.Q2), new FiscalQuarter(2018, QuarterNumber.Q4)) },
                { "g-2017-Q2,g-2018-Q4", new ReportingPeriod<GenericQuarter>(new GenericQuarter(2017, QuarterNumber.Q2), new GenericQuarter(2018, QuarterNumber.Q4)) },
                { "c-2017,c-2018", new ReportingPeriod<CalendarYear>(new CalendarYear(2017), new CalendarYear(2018)) },
                { "f-2017,f-2018", new ReportingPeriod<FiscalYear>(new FiscalYear(2017), new FiscalYear(2018)) },
                { "g-2017,g-2018", new ReportingPeriod<GenericYear>(new GenericYear(2017), new GenericYear(2018)) },
                { "c-unbounded,c-2012-02", new ReportingPeriod<CalendarUnitOfTime>(new CalendarUnbounded(), new CalendarMonth(2012, MonthOfYear.February)) },
                { "g-2012-02,g-unbounded", new ReportingPeriod<GenericUnitOfTime>(new GenericMonth(2012, MonthNumber.Two), new GenericUnbounded()) },
                { "f-unbounded,f-unbounded", new ReportingPeriod<FiscalUnitOfTime>(new FiscalUnbounded(), new FiscalUnbounded()) },
            };

            // Act
            var serialized = reportingPeriods.Select(_ => new { Actual = _.Value.SerializeToString(), Expected = _.Key }).ToList();

            // Assert
            serialized.ForEach(_ => _.Actual.Should().Be(_.Expected));
        }
    }
}
