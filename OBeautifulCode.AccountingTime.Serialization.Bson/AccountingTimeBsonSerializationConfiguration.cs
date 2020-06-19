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

    using MongoDB.Bson.Serialization;

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
            }
            .Concat(BuildTypesToRegisterWithSerializers())
            .ToList();

        private static IReadOnlyCollection<TypeToRegisterForBson> BuildTypesToRegisterWithSerializers()
        {
            var result = new List<TypeToRegisterForBson>();

            var unitOfTimeTypes = TypeHelper.GetAllUnitOfTimeTypes().ToList();

            foreach (var unitOfTimeType in unitOfTimeTypes)
            {
                var unitOfTimeSerializerType = typeof(UnitOfTimeSerializer<>).MakeGenericType(unitOfTimeType);

                var unitOfTimeSerializer = (IBsonSerializer)Activator.CreateInstance(unitOfTimeSerializerType);

                var unitOfTimeTypeToRegister = new TypeToRegisterForBson(
                    unitOfTimeType,
                    MemberTypesToInclude.None,
                    RelatedTypesToInclude.None,
                    new BsonSerializerBuilder(() => unitOfTimeSerializer, BsonSerializerOutputKind.String),
                    null);

                result.Add(unitOfTimeTypeToRegister);
            }

            var reportingPeriodSerializer = new ReportingPeriodSerializer();

            var reportingPeriodTypeToRegister = new TypeToRegisterForBson(
                typeof(ReportingPeriod),
                MemberTypesToInclude.None,
                RelatedTypesToInclude.None,
                new BsonSerializerBuilder(() => reportingPeriodSerializer, BsonSerializerOutputKind.Object),
                null);

            result.Add(reportingPeriodTypeToRegister);

            return result;
        }
    }
}
