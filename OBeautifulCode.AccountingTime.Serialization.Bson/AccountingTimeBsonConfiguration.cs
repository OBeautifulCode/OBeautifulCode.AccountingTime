// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountingTimeBsonConfiguration.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Serialization.Bson
{
    using System;
    using System.Linq;

    using MongoDB.Bson.Serialization;
    using Naos.Serialization.Bson;

    /// <summary>
    /// A <see cref="BsonConfigurationBase"/> for Accounting Time types.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class AccountingTimeBsonConfiguration : BsonConfigurationBase
    {
        /// <inheritdoc />
        protected override void CustomConfiguration()
        {
            // register serializer for various flavors of UnitOfTime
            var unitOfTimeTypesToRegister = this.GetSubclassTypes(typeof(UnitOfTime), includeSpecifiedTypeInReturnList: true).ToList();

            unitOfTimeTypesToRegister.ForEach(
                t =>
                {
                    this.RegisterCustomSerializer(
                        t,
                        Activator.CreateInstance(typeof(UnitOfTimeSerializer<>).MakeGenericType(t)) as IBsonSerializer);
                });

            // register serializer for various flavors of IReportingPeriod
            var reportingPeriodType = typeof(IReportingPeriod<>);
            var reportingPeriodTypesToRegister =
                reportingPeriodType.Assembly.GetTypes()
                    .Where(type =>
                        (type == reportingPeriodType) ||
                        type.GetInterfaces()
                            .Where(_ => _.IsGenericType)
                            .Select(_ => _.GetGenericTypeDefinition())
                            .Contains(reportingPeriodType))
                    .ToList();

            foreach (var reportingPeriodTypeToRegister in reportingPeriodTypesToRegister)
            {
                unitOfTimeTypesToRegister.ForEach(
                    t =>
                    {
                        this.RegisterCustomSerializer(
                            reportingPeriodTypeToRegister.MakeGenericType(t),
                            Activator.CreateInstance(
                                typeof(ReportingPeriodSerializer<>).MakeGenericType(
                                    reportingPeriodTypeToRegister.MakeGenericType(t))) as IBsonSerializer);
                    });
            }
        }
    }
}
