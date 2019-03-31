// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FiftyTwoFiftyThreeWeekAccountingPeriodSystemTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Test
{
    using System;
    using System.Linq;

    using FakeItEasy;

    using FluentAssertions;
    using FluentAssertions.Extensions;

    using OBeautifulCode.AutoFakeItEasy;

    using Xunit;

    public static class FiftyTwoFiftyThreeWeekAccountingPeriodSystemTest
    {
        private static readonly FiftyTwoFiftyThreeWeekAccountingPeriodSystem ObjectForEquatableTests = A.Dummy<FiftyTwoFiftyThreeWeekAccountingPeriodSystem>();

        private static readonly FiftyTwoFiftyThreeWeekAccountingPeriodSystem ObjectThatIsEqualButNotTheSameAsObjectForEquatableTests =
            new FiftyTwoFiftyThreeWeekAccountingPeriodSystem(ObjectForEquatableTests.LastDayOfWeekInAccountingYear, ObjectForEquatableTests.AnchorMonth, ObjectForEquatableTests.FiftyTwoFiftyThreeWeekMethodology);

        private static readonly FiftyTwoFiftyThreeWeekAccountingPeriodSystem[] ObjectsThatAreNotEqualToObjectForEquatableTests =
        {
            new FiftyTwoFiftyThreeWeekAccountingPeriodSystem(A.Dummy<DayOfWeek>().ThatIsNot(ObjectForEquatableTests.LastDayOfWeekInAccountingYear), ObjectForEquatableTests.AnchorMonth, ObjectForEquatableTests.FiftyTwoFiftyThreeWeekMethodology),
            new FiftyTwoFiftyThreeWeekAccountingPeriodSystem(ObjectForEquatableTests.LastDayOfWeekInAccountingYear, A.Dummy<MonthOfYear>().ThatIsNot(ObjectForEquatableTests.AnchorMonth), ObjectForEquatableTests.FiftyTwoFiftyThreeWeekMethodology),
            new FiftyTwoFiftyThreeWeekAccountingPeriodSystem(ObjectForEquatableTests.LastDayOfWeekInAccountingYear, ObjectForEquatableTests.AnchorMonth, A.Dummy<FiftyTwoFiftyThreeWeekMethodology>().ThatIsNot(ObjectForEquatableTests.FiftyTwoFiftyThreeWeekMethodology)),
        };

        private static readonly AccountingPeriodSystem ObjectThatIsNotTheSameTypeAsObjectForEquatableTests = A.Dummy<FiscalYearAccountingPeriodSystem>();

        [Fact]
        public static void Constructor___Should_throw_ArgumentOutOfRangeException___When_parameter_anchorMonth_is_Invalid()
        {
            // Arrange
            var expectedLastDayOfWeek = A.Dummy<DayOfWeek>();
            var methodology = A.Dummy<FiftyTwoFiftyThreeWeekMethodology>();

            // Act
            var ex = Record.Exception(() => new FiftyTwoFiftyThreeWeekAccountingPeriodSystem(expectedLastDayOfWeek, MonthOfYear.Invalid, methodology));

            // Assert
            ex.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void Constructor___Should_throw_ArgumentOutOfRangeException___When_parameter_fiftyTwoFiftyThreeWeekMethodology_is_Unknown()
        {
            // Arrange
            var expectedLastDayOfWeek = A.Dummy<DayOfWeek>();
            var monthOfYear = A.Dummy<MonthOfYear>();

            // Act
            var ex = Record.Exception(() => new FiftyTwoFiftyThreeWeekAccountingPeriodSystem(expectedLastDayOfWeek, monthOfYear, FiftyTwoFiftyThreeWeekMethodology.Unknown));

            // Assert
            ex.Should().BeOfType<ArgumentOutOfRangeException>();
            ex.Message.Should().Contain("fiftyTwoFiftyThreeWeekMethodology");
        }

        [Fact]
        public static void LastDayOfWeekInAccountingYear___Should_return_same_lastDayOfWeekInAccountingYear_passed_to_constructor___When_getting()
        {
            // Arrange
            var expectedLastDayOfWeek = A.Dummy<DayOfWeek>();
            var anchorMonth = A.Dummy<MonthOfYear>();
            var methodology = A.Dummy<FiftyTwoFiftyThreeWeekMethodology>();
            var systemUnderTest = new FiftyTwoFiftyThreeWeekAccountingPeriodSystem(expectedLastDayOfWeek, anchorMonth, methodology);

            // Act
            var actualLastDayOfWeek = systemUnderTest.LastDayOfWeekInAccountingYear;

            // Assert
            actualLastDayOfWeek.Should().Be(expectedLastDayOfWeek);
        }

        [Fact]
        public static void AnchorMonth___Should_return_same_lastDayOfWeekInAccountingYear_passed_to_constructor___When_getting()
        {
            // Arrange
            var lastDayOfWeek = A.Dummy<DayOfWeek>();
            var expectedAnchorMonth = A.Dummy<MonthOfYear>();
            var methodology = A.Dummy<FiftyTwoFiftyThreeWeekMethodology>();
            var systemUnderTest = new FiftyTwoFiftyThreeWeekAccountingPeriodSystem(lastDayOfWeek, expectedAnchorMonth, methodology);

            // Act
            var actualAnchorMonth = systemUnderTest.AnchorMonth;

            // Assert
            actualAnchorMonth.Should().Be(expectedAnchorMonth);
        }

        [Fact]
        public static void FiftyTwoFiftyThreeWeekMethodology___Should_return_same_lastDayOfWeekInAccountingYear_passed_to_constructor___When_getting()
        {
            // Arrange
            var lastDayOfWeek = A.Dummy<DayOfWeek>();
            var anchorMonth = A.Dummy<MonthOfYear>();
            var expectedMethodology = A.Dummy<FiftyTwoFiftyThreeWeekMethodology>();
            var systemUnderTest = new FiftyTwoFiftyThreeWeekAccountingPeriodSystem(lastDayOfWeek, anchorMonth, expectedMethodology);

            // Act
            var actualMethodology = systemUnderTest.FiftyTwoFiftyThreeWeekMethodology;

            // Assert
            actualMethodology.Should().Be(expectedMethodology);
        }

        [Fact]
        public static void GetReportingPeriodForFiscalYear___Should_return_reporting_period_that_ends_on_chosen_day_of_week_that_last_occurs_in_anchor_month_and_starts_on_day_after_last_years_ending_date___When_methodology_is_LastOccurrenceInAnchorMonth()
        {
            // Arrange
            var systemUnderTest = new FiftyTwoFiftyThreeWeekAccountingPeriodSystem(DayOfWeek.Saturday, MonthOfYear.August, FiftyTwoFiftyThreeWeekMethodology.LastOccurrenceInAnchorMonth);

            var expectedReportingPeriod1 = new ReportingPeriod<CalendarDay>(27.August(2006).ToCalendarDay(), 25.August(2007).ToCalendarDay());
            var expectedReportingPeriod2 = new ReportingPeriod<CalendarDay>(26.August(2007).ToCalendarDay(), 30.August(2008).ToCalendarDay());
            var expectedReportingPeriod3 = new ReportingPeriod<CalendarDay>(31.August(2008).ToCalendarDay(), 29.August(2009).ToCalendarDay());
            var expectedReportingPeriod4 = new ReportingPeriod<CalendarDay>(30.August(2009).ToCalendarDay(), 28.August(2010).ToCalendarDay());
            var expectedReportingPeriod5 = new ReportingPeriod<CalendarDay>(29.August(2010).ToCalendarDay(), 27.August(2011).ToCalendarDay());
            var expectedReportingPeriod6 = new ReportingPeriod<CalendarDay>(28.August(2011).ToCalendarDay(), 25.August(2012).ToCalendarDay());
            var expectedReportingPeriod7 = new ReportingPeriod<CalendarDay>(26.August(2012).ToCalendarDay(), 31.August(2013).ToCalendarDay());
            var expectedReportingPeriod8 = new ReportingPeriod<CalendarDay>(1.September(2013).ToCalendarDay(), 30.August(2014).ToCalendarDay());
            var expectedReportingPeriod9 = new ReportingPeriod<CalendarDay>(31.August(2014).ToCalendarDay(), 29.August(2015).ToCalendarDay());
            var expectedReportingPeriod10 = new ReportingPeriod<CalendarDay>(30.August(2015).ToCalendarDay(), 27.August(2016).ToCalendarDay());
            var expectedReportingPeriod11 = new ReportingPeriod<CalendarDay>(28.August(2016).ToCalendarDay(), 26.August(2017).ToCalendarDay());
            var expectedReportingPeriod12 = new ReportingPeriod<CalendarDay>(27.August(2017).ToCalendarDay(), 25.August(2018).ToCalendarDay());
            var expectedReportingPeriod13 = new ReportingPeriod<CalendarDay>(26.August(2018).ToCalendarDay(), 31.August(2019).ToCalendarDay());

            // Act
            var actualReportingPeriod1 = systemUnderTest.GetReportingPeriodForFiscalYear(new FiscalYear(2007));
            var actualReportingPeriod2 = systemUnderTest.GetReportingPeriodForFiscalYear(new FiscalYear(2008));
            var actualReportingPeriod3 = systemUnderTest.GetReportingPeriodForFiscalYear(new FiscalYear(2009));
            var actualReportingPeriod4 = systemUnderTest.GetReportingPeriodForFiscalYear(new FiscalYear(2010));
            var actualReportingPeriod5 = systemUnderTest.GetReportingPeriodForFiscalYear(new FiscalYear(2011));
            var actualReportingPeriod6 = systemUnderTest.GetReportingPeriodForFiscalYear(new FiscalYear(2012));
            var actualReportingPeriod7 = systemUnderTest.GetReportingPeriodForFiscalYear(new FiscalYear(2013));
            var actualReportingPeriod8 = systemUnderTest.GetReportingPeriodForFiscalYear(new FiscalYear(2014));
            var actualReportingPeriod9 = systemUnderTest.GetReportingPeriodForFiscalYear(new FiscalYear(2015));
            var actualReportingPeriod10 = systemUnderTest.GetReportingPeriodForFiscalYear(new FiscalYear(2016));
            var actualReportingPeriod11 = systemUnderTest.GetReportingPeriodForFiscalYear(new FiscalYear(2017));
            var actualReportingPeriod12 = systemUnderTest.GetReportingPeriodForFiscalYear(new FiscalYear(2018));
            var actualReportingPeriod13 = systemUnderTest.GetReportingPeriodForFiscalYear(new FiscalYear(2019));

            // Assert
            actualReportingPeriod1.Should().Be(expectedReportingPeriod1);
            actualReportingPeriod2.Should().Be(expectedReportingPeriod2);
            actualReportingPeriod3.Should().Be(expectedReportingPeriod3);
            actualReportingPeriod4.Should().Be(expectedReportingPeriod4);
            actualReportingPeriod5.Should().Be(expectedReportingPeriod5);
            actualReportingPeriod6.Should().Be(expectedReportingPeriod6);
            actualReportingPeriod7.Should().Be(expectedReportingPeriod7);
            actualReportingPeriod8.Should().Be(expectedReportingPeriod8);
            actualReportingPeriod9.Should().Be(expectedReportingPeriod9);
            actualReportingPeriod10.Should().Be(expectedReportingPeriod10);
            actualReportingPeriod11.Should().Be(expectedReportingPeriod11);
            actualReportingPeriod12.Should().Be(expectedReportingPeriod12);
            actualReportingPeriod13.Should().Be(expectedReportingPeriod13);
        }

        [Fact]
        public static void GetReportingPeriodForFiscalYear___Should_return_reporting_period_that_ends_on_chosen_day_of_week_that_is_nearest_to_last_day_of_anchor_month_and_starts_on_day_after_last_years_ending_date___When_methodology_is_ClosestToLastDayOfAnchorMonth()
        {
            // Arrange
            var systemUnderTest = new FiftyTwoFiftyThreeWeekAccountingPeriodSystem(DayOfWeek.Saturday, MonthOfYear.August, FiftyTwoFiftyThreeWeekMethodology.ClosestToLastDayOfAnchorMonth);

            var expectedReportingPeriod1 = new ReportingPeriod<CalendarDay>(3.September(2006).ToCalendarDay(), 1.September(2007).ToCalendarDay());
            var expectedReportingPeriod2 = new ReportingPeriod<CalendarDay>(2.September(2007).ToCalendarDay(), 30.August(2008).ToCalendarDay());
            var expectedReportingPeriod3 = new ReportingPeriod<CalendarDay>(31.August(2008).ToCalendarDay(), 29.August(2009).ToCalendarDay());
            var expectedReportingPeriod4 = new ReportingPeriod<CalendarDay>(30.August(2009).ToCalendarDay(), 28.August(2010).ToCalendarDay());
            var expectedReportingPeriod5 = new ReportingPeriod<CalendarDay>(29.August(2010).ToCalendarDay(), 3.September(2011).ToCalendarDay());
            var expectedReportingPeriod6 = new ReportingPeriod<CalendarDay>(4.September(2011).ToCalendarDay(), 1.September(2012).ToCalendarDay());
            var expectedReportingPeriod7 = new ReportingPeriod<CalendarDay>(2.September(2012).ToCalendarDay(), 31.August(2013).ToCalendarDay());
            var expectedReportingPeriod8 = new ReportingPeriod<CalendarDay>(1.September(2013).ToCalendarDay(), 30.August(2014).ToCalendarDay());
            var expectedReportingPeriod9 = new ReportingPeriod<CalendarDay>(31.August(2014).ToCalendarDay(), 29.August(2015).ToCalendarDay());
            var expectedReportingPeriod10 = new ReportingPeriod<CalendarDay>(30.August(2015).ToCalendarDay(), 3.September(2016).ToCalendarDay());
            var expectedReportingPeriod11 = new ReportingPeriod<CalendarDay>(4.September(2016).ToCalendarDay(), 2.September(2017).ToCalendarDay());
            var expectedReportingPeriod12 = new ReportingPeriod<CalendarDay>(3.September(2017).ToCalendarDay(), 1.September(2018).ToCalendarDay());
            var expectedReportingPeriod13 = new ReportingPeriod<CalendarDay>(2.September(2018).ToCalendarDay(), 31.August(2019).ToCalendarDay());

            // Act
            var actualReportingPeriod1 = systemUnderTest.GetReportingPeriodForFiscalYear(new FiscalYear(2007));
            var actualReportingPeriod2 = systemUnderTest.GetReportingPeriodForFiscalYear(new FiscalYear(2008));
            var actualReportingPeriod3 = systemUnderTest.GetReportingPeriodForFiscalYear(new FiscalYear(2009));
            var actualReportingPeriod4 = systemUnderTest.GetReportingPeriodForFiscalYear(new FiscalYear(2010));
            var actualReportingPeriod5 = systemUnderTest.GetReportingPeriodForFiscalYear(new FiscalYear(2011));
            var actualReportingPeriod6 = systemUnderTest.GetReportingPeriodForFiscalYear(new FiscalYear(2012));
            var actualReportingPeriod7 = systemUnderTest.GetReportingPeriodForFiscalYear(new FiscalYear(2013));
            var actualReportingPeriod8 = systemUnderTest.GetReportingPeriodForFiscalYear(new FiscalYear(2014));
            var actualReportingPeriod9 = systemUnderTest.GetReportingPeriodForFiscalYear(new FiscalYear(2015));
            var actualReportingPeriod10 = systemUnderTest.GetReportingPeriodForFiscalYear(new FiscalYear(2016));
            var actualReportingPeriod11 = systemUnderTest.GetReportingPeriodForFiscalYear(new FiscalYear(2017));
            var actualReportingPeriod12 = systemUnderTest.GetReportingPeriodForFiscalYear(new FiscalYear(2018));
            var actualReportingPeriod13 = systemUnderTest.GetReportingPeriodForFiscalYear(new FiscalYear(2019));

            // Assert
            actualReportingPeriod1.Should().Be(expectedReportingPeriod1);
            actualReportingPeriod2.Should().Be(expectedReportingPeriod2);
            actualReportingPeriod3.Should().Be(expectedReportingPeriod3);
            actualReportingPeriod4.Should().Be(expectedReportingPeriod4);
            actualReportingPeriod5.Should().Be(expectedReportingPeriod5);
            actualReportingPeriod6.Should().Be(expectedReportingPeriod6);
            actualReportingPeriod7.Should().Be(expectedReportingPeriod7);
            actualReportingPeriod8.Should().Be(expectedReportingPeriod8);
            actualReportingPeriod9.Should().Be(expectedReportingPeriod9);
            actualReportingPeriod10.Should().Be(expectedReportingPeriod10);
            actualReportingPeriod11.Should().Be(expectedReportingPeriod11);
            actualReportingPeriod12.Should().Be(expectedReportingPeriod12);
            actualReportingPeriod13.Should().Be(expectedReportingPeriod13);
        }

        [Fact]
        public static void EqualsOperator___Should_return_true___When_both_sides_of_operator_are_null()
        {
            // Arrange
            FiftyTwoFiftyThreeWeekAccountingPeriodSystem systemUnderTest1 = null;
            FiftyTwoFiftyThreeWeekAccountingPeriodSystem systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 == systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void EqualsOperator___Should_return_false___When_one_side_of_operator_is_null_and_the_other_side_is_not_null()
        {
            // Arrange
            FiftyTwoFiftyThreeWeekAccountingPeriodSystem systemUnderTest = null;

            // Act
            var result1 = systemUnderTest == ObjectForEquatableTests;
            var result2 = ObjectForEquatableTests == systemUnderTest;

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
        }

        [Fact]
        public static void EqualsOperator___Should_return_true___When_same_object_is_on_both_sides_of_operator()
        {
            // Arrange, Act
#pragma warning disable CS1718 // Comparison made to same variable
            var result = ObjectForEquatableTests == ObjectForEquatableTests;
#pragma warning restore CS1718 // Comparison made to same variable

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void EqualsOperator___Should_return_false___When_objects_being_compared_have_different_property_values()
        {
            // Arrange, Act
            var results = ObjectsThatAreNotEqualToObjectForEquatableTests.Select(_ => ObjectForEquatableTests == _).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void EqualsOperator___Should_return_true___When_objects_being_compared_have_same_property_values()
        {
            // Arrange, Act
            var result = ObjectForEquatableTests == ObjectThatIsEqualButNotTheSameAsObjectForEquatableTests;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_false___When_both_sides_of_operator_are_null()
        {
            // Arrange
            FiftyTwoFiftyThreeWeekAccountingPeriodSystem systemUnderTest1 = null;
            FiftyTwoFiftyThreeWeekAccountingPeriodSystem systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 != systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_true___When_one_side_of_operator_is_null_and_the_other_side_is_not_null()
        {
            // Arrange
            FiftyTwoFiftyThreeWeekAccountingPeriodSystem systemUnderTest = null;

            // Act
            var result1 = systemUnderTest != ObjectForEquatableTests;
            var result2 = ObjectForEquatableTests != systemUnderTest;

            // Assert
            result1.Should().BeTrue();
            result2.Should().BeTrue();
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_false___When_same_object_is_on_both_sides_of_operator()
        {
            // Arrange, Act
#pragma warning disable CS1718 // Comparison made to same variable
            var result = ObjectForEquatableTests != ObjectForEquatableTests;
#pragma warning restore CS1718 // Comparison made to same variable

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_true___When_objects_being_compared_have_different_property_values()
        {
            // Arrange, Act
            var results = ObjectsThatAreNotEqualToObjectForEquatableTests.Select(_ => ObjectForEquatableTests != _).ToList();

            // sAssert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_false___When_objects_being_compared_have_same_property_values()
        {
            // Arrange, Act
            var result = ObjectForEquatableTests != ObjectThatIsEqualButNotTheSameAsObjectForEquatableTests;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void Equals_with_FiftyTwoFiftyThreeWeekAccountingPeriodSystem___Should_return_false___When_parameter_other_is_null()
        {
            // Arrange
            FiftyTwoFiftyThreeWeekAccountingPeriodSystem systemUnderTest = null;

            // Act
            var result = ObjectForEquatableTests.Equals(systemUnderTest);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void Equals_with_FiftyTwoFiftyThreeWeekAccountingPeriodSystem___Should_return_true___When_parameter_other_is_same_object()
        {
            // Arrange, Act
            var result = ObjectForEquatableTests.Equals(ObjectForEquatableTests);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Equals_with_FiftyTwoFiftyThreeWeekAccountingPeriodSystem___Should_return_false___When_objects_being_compared_have_different_property_values()
        {
            // Arrange, Act
            var results = ObjectsThatAreNotEqualToObjectForEquatableTests.Select(_ => ObjectForEquatableTests.Equals(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Equals_with_FiftyTwoFiftyThreeWeekAccountingPeriodSystem___Should_return_true___When_objects_being_compared_have_same_property_values()
        {
            // Arrange, Act
            var result = ObjectForEquatableTests.Equals(ObjectThatIsEqualButNotTheSameAsObjectForEquatableTests);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Equals_with_Object___Should_return_false___When_parameter_other_is_null()
        {
            // Arrange, Act
            var result = ObjectForEquatableTests.Equals(null);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void Equals_with_Object___Should_return_false___When_parameter_other_is_not_of_the_same_type()
        {
            // Arrange, Act
            var result = ObjectForEquatableTests.Equals((object)ObjectThatIsNotTheSameTypeAsObjectForEquatableTests);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void Equals_with_Object___Should_return_true___When_parameter_other_is_same_object()
        {
            // Arrange, Act
            var result = ObjectForEquatableTests.Equals((object)ObjectForEquatableTests);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Equals_with_Object___Should_return_false___When_objects_being_compared_have_different_property_values()
        {
            // Arrange, Act
            var results = ObjectsThatAreNotEqualToObjectForEquatableTests.Select(_ => ObjectForEquatableTests.Equals((object)_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Equals_with_Object___Should_return_true___When_objects_being_compared_have_same_property_values()
        {
            // Arrange, Act
            var result = ObjectForEquatableTests.Equals((object)ObjectThatIsEqualButNotTheSameAsObjectForEquatableTests);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void GetHashCode___Should_not_be_equal_for_two_objects___When_objects_have_different_property_values()
        {
            // Arrange, Act
            var hashCode1 = ObjectForEquatableTests.GetHashCode();
            var hashCode2 = ObjectsThatAreNotEqualToObjectForEquatableTests.Select(_ => _.GetHashCode()).ToList();

            // Assert
            hashCode2.ForEach(_ => _.Should().NotBe(hashCode1));
        }

        [Fact]
        public static void GetHashCode___Should_be_equal_for_two_objects___When_objects_have_the_same_property_values()
        {
            // Arrange, Act
            var hash1 = ObjectForEquatableTests.GetHashCode();
            var hash2 = ObjectThatIsEqualButNotTheSameAsObjectForEquatableTests.GetHashCode();

            // Assert
            hash1.Should().Be(hash2);
        }

        [Fact]
        public static void DeepClone___Should_deep_clone_object___When_called()
        {
            // Arrange
            var systemUnderTest = A.Dummy<FiftyTwoFiftyThreeWeekAccountingPeriodSystem>();

            // Act
            var actual = (FiftyTwoFiftyThreeWeekAccountingPeriodSystem)systemUnderTest.DeepClone();

            // Assert
            actual.Should().Be(systemUnderTest);
            actual.Should().NotBeSameAs(systemUnderTest);
        }
    }
}
