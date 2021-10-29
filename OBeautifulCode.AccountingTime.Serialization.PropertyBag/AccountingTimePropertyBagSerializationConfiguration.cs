// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountingTimePropertyBagSerializationConfiguration.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Serialization.PropertyBag
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using OBeautifulCode.Serialization.PropertyBag;
    using OBeautifulCode.Type;
    using OBeautifulCode.Type.Recipes;

    /// <inheritdoc />
    public class AccountingTimePropertyBagSerializationConfiguration : PropertyBagSerializationConfigurationBase
    {
        /// <inheritdoc />
        protected override IReadOnlyCollection<TypeToRegisterForPropertyBag> TypesToRegisterForPropertyBag => new[]
            {
                typeof(UnitOfTime).ToTypeToRegisterForPropertyBagUsingStringSerializer(new UnitOfTimeStringSerializer()),
                typeof(ReportingPeriod).ToTypeToRegisterForPropertyBagUsingStringSerializer(new ReportingPeriodStringSerializer()),
            }
            .Concat(
                new Type[0]
                    .Concat(new[] { typeof(IModel) })
                    .Concat(OBeautifulCode.AccountingTime.ProjectInfo.Assembly.GetPublicEnumTypes())
                    .Select(_ => _.ToTypeToRegisterForPropertyBag()))
            .ToList();
    }
}
