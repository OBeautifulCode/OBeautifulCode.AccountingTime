// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalendarDayTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Test
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;

    using FakeItEasy;

    using OBeautifulCode.AccountingTime.Test.Internal;
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.AutoFakeItEasy;
    using OBeautifulCode.CodeGen.ModelObject.Recipes;
    using OBeautifulCode.Equality.Recipes;

    using Xunit;

    using static System.FormattableString;

    public static partial class CalendarDayTest
    {
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static CalendarDayTest()
        {
            var referenceCalendarDay = A.Dummy<CalendarDay>();

            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CalendarDay>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'year' is < 0",
                        ConstructionFunc = () => new CalendarDay(A.Dummy<NegativeInteger>(), referenceCalendarDay.MonthOfYear, referenceCalendarDay.DayOfMonth),
                        ExpectedExceptionMessageContains = new[] { "'year' < '1'" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CalendarDay>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'year' = 0",
                        ConstructionFunc = () => new CalendarDay(0, referenceCalendarDay.MonthOfYear, referenceCalendarDay.DayOfMonth),
                        ExpectedExceptionMessageContains = new[] { "'year' < '1'" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CalendarDay>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'year' = 10000",
                        ConstructionFunc = () => new CalendarDay(10000, referenceCalendarDay.MonthOfYear, referenceCalendarDay.DayOfMonth),
                        ExpectedExceptionMessageContains = new[] { "'year' > '9999'" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CalendarDay>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'year' > 9999",
                        ConstructionFunc = () => new CalendarDay(A.Dummy<PositiveInteger>().ThatIs(_ => _ > 9999), referenceCalendarDay.MonthOfYear, referenceCalendarDay.DayOfMonth),
                        ExpectedExceptionMessageContains = new[] { "'year' > '9999'" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CalendarDay>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'monthOfYear' is MonthOfYear.Invalid",
                        ConstructionFunc = () => new CalendarDay(referenceCalendarDay.Year, MonthOfYear.Invalid, referenceCalendarDay.DayOfMonth),
                        ExpectedExceptionMessageContains = new[] { "monthOfYear", "Invalid" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CalendarDay>
                    {
                        Name = "Constructor should throw ArgumentOutOfRangeException when parameter 'dayOfMonth' is DayOfMonth.Invalid",
                        ConstructionFunc = () => new CalendarDay(referenceCalendarDay.Year, referenceCalendarDay.MonthOfYear, DayOfMonth.Invalid),
                        ExpectedExceptionMessageContains = new[] { "dayOfMonth", "Invalid" },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CalendarDay>
                    {
                        Name = "Constructor should throw ArgumentException when parameter 'dayOfMonth' is not a valid day in the specified 'year' and 'monthOfYear' - case 1",
                        ConstructionFunc = () => new CalendarDay(2016, MonthOfYear.February, (DayOfMonth)30),
                        ExpectedExceptionMessageContains = new[] { "day (Thirty) is not a valid day in the specified month (February) and year (2016)" },
                        ExpectedExceptionType = typeof(ArgumentException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CalendarDay>
                    {
                        Name = "Constructor should throw ArgumentException when parameter 'dayOfMonth' is not a valid day in the specified 'year' and 'monthOfYear' - case 2",
                        ConstructionFunc = () => new CalendarDay(2016, MonthOfYear.February, (DayOfMonth)31),
                        ExpectedExceptionMessageContains = new[] { "day (ThirtyOne) is not a valid day in the specified month (February) and year (2016)" },
                        ExpectedExceptionType = typeof(ArgumentException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CalendarDay>
                    {
                        Name = "Constructor should throw ArgumentException when parameter 'dayOfMonth' is not a valid day in the specified 'year' and 'monthOfYear' - case 3",
                        ConstructionFunc = () => new CalendarDay(2015, MonthOfYear.February, (DayOfMonth)29),
                        ExpectedExceptionMessageContains = new[] { "day (TwentyNine) is not a valid day in the specified month (February) and year (2015)" },
                        ExpectedExceptionType = typeof(ArgumentException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CalendarDay>
                    {
                        Name = "Constructor should throw ArgumentException when parameter 'dayOfMonth' is not a valid day in the specified 'year' and 'monthOfYear' - case 4",
                        ConstructionFunc = () => new CalendarDay(2015, MonthOfYear.February, (DayOfMonth)30),
                        ExpectedExceptionMessageContains = new[] { "day (Thirty) is not a valid day in the specified month (February) and year (2015)" },
                        ExpectedExceptionType = typeof(ArgumentException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CalendarDay>
                    {
                        Name = "Constructor should throw ArgumentException when parameter 'dayOfMonth' is not a valid day in the specified 'year' and 'monthOfYear' - case 5",
                        ConstructionFunc = () => new CalendarDay(2015, MonthOfYear.February, (DayOfMonth)31),
                        ExpectedExceptionMessageContains = new[] { "day (ThirtyOne) is not a valid day in the specified month (February) and year (2015)" },
                        ExpectedExceptionType = typeof(ArgumentException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CalendarDay>
                    {
                        Name = "Constructor should throw ArgumentException when parameter 'dayOfMonth' is not a valid day in the specified 'year' and 'monthOfYear' - case 6",
                        ConstructionFunc = () => new CalendarDay(2016, MonthOfYear.November, (DayOfMonth)31),
                        ExpectedExceptionMessageContains = new[] { "day (ThirtyOne) is not a valid day in the specified month (November) and year (2016)" },
                        ExpectedExceptionType = typeof(ArgumentException),
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CalendarDay>
                    {
                        Name = "Constructor should throw ArgumentException when parameter 'dayOfMonth' is not a valid day in the specified 'year' and 'monthOfYear' - case 7",
                        ConstructionFunc = () => new CalendarDay(2017, MonthOfYear.April, (DayOfMonth)31),
                        ExpectedExceptionMessageContains = new[] { "day (ThirtyOne) is not a valid day in the specified month (April) and year (2017)" },
                        ExpectedExceptionType = typeof(ArgumentException),
                    });

            DeepCloneWithTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<CalendarDay>
                    {
                        Name = Invariant($"{nameof(CalendarDay.DeepCloneWithYear)} should deep clone object and replace {nameof(CalendarDay.Year)} with the provided year"),
                        WithPropertyName = nameof(CalendarDay.Year),
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<CalendarDay>();

                            var referenceObject = A.Dummy<CalendarDay>().ThatIs(_ => !systemUnderTest.Year.IsEqualTo(_.Year));

                            var result = new SystemUnderTestDeepCloneWithValue<CalendarDay>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = referenceObject.Year,
                            };

                            return result;
                        },
                    })
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<CalendarDay>
                    {
                        Name = Invariant($"{nameof(CalendarDay.DeepCloneWithMonthOfYear)} should deep clone object and replace {nameof(CalendarDay.MonthOfYear)} with the provided monthOfYear"),
                        WithPropertyName = nameof(CalendarDay.MonthOfYear),
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<CalendarDay>().ThatIs(_ => _.DayOfMonth <= DayOfMonth.TwentyEight);

                            var referenceObject = A.Dummy<CalendarDay>().ThatIs(_ => !systemUnderTest.MonthOfYear.IsEqualTo(_.MonthOfYear));

                            var result = new SystemUnderTestDeepCloneWithValue<CalendarDay>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = referenceObject.MonthOfYear,
                            };

                            return result;
                        },
                    })
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<CalendarDay>
                    {
                        Name = Invariant($"{nameof(CalendarDay.DeepCloneWithDayOfMonth)} should deep clone object and replace {nameof(CalendarDay.DayOfMonth)} with the provided dayOfMonth"),
                        WithPropertyName = nameof(CalendarDay.DayOfMonth),
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<CalendarDay>().Whose(_ => new[] { MonthOfYear.January, MonthOfYear.March, MonthOfYear.March, MonthOfYear.July, MonthOfYear.August, MonthOfYear.October, MonthOfYear.December }.Contains(_.MonthOfYear));

                            var referenceObject = A.Dummy<CalendarDay>().ThatIs(_ => !systemUnderTest.DayOfMonth.IsEqualTo(_.DayOfMonth));

                            var result = new SystemUnderTestDeepCloneWithValue<CalendarDay>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = referenceObject.DayOfMonth,
                            };

                            return result;
                        },
                    });

            EquatableTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new EquatableTestScenario<CalendarDay>
                    {
                        Name = "Equatable Test Scenario",
                        ReferenceObject = ReferenceObjectForEquatableTestScenarios,
                        ObjectsThatAreEqualToButNotTheSameAsReferenceObject = new[]
                        {
                            new CalendarDay(
                                    ReferenceObjectForEquatableTestScenarios.Year,
                                    ReferenceObjectForEquatableTestScenarios.MonthOfYear,
                                    ReferenceObjectForEquatableTestScenarios.DayOfMonth),
                        },
                        ObjectsThatAreNotEqualToReferenceObject = new[]
                        {
                            ReferenceObjectForEquatableTestScenarios.Tweak(CalendarDayComponent.Day),
                            ReferenceObjectForEquatableTestScenarios.Tweak(CalendarDayComponent.Month),
                            ReferenceObjectForEquatableTestScenarios.Tweak(CalendarDayComponent.Year),
                        },
                        ObjectsThatAreNotOfTheSameTypeAsReferenceObject = new[]
                            {
                                A.Dummy<object>(),
                                A.Dummy<string>(),
                                A.Dummy<int>(),
                                A.Dummy<int?>(),
                                A.Dummy<Guid>(),
                            }
                            .Concat(TestCommon.GetDummyOfEachUnitOfTimeKind().Where(_ => _.GetType() != typeof(CalendarDay)))
                            .ToList(),
                    });

            ComparableTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ComparableTestScenario<CalendarDay>
                    {
                        Name = "Comparable Test Scenario",
                        ReferenceObject = referenceCalendarDay,
                        ObjectsThatAreLessThanReferenceObject = new[]
                        {
                            referenceCalendarDay.TweakBy(-1, CalendarDayComponent.Day),
                            referenceCalendarDay.TweakBy(-1, CalendarDayComponent.Month),
                            referenceCalendarDay.TweakBy(-1, CalendarDayComponent.Year),
                        },
                        ObjectsThatAreGreaterThanReferenceObject = new[]
                        {
                            referenceCalendarDay.TweakBy(1, CalendarDayComponent.Day),
                            referenceCalendarDay.TweakBy(1, CalendarDayComponent.Month),
                            referenceCalendarDay.TweakBy(1, CalendarDayComponent.Year),
                        },
                        ObjectsThatAreEqualToButNotTheSameAsReferenceObject = new[]
                        {
                            referenceCalendarDay.DeepClone(),
                        },
                        ObjectsThatAreNotOfTheSameTypeAsReferenceObject = new[]
                        {
                            A.Dummy<object>(),
                            A.Dummy<string>(),
                            A.Dummy<int>(),
                            A.Dummy<int?>(),
                            A.Dummy<Guid>(),
                            A.Dummy<CalendarMonth>(),
                            A.Dummy<CalendarQuarter>(),
                            A.Dummy<CalendarUnbounded>(),
                            A.Dummy<CalendarYear>(),
                            A.Dummy<FiscalMonth>(),
                            A.Dummy<FiscalQuarter>(),
                            A.Dummy<FiscalUnbounded>(),
                            A.Dummy<FiscalYear>(),
                            A.Dummy<GenericMonth>(),
                            A.Dummy<GenericQuarter>(),
                            A.Dummy<GenericUnbounded>(),
                            A.Dummy<GenericYear>(),
                        },
                        ObjectsThatDeriveFromScenarioTypeButAreNotOfTheSameTypeAsReferenceObject = new CalendarDay[]
                        {
                        },
                    });

            StringRepresentationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new StringRepresentationTestScenario<CalendarDay>
                    {
                        Name = "String Representation - case 1",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                            new SystemUnderTestExpectedStringRepresentation<CalendarDay>
                            {
                                SystemUnderTest = new CalendarDay(2017, MonthOfYear.November, DayOfMonth.Thirty),
                                ExpectedStringRepresentation = "2017-11-30",
                            },
                    })
                .AddScenario(() =>
                    new StringRepresentationTestScenario<CalendarDay>
                    {
                        Name = "String Representation - case 2",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                            new SystemUnderTestExpectedStringRepresentation<CalendarDay>
                            {
                                SystemUnderTest = new CalendarDay(2017, MonthOfYear.February, DayOfMonth.Three),
                                ExpectedStringRepresentation = "2017-02-03",
                            },
                    });
        }

        [Fact]
        public static void MonthNumber___Should_return_month_number_of_monthOfYear_passed_to_constructor___When_getting()
        {
            // Arrange
            var validDay = A.Dummy<CalendarDay>();

            var year = validDay.Year;
            var monthOfYear = validDay.MonthOfYear;
            var dayOfMonth = validDay.DayOfMonth;

            var systemUnderTest = new CalendarDay(year, monthOfYear, dayOfMonth);

            // Act
            var actualMonthNumber = systemUnderTest.MonthNumber;

            // Assert
            actualMonthNumber.AsTest().Must().BeEqualTo((MonthNumber)monthOfYear);
        }

        [Fact]
        public static void UnitOfTimeKind__Should_return_Calendar___When_called()
        {
            // Arrange
            var unitOfTime = A.Dummy<CalendarDay>();

            // Act
            var kind = unitOfTime.UnitOfTimeKind;

            // Assert
            kind.AsTest().Must().BeEqualTo(UnitOfTimeKind.Calendar);
        }

        [Fact]
        public static void UnitOfTimeGranularity__Should_return_Day___When_called()
        {
            // Arrange
            var unitOfTime = A.Dummy<CalendarDay>();

            // Act
            var granularity = unitOfTime.UnitOfTimeGranularity;

            // Assert
            granularity.AsTest().Must().BeEqualTo(UnitOfTimeGranularity.Day);
        }

        [Fact]
        public static void ToDateTime___Should_return_DateTime_version_of_CalendarDay_with_DateTimeKind_Unspecified___When_called()
        {
            // Arrange
            var systemUnderTest1 = A.Dummy<CalendarDay>();
            var systemUnderTest2 = new CalendarDay(2017, MonthOfYear.November, DayOfMonth.Thirty);

            var expectedDateTime1 = new DateTime(systemUnderTest1.Year, (int)systemUnderTest1.MonthOfYear, (int)systemUnderTest1.DayOfMonth);
            var expectedDateTime2 = new DateTime(2017, 11, 30);

            // Act
            var dateTime1 = systemUnderTest1.ToDateTime();
            var dateTime2 = systemUnderTest2.ToDateTime();

            // Assert
            dateTime1.AsTest().Must().BeEqualTo(expectedDateTime1);
            dateTime1.Kind.AsTest().Must().BeEqualTo(DateTimeKind.Unspecified);

            dateTime2.AsTest().Must().BeEqualTo(expectedDateTime2);
            dateTime2.Kind.AsTest().Must().BeEqualTo(DateTimeKind.Unspecified);
        }

        [Fact]
        public static void ToString_format_formatProvider___Should_return_result_of_calling_ToString_on_DateTime_representation_of_object___When_calling_overload_with_formatting()
        {
            // Arrange
            var systemUnderTest1 = new CalendarDay(2017, MonthOfYear.November, DayOfMonth.Thirty);
            var systemUnderTest2 = new CalendarDay(2017, MonthOfYear.February, DayOfMonth.Three);

            // Act
            var toString1A = systemUnderTest1.ToString("MM yyyy dd");
            var toString1B = systemUnderTest1.ToString("MM yyyy dd", CultureInfo.CurrentCulture);
            var toString2A = systemUnderTest2.ToString("MM yyyy dd");
            var toString2B = systemUnderTest2.ToString("MM yyyy dd", CultureInfo.CurrentCulture);

            // Assert
            toString1A.AsTest().Must().BeEqualTo("11 2017 30");
            toString1B.AsTest().Must().BeEqualTo("11 2017 30");
            toString2A.AsTest().Must().BeEqualTo("02 2017 03");
            toString2B.AsTest().Must().BeEqualTo("02 2017 03");
        }

        #pragma warning disable SA1201 // Elements should appear in the correct order
        private enum CalendarDayComponent
        #pragma warning restore SA1201 // Elements should appear in the correct order
        {
            Day,

            Month,

            Year,
        }

        [SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "OBeautifulCode.AccountingTime.CalendarDay", Justification = "In this case we are trying to determine if creating the object will throw.")]
        private static CalendarDay Tweak(
            this CalendarDay calendarDay,
            CalendarDayComponent componentToTweak)
        {
            if (componentToTweak == CalendarDayComponent.Day)
            {
                var tweakedDay = A.Dummy<DayOfMonth>().ThatIs(
                    d =>
                        d != calendarDay.DayOfMonth &&
                        DoesNotThrow(() => new CalendarDay(calendarDay.Year, calendarDay.MonthOfYear, d)));
                var result = new CalendarDay(calendarDay.Year, calendarDay.MonthOfYear, tweakedDay);
                return result;
            }

            if (componentToTweak == CalendarDayComponent.Month)
            {
                var tweakedMonth = A.Dummy<MonthOfYear>().ThatIs(
                    m =>
                        m != calendarDay.MonthOfYear &&
                        DoesNotThrow(() => new CalendarDay(calendarDay.Year, m, calendarDay.DayOfMonth)));
                var result = new CalendarDay(calendarDay.Year, tweakedMonth, calendarDay.DayOfMonth);
                return result;
            }

            if (componentToTweak == CalendarDayComponent.Year)
            {
                var tweakedYear = A.Dummy<PositiveInteger>().ThatIs(
                    y =>
                        y != calendarDay.Year &&
                        y <= 9999 &&
                        DoesNotThrow(() => new CalendarDay(y, calendarDay.MonthOfYear, calendarDay.DayOfMonth)));
                var result = new CalendarDay(tweakedYear, calendarDay.MonthOfYear, calendarDay.DayOfMonth);
                return result;
            }

            throw new NotSupportedException("this calendar day component is not supported: " + componentToTweak);
        }

        private static CalendarDay TweakBy(
            this CalendarDay calendarDay,
            int amount,
            CalendarDayComponent componentToTweak)
        {
            if (componentToTweak == CalendarDayComponent.Day)
            {
                var updatedDateTime = calendarDay.ToDateTime().AddDays(amount);

                var result = new CalendarDay(updatedDateTime.Year, (MonthOfYear)updatedDateTime.Month, (DayOfMonth)updatedDateTime.Day);

                return result;
            }

            if (componentToTweak == CalendarDayComponent.Month)
            {
                var updatedDateTime = calendarDay.ToDateTime().AddMonths(amount);

                var result = new CalendarDay(updatedDateTime.Year, (MonthOfYear)updatedDateTime.Month, (DayOfMonth)updatedDateTime.Day);

                return result;
            }

            if (componentToTweak == CalendarDayComponent.Year)
            {
                var updatedDateTime = calendarDay.ToDateTime().AddYears(amount);

                var result = new CalendarDay(updatedDateTime.Year, (MonthOfYear)updatedDateTime.Month, (DayOfMonth)updatedDateTime.Day);

                return result;
            }

            throw new NotSupportedException("this calendar day component is not supported: " + componentToTweak);
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "The purpose of this method is to determine if any exception has been thrown.")]
        private static bool DoesNotThrow(Action action)
        {
            try
            {
                action();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}