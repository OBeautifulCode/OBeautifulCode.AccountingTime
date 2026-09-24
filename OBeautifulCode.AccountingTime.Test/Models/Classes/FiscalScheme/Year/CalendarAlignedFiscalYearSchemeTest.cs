// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalendarAlignedFiscalYearSchemeTest.cs" company="OBeautifulCode">
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
    using OBeautifulCode.AutoFakeItEasy;
    using OBeautifulCode.CodeAnalysis.Recipes;
    using OBeautifulCode.CodeGen.ModelObject.Recipes;
    using OBeautifulCode.Math.Recipes;

    using Xunit;

    using static System.FormattableString;

    [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
    public static partial class CalendarAlignedFiscalYearSchemeTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static CalendarAlignedFiscalYearSchemeTest()
        {
        }

        [Fact]
        public static void GetCalendarDayPeriod___Should_return_reporting_period_beginning_January_1_and_ending_December_31_of_specified_year___When_called()
        {
            // Arrange
            var fiscalYear = A.Dummy<FiscalYear>();
            var systemUnderTest = new CalendarAlignedFiscalYearScheme();
            var expectedReportingPeriod = new ReportingPeriod(
                1.January(fiscalYear.Year).ToCalendarDay(),
                31.December(fiscalYear.Year).ToCalendarDay());

            // Act
            var actualReportingPeriod = systemUnderTest.GetCalendarDayPeriod(fiscalYear);

            // Assert
            actualReportingPeriod.Should().Be(expectedReportingPeriod);
        }
    }
}