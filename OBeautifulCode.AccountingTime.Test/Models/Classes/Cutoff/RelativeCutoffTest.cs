// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RelativeCutoffTest.cs" company="OBeautifulCode">
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
    using OBeautifulCode.Type;

    using Xunit;

    using static System.FormattableString;

    [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
    public static partial class RelativeCutoffTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static RelativeCutoffTest()
        {
            ConstructorArgumentValidationTestScenarios
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<RelativeCutoff>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'startOrEnd' is neither ReportingPeriodComponent.Start nor ReportingPeriodComponent.End",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<RelativeCutoff>();

                            var result = new RelativeCutoff(
                                referenceObject.Duration,
                                A.Dummy<ReportingPeriodComponent>().ThatIsNotIn(new[] { ReportingPeriodComponent.Start, ReportingPeriodComponent.End }));

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "startOrEnd is neither ReportingPeriodComponent.Start nor ReportingPeriodComponent.End", },
                    });
        }
    }
}