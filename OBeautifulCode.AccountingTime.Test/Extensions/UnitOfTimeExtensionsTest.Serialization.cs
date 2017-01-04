// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfTimeExtensionsTest.Serialization.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Xunit;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "There are many kinds of units-of-time.")]
    public static partial class UnitOfTimeExtensionsTest
    {
        // ReSharper disable InconsistentNaming
        [Fact]
        public static void DeserializeFromSortableString___Should_throw_ArgumentNullException___When_parameter_unitOfTime_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => UnitOfTimeExtensions.DeserializeFromSortableString<UnitOfTime>(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_throw_ArgumentException___When_parameter_unitOfTime_is_whitespace()
        {
            // Arrange
            var unitsOfTime = new[] { string.Empty, "  ", "  \r\n " };

            // Act
            var exceptions = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromSortableString<UnitOfTime>())).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<ArgumentException>());
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "This test is inherently complex.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "This test is inherently complex.")]
        public static void DeserializeFromSortableString___Should_throw_InvalidOperationException___When_the_kind_of_unit_of_time_encoded_cannot_be_casted_to_specified_generic_type_parameter()
        {
            // Arrange
            var unitsOfTime = new Dictionary<string, IEnumerable<Type>>
            {
                { "c-2015-11-11", Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(CalendarDay)) && (_ != typeof(CalendarUnitOfTime))) },
                { "c-2017-03", Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(CalendarMonth)) && (_ != typeof(CalendarUnitOfTime))) },
                { "c-2017-Q1", Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(CalendarQuarter)) && (_ != typeof(CalendarUnitOfTime))) },
                { "c-2017", Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(CalendarYear)) && (_ != typeof(CalendarUnitOfTime))) },
                { "c-unbounded", Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(CalendarUnbounded)) && (_ != typeof(CalendarUnitOfTime))) },
                { "f-2017-03", Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(FiscalMonth)) && (_ != typeof(FiscalUnitOfTime))) },
                { "f-2017-Q1", Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(FiscalQuarter)) && (_ != typeof(FiscalUnitOfTime))) },
                { "f-2017", Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(FiscalYear)) && (_ != typeof(FiscalUnitOfTime))) },
                { "f-unbounded", Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(FiscalUnbounded)) && (_ != typeof(FiscalUnitOfTime))) },
                { "g-2017-03", Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(GenericQuarter)) && (_ != typeof(GenericUnitOfTime))) },
                { "g-2017-Q1", Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(GenericQuarter)) && (_ != typeof(GenericUnitOfTime))) },
                { "g-2017",  Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(GenericYear)) && (_ != typeof(GenericUnitOfTime))) },
                { "g-unbounded", Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(GenericUnbounded)) && (_ != typeof(GenericUnitOfTime))) }
            };

            var deserializeFromSortableString = typeof(UnitOfTimeExtensions).GetMethod(nameof(UnitOfTimeExtensions.DeserializeFromSortableString));

            // Act
            var exceptions = new List<Exception>();
            foreach (var unitOfTime in unitsOfTime.Keys)
            {
                foreach (var type in unitsOfTime[unitOfTime])
                {
                    var genericMethod = deserializeFromSortableString.MakeGenericMethod(type);
                    // ReSharper disable PossibleNullReferenceException
                    exceptions.Add(Record.Exception(() => genericMethod.Invoke(null, new object[] { unitOfTime })).InnerException);
                    // ReSharper restore PossibleNullReferenceException
                }
            }

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_throw_InvalidOperationException___When_unitOfTime_is_a_malformed_CalendarDay()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                "c-201a-11-11",
                "c-xxxx-11-11",
                "c-10000-11-11",
                "c-T001-11-11",
                "c-0-11-11",
                "c-200-11-11",
                "c-0000-11-11",
                "c-999-11-11",
                "c-2007-1-11",
                "c-2007-9-11",
                "c-2007-13-11",
                "c-2007-99-11",
                "c-2007-00-11",
                "c-2007-001-11",
                "c-2007-012-11",
                "c-2007-11-1",
                "c-2007-11-9",
                "c-2007-11-32",
                "c-2007-11-31",
                "c-2015-02-29",
                "c-2015-03-00",
                "c-2015-03-001",
                "c-2015-03-030"
            };

            // Act
            var exceptions = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromSortableString<UnitOfTime>())).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_throw_InvalidOperationException___When_unitOfTime_is_a_malformed_CalendarMonth()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                "c-201a-11",
                "c-xxxx-11",
                "c-10000-11",
                "c-T001-11",
                "c-0-11",
                "c-200-11",
                "c-0000-11",
                "c-999-11",
                "c-2007-1",
                "c-2007-9",
                "c-2007-13",
                "c-2007-99",
                "c-2007-00",
                "c-2007-001",
                "c-2007-012"
            };

            // Act
            var exceptions = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromSortableString<UnitOfTime>())).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_throw_InvalidOperationException___When_unitOfTime_is_a_malformed_FiscalMonth()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                "f-201a-11",
                "f-xxxx-11",
                "f-10000-11",
                "f-T001-11",
                "f-0-11",
                "f-200-11",
                "f-0000-11",
                "f-999-11",
                "f-2007-1",
                "f-2007-9",
                "f-2007-13",
                "f-2007-99",
                "f-2007-00",
                "f-2007-001",
                "f-2007-012"
            };

            // Act
            var exceptions = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromSortableString<UnitOfTime>())).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_throw_InvalidOperationException___When_unitOfTime_is_a_malformed_GenericMonth()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                "g-201a-11",
                "g-xxxx-11",
                "g-10000-11",
                "g-T001-11",
                "g-0-11",
                "g-200-11",
                "g-0000-11",
                "g-999-11",
                "g-2007-1",
                "g-2007-9",
                "g-2007-13",
                "g-2007-99",
                "g-2007-00",
                "g-2007-001",
                "g-2007-012"
            };

            // Act
            var exceptions = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromSortableString<UnitOfTime>())).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_throw_InvalidOperationException___When_unitOfTime_is_a_malformed_CalendarQuarter()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                "c-201a-Q3",
                "c-xxxx-Q3",
                "c-10000-Q3",
                "c-T001-Q3",
                "c-0-Q3",
                "c-200-Q3",
                "c-0000-Q3",
                "c-999-Q3",
                "c-2007-Q01",
                "c-2007-Q00",
                "c-2007-Q004",
                "c-2007-Q5",
                "c-2007-Q31",
                "c-2007-1",
                "c-2007Q-1"
            };

            // Act
            var exceptions = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromSortableString<UnitOfTime>())).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_throw_InvalidOperationException___When_unitOfTime_is_a_malformed_FiscalQuarter()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                "f-201a-Q3",
                "f-xxxx-Q3",
                "f-10000-Q3",
                "f-T001-Q3",
                "f-0-Q3",
                "f-200-Q3",
                "f-0000-Q3",
                "f-999-Q3",
                "f-2007-Q01",
                "f-2007-Q00",
                "f-2007-Q004",
                "f-2007-Q5",
                "f-2007-Q31",
                "f-2007-1",
                "f-2007Q-1"
            };

            // Act
            var exceptions = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromSortableString<UnitOfTime>())).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_throw_InvalidOperationException___When_unitOfTime_is_a_malformed_GenericQuarter()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                "g-201a-Q3",
                "g-xxxx-Q3",
                "g-10000-Q3",
                "g-T001-Q3",
                "g-0-Q3",
                "g-200-Q3",
                "g-0000-Q3",
                "g-999-Q3",
                "g-2007-Q01",
                "g-2007-Q00",
                "g-2007-Q004",
                "g-2007-Q5",
                "g-2007-Q31",
                "g-2007-1",
                "g-2007Q-1"
            };

            // Act
            var exceptions = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromSortableString<UnitOfTime>())).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_throw_InvalidOperationException___When_unitOfTime_is_a_malformed_CalendarYear()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                "c-201a",
                "c-xxxx",
                "c-10000",
                "c-T001",
                "c-0",
                "c-200",
                "c-0000",
                "c-999"
            };

            // Act
            var exceptions = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromSortableString<UnitOfTime>())).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_throw_InvalidOperationException___When_unitOfTime_is_a_malformed_FiscalYear()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                "f-201a",
                "f-xxxx",
                "f-10000",
                "f-T001",
                "f-0",
                "f-200",
                "f-0000",
                "f-999"
            };

            // Act
            var exceptions = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromSortableString<UnitOfTime>())).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_throw_InvalidOperationException___When_unitOfTime_is_a_malformed_GenericYear()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                "g-201a",
                "g-xxxx",
                "g-10000",
                "g-T001",
                "g-0",
                "g-200",
                "g-0000",
                "g-999"
            };

            // Act
            var exceptions = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromSortableString<UnitOfTime>())).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_throw_InvalidOperationException___When_unitOfTime_is_a_malformed_CalendarUnbounded()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                "c-unbounded-",
                "c-unbounded--",
                "cunbounded",
                "cu-unbounded"
            };

            // Act
            var exceptions = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromSortableString<UnitOfTime>())).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_throw_InvalidOperationException___When_unitOfTime_is_a_malformed_FiscalUnbounded()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                "f-unbounded-",
                "f-unbounded--",
                "funbounded",
                "fu-unbounded"
            };

            // Act
            var exceptions = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromSortableString<UnitOfTime>())).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_throw_InvalidOperationException___When_unitOfTime_is_a_malformed_GenericUnbounded()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                "g-unbounded-",
                "g-unbounded--",
                "gunbounded",
                "gu-unbounded"
            };

            // Act
            var exceptions = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromSortableString<UnitOfTime>())).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_deserialize_a_CalendarDay___When_unitOfTime_is_a_well_formed_CalendarDay()
        {
            // Arrange
            var unitsOfTime = new Dictionary<string, CalendarDay>
            {
                { "c-2001-01-09", new CalendarDay(2001, MonthOfYear.January, DayOfMonth.Nine) },
                { "c-2016-02-29", new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine) },
                { "c-2001-11-04", new CalendarDay(2001, MonthOfYear.November, DayOfMonth.Four) },
                { "c-2001-12-30", new CalendarDay(2001, MonthOfYear.December, DayOfMonth.Thirty) }
            };

            // Act
            var deserialized1 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<UnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized2 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<CalendarUnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized3 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<CalendarDay>(), Expected = _.Value }).ToList();

            // Assert
            deserialized1.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized2.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized3.ForEach(_ => _.Actual.Should().Be(_.Expected));
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_deserialize_a_CalendarMonth___When_unitOfTime_is_a_well_formed_CalendarMonth()
        {
            // Arrange
            var unitsOfTime = new Dictionary<string, CalendarMonth>
            {
                { "c-2001-01", new CalendarMonth(2001, MonthOfYear.January) },
                { "c-2002-07", new CalendarMonth(2002, MonthOfYear.July) },
                { "c-2010-11", new CalendarMonth(2010, MonthOfYear.November) },
                { "c-2016-12", new CalendarMonth(2016, MonthOfYear.December) }
            };

            // Act
            var deserialized1 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<UnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized2 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<CalendarUnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized3 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<CalendarMonth>(), Expected = _.Value }).ToList();

            // Assert
            deserialized1.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized2.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized3.ForEach(_ => _.Actual.Should().Be(_.Expected));
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_deserialize_a_FiscalMonth___When_unitOfTime_is_a_well_formed_FiscalMonth()
        {
            // Arrange
            var unitsOfTime = new Dictionary<string, FiscalMonth>
            {
                { "f-2001-01", new FiscalMonth(2001, MonthNumber.One) },
                { "f-2002-07", new FiscalMonth(2002, MonthNumber.Seven) },
                { "f-2010-11", new FiscalMonth(2010, MonthNumber.Eleven) },
                { "f-2016-12", new FiscalMonth(2016, MonthNumber.Twelve) }
            };

            // Act
            var deserialized1 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<UnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized2 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<FiscalUnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized3 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<FiscalMonth>(), Expected = _.Value }).ToList();

            // Assert
            deserialized1.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized2.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized3.ForEach(_ => _.Actual.Should().Be(_.Expected));
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_deserialize_a_GenericMonth___When_unitOfTime_is_a_well_formed_GenericMonth()
        {
            // Arrange
            var unitsOfTime = new Dictionary<string, GenericMonth>
            {
                { "g-2001-01", new GenericMonth(2001, MonthNumber.One) },
                { "g-2002-07", new GenericMonth(2002, MonthNumber.Seven) },
                { "g-2010-11", new GenericMonth(2010, MonthNumber.Eleven) },
                { "g-2016-12", new GenericMonth(2016, MonthNumber.Twelve) }
            };

            // Act
            var deserialized1 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<UnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized2 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<GenericUnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized3 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<GenericMonth>(), Expected = _.Value }).ToList();

            // Assert
            deserialized1.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized2.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized3.ForEach(_ => _.Actual.Should().Be(_.Expected));
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_deserialize_a_CalendarQuarter___When_unitOfTime_is_a_well_formed_CalendarQuarter()
        {
            // Arrange
            var unitsOfTime = new Dictionary<string, CalendarQuarter>
            {
                { "c-2001-Q1", new CalendarQuarter(2001, QuarterNumber.Q1) },
                { "c-2002-Q2", new CalendarQuarter(2002, QuarterNumber.Q2) },
                { "c-2010-Q3", new CalendarQuarter(2010, QuarterNumber.Q3) },
                { "c-2016-Q4", new CalendarQuarter(2016, QuarterNumber.Q4) }
            };

            // Act
            var deserialized1 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<UnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized2 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<CalendarUnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized3 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<CalendarQuarter>(), Expected = _.Value }).ToList();

            // Assert
            deserialized1.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized2.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized3.ForEach(_ => _.Actual.Should().Be(_.Expected));
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_deserialize_a_FiscalQuarter___When_unitOfTime_is_a_well_formed_FiscalQuarter()
        {
            // Arrange
            var unitsOfTime = new Dictionary<string, FiscalQuarter>
            {
                { "f-2001-Q1", new FiscalQuarter(2001, QuarterNumber.Q1) },
                { "f-2002-Q2", new FiscalQuarter(2002, QuarterNumber.Q2) },
                { "f-2010-Q3", new FiscalQuarter(2010, QuarterNumber.Q3) },
                { "f-2016-Q4", new FiscalQuarter(2016, QuarterNumber.Q4) }
            };

            // Act
            var deserialized1 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<UnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized2 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<FiscalUnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized3 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<FiscalQuarter>(), Expected = _.Value }).ToList();

            // Assert
            deserialized1.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized2.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized3.ForEach(_ => _.Actual.Should().Be(_.Expected));
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_deserialize_a_GenericQuarter___When_unitOfTime_is_a_well_formed_GenericQuarter()
        {
            // Arrange
            var unitsOfTime = new Dictionary<string, GenericQuarter>
            {
                { "g-2001-Q1", new GenericQuarter(2001, QuarterNumber.Q1) },
                { "g-2002-Q2", new GenericQuarter(2002, QuarterNumber.Q2) },
                { "g-2010-Q3", new GenericQuarter(2010, QuarterNumber.Q3) },
                { "g-2016-Q4", new GenericQuarter(2016, QuarterNumber.Q4) }
            };

            // Act
            var deserialized1 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<UnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized2 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<GenericUnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized3 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<GenericQuarter>(), Expected = _.Value }).ToList();

            // Assert
            deserialized1.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized2.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized3.ForEach(_ => _.Actual.Should().Be(_.Expected));
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_deserialize_a_CalendarYear___When_unitOfTime_is_a_well_formed_CalendarYear()
        {
            // Arrange
            var unitsOfTime = new Dictionary<string, CalendarYear>
            {
                { "c-2001", new CalendarYear(2001) },
                { "c-2016", new CalendarYear(2016) }
            };

            // Act
            var deserialized1 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<UnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized2 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<CalendarUnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized3 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<CalendarYear>(), Expected = _.Value }).ToList();

            // Assert
            deserialized1.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized2.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized3.ForEach(_ => _.Actual.Should().Be(_.Expected));
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_deserialize_a_FiscalYear___When_unitOfTime_is_a_well_formed_FiscalYear()
        {
            // Arrange
            var unitsOfTime = new Dictionary<string, FiscalYear>
            {
                { "f-2001", new FiscalYear(2001) },
                { "f-2016", new FiscalYear(2016) }
            };

            // Act
            var deserialized1 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<UnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized2 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<FiscalUnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized3 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<FiscalYear>(), Expected = _.Value }).ToList();

            // Assert
            deserialized1.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized2.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized3.ForEach(_ => _.Actual.Should().Be(_.Expected));
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_deserialize_a_GenericYear___When_unitOfTime_is_a_well_formed_GenericYear()
        {
            // Arrange
            var unitsOfTime = new Dictionary<string, GenericYear>
            {
                { "g-2001", new GenericYear(2001) },
                { "g-2016", new GenericYear(2016) }
            };

            // Act
            var deserialized1 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<UnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized2 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<GenericUnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized3 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<GenericYear>(), Expected = _.Value }).ToList();

            // Assert
            deserialized1.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized2.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized3.ForEach(_ => _.Actual.Should().Be(_.Expected));
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_deserialize_a_CalendarUnbounded___When_unitOfTime_is_a_well_formed_CalendarUnbounded()
        {
            // Arrange
            var unitsOfTime = new Dictionary<string, CalendarUnbounded>
            {
                { "c-unbounded", new CalendarUnbounded() }
            };

            // Act
            var deserialized1 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<UnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized2 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<CalendarUnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized3 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<CalendarUnbounded>(), Expected = _.Value }).ToList();

            // Assert
            deserialized1.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized2.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized3.ForEach(_ => _.Actual.Should().Be(_.Expected));
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_deserialize_a_FiscalUnbounded___When_unitOfTime_is_a_well_formed_FiscalUnbounded()
        {
            // Arrange
            var unitsOfTime = new Dictionary<string, FiscalUnbounded>
            {
                { "f-unbounded", new FiscalUnbounded() }
            };

            // Act
            var deserialized1 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<UnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized2 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<FiscalUnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized3 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<FiscalUnbounded>(), Expected = _.Value }).ToList();

            // Assert
            deserialized1.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized2.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized3.ForEach(_ => _.Actual.Should().Be(_.Expected));
        }

        [Fact]
        public static void DeserializeFromSortableString___Should_deserialize_a_GenericUnbounded___When_unitOfTime_is_a_well_formed_GenericUnbounded()
        {
            // Arrange
            var unitsOfTime = new Dictionary<string, GenericUnbounded>
            {
                { "g-unbounded", new GenericUnbounded() }
            };

            // Act
            var deserialized1 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<UnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized2 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<GenericUnitOfTime>(), Expected = _.Value }).ToList();
            var deserialized3 = unitsOfTime.Select(_ => new { Actual = _.Key.DeserializeFromSortableString<GenericUnbounded>(), Expected = _.Value }).ToList();

            // Assert
            deserialized1.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized2.ForEach(_ => _.Actual.Should().Be(_.Expected));
            deserialized3.ForEach(_ => _.Actual.Should().Be(_.Expected));
        }

        [Fact]
        public static void SerializeToSortableString___Should_throw_ArgumentNullException___When_parameter_unitOfTime_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => UnitOfTimeExtensions.SerializeToSortableString(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void SerializeToSortableString___Should_return_expected_serialized_sortable_string_representation_of_unitOfTime___When_unitOfTime_is_a_CalendarDay()
        {
            // Arrange
            var calendarDaysBySerializedString = new Dictionary<string, UnitOfTime>
            {
                { "c-2017-01-03", new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Three) },
                { "c-2017-11-09", new CalendarDay(2017, MonthOfYear.November, DayOfMonth.Nine) },
                { "c-2017-07-21", new CalendarDay(2017, MonthOfYear.July, DayOfMonth.TwentyOne) },
                { "c-2017-10-08", new CalendarDay(2017, MonthOfYear.October, DayOfMonth.Eight) },
                { "c-2017-11-30",  new CalendarDay(2017, MonthOfYear.November, DayOfMonth.Thirty) }
            };

            // Act
            var results = calendarDaysBySerializedString.Select(_ => new { Expected = _.Key, Actual = _.Value.SerializeToSortableString() }).ToList();

            // Assert
            results.All(_ => _.Actual == _.Expected).Should().BeTrue();
        }

        [Fact]
        public static void SerializeToSortableString___Should_return_expected_serialized_sortable_string_representation_of_unitOfTime___When_unitOfTime_is_a_CalendarMonth()
        {
            // Arrange
            var calendarDaysBySerializedString = new Dictionary<string, UnitOfTime>
            {
                { "c-2017-01", new CalendarMonth(2017, MonthOfYear.January) },
                { "c-2017-07", new CalendarMonth(2017, MonthOfYear.July) },
                { "c-2017-11", new CalendarMonth(2017, MonthOfYear.November) }
            };

            // Act
            var results = calendarDaysBySerializedString.Select(_ => new { Expected = _.Key, Actual = _.Value.SerializeToSortableString() }).ToList();

            // Assert
            results.All(_ => _.Actual == _.Expected).Should().BeTrue();
        }

        [Fact]
        public static void SerializeToSortableString___Should_return_expected_serialized_sortable_string_representation_of_unitOfTime___When_unitOfTime_is_a_FiscalMonth()
        {
            // Arrange
            var calendarDaysBySerializedString = new Dictionary<string, UnitOfTime>
            {
                { "f-2017-01", new FiscalMonth(2017, MonthNumber.One) },
                { "f-2017-07", new FiscalMonth(2017, MonthNumber.Seven) },
                { "f-2017-11", new FiscalMonth(2017, MonthNumber.Eleven) }
            };

            // Act
            var results = calendarDaysBySerializedString.Select(_ => new { Expected = _.Key, Actual = _.Value.SerializeToSortableString() }).ToList();

            // Assert
            results.All(_ => _.Actual == _.Expected).Should().BeTrue();
        }

        [Fact]
        public static void SerializeToSortableString___Should_return_expected_serialized_sortable_string_representation_of_unitOfTime___When_unitOfTime_is_a_GenericMonth()
        {
            // Arrange
            var calendarDaysBySerializedString = new Dictionary<string, UnitOfTime>
            {
                { "g-2017-01", new GenericMonth(2017, MonthNumber.One) },
                { "g-2017-07", new GenericMonth(2017, MonthNumber.Seven) },
                { "g-2017-11", new GenericMonth(2017, MonthNumber.Eleven) }
            };

            // Act
            var results = calendarDaysBySerializedString.Select(_ => new { Expected = _.Key, Actual = _.Value.SerializeToSortableString() }).ToList();

            // Assert
            results.All(_ => _.Actual == _.Expected).Should().BeTrue();
        }

        [Fact]
        public static void SerializeToSortableString___Should_return_expected_serialized_sortable_string_representation_of_unitOfTime___When_unitOfTime_is_a_CalendarQuarter()
        {
            // Arrange
            var calendarDaysBySerializedString = new Dictionary<string, UnitOfTime>
            {
                { "c-2017-Q1", new CalendarQuarter(2017, QuarterNumber.Q1) },
                { "c-2017-Q2", new CalendarQuarter(2017, QuarterNumber.Q2) },
                { "c-2017-Q3", new CalendarQuarter(2017, QuarterNumber.Q3) },
                { "c-2017-Q4", new CalendarQuarter(2017, QuarterNumber.Q4) }
            };

            // Act
            var results = calendarDaysBySerializedString.Select(_ => new { Expected = _.Key, Actual = _.Value.SerializeToSortableString() }).ToList();

            // Assert
            results.All(_ => _.Actual == _.Expected).Should().BeTrue();
        }

        [Fact]
        public static void SerializeToSortableString___Should_return_expected_serialized_sortable_string_representation_of_unitOfTime___When_unitOfTime_is_a_FiscalQuarter()
        {
            // Arrange
            var calendarDaysBySerializedString = new Dictionary<string, UnitOfTime>
            {
                { "f-2017-Q1", new FiscalQuarter(2017, QuarterNumber.Q1) },
                { "f-2017-Q2", new FiscalQuarter(2017, QuarterNumber.Q2) },
                { "f-2017-Q3", new FiscalQuarter(2017, QuarterNumber.Q3) },
                { "f-2017-Q4", new FiscalQuarter(2017, QuarterNumber.Q4) }
            };

            // Act
            var results = calendarDaysBySerializedString.Select(_ => new { Expected = _.Key, Actual = _.Value.SerializeToSortableString() }).ToList();

            // Assert
            results.All(_ => _.Actual == _.Expected).Should().BeTrue();
        }

        [Fact]
        public static void SerializeToSortableString___Should_return_expected_serialized_sortable_string_representation_of_unitOfTime___When_unitOfTime_is_a_GenericQuarter()
        {
            // Arrange
            var calendarDaysBySerializedString = new Dictionary<string, UnitOfTime>
            {
                { "g-2017-Q1", new GenericQuarter(2017, QuarterNumber.Q1) },
                { "g-2017-Q2", new GenericQuarter(2017, QuarterNumber.Q2) },
                { "g-2017-Q3", new GenericQuarter(2017, QuarterNumber.Q3) },
                { "g-2017-Q4", new GenericQuarter(2017, QuarterNumber.Q4) }
            };

            // Act
            var results = calendarDaysBySerializedString.Select(_ => new { Expected = _.Key, Actual = _.Value.SerializeToSortableString() }).ToList();

            // Assert
            results.All(_ => _.Actual == _.Expected).Should().BeTrue();
        }

        [Fact]
        public static void SerializeToSortableString___Should_return_expected_serialized_sortable_string_representation_of_unitOfTime___When_unitOfTime_is_a_CalendarYear()
        {
            // Arrange
            var calendarDaysBySerializedString = new Dictionary<string, UnitOfTime>
            {
                { "c-2017", new CalendarYear(2017) },
                { "c-2009", new CalendarYear(2009) }
            };

            // Act
            var results = calendarDaysBySerializedString.Select(_ => new { Expected = _.Key, Actual = _.Value.SerializeToSortableString() }).ToList();

            // Assert
            results.All(_ => _.Actual == _.Expected).Should().BeTrue();
        }

        [Fact]
        public static void SerializeToSortableString___Should_return_expected_serialized_sortable_string_representation_of_unitOfTime___When_unitOfTime_is_a_FiscalYear()
        {
            // Arrange
            var calendarDaysBySerializedString = new Dictionary<string, UnitOfTime>
            {
                { "f-2017", new FiscalYear(2017) },
                { "f-2009", new FiscalYear(2009) }
            };

            // Act
            var results = calendarDaysBySerializedString.Select(_ => new { Expected = _.Key, Actual = _.Value.SerializeToSortableString() }).ToList();

            // Assert
            results.All(_ => _.Actual == _.Expected).Should().BeTrue();
        }

        [Fact]
        public static void SerializeToSortableString___Should_return_expected_serialized_sortable_string_representation_of_unitOfTime___When_unitOfTime_is_a_GenericYear()
        {
            // Arrange
            var calendarDaysBySerializedString = new Dictionary<string, UnitOfTime>
            {
                { "g-2017", new GenericYear(2017) },
                { "g-2009", new GenericYear(2009) }
            };

            // Act
            var results = calendarDaysBySerializedString.Select(_ => new { Expected = _.Key, Actual = _.Value.SerializeToSortableString() }).ToList();

            // Assert
            results.All(_ => _.Actual == _.Expected).Should().BeTrue();
        }

        [Fact]
        public static void SerializeToSortableString___Should_return_expected_serialized_sortable_string_representation_of_unitOfTime___When_unitOfTime_is_a_CalendarUnbounded()
        {
            // Arrange
            var calendarUnboundedBySerializedString = new Dictionary<string, UnitOfTime>
            {
                { "c-unbounded", new CalendarUnbounded() },
            };

            // Act
            var results = calendarUnboundedBySerializedString.Select(_ => new { Expected = _.Key, Actual = _.Value.SerializeToSortableString() }).ToList();

            // Assert
            results.All(_ => _.Actual == _.Expected).Should().BeTrue();
        }

        [Fact]
        public static void SerializeToSortableString___Should_return_expected_serialized_sortable_string_representation_of_unitOfTime___When_unitOfTime_is_a_FiscalUnbounded()
        {
            // Arrange
            var calendarUnboundedBySerializedString = new Dictionary<string, UnitOfTime>
            {
                { "f-unbounded", new FiscalUnbounded() },
            };

            // Act
            var results = calendarUnboundedBySerializedString.Select(_ => new { Expected = _.Key, Actual = _.Value.SerializeToSortableString() }).ToList();

            // Assert
            results.All(_ => _.Actual == _.Expected).Should().BeTrue();
        }

        [Fact]
        public static void SerializeToSortableString___Should_return_expected_serialized_sortable_string_representation_of_unitOfTime___When_unitOfTime_is_a_GenericUnbounded()
        {
            // Arrange
            var calendarUnboundedBySerializedString = new Dictionary<string, UnitOfTime>
            {
                { "g-unbounded", new GenericUnbounded() },
            };

            // Act
            var results = calendarUnboundedBySerializedString.Select(_ => new { Expected = _.Key, Actual = _.Value.SerializeToSortableString() }).ToList();

            // Assert
            results.All(_ => _.Actual == _.Expected).Should().BeTrue();
        }

        // ReSharper restore InconsistentNaming
    }
}

// ReSharper restore CheckNamespace