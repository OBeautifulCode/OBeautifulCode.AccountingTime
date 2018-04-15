// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountingTimePropertyBagConfiguration.cs" company="OBeautifulCode">
//    Copyright (c) OBeautifulCode 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Serialization.PropertyBag
{
    using System;
    using System.Collections.Generic;

    using Naos.Serialization.Domain;
    using Naos.Serialization.PropertyBag;

    /// <summary>
    /// Represents the start, end, or both, of a reporting period.
    /// </summary>
    public class AccountingTimePropertyBagConfiguration : PropertyBagConfigurationBase
    {
        /// <inheritdoc />
        protected override IReadOnlyDictionary<Type, IStringSerializeAndDeserialize> CustomTypeToSerializerMappings()
        {
            var reportingPeriodStringSerializer = new ReportingPeriodStringSerializer();
            var unitOfTimeStringSerializer = new UnitOfTimeStringSerializer();
            var ret = new Dictionary<Type, IStringSerializeAndDeserialize>
                          {
                              { typeof(IReportingPeriod<UnitOfTime>), reportingPeriodStringSerializer },
                              { typeof(IReportingPeriod<CalendarDay>), reportingPeriodStringSerializer },
                              { typeof(IReportingPeriod<CalendarMonth>), reportingPeriodStringSerializer },
                              { typeof(IReportingPeriod<CalendarQuarter>), reportingPeriodStringSerializer },
                              { typeof(IReportingPeriod<CalendarUnbounded>), reportingPeriodStringSerializer },
                              { typeof(IReportingPeriod<CalendarUnitOfTime>), reportingPeriodStringSerializer },
                              { typeof(IReportingPeriod<CalendarYear>), reportingPeriodStringSerializer },
                              { typeof(IReportingPeriod<FiscalMonth>), reportingPeriodStringSerializer },
                              { typeof(IReportingPeriod<FiscalQuarter>), reportingPeriodStringSerializer },
                              { typeof(IReportingPeriod<FiscalUnbounded>), reportingPeriodStringSerializer },
                              { typeof(IReportingPeriod<FiscalUnitOfTime>), reportingPeriodStringSerializer },
                              { typeof(IReportingPeriod<FiscalYear>), reportingPeriodStringSerializer },
                              { typeof(IReportingPeriod<GenericMonth>), reportingPeriodStringSerializer },
                              { typeof(IReportingPeriod<GenericQuarter>), reportingPeriodStringSerializer },
                              { typeof(IReportingPeriod<GenericUnbounded>), reportingPeriodStringSerializer },
                              { typeof(IReportingPeriod<GenericUnitOfTime>), reportingPeriodStringSerializer },
                              { typeof(IReportingPeriod<GenericYear>), reportingPeriodStringSerializer },
                              { typeof(ReportingPeriod<UnitOfTime>), reportingPeriodStringSerializer },
                              { typeof(ReportingPeriod<CalendarDay>), reportingPeriodStringSerializer },
                              { typeof(ReportingPeriod<CalendarMonth>), reportingPeriodStringSerializer },
                              { typeof(ReportingPeriod<CalendarQuarter>), reportingPeriodStringSerializer },
                              { typeof(ReportingPeriod<CalendarUnbounded>), reportingPeriodStringSerializer },
                              { typeof(ReportingPeriod<CalendarUnitOfTime>), reportingPeriodStringSerializer },
                              { typeof(ReportingPeriod<CalendarYear>), reportingPeriodStringSerializer },
                              { typeof(ReportingPeriod<FiscalMonth>), reportingPeriodStringSerializer },
                              { typeof(ReportingPeriod<FiscalQuarter>), reportingPeriodStringSerializer },
                              { typeof(ReportingPeriod<FiscalUnbounded>), reportingPeriodStringSerializer },
                              { typeof(ReportingPeriod<FiscalUnitOfTime>), reportingPeriodStringSerializer },
                              { typeof(ReportingPeriod<FiscalYear>), reportingPeriodStringSerializer },
                              { typeof(ReportingPeriod<GenericMonth>), reportingPeriodStringSerializer },
                              { typeof(ReportingPeriod<GenericQuarter>), reportingPeriodStringSerializer },
                              { typeof(ReportingPeriod<GenericUnbounded>), reportingPeriodStringSerializer },
                              { typeof(ReportingPeriod<GenericUnitOfTime>), reportingPeriodStringSerializer },
                              { typeof(ReportingPeriod<GenericYear>), reportingPeriodStringSerializer },
                              { typeof(UnitOfTime), unitOfTimeStringSerializer },
                              { typeof(CalendarDay), unitOfTimeStringSerializer },
                              { typeof(CalendarMonth), unitOfTimeStringSerializer },
                              { typeof(CalendarQuarter), unitOfTimeStringSerializer },
                              { typeof(CalendarUnbounded), unitOfTimeStringSerializer },
                              { typeof(CalendarUnitOfTime), unitOfTimeStringSerializer },
                              { typeof(CalendarYear), unitOfTimeStringSerializer },
                              { typeof(DayOfMonth), unitOfTimeStringSerializer },
                              { typeof(FiscalMonth), unitOfTimeStringSerializer },
                              { typeof(FiscalQuarter), unitOfTimeStringSerializer },
                              { typeof(FiscalUnbounded), unitOfTimeStringSerializer },
                              { typeof(FiscalUnitOfTime), unitOfTimeStringSerializer },
                              { typeof(FiscalYear), unitOfTimeStringSerializer },
                              { typeof(GenericMonth), unitOfTimeStringSerializer },
                              { typeof(GenericQuarter), unitOfTimeStringSerializer },
                              { typeof(GenericUnbounded), unitOfTimeStringSerializer },
                              { typeof(GenericUnitOfTime), unitOfTimeStringSerializer },
                              { typeof(GenericYear), unitOfTimeStringSerializer },
                          };
            return ret;
        }
    }
}
