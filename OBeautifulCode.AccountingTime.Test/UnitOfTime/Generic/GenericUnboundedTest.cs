// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GenericUnboundedTest.cs" company="OBeautifulCode">
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
    using OBeautifulCode.CodeGen.ModelObject.Recipes;

    using Xunit;

    public static partial class GenericUnboundedTest
    {
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static GenericUnboundedTest()
        {
            var referenceGenericUnbounded = A.Dummy<GenericUnbounded>();

            ComparableTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ComparableTestScenario<GenericUnbounded>
                    {
                        Name = "Comparable Test Scenario",
                        ReferenceObject = referenceGenericUnbounded,
                        ObjectsThatAreLessThanReferenceObject = new GenericUnbounded[]
                        {
                        },
                        ObjectsThatAreGreaterThanReferenceObject = new GenericUnbounded[]
                        {
                        },
                        ObjectsThatAreEqualToButNotTheSameAsReferenceObject = new[]
                        {
                            referenceGenericUnbounded.DeepClone(),
                        },
                        ObjectsThatAreNotOfTheSameTypeAsReferenceObject = new[]
                            {
                                A.Dummy<object>(),
                                A.Dummy<string>(),
                                A.Dummy<int>(),
                                A.Dummy<int?>(),
                                A.Dummy<Guid>(),
                            }
                            .Concat(TestCommon.GetDummyOfEachUnitOfTimeKind().Where(_ => _.GetType() != typeof(GenericUnbounded)))
                            .ToList(),
                        ObjectsThatDeriveFromScenarioTypeButAreNotOfTheSameTypeAsReferenceObject = new GenericUnbounded[]
                        {
                        },
                    });

            StringRepresentationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new StringRepresentationTestScenario<GenericUnbounded>
                    {
                        Name = "String Representation - case 1",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                            new SystemUnderTestExpectedStringRepresentation<GenericUnbounded>
                            {
                                SystemUnderTest = new GenericUnbounded(),
                                ExpectedStringRepresentation = "generic unbounded",
                            },
                    });
        }

        [Fact]
        public static void UnitOfTimeKind__Should_return_Generic___When_called()
        {
            // Arrange
            var unitOfTime = A.Dummy<GenericUnbounded>();

            // Act
            var kind = unitOfTime.UnitOfTimeKind;

            // Assert
            kind.AsTest().Must().BeEqualTo(UnitOfTimeKind.Generic);
        }

        [Fact]
        public static void UnitOfTimeGranularity__Should_return_Unbounded___When_called()
        {
            // Arrange
            var unitOfTime = A.Dummy<GenericUnbounded>();

            // Act
            var granularity = unitOfTime.UnitOfTimeGranularity;

            // Assert
            granularity.AsTest().Must().BeEqualTo(UnitOfTimeGranularity.Unbounded);
        }
    }
}