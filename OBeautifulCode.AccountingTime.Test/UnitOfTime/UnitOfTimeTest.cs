// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfTimeTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Test
{
    using System;
    using System.Linq;

    using FakeItEasy;

    using OBeautifulCode.CodeGen.ModelObject.Recipes;

    public static partial class UnitOfTimeTest
    {
        static UnitOfTimeTest()
        {
            var referenceUnitOfTime = A.Dummy<FiscalYear>();

            ComparableTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ComparableTestScenario<UnitOfTime>
                    {
                        Name = "Comparable Test Scenario",
                        ReferenceObject = referenceUnitOfTime,
                        ObjectsThatAreLessThanReferenceObject = new[]
                        {
                            new FiscalYear(referenceUnitOfTime.Year - 1),
                        },
                        ObjectsThatAreGreaterThanReferenceObject = new[]
                        {
                            new FiscalYear(referenceUnitOfTime.Year + 1),
                        },
                        ObjectsThatAreEqualToButNotTheSameAsReferenceObject = new[]
                        {
                            referenceUnitOfTime.DeepClone(),
                        },
                        ObjectsThatAreNotOfTheSameTypeAsReferenceObject = new[]
                        {
                            A.Dummy<object>(),
                            A.Dummy<string>(),
                            A.Dummy<int>(),
                            A.Dummy<int?>(),
                            A.Dummy<Guid>(),
                        },
                        ObjectsThatDeriveFromScenarioTypeButAreNotOfTheSameTypeAsReferenceObject =
                            Common.GetDummyOfEachUnitOfTimeKind().Where(_ => _.GetType() != typeof(FiscalYear)).ToList(),
                    });
        }
    }
}