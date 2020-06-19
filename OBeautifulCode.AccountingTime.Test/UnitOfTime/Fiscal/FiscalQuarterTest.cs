// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FiscalQuarterTest.cs" company="OBeautifulCode">
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

    public static partial class FiscalQuarterTest
    {
        static FiscalQuarterTest()
        {
            var referenceFiscalQuarter = A.Dummy<FiscalQuarter>();

            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<FiscalQuarter>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'year' is < 0",
                        ConstructionFunc = () => new FiscalQuarter(A.Dummy<NegativeInteger>(), referenceFiscalQuarter.QuarterNumber),
                        ExpectedExceptionMessageContains = new[] { "year", "not greater than or equal to" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<FiscalQuarter>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'year' = 0",
                        ConstructionFunc = () => new FiscalQuarter(0, referenceFiscalQuarter.QuarterNumber),
                        ExpectedExceptionMessageContains = new[] { "year", "not greater than or equal to" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<FiscalQuarter>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'year' = 10000",
                        ConstructionFunc = () => new FiscalQuarter(10000, referenceFiscalQuarter.QuarterNumber),
                        ExpectedExceptionMessageContains = new[] { "year", "not less than or equal to" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<FiscalQuarter>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'year' > 9999",
                        ConstructionFunc = () => new FiscalQuarter(A.Dummy<PositiveInteger>().ThatIs(_ => _ > 9999), referenceFiscalQuarter.QuarterNumber),
                        ExpectedExceptionMessageContains = new[] { "year", "not less than or equal to" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<FiscalQuarter>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'quarterNumber' is QuarterNumber.Invalid",
                        ConstructionFunc = () => new FiscalQuarter(referenceFiscalQuarter.Year, QuarterNumber.Invalid),
                        ExpectedExceptionMessageContains = new[] { "quarterNumber", "Invalid" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    });

            ComparableTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ComparableTestScenario<FiscalQuarter>
                    {
                        Name = "Comparable Test Scenario",
                        ReferenceObject = referenceFiscalQuarter,
                        ObjectsThatAreLessThanReferenceObject = new[]
                        {
                            referenceFiscalQuarter.TweakBy(-1, FiscalQuarterComponent.Quarter),
                            referenceFiscalQuarter.TweakBy(-1, FiscalQuarterComponent.Year),
                        },
                        ObjectsThatAreGreaterThanReferenceObject = new[]
                        {
                            referenceFiscalQuarter.TweakBy(1, FiscalQuarterComponent.Quarter),
                            referenceFiscalQuarter.TweakBy(1, FiscalQuarterComponent.Year),
                        },
                        ObjectsThatAreEqualToButNotTheSameAsReferenceObject = new[]
                        {
                            referenceFiscalQuarter.DeepClone(),
                        },
                        ObjectsThatAreNotOfTheSameTypeAsReferenceObject = new[]
                            {
                                A.Dummy<object>(),
                                A.Dummy<string>(),
                                A.Dummy<int>(),
                                A.Dummy<int?>(),
                                A.Dummy<Guid>(),
                            }
                            .Concat(Common.GetDummyOfEachUnitOfTimeKind().Where(_ => _.GetType() != typeof(FiscalQuarter)))
                            .ToList(),
                        ObjectsThatDeriveFromScenarioTypeButAreNotOfTheSameTypeAsReferenceObject = new FiscalQuarter[]
                        {
                        },
                    });

            StringRepresentationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new StringRepresentationTestScenario<FiscalQuarter>
                    {
                        Name = "String Representation - case 1",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                            new SystemUnderTestExpectedStringRepresentation<FiscalQuarter>
                            {
                                SystemUnderTest = new FiscalQuarter(2017, QuarterNumber.Q1),
                                ExpectedStringRepresentation = "1Q2017",
                            },
                    })
                .AddScenario(() =>
                    new StringRepresentationTestScenario<FiscalQuarter>
                    {
                        Name = "String Representation - case 2",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                            new SystemUnderTestExpectedStringRepresentation<FiscalQuarter>
                            {
                                SystemUnderTest = new FiscalQuarter(2020, QuarterNumber.Q4),
                                ExpectedStringRepresentation = "4Q2020",
                            },
                    });
        }

        [Fact]
        public static void UnitOfTimeKind__Should_return_Fiscal___When_called()
        {
            // Arrange
            var unitOfTime = A.Dummy<FiscalQuarter>();

            // Act
            var kind = unitOfTime.UnitOfTimeKind;

            // Assert
            kind.AsTest().Must().BeEqualTo(UnitOfTimeKind.Fiscal);
        }

        [Fact]
        public static void UnitOfTimeGranularity__Should_return_Quarter___When_called()
        {
            // Arrange
            var unitOfTime = A.Dummy<FiscalQuarter>();

            // Act
            var granularity = unitOfTime.UnitOfTimeGranularity;

            // Assert
            granularity.AsTest().Must().BeEqualTo(UnitOfTimeGranularity.Quarter);
        }

        #pragma warning disable SA1201 // Elements should appear in the correct order
        private enum FiscalQuarterComponent
        #pragma warning restore SA1201 // Elements should appear in the correct order
        {
            Quarter,

            Year,
        }

        private static FiscalQuarter TweakBy(
            this FiscalQuarter fiscalQuarter,
            int amount,
            FiscalQuarterComponent componentToTweak)
        {
            if (componentToTweak == FiscalQuarterComponent.Quarter)
            {
                var referenceMonth = new DateTime(fiscalQuarter.Year, (int)fiscalQuarter.QuarterNumber * 3, 1);

                var updatedMonth = referenceMonth.AddMonths(amount * 3);

                var result = new FiscalQuarter(updatedMonth.Year, (QuarterNumber)(updatedMonth.Month / 3));

                return result;
            }

            if (componentToTweak == FiscalQuarterComponent.Year)
            {
                var result = new FiscalQuarter(fiscalQuarter.Year + amount, fiscalQuarter.QuarterNumber);

                return result;
            }

            throw new NotSupportedException("this fiscal quarter component is not supported: " + componentToTweak);
        }
    }
}