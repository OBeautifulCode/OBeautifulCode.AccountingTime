// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountingTimeJsonSerializationConfiguration.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Serialization.Json
{
    using System.Collections.Generic;

    using OBeautifulCode.Serialization.Json;

    using static System.FormattableString;

    /// <inheritdoc />
    public class AccountingTimeJsonSerializationConfiguration : JsonSerializationConfigurationBase
    {
        /// <inheritdoc />
        protected override IReadOnlyCollection<string> TypeToRegisterNamespacePrefixFilters => new[] { Invariant($"{nameof(OBeautifulCode)}.{nameof(AccountingTime)}") };

        /// <inheritdoc />
        protected override IReadOnlyCollection<TypeToRegisterForJson> TypesToRegisterForJson => new[]
        {
            typeof(AccountingPeriodSystem).ToTypeToRegisterForJson(),
            typeof(UnitOfTime).ToTypeToRegisterForJsonUsingStringSerializer(new UnitOfTimeStringSerializer()),
            typeof(ReportingPeriod).ToTypeToRegisterForJsonUsingStringSerializer(new ReportingPeriodStringSerializer()),
        };
    }
}
