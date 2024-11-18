// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeExtensionsTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Test
{
    using System;
    using System.Linq;
    using FakeItEasy;
    using FluentAssertions;
    using OBeautifulCode.Assertion.Recipes;
    using Xunit;

    public static class DateTimeExtensionsTest
    {
        [Fact]
        public static void ToCalendarDay___Should_return_CalendarDay_equivalent_of_parameter_value___When_called()
        {
            // Arrange
            var dateTime1 = new DateTime(2016, 11, 28);
            var dateTime2 = new DateTime(2016, 2, 29);
            var dateTime3 = new DateTime(2017, 1, 1);
            var dateTime4 = new DateTime(2017, 12, 31);

            var expectedCalendarDay1 = new CalendarDay(2016, MonthOfYear.November, DayOfMonth.TwentyEight);
            var expectedCalendarDay2 = new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine);
            var expectedCalendarDay3 = new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One);
            var expectedCalendarDay4 = new CalendarDay(2017, MonthOfYear.December, DayOfMonth.ThirtyOne);

            // Act
            var actualCalendarDay1 = dateTime1.ToCalendarDay();
            var actualCalendarDay2 = dateTime2.ToCalendarDay();
            var actualCalendarDay3 = dateTime3.ToCalendarDay();
            var actualCalendarDay4 = dateTime4.ToCalendarDay();

            // Assert
            actualCalendarDay1.Should().Be(expectedCalendarDay1);
            actualCalendarDay2.Should().Be(expectedCalendarDay2);
            actualCalendarDay3.Should().Be(expectedCalendarDay3);
            actualCalendarDay4.Should().Be(expectedCalendarDay4);
        }

        [Fact]
        public static void Previous___Should_throw_ArgumentOutOfRangeException___When_parameter_granularity_is_Unbounded()
        {
            // Arrange, Act
            var actual = Record.Exception(() => A.Dummy<DateTime>().Previous(UnitOfTimeGranularity.Unbounded));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentOutOfRangeException>();
            actual.Message.AsTest().Must().ContainString("granularity");
        }

        [Fact]
        public static void Previous___Should_return_previous_UnitOfTime___When_called()
        {
            // Arrange
            var valueAndExpected = new[]
            {
                new
                {
                    Value = new DateTime(2023, 1, 1),
                    Granularity = UnitOfTimeGranularity.Year,
                    Expected = (CalendarUnitOfTime)new CalendarYear(2022),
                },
                new
                {
                    Value = new DateTime(2023, 12, 31),
                    Granularity = UnitOfTimeGranularity.Year,
                    Expected = (CalendarUnitOfTime)new CalendarYear(2022),
                },
                new
                {
                    Value = new DateTime(2023, 1, 1),
                    Granularity = UnitOfTimeGranularity.Quarter,
                    Expected = (CalendarUnitOfTime)new CalendarQuarter(2022, QuarterNumber.Q4),
                },
                new
                {
                    Value = new DateTime(2023, 1, 31),
                    Granularity = UnitOfTimeGranularity.Quarter,
                    Expected = (CalendarUnitOfTime)new CalendarQuarter(2022, QuarterNumber.Q4),
                },
                new
                {
                    Value = new DateTime(2023, 2, 1),
                    Granularity = UnitOfTimeGranularity.Quarter,
                    Expected = (CalendarUnitOfTime)new CalendarQuarter(2022, QuarterNumber.Q4),
                },
                new
                {
                    Value = new DateTime(2023, 2, 28),
                    Granularity = UnitOfTimeGranularity.Quarter,
                    Expected = (CalendarUnitOfTime)new CalendarQuarter(2022, QuarterNumber.Q4),
                },
                new
                {
                    Value = new DateTime(2023, 3, 1),
                    Granularity = UnitOfTimeGranularity.Quarter,
                    Expected = (CalendarUnitOfTime)new CalendarQuarter(2022, QuarterNumber.Q4),
                },
                new
                {
                    Value = new DateTime(2023, 3, 31),
                    Granularity = UnitOfTimeGranularity.Quarter,
                    Expected = (CalendarUnitOfTime)new CalendarQuarter(2022, QuarterNumber.Q4),
                },
                new
                {
                    Value = new DateTime(2023, 4, 1),
                    Granularity = UnitOfTimeGranularity.Quarter,
                    Expected = (CalendarUnitOfTime)new CalendarQuarter(2023, QuarterNumber.Q1),
                },
                new
                {
                    Value = new DateTime(2023, 4, 30),
                    Granularity = UnitOfTimeGranularity.Quarter,
                    Expected = (CalendarUnitOfTime)new CalendarQuarter(2023, QuarterNumber.Q1),
                },
                new
                {
                    Value = new DateTime(2023, 5, 1),
                    Granularity = UnitOfTimeGranularity.Quarter,
                    Expected = (CalendarUnitOfTime)new CalendarQuarter(2023, QuarterNumber.Q1),
                },
                new
                {
                    Value = new DateTime(2023, 5, 31),
                    Granularity = UnitOfTimeGranularity.Quarter,
                    Expected = (CalendarUnitOfTime)new CalendarQuarter(2023, QuarterNumber.Q1),
                },
                new
                {
                    Value = new DateTime(2023, 6, 1),
                    Granularity = UnitOfTimeGranularity.Quarter,
                    Expected = (CalendarUnitOfTime)new CalendarQuarter(2023, QuarterNumber.Q1),
                },
                new
                {
                    Value = new DateTime(2023, 6, 30),
                    Granularity = UnitOfTimeGranularity.Quarter,
                    Expected = (CalendarUnitOfTime)new CalendarQuarter(2023, QuarterNumber.Q1),
                },
                new
                {
                    Value = new DateTime(2023, 7, 1),
                    Granularity = UnitOfTimeGranularity.Quarter,
                    Expected = (CalendarUnitOfTime)new CalendarQuarter(2023, QuarterNumber.Q2),
                },
                new
                {
                    Value = new DateTime(2023, 7, 31),
                    Granularity = UnitOfTimeGranularity.Quarter,
                    Expected = (CalendarUnitOfTime)new CalendarQuarter(2023, QuarterNumber.Q2),
                },
                new
                {
                    Value = new DateTime(2023, 8, 1),
                    Granularity = UnitOfTimeGranularity.Quarter,
                    Expected = (CalendarUnitOfTime)new CalendarQuarter(2023, QuarterNumber.Q2),
                },
                new
                {
                    Value = new DateTime(2023, 8, 30),
                    Granularity = UnitOfTimeGranularity.Quarter,
                    Expected = (CalendarUnitOfTime)new CalendarQuarter(2023, QuarterNumber.Q2),
                },
                new
                {
                    Value = new DateTime(2023, 9, 1),
                    Granularity = UnitOfTimeGranularity.Quarter,
                    Expected = (CalendarUnitOfTime)new CalendarQuarter(2023, QuarterNumber.Q2),
                },
                new
                {
                    Value = new DateTime(2023, 9, 30),
                    Granularity = UnitOfTimeGranularity.Quarter,
                    Expected = (CalendarUnitOfTime)new CalendarQuarter(2023, QuarterNumber.Q2),
                },
                new
                {
                    Value = new DateTime(2023, 10, 1),
                    Granularity = UnitOfTimeGranularity.Quarter,
                    Expected = (CalendarUnitOfTime)new CalendarQuarter(2023, QuarterNumber.Q3),
                },
                new
                {
                    Value = new DateTime(2023, 10, 31),
                    Granularity = UnitOfTimeGranularity.Quarter,
                    Expected = (CalendarUnitOfTime)new CalendarQuarter(2023, QuarterNumber.Q3),
                },
                new
                {
                    Value = new DateTime(2023, 11, 1),
                    Granularity = UnitOfTimeGranularity.Quarter,
                    Expected = (CalendarUnitOfTime)new CalendarQuarter(2023, QuarterNumber.Q3),
                },
                new
                {
                    Value = new DateTime(2023, 11, 30),
                    Granularity = UnitOfTimeGranularity.Quarter,
                    Expected = (CalendarUnitOfTime)new CalendarQuarter(2023, QuarterNumber.Q3),
                },
                new
                {
                    Value = new DateTime(2023, 12, 1),
                    Granularity = UnitOfTimeGranularity.Quarter,
                    Expected = (CalendarUnitOfTime)new CalendarQuarter(2023, QuarterNumber.Q3),
                },
                new
                {
                    Value = new DateTime(2023, 12, 31),
                    Granularity = UnitOfTimeGranularity.Quarter,
                    Expected = (CalendarUnitOfTime)new CalendarQuarter(2023, QuarterNumber.Q3),
                },
                new
                {
                    Value = new DateTime(2023, 1, 1),
                    Granularity = UnitOfTimeGranularity.Month,
                    Expected = (CalendarUnitOfTime)new CalendarMonth(2022, MonthOfYear.December),
                },
                new
                {
                    Value = new DateTime(2023, 1, 31),
                    Granularity = UnitOfTimeGranularity.Month,
                    Expected = (CalendarUnitOfTime)new CalendarMonth(2022, MonthOfYear.December),
                },
                new
                {
                    Value = new DateTime(2023, 2, 1),
                    Granularity = UnitOfTimeGranularity.Month,
                    Expected = (CalendarUnitOfTime)new CalendarMonth(2023, MonthOfYear.January),
                },
                new
                {
                    Value = new DateTime(2023, 2, 28),
                    Granularity = UnitOfTimeGranularity.Month,
                    Expected = (CalendarUnitOfTime)new CalendarMonth(2023, MonthOfYear.January),
                },
                new
                {
                    Value = new DateTime(2023, 3, 1),
                    Granularity = UnitOfTimeGranularity.Month,
                    Expected = (CalendarUnitOfTime)new CalendarMonth(2023, MonthOfYear.February),
                },
                new
                {
                    Value = new DateTime(2023, 3, 31),
                    Granularity = UnitOfTimeGranularity.Month,
                    Expected = (CalendarUnitOfTime)new CalendarMonth(2023, MonthOfYear.February),
                },
                new
                {
                    Value = new DateTime(2023, 4, 1),
                    Granularity = UnitOfTimeGranularity.Month,
                    Expected = (CalendarUnitOfTime)new CalendarMonth(2023, MonthOfYear.March),
                },
                new
                {
                    Value = new DateTime(2023, 4, 30),
                    Granularity = UnitOfTimeGranularity.Month,
                    Expected = (CalendarUnitOfTime)new CalendarMonth(2023, MonthOfYear.March),
                },
                new
                {
                    Value = new DateTime(2023, 5, 1),
                    Granularity = UnitOfTimeGranularity.Month,
                    Expected = (CalendarUnitOfTime)new CalendarMonth(2023, MonthOfYear.April),
                },
                new
                {
                    Value = new DateTime(2023, 5, 31),
                    Granularity = UnitOfTimeGranularity.Month,
                    Expected = (CalendarUnitOfTime)new CalendarMonth(2023, MonthOfYear.April),
                },
                new
                {
                    Value = new DateTime(2023, 6, 1),
                    Granularity = UnitOfTimeGranularity.Month,
                    Expected = (CalendarUnitOfTime)new CalendarMonth(2023, MonthOfYear.May),
                },
                new
                {
                    Value = new DateTime(2023, 6, 30),
                    Granularity = UnitOfTimeGranularity.Month,
                    Expected = (CalendarUnitOfTime)new CalendarMonth(2023, MonthOfYear.May),
                },
                new
                {
                    Value = new DateTime(2023, 7, 1),
                    Granularity = UnitOfTimeGranularity.Month,
                    Expected = (CalendarUnitOfTime)new CalendarMonth(2023, MonthOfYear.June),
                },
                new
                {
                    Value = new DateTime(2023, 7, 31),
                    Granularity = UnitOfTimeGranularity.Month,
                    Expected = (CalendarUnitOfTime)new CalendarMonth(2023, MonthOfYear.June),
                },
                new
                {
                    Value = new DateTime(2023, 8, 1),
                    Granularity = UnitOfTimeGranularity.Month,
                    Expected = (CalendarUnitOfTime)new CalendarMonth(2023, MonthOfYear.July),
                },
                new
                {
                    Value = new DateTime(2023, 8, 30),
                    Granularity = UnitOfTimeGranularity.Month,
                    Expected = (CalendarUnitOfTime)new CalendarMonth(2023, MonthOfYear.July),
                },
                new
                {
                    Value = new DateTime(2023, 9, 1),
                    Granularity = UnitOfTimeGranularity.Month,
                    Expected = (CalendarUnitOfTime)new CalendarMonth(2023, MonthOfYear.August),
                },
                new
                {
                    Value = new DateTime(2023, 9, 30),
                    Granularity = UnitOfTimeGranularity.Month,
                    Expected = (CalendarUnitOfTime)new CalendarMonth(2023, MonthOfYear.August),
                },
                new
                {
                    Value = new DateTime(2023, 10, 1),
                    Granularity = UnitOfTimeGranularity.Month,
                    Expected = (CalendarUnitOfTime)new CalendarMonth(2023, MonthOfYear.September),
                },
                new
                {
                    Value = new DateTime(2023, 10, 31),
                    Granularity = UnitOfTimeGranularity.Month,
                    Expected = (CalendarUnitOfTime)new CalendarMonth(2023, MonthOfYear.September),
                },
                new
                {
                    Value = new DateTime(2023, 11, 1),
                    Granularity = UnitOfTimeGranularity.Month,
                    Expected = (CalendarUnitOfTime)new CalendarMonth(2023, MonthOfYear.October),
                },
                new
                {
                    Value = new DateTime(2023, 11, 30),
                    Granularity = UnitOfTimeGranularity.Month,
                    Expected = (CalendarUnitOfTime)new CalendarMonth(2023, MonthOfYear.October),
                },
                new
                {
                    Value = new DateTime(2023, 12, 1),
                    Granularity = UnitOfTimeGranularity.Month,
                    Expected = (CalendarUnitOfTime)new CalendarMonth(2023, MonthOfYear.November),
                },
                new
                {
                    Value = new DateTime(2023, 12, 31),
                    Granularity = UnitOfTimeGranularity.Month,
                    Expected = (CalendarUnitOfTime)new CalendarMonth(2023, MonthOfYear.November),
                },
                new
                {
                    Value = new DateTime(2023, 1, 1),
                    Granularity = UnitOfTimeGranularity.Day,
                    Expected = (CalendarUnitOfTime)new CalendarDay(2022, MonthOfYear.December, DayOfMonth.ThirtyOne),
                },
                new
                {
                    Value = new DateTime(2023, 1, 1, 23, 59, 59),
                    Granularity = UnitOfTimeGranularity.Day,
                    Expected = (CalendarUnitOfTime)new CalendarDay(2022, MonthOfYear.December, DayOfMonth.ThirtyOne),
                },
                new
                {
                    Value = new DateTime(2023, 1, 2),
                    Granularity = UnitOfTimeGranularity.Day,
                    Expected = (CalendarUnitOfTime)new CalendarDay(2023, MonthOfYear.January, DayOfMonth.One),
                },
                new
                {
                    Value = new DateTime(2023, 12, 31),
                    Granularity = UnitOfTimeGranularity.Day,
                    Expected = (CalendarUnitOfTime)new CalendarDay(2023, MonthOfYear.December, DayOfMonth.Thirty),
                },
            };

            var expected = valueAndExpected.Select(_ => _.Expected).ToList();

            // Act
            var actual = valueAndExpected.Select(_ => _.Value.Previous(_.Granularity)).ToList();

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }
    }
}