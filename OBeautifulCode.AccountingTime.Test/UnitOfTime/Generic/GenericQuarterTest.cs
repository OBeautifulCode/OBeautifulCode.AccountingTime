// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GenericQuarterTest.cs" company="OBeautifulCode">
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

    public static partial class GenericQuarterTest
    {
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static GenericQuarterTest()
        {
            var referenceGenericQuarter = A.Dummy<GenericQuarter>();

            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<GenericQuarter>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'year' is < 0",
                        ConstructionFunc = () => new GenericQuarter(A.Dummy<NegativeInteger>(), referenceGenericQuarter.QuarterNumber),
                        ExpectedExceptionMessageContains = new[] { "'year' < '1'" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<GenericQuarter>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'year' = 0",
                        ConstructionFunc = () => new GenericQuarter(0, referenceGenericQuarter.QuarterNumber),
                        ExpectedExceptionMessageContains = new[] { "'year' < '1'" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<GenericQuarter>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'year' = 10000",
                        ConstructionFunc = () => new GenericQuarter(10000, referenceGenericQuarter.QuarterNumber),
                        ExpectedExceptionMessageContains = new[] { "'year' > '9999'" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<GenericQuarter>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'year' > 9999",
                        ConstructionFunc = () => new GenericQuarter(A.Dummy<PositiveInteger>().ThatIs(_ => _ > 9999), referenceGenericQuarter.QuarterNumber),
                        ExpectedExceptionMessageContains = new[] { "'year' > '9999'" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<GenericQuarter>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'quarterNumber' is QuarterNumber.Invalid",
                        ConstructionFunc = () => new GenericQuarter(referenceGenericQuarter.Year, QuarterNumber.Invalid),
                        ExpectedExceptionMessageContains = new[] { "quarterNumber", "Invalid" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    });

            ComparableTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ComparableTestScenario<GenericQuarter>
                    {
                        Name = "Comparable Test Scenario",
                        ReferenceObject = referenceGenericQuarter,
                        ObjectsThatAreLessThanReferenceObject = new[]
                        {
                            referenceGenericQuarter.TweakBy(-1, GenericQuarterComponent.Quarter),
                            referenceGenericQuarter.TweakBy(-1, GenericQuarterComponent.Year),
                        },
                        ObjectsThatAreGreaterThanReferenceObject = new[]
                        {
                            referenceGenericQuarter.TweakBy(1, GenericQuarterComponent.Quarter),
                            referenceGenericQuarter.TweakBy(1, GenericQuarterComponent.Year),
                        },
                        ObjectsThatAreEqualToButNotTheSameAsReferenceObject = new[]
                        {
                            referenceGenericQuarter.DeepClone(),
                        },
                        ObjectsThatAreNotOfTheSameTypeAsReferenceObject = new[]
                            {
                                A.Dummy<object>(),
                                A.Dummy<string>(),
                                A.Dummy<int>(),
                                A.Dummy<int?>(),
                                A.Dummy<Guid>(),
                            }
                            .Concat(TestCommon.GetDummyOfEachUnitOfTimeKind().Where(_ => _.GetType() != typeof(GenericQuarter)))
                            .ToList(),
                        ObjectsThatDeriveFromScenarioTypeButAreNotOfTheSameTypeAsReferenceObject = new GenericQuarter[]
                        {
                        },
                    });

            StringRepresentationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new StringRepresentationTestScenario<GenericQuarter>
                    {
                        Name = "String Representation - case 1",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                            new SystemUnderTestExpectedStringRepresentation<GenericQuarter>
                            {
                                SystemUnderTest = new GenericQuarter(2017, QuarterNumber.Q1),
                                ExpectedStringRepresentation = "1Q2017",
                            },
                    })
                .AddScenario(() =>
                    new StringRepresentationTestScenario<GenericQuarter>
                    {
                        Name = "String Representation - case 2",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                            new SystemUnderTestExpectedStringRepresentation<GenericQuarter>
                            {
                                SystemUnderTest = new GenericQuarter(2020, QuarterNumber.Q4),
                                ExpectedStringRepresentation = "4Q2020",
                            },
                    });
        }

        [Fact]
        public static void UnitOfTimeKind__Should_return_Generic___When_called()
        {
            // Arrange
            var unitOfTime = A.Dummy<GenericQuarter>();

            // Act
            var kind = unitOfTime.UnitOfTimeKind;

            // Assert
            kind.AsTest().Must().BeEqualTo(UnitOfTimeKind.Generic);
        }

        [Fact]
        public static void UnitOfTimeGranularity__Should_return_Quarter___When_called()
        {
            // Arrange
            var unitOfTime = A.Dummy<GenericQuarter>();

            // Act
            var granularity = unitOfTime.UnitOfTimeGranularity;

            // Assert
            granularity.AsTest().Must().BeEqualTo(UnitOfTimeGranularity.Quarter);
        }

#pragma warning disable SA1201 // Elements should appear in the correct order
        private enum GenericQuarterComponent
#pragma warning restore SA1201 // Elements should appear in the correct order
        {
            Quarter,

            Year,
        }

        private static GenericQuarter TweakBy(
            this GenericQuarter genericQuarter,
            int amount,
            GenericQuarterComponent componentToTweak)
        {
            if (componentToTweak == GenericQuarterComponent.Quarter)
            {
                var referenceMonth = new DateTime(genericQuarter.Year, (int)genericQuarter.QuarterNumber * 3, 1);

                var updatedMonth = referenceMonth.AddMonths(amount * 3);

                var result = new GenericQuarter(updatedMonth.Year, (QuarterNumber)(updatedMonth.Month / 3));

                return result;
            }

            if (componentToTweak == GenericQuarterComponent.Year)
            {
                var result = new GenericQuarter(genericQuarter.Year + amount, genericQuarter.QuarterNumber);

                return result;
            }

            throw new NotSupportedException("this generic quarter component is not supported: " + componentToTweak);
        }
    }
}