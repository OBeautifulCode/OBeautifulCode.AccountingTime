// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriodStringSerializer.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Serialization.PropertyBag
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Naos.Serialization.Domain;

    using static System.FormattableString;

    /// <summary>
    /// Represents the start, end, or both, of a reporting period.
    /// </summary>
    public class ReportingPeriodStringSerializer : IStringSerializeAndDeserialize
    {
        /// <inheritdoc />
        public SerializationKind SerializationKind => SerializationKind.Default;

        /// <inheritdoc />
        public Type ConfigurationType => null;

        /// <inheritdoc />
        public string SerializeToString(object objectToSerialize)
        {
            if (objectToSerialize is IReportingPeriod<UnitOfTime> objectAsReportingPeriod)
            {
                return objectAsReportingPeriod.SerializeToString();
            }
            else
            {
                throw new NotSupportedException(Invariant($"Unsupported type {objectToSerialize?.GetType().FullName ?? "<NULL OBJECT>"}, expected an implmenter {nameof(IReportingPeriod<UnitOfTime>)}"));
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
            if (!IsReportingPeriod(type))
            {
                throw new NotSupportedException(Invariant($"Unsupported type {type?.FullName ?? "<NULL TYPE>"}, expected an implmenter {nameof(IReportingPeriod<UnitOfTime>)}"));
            }

            return serializedString.DeserializeFromString(type);
        }

        private static bool IsReportingPeriod(Type type)
        {
            var queue = new Queue<Type>(new[] { type });
            while (queue.Any())
            {
                var item = queue.Dequeue();
                if (item == null)
                {
                    continue;
                }

                if (item.IsGenericType && item.GetGenericTypeDefinition() == typeof(IReportingPeriod<>))
                {
                    return true;
                }

                item.GetInterfaces().ToList().ForEach(_ => queue.Enqueue(_));
                queue.Enqueue(item.BaseType);
            }

            return false;
        }
    }
}
