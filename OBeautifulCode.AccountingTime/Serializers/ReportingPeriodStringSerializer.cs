// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriodStringSerializer.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    using OBeautifulCode.Serialization;

    using static System.FormattableString;

    /// <summary>
    /// String serialize for <see cref="ReportingPeriod"/>.
    /// </summary>
    public class ReportingPeriodStringSerializer : IStringSerializeAndDeserialize
    {
        /// <inheritdoc />
        public string SerializeToString(
            object objectToSerialize)
        {
            string result;

            if (objectToSerialize == null)
            {
                result = null;
            }
            else if (objectToSerialize is ReportingPeriod reportingPeriod)
            {
                result = reportingPeriod.SerializeToString();
            }
            else
            {
                throw new ArgumentException(Invariant($"{nameof(objectToSerialize)} is not a {nameof(ReportingPeriod)}."));
            }

            return result;
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
            var result = serializedString?.DeserializeFromString();

            return result;
        }
    }
}
