// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfTimeBsonSerializer{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Serialization.Bson
{
    using System;

    using MongoDB.Bson;
    using MongoDB.Bson.Serialization;
    using MongoDB.Bson.Serialization.Serializers;

    using OBeautifulCode.Assertion.Recipes;

    using static System.FormattableString;

    /// <inheritdoc />
    public class UnitOfTimeBsonSerializer<TValue> : SerializerBase<TValue>
        where TValue : UnitOfTime
    {
        /// <inheritdoc />
        public override TValue Deserialize(
            BsonDeserializationContext context,
            BsonDeserializationArgs args)
        {
            new { context }.AsArg().Must().NotBeNull();

            var type = context.Reader.GetCurrentBsonType();

            TValue result;

            switch (type)
            {
                case BsonType.String:
                    result = context.Reader.ReadString().DeserializeFromSortableString<TValue>();
                    break;
                case BsonType.Null:
                    context.Reader.ReadNull();
                    result = default;
                    break;
                default:
                    throw new NotSupportedException(Invariant($"Cannot convert a {type} to a {typeof(TValue).Name}."));
            }

            return result;
        }

        /// <inheritdoc />
        public override void Serialize(
            BsonSerializationContext context,
            BsonSerializationArgs args,
            TValue value)
        {
            new { context }.AsArg().Must().NotBeNull();

            if (value == null)
            {
                context.Writer.WriteNull();
            }
            else
            {
                context.Writer.WriteString(value.SerializeToSortableString());
            }
        }
    }
}
