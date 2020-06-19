// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriodStringSerializer.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Serialization.PropertyBag
{
    using System;

    using OBeautifulCode.Serialization;

    using static System.FormattableString;

    /// <summary>
    /// Represents the start, end, or both, of a reporting period.
    /// </summary>
    public class ReportingPeriodStringSerializer : IStringSerializeAndDeserialize
    {
        /// <inheritdoc />
        public string SerializeToString(
            object objectToSerialize)
        {
            if (objectToSerialize is ReportingPeriod objectAsReportingPeriod)
            {
                return objectAsReportingPeriod.SerializeToString();
            }
            else
            {
                throw new NotSupportedException(Invariant($"Unsupported type {objectToSerialize?.GetType().FullName ?? "<NULL OBJECT>"}, expected an implementer {nameof(ReportingPeriod)}"));
            }
        }

        /// <inheritdoc />
        public T Deserialize<T>(
            string serializedString)
        {
            var result = (T)this.Deserialize(serializedString, typeof(T));

            return result;
        }

        /// <inheritdoc />
        public object Deserialize(
            string serializedString,
            Type type)
        {
            if (type != typeof(ReportingPeriod))
            {
                throw new NotSupportedException(Invariant($"Unsupported type {type?.FullName ?? "<NULL TYPE>"}, expected an implementer {nameof(ReportingPeriod)}"));
            }

            var result = serializedString.DeserializeFromString();

            return result;
        }
    }
}
