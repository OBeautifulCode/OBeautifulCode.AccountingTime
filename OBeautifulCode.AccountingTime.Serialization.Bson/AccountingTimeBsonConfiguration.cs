// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountingTimeBsonConfiguration.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Serialization.Bson
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MongoDB.Bson.Serialization;

    using Naos.Serialization.Bson;

    /// <inheritdoc />
    public class AccountingTimeBsonConfiguration : BsonConfigurationBase
    {
        /// <inheritdoc />
        protected override IReadOnlyCollection<Type> TypesToAutoRegisterWithDiscovery => new[] { typeof(AccountingPeriodSystem) };

        /// <inheritdoc />
        protected override IReadOnlyCollection<RegisteredBsonSerializer> SerializersToRegister => BuildRegisteredBsonSerializers();

        private static IReadOnlyCollection<RegisteredBsonSerializer> BuildRegisteredBsonSerializers()
        {
            // register serializer for various flavors of UnitOfTime
            var unitOfTimeTypesToRegister = TypeHelper.GetAllUnitOfTimeTypes().ToList();

            var result = new List<RegisteredBsonSerializer>();

            var unitOfTimeSerializers = unitOfTimeTypesToRegister
                .Select(_ => new RegisteredBsonSerializer(
                    () => Activator.CreateInstance(typeof(UnitOfTimeSerializer<>).MakeGenericType(_)) as IBsonSerializer,
                    new[] { _ }))
                .ToList();

            result.AddRange(unitOfTimeSerializers);

            // register serializer for various flavors of IReportingPeriod
            var reportingPeriodTypesToRegister = TypeHelper.GetAllBoundReportingPeriodTypes().ToList();

            var reportingPeriodSerializers = reportingPeriodTypesToRegister
                .Select(_ => new RegisteredBsonSerializer(
                    () => Activator.CreateInstance(typeof(ReportingPeriodSerializer<>).MakeGenericType(_)) as IBsonSerializer,
                    new[] { _ }))
                .ToList();

            result.AddRange(reportingPeriodSerializers);

            return result;
        }
    }
}
