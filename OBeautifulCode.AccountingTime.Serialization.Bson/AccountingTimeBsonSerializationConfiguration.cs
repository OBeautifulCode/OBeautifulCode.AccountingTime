// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountingTimeBsonSerializationConfiguration.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Serialization.Bson
{
    using System.Collections.Generic;

    using OBeautifulCode.Serialization;
    using OBeautifulCode.Serialization.Bson;

    using static System.FormattableString;

    /// <inheritdoc />
    public class AccountingTimeBsonSerializationConfiguration : BsonSerializationConfigurationBase
    {
        /// <inheritdoc />
        protected override IReadOnlyCollection<string> TypeToRegisterNamespacePrefixFilters => new[] { Invariant($"{nameof(OBeautifulCode)}.{nameof(AccountingTime)}") };

        /// <inheritdoc />
        protected override IReadOnlyCollection<TypeToRegisterForBson> TypesToRegisterForBson => new[]
        {
            typeof(AccountingPeriodSystem).ToTypeToRegisterForBson(),
            typeof(UnitOfTime).ToTypeToRegisterForBsonUsingStringSerializer(new UnitOfTimeStringSerializer()),
            new TypeToRegisterForBson(typeof(ReportingPeriod), MemberTypesToInclude.None, RelatedTypesToInclude.None, new BsonSerializerBuilder(() => new ReportingPeriodBsonSerializer(), BsonSerializerOutputKind.Object), null),
            typeof(MonthNumber).ToTypeToRegisterForBson(),
            typeof(MonthOfYear).ToTypeToRegisterForBson(),
            typeof(QuarterNumber).ToTypeToRegisterForBson(),
            typeof(UnitOfTimeGranularity).ToTypeToRegisterForBson(),
            typeof(UnitOfTimeKind).ToTypeToRegisterForBson(),
        };
    }
}
