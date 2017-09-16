﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriodPersistenceModel.cs" company="OBeautifulCode">
//    Copyright (c) OBeautifulCode 2017. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in OBeautifulCode.AccountingTime.Serialization source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Serialization
{
    using MongoDB.Bson.Serialization;

    /// <summary>
    /// The persistence model for an <see cref="IReportingPeriod{UnitOfTime}"/>
    /// </summary>
#if !OBeautifulCodeAccountingTimeSerializationRecipesProject
    [System.Diagnostics.DebuggerStepThrough]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.CodeDom.Compiler.GeneratedCode("OBeautifulCode.AccountingTime.Serialization", "See package version number")]
#endif
    internal class ReportingPeriodPersistenceModel
    {
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
