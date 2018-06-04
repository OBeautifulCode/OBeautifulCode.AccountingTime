// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriodJsonConverter.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Serialization.Json
{
    using System;

    using Newtonsoft.Json;

    using OBeautifulCode.AccountingTime;
    using OBeautifulCode.Validation.Recipes;

    /// <summary>
    /// Converts an <see cref="IReportingPeriod{T}"/> to and from JSON.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class ReportingPeriodJsonConverter : JsonConverter
    {
        /// <inheritdoc />
        public override void WriteJson(
            JsonWriter writer,
            object value,
            JsonSerializer serializer)
        {
            if (value is IReportingPeriod<UnitOfTime> reportingPeriod)
            {
                new { writer }.Must().NotBeNull();

                var stringToWrite = reportingPeriod.SerializeToString();
                writer.WriteValue(stringToWrite);
            }
        }

        /// <inheritdoc />
        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            new { reader }.Must().NotBeNull();

            object result = null;
            if (reader.Value != null)
            {
                result = reader.Value.ToString().DeserializeFromString(objectType);
            }

            return result;
        }

        /// <inheritdoc />
        public override bool CanConvert(
            Type objectType)
        {
            new { objectType }.Must().NotBeNull();

            if (objectType.IsGenericType)
            {
                var genericType = objectType.MakeGenericType();
                var result = genericType == typeof(IReportingPeriod<>);
                return result;
            }

            return false;
        }
    }
}
