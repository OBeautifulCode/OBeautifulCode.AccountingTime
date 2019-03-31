// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountingTimePropertyBagConfiguration.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Serialization.PropertyBag
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Naos.Serialization.Domain;
    using Naos.Serialization.PropertyBag;

    /// <inheritdoc />
    public class AccountingTimePropertyBagConfiguration : PropertyBagConfigurationBase
    {
        /// <inheritdoc />
        protected override IReadOnlyDictionary<Type, IStringSerializeAndDeserialize> CustomTypeToSerializerMappings()
        {
            var reportingPeriodStringSerializer = new ReportingPeriodStringSerializer();

            var unitOfTimeStringSerializer = new UnitOfTimeStringSerializer();

            var unitOfTimeTypeToSerializerMap = TypeHelper.GetAllUnitOfTimeTypes().ToDictionary(_ => _, _ => (IStringSerializeAndDeserialize)unitOfTimeStringSerializer);
            var reportingPeriodTypeToSerializerMap = TypeHelper.GetAllBoundReportingPeriodTypes().ToDictionary(_ => _, _ => (IStringSerializeAndDeserialize)reportingPeriodStringSerializer);

            var result = new KeyValuePair<Type, IStringSerializeAndDeserialize>[0].Concat(unitOfTimeTypeToSerializerMap).Concat(reportingPeriodTypeToSerializerMap).ToDictionary(_ => _.Key, _ => _.Value);

            return result;
        }
    }
}
