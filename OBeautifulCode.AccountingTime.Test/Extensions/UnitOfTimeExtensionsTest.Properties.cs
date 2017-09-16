// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfTimeExtensionsTest.Properties.cs" company="OBeautifulCode">
//    Copyright (c) OBeautifulCode 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime.Test
{
    using System;
    using System.Collections.Generic;

    using FakeItEasy;

    using FluentAssertions;

    using Xunit;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "There are many kinds of units-of-time.")]
    public static partial class UnitOfTimeExtensionsTest
    {
        // ReSharper disable InconsistentNaming
        [Fact]
        public static void GetFirstCalendarDay___Should_throw_ArgumentNullException___When_parameter_unitOfTime_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => UnitOfTimeExtensions.GetFirstCalendarDay(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void GetFirstCalendarDay___Should_return_same_unitOfTime___When_parameter_unitOfTime_is_of_type_CalendarDay()
        {
            // Arrange
            var expectedDay = A.Dummy<CalendarDay>();

            // Act
            var actualDay = expectedDay.GetFirstCalendarDay();

            // Assert
            actualDay.Should().Be(expectedDay);
        }

        [Fact]
        public static void GetFirstCalendarDay___Should_return_first_day_of_month___When_parameter_unitOfTime_is_of_type_CalendarMonth()
        {
            // Arrange
            var month = A.Dummy<CalendarMonth>();
            var expectedDay = new CalendarDay(month.Year, month.MonthOfYear, DayOfMonth.One);

            // Act
            var actualDay = month.GetFirstCalendarDay();

            // Assert
            actualDay.Should().Be(expectedDay);
        }

        [Fact]
        public static void GetFirstCalendarDay___Should_return_first_day_of_quarter___When_parameter_unitOfTime_is_of_type_CalendarQuarter()
        {
            // Arrange
            var quarter1 = new CalendarQuarter(2016, QuarterNumber.Q1);
            var quarter2 = new CalendarQuarter(2016, QuarterNumber.Q2);
            var quarter3 = new CalendarQuarter(2016, QuarterNumber.Q3);
            var quarter4 = new CalendarQuarter(2016, QuarterNumber.Q4);

            var expectedDay1 = new CalendarDay(2016, MonthOfYear.January, DayOfMonth.One);
            var expectedDay2 = new CalendarDay(2016, MonthOfYear.April, DayOfMonth.One);
            var expectedDay3 = new CalendarDay(2016, MonthOfYear.July, DayOfMonth.One);
            var expectedDay4 = new CalendarDay(2016, MonthOfYear.October, DayOfMonth.One);

            // Act
            var actualDay1 = quarter1.GetFirstCalendarDay();
            var actualDay2 = quarter2.GetFirstCalendarDay();
            var actualDay3 = quarter3.GetFirstCalendarDay();
            var actualDay4 = quarter4.GetFirstCalendarDay();

            // Assert
            actualDay1.Should().Be(expectedDay1);
            actualDay2.Should().Be(expectedDay2);
            actualDay3.Should().Be(expectedDay3);
            actualDay4.Should().Be(expectedDay4);
        }

        [Fact]
        public static void GetFirstCalendarYear___Should_return_first_day_of_year___When_parameter_unitOfTime_is_of_type_CalendarYear()
        {
            // Arrange
            var year1 = new CalendarYear(2016);
            var year2 = new CalendarYear(2017);

            var expectedDay1 = new CalendarDay(2016, MonthOfYear.January, DayOfMonth.One);
            var expectedDay2 = new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One);

            // Act
            var actualDay1 = year1.GetFirstCalendarDay();
            var actualDay2 = year2.GetFirstCalendarDay();

            // Assert
            actualDay1.Should().Be(expectedDay1);
            actualDay2.Should().Be(expectedDay2);
        }

        [Fact]
        public static void GetFirstCalendarYear___Should_throw_InvalidOperationException___When_parameter_unitOfTime_is_of_type_CalendarUnbounded()
        {
            // Arrange
            var systemUnderTest = A.Dummy<CalendarUnbounded>();

            // Act
            var ex = Record.Exception(() => systemUnderTest.GetFirstCalendarDay());

            // Assert
            ex.Should().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void GetLastCalendarDay___Should_throw_ArgumentNullException___When_parameter_unitOfTime_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => UnitOfTimeExtensions.GetLastCalendarDay(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void GetLastCalendarDay___Should_return_same_unitOfTime___When_parameter_unitOfTime_is_of_type_CalendarDay()
        {
            // Arrange
            var expectedDay = A.Dummy<CalendarDay>();

            // Act
            var actualDay = expectedDay.GetLastCalendarDay();

            // Assert
            actualDay.Should().Be(expectedDay);
        }

        [Fact]
        public static void GetLastCalendarDay___Should_return_last_day_of_month___When_parameter_unitOfTime_is_of_type_CalendarMonth()
        {
            // Arrange
            var month1 = new CalendarMonth(2016, MonthOfYear.February);
            var month2 = new CalendarMonth(2016, MonthOfYear.November);
            var month3 = new CalendarMonth(2016, MonthOfYear.October);

            var expectedDay1 = new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine);
            var expectedDay2 = new CalendarDay(2016, MonthOfYear.November, DayOfMonth.Thirty);
            var expectedDay3 = new CalendarDay(2016, MonthOfYear.October, DayOfMonth.ThirtyOne);

            // Act
            var actualDay1 = month1.GetLastCalendarDay();
            var actualDay2 = month2.GetLastCalendarDay();
            var actualDay3 = month3.GetLastCalendarDay();

            // Assert
            actualDay1.Should().Be(expectedDay1);
            actualDay2.Should().Be(expectedDay2);
            actualDay3.Should().Be(expectedDay3);
        }

        [Fact]
        public static void GetLastCalendarDay___Should_return_last_day_of_quarter___When_parameter_unitOfTime_is_of_type_CalendarQuarter()
        {
            // Arrange
            var quarter1 = new CalendarQuarter(2016, QuarterNumber.Q1);
            var quarter2 = new CalendarQuarter(2016, QuarterNumber.Q2);
            var quarter3 = new CalendarQuarter(2016, QuarterNumber.Q3);
            var quarter4 = new CalendarQuarter(2016, QuarterNumber.Q4);

            var expectedDay1 = new CalendarDay(2016, MonthOfYear.March, DayOfMonth.ThirtyOne);
            var expectedDay2 = new CalendarDay(2016, MonthOfYear.June, DayOfMonth.Thirty);
            var expectedDay3 = new CalendarDay(2016, MonthOfYear.September, DayOfMonth.Thirty);
            var expectedDay4 = new CalendarDay(2016, MonthOfYear.December, DayOfMonth.ThirtyOne);

            // Act
            var actualDay1 = quarter1.GetLastCalendarDay();
            var actualDay2 = quarter2.GetLastCalendarDay();
            var actualDay3 = quarter3.GetLastCalendarDay();
            var actualDay4 = quarter4.GetLastCalendarDay();

            // Assert
            actualDay1.Should().Be(expectedDay1);
            actualDay2.Should().Be(expectedDay2);
            actualDay3.Should().Be(expectedDay3);
            actualDay4.Should().Be(expectedDay4);
        }

        [Fact]
        public static void GetLastCalendarYear___Should_return_last_day_of_year___When_parameter_unitOfTime_is_of_type_CalendarYear()
        {
            // Arrange
            var year1 = new CalendarYear(2016);
            var year2 = new CalendarYear(2017);

            var expectedDay1 = new CalendarDay(2016, MonthOfYear.December, DayOfMonth.ThirtyOne);
            var expectedDay2 = new CalendarDay(2017, MonthOfYear.December, DayOfMonth.ThirtyOne);

            // Act
            var actualDay1 = year1.GetLastCalendarDay();
            var actualDay2 = year2.GetLastCalendarDay();

            // Assert
            actualDay1.Should().Be(expectedDay1);
            actualDay2.Should().Be(expectedDay2);
        }

        [Fact]
        public static void GetLastCalendarYear___Should_throw_InvalidOperationException___When_parameter_unitOfTime_is_of_type_CalendarUnbounded()
        {
            // Arrange
            var systemUnderTest = A.Dummy<CalendarUnbounded>();

            // Act
            var ex = Record.Exception(() => systemUnderTest.GetLastCalendarDay());

            // Assert
            ex.Should().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void GetUnitsToDate___Should_throw_ArgumentNullException___When_parameter_lastUnitOfTimeInYear_is_null()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => UnitOfTimeExtensions.GetUnitsToDate<CalendarDay>(null));
            var ex2 = Record.Exception(() => UnitOfTimeExtensions.GetUnitsToDate<CalendarMonth>(null));
            var ex3 = Record.Exception(() => UnitOfTimeExtensions.GetUnitsToDate<CalendarQuarter>(null));
            var ex4 = Record.Exception(() => UnitOfTimeExtensions.GetUnitsToDate<CalendarYear>(null));

            var ex5 = Record.Exception(() => UnitOfTimeExtensions.GetUnitsToDate<FiscalMonth>(null));
            var ex6 = Record.Exception(() => UnitOfTimeExtensions.GetUnitsToDate<FiscalQuarter>(null));
            var ex7 = Record.Exception(() => UnitOfTimeExtensions.GetUnitsToDate<FiscalYear>(null));

            var ex8 = Record.Exception(() => UnitOfTimeExtensions.GetUnitsToDate<GenericMonth>(null));
            var ex9 = Record.Exception(() => UnitOfTimeExtensions.GetUnitsToDate<GenericQuarter>(null));
            var ex10 = Record.Exception(() => UnitOfTimeExtensions.GetUnitsToDate<GenericYear>(null));

            // Assert
            ex1.Should().BeOfType<ArgumentNullException>();
            ex2.Should().BeOfType<ArgumentNullException>();
            ex3.Should().BeOfType<ArgumentNullException>();
            ex4.Should().BeOfType<ArgumentNullException>();
            ex5.Should().BeOfType<ArgumentNullException>();
            ex6.Should().BeOfType<ArgumentNullException>();
            ex7.Should().BeOfType<ArgumentNullException>();
            ex8.Should().BeOfType<ArgumentNullException>();
            ex9.Should().BeOfType<ArgumentNullException>();
            ex10.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void GetUnitsToDate___Should_return_all_days_from_january_1_to_lastUnitOfTimeInYear___When_lastUnitOfTimeInYear_is_of_type_CalendarDay()
        {
            // Arrange
            var lastUnitOfTimeInYear1 = new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One);
            var lastUnitOfTimeInYear2 = new CalendarDay(2016, MonthOfYear.January, DayOfMonth.One);

            var expectedUnitsToDate1 = new List<CalendarDay>();
            for (var date = new DateTime(2016, 1, 1); date <= lastUnitOfTimeInYear1.ToDateTime(); date = date.AddDays(1))
            {
                expectedUnitsToDate1.Add(date.ToCalendarDay());
            }

            var expectedUnitsToDate2 = new List<CalendarDay> { lastUnitOfTimeInYear2 };

            // Act
            var actualUnitsToDate1 = lastUnitOfTimeInYear1.GetUnitsToDate();
            var actualUnitsToDate2 = lastUnitOfTimeInYear2.GetUnitsToDate();

            // Assert
            actualUnitsToDate1.Should().Equal(expectedUnitsToDate1);
            actualUnitsToDate2.Should().Equal(expectedUnitsToDate2);
        }

        [Fact]
        public static void GetUnitsToDate___Should_return_all_months_from_month_1_to_lastUnitOfTimeInYear___When_lastUnitOfTimeInYear_is_of_type_CalendarMonth()
        {
            // Arrange
            var lastUnitOfTimeInYear1 = new CalendarMonth(2016, (MonthOfYear)MonthNumber.One);
            var lastUnitOfTimeInYear2 = new CalendarMonth(2016, (MonthOfYear)MonthNumber.Three);
            var lastUnitOfTimeInYear3 = new CalendarMonth(2016, (MonthOfYear)MonthNumber.Twelve);

            var expectedUnitsToDate1 = new List<CalendarMonth> { lastUnitOfTimeInYear1 };
            var expectedUnitsToDate2 = new List<CalendarMonth> { new CalendarMonth(2016, (MonthOfYear)MonthNumber.One), new CalendarMonth(2016, (MonthOfYear)MonthNumber.Two), new CalendarMonth(2016, (MonthOfYear)MonthNumber.Three) };
            var expectedUnitsToDate3 = new List<CalendarMonth> { new CalendarMonth(2016, (MonthOfYear)MonthNumber.One), new CalendarMonth(2016, (MonthOfYear)MonthNumber.Two), new CalendarMonth(2016, (MonthOfYear)MonthNumber.Three), new CalendarMonth(2016, (MonthOfYear)MonthNumber.Four), new CalendarMonth(2016, (MonthOfYear)MonthNumber.Five), new CalendarMonth(2016, (MonthOfYear)MonthNumber.Six), new CalendarMonth(2016, (MonthOfYear)MonthNumber.Seven), new CalendarMonth(2016, (MonthOfYear)MonthNumber.Eight), new CalendarMonth(2016, (MonthOfYear)MonthNumber.Nine), new CalendarMonth(2016, (MonthOfYear)MonthNumber.Ten), new CalendarMonth(2016, (MonthOfYear)MonthNumber.Eleven), new CalendarMonth(2016, (MonthOfYear)MonthNumber.Twelve) };

            // Act
            var actualUnitsToDate1 = lastUnitOfTimeInYear1.GetUnitsToDate();
            var actualUnitsToDate2 = lastUnitOfTimeInYear2.GetUnitsToDate();
            var actualUnitsToDate3 = lastUnitOfTimeInYear3.GetUnitsToDate();

            // Assert
            actualUnitsToDate1.Should().Equal(expectedUnitsToDate1);
            actualUnitsToDate2.Should().Equal(expectedUnitsToDate2);
            actualUnitsToDate3.Should().Equal(expectedUnitsToDate3);
        }

        [Fact]
        public static void GetUnitsToDate___Should_return_all_months_from_month_1_to_lastUnitOfTimeInYear___When_lastUnitOfTimeInYear_is_of_type_FiscalMonth()
        {
            // Arrange
            var lastUnitOfTimeInYear1 = new FiscalMonth(2016, MonthNumber.One);
            var lastUnitOfTimeInYear2 = new FiscalMonth(2016, MonthNumber.Three);
            var lastUnitOfTimeInYear3 = new FiscalMonth(2016, MonthNumber.Twelve);

            var expectedUnitsToDate1 = new List<FiscalMonth> { lastUnitOfTimeInYear1 };
            var expectedUnitsToDate2 = new List<FiscalMonth> { new FiscalMonth(2016, MonthNumber.One), new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Three) };
            var expectedUnitsToDate3 = new List<FiscalMonth> { new FiscalMonth(2016, MonthNumber.One), new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Three), new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2016, MonthNumber.Five), new FiscalMonth(2016, MonthNumber.Six), new FiscalMonth(2016, MonthNumber.Seven), new FiscalMonth(2016, MonthNumber.Eight), new FiscalMonth(2016, MonthNumber.Nine), new FiscalMonth(2016, MonthNumber.Ten), new FiscalMonth(2016, MonthNumber.Eleven), new FiscalMonth(2016, MonthNumber.Twelve) };

            // Act
            var actualUnitsToDate1 = lastUnitOfTimeInYear1.GetUnitsToDate();
            var actualUnitsToDate2 = lastUnitOfTimeInYear2.GetUnitsToDate();
            var actualUnitsToDate3 = lastUnitOfTimeInYear3.GetUnitsToDate();

            // Assert
            actualUnitsToDate1.Should().Equal(expectedUnitsToDate1);
            actualUnitsToDate2.Should().Equal(expectedUnitsToDate2);
            actualUnitsToDate3.Should().Equal(expectedUnitsToDate3);
        }

        [Fact]
        public static void GetUnitsToDate___Should_return_all_months_from_month_1_to_lastUnitOfTimeInYear___When_lastUnitOfTimeInYear_is_of_type_GenericMonth()
        {
            // Arrange
            var lastUnitOfTimeInYear1 = new GenericMonth(2016, MonthNumber.One);
            var lastUnitOfTimeInYear2 = new GenericMonth(2016, MonthNumber.Three);
            var lastUnitOfTimeInYear3 = new GenericMonth(2016, MonthNumber.Twelve);

            var expectedUnitsToDate1 = new List<GenericMonth> { lastUnitOfTimeInYear1 };
            var expectedUnitsToDate2 = new List<GenericMonth> { new GenericMonth(2016, MonthNumber.One), new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Three) };
            var expectedUnitsToDate3 = new List<GenericMonth> { new GenericMonth(2016, MonthNumber.One), new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Three), new GenericMonth(2016, MonthNumber.Four), new GenericMonth(2016, MonthNumber.Five), new GenericMonth(2016, MonthNumber.Six), new GenericMonth(2016, MonthNumber.Seven), new GenericMonth(2016, MonthNumber.Eight), new GenericMonth(2016, MonthNumber.Nine), new GenericMonth(2016, MonthNumber.Ten), new GenericMonth(2016, MonthNumber.Eleven), new GenericMonth(2016, MonthNumber.Twelve) };

            // Act
            var actualUnitsToDate1 = lastUnitOfTimeInYear1.GetUnitsToDate();
            var actualUnitsToDate2 = lastUnitOfTimeInYear2.GetUnitsToDate();
            var actualUnitsToDate3 = lastUnitOfTimeInYear3.GetUnitsToDate();

            // Assert
            actualUnitsToDate1.Should().Equal(expectedUnitsToDate1);
            actualUnitsToDate2.Should().Equal(expectedUnitsToDate2);
            actualUnitsToDate3.Should().Equal(expectedUnitsToDate3);
        }

        [Fact]
        public static void GetUnitsToDate___Should_return_all_quarters_from_1Q_to_lastUnitOfTimeInYear___When_lastUnitOfTimeInYear_is_of_type_CalendarQuarter()
        {
            // Arrange
            var lastUnitOfTimeInYear1 = new CalendarQuarter(2016, QuarterNumber.Q1);
            var lastUnitOfTimeInYear2 = new CalendarQuarter(2016, QuarterNumber.Q2);
            var lastUnitOfTimeInYear3 = new CalendarQuarter(2016, QuarterNumber.Q3);
            var lastUnitOfTimeInYear4 = new CalendarQuarter(2016, QuarterNumber.Q4);

            var expectedUnitsToDate1 = new List<CalendarQuarter> { lastUnitOfTimeInYear1 };
            var expectedUnitsToDate2 = new List<CalendarQuarter> { lastUnitOfTimeInYear1, lastUnitOfTimeInYear2 };
            var expectedUnitsToDate3 = new List<CalendarQuarter> { lastUnitOfTimeInYear1, lastUnitOfTimeInYear2, lastUnitOfTimeInYear3 };
            var expectedUnitsToDate4 = new List<CalendarQuarter> { lastUnitOfTimeInYear1, lastUnitOfTimeInYear2, lastUnitOfTimeInYear3, lastUnitOfTimeInYear4 };

            // Act
            var actualUnitsToDate1 = lastUnitOfTimeInYear1.GetUnitsToDate();
            var actualUnitsToDate2 = lastUnitOfTimeInYear2.GetUnitsToDate();
            var actualUnitsToDate3 = lastUnitOfTimeInYear3.GetUnitsToDate();
            var actualUnitsToDate4 = lastUnitOfTimeInYear4.GetUnitsToDate();

            // Assert
            actualUnitsToDate1.Should().Equal(expectedUnitsToDate1);
            actualUnitsToDate2.Should().Equal(expectedUnitsToDate2);
            actualUnitsToDate3.Should().Equal(expectedUnitsToDate3);
            actualUnitsToDate4.Should().Equal(expectedUnitsToDate4);
        }

        [Fact]
        public static void GetUnitsToDate___Should_return_all_quarters_from_1Q_to_lastUnitOfTimeInYear___When_lastUnitOfTimeInYear_is_of_type_FiscalQuarter()
        {
            // Arrange
            var lastUnitOfTimeInYear1 = new FiscalQuarter(2016, QuarterNumber.Q1);
            var lastUnitOfTimeInYear2 = new FiscalQuarter(2016, QuarterNumber.Q2);
            var lastUnitOfTimeInYear3 = new FiscalQuarter(2016, QuarterNumber.Q3);
            var lastUnitOfTimeInYear4 = new FiscalQuarter(2016, QuarterNumber.Q4);

            var expectedUnitsToDate1 = new List<FiscalQuarter> { lastUnitOfTimeInYear1 };
            var expectedUnitsToDate2 = new List<FiscalQuarter> { lastUnitOfTimeInYear1, lastUnitOfTimeInYear2 };
            var expectedUnitsToDate3 = new List<FiscalQuarter> { lastUnitOfTimeInYear1, lastUnitOfTimeInYear2, lastUnitOfTimeInYear3 };
            var expectedUnitsToDate4 = new List<FiscalQuarter> { lastUnitOfTimeInYear1, lastUnitOfTimeInYear2, lastUnitOfTimeInYear3, lastUnitOfTimeInYear4 };

            // Act
            var actualUnitsToDate1 = lastUnitOfTimeInYear1.GetUnitsToDate();
            var actualUnitsToDate2 = lastUnitOfTimeInYear2.GetUnitsToDate();
            var actualUnitsToDate3 = lastUnitOfTimeInYear3.GetUnitsToDate();
            var actualUnitsToDate4 = lastUnitOfTimeInYear4.GetUnitsToDate();

            // Assert
            actualUnitsToDate1.Should().Equal(expectedUnitsToDate1);
            actualUnitsToDate2.Should().Equal(expectedUnitsToDate2);
            actualUnitsToDate3.Should().Equal(expectedUnitsToDate3);
            actualUnitsToDate4.Should().Equal(expectedUnitsToDate4);
        }

        [Fact]
        public static void GetUnitsToDate___Should_return_all_quarters_from_1Q_to_lastUnitOfTimeInYear___When_lastUnitOfTimeInYear_is_of_type_GenericQuarter()
        {
            // Arrange
            var lastUnitOfTimeInYear1 = new GenericQuarter(2016, QuarterNumber.Q1);
            var lastUnitOfTimeInYear2 = new GenericQuarter(2016, QuarterNumber.Q2);
            var lastUnitOfTimeInYear3 = new GenericQuarter(2016, QuarterNumber.Q3);
            var lastUnitOfTimeInYear4 = new GenericQuarter(2016, QuarterNumber.Q4);

            var expectedUnitsToDate1 = new List<GenericQuarter> { lastUnitOfTimeInYear1 };
            var expectedUnitsToDate2 = new List<GenericQuarter> { lastUnitOfTimeInYear1, lastUnitOfTimeInYear2 };
            var expectedUnitsToDate3 = new List<GenericQuarter> { lastUnitOfTimeInYear1, lastUnitOfTimeInYear2, lastUnitOfTimeInYear3 };
            var expectedUnitsToDate4 = new List<GenericQuarter> { lastUnitOfTimeInYear1, lastUnitOfTimeInYear2, lastUnitOfTimeInYear3, lastUnitOfTimeInYear4 };

            // Act
            var actualUnitsToDate1 = lastUnitOfTimeInYear1.GetUnitsToDate();
            var actualUnitsToDate2 = lastUnitOfTimeInYear2.GetUnitsToDate();
            var actualUnitsToDate3 = lastUnitOfTimeInYear3.GetUnitsToDate();
            var actualUnitsToDate4 = lastUnitOfTimeInYear4.GetUnitsToDate();

            // Assert
            actualUnitsToDate1.Should().Equal(expectedUnitsToDate1);
            actualUnitsToDate2.Should().Equal(expectedUnitsToDate2);
            actualUnitsToDate3.Should().Equal(expectedUnitsToDate3);
            actualUnitsToDate4.Should().Equal(expectedUnitsToDate4);
        }

        [Fact]
        public static void GetUnitsToDate___Should_return_list_with_only_lastUnitOfTimeInYear___When_lastUnitOfTimeInYear_is_of_type_CalendarYear()
        {
            // Arrange
            var lastUnitOfTimeInYear = new CalendarYear(2016);
            var expectedUnitsToDate = new List<CalendarYear> { lastUnitOfTimeInYear };

            // Act
            var actualUnitsToDate = lastUnitOfTimeInYear.GetUnitsToDate();

            // Assert
            actualUnitsToDate.Should().Equal(expectedUnitsToDate);
        }

        [Fact]
        public static void GetUnitsToDate___Should_return_list_with_only_lastUnitOfTimeInYear___When_lastUnitOfTimeInYear_is_of_type_FiscalYear()
        {
            // Arrange
            var lastUnitOfTimeInYear = new FiscalYear(2016);
            var expectedUnitsToDate = new List<FiscalYear> { lastUnitOfTimeInYear };

            // Act
            var actualUnitsToDate = lastUnitOfTimeInYear.GetUnitsToDate();

            // Assert
            actualUnitsToDate.Should().Equal(expectedUnitsToDate);
        }

        [Fact]
        public static void GetUnitsToDate___Should_return_list_with_only_lastUnitOfTimeInYear___When_lastUnitOfTimeInYear_is_of_type_GenericYear()
        {
            // Arrange
            var lastUnitOfTimeInYear = new GenericYear(2016);
            var expectedUnitsToDate = new List<GenericYear> { lastUnitOfTimeInYear };

            // Act
            var actualUnitsToDate = lastUnitOfTimeInYear.GetUnitsToDate();

            // Assert
            actualUnitsToDate.Should().Equal(expectedUnitsToDate);
        }

        // ReSharper restore InconsistentNaming
    }
}

// ReSharper restore CheckNamespace