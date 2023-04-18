// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Test
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using FakeItEasy;

    using OBeautifulCode.AutoFakeItEasy;
    using OBeautifulCode.CodeAnalysis.Recipes;
    using OBeautifulCode.CodeGen.ModelObject.Recipes;
    using OBeautifulCode.Equality.Recipes;
    using OBeautifulCode.Math.Recipes;

    using Xunit;

    using static System.FormattableString;

    [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
    public static partial class UnitTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static UnitTest()
        {
            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<Unit>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'kind' is UnitOfTimeKind.Invalid",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<Unit>();

                            var result = new Unit(
                                UnitOfTimeKind.Invalid,
                                referenceObject.Granularity);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "kind is UnitOfTimeKind.Invalid" },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<Unit>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'granularity' is UnitOfTimeGranularity.Invalid",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<Unit>();

                            var result = new Unit(
                                referenceObject.Kind,
                                UnitOfTimeGranularity.Invalid);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "granularity is UnitOfTimeGranularity.Invalid" },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<Unit>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'granularity' is UnitOfTimeGranularity.Day and parameter 'kind' is not UnitOfTimeKind.Calendar",
                        ConstructionFunc = () =>
                        {
                            var result = new Unit(
                                A.Dummy<UnitOfTimeKind>().ThatIsNot(UnitOfTimeKind.Calendar),
                                UnitOfTimeGranularity.Day);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "granularity of UnitOfTimeGranularity.Day is only applicable when kind is UnitOfTimeKind.Calendar" },
                    });

            DeepCloneWithTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<Unit>
                    {
                        Name = "DeepCloneWithKind should deep clone object and replace Kind with the provided kind",
                        WithPropertyName = "Kind",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<Unit>().Whose(_ => _.Granularity != UnitOfTimeGranularity.Day);

                            var referenceObject = A.Dummy<Unit>().ThatIs(_ => !systemUnderTest.Kind.IsEqualTo(_.Kind));

                            var result = new SystemUnderTestDeepCloneWithValue<Unit>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = referenceObject.Kind,
                            };

                            return result;
                        },
                    })
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<Unit>
                    {
                        Name = "DeepCloneWithGranularity should deep clone object and replace Granularity with the provided granularity",
                        WithPropertyName = "Granularity",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<Unit>();

                            var referenceObject = A.Dummy<Unit>().ThatIs(_ => (!systemUnderTest.Granularity.IsEqualTo(_.Granularity)) && (_.Granularity != UnitOfTimeGranularity.Day));

                            var result = new SystemUnderTestDeepCloneWithValue<Unit>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = referenceObject.Granularity,
                            };

                            return result;
                        },
                    });

            var referenceObjectForEquatableTestScenarios = A.Dummy<Unit>().Whose(_ => _.Granularity != UnitOfTimeGranularity.Day);

            EquatableTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new EquatableTestScenario<Unit>
                    {
                        Name = "Equatable Test Scenario",
                        ReferenceObject = referenceObjectForEquatableTestScenarios,
                        ObjectsThatAreEqualToButNotTheSameAsReferenceObject = new Unit[]
                        {
                            new Unit(
                                referenceObjectForEquatableTestScenarios.Kind,
                                referenceObjectForEquatableTestScenarios.Granularity),
                        },
                        ObjectsThatAreNotEqualToReferenceObject = new Unit[]
                        {
                            new Unit(
                                A.Dummy<Unit>().Whose(_ => (_.Granularity != UnitOfTimeGranularity.Day) && (!_.Kind.IsEqualTo(referenceObjectForEquatableTestScenarios.Kind))).Kind,
                                referenceObjectForEquatableTestScenarios.Granularity),
                            new Unit(
                                referenceObjectForEquatableTestScenarios.Kind,
                                A.Dummy<Unit>().Whose(_ => (_.Granularity != UnitOfTimeGranularity.Day) && (!_.Granularity.IsEqualTo(referenceObjectForEquatableTestScenarios.Granularity))).Granularity),
                        },
                        ObjectsThatAreNotOfTheSameTypeAsReferenceObject = new object[]
                        {
                            A.Dummy<object>(),
                            A.Dummy<string>(),
                            A.Dummy<int>(),
                            A.Dummy<int?>(),
                            A.Dummy<Guid>(),
                        },
                    });
        }
    }
}