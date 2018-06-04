// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriodPersistenceModel.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Serialization.Bson
{
    using MongoDB.Bson.Serialization;

    /// <summary>
    /// The persistence model for an <see cref="IReportingPeriod{UnitOfTime}"/>.
    /// </summary>
    internal class ReportingPeriodPersistenceModel
    {
        /// <summary>
        /// The reporting period serializer.
        /// </summary>
        public static readonly BsonClassMapSerializer<ReportingPeriodPersistenceModel> Serializer =
                new BsonClassMapSerializer<ReportingPeriodPersistenceModel>(BsonClassMap.LookupClassMap(typeof(ReportingPeriodPersistenceModel)));

        /// <summary>
        /// Gets or sets the start of the reporting period.
        /// </summary>
        public string Start { get; set; }

        /// <summary>
        /// Gets or sets the end of the reporting period.
        /// </summary>
        public string End { get; set; }
    }
}
