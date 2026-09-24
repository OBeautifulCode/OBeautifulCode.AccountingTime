// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalendarAlignedFiscalQuarterSchemeTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Test
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using FakeItEasy;
    using FluentAssertions;
    using FluentAssertions.Extensions;
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.AutoFakeItEasy;
    using OBeautifulCode.CodeAnalysis.Recipes;
    using OBeautifulCode.CodeGen.ModelObject.Recipes;
    using OBeautifulCode.Math.Recipes;

    using Xunit;

    using static System.FormattableString;

    [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
    public static partial class CalendarAlignedFiscalQuarterSchemeTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static CalendarAlignedFiscalQuarterSchemeTest()
        {
        }

        [Fact]
        public static void GetCalendarDayPeriod___Should_throw_ArgumentNullException___When_parameter_fiscalQuarter_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<CalendarAlignedFiscalQuarterScheme>();

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCalendarDayPeriod(null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("fiscalQuarter");
        }

        [Fact]
        public static void GetCalendarDayPeriod___Should_return_contiguous_quarters_that_span_the_fiscal_year___When_called()
        {
            // Arrange
            var year = ThreadSafeRandom.Next(1950, 2051);

            var expected1 = new ReportingPeriod(
                new CalendarDay(year, MonthOfYear.January, DayOfMonth.One),
                new CalendarDay(year, MonthOfYear.March, DayOfMonth.ThirtyOne));

            var expected2 = new ReportingPeriod(
                new CalendarDay(year, MonthOfYear.April, DayOfMonth.One),
                new CalendarDay(year, MonthOfYear.June, DayOfMonth.Thirty));

            var expected3 = new ReportingPeriod(
                new CalendarDay(year, MonthOfYear.July, DayOfMonth.One),
                new CalendarDay(year, MonthOfYear.September, DayOfMonth.Thirty));

            var expected4 = new ReportingPeriod(
                new CalendarDay(year, MonthOfYear.October, DayOfMonth.One),
                new CalendarDay(year, MonthOfYear.December, DayOfMonth.ThirtyOne));

            var systemUnderTest = new CalendarAlignedFiscalQuarterScheme();

            // Act
            var actual1 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(year, QuarterNumber.Q1));
            var actual2 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(year, QuarterNumber.Q2));
            var actual3 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(year, QuarterNumber.Q3));
            var actual4 = systemUnderTest.GetCalendarDayPeriod(new FiscalQuarter(year, QuarterNumber.Q4));

            // Assert
            actual1.AsTest().Must().BeEqualTo(expected1);
            actual2.AsTest().Must().BeEqualTo(expected2);
            actual3.AsTest().Must().BeEqualTo(expected3);
            actual4.AsTest().Must().BeEqualTo(expected4);
        }
    }
}