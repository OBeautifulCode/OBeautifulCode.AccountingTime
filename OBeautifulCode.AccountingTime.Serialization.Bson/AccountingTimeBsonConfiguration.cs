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
        protected override void FinalConfiguration()
        {
            // register serializer for various flavors of UnitOfTime
            var unitOfTimeTypesToRegister = TypeHelper.GetAllUnitOfTimeTypes().ToList();

            unitOfTimeTypesToRegister.ForEach(
                t =>
                {
                    this.RegisterCustomSerializer(
                        t,
                        Activator.CreateInstance(typeof(UnitOfTimeSerializer<>).MakeGenericType(t)) as IBsonSerializer);
                });

            // register serializer for various flavors of IReportingPeriod
            var reportingPeriodTypesToRegister = TypeHelper.GetAllBoundReportingPeriodTypes().ToList();

            reportingPeriodTypesToRegister.ForEach(
                t =>
                {
                    this.RegisterCustomSerializer(
                        t,
                        Activator.CreateInstance(typeof(ReportingPeriodSerializer<>).MakeGenericType(t)) as IBsonSerializer);
                });
        }
    }
}
