// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountingTimeJsonSerializationConfiguration.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Serialization.Json
{
    using System.Collections.Generic;

    using OBeautifulCode.Serialization.Json;

    /// <inheritdoc />
    public class AccountingTimeJsonSerializationConfiguration : JsonSerializationConfigurationBase
    {
        /// <inheritdoc />
        protected override IReadOnlyCollection<TypeToRegisterForJson> TypesToRegisterForJson => new[]
        {
            typeof(AccountingPeriodSystem).ToTypeToRegisterForJson(),
            typeof(UnitOfTime).ToTypeToRegisterForJsonUsingStringSerializer(new UnitOfTimeStringSerializer()),
            typeof(ReportingPeriod).ToTypeToRegisterForJsonUsingStringSerializer(new ReportingPeriodStringSerializer()),
            typeof(MonthNumber).ToTypeToRegisterForJson(),
            typeof(MonthOfYear).ToTypeToRegisterForJson(),
            typeof(QuarterNumber).ToTypeToRegisterForJson(),
            typeof(UnitOfTimeGranularity).ToTypeToRegisterForJson(),
            typeof(UnitOfTimeKind).ToTypeToRegisterForJson(),
            typeof(Unit).ToTypeToRegisterForJson(),
            typeof(Duration).ToTypeToRegisterForJson(),
        };
    }
}
