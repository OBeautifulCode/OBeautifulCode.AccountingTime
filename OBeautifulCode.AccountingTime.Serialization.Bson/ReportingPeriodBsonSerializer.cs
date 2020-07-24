// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriodBsonSerializer.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Serialization.Bson
{
    using System;

    using MongoDB.Bson;
    using MongoDB.Bson.Serialization;
    using MongoDB.Bson.Serialization.Serializers;

    using static System.FormattableString;

    /// <inheritdoc />
    public class ReportingPeriodBsonSerializer : SerializerBase<ReportingPeriod>
    {
        /// <inheritdoc />
        public override ReportingPeriod Deserialize(
            BsonDeserializationContext context,
            BsonDeserializationArgs args)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var type = context.Reader.GetCurrentBsonType();

            ReportingPeriod result;

            switch (type)
            {
                case BsonType.Document:
                    var persistenceModel = ReportingPeriodPersistenceModel.Serializer.Deserialize(context);
                    result = (persistenceModel.Start + "," + persistenceModel.End).DeserializeFromString();
                    break;
                case BsonType.Null:
                    context.Reader.ReadNull();
                    result = null;
                    break;
                default:
                    throw new NotSupportedException(Invariant($"Cannot convert a {type} to a {nameof(ReportingPeriod)}."));
            }

            return result;
        }

        /// <inheritdoc />
        public override void Serialize(
            BsonSerializationContext context,
            BsonSerializationArgs args,
            ReportingPeriod value)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

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
