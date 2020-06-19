// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FiscalUnitOfTimeTest.cs" company="OBeautifulCode">
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

    public static partial class FiscalUnitOfTimeTest
    {
        static FiscalUnitOfTimeTest()
        {
            var referenceFiscalUnitOfTime = A.Dummy<FiscalYear>();

            ComparableTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ComparableTestScenario<FiscalUnitOfTime>
                    {
                        Name = "Comparable Test Scenario",
                        ReferenceObject = referenceFiscalUnitOfTime,
                        ObjectsThatAreLessThanReferenceObject = new[]
                        {
                            new FiscalYear(referenceFiscalUnitOfTime.Year - 1),
                        },
                        ObjectsThatAreGreaterThanReferenceObject = new[]
                        {
                            new FiscalYear(referenceFiscalUnitOfTime.Year + 1),
                        },
                        ObjectsThatAreEqualToButNotTheSameAsReferenceObject = new[]
                        {
                            referenceFiscalUnitOfTime.DeepClone(),
                        },
                        ObjectsThatAreNotOfTheSameTypeAsReferenceObject = new[]
                            {
                                A.Dummy<object>(),
                                A.Dummy<string>(),
                                A.Dummy<int>(),
                                A.Dummy<int?>(),
                                A.Dummy<Guid>(),
                            }
                            .Concat(Common.GetDummyOfEachUnitOfTimeKind().Where(_ => _.UnitOfTimeKind != UnitOfTimeKind.Fiscal))
                            .ToList(),
                        ObjectsThatDeriveFromScenarioTypeButAreNotOfTheSameTypeAsReferenceObject = new FiscalUnitOfTime[]
                        {
                            A.Dummy<FiscalMonth>(),
                            A.Dummy<FiscalQuarter>(),
                            A.Dummy<FiscalUnbounded>(),
                        },
                    });
        }

        [Fact]
        public static void UnitOfTimeKind__Should_return_Fiscal___When_called()
        {
            // Arrange
            var unitOfTime = A.Dummy<FiscalUnitOfTime>();

            // Act
            var kind = unitOfTime.UnitOfTimeKind;

            // Assert
            kind.AsTest().Must().BeEqualTo(UnitOfTimeKind.Fiscal);
        }
    }
}