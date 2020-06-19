// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FiscalUnboundedTest.cs" company="OBeautifulCode">
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

    public static partial class FiscalUnboundedTest
    {
        static FiscalUnboundedTest()
        {
            var referenceFiscalUnbounded = A.Dummy<FiscalUnbounded>();

            ComparableTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ComparableTestScenario<FiscalUnbounded>
                    {
                        Name = "Comparable Test Scenario",
                        ReferenceObject = referenceFiscalUnbounded,
                        ObjectsThatAreLessThanReferenceObject = new FiscalUnbounded[]
                        {
                        },
                        ObjectsThatAreGreaterThanReferenceObject = new FiscalUnbounded[]
                        {
                        },
                        ObjectsThatAreEqualToButNotTheSameAsReferenceObject = new[]
                        {
                            referenceFiscalUnbounded.DeepClone(),
                        },
                        ObjectsThatAreNotOfTheSameTypeAsReferenceObject = new[]
                            {
                                A.Dummy<object>(),
                                A.Dummy<string>(),
                                A.Dummy<int>(),
                                A.Dummy<int?>(),
                                A.Dummy<Guid>(),
                            }
                            .Concat(Common.GetDummyOfEachUnitOfTimeKind().Where(_ => _.GetType() != typeof(FiscalUnbounded)))
                            .ToList(),
                        ObjectsThatDeriveFromScenarioTypeButAreNotOfTheSameTypeAsReferenceObject = new FiscalUnbounded[]
                        {
                        },
                    });

            StringRepresentationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new StringRepresentationTestScenario<FiscalUnbounded>
                    {
                        Name = "String Representation - case 1",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                            new SystemUnderTestExpectedStringRepresentation<FiscalUnbounded>
                            {
                                SystemUnderTest = new FiscalUnbounded(),
                                ExpectedStringRepresentation = "fiscal unbounded",
                            },
                    });
        }

        [Fact]
        public static void UnitOfTimeKind__Should_return_Fiscal___When_called()
        {
            // Arrange
            var unitOfTime = A.Dummy<FiscalUnbounded>();

            // Act
            var kind = unitOfTime.UnitOfTimeKind;

            // Assert
            kind.AsTest().Must().BeEqualTo(UnitOfTimeKind.Fiscal);
        }

        [Fact]
        public static void UnitOfTimeGranularity__Should_return_Unbounded___When_called()
        {
            // Arrange
            var unitOfTime = A.Dummy<FiscalUnbounded>();

            // Act
            var granularity = unitOfTime.UnitOfTimeGranularity;

            // Assert
            granularity.AsTest().Must().BeEqualTo(UnitOfTimeGranularity.Unbounded);
        }
    }
}