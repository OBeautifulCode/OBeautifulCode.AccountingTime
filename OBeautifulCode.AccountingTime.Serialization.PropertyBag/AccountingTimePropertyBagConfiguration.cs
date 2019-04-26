// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountingTimePropertyBagConfiguration.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Serialization.PropertyBag
{
    using System.Collections.Generic;

    using Naos.Serialization.Domain;
    using Naos.Serialization.PropertyBag;

    /// <inheritdoc />
    public class AccountingTimePropertyBagConfiguration : PropertyBagConfigurationBase
    {
        /// <inheritdoc />
        protected override IReadOnlyCollection<RegisteredStringSerializer> SerializersToRegister => new[]
        {
            new RegisteredStringSerializer(() => new ReportingPeriodStringSerializer(), TypeHelper.GetAllBoundReportingPeriodTypes()),
            new RegisteredStringSerializer(() => new UnitOfTimeStringSerializer(), TypeHelper.GetAllUnitOfTimeTypes()),
        };
    }
}
