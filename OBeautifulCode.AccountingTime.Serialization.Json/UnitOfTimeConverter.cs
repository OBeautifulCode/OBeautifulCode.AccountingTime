// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfTimeConverter.cs" company="OBeautifulCode">
//    Copyright (c) OBeautifulCode 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Serialization.Json
{
    using System;

    using Newtonsoft.Json;

    using OBeautifulCode.AccountingTime;

    /// <summary>
    /// Converts a <see cref="UnitOfTime"/> to and from JSON.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class UnitOfTimeConverter : JsonConverter
    {
        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var unitOfTime = value as UnitOfTime;
            if (unitOfTime != null)
            {
                var stringToWrite = unitOfTime.SerializeToSortableString();
                writer.WriteValue(stringToWrite);
            }
        }

        /// <inheritdoc />
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            UnitOfTime result = null;
            if (reader.Value != null)
            {
                result = reader.Value.ToString().DeserializeFromSortableString<UnitOfTime>();
            }

            return result;
        }

        /// <inheritdoc />
        public override bool CanConvert(Type objectType)
        {
            var result = typeof(UnitOfTime).IsAssignableFrom(objectType);
            return result;
        }
    }
}
