// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountingTimePropertyBagConfigurationTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Serialization.Test
{
    using System;
    using System.Collections.Generic;

    using FakeItEasy;

    using FluentAssertions;

    using OBeautifulCode.AccountingTime.Serialization.PropertyBag;
    using OBeautifulCode.Serialization.PropertyBag;

    using Xunit;

    public static class AccountingTimePropertyBagConfigurationTest
    {
        private static readonly ObcPropertyBagSerializer PropertyBagSerializer = new ObcPropertyBagSerializer(typeof(AccountingTimeTestPropertyBagConfiguration));

        [Fact]
        public static void Deserialize___Should_roundtrip_serialized_UnitOfTimeModel___When_model_contains_only_null_values()
        {
            // Arrange
            var expected = new UnitOfTimeModel();
            var json = PropertyBagSerializer.SerializeToString(expected);

            // Act
            var actual = PropertyBagSerializer.Deserialize<UnitOfTimeModel>(json);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public static void Deserialize___Should_roundtrip_serialized_UnitOfTimeModel___When_model_contains_non_null_values()
        {
            // Arrange
            var expected = A.Dummy<UnitOfTimeModel>();
            var json = PropertyBagSerializer.SerializeToString(expected);

            // Act
            var actual = PropertyBagSerializer.Deserialize<UnitOfTimeModel>(json);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public static void Deserialize___Should_roundtrip_serialized_ReportingPeriodModel___When_model_contains_only_null_values()
        {
            // Arrange
            var expected1 = new ReportingPeriodModel();
            var expected2 = new IReportingPeriodModel();

            var json1 = PropertyBagSerializer.SerializeToString(expected1);
            var json2 = PropertyBagSerializer.SerializeToString(expected2);

            // Act
            var actual1 = PropertyBagSerializer.Deserialize<ReportingPeriodModel>(json1);
            var actual2 = PropertyBagSerializer.Deserialize<IReportingPeriodModel>(json2);

            // Assert
            actual1.Should().Be(expected1);
            actual2.Should().Be(expected2);
        }

        [Fact]
        public static void Deserialize___Should_roundtrip_serialized_ReportingPeriodModel___When_model_contains_non_null_values()
        {
            // Arrange
            var expected1 = A.Dummy<ReportingPeriodModel>();
            var expected2 = A.Dummy<IReportingPeriodModel>();

            var json1 = PropertyBagSerializer.SerializeToString(expected1);
            var json2 = PropertyBagSerializer.SerializeToString(expected2);

            // Act
            var actual1 = PropertyBagSerializer.Deserialize<ReportingPeriodModel>(json1);
            var actual2 = PropertyBagSerializer.Deserialize<IReportingPeriodModel>(json2);

            // Assert
            actual1.Should().Be(expected1);
            actual2.Should().Be(expected2);
        }

        [Fact]
        public static void UnitOfTimeModel_without_nulls___Should_serialize_to_sortable_string_representation_of_UnitOfTime___When_using_UnitOfTimeConverter()
        {
            // Arrange
            var model = A.Dummy<UnitOfTimeModel>();
            var expectedJson =
                nameof(model.UnitOfTime) + "=" + model.UnitOfTime.SerializeToSortableString() + Environment.NewLine +
                nameof(model.CalendarUnitOfTime) + "=" + model.CalendarUnitOfTime.SerializeToSortableString() + Environment.NewLine +
                nameof(model.CalendarDay) + "=" + model.CalendarDay.SerializeToSortableString() + Environment.NewLine +
                nameof(model.CalendarMonth) + "=" + model.CalendarMonth.SerializeToSortableString() + Environment.NewLine +
                nameof(model.CalendarQuarter) + "=" + model.CalendarQuarter.SerializeToSortableString() + Environment.NewLine +
                nameof(model.CalendarYear) + "=" + model.CalendarYear.SerializeToSortableString() + Environment.NewLine +
                nameof(model.CalendarUnbounded) + "=" + model.CalendarUnbounded.SerializeToSortableString() + Environment.NewLine +
                nameof(model.FiscalUnitOfTime) + "=" + model.FiscalUnitOfTime.SerializeToSortableString() + Environment.NewLine +
                nameof(model.FiscalMonth) + "=" + model.FiscalMonth.SerializeToSortableString() + Environment.NewLine +
                nameof(model.FiscalQuarter) + "=" + model.FiscalQuarter.SerializeToSortableString() + Environment.NewLine +
                nameof(model.FiscalYear) + "=" + model.FiscalYear.SerializeToSortableString() + Environment.NewLine +
                nameof(model.FiscalUnbounded) + "=" + model.FiscalUnbounded.SerializeToSortableString() + Environment.NewLine +
                nameof(model.GenericUnitOfTime) + "=" + model.GenericUnitOfTime.SerializeToSortableString() + Environment.NewLine +
                nameof(model.GenericMonth) + "=" + model.GenericMonth.SerializeToSortableString() + Environment.NewLine +
                nameof(model.GenericQuarter) + "=" + model.GenericQuarter.SerializeToSortableString() + Environment.NewLine +
                nameof(model.GenericYear) + "=" + model.GenericYear.SerializeToSortableString() + Environment.NewLine +
                nameof(model.GenericUnbounded) + "=" + model.GenericUnbounded.SerializeToSortableString() + Environment.NewLine;

            // Act
            var actualJson = PropertyBagSerializer.SerializeToString(model);

            // Assert
            actualJson.Should().Contain(expectedJson);
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

        private class AccountingTimeTestPropertyBagConfiguration : PropertyBagConfigurationBase
        {
            public override IReadOnlyCollection<Type> DependentConfigurationTypes => new[] { typeof(AccountingTimePropertyBagConfiguration) };

            protected override IReadOnlyCollection<Type> TypesToAutoRegister => new[] { typeof(AccountingPeriodSystemModel), typeof(UnitOfTimeModel), typeof(IReportingPeriodModel), typeof(ReportingPeriodModel) };
        }
    }
}
