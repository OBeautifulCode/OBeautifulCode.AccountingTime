// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FiscalYearTest.cs" company="OBeautifulCode">
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

    public static partial class FiscalYearTest
    {
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static FiscalYearTest()
        {
            var referenceFiscalYear = A.Dummy<FiscalYear>();

            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<FiscalYear>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'year' is < 0",
                        ConstructionFunc = () => new FiscalYear(A.Dummy<NegativeInteger>()),
                        ExpectedExceptionMessageContains = new[] { "year", "not greater than or equal to" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<FiscalYear>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'year' = 0",
                        ConstructionFunc = () => new FiscalYear(0),
                        ExpectedExceptionMessageContains = new[] { "year", "not greater than or equal to" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<FiscalYear>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'year' = 10000",
                        ConstructionFunc = () => new FiscalYear(10000),
                        ExpectedExceptionMessageContains = new[] { "year", "not less than or equal to" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<FiscalYear>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'year' > 9999",
                        ConstructionFunc = () => new FiscalYear(A.Dummy<PositiveInteger>().ThatIs(_ => _ > 9999)),
                        ExpectedExceptionMessageContains = new[] { "year", "not less than or equal to" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    });

            ComparableTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ComparableTestScenario<FiscalYear>
                    {
                        Name = "Comparable Test Scenario",
                        ReferenceObject = referenceFiscalYear,
                        ObjectsThatAreLessThanReferenceObject = new[]
                        {
                            referenceFiscalYear.TweakBy(-1),
                        },
                        ObjectsThatAreGreaterThanReferenceObject = new[]
                        {
                            referenceFiscalYear.TweakBy(1),
                        },
                        ObjectsThatAreEqualToButNotTheSameAsReferenceObject = new[]
                        {
                            referenceFiscalYear.DeepClone(),
                        },
                        ObjectsThatAreNotOfTheSameTypeAsReferenceObject =
                            new[]
                            {
                                A.Dummy<object>(),
                                A.Dummy<string>(),
                                A.Dummy<int>(),
                                A.Dummy<int?>(),
                                A.Dummy<Guid>(),
                            }
                            .Concat(TestCommon.GetDummyOfEachUnitOfTimeKind().Where(_ => _.GetType() != typeof(FiscalYear)))
                            .ToList(),
                        ObjectsThatDeriveFromScenarioTypeButAreNotOfTheSameTypeAsReferenceObject = new FiscalYear[]
                        {
                        },
                    });

            StringRepresentationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new StringRepresentationTestScenario<FiscalYear>
                    {
                        Name = "String Representation - case 1",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                            new SystemUnderTestExpectedStringRepresentation<FiscalYear>
                            {
                                SystemUnderTest = new FiscalYear(2017),
                                ExpectedStringRepresentation = "FY2017",
                            },
                    })
                .AddScenario(() =>
                    new StringRepresentationTestScenario<FiscalYear>
                    {
                        Name = "String Representation - case 2",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                            new SystemUnderTestExpectedStringRepresentation<FiscalYear>
                            {
                                SystemUnderTest = new FiscalYear(2020),
                                ExpectedStringRepresentation = "FY2020",
                            },
                    });
        }

        [Fact]
        public static void UnitOfTimeKind__Should_return_Fiscal___When_called()
        {
            // Arrange
            var unitOfTime = A.Dummy<FiscalYear>();

            // Act
            var kind = unitOfTime.UnitOfTimeKind;

            // Assert
            kind.AsTest().Must().BeEqualTo(UnitOfTimeKind.Fiscal);
        }

        [Fact]
        public static void UnitOfTimeGranularity__Should_return_Year___When_called()
        {
            // Arrange
            var unitOfTime = A.Dummy<FiscalYear>();

            // Act
            var granularity = unitOfTime.UnitOfTimeGranularity;

            // Assert
            granularity.AsTest().Must().BeEqualTo(UnitOfTimeGranularity.Year);
        }

        private static FiscalYear TweakBy(
            this FiscalYear fiscalYear,
            int amount)
        {
            var result = new FiscalYear(fiscalYear.Year + amount);

            return result;
        }
    }
}