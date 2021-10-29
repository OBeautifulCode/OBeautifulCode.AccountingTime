// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Timeseries{T}Test.cs" company="OBeautifulCode">
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
    public static partial class TimeseriesTTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static TimeseriesTTest()
        {
            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<Timeseries<Version>>
                    {
                        Name = "constructor should throw ArgumentNullException when parameter 'datapoints' is null scenario",
                        ConstructionFunc = () =>
                        {
                            var result = new Timeseries<Version>(
                                                 null);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentNullException),
                        ExpectedExceptionMessageContains = new[] { "datapoints", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<Timeseries<Version>>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'datapoints' contains a null element scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<Timeseries<Version>>();

                            var result = new Timeseries<Version>(
                                                 new Datapoint<Version>[0].Concat(referenceObject.Datapoints).Concat(new Datapoint<Version>[] { null }).Concat(referenceObject.Datapoints).ToList());

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "datapoints", "contains at least one null element", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<Timeseries<Version>>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'datapoints' contains reporting periods of different kinds",
                        ConstructionFunc = () =>
                        {
                            var datapoints = new[]
                            {
                                new Datapoint<Version>(QuarterNumber.Q1.ToCalendar(2020).ToReportingPeriod(), A.Dummy<Version>()),
                                new Datapoint<Version>(QuarterNumber.Q2.ToFiscal(2020).ToReportingPeriod(), A.Dummy<Version>()),
                            };

                            var result = new Timeseries<Version>(datapoints);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "contains reporting periods of different UnitOfTimeKinds", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<Timeseries<Version>>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'datapoints' contains reporting periods that are not in order scenario 1",
                        ConstructionFunc = () =>
                        {
                            var datapoints = new[]
                            {
                                new Datapoint<Version>(QuarterNumber.Q1.ToCalendar(2020).ToReportingPeriod(), A.Dummy<Version>()),
                                new Datapoint<Version>(QuarterNumber.Q2.ToCalendar(2020).ToReportingPeriod(), A.Dummy<Version>()),
                                new Datapoint<Version>(QuarterNumber.Q4.ToCalendar(2020).ToReportingPeriod(), A.Dummy<Version>()),
                                new Datapoint<Version>(QuarterNumber.Q3.ToCalendar(2020).ToReportingPeriod(), A.Dummy<Version>()),
                            };

                            var result = new Timeseries<Version>(datapoints);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "datapoints are not in order and/or contain overlaps", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<Timeseries<Version>>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'datapoints' contains reporting periods that are not in order scenario 2",
                        ConstructionFunc = () =>
                        {
                            var datapoints = new[]
                            {
                                new Datapoint<Version>(QuarterNumber.Q1.ToCalendar(2020).ToReportingPeriod(), A.Dummy<Version>()),
                                new Datapoint<Version>(QuarterNumber.Q2.ToCalendar(2020).ToReportingPeriod(), A.Dummy<Version>()),
                                new Datapoint<Version>(new ReportingPeriod(new CalendarUnbounded(), QuarterNumber.Q3.ToCalendar(2020)), A.Dummy<Version>()),
                                new Datapoint<Version>(QuarterNumber.Q4.ToCalendar(2020).ToReportingPeriod(), A.Dummy<Version>()),
                            };

                            var result = new Timeseries<Version>(datapoints);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "datapoints are not in order and/or contain overlaps", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<Timeseries<Version>>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'datapoints' contains reporting periods that are not in order scenario 3",
                        ConstructionFunc = () =>
                        {
                            var datapoints = new[]
                            {
                                new Datapoint<Version>(QuarterNumber.Q1.ToCalendar(2020).ToReportingPeriod(), A.Dummy<Version>()),
                                new Datapoint<Version>(QuarterNumber.Q2.ToCalendar(2020).ToReportingPeriod(), A.Dummy<Version>()),
                                new Datapoint<Version>(new ReportingPeriod(QuarterNumber.Q3.ToCalendar(2020), new CalendarUnbounded()), A.Dummy<Version>()),
                                new Datapoint<Version>(QuarterNumber.Q4.ToCalendar(2020).ToReportingPeriod(), A.Dummy<Version>()),
                            };

                            var result = new Timeseries<Version>(datapoints);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "datapoints are not in order and/or contain overlaps", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<Timeseries<Version>>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'datapoints' contains reporting periods that overlap scenario 1",
                        ConstructionFunc = () =>
                        {
                            var datapoints = new[]
                            {
                                new Datapoint<Version>(QuarterNumber.Q1.ToCalendar(2020).ToReportingPeriod(), A.Dummy<Version>()),
                                new Datapoint<Version>(QuarterNumber.Q2.ToCalendar(2020).ToReportingPeriod(), A.Dummy<Version>()),
                                new Datapoint<Version>(new ReportingPeriod(QuarterNumber.Q3.ToCalendar(2020), new CalendarUnbounded()), A.Dummy<Version>()),
                                new Datapoint<Version>(new CalendarUnbounded().ToReportingPeriod(), A.Dummy<Version>()),
                            };

                            var result = new Timeseries<Version>(datapoints);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "datapoints are not in order and/or contain overlaps", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<Timeseries<Version>>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'datapoints' contains reporting periods that overlap scenario 1",
                        ConstructionFunc = () =>
                        {
                            var datapoints = new[]
                            {
                                new Datapoint<Version>(new ReportingPeriod(QuarterNumber.Q1.ToCalendar(2020), QuarterNumber.Q2.ToCalendar(2020)), A.Dummy<Version>()),
                                new Datapoint<Version>(new ReportingPeriod(QuarterNumber.Q2.ToCalendar(2020), QuarterNumber.Q3.ToCalendar(2020)), A.Dummy<Version>()),
                                new Datapoint<Version>(new ReportingPeriod(QuarterNumber.Q4.ToCalendar(2020), new CalendarUnbounded()), A.Dummy<Version>()),
                                new Datapoint<Version>(new ReportingPeriod(QuarterNumber.Q1.ToCalendar(2021), new CalendarUnbounded()), A.Dummy<Version>()),
                            };

                            var result = new Timeseries<Version>(datapoints);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "datapoints are not in order and/or contain overlaps", },
                    });
        }
    }
}