// --------------------------------------------------------------------------------------------------------------------
// <copyright file="YearNumberExtensionsTest.cs" company="OBeautifulCode">
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

        [Fact]
        public static void ToUnitOfTime___Should_throw_ArgumentOutOfRangeException___When_parameter_unitOfTimeKind_is_Invalid()
        {
            // Arrange, Act
            var ex = Record.Exception(() => A.Dummy<int>().ToUnitOfTime(UnitOfTimeKind.Invalid));

            // Assert
            ex.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void ToUnitOfTime___Should_return_UnitOfTime_of_specified_kind_with_specified_year___When_called()
        {
            // Arrange
            var expected = new UnitOfTime[]
            {
                A.Dummy<CalendarYear>(),
                A.Dummy<FiscalYear>(),
                A.Dummy<GenericYear>(),
            };

            // Act
            var actual = expected.Select(_ => ((IHaveAYear)_).Year.ToUnitOfTime(_.UnitOfTimeKind)).ToArray();

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }
    }
}
