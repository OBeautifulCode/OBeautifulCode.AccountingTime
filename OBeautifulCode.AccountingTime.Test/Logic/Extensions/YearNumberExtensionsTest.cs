// --------------------------------------------------------------------------------------------------------------------
// <copyright file="YearNumberExtensionsTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Test
{
    using FakeItEasy;
    using FluentAssertions;
    using Xunit;

    public static class YearNumberExtensionsTest
    {
        [Fact]
        public static void ToCalendar___Should_return_CalendarYear_with_specified_year___When_called()
        {
            // Arrange
            var expected = A.Dummy<CalendarYear>();

            // Act
            var actual = expected.Year.ToCalendar();

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public static void ToFiscal___Should_return_FiscalYear_with_specified_year___When_called()
        {
            // Arrange
            var expected = A.Dummy<FiscalYear>();

            // Act
            var actual = expected.Year.ToFiscal();

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public static void ToGeneric___Should_return_GenericYear_with_specified_year___When_called()
        {
            // Arrange
            var expected = A.Dummy<GenericYear>();

            // Act
            var actual = expected.Year.ToGeneric();

            // Assert
            actual.Should().Be(expected);
        }
    }
}
