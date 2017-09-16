// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeExtensionsTest.cs" company="OBeautifulCode">
//    Copyright (c) OBeautifulCode 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime.Test
{
    using System;

    using FluentAssertions;

    using Xunit;

    public static class DateTimeExtensionsTest
    {
        // ReSharper disable InconsistentNaming
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

        // ReSharper restore InconsistentNaming
    }
}

// ReSharper restore CheckNamespace