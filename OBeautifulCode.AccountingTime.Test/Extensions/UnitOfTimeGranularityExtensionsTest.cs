// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfTimeGranularityExtensionsTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime.Test
{
    using System;
    using System.Collections.Generic;

    using FakeItEasy;

    using FluentAssertions;

    using Xunit;

    public static class UnitOfTimeGranularityExtensionsTest
    {
        // ReSharper disable InconsistentNaming
        [Fact]
        public static void IsLessGranularThan___Should_throw_ArgumentException___When_parameter_granularity1_is_Invalid()
        {
            // Arrange
            UnitOfTimeGranularity granularity1 = UnitOfTimeGranularity.Invalid;
            UnitOfTimeGranularity granularity2 = A.Dummy<UnitOfTimeGranularity>();

            // Act
            var ex = Record.Exception(() => granularity1.IsLessGranularThan(granularity2));

            // Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void IsLessGranularThan___Should_throw_ArgumentException___When_parameter_granularity2_is_Invalid()
        {
            // Arrange
            UnitOfTimeGranularity granularity1 = A.Dummy<UnitOfTimeGranularity>();
            UnitOfTimeGranularity granularity2 = UnitOfTimeGranularity.Invalid;

            // Act
            var ex = Record.Exception(() => granularity1.IsLessGranularThan(granularity2));

            // Assert
            ex.Should().BeOfType<ArgumentException>();
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
                new { Granularity1 = UnitOfTimeGranularity.Unbounded, Granularity2 = new[] { UnitOfTimeGranularity.Unbounded } }
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
                new { Granularity1 = UnitOfTimeGranularity.Unbounded, Granularity2 = new[] { UnitOfTimeGranularity.Day, UnitOfTimeGranularity.Month, UnitOfTimeGranularity.Quarter, UnitOfTimeGranularity.Year } }
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
        public static void IsAsGranularOrLessGranularThan___Should_throw_ArgumentException___When_parameter_granularity1_is_Invalid()
        {
            // Arrange
            UnitOfTimeGranularity granularity1 = UnitOfTimeGranularity.Invalid;
            UnitOfTimeGranularity granularity2 = A.Dummy<UnitOfTimeGranularity>();

            // Act
            var ex = Record.Exception(() => granularity1.IsAsGranularOrLessGranularThan(granularity2));

            // Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void IsAsGranularOrLessGranularThan___Should_throw_ArgumentException___When_parameter_granularity2_is_Invalid()
        {
            // Arrange
            UnitOfTimeGranularity granularity1 = A.Dummy<UnitOfTimeGranularity>();
            UnitOfTimeGranularity granularity2 = UnitOfTimeGranularity.Invalid;

            // Act
            var ex = Record.Exception(() => granularity1.IsAsGranularOrLessGranularThan(granularity2));

            // Assert
            ex.Should().BeOfType<ArgumentException>();
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
                new { Granularity1 = UnitOfTimeGranularity.Unbounded, Granularity2 = new UnitOfTimeGranularity[] { } }
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
                new { Granularity1 = UnitOfTimeGranularity.Unbounded, Granularity2 = new[] { UnitOfTimeGranularity.Day, UnitOfTimeGranularity.Month, UnitOfTimeGranularity.Quarter, UnitOfTimeGranularity.Year, UnitOfTimeGranularity.Unbounded } }
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
        public static void IsMoreGranularThan___Should_throw_ArgumentException___When_parameter_granularity1_is_Invalid()
        {
            // Arrange
            UnitOfTimeGranularity granularity1 = UnitOfTimeGranularity.Invalid;
            UnitOfTimeGranularity granularity2 = A.Dummy<UnitOfTimeGranularity>();

            // Act
            var ex = Record.Exception(() => granularity1.IsMoreGranularThan(granularity2));

            // Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void IsMoreGranularThan___Should_throw_ArgumentException___When_parameter_granularity2_is_Invalid()
        {
            // Arrange
            UnitOfTimeGranularity granularity1 = A.Dummy<UnitOfTimeGranularity>();
            UnitOfTimeGranularity granularity2 = UnitOfTimeGranularity.Invalid;

            // Act
            var ex = Record.Exception(() => granularity1.IsMoreGranularThan(granularity2));

            // Assert
            ex.Should().BeOfType<ArgumentException>();
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
                new { Granularity1 = UnitOfTimeGranularity.Unbounded, Granularity2 = new[] { UnitOfTimeGranularity.Day, UnitOfTimeGranularity.Month, UnitOfTimeGranularity.Quarter, UnitOfTimeGranularity.Year, UnitOfTimeGranularity.Unbounded } }
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
                new { Granularity1 = UnitOfTimeGranularity.Unbounded, Granularity2 = new UnitOfTimeGranularity[] { } }
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
        public static void IsAsGranularOrMoreGranularThan___Should_throw_ArgumentException___When_parameter_granularity1_is_Invalid()
        {
            // Arrange
            UnitOfTimeGranularity granularity1 = UnitOfTimeGranularity.Invalid;
            UnitOfTimeGranularity granularity2 = A.Dummy<UnitOfTimeGranularity>();

            // Act
            var ex = Record.Exception(() => granularity1.IsAsGranularOrMoreGranularThan(granularity2));

            // Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void IsAsGranularOrMoreGranularThan___Should_throw_ArgumentException___When_parameter_granularity2_is_Invalid()
        {
            // Arrange
            UnitOfTimeGranularity granularity1 = A.Dummy<UnitOfTimeGranularity>();
            UnitOfTimeGranularity granularity2 = UnitOfTimeGranularity.Invalid;

            // Act
            var ex = Record.Exception(() => granularity1.IsAsGranularOrMoreGranularThan(granularity2));

            // Assert
            ex.Should().BeOfType<ArgumentException>();
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
                new { Granularity1 = UnitOfTimeGranularity.Unbounded, Granularity2 = new[] { UnitOfTimeGranularity.Day, UnitOfTimeGranularity.Month, UnitOfTimeGranularity.Quarter, UnitOfTimeGranularity.Year } }
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
                new { Granularity1 = UnitOfTimeGranularity.Unbounded, Granularity2 = new[] { UnitOfTimeGranularity.Unbounded } }
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

        // ReSharper restore InconsistentNaming
    }
}

// ReSharper restore CheckNamespace