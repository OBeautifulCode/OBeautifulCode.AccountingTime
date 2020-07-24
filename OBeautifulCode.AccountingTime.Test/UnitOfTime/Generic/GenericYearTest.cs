// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GenericYearTest.cs" company="OBeautifulCode">
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

    public static partial class GenericYearTest
    {
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static GenericYearTest()
        {
            var referenceGenericYear = A.Dummy<GenericYear>();

            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<GenericYear>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'year' is < 0",
                        ConstructionFunc = () => new GenericYear(A.Dummy<NegativeInteger>()),
                        ExpectedExceptionMessageContains = new[] { "'year' < '1'" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<GenericYear>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'year' = 0",
                        ConstructionFunc = () => new GenericYear(0),
                        ExpectedExceptionMessageContains = new[] { "'year' < '1'" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<GenericYear>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'year' = 10000",
                        ConstructionFunc = () => new GenericYear(10000),
                        ExpectedExceptionMessageContains = new[] { "'year' > '9999'" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<GenericYear>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'year' > 9999",
                        ConstructionFunc = () => new GenericYear(A.Dummy<PositiveInteger>().ThatIs(_ => _ > 9999)),
                        ExpectedExceptionMessageContains = new[] { "'year' > '9999'" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    });

            ComparableTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ComparableTestScenario<GenericYear>
                    {
                        Name = "Comparable Test Scenario",
                        ReferenceObject = referenceGenericYear,
                        ObjectsThatAreLessThanReferenceObject = new[]
                        {
                            referenceGenericYear.TweakBy(-1),
                        },
                        ObjectsThatAreGreaterThanReferenceObject = new[]
                        {
                            referenceGenericYear.TweakBy(1),
                        },
                        ObjectsThatAreEqualToButNotTheSameAsReferenceObject = new[]
                        {
                            referenceGenericYear.DeepClone(),
                        },
                        ObjectsThatAreNotOfTheSameTypeAsReferenceObject = new[]
                            {
                                A.Dummy<object>(),
                                A.Dummy<string>(),
                                A.Dummy<int>(),
                                A.Dummy<int?>(),
                                A.Dummy<Guid>(),
                            }
                            .Concat(TestCommon.GetDummyOfEachUnitOfTimeKind().Where(_ => _.GetType() != typeof(GenericYear)))
                            .ToList(),
                        ObjectsThatDeriveFromScenarioTypeButAreNotOfTheSameTypeAsReferenceObject = new GenericYear[]
                        {
                        },
                    });

            StringRepresentationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new StringRepresentationTestScenario<GenericYear>
                    {
                        Name = "String Representation - case 1",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                            new SystemUnderTestExpectedStringRepresentation<GenericYear>
                            {
                                SystemUnderTest = new GenericYear(2017),
                                ExpectedStringRepresentation = "2017",
                            },
                    })
                .AddScenario(() =>
                    new StringRepresentationTestScenario<GenericYear>
                    {
                        Name = "String Representation - case 2",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                            new SystemUnderTestExpectedStringRepresentation<GenericYear>
                            {
                                SystemUnderTest = new GenericYear(2020),
                                ExpectedStringRepresentation = "2020",
                            },
                    });
        }

        [Fact]
        public static void UnitOfTimeKind__Should_return_Generic___When_called()
        {
            // Arrange
            var unitOfTime = A.Dummy<GenericYear>();

            // Act
            var kind = unitOfTime.UnitOfTimeKind;

            // Assert
            kind.AsTest().Must().BeEqualTo(UnitOfTimeKind.Generic);
        }

        [Fact]
        public static void UnitOfTimeGranularity__Should_return_Year___When_called()
        {
            // Arrange
            var unitOfTime = A.Dummy<GenericYear>();

            // Act
            var granularity = unitOfTime.UnitOfTimeGranularity;

            // Assert
            granularity.AsTest().Must().BeEqualTo(UnitOfTimeGranularity.Year);
        }

        private static GenericYear TweakBy(
            this GenericYear genericYear,
            int amount)
        {
            var result = new GenericYear(genericYear.Year + amount);

            return result;
        }
    }
}