﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QuarterNumberExtensionsTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Test
{
    using System;
    using System.Linq;

    using FakeItEasy;

    using FluentAssertions;

    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.AutoFakeItEasy;

    using Xunit;

    public static class QuarterNumberExtensionsTest
    {
        [Fact]
        public static void ToCalendar___Should_throw_ArgumentOutOfRangeException___When_parameter_quarterNumber_is_Invalid()
        {
            // Arrange, Act
            var ex = Record.Exception(() => QuarterNumber.Invalid.ToCalendar(A.Dummy<PositiveInteger>().ThatIs(y => y < 10000)));

            // Assert
            ex.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void ToCalendar___Should_return_CalendarQuarter_with_specified_quarterNumber_and_year___When_called()
        {
            // Arrange
            var expectedQuarter = A.Dummy<CalendarQuarter>();

            // Act
            var actualQuarter = expectedQuarter.QuarterNumber.ToCalendar(expectedQuarter.Year);

            // Assert
            actualQuarter.Should().Be(expectedQuarter);
        }

        [Fact]
        public static void ToFiscal___Should_throw_ArgumentOutOfRangeException___When_parameter_quarterNumber_is_Invalid()
        {
            // Arrange, Act
            var ex = Record.Exception(() => QuarterNumber.Invalid.ToFiscal(A.Dummy<PositiveInteger>().ThatIs(y => y < 10000)));

            // Assert
            ex.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void ToFiscal___Should_return_FiscalQuarter_with_specified_quarterNumber_and_year___When_called()
        {
            // Arrange
            var expectedQuarter = A.Dummy<FiscalQuarter>();

            // Act
            var actualQuarter = expectedQuarter.QuarterNumber.ToFiscal(expectedQuarter.Year);

            // Assert
            actualQuarter.Should().Be(expectedQuarter);
        }

        [Fact]
        public static void ToGeneric___Should_throw_ArgumentOutOfRangeException___When_parameter_quarterNumber_is_Invalid()
        {
            // Arrange, Act
            var ex = Record.Exception(() => QuarterNumber.Invalid.ToGeneric(A.Dummy<PositiveInteger>().ThatIs(y => y < 10000)));

            // Assert
            ex.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void ToGeneric___Should_return_GenericQuarter_with_specified_quarterNumber_and_year___When_called()
        {
            // Arrange
            var expectedQuarter = A.Dummy<GenericQuarter>();

            // Act
            var actualQuarter = expectedQuarter.QuarterNumber.ToGeneric(expectedQuarter.Year);

            // Assert
            actualQuarter.Should().Be(expectedQuarter);
        }

        [Fact]
        public static void ToOrdinalIndicator___Should_throw_ArgumentOutOfRangeException___When_parameter_quarterNumber_is_Invalid()
        {
            // Arrange, Act
            var ex = Record.Exception(() => QuarterNumber.Invalid.ToOrdinalIndicator());

            // Assert
            ex.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void ToOrdinalIndicator___Should_return_ordinal_indicator___When_called()
        {
            // Arrange
            var quarterNumbers = new[] { QuarterNumber.Q1, QuarterNumber.Q2, QuarterNumber.Q3, QuarterNumber.Q4 };

            var expected = new[] { "1st", "2nd", "3rd", "4th" };

            // Act
            var actual = quarterNumbers.Select(_ => _.ToOrdinalIndicator()).ToArray();

            // Assert
            expected.AsTest().Must().BeEqualTo(actual);
        }
    }
}
