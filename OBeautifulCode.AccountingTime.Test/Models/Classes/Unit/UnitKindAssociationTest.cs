// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitKindAssociationTest.cs" company="OBeautifulCode">
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
    public static partial class UnitKindAssociationTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static UnitKindAssociationTest()
        {
            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<UnitKindAssociation>
                    {
                        Name = "constructor should throw ArgumentNullException when parameter 'reportingPeriod1' is null scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<UnitKindAssociation>();

                            var result = new UnitKindAssociation(
                                null,
                                referenceObject.ReportingPeriod2,
                                referenceObject.Id);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentNullException),
                        ExpectedExceptionMessageContains = new[] { "reportingPeriod1", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<UnitKindAssociation>
                    {
                        Name = "constructor should throw ArgumentNullException when parameter 'reportingPeriod2' is null scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<UnitKindAssociation>();

                            var result = new UnitKindAssociation(
                                referenceObject.ReportingPeriod1,
                                null,
                                referenceObject.Id);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentNullException),
                        ExpectedExceptionMessageContains = new[] { "reportingPeriod2", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<UnitKindAssociation>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'reportingPeriod1' has a component with unbounded granularity",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<UnitKindAssociation>();

                            var result = new UnitKindAssociation(
                                A.Dummy<ReportingPeriod>().Whose(_ => _.HasComponentWithUnboundedGranularity()),
                                referenceObject.ReportingPeriod2,
                                referenceObject.Id);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "reportingPeriod1 has a component whose UnitOfTimeGranularity is Unbounded", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<UnitKindAssociation>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'reportingPeriod2' has a component with unbounded granularity",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<UnitKindAssociation>();

                            var result = new UnitKindAssociation(
                                referenceObject.ReportingPeriod1,
                                A.Dummy<ReportingPeriod>().Whose(_ => _.HasComponentWithUnboundedGranularity()),
                                referenceObject.Id);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "reportingPeriod2 has a component whose UnitOfTimeGranularity is Unbounded", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<UnitKindAssociation>
                    {
                        Name = "constructor should throw ArgumentException when parameters 'reportingPeriod1' and 'reportingPeriod2' have the same UnitOfTimeKind",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<UnitKindAssociation>();

                            var result = new UnitKindAssociation(
                                referenceObject.ReportingPeriod1,
                                A.Dummy<ReportingPeriod>().Whose(_ => (!_.HasComponentWithUnboundedGranularity()) && (_.GetUnitOfTimeKind() == referenceObject.ReportingPeriod1.GetUnitOfTimeKind())),
                                referenceObject.Id);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "The specified reporting periods have the same UnitOfTimeKind", },
                    });

            EquatableTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new EquatableTestScenario<UnitKindAssociation>
                    {
                        Name = "Fix Default Code Generated Scenario",
                        ReferenceObject = ReferenceObjectForEquatableTestScenarios,
                        ObjectsThatAreEqualToButNotTheSameAsReferenceObject = new UnitKindAssociation[]
                        {
                                new UnitKindAssociation(
                                        ReferenceObjectForEquatableTestScenarios.ReportingPeriod1,
                                        ReferenceObjectForEquatableTestScenarios.ReportingPeriod2,
                                        ReferenceObjectForEquatableTestScenarios.Id),
                        },
                        ObjectsThatAreNotEqualToReferenceObject = new UnitKindAssociation[]
                        {
                                new UnitKindAssociation(
                                        A.Dummy<UnitKindAssociation>().Whose(_ => !_.ReportingPeriod1.IsEqualTo(ReferenceObjectForEquatableTestScenarios.ReportingPeriod1) && (_.ReportingPeriod1.GetUnitOfTimeKind() != ReferenceObjectForEquatableTestScenarios.ReportingPeriod2.GetUnitOfTimeKind())).ReportingPeriod1,
                                        ReferenceObjectForEquatableTestScenarios.ReportingPeriod2,
                                        ReferenceObjectForEquatableTestScenarios.Id),
                                new UnitKindAssociation(
                                        ReferenceObjectForEquatableTestScenarios.ReportingPeriod1,
                                        A.Dummy<UnitKindAssociation>().Whose(_ => !_.ReportingPeriod2.IsEqualTo(ReferenceObjectForEquatableTestScenarios.ReportingPeriod2) && (_.ReportingPeriod2.GetUnitOfTimeKind() != ReferenceObjectForEquatableTestScenarios.ReportingPeriod1.GetUnitOfTimeKind())).ReportingPeriod2,
                                        ReferenceObjectForEquatableTestScenarios.Id),
                                new UnitKindAssociation(
                                        ReferenceObjectForEquatableTestScenarios.ReportingPeriod1,
                                        ReferenceObjectForEquatableTestScenarios.ReportingPeriod2,
                                        A.Dummy<UnitKindAssociation>().Whose(_ => !_.Id.IsEqualTo(ReferenceObjectForEquatableTestScenarios.Id)).Id),
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

            DeepCloneWithTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<UnitKindAssociation>
                    {
                        Name = "DeepCloneWithReportingPeriod1 should deep clone object and replace ReportingPeriod1 with the provided reportingPeriod1",
                        WithPropertyName = "ReportingPeriod1",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<UnitKindAssociation>();

                            var referenceObject = A.Dummy<UnitKindAssociation>().ThatIs(_ => (!systemUnderTest.ReportingPeriod1.IsEqualTo(_.ReportingPeriod1)) && (_.ReportingPeriod1.GetUnitOfTimeKind() != systemUnderTest.ReportingPeriod2.GetUnitOfTimeKind()));

                            var result = new SystemUnderTestDeepCloneWithValue<UnitKindAssociation>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = referenceObject.ReportingPeriod1,
                            };

                            return result;
                        },
                    })
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<UnitKindAssociation>
                    {
                        Name = "DeepCloneWithReportingPeriod2 should deep clone object and replace ReportingPeriod2 with the provided reportingPeriod2",
                        WithPropertyName = "ReportingPeriod2",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<UnitKindAssociation>();

                            var referenceObject = A.Dummy<UnitKindAssociation>().ThatIs(_ => (!systemUnderTest.ReportingPeriod2.IsEqualTo(_.ReportingPeriod2)) && (_.ReportingPeriod2.GetUnitOfTimeKind() != systemUnderTest.ReportingPeriod1.GetUnitOfTimeKind()));

                            var result = new SystemUnderTestDeepCloneWithValue<UnitKindAssociation>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = referenceObject.ReportingPeriod2,
                            };

                            return result;
                        },
                    })
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<UnitKindAssociation>
                    {
                        Name = "DeepCloneWithId should deep clone object and replace Id with the provided id",
                        WithPropertyName = "Id",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<UnitKindAssociation>();

                            var referenceObject = A.Dummy<UnitKindAssociation>().ThatIs(_ => !systemUnderTest.Id.IsEqualTo(_.Id));

                            var result = new SystemUnderTestDeepCloneWithValue<UnitKindAssociation>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = referenceObject.Id,
                            };

                            return result;
                        },
                    });
        }
    }
}