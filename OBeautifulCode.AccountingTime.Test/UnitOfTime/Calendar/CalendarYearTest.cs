﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalendarYearTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Test
{
    using System;
    using System.Linq;

    using FakeItEasy;

    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.AutoFakeItEasy;
    using OBeautifulCode.CodeGen.ModelObject.Recipes;

    using Xunit;

    public static partial class CalendarYearTest
    {
        static CalendarYearTest()
        {
            var referenceCalendarYear = A.Dummy<CalendarYear>();

            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CalendarYear>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'year' is < 0",
                        ConstructionFunc = () => new CalendarYear(A.Dummy<NegativeInteger>()),
                        ExpectedExceptionMessageContains = new[] { "year", "not greater than or equal to" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CalendarYear>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'year' = 0",
                        ConstructionFunc = () => new CalendarYear(0),
                        ExpectedExceptionMessageContains = new[] { "year", "not greater than or equal to" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CalendarYear>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'year' = 10000",
                        ConstructionFunc = () => new CalendarYear(10000),
                        ExpectedExceptionMessageContains = new[] { "year", "not less than or equal to" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CalendarYear>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'year' > 9999",
                        ConstructionFunc = () => new CalendarYear(A.Dummy<PositiveInteger>().ThatIs(_ => _ > 9999)),
                        ExpectedExceptionMessageContains = new[] { "year", "not less than or equal to" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    });

            ComparableTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ComparableTestScenario<CalendarYear>
                    {
                        Name = "Comparable Test Scenario",
                        ReferenceObject = referenceCalendarYear,
                        ObjectsThatAreLessThanReferenceObject = new[]
                        {
                            referenceCalendarYear.TweakBy(-1),
                        },
                        ObjectsThatAreGreaterThanReferenceObject = new[]
                        {
                            referenceCalendarYear.TweakBy(1),
                        },
                        ObjectsThatAreEqualToButNotTheSameAsReferenceObject = new[]
                        {
                            referenceCalendarYear.DeepClone(),
                        },
                        ObjectsThatAreNotOfTheSameTypeAsReferenceObject = new[]
                            {
                                A.Dummy<object>(),
                                A.Dummy<string>(),
                                A.Dummy<int>(),
                                A.Dummy<int?>(),
                                A.Dummy<Guid>(),
                            }
                            .Concat(Common.GetDummyOfEachUnitOfTimeKind().Where(_ => _.GetType() != typeof(CalendarYear)))
                            .ToList(),
                        ObjectsThatDeriveFromScenarioTypeButAreNotOfTheSameTypeAsReferenceObject = new CalendarYear[]
                        {
                        },
                    });

            StringRepresentationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new StringRepresentationTestScenario<CalendarYear>
                    {
                        Name = "String Representation - case 1",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                            new SystemUnderTestExpectedStringRepresentation<CalendarYear>
                            {
                                SystemUnderTest = new CalendarYear(2017),
                                ExpectedStringRepresentation = "CY2017",
                            },
                    })
                .AddScenario(() =>
                    new StringRepresentationTestScenario<CalendarYear>
                    {
                        Name = "String Representation - case 2",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                            new SystemUnderTestExpectedStringRepresentation<CalendarYear>
                            {
                                SystemUnderTest = new CalendarYear(2020),
                                ExpectedStringRepresentation = "CY2020",
                            },
                    });
        }

        [Fact]
        public static void UnitOfTimeKind__Should_return_Calendar___When_called()
        {
            // Arrange
            var unitOfTime = A.Dummy<CalendarYear>();

            // Act
            var kind = unitOfTime.UnitOfTimeKind;

            // Assert
            kind.AsTest().Must().BeEqualTo(UnitOfTimeKind.Calendar);
        }

        [Fact]
        public static void UnitOfTimeGranularity__Should_return_Year___When_called()
        {
            // Arrange
            var unitOfTime = A.Dummy<CalendarYear>();

            // Act
            var granularity = unitOfTime.UnitOfTimeGranularity;

            // Assert
            granularity.AsTest().Must().BeEqualTo(UnitOfTimeGranularity.Year);
        }

        private static CalendarYear TweakBy(
            this CalendarYear calendarYear,
            int amount)
        {
            var result = new CalendarYear(calendarYear.Year + amount);

            return result;
        }
    }
}