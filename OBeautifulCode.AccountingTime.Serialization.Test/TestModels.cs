// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestModels.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Serialization.Test
{
    using System;

    using OBeautifulCode.Equality.Recipes;

#pragma warning disable SA1649 // File name should match first type name
    public class AccountingPeriodSystemModel : IEquatable<AccountingPeriodSystemModel>
    {
        public AccountingPeriodSystem AccountingPeriodSystem { get; set; }

        public CalendarYearAccountingPeriodSystem CalendarYearAccountingPeriodSystem { get; set; }

        public FiscalYearAccountingPeriodSystem FiscalYearAccountingPeriodSystem { get; set; }

        public FiftyTwoFiftyThreeWeekAccountingPeriodSystem FiftyTwoFiftyThreeWeekAccountingPeriodSystem { get; set; }

        public static bool operator ==(
            AccountingPeriodSystemModel left,
            AccountingPeriodSystemModel right)
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
                (left.AccountingPeriodSystem == right.AccountingPeriodSystem) &&
                (left.CalendarYearAccountingPeriodSystem == right.CalendarYearAccountingPeriodSystem) &&
                (left.FiscalYearAccountingPeriodSystem == right.FiscalYearAccountingPeriodSystem) &&
                (left.FiftyTwoFiftyThreeWeekAccountingPeriodSystem == right.FiftyTwoFiftyThreeWeekAccountingPeriodSystem);
            return result;
        }

        public static bool operator !=(
            AccountingPeriodSystemModel left,
            AccountingPeriodSystemModel right)
            => !(left == right);

        public bool Equals(AccountingPeriodSystemModel other) => this == other;

        public override bool Equals(object obj) => this == (obj as AccountingPeriodSystemModel);

        public override int GetHashCode() =>
            HashCodeHelper.Initialize()
                .Hash(this.AccountingPeriodSystem)
                .Hash(this.CalendarYearAccountingPeriodSystem)
                .Hash(this.FiscalYearAccountingPeriodSystem)
                .Hash(this.FiftyTwoFiftyThreeWeekAccountingPeriodSystem)
                .Value;
    }

    public class UnitOfTimeModel : IEquatable<UnitOfTimeModel>
    {
        public UnitOfTime UnitOfTime { get; set; }

        public CalendarUnitOfTime CalendarUnitOfTime { get; set; }

        public CalendarDay CalendarDay { get; set; }

        public CalendarMonth CalendarMonth { get; set; }

        public CalendarQuarter CalendarQuarter { get; set; }

        public CalendarYear CalendarYear { get; set; }

        public CalendarUnbounded CalendarUnbounded { get; set; }

        public FiscalUnitOfTime FiscalUnitOfTime { get; set; }

        public FiscalMonth FiscalMonth { get; set; }

        public FiscalQuarter FiscalQuarter { get; set; }

        public FiscalYear FiscalYear { get; set; }

        public FiscalUnbounded FiscalUnbounded { get; set; }

        public GenericUnitOfTime GenericUnitOfTime { get; set; }

        public GenericMonth GenericMonth { get; set; }

        public GenericQuarter GenericQuarter { get; set; }

        public GenericYear GenericYear { get; set; }

        public GenericUnbounded GenericUnbounded { get; set; }

        public static bool operator ==(
            UnitOfTimeModel left,
            UnitOfTimeModel right)
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
            UnitOfTimeModel left,
            UnitOfTimeModel right)
            => !(left == right);

        public bool Equals(UnitOfTimeModel other) => this == other;

        public override bool Equals(object obj) => this == (obj as UnitOfTimeModel);

        public override int GetHashCode() =>
            HashCodeHelper.Initialize()
                .Hash(this.UnitOfTime)
                .Hash(this.CalendarUnitOfTime)
                .Hash(this.CalendarDay)
                .Hash(this.CalendarMonth)
                .Hash(this.CalendarQuarter)
                .Hash(this.CalendarYear)
                .Hash(this.CalendarUnbounded)
                .Hash(this.FiscalUnitOfTime)
                .Hash(this.FiscalMonth)
                .Hash(this.FiscalQuarter)
                .Hash(this.FiscalYear)
                .Hash(this.FiscalUnbounded)
                .Hash(this.GenericUnitOfTime)
                .Hash(this.GenericMonth)
                .Hash(this.GenericQuarter)
                .Hash(this.GenericYear)
                .Hash(this.GenericUnbounded)
                .Value;
    }

    public class ReportingPeriodModel : IEquatable<ReportingPeriodModel>
    {
        public ReportingPeriod<UnitOfTime> UnitOfTime { get; set; }

        public ReportingPeriod<CalendarUnitOfTime> CalendarUnitOfTime { get; set; }

        public ReportingPeriod<CalendarDay> CalendarDay { get; set; }

        public ReportingPeriod<CalendarMonth> CalendarMonth { get; set; }

        public ReportingPeriod<CalendarQuarter> CalendarQuarter { get; set; }

        public ReportingPeriod<CalendarYear> CalendarYear { get; set; }

        public ReportingPeriod<CalendarUnbounded> CalendarUnbounded { get; set; }

        public ReportingPeriod<FiscalUnitOfTime> FiscalUnitOfTime { get; set; }

        public ReportingPeriod<FiscalMonth> FiscalMonth { get; set; }

        public ReportingPeriod<FiscalQuarter> FiscalQuarter { get; set; }

        public ReportingPeriod<FiscalYear> FiscalYear { get; set; }

        public ReportingPeriod<FiscalUnbounded> FiscalUnbounded { get; set; }

        public ReportingPeriod<GenericUnitOfTime> GenericUnitOfTime { get; set; }

        public ReportingPeriod<GenericMonth> GenericMonth { get; set; }

        public ReportingPeriod<GenericQuarter> GenericQuarter { get; set; }

        public ReportingPeriod<GenericYear> GenericYear { get; set; }

        public ReportingPeriod<GenericUnbounded> GenericUnbounded { get; set; }

        public static bool operator ==(
            ReportingPeriodModel left,
            ReportingPeriodModel right)
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
            ReportingPeriodModel left,
            ReportingPeriodModel right)
            => !(left == right);

        public bool Equals(ReportingPeriodModel other) => this == other;

        public override bool Equals(object obj) => this == (obj as ReportingPeriodModel);

        public override int GetHashCode() =>
            HashCodeHelper.Initialize()
                .Hash(this.UnitOfTime)
                .Hash(this.CalendarUnitOfTime)
                .Hash(this.CalendarDay)
                .Hash(this.CalendarMonth)
                .Hash(this.CalendarQuarter)
                .Hash(this.CalendarYear)
                .Hash(this.CalendarUnbounded)
                .Hash(this.FiscalUnitOfTime)
                .Hash(this.FiscalMonth)
                .Hash(this.FiscalQuarter)
                .Hash(this.FiscalYear)
                .Hash(this.FiscalUnbounded)
                .Hash(this.GenericUnitOfTime)
                .Hash(this.GenericMonth)
                .Hash(this.GenericQuarter)
                .Hash(this.GenericYear)
                .Hash(this.GenericUnbounded)
                .Value;
    }

    // ReSharper disable once InconsistentNaming
    public class IReportingPeriodModel : IEquatable<IReportingPeriodModel>
    {
        public IReportingPeriod<UnitOfTime> UnitOfTime { get; set; }

        public IReportingPeriod<CalendarUnitOfTime> CalendarUnitOfTime { get; set; }

        public IReportingPeriod<CalendarDay> CalendarDay { get; set; }

        public IReportingPeriod<CalendarMonth> CalendarMonth { get; set; }

        public IReportingPeriod<CalendarQuarter> CalendarQuarter { get; set; }

        public IReportingPeriod<CalendarYear> CalendarYear { get; set; }

        public IReportingPeriod<CalendarUnbounded> CalendarUnbounded { get; set; }

        public IReportingPeriod<FiscalUnitOfTime> FiscalUnitOfTime { get; set; }

        public IReportingPeriod<FiscalMonth> FiscalMonth { get; set; }

        public IReportingPeriod<FiscalQuarter> FiscalQuarter { get; set; }

        public IReportingPeriod<FiscalYear> FiscalYear { get; set; }

        public IReportingPeriod<FiscalUnbounded> FiscalUnbounded { get; set; }

        public IReportingPeriod<GenericUnitOfTime> GenericUnitOfTime { get; set; }

        public IReportingPeriod<GenericMonth> GenericMonth { get; set; }

        public IReportingPeriod<GenericQuarter> GenericQuarter { get; set; }

        public IReportingPeriod<GenericYear> GenericYear { get; set; }

        public IReportingPeriod<GenericUnbounded> GenericUnbounded { get; set; }

        public static bool operator ==(
            IReportingPeriodModel left,
            IReportingPeriodModel right)
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
                AreEqual(left.UnitOfTime, right.UnitOfTime) &&
                AreEqual(left.CalendarUnitOfTime, right.CalendarUnitOfTime) &&
                AreEqual(left.CalendarDay, right.CalendarDay) &&
                AreEqual(left.CalendarMonth, right.CalendarMonth) &&
                AreEqual(left.CalendarQuarter, right.CalendarQuarter) &&
                AreEqual(left.CalendarYear, right.CalendarYear) &&
                AreEqual(left.CalendarUnbounded, right.CalendarUnbounded) &&
                AreEqual(left.FiscalUnitOfTime, right.FiscalUnitOfTime) &&
                AreEqual(left.FiscalMonth, right.FiscalMonth) &&
                AreEqual(left.FiscalQuarter, right.FiscalQuarter) &&
                AreEqual(left.FiscalYear, right.FiscalYear) &&
                AreEqual(left.FiscalUnbounded, right.FiscalUnbounded) &&
                AreEqual(left.GenericUnitOfTime, right.GenericUnitOfTime) &&
                AreEqual(left.GenericMonth, right.GenericMonth) &&
                AreEqual(left.GenericQuarter, right.GenericQuarter) &&
                AreEqual(left.GenericYear, right.GenericYear) &&
                AreEqual(left.GenericUnbounded, right.GenericUnbounded);

            return result;
        }

        public static bool operator !=(
            IReportingPeriodModel left,
            IReportingPeriodModel right)
            => !(left == right);

        public bool Equals(IReportingPeriodModel other) => this == other;

        public override bool Equals(object obj) => this == (obj as IReportingPeriodModel);

        public override int GetHashCode() =>
            HashCodeHelper.Initialize()
                .Hash(this.UnitOfTime)
                .Hash(this.CalendarUnitOfTime)
                .Hash(this.CalendarDay)
                .Hash(this.CalendarMonth)
                .Hash(this.CalendarQuarter)
                .Hash(this.CalendarYear)
                .Hash(this.CalendarUnbounded)
                .Hash(this.FiscalUnitOfTime)
                .Hash(this.FiscalMonth)
                .Hash(this.FiscalQuarter)
                .Hash(this.FiscalYear)
                .Hash(this.FiscalUnbounded)
                .Hash(this.GenericUnitOfTime)
                .Hash(this.GenericMonth)
                .Hash(this.GenericQuarter)
                .Hash(this.GenericYear)
                .Hash(this.GenericUnbounded)
                .Value;

        private static bool AreEqual(
            IReportingPeriod<UnitOfTime> first,
            IReportingPeriod<UnitOfTime> second)
        {
            if ((first == null) || (second == null))
            {
                if ((first == null) && (second == null))
                {
                    return true;
                }

                return false;
            }

            var result = first.Equals(second);

            return result;
        }
    }
#pragma warning restore SA1649 // File name should match first type name
}
