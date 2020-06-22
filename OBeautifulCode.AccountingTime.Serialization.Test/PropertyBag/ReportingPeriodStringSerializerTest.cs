// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriodStringSerializerTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Serialization.Test
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using OBeautifulCode.AccountingTime.Serialization.PropertyBag;
    using OBeautifulCode.AccountingTime.Serialization.Test.Internal;
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Serialization.PropertyBag;

    using Xunit;

    public static class ReportingPeriodStringSerializerTest
    {
        private static readonly ObcPropertyBagSerializer PropertyBagSerializer = new ObcPropertyBagSerializer<AccountingTimeTestPropertyBagConfiguration>();

        [Fact]
        public static void ReportingPeriodModelWrapper_with_nulls___Should_roundtrip_to_property_bag_and_back___When_using_ReportingPeriodStringSerializer()
        {
            // Arrange
            var expected = new ReportingPeriodModelWrapper();

            var propertyBag = PropertyBagSerializer.SerializeToString(expected);

            // Act
            var actual = PropertyBagSerializer.Deserialize<ReportingPeriodModelWrapper>(propertyBag);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void ReportingPeriodModelWrapper_without_nulls___Should_roundtrip_to_property_bag_and_back___When_using_ReportingPeriodStringSerializer()
        {
            // Arrange
            var expected = TestModels.GetDummyReportingPeriodModelWrapper();

            var propertyBag = PropertyBagSerializer.SerializeToString(expected);

            // Act
            var actual = PropertyBagSerializer.Deserialize<ReportingPeriodModelWrapper>(propertyBag);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void ReportingPeriodModelWrapper_without_nulls___Should_serialize_to_string_representation_of_ReportingPeriod___When_using_ReportingPeriodStringSerializer()
        {
            // Arrange
            var model = TestModels.GetDummyReportingPeriodModelWrapper();

            var expected =
                nameof(model.UnitOfTime) + "=" + model.UnitOfTime.SerializeToString() + Environment.NewLine +
                nameof(model.CalendarUnitOfTime) + "=" + model.CalendarUnitOfTime.SerializeToString() + Environment.NewLine +
                nameof(model.CalendarDay) + "=" + model.CalendarDay.SerializeToString() + Environment.NewLine +
                nameof(model.CalendarMonth) + "=" + model.CalendarMonth.SerializeToString() + Environment.NewLine +
                nameof(model.CalendarQuarter) + "=" + model.CalendarQuarter.SerializeToString() + Environment.NewLine +
                nameof(model.CalendarYear) + "=" + model.CalendarYear.SerializeToString() + Environment.NewLine +
                nameof(model.CalendarUnbounded) + "=" + model.CalendarUnbounded.SerializeToString() + Environment.NewLine +
                nameof(model.FiscalUnitOfTime) + "=" + model.FiscalUnitOfTime.SerializeToString() + Environment.NewLine +
                nameof(model.FiscalMonth) + "=" + model.FiscalMonth.SerializeToString() + Environment.NewLine +
                nameof(model.FiscalQuarter) + "=" + model.FiscalQuarter.SerializeToString() + Environment.NewLine +
                nameof(model.FiscalYear) + "=" + model.FiscalYear.SerializeToString() + Environment.NewLine +
                nameof(model.FiscalUnbounded) + "=" + model.FiscalUnbounded.SerializeToString() + Environment.NewLine +
                nameof(model.GenericUnitOfTime) + "=" + model.GenericUnitOfTime.SerializeToString() + Environment.NewLine +
                nameof(model.GenericMonth) + "=" + model.GenericMonth.SerializeToString() + Environment.NewLine +
                nameof(model.GenericQuarter) + "=" + model.GenericQuarter.SerializeToString() + Environment.NewLine +
                nameof(model.GenericYear) + "=" + model.GenericYear.SerializeToString() + Environment.NewLine +
                nameof(model.GenericUnbounded) + "=" + model.GenericUnbounded.SerializeToString() + Environment.NewLine;

            // Act
            var actual = PropertyBagSerializer.SerializeToString(model);

            // Assert
            actual.AsTest().Must().ContainString(expected);
        }

        [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = ObcSuppressBecause.CA1812_AvoidUninstantiatedInternalClasses_ClassExistsToUseItsTypeInUnitTests)]
        private class AccountingTimeTestPropertyBagConfiguration : PropertyBagSerializationConfigurationBase
        {
            protected override IReadOnlyCollection<PropertyBagSerializationConfigurationType> DependentPropertyBagSerializationConfigurationTypes => new[] { typeof(AccountingTimePropertyBagSerializationConfiguration).ToPropertyBagSerializationConfigurationType() };

            protected override IReadOnlyCollection<TypeToRegisterForPropertyBag> TypesToRegisterForPropertyBag => new[] { typeof(ReportingPeriodModelWrapper).ToTypeToRegisterForPropertyBag(), };
        }
    }
}
