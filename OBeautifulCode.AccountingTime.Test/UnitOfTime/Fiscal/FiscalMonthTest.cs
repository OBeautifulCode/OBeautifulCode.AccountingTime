// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FiscalMonthTest.cs" company="OBeautifulCode">
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

    public static partial class FiscalMonthTest
    {
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static FiscalMonthTest()
        {
            var referenceFiscalMonth = A.Dummy<FiscalMonth>();

            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<FiscalMonth>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'year' is < 0",
                        ConstructionFunc = () => new FiscalMonth(A.Dummy<NegativeInteger>(), referenceFiscalMonth.MonthNumber),
                        ExpectedExceptionMessageContains = new[] { "'year' < '1'" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<FiscalMonth>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'year' = 0",
                        ConstructionFunc = () => new FiscalMonth(0, referenceFiscalMonth.MonthNumber),
                        ExpectedExceptionMessageContains = new[] { "'year' < '1'" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<FiscalMonth>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'year' = 10000",
                        ConstructionFunc = () => new FiscalMonth(10000, referenceFiscalMonth.MonthNumber),
                        ExpectedExceptionMessageContains = new[] { "'year' > '9999'" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<FiscalMonth>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'year' > 9999",
                        ConstructionFunc = () => new FiscalMonth(A.Dummy<PositiveInteger>().ThatIs(_ => _ > 9999), referenceFiscalMonth.MonthNumber),
                        ExpectedExceptionMessageContains = new[] { "'year' > '9999'" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<FiscalMonth>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'monthNumber' is MonthNumber.Invalid",
                        ConstructionFunc = () => new FiscalMonth(referenceFiscalMonth.Year, MonthNumber.Invalid),
                        ExpectedExceptionMessageContains = new[] { "monthNumber", "Invalid" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    });

            ComparableTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ComparableTestScenario<FiscalMonth>
                    {
                        Name = "Comparable Test Scenario",
                        ReferenceObject = referenceFiscalMonth,
                        ObjectsThatAreLessThanReferenceObject = new[]
                        {
                            referenceFiscalMonth.TweakBy(-1, FiscalMonthComponent.Month),
                            referenceFiscalMonth.TweakBy(-1, FiscalMonthComponent.Year),
                        },
                        ObjectsThatAreGreaterThanReferenceObject = new[]
                        {
                            referenceFiscalMonth.TweakBy(1, FiscalMonthComponent.Month),
                            referenceFiscalMonth.TweakBy(1, FiscalMonthComponent.Year),
                        },
                        ObjectsThatAreEqualToButNotTheSameAsReferenceObject = new[]
                        {
                            referenceFiscalMonth.DeepClone(),
                        },
                        ObjectsThatAreNotOfTheSameTypeAsReferenceObject = new[]
                            {
                                A.Dummy<object>(),
                                A.Dummy<string>(),
                                A.Dummy<int>(),
                                A.Dummy<int?>(),
                                A.Dummy<Guid>(),
                            }
                            .Concat(TestCommon.GetDummyOfEachUnitOfTimeKind().Where(_ => _.GetType() != typeof(FiscalMonth)))
                            .ToList(),
                        ObjectsThatDeriveFromScenarioTypeButAreNotOfTheSameTypeAsReferenceObject = new FiscalMonth[]
                        {
                        },
                    });

            StringRepresentationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new StringRepresentationTestScenario<FiscalMonth>
                    {
                        Name = "String Representation - case 1",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                            new SystemUnderTestExpectedStringRepresentation<FiscalMonth>
                            {
                                SystemUnderTest = new FiscalMonth(2017, MonthNumber.One),
                                ExpectedStringRepresentation = "1st month of FY2017",
                            },
                    })
                .AddScenario(() =>
                    new StringRepresentationTestScenario<FiscalMonth>
                    {
                        Name = "String Representation - case 2",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                            new SystemUnderTestExpectedStringRepresentation<FiscalMonth>
                            {
                                SystemUnderTest = new FiscalMonth(2017, MonthNumber.Two),
                                ExpectedStringRepresentation = "2nd month of FY2017",
                            },
                    })
                .AddScenario(() =>
                    new StringRepresentationTestScenario<FiscalMonth>
                    {
                        Name = "String Representation - case 3",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                            new SystemUnderTestExpectedStringRepresentation<FiscalMonth>
                            {
                                SystemUnderTest = new FiscalMonth(2017, MonthNumber.Three),
                                ExpectedStringRepresentation = "3rd month of FY2017",
                            },
                    })
                .AddScenario(() =>
                    new StringRepresentationTestScenario<FiscalMonth>
                    {
                        Name = "String Representation - case 4",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                            new SystemUnderTestExpectedStringRepresentation<FiscalMonth>
                            {
                                SystemUnderTest = new FiscalMonth(2017, MonthNumber.Four),
                                ExpectedStringRepresentation = "4th month of FY2017",
                            },
                    })
                .AddScenario(() =>
                    new StringRepresentationTestScenario<FiscalMonth>
                    {
                        Name = "String Representation - case 5",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                            new SystemUnderTestExpectedStringRepresentation<FiscalMonth>
                            {
                                SystemUnderTest = new FiscalMonth(2017, MonthNumber.Five),
                                ExpectedStringRepresentation = "5th month of FY2017",
                            },
                    })
                .AddScenario(() =>
                    new StringRepresentationTestScenario<FiscalMonth>
                    {
                        Name = "String Representation - case 6",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                            new SystemUnderTestExpectedStringRepresentation<FiscalMonth>
                            {
                                SystemUnderTest = new FiscalMonth(2017, MonthNumber.Six),
                                ExpectedStringRepresentation = "6th month of FY2017",
                            },
                    })
                .AddScenario(() =>
                    new StringRepresentationTestScenario<FiscalMonth>
                    {
                        Name = "String Representation - case 7",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                            new SystemUnderTestExpectedStringRepresentation<FiscalMonth>
                            {
                                SystemUnderTest = new FiscalMonth(2017, MonthNumber.Seven),
                                ExpectedStringRepresentation = "7th month of FY2017",
                            },
                    })
                .AddScenario(() =>
                    new StringRepresentationTestScenario<FiscalMonth>
                    {
                        Name = "String Representation - case 8",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                            new SystemUnderTestExpectedStringRepresentation<FiscalMonth>
                            {
                                SystemUnderTest = new FiscalMonth(2017, MonthNumber.Eight),
                                ExpectedStringRepresentation = "8th month of FY2017",
                            },
                    })
                .AddScenario(() =>
                    new StringRepresentationTestScenario<FiscalMonth>
                    {
                        Name = "String Representation - case 9",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                            new SystemUnderTestExpectedStringRepresentation<FiscalMonth>
                            {
                                SystemUnderTest = new FiscalMonth(2017, MonthNumber.Nine),
                                ExpectedStringRepresentation = "9th month of FY2017",
                            },
                    })
                .AddScenario(() =>
                    new StringRepresentationTestScenario<FiscalMonth>
                    {
                        Name = "String Representation - case 10",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                            new SystemUnderTestExpectedStringRepresentation<FiscalMonth>
                            {
                                SystemUnderTest = new FiscalMonth(2017, MonthNumber.Ten),
                                ExpectedStringRepresentation = "10th month of FY2017",
                            },
                    })
                .AddScenario(() =>
                    new StringRepresentationTestScenario<FiscalMonth>
                    {
                        Name = "String Representation - case 11",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                            new SystemUnderTestExpectedStringRepresentation<FiscalMonth>
                            {
                                SystemUnderTest = new FiscalMonth(2017, MonthNumber.Eleven),
                                ExpectedStringRepresentation = "11th month of FY2017",
                            },
                    })
                .AddScenario(() =>
                    new StringRepresentationTestScenario<FiscalMonth>
                    {
                        Name = "String Representation - case 12",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                            new SystemUnderTestExpectedStringRepresentation<FiscalMonth>
                            {
                                SystemUnderTest = new FiscalMonth(2017, MonthNumber.Twelve),
                                ExpectedStringRepresentation = "12th month of FY2017",
                            },
                    });
        }

        [Fact]
        public static void UnitOfTimeKind__Should_return_Fiscal___When_called()
        {
            // Arrange
            var unitOfTime = A.Dummy<FiscalMonth>();

            // Act
            var kind = unitOfTime.UnitOfTimeKind;

            // Assert
            kind.AsTest().Must().BeEqualTo(UnitOfTimeKind.Fiscal);
        }

        [Fact]
        public static void UnitOfTimeGranularity__Should_return_Month___When_called()
        {
            // Arrange
            var unitOfTime = A.Dummy<FiscalMonth>();

            // Act
            var granularity = unitOfTime.UnitOfTimeGranularity;

            // Assert
            granularity.AsTest().Must().BeEqualTo(UnitOfTimeGranularity.Month);
        }

        #pragma warning disable SA1201 // Elements should appear in the correct order
        private enum FiscalMonthComponent
        #pragma warning disable SA1201 // Elements should appear in the correct order
        {
            Month,

            Year,
        }

        private static FiscalMonth TweakBy(
            this FiscalMonth fiscalMonth,
            int amount,
            FiscalMonthComponent componentToTweak)
        {
            if (componentToTweak == FiscalMonthComponent.Month)
            {
                var referenceMonth = new DateTime(fiscalMonth.Year, (int)fiscalMonth.MonthNumber, 1);

                var updatedMonth = referenceMonth.AddMonths(amount);

                var result = new FiscalMonth(updatedMonth.Year, (MonthNumber)updatedMonth.Month);

                return result;
            }

            if (componentToTweak == FiscalMonthComponent.Year)
            {
                var result = new FiscalMonth(fiscalMonth.Year + amount, fiscalMonth.MonthNumber);

                return result;
            }

            throw new NotSupportedException("this fiscal month component is not supported: " + componentToTweak);
        }
    }
}