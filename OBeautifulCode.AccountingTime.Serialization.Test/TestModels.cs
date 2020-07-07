// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestModels.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Serialization.Test
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using FakeItEasy;

    using MongoDB.Bson.Serialization.Attributes;

    using Newtonsoft.Json;

    using OBeautifulCode.AccountingTime.Serialization.Bson;
    using OBeautifulCode.AccountingTime.Serialization.Test.Internal;
    using OBeautifulCode.AccountingTime.Test;
    using OBeautifulCode.Serialization.Bson;
    using OBeautifulCode.Serialization.Json;

    public static class TestModels
    {
        public static ReportingPeriodModelWrapper GetDummyReportingPeriodModelWrapper()
        {
            var result = new ReportingPeriodModelWrapper
            {
                UnitOfTime = A.Dummy<ReportingPeriod>(),
                CalendarUnitOfTime = A.Dummy<CalendarReportingPeriod>(),
                CalendarDay = A.Dummy<CalendarDayReportingPeriod>(),
                CalendarMonth = A.Dummy<CalendarMonthReportingPeriod>(),
                CalendarQuarter = A.Dummy<CalendarQuarterReportingPeriod>(),
                CalendarUnbounded = A.Dummy<CalendarUnboundedReportingPeriod>(),
                CalendarYear = A.Dummy<CalendarYearReportingPeriod>(),
                FiscalUnitOfTime = A.Dummy<FiscalReportingPeriod>(),
                FiscalMonth = A.Dummy<FiscalMonthReportingPeriod>(),
                FiscalQuarter = A.Dummy<FiscalQuarterReportingPeriod>(),
                FiscalUnbounded = A.Dummy<FiscalUnboundedReportingPeriod>(),
                FiscalYear = A.Dummy<FiscalYearReportingPeriod>(),
                GenericUnitOfTime = A.Dummy<GenericReportingPeriod>(),
                GenericMonth = A.Dummy<GenericMonthReportingPeriod>(),
                GenericQuarter = A.Dummy<GenericQuarterReportingPeriod>(),
                GenericUnbounded = A.Dummy<GenericUnboundedReportingPeriod>(),
                GenericYear = A.Dummy<GenericYearReportingPeriod>(),
            };

            return result;
        }
    }

    public class ReportingPeriodModelWrapper : IEquatable<ReportingPeriodModelWrapper>
    {
        [BsonSerializer(typeof(ReportingPeriodBsonSerializer))]
        [JsonConverter(typeof(ReportingPeriodStringSerializerBackedJsonConverter))]
        public ReportingPeriod UnitOfTime { get; set; }

        [BsonSerializer(typeof(ReportingPeriodBsonSerializer))]
        [JsonConverter(typeof(ReportingPeriodStringSerializerBackedJsonConverter))]
        public ReportingPeriod CalendarUnitOfTime { get; set; }

        [BsonSerializer(typeof(ReportingPeriodBsonSerializer))]
        [JsonConverter(typeof(ReportingPeriodStringSerializerBackedJsonConverter))]
        public ReportingPeriod CalendarDay { get; set; }

        [BsonSerializer(typeof(ReportingPeriodBsonSerializer))]
        [JsonConverter(typeof(ReportingPeriodStringSerializerBackedJsonConverter))]
        public ReportingPeriod CalendarMonth { get; set; }

        [BsonSerializer(typeof(ReportingPeriodBsonSerializer))]
        [JsonConverter(typeof(ReportingPeriodStringSerializerBackedJsonConverter))]
        public ReportingPeriod CalendarQuarter { get; set; }

        [BsonSerializer(typeof(ReportingPeriodBsonSerializer))]
        [JsonConverter(typeof(ReportingPeriodStringSerializerBackedJsonConverter))]
        public ReportingPeriod CalendarYear { get; set; }

        [BsonSerializer(typeof(ReportingPeriodBsonSerializer))]
        [JsonConverter(typeof(ReportingPeriodStringSerializerBackedJsonConverter))]
        public ReportingPeriod CalendarUnbounded { get; set; }

        [BsonSerializer(typeof(ReportingPeriodBsonSerializer))]
        [JsonConverter(typeof(ReportingPeriodStringSerializerBackedJsonConverter))]
        public ReportingPeriod FiscalUnitOfTime { get; set; }

        [BsonSerializer(typeof(ReportingPeriodBsonSerializer))]
        [JsonConverter(typeof(ReportingPeriodStringSerializerBackedJsonConverter))]
        public ReportingPeriod FiscalMonth { get; set; }

        [BsonSerializer(typeof(ReportingPeriodBsonSerializer))]
        [JsonConverter(typeof(ReportingPeriodStringSerializerBackedJsonConverter))]
        public ReportingPeriod FiscalQuarter { get; set; }

        [BsonSerializer(typeof(ReportingPeriodBsonSerializer))]
        [JsonConverter(typeof(ReportingPeriodStringSerializerBackedJsonConverter))]
        public ReportingPeriod FiscalYear { get; set; }

        [BsonSerializer(typeof(ReportingPeriodBsonSerializer))]
        [JsonConverter(typeof(ReportingPeriodStringSerializerBackedJsonConverter))]
        public ReportingPeriod FiscalUnbounded { get; set; }

        [BsonSerializer(typeof(ReportingPeriodBsonSerializer))]
        [JsonConverter(typeof(ReportingPeriodStringSerializerBackedJsonConverter))]
        public ReportingPeriod GenericUnitOfTime { get; set; }

        [BsonSerializer(typeof(ReportingPeriodBsonSerializer))]
        [JsonConverter(typeof(ReportingPeriodStringSerializerBackedJsonConverter))]
        public ReportingPeriod GenericMonth { get; set; }

        [BsonSerializer(typeof(ReportingPeriodBsonSerializer))]
        [JsonConverter(typeof(ReportingPeriodStringSerializerBackedJsonConverter))]
        public ReportingPeriod GenericQuarter { get; set; }

        [BsonSerializer(typeof(ReportingPeriodBsonSerializer))]
        [JsonConverter(typeof(ReportingPeriodStringSerializerBackedJsonConverter))]
        public ReportingPeriod GenericYear { get; set; }

        [BsonSerializer(typeof(ReportingPeriodBsonSerializer))]
        [JsonConverter(typeof(ReportingPeriodStringSerializerBackedJsonConverter))]
        public ReportingPeriod GenericUnbounded { get; set; }

        public static bool operator ==(
                ReportingPeriodModelWrapper left,
                ReportingPeriodModelWrapper right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            {
                return false;
            }

            var result =
                (left.UnitOfTime == right.UnitOfTime) &&
                (left.CalendarUnitOfTime == right.CalendarUnitOfTime) &&
                (left.CalendarDay == right.CalendarDay) &&
                (left.CalendarMonth == right.CalendarMonth) &&
                (left.CalendarQuarter == right.CalendarQuarter) &&
                (left.CalendarYear == right.CalendarYear) &&
                (left.CalendarUnbounded == right.CalendarUnbounded) &&
                (left.FiscalUnitOfTime == right.FiscalUnitOfTime) &&
                (left.FiscalMonth == right.FiscalMonth) &&
                (left.FiscalQuarter == right.FiscalQuarter) &&
                (left.FiscalYear == right.FiscalYear) &&
                (left.FiscalUnbounded == right.FiscalUnbounded) &&
                (left.GenericUnitOfTime == right.GenericUnitOfTime) &&
                (left.GenericMonth == right.GenericMonth) &&
                (left.GenericQuarter == right.GenericQuarter) &&
                (left.GenericYear == right.GenericYear) &&
                (left.GenericUnbounded == right.GenericUnbounded);

            return result;
        }

        public static bool operator !=(
            ReportingPeriodModelWrapper left,
            ReportingPeriodModelWrapper right)
            => !(left == right);

        public bool Equals(ReportingPeriodModelWrapper other) => this == other;

        public override bool Equals(object obj) => this == (obj as ReportingPeriodModelWrapper);

        [SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations", Justification = ObcSuppressBecause.CA1065_DoNotRaiseExceptionsInUnexpectedLocations_ThrowNotImplementedExceptionWhenForcedToSpecifyMemberThatWillNeverBeUsedInTesting)]
        public override int GetHashCode() => throw new NotImplementedException();
    }

    public class UnitOfTimeModelWrapper : IEquatable<UnitOfTimeModelWrapper>
    {
        [BsonSerializer(typeof(UnitOfTimeStringSerializedBackedRegisteredTypeBsonSerializer<UnitOfTime>))]
        [JsonConverter(typeof(UnitOfTimeStringSerializerBackedJsonConverter))]
        public UnitOfTime UnitOfTime { get; set; }

        [BsonSerializer(typeof(UnitOfTimeStringSerializedBackedRegisteredTypeBsonSerializer<CalendarUnitOfTime>))]
        [JsonConverter(typeof(UnitOfTimeStringSerializerBackedJsonConverter))]
        public CalendarUnitOfTime CalendarUnitOfTime { get; set; }

        [BsonSerializer(typeof(UnitOfTimeStringSerializedBackedRegisteredTypeBsonSerializer<CalendarDay>))]
        [JsonConverter(typeof(UnitOfTimeStringSerializerBackedJsonConverter))]
        public CalendarDay CalendarDay { get; set; }

        [BsonSerializer(typeof(UnitOfTimeStringSerializedBackedRegisteredTypeBsonSerializer<CalendarMonth>))]
        [JsonConverter(typeof(UnitOfTimeStringSerializerBackedJsonConverter))]
        public CalendarMonth CalendarMonth { get; set; }

        [BsonSerializer(typeof(UnitOfTimeStringSerializedBackedRegisteredTypeBsonSerializer<CalendarQuarter>))]
        [JsonConverter(typeof(UnitOfTimeStringSerializerBackedJsonConverter))]
        public CalendarQuarter CalendarQuarter { get; set; }

        [BsonSerializer(typeof(UnitOfTimeStringSerializedBackedRegisteredTypeBsonSerializer<CalendarYear>))]
        [JsonConverter(typeof(UnitOfTimeStringSerializerBackedJsonConverter))]
        public CalendarYear CalendarYear { get; set; }

        [BsonSerializer(typeof(UnitOfTimeStringSerializedBackedRegisteredTypeBsonSerializer<CalendarUnbounded>))]
        [JsonConverter(typeof(UnitOfTimeStringSerializerBackedJsonConverter))]
        public CalendarUnbounded CalendarUnbounded { get; set; }

        [BsonSerializer(typeof(UnitOfTimeStringSerializedBackedRegisteredTypeBsonSerializer<FiscalUnitOfTime>))]
        [JsonConverter(typeof(UnitOfTimeStringSerializerBackedJsonConverter))]
        public FiscalUnitOfTime FiscalUnitOfTime { get; set; }

        [BsonSerializer(typeof(UnitOfTimeStringSerializedBackedRegisteredTypeBsonSerializer<FiscalMonth>))]
        [JsonConverter(typeof(UnitOfTimeStringSerializerBackedJsonConverter))]
        public FiscalMonth FiscalMonth { get; set; }

        [BsonSerializer(typeof(UnitOfTimeStringSerializedBackedRegisteredTypeBsonSerializer<FiscalQuarter>))]
        [JsonConverter(typeof(UnitOfTimeStringSerializerBackedJsonConverter))]
        public FiscalQuarter FiscalQuarter { get; set; }

        [BsonSerializer(typeof(UnitOfTimeStringSerializedBackedRegisteredTypeBsonSerializer<FiscalYear>))]
        [JsonConverter(typeof(UnitOfTimeStringSerializerBackedJsonConverter))]
        public FiscalYear FiscalYear { get; set; }

        [BsonSerializer(typeof(UnitOfTimeStringSerializedBackedRegisteredTypeBsonSerializer<FiscalUnbounded>))]
        [JsonConverter(typeof(UnitOfTimeStringSerializerBackedJsonConverter))]
        public FiscalUnbounded FiscalUnbounded { get; set; }

        [BsonSerializer(typeof(UnitOfTimeStringSerializedBackedRegisteredTypeBsonSerializer<GenericUnitOfTime>))]
        [JsonConverter(typeof(UnitOfTimeStringSerializerBackedJsonConverter))]
        public GenericUnitOfTime GenericUnitOfTime { get; set; }

        [BsonSerializer(typeof(UnitOfTimeStringSerializedBackedRegisteredTypeBsonSerializer<GenericMonth>))]
        [JsonConverter(typeof(UnitOfTimeStringSerializerBackedJsonConverter))]
        public GenericMonth GenericMonth { get; set; }

        [BsonSerializer(typeof(UnitOfTimeStringSerializedBackedRegisteredTypeBsonSerializer<GenericQuarter>))]
        [JsonConverter(typeof(UnitOfTimeStringSerializerBackedJsonConverter))]
        public GenericQuarter GenericQuarter { get; set; }

        [BsonSerializer(typeof(UnitOfTimeStringSerializedBackedRegisteredTypeBsonSerializer<GenericYear>))]
        [JsonConverter(typeof(UnitOfTimeStringSerializerBackedJsonConverter))]
        public GenericYear GenericYear { get; set; }

        [BsonSerializer(typeof(UnitOfTimeStringSerializedBackedRegisteredTypeBsonSerializer<GenericUnbounded>))]
        [JsonConverter(typeof(UnitOfTimeStringSerializerBackedJsonConverter))]
        public GenericUnbounded GenericUnbounded { get; set; }

        public static bool operator ==(
                UnitOfTimeModelWrapper left,
                UnitOfTimeModelWrapper right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            {
                return false;
            }

            var result =
                (left.UnitOfTime == right.UnitOfTime) &&
                (left.CalendarUnitOfTime == right.CalendarUnitOfTime) &&
                (left.CalendarDay == right.CalendarDay) &&
                (left.CalendarMonth == right.CalendarMonth) &&
                (left.CalendarQuarter == right.CalendarQuarter) &&
                (left.CalendarYear == right.CalendarYear) &&
                (left.CalendarUnbounded == right.CalendarUnbounded) &&
                (left.FiscalUnitOfTime == right.FiscalUnitOfTime) &&
                (left.FiscalMonth == right.FiscalMonth) &&
                (left.FiscalQuarter == right.FiscalQuarter) &&
                (left.FiscalYear == right.FiscalYear) &&
                (left.FiscalUnbounded == right.FiscalUnbounded) &&
                (left.GenericUnitOfTime == right.GenericUnitOfTime) &&
                (left.GenericMonth == right.GenericMonth) &&
                (left.GenericQuarter == right.GenericQuarter) &&
                (left.GenericYear == right.GenericYear) &&
                (left.GenericUnbounded == right.GenericUnbounded);

            return result;
        }

        public static bool operator !=(
            UnitOfTimeModelWrapper left,
            UnitOfTimeModelWrapper right)
            => !(left == right);

        public bool Equals(UnitOfTimeModelWrapper other) => this == other;

        public override bool Equals(object obj) => this == (obj as UnitOfTimeModelWrapper);

        [SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations", Justification = ObcSuppressBecause.CA1065_DoNotRaiseExceptionsInUnexpectedLocations_ThrowNotImplementedExceptionWhenForcedToSpecifyMemberThatWillNeverBeUsedInTesting)]
        public override int GetHashCode() => throw new NotImplementedException();
    }

    public class ReportingPeriodStringSerializerBackedJsonConverter : StringSerializerBackedJsonConverter<ReportingPeriod>
    {
        private static readonly ReportingPeriodStringSerializer ReportingPeriodStringSerializer = new ReportingPeriodStringSerializer();

        public ReportingPeriodStringSerializerBackedJsonConverter()
            : base(ReportingPeriodStringSerializer, CanConvertTypeMatchStrategy.TypeToConsiderEqualsRegisteredType)
        {
        }
    }

    public class UnitOfTimeStringSerializerBackedJsonConverter : StringSerializerBackedJsonConverter<UnitOfTime>
    {
        private static readonly UnitOfTimeStringSerializer UnitOfTimeStringSerializer = new UnitOfTimeStringSerializer();

        public UnitOfTimeStringSerializerBackedJsonConverter()
            : base(UnitOfTimeStringSerializer, CanConvertTypeMatchStrategy.TypeToConsiderIsAssignableToRegisteredType)
        {
        }
    }

    public class UnitOfTimeStringSerializedBackedBsonSerializer : StringSerializerBackedBsonSerializer<UnitOfTime>
    {
        private static readonly UnitOfTimeStringSerializer UnitOfTimeStringSerializer = new UnitOfTimeStringSerializer();

        public UnitOfTimeStringSerializedBackedBsonSerializer()
            : base(UnitOfTimeStringSerializer)
        {
        }
    }

    public class UnitOfTimeStringSerializedBackedRegisteredTypeBsonSerializer<T> : RegisteredTypeBsonSerializer<T>
    {
        public UnitOfTimeStringSerializedBackedRegisteredTypeBsonSerializer()
            : base(new UnitOfTimeStringSerializedBackedBsonSerializer())
        {
        }
    }
}
