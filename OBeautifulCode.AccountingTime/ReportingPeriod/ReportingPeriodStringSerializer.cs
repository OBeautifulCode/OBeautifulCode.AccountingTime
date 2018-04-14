// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriodComponent.cs" company="OBeautifulCode">
//    Copyright (c) OBeautifulCode 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;
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
            if (!(type?.GetInterfaces().Contains(typeof(IReportingPeriod<UnitOfTime>)) ?? false))
            {
                throw new NotSupportedException(Invariant($"Unsupported type {type?.FullName ?? "<NULL TYPE>"}, expected an implmenter {nameof(IReportingPeriod<UnitOfTime>)}"));
            }

            return serializedString.DeserializeFromString(type);
        }
    }
}
