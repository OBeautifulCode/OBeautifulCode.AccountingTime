// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriodTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Test
{
    using System;
    using System.Linq;
    using FakeItEasy;

    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.AutoFakeItEasy;
    using OBeautifulCode.CodeGen.ModelObject.Recipes;

    using Xunit;

    using static System.FormattableString;

    public static partial class ReportingPeriodTest
    {
        static ReportingPeriodTest()
        {
            DeepCloneWithTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<ReportingPeriod>
                    {
                        Name = Invariant($"{nameof(ReportingPeriod.DeepCloneWithStart)} should deep clone object and replace {nameof(ReportingPeriod.Start)} with the provided start when {nameof(ReportingPeriod.End)} is {nameof(UnitOfTimeGranularity.Unbounded)} and provided start has the same {nameof(UnitOfTimeKind)} as {nameof(ReportingPeriod.End)}"),
                        WithPropertyName = nameof(ReportingPeriod.Start),
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<ReportingPeriod>().Whose(_ => _.End.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded);

                            var referenceObject = A.Dummy<ReportingPeriod>().Whose(_ => _.GetUnitOfTimeKind() == systemUnderTest.GetUnitOfTimeKind());

                            var result = new SystemUnderTestDeepCloneWithValue<ReportingPeriod>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = referenceObject.Start,
                            };

                            return result;
                        },
                    })
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<ReportingPeriod>
                    {
                        Name = Invariant($"{nameof(ReportingPeriod.DeepCloneWithStart)} should deep clone object and replace {nameof(ReportingPeriod.Start)} with the provided start when {nameof(ReportingPeriod.End)} is bounded and provided start is {nameof(UnitOfTimeGranularity.Unbounded)} having the same {nameof(UnitOfTimeKind)}"),
                        WithPropertyName = nameof(ReportingPeriod.Start),
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<ReportingPeriod>().Whose(_ => _.End.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded);

                            var referenceObject = A.Dummy<ReportingPeriod>().Whose(_ => (_.Start.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded) && (_.Start.UnitOfTimeKind == systemUnderTest.End.UnitOfTimeKind));

                            var result = new SystemUnderTestDeepCloneWithValue<ReportingPeriod>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = referenceObject.Start,
                            };

                            return result;
                        },
                    })
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<ReportingPeriod>
                    {
                        Name = Invariant($"{nameof(ReportingPeriod.DeepCloneWithStart)} should deep clone object and replace {nameof(ReportingPeriod.Start)} with the provided start when {nameof(ReportingPeriod.End)} is bounded and provided start is of the same type as and <= {nameof(ReportingPeriod.End)}"),
                        WithPropertyName = nameof(ReportingPeriod.Start),
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<ReportingPeriod>().Whose(_ => _.End.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded);

                            var referenceObject = A.Dummy<ReportingPeriod>().Whose(_ => (_.Start.GetType() == systemUnderTest.End.GetType()) && (_.Start <= systemUnderTest.End));

                            var result = new SystemUnderTestDeepCloneWithValue<ReportingPeriod>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = referenceObject.Start,
                            };

                            return result;
                        },
                    })
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<ReportingPeriod>
                    {
                        Name = Invariant($"{nameof(ReportingPeriod.DeepCloneWithEnd)} should deep clone object and replace {nameof(ReportingPeriod.End)} with the provided end when {nameof(ReportingPeriod.Start)} is {nameof(UnitOfTimeGranularity.Unbounded)} and provided end has the same {nameof(UnitOfTimeKind)} as {nameof(ReportingPeriod.Start)}"),
                        WithPropertyName = nameof(ReportingPeriod.End),
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<ReportingPeriod>().Whose(_ => _.Start.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded);

                            var referenceObject = A.Dummy<ReportingPeriod>().Whose(_ => _.GetUnitOfTimeKind() == systemUnderTest.GetUnitOfTimeKind());

                            var result = new SystemUnderTestDeepCloneWithValue<ReportingPeriod>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = referenceObject.End,
                            };

                            return result;
                        },
                    })
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<ReportingPeriod>
                    {
                        Name = Invariant($"{nameof(ReportingPeriod.DeepCloneWithEnd)} should deep clone object and replace {nameof(ReportingPeriod.End)} with the provided end when {nameof(ReportingPeriod.Start)} is bounded and provided end is {nameof(UnitOfTimeGranularity.Unbounded)} having the same {nameof(UnitOfTimeKind)}"),
                        WithPropertyName = nameof(ReportingPeriod.End),
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<ReportingPeriod>().Whose(_ => _.Start.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded);

                            var referenceObject = A.Dummy<ReportingPeriod>().Whose(_ => (_.End.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded) && (_.End.UnitOfTimeKind == systemUnderTest.Start.UnitOfTimeKind));

                            var result = new SystemUnderTestDeepCloneWithValue<ReportingPeriod>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = referenceObject.End,
                            };

                            return result;
                        },
                    })
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<ReportingPeriod>
                    {
                        Name = Invariant($"{nameof(ReportingPeriod.DeepCloneWithEnd)} should deep clone object and replace {nameof(ReportingPeriod.End)} with the provided end when {nameof(ReportingPeriod.Start)} is bounded and provided end is of the same type as and >= {nameof(ReportingPeriod.Start)}"),
                        WithPropertyName = nameof(ReportingPeriod.End),
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<ReportingPeriod>().Whose(_ => _.Start.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded);

                            var referenceObject = A.Dummy<ReportingPeriod>().Whose(_ => (_.End.GetType() == systemUnderTest.Start.GetType()) && (_.End >= systemUnderTest.Start));

                            var result = new SystemUnderTestDeepCloneWithValue<ReportingPeriod>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = referenceObject.End,
                            };

                            return result;
                        },
                    });

            var unboundedStartReportingPeriod = A.Dummy<ReportingPeriod>().Whose(_ => _.Start.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded);
            var boundedStartReportingPeriod = A.Dummy<ReportingPeriod>().Whose(_ => _.Start.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded);
            var unboundedEndReportingPeriod = A.Dummy<ReportingPeriod>().Whose(_ => _.End.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded);
            var boundedEndReportingPeriod = A.Dummy<ReportingPeriod>().Whose(_ => _.End.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded);

            EquatableTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new EquatableTestScenario<ReportingPeriod>
                    {
                        Name = "Reference object has unbounded Start.  Create an End of the same UnitOfTimeKind that != reference object's End",
                        ReferenceObject = unboundedStartReportingPeriod,
                        ObjectsThatAreEqualToButNotTheSameAsReferenceObject = new[]
                        {
                            unboundedStartReportingPeriod.DeepClone(),
                        },
                        ObjectsThatAreNotEqualToReferenceObject = new[]
                        {
                            new ReportingPeriod(unboundedStartReportingPeriod.Start, A.Dummy<UnitOfTime>().Whose(_ => (_.UnitOfTimeKind == unboundedStartReportingPeriod.GetUnitOfTimeKind()) && (_ != unboundedStartReportingPeriod.End))),
                        },
                        ObjectsThatAreNotOfTheSameTypeAsReferenceObject =
                            new[]
                            {
                                A.Dummy<object>(),
                                A.Dummy<string>(),
                                A.Dummy<int>(),
                                A.Dummy<int?>(),
                                A.Dummy<Guid>(),
                            }
                            .Concat(Common.GetDummyOfEachUnitOfTimeKind())
                            .ToArray(),
                    })
                .AddScenario(() =>
                    new EquatableTestScenario<ReportingPeriod>
                    {
                        Name = "Reference object has bounded Start.  Create an End that is of the same type as reference object's Start (so it will be bounded), that is > but != reference object's Start",
                        ReferenceObject = boundedStartReportingPeriod,
                        ObjectsThatAreEqualToButNotTheSameAsReferenceObject = new[]
                        {
                            boundedStartReportingPeriod.DeepClone(),
                        },
                        ObjectsThatAreNotEqualToReferenceObject = new[]
                        {
                            new ReportingPeriod(boundedStartReportingPeriod.Start, A.Dummy<UnitOfTime>().Whose(_ => (_.GetType() == boundedStartReportingPeriod.Start.GetType()) && (_ > boundedStartReportingPeriod.Start) && (_ != boundedStartReportingPeriod.End))),
                        },
                        ObjectsThatAreNotOfTheSameTypeAsReferenceObject =
                            new[]
                                {
                                    A.Dummy<object>(),
                                    A.Dummy<string>(),
                                    A.Dummy<int>(),
                                    A.Dummy<int?>(),
                                    A.Dummy<Guid>(),
                                }
                                .Concat(Common.GetDummyOfEachUnitOfTimeKind())
                                .ToArray(),
                    })
                .AddScenario(() =>
                    new EquatableTestScenario<ReportingPeriod>
                    {
                        Name = "Reference object has unbounded End.  Create an Start of the same UnitOfTimeKind that != reference object's Start",
                        ReferenceObject = unboundedEndReportingPeriod,
                        ObjectsThatAreEqualToButNotTheSameAsReferenceObject = new[]
                        {
                            unboundedEndReportingPeriod.DeepClone(),
                        },
                        ObjectsThatAreNotEqualToReferenceObject = new[]
                        {
                            new ReportingPeriod(A.Dummy<UnitOfTime>().Whose(_ => (_.UnitOfTimeKind == unboundedEndReportingPeriod.GetUnitOfTimeKind()) && (_ != unboundedEndReportingPeriod.Start)), unboundedEndReportingPeriod.End),
                        },
                        ObjectsThatAreNotOfTheSameTypeAsReferenceObject =
                            new[]
                                {
                                    A.Dummy<object>(),
                                    A.Dummy<string>(),
                                    A.Dummy<int>(),
                                    A.Dummy<int?>(),
                                    A.Dummy<Guid>(),
                                }
                                .Concat(Common.GetDummyOfEachUnitOfTimeKind())
                                .ToArray(),
                    })
                .AddScenario(() =>
                    new EquatableTestScenario<ReportingPeriod>
                    {
                        Name = "Reference object has bounded End.  Create a Start that is of the same type as reference object's End (so it will be bounded), that is < but != reference object's End",
                        ReferenceObject = boundedEndReportingPeriod,
                        ObjectsThatAreEqualToButNotTheSameAsReferenceObject = new[]
                        {
                            boundedEndReportingPeriod.DeepClone(),
                        },
                        ObjectsThatAreNotEqualToReferenceObject = new[]
                        {
                            new ReportingPeriod(A.Dummy<UnitOfTime>().Whose(_ => (_.GetType() == boundedEndReportingPeriod.End.GetType()) && (_ < boundedEndReportingPeriod.End) && (_ != boundedEndReportingPeriod.Start)), boundedEndReportingPeriod.End),
                        },
                        ObjectsThatAreNotOfTheSameTypeAsReferenceObject =
                            new[]
                                {
                                    A.Dummy<object>(),
                                    A.Dummy<string>(),
                                    A.Dummy<int>(),
                                    A.Dummy<int?>(),
                                    A.Dummy<Guid>(),
                                }
                                .Concat(Common.GetDummyOfEachUnitOfTimeKind())
                                .ToArray(),
                    });

            StringRepresentationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new StringRepresentationTestScenario<ReportingPeriod>
                    {
                        Name = "String Representation - case 1",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                            new SystemUnderTestExpectedStringRepresentation<ReportingPeriod>
                            {
                                SystemUnderTest = new ReportingPeriod(
                                    new CalendarDay(2017, MonthOfYear.November, DayOfMonth.Thirty),
                                    new CalendarDay(2018, MonthOfYear.March, DayOfMonth.TwentyFour)),
                                ExpectedStringRepresentation = "2017-11-30 to 2018-03-24",
                            },
                    });
        }

        [Fact]
        public static void Constructor___Should_throw_ArgumentException___When_parameters_start_and_end_bounded_and_not_of_the_same_concrete_type()
        {
            // Arrange
            var start1 = A.Dummy<CalendarQuarter>();
            var end1 = A.Dummy<CalendarDay>();

            var start2 = A.Dummy<FiscalQuarter>();
            var end2 = A.Dummy<CalendarQuarter>();

            // Act
            var ex1 = Record.Exception(() => new ReportingPeriod(start1, end1));
            var ex2 = Record.Exception(() => new ReportingPeriod(start2, end2));

            // Assert
            ex1.AsTest().Must().BeOfType<ArgumentException>();
            ex2.AsTest().Must().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void Constructor___Should_throw_ArgumentOutOfRangeException___When_parameters_start_and_end_are_bounded_and_start_is_greater_than_parameter_end()
        {
            // Arrange
            var start = A.Dummy<CalendarQuarter>();
            var end = A.Dummy<CalendarQuarter>().ThatIs(q => q < start);

            // Act
            var ex = Record.Exception(() => new ReportingPeriod(start, end));

            // Assert
            ex.AsTest().Must().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void Constructor___Should_throw_ArgumentException___When_parameters_start_and_or_end_is_unbounded_and_not_the_same_kind_of_unit_of_time()
        {
            // Arrange
            var start1 = A.Dummy<FiscalUnbounded>();
            var end1 = A.Dummy<CalendarYear>();

            var start2 = A.Dummy<FiscalMonth>();
            var end2 = A.Dummy<GenericUnbounded>();

            var start3 = A.Dummy<FiscalUnbounded>();
            var end3 = A.Dummy<GenericUnbounded>();

            var start4 = A.Dummy<GenericUnbounded>();
            var end4 = A.Dummy<CalendarUnbounded>();

            // Act
            var ex1 = Record.Exception(() => new ReportingPeriod(start1, end1));
            var ex2 = Record.Exception(() => new ReportingPeriod(start2, end2));
            var ex3 = Record.Exception(() => new ReportingPeriod(start3, end3));
            var ex4 = Record.Exception(() => new ReportingPeriod(start4, end4));

            // Assert
            ex1.AsTest().Must().BeOfType<ArgumentException>();
            ex2.AsTest().Must().BeOfType<ArgumentException>();
            ex3.AsTest().Must().BeOfType<ArgumentException>();
            ex4.AsTest().Must().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void Constructor___Should_not_throw___When_parameters_start_and_end_are_bounded_and_start_is_equal_to_parameter_end()
        {
            // Arrange
            var start = (UnitOfTime)A.Dummy<IAmBoundedTime>();

            // Act
            var ex = Record.Exception(() => new ReportingPeriod(start, start));

            // Assert
            ex.AsTest().Must().BeNull();
        }

        [Fact]
        public static void Constructor___Should_not_throw___When_parameters_start_and_or_end_are_unbounded_and_are_the_same_kind_of_unit_of_time()
        {
            // Arrange
            var start1 = A.Dummy<CalendarUnbounded>();
            var end1 = A.Dummy<CalendarMonth>();

            var start2 = A.Dummy<FiscalYear>();
            var end2 = A.Dummy<FiscalUnbounded>();

            var start3 = A.Dummy<GenericUnbounded>();
            var end3 = A.Dummy<GenericUnbounded>();

            // Act
            var ex1 = Record.Exception(() => new ReportingPeriod(start1, end1));
            var ex2 = Record.Exception(() => new ReportingPeriod(start2, end2));
            var ex3 = Record.Exception(() => new ReportingPeriod(start3, end3));

            // Assert
            ex1.AsTest().Must().BeNull();
            ex2.AsTest().Must().BeNull();
            ex3.AsTest().Must().BeNull();
        }
    }
}
