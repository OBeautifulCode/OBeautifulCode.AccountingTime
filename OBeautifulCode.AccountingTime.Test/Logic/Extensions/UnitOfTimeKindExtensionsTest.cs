// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfTimeKindExtensionsTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Test
{
    using System;
    using System.Linq;
    using FluentAssertions;
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Enum.Recipes;
    using Xunit;

    public static class UnitOfTimeKindExtensionsTest
    {
        [Fact]
        public static void ToUnbounded___Should_throw_ArgumentOutOfRangeException___When_parameter_unitOfTimeKind_is_Invalid()
        {
            // Arrange, Act
            var ex = Record.Exception(() => UnitOfTimeKind.Invalid.ToUnbounded());

            // Assert
            ex.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void ToUnbounded___Should_return_specified_kind_of_unbounded_unit_of_time___When_called()
        {
            // Arrange
            var unitOfTimeKinds = EnumExtensions.GetDefinedEnumValues<UnitOfTimeKind>().Except(new[] { UnitOfTimeKind.Invalid }).ToList();

            // Act
            var actual = unitOfTimeKinds.Select(_ => new { UnitOfTimeKind = _, UnitOfTime = _.ToUnbounded() }).ToList();

            // Assert
            actual.Select(_ => _.UnitOfTime.UnitOfTimeGranularity).AsTest().Must().Each().BeEqualTo(UnitOfTimeGranularity.Unbounded);
            actual.Select(_ => _.UnitOfTimeKind).AsTest().Must().BeEqualTo(actual.Select(_ => _.UnitOfTime.UnitOfTimeKind));
        }
    }
}
