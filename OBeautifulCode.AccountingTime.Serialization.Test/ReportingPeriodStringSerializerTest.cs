// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriodStringSerializerTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Serialization.Test
{
    using System;

    using FakeItEasy;

    using FluentAssertions;

    using Naos.Serialization.Domain;
    using Naos.Serialization.PropertyBag;

    using OBeautifulCode.AccountingTime.Serialization.PropertyBag;

    using Xunit;

    public static class ReportingPeriodStringSerializerTest
    {
        private static readonly NaosPropertyBagSerializer PropertyBagSerializer = new NaosPropertyBagSerializer(SerializationKind.Default, typeof(AccountingTimePropertyBagConfiguration));

        [Fact]
        public static void ReportingPeriodModel_without_nulls___Should_roundtrip_to_json_and_back___When_using_ReportingPeriodConverter()
        {
            // Arrange
            var expectedModel1 = A.Dummy<ReportingPeriodModel>();
            var expectedModel2 = A.Dummy<IReportingPeriodModel>();

            // Act
            var json1 = PropertyBagSerializer.SerializeToString(expectedModel1);
            var json2 = PropertyBagSerializer.SerializeToString(expectedModel2);

            var actualModel1 = PropertyBagSerializer.Deserialize<ReportingPeriodModel>(json1);
            var actualModel2 = PropertyBagSerializer.Deserialize<IReportingPeriodModel>(json2);

            // Assert
            actualModel1.UnitOfTime.Should().Be(expectedModel1.UnitOfTime);
            actualModel1.CalendarUnitOfTime.Should().Be(expectedModel1.CalendarUnitOfTime);
            actualModel1.CalendarDay.Should().Be(expectedModel1.CalendarDay);
            actualModel1.CalendarMonth.Should().Be(expectedModel1.CalendarMonth);
            actualModel1.CalendarQuarter.Should().Be(expectedModel1.CalendarQuarter);
            actualModel1.CalendarYear.Should().Be(expectedModel1.CalendarYear);
            actualModel1.CalendarUnbounded.Should().Be(expectedModel1.CalendarUnbounded);
            actualModel1.FiscalUnitOfTime.Should().Be(expectedModel1.FiscalUnitOfTime);
            actualModel1.FiscalMonth.Should().Be(expectedModel1.FiscalMonth);
            actualModel1.FiscalQuarter.Should().Be(expectedModel1.FiscalQuarter);
            actualModel1.FiscalYear.Should().Be(expectedModel1.FiscalYear);
            actualModel1.FiscalUnbounded.Should().Be(expectedModel1.FiscalUnbounded);
            actualModel1.GenericUnitOfTime.Should().Be(expectedModel1.GenericUnitOfTime);
            actualModel1.GenericMonth.Should().Be(expectedModel1.GenericMonth);
            actualModel1.GenericQuarter.Should().Be(expectedModel1.GenericQuarter);
            actualModel1.GenericYear.Should().Be(expectedModel1.GenericYear);
            actualModel1.GenericUnbounded.Should().Be(expectedModel1.GenericUnbounded);

            actualModel2.UnitOfTime.Should().Be(expectedModel2.UnitOfTime);
            actualModel2.CalendarUnitOfTime.Should().Be(expectedModel2.CalendarUnitOfTime);
            actualModel2.CalendarDay.Should().Be(expectedModel2.CalendarDay);
            actualModel2.CalendarMonth.Should().Be(expectedModel2.CalendarMonth);
            actualModel2.CalendarQuarter.Should().Be(expectedModel2.CalendarQuarter);
            actualModel2.CalendarYear.Should().Be(expectedModel2.CalendarYear);
            actualModel2.CalendarUnbounded.Should().Be(expectedModel2.CalendarUnbounded);
            actualModel2.FiscalUnitOfTime.Should().Be(expectedModel2.FiscalUnitOfTime);
            actualModel2.FiscalMonth.Should().Be(expectedModel2.FiscalMonth);
            actualModel2.FiscalQuarter.Should().Be(expectedModel2.FiscalQuarter);
            actualModel2.FiscalYear.Should().Be(expectedModel2.FiscalYear);
            actualModel2.FiscalUnbounded.Should().Be(expectedModel2.FiscalUnbounded);
            actualModel2.GenericUnitOfTime.Should().Be(expectedModel2.GenericUnitOfTime);
            actualModel2.GenericMonth.Should().Be(expectedModel2.GenericMonth);
            actualModel2.GenericQuarter.Should().Be(expectedModel2.GenericQuarter);
            actualModel2.GenericYear.Should().Be(expectedModel2.GenericYear);
            actualModel2.GenericUnbounded.Should().Be(expectedModel2.GenericUnbounded);
        }

        [Fact]
        public static void ReportingPeriodModel_with_nulls___Should_roundtrip_to_json_and_back___When_using_ReportingPeriodConverter()
        {
            // Arrange
            var expectedModel1 = new ReportingPeriodModel();
            var expectedModel2 = new IReportingPeriodModel();

            // Act
            var json1 = PropertyBagSerializer.SerializeToString(expectedModel1);
            var json2 = PropertyBagSerializer.SerializeToString(expectedModel2);

            var actualModel1 = PropertyBagSerializer.Deserialize<ReportingPeriodModel>(json1);
            var actualModel2 = PropertyBagSerializer.Deserialize<IReportingPeriodModel>(json2);

            // Assert
            actualModel1.UnitOfTime.Should().BeNull();
            actualModel1.CalendarUnitOfTime.Should().BeNull();
            actualModel1.CalendarDay.Should().BeNull();
            actualModel1.CalendarMonth.Should().BeNull();
            actualModel1.CalendarQuarter.Should().BeNull();
            actualModel1.CalendarYear.Should().BeNull();
            actualModel1.CalendarUnbounded.Should().BeNull();
            actualModel1.FiscalUnitOfTime.Should().BeNull();
            actualModel1.FiscalMonth.Should().BeNull();
            actualModel1.FiscalQuarter.Should().BeNull();
            actualModel1.FiscalYear.Should().BeNull();
            actualModel1.FiscalUnbounded.Should().BeNull();
            actualModel1.GenericUnitOfTime.Should().BeNull();
            actualModel1.GenericMonth.Should().BeNull();
            actualModel1.GenericQuarter.Should().BeNull();
            actualModel1.GenericYear.Should().BeNull();
            actualModel1.GenericUnbounded.Should().BeNull();

            actualModel2.UnitOfTime.Should().BeNull();
            actualModel2.CalendarUnitOfTime.Should().BeNull();
            actualModel2.CalendarDay.Should().BeNull();
            actualModel2.CalendarMonth.Should().BeNull();
            actualModel2.CalendarQuarter.Should().BeNull();
            actualModel2.CalendarYear.Should().BeNull();
            actualModel2.CalendarUnbounded.Should().BeNull();
            actualModel2.FiscalUnitOfTime.Should().BeNull();
            actualModel2.FiscalMonth.Should().BeNull();
            actualModel2.FiscalQuarter.Should().BeNull();
            actualModel2.FiscalYear.Should().BeNull();
            actualModel2.FiscalUnbounded.Should().BeNull();
            actualModel2.GenericUnitOfTime.Should().BeNull();
            actualModel2.GenericMonth.Should().BeNull();
            actualModel2.GenericQuarter.Should().BeNull();
            actualModel2.GenericYear.Should().BeNull();
            actualModel2.GenericUnbounded.Should().BeNull();
        }

        [Fact]
        public static void ReportingPeriodModel_without_nulls___Should_serialize_to_string_representation_of_ReportingPeriod___When_using_custom_serializers()
        {
            // Arrange
            var model1 = A.Dummy<ReportingPeriodModel>();
            var model2 = A.Dummy<IReportingPeriodModel>();

            var expectedJson1 =
                nameof(model1.UnitOfTime) + "=" + model1.UnitOfTime.SerializeToString() + Environment.NewLine +
                nameof(model1.CalendarUnitOfTime) + "=" + model1.CalendarUnitOfTime.SerializeToString() + Environment.NewLine +
                nameof(model1.CalendarDay) + "=" + model1.CalendarDay.SerializeToString() + Environment.NewLine +
                nameof(model1.CalendarMonth) + "=" + model1.CalendarMonth.SerializeToString() + Environment.NewLine +
                nameof(model1.CalendarQuarter) + "=" + model1.CalendarQuarter.SerializeToString() + Environment.NewLine +
                nameof(model1.CalendarYear) + "=" + model1.CalendarYear.SerializeToString() + Environment.NewLine +
                nameof(model1.CalendarUnbounded) + "=" + model1.CalendarUnbounded.SerializeToString() + Environment.NewLine +
                nameof(model1.FiscalUnitOfTime) + "=" + model1.FiscalUnitOfTime.SerializeToString() + Environment.NewLine +
                nameof(model1.FiscalMonth) + "=" + model1.FiscalMonth.SerializeToString() + Environment.NewLine +
                nameof(model1.FiscalQuarter) + "=" + model1.FiscalQuarter.SerializeToString() + Environment.NewLine +
                nameof(model1.FiscalYear) + "=" + model1.FiscalYear.SerializeToString() + Environment.NewLine +
                nameof(model1.FiscalUnbounded) + "=" + model1.FiscalUnbounded.SerializeToString() + Environment.NewLine +
                nameof(model1.GenericUnitOfTime) + "=" + model1.GenericUnitOfTime.SerializeToString() + Environment.NewLine +
                nameof(model1.GenericMonth) + "=" + model1.GenericMonth.SerializeToString() + Environment.NewLine +
                nameof(model1.GenericQuarter) + "=" + model1.GenericQuarter.SerializeToString() + Environment.NewLine +
                nameof(model1.GenericYear) + "=" + model1.GenericYear.SerializeToString() + Environment.NewLine +
                nameof(model1.GenericUnbounded) + "=" + model1.GenericUnbounded.SerializeToString() + Environment.NewLine;

            var expectedJson2 =
                nameof(model2.UnitOfTime) + "=" + model2.UnitOfTime.SerializeToString() + Environment.NewLine +
                nameof(model2.CalendarUnitOfTime) + "=" + model2.CalendarUnitOfTime.SerializeToString() + Environment.NewLine +
                nameof(model2.CalendarDay) + "=" + model2.CalendarDay.SerializeToString() + Environment.NewLine +
                nameof(model2.CalendarMonth) + "=" + model2.CalendarMonth.SerializeToString() + Environment.NewLine +
                nameof(model2.CalendarQuarter) + "=" + model2.CalendarQuarter.SerializeToString() + Environment.NewLine +
                nameof(model2.CalendarYear) + "=" + model2.CalendarYear.SerializeToString() + Environment.NewLine +
                nameof(model2.CalendarUnbounded) + "=" + model2.CalendarUnbounded.SerializeToString() + Environment.NewLine +
                nameof(model2.FiscalUnitOfTime) + "=" + model2.FiscalUnitOfTime.SerializeToString() + Environment.NewLine +
                nameof(model2.FiscalMonth) + "=" + model2.FiscalMonth.SerializeToString() + Environment.NewLine +
                nameof(model2.FiscalQuarter) + "=" + model2.FiscalQuarter.SerializeToString() + Environment.NewLine +
                nameof(model2.FiscalYear) + "=" + model2.FiscalYear.SerializeToString() + Environment.NewLine +
                nameof(model2.FiscalUnbounded) + "=" + model2.FiscalUnbounded.SerializeToString() + Environment.NewLine +
                nameof(model2.GenericUnitOfTime) + "=" + model2.GenericUnitOfTime.SerializeToString() + Environment.NewLine +
                nameof(model2.GenericMonth) + "=" + model2.GenericMonth.SerializeToString() + Environment.NewLine +
                nameof(model2.GenericQuarter) + "=" + model2.GenericQuarter.SerializeToString() + Environment.NewLine +
                nameof(model2.GenericYear) + "=" + model2.GenericYear.SerializeToString() + Environment.NewLine +
                nameof(model2.GenericUnbounded) + "=" + model2.GenericUnbounded.SerializeToString() + Environment.NewLine;

            // Act
            var actualJson1 = PropertyBagSerializer.SerializeToString(model1);
            var actualJson2 = PropertyBagSerializer.SerializeToString(model2);

            // Assert
            actualJson1.Should().Contain(expectedJson1);
            actualJson2.Should().Contain(expectedJson2);
        }

        private class ReportingPeriodModel
        {
            public ReportingPeriod<UnitOfTime> UnitOfTime { get; set; }

            public ReportingPeriod<CalendarUnitOfTime> CalendarUnitOfTime { get; set; }

            public ReportingPeriod<CalendarDay> CalendarDay { get; set; }

            public ReportingPeriod<CalendarMonth> CalendarMonth { get; set; }

            public ReportingPeriod<CalendarQuarter> CalendarQuarter { get; set; }

            public ReportingPeriod<CalendarYear> CalendarYear { get; set; }

            public ReportingPeriod<CalendarUnbounded> CalendarUnbounded { get; set; }

            public ReportingPeriod<FiscalUnitOfTime> FiscalUnitOfTime { get; set; }

            public ReportingPeriod<FiscalMonth> FiscalMonth { get; set; }

            public ReportingPeriod<FiscalQuarter> FiscalQuarter { get; set; }

            public ReportingPeriod<FiscalYear> FiscalYear { get; set; }

            public ReportingPeriod<FiscalUnbounded> FiscalUnbounded { get; set; }

            public ReportingPeriod<GenericUnitOfTime> GenericUnitOfTime { get; set; }

            public ReportingPeriod<GenericMonth> GenericMonth { get; set; }

            public ReportingPeriod<GenericQuarter> GenericQuarter { get; set; }

            public ReportingPeriod<GenericYear> GenericYear { get; set; }

            public ReportingPeriod<GenericUnbounded> GenericUnbounded { get; set; }
        }

        private class IReportingPeriodModel
        {
            public IReportingPeriod<UnitOfTime> UnitOfTime { get; set; }

            public IReportingPeriod<CalendarUnitOfTime> CalendarUnitOfTime { get; set; }

            public IReportingPeriod<CalendarDay> CalendarDay { get; set; }

            public IReportingPeriod<CalendarMonth> CalendarMonth { get; set; }

            public IReportingPeriod<CalendarQuarter> CalendarQuarter { get; set; }

            public IReportingPeriod<CalendarYear> CalendarYear { get; set; }

            public IReportingPeriod<CalendarUnbounded> CalendarUnbounded { get; set; }

            public IReportingPeriod<FiscalUnitOfTime> FiscalUnitOfTime { get; set; }

            public IReportingPeriod<FiscalMonth> FiscalMonth { get; set; }

            public IReportingPeriod<FiscalQuarter> FiscalQuarter { get; set; }

            public IReportingPeriod<FiscalYear> FiscalYear { get; set; }

            public IReportingPeriod<FiscalUnbounded> FiscalUnbounded { get; set; }

            public IReportingPeriod<GenericUnitOfTime> GenericUnitOfTime { get; set; }

            public IReportingPeriod<GenericMonth> GenericMonth { get; set; }

            public IReportingPeriod<GenericQuarter> GenericQuarter { get; set; }

            public IReportingPeriod<GenericYear> GenericYear { get; set; }

            public IReportingPeriod<GenericUnbounded> GenericUnbounded { get; set; }
        }
    }
}
