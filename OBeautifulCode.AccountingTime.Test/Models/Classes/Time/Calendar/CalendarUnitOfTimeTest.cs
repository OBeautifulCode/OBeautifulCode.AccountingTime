// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalendarUnitOfTimeTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Test
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using FakeItEasy;

    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.CodeAnalysis.Recipes;
    using OBeautifulCode.CodeGen.ModelObject.Recipes;

    using Xunit;

    public static partial class CalendarUnitOfTimeTest
    {
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static CalendarUnitOfTimeTest()
        {
            var referenceCalendarUnitOfTime = A.Dummy<CalendarYear>();

            ComparableTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ComparableTestScenario<CalendarUnitOfTime>
                    {
                        Name = "Comparable Test Scenario",
                        ReferenceObject = referenceCalendarUnitOfTime,
                        ObjectsThatAreLessThanReferenceObject = new[]
                        {
                            new CalendarYear(referenceCalendarUnitOfTime.Year - 1),
                        },
                        ObjectsThatAreGreaterThanReferenceObject = new[]
                        {
                            new CalendarYear(referenceCalendarUnitOfTime.Year + 1),
                        },
                        ObjectsThatAreEqualToButNotTheSameAsReferenceObject = new[]
                        {
                            referenceCalendarUnitOfTime.DeepClone(),
                        },
                        ObjectsThatAreNotOfTheSameTypeAsReferenceObject = new[]
                            {
                                A.Dummy<object>(),
                                A.Dummy<string>(),
                                A.Dummy<int>(),
                                A.Dummy<int?>(),
                                A.Dummy<Guid>(),
                            }
                            .Concat(TestCommon.GetDummyOfEachUnitOfTimeKind().Where(_ => _.UnitOfTimeKind != UnitOfTimeKind.Calendar))
                            .ToList(),
                        ObjectsThatDeriveFromScenarioTypeButAreNotOfTheSameTypeAsReferenceObject = new CalendarUnitOfTime[]
                        {
                            A.Dummy<CalendarDay>(),
                            A.Dummy<CalendarMonth>(),
                            A.Dummy<CalendarQuarter>(),
                            A.Dummy<CalendarUnbounded>(),
                        },
                    });
        }

        [Fact]
        public static void UnitOfTimeKind__Should_return_Calendar___When_called()
        {
            // Arrange
            var unitOfTime = A.Dummy<CalendarUnitOfTime>();

            // Act
            var kind = unitOfTime.UnitOfTimeKind;

            // Assert
            kind.AsTest().Must().BeEqualTo(UnitOfTimeKind.Calendar);
        }
    }
}