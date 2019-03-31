// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountingTimeJsonConfiguration.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Serialization.Json
{
    using System;
    using System.Collections.Generic;

    using Naos.Serialization.Domain;
    using Naos.Serialization.Json;

    /// <inheritdoc />
    public class AccountingTimeJsonConfiguration : JsonConfigurationBase
    {
        /// <inheritdoc />
        protected override IReadOnlyCollection<Type> TypesToAutoRegisterWithDiscovery => new[] { typeof(AccountingPeriodSystem) };

        /// <inheritdoc />
        protected override IReadOnlyDictionary<SerializationDirection, IReadOnlyCollection<RegisteredJsonConverter>>
            ConvertersToPushOnStack =>
            new Dictionary<SerializationDirection, IReadOnlyCollection<RegisteredJsonConverter>>
            {
                {
                    SerializationDirection.Serialize,
                    new[]
                    {
                        new RegisteredJsonConverter(() => new UnitOfTimeJsonConverter(), RegisteredJsonConverterOutputKind.String, TypeHelper.GetAllUnitOfTimeTypes()),
                        new RegisteredJsonConverter(() => new ReportingPeriodJsonConverter(), RegisteredJsonConverterOutputKind.String, TypeHelper.GetAllBoundReportingPeriodTypes()),
                    }
                },
                {
                    SerializationDirection.Deserialize,
                    new[]
                    {
                        new RegisteredJsonConverter(() => new UnitOfTimeJsonConverter(), RegisteredJsonConverterOutputKind.String, TypeHelper.GetAllUnitOfTimeTypes()),
                        new RegisteredJsonConverter(() => new ReportingPeriodJsonConverter(), RegisteredJsonConverterOutputKind.String, TypeHelper.GetAllBoundReportingPeriodTypes()),
                    }
                },
            };
    }
}
