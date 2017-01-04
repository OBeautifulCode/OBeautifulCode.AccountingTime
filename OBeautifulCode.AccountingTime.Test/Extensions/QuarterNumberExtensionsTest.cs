// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QuarterNumberExtensionsTest.cs" company="OBeautifulCode">
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

    public static class QuarterNumberExtensionsTest
    {
        // ReSharper disable InconsistentNaming
        [Fact]
        public static void ToCalendar___Should_throw_ArgumentException___When_parameter_quarterNumber_is_Invalid()
        {
            // Arrange, Act
            var ex = Record.Exception(() => QuarterNumber.Invalid.ToCalendar(A.Dummy<PositiveInteger>().ThatIs(y => y < 10000)));

            // Assert
            ex.Should().BeOfType<ArgumentException>();
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
        public static void ToFiscal___Should_throw_ArgumentException___When_parameter_quarterNumber_is_Invalid()
        {
            // Arrange, Act
            var ex = Record.Exception(() => QuarterNumber.Invalid.ToFiscal(A.Dummy<PositiveInteger>().ThatIs(y => y < 10000)));

            // Assert
            ex.Should().BeOfType<ArgumentException>();
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
        public static void ToGeneric___Should_throw_ArgumentException___When_parameter_quarterNumber_is_Invalid()
        {
            // Arrange, Act
            var ex = Record.Exception(() => QuarterNumber.Invalid.ToGeneric(A.Dummy<PositiveInteger>().ThatIs(y => y < 10000)));

            // Assert
            ex.Should().BeOfType<ArgumentException>();
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

        // ReSharper restore InconsistentNaming
    }
}

// ReSharper restore CheckNamespace