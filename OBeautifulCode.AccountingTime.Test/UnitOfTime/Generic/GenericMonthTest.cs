// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GenericMonthTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Test
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using FakeItEasy;

    using OBeautifulCode.AccountingTime.Test.Internal;
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.AutoFakeItEasy;
    using OBeautifulCode.CodeGen.ModelObject.Recipes;

    using Xunit;

    public static partial class GenericMonthTest
    {
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static GenericMonthTest()
        {
            var referenceGenericMonth = A.Dummy<GenericMonth>();

            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<GenericMonth>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'year' is < 0",
                        ConstructionFunc = () => new GenericMonth(A.Dummy<NegativeInteger>(), referenceGenericMonth.MonthNumber),
                        ExpectedExceptionMessageContains = new[] { "'year' < '1'" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<GenericMonth>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'year' = 0",
                        ConstructionFunc = () => new GenericMonth(0, referenceGenericMonth.MonthNumber),
                        ExpectedExceptionMessageContains = new[] { "'year' < '1'" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<GenericMonth>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'year' = 10000",
                        ConstructionFunc = () => new GenericMonth(10000, referenceGenericMonth.MonthNumber),
                        ExpectedExceptionMessageContains = new[] { "'year' > '9999'" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<GenericMonth>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'year' > 9999",
                        ConstructionFunc = () => new GenericMonth(A.Dummy<PositiveInteger>().ThatIs(_ => _ > 9999), referenceGenericMonth.MonthNumber),
                        ExpectedExceptionMessageContains = new[] { "'year' > '9999'" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<GenericMonth>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'monthNumber' is MonthNumber.Invalid",
                        ConstructionFunc = () => new GenericMonth(referenceGenericMonth.Year, MonthNumber.Invalid),
                        ExpectedExceptionMessageContains = new[] { "monthNumber", "Invalid" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    });

            ComparableTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ComparableTestScenario<GenericMonth>
                    {
                        Name = "Comparable Test Scenario",
                        ReferenceObject = referenceGenericMonth,
                        ObjectsThatAreLessThanReferenceObject = new[]
                        {
                            referenceGenericMonth.TweakBy(-1, GenericMonthComponent.Month),
                            referenceGenericMonth.TweakBy(-1, GenericMonthComponent.Year),
                        },
                        ObjectsThatAreGreaterThanReferenceObject = new[]
                        {
                            referenceGenericMonth.TweakBy(1, GenericMonthComponent.Month),
                            referenceGenericMonth.TweakBy(1, GenericMonthComponent.Year),
                        },
                        ObjectsThatAreEqualToButNotTheSameAsReferenceObject = new[]
                        {
                            referenceGenericMonth.DeepClone(),
                        },
                        ObjectsThatAreNotOfTheSameTypeAsReferenceObject = new[]
                            {
                                A.Dummy<object>(),
                                A.Dummy<string>(),
                                A.Dummy<int>(),
                                A.Dummy<int?>(),
                                A.Dummy<Guid>(),
                            }
                            .Concat(TestCommon.GetDummyOfEachUnitOfTimeKind().Where(_ => _.GetType() != typeof(GenericMonth)))
                            .ToList(),
                        ObjectsThatDeriveFromScenarioTypeButAreNotOfTheSameTypeAsReferenceObject = new GenericMonth[]
                        {
                        },
                    });

            StringRepresentationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new StringRepresentationTestScenario<GenericMonth>
                    {
                        Name = "String Representation - case 1",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                            new SystemUnderTestExpectedStringRepresentation<GenericMonth>
                            {
                                SystemUnderTest = new GenericMonth(2017, MonthNumber.One),
                                ExpectedStringRepresentation = "1st month of 2017",
                            },
                    })
                .AddScenario(() =>
                    new StringRepresentationTestScenario<GenericMonth>
                    {
                        Name = "String Representation - case 2",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                            new SystemUnderTestExpectedStringRepresentation<GenericMonth>
                            {
                                SystemUnderTest = new GenericMonth(2017, MonthNumber.Two),
                                ExpectedStringRepresentation = "2nd month of 2017",
                            },
                    })
                .AddScenario(() =>
                    new StringRepresentationTestScenario<GenericMonth>
                    {
                        Name = "String Representation - case 3",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                            new SystemUnderTestExpectedStringRepresentation<GenericMonth>
                            {
                                SystemUnderTest = new GenericMonth(2017, MonthNumber.Three),
                                ExpectedStringRepresentation = "3rd month of 2017",
                            },
                    })
                .AddScenario(() =>
                    new StringRepresentationTestScenario<GenericMonth>
                    {
                        Name = "String Representation - case 4",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                            new SystemUnderTestExpectedStringRepresentation<GenericMonth>
                            {
                                SystemUnderTest = new GenericMonth(2017, MonthNumber.Four),
                                ExpectedStringRepresentation = "4th month of 2017",
                            },
                    })
                .AddScenario(() =>
                    new StringRepresentationTestScenario<GenericMonth>
                    {
                        Name = "String Representation - case 5",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                            new SystemUnderTestExpectedStringRepresentation<GenericMonth>
                            {
                                SystemUnderTest = new GenericMonth(2017, MonthNumber.Five),
                                ExpectedStringRepresentation = "5th month of 2017",
                            },
                    })
                .AddScenario(() =>
                    new StringRepresentationTestScenario<GenericMonth>
                    {
                        Name = "String Representation - case 6",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                            new SystemUnderTestExpectedStringRepresentation<GenericMonth>
                            {
                                SystemUnderTest = new GenericMonth(2017, MonthNumber.Six),
                                ExpectedStringRepresentation = "6th month of 2017",
                            },
                    })
                .AddScenario(() =>
                    new StringRepresentationTestScenario<GenericMonth>
                    {
                        Name = "String Representation - case 7",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                            new SystemUnderTestExpectedStringRepresentation<GenericMonth>
                            {
                                SystemUnderTest = new GenericMonth(2017, MonthNumber.Seven),
                                ExpectedStringRepresentation = "7th month of 2017",
                            },
                    })
                .AddScenario(() =>
                    new StringRepresentationTestScenario<GenericMonth>
                    {
                        Name = "String Representation - case 8",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                            new SystemUnderTestExpectedStringRepresentation<GenericMonth>
                            {
                                SystemUnderTest = new GenericMonth(2017, MonthNumber.Eight),
                                ExpectedStringRepresentation = "8th month of 2017",
                            },
                    })
                .AddScenario(() =>
                    new StringRepresentationTestScenario<GenericMonth>
                    {
                        Name = "String Representation - case 9",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                            new SystemUnderTestExpectedStringRepresentation<GenericMonth>
                            {
                                SystemUnderTest = new GenericMonth(2017, MonthNumber.Nine),
                                ExpectedStringRepresentation = "9th month of 2017",
                            },
                    })
                .AddScenario(() =>
                    new StringRepresentationTestScenario<GenericMonth>
                    {
                        Name = "String Representation - case 10",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                            new SystemUnderTestExpectedStringRepresentation<GenericMonth>
                            {
                                SystemUnderTest = new GenericMonth(2017, MonthNumber.Ten),
                                ExpectedStringRepresentation = "10th month of 2017",
                            },
                    })
                .AddScenario(() =>
                    new StringRepresentationTestScenario<GenericMonth>
                    {
                        Name = "String Representation - case 11",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                            new SystemUnderTestExpectedStringRepresentation<GenericMonth>
                            {
                                SystemUnderTest = new GenericMonth(2017, MonthNumber.Eleven),
                                ExpectedStringRepresentation = "11th month of 2017",
                            },
                    })
                .AddScenario(() =>
                    new StringRepresentationTestScenario<GenericMonth>
                    {
                        Name = "String Representation - case 12",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                            new SystemUnderTestExpectedStringRepresentation<GenericMonth>
                            {
                                SystemUnderTest = new GenericMonth(2017, MonthNumber.Twelve),
                                ExpectedStringRepresentation = "12th month of 2017",
                            },
                    });
        }

        [Fact]
        public static void UnitOfTimeKind__Should_return_Generic___When_called()
        {
            // Arrange
            var unitOfTime = A.Dummy<GenericMonth>();

            // Act
            var kind = unitOfTime.UnitOfTimeKind;

            // Assert
            kind.AsTest().Must().BeEqualTo(UnitOfTimeKind.Generic);
        }

        [Fact]
        public static void UnitOfTimeGranularity__Should_return_Month___When_called()
        {
            // Arrange
            var unitOfTime = A.Dummy<GenericMonth>();

            // Act
            var granularity = unitOfTime.UnitOfTimeGranularity;

            // Assert
            granularity.AsTest().Must().BeEqualTo(UnitOfTimeGranularity.Month);
        }

        #pragma warning disable SA1201 // Elements should appear in the correct order
        private enum GenericMonthComponent
        #pragma warning disable SA1201 // Elements should appear in the correct order
        {
            Month,

            Year,
        }

        private static GenericMonth TweakBy(
            this GenericMonth genericMonth,
            int amount,
            GenericMonthComponent componentToTweak)
        {
            if (componentToTweak == GenericMonthComponent.Month)
            {
                var referenceMonth = new DateTime(genericMonth.Year, (int)genericMonth.MonthNumber, 1);

                var updatedMonth = referenceMonth.AddMonths(amount);

                var result = new GenericMonth(updatedMonth.Year, (MonthNumber)updatedMonth.Month);

                return result;
            }

            if (componentToTweak == GenericMonthComponent.Year)
            {
                var result = new GenericMonth(genericMonth.Year + amount, genericMonth.MonthNumber);

                return result;
            }

            throw new NotSupportedException("this generic month component is not supported: " + componentToTweak);
        }
    }
}