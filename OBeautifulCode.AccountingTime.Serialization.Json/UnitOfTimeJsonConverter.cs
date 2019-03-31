// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfTimeJsonConverter.cs" company="OBeautifulCode">
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
    /// Converts a <see cref="UnitOfTime"/> to and from JSON.
    /// </summary>
    public class UnitOfTimeJsonConverter : JsonConverter
    {
        /// <inheritdoc />
        public override void WriteJson(
            JsonWriter writer,
            object value,
            JsonSerializer serializer)
        {
            var unitOfTime = value as UnitOfTime;
            if (unitOfTime != null)
            {
                new { writer }.Must().NotBeNull();

                var stringToWrite = unitOfTime.SerializeToSortableString();
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

            UnitOfTime result = null;
            if (reader.Value != null)
            {
                result = reader.Value.ToString().DeserializeFromSortableString<UnitOfTime>();
            }

            return result;
        }

        /// <inheritdoc />
        public override bool CanConvert(
            Type objectType)
        {
            var result = typeof(UnitOfTime).IsAssignableFrom(objectType);
            return result;
        }
    }
}
