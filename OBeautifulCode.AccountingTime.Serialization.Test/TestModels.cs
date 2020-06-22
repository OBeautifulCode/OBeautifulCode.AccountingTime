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
    using OBeautifulCode.AccountingTime.Serialization.Json;
    using OBeautifulCode.AccountingTime.Serialization.Test.Internal;
    using OBeautifulCode.AccountingTime.Test;

    public static class TestModels
    {
        public static ReportingPeriodModelWrapper GetDummyReportingPeriodModelWrapper()
        {
            var result = new ReportingPeriodModelWrapper
            {
                UnitOfTime = A.Dummy<ReportingPeriod>(),
                CalendarUnitOfTime = TestCommon.GetDummyCalendarReportingPeriod(),
                CalendarDay = TestCommon.GetDummyCalendarDayReportingPeriod(),
                CalendarMonth = TestCommon.GetDummyCalendarMonthReportingPeriod(),
                CalendarQuarter = TestCommon.GetDummyCalendarQuarterReportingPeriod(),
                CalendarUnbounded = TestCommon.GetDummyCalendarUnboundedReportingPeriod(),
                CalendarYear = TestCommon.GetDummyCalendarYearReportingPeriod(),
                FiscalUnitOfTime = TestCommon.GetDummyFiscalReportingPeriod(),
                FiscalMonth = TestCommon.GetDummyFiscalMonthReportingPeriod(),
                FiscalQuarter = TestCommon.GetDummyFiscalQuarterReportingPeriod(),
                FiscalUnbounded = TestCommon.GetDummyFiscalUnboundedReportingPeriod(),
                FiscalYear = TestCommon.GetDummyFiscalYearReportingPeriod(),
                GenericUnitOfTime = TestCommon.GetDummyGenericReportingPeriod(),
                GenericMonth = TestCommon.GetDummyGenericMonthReportingPeriod(),
                GenericQuarter = TestCommon.GetDummyGenericQuarterReportingPeriod(),
                GenericUnbounded = TestCommon.GetDummyGenericUnboundedReportingPeriod(),
                GenericYear = TestCommon.GetDummyGenericYearReportingPeriod(),
            };

            return result;
        }
    }

    public class ReportingPeriodModelWrapper : IEquatable<ReportingPeriodModelWrapper>
    {
        [BsonSerializer(typeof(ReportingPeriodBsonSerializer))]
        [JsonConverter(typeof(ReportingPeriodJsonConverter))]
        public ReportingPeriod UnitOfTime { get; set; }

        [BsonSerializer(typeof(ReportingPeriodBsonSerializer))]
        [JsonConverter(typeof(ReportingPeriodJsonConverter))]
        public ReportingPeriod CalendarUnitOfTime { get; set; }

        [BsonSerializer(typeof(ReportingPeriodBsonSerializer))]
        [JsonConverter(typeof(ReportingPeriodJsonConverter))]
        public ReportingPeriod CalendarDay { get; set; }

        [BsonSerializer(typeof(ReportingPeriodBsonSerializer))]
        [JsonConverter(typeof(ReportingPeriodJsonConverter))]
        public ReportingPeriod CalendarMonth { get; set; }

        [BsonSerializer(typeof(ReportingPeriodBsonSerializer))]
        [JsonConverter(typeof(ReportingPeriodJsonConverter))]
        public ReportingPeriod CalendarQuarter { get; set; }

        [BsonSerializer(typeof(ReportingPeriodBsonSerializer))]
        [JsonConverter(typeof(ReportingPeriodJsonConverter))]
        public ReportingPeriod CalendarYear { get; set; }

        [BsonSerializer(typeof(ReportingPeriodBsonSerializer))]
        [JsonConverter(typeof(ReportingPeriodJsonConverter))]
        public ReportingPeriod CalendarUnbounded { get; set; }

        [BsonSerializer(typeof(ReportingPeriodBsonSerializer))]
        [JsonConverter(typeof(ReportingPeriodJsonConverter))]
        public ReportingPeriod FiscalUnitOfTime { get; set; }

        [BsonSerializer(typeof(ReportingPeriodBsonSerializer))]
        [JsonConverter(typeof(ReportingPeriodJsonConverter))]
        public ReportingPeriod FiscalMonth { get; set; }

        [BsonSerializer(typeof(ReportingPeriodBsonSerializer))]
        [JsonConverter(typeof(ReportingPeriodJsonConverter))]
        public ReportingPeriod FiscalQuarter { get; set; }

        [BsonSerializer(typeof(ReportingPeriodBsonSerializer))]
        [JsonConverter(typeof(ReportingPeriodJsonConverter))]
        public ReportingPeriod FiscalYear { get; set; }

        [BsonSerializer(typeof(ReportingPeriodBsonSerializer))]
        [JsonConverter(typeof(ReportingPeriodJsonConverter))]
        public ReportingPeriod FiscalUnbounded { get; set; }

        [BsonSerializer(typeof(ReportingPeriodBsonSerializer))]
        [JsonConverter(typeof(ReportingPeriodJsonConverter))]
        public ReportingPeriod GenericUnitOfTime { get; set; }

        [BsonSerializer(typeof(ReportingPeriodBsonSerializer))]
        [JsonConverter(typeof(ReportingPeriodJsonConverter))]
        public ReportingPeriod GenericMonth { get; set; }

        [BsonSerializer(typeof(ReportingPeriodBsonSerializer))]
        [JsonConverter(typeof(ReportingPeriodJsonConverter))]
        public ReportingPeriod GenericQuarter { get; set; }

        [BsonSerializer(typeof(ReportingPeriodBsonSerializer))]
        [JsonConverter(typeof(ReportingPeriodJsonConverter))]
        public ReportingPeriod GenericYear { get; set; }

        [BsonSerializer(typeof(ReportingPeriodBsonSerializer))]
        [JsonConverter(typeof(ReportingPeriodJsonConverter))]
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
        [BsonSerializer(typeof(UnitOfTimeBsonSerializer<UnitOfTime>))]
        [JsonConverter(typeof(UnitOfTimeJsonConverter))]
        public UnitOfTime UnitOfTime { get; set; }

        [BsonSerializer(typeof(UnitOfTimeBsonSerializer<CalendarUnitOfTime>))]
        [JsonConverter(typeof(UnitOfTimeJsonConverter))]
        public CalendarUnitOfTime CalendarUnitOfTime { get; set; }

        [BsonSerializer(typeof(UnitOfTimeBsonSerializer<CalendarDay>))]
        [JsonConverter(typeof(UnitOfTimeJsonConverter))]
        public CalendarDay CalendarDay { get; set; }

        [BsonSerializer(typeof(UnitOfTimeBsonSerializer<CalendarMonth>))]
        [JsonConverter(typeof(UnitOfTimeJsonConverter))]
        public CalendarMonth CalendarMonth { get; set; }

        [BsonSerializer(typeof(UnitOfTimeBsonSerializer<CalendarQuarter>))]
        [JsonConverter(typeof(UnitOfTimeJsonConverter))]
        public CalendarQuarter CalendarQuarter { get; set; }

        [BsonSerializer(typeof(UnitOfTimeBsonSerializer<CalendarYear>))]
        [JsonConverter(typeof(UnitOfTimeJsonConverter))]
        public CalendarYear CalendarYear { get; set; }

        [BsonSerializer(typeof(UnitOfTimeBsonSerializer<CalendarUnbounded>))]
        [JsonConverter(typeof(UnitOfTimeJsonConverter))]
        public CalendarUnbounded CalendarUnbounded { get; set; }

        [BsonSerializer(typeof(UnitOfTimeBsonSerializer<FiscalUnitOfTime>))]
        [JsonConverter(typeof(UnitOfTimeJsonConverter))]
        public FiscalUnitOfTime FiscalUnitOfTime { get; set; }

        [BsonSerializer(typeof(UnitOfTimeBsonSerializer<FiscalMonth>))]
        [JsonConverter(typeof(UnitOfTimeJsonConverter))]
        public FiscalMonth FiscalMonth { get; set; }

        [BsonSerializer(typeof(UnitOfTimeBsonSerializer<FiscalQuarter>))]
        [JsonConverter(typeof(UnitOfTimeJsonConverter))]
        public FiscalQuarter FiscalQuarter { get; set; }

        [BsonSerializer(typeof(UnitOfTimeBsonSerializer<FiscalYear>))]
        [JsonConverter(typeof(UnitOfTimeJsonConverter))]
        public FiscalYear FiscalYear { get; set; }

        [BsonSerializer(typeof(UnitOfTimeBsonSerializer<FiscalUnbounded>))]
        [JsonConverter(typeof(UnitOfTimeJsonConverter))]
        public FiscalUnbounded FiscalUnbounded { get; set; }

        [BsonSerializer(typeof(UnitOfTimeBsonSerializer<GenericUnitOfTime>))]
        [JsonConverter(typeof(UnitOfTimeJsonConverter))]
        public GenericUnitOfTime GenericUnitOfTime { get; set; }

        [BsonSerializer(typeof(UnitOfTimeBsonSerializer<GenericMonth>))]
        [JsonConverter(typeof(UnitOfTimeJsonConverter))]
        public GenericMonth GenericMonth { get; set; }

        [BsonSerializer(typeof(UnitOfTimeBsonSerializer<GenericQuarter>))]
        [JsonConverter(typeof(UnitOfTimeJsonConverter))]
        public GenericQuarter GenericQuarter { get; set; }

        [BsonSerializer(typeof(UnitOfTimeBsonSerializer<GenericYear>))]
        [JsonConverter(typeof(UnitOfTimeJsonConverter))]
        public GenericYear GenericYear { get; set; }

        [BsonSerializer(typeof(UnitOfTimeBsonSerializer<GenericUnbounded>))]
        [JsonConverter(typeof(UnitOfTimeJsonConverter))]
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
}
