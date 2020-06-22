// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfTimeStringSerializerTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Serialization.Test
{
    using System;
    using System.Collections.Generic;

    using FakeItEasy;

    using OBeautifulCode.AccountingTime.Serialization.PropertyBag;
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Serialization.PropertyBag;

    using Xunit;

    public static class UnitOfTimeStringSerializerTest
    {
        private static readonly ObcPropertyBagSerializer PropertyBagSerializer = new ObcPropertyBagSerializer<AccountingTimeTestPropertyBagConfiguration>();

        [Fact]
        public static void UnitOfTimeModelWrapper_with_nulls___Should_roundtrip_to_property_bag_and_back___When_using_UnitOfTimeStringSerializer()
        {
            // Arrange
            var expected = new UnitOfTimeModelWrapper();

            var propertyBag = PropertyBagSerializer.SerializeToString(expected);

            // Act
            var actual = PropertyBagSerializer.Deserialize<UnitOfTimeModelWrapper>(propertyBag);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void UnitOfTimeModelWrapper_without_nulls___Should_roundtrip_to_property_bag_and_back___When_using_UnitOfTimeStringSerializer()
        {
            // Arrange
            var expected = A.Dummy<UnitOfTimeModelWrapper>();

            var propertyBag = PropertyBagSerializer.SerializeToString(expected);

            // Act
            var actual = PropertyBagSerializer.Deserialize<UnitOfTimeModelWrapper>(propertyBag);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void UnitOfTimeModelWrapper_without_nulls___Should_serialize_to_sortable_string_representation_of_UnitOfTime___When_using_UnitOfTimeStringSerializer()
        {
            // Arrange
            var model = A.Dummy<UnitOfTimeModelWrapper>();

            var expected =
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
            var actual = PropertyBagSerializer.SerializeToString(model);

            // Assert
            actual.AsTest().Must().ContainString(expected);
        }

        private class AccountingTimeTestPropertyBagConfiguration : PropertyBagSerializationConfigurationBase
        {
            protected override IReadOnlyCollection<PropertyBagSerializationConfigurationType> DependentPropertyBagSerializationConfigurationTypes => new[] { typeof(AccountingTimePropertyBagSerializationConfiguration).ToPropertyBagSerializationConfigurationType() };

            protected override IReadOnlyCollection<TypeToRegisterForPropertyBag> TypesToRegisterForPropertyBag => new[] { typeof(UnitOfTimeModelWrapper).ToTypeToRegisterForPropertyBag(), };
        }
    }
}
