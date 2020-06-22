// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Common.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using FakeItEasy;

    using OBeautifulCode.AutoFakeItEasy;

    public static class Common
    {
        public static readonly Type[] AllUnitOfTimeTypesExceptUnitOfTime = TypeHelper.GetAllUnitOfTimeTypes().Except(new[] { typeof(UnitOfTime) }).ToArray();

        public static IReadOnlyCollection<UnitOfTime> GetDummyOfEachUnitOfTimeKind()
        {
            var result = TypeHelper.GetAllUnitOfTimeTypes().Where(_ => !_.IsAbstract).Select(_ => (UnitOfTime)AD.ummy(_)).ToList();

            return result;
        }

        public static ReportingPeriod GetDummyCalendarReportingPeriod()
        {
            var result = A.Dummy<ReportingPeriod>().Whose(_ => _.GetUnitOfTimeKind() == UnitOfTimeKind.Calendar);

            return result;
        }

        public static ReportingPeriod GetDummyFiscalReportingPeriod()
        {
            var result = A.Dummy<ReportingPeriod>().Whose(_ => _.GetUnitOfTimeKind() == UnitOfTimeKind.Fiscal);

            return result;
        }

        public static ReportingPeriod GetDummyGenericReportingPeriod()
        {
            var result = A.Dummy<ReportingPeriod>().Whose(_ => _.GetUnitOfTimeKind() == UnitOfTimeKind.Generic);

            return result;
        }

        public static ReportingPeriod GetDummyCalendarDayReportingPeriod()
        {
            var result = A.Dummy<ReportingPeriod>().Whose(_ => (_.GetUnitOfTimeKind() == UnitOfTimeKind.Calendar) && (!_.HasComponentWithUnboundedGranularity()) && (_.GetUnitOfTimeGranularity() == UnitOfTimeGranularity.Day));

            return result;
        }

        public static ReportingPeriod GetDummyCalendarMonthReportingPeriod()
        {
            var result = A.Dummy<ReportingPeriod>().Whose(_ => (_.GetUnitOfTimeKind() == UnitOfTimeKind.Calendar) && (!_.HasComponentWithUnboundedGranularity()) && (_.GetUnitOfTimeGranularity() == UnitOfTimeGranularity.Month));

            return result;
        }

        public static ReportingPeriod GetDummyCalendarQuarterReportingPeriod()
        {
            var result = A.Dummy<ReportingPeriod>().Whose(_ => (_.GetUnitOfTimeKind() == UnitOfTimeKind.Calendar) && (!_.HasComponentWithUnboundedGranularity()) && (_.GetUnitOfTimeGranularity() == UnitOfTimeGranularity.Quarter));

            return result;
        }

        public static ReportingPeriod GetDummyCalendarYearReportingPeriod()
        {
            var result = A.Dummy<ReportingPeriod>().Whose(_ => (_.GetUnitOfTimeKind() == UnitOfTimeKind.Calendar) && (!_.HasComponentWithUnboundedGranularity()) && (_.GetUnitOfTimeGranularity() == UnitOfTimeGranularity.Year));

            return result;
        }

        public static ReportingPeriod GetDummyCalendarUnboundedReportingPeriod()
        {
            var result = new ReportingPeriod(new CalendarUnbounded(), new CalendarUnbounded());

            return result;
        }

        public static ReportingPeriod GetDummyFiscalMonthReportingPeriod()
        {
            var result = A.Dummy<ReportingPeriod>().Whose(_ => (_.GetUnitOfTimeKind() == UnitOfTimeKind.Fiscal) && (!_.HasComponentWithUnboundedGranularity()) && (_.GetUnitOfTimeGranularity() == UnitOfTimeGranularity.Month));

            return result;
        }

        public static ReportingPeriod GetDummyFiscalQuarterReportingPeriod()
        {
            var result = A.Dummy<ReportingPeriod>().Whose(_ => (_.GetUnitOfTimeKind() == UnitOfTimeKind.Fiscal) && (!_.HasComponentWithUnboundedGranularity()) && (_.GetUnitOfTimeGranularity() == UnitOfTimeGranularity.Quarter));

            return result;
        }

        public static ReportingPeriod GetDummyFiscalYearReportingPeriod()
        {
            var result = A.Dummy<ReportingPeriod>().Whose(_ => (_.GetUnitOfTimeKind() == UnitOfTimeKind.Fiscal) && (!_.HasComponentWithUnboundedGranularity()) && (_.GetUnitOfTimeGranularity() == UnitOfTimeGranularity.Year));

            return result;
        }

        public static ReportingPeriod GetDummyFiscalUnboundedReportingPeriod()
        {
            var result = new ReportingPeriod(new FiscalUnbounded(), new FiscalUnbounded());

            return result;
        }

        public static ReportingPeriod GetDummyGenericMonthReportingPeriod()
        {
            var result = A.Dummy<ReportingPeriod>().Whose(_ => (_.GetUnitOfTimeKind() == UnitOfTimeKind.Generic) && (!_.HasComponentWithUnboundedGranularity()) && (_.GetUnitOfTimeGranularity() == UnitOfTimeGranularity.Month));

            return result;
        }

        public static ReportingPeriod GetDummyGenericQuarterReportingPeriod()
        {
            var result = A.Dummy<ReportingPeriod>().Whose(_ => (_.GetUnitOfTimeKind() == UnitOfTimeKind.Generic) && (!_.HasComponentWithUnboundedGranularity()) && (_.GetUnitOfTimeGranularity() == UnitOfTimeGranularity.Quarter));

            return result;
        }

        public static ReportingPeriod GetDummyGenericYearReportingPeriod()
        {
            var result = A.Dummy<ReportingPeriod>().Whose(_ => (_.GetUnitOfTimeKind() == UnitOfTimeKind.Generic) && (!_.HasComponentWithUnboundedGranularity()) && (_.GetUnitOfTimeGranularity() == UnitOfTimeGranularity.Year));

            return result;
        }

        public static ReportingPeriod GetDummyGenericUnboundedReportingPeriod()
        {
            var result = new ReportingPeriod(new GenericUnbounded(), new GenericUnbounded());

            return result;
        }
    }
}
