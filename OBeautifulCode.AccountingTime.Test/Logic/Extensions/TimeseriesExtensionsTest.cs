// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TimeseriesExtensionsTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Test
{
    using System;
    using System.Linq;

    using FakeItEasy;

    using OBeautifulCode.Assertion.Recipes;

    using Xunit;

    public static class TimeseriesExtensionsTest
    {
        [Fact]
        public static void GetMatchingDatapoints___Should_throw_ArgumentNullException___When_parameter_timeseries_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TimeseriesExtensions.GetMatchingDatapoints<Version>(null, A.Dummy<ReportingPeriod>()));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("timeseries");
        }

        [Fact]
        public static void GetMatchingDatapoints___Should_throw_ArgumentNullException___When_parameter_reportingPeriod_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => A.Dummy<Timeseries<Version>>().GetMatchingDatapoints(null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("reportingPeriod");
        }

        [Fact]
        public static void GetMatchingDatapoints___Should_throw_NotSupportedException___When_parameter_reportingPeriodComparison_is_Unknown()
        {
            // Arrange, Act
            var actual = Record.Exception(() => A.Dummy<Timeseries<Version>>().GetMatchingDatapoints(A.Dummy<ReportingPeriod>(), ReportingPeriodComparison.Unknown));

            // Assert
            actual.AsTest().Must().BeOfType<NotSupportedException>();
            actual.Message.AsTest().Must().ContainString("This ReportingPeriodComparison is not supported");
        }

        [Fact]
        public static void GetMatchingDatapoints___Should_return_empty_collection___When_timeseries_is_empty()
        {
            // Arrange
            var timeseries = new Timeseries<Version>(new Datapoint<Version>[0]);

            // Act
            var actual1 = timeseries.GetMatchingDatapoints(A.Dummy<ReportingPeriod>(), ReportingPeriodComparison.Contains);
            var actual2 = timeseries.GetMatchingDatapoints(A.Dummy<ReportingPeriod>(), ReportingPeriodComparison.IsEqualToIgnoringGranularity);

            // Assert
            actual1.AsTest().Must().BeEmptyEnumerable();
            actual2.AsTest().Must().BeEmptyEnumerable();
        }

        [Fact]
        public static void GetMatchingDatapoints___Should_return_empty_collection___When_there_are_no_matching_datapoints_regardless_of_reportingPeriodComparison()
        {
            // Arrange
            var datapointReportingPeriods = new[]
            {
                new ReportingPeriod(new CalendarUnbounded(), new CalendarYear(2018)),
                new CalendarYear(2020).ToReportingPeriod(),
                new CalendarYear(2021).ToReportingPeriod(),
                new CalendarYear(2023).ToReportingPeriod(),
                new ReportingPeriod(new CalendarYear(2024), new CalendarUnbounded()),
            };

            var datapoints = datapointReportingPeriods.Select(_ => new Datapoint<Version>(_, A.Dummy<Version>())).ToList();

            var timeseries = new Timeseries<Version>(datapoints);

            var reportingPeriods = new[]
            {
                new FiscalYear(2020).ToReportingPeriod(),
                new CalendarYear(2019).ToReportingPeriod(),
                new CalendarYear(2022).ToReportingPeriod(),
            };

            // Act
            var actual1 = reportingPeriods.Select(_ => timeseries.GetMatchingDatapoints(_, ReportingPeriodComparison.Contains));
            var actual2 = reportingPeriods.Select(_ => timeseries.GetMatchingDatapoints(_, ReportingPeriodComparison.IsEqualToIgnoringGranularity));

            // Assert
            actual1.AsTest().Must().Each().BeEmptyEnumerable();
            actual2.AsTest().Must().Each().BeEmptyEnumerable();
        }

        [Fact]
        public static void GetMatchingDatapoints___Should_return_empty_collection___When_reportingPeriodComparison_is_set_to_IsEqualToIgnoringGranularity_but_only_containing_or_overlapping_datapoints_exist()
        {
            // Arrange
            var datapointReportingPeriods = new[]
            {
                new ReportingPeriod(new CalendarUnbounded(), new CalendarYear(2018)),
                new CalendarYear(2020).ToReportingPeriod(),
                new CalendarYear(2021).ToReportingPeriod(),
                new CalendarYear(2023).ToReportingPeriod(),
                new ReportingPeriod(new CalendarYear(2024), new CalendarUnbounded()),
            };

            var datapoints = datapointReportingPeriods.Select(_ => new Datapoint<Version>(_, A.Dummy<Version>())).ToList();

            var timeseries = new Timeseries<Version>(datapoints);

            var reportingPeriods = new[]
            {
                new CalendarYear(2018).ToReportingPeriod(),
                new ReportingPeriod(new CalendarYear(2020), new CalendarYear(2021)),
                new ReportingPeriod(new CalendarYear(2019), new CalendarYear(2020)),
                new ReportingPeriod(new CalendarQuarter(2023, QuarterNumber.Q1), new CalendarQuarter(2023, QuarterNumber.Q3)),
            };

            // Act
            var actual = reportingPeriods.Select(_ => timeseries.GetMatchingDatapoints(_, ReportingPeriodComparison.IsEqualToIgnoringGranularity));

            // Assert
            actual.AsTest().Must().Each().BeEmptyEnumerable();
        }

        [Fact]
        public static void GetMatchingDatapoints___Should_return_single_matching_datapoint___When_reportingPeriodComparison_is_set_to_IsEqualToIgnoringGranularity()
        {
            // Arrange
            var datapointReportingPeriods = new[]
            {
                new ReportingPeriod(new CalendarUnbounded(), new CalendarYear(2018)),
                new CalendarYear(2020).ToReportingPeriod(),
                new CalendarYear(2021).ToReportingPeriod(),
                new CalendarYear(2023).ToReportingPeriod(),
                new ReportingPeriod(new CalendarYear(2024), new CalendarUnbounded()),
            };

            var mostGranularDatapointReportingPeriods = datapointReportingPeriods.Select(_ => _.ToMostGranular());

            var datapoints = datapointReportingPeriods.Select(_ => new Datapoint<Version>(_, A.Dummy<Version>())).ToList();

            var timeseries = new Timeseries<Version>(datapoints);

            // Act
            var actual1 = datapointReportingPeriods.Select(_ => timeseries.GetMatchingDatapoints(_.DeepClone(), ReportingPeriodComparison.IsEqualToIgnoringGranularity).Single()).ToList();
            var actual2 = mostGranularDatapointReportingPeriods.Select(_ => timeseries.GetMatchingDatapoints(_.DeepClone(), ReportingPeriodComparison.IsEqualToIgnoringGranularity).Single()).ToList();

            // Assert
            actual1.AsTest().Must().BeEqualTo(datapoints);
            actual2.AsTest().Must().BeEqualTo(datapoints);
        }

        [Fact]
        public static void GetMatchingDatapoints___Should_return_empty_collection___When_reportingPeriodComparison_is_set_to_Contains_but_only_contained_or_overlapping_datapoints_exist()
        {
            // Arrange
            var datapointReportingPeriods = new[]
            {
                new ReportingPeriod(new CalendarUnbounded(), new CalendarYear(2018)),
                new CalendarYear(2020).ToReportingPeriod(),
                new CalendarYear(2021).ToReportingPeriod(),
                new CalendarYear(2023).ToReportingPeriod(),
                new ReportingPeriod(new CalendarYear(2024), new CalendarUnbounded()),
            };

            var datapoints = datapointReportingPeriods.Select(_ => new Datapoint<Version>(_, A.Dummy<Version>())).ToList();

            var timeseries = new Timeseries<Version>(datapoints);

            var reportingPeriods = new[]
            {
                new ReportingPeriod(new CalendarYear(2018), new CalendarYear(2019)),
                new ReportingPeriod(new CalendarYear(2020), new CalendarYear(2021)),
                new ReportingPeriod(new CalendarYear(2021), new CalendarYear(2022)),
                new ReportingPeriod(new CalendarYear(2023), new CalendarYear(2025)),
            };

            // Act
            var actual = reportingPeriods.Select(_ => timeseries.GetMatchingDatapoints(_, ReportingPeriodComparison.Contains));

            // Assert
            actual.AsTest().Must().Each().BeEmptyEnumerable();
        }

        [Fact]
        public static void GetMatchingDatapoints___Should_return_single_matching_datapoint___When_reportingPeriodComparison_is_set_to_Contains()
        {
            // Arrange
            var datapointReportingPeriods = new[]
            {
                new ReportingPeriod(new CalendarUnbounded(), new CalendarYear(2018)),
                new CalendarYear(2020).ToReportingPeriod(),
                new CalendarYear(2021).ToReportingPeriod(),
                new CalendarYear(2023).ToReportingPeriod(),
                new ReportingPeriod(new CalendarYear(2024), new CalendarUnbounded()),
            };

            var datapoints = datapointReportingPeriods.Select(_ => new Datapoint<Version>(_, A.Dummy<Version>())).ToList();

            var timeseries = new Timeseries<Version>(datapoints);

            var reportingPeriods = new[]
            {
                new ReportingPeriod(new CalendarYear(2016), new CalendarYear(2018)),
                new ReportingPeriod(new CalendarYear(2020), new CalendarYear(2020)),
                new ReportingPeriod(new CalendarQuarter(2021, QuarterNumber.Q2), new CalendarQuarter(2021, QuarterNumber.Q3)),
                new ReportingPeriod(new CalendarQuarter(2023, QuarterNumber.Q1), new CalendarQuarter(2023, QuarterNumber.Q4)),
                new ReportingPeriod(new CalendarYear(2025), new CalendarYear(2028)),
            };

            var mostGranularDatapointReportingPeriods = reportingPeriods.Select(_ => _.ToMostGranular());

            // Act
            var actual1 = reportingPeriods.Select(_ => timeseries.GetMatchingDatapoints(_.DeepClone(), ReportingPeriodComparison.Contains).Single()).ToList();
            var actual2 = mostGranularDatapointReportingPeriods.Select(_ => timeseries.GetMatchingDatapoints(_.DeepClone(), ReportingPeriodComparison.Contains).Single()).ToList();

            // Assert
            actual1.AsTest().Must().BeEqualTo(datapoints);
            actual2.AsTest().Must().BeEqualTo(datapoints);
        }
    }
}