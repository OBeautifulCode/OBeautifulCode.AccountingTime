// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountingTimeBsonSerializationConfiguration.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Serialization.Bson
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using OBeautifulCode.Serialization;
    using OBeautifulCode.Serialization.Bson;
    using OBeautifulCode.Type;
    using OBeautifulCode.Type.Recipes;

    /// <inheritdoc />
    public class AccountingTimeBsonSerializationConfiguration : BsonSerializationConfigurationBase
    {
        /// <inheritdoc />
        protected override IReadOnlyCollection<TypeToRegisterForBson> TypesToRegisterForBson => new[]
            {
                typeof(UnitOfTime).ToTypeToRegisterForBsonUsingStringSerializer(new UnitOfTimeStringSerializer()),
                new TypeToRegisterForBson(typeof(ReportingPeriod), MemberTypesToInclude.None, RelatedTypesToInclude.None, new BsonSerializerBuilder(() => new ReportingPeriodBsonSerializer(), BsonSerializerOutputKind.Object), null),
            }
            .Concat(
                new Type[0]
                    .Concat(new[] { typeof(IModel) })
                    .Concat(OBeautifulCode.AccountingTime.ProjectInfo.Assembly.GetPublicEnumTypes())
                    .Select(_ => _.ToTypeToRegisterForBson()))
            .ToList();
    }
}
