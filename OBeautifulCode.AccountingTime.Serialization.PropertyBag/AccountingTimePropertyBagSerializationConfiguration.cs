// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountingTimePropertyBagSerializationConfiguration.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Serialization.PropertyBag
{
    using System.Collections.Generic;
    using OBeautifulCode.Serialization.PropertyBag;

    /// <inheritdoc />
    public class AccountingTimePropertyBagSerializationConfiguration : PropertyBagSerializationConfigurationBase
    {
        /// <inheritdoc />
        protected override IReadOnlyCollection<string> TypeToRegisterNamespacePrefixFilters =>
            new[]
            {
                OBeautifulCode.AccountingTime.ProjectInfo.Namespace,
            };

        /// <inheritdoc />
        protected override IReadOnlyCollection<TypeToRegisterForPropertyBag> TypesToRegisterForPropertyBag => new[]
        {
            typeof(UnitOfTime).ToTypeToRegisterForPropertyBagUsingStringSerializer(new UnitOfTimeStringSerializer()),
            typeof(ReportingPeriod).ToTypeToRegisterForPropertyBagUsingStringSerializer(new ReportingPeriodStringSerializer()),
            typeof(MonthNumber).ToTypeToRegisterForPropertyBag(),
            typeof(MonthOfYear).ToTypeToRegisterForPropertyBag(),
            typeof(QuarterNumber).ToTypeToRegisterForPropertyBag(),
            typeof(UnitOfTimeGranularity).ToTypeToRegisterForPropertyBag(),
            typeof(UnitOfTimeKind).ToTypeToRegisterForPropertyBag(),
        };
    }
}
