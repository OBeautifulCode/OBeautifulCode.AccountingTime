// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriodCriteriaTest.cs" company="OBeautifulCode">
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
    public static partial class ReportingPeriodCriteriaTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static ReportingPeriodCriteriaTest()
        {
            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<ReportingPeriodCriteria>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'boundsConstraint' is Unknown",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<ReportingPeriodCriteria>();

                            var result = new ReportingPeriodCriteria(
                                referenceObject.Unit,
                                ReportingPeriodBoundsConstraint.Unknown,
                                referenceObject.SpanConstraint);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "boundsConstraint", "Unknown", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<ReportingPeriodCriteria>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'spanConstraint' is Unknown",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<ReportingPeriodCriteria>();

                            var result = new ReportingPeriodCriteria(
                                referenceObject.Unit,
                                referenceObject.BoundsConstraint,
                                ReportingPeriodSpanConstraint.Unknown);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "spanConstraint", "Unknown", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<ReportingPeriodCriteria>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'unit' has Unbounded granularity and parameter 'boundsConstraint' calls for one or more components to be bounded",
                        ConstructionFunc = () =>
                        {
                            var result = new ReportingPeriodCriteria(
                                A.Dummy<Unit>().Whose(_ => _.Granularity == UnitOfTimeGranularity.Unbounded),
                                A.Dummy<ReportingPeriodBoundsConstraint>().ThatIsIn(new[]
                                {
                                    ReportingPeriodBoundsConstraint.MustBeFullyBounded,
                                    ReportingPeriodBoundsConstraint.MustHaveBoundedComponent,
                                    ReportingPeriodBoundsConstraint.MustHaveBoundedStart,
                                    ReportingPeriodBoundsConstraint.MustHaveBoundedEnd,
                                    ReportingPeriodBoundsConstraint.MustHaveBoundedStartAndUnboundedEnd,
                                    ReportingPeriodBoundsConstraint.MustHaveUnboundedStartAndBoundedEnd,
                                }),
                                A.Dummy<ReportingPeriodSpanConstraint>());

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "resulting in a constraint that is logically impossible to meet" },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<ReportingPeriodCriteria>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'unit' has bounded granularity and parameter 'boundsConstraint' calls for reporting period to be fully unbounded",
                        ConstructionFunc = () =>
                        {
                            var result = new ReportingPeriodCriteria(
                                A.Dummy<Unit>().Whose(_ => _.Granularity != UnitOfTimeGranularity.Unbounded),
                                A.Dummy<ReportingPeriodBoundsConstraint>().ThatIsIn(new[]
                                {
                                    ReportingPeriodBoundsConstraint.MustBeFullyUnbounded,
                                }),
                                A.Dummy<ReportingPeriodSpanConstraint>());

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "resulting in a constraint that is logically impossible to meet" },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<ReportingPeriodCriteria>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'unit' has Unbounded granularity and parameter 'spanConstraint' is MustHaveDifferentStartAndEnd.",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<ReportingPeriodCriteria>()
                                .Whose(_ => (_.Unit != null) && (_.Unit.Granularity == UnitOfTimeGranularity.Unbounded));

                            var result = new ReportingPeriodCriteria(
                                referenceObject.Unit,
                                referenceObject.BoundsConstraint,
                                ReportingPeriodSpanConstraint.MustHaveDifferentStartAndEnd);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "resulting in a constraint that is logically impossible to meet" },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<ReportingPeriodCriteria>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'spanConstraint' is MustHaveDifferentStartAndEnd and 'boundsConstraint' is MustBeFullyUnbounded.",
                        ConstructionFunc = () =>
                        {
                            var result = new ReportingPeriodCriteria(
                                null,
                                ReportingPeriodBoundsConstraint.MustBeFullyUnbounded,
                                ReportingPeriodSpanConstraint.MustHaveDifferentStartAndEnd);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "resulting in a constraint that is logically impossible to meet" },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<ReportingPeriodCriteria>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'spanConstraint' is MustHaveSameStartAndEnd and 'boundsConstraint' is MustHaveUnboundedStartAndBoundedEnd or MustHaveBoundedStartAndUnboundedEnd.",
                        ConstructionFunc = () =>
                        {
                            var result = new ReportingPeriodCriteria(
                                null,
                                A.Dummy<ReportingPeriodBoundsConstraint>().ThatIsIn(new[] { ReportingPeriodBoundsConstraint.MustHaveBoundedStartAndUnboundedEnd, ReportingPeriodBoundsConstraint.MustHaveUnboundedStartAndBoundedEnd }),
                                ReportingPeriodSpanConstraint.MustHaveSameStartAndEnd);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "resulting in a constraint that is logically impossible to meet" },
                    });

            EquatableTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                {
                    ReportingPeriodCriteria notEqualScenario1;

                    while (true)
                    {
                        try
                        {
                            notEqualScenario1 = new ReportingPeriodCriteria(
                                A.Dummy<ReportingPeriodCriteria>().Whose(_ =>
                                    !_.Unit.IsEqualTo(ReferenceObjectForEquatableTestScenarios.Unit)).Unit,
                                ReferenceObjectForEquatableTestScenarios.BoundsConstraint,
                                ReferenceObjectForEquatableTestScenarios.SpanConstraint);

                            break;
                        }
                        catch (ArgumentException)
                        {
                        }
                    }

                    ReportingPeriodCriteria notEqualScenario2;

                    while (true)
                    {
                        try
                        {
                            notEqualScenario2 = new ReportingPeriodCriteria(
                                ReferenceObjectForEquatableTestScenarios.Unit,
                                A.Dummy<ReportingPeriodCriteria>().Whose(_ => !_.BoundsConstraint.IsEqualTo(ReferenceObjectForEquatableTestScenarios.BoundsConstraint)).BoundsConstraint,
                                ReferenceObjectForEquatableTestScenarios.SpanConstraint);

                            break;
                        }
                        catch (ArgumentException)
                        {
                        }
                    }

                    ReportingPeriodCriteria notEqualScenario3;

                    while (true)
                    {
                        try
                        {
                            notEqualScenario3 = new ReportingPeriodCriteria(
                                ReferenceObjectForEquatableTestScenarios.Unit,
                                ReferenceObjectForEquatableTestScenarios.BoundsConstraint,
                                A.Dummy<ReportingPeriodCriteria>().Whose(_ => !_.SpanConstraint.IsEqualTo(ReferenceObjectForEquatableTestScenarios.SpanConstraint)).SpanConstraint);

                            break;
                        }
                        catch (ArgumentException)
                        {
                        }
                    }

                    return new EquatableTestScenario<ReportingPeriodCriteria>
                    {
                        Name = "Default Code Generated Scenario",
                        ReferenceObject = ReferenceObjectForEquatableTestScenarios,
                        ObjectsThatAreEqualToButNotTheSameAsReferenceObject = new[]
                        {
                                new ReportingPeriodCriteria(
                                        ReferenceObjectForEquatableTestScenarios.Unit,
                                        ReferenceObjectForEquatableTestScenarios.BoundsConstraint,
                                        ReferenceObjectForEquatableTestScenarios.SpanConstraint),
                        },
                        ObjectsThatAreNotEqualToReferenceObject = new[]
                        {
                                notEqualScenario1,
                                notEqualScenario2,
                                notEqualScenario3,
                        },
                        ObjectsThatAreNotOfTheSameTypeAsReferenceObject = new[]
                        {
                                A.Dummy<object>(),
                                A.Dummy<string>(),
                                A.Dummy<int>(),
                                A.Dummy<int?>(),
                                A.Dummy<Guid>(),
                        },
                    };
                });
        }
    }
}