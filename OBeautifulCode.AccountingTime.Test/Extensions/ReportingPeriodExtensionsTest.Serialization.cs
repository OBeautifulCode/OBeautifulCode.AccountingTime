// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriodExtensionsTest.Serialization.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Test
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using FluentAssertions;

    using Xunit;

    [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Testing this class requires lots of types because of the number of unit-of-time types intersected with the options for reporting period.")]
    public static partial class ReportingPeriodExtensionsTest
    {
        [Fact]
        public static void DeserializeFromString___Should_throw_ArgumentNullException___When_parameter_unitOfTime_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.DeserializeFromString(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void DeserializeFromString___Should_throw_ArgumentException___When_parameter_unitOfTime_is_white_space()
        {
            // Arrange
            var reportingPeriods = new[] { string.Empty, "  ", "  \r\n " };

            // Act
            var exceptions = reportingPeriods.Select(_ => Record.Exception(_.DeserializeFromString)).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<ArgumentException>());
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
            var exceptions = unitsOfTime.Select(_ => Record.Exception(_.DeserializeFromString)).ToList();

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
            var exceptions = unitsOfTime.Select(_ => Record.Exception(_.DeserializeFromString)).ToList();

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
            var exceptions = unitsOfTime.Select(_ => Record.Exception(_.DeserializeFromString)).ToList();

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
            var exceptions1 = unitsOfTime1.Select(_ => Record.Exception(_.DeserializeFromString)).ToList();
            var exceptions2 = unitsOfTime2.Select(_ => Record.Exception(_.DeserializeFromString)).ToList();

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
            var exceptions = unitsOfTime.Select(_ => Record.Exception(_.DeserializeFromString)).ToList();

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
            var exceptions = unitsOfTime.Select(_ => Record.Exception(_.DeserializeFromString)).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_ReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_CalendarDay_string()
        {
            // Arrange
            var reportingPeriod = "c-2001-01-10,c-2016-02-29";
            var expected = new ReportingPeriod(new CalendarDay(2001, MonthOfYear.January, DayOfMonth.Ten), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine));

            // Act
            var deserialized = reportingPeriod.DeserializeFromString();

            // Assert
            deserialized.Should().Be(expected);
        }

        [Fact]
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_ReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_CalendarMonth_string()
        {
            // Arrange
            var reportingPeriod = "c-2001-01,c-2001-02";
            var expected = new ReportingPeriod(new CalendarMonth(2001, MonthOfYear.January), new CalendarMonth(2001, MonthOfYear.February));

            // Act
            var deserialized = reportingPeriod.DeserializeFromString();

            // Assert
            deserialized.Should().Be(expected);
        }

        [Fact]
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_ReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_FiscalMonth_string()
        {
            // Arrange
            var reportingPeriod = "f-2001-01,f-2001-02";
            var expected = new ReportingPeriod(new FiscalMonth(2001, MonthNumber.One), new FiscalMonth(2001, MonthNumber.Two));

            // Act
            var deserialized = reportingPeriod.DeserializeFromString();

            // Assert
            deserialized.Should().Be(expected);
        }

        [Fact]
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_ReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_GenericMonth_string()
        {
            // Arrange
            var reportingPeriod = "g-2001-01,g-2001-02";
            var expected = new ReportingPeriod(new GenericMonth(2001, MonthNumber.One), new GenericMonth(2001, MonthNumber.Two));

            // Act
            var deserialized = reportingPeriod.DeserializeFromString();

            // Assert
            deserialized.Should().Be(expected);
        }

        [Fact]
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_ReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_CalendarQuarter_string()
        {
            // Arrange
            var reportingPeriod = "c-2001-Q1,c-2001-Q2";
            var expected = new ReportingPeriod(new CalendarQuarter(2001, QuarterNumber.Q1), new CalendarQuarter(2001, QuarterNumber.Q2));

            // Act
            var deserialized = reportingPeriod.DeserializeFromString();

            // Assert
            deserialized.Should().Be(expected);
        }

        [Fact]
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_ReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_FiscalQuarter_string()
        {
            // Arrange
            var reportingPeriod = "f-2001-Q1,f-2001-Q2";
            var expected = new ReportingPeriod(new FiscalQuarter(2001, QuarterNumber.Q1), new FiscalQuarter(2001, QuarterNumber.Q2));

            // Act
            var deserialized = reportingPeriod.DeserializeFromString();

            // Assert
            deserialized.Should().Be(expected);
        }

        [Fact]
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_ReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_GenericQuarter_string()
        {
            // Arrange
            var reportingPeriod = "g-2001-Q1,g-2001-Q2";
            var expected = new ReportingPeriod(new GenericQuarter(2001, QuarterNumber.Q1), new GenericQuarter(2001, QuarterNumber.Q2));

            // Act
            var deserialized = reportingPeriod.DeserializeFromString();

            // Assert
            deserialized.Should().Be(expected);
        }

        [Fact]
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_ReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_CalendarYear_string()
        {
            // Arrange
            var reportingPeriod = "c-2001,c-2002";
            var expected = new ReportingPeriod(new CalendarYear(2001), new CalendarYear(2002));

            // Act
            var deserialized = reportingPeriod.DeserializeFromString();

            // Assert
            deserialized.Should().Be(expected);
        }

        [Fact]
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_ReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_FiscalYear_string()
        {
            // Arrange
            var reportingPeriod = "f-2001,f-2002";
            var expected = new ReportingPeriod(new FiscalYear(2001), new FiscalYear(2002));

            // Act
            var deserialized = reportingPeriod.DeserializeFromString();

            // Assert
            deserialized.Should().Be(expected);
        }

        [Fact]
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_ReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_GenericYear_string()
        {
            // Arrange
            var reportingPeriod = "g-2001,g-2002";
            var expected = new ReportingPeriod(new GenericYear(2001), new GenericYear(2002));

            // Act
            var deserialized = reportingPeriod.DeserializeFromString();

            // Assert
            deserialized.Should().Be(expected);
        }

        [Fact]
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_ReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_CalendarUnbounded_string()
        {
            // Arrange
            var reportingPeriod1 = "c-unbounded,c-2002";
            var reportingPeriod2 = "c-2002,c-unbounded";
            var reportingPeriod3 = "c-unbounded,c-unbounded";

            var expected1 = new ReportingPeriod(new CalendarUnbounded(), new CalendarYear(2002));
            var expected2 = new ReportingPeriod(new CalendarYear(2002), new CalendarUnbounded());
            var expected3 = new ReportingPeriod(new CalendarUnbounded(), new CalendarUnbounded());

            // Act
            var deserialized1 = reportingPeriod1.DeserializeFromString();
            var deserialized2 = reportingPeriod2.DeserializeFromString();
            var deserialized3 = reportingPeriod3.DeserializeFromString();

            // Assert
            deserialized1.Should().Be(expected1);
            deserialized2.Should().Be(expected2);
            deserialized3.Should().Be(expected3);
        }

        [Fact]
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_ReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_FiscalUnbounded_string()
        {
            // Arrange
            var reportingPeriod1 = "f-unbounded,f-2002";
            var reportingPeriod2 = "f-2002,f-unbounded";
            var reportingPeriod3 = "f-unbounded,f-unbounded";

            var expected1 = new ReportingPeriod(new FiscalUnbounded(), new FiscalYear(2002));
            var expected2 = new ReportingPeriod(new FiscalYear(2002), new FiscalUnbounded());
            var expected3 = new ReportingPeriod(new FiscalUnbounded(), new FiscalUnbounded());

            // Act
            var deserialized1 = reportingPeriod1.DeserializeFromString();
            var deserialized2 = reportingPeriod2.DeserializeFromString();
            var deserialized3 = reportingPeriod3.DeserializeFromString();

            // Assert
            deserialized1.Should().Be(expected1);
            deserialized2.Should().Be(expected2);
            deserialized3.Should().Be(expected3);
        }

        [Fact]
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_ReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_GenericUnbounded_string()
        {
            // Arrange
            var reportingPeriod1 = "g-unbounded,g-2002";
            var reportingPeriod2 = "g-2002,g-unbounded";
            var reportingPeriod3 = "g-unbounded,g-unbounded";

            var expected1 = new ReportingPeriod(new GenericUnbounded(), new GenericYear(2002));
            var expected2 = new ReportingPeriod(new GenericYear(2002), new GenericUnbounded());
            var expected3 = new ReportingPeriod(new GenericUnbounded(), new GenericUnbounded());

            // Act
            var deserialized1 = reportingPeriod1.DeserializeFromString();
            var deserialized2 = reportingPeriod2.DeserializeFromString();
            var deserialized3 = reportingPeriod3.DeserializeFromString();

            // Assert
            deserialized1.Should().Be(expected1);
            deserialized2.Should().Be(expected2);
            deserialized3.Should().Be(expected3);
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
        [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Need to test various flavors of unit-of-time.")]
        public static void SerializeToString__Should_return_expected_serialized_string_representation_of_reportingPeriod___When_called()
        {
            // Arrange
            var reportingPeriods = new Dictionary<string, ReportingPeriod>
            {
                { "c-2017-05-17,c-2018-12-09", new ReportingPeriod(new CalendarDay(2017, MonthOfYear.May, DayOfMonth.Seventeen), new CalendarDay(2018, MonthOfYear.December, DayOfMonth.Nine)) },
                { "c-2017-05,c-2018-12", new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.May), new CalendarMonth(2018, MonthOfYear.December)) },
                { "f-2017-05,f-2018-12", new ReportingPeriod(new FiscalMonth(2017, MonthNumber.Five), new FiscalMonth(2018, MonthNumber.Twelve)) },
                { "g-2017-05,g-2018-12", new ReportingPeriod(new GenericMonth(2017, MonthNumber.Five), new GenericMonth(2018, MonthNumber.Twelve)) },
                { "c-2017-Q2,c-2018-Q4", new ReportingPeriod(new CalendarQuarter(2017, QuarterNumber.Q2), new CalendarQuarter(2018, QuarterNumber.Q4)) },
                { "f-2017-Q2,f-2018-Q4", new ReportingPeriod(new FiscalQuarter(2017, QuarterNumber.Q2), new FiscalQuarter(2018, QuarterNumber.Q4)) },
                { "g-2017-Q2,g-2018-Q4", new ReportingPeriod(new GenericQuarter(2017, QuarterNumber.Q2), new GenericQuarter(2018, QuarterNumber.Q4)) },
                { "c-2017,c-2018", new ReportingPeriod(new CalendarYear(2017), new CalendarYear(2018)) },
                { "f-2017,f-2018", new ReportingPeriod(new FiscalYear(2017), new FiscalYear(2018)) },
                { "g-2017,g-2018", new ReportingPeriod(new GenericYear(2017), new GenericYear(2018)) },
                { "c-unbounded,c-2012-02", new ReportingPeriod(new CalendarUnbounded(), new CalendarMonth(2012, MonthOfYear.February)) },
                { "g-2012-02,g-unbounded", new ReportingPeriod(new GenericMonth(2012, MonthNumber.Two), new GenericUnbounded()) },
                { "f-unbounded,f-unbounded", new ReportingPeriod(new FiscalUnbounded(), new FiscalUnbounded()) },
            };

            // Act
            var serialized = reportingPeriods.Select(_ => new { Actual = _.Value.SerializeToString(), Expected = _.Key }).ToList();

            // Assert
            serialized.ForEach(_ => _.Actual.Should().Be(_.Expected));
        }
    }
}
