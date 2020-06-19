// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalendarUnboundedTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Test
{
    using System;
    using System.Linq;

    using FakeItEasy;

    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.CodeGen.ModelObject.Recipes;

    using Xunit;

    public static partial class CalendarUnboundedTest
    {
        static CalendarUnboundedTest()
        {
            var referenceCalendarUnbounded = A.Dummy<CalendarUnbounded>();

            ComparableTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ComparableTestScenario<CalendarUnbounded>
                    {
                        Name = "Comparable Test Scenario",
                        ReferenceObject = referenceCalendarUnbounded,
                        ObjectsThatAreLessThanReferenceObject = new CalendarUnbounded[]
                        {
                        },
                        ObjectsThatAreGreaterThanReferenceObject = new CalendarUnbounded[]
                        {
                        },
                        ObjectsThatAreEqualToButNotTheSameAsReferenceObject = new[]
                        {
                            referenceCalendarUnbounded.DeepClone(),
                        },
                        ObjectsThatAreNotOfTheSameTypeAsReferenceObject = new[]
                            {
                                A.Dummy<object>(),
                                A.Dummy<string>(),
                                A.Dummy<int>(),
                                A.Dummy<int?>(),
                                A.Dummy<Guid>(),
                            }
                            .Concat(Common.GetDummyOfEachUnitOfTimeKind().Where(_ => _.GetType() != typeof(CalendarUnbounded)))
                            .ToList(),
                        ObjectsThatDeriveFromScenarioTypeButAreNotOfTheSameTypeAsReferenceObject = new CalendarUnbounded[]
                        {
                        },
                    });

            StringRepresentationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new StringRepresentationTestScenario<CalendarUnbounded>
                    {
                        Name = "String Representation - case 1",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                            new SystemUnderTestExpectedStringRepresentation<CalendarUnbounded>
                            {
                                SystemUnderTest = new CalendarUnbounded(),
                                ExpectedStringRepresentation = "calendar unbounded",
                            },
                    });
        }

        [Fact]
        public static void UnitOfTimeKind__Should_return_Calendar___When_called()
        {
            // Arrange
            var unitOfTime = A.Dummy<CalendarUnbounded>();

            // Act
            var kind = unitOfTime.UnitOfTimeKind;

            // Assert
            kind.AsTest().Must().BeEqualTo(UnitOfTimeKind.Calendar);
        }

        [Fact]
        public static void UnitOfTimeGranularity__Should_return_Unbounded___When_called()
        {
            // Arrange
            var unitOfTime = A.Dummy<CalendarUnbounded>();

            // Act
            var granularity = unitOfTime.UnitOfTimeGranularity;

            // Assert
            granularity.AsTest().Must().BeEqualTo(UnitOfTimeGranularity.Unbounded);
        }
    }
}