// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriodSerializer{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Serialization.Bson
{
    using System;

    using MongoDB.Bson;
    using MongoDB.Bson.Serialization;
    using MongoDB.Bson.Serialization.Serializers;

    using OBeautifulCode.Validation.Recipes;

    using static System.FormattableString;

    /// <inheritdoc />
    internal class ReportingPeriodSerializer<TValue> : SerializerBase<TValue>
        where TValue : class, IReportingPeriod<UnitOfTime>
    {
        /// <inheritdoc />
        public override TValue Deserialize(
            BsonDeserializationContext context,
            BsonDeserializationArgs args)
        {
            new { context }.Must().NotBeNull();

            var type = context.Reader.GetCurrentBsonType();
            switch (type)
            {
                case BsonType.Document:
                    var persistenceModel = ReportingPeriodPersistenceModel.Serializer.Deserialize(context);
                    var reportingPeriod = (persistenceModel.Start + "," + persistenceModel.End).DeserializeFromString<TValue>();
                    return reportingPeriod;
                case BsonType.Null:
                    context.Reader.ReadNull();
                    return default(TValue);
                default:
                    throw new NotSupportedException(Invariant($"Cannot convert a {type} to a {typeof(TValue).Name}."));
            }
        }

        /// <inheritdoc />
        public override void Serialize(
            BsonSerializationContext context,
            BsonSerializationArgs args,
            TValue value)
        {
            new { context }.Must().NotBeNull();

            if (value == null)
            {
                context.Writer.WriteNull();
            }
            else
            {
                var persistenceModel = new ReportingPeriodPersistenceModel
                {
                    Start = value.Start.SerializeToSortableString(),
                    End = value.End.SerializeToSortableString(),
                };

                ReportingPeriodPersistenceModel.Serializer.Serialize(context, persistenceModel);
            }
        }
    }
}
