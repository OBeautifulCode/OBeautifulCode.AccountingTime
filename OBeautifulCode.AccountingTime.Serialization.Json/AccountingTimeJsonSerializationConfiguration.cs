// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountingTimeJsonSerializationConfiguration.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Serialization.Json
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using OBeautifulCode.Serialization.Json;
    using OBeautifulCode.Type;
    using OBeautifulCode.Type.Recipes;

    /// <inheritdoc />
    public class AccountingTimeJsonSerializationConfiguration : JsonSerializationConfigurationBase
    {
        /// <inheritdoc />
        protected override IReadOnlyCollection<string> TypeToRegisterNamespacePrefixFilters =>
            new[]
            {
                OBeautifulCode.AccountingTime.ProjectInfo.Namespace,
            };

        /// <inheritdoc />
        protected override IReadOnlyCollection<TypeToRegisterForJson> TypesToRegisterForJson => new[]
            {
                typeof(UnitOfTime).ToTypeToRegisterForJsonUsingStringSerializer(new UnitOfTimeStringSerializer()),
                typeof(ReportingPeriod).ToTypeToRegisterForJsonUsingStringSerializer(new ReportingPeriodStringSerializer()),

                // This is necessary because ITimeseries is never hit in the traversal of types.
                // It does not implement IModel.
                // Further, we don't have a model with an ITimeseries property (which is typically
                // how we would discover a model interface like ITimeseries).
                typeof(ITimeseries).ToTypeToRegisterForJson(),
            }
            .Concat(
                new Type[0]
                    .Concat(new[] { typeof(IModel) })
                    .Concat(OBeautifulCode.AccountingTime.ProjectInfo.Assembly.GetPublicEnumTypes())
                    .Select(_ => _.ToTypeToRegisterForJson()))
            .ToList();
    }
}
