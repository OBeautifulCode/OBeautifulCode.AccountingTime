// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfTimeStringSerializer.cs" company="OBeautifulCode">
//    Copyright (c) OBeautifulCode 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Serialization.PropertyBag
{
    using System;

    using Naos.Serialization.Domain;

    using static System.FormattableString;

    /// <summary>
    /// Represents the start, end, or both, of a reporting period.
    /// </summary>
    public class UnitOfTimeStringSerializer : IStringSerializeAndDeserialize
    {
        /// <inheritdoc />
        public SerializationKind SerializationKind => SerializationKind.Default;

        /// <inheritdoc />
        public Type ConfigurationType => null;

        /// <inheritdoc />
        public string SerializeToString(object objectToSerialize)
        {
            if (objectToSerialize is UnitOfTime objectAsUnitOfTime)
            {
                return objectAsUnitOfTime.SerializeToSortableString();
            }
            else
            {
                throw new NotSupportedException(Invariant($"Unsupported type {objectToSerialize?.GetType().FullName ?? "<NULL OBJECT>"}, expected an implmenter {nameof(UnitOfTime)}"));
            }
        }

        /// <inheritdoc />
        public T Deserialize<T>(string serializedString)
        {
            return (T)this.Deserialize(serializedString, typeof(T));
        }

        /// <inheritdoc />
        public object Deserialize(string serializedString, Type type)
        {
            if (!UnitOfTime.IsUnitOfTimeType(type))
            {
                throw new NotSupportedException(Invariant($"Unsupported type {type?.FullName ?? "<NULL TYPE>"}, expected an implmenter {nameof(UnitOfTime)}"));
            }

            return serializedString.DeserializeFromSortableString(type);
        }
    }
}
