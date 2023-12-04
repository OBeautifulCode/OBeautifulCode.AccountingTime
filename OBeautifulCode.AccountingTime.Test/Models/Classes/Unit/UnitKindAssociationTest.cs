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
        }
    }
}