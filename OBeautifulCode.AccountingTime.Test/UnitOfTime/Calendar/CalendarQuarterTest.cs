// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalendarQuarterTest.cs" company="OBeautifulCode">
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
    using OBeautifulCode.AutoFakeItEasy;
    using OBeautifulCode.CodeAnalysis.Recipes;
    using OBeautifulCode.CodeGen.ModelObject.Recipes;

    using Xunit;

    public static partial class CalendarQuarterTest
    {
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static CalendarQuarterTest()
        {
            var referenceCalendarQuarter = A.Dummy<CalendarQuarter>();

            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CalendarQuarter>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'year' is < 0",
                        ConstructionFunc = () => new CalendarQuarter(A.Dummy<NegativeInteger>(), referenceCalendarQuarter.QuarterNumber),
                        ExpectedExceptionMessageContains = new[] { "'year' < '1'" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CalendarQuarter>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'year' = 0",
                        ConstructionFunc = () => new CalendarQuarter(0, referenceCalendarQuarter.QuarterNumber),
                        ExpectedExceptionMessageContains = new[] { "'year' < '1'" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CalendarQuarter>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'year' = 10000",
                        ConstructionFunc = () => new CalendarQuarter(10000, referenceCalendarQuarter.QuarterNumber),
                        ExpectedExceptionMessageContains = new[] { "'year' > '9999'" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CalendarQuarter>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'year' > 9999",
                        ConstructionFunc = () => new CalendarQuarter(A.Dummy<PositiveInteger>().ThatIs(_ => _ > 9999), referenceCalendarQuarter.QuarterNumber),
                        ExpectedExceptionMessageContains = new[] { "'year' > '9999'" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CalendarQuarter>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'quarterNumber' is QuarterNumber.Invalid",
                        ConstructionFunc = () => new CalendarQuarter(referenceCalendarQuarter.Year, QuarterNumber.Invalid),
                        ExpectedExceptionMessageContains = new[] { "quarterNumber", "Invalid" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    });

            ComparableTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ComparableTestScenario<CalendarQuarter>
                    {
                        Name = "Comparable Test Scenario",
                        ReferenceObject = referenceCalendarQuarter,
                        ObjectsThatAreLessThanReferenceObject = new[]
                        {
                            referenceCalendarQuarter.TweakBy(-1, CalendarQuarterComponent.Quarter),
                            referenceCalendarQuarter.TweakBy(-1, CalendarQuarterComponent.Year),
                        },
                        ObjectsThatAreGreaterThanReferenceObject = new[]
                        {
                            referenceCalendarQuarter.TweakBy(1, CalendarQuarterComponent.Quarter),
                            referenceCalendarQuarter.TweakBy(1, CalendarQuarterComponent.Year),
                        },
                        ObjectsThatAreEqualToButNotTheSameAsReferenceObject = new[]
                        {
                            referenceCalendarQuarter.DeepClone(),
                        },
                        ObjectsThatAreNotOfTheSameTypeAsReferenceObject = new[]
                            {
                                A.Dummy<object>(),
                                A.Dummy<string>(),
                                A.Dummy<int>(),
                                A.Dummy<int?>(),
                                A.Dummy<Guid>(),
                            }
                            .Concat(TestCommon.GetDummyOfEachUnitOfTimeKind().Where(_ => _.GetType() != typeof(CalendarQuarter)))
                            .ToList(),
                        ObjectsThatDeriveFromScenarioTypeButAreNotOfTheSameTypeAsReferenceObject = new CalendarQuarter[]
                        {
                        },
                    });

            StringRepresentationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new StringRepresentationTestScenario<CalendarQuarter>
                    {
                        Name = "String Representation - case 1",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                            new SystemUnderTestExpectedStringRepresentation<CalendarQuarter>
                            {
                                SystemUnderTest = new CalendarQuarter(2017, QuarterNumber.Q1),
                                ExpectedStringRepresentation = "1Q2017",
                            },
                    })
                .AddScenario(() =>
                    new StringRepresentationTestScenario<CalendarQuarter>
                    {
                        Name = "String Representation - case 2",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                            new SystemUnderTestExpectedStringRepresentation<CalendarQuarter>
                            {
                                SystemUnderTest = new CalendarQuarter(2020, QuarterNumber.Q4),
                                ExpectedStringRepresentation = "4Q2020",
                            },
                    });
        }

        [Fact]
        public static void UnitOfTimeKind__Should_return_Calendar___When_called()
        {
            // Arrange
            var unitOfTime = A.Dummy<CalendarQuarter>();

            // Act
            var kind = unitOfTime.UnitOfTimeKind;

            // Assert
            kind.AsTest().Must().BeEqualTo(UnitOfTimeKind.Calendar);
        }

        [Fact]
        public static void UnitOfTimeGranularity__Should_return_Quarter___When_called()
        {
            // Arrange
            var unitOfTime = A.Dummy<CalendarQuarter>();

            // Act
            var granularity = unitOfTime.UnitOfTimeGranularity;

            // Assert
            granularity.AsTest().Must().BeEqualTo(UnitOfTimeGranularity.Quarter);
        }

        #pragma warning disable SA1201 // Elements should appear in the correct order
        private enum CalendarQuarterComponent
        #pragma warning restore SA1201 // Elements should appear in the correct order
        {
            Quarter,

            Year,
        }

        private static CalendarQuarter TweakBy(
            this CalendarQuarter calendarQuarter,
            int amount,
            CalendarQuarterComponent componentToTweak)
        {
            if (componentToTweak == CalendarQuarterComponent.Quarter)
            {
                var referenceMonth = new DateTime(calendarQuarter.Year, (int)calendarQuarter.QuarterNumber * 3, 1);

                var updatedMonth = referenceMonth.AddMonths(amount * 3);

                var result = new CalendarQuarter(updatedMonth.Year, (QuarterNumber)(updatedMonth.Month / 3));

                return result;
            }

            if (componentToTweak == CalendarQuarterComponent.Year)
            {
                var result = new CalendarQuarter(calendarQuarter.Year + amount, calendarQuarter.QuarterNumber);

                return result;
            }

            throw new NotSupportedException("this calendar quarter component is not supported: " + componentToTweak);
        }
    }
}
