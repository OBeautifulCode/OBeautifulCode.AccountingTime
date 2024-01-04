// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriodExtensionsTest.Manipulation.cs" company="OBeautifulCode">
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
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.AutoFakeItEasy;
    using OBeautifulCode.Equality.Recipes;
    using Xunit;

    [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Testing this class requires lots of types because of the number of unit-of-time types intersected with the options for reporting period.")]
    public static partial class ReportingPeriodExtensionsTest
    {
        [Fact]
        public static void CloneWithAdjustment___Should_throw_ArgumentNullException___When_parameter_reportingPeriod_is_null()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod>().Whose(_ => _.Start.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded && _.End.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded);
            var reportingPeriodComponent = A.Dummy<ReportingPeriodComponent>();
            var granularityToAdd = reportingPeriod.Start.UnitOfTimeGranularity;
            var unitsToAdd = A.Dummy<int>().ThatIs(i => i > -100 && i < 100, -1);

            // Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.CloneWithAdjustment(null, reportingPeriodComponent, unitsToAdd, granularityToAdd));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void CloneWithAdjustment___Should_throw_ArgumentOutOfRangeException___When_parameter_reportingPeriodComponent_is_Invalid()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod>().Whose(_ => _.Start.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded && _.End.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded);
            var granularityToAdd = reportingPeriod.Start.UnitOfTimeGranularity;
            var unitsToAdd = A.Dummy<int>().ThatIs(i => i > -100 && i < 100, -1);

            // Act
            var ex = Record.Exception(() => reportingPeriod.CloneWithAdjustment(ReportingPeriodComponent.Invalid, unitsToAdd, granularityToAdd));

            // Assert
            ex.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Addis", Justification = "this is spelled correctly")]
        public static void CloneWithAdjustment___Should_throw_ArgumentOutOfRangeException___When_parameter_granularityOfUnitsToAdd_is_Invalid()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod>().Whose(_ => _.Start.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded && _.End.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded);
            var reportingPeriodComponent = A.Dummy<ReportingPeriodComponent>();
            var unitsToAdd = A.Dummy<int>().ThatIs(i => i > -100 && i < 100, -1);

            // Act
            var ex = Record.Exception(() => reportingPeriod.CloneWithAdjustment(reportingPeriodComponent, unitsToAdd, UnitOfTimeGranularity.Invalid));

            // Assert
            ex.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Addis", Justification = "this is spelled correctly")]
        public static void CloneWithAdjustment___Should_throw_ArgumentOutOfRangeException___When_parameter_granularityOfUnitsToAdd_is_Unbounded()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod>().Whose(_ => _.Start.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded && _.End.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded);
            var reportingPeriodComponent = A.Dummy<ReportingPeriodComponent>();
            var unitsToAdd = A.Dummy<int>().ThatIs(i => i > -100 && i < 100, -1);

            // Act
            var ex = Record.Exception(() => reportingPeriod.CloneWithAdjustment(reportingPeriodComponent, unitsToAdd, UnitOfTimeGranularity.Unbounded));

            // Assert
            ex.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void CloneWithAdjustment___Should_throw_ArgumentException___When_adjusting_reportingPeriod_with_an_Unbounded_Start()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod>().Whose(_ => _.Start.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded && _.End.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded);
            var granularityToAdd = reportingPeriod.End.UnitOfTimeGranularity;
            var unitsToAdd = A.Dummy<int>().ThatIs(i => i > -100 && i < 100, -1);

            // Act
            var ex1 = Record.Exception(() => reportingPeriod.CloneWithAdjustment(ReportingPeriodComponent.Start, unitsToAdd, granularityToAdd));
            var ex2 = Record.Exception(() => reportingPeriod.CloneWithAdjustment(ReportingPeriodComponent.Both, unitsToAdd, granularityToAdd));

            // Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex2.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void CloneWithAdjustment___Should_throw_ArgumentException___When_adjusting_reportingPeriod_with_an_Unbounded_End()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod>().Whose(_ => _.Start.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded && _.End.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded);
            var granularityToAdd = reportingPeriod.Start.UnitOfTimeGranularity;
            var unitsToAdd = A.Dummy<int>().ThatIs(i => i > -100 && i < 100, -1);

            // Act
            var ex1 = Record.Exception(() => reportingPeriod.CloneWithAdjustment(ReportingPeriodComponent.End, unitsToAdd, granularityToAdd));
            var ex2 = Record.Exception(() => reportingPeriod.CloneWithAdjustment(ReportingPeriodComponent.Both, unitsToAdd, granularityToAdd));

            // Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex2.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Addis", Justification = "this is spelled correctly")]
        [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "testing all the flavors of unit-of-time")]
        public static void CloneWithAdjustment___Should_throw_ArgumentException___When_granularityOfUnitsToAdd_is_more_granular_than_component_being_adjusted()
        {
            // Arrange
            var unitsToAdd = A.Dummy<int>().ThatIs(i => i > -100 && i < 100, -1);
            var tests = new[]
            {
                new { ReportingPeriod = A.Dummy<CalendarDayReportingPeriod>().ReportingPeriod, GranularityOfUnitsToAdd = new UnitOfTimeGranularity[] { } },
                new { ReportingPeriod = A.Dummy<CalendarMonthReportingPeriod>().ReportingPeriod, GranularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Day } },
                new { ReportingPeriod = A.Dummy<CalendarQuarterReportingPeriod>().ReportingPeriod, GranularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Day, UnitOfTimeGranularity.Month } },
                new { ReportingPeriod = A.Dummy<CalendarYearReportingPeriod>().ReportingPeriod, GranularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Day, UnitOfTimeGranularity.Month, UnitOfTimeGranularity.Quarter } },
                new { ReportingPeriod = A.Dummy<FiscalMonthReportingPeriod>().ReportingPeriod, GranularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Day } },
                new { ReportingPeriod = A.Dummy<FiscalQuarterReportingPeriod>().ReportingPeriod, GranularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Day, UnitOfTimeGranularity.Month } },
                new { ReportingPeriod = A.Dummy<FiscalYearReportingPeriod>().ReportingPeriod, GranularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Day, UnitOfTimeGranularity.Month, UnitOfTimeGranularity.Quarter } },
                new { ReportingPeriod = A.Dummy<GenericMonthReportingPeriod>().ReportingPeriod, GranularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Day } },
                new { ReportingPeriod = A.Dummy<GenericQuarterReportingPeriod>().ReportingPeriod, GranularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Day, UnitOfTimeGranularity.Month } },
                new { ReportingPeriod = A.Dummy<GenericYearReportingPeriod>().ReportingPeriod, GranularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Day, UnitOfTimeGranularity.Month, UnitOfTimeGranularity.Quarter } },
            };

            // Act
            var exceptions = new List<Exception>();
            foreach (var test in tests)
            {
                foreach (var granularityOfUnitsToAdd in test.GranularityOfUnitsToAdd)
                {
                    exceptions.Add(Record.Exception(() => test.ReportingPeriod.CloneWithAdjustment(ReportingPeriodComponent.Start, unitsToAdd, granularityOfUnitsToAdd)));
                    exceptions.Add(Record.Exception(() => test.ReportingPeriod.CloneWithAdjustment(ReportingPeriodComponent.End, unitsToAdd, granularityOfUnitsToAdd)));
                    exceptions.Add(Record.Exception(() => test.ReportingPeriod.CloneWithAdjustment(ReportingPeriodComponent.Both, unitsToAdd, granularityOfUnitsToAdd)));
                }
            }

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<ArgumentException>());
        }

        [Fact]
        public static void CloneWithAdjustment___Should_adjust_Start_of_reportingPeriod___When_ReportingPeriodComponent_is_Start()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = 1,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Month,
                    ExpectedReportingPeriod = new ReportingPeriod(new FiscalMonth(2016, MonthNumber.Five), new FiscalMonth(2017, MonthNumber.Eleven)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = -2,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Quarter,
                    ExpectedReportingPeriod = new ReportingPeriod(new FiscalMonth(2015, MonthNumber.Ten), new FiscalMonth(2017, MonthNumber.Eleven)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalMonth(2014, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = 2,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year,
                    ExpectedReportingPeriod = new ReportingPeriod(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Eleven)),
                },
            };

            // Act, Assert
            foreach (var test in tests)
            {
                var actualReportingPeriod = test.ReportingPeriod.CloneWithAdjustment(ReportingPeriodComponent.Start, test.UnitsToAdd, test.GranularityOfUnitsToAdd);
                actualReportingPeriod.Should().Be(test.ExpectedReportingPeriod);
            }
        }

        [Fact]
        public static void CloneWithAdjustment___Should_adjust_End_of_reportingPeriod___When_ReportingPeriodComponent_is_End()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = 1,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Month,
                    ExpectedReportingPeriod = new ReportingPeriod(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Twelve)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = -2,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Quarter,
                    ExpectedReportingPeriod = new ReportingPeriod(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Five)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = 2,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year,
                    ExpectedReportingPeriod = new ReportingPeriod(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2019, MonthNumber.Eleven)),
                },
            };

            // Act, Assert
            foreach (var test in tests)
            {
                var actualReportingPeriod = test.ReportingPeriod.CloneWithAdjustment(ReportingPeriodComponent.End, test.UnitsToAdd, test.GranularityOfUnitsToAdd);
                actualReportingPeriod.Should().Be(test.ExpectedReportingPeriod);
            }
        }

        [Fact]
        public static void CloneWithAdjustment___Should_adjust_Start_and_End_of_reportingPeriod___When_ReportingPeriodComponent_is_Both()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = 1,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Month,
                    ExpectedReportingPeriod = new ReportingPeriod(new FiscalMonth(2016, MonthNumber.Five), new FiscalMonth(2017, MonthNumber.Twelve)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = -2,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Quarter,
                    ExpectedReportingPeriod = new ReportingPeriod(new FiscalMonth(2015, MonthNumber.Ten), new FiscalMonth(2017, MonthNumber.Five)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = 2,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year,
                    ExpectedReportingPeriod = new ReportingPeriod(new FiscalMonth(2018, MonthNumber.Four), new FiscalMonth(2019, MonthNumber.Eleven)),
                },
            };

            // Act, Assert
            foreach (var test in tests)
            {
                var actualReportingPeriod = test.ReportingPeriod.CloneWithAdjustment(ReportingPeriodComponent.Both, test.UnitsToAdd, test.GranularityOfUnitsToAdd);
                actualReportingPeriod.Should().Be(test.ExpectedReportingPeriod);
            }
        }

        [Fact]
        public static void CloneWithAdjustment___Should_adjust_Start_of_reportingPeriod___When_ReportingPeriodComponent_is_Start_and_reportingPeriod_End_is_unbounded()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalMonth(2016, MonthNumber.Four), new FiscalUnbounded()),
                    UnitsToAdd = 1,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Month,
                    ExpectedReportingPeriod = new ReportingPeriod(new FiscalMonth(2016, MonthNumber.Five), new FiscalUnbounded()),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalMonth(2016, MonthNumber.Four), new FiscalUnbounded()),
                    UnitsToAdd = -2,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Quarter,
                    ExpectedReportingPeriod = new ReportingPeriod(new FiscalMonth(2015, MonthNumber.Ten), new FiscalUnbounded()),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalMonth(2014, MonthNumber.Four), new FiscalUnbounded()),
                    UnitsToAdd = 2,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year,
                    ExpectedReportingPeriod = new ReportingPeriod(new FiscalMonth(2016, MonthNumber.Four), new FiscalUnbounded()),
                },
            };

            // Act, Assert
            foreach (var test in tests)
            {
                var actualReportingPeriod = test.ReportingPeriod.CloneWithAdjustment(ReportingPeriodComponent.Start, test.UnitsToAdd, test.GranularityOfUnitsToAdd);
                actualReportingPeriod.Should().Be(test.ExpectedReportingPeriod);
            }
        }

        [Fact]
        public static void CloneWithAdjustment___Should_adjust_Start_of_reportingPeriod___When_ReportingPeriodComponent_is_End_and_reportingPeriod_Start_is_unbounded()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalUnbounded(), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = 1,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Month,
                    ExpectedReportingPeriod = new ReportingPeriod(new FiscalUnbounded(), new FiscalMonth(2017, MonthNumber.Twelve)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalUnbounded(), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = -2,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Quarter,
                    ExpectedReportingPeriod = new ReportingPeriod(new FiscalUnbounded(), new FiscalMonth(2017, MonthNumber.Five)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalUnbounded(), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = 2,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year,
                    ExpectedReportingPeriod = new ReportingPeriod(new FiscalUnbounded(), new FiscalMonth(2019, MonthNumber.Eleven)),
                },
            };

            // Act, Assert
            foreach (var test in tests)
            {
                var actualReportingPeriod = test.ReportingPeriod.CloneWithAdjustment(ReportingPeriodComponent.End, test.UnitsToAdd, test.GranularityOfUnitsToAdd);
                actualReportingPeriod.Should().Be(test.ExpectedReportingPeriod);
            }
        }

        [Fact]
        public static void CloneWithAdjustment___Should_throw_InvalidOperationException___When_adjusting_Start_and_adjusting_reporting_period_causes_Start_to_be_Greater_than_End___When_called()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2016, MonthNumber.Eleven)),
                    UnitsToAdd = 8,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Month,
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalMonth(2016, MonthNumber.Three), new FiscalMonth(2016, MonthNumber.Eleven)),
                    UnitsToAdd = 3,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Quarter,
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalMonth(2014, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Three)),
                    UnitsToAdd = 3,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year,
                },
            };

            // Act, Assert
            foreach (var test in tests)
            {
                var ex = Record.Exception(() => test.ReportingPeriod.CloneWithAdjustment(ReportingPeriodComponent.Start, test.UnitsToAdd, test.GranularityOfUnitsToAdd));
                ex.Should().BeOfType<InvalidOperationException>();
            }
        }

        [Fact]
        public static void CloneWithAdjustment___Should_throw_InvalidOperationException___When_adjusting_End_and_adjusting_reporting_period_causes_Start_to_be_Greater_than_End___When_called()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2016, MonthNumber.Eleven)),
                    UnitsToAdd = -8,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Month,
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalMonth(2016, MonthNumber.Three), new FiscalMonth(2016, MonthNumber.Eleven)),
                    UnitsToAdd = -3,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Quarter,
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalMonth(2014, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Three)),
                    UnitsToAdd = -3,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year,
                },
            };

            // Act, Assert
            foreach (var test in tests)
            {
                var ex = Record.Exception(() => test.ReportingPeriod.CloneWithAdjustment(ReportingPeriodComponent.End, test.UnitsToAdd, test.GranularityOfUnitsToAdd));
                ex.Should().BeOfType<InvalidOperationException>();
            }
        }

        [Fact]
        public static void CreatePermutations___Should_throw_ArgumentNullException___When_parameter_reportingPeriod_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.CreatePermutations(null, A.Dummy<PositiveInteger>()));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void CreatePermutations___Should_throw_ArgumentException___When_parameter_reportingPeriod_Start_and_or_End_is_unbounded()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod(new CalendarUnbounded(), A.Dummy<CalendarUnitOfTime>());
            var reportingPeriod2 = new ReportingPeriod(A.Dummy<GenericUnitOfTime>(), new GenericUnbounded());
            var reportingPeriod3 = new ReportingPeriod(new FiscalUnbounded(), new FiscalUnbounded());

            // Act
            var ex1 = Record.Exception(() => reportingPeriod1.CreatePermutations(A.Dummy<PositiveInteger>()));
            var ex2 = Record.Exception(() => reportingPeriod2.CreatePermutations(A.Dummy<PositiveInteger>()));
            var ex3 = Record.Exception(() => reportingPeriod3.CreatePermutations(A.Dummy<PositiveInteger>()));

            // Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex2.Should().BeOfType<ArgumentException>();
            ex3.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void CreatePermutations___Should_throw_ArgumentOutOfRangeException___When_parameter_maxUnitsInAnyReportingPeriod_is_0_or_less()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod>().Whose(_ => _.Start.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded && _.End.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded);

            // Act
            var ex1 = Record.Exception(() => reportingPeriod.CreatePermutations(0));
            var ex2 = Record.Exception(() => reportingPeriod.CreatePermutations(A.Dummy<NegativeInteger>()));

            // Assert
            ex1.Should().BeOfType<ArgumentOutOfRangeException>();
            ex2.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void CreatePermutations___Should_return_permutations___When_called_for_reporting_period_of_CalendarDay()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight));
            var reportingPeriod2 = new ReportingPeriod(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Three));

            // Act
            var permutations1a = reportingPeriod1.CreatePermutations(1);
            var permutations1b = reportingPeriod1.CreatePermutations(5);

            var permutations2a = reportingPeriod2.CreatePermutations(1);
            var permutations2b = reportingPeriod2.CreatePermutations(3);

            // Assert
            permutations1a.Should().Equal(reportingPeriod1);
            permutations1b.Should().Equal(reportingPeriod1);

            permutations2a.Should().Equal(
                new ReportingPeriod(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight)),
                new ReportingPeriod(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine)),
                new ReportingPeriod(new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One)),
                new ReportingPeriod(new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Two), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Two)),
                new ReportingPeriod(new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Three), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Three)));

            permutations2b.Should().Equal(
                new ReportingPeriod(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight)),
                new ReportingPeriod(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine)),
                new ReportingPeriod(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One)),
                new ReportingPeriod(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine)),
                new ReportingPeriod(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One)),
                new ReportingPeriod(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Two)),
                new ReportingPeriod(new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One)),
                new ReportingPeriod(new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Two)),
                new ReportingPeriod(new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Three)),
                new ReportingPeriod(new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Two), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Two)),
                new ReportingPeriod(new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Two), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Three)),
                new ReportingPeriod(new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Three), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Three)));
        }

        [Fact]
        public static void CreatePermutations___Should_return_permutations___When_called_for_reporting_period_of_CalendarMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.February));
            var reportingPeriod2 = new ReportingPeriod(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.June));

            // Act
            var permutations1a = reportingPeriod1.CreatePermutations(1);
            var permutations1b = reportingPeriod1.CreatePermutations(5);

            var permutations2a = reportingPeriod2.CreatePermutations(1);
            var permutations2b = reportingPeriod2.CreatePermutations(3);

            // Assert
            permutations1a.Should().Equal(reportingPeriod1);
            permutations1b.Should().Equal(reportingPeriod1);

            permutations2a.Should().Equal(
                new ReportingPeriod(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.February)),
                new ReportingPeriod(new CalendarMonth(2016, MonthOfYear.March), new CalendarMonth(2016, MonthOfYear.March)),
                new ReportingPeriod(new CalendarMonth(2016, MonthOfYear.April), new CalendarMonth(2016, MonthOfYear.April)),
                new ReportingPeriod(new CalendarMonth(2016, MonthOfYear.May), new CalendarMonth(2016, MonthOfYear.May)),
                new ReportingPeriod(new CalendarMonth(2016, MonthOfYear.June), new CalendarMonth(2016, MonthOfYear.June)));

            permutations2b.Should().Equal(
                new ReportingPeriod(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.February)),
                new ReportingPeriod(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.March)),
                new ReportingPeriod(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.April)),
                new ReportingPeriod(new CalendarMonth(2016, MonthOfYear.March), new CalendarMonth(2016, MonthOfYear.March)),
                new ReportingPeriod(new CalendarMonth(2016, MonthOfYear.March), new CalendarMonth(2016, MonthOfYear.April)),
                new ReportingPeriod(new CalendarMonth(2016, MonthOfYear.March), new CalendarMonth(2016, MonthOfYear.May)),
                new ReportingPeriod(new CalendarMonth(2016, MonthOfYear.April), new CalendarMonth(2016, MonthOfYear.April)),
                new ReportingPeriod(new CalendarMonth(2016, MonthOfYear.April), new CalendarMonth(2016, MonthOfYear.May)),
                new ReportingPeriod(new CalendarMonth(2016, MonthOfYear.April), new CalendarMonth(2016, MonthOfYear.June)),
                new ReportingPeriod(new CalendarMonth(2016, MonthOfYear.May), new CalendarMonth(2016, MonthOfYear.May)),
                new ReportingPeriod(new CalendarMonth(2016, MonthOfYear.May), new CalendarMonth(2016, MonthOfYear.June)),
                new ReportingPeriod(new CalendarMonth(2016, MonthOfYear.June), new CalendarMonth(2016, MonthOfYear.June)));
        }

        [Fact]
        public static void CreatePermutations___Should_return_permutations___When_called_for_reporting_period_of_FiscalMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Two));
            var reportingPeriod2 = new ReportingPeriod(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Six));

            // Act
            var permutations1a = reportingPeriod1.CreatePermutations(1);
            var permutations1b = reportingPeriod1.CreatePermutations(5);

            var permutations2a = reportingPeriod2.CreatePermutations(1);
            var permutations2b = reportingPeriod2.CreatePermutations(3);

            // Assert
            permutations1a.Should().Equal(reportingPeriod1);
            permutations1b.Should().Equal(reportingPeriod1);

            permutations2a.Should().Equal(
                new ReportingPeriod(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Two)),
                new ReportingPeriod(new FiscalMonth(2016, MonthNumber.Three), new FiscalMonth(2016, MonthNumber.Three)),
                new ReportingPeriod(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2016, MonthNumber.Four)),
                new ReportingPeriod(new FiscalMonth(2016, MonthNumber.Five), new FiscalMonth(2016, MonthNumber.Five)),
                new ReportingPeriod(new FiscalMonth(2016, MonthNumber.Six), new FiscalMonth(2016, MonthNumber.Six)));

            permutations2b.Should().Equal(
                new ReportingPeriod(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Two)),
                new ReportingPeriod(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Three)),
                new ReportingPeriod(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Four)),
                new ReportingPeriod(new FiscalMonth(2016, MonthNumber.Three), new FiscalMonth(2016, MonthNumber.Three)),
                new ReportingPeriod(new FiscalMonth(2016, MonthNumber.Three), new FiscalMonth(2016, MonthNumber.Four)),
                new ReportingPeriod(new FiscalMonth(2016, MonthNumber.Three), new FiscalMonth(2016, MonthNumber.Five)),
                new ReportingPeriod(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2016, MonthNumber.Four)),
                new ReportingPeriod(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2016, MonthNumber.Five)),
                new ReportingPeriod(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2016, MonthNumber.Six)),
                new ReportingPeriod(new FiscalMonth(2016, MonthNumber.Five), new FiscalMonth(2016, MonthNumber.Five)),
                new ReportingPeriod(new FiscalMonth(2016, MonthNumber.Five), new FiscalMonth(2016, MonthNumber.Six)),
                new ReportingPeriod(new FiscalMonth(2016, MonthNumber.Six), new FiscalMonth(2016, MonthNumber.Six)));
        }

        [Fact]
        public static void CreatePermutations___Should_return_permutations___When_called_for_reporting_period_of_GenericMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Two));
            var reportingPeriod2 = new ReportingPeriod(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Six));

            // Act
            var permutations1a = reportingPeriod1.CreatePermutations(1);
            var permutations1b = reportingPeriod1.CreatePermutations(5);

            var permutations2a = reportingPeriod2.CreatePermutations(1);
            var permutations2b = reportingPeriod2.CreatePermutations(3);

            // Assert
            permutations1a.Should().Equal(reportingPeriod1);
            permutations1b.Should().Equal(reportingPeriod1);

            permutations2a.Should().Equal(
                new ReportingPeriod(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Two)),
                new ReportingPeriod(new GenericMonth(2016, MonthNumber.Three), new GenericMonth(2016, MonthNumber.Three)),
                new ReportingPeriod(new GenericMonth(2016, MonthNumber.Four), new GenericMonth(2016, MonthNumber.Four)),
                new ReportingPeriod(new GenericMonth(2016, MonthNumber.Five), new GenericMonth(2016, MonthNumber.Five)),
                new ReportingPeriod(new GenericMonth(2016, MonthNumber.Six), new GenericMonth(2016, MonthNumber.Six)));

            permutations2b.Should().Equal(
                new ReportingPeriod(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Two)),
                new ReportingPeriod(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Three)),
                new ReportingPeriod(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Four)),
                new ReportingPeriod(new GenericMonth(2016, MonthNumber.Three), new GenericMonth(2016, MonthNumber.Three)),
                new ReportingPeriod(new GenericMonth(2016, MonthNumber.Three), new GenericMonth(2016, MonthNumber.Four)),
                new ReportingPeriod(new GenericMonth(2016, MonthNumber.Three), new GenericMonth(2016, MonthNumber.Five)),
                new ReportingPeriod(new GenericMonth(2016, MonthNumber.Four), new GenericMonth(2016, MonthNumber.Four)),
                new ReportingPeriod(new GenericMonth(2016, MonthNumber.Four), new GenericMonth(2016, MonthNumber.Five)),
                new ReportingPeriod(new GenericMonth(2016, MonthNumber.Four), new GenericMonth(2016, MonthNumber.Six)),
                new ReportingPeriod(new GenericMonth(2016, MonthNumber.Five), new GenericMonth(2016, MonthNumber.Five)),
                new ReportingPeriod(new GenericMonth(2016, MonthNumber.Five), new GenericMonth(2016, MonthNumber.Six)),
                new ReportingPeriod(new GenericMonth(2016, MonthNumber.Six), new GenericMonth(2016, MonthNumber.Six)));
        }

        [Fact]
        public static void CreatePermutations___Should_return_permutations___When_called_for_reporting_period_of_CalendarQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q2));
            var reportingPeriod2 = new ReportingPeriod(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2017, QuarterNumber.Q2));

            // Act
            var permutations1a = reportingPeriod1.CreatePermutations(1);
            var permutations1b = reportingPeriod1.CreatePermutations(5);

            var permutations2a = reportingPeriod2.CreatePermutations(1);
            var permutations2b = reportingPeriod2.CreatePermutations(3);

            // Assert
            permutations1a.Should().Equal(reportingPeriod1);
            permutations1b.Should().Equal(reportingPeriod1);

            permutations2a.Should().Equal(
                new ReportingPeriod(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q2)),
                new ReportingPeriod(new CalendarQuarter(2016, QuarterNumber.Q3), new CalendarQuarter(2016, QuarterNumber.Q3)),
                new ReportingPeriod(new CalendarQuarter(2016, QuarterNumber.Q4), new CalendarQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod(new CalendarQuarter(2017, QuarterNumber.Q1), new CalendarQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod(new CalendarQuarter(2017, QuarterNumber.Q2), new CalendarQuarter(2017, QuarterNumber.Q2)));

            permutations2b.Should().Equal(
                new ReportingPeriod(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q2)),
                new ReportingPeriod(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q3)),
                new ReportingPeriod(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod(new CalendarQuarter(2016, QuarterNumber.Q3), new CalendarQuarter(2016, QuarterNumber.Q3)),
                new ReportingPeriod(new CalendarQuarter(2016, QuarterNumber.Q3), new CalendarQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod(new CalendarQuarter(2016, QuarterNumber.Q3), new CalendarQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod(new CalendarQuarter(2016, QuarterNumber.Q4), new CalendarQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod(new CalendarQuarter(2016, QuarterNumber.Q4), new CalendarQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod(new CalendarQuarter(2016, QuarterNumber.Q4), new CalendarQuarter(2017, QuarterNumber.Q2)),
                new ReportingPeriod(new CalendarQuarter(2017, QuarterNumber.Q1), new CalendarQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod(new CalendarQuarter(2017, QuarterNumber.Q1), new CalendarQuarter(2017, QuarterNumber.Q2)),
                new ReportingPeriod(new CalendarQuarter(2017, QuarterNumber.Q2), new CalendarQuarter(2017, QuarterNumber.Q2)));
        }

        [Fact]
        public static void CreatePermutations___Should_return_permutations___When_called_for_reporting_period_of_FiscalQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q2));
            var reportingPeriod2 = new ReportingPeriod(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2017, QuarterNumber.Q2));

            // Act
            var permutations1a = reportingPeriod1.CreatePermutations(1);
            var permutations1b = reportingPeriod1.CreatePermutations(5);

            var permutations2a = reportingPeriod2.CreatePermutations(1);
            var permutations2b = reportingPeriod2.CreatePermutations(3);

            // Assert
            permutations1a.Should().Equal(reportingPeriod1);
            permutations1b.Should().Equal(reportingPeriod1);

            permutations2a.Should().Equal(
                new ReportingPeriod(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q2)),
                new ReportingPeriod(new FiscalQuarter(2016, QuarterNumber.Q3), new FiscalQuarter(2016, QuarterNumber.Q3)),
                new ReportingPeriod(new FiscalQuarter(2016, QuarterNumber.Q4), new FiscalQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod(new FiscalQuarter(2017, QuarterNumber.Q1), new FiscalQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod(new FiscalQuarter(2017, QuarterNumber.Q2), new FiscalQuarter(2017, QuarterNumber.Q2)));

            permutations2b.Should().Equal(
                new ReportingPeriod(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q2)),
                new ReportingPeriod(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q3)),
                new ReportingPeriod(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod(new FiscalQuarter(2016, QuarterNumber.Q3), new FiscalQuarter(2016, QuarterNumber.Q3)),
                new ReportingPeriod(new FiscalQuarter(2016, QuarterNumber.Q3), new FiscalQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod(new FiscalQuarter(2016, QuarterNumber.Q3), new FiscalQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod(new FiscalQuarter(2016, QuarterNumber.Q4), new FiscalQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod(new FiscalQuarter(2016, QuarterNumber.Q4), new FiscalQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod(new FiscalQuarter(2016, QuarterNumber.Q4), new FiscalQuarter(2017, QuarterNumber.Q2)),
                new ReportingPeriod(new FiscalQuarter(2017, QuarterNumber.Q1), new FiscalQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod(new FiscalQuarter(2017, QuarterNumber.Q1), new FiscalQuarter(2017, QuarterNumber.Q2)),
                new ReportingPeriod(new FiscalQuarter(2017, QuarterNumber.Q2), new FiscalQuarter(2017, QuarterNumber.Q2)));
        }

        [Fact]
        public static void CreatePermutations___Should_return_permutations___When_called_for_reporting_period_of_GenericQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q2));
            var reportingPeriod2 = new ReportingPeriod(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2017, QuarterNumber.Q2));

            // Act
            var permutations1a = reportingPeriod1.CreatePermutations(1);
            var permutations1b = reportingPeriod1.CreatePermutations(5);

            var permutations2a = reportingPeriod2.CreatePermutations(1);
            var permutations2b = reportingPeriod2.CreatePermutations(3);

            // Assert
            permutations1a.Should().Equal(reportingPeriod1);
            permutations1b.Should().Equal(reportingPeriod1);

            permutations2a.Should().Equal(
                new ReportingPeriod(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q2)),
                new ReportingPeriod(new GenericQuarter(2016, QuarterNumber.Q3), new GenericQuarter(2016, QuarterNumber.Q3)),
                new ReportingPeriod(new GenericQuarter(2016, QuarterNumber.Q4), new GenericQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod(new GenericQuarter(2017, QuarterNumber.Q1), new GenericQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod(new GenericQuarter(2017, QuarterNumber.Q2), new GenericQuarter(2017, QuarterNumber.Q2)));

            permutations2b.Should().Equal(
                new ReportingPeriod(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q2)),
                new ReportingPeriod(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q3)),
                new ReportingPeriod(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod(new GenericQuarter(2016, QuarterNumber.Q3), new GenericQuarter(2016, QuarterNumber.Q3)),
                new ReportingPeriod(new GenericQuarter(2016, QuarterNumber.Q3), new GenericQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod(new GenericQuarter(2016, QuarterNumber.Q3), new GenericQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod(new GenericQuarter(2016, QuarterNumber.Q4), new GenericQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod(new GenericQuarter(2016, QuarterNumber.Q4), new GenericQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod(new GenericQuarter(2016, QuarterNumber.Q4), new GenericQuarter(2017, QuarterNumber.Q2)),
                new ReportingPeriod(new GenericQuarter(2017, QuarterNumber.Q1), new GenericQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod(new GenericQuarter(2017, QuarterNumber.Q1), new GenericQuarter(2017, QuarterNumber.Q2)),
                new ReportingPeriod(new GenericQuarter(2017, QuarterNumber.Q2), new GenericQuarter(2017, QuarterNumber.Q2)));
        }

        [Fact]
        public static void CreatePermutations___Should_return_permutations___When_called_for_reporting_period_of_CalendarYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod(new CalendarYear(2016), new CalendarYear(2016));
            var reportingPeriod2 = new ReportingPeriod(new CalendarYear(2016), new CalendarYear(2019));

            // Act
            var permutations1a = reportingPeriod1.CreatePermutations(1);
            var permutations1b = reportingPeriod1.CreatePermutations(5);

            var permutations2a = reportingPeriod2.CreatePermutations(1);
            var permutations2b = reportingPeriod2.CreatePermutations(3);

            // Assert
            permutations1a.Should().Equal(reportingPeriod1);
            permutations1b.Should().Equal(reportingPeriod1);

            permutations2a.Should().Equal(
                new ReportingPeriod(new CalendarYear(2016), new CalendarYear(2016)),
                new ReportingPeriod(new CalendarYear(2017), new CalendarYear(2017)),
                new ReportingPeriod(new CalendarYear(2018), new CalendarYear(2018)),
                new ReportingPeriod(new CalendarYear(2019), new CalendarYear(2019)));

            permutations2b.Should().Equal(
                new ReportingPeriod(new CalendarYear(2016), new CalendarYear(2016)),
                new ReportingPeriod(new CalendarYear(2016), new CalendarYear(2017)),
                new ReportingPeriod(new CalendarYear(2016), new CalendarYear(2018)),
                new ReportingPeriod(new CalendarYear(2017), new CalendarYear(2017)),
                new ReportingPeriod(new CalendarYear(2017), new CalendarYear(2018)),
                new ReportingPeriod(new CalendarYear(2017), new CalendarYear(2019)),
                new ReportingPeriod(new CalendarYear(2018), new CalendarYear(2018)),
                new ReportingPeriod(new CalendarYear(2018), new CalendarYear(2019)),
                new ReportingPeriod(new CalendarYear(2019), new CalendarYear(2019)));
        }

        [Fact]
        public static void CreatePermutations___Should_return_permutations___When_called_for_reporting_period_of_FiscalYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod(new FiscalYear(2016), new FiscalYear(2016));
            var reportingPeriod2 = new ReportingPeriod(new FiscalYear(2016), new FiscalYear(2019));

            // Act
            var permutations1a = reportingPeriod1.CreatePermutations(1);
            var permutations1b = reportingPeriod1.CreatePermutations(5);

            var permutations2a = reportingPeriod2.CreatePermutations(1);
            var permutations2b = reportingPeriod2.CreatePermutations(3);

            // Assert
            permutations1a.Should().Equal(reportingPeriod1);
            permutations1b.Should().Equal(reportingPeriod1);

            permutations2a.Should().Equal(
                new ReportingPeriod(new FiscalYear(2016), new FiscalYear(2016)),
                new ReportingPeriod(new FiscalYear(2017), new FiscalYear(2017)),
                new ReportingPeriod(new FiscalYear(2018), new FiscalYear(2018)),
                new ReportingPeriod(new FiscalYear(2019), new FiscalYear(2019)));

            permutations2b.Should().Equal(
                new ReportingPeriod(new FiscalYear(2016), new FiscalYear(2016)),
                new ReportingPeriod(new FiscalYear(2016), new FiscalYear(2017)),
                new ReportingPeriod(new FiscalYear(2016), new FiscalYear(2018)),
                new ReportingPeriod(new FiscalYear(2017), new FiscalYear(2017)),
                new ReportingPeriod(new FiscalYear(2017), new FiscalYear(2018)),
                new ReportingPeriod(new FiscalYear(2017), new FiscalYear(2019)),
                new ReportingPeriod(new FiscalYear(2018), new FiscalYear(2018)),
                new ReportingPeriod(new FiscalYear(2018), new FiscalYear(2019)),
                new ReportingPeriod(new FiscalYear(2019), new FiscalYear(2019)));
        }

        [Fact]
        public static void CreatePermutations___Should_return_permutations___When_called_for_reporting_period_of_GenericYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod(new GenericYear(2016), new GenericYear(2016));
            var reportingPeriod2 = new ReportingPeriod(new GenericYear(2016), new GenericYear(2019));

            // Act
            var permutations1a = reportingPeriod1.CreatePermutations(1);
            var permutations1b = reportingPeriod1.CreatePermutations(5);

            var permutations2a = reportingPeriod2.CreatePermutations(1);
            var permutations2b = reportingPeriod2.CreatePermutations(3);

            // Assert
            permutations1a.Should().Equal(reportingPeriod1);
            permutations1b.Should().Equal(reportingPeriod1);

            permutations2a.Should().Equal(
                new ReportingPeriod(new GenericYear(2016), new GenericYear(2016)),
                new ReportingPeriod(new GenericYear(2017), new GenericYear(2017)),
                new ReportingPeriod(new GenericYear(2018), new GenericYear(2018)),
                new ReportingPeriod(new GenericYear(2019), new GenericYear(2019)));

            permutations2b.Should().Equal(
                new ReportingPeriod(new GenericYear(2016), new GenericYear(2016)),
                new ReportingPeriod(new GenericYear(2016), new GenericYear(2017)),
                new ReportingPeriod(new GenericYear(2016), new GenericYear(2018)),
                new ReportingPeriod(new GenericYear(2017), new GenericYear(2017)),
                new ReportingPeriod(new GenericYear(2017), new GenericYear(2018)),
                new ReportingPeriod(new GenericYear(2017), new GenericYear(2019)),
                new ReportingPeriod(new GenericYear(2018), new GenericYear(2018)),
                new ReportingPeriod(new GenericYear(2018), new GenericYear(2019)),
                new ReportingPeriod(new GenericYear(2019), new GenericYear(2019)));
        }

        [Fact]
        public static void MergeIntoExtremalReportingPeriod___Should_throw_ArgumentNullException___When_parameter_reportingPeriods_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.MergeIntoExtremalReportingPeriod(null));

            // Assert
            ex.AsTest().Must().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void MergeIntoExtremalReportingPeriod___Should_throw_ArgumentException___When_parameter_reportingPeriods_is_empty()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.MergeIntoExtremalReportingPeriod(new ReportingPeriod[0]));

            // Assert
            ex.AsTest().Must().BeOfType<ArgumentException>();
            ex.Message.AsTest().Must().ContainString("is empty");
        }

        [Fact]
        public static void MergeIntoExtremalReportingPeriod___Should_throw_ArgumentException___When_parameter_reportingPeriods_contains_a_null_element()
        {
            // Arrange
            var reportingPeriods = new[] { A.Dummy<ReportingPeriod>(), null, A.Dummy<ReportingPeriod>() };

            // Act
            var ex = Record.Exception(() => reportingPeriods.MergeIntoExtremalReportingPeriod());

            // Assert
            ex.AsTest().Must().BeOfType<ArgumentException>();
            ex.Message.AsTest().Must().ContainString("contains a null element");
        }

        [Fact]
        public static void MergeIntoExtremalReportingPeriod___Should_throw_ArgumentException___When_parameter_reportingPeriods_contains_elements_with_different_UnitOfTimeKind()
        {
            // Arrange
            var reportingPeriod1 = A.Dummy<ReportingPeriod>();
            var reportingPeriod2 = A.Dummy<ReportingPeriod>().Whose(_ => _.GetUnitOfTimeKind() != reportingPeriod1.GetUnitOfTimeKind());
            var reportingPeriod3 = A.Dummy<ReportingPeriod>().Whose(_ => _.GetUnitOfTimeKind() == reportingPeriod1.GetUnitOfTimeKind());
            var reportingPeriods1 = new[] { reportingPeriod1, reportingPeriod2 };
            var reportingPeriods2 = new[] { reportingPeriod1, reportingPeriod3, reportingPeriod2 };

            // Act
            var ex1 = Record.Exception(() => reportingPeriods1.MergeIntoExtremalReportingPeriod());
            var ex2 = Record.Exception(() => reportingPeriods2.MergeIntoExtremalReportingPeriod());

            // Assert
            ex1.AsTest().Must().BeOfType<ArgumentException>();
            ex1.Message.AsTest().Must().ContainString("contains elements with different UnitOfTimeKind");

            ex2.AsTest().Must().BeOfType<ArgumentException>();
            ex2.Message.AsTest().Must().ContainString("contains elements with different UnitOfTimeKind");
        }

        [Fact]
        public static void MergeIntoExtremalReportingPeriod___Should_return_same_reporting_period___When_parameter_reportingPeriods_contains_single_reporting_period()
        {
            // Arrange
            var expected = Some.ReadOnlyDummies<ReportingPeriod>().ToList();

            // Act
            var actual = expected.Select(_ => new[] { _ }.MergeIntoExtremalReportingPeriod()).ToList();

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void MergeIntoExtremalReportingPeriod___Should_return_extremal_reporting_period___When_called()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarUnbounded(), new CalendarUnbounded()),
                        A.Dummy<CalendarDay>().ToReportingPeriod(),
                    },
                    Expected = new ReportingPeriod(new CalendarUnbounded(), new CalendarUnbounded()),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        A.Dummy<CalendarDay>().ToReportingPeriod(),
                        new ReportingPeriod(new CalendarUnbounded(), new CalendarUnbounded()),
                    },
                    Expected = new ReportingPeriod(new CalendarUnbounded(), new CalendarUnbounded()),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new CalendarMonth(2020, MonthOfYear.January).ToReportingPeriod(),
                        new ReportingPeriod(new CalendarUnbounded(), new CalendarMonth(2020, MonthOfYear.January)),
                    },
                    Expected = new ReportingPeriod(new CalendarUnbounded(), new CalendarMonth(2020, MonthOfYear.January)),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarUnbounded(), new CalendarMonth(2020, MonthOfYear.January)),
                        new CalendarMonth(2020, MonthOfYear.January).ToReportingPeriod(),
                    },
                    Expected = new ReportingPeriod(new CalendarUnbounded(), new CalendarMonth(2020, MonthOfYear.January)),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new CalendarMonth(2020, MonthOfYear.January).ToReportingPeriod(),
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarUnbounded()),
                    },
                    Expected = new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarUnbounded()),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarUnbounded()),
                        new CalendarMonth(2020, MonthOfYear.January).ToReportingPeriod(),
                    },
                    Expected = new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarUnbounded()),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarMonth(2020, MonthOfYear.March)),
                        new ReportingPeriod(new CalendarUnbounded(), new CalendarMonth(2019, MonthOfYear.December)),
                    },
                    Expected = new ReportingPeriod(new CalendarUnbounded(), new CalendarMonth(2020, MonthOfYear.March)),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarMonth(2020, MonthOfYear.March)),
                        new ReportingPeriod(new CalendarUnbounded(), new CalendarMonth(2020, MonthOfYear.January)),
                    },
                    Expected = new ReportingPeriod(new CalendarUnbounded(), new CalendarMonth(2020, MonthOfYear.March)),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarMonth(2020, MonthOfYear.March)),
                        new ReportingPeriod(new CalendarUnbounded(), new CalendarMonth(2020, MonthOfYear.February)),
                    },
                    Expected = new ReportingPeriod(new CalendarUnbounded(), new CalendarMonth(2020, MonthOfYear.March)),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarMonth(2020, MonthOfYear.March)),
                        new ReportingPeriod(new CalendarUnbounded(), new CalendarMonth(2020, MonthOfYear.March)),
                    },
                    Expected = new ReportingPeriod(new CalendarUnbounded(), new CalendarMonth(2020, MonthOfYear.March)),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarMonth(2020, MonthOfYear.March)),
                        new ReportingPeriod(new CalendarUnbounded(), new CalendarMonth(2020, MonthOfYear.April)),
                    },
                    Expected = new ReportingPeriod(new CalendarUnbounded(), new CalendarMonth(2020, MonthOfYear.April)),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarUnbounded(), new CalendarMonth(2019, MonthOfYear.December)),
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarMonth(2020, MonthOfYear.March)),
                    },
                    Expected = new ReportingPeriod(new CalendarUnbounded(), new CalendarMonth(2020, MonthOfYear.March)),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarUnbounded(), new CalendarMonth(2020, MonthOfYear.January)),
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarMonth(2020, MonthOfYear.March)),
                    },
                    Expected = new ReportingPeriod(new CalendarUnbounded(), new CalendarMonth(2020, MonthOfYear.March)),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarUnbounded(), new CalendarMonth(2020, MonthOfYear.February)),
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarMonth(2020, MonthOfYear.March)),
                    },
                    Expected = new ReportingPeriod(new CalendarUnbounded(), new CalendarMonth(2020, MonthOfYear.March)),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarUnbounded(), new CalendarMonth(2020, MonthOfYear.March)),
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarMonth(2020, MonthOfYear.March)),
                    },
                    Expected = new ReportingPeriod(new CalendarUnbounded(), new CalendarMonth(2020, MonthOfYear.March)),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarUnbounded(), new CalendarMonth(2020, MonthOfYear.April)),
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarMonth(2020, MonthOfYear.March)),
                    },
                    Expected = new ReportingPeriod(new CalendarUnbounded(), new CalendarMonth(2020, MonthOfYear.April)),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarMonth(2020, MonthOfYear.March)),
                        new ReportingPeriod(new CalendarMonth(2019, MonthOfYear.December), new CalendarUnbounded()),
                    },
                    Expected = new ReportingPeriod(new CalendarMonth(2019, MonthOfYear.December), new CalendarUnbounded()),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarMonth(2020, MonthOfYear.March)),
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarUnbounded()),
                    },
                    Expected = new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarUnbounded()),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarMonth(2020, MonthOfYear.March)),
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.February), new CalendarUnbounded()),
                    },
                    Expected = new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarUnbounded()),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarMonth(2020, MonthOfYear.March)),
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.March), new CalendarUnbounded()),
                    },
                    Expected = new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarUnbounded()),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarMonth(2020, MonthOfYear.March)),
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.April), new CalendarUnbounded()),
                    },
                    Expected = new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarUnbounded()),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarMonth(2019, MonthOfYear.December), new CalendarUnbounded()),
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarMonth(2020, MonthOfYear.March)),
                    },
                    Expected = new ReportingPeriod(new CalendarMonth(2019, MonthOfYear.December), new CalendarUnbounded()),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarUnbounded()),
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarMonth(2020, MonthOfYear.March)),
                    },
                    Expected = new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarUnbounded()),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.February), new CalendarUnbounded()),
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarMonth(2020, MonthOfYear.March)),
                    },
                    Expected = new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarUnbounded()),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.March), new CalendarUnbounded()),
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarMonth(2020, MonthOfYear.March)),
                    },
                    Expected = new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarUnbounded()),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.April), new CalendarUnbounded()),
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarMonth(2020, MonthOfYear.March)),
                    },
                    Expected = new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarUnbounded()),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.April), new CalendarMonth(2020, MonthOfYear.May)),
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarMonth(2020, MonthOfYear.February)),
                    },
                    Expected = new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarMonth(2020, MonthOfYear.May)),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarMonth(2020, MonthOfYear.February)),
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.April), new CalendarMonth(2020, MonthOfYear.May)),
                    },
                    Expected = new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarMonth(2020, MonthOfYear.May)),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.April), new CalendarMonth(2020, MonthOfYear.May)),
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarMonth(2020, MonthOfYear.July)),
                    },
                    Expected = new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarMonth(2020, MonthOfYear.July)),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarMonth(2020, MonthOfYear.July)),
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.April), new CalendarMonth(2020, MonthOfYear.May)),
                    },
                    Expected = new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarMonth(2020, MonthOfYear.July)),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarMonth(2020, MonthOfYear.March)),
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.February), new CalendarMonth(2020, MonthOfYear.April)),
                    },
                    Expected = new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarMonth(2020, MonthOfYear.April)),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.February), new CalendarMonth(2020, MonthOfYear.April)),
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarMonth(2020, MonthOfYear.March)),
                    },
                    Expected = new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarMonth(2020, MonthOfYear.April)),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarMonth(2020, MonthOfYear.April)),
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.February), new CalendarMonth(2020, MonthOfYear.May)),
                    },
                    Expected = new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarMonth(2020, MonthOfYear.May)),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.February), new CalendarMonth(2020, MonthOfYear.May)),
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarMonth(2020, MonthOfYear.April)),
                    },
                    Expected = new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarMonth(2020, MonthOfYear.May)),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.March), new CalendarMonth(2020, MonthOfYear.May)),
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarMonth(2020, MonthOfYear.April)),
                    },
                    Expected = new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarMonth(2020, MonthOfYear.May)),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarMonth(2020, MonthOfYear.April)),
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.March), new CalendarMonth(2020, MonthOfYear.May)),
                    },
                    Expected = new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.January), new CalendarMonth(2020, MonthOfYear.May)),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.February), new CalendarMonth(2020, MonthOfYear.April)),
                        new ReportingPeriod(new CalendarDay(2020, MonthOfYear.June, DayOfMonth.Five), new CalendarDay(2020, MonthOfYear.July, DayOfMonth.Twenty)),
                    },
                    Expected = new ReportingPeriod(new CalendarDay(2020, MonthOfYear.February, DayOfMonth.One), new CalendarDay(2020, MonthOfYear.July, DayOfMonth.Twenty)),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarDay(2020, MonthOfYear.June, DayOfMonth.Five), new CalendarDay(2020, MonthOfYear.July, DayOfMonth.Twenty)),
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.February), new CalendarMonth(2020, MonthOfYear.April)),
                    },
                    Expected = new ReportingPeriod(new CalendarDay(2020, MonthOfYear.February, DayOfMonth.One), new CalendarDay(2020, MonthOfYear.July, DayOfMonth.Twenty)),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarDay(2020, MonthOfYear.February, DayOfMonth.Two), new CalendarDay(2020, MonthOfYear.April, DayOfMonth.Ten)),
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.June), new CalendarMonth(2020, MonthOfYear.July)),
                    },
                    Expected = new ReportingPeriod(new CalendarDay(2020, MonthOfYear.February, DayOfMonth.Two), new CalendarDay(2020, MonthOfYear.July, DayOfMonth.ThirtyOne)),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.June), new CalendarMonth(2020, MonthOfYear.July)),
                        new ReportingPeriod(new CalendarDay(2020, MonthOfYear.February, DayOfMonth.Two), new CalendarDay(2020, MonthOfYear.April, DayOfMonth.Ten)),
                    },
                    Expected = new ReportingPeriod(new CalendarDay(2020, MonthOfYear.February, DayOfMonth.Two), new CalendarDay(2020, MonthOfYear.July, DayOfMonth.ThirtyOne)),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.February), new CalendarMonth(2020, MonthOfYear.April)),
                        new ReportingPeriod(new CalendarDay(2020, MonthOfYear.March, DayOfMonth.Five), new CalendarDay(2020, MonthOfYear.April, DayOfMonth.One)),
                    },
                    Expected = new ReportingPeriod(new CalendarDay(2020, MonthOfYear.February, DayOfMonth.One), new CalendarDay(2020, MonthOfYear.April, DayOfMonth.Thirty)),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarDay(2020, MonthOfYear.March, DayOfMonth.Five), new CalendarDay(2020, MonthOfYear.April, DayOfMonth.One)),
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.February), new CalendarMonth(2020, MonthOfYear.April)),
                    },
                    Expected = new ReportingPeriod(new CalendarDay(2020, MonthOfYear.February, DayOfMonth.One), new CalendarDay(2020, MonthOfYear.April, DayOfMonth.Thirty)),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.February), new CalendarMonth(2020, MonthOfYear.April)),
                        new ReportingPeriod(new CalendarDay(2020, MonthOfYear.January, DayOfMonth.Ten), new CalendarDay(2020, MonthOfYear.May, DayOfMonth.Four)),
                    },
                    Expected = new ReportingPeriod(new CalendarDay(2020, MonthOfYear.January, DayOfMonth.Ten), new CalendarDay(2020, MonthOfYear.May, DayOfMonth.Four)),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarDay(2020, MonthOfYear.January, DayOfMonth.Ten), new CalendarDay(2020, MonthOfYear.May, DayOfMonth.Four)),
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.February), new CalendarMonth(2020, MonthOfYear.April)),
                    },
                    Expected = new ReportingPeriod(new CalendarDay(2020, MonthOfYear.January, DayOfMonth.Ten), new CalendarDay(2020, MonthOfYear.May, DayOfMonth.Four)),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.February), new CalendarMonth(2020, MonthOfYear.April)),
                        new ReportingPeriod(new CalendarDay(2020, MonthOfYear.March, DayOfMonth.Five), new CalendarDay(2020, MonthOfYear.May, DayOfMonth.Four)),
                    },
                    Expected = new ReportingPeriod(new CalendarDay(2020, MonthOfYear.February, DayOfMonth.One), new CalendarDay(2020, MonthOfYear.May, DayOfMonth.Four)),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarDay(2020, MonthOfYear.March, DayOfMonth.Five), new CalendarDay(2020, MonthOfYear.May, DayOfMonth.Four)),
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.February), new CalendarMonth(2020, MonthOfYear.April)),
                    },
                    Expected = new ReportingPeriod(new CalendarDay(2020, MonthOfYear.February, DayOfMonth.One), new CalendarDay(2020, MonthOfYear.May, DayOfMonth.Four)),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarDay(2020, MonthOfYear.January, DayOfMonth.Three), new CalendarDay(2020, MonthOfYear.March, DayOfMonth.Four)),
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.February), new CalendarMonth(2020, MonthOfYear.April)),
                    },
                    Expected = new ReportingPeriod(new CalendarDay(2020, MonthOfYear.January, DayOfMonth.Three), new CalendarDay(2020, MonthOfYear.April, DayOfMonth.Thirty)),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarMonth(2020, MonthOfYear.February), new CalendarMonth(2020, MonthOfYear.April)),
                        new ReportingPeriod(new CalendarDay(2020, MonthOfYear.January, DayOfMonth.Three), new CalendarDay(2020, MonthOfYear.March, DayOfMonth.Four)),
                    },
                    Expected = new ReportingPeriod(new CalendarDay(2020, MonthOfYear.January, DayOfMonth.Three), new CalendarDay(2020, MonthOfYear.April, DayOfMonth.Thirty)),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarYear(2020), new CalendarYear(2021)),
                        new ReportingPeriod(new CalendarMonth(2019, MonthOfYear.December), new CalendarMonth(2022, MonthOfYear.January)),
                        new ReportingPeriod(new CalendarDay(2019, MonthOfYear.November, DayOfMonth.Fifteen), new CalendarDay(2022, MonthOfYear.February, DayOfMonth.Four)),
                    },
                    Expected = new ReportingPeriod(new CalendarDay(2019, MonthOfYear.November, DayOfMonth.Fifteen), new CalendarDay(2022, MonthOfYear.February, DayOfMonth.Four)),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarDay(2019, MonthOfYear.November, DayOfMonth.Fifteen), new CalendarDay(2022, MonthOfYear.February, DayOfMonth.Four)),
                        new ReportingPeriod(new CalendarMonth(2019, MonthOfYear.December), new CalendarMonth(2022, MonthOfYear.January)),
                        new ReportingPeriod(new CalendarYear(2020), new CalendarYear(2021)),
                    },
                    Expected = new ReportingPeriod(new CalendarDay(2019, MonthOfYear.November, DayOfMonth.Fifteen), new CalendarDay(2022, MonthOfYear.February, DayOfMonth.Four)),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarYear(2020), new CalendarYear(2025)),
                        new ReportingPeriod(new CalendarMonth(2021, MonthOfYear.January), new CalendarMonth(2024, MonthOfYear.November)),
                        new ReportingPeriod(new CalendarDay(2022, MonthOfYear.August, DayOfMonth.Fifteen), new CalendarDay(2023, MonthOfYear.February, DayOfMonth.Four)),
                    },
                    Expected = new ReportingPeriod(new CalendarDay(2020, MonthOfYear.January, DayOfMonth.One), new CalendarDay(2025, MonthOfYear.December, DayOfMonth.ThirtyOne)),
                },
                new
                {
                    ReportingPeriods = new[]
                    {
                        new ReportingPeriod(new CalendarDay(2022, MonthOfYear.August, DayOfMonth.Fifteen), new CalendarDay(2023, MonthOfYear.February, DayOfMonth.Four)),
                        new ReportingPeriod(new CalendarMonth(2021, MonthOfYear.January), new CalendarMonth(2024, MonthOfYear.November)),
                        new ReportingPeriod(new CalendarYear(2020), new CalendarYear(2025)),
                    },
                    Expected = new ReportingPeriod(new CalendarDay(2020, MonthOfYear.January, DayOfMonth.One), new CalendarDay(2025, MonthOfYear.December, DayOfMonth.ThirtyOne)),
                },
            };

            // Act
            var actual = tests.Select(_ => _.ReportingPeriods.MergeIntoExtremalReportingPeriod()).ToList();

            // Assert
            actual.AsTest().Must().BeEqualTo(tests.Select(_ => _.Expected).ToList());
        }

        [Fact]
        public static void Split___Should_throw_ArgumentNullException___When_parameter_reportingPeriod_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.Split(null, A.Dummy<UnitOfTimeGranularity>()));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void Split___Should_throw_ArgumentException___When_parameter_reportingPeriod_has_an_unbounded_component()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                A.Dummy<ReportingPeriod>().Whose(_ => (_.Start.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded) && (_.End.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded)),
                A.Dummy<ReportingPeriod>().Whose(_ => (_.Start.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded) && (_.End.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded)),
                A.Dummy<ReportingPeriod>().Whose(_ => (_.Start.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded) && (_.End.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded)),
            };

            // Act
            var exceptions = reportingPeriods.Select(_ => Record.Exception(() => _.Split(A.Dummy<UnitOfTimeGranularity>()))).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<ArgumentException>());
        }

        [Fact]
        public static void Split___Should_throw_ArgumentOutOfRangeException___When_parameter_granularity_is_Invalid()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod>().Whose(_ => !_.HasComponentWithUnboundedGranularity());

            // Act
            var ex = Record.Exception(() => reportingPeriod.Split(UnitOfTimeGranularity.Invalid));

            // Assert
            ex.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void Split___Should_throw_ArgumentOutOfRangeException___When_parameter_granularity_is_Unbounded()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod>().Whose(_ => !_.HasComponentWithUnboundedGranularity());

            // Act
            var ex = Record.Exception(() => reportingPeriod.Split(UnitOfTimeGranularity.Unbounded));

            // Assert
            ex.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_CalendarYear___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_CalendarYear_and_granularity_is_Year()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarYear(2017), new CalendarYear(2017)),
                    ExpectedUnitsOfTime = new[] { new CalendarYear(2017) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarYear(2017), new CalendarYear(2018)),
                    ExpectedUnitsOfTime = new[] { new CalendarYear(2017), new CalendarYear(2018) },
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Year, OverflowStrategy.ThrowOnOverflow), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_CalendarQuarter___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_CalendarYear_and_granularity_is_Quarter()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarYear(2017), new CalendarYear(2017)),
                    ExpectedUnitsOfTime = new[] { new CalendarQuarter(2017, QuarterNumber.Q1), new CalendarQuarter(2017, QuarterNumber.Q2), new CalendarQuarter(2017, QuarterNumber.Q3), new CalendarQuarter(2017, QuarterNumber.Q4) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarYear(2017), new CalendarYear(2018)),
                    ExpectedUnitsOfTime = new[] { new CalendarQuarter(2017, QuarterNumber.Q1), new CalendarQuarter(2017, QuarterNumber.Q2), new CalendarQuarter(2017, QuarterNumber.Q3), new CalendarQuarter(2017, QuarterNumber.Q4), new CalendarQuarter(2018, QuarterNumber.Q1), new CalendarQuarter(2018, QuarterNumber.Q2), new CalendarQuarter(2018, QuarterNumber.Q3), new CalendarQuarter(2018, QuarterNumber.Q4) },
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Quarter, OverflowStrategy.ThrowOnOverflow), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_CalendarMonth___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_CalendarYear_and_granularity_is_Month()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarYear(2017), new CalendarYear(2017)),
                    ExpectedUnitsOfTime = new[] { new CalendarMonth(2017, MonthOfYear.January), new CalendarMonth(2017, MonthOfYear.February), new CalendarMonth(2017, MonthOfYear.March), new CalendarMonth(2017, MonthOfYear.April), new CalendarMonth(2017, MonthOfYear.May), new CalendarMonth(2017, MonthOfYear.June), new CalendarMonth(2017, MonthOfYear.July), new CalendarMonth(2017, MonthOfYear.August), new CalendarMonth(2017, MonthOfYear.September), new CalendarMonth(2017, MonthOfYear.October), new CalendarMonth(2017, MonthOfYear.November), new CalendarMonth(2017, MonthOfYear.December) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarYear(2017), new CalendarYear(2018)),
                    ExpectedUnitsOfTime = new[] { new CalendarMonth(2017, MonthOfYear.January), new CalendarMonth(2017, MonthOfYear.February), new CalendarMonth(2017, MonthOfYear.March), new CalendarMonth(2017, MonthOfYear.April), new CalendarMonth(2017, MonthOfYear.May), new CalendarMonth(2017, MonthOfYear.June), new CalendarMonth(2017, MonthOfYear.July), new CalendarMonth(2017, MonthOfYear.August), new CalendarMonth(2017, MonthOfYear.September), new CalendarMonth(2017, MonthOfYear.October), new CalendarMonth(2017, MonthOfYear.November), new CalendarMonth(2017, MonthOfYear.December), new CalendarMonth(2018, MonthOfYear.January), new CalendarMonth(2018, MonthOfYear.February), new CalendarMonth(2018, MonthOfYear.March), new CalendarMonth(2018, MonthOfYear.April), new CalendarMonth(2018, MonthOfYear.May), new CalendarMonth(2018, MonthOfYear.June), new CalendarMonth(2018, MonthOfYear.July), new CalendarMonth(2018, MonthOfYear.August), new CalendarMonth(2018, MonthOfYear.September), new CalendarMonth(2018, MonthOfYear.October), new CalendarMonth(2018, MonthOfYear.November), new CalendarMonth(2018, MonthOfYear.December) },
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Month, OverflowStrategy.ThrowOnOverflow), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_CalendarDay___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_CalendarYear_and_granularity_is_Day()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarYear(2017), new CalendarYear(2017)),
                    ExpectedUnitsOfTimeBeginning = new[] { new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One), new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Two), new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Three) },
                    ExpectedUnitsOfTimeEnd = new[] { new CalendarDay(2017, MonthOfYear.December, DayOfMonth.TwentyNine), new CalendarDay(2017, MonthOfYear.December, DayOfMonth.Thirty), new CalendarDay(2017, MonthOfYear.December, DayOfMonth.ThirtyOne) },
                    ExpectedCount = 365,
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarYear(2017), new CalendarYear(2018)),
                    ExpectedUnitsOfTimeBeginning = new[] { new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One), new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Two), new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Three) },
                    ExpectedUnitsOfTimeEnd = new[] { new CalendarDay(2018, MonthOfYear.December, DayOfMonth.TwentyNine), new CalendarDay(2018, MonthOfYear.December, DayOfMonth.Thirty), new CalendarDay(2018, MonthOfYear.December, DayOfMonth.ThirtyOne) },
                    ExpectedCount = 365 * 2,
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Day, OverflowStrategy.ThrowOnOverflow), ExpectedBeginning = _.ExpectedUnitsOfTimeBeginning, ExpectedEnd = _.ExpectedUnitsOfTimeEnd, _.ExpectedCount }).ToList();

            // Assert
            results.ForEach(_ =>
            {
                _.Actual.Should().StartWith(_.ExpectedBeginning);
                _.Actual.Should().EndWith(_.ExpectedEnd);
                _.Actual.Should().HaveCount(_.ExpectedCount);
            });
        }

        [Fact]
        public static void Split___Should_throw_InvalidOperationException___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_CalendarQuarter_and_granularity_is_Year_and_there_is_overflow()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new ReportingPeriod(new CalendarQuarter(2017, QuarterNumber.Q1), new CalendarQuarter(2019, QuarterNumber.Q3)),
                new ReportingPeriod(new CalendarQuarter(2017, QuarterNumber.Q2), new CalendarQuarter(2019, QuarterNumber.Q4)),
                new ReportingPeriod(new CalendarQuarter(2017, QuarterNumber.Q1), new CalendarQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod(new CalendarQuarter(2017, QuarterNumber.Q4), new CalendarQuarter(2019, QuarterNumber.Q3)),
            };

            // Act
            var exceptions = reportingPeriods.Select(_ => Record.Exception(() => _.Split(UnitOfTimeGranularity.Year, OverflowStrategy.ThrowOnOverflow))).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_CalendarYear___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_CalendarQuarter_and_granularity_is_Year()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarQuarter(2017, QuarterNumber.Q1), new CalendarQuarter(2017, QuarterNumber.Q4)),
                    ExpectedUnitsOfTime = new[] { new CalendarYear(2017) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarQuarter(2017, QuarterNumber.Q1), new CalendarQuarter(2018, QuarterNumber.Q4)),
                    ExpectedUnitsOfTime = new[] { new CalendarYear(2017), new CalendarYear(2018) },
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Year, OverflowStrategy.ThrowOnOverflow), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_CalendarQuarter___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_CalendarQuarter_and_granularity_is_Quarter()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarQuarter(2017, QuarterNumber.Q1), new CalendarQuarter(2017, QuarterNumber.Q1)),
                    ExpectedUnitsOfTime = new[] { new CalendarQuarter(2017, QuarterNumber.Q1) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarQuarter(2017, QuarterNumber.Q2), new CalendarQuarter(2018, QuarterNumber.Q2)),
                    ExpectedUnitsOfTime = new[] { new CalendarQuarter(2017, QuarterNumber.Q2), new CalendarQuarter(2017, QuarterNumber.Q3), new CalendarQuarter(2017, QuarterNumber.Q4), new CalendarQuarter(2018, QuarterNumber.Q1), new CalendarQuarter(2018, QuarterNumber.Q2) },
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Quarter, OverflowStrategy.ThrowOnOverflow), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_CalendarMonth___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_CalendarQuarter_and_granularity_is_Month()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarQuarter(2017, QuarterNumber.Q1), new CalendarQuarter(2017, QuarterNumber.Q1)),
                    ExpectedUnitsOfTime = new[] { new CalendarMonth(2017, MonthOfYear.January), new CalendarMonth(2017, MonthOfYear.February), new CalendarMonth(2017, MonthOfYear.March) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarQuarter(2017, QuarterNumber.Q2), new CalendarQuarter(2017, QuarterNumber.Q2)),
                    ExpectedUnitsOfTime = new[] { new CalendarMonth(2017, MonthOfYear.April), new CalendarMonth(2017, MonthOfYear.May), new CalendarMonth(2017, MonthOfYear.June) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarQuarter(2017, QuarterNumber.Q3), new CalendarQuarter(2017, QuarterNumber.Q3)),
                    ExpectedUnitsOfTime = new[] { new CalendarMonth(2017, MonthOfYear.July), new CalendarMonth(2017, MonthOfYear.August), new CalendarMonth(2017, MonthOfYear.September) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarQuarter(2017, QuarterNumber.Q4), new CalendarQuarter(2017, QuarterNumber.Q4)),
                    ExpectedUnitsOfTime = new[] { new CalendarMonth(2017, MonthOfYear.October), new CalendarMonth(2017, MonthOfYear.November), new CalendarMonth(2017, MonthOfYear.December) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarQuarter(2017, QuarterNumber.Q4), new CalendarQuarter(2018, QuarterNumber.Q1)),
                    ExpectedUnitsOfTime = new[] { new CalendarMonth(2017, MonthOfYear.October), new CalendarMonth(2017, MonthOfYear.November), new CalendarMonth(2017, MonthOfYear.December), new CalendarMonth(2018, MonthOfYear.January), new CalendarMonth(2018, MonthOfYear.February), new CalendarMonth(2018, MonthOfYear.March) },
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Month, OverflowStrategy.ThrowOnOverflow), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_CalendarDay___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_CalendarQuarter_and_granularity_is_Day()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarQuarter(2017, QuarterNumber.Q4), new CalendarQuarter(2017, QuarterNumber.Q4)),
                    ExpectedUnitsOfTimeBeginning = new[] { new CalendarDay(2017, MonthOfYear.October, DayOfMonth.One), new CalendarDay(2017, MonthOfYear.October, DayOfMonth.Two), new CalendarDay(2017, MonthOfYear.October, DayOfMonth.Three) },
                    ExpectedUnitsOfTimeEnd = new[] { new CalendarDay(2017, MonthOfYear.December, DayOfMonth.TwentyNine), new CalendarDay(2017, MonthOfYear.December, DayOfMonth.Thirty), new CalendarDay(2017, MonthOfYear.December, DayOfMonth.ThirtyOne) },
                    ExpectedCount = 31 + 30 + 31,
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarQuarter(2017, QuarterNumber.Q2), new CalendarQuarter(2018, QuarterNumber.Q3)),
                    ExpectedUnitsOfTimeBeginning = new[] { new CalendarDay(2017, MonthOfYear.April, DayOfMonth.One), new CalendarDay(2017, MonthOfYear.April, DayOfMonth.Two), new CalendarDay(2017, MonthOfYear.April, DayOfMonth.Three) },
                    ExpectedUnitsOfTimeEnd = new[] { new CalendarDay(2018, MonthOfYear.September, DayOfMonth.TwentyEight), new CalendarDay(2018, MonthOfYear.September, DayOfMonth.TwentyNine), new CalendarDay(2018, MonthOfYear.September, DayOfMonth.Thirty) },
                    ExpectedCount = 548,
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Day, OverflowStrategy.ThrowOnOverflow), ExpectedBeginning = _.ExpectedUnitsOfTimeBeginning, ExpectedEnd = _.ExpectedUnitsOfTimeEnd, _.ExpectedCount }).ToList();

            // Assert
            results.ForEach(_ =>
            {
                _.Actual.Should().StartWith(_.ExpectedBeginning);
                _.Actual.Should().EndWith(_.ExpectedEnd);
                _.Actual.Should().HaveCount(_.ExpectedCount);
            });
        }

        [Fact]
        public static void Split___Should_throw_InvalidOperationException___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_CalendarMonth_and_granularity_is_Year_and_there_is_overflow()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.March), new CalendarMonth(2018, MonthOfYear.February)),
                new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.April), new CalendarMonth(2019, MonthOfYear.July)),
                new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.January), new CalendarMonth(2019, MonthOfYear.November)),
                new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.February), new CalendarMonth(2019, MonthOfYear.December)),
            };

            // Act
            var exceptions = reportingPeriods.Select(_ => Record.Exception(() => _.Split(UnitOfTimeGranularity.Year, OverflowStrategy.ThrowOnOverflow))).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_CalendarYear___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_CalendarMonth_and_granularity_is_Year()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.January), new CalendarMonth(2017, MonthOfYear.December)),
                    ExpectedUnitsOfTime = new[] { new CalendarYear(2017) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.January), new CalendarMonth(2018, MonthOfYear.December)),
                    ExpectedUnitsOfTime = new[] { new CalendarYear(2017), new CalendarYear(2018) },
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Year, OverflowStrategy.ThrowOnOverflow), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
        }

        [Fact]
        public static void Split___Should_throw_InvalidOperationException___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_CalendarMonth_and_granularity_is_Quarter_and_there_is_overflow()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.February), new CalendarMonth(2018, MonthOfYear.March)),
                new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.February), new CalendarMonth(2018, MonthOfYear.June)),
                new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.February), new CalendarMonth(2018, MonthOfYear.September)),
                new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.February), new CalendarMonth(2018, MonthOfYear.December)),
                new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.January), new CalendarMonth(2018, MonthOfYear.February)),
                new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.January), new CalendarMonth(2018, MonthOfYear.May)),
                new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.January), new CalendarMonth(2018, MonthOfYear.August)),
                new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.January), new CalendarMonth(2018, MonthOfYear.November)),
                new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.April), new CalendarMonth(2018, MonthOfYear.November)),
                new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.May), new CalendarMonth(2018, MonthOfYear.December)),
                new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.June), new CalendarMonth(2018, MonthOfYear.December)),
                new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.July), new CalendarMonth(2018, MonthOfYear.November)),
                new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.August), new CalendarMonth(2018, MonthOfYear.December)),
                new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.September), new CalendarMonth(2018, MonthOfYear.December)),
                new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.October), new CalendarMonth(2018, MonthOfYear.November)),
                new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.November), new CalendarMonth(2018, MonthOfYear.November)),
                new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.November), new CalendarMonth(2018, MonthOfYear.February)),
                new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.April), new CalendarMonth(2018, MonthOfYear.April)),
            };

            // Act
            var exceptions = reportingPeriods.Select(_ => Record.Exception(() => _.Split(UnitOfTimeGranularity.Quarter, OverflowStrategy.ThrowOnOverflow))).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_CalendarQuarter___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_CalendarMonth_and_granularity_is_Quarter()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.January), new CalendarMonth(2017, MonthOfYear.March)),
                    ExpectedUnitsOfTime = new[] { new CalendarQuarter(2017, QuarterNumber.Q1) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.April), new CalendarMonth(2017, MonthOfYear.June)),
                    ExpectedUnitsOfTime = new[] { new CalendarQuarter(2017, QuarterNumber.Q2) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.July), new CalendarMonth(2017, MonthOfYear.September)),
                    ExpectedUnitsOfTime = new[] { new CalendarQuarter(2017, QuarterNumber.Q3) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.October), new CalendarMonth(2017, MonthOfYear.December)),
                    ExpectedUnitsOfTime = new[] { new CalendarQuarter(2017, QuarterNumber.Q4) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.January), new CalendarMonth(2018, MonthOfYear.March)),
                    ExpectedUnitsOfTime = new[] { new CalendarQuarter(2017, QuarterNumber.Q1), new CalendarQuarter(2017, QuarterNumber.Q2), new CalendarQuarter(2017, QuarterNumber.Q3), new CalendarQuarter(2017, QuarterNumber.Q4), new CalendarQuarter(2018, QuarterNumber.Q1) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.April), new CalendarMonth(2018, MonthOfYear.June)),
                    ExpectedUnitsOfTime = new[] { new CalendarQuarter(2017, QuarterNumber.Q2), new CalendarQuarter(2017, QuarterNumber.Q3), new CalendarQuarter(2017, QuarterNumber.Q4), new CalendarQuarter(2018, QuarterNumber.Q1), new CalendarQuarter(2018, QuarterNumber.Q2) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.July), new CalendarMonth(2018, MonthOfYear.September)),
                    ExpectedUnitsOfTime = new[] { new CalendarQuarter(2017, QuarterNumber.Q3), new CalendarQuarter(2017, QuarterNumber.Q4), new CalendarQuarter(2018, QuarterNumber.Q1), new CalendarQuarter(2018, QuarterNumber.Q2), new CalendarQuarter(2018, QuarterNumber.Q3) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.October), new CalendarMonth(2018, MonthOfYear.December)),
                    ExpectedUnitsOfTime = new[] { new CalendarQuarter(2017, QuarterNumber.Q4), new CalendarQuarter(2018, QuarterNumber.Q1), new CalendarQuarter(2018, QuarterNumber.Q2), new CalendarQuarter(2018, QuarterNumber.Q3), new CalendarQuarter(2018, QuarterNumber.Q4) },
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Quarter, OverflowStrategy.ThrowOnOverflow), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_CalendarMonth___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_CalendarMonth_and_granularity_is_Month()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.November), new CalendarMonth(2018, MonthOfYear.March)),
                    ExpectedUnitsOfTime = new[] { new CalendarMonth(2017, MonthOfYear.November), new CalendarMonth(2017, MonthOfYear.December), new CalendarMonth(2018, MonthOfYear.January), new CalendarMonth(2018, MonthOfYear.February), new CalendarMonth(2018, MonthOfYear.March) },
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Month, OverflowStrategy.ThrowOnOverflow), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_CalendarDay___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_CalendarMonth_and_granularity_is_Day()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.November), new CalendarMonth(2017, MonthOfYear.November)),
                    ExpectedUnitsOfTimeBeginning = new[] { new CalendarDay(2017, MonthOfYear.November, DayOfMonth.One), new CalendarDay(2017, MonthOfYear.November, DayOfMonth.Two), new CalendarDay(2017, MonthOfYear.November, DayOfMonth.Three) },
                    ExpectedUnitsOfTimeEnd = new[] { new CalendarDay(2017, MonthOfYear.November, DayOfMonth.TwentyEight), new CalendarDay(2017, MonthOfYear.November, DayOfMonth.TwentyNine), new CalendarDay(2017, MonthOfYear.November, DayOfMonth.Thirty) },
                    ExpectedCount = 30,
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.November), new CalendarMonth(2018, MonthOfYear.November)),
                    ExpectedUnitsOfTimeBeginning = new[] { new CalendarDay(2017, MonthOfYear.November, DayOfMonth.One), new CalendarDay(2017, MonthOfYear.November, DayOfMonth.Two), new CalendarDay(2017, MonthOfYear.November, DayOfMonth.Three) },
                    ExpectedUnitsOfTimeEnd = new[] { new CalendarDay(2018, MonthOfYear.November, DayOfMonth.TwentyEight), new CalendarDay(2018, MonthOfYear.November, DayOfMonth.TwentyNine), new CalendarDay(2018, MonthOfYear.November, DayOfMonth.Thirty) },
                    ExpectedCount = 365 + 30,
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Day, OverflowStrategy.ThrowOnOverflow), ExpectedBeginning = _.ExpectedUnitsOfTimeBeginning, ExpectedEnd = _.ExpectedUnitsOfTimeEnd, _.ExpectedCount }).ToList();

            // Assert
            results.ForEach(_ =>
            {
                _.Actual.Should().StartWith(_.ExpectedBeginning);
                _.Actual.Should().EndWith(_.ExpectedEnd);
                _.Actual.Should().HaveCount(_.ExpectedCount);
            });
        }

        [Fact]
        public static void Split___Should_throw_InvalidOperationException___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_CalendarDay_and_granularity_is_Year_and_there_is_overflow()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new ReportingPeriod(new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Two), new CalendarDay(2018, MonthOfYear.December, DayOfMonth.ThirtyOne)),
                new ReportingPeriod(new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One), new CalendarDay(2018, MonthOfYear.December, DayOfMonth.Thirty)),
            };

            // Act
            var exceptions = reportingPeriods.Select(_ => Record.Exception(() => _.Split(UnitOfTimeGranularity.Year, OverflowStrategy.ThrowOnOverflow))).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_CalendarYear___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_CalendarDay_and_granularity_is_Year()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One), new CalendarDay(2018, MonthOfYear.December, DayOfMonth.ThirtyOne)),
                    ExpectedUnitsOfTime = new[] { new CalendarYear(2017), new CalendarYear(2018) },
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Year, OverflowStrategy.ThrowOnOverflow), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
        }

        [Fact]
        public static void Split___Should_throw_InvalidOperationException___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_CalendarDay_and_granularity_is_Quarter_and_there_is_overflow()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new ReportingPeriod(new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One), new CalendarDay(2018, MonthOfYear.March, DayOfMonth.Thirty)),
                new ReportingPeriod(new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Two), new CalendarDay(2018, MonthOfYear.March, DayOfMonth.ThirtyOne)),
                new ReportingPeriod(new CalendarDay(2017, MonthOfYear.April, DayOfMonth.One), new CalendarDay(2018, MonthOfYear.June, DayOfMonth.TwentyNine)),
                new ReportingPeriod(new CalendarDay(2017, MonthOfYear.April, DayOfMonth.Two), new CalendarDay(2018, MonthOfYear.June, DayOfMonth.Thirty)),
                new ReportingPeriod(new CalendarDay(2017, MonthOfYear.July, DayOfMonth.One), new CalendarDay(2018, MonthOfYear.September, DayOfMonth.TwentyNine)),
                new ReportingPeriod(new CalendarDay(2017, MonthOfYear.July, DayOfMonth.Two), new CalendarDay(2018, MonthOfYear.September, DayOfMonth.Thirty)),
                new ReportingPeriod(new CalendarDay(2017, MonthOfYear.October, DayOfMonth.One), new CalendarDay(2018, MonthOfYear.December, DayOfMonth.Thirty)),
                new ReportingPeriod(new CalendarDay(2017, MonthOfYear.October, DayOfMonth.Two), new CalendarDay(2018, MonthOfYear.December, DayOfMonth.ThirtyOne)),
                new ReportingPeriod(new CalendarDay(2017, MonthOfYear.May, DayOfMonth.One), new CalendarDay(2018, MonthOfYear.April, DayOfMonth.Thirty)),
            };

            // Act
            var exceptions = reportingPeriods.Select(_ => Record.Exception(() => _.Split(UnitOfTimeGranularity.Quarter, OverflowStrategy.ThrowOnOverflow))).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_CalendarQuarter___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_CalendarDay_and_granularity_is_Quarter()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One), new CalendarDay(2017, MonthOfYear.March, DayOfMonth.ThirtyOne)),
                    ExpectedUnitsOfTime = new[] { new CalendarQuarter(2017, QuarterNumber.Q1) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarDay(2017, MonthOfYear.April, DayOfMonth.One), new CalendarDay(2017, MonthOfYear.June, DayOfMonth.Thirty)),
                    ExpectedUnitsOfTime = new[] { new CalendarQuarter(2017, QuarterNumber.Q2) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarDay(2017, MonthOfYear.July, DayOfMonth.One), new CalendarDay(2017, MonthOfYear.September, DayOfMonth.Thirty)),
                    ExpectedUnitsOfTime = new[] { new CalendarQuarter(2017, QuarterNumber.Q3) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarDay(2017, MonthOfYear.October, DayOfMonth.One), new CalendarDay(2017, MonthOfYear.December, DayOfMonth.ThirtyOne)),
                    ExpectedUnitsOfTime = new[] { new CalendarQuarter(2017, QuarterNumber.Q4) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One), new CalendarDay(2018, MonthOfYear.March, DayOfMonth.ThirtyOne)),
                    ExpectedUnitsOfTime = new[] { new CalendarQuarter(2017, QuarterNumber.Q1), new CalendarQuarter(2017, QuarterNumber.Q2), new CalendarQuarter(2017, QuarterNumber.Q3), new CalendarQuarter(2017, QuarterNumber.Q4), new CalendarQuarter(2018, QuarterNumber.Q1) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarDay(2017, MonthOfYear.April, DayOfMonth.One), new CalendarDay(2018, MonthOfYear.June, DayOfMonth.Thirty)),
                    ExpectedUnitsOfTime = new[] { new CalendarQuarter(2017, QuarterNumber.Q2), new CalendarQuarter(2017, QuarterNumber.Q3), new CalendarQuarter(2017, QuarterNumber.Q4), new CalendarQuarter(2018, QuarterNumber.Q1), new CalendarQuarter(2018, QuarterNumber.Q2) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarDay(2017, MonthOfYear.July, DayOfMonth.One), new CalendarDay(2018, MonthOfYear.September, DayOfMonth.Thirty)),
                    ExpectedUnitsOfTime = new[] { new CalendarQuarter(2017, QuarterNumber.Q3), new CalendarQuarter(2017, QuarterNumber.Q4), new CalendarQuarter(2018, QuarterNumber.Q1), new CalendarQuarter(2018, QuarterNumber.Q2), new CalendarQuarter(2018, QuarterNumber.Q3) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarDay(2017, MonthOfYear.October, DayOfMonth.One), new CalendarDay(2018, MonthOfYear.December, DayOfMonth.ThirtyOne)),
                    ExpectedUnitsOfTime = new[] { new CalendarQuarter(2017, QuarterNumber.Q4), new CalendarQuarter(2018, QuarterNumber.Q1), new CalendarQuarter(2018, QuarterNumber.Q2), new CalendarQuarter(2018, QuarterNumber.Q3), new CalendarQuarter(2018, QuarterNumber.Q4) },
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Quarter, OverflowStrategy.ThrowOnOverflow), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
        }

        [Fact]
        public static void Split___Should_throw_InvalidOperationException___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_CalendarDay_and_granularity_is_Month_and_there_is_overflow()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new ReportingPeriod(new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One), new CalendarDay(2018, MonthOfYear.January, DayOfMonth.Thirty)),
                new ReportingPeriod(new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Two), new CalendarDay(2018, MonthOfYear.January, DayOfMonth.ThirtyOne)),
                new ReportingPeriod(new CalendarDay(2017, MonthOfYear.February, DayOfMonth.Three), new CalendarDay(2018, MonthOfYear.August, DayOfMonth.One)),
                new ReportingPeriod(new CalendarDay(2017, MonthOfYear.February, DayOfMonth.One), new CalendarDay(2018, MonthOfYear.August, DayOfMonth.One)),
                new ReportingPeriod(new CalendarDay(2017, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2018, MonthOfYear.August, DayOfMonth.ThirtyOne)),
            };

            // Act
            var exceptions = reportingPeriods.Select(_ => Record.Exception(() => _.Split(UnitOfTimeGranularity.Month, OverflowStrategy.ThrowOnOverflow))).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_CalendarMonth___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_CalendarDay_and_granularity_is_Month()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One), new CalendarDay(2017, MonthOfYear.January, DayOfMonth.ThirtyOne)),
                    ExpectedUnitsOfTime = new[] { new CalendarMonth(2017, MonthOfYear.January) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarDay(2017, MonthOfYear.December, DayOfMonth.One), new CalendarDay(2018, MonthOfYear.January, DayOfMonth.ThirtyOne)),
                    ExpectedUnitsOfTime = new[] { new CalendarMonth(2017, MonthOfYear.December), new CalendarMonth(2018, MonthOfYear.January) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarDay(2017, MonthOfYear.November, DayOfMonth.One), new CalendarDay(2018, MonthOfYear.February, DayOfMonth.TwentyEight)),
                    ExpectedUnitsOfTime = new[] { new CalendarMonth(2017, MonthOfYear.November), new CalendarMonth(2017, MonthOfYear.December), new CalendarMonth(2018, MonthOfYear.January), new CalendarMonth(2018, MonthOfYear.February) },
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Month, OverflowStrategy.ThrowOnOverflow), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_CalendarDay___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_CalendarDay_and_granularity_is_Day()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Two), new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Two)),
                    ExpectedUnitsOfTimeBeginning = new[] { new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Two),  },
                    ExpectedUnitsOfTimeEnd = new[] { new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Two) },
                    ExpectedCount = 1,
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Two), new CalendarDay(2017, MonthOfYear.March, DayOfMonth.Ten)),
                    ExpectedUnitsOfTimeBeginning = new[] { new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Two), new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Three), new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Four), },
                    ExpectedUnitsOfTimeEnd = new[] { new CalendarDay(2017, MonthOfYear.March, DayOfMonth.Eight), new CalendarDay(2017, MonthOfYear.March, DayOfMonth.Nine), new CalendarDay(2017, MonthOfYear.March, DayOfMonth.Ten) },
                    ExpectedCount = 30 + 28 + 10,
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Day, OverflowStrategy.ThrowOnOverflow), ExpectedBeginning = _.ExpectedUnitsOfTimeBeginning, ExpectedEnd = _.ExpectedUnitsOfTimeEnd, _.ExpectedCount }).ToList();

            // Assert
            results.ForEach(_ =>
            {
                _.Actual.Should().StartWith(_.ExpectedBeginning);
                _.Actual.Should().EndWith(_.ExpectedEnd);
                _.Actual.Should().HaveCount(_.ExpectedCount);
            });
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_FiscalYear___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_FiscalYear_and_granularity_is_Year()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalYear(2017), new FiscalYear(2017)),
                    ExpectedUnitsOfTime = new[] { new FiscalYear(2017) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalYear(2017), new FiscalYear(2018)),
                    ExpectedUnitsOfTime = new[] { new FiscalYear(2017), new FiscalYear(2018) },
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Year, OverflowStrategy.ThrowOnOverflow), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_FiscalQuarter___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_FiscalYear_and_granularity_is_Quarter()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalYear(2017), new FiscalYear(2017)),
                    ExpectedUnitsOfTime = new[] { new FiscalQuarter(2017, QuarterNumber.Q1), new FiscalQuarter(2017, QuarterNumber.Q2), new FiscalQuarter(2017, QuarterNumber.Q3), new FiscalQuarter(2017, QuarterNumber.Q4) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalYear(2017), new FiscalYear(2018)),
                    ExpectedUnitsOfTime = new[] { new FiscalQuarter(2017, QuarterNumber.Q1), new FiscalQuarter(2017, QuarterNumber.Q2), new FiscalQuarter(2017, QuarterNumber.Q3), new FiscalQuarter(2017, QuarterNumber.Q4), new FiscalQuarter(2018, QuarterNumber.Q1), new FiscalQuarter(2018, QuarterNumber.Q2), new FiscalQuarter(2018, QuarterNumber.Q3), new FiscalQuarter(2018, QuarterNumber.Q4) },
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Quarter, OverflowStrategy.ThrowOnOverflow), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_FiscalMonth___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_FiscalYear_and_granularity_is_Month()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalYear(2017), new FiscalYear(2017)),
                    ExpectedUnitsOfTime = new[] { new FiscalMonth(2017, MonthNumber.One), new FiscalMonth(2017, MonthNumber.Two), new FiscalMonth(2017, MonthNumber.Three), new FiscalMonth(2017, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Five), new FiscalMonth(2017, MonthNumber.Six), new FiscalMonth(2017, MonthNumber.Seven), new FiscalMonth(2017, MonthNumber.Eight), new FiscalMonth(2017, MonthNumber.Nine), new FiscalMonth(2017, MonthNumber.Ten), new FiscalMonth(2017, MonthNumber.Eleven), new FiscalMonth(2017, MonthNumber.Twelve) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalYear(2017), new FiscalYear(2018)),
                    ExpectedUnitsOfTime = new[] { new FiscalMonth(2017, MonthNumber.One), new FiscalMonth(2017, MonthNumber.Two), new FiscalMonth(2017, MonthNumber.Three), new FiscalMonth(2017, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Five), new FiscalMonth(2017, MonthNumber.Six), new FiscalMonth(2017, MonthNumber.Seven), new FiscalMonth(2017, MonthNumber.Eight), new FiscalMonth(2017, MonthNumber.Nine), new FiscalMonth(2017, MonthNumber.Ten), new FiscalMonth(2017, MonthNumber.Eleven), new FiscalMonth(2017, MonthNumber.Twelve), new FiscalMonth(2018, MonthNumber.One), new FiscalMonth(2018, MonthNumber.Two), new FiscalMonth(2018, MonthNumber.Three), new FiscalMonth(2018, MonthNumber.Four), new FiscalMonth(2018, MonthNumber.Five), new FiscalMonth(2018, MonthNumber.Six), new FiscalMonth(2018, MonthNumber.Seven), new FiscalMonth(2018, MonthNumber.Eight), new FiscalMonth(2018, MonthNumber.Nine), new FiscalMonth(2018, MonthNumber.Ten), new FiscalMonth(2018, MonthNumber.Eleven), new FiscalMonth(2018, MonthNumber.Twelve) },
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Month, OverflowStrategy.ThrowOnOverflow), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
        }

        [Fact]
        public static void Split___Should_throw_ArgumentException___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_FiscalYear_and_granularity_is_Day()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new ReportingPeriod(new FiscalYear(2017), new FiscalYear(2017)),
                new ReportingPeriod(new FiscalYear(2017), new FiscalYear(2018)),
            };

            // Act
            var exceptions = reportingPeriods.Select(_ => Record.Exception(() => _.Split(UnitOfTimeGranularity.Day, OverflowStrategy.ThrowOnOverflow))).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<ArgumentException>());
        }

        [Fact]
        public static void Split___Should_throw_InvalidOperationException___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_FiscalQuarter_and_granularity_is_Year_and_there_is_overflow()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new ReportingPeriod(new FiscalQuarter(2017, QuarterNumber.Q1), new FiscalQuarter(2019, QuarterNumber.Q3)),
                new ReportingPeriod(new FiscalQuarter(2017, QuarterNumber.Q2), new FiscalQuarter(2019, QuarterNumber.Q4)),
                new ReportingPeriod(new FiscalQuarter(2017, QuarterNumber.Q1), new FiscalQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod(new FiscalQuarter(2017, QuarterNumber.Q4), new FiscalQuarter(2019, QuarterNumber.Q3)),
            };

            // Act
            var exceptions = reportingPeriods.Select(_ => Record.Exception(() => _.Split(UnitOfTimeGranularity.Year, OverflowStrategy.ThrowOnOverflow))).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_FiscalYear___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_FiscalQuarter_and_granularity_is_Year()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalQuarter(2017, QuarterNumber.Q1), new FiscalQuarter(2017, QuarterNumber.Q4)),
                    ExpectedUnitsOfTime = new[] { new FiscalYear(2017) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalQuarter(2017, QuarterNumber.Q1), new FiscalQuarter(2018, QuarterNumber.Q4)),
                    ExpectedUnitsOfTime = new[] { new FiscalYear(2017), new FiscalYear(2018) },
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Year, OverflowStrategy.ThrowOnOverflow), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_FiscalQuarter___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_FiscalQuarter_and_granularity_is_Quarter()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalQuarter(2017, QuarterNumber.Q1), new FiscalQuarter(2017, QuarterNumber.Q1)),
                    ExpectedUnitsOfTime = new[] { new FiscalQuarter(2017, QuarterNumber.Q1) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalQuarter(2017, QuarterNumber.Q2), new FiscalQuarter(2018, QuarterNumber.Q2)),
                    ExpectedUnitsOfTime = new[] { new FiscalQuarter(2017, QuarterNumber.Q2), new FiscalQuarter(2017, QuarterNumber.Q3), new FiscalQuarter(2017, QuarterNumber.Q4), new FiscalQuarter(2018, QuarterNumber.Q1), new FiscalQuarter(2018, QuarterNumber.Q2) },
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Quarter, OverflowStrategy.ThrowOnOverflow), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_FiscalMonth___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_FiscalQuarter_and_granularity_is_Month()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalQuarter(2017, QuarterNumber.Q1), new FiscalQuarter(2017, QuarterNumber.Q1)),
                    ExpectedUnitsOfTime = new[] { new FiscalMonth(2017, MonthNumber.One), new FiscalMonth(2017, MonthNumber.Two), new FiscalMonth(2017, MonthNumber.Three) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalQuarter(2017, QuarterNumber.Q2), new FiscalQuarter(2017, QuarterNumber.Q2)),
                    ExpectedUnitsOfTime = new[] { new FiscalMonth(2017, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Five), new FiscalMonth(2017, MonthNumber.Six) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalQuarter(2017, QuarterNumber.Q3), new FiscalQuarter(2017, QuarterNumber.Q3)),
                    ExpectedUnitsOfTime = new[] { new FiscalMonth(2017, MonthNumber.Seven), new FiscalMonth(2017, MonthNumber.Eight), new FiscalMonth(2017, MonthNumber.Nine) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalQuarter(2017, QuarterNumber.Q4), new FiscalQuarter(2017, QuarterNumber.Q4)),
                    ExpectedUnitsOfTime = new[] { new FiscalMonth(2017, MonthNumber.Ten), new FiscalMonth(2017, MonthNumber.Eleven), new FiscalMonth(2017, MonthNumber.Twelve) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalQuarter(2017, QuarterNumber.Q4), new FiscalQuarter(2018, QuarterNumber.Q1)),
                    ExpectedUnitsOfTime = new[] { new FiscalMonth(2017, MonthNumber.Ten), new FiscalMonth(2017, MonthNumber.Eleven), new FiscalMonth(2017, MonthNumber.Twelve), new FiscalMonth(2018, MonthNumber.One), new FiscalMonth(2018, MonthNumber.Two), new FiscalMonth(2018, MonthNumber.Three) },
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Month, OverflowStrategy.ThrowOnOverflow), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
        }

        [Fact]
        public static void Split___Should_throw_ArgumentException___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_FiscalQuarter_and_granularity_is_Day()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new ReportingPeriod(new FiscalQuarter(2017, QuarterNumber.Q1), new FiscalQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod(new FiscalQuarter(2017, QuarterNumber.Q3), new FiscalQuarter(2018, QuarterNumber.Q2)),
            };

            // Act
            var exceptions = reportingPeriods.Select(_ => Record.Exception(() => _.Split(UnitOfTimeGranularity.Day, OverflowStrategy.ThrowOnOverflow))).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<ArgumentException>());
        }

        [Fact]
        public static void Split___Should_throw_InvalidOperationException___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_FiscalMonth_and_granularity_is_Year_and_there_is_overflow()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new ReportingPeriod(new FiscalMonth(2017, MonthNumber.Three), new FiscalMonth(2018, MonthNumber.Two)),
                new ReportingPeriod(new FiscalMonth(2017, MonthNumber.Four), new FiscalMonth(2019, MonthNumber.Seven)),
                new ReportingPeriod(new FiscalMonth(2017, MonthNumber.One), new FiscalMonth(2019, MonthNumber.Eleven)),
                new ReportingPeriod(new FiscalMonth(2017, MonthNumber.Two), new FiscalMonth(2019, MonthNumber.Twelve)),
            };

            // Act
            var exceptions = reportingPeriods.Select(_ => Record.Exception(() => _.Split(UnitOfTimeGranularity.Year, OverflowStrategy.ThrowOnOverflow))).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_FiscalYear___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_FiscalMonth_and_granularity_is_Year()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalMonth(2017, MonthNumber.One), new FiscalMonth(2017, MonthNumber.Twelve)),
                    ExpectedUnitsOfTime = new[] { new FiscalYear(2017) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalMonth(2017, MonthNumber.One), new FiscalMonth(2018, MonthNumber.Twelve)),
                    ExpectedUnitsOfTime = new[] { new FiscalYear(2017), new FiscalYear(2018) },
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Year, OverflowStrategy.ThrowOnOverflow), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
        }

        [Fact]
        public static void Split___Should_throw_InvalidOperationException___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_FiscalMonth_and_granularity_is_Quarter_and_there_is_overflow()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new ReportingPeriod(new FiscalMonth(2017, MonthNumber.Two), new FiscalMonth(2018, MonthNumber.Three)),
                new ReportingPeriod(new FiscalMonth(2017, MonthNumber.Two), new FiscalMonth(2018, MonthNumber.Six)),
                new ReportingPeriod(new FiscalMonth(2017, MonthNumber.Two), new FiscalMonth(2018, MonthNumber.Nine)),
                new ReportingPeriod(new FiscalMonth(2017, MonthNumber.Two), new FiscalMonth(2018, MonthNumber.Twelve)),
                new ReportingPeriod(new FiscalMonth(2017, MonthNumber.One), new FiscalMonth(2018, MonthNumber.Two)),
                new ReportingPeriod(new FiscalMonth(2017, MonthNumber.One), new FiscalMonth(2018, MonthNumber.Five)),
                new ReportingPeriod(new FiscalMonth(2017, MonthNumber.One), new FiscalMonth(2018, MonthNumber.Eight)),
                new ReportingPeriod(new FiscalMonth(2017, MonthNumber.One), new FiscalMonth(2018, MonthNumber.Eleven)),
                new ReportingPeriod(new FiscalMonth(2017, MonthNumber.Four), new FiscalMonth(2018, MonthNumber.Eleven)),
                new ReportingPeriod(new FiscalMonth(2017, MonthNumber.Five), new FiscalMonth(2018, MonthNumber.Twelve)),
                new ReportingPeriod(new FiscalMonth(2017, MonthNumber.Six), new FiscalMonth(2018, MonthNumber.Twelve)),
                new ReportingPeriod(new FiscalMonth(2017, MonthNumber.Seven), new FiscalMonth(2018, MonthNumber.Eleven)),
                new ReportingPeriod(new FiscalMonth(2017, MonthNumber.Eight), new FiscalMonth(2018, MonthNumber.Twelve)),
                new ReportingPeriod(new FiscalMonth(2017, MonthNumber.Nine), new FiscalMonth(2018, MonthNumber.Twelve)),
                new ReportingPeriod(new FiscalMonth(2017, MonthNumber.Ten), new FiscalMonth(2018, MonthNumber.Eleven)),
                new ReportingPeriod(new FiscalMonth(2017, MonthNumber.Eleven), new FiscalMonth(2018, MonthNumber.Eleven)),
                new ReportingPeriod(new FiscalMonth(2017, MonthNumber.Eleven), new FiscalMonth(2018, MonthNumber.Two)),
                new ReportingPeriod(new FiscalMonth(2017, MonthNumber.Four), new FiscalMonth(2018, MonthNumber.Four)),
            };

            // Act
            var exceptions = reportingPeriods.Select(_ => Record.Exception(() => _.Split(UnitOfTimeGranularity.Year, OverflowStrategy.ThrowOnOverflow))).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_FiscalQuarter___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_FiscalMonth_and_granularity_is_Quarter()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalMonth(2017, MonthNumber.One), new FiscalMonth(2017, MonthNumber.Three)),
                    ExpectedUnitsOfTime = new[] { new FiscalQuarter(2017, QuarterNumber.Q1) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalMonth(2017, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Six)),
                    ExpectedUnitsOfTime = new[] { new FiscalQuarter(2017, QuarterNumber.Q2) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalMonth(2017, MonthNumber.Seven), new FiscalMonth(2017, MonthNumber.Nine)),
                    ExpectedUnitsOfTime = new[] { new FiscalQuarter(2017, QuarterNumber.Q3) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalMonth(2017, MonthNumber.Ten), new FiscalMonth(2017, MonthNumber.Twelve)),
                    ExpectedUnitsOfTime = new[] { new FiscalQuarter(2017, QuarterNumber.Q4) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalMonth(2017, MonthNumber.One), new FiscalMonth(2018, MonthNumber.Three)),
                    ExpectedUnitsOfTime = new[] { new FiscalQuarter(2017, QuarterNumber.Q1), new FiscalQuarter(2017, QuarterNumber.Q2), new FiscalQuarter(2017, QuarterNumber.Q3), new FiscalQuarter(2017, QuarterNumber.Q4), new FiscalQuarter(2018, QuarterNumber.Q1) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalMonth(2017, MonthNumber.Four), new FiscalMonth(2018, MonthNumber.Six)),
                    ExpectedUnitsOfTime = new[] { new FiscalQuarter(2017, QuarterNumber.Q2), new FiscalQuarter(2017, QuarterNumber.Q3), new FiscalQuarter(2017, QuarterNumber.Q4), new FiscalQuarter(2018, QuarterNumber.Q1), new FiscalQuarter(2018, QuarterNumber.Q2) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalMonth(2017, MonthNumber.Seven), new FiscalMonth(2018, MonthNumber.Nine)),
                    ExpectedUnitsOfTime = new[] { new FiscalQuarter(2017, QuarterNumber.Q3), new FiscalQuarter(2017, QuarterNumber.Q4), new FiscalQuarter(2018, QuarterNumber.Q1), new FiscalQuarter(2018, QuarterNumber.Q2), new FiscalQuarter(2018, QuarterNumber.Q3) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalMonth(2017, MonthNumber.Ten), new FiscalMonth(2018, MonthNumber.Twelve)),
                    ExpectedUnitsOfTime = new[] { new FiscalQuarter(2017, QuarterNumber.Q4), new FiscalQuarter(2018, QuarterNumber.Q1), new FiscalQuarter(2018, QuarterNumber.Q2), new FiscalQuarter(2018, QuarterNumber.Q3), new FiscalQuarter(2018, QuarterNumber.Q4) },
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Quarter, OverflowStrategy.ThrowOnOverflow), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_FiscalMonth___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_FiscalMonth_and_granularity_is_Month()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new FiscalMonth(2017, MonthNumber.Eleven), new FiscalMonth(2018, MonthNumber.Three)),
                    ExpectedUnitsOfTime = new[] { new FiscalMonth(2017, MonthNumber.Eleven), new FiscalMonth(2017, MonthNumber.Twelve), new FiscalMonth(2018, MonthNumber.One), new FiscalMonth(2018, MonthNumber.Two), new FiscalMonth(2018, MonthNumber.Three) },
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Month, OverflowStrategy.ThrowOnOverflow), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
        }

        [Fact]
        public static void Split___Should_throw_ArgumentException___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_FiscalMonth_and_granularity_is_Day()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new ReportingPeriod(new FiscalMonth(2017, MonthNumber.Eleven), new FiscalMonth(2018, MonthNumber.Three)),
            };

            // Act
            var exceptions = reportingPeriods.Select(_ => Record.Exception(() => _.Split(UnitOfTimeGranularity.Day, OverflowStrategy.ThrowOnOverflow))).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<ArgumentException>());
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_GenericYear___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_GenericYear_and_granularity_is_Year()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new GenericYear(2017), new GenericYear(2017)),
                    ExpectedUnitsOfTime = new[] { new GenericYear(2017) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new GenericYear(2017), new GenericYear(2018)),
                    ExpectedUnitsOfTime = new[] { new GenericYear(2017), new GenericYear(2018) },
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Year, OverflowStrategy.ThrowOnOverflow), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_GenericQuarter___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_GenericYear_and_granularity_is_Quarter()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new GenericYear(2017), new GenericYear(2017)),
                    ExpectedUnitsOfTime = new[] { new GenericQuarter(2017, QuarterNumber.Q1), new GenericQuarter(2017, QuarterNumber.Q2), new GenericQuarter(2017, QuarterNumber.Q3), new GenericQuarter(2017, QuarterNumber.Q4) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new GenericYear(2017), new GenericYear(2018)),
                    ExpectedUnitsOfTime = new[] { new GenericQuarter(2017, QuarterNumber.Q1), new GenericQuarter(2017, QuarterNumber.Q2), new GenericQuarter(2017, QuarterNumber.Q3), new GenericQuarter(2017, QuarterNumber.Q4), new GenericQuarter(2018, QuarterNumber.Q1), new GenericQuarter(2018, QuarterNumber.Q2), new GenericQuarter(2018, QuarterNumber.Q3), new GenericQuarter(2018, QuarterNumber.Q4) },
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Quarter, OverflowStrategy.ThrowOnOverflow), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_GenericMonth___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_GenericYear_and_granularity_is_Month()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new GenericYear(2017), new GenericYear(2017)),
                    ExpectedUnitsOfTime = new[] { new GenericMonth(2017, MonthNumber.One), new GenericMonth(2017, MonthNumber.Two), new GenericMonth(2017, MonthNumber.Three), new GenericMonth(2017, MonthNumber.Four), new GenericMonth(2017, MonthNumber.Five), new GenericMonth(2017, MonthNumber.Six), new GenericMonth(2017, MonthNumber.Seven), new GenericMonth(2017, MonthNumber.Eight), new GenericMonth(2017, MonthNumber.Nine), new GenericMonth(2017, MonthNumber.Ten), new GenericMonth(2017, MonthNumber.Eleven), new GenericMonth(2017, MonthNumber.Twelve) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new GenericYear(2017), new GenericYear(2018)),
                    ExpectedUnitsOfTime = new[] { new GenericMonth(2017, MonthNumber.One), new GenericMonth(2017, MonthNumber.Two), new GenericMonth(2017, MonthNumber.Three), new GenericMonth(2017, MonthNumber.Four), new GenericMonth(2017, MonthNumber.Five), new GenericMonth(2017, MonthNumber.Six), new GenericMonth(2017, MonthNumber.Seven), new GenericMonth(2017, MonthNumber.Eight), new GenericMonth(2017, MonthNumber.Nine), new GenericMonth(2017, MonthNumber.Ten), new GenericMonth(2017, MonthNumber.Eleven), new GenericMonth(2017, MonthNumber.Twelve), new GenericMonth(2018, MonthNumber.One), new GenericMonth(2018, MonthNumber.Two), new GenericMonth(2018, MonthNumber.Three), new GenericMonth(2018, MonthNumber.Four), new GenericMonth(2018, MonthNumber.Five), new GenericMonth(2018, MonthNumber.Six), new GenericMonth(2018, MonthNumber.Seven), new GenericMonth(2018, MonthNumber.Eight), new GenericMonth(2018, MonthNumber.Nine), new GenericMonth(2018, MonthNumber.Ten), new GenericMonth(2018, MonthNumber.Eleven), new GenericMonth(2018, MonthNumber.Twelve) },
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Month, OverflowStrategy.ThrowOnOverflow), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
        }

        [Fact]
        public static void Split___Should_throw_ArgumentException___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_GenericYear_and_granularity_is_Day()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new ReportingPeriod(new GenericYear(2017), new GenericYear(2017)),
                new ReportingPeriod(new GenericYear(2017), new GenericYear(2018)),
            };

            // Act
            var exceptions = reportingPeriods.Select(_ => Record.Exception(() => _.Split(UnitOfTimeGranularity.Day, OverflowStrategy.ThrowOnOverflow))).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<ArgumentException>());
        }

        [Fact]
        public static void Split___Should_throw_InvalidOperationException___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_GenericQuarter_and_granularity_is_Year_and_there_is_overflow()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new ReportingPeriod(new GenericQuarter(2017, QuarterNumber.Q1), new GenericQuarter(2019, QuarterNumber.Q3)),
                new ReportingPeriod(new GenericQuarter(2017, QuarterNumber.Q2), new GenericQuarter(2019, QuarterNumber.Q4)),
                new ReportingPeriod(new GenericQuarter(2017, QuarterNumber.Q1), new GenericQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod(new GenericQuarter(2017, QuarterNumber.Q4), new GenericQuarter(2019, QuarterNumber.Q3)),
            };

            // Act
            var exceptions = reportingPeriods.Select(_ => Record.Exception(() => _.Split(UnitOfTimeGranularity.Year, OverflowStrategy.ThrowOnOverflow))).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_GenericYear___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_GenericQuarter_and_granularity_is_Year()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new GenericQuarter(2017, QuarterNumber.Q1), new GenericQuarter(2017, QuarterNumber.Q4)),
                    ExpectedUnitsOfTime = new[] { new GenericYear(2017) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new GenericQuarter(2017, QuarterNumber.Q1), new GenericQuarter(2018, QuarterNumber.Q4)),
                    ExpectedUnitsOfTime = new[] { new GenericYear(2017), new GenericYear(2018) },
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Year, OverflowStrategy.ThrowOnOverflow), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_GenericQuarter___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_GenericQuarter_and_granularity_is_Quarter()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new GenericQuarter(2017, QuarterNumber.Q1), new GenericQuarter(2017, QuarterNumber.Q1)),
                    ExpectedUnitsOfTime = new[] { new GenericQuarter(2017, QuarterNumber.Q1) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new GenericQuarter(2017, QuarterNumber.Q2), new GenericQuarter(2018, QuarterNumber.Q2)),
                    ExpectedUnitsOfTime = new[] { new GenericQuarter(2017, QuarterNumber.Q2), new GenericQuarter(2017, QuarterNumber.Q3), new GenericQuarter(2017, QuarterNumber.Q4), new GenericQuarter(2018, QuarterNumber.Q1), new GenericQuarter(2018, QuarterNumber.Q2) },
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Quarter, OverflowStrategy.ThrowOnOverflow), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_GenericMonth___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_GenericQuarter_and_granularity_is_Month()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new GenericQuarter(2017, QuarterNumber.Q1), new GenericQuarter(2017, QuarterNumber.Q1)),
                    ExpectedUnitsOfTime = new[] { new GenericMonth(2017, MonthNumber.One), new GenericMonth(2017, MonthNumber.Two), new GenericMonth(2017, MonthNumber.Three) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new GenericQuarter(2017, QuarterNumber.Q2), new GenericQuarter(2017, QuarterNumber.Q2)),
                    ExpectedUnitsOfTime = new[] { new GenericMonth(2017, MonthNumber.Four), new GenericMonth(2017, MonthNumber.Five), new GenericMonth(2017, MonthNumber.Six) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new GenericQuarter(2017, QuarterNumber.Q3), new GenericQuarter(2017, QuarterNumber.Q3)),
                    ExpectedUnitsOfTime = new[] { new GenericMonth(2017, MonthNumber.Seven), new GenericMonth(2017, MonthNumber.Eight), new GenericMonth(2017, MonthNumber.Nine) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new GenericQuarter(2017, QuarterNumber.Q4), new GenericQuarter(2017, QuarterNumber.Q4)),
                    ExpectedUnitsOfTime = new[] { new GenericMonth(2017, MonthNumber.Ten), new GenericMonth(2017, MonthNumber.Eleven), new GenericMonth(2017, MonthNumber.Twelve) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new GenericQuarter(2017, QuarterNumber.Q4), new GenericQuarter(2018, QuarterNumber.Q1)),
                    ExpectedUnitsOfTime = new[] { new GenericMonth(2017, MonthNumber.Ten), new GenericMonth(2017, MonthNumber.Eleven), new GenericMonth(2017, MonthNumber.Twelve), new GenericMonth(2018, MonthNumber.One), new GenericMonth(2018, MonthNumber.Two), new GenericMonth(2018, MonthNumber.Three) },
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Month, OverflowStrategy.ThrowOnOverflow), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
        }

        [Fact]
        public static void Split___Should_throw_ArgumentException___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_GenericQuarter_and_granularity_is_Day()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new ReportingPeriod(new GenericQuarter(2017, QuarterNumber.Q1), new GenericQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod(new GenericQuarter(2017, QuarterNumber.Q3), new GenericQuarter(2018, QuarterNumber.Q2)),
            };

            // Act
            var exceptions = reportingPeriods.Select(_ => Record.Exception(() => _.Split(UnitOfTimeGranularity.Day, OverflowStrategy.ThrowOnOverflow))).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<ArgumentException>());
        }

        [Fact]
        public static void Split___Should_throw_InvalidOperationException___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_GenericMonth_and_granularity_is_Year_and_there_is_overflow()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new ReportingPeriod(new GenericMonth(2017, MonthNumber.Three), new GenericMonth(2018, MonthNumber.Two)),
                new ReportingPeriod(new GenericMonth(2017, MonthNumber.Four), new GenericMonth(2019, MonthNumber.Seven)),
                new ReportingPeriod(new GenericMonth(2017, MonthNumber.One), new GenericMonth(2019, MonthNumber.Eleven)),
                new ReportingPeriod(new GenericMonth(2017, MonthNumber.Two), new GenericMonth(2019, MonthNumber.Twelve)),
            };

            // Act
            var exceptions = reportingPeriods.Select(_ => Record.Exception(() => _.Split(UnitOfTimeGranularity.Year, OverflowStrategy.ThrowOnOverflow))).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_GenericYear___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_GenericMonth_and_granularity_is_Year()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new GenericMonth(2017, MonthNumber.One), new GenericMonth(2017, MonthNumber.Twelve)),
                    ExpectedUnitsOfTime = new[] { new GenericYear(2017) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new GenericMonth(2017, MonthNumber.One), new GenericMonth(2018, MonthNumber.Twelve)),
                    ExpectedUnitsOfTime = new[] { new GenericYear(2017), new GenericYear(2018) },
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Year, OverflowStrategy.ThrowOnOverflow), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
        }

        [Fact]
        public static void Split___Should_throw_InvalidOperationException___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_GenericMonth_and_granularity_is_Quarter_and_there_is_overflow()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new ReportingPeriod(new GenericMonth(2017, MonthNumber.Two), new GenericMonth(2018, MonthNumber.Three)),
                new ReportingPeriod(new GenericMonth(2017, MonthNumber.Two), new GenericMonth(2018, MonthNumber.Six)),
                new ReportingPeriod(new GenericMonth(2017, MonthNumber.Two), new GenericMonth(2018, MonthNumber.Nine)),
                new ReportingPeriod(new GenericMonth(2017, MonthNumber.Two), new GenericMonth(2018, MonthNumber.Twelve)),
                new ReportingPeriod(new GenericMonth(2017, MonthNumber.One), new GenericMonth(2018, MonthNumber.Two)),
                new ReportingPeriod(new GenericMonth(2017, MonthNumber.One), new GenericMonth(2018, MonthNumber.Five)),
                new ReportingPeriod(new GenericMonth(2017, MonthNumber.One), new GenericMonth(2018, MonthNumber.Eight)),
                new ReportingPeriod(new GenericMonth(2017, MonthNumber.One), new GenericMonth(2018, MonthNumber.Eleven)),
                new ReportingPeriod(new GenericMonth(2017, MonthNumber.Four), new GenericMonth(2018, MonthNumber.Eleven)),
                new ReportingPeriod(new GenericMonth(2017, MonthNumber.Five), new GenericMonth(2018, MonthNumber.Twelve)),
                new ReportingPeriod(new GenericMonth(2017, MonthNumber.Six), new GenericMonth(2018, MonthNumber.Twelve)),
                new ReportingPeriod(new GenericMonth(2017, MonthNumber.Seven), new GenericMonth(2018, MonthNumber.Eleven)),
                new ReportingPeriod(new GenericMonth(2017, MonthNumber.Eight), new GenericMonth(2018, MonthNumber.Twelve)),
                new ReportingPeriod(new GenericMonth(2017, MonthNumber.Nine), new GenericMonth(2018, MonthNumber.Twelve)),
                new ReportingPeriod(new GenericMonth(2017, MonthNumber.Ten), new GenericMonth(2018, MonthNumber.Eleven)),
                new ReportingPeriod(new GenericMonth(2017, MonthNumber.Eleven), new GenericMonth(2018, MonthNumber.Eleven)),
                new ReportingPeriod(new GenericMonth(2017, MonthNumber.Eleven), new GenericMonth(2018, MonthNumber.Two)),
                new ReportingPeriod(new GenericMonth(2017, MonthNumber.Four), new GenericMonth(2018, MonthNumber.Four)),
            };

            // Act
            var exceptions = reportingPeriods.Select(_ => Record.Exception(() => _.Split(UnitOfTimeGranularity.Year, OverflowStrategy.ThrowOnOverflow))).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_GenericQuarter___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_GenericMonth_and_granularity_is_Quarter()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new GenericMonth(2017, MonthNumber.One), new GenericMonth(2017, MonthNumber.Three)),
                    ExpectedUnitsOfTime = new[] { new GenericQuarter(2017, QuarterNumber.Q1) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new GenericMonth(2017, MonthNumber.Four), new GenericMonth(2017, MonthNumber.Six)),
                    ExpectedUnitsOfTime = new[] { new GenericQuarter(2017, QuarterNumber.Q2) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new GenericMonth(2017, MonthNumber.Seven), new GenericMonth(2017, MonthNumber.Nine)),
                    ExpectedUnitsOfTime = new[] { new GenericQuarter(2017, QuarterNumber.Q3) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new GenericMonth(2017, MonthNumber.Ten), new GenericMonth(2017, MonthNumber.Twelve)),
                    ExpectedUnitsOfTime = new[] { new GenericQuarter(2017, QuarterNumber.Q4) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new GenericMonth(2017, MonthNumber.One), new GenericMonth(2018, MonthNumber.Three)),
                    ExpectedUnitsOfTime = new[] { new GenericQuarter(2017, QuarterNumber.Q1), new GenericQuarter(2017, QuarterNumber.Q2), new GenericQuarter(2017, QuarterNumber.Q3), new GenericQuarter(2017, QuarterNumber.Q4), new GenericQuarter(2018, QuarterNumber.Q1) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new GenericMonth(2017, MonthNumber.Four), new GenericMonth(2018, MonthNumber.Six)),
                    ExpectedUnitsOfTime = new[] { new GenericQuarter(2017, QuarterNumber.Q2), new GenericQuarter(2017, QuarterNumber.Q3), new GenericQuarter(2017, QuarterNumber.Q4), new GenericQuarter(2018, QuarterNumber.Q1), new GenericQuarter(2018, QuarterNumber.Q2) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new GenericMonth(2017, MonthNumber.Seven), new GenericMonth(2018, MonthNumber.Nine)),
                    ExpectedUnitsOfTime = new[] { new GenericQuarter(2017, QuarterNumber.Q3), new GenericQuarter(2017, QuarterNumber.Q4), new GenericQuarter(2018, QuarterNumber.Q1), new GenericQuarter(2018, QuarterNumber.Q2), new GenericQuarter(2018, QuarterNumber.Q3) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new GenericMonth(2017, MonthNumber.Ten), new GenericMonth(2018, MonthNumber.Twelve)),
                    ExpectedUnitsOfTime = new[] { new GenericQuarter(2017, QuarterNumber.Q4), new GenericQuarter(2018, QuarterNumber.Q1), new GenericQuarter(2018, QuarterNumber.Q2), new GenericQuarter(2018, QuarterNumber.Q3), new GenericQuarter(2018, QuarterNumber.Q4) },
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Quarter, OverflowStrategy.ThrowOnOverflow), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_GenericMonth___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_GenericMonth_and_granularity_is_Month()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new GenericMonth(2017, MonthNumber.Eleven), new GenericMonth(2018, MonthNumber.Three)),
                    ExpectedUnitsOfTime = new[] { new GenericMonth(2017, MonthNumber.Eleven), new GenericMonth(2017, MonthNumber.Twelve), new GenericMonth(2018, MonthNumber.One), new GenericMonth(2018, MonthNumber.Two), new GenericMonth(2018, MonthNumber.Three) },
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Month, OverflowStrategy.ThrowOnOverflow), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
        }

        [Fact]
        public static void Split___Should_throw_ArgumentException___When_overflowStrategy_is_ThrowOnOverflow_and_reportingPeriod_is_in_GenericMonth_and_granularity_is_Day()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new ReportingPeriod(new GenericMonth(2017, MonthNumber.Eleven), new GenericMonth(2018, MonthNumber.Three)),
            };

            // Act
            var exceptions = reportingPeriods.Select(_ => Record.Exception(() => _.Split(UnitOfTimeGranularity.Day, OverflowStrategy.ThrowOnOverflow))).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<ArgumentException>());
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_CalendarYear___When_overflowStrategy_is_DiscardOverflow_and_reportingPeriod_is_in_CalendarYear_and_granularity_is_Year()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarYear(2017), new CalendarYear(2017)),
                    ExpectedUnitsOfTime = new[] { new CalendarYear(2017) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarYear(2017), new CalendarYear(2018)),
                    ExpectedUnitsOfTime = new[] { new CalendarYear(2017), new CalendarYear(2018) },
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Year, OverflowStrategy.DiscardOverflow), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_CalendarQuarter___When_overflowStrategy_is_DiscardOverflow_and_reportingPeriod_is_in_CalendarYear_and_granularity_is_Quarter()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarYear(2017), new CalendarYear(2017)),
                    ExpectedUnitsOfTime = new[] { new CalendarQuarter(2017, QuarterNumber.Q1), new CalendarQuarter(2017, QuarterNumber.Q2), new CalendarQuarter(2017, QuarterNumber.Q3), new CalendarQuarter(2017, QuarterNumber.Q4) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarYear(2017), new CalendarYear(2018)),
                    ExpectedUnitsOfTime = new[] { new CalendarQuarter(2017, QuarterNumber.Q1), new CalendarQuarter(2017, QuarterNumber.Q2), new CalendarQuarter(2017, QuarterNumber.Q3), new CalendarQuarter(2017, QuarterNumber.Q4), new CalendarQuarter(2018, QuarterNumber.Q1), new CalendarQuarter(2018, QuarterNumber.Q2), new CalendarQuarter(2018, QuarterNumber.Q3), new CalendarQuarter(2018, QuarterNumber.Q4) },
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Quarter, OverflowStrategy.DiscardOverflow), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_CalendarMonth___When_overflowStrategy_is_DiscardOverflow_and_reportingPeriod_is_in_CalendarYear_and_granularity_is_Month()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarYear(2017), new CalendarYear(2017)),
                    ExpectedUnitsOfTime = new[] { new CalendarMonth(2017, MonthOfYear.January), new CalendarMonth(2017, MonthOfYear.February), new CalendarMonth(2017, MonthOfYear.March), new CalendarMonth(2017, MonthOfYear.April), new CalendarMonth(2017, MonthOfYear.May), new CalendarMonth(2017, MonthOfYear.June), new CalendarMonth(2017, MonthOfYear.July), new CalendarMonth(2017, MonthOfYear.August), new CalendarMonth(2017, MonthOfYear.September), new CalendarMonth(2017, MonthOfYear.October), new CalendarMonth(2017, MonthOfYear.November), new CalendarMonth(2017, MonthOfYear.December) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarYear(2017), new CalendarYear(2018)),
                    ExpectedUnitsOfTime = new[] { new CalendarMonth(2017, MonthOfYear.January), new CalendarMonth(2017, MonthOfYear.February), new CalendarMonth(2017, MonthOfYear.March), new CalendarMonth(2017, MonthOfYear.April), new CalendarMonth(2017, MonthOfYear.May), new CalendarMonth(2017, MonthOfYear.June), new CalendarMonth(2017, MonthOfYear.July), new CalendarMonth(2017, MonthOfYear.August), new CalendarMonth(2017, MonthOfYear.September), new CalendarMonth(2017, MonthOfYear.October), new CalendarMonth(2017, MonthOfYear.November), new CalendarMonth(2017, MonthOfYear.December), new CalendarMonth(2018, MonthOfYear.January), new CalendarMonth(2018, MonthOfYear.February), new CalendarMonth(2018, MonthOfYear.March), new CalendarMonth(2018, MonthOfYear.April), new CalendarMonth(2018, MonthOfYear.May), new CalendarMonth(2018, MonthOfYear.June), new CalendarMonth(2018, MonthOfYear.July), new CalendarMonth(2018, MonthOfYear.August), new CalendarMonth(2018, MonthOfYear.September), new CalendarMonth(2018, MonthOfYear.October), new CalendarMonth(2018, MonthOfYear.November), new CalendarMonth(2018, MonthOfYear.December) },
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Month, OverflowStrategy.DiscardOverflow), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_CalendarDay___When_overflowStrategy_is_DiscardOverflow_and_reportingPeriod_is_in_CalendarYear_and_granularity_is_Day()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarYear(2017), new CalendarYear(2017)),
                    ExpectedUnitsOfTimeBeginning = new[] { new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One), new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Two), new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Three) },
                    ExpectedUnitsOfTimeEnd = new[] { new CalendarDay(2017, MonthOfYear.December, DayOfMonth.TwentyNine), new CalendarDay(2017, MonthOfYear.December, DayOfMonth.Thirty), new CalendarDay(2017, MonthOfYear.December, DayOfMonth.ThirtyOne) },
                    ExpectedCount = 365,
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarYear(2017), new CalendarYear(2018)),
                    ExpectedUnitsOfTimeBeginning = new[] { new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One), new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Two), new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Three) },
                    ExpectedUnitsOfTimeEnd = new[] { new CalendarDay(2018, MonthOfYear.December, DayOfMonth.TwentyNine), new CalendarDay(2018, MonthOfYear.December, DayOfMonth.Thirty), new CalendarDay(2018, MonthOfYear.December, DayOfMonth.ThirtyOne) },
                    ExpectedCount = 365 * 2,
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Day, OverflowStrategy.DiscardOverflow), ExpectedBeginning = _.ExpectedUnitsOfTimeBeginning, ExpectedEnd = _.ExpectedUnitsOfTimeEnd, _.ExpectedCount }).ToList();

            // Assert
            results.ForEach(_ =>
            {
                _.Actual.Should().StartWith(_.ExpectedBeginning);
                _.Actual.Should().EndWith(_.ExpectedEnd);
                _.Actual.Should().HaveCount(_.ExpectedCount);
            });
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_CalendarYear_discarding_overflow___When_overflowStrategy_is_DiscardOverflow_and_reportingPeriod_is_in_CalendarQuarter_and_granularity_is_Year_and_there_is_overflow()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new ReportingPeriod(new CalendarQuarter(2017, QuarterNumber.Q1), new CalendarQuarter(2019, QuarterNumber.Q3)),
                new ReportingPeriod(new CalendarQuarter(2017, QuarterNumber.Q2), new CalendarQuarter(2019, QuarterNumber.Q4)),
                new ReportingPeriod(new CalendarQuarter(2017, QuarterNumber.Q1), new CalendarQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod(new CalendarQuarter(2017, QuarterNumber.Q4), new CalendarQuarter(2019, QuarterNumber.Q3)),
            };

            var expected = new IReadOnlyList<UnitOfTime>[]
            {
                new[]
                {
                    new CalendarYear(2017),
                    new CalendarYear(2018),
                },
                new[]
                {
                    new CalendarYear(2018),
                    new CalendarYear(2019),
                },
                new CalendarYear[]
                {
                },
                new CalendarYear[]
                {
                    new CalendarYear(2018),
                },
            };

            // Act
            var actual = reportingPeriods.Select(_ => _.Split(UnitOfTimeGranularity.Year, OverflowStrategy.DiscardOverflow)).ToArray();

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_CalendarYear___When_overflowStrategy_is_DiscardOverflow_and_reportingPeriod_is_in_CalendarQuarter_and_granularity_is_Year()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarQuarter(2017, QuarterNumber.Q1), new CalendarQuarter(2017, QuarterNumber.Q4)),
                    ExpectedUnitsOfTime = new[] { new CalendarYear(2017) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarQuarter(2017, QuarterNumber.Q1), new CalendarQuarter(2018, QuarterNumber.Q4)),
                    ExpectedUnitsOfTime = new[] { new CalendarYear(2017), new CalendarYear(2018) },
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Year, OverflowStrategy.DiscardOverflow), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_CalendarQuarter___When_overflowStrategy_is_DiscardOverflow_and_reportingPeriod_is_in_CalendarQuarter_and_granularity_is_Quarter()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarQuarter(2017, QuarterNumber.Q1), new CalendarQuarter(2017, QuarterNumber.Q1)),
                    ExpectedUnitsOfTime = new[] { new CalendarQuarter(2017, QuarterNumber.Q1) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarQuarter(2017, QuarterNumber.Q2), new CalendarQuarter(2018, QuarterNumber.Q2)),
                    ExpectedUnitsOfTime = new[] { new CalendarQuarter(2017, QuarterNumber.Q2), new CalendarQuarter(2017, QuarterNumber.Q3), new CalendarQuarter(2017, QuarterNumber.Q4), new CalendarQuarter(2018, QuarterNumber.Q1), new CalendarQuarter(2018, QuarterNumber.Q2) },
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Quarter, OverflowStrategy.DiscardOverflow), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_CalendarMonth___When_overflowStrategy_is_DiscardOverflow_and_reportingPeriod_is_in_CalendarQuarter_and_granularity_is_Month()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarQuarter(2017, QuarterNumber.Q1), new CalendarQuarter(2017, QuarterNumber.Q1)),
                    ExpectedUnitsOfTime = new[] { new CalendarMonth(2017, MonthOfYear.January), new CalendarMonth(2017, MonthOfYear.February), new CalendarMonth(2017, MonthOfYear.March) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarQuarter(2017, QuarterNumber.Q2), new CalendarQuarter(2017, QuarterNumber.Q2)),
                    ExpectedUnitsOfTime = new[] { new CalendarMonth(2017, MonthOfYear.April), new CalendarMonth(2017, MonthOfYear.May), new CalendarMonth(2017, MonthOfYear.June) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarQuarter(2017, QuarterNumber.Q3), new CalendarQuarter(2017, QuarterNumber.Q3)),
                    ExpectedUnitsOfTime = new[] { new CalendarMonth(2017, MonthOfYear.July), new CalendarMonth(2017, MonthOfYear.August), new CalendarMonth(2017, MonthOfYear.September) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarQuarter(2017, QuarterNumber.Q4), new CalendarQuarter(2017, QuarterNumber.Q4)),
                    ExpectedUnitsOfTime = new[] { new CalendarMonth(2017, MonthOfYear.October), new CalendarMonth(2017, MonthOfYear.November), new CalendarMonth(2017, MonthOfYear.December) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarQuarter(2017, QuarterNumber.Q4), new CalendarQuarter(2018, QuarterNumber.Q1)),
                    ExpectedUnitsOfTime = new[] { new CalendarMonth(2017, MonthOfYear.October), new CalendarMonth(2017, MonthOfYear.November), new CalendarMonth(2017, MonthOfYear.December), new CalendarMonth(2018, MonthOfYear.January), new CalendarMonth(2018, MonthOfYear.February), new CalendarMonth(2018, MonthOfYear.March) },
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Month, OverflowStrategy.DiscardOverflow), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_CalendarDay___When_overflowStrategy_is_DiscardOverflow_and_reportingPeriod_is_in_CalendarQuarter_and_granularity_is_Day()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarQuarter(2017, QuarterNumber.Q4), new CalendarQuarter(2017, QuarterNumber.Q4)),
                    ExpectedUnitsOfTimeBeginning = new[] { new CalendarDay(2017, MonthOfYear.October, DayOfMonth.One), new CalendarDay(2017, MonthOfYear.October, DayOfMonth.Two), new CalendarDay(2017, MonthOfYear.October, DayOfMonth.Three) },
                    ExpectedUnitsOfTimeEnd = new[] { new CalendarDay(2017, MonthOfYear.December, DayOfMonth.TwentyNine), new CalendarDay(2017, MonthOfYear.December, DayOfMonth.Thirty), new CalendarDay(2017, MonthOfYear.December, DayOfMonth.ThirtyOne) },
                    ExpectedCount = 31 + 30 + 31,
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarQuarter(2017, QuarterNumber.Q2), new CalendarQuarter(2018, QuarterNumber.Q3)),
                    ExpectedUnitsOfTimeBeginning = new[] { new CalendarDay(2017, MonthOfYear.April, DayOfMonth.One), new CalendarDay(2017, MonthOfYear.April, DayOfMonth.Two), new CalendarDay(2017, MonthOfYear.April, DayOfMonth.Three) },
                    ExpectedUnitsOfTimeEnd = new[] { new CalendarDay(2018, MonthOfYear.September, DayOfMonth.TwentyEight), new CalendarDay(2018, MonthOfYear.September, DayOfMonth.TwentyNine), new CalendarDay(2018, MonthOfYear.September, DayOfMonth.Thirty) },
                    ExpectedCount = 548,
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Day, OverflowStrategy.DiscardOverflow), ExpectedBeginning = _.ExpectedUnitsOfTimeBeginning, ExpectedEnd = _.ExpectedUnitsOfTimeEnd, _.ExpectedCount }).ToList();

            // Assert
            results.ForEach(_ =>
            {
                _.Actual.Should().StartWith(_.ExpectedBeginning);
                _.Actual.Should().EndWith(_.ExpectedEnd);
                _.Actual.Should().HaveCount(_.ExpectedCount);
            });
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_CalendarYear_discarding_overflow___When_overflowStrategy_is_DiscardOverflow_and_reportingPeriod_is_in_CalendarMonth_and_granularity_is_Year_and_there_is_overflow()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.March), new CalendarMonth(2018, MonthOfYear.February)),
                new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.April), new CalendarMonth(2019, MonthOfYear.July)),
                new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.January), new CalendarMonth(2019, MonthOfYear.November)),
                new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.February), new CalendarMonth(2019, MonthOfYear.December)),
            };

            var expected = new IReadOnlyList<UnitOfTime>[]
            {
                new CalendarYear[]
                {
                },
                new[]
                {
                    new CalendarYear(2018),
                },
                new CalendarYear[]
                {
                    new CalendarYear(2017),
                    new CalendarYear(2018),
                },
                new CalendarYear[]
                {
                    new CalendarYear(2018),
                    new CalendarYear(2019),
                },
            };

            // Act
            var actual = reportingPeriods.Select(_ => _.Split(UnitOfTimeGranularity.Year, OverflowStrategy.DiscardOverflow)).ToArray();

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_CalendarYear___When_overflowStrategy_is_DiscardOverflow_and_reportingPeriod_is_in_CalendarMonth_and_granularity_is_Year()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.January), new CalendarMonth(2017, MonthOfYear.December)),
                    ExpectedUnitsOfTime = new[] { new CalendarYear(2017) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.January), new CalendarMonth(2018, MonthOfYear.December)),
                    ExpectedUnitsOfTime = new[] { new CalendarYear(2017), new CalendarYear(2018) },
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Year, OverflowStrategy.DiscardOverflow), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_CalendarQuarter_discarding_overflow___When_overflowStrategy_is_DiscardOverflow_and_reportingPeriod_is_in_CalendarMonth_and_granularity_is_Quarter_and_there_is_overflow()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.February), new CalendarMonth(2018, MonthOfYear.March)),
                new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.February), new CalendarMonth(2018, MonthOfYear.July)),
                new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.February), new CalendarMonth(2018, MonthOfYear.May)),
                new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.January), new CalendarMonth(2018, MonthOfYear.February)),
                new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.January), new CalendarMonth(2018, MonthOfYear.March)),
                new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.January), new CalendarMonth(2018, MonthOfYear.April)),
                new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.February), new CalendarMonth(2017, MonthOfYear.April)),
            };

            var expected = new IReadOnlyList<UnitOfTime>[]
            {
                new[]
                {
                    new CalendarQuarter(2017, QuarterNumber.Q2),
                    new CalendarQuarter(2017, QuarterNumber.Q3),
                    new CalendarQuarter(2017, QuarterNumber.Q4),
                    new CalendarQuarter(2018, QuarterNumber.Q1),
                },
                new[]
                {
                    new CalendarQuarter(2017, QuarterNumber.Q2),
                    new CalendarQuarter(2017, QuarterNumber.Q3),
                    new CalendarQuarter(2017, QuarterNumber.Q4),
                    new CalendarQuarter(2018, QuarterNumber.Q1),
                    new CalendarQuarter(2018, QuarterNumber.Q2),
                },
                new[]
                {
                    new CalendarQuarter(2017, QuarterNumber.Q2),
                    new CalendarQuarter(2017, QuarterNumber.Q3),
                    new CalendarQuarter(2017, QuarterNumber.Q4),
                    new CalendarQuarter(2018, QuarterNumber.Q1),
                },
                new[]
                {
                    new CalendarQuarter(2017, QuarterNumber.Q1),
                    new CalendarQuarter(2017, QuarterNumber.Q2),
                    new CalendarQuarter(2017, QuarterNumber.Q3),
                    new CalendarQuarter(2017, QuarterNumber.Q4),
                },
                new[]
                {
                    new CalendarQuarter(2017, QuarterNumber.Q1),
                    new CalendarQuarter(2017, QuarterNumber.Q2),
                    new CalendarQuarter(2017, QuarterNumber.Q3),
                    new CalendarQuarter(2017, QuarterNumber.Q4),
                    new CalendarQuarter(2018, QuarterNumber.Q1),
                },
                new[]
                {
                    new CalendarQuarter(2017, QuarterNumber.Q1),
                    new CalendarQuarter(2017, QuarterNumber.Q2),
                    new CalendarQuarter(2017, QuarterNumber.Q3),
                    new CalendarQuarter(2017, QuarterNumber.Q4),
                    new CalendarQuarter(2018, QuarterNumber.Q1),
                },
                new CalendarQuarter[]
                {
                },
            };

            // Act
            var actual = reportingPeriods.Select(_ => _.Split(UnitOfTimeGranularity.Quarter, OverflowStrategy.DiscardOverflow)).ToArray();

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_CalendarQuarter___When_overflowStrategy_is_DiscardOverflow_and_reportingPeriod_is_in_CalendarMonth_and_granularity_is_Quarter()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.January), new CalendarMonth(2017, MonthOfYear.March)),
                    ExpectedUnitsOfTime = new[] { new CalendarQuarter(2017, QuarterNumber.Q1) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.April), new CalendarMonth(2017, MonthOfYear.June)),
                    ExpectedUnitsOfTime = new[] { new CalendarQuarter(2017, QuarterNumber.Q2) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.July), new CalendarMonth(2017, MonthOfYear.September)),
                    ExpectedUnitsOfTime = new[] { new CalendarQuarter(2017, QuarterNumber.Q3) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.October), new CalendarMonth(2017, MonthOfYear.December)),
                    ExpectedUnitsOfTime = new[] { new CalendarQuarter(2017, QuarterNumber.Q4) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.January), new CalendarMonth(2018, MonthOfYear.March)),
                    ExpectedUnitsOfTime = new[] { new CalendarQuarter(2017, QuarterNumber.Q1), new CalendarQuarter(2017, QuarterNumber.Q2), new CalendarQuarter(2017, QuarterNumber.Q3), new CalendarQuarter(2017, QuarterNumber.Q4), new CalendarQuarter(2018, QuarterNumber.Q1) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.April), new CalendarMonth(2018, MonthOfYear.June)),
                    ExpectedUnitsOfTime = new[] { new CalendarQuarter(2017, QuarterNumber.Q2), new CalendarQuarter(2017, QuarterNumber.Q3), new CalendarQuarter(2017, QuarterNumber.Q4), new CalendarQuarter(2018, QuarterNumber.Q1), new CalendarQuarter(2018, QuarterNumber.Q2) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.July), new CalendarMonth(2018, MonthOfYear.September)),
                    ExpectedUnitsOfTime = new[] { new CalendarQuarter(2017, QuarterNumber.Q3), new CalendarQuarter(2017, QuarterNumber.Q4), new CalendarQuarter(2018, QuarterNumber.Q1), new CalendarQuarter(2018, QuarterNumber.Q2), new CalendarQuarter(2018, QuarterNumber.Q3) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.October), new CalendarMonth(2018, MonthOfYear.December)),
                    ExpectedUnitsOfTime = new[] { new CalendarQuarter(2017, QuarterNumber.Q4), new CalendarQuarter(2018, QuarterNumber.Q1), new CalendarQuarter(2018, QuarterNumber.Q2), new CalendarQuarter(2018, QuarterNumber.Q3), new CalendarQuarter(2018, QuarterNumber.Q4) },
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Quarter, OverflowStrategy.DiscardOverflow), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_CalendarMonth___When_overflowStrategy_is_DiscardOverflow_and_reportingPeriod_is_in_CalendarMonth_and_granularity_is_Month()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.November), new CalendarMonth(2018, MonthOfYear.March)),
                    ExpectedUnitsOfTime = new[] { new CalendarMonth(2017, MonthOfYear.November), new CalendarMonth(2017, MonthOfYear.December), new CalendarMonth(2018, MonthOfYear.January), new CalendarMonth(2018, MonthOfYear.February), new CalendarMonth(2018, MonthOfYear.March) },
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Month, OverflowStrategy.DiscardOverflow), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_CalendarDay___When_overflowStrategy_is_DiscardOverflow_and_reportingPeriod_is_in_CalendarMonth_and_granularity_is_Day()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.November), new CalendarMonth(2017, MonthOfYear.November)),
                    ExpectedUnitsOfTimeBeginning = new[] { new CalendarDay(2017, MonthOfYear.November, DayOfMonth.One), new CalendarDay(2017, MonthOfYear.November, DayOfMonth.Two), new CalendarDay(2017, MonthOfYear.November, DayOfMonth.Three) },
                    ExpectedUnitsOfTimeEnd = new[] { new CalendarDay(2017, MonthOfYear.November, DayOfMonth.TwentyEight), new CalendarDay(2017, MonthOfYear.November, DayOfMonth.TwentyNine), new CalendarDay(2017, MonthOfYear.November, DayOfMonth.Thirty) },
                    ExpectedCount = 30,
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarMonth(2017, MonthOfYear.November), new CalendarMonth(2018, MonthOfYear.November)),
                    ExpectedUnitsOfTimeBeginning = new[] { new CalendarDay(2017, MonthOfYear.November, DayOfMonth.One), new CalendarDay(2017, MonthOfYear.November, DayOfMonth.Two), new CalendarDay(2017, MonthOfYear.November, DayOfMonth.Three) },
                    ExpectedUnitsOfTimeEnd = new[] { new CalendarDay(2018, MonthOfYear.November, DayOfMonth.TwentyEight), new CalendarDay(2018, MonthOfYear.November, DayOfMonth.TwentyNine), new CalendarDay(2018, MonthOfYear.November, DayOfMonth.Thirty) },
                    ExpectedCount = 365 + 30,
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Day, OverflowStrategy.DiscardOverflow), ExpectedBeginning = _.ExpectedUnitsOfTimeBeginning, ExpectedEnd = _.ExpectedUnitsOfTimeEnd, _.ExpectedCount }).ToList();

            // Assert
            results.ForEach(_ =>
            {
                _.Actual.Should().StartWith(_.ExpectedBeginning);
                _.Actual.Should().EndWith(_.ExpectedEnd);
                _.Actual.Should().HaveCount(_.ExpectedCount);
            });
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_CalendarYear___When_overflowStrategy_is_DiscardOverflow_and_reportingPeriod_is_in_CalendarDay_and_granularity_is_Year_and_there_is_overflow()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new ReportingPeriod(new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Two), new CalendarDay(2018, MonthOfYear.December, DayOfMonth.Thirty)),
                new ReportingPeriod(new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Two), new CalendarDay(2018, MonthOfYear.December, DayOfMonth.ThirtyOne)),
                new ReportingPeriod(new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One), new CalendarDay(2018, MonthOfYear.December, DayOfMonth.Thirty)),
                new ReportingPeriod(new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One), new CalendarDay(2018, MonthOfYear.December, DayOfMonth.ThirtyOne)),
            };

            var expected = new IReadOnlyList<UnitOfTime>[]
            {
                new CalendarYear[]
                {
                },
                new[]
                {
                    new CalendarYear(2018),
                },
                new[]
                {
                    new CalendarYear(2017),
                },
                new[]
                {
                    new CalendarYear(2017),
                    new CalendarYear(2018),
                },
            };

            // Act
            var actual = reportingPeriods.Select(_ => _.Split(UnitOfTimeGranularity.Year, OverflowStrategy.DiscardOverflow)).ToArray();

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_CalendarYear___When_overflowStrategy_is_DiscardOverflow_and_reportingPeriod_is_in_CalendarDay_and_granularity_is_Year()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One), new CalendarDay(2018, MonthOfYear.December, DayOfMonth.ThirtyOne)),
                    ExpectedUnitsOfTime = new[] { new CalendarYear(2017), new CalendarYear(2018) },
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Year, OverflowStrategy.DiscardOverflow), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_CalendarQuarter_discarding_overflow___When_overflowStrategy_is_DiscardOverflow_and_reportingPeriod_is_in_CalendarDay_and_granularity_is_Quarter_and_there_is_overflow()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new ReportingPeriod(new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One), new CalendarDay(2018, MonthOfYear.March, DayOfMonth.Thirty)),
                new ReportingPeriod(new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Two), new CalendarDay(2018, MonthOfYear.March, DayOfMonth.ThirtyOne)),
                new ReportingPeriod(new CalendarDay(2017, MonthOfYear.April, DayOfMonth.One), new CalendarDay(2018, MonthOfYear.June, DayOfMonth.TwentyNine)),
                new ReportingPeriod(new CalendarDay(2017, MonthOfYear.April, DayOfMonth.Two), new CalendarDay(2018, MonthOfYear.June, DayOfMonth.Thirty)),
                new ReportingPeriod(new CalendarDay(2017, MonthOfYear.April, DayOfMonth.Two), new CalendarDay(2017, MonthOfYear.June, DayOfMonth.TwentyNine)),
                new ReportingPeriod(new CalendarDay(2017, MonthOfYear.April, DayOfMonth.Two), new CalendarDay(2017, MonthOfYear.August, DayOfMonth.ThirtyOne)),
            };

            var expected = new IReadOnlyList<UnitOfTime>[]
            {
                new[]
                {
                    new CalendarQuarter(2017, QuarterNumber.Q1),
                    new CalendarQuarter(2017, QuarterNumber.Q2),
                    new CalendarQuarter(2017, QuarterNumber.Q3),
                    new CalendarQuarter(2017, QuarterNumber.Q4),
                },
                new[]
                {
                    new CalendarQuarter(2017, QuarterNumber.Q2),
                    new CalendarQuarter(2017, QuarterNumber.Q3),
                    new CalendarQuarter(2017, QuarterNumber.Q4),
                    new CalendarQuarter(2018, QuarterNumber.Q1),
                },
                new[]
                {
                    new CalendarQuarter(2017, QuarterNumber.Q2),
                    new CalendarQuarter(2017, QuarterNumber.Q3),
                    new CalendarQuarter(2017, QuarterNumber.Q4),
                    new CalendarQuarter(2018, QuarterNumber.Q1),
                },
                new[]
                {
                    new CalendarQuarter(2017, QuarterNumber.Q3),
                    new CalendarQuarter(2017, QuarterNumber.Q4),
                    new CalendarQuarter(2018, QuarterNumber.Q1),
                    new CalendarQuarter(2018, QuarterNumber.Q2),
                },
                new CalendarQuarter[]
                {
                },
                new CalendarQuarter[]
                {
                },
            };

            // Act
            var actual = reportingPeriods.Select(_ => _.Split(UnitOfTimeGranularity.Quarter, OverflowStrategy.DiscardOverflow)).ToArray();

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_CalendarQuarter___When_overflowStrategy_is_DiscardOverflow_and_reportingPeriod_is_in_CalendarDay_and_granularity_is_Quarter()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One), new CalendarDay(2017, MonthOfYear.March, DayOfMonth.ThirtyOne)),
                    ExpectedUnitsOfTime = new[] { new CalendarQuarter(2017, QuarterNumber.Q1) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarDay(2017, MonthOfYear.April, DayOfMonth.One), new CalendarDay(2017, MonthOfYear.June, DayOfMonth.Thirty)),
                    ExpectedUnitsOfTime = new[] { new CalendarQuarter(2017, QuarterNumber.Q2) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarDay(2017, MonthOfYear.July, DayOfMonth.One), new CalendarDay(2017, MonthOfYear.September, DayOfMonth.Thirty)),
                    ExpectedUnitsOfTime = new[] { new CalendarQuarter(2017, QuarterNumber.Q3) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarDay(2017, MonthOfYear.October, DayOfMonth.One), new CalendarDay(2017, MonthOfYear.December, DayOfMonth.ThirtyOne)),
                    ExpectedUnitsOfTime = new[] { new CalendarQuarter(2017, QuarterNumber.Q4) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One), new CalendarDay(2018, MonthOfYear.March, DayOfMonth.ThirtyOne)),
                    ExpectedUnitsOfTime = new[] { new CalendarQuarter(2017, QuarterNumber.Q1), new CalendarQuarter(2017, QuarterNumber.Q2), new CalendarQuarter(2017, QuarterNumber.Q3), new CalendarQuarter(2017, QuarterNumber.Q4), new CalendarQuarter(2018, QuarterNumber.Q1) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarDay(2017, MonthOfYear.April, DayOfMonth.One), new CalendarDay(2018, MonthOfYear.June, DayOfMonth.Thirty)),
                    ExpectedUnitsOfTime = new[] { new CalendarQuarter(2017, QuarterNumber.Q2), new CalendarQuarter(2017, QuarterNumber.Q3), new CalendarQuarter(2017, QuarterNumber.Q4), new CalendarQuarter(2018, QuarterNumber.Q1), new CalendarQuarter(2018, QuarterNumber.Q2) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarDay(2017, MonthOfYear.July, DayOfMonth.One), new CalendarDay(2018, MonthOfYear.September, DayOfMonth.Thirty)),
                    ExpectedUnitsOfTime = new[] { new CalendarQuarter(2017, QuarterNumber.Q3), new CalendarQuarter(2017, QuarterNumber.Q4), new CalendarQuarter(2018, QuarterNumber.Q1), new CalendarQuarter(2018, QuarterNumber.Q2), new CalendarQuarter(2018, QuarterNumber.Q3) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarDay(2017, MonthOfYear.October, DayOfMonth.One), new CalendarDay(2018, MonthOfYear.December, DayOfMonth.ThirtyOne)),
                    ExpectedUnitsOfTime = new[] { new CalendarQuarter(2017, QuarterNumber.Q4), new CalendarQuarter(2018, QuarterNumber.Q1), new CalendarQuarter(2018, QuarterNumber.Q2), new CalendarQuarter(2018, QuarterNumber.Q3), new CalendarQuarter(2018, QuarterNumber.Q4) },
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Quarter, OverflowStrategy.DiscardOverflow), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_CalendarMonth_discarding_overflow___When_overflowStrategy_is_DiscardOverflow_and_reportingPeriod_is_in_CalendarDay_and_granularity_is_Month_and_there_is_overflow()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new ReportingPeriod(new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One), new CalendarDay(2018, MonthOfYear.January, DayOfMonth.Thirty)),
                new ReportingPeriod(new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Two), new CalendarDay(2018, MonthOfYear.January, DayOfMonth.ThirtyOne)),
                new ReportingPeriod(new CalendarDay(2017, MonthOfYear.February, DayOfMonth.One), new CalendarDay(2018, MonthOfYear.January, DayOfMonth.ThirtyOne)),
                new ReportingPeriod(new CalendarDay(2017, MonthOfYear.February, DayOfMonth.Two), new CalendarDay(2017, MonthOfYear.March, DayOfMonth.Eight)),
                new ReportingPeriod(new CalendarDay(2017, MonthOfYear.February, DayOfMonth.Two), new CalendarDay(2017, MonthOfYear.February, DayOfMonth.Eight)),
            };

            var expected = new IReadOnlyList<UnitOfTime>[]
            {
                new[]
                {
                    new CalendarMonth(2017, MonthOfYear.January),
                    new CalendarMonth(2017, MonthOfYear.February),
                    new CalendarMonth(2017, MonthOfYear.March),
                    new CalendarMonth(2017, MonthOfYear.April),
                    new CalendarMonth(2017, MonthOfYear.May),
                    new CalendarMonth(2017, MonthOfYear.June),
                    new CalendarMonth(2017, MonthOfYear.July),
                    new CalendarMonth(2017, MonthOfYear.August),
                    new CalendarMonth(2017, MonthOfYear.September),
                    new CalendarMonth(2017, MonthOfYear.October),
                    new CalendarMonth(2017, MonthOfYear.November),
                    new CalendarMonth(2017, MonthOfYear.December),
                },
                new[]
                {
                    new CalendarMonth(2017, MonthOfYear.February),
                    new CalendarMonth(2017, MonthOfYear.March),
                    new CalendarMonth(2017, MonthOfYear.April),
                    new CalendarMonth(2017, MonthOfYear.May),
                    new CalendarMonth(2017, MonthOfYear.June),
                    new CalendarMonth(2017, MonthOfYear.July),
                    new CalendarMonth(2017, MonthOfYear.August),
                    new CalendarMonth(2017, MonthOfYear.September),
                    new CalendarMonth(2017, MonthOfYear.October),
                    new CalendarMonth(2017, MonthOfYear.November),
                    new CalendarMonth(2017, MonthOfYear.December),
                    new CalendarMonth(2018, MonthOfYear.January),
                },
                new[]
                {
                    new CalendarMonth(2017, MonthOfYear.February),
                    new CalendarMonth(2017, MonthOfYear.March),
                    new CalendarMonth(2017, MonthOfYear.April),
                    new CalendarMonth(2017, MonthOfYear.May),
                    new CalendarMonth(2017, MonthOfYear.June),
                    new CalendarMonth(2017, MonthOfYear.July),
                    new CalendarMonth(2017, MonthOfYear.August),
                    new CalendarMonth(2017, MonthOfYear.September),
                    new CalendarMonth(2017, MonthOfYear.October),
                    new CalendarMonth(2017, MonthOfYear.November),
                    new CalendarMonth(2017, MonthOfYear.December),
                    new CalendarMonth(2018, MonthOfYear.January),
                },
                new CalendarMonth[]
                {
                },
                new CalendarMonth[]
                {
                },
            };

            // Act
            var actual = reportingPeriods.Select(_ => _.Split(UnitOfTimeGranularity.Month, OverflowStrategy.DiscardOverflow)).ToArray();

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_CalendarMonth___When_overflowStrategy_is_DiscardOverflow_and_reportingPeriod_is_in_CalendarDay_and_granularity_is_Month()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One), new CalendarDay(2017, MonthOfYear.January, DayOfMonth.ThirtyOne)),
                    ExpectedUnitsOfTime = new[] { new CalendarMonth(2017, MonthOfYear.January) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarDay(2017, MonthOfYear.December, DayOfMonth.One), new CalendarDay(2018, MonthOfYear.January, DayOfMonth.ThirtyOne)),
                    ExpectedUnitsOfTime = new[] { new CalendarMonth(2017, MonthOfYear.December), new CalendarMonth(2018, MonthOfYear.January) },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarDay(2017, MonthOfYear.November, DayOfMonth.One), new CalendarDay(2018, MonthOfYear.February, DayOfMonth.TwentyEight)),
                    ExpectedUnitsOfTime = new[] { new CalendarMonth(2017, MonthOfYear.November), new CalendarMonth(2017, MonthOfYear.December), new CalendarMonth(2018, MonthOfYear.January), new CalendarMonth(2018, MonthOfYear.February) },
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Month, OverflowStrategy.DiscardOverflow), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_CalendarDay___When_overflowStrategy_is_DiscardOverflow_and_reportingPeriod_is_in_CalendarDay_and_granularity_is_Day()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Two), new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Two)),
                    ExpectedUnitsOfTimeBeginning = new[] { new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Two),  },
                    ExpectedUnitsOfTimeEnd = new[] { new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Two) },
                    ExpectedCount = 1,
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Two), new CalendarDay(2017, MonthOfYear.March, DayOfMonth.Ten)),
                    ExpectedUnitsOfTimeBeginning = new[] { new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Two), new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Three), new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Four), },
                    ExpectedUnitsOfTimeEnd = new[] { new CalendarDay(2017, MonthOfYear.March, DayOfMonth.Eight), new CalendarDay(2017, MonthOfYear.March, DayOfMonth.Nine), new CalendarDay(2017, MonthOfYear.March, DayOfMonth.Ten) },
                    ExpectedCount = 30 + 28 + 10,
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Day, OverflowStrategy.DiscardOverflow), ExpectedBeginning = _.ExpectedUnitsOfTimeBeginning, ExpectedEnd = _.ExpectedUnitsOfTimeEnd, _.ExpectedCount }).ToList();

            // Assert
            results.ForEach(_ =>
            {
                _.Actual.Should().StartWith(_.ExpectedBeginning);
                _.Actual.Should().EndWith(_.ExpectedEnd);
                _.Actual.Should().HaveCount(_.ExpectedCount);
            });
        }

        [Fact]
        public static void Split___Should_throw_ArgumentException___When_overflowStrategy_is_DiscardOverflow_and_reportingPeriod_is_in_FiscalYear_and_granularity_is_Day()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new ReportingPeriod(new FiscalYear(2017), new FiscalYear(2017)),
                new ReportingPeriod(new FiscalYear(2017), new FiscalYear(2018)),
            };

            // Act
            var exceptions = reportingPeriods.Select(_ => Record.Exception(() => _.Split(UnitOfTimeGranularity.Day, OverflowStrategy.DiscardOverflow))).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<ArgumentException>());
        }

        [Fact]
        public static void Split___Should_throw_ArgumentException___When_overflowStrategy_is_DiscardOverflow_and_reportingPeriod_is_in_FiscalQuarter_and_granularity_is_Day()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new ReportingPeriod(new FiscalQuarter(2017, QuarterNumber.Q1), new FiscalQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod(new FiscalQuarter(2017, QuarterNumber.Q3), new FiscalQuarter(2018, QuarterNumber.Q2)),
            };

            // Act
            var exceptions = reportingPeriods.Select(_ => Record.Exception(() => _.Split(UnitOfTimeGranularity.Day, OverflowStrategy.DiscardOverflow))).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<ArgumentException>());
        }

        [Fact]
        public static void Split___Should_throw_ArgumentException___When_overflowStrategy_is_DiscardOverflow_and_reportingPeriod_is_in_FiscalMonth_and_granularity_is_Day()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new ReportingPeriod(new FiscalMonth(2017, MonthNumber.Eleven), new FiscalMonth(2018, MonthNumber.Three)),
            };

            // Act
            var exceptions = reportingPeriods.Select(_ => Record.Exception(() => _.Split(UnitOfTimeGranularity.Day, OverflowStrategy.DiscardOverflow))).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<ArgumentException>());
        }

        [Fact]
        public static void Split___Should_throw_ArgumentException___When_overflowStrategy_is_DiscardOverflow_and_reportingPeriod_is_in_GenericYear_and_granularity_is_Day()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new ReportingPeriod(new GenericYear(2017), new GenericYear(2017)),
                new ReportingPeriod(new GenericYear(2017), new GenericYear(2018)),
            };

            // Act
            var exceptions = reportingPeriods.Select(_ => Record.Exception(() => _.Split(UnitOfTimeGranularity.Day, OverflowStrategy.DiscardOverflow))).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<ArgumentException>());
        }

        [Fact]
        public static void Split___Should_throw_ArgumentException___When_overflowStrategy_is_DiscardOverflow_and_reportingPeriod_is_in_GenericQuarter_and_granularity_is_Day()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new ReportingPeriod(new GenericQuarter(2017, QuarterNumber.Q1), new GenericQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod(new GenericQuarter(2017, QuarterNumber.Q3), new GenericQuarter(2018, QuarterNumber.Q2)),
            };

            // Act
            var exceptions = reportingPeriods.Select(_ => Record.Exception(() => _.Split(UnitOfTimeGranularity.Day, OverflowStrategy.DiscardOverflow))).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<ArgumentException>());
        }

        [Fact]
        public static void Split___Should_throw_ArgumentException___When_overflowStrategy_is_DiscardOverflow_and_reportingPeriod_is_in_GenericMonth_and_granularity_is_Day()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new ReportingPeriod(new GenericMonth(2017, MonthNumber.Eleven), new GenericMonth(2018, MonthNumber.Three)),
            };

            // Act
            var exceptions = reportingPeriods.Select(_ => Record.Exception(() => _.Split(UnitOfTimeGranularity.Day, OverflowStrategy.DiscardOverflow))).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<ArgumentException>());
        }

        [Fact]
        public static void ToMostGranular___Should_throw_ArgumentNullException___When_parameter_reportingPeriod_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.ToMostGranular(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void ToMostGranular___Should_return_clone_of_reportingPeriod___When_both_components_are_Unbounded()
        {
            // Arrange
            var expected = A.Dummy<ReportingPeriod>().Whose(_ => (_.Start.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded) && (_.End.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded));

            // Act
            var actual = expected.ToMostGranular();

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public static void ToMostGranular___Should_return_a_reporting_period_whose_Start_is_the_most_granular_reportingPeriod_Start_and_whose_End_is_the_most_granular_reportingPeriod_End()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod>();
            var mostGranularStart = reportingPeriod.Start.ToMostGranular();
            var mostGranularEnd = reportingPeriod.End.ToMostGranular();
            var expectedReportingPeriod = new ReportingPeriod(mostGranularStart.Start, mostGranularEnd.End);

            // Act
            var actualReportingPeriod = reportingPeriod.ToMostGranular();

            // Assert
            actualReportingPeriod.Should().Be(expectedReportingPeriod);
        }

        [Fact]
        public static void ToMostGranular___Should_return_the_most_granular_possible_but_equivalent_reporting_period___When_called()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Two),
                        new CalendarDay(2017, MonthOfYear.December, DayOfMonth.Ten)),
                    MostGranular = new ReportingPeriod(
                        new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Two),
                        new CalendarDay(2017, MonthOfYear.December, DayOfMonth.Ten)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Two),
                        new CalendarUnbounded()),
                    MostGranular = new ReportingPeriod(
                        new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Two),
                        new CalendarUnbounded()),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Two)),
                    MostGranular = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Two)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarDay(2017, MonthOfYear.February, DayOfMonth.One),
                        new CalendarDay(2017, MonthOfYear.February, DayOfMonth.TwentyEight)),
                    MostGranular = new ReportingPeriod(
                        new CalendarDay(2017, MonthOfYear.February, DayOfMonth.One),
                        new CalendarDay(2017, MonthOfYear.February, DayOfMonth.TwentyEight)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarMonth(2017, MonthOfYear.February),
                        new CalendarMonth(2018, MonthOfYear.March)),
                    MostGranular = new ReportingPeriod(
                        new CalendarDay(2017, MonthOfYear.February, DayOfMonth.One),
                        new CalendarDay(2018, MonthOfYear.March, DayOfMonth.ThirtyOne)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarMonth(2017, MonthOfYear.February),
                        new CalendarUnbounded()),
                    MostGranular = new ReportingPeriod(
                        new CalendarDay(2017, MonthOfYear.February, DayOfMonth.One),
                        new CalendarUnbounded()),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarMonth(2018, MonthOfYear.March)),
                    MostGranular = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarDay(2018, MonthOfYear.March, DayOfMonth.ThirtyOne)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarMonth(2017, MonthOfYear.January),
                        new CalendarMonth(2017, MonthOfYear.March)),
                    MostGranular = new ReportingPeriod(
                        new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One),
                        new CalendarDay(2017, MonthOfYear.March, DayOfMonth.ThirtyOne)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarQuarter(2017, QuarterNumber.Q2),
                        new CalendarQuarter(2017, QuarterNumber.Q2)),
                    MostGranular = new ReportingPeriod(
                        new CalendarDay(2017, MonthOfYear.April, DayOfMonth.One),
                        new CalendarDay(2017, MonthOfYear.June, DayOfMonth.Thirty)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarQuarter(2017, QuarterNumber.Q2),
                        new CalendarUnbounded()),
                    MostGranular = new ReportingPeriod(
                        new CalendarDay(2017, MonthOfYear.April, DayOfMonth.One),
                        new CalendarUnbounded()),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarQuarter(2017, QuarterNumber.Q2)),
                    MostGranular = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarDay(2017, MonthOfYear.June, DayOfMonth.Thirty)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarQuarter(2017, QuarterNumber.Q1),
                        new CalendarQuarter(2017, QuarterNumber.Q4)),
                    MostGranular = new ReportingPeriod(
                        new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One),
                        new CalendarDay(2017, MonthOfYear.December, DayOfMonth.ThirtyOne)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarQuarter(2017, QuarterNumber.Q2),
                        new CalendarQuarter(2018, QuarterNumber.Q3)),
                    MostGranular = new ReportingPeriod(
                        new CalendarDay(2017, MonthOfYear.April, DayOfMonth.One),
                        new CalendarDay(2018, MonthOfYear.September, DayOfMonth.Thirty)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarYear(2017),
                        new CalendarYear(2017)),
                    MostGranular = new ReportingPeriod(
                        new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One),
                        new CalendarDay(2017, MonthOfYear.December, DayOfMonth.ThirtyOne)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarYear(2017),
                        new CalendarUnbounded()),
                    MostGranular = new ReportingPeriod(
                        new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One),
                        new CalendarUnbounded()),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarYear(2017)),
                    MostGranular = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarDay(2017, MonthOfYear.December, DayOfMonth.ThirtyOne)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarYear(2017),
                        new CalendarYear(2018)),
                    MostGranular = new ReportingPeriod(
                        new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One),
                        new CalendarDay(2018, MonthOfYear.December, DayOfMonth.ThirtyOne)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalMonth(2017, MonthNumber.Two),
                        new FiscalMonth(2018, MonthNumber.Three)),
                    MostGranular = new ReportingPeriod(
                        new FiscalMonth(2017, MonthNumber.Two),
                        new FiscalMonth(2018, MonthNumber.Three)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalMonth(2017, MonthNumber.Two),
                        new FiscalUnbounded()),
                    MostGranular = new ReportingPeriod(
                        new FiscalMonth(2017, MonthNumber.Two),
                        new FiscalUnbounded()),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalUnbounded(),
                        new FiscalMonth(2018, MonthNumber.Three)),
                    MostGranular = new ReportingPeriod(
                        new FiscalUnbounded(),
                        new FiscalMonth(2018, MonthNumber.Three)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalMonth(2017, MonthNumber.Four),
                        new FiscalMonth(2018, MonthNumber.Six)),
                    MostGranular = new ReportingPeriod(
                        new FiscalMonth(2017, MonthNumber.Four),
                        new FiscalMonth(2018, MonthNumber.Six)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalQuarter(2017, QuarterNumber.Q2),
                        new FiscalQuarter(2018, QuarterNumber.Q2)),
                    MostGranular = new ReportingPeriod(
                        new FiscalMonth(2017, MonthNumber.Four),
                        new FiscalMonth(2018, MonthNumber.Six)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalQuarter(2017, QuarterNumber.Q2),
                        new FiscalUnbounded()),
                    MostGranular = new ReportingPeriod(
                        new FiscalMonth(2017, MonthNumber.Four),
                        new FiscalUnbounded()),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalUnbounded(),
                        new FiscalQuarter(2017, QuarterNumber.Q2)),
                    MostGranular = new ReportingPeriod(
                        new FiscalUnbounded(),
                        new FiscalMonth(2017, MonthNumber.Six)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalQuarter(2017, QuarterNumber.Q1),
                        new FiscalQuarter(2017, QuarterNumber.Q4)),
                    MostGranular = new ReportingPeriod(
                        new FiscalMonth(2017, MonthNumber.One),
                        new FiscalMonth(2017, MonthNumber.Twelve)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalYear(2017),
                        new FiscalYear(2017)),
                    MostGranular = new ReportingPeriod(
                        new FiscalMonth(2017, MonthNumber.One),
                        new FiscalMonth(2017, MonthNumber.Twelve)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalYear(2017),
                        new FiscalUnbounded()),
                    MostGranular = new ReportingPeriod(
                        new FiscalMonth(2017, MonthNumber.One),
                        new FiscalUnbounded()),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalUnbounded(),
                        new FiscalYear(2017)),
                    MostGranular = new ReportingPeriod(
                        new FiscalUnbounded(),
                        new FiscalMonth(2017, MonthNumber.Twelve)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalYear(2017),
                        new FiscalYear(2018)),
                    MostGranular = new ReportingPeriod(
                        new FiscalMonth(2017, MonthNumber.One),
                        new FiscalMonth(2018, MonthNumber.Twelve)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericMonth(2017, MonthNumber.Two),
                        new GenericMonth(2018, MonthNumber.Three)),
                    MostGranular = new ReportingPeriod(
                        new GenericMonth(2017, MonthNumber.Two),
                        new GenericMonth(2018, MonthNumber.Three)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericMonth(2017, MonthNumber.Two),
                        new GenericUnbounded()),
                    MostGranular = new ReportingPeriod(
                        new GenericMonth(2017, MonthNumber.Two),
                        new GenericUnbounded()),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericUnbounded(),
                        new GenericMonth(2018, MonthNumber.Three)),
                    MostGranular = new ReportingPeriod(
                        new GenericUnbounded(),
                        new GenericMonth(2018, MonthNumber.Three)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericMonth(2017, MonthNumber.Four),
                        new GenericMonth(2018, MonthNumber.Six)),
                    MostGranular = new ReportingPeriod(
                        new GenericMonth(2017, MonthNumber.Four),
                        new GenericMonth(2018, MonthNumber.Six)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericQuarter(2017, QuarterNumber.Q2),
                        new GenericQuarter(2018, QuarterNumber.Q2)),
                    MostGranular = new ReportingPeriod(
                        new GenericMonth(2017, MonthNumber.Four),
                        new GenericMonth(2018, MonthNumber.Six)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericQuarter(2017, QuarterNumber.Q2),
                        new GenericUnbounded()),
                    MostGranular = new ReportingPeriod(
                        new GenericMonth(2017, MonthNumber.Four),
                        new GenericUnbounded()),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericUnbounded(),
                        new GenericQuarter(2017, QuarterNumber.Q2)),
                    MostGranular = new ReportingPeriod(
                        new GenericUnbounded(),
                        new GenericMonth(2017, MonthNumber.Six)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericQuarter(2017, QuarterNumber.Q1),
                        new GenericQuarter(2017, QuarterNumber.Q4)),
                    MostGranular = new ReportingPeriod(
                        new GenericMonth(2017, MonthNumber.One),
                        new GenericMonth(2017, MonthNumber.Twelve)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericYear(2017),
                        new GenericYear(2017)),
                    MostGranular = new ReportingPeriod(
                        new GenericMonth(2017, MonthNumber.One),
                        new GenericMonth(2017, MonthNumber.Twelve)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericYear(2017),
                        new GenericUnbounded()),
                    MostGranular = new ReportingPeriod(
                        new GenericMonth(2017, MonthNumber.One),
                        new GenericUnbounded()),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericUnbounded(),
                        new GenericYear(2017)),
                    MostGranular = new ReportingPeriod(
                        new GenericUnbounded(),
                        new GenericMonth(2017, MonthNumber.Twelve)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericYear(2017),
                        new GenericYear(2018)),
                    MostGranular = new ReportingPeriod(
                        new GenericMonth(2017, MonthNumber.One),
                        new GenericMonth(2018, MonthNumber.Twelve)),
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Expected = _.MostGranular, Actual = _.ReportingPeriod.ToMostGranular() }).ToList();

            // Assert
            results.ForEach(_ => _.Expected.Should().Be(_.Actual));
            results.ForEach(_ => _.Expected.Should().NotBeSameAs(_.Actual));
        }

        [Fact]
        public static void ToLeastGranular___Should_throw_ArgumentNullException___When_parameter_reportingPeriod_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.ToLeastGranular(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void ToLeastGranular___Should_return_parameter_reportingPeriod___When_both_components_are_Unbounded()
        {
            // Arrange
            var expected = A.Dummy<ReportingPeriod>().Whose(_ => (_.Start.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded) && (_.End.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded));

            // Act
            var actual = expected.ToLeastGranular();

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void ToLeastGranular___Should_return_the_least_granular_possible_but_equivalent_reporting_period___When_reporting_period_has_uniform_granularity()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Two),
                        new CalendarDay(2017, MonthOfYear.December, DayOfMonth.Ten)),
                    LeastGranular = new ReportingPeriod(
                        new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Two),
                        new CalendarDay(2017, MonthOfYear.December, DayOfMonth.Ten)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarDay(2017, MonthOfYear.February, DayOfMonth.One),
                        new CalendarDay(2017, MonthOfYear.February, DayOfMonth.TwentyEight)),
                    LeastGranular = new ReportingPeriod(
                        new CalendarMonth(2017, MonthOfYear.February),
                        new CalendarMonth(2017, MonthOfYear.February)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarDay(2017, MonthOfYear.February, DayOfMonth.One),
                        new CalendarDay(2018, MonthOfYear.March, DayOfMonth.ThirtyOne)),
                    LeastGranular = new ReportingPeriod(
                        new CalendarMonth(2017, MonthOfYear.February),
                        new CalendarMonth(2018, MonthOfYear.March)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarDay(2017, MonthOfYear.April, DayOfMonth.One),
                        new CalendarDay(2017, MonthOfYear.June, DayOfMonth.Thirty)),
                    LeastGranular = new ReportingPeriod(
                        new CalendarQuarter(2017, QuarterNumber.Q2),
                        new CalendarQuarter(2017, QuarterNumber.Q2)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarDay(2017, MonthOfYear.April, DayOfMonth.One),
                        new CalendarDay(2018, MonthOfYear.September, DayOfMonth.Thirty)),
                    LeastGranular = new ReportingPeriod(
                        new CalendarQuarter(2017, QuarterNumber.Q2),
                        new CalendarQuarter(2018, QuarterNumber.Q3)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One),
                        new CalendarDay(2017, MonthOfYear.December, DayOfMonth.ThirtyOne)),
                    LeastGranular = new ReportingPeriod(
                        new CalendarYear(2017),
                        new CalendarYear(2017)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One),
                        new CalendarDay(2018, MonthOfYear.December, DayOfMonth.ThirtyOne)),
                    LeastGranular = new ReportingPeriod(
                        new CalendarYear(2017),
                        new CalendarYear(2018)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarMonth(2017, MonthOfYear.February),
                        new CalendarMonth(2017, MonthOfYear.April)),
                    LeastGranular = new ReportingPeriod(
                        new CalendarMonth(2017, MonthOfYear.February),
                        new CalendarMonth(2017, MonthOfYear.April)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarMonth(2017, MonthOfYear.October),
                        new CalendarMonth(2017, MonthOfYear.December)),
                    LeastGranular = new ReportingPeriod(
                        new CalendarQuarter(2017, QuarterNumber.Q4),
                        new CalendarQuarter(2017, QuarterNumber.Q4)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarMonth(2017, MonthOfYear.October),
                        new CalendarMonth(2018, MonthOfYear.June)),
                    LeastGranular = new ReportingPeriod(
                        new CalendarQuarter(2017, QuarterNumber.Q4),
                        new CalendarQuarter(2018, QuarterNumber.Q2)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarMonth(2017, MonthOfYear.January),
                        new CalendarMonth(2017, MonthOfYear.December)),
                    LeastGranular = new ReportingPeriod(
                        new CalendarYear(2017),
                        new CalendarYear(2017)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarMonth(2017, MonthOfYear.January),
                        new CalendarMonth(2018, MonthOfYear.December)),
                    LeastGranular = new ReportingPeriod(
                        new CalendarYear(2017),
                        new CalendarYear(2018)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarQuarter(2017, QuarterNumber.Q2),
                        new CalendarQuarter(2018, QuarterNumber.Q4)),
                    LeastGranular = new ReportingPeriod(
                        new CalendarQuarter(2017, QuarterNumber.Q2),
                        new CalendarQuarter(2018, QuarterNumber.Q4)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarQuarter(2017, QuarterNumber.Q1),
                        new CalendarQuarter(2017, QuarterNumber.Q4)),
                    LeastGranular = new ReportingPeriod(
                        new CalendarYear(2017),
                        new CalendarYear(2017)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarQuarter(2017, QuarterNumber.Q1),
                        new CalendarQuarter(2018, QuarterNumber.Q4)),
                    LeastGranular = new ReportingPeriod(
                        new CalendarYear(2017),
                        new CalendarYear(2018)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarYear(2017),
                        new CalendarYear(2018)),
                    LeastGranular = new ReportingPeriod(
                        new CalendarYear(2017),
                        new CalendarYear(2018)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalMonth(2017, MonthNumber.Two),
                        new FiscalMonth(2017, MonthNumber.Four)),
                    LeastGranular = new ReportingPeriod(
                        new FiscalMonth(2017, MonthNumber.Two),
                        new FiscalMonth(2017, MonthNumber.Four)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalMonth(2017, MonthNumber.Ten),
                        new FiscalMonth(2017, MonthNumber.Twelve)),
                    LeastGranular = new ReportingPeriod(
                        new FiscalQuarter(2017, QuarterNumber.Q4),
                        new FiscalQuarter(2017, QuarterNumber.Q4)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalMonth(2017, MonthNumber.Ten),
                        new FiscalMonth(2018, MonthNumber.Six)),
                    LeastGranular = new ReportingPeriod(
                        new FiscalQuarter(2017, QuarterNumber.Q4),
                        new FiscalQuarter(2018, QuarterNumber.Q2)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalMonth(2017, MonthNumber.One),
                        new FiscalMonth(2017, MonthNumber.Twelve)),
                    LeastGranular = new ReportingPeriod(
                        new FiscalYear(2017),
                        new FiscalYear(2017)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalMonth(2017, MonthNumber.One),
                        new FiscalMonth(2018, MonthNumber.Twelve)),
                    LeastGranular = new ReportingPeriod(
                        new FiscalYear(2017),
                        new FiscalYear(2018)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalQuarter(2017, QuarterNumber.Q2),
                        new FiscalQuarter(2018, QuarterNumber.Q4)),
                    LeastGranular = new ReportingPeriod(
                        new FiscalQuarter(2017, QuarterNumber.Q2),
                        new FiscalQuarter(2018, QuarterNumber.Q4)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalQuarter(2017, QuarterNumber.Q1),
                        new FiscalQuarter(2017, QuarterNumber.Q4)),
                    LeastGranular = new ReportingPeriod(
                        new FiscalYear(2017),
                        new FiscalYear(2017)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalQuarter(2017, QuarterNumber.Q1),
                        new FiscalQuarter(2018, QuarterNumber.Q4)),
                    LeastGranular = new ReportingPeriod(
                        new FiscalYear(2017),
                        new FiscalYear(2018)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalYear(2017),
                        new FiscalYear(2018)),
                    LeastGranular = new ReportingPeriod(
                        new FiscalYear(2017),
                        new FiscalYear(2018)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericMonth(2017, MonthNumber.Two),
                        new GenericMonth(2017, MonthNumber.Four)),
                    LeastGranular = new ReportingPeriod(
                        new GenericMonth(2017, MonthNumber.Two),
                        new GenericMonth(2017, MonthNumber.Four)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericMonth(2017, MonthNumber.Ten),
                        new GenericMonth(2017, MonthNumber.Twelve)),
                    LeastGranular = new ReportingPeriod(
                        new GenericQuarter(2017, QuarterNumber.Q4),
                        new GenericQuarter(2017, QuarterNumber.Q4)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericMonth(2017, MonthNumber.Ten),
                        new GenericMonth(2018, MonthNumber.Six)),
                    LeastGranular = new ReportingPeriod(
                        new GenericQuarter(2017, QuarterNumber.Q4),
                        new GenericQuarter(2018, QuarterNumber.Q2)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericMonth(2017, MonthNumber.One),
                        new GenericMonth(2017, MonthNumber.Twelve)),
                    LeastGranular = new ReportingPeriod(
                        new GenericYear(2017),
                        new GenericYear(2017)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericMonth(2017, MonthNumber.One),
                        new GenericMonth(2018, MonthNumber.Twelve)),
                    LeastGranular = new ReportingPeriod(
                        new GenericYear(2017),
                        new GenericYear(2018)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericQuarter(2017, QuarterNumber.Q2),
                        new GenericQuarter(2018, QuarterNumber.Q4)),
                    LeastGranular = new ReportingPeriod(
                        new GenericQuarter(2017, QuarterNumber.Q2),
                        new GenericQuarter(2018, QuarterNumber.Q4)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericQuarter(2017, QuarterNumber.Q1),
                        new GenericQuarter(2017, QuarterNumber.Q4)),
                    LeastGranular = new ReportingPeriod(
                        new GenericYear(2017),
                        new GenericYear(2017)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericQuarter(2017, QuarterNumber.Q1),
                        new GenericQuarter(2018, QuarterNumber.Q4)),
                    LeastGranular = new ReportingPeriod(
                        new GenericYear(2017),
                        new GenericYear(2018)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericYear(2017),
                        new GenericYear(2018)),
                    LeastGranular = new ReportingPeriod(
                        new GenericYear(2017),
                        new GenericYear(2018)),
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Expected = _.LeastGranular, Actual = _.ReportingPeriod.ToLeastGranular() }).ToList();

            // Assert
            results.ForEach(_ => _.Expected.Should().Be(_.Actual));
            results.ForEach(_ => _.Expected.Should().NotBeSameAs(_.Actual));
        }

        [Fact]
        public static void ToLeastGranular___Should_return_the_least_granular_possible_but_equivalent_reporting_period___When_reporting_period_end_has_Unbounded_granularity()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarDay(2017, MonthOfYear.January, DayOfMonth.ThirtyOne),
                        new CalendarUnbounded()),
                    LeastGranular = new ReportingPeriod(
                        new CalendarDay(2017, MonthOfYear.January, DayOfMonth.ThirtyOne),
                        new CalendarUnbounded()),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarDay(2017, MonthOfYear.August, DayOfMonth.Fifteen),
                        new CalendarUnbounded()),
                    LeastGranular = new ReportingPeriod(
                        new CalendarDay(2017, MonthOfYear.August, DayOfMonth.Fifteen),
                        new CalendarUnbounded()),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarDay(2017, MonthOfYear.February, DayOfMonth.One),
                        new CalendarUnbounded()),
                    LeastGranular = new ReportingPeriod(
                        new CalendarMonth(2017, MonthOfYear.February),
                        new CalendarUnbounded()),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarDay(2017, MonthOfYear.July, DayOfMonth.One),
                        new CalendarUnbounded()),
                    LeastGranular = new ReportingPeriod(
                        new CalendarQuarter(2017, QuarterNumber.Q3),
                        new CalendarUnbounded()),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One),
                        new CalendarUnbounded()),
                    LeastGranular = new ReportingPeriod(
                        new CalendarYear(2017),
                        new CalendarUnbounded()),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarMonth(2017, MonthOfYear.February),
                        new CalendarUnbounded()),
                    LeastGranular = new ReportingPeriod(
                        new CalendarMonth(2017, MonthOfYear.February),
                        new CalendarUnbounded()),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarMonth(2017, MonthOfYear.June),
                        new CalendarUnbounded()),
                    LeastGranular = new ReportingPeriod(
                        new CalendarMonth(2017, MonthOfYear.June),
                        new CalendarUnbounded()),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarMonth(2017, MonthOfYear.April),
                        new CalendarUnbounded()),
                    LeastGranular = new ReportingPeriod(
                        new CalendarQuarter(2017, QuarterNumber.Q2),
                        new CalendarUnbounded()),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarMonth(2017, MonthOfYear.January),
                        new CalendarUnbounded()),
                    LeastGranular = new ReportingPeriod(
                        new CalendarYear(2017),
                        new CalendarUnbounded()),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarQuarter(2017, QuarterNumber.Q2),
                        new CalendarUnbounded()),
                    LeastGranular = new ReportingPeriod(
                        new CalendarQuarter(2017, QuarterNumber.Q2),
                        new CalendarUnbounded()),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarQuarter(2017, QuarterNumber.Q1),
                        new CalendarUnbounded()),
                    LeastGranular = new ReportingPeriod(
                        new CalendarYear(2017),
                        new CalendarUnbounded()),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarYear(2017),
                        new CalendarUnbounded()),
                    LeastGranular = new ReportingPeriod(
                        new CalendarYear(2017),
                        new CalendarUnbounded()),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalMonth(2017, MonthNumber.Two),
                        new FiscalUnbounded()),
                    LeastGranular = new ReportingPeriod(
                        new FiscalMonth(2017, MonthNumber.Two),
                        new FiscalUnbounded()),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalMonth(2017, MonthNumber.Six),
                        new FiscalUnbounded()),
                    LeastGranular = new ReportingPeriod(
                        new FiscalMonth(2017, MonthNumber.Six),
                        new FiscalUnbounded()),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalMonth(2017, MonthNumber.Four),
                        new FiscalUnbounded()),
                    LeastGranular = new ReportingPeriod(
                        new FiscalQuarter(2017, QuarterNumber.Q2),
                        new FiscalUnbounded()),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalMonth(2017, MonthNumber.One),
                        new FiscalUnbounded()),
                    LeastGranular = new ReportingPeriod(
                        new FiscalYear(2017),
                        new FiscalUnbounded()),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalQuarter(2017, QuarterNumber.Q2),
                        new FiscalUnbounded()),
                    LeastGranular = new ReportingPeriod(
                        new FiscalQuarter(2017, QuarterNumber.Q2),
                        new FiscalUnbounded()),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalQuarter(2017, QuarterNumber.Q1),
                        new FiscalUnbounded()),
                    LeastGranular = new ReportingPeriod(
                        new FiscalYear(2017),
                        new FiscalUnbounded()),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalYear(2017),
                        new FiscalUnbounded()),
                    LeastGranular = new ReportingPeriod(
                        new FiscalYear(2017),
                        new FiscalUnbounded()),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericMonth(2017, MonthNumber.Two),
                        new GenericUnbounded()),
                    LeastGranular = new ReportingPeriod(
                        new GenericMonth(2017, MonthNumber.Two),
                        new GenericUnbounded()),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericMonth(2017, MonthNumber.Six),
                        new GenericUnbounded()),
                    LeastGranular = new ReportingPeriod(
                        new GenericMonth(2017, MonthNumber.Six),
                        new GenericUnbounded()),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericMonth(2017, MonthNumber.Four),
                        new GenericUnbounded()),
                    LeastGranular = new ReportingPeriod(
                        new GenericQuarter(2017, QuarterNumber.Q2),
                        new GenericUnbounded()),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericMonth(2017, MonthNumber.One),
                        new GenericUnbounded()),
                    LeastGranular = new ReportingPeriod(
                        new GenericYear(2017),
                        new GenericUnbounded()),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericQuarter(2017, QuarterNumber.Q2),
                        new GenericUnbounded()),
                    LeastGranular = new ReportingPeriod(
                        new GenericQuarter(2017, QuarterNumber.Q2),
                        new GenericUnbounded()),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericQuarter(2017, QuarterNumber.Q1),
                        new GenericUnbounded()),
                    LeastGranular = new ReportingPeriod(
                        new GenericYear(2017),
                        new GenericUnbounded()),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericYear(2017),
                        new GenericUnbounded()),
                    LeastGranular = new ReportingPeriod(
                        new GenericYear(2017),
                        new GenericUnbounded()),
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Expected = _.LeastGranular, Actual = _.ReportingPeriod.ToLeastGranular() }).ToList();

            // Assert
            results.ForEach(_ => _.Expected.Should().Be(_.Actual));
            results.ForEach(_ => _.Expected.Should().NotBeSameAs(_.Actual));
        }

        [Fact]
        public static void ToLeastGranular___Should_return_the_least_granular_possible_but_equivalent_reporting_period___When_reporting_period_start_has_Unbounded_granularity()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One)),
                    LeastGranular = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarDay(2017, MonthOfYear.May, DayOfMonth.Fifteen)),
                    LeastGranular = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarDay(2017, MonthOfYear.May, DayOfMonth.Fifteen)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarDay(2017, MonthOfYear.May, DayOfMonth.ThirtyOne)),
                    LeastGranular = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarMonth(2017, MonthOfYear.May)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarDay(2017, MonthOfYear.June, DayOfMonth.Thirty)),
                    LeastGranular = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarQuarter(2017, QuarterNumber.Q2)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarDay(2017, MonthOfYear.December, DayOfMonth.ThirtyOne)),
                    LeastGranular = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarYear(2017)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarMonth(2017, MonthOfYear.April)),
                    LeastGranular = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarMonth(2017, MonthOfYear.April)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarMonth(2017, MonthOfYear.June)),
                    LeastGranular = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarQuarter(2017, QuarterNumber.Q2)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarMonth(2017, MonthOfYear.December)),
                    LeastGranular = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarYear(2017)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                    LeastGranular = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarQuarter(2017, QuarterNumber.Q4)),
                    LeastGranular = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarYear(2017)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarYear(2017)),
                    LeastGranular = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarYear(2017)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalUnbounded(),
                        new FiscalMonth(2017, MonthNumber.Four)),
                    LeastGranular = new ReportingPeriod(
                        new FiscalUnbounded(),
                        new FiscalMonth(2017, MonthNumber.Four)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalUnbounded(),
                        new FiscalMonth(2017, MonthNumber.Six)),
                    LeastGranular = new ReportingPeriod(
                        new FiscalUnbounded(),
                        new FiscalQuarter(2017, QuarterNumber.Q2)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalUnbounded(),
                        new FiscalMonth(2017, MonthNumber.Twelve)),
                    LeastGranular = new ReportingPeriod(
                        new FiscalUnbounded(),
                        new FiscalYear(2017)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalUnbounded(),
                        new FiscalQuarter(2017, QuarterNumber.Q3)),
                    LeastGranular = new ReportingPeriod(
                        new FiscalUnbounded(),
                        new FiscalQuarter(2017, QuarterNumber.Q3)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalUnbounded(),
                        new FiscalQuarter(2017, QuarterNumber.Q4)),
                    LeastGranular = new ReportingPeriod(
                        new FiscalUnbounded(),
                        new FiscalYear(2017)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalUnbounded(),
                        new FiscalYear(2017)),
                    LeastGranular = new ReportingPeriod(
                        new FiscalUnbounded(),
                        new FiscalYear(2017)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericUnbounded(),
                        new GenericMonth(2017, MonthNumber.Four)),
                    LeastGranular = new ReportingPeriod(
                        new GenericUnbounded(),
                        new GenericMonth(2017, MonthNumber.Four)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericUnbounded(),
                        new GenericMonth(2017, MonthNumber.Six)),
                    LeastGranular = new ReportingPeriod(
                        new GenericUnbounded(),
                        new GenericQuarter(2017, QuarterNumber.Q2)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericUnbounded(),
                        new GenericMonth(2017, MonthNumber.Twelve)),
                    LeastGranular = new ReportingPeriod(
                        new GenericUnbounded(),
                        new GenericYear(2017)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericUnbounded(),
                        new GenericQuarter(2017, QuarterNumber.Q3)),
                    LeastGranular = new ReportingPeriod(
                        new GenericUnbounded(),
                        new GenericQuarter(2017, QuarterNumber.Q3)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericUnbounded(),
                        new GenericQuarter(2017, QuarterNumber.Q4)),
                    LeastGranular = new ReportingPeriod(
                        new GenericUnbounded(),
                        new GenericYear(2017)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericUnbounded(),
                        new GenericYear(2017)),
                    LeastGranular = new ReportingPeriod(
                        new GenericUnbounded(),
                        new GenericYear(2017)),
                },
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Expected = _.LeastGranular, Actual = _.ReportingPeriod.ToLeastGranular() }).ToList();

            // Assert
            results.ForEach(_ => _.Expected.Should().Be(_.Actual));
            results.ForEach(_ => _.Expected.Should().NotBeSameAs(_.Actual));
        }

        [Fact]
        public static void ToAllGranularities___Should_throw_ArgumentNullException___When_parameter_reportingPeriod_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.ToAllGranularities(null, A.Dummy<bool>()));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void ToAllGranularities___Should_return_all_granularities___When_called()
        {
            // Arrange
            var reportingPeriods = Some.ReadOnlyDummies<ReportingPeriod>(100);

            var expected1 = reportingPeriods.Select(_ => _.ToAllLessGranular().Concat(_.ToAllMoreGranular()).ToList()).ToList();
            var expected2 = reportingPeriods.Select(_ => new[] { _ }.Concat(_.ToAllLessGranular()).Concat(_.ToAllMoreGranular()).ToList()).ToList();

            // Act
            var actual1 = reportingPeriods.Select(_ => _.ToAllGranularities(includeSpecifiedReportingPeriodInResult: false).ToList()).ToList();
            var actual2 = reportingPeriods.Select(_ => _.ToAllGranularities(includeSpecifiedReportingPeriodInResult: true).ToList()).ToList();

            // Assert
            expected1.AsTest().Must().BeEqualTo(actual1);
            expected2.AsTest().Must().BeEqualTo(actual2);
        }

        [Fact]
        public static void ToAllMoreGranular___Should_throw_ArgumentNullException___When_parameter_reportingPeriod_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.ToAllMoreGranular(null, A.Dummy<bool>()));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void ToAllMoreGranular___Should_return_all_more_granular_but_equivalent_reporting_periods___When_called()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarUnbounded()),
                    AllMoreGranular = new ReportingPeriod[0],
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarDay(2020, MonthOfYear.July, DayOfMonth.Two),
                        new CalendarDay(2020, MonthOfYear.July, DayOfMonth.Five)),
                    AllMoreGranular = new ReportingPeriod[0],
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarDay(2020, MonthOfYear.July, DayOfMonth.Two),
                        new CalendarUnbounded()),
                    AllMoreGranular = new ReportingPeriod[0],
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarDay(2020, MonthOfYear.July, DayOfMonth.Five)),
                    AllMoreGranular = new ReportingPeriod[0],
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarDay(2020, MonthOfYear.March, DayOfMonth.One),
                        new CalendarDay(2020, MonthOfYear.March, DayOfMonth.ThirtyOne)),
                    AllMoreGranular = new ReportingPeriod[0],
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarMonth(2020, MonthOfYear.December),
                        new CalendarMonth(2021, MonthOfYear.February)),
                    AllMoreGranular = new[]
                    {
                        new ReportingPeriod(
                            new CalendarDay(2020, MonthOfYear.December, DayOfMonth.One),
                            new CalendarDay(2021, MonthOfYear.February, DayOfMonth.TwentyEight)),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarMonth(2020, MonthOfYear.December),
                        new CalendarUnbounded()),
                    AllMoreGranular = new[]
                    {
                        new ReportingPeriod(
                            new CalendarDay(2020, MonthOfYear.December, DayOfMonth.One),
                            new CalendarUnbounded()),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarMonth(2021, MonthOfYear.February)),
                    AllMoreGranular = new[]
                    {
                        new ReportingPeriod(
                            new CalendarUnbounded(),
                            new CalendarDay(2021, MonthOfYear.February, DayOfMonth.TwentyEight)),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarQuarter(2020, QuarterNumber.Q1),
                        new CalendarQuarter(2020, QuarterNumber.Q3)),
                    AllMoreGranular = new[]
                    {
                        new ReportingPeriod(
                            new CalendarMonth(2020, MonthOfYear.January),
                            new CalendarMonth(2020, MonthOfYear.September)),
                        new ReportingPeriod(
                            new CalendarDay(2020, MonthOfYear.January, DayOfMonth.One),
                            new CalendarDay(2020, MonthOfYear.September, DayOfMonth.Thirty)),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarQuarter(2020, QuarterNumber.Q1),
                        new CalendarUnbounded()),
                    AllMoreGranular = new[]
                    {
                        new ReportingPeriod(
                            new CalendarMonth(2020, MonthOfYear.January),
                            new CalendarUnbounded()),
                        new ReportingPeriod(
                            new CalendarDay(2020, MonthOfYear.January, DayOfMonth.One),
                            new CalendarUnbounded()),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarQuarter(2020, QuarterNumber.Q3)),
                    AllMoreGranular = new[]
                    {
                        new ReportingPeriod(
                            new CalendarUnbounded(),
                            new CalendarMonth(2020, MonthOfYear.September)),
                        new ReportingPeriod(
                            new CalendarUnbounded(),
                            new CalendarDay(2020, MonthOfYear.September, DayOfMonth.Thirty)),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarYear(2020),
                        new CalendarYear(2021)),
                    AllMoreGranular = new[]
                    {
                        new ReportingPeriod(
                            new CalendarQuarter(2020, QuarterNumber.Q1),
                            new CalendarQuarter(2021, QuarterNumber.Q4)),
                        new ReportingPeriod(
                            new CalendarMonth(2020, MonthOfYear.January),
                            new CalendarMonth(2021, MonthOfYear.December)),
                        new ReportingPeriod(
                            new CalendarDay(2020, MonthOfYear.January, DayOfMonth.One),
                            new CalendarDay(2021, MonthOfYear.December, DayOfMonth.ThirtyOne)),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarYear(2021)),
                    AllMoreGranular = new[]
                    {
                        new ReportingPeriod(
                            new CalendarUnbounded(),
                            new CalendarQuarter(2021, QuarterNumber.Q4)),
                        new ReportingPeriod(
                            new CalendarUnbounded(),
                            new CalendarMonth(2021, MonthOfYear.December)),
                        new ReportingPeriod(
                            new CalendarUnbounded(),
                            new CalendarDay(2021, MonthOfYear.December, DayOfMonth.ThirtyOne)),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarYear(2020),
                        new CalendarUnbounded()),
                    AllMoreGranular = new[]
                    {
                        new ReportingPeriod(
                            new CalendarQuarter(2020, QuarterNumber.Q1),
                            new CalendarUnbounded()),
                        new ReportingPeriod(
                            new CalendarMonth(2020, MonthOfYear.January),
                            new CalendarUnbounded()),
                        new ReportingPeriod(
                            new CalendarDay(2020, MonthOfYear.January, DayOfMonth.One),
                            new CalendarUnbounded()),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalUnbounded(),
                        new FiscalUnbounded()),
                    AllMoreGranular = new ReportingPeriod[0],
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalMonth(2020, MonthNumber.Twelve),
                        new FiscalMonth(2021, MonthNumber.Two)),
                    AllMoreGranular = new ReportingPeriod[] { },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalMonth(2020, MonthNumber.Twelve),
                        new FiscalUnbounded()),
                    AllMoreGranular = new ReportingPeriod[] { },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalUnbounded(),
                        new FiscalMonth(2021, MonthNumber.Two)),
                    AllMoreGranular = new ReportingPeriod[] { },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalQuarter(2020, QuarterNumber.Q1),
                        new FiscalQuarter(2020, QuarterNumber.Q3)),
                    AllMoreGranular = new[]
                    {
                        new ReportingPeriod(
                            new FiscalMonth(2020, MonthNumber.One),
                            new FiscalMonth(2020, MonthNumber.Nine)),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalQuarter(2020, QuarterNumber.Q1),
                        new FiscalUnbounded()),
                    AllMoreGranular = new[]
                    {
                        new ReportingPeriod(
                            new FiscalMonth(2020, MonthNumber.One),
                            new FiscalUnbounded()),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalUnbounded(),
                        new FiscalQuarter(2020, QuarterNumber.Q3)),
                    AllMoreGranular = new[]
                    {
                        new ReportingPeriod(
                            new FiscalUnbounded(),
                            new FiscalMonth(2020, MonthNumber.Nine)),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalYear(2020),
                        new FiscalYear(2021)),
                    AllMoreGranular = new[]
                    {
                        new ReportingPeriod(
                            new FiscalQuarter(2020, QuarterNumber.Q1),
                            new FiscalQuarter(2021, QuarterNumber.Q4)),
                        new ReportingPeriod(
                            new FiscalMonth(2020, MonthNumber.One),
                            new FiscalMonth(2021, MonthNumber.Twelve)),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalUnbounded(),
                        new FiscalYear(2021)),
                    AllMoreGranular = new[]
                    {
                        new ReportingPeriod(
                            new FiscalUnbounded(),
                            new FiscalQuarter(2021, QuarterNumber.Q4)),
                        new ReportingPeriod(
                            new FiscalUnbounded(),
                            new FiscalMonth(2021, MonthNumber.Twelve)),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalYear(2020),
                        new FiscalUnbounded()),
                    AllMoreGranular = new[]
                    {
                        new ReportingPeriod(
                            new FiscalQuarter(2020, QuarterNumber.Q1),
                            new FiscalUnbounded()),
                        new ReportingPeriod(
                            new FiscalMonth(2020, MonthNumber.One),
                            new FiscalUnbounded()),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericUnbounded(),
                        new GenericUnbounded()),
                    AllMoreGranular = new ReportingPeriod[0],
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericMonth(2020, MonthNumber.Twelve),
                        new GenericMonth(2021, MonthNumber.Two)),
                    AllMoreGranular = new ReportingPeriod[] { },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericMonth(2020, MonthNumber.Twelve),
                        new GenericUnbounded()),
                    AllMoreGranular = new ReportingPeriod[] { },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericUnbounded(),
                        new GenericMonth(2021, MonthNumber.Two)),
                    AllMoreGranular = new ReportingPeriod[] { },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericQuarter(2020, QuarterNumber.Q1),
                        new GenericQuarter(2020, QuarterNumber.Q3)),
                    AllMoreGranular = new[]
                    {
                        new ReportingPeriod(
                            new GenericMonth(2020, MonthNumber.One),
                            new GenericMonth(2020, MonthNumber.Nine)),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericQuarter(2020, QuarterNumber.Q1),
                        new GenericUnbounded()),
                    AllMoreGranular = new[]
                    {
                        new ReportingPeriod(
                            new GenericMonth(2020, MonthNumber.One),
                            new GenericUnbounded()),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericUnbounded(),
                        new GenericQuarter(2020, QuarterNumber.Q3)),
                    AllMoreGranular = new[]
                    {
                        new ReportingPeriod(
                            new GenericUnbounded(),
                            new GenericMonth(2020, MonthNumber.Nine)),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericYear(2020),
                        new GenericYear(2021)),
                    AllMoreGranular = new[]
                    {
                        new ReportingPeriod(
                            new GenericQuarter(2020, QuarterNumber.Q1),
                            new GenericQuarter(2021, QuarterNumber.Q4)),
                        new ReportingPeriod(
                            new GenericMonth(2020, MonthNumber.One),
                            new GenericMonth(2021, MonthNumber.Twelve)),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericUnbounded(),
                        new GenericYear(2021)),
                    AllMoreGranular = new[]
                    {
                        new ReportingPeriod(
                            new GenericUnbounded(),
                            new GenericQuarter(2021, QuarterNumber.Q4)),
                        new ReportingPeriod(
                            new GenericUnbounded(),
                            new GenericMonth(2021, MonthNumber.Twelve)),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericYear(2020),
                        new GenericUnbounded()),
                    AllMoreGranular = new[]
                    {
                        new ReportingPeriod(
                            new GenericQuarter(2020, QuarterNumber.Q1),
                            new GenericUnbounded()),
                        new ReportingPeriod(
                            new GenericMonth(2020, MonthNumber.One),
                            new GenericUnbounded()),
                    },
                },
            };

            // Act
            var results1 = reportingPeriods.Select(_ => new { Expected = _.AllMoreGranular, Actual = _.ReportingPeriod.ToAllMoreGranular(includeSpecifiedReportingPeriodInResult: false) }).ToList();
            var results2 = reportingPeriods.Select(_ => new { Expected = _.AllMoreGranular.Concat(new[] { _.ReportingPeriod }).ToList(), Actual = _.ReportingPeriod.ToAllMoreGranular(includeSpecifiedReportingPeriodInResult: true) }).ToList();

            // Assert
            results1.Select(_ => _.Actual.IsUnorderedEqualTo(_.Expected)).Must().Each().BeTrue();
            results2.Select(_ => _.Actual.IsUnorderedEqualTo(_.Expected)).Must().Each().BeTrue();
        }

        [Fact]
        public static void ToAllLessGranular___Should_throw_ArgumentNullException___When_parameter_reportingPeriod_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.ToAllLessGranular(null, A.Dummy<bool>()));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void ToAllLessGranular___Should_return_all_less_granular_but_equivalent_reporting_periods___When_called()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarUnbounded()),
                    AllLessGranular = new ReportingPeriod[0],
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarDay(2020, MonthOfYear.July, DayOfMonth.Two),
                        new CalendarDay(2020, MonthOfYear.July, DayOfMonth.Five)),
                    AllLessGranular = new ReportingPeriod[0],
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarDay(2020, MonthOfYear.July, DayOfMonth.Two),
                        new CalendarUnbounded()),
                    AllLessGranular = new ReportingPeriod[0],
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarDay(2020, MonthOfYear.July, DayOfMonth.Five)),
                    AllLessGranular = new ReportingPeriod[0],
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarDay(2020, MonthOfYear.May, DayOfMonth.One),
                        new CalendarDay(2021, MonthOfYear.May, DayOfMonth.ThirtyOne)),
                    AllLessGranular = new[]
                    {
                        new ReportingPeriod(
                            new CalendarMonth(2020, MonthOfYear.May),
                            new CalendarMonth(2021, MonthOfYear.May)),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarDay(2021, MonthOfYear.May, DayOfMonth.ThirtyOne)),
                    AllLessGranular = new[]
                    {
                        new ReportingPeriod(
                            new CalendarUnbounded(),
                            new CalendarMonth(2021, MonthOfYear.May)),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarDay(2020, MonthOfYear.May, DayOfMonth.One),
                        new CalendarUnbounded()),
                    AllLessGranular = new[]
                    {
                        new ReportingPeriod(
                            new CalendarMonth(2020, MonthOfYear.May),
                            new CalendarUnbounded()),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarDay(2020, MonthOfYear.March, DayOfMonth.One),
                        new CalendarDay(2021, MonthOfYear.March, DayOfMonth.ThirtyOne)),
                    AllLessGranular = new[]
                    {
                        new ReportingPeriod(
                            new CalendarMonth(2020, MonthOfYear.March),
                            new CalendarMonth(2021, MonthOfYear.March)),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarDay(2021, MonthOfYear.March, DayOfMonth.ThirtyOne)),
                    AllLessGranular = new[]
                    {
                        new ReportingPeriod(
                            new CalendarUnbounded(),
                            new CalendarMonth(2021, MonthOfYear.March)),
                        new ReportingPeriod(
                            new CalendarUnbounded(),
                            new CalendarQuarter(2021, QuarterNumber.Q1)),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarDay(2020, MonthOfYear.March, DayOfMonth.One),
                        new CalendarUnbounded()),
                    AllLessGranular = new[]
                    {
                        new ReportingPeriod(
                            new CalendarMonth(2020, MonthOfYear.March),
                            new CalendarUnbounded()),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarDay(2020, MonthOfYear.October, DayOfMonth.One),
                        new CalendarDay(2021, MonthOfYear.June, DayOfMonth.Thirty)),
                    AllLessGranular = new[]
                    {
                        new ReportingPeriod(
                            new CalendarMonth(2020, MonthOfYear.October),
                            new CalendarMonth(2021, MonthOfYear.June)),
                        new ReportingPeriod(
                            new CalendarQuarter(2020, QuarterNumber.Q4),
                            new CalendarQuarter(2021, QuarterNumber.Q2)),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarDay(2021, MonthOfYear.June, DayOfMonth.Thirty)),
                    AllLessGranular = new[]
                    {
                        new ReportingPeriod(
                            new CalendarUnbounded(),
                            new CalendarMonth(2021, MonthOfYear.June)),
                        new ReportingPeriod(
                            new CalendarUnbounded(),
                            new CalendarQuarter(2021, QuarterNumber.Q2)),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarDay(2020, MonthOfYear.October, DayOfMonth.One),
                        new CalendarUnbounded()),
                    AllLessGranular = new[]
                    {
                        new ReportingPeriod(
                            new CalendarMonth(2020, MonthOfYear.October),
                            new CalendarUnbounded()),
                        new ReportingPeriod(
                            new CalendarQuarter(2020, QuarterNumber.Q4),
                            new CalendarUnbounded()),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarDay(2020, MonthOfYear.January, DayOfMonth.One),
                        new CalendarDay(2021, MonthOfYear.December, DayOfMonth.ThirtyOne)),
                    AllLessGranular = new[]
                    {
                        new ReportingPeriod(
                            new CalendarMonth(2020, MonthOfYear.January),
                            new CalendarMonth(2021, MonthOfYear.December)),
                        new ReportingPeriod(
                            new CalendarQuarter(2020, QuarterNumber.Q1),
                            new CalendarQuarter(2021, QuarterNumber.Q4)),
                        new ReportingPeriod(
                            new CalendarYear(2020),
                            new CalendarYear(2021)),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarDay(2021, MonthOfYear.December, DayOfMonth.ThirtyOne)),
                    AllLessGranular = new[]
                    {
                        new ReportingPeriod(
                            new CalendarUnbounded(),
                            new CalendarMonth(2021, MonthOfYear.December)),
                        new ReportingPeriod(
                            new CalendarUnbounded(),
                            new CalendarQuarter(2021, QuarterNumber.Q4)),
                        new ReportingPeriod(
                            new CalendarUnbounded(),
                            new CalendarYear(2021)),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarDay(2020, MonthOfYear.January, DayOfMonth.One),
                        new CalendarUnbounded()),
                    AllLessGranular = new[]
                    {
                        new ReportingPeriod(
                            new CalendarMonth(2020, MonthOfYear.January),
                            new CalendarUnbounded()),
                        new ReportingPeriod(
                            new CalendarQuarter(2020, QuarterNumber.Q1),
                            new CalendarUnbounded()),
                        new ReportingPeriod(
                            new CalendarYear(2020),
                            new CalendarUnbounded()),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarMonth(2020, MonthOfYear.February),
                        new CalendarMonth(2021, MonthOfYear.February)),
                    AllLessGranular = new ReportingPeriod[] { },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarMonth(2021, MonthOfYear.February)),
                    AllLessGranular = new ReportingPeriod[] { },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarMonth(2020, MonthOfYear.February),
                        new CalendarUnbounded()),
                    AllLessGranular = new ReportingPeriod[] { },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarMonth(2020, MonthOfYear.April),
                        new CalendarMonth(2021, MonthOfYear.June)),
                    AllLessGranular = new[]
                    {
                        new ReportingPeriod(
                            new CalendarQuarter(2020, QuarterNumber.Q2),
                            new CalendarQuarter(2021, QuarterNumber.Q2)),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarMonth(2021, MonthOfYear.June)),
                    AllLessGranular = new[]
                    {
                        new ReportingPeriod(
                            new CalendarUnbounded(),
                            new CalendarQuarter(2021, QuarterNumber.Q2)),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarMonth(2020, MonthOfYear.April),
                        new CalendarUnbounded()),
                    AllLessGranular = new[]
                    {
                        new ReportingPeriod(
                            new CalendarQuarter(2020, QuarterNumber.Q2),
                            new CalendarUnbounded()),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarMonth(2020, MonthOfYear.January),
                        new CalendarMonth(2021, MonthOfYear.June)),
                    AllLessGranular = new[]
                    {
                        new ReportingPeriod(
                            new CalendarQuarter(2020, QuarterNumber.Q1),
                            new CalendarQuarter(2021, QuarterNumber.Q2)),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarMonth(2021, MonthOfYear.June)),
                    AllLessGranular = new[]
                    {
                        new ReportingPeriod(
                            new CalendarUnbounded(),
                            new CalendarQuarter(2021, QuarterNumber.Q2)),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarMonth(2020, MonthOfYear.January),
                        new CalendarUnbounded()),
                    AllLessGranular = new[]
                    {
                        new ReportingPeriod(
                            new CalendarQuarter(2020, QuarterNumber.Q1),
                            new CalendarUnbounded()),
                        new ReportingPeriod(
                            new CalendarYear(2020),
                            new CalendarUnbounded()),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarQuarter(2020, QuarterNumber.Q2),
                        new CalendarQuarter(2021, QuarterNumber.Q3)),
                    AllLessGranular = new ReportingPeriod[] { },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarQuarter(2021, QuarterNumber.Q3)),
                    AllLessGranular = new ReportingPeriod[] { },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarQuarter(2020, QuarterNumber.Q2),
                        new CalendarUnbounded()),
                    AllLessGranular = new ReportingPeriod[] { },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarQuarter(2020, QuarterNumber.Q2),
                        new CalendarQuarter(2021, QuarterNumber.Q4)),
                    AllLessGranular = new ReportingPeriod[] { },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarQuarter(2021, QuarterNumber.Q4)),
                    AllLessGranular = new ReportingPeriod[]
                    {
                        new ReportingPeriod(
                            new CalendarUnbounded(),
                            new CalendarYear(2021)),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarQuarter(2020, QuarterNumber.Q2),
                        new CalendarUnbounded()),
                    AllLessGranular = new ReportingPeriod[] { },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarQuarter(2020, QuarterNumber.Q1),
                        new CalendarQuarter(2021, QuarterNumber.Q4)),
                    AllLessGranular = new[]
                    {
                        new ReportingPeriod(
                            new CalendarYear(2020),
                            new CalendarYear(2021)),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarUnbounded(),
                        new CalendarQuarter(2021, QuarterNumber.Q4)),
                    AllLessGranular = new[]
                    {
                        new ReportingPeriod(
                            new CalendarUnbounded(),
                            new CalendarYear(2021)),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarQuarter(2020, QuarterNumber.Q1),
                        new CalendarUnbounded()),
                    AllLessGranular = new[]
                    {
                        new ReportingPeriod(
                            new CalendarYear(2020),
                            new CalendarUnbounded()),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new CalendarYear(2020),
                        new CalendarYear(2020)),
                    AllLessGranular = new ReportingPeriod[] { },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalUnbounded(),
                        new FiscalUnbounded()),
                    AllLessGranular = new ReportingPeriod[] { },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalMonth(2020, MonthNumber.Two),
                        new FiscalMonth(2021, MonthNumber.Two)),
                    AllLessGranular = new ReportingPeriod[] { },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalUnbounded(),
                        new FiscalMonth(2021, MonthNumber.Two)),
                    AllLessGranular = new ReportingPeriod[] { },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalMonth(2020, MonthNumber.Two),
                        new FiscalUnbounded()),
                    AllLessGranular = new ReportingPeriod[] { },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalMonth(2020, MonthNumber.Four),
                        new FiscalMonth(2021, MonthNumber.Six)),
                    AllLessGranular = new[]
                    {
                        new ReportingPeriod(
                            new FiscalQuarter(2020, QuarterNumber.Q2),
                            new FiscalQuarter(2021, QuarterNumber.Q2)),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalUnbounded(),
                        new FiscalMonth(2021, MonthNumber.Six)),
                    AllLessGranular = new[]
                    {
                        new ReportingPeriod(
                            new FiscalUnbounded(),
                            new FiscalQuarter(2021, QuarterNumber.Q2)),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalMonth(2020, MonthNumber.Four),
                        new FiscalUnbounded()),
                    AllLessGranular = new[]
                    {
                        new ReportingPeriod(
                            new FiscalQuarter(2020, QuarterNumber.Q2),
                            new FiscalUnbounded()),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalMonth(2020, MonthNumber.One),
                        new FiscalMonth(2021, MonthNumber.Six)),
                    AllLessGranular = new[]
                    {
                        new ReportingPeriod(
                            new FiscalQuarter(2020, QuarterNumber.Q1),
                            new FiscalQuarter(2021, QuarterNumber.Q2)),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalUnbounded(),
                        new FiscalMonth(2021, MonthNumber.Six)),
                    AllLessGranular = new[]
                    {
                        new ReportingPeriod(
                            new FiscalUnbounded(),
                            new FiscalQuarter(2021, QuarterNumber.Q2)),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalMonth(2020, MonthNumber.One),
                        new FiscalUnbounded()),
                    AllLessGranular = new[]
                    {
                        new ReportingPeriod(
                            new FiscalQuarter(2020, QuarterNumber.Q1),
                            new FiscalUnbounded()),
                        new ReportingPeriod(
                            new FiscalYear(2020),
                            new FiscalUnbounded()),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalQuarter(2020, QuarterNumber.Q2),
                        new FiscalQuarter(2021, QuarterNumber.Q3)),
                    AllLessGranular = new ReportingPeriod[] { },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalUnbounded(),
                        new FiscalQuarter(2021, QuarterNumber.Q3)),
                    AllLessGranular = new ReportingPeriod[] { },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalQuarter(2020, QuarterNumber.Q2),
                        new FiscalUnbounded()),
                    AllLessGranular = new ReportingPeriod[] { },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalQuarter(2020, QuarterNumber.Q2),
                        new FiscalQuarter(2021, QuarterNumber.Q4)),
                    AllLessGranular = new ReportingPeriod[] { },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalUnbounded(),
                        new FiscalQuarter(2021, QuarterNumber.Q4)),
                    AllLessGranular = new ReportingPeriod[]
                    {
                        new ReportingPeriod(
                            new FiscalUnbounded(),
                            new FiscalYear(2021)),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalQuarter(2020, QuarterNumber.Q2),
                        new FiscalUnbounded()),
                    AllLessGranular = new ReportingPeriod[] { },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalQuarter(2020, QuarterNumber.Q1),
                        new FiscalQuarter(2021, QuarterNumber.Q4)),
                    AllLessGranular = new[]
                    {
                        new ReportingPeriod(
                            new FiscalYear(2020),
                            new FiscalYear(2021)),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalUnbounded(),
                        new FiscalQuarter(2021, QuarterNumber.Q4)),
                    AllLessGranular = new[]
                    {
                        new ReportingPeriod(
                            new FiscalUnbounded(),
                            new FiscalYear(2021)),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalQuarter(2020, QuarterNumber.Q1),
                        new FiscalUnbounded()),
                    AllLessGranular = new[]
                    {
                        new ReportingPeriod(
                            new FiscalYear(2020),
                            new FiscalUnbounded()),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new FiscalYear(2020),
                        new FiscalYear(2020)),
                    AllLessGranular = new ReportingPeriod[] { },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericUnbounded(),
                        new GenericUnbounded()),
                    AllLessGranular = new ReportingPeriod[] { },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericMonth(2020, MonthNumber.Two),
                        new GenericMonth(2021, MonthNumber.Two)),
                    AllLessGranular = new ReportingPeriod[] { },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericUnbounded(),
                        new GenericMonth(2021, MonthNumber.Two)),
                    AllLessGranular = new ReportingPeriod[] { },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericMonth(2020, MonthNumber.Two),
                        new GenericUnbounded()),
                    AllLessGranular = new ReportingPeriod[] { },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericMonth(2020, MonthNumber.Four),
                        new GenericMonth(2021, MonthNumber.Six)),
                    AllLessGranular = new[]
                    {
                        new ReportingPeriod(
                            new GenericQuarter(2020, QuarterNumber.Q2),
                            new GenericQuarter(2021, QuarterNumber.Q2)),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericUnbounded(),
                        new GenericMonth(2021, MonthNumber.Six)),
                    AllLessGranular = new[]
                    {
                        new ReportingPeriod(
                            new GenericUnbounded(),
                            new GenericQuarter(2021, QuarterNumber.Q2)),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericMonth(2020, MonthNumber.Four),
                        new GenericUnbounded()),
                    AllLessGranular = new[]
                    {
                        new ReportingPeriod(
                            new GenericQuarter(2020, QuarterNumber.Q2),
                            new GenericUnbounded()),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericMonth(2020, MonthNumber.One),
                        new GenericMonth(2021, MonthNumber.Six)),
                    AllLessGranular = new[]
                    {
                        new ReportingPeriod(
                            new GenericQuarter(2020, QuarterNumber.Q1),
                            new GenericQuarter(2021, QuarterNumber.Q2)),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericUnbounded(),
                        new GenericMonth(2021, MonthNumber.Six)),
                    AllLessGranular = new[]
                    {
                        new ReportingPeriod(
                            new GenericUnbounded(),
                            new GenericQuarter(2021, QuarterNumber.Q2)),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericMonth(2020, MonthNumber.One),
                        new GenericUnbounded()),
                    AllLessGranular = new[]
                    {
                        new ReportingPeriod(
                            new GenericQuarter(2020, QuarterNumber.Q1),
                            new GenericUnbounded()),
                        new ReportingPeriod(
                            new GenericYear(2020),
                            new GenericUnbounded()),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericQuarter(2020, QuarterNumber.Q2),
                        new GenericQuarter(2021, QuarterNumber.Q3)),
                    AllLessGranular = new ReportingPeriod[] { },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericUnbounded(),
                        new GenericQuarter(2021, QuarterNumber.Q3)),
                    AllLessGranular = new ReportingPeriod[] { },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericQuarter(2020, QuarterNumber.Q2),
                        new GenericUnbounded()),
                    AllLessGranular = new ReportingPeriod[] { },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericQuarter(2020, QuarterNumber.Q2),
                        new GenericQuarter(2021, QuarterNumber.Q4)),
                    AllLessGranular = new ReportingPeriod[] { },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericUnbounded(),
                        new GenericQuarter(2021, QuarterNumber.Q4)),
                    AllLessGranular = new ReportingPeriod[]
                    {
                        new ReportingPeriod(
                            new GenericUnbounded(),
                            new GenericYear(2021)),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericQuarter(2020, QuarterNumber.Q2),
                        new GenericUnbounded()),
                    AllLessGranular = new ReportingPeriod[] { },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericQuarter(2020, QuarterNumber.Q1),
                        new GenericQuarter(2021, QuarterNumber.Q4)),
                    AllLessGranular = new[]
                    {
                        new ReportingPeriod(
                            new GenericYear(2020),
                            new GenericYear(2021)),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericUnbounded(),
                        new GenericQuarter(2021, QuarterNumber.Q4)),
                    AllLessGranular = new[]
                    {
                        new ReportingPeriod(
                            new GenericUnbounded(),
                            new GenericYear(2021)),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericQuarter(2020, QuarterNumber.Q1),
                        new GenericUnbounded()),
                    AllLessGranular = new[]
                    {
                        new ReportingPeriod(
                            new GenericYear(2020),
                            new GenericUnbounded()),
                    },
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod(
                        new GenericYear(2020),
                        new GenericYear(2020)),
                    AllLessGranular = new ReportingPeriod[] { },
                },
            };

            // Act
            var results1 = reportingPeriods.Select(_ => new { Expected = _.AllLessGranular, Actual = _.ReportingPeriod.ToAllLessGranular(includeSpecifiedReportingPeriodInResult: false) }).ToList();
            var results2 = reportingPeriods.Select(_ => new { Expected = _.AllLessGranular.Concat(new[] { _.ReportingPeriod }).ToList(), Actual = _.ReportingPeriod.ToAllLessGranular(includeSpecifiedReportingPeriodInResult: true) }).ToList();

            // Assert
            results1.Select(_ => _.Actual.IsUnorderedEqualTo(_.Expected)).Must().Each().BeTrue();
            results2.Select(_ => _.Actual.IsUnorderedEqualTo(_.Expected)).Must().Each().BeTrue();
        }
    }
}
