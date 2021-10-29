// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalendarMonthTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Test
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using FakeItEasy;

    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.AutoFakeItEasy;
    using OBeautifulCode.CodeAnalysis.Recipes;
    using OBeautifulCode.CodeGen.ModelObject.Recipes;

    using Xunit;

    public static partial class CalendarMonthTest
    {
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static CalendarMonthTest()
        {
            var referenceCalendarMonth = A.Dummy<CalendarMonth>();

            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CalendarMonth>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'year' is < 0",
                        ConstructionFunc = () => new CalendarMonth(A.Dummy<NegativeInteger>(), referenceCalendarMonth.MonthOfYear),
                        ExpectedExceptionMessageContains = new[] { "'year' < '1'" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CalendarMonth>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'year' = 0",
                        ConstructionFunc = () => new CalendarMonth(0, referenceCalendarMonth.MonthOfYear),
                        ExpectedExceptionMessageContains = new[] { "'year' < '1'" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CalendarMonth>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'year' = 10000",
                        ConstructionFunc = () => new CalendarMonth(10000, referenceCalendarMonth.MonthOfYear),
                        ExpectedExceptionMessageContains = new[] { "'year' > '9999'" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CalendarMonth>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'year' > 9999",
                        ConstructionFunc = () => new CalendarMonth(A.Dummy<PositiveInteger>().ThatIs(_ => _ > 9999, -1), referenceCalendarMonth.MonthOfYear),
                        ExpectedExceptionMessageContains = new[] { "'year' > '9999'" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CalendarMonth>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'monthOfYear' is MonthOfYear.Invalid",
                        ConstructionFunc = () => new CalendarMonth(referenceCalendarMonth.Year, MonthOfYear.Invalid),
                        ExpectedExceptionMessageContains = new[] { "monthOfYear", "Invalid" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    });

            ComparableTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ComparableTestScenario<CalendarMonth>
                    {
                        Name = "Comparable Test Scenario",
                        ReferenceObject = referenceCalendarMonth,
                        ObjectsThatAreLessThanReferenceObject = new[]
                        {
                            referenceCalendarMonth.TweakBy(-1, CalendarMonthComponent.Month),
                            referenceCalendarMonth.TweakBy(-1, CalendarMonthComponent.Year),
                        },
                        ObjectsThatAreGreaterThanReferenceObject = new[]
                        {
                            referenceCalendarMonth.TweakBy(1, CalendarMonthComponent.Month),
                            referenceCalendarMonth.TweakBy(1, CalendarMonthComponent.Year),
                        },
                        ObjectsThatAreEqualToButNotTheSameAsReferenceObject = new[]
                        {
                            referenceCalendarMonth.DeepClone(),
                        },
                        ObjectsThatAreNotOfTheSameTypeAsReferenceObject = new[]
                            {
                                A.Dummy<object>(),
                                A.Dummy<string>(),
                                A.Dummy<int>(),
                                A.Dummy<int?>(),
                                A.Dummy<Guid>(),
                            }
                            .Concat(TestCommon.GetDummyOfEachUnitOfTimeKind().Where(_ => _.GetType() != typeof(CalendarMonth)))
                            .ToList(),
                        ObjectsThatDeriveFromScenarioTypeButAreNotOfTheSameTypeAsReferenceObject = new CalendarMonth[]
                        {
                        },
                    });

            StringRepresentationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new StringRepresentationTestScenario<CalendarMonth>
                    {
                        Name = "String Representation - case 1",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                            new SystemUnderTestExpectedStringRepresentation<CalendarMonth>
                            {
                                SystemUnderTest = new CalendarMonth(2017, MonthOfYear.November),
                                ExpectedStringRepresentation = "2017-11",
                            },
                    })
                .AddScenario(() =>
                    new StringRepresentationTestScenario<CalendarMonth>
                    {
                        Name = "String Representation - case 2",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                            new SystemUnderTestExpectedStringRepresentation<CalendarMonth>
                            {
                                SystemUnderTest = new CalendarMonth(2017, MonthOfYear.February),
                                ExpectedStringRepresentation = "2017-02",
                            },
                    });
        }

        [Fact]
        public static void MonthNumber___Should_return_month_number_of_monthOfYear_passed_to_constructor___When_getting()
        {
            // Arrange
            var validMonth = A.Dummy<CalendarMonth>();

            var year = validMonth.Year;
            var monthOfYear = validMonth.MonthOfYear;

            var systemUnderTest = new CalendarMonth(year, monthOfYear);

            // Act
            var actualMonthNumber = systemUnderTest.MonthNumber;

            // Assert
            actualMonthNumber.AsTest().Must().BeEqualTo((MonthNumber)monthOfYear);
        }

        [Fact]
        public static void UnitOfTimeKind__Should_return_Calendar___When_called()
        {
            // Arrange
            var unitOfTime = A.Dummy<CalendarMonth>();

            // Act
            var kind = unitOfTime.UnitOfTimeKind;

            // Assert
            kind.AsTest().Must().BeEqualTo(UnitOfTimeKind.Calendar);
        }

        [Fact]
        public static void UnitOfTimeGranularity__Should_return_Month___When_called()
        {
            // Arrange
            var unitOfTime = A.Dummy<CalendarMonth>();

            // Act
            var granularity = unitOfTime.UnitOfTimeGranularity;

            // Assert
            granularity.AsTest().Must().BeEqualTo(UnitOfTimeGranularity.Month);
        }

        #pragma warning disable SA1201 // Elements should appear in the correct order
        private enum CalendarMonthComponent
        #pragma warning restore SA1201 // Elements should appear in the correct order
        {
            Month,

            Year,
        }

        private static CalendarMonth TweakBy(
            this CalendarMonth calendarMonth,
            int amount,
            CalendarMonthComponent componentToTweak)
        {
            if (componentToTweak == CalendarMonthComponent.Month)
            {
                var referenceMonth = new DateTime(calendarMonth.Year, (int)calendarMonth.MonthOfYear, 1);

                var updatedMonth = referenceMonth.AddMonths(amount);

                var result = new CalendarMonth(updatedMonth.Year, (MonthOfYear)updatedMonth.Month);

                return result;
            }

            if (componentToTweak == CalendarMonthComponent.Year)
            {
                var result = new CalendarMonth(calendarMonth.Year + amount, calendarMonth.MonthOfYear);

                return result;
            }

            throw new NotSupportedException("this calendar month component is not supported: " + componentToTweak);
        }
    }
}