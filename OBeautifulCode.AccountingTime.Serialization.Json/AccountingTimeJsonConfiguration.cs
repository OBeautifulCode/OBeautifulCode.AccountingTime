// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountingTimeJsonConfiguration.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Serialization.Json
{
    using System;
    using System.Collections.Generic;

    using Naos.Serialization.Json;

    /// <inheritdoc />
    public class AccountingTimeJsonConfiguration : JsonConfigurationBase
    {
        /// <inheritdoc />
        protected override IReadOnlyCollection<Type> TypesToAutoRegisterWithDiscovery => new[] { typeof(AccountingPeriodSystem) };

        /// <inheritdoc />
        protected override IReadOnlyCollection<RegisteredJsonConverter> ConvertersToRegister => new[]
        {
            new RegisteredJsonConverter(() => new UnitOfTimeJsonConverter(), () => new UnitOfTimeJsonConverter(), RegisteredJsonConverterOutputKind.String, TypeHelper.GetAllUnitOfTimeTypes()),
            new RegisteredJsonConverter(() => new ReportingPeriodJsonConverter(), () => new ReportingPeriodJsonConverter(), RegisteredJsonConverterOutputKind.String, TypeHelper.GetAllBoundReportingPeriodTypes()),
        };
    }
}
