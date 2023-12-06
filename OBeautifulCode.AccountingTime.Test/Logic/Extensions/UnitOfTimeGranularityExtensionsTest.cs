// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfTimeGranularityExtensionsTest.cs" company="OBeautifulCode">
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

    using Xunit;

    public static class UnitOfTimeGranularityExtensionsTest
    {
        [Fact]
        public static void IsLessGranularThan___Should_throw_ArgumentOutOfRangeException___When_parameter_granularity1_is_Invalid()
        {
            // Arrange
            var granularity1 = UnitOfTimeGranularity.Invalid;
            var granularity2 = A.Dummy<UnitOfTimeGranularity>();

            // Act
            var ex = Record.Exception(() => granularity1.IsLessGranularThan(granularity2));

            // Assert
            ex.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void IsLessGranularThan___Should_throw_ArgumentOutOfRangeException___When_parameter_granularity2_is_Invalid()
        {
            // Arrange
            var granularity1 = A.Dummy<UnitOfTimeGranularity>();
            var granularity2 = UnitOfTimeGranularity.Invalid;

            // Act
            var ex = Record.Exception(() => granularity1.IsLessGranularThan(granularity2));

            // Assert
            ex.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void IsLessGranularThan___Should_return_false___When_granularity1_is_as_or_more_granular_than_granularity2()
        {
            // Arrange
            var granularityTests = new[]
            {
                new { Granularity1 = UnitOfTimeGranularity.Day, Granularity2 = new[] { UnitOfTimeGranularity.Day, UnitOfTimeGranularity.Month, UnitOfTimeGranularity.Quarter, UnitOfTimeGranularity.Year, UnitOfTimeGranularity.Unbounded } },
                new { Granularity1 = UnitOfTimeGranularity.Month, Granularity2 = new[] { UnitOfTimeGranularity.Month, UnitOfTimeGranularity.Quarter, UnitOfTimeGranularity.Year, UnitOfTimeGranularity.Unbounded } },
                new { Granularity1 = UnitOfTimeGranularity.Quarter, Granularity2 = new[] { UnitOfTimeGranularity.Quarter, UnitOfTimeGranularity.Year, UnitOfTimeGranularity.Unbounded } },
                new { Granularity1 = UnitOfTimeGranularity.Year, Granularity2 = new[] { UnitOfTimeGranularity.Year, UnitOfTimeGranularity.Unbounded } },
                new { Granularity1 = UnitOfTimeGranularity.Unbounded, Granularity2 = new[] { UnitOfTimeGranularity.Unbounded } },
            };

            // Act
            var results = new List<bool>();
            foreach (var granularityTest in granularityTests)
            {
                foreach (var granularity2 in granularityTest.Granularity2)
                {
                    results.Add(granularityTest.Granularity1.IsLessGranularThan(granularity2));
                }
            }

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void IsLessGranularThan___Should_return_true___When_granularity1_is_less_granular_than_granularity2()
        {
            // Arrange
            var granularityTests = new[]
            {
                new { Granularity1 = UnitOfTimeGranularity.Day, Granularity2 = new UnitOfTimeGranularity[] { } },
                new { Granularity1 = UnitOfTimeGranularity.Month, Granularity2 = new[] { UnitOfTimeGranularity.Day } },
                new { Granularity1 = UnitOfTimeGranularity.Quarter, Granularity2 = new[] { UnitOfTimeGranularity.Day, UnitOfTimeGranularity.Month } },
                new { Granularity1 = UnitOfTimeGranularity.Year, Granularity2 = new[] { UnitOfTimeGranularity.Day, UnitOfTimeGranularity.Month, UnitOfTimeGranularity.Quarter } },
                new { Granularity1 = UnitOfTimeGranularity.Unbounded, Granularity2 = new[] { UnitOfTimeGranularity.Day, UnitOfTimeGranularity.Month, UnitOfTimeGranularity.Quarter, UnitOfTimeGranularity.Year } },
            };

            // Act
            var results = new List<bool>();
            foreach (var granularityTest in granularityTests)
            {
                foreach (var granularity2 in granularityTest.Granularity2)
                {
                    results.Add(granularityTest.Granularity1.IsLessGranularThan(granularity2));
                }
            }

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void IsAsGranularOrLessGranularThan___Should_throw_ArgumentOutOfRangeException___When_parameter_granularity1_is_Invalid()
        {
            // Arrange
            var granularity1 = UnitOfTimeGranularity.Invalid;
            var granularity2 = A.Dummy<UnitOfTimeGranularity>();

            // Act
            var ex = Record.Exception(() => granularity1.IsAsGranularOrLessGranularThan(granularity2));

            // Assert
            ex.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void IsAsGranularOrLessGranularThan___Should_throw_ArgumentOutOfRangeException___When_parameter_granularity2_is_Invalid()
        {
            // Arrange
            var granularity1 = A.Dummy<UnitOfTimeGranularity>();
            var granularity2 = UnitOfTimeGranularity.Invalid;

            // Act
            var ex = Record.Exception(() => granularity1.IsAsGranularOrLessGranularThan(granularity2));

            // Assert
            ex.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void IsAsGranularOrLessGranularThan___Should_return_false___When_granularity1_is_more_granular_than_granularity2()
        {
            // Arrange
            var granularityTests = new[]
            {
                new { Granularity1 = UnitOfTimeGranularity.Day, Granularity2 = new[] { UnitOfTimeGranularity.Month, UnitOfTimeGranularity.Quarter, UnitOfTimeGranularity.Year, UnitOfTimeGranularity.Unbounded } },
                new { Granularity1 = UnitOfTimeGranularity.Month, Granularity2 = new[] { UnitOfTimeGranularity.Quarter, UnitOfTimeGranularity.Year, UnitOfTimeGranularity.Unbounded } },
                new { Granularity1 = UnitOfTimeGranularity.Quarter, Granularity2 = new[] { UnitOfTimeGranularity.Year, UnitOfTimeGranularity.Unbounded } },
                new { Granularity1 = UnitOfTimeGranularity.Year, Granularity2 = new[] { UnitOfTimeGranularity.Unbounded } },
                new { Granularity1 = UnitOfTimeGranularity.Unbounded, Granularity2 = new UnitOfTimeGranularity[] { } },
            };

            // Act
            var results = new List<bool>();
            foreach (var granularityTest in granularityTests)
            {
                foreach (var granularity2 in granularityTest.Granularity2)
                {
                    results.Add(granularityTest.Granularity1.IsAsGranularOrLessGranularThan(granularity2));
                }
            }

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void IsAsGranularOrLessGranularThan___Should_return_true___When_granularity1_is_as_or_less_granular_than_granularity2()
        {
            // Arrange
            var granularityTests = new[]
            {
                new { Granularity1 = UnitOfTimeGranularity.Day, Granularity2 = new[] { UnitOfTimeGranularity.Day } },
                new { Granularity1 = UnitOfTimeGranularity.Month, Granularity2 = new[] { UnitOfTimeGranularity.Day, UnitOfTimeGranularity.Month } },
                new { Granularity1 = UnitOfTimeGranularity.Quarter, Granularity2 = new[] { UnitOfTimeGranularity.Day, UnitOfTimeGranularity.Month, UnitOfTimeGranularity.Quarter } },
                new { Granularity1 = UnitOfTimeGranularity.Year, Granularity2 = new[] { UnitOfTimeGranularity.Day, UnitOfTimeGranularity.Month, UnitOfTimeGranularity.Quarter, UnitOfTimeGranularity.Year } },
                new { Granularity1 = UnitOfTimeGranularity.Unbounded, Granularity2 = new[] { UnitOfTimeGranularity.Day, UnitOfTimeGranularity.Month, UnitOfTimeGranularity.Quarter, UnitOfTimeGranularity.Year, UnitOfTimeGranularity.Unbounded } },
            };

            // Act
            var results = new List<bool>();
            foreach (var granularityTest in granularityTests)
            {
                foreach (var granularity2 in granularityTest.Granularity2)
                {
                    results.Add(granularityTest.Granularity1.IsAsGranularOrLessGranularThan(granularity2));
                }
            }

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void IsMoreGranularThan___Should_throw_ArgumentOutOfRangeException___When_parameter_granularity1_is_Invalid()
        {
            // Arrange
            var granularity1 = UnitOfTimeGranularity.Invalid;
            var granularity2 = A.Dummy<UnitOfTimeGranularity>();

            // Act
            var ex = Record.Exception(() => granularity1.IsMoreGranularThan(granularity2));

            // Assert
            ex.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void IsMoreGranularThan___Should_throw_ArgumentOutOfRangeException___When_parameter_granularity2_is_Invalid()
        {
            // Arrange
            var granularity1 = A.Dummy<UnitOfTimeGranularity>();
            var granularity2 = UnitOfTimeGranularity.Invalid;

            // Act
            var ex = Record.Exception(() => granularity1.IsMoreGranularThan(granularity2));

            // Assert
            ex.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void IsMoreGranularThan___Should_return_false___When_granularity1_is_as_or_less_granular_than_granularity2()
        {
            // Arrange
            var granularityTests = new[]
            {
                new { Granularity1 = UnitOfTimeGranularity.Day, Granularity2 = new[] { UnitOfTimeGranularity.Day } },
                new { Granularity1 = UnitOfTimeGranularity.Month, Granularity2 = new[] { UnitOfTimeGranularity.Day, UnitOfTimeGranularity.Month } },
                new { Granularity1 = UnitOfTimeGranularity.Quarter, Granularity2 = new[] { UnitOfTimeGranularity.Day, UnitOfTimeGranularity.Month, UnitOfTimeGranularity.Quarter } },
                new { Granularity1 = UnitOfTimeGranularity.Year, Granularity2 = new[] { UnitOfTimeGranularity.Day, UnitOfTimeGranularity.Month, UnitOfTimeGranularity.Quarter, UnitOfTimeGranularity.Year } },
                new { Granularity1 = UnitOfTimeGranularity.Unbounded, Granularity2 = new[] { UnitOfTimeGranularity.Day, UnitOfTimeGranularity.Month, UnitOfTimeGranularity.Quarter, UnitOfTimeGranularity.Year, UnitOfTimeGranularity.Unbounded } },
            };

            // Act
            var results = new List<bool>();
            foreach (var granularityTest in granularityTests)
            {
                foreach (var granularity2 in granularityTest.Granularity2)
                {
                    results.Add(granularityTest.Granularity1.IsMoreGranularThan(granularity2));
                }
            }

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void IsMoreGranularThan___Should_return_true___When_granularity1_is_more_granular_than_granularity2()
        {
            // Arrange
            var granularityTests = new[]
            {
                new { Granularity1 = UnitOfTimeGranularity.Day, Granularity2 = new[] { UnitOfTimeGranularity.Month, UnitOfTimeGranularity.Quarter, UnitOfTimeGranularity.Year, UnitOfTimeGranularity.Unbounded } },
                new { Granularity1 = UnitOfTimeGranularity.Month, Granularity2 = new[] { UnitOfTimeGranularity.Quarter, UnitOfTimeGranularity.Year, UnitOfTimeGranularity.Unbounded } },
                new { Granularity1 = UnitOfTimeGranularity.Quarter, Granularity2 = new[] { UnitOfTimeGranularity.Year, UnitOfTimeGranularity.Unbounded } },
                new { Granularity1 = UnitOfTimeGranularity.Year, Granularity2 = new[] { UnitOfTimeGranularity.Unbounded } },
                new { Granularity1 = UnitOfTimeGranularity.Unbounded, Granularity2 = new UnitOfTimeGranularity[] { } },
            };

            // Act
            var results = new List<bool>();
            foreach (var granularityTest in granularityTests)
            {
                foreach (var granularity2 in granularityTest.Granularity2)
                {
                    results.Add(granularityTest.Granularity1.IsMoreGranularThan(granularity2));
                }
            }

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void IsAsGranularOrMoreGranularThan___Should_throw_ArgumentOutOfRangeException___When_parameter_granularity1_is_Invalid()
        {
            // Arrange
            var granularity1 = UnitOfTimeGranularity.Invalid;
            var granularity2 = A.Dummy<UnitOfTimeGranularity>();

            // Act
            var ex = Record.Exception(() => granularity1.IsAsGranularOrMoreGranularThan(granularity2));

            // Assert
            ex.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void IsAsGranularOrMoreGranularThan___Should_throw_ArgumentOutOfRangeException___When_parameter_granularity2_is_Invalid()
        {
            // Arrange
            var granularity1 = A.Dummy<UnitOfTimeGranularity>();
            var granularity2 = UnitOfTimeGranularity.Invalid;

            // Act
            var ex = Record.Exception(() => granularity1.IsAsGranularOrMoreGranularThan(granularity2));

            // Assert
            ex.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void IsAsGranularOrMoreGranularThan___Should_return_false___When_granularity1_is_less_granular_than_granularity2()
        {
            // Arrange
            var granularityTests = new[]
            {
                new { Granularity1 = UnitOfTimeGranularity.Day, Granularity2 = new UnitOfTimeGranularity[] { } },
                new { Granularity1 = UnitOfTimeGranularity.Month, Granularity2 = new[] { UnitOfTimeGranularity.Day } },
                new { Granularity1 = UnitOfTimeGranularity.Quarter, Granularity2 = new[] { UnitOfTimeGranularity.Day, UnitOfTimeGranularity.Month } },
                new { Granularity1 = UnitOfTimeGranularity.Year, Granularity2 = new[] { UnitOfTimeGranularity.Day, UnitOfTimeGranularity.Month, UnitOfTimeGranularity.Quarter } },
                new { Granularity1 = UnitOfTimeGranularity.Unbounded, Granularity2 = new[] { UnitOfTimeGranularity.Day, UnitOfTimeGranularity.Month, UnitOfTimeGranularity.Quarter, UnitOfTimeGranularity.Year } },
            };

            // Act
            var results = new List<bool>();
            foreach (var granularityTest in granularityTests)
            {
                foreach (var granularity2 in granularityTest.Granularity2)
                {
                    results.Add(granularityTest.Granularity1.IsAsGranularOrMoreGranularThan(granularity2));
                }
            }

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void IsAsGranularOrMoreGranularThan___Should_return_true___When_granularity1_is_as_or_more_granular_than_granularity2()
        {
            // Arrange
            var granularityTests = new[]
            {
                new { Granularity1 = UnitOfTimeGranularity.Day, Granularity2 = new[] { UnitOfTimeGranularity.Day, UnitOfTimeGranularity.Month, UnitOfTimeGranularity.Quarter, UnitOfTimeGranularity.Year, UnitOfTimeGranularity.Unbounded } },
                new { Granularity1 = UnitOfTimeGranularity.Month, Granularity2 = new[] { UnitOfTimeGranularity.Month, UnitOfTimeGranularity.Quarter, UnitOfTimeGranularity.Year, UnitOfTimeGranularity.Unbounded } },
                new { Granularity1 = UnitOfTimeGranularity.Quarter, Granularity2 = new[] { UnitOfTimeGranularity.Quarter, UnitOfTimeGranularity.Year, UnitOfTimeGranularity.Unbounded } },
                new { Granularity1 = UnitOfTimeGranularity.Year, Granularity2 = new[] { UnitOfTimeGranularity.Year, UnitOfTimeGranularity.Unbounded } },
                new { Granularity1 = UnitOfTimeGranularity.Unbounded, Granularity2 = new[] { UnitOfTimeGranularity.Unbounded } },
            };

            // Act
            var results = new List<bool>();
            foreach (var granularityTest in granularityTests)
            {
                foreach (var granularity2 in granularityTest.Granularity2)
                {
                    results.Add(granularityTest.Granularity1.IsAsGranularOrMoreGranularThan(granularity2));
                }
            }

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void IsMostGranular_UnitOfTimeGranularity___Should_throw_ArgumentOutOfRangeException__When_parameter_granularity_is_Invalid()
        {
            // Arrange
            var granularity = UnitOfTimeGranularity.Invalid;

            // Act
            var ex = Record.Exception(() => granularity.IsMostGranular());

            // Assert
            ex.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void IsMostGranular_UnitOfTimeGranularity___Should_return_false___When_parameter_granularity_is_not_Day()
        {
            // Arrange
            var granularity = new[] { UnitOfTimeGranularity.Month, UnitOfTimeGranularity.Quarter, UnitOfTimeGranularity.Unbounded, UnitOfTimeGranularity.Year };

            // Act
            var results = granularity.Select(_ => _.IsMostGranular()).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void IsMostGranular_UnitOfTimeGranularity___Should_return_true___When_parameter_granularity_is_Day()
        {
            // Arrange
            var granularity = UnitOfTimeGranularity.Day;

            // Act
            var result = granularity.IsMostGranular();

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void IsMostGranular_Unit___Should_throw_ArgumentNullException__When_parameter_unit_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => UnitOfTimeGranularityExtensions.IsMostGranular(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void IsMostGranular_Unit___Should_return_false___When_unit_is_not_the_most_granular()
        {
            // Arrange
            var units = new[]
            {
                new Unit(UnitOfTimeKind.Calendar, UnitOfTimeGranularity.Month),
                new Unit(UnitOfTimeKind.Calendar, UnitOfTimeGranularity.Quarter),
                new Unit(UnitOfTimeKind.Calendar, UnitOfTimeGranularity.Year),
                new Unit(UnitOfTimeKind.Calendar, UnitOfTimeGranularity.Unbounded),
                new Unit(UnitOfTimeKind.Fiscal, UnitOfTimeGranularity.Quarter),
                new Unit(UnitOfTimeKind.Fiscal, UnitOfTimeGranularity.Year),
                new Unit(UnitOfTimeKind.Fiscal, UnitOfTimeGranularity.Unbounded),
                new Unit(UnitOfTimeKind.Generic, UnitOfTimeGranularity.Quarter),
                new Unit(UnitOfTimeKind.Generic, UnitOfTimeGranularity.Year),
                new Unit(UnitOfTimeKind.Generic, UnitOfTimeGranularity.Unbounded),
            };

            // Act
            var results = units.Select(_ => _.IsMostGranular()).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void IsMostGranular_Unit___Should_return_true___When_unit_is_the_most_granular()
        {
            // Arrange
            var units = new[]
            {
                new Unit(UnitOfTimeKind.Calendar, UnitOfTimeGranularity.Day),
                new Unit(UnitOfTimeKind.Fiscal, UnitOfTimeGranularity.Month),
                new Unit(UnitOfTimeKind.Generic, UnitOfTimeGranularity.Month),
            };

            // Act
            var results = units.Select(_ => _.IsMostGranular()).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void IsLeastGranular___Should_throw_ArgumentOutOfRangeException___When_parameter_granularity_is_Invalid()
        {
            // Arrange
            var granularity = UnitOfTimeGranularity.Invalid;

            // Act
            var ex = Record.Exception(() => granularity.IsLeastGranular());

            // Assert
            ex.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void IsLeastGranular___Should_return_false___When_parameter_granularity_is_not_Unbounded()
        {
            // Arrange
            var granularity = new[] { UnitOfTimeGranularity.Month, UnitOfTimeGranularity.Quarter, UnitOfTimeGranularity.Day, UnitOfTimeGranularity.Year };

            // Act
            var results = granularity.Select(_ => _.IsLeastGranular()).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void IsLeastGranular___Should_return_true___When_parameter_granularity_is_Unbounded()
        {
            // Arrange
            var granularity = UnitOfTimeGranularity.Unbounded;

            // Act
            var result = granularity.IsLeastGranular();

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void OneNotchMoreGranular___Should_throw_ArgumentOutOfRangeException___When_parameter_granularity_is_Invalid()
        {
            // Arrange
            var granularity = UnitOfTimeGranularity.Invalid;

            // Act
            var ex = Record.Exception(() => granularity.OneNotchMoreGranular());

            // Assert
            ex.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void OneNotchMoreGranular___Should_throw_ArgumentException___When_parameter_granularity_is_Day()
        {
            // Arrange
            var granularity = UnitOfTimeGranularity.Day;

            // Act
            var ex = Record.Exception(() => granularity.OneNotchMoreGranular());

            // Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void OneNotchMoreGranular___Should_return_a_granularity_that_is_one_notch_more_granular___When_parameter_granularity_is_not_Day()
        {
            // Arrange
            var granularity = new[]
            {
                new { Granularity = UnitOfTimeGranularity.Month, OneNotchMoreGranular = UnitOfTimeGranularity.Day },
                new { Granularity = UnitOfTimeGranularity.Quarter, OneNotchMoreGranular = UnitOfTimeGranularity.Month },
                new { Granularity = UnitOfTimeGranularity.Year, OneNotchMoreGranular = UnitOfTimeGranularity.Quarter },
                new { Granularity = UnitOfTimeGranularity.Unbounded, OneNotchMoreGranular = UnitOfTimeGranularity.Year },
            };

            // Act
            var results = granularity.Select(_ => new { Expected = _.OneNotchMoreGranular, Actual = _.Granularity.OneNotchMoreGranular() }).ToList();

            // Assert
            results.ForEach(_ => _.Expected.Should().Be(_.Actual));
        }

        [Fact]
        public static void OneNotchLessGranular___Should_throw_ArgumentOutOfRangeException___When_parameter_granularity_is_Invalid()
        {
            // Arrange
            var granularity = UnitOfTimeGranularity.Invalid;

            // Act
            var ex = Record.Exception(() => granularity.OneNotchLessGranular());

            // Assert
            ex.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void OneNotchLessGranular___Should_throw_ArgumentException___When_parameter_granularity_is_Unbounded()
        {
            // Arrange
            var granularity = UnitOfTimeGranularity.Unbounded;

            // Act
            var ex = Record.Exception(() => granularity.OneNotchLessGranular());

            // Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void OneNotchLessGranular___Should_return_a_granularity_that_is_one_notch_more_granular___When_parameter_granularity_is_not_Unbounded()
        {
            // Arrange
            var granularity = new[]
            {
                new { Granularity = UnitOfTimeGranularity.Day, OneNotchLessGranular = UnitOfTimeGranularity.Month },
                new { Granularity = UnitOfTimeGranularity.Month, OneNotchLessGranular = UnitOfTimeGranularity.Quarter },
                new { Granularity = UnitOfTimeGranularity.Quarter, OneNotchLessGranular = UnitOfTimeGranularity.Year },
                new { Granularity = UnitOfTimeGranularity.Year, OneNotchLessGranular = UnitOfTimeGranularity.Unbounded },
            };

            // Act
            var results = granularity.Select(_ => new { Expected = _.OneNotchLessGranular, Actual = _.Granularity.OneNotchLessGranular() }).ToList();

            // Assert
            results.ForEach(_ => _.Expected.Should().Be(_.Actual));
        }
    }
}
