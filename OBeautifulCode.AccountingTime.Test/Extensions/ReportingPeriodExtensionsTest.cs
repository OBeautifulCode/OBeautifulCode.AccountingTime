﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriodExtensionsTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoFakeItEasy;

    using FakeItEasy;

    using FluentAssertions;

    using Naos.Recipes.TupleInitializers;

    using Xunit;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Testing this class requires lots of types because of the number of unit-of-time types intersected with the options for reporting period.")]
    public static class ReportingPeriodExtensionsTest
    {
        // ReSharper disable InconsistentNaming
        [Fact]
        public static void CloneWithAdjustment___Should_throw_ArgumentNullException___When_parameter_reportingPeriod_is_null()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod<UnitOfTime>>().Whose(_ => _.Start.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded && _.End.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded);
            var reportingPeriodComponent = A.Dummy<ReportingPeriodComponent>();
            var granularityToAdd = reportingPeriod.Start.UnitOfTimeGranularity;
            var unitsToAdd = A.Dummy<int>().ThatIs(i => i > -100 && i < 100);

            // Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.CloneWithAdjustment<IReportingPeriod<UnitOfTime>>(null, reportingPeriodComponent, unitsToAdd, granularityToAdd));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void CloneWithAdjustment___Should_throw_ArgumentException___When_parameter_reportingPeriodComponent_is_Invalid()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod<UnitOfTime>>().Whose(_ => _.Start.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded && _.End.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded);
            var granularityToAdd = reportingPeriod.Start.UnitOfTimeGranularity;
            var unitsToAdd = A.Dummy<int>().ThatIs(i => i > -100 && i < 100);

            // Act
            var ex = Record.Exception(() => reportingPeriod.CloneWithAdjustment<IReportingPeriod<UnitOfTime>>(ReportingPeriodComponent.Invalid, unitsToAdd, granularityToAdd));

            // Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void CloneWithAdjustment___Should_throw_ArgumentException___When_parameter_granularityOfUnitsToAdd_is_Invalid()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod<UnitOfTime>>().Whose(_ => _.Start.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded && _.End.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded);
            var reportingPeriodComponent = A.Dummy<ReportingPeriodComponent>();
            var unitsToAdd = A.Dummy<int>().ThatIs(i => i > -100 && i < 100);

            // Act
            var ex = Record.Exception(() => reportingPeriod.CloneWithAdjustment<IReportingPeriod<UnitOfTime>>(reportingPeriodComponent, unitsToAdd, UnitOfTimeGranularity.Invalid));

            // Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void CloneWithAdjustment___Should_throw_ArgumentException___When_parameter_granularityOfUnitsToAdd_is_Unbounded()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod<UnitOfTime>>().Whose(_ => _.Start.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded && _.End.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded);
            var reportingPeriodComponent = A.Dummy<ReportingPeriodComponent>();
            var unitsToAdd = A.Dummy<int>().ThatIs(i => i > -100 && i < 100);

            // Act
            var ex = Record.Exception(() => reportingPeriod.CloneWithAdjustment<IReportingPeriod<UnitOfTime>>(reportingPeriodComponent, unitsToAdd, UnitOfTimeGranularity.Unbounded));

            // Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void CloneWithAdjustment___Should_throw_ArgumentException___When_adjusting_reportingPeriod_with_an_Unbounded_Start()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod<UnitOfTime>>().Whose(_ => _.Start.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded && _.End.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded);
            var granularityToAdd = reportingPeriod.End.UnitOfTimeGranularity;
            var unitsToAdd = A.Dummy<int>().ThatIs(i => i > -100 && i < 100);

            // Act
            var ex1 = Record.Exception(() => reportingPeriod.CloneWithAdjustment<IReportingPeriod<UnitOfTime>>(ReportingPeriodComponent.Start, unitsToAdd, granularityToAdd));
            var ex2 = Record.Exception(() => reportingPeriod.CloneWithAdjustment<IReportingPeriod<UnitOfTime>>(ReportingPeriodComponent.Both, unitsToAdd, granularityToAdd));

            // Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex2.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void CloneWithAdjustment___Should_throw_ArgumentException___When_adjusting_reportingPeriod_with_an_Unbounded_End()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod<UnitOfTime>>().Whose(_ => _.Start.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded && _.End.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded);
            var granularityToAdd = reportingPeriod.Start.UnitOfTimeGranularity;
            var unitsToAdd = A.Dummy<int>().ThatIs(i => i > -100 && i < 100);

            // Act
            var ex1 = Record.Exception(() => reportingPeriod.CloneWithAdjustment<IReportingPeriod<UnitOfTime>>(ReportingPeriodComponent.End, unitsToAdd, granularityToAdd));
            var ex2 = Record.Exception(() => reportingPeriod.CloneWithAdjustment<IReportingPeriod<UnitOfTime>>(ReportingPeriodComponent.Both, unitsToAdd, granularityToAdd));

            // Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex2.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "testing all the flavors of unit-of-time")]
        public static void CloneWithAdjustment___Should_throw_ArgumentException___When_granularityOfUnitsToAdd_is_more_granular_than_component_being_adjusted()
        {
            // Arrange
            var unitsToAdd = A.Dummy<int>().ThatIs(i => i > -100 && i < 100);
            var tests = new[]
            {
                new { ReportingPeriod = (IReportingPeriod<UnitOfTime>)A.Dummy<IReportingPeriod<CalendarDay>>(), GranularityOfUnitsToAdd = new UnitOfTimeGranularity[] { } },
                new { ReportingPeriod = (IReportingPeriod<UnitOfTime>)A.Dummy<IReportingPeriod<CalendarMonth>>(), GranularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Day } },
                new { ReportingPeriod = (IReportingPeriod<UnitOfTime>)A.Dummy<IReportingPeriod<CalendarQuarter>>(), GranularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Day, UnitOfTimeGranularity.Month } },
                new { ReportingPeriod = (IReportingPeriod<UnitOfTime>)A.Dummy<IReportingPeriod<CalendarYear>>(), GranularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Day, UnitOfTimeGranularity.Month, UnitOfTimeGranularity.Quarter } },
                new { ReportingPeriod = (IReportingPeriod<UnitOfTime>)A.Dummy<IReportingPeriod<FiscalMonth>>(), GranularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Day } },
                new { ReportingPeriod = (IReportingPeriod<UnitOfTime>)A.Dummy<IReportingPeriod<FiscalQuarter>>(), GranularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Day, UnitOfTimeGranularity.Month } },
                new { ReportingPeriod = (IReportingPeriod<UnitOfTime>)A.Dummy<IReportingPeriod<FiscalYear>>(), GranularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Day, UnitOfTimeGranularity.Month, UnitOfTimeGranularity.Quarter } },
                new { ReportingPeriod = (IReportingPeriod<UnitOfTime>)A.Dummy<IReportingPeriod<GenericMonth>>(), GranularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Day } },
                new { ReportingPeriod = (IReportingPeriod<UnitOfTime>)A.Dummy<IReportingPeriod<GenericQuarter>>(), GranularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Day, UnitOfTimeGranularity.Month } },
                new { ReportingPeriod = (IReportingPeriod<UnitOfTime>)A.Dummy<IReportingPeriod<GenericYear>>(), GranularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Day, UnitOfTimeGranularity.Month, UnitOfTimeGranularity.Quarter } }
            };

            // Act
            var exceptions = new List<Exception>();
            foreach (var test in tests)
            {
                foreach (var granularityOfUnitsToAdd in test.GranularityOfUnitsToAdd)
                {
                    exceptions.Add(Record.Exception(() => test.ReportingPeriod.CloneWithAdjustment<IReportingPeriod<UnitOfTime>>(ReportingPeriodComponent.Start, unitsToAdd, granularityOfUnitsToAdd)));
                    exceptions.Add(Record.Exception(() => test.ReportingPeriod.CloneWithAdjustment<IReportingPeriod<UnitOfTime>>(ReportingPeriodComponent.End, unitsToAdd, granularityOfUnitsToAdd)));
                    exceptions.Add(Record.Exception(() => test.ReportingPeriod.CloneWithAdjustment<IReportingPeriod<UnitOfTime>>(ReportingPeriodComponent.Both, unitsToAdd, granularityOfUnitsToAdd)));
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
                    ReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = 1,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Month,
                    ExpectedReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Five), new FiscalMonth(2017, MonthNumber.Eleven)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = -2,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Quarter,
                    ExpectedReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2015, MonthNumber.Ten), new FiscalMonth(2017, MonthNumber.Eleven)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2014, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = 2,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year,
                    ExpectedReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Eleven)),
                }
            };

            // Act, Assert
            foreach (var test in tests)
            {
                var actualReportingPeriod = test.ReportingPeriod.CloneWithAdjustment<ReportingPeriod<FiscalMonth>>(ReportingPeriodComponent.Start, test.UnitsToAdd, test.GranularityOfUnitsToAdd);
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
                    ReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = 1,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Month,
                    ExpectedReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Twelve))
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = -2,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Quarter,
                    ExpectedReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Five))
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = 2,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year,
                    ExpectedReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2019, MonthNumber.Eleven))
                }
            };

            // Act, Assert
            foreach (var test in tests)
            {
                var actualReportingPeriod = test.ReportingPeriod.CloneWithAdjustment<ReportingPeriod<FiscalMonth>>(ReportingPeriodComponent.End, test.UnitsToAdd, test.GranularityOfUnitsToAdd);
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
                    ReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = 1,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Month,
                    ExpectedReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Five), new FiscalMonth(2017, MonthNumber.Twelve))
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = -2,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Quarter,
                    ExpectedReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2015, MonthNumber.Ten), new FiscalMonth(2017, MonthNumber.Five))
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = 2,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year,
                    ExpectedReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2018, MonthNumber.Four), new FiscalMonth(2019, MonthNumber.Eleven))
                }
            };

            // Act, Assert
            foreach (var test in tests)
            {
                var actualReportingPeriod = test.ReportingPeriod.CloneWithAdjustment<ReportingPeriod<FiscalMonth>>(ReportingPeriodComponent.Both, test.UnitsToAdd, test.GranularityOfUnitsToAdd);
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
                    ReportingPeriod = new ReportingPeriod<FiscalUnitOfTime>(new FiscalMonth(2016, MonthNumber.Four), new FiscalUnbounded()),
                    UnitsToAdd = 1,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Month,
                    ExpectedReportingPeriod = new ReportingPeriod<FiscalUnitOfTime>(new FiscalMonth(2016, MonthNumber.Five), new FiscalUnbounded())
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalUnitOfTime>(new FiscalMonth(2016, MonthNumber.Four), new FiscalUnbounded()),
                    UnitsToAdd = -2,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Quarter,
                    ExpectedReportingPeriod = new ReportingPeriod<FiscalUnitOfTime>(new FiscalMonth(2015, MonthNumber.Ten), new FiscalUnbounded())
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalUnitOfTime>(new FiscalMonth(2014, MonthNumber.Four), new FiscalUnbounded()),
                    UnitsToAdd = 2,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year,
                    ExpectedReportingPeriod = new ReportingPeriod<FiscalUnitOfTime>(new FiscalMonth(2016, MonthNumber.Four), new FiscalUnbounded())
                }
            };

            // Act, Assert
            foreach (var test in tests)
            {
                var actualReportingPeriod = test.ReportingPeriod.CloneWithAdjustment<ReportingPeriod<FiscalUnitOfTime>>(ReportingPeriodComponent.Start, test.UnitsToAdd, test.GranularityOfUnitsToAdd);
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
                    ReportingPeriod = new ReportingPeriod<FiscalUnitOfTime>(new FiscalUnbounded(), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = 1,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Month,
                    ExpectedReportingPeriod = new ReportingPeriod<FiscalUnitOfTime>(new FiscalUnbounded(), new FiscalMonth(2017, MonthNumber.Twelve))
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalUnitOfTime>(new FiscalUnbounded(), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = -2,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Quarter,
                    ExpectedReportingPeriod = new ReportingPeriod<FiscalUnitOfTime>(new FiscalUnbounded(), new FiscalMonth(2017, MonthNumber.Five))
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalUnitOfTime>(new FiscalUnbounded(), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = 2,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year,
                    ExpectedReportingPeriod = new ReportingPeriod<FiscalUnitOfTime>(new FiscalUnbounded(), new FiscalMonth(2019, MonthNumber.Eleven))
                }
            };

            // Act, Assert
            foreach (var test in tests)
            {
                var actualReportingPeriod = test.ReportingPeriod.CloneWithAdjustment<ReportingPeriod<FiscalUnitOfTime>>(ReportingPeriodComponent.End, test.UnitsToAdd, test.GranularityOfUnitsToAdd);
                actualReportingPeriod.Should().Be(test.ExpectedReportingPeriod);
            }
        }

        [Fact]
        public static void CloneWithAdjustment___Should_convert_return_type_to_TReportingPeriod___When_called()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = 1,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Month,
                    ExpectedReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Five), new FiscalMonth(2017, MonthNumber.Twelve))
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = -2,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Quarter,
                    ExpectedReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2015, MonthNumber.Ten), new FiscalMonth(2017, MonthNumber.Five))
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = 2,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year,
                    ExpectedReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2018, MonthNumber.Four), new FiscalMonth(2019, MonthNumber.Eleven))
                }
            };

            // Act, Assert
            foreach (var test in tests)
            {
                var actualReportingPeriod1 = test.ReportingPeriod.CloneWithAdjustment<ReportingPeriod<FiscalUnitOfTime>>(ReportingPeriodComponent.Both, test.UnitsToAdd, test.GranularityOfUnitsToAdd);
                var actualReportingPeriod2 = test.ReportingPeriod.CloneWithAdjustment<ReportingPeriod<UnitOfTime>>(ReportingPeriodComponent.Both, test.UnitsToAdd, test.GranularityOfUnitsToAdd);
                actualReportingPeriod1.Should().Be(test.ExpectedReportingPeriod);
                actualReportingPeriod2.Should().Be(test.ExpectedReportingPeriod);
            }
        }

        [Fact]
        public static void CloneWithAdjustment___Should_throw_InvalidOperationException___When_adjusted_reporting_period_cannot_be_converted_to_return_type_TReportingPeriod___When_called()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = 1,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Month,
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = -2,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Quarter,
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = 2,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year,
                }
            };

            // Act, Assert
            foreach (var test in tests)
            {
                var ex1 = Record.Exception(() => test.ReportingPeriod.CloneWithAdjustment<ReportingPeriod<CalendarUnitOfTime>>(ReportingPeriodComponent.Both, test.UnitsToAdd, test.GranularityOfUnitsToAdd));
                var ex2 = Record.Exception(() => test.ReportingPeriod.CloneWithAdjustment<ReportingPeriod<GenericMonth>>(ReportingPeriodComponent.Both, test.UnitsToAdd, test.GranularityOfUnitsToAdd));
                ex1.Should().BeOfType<InvalidOperationException>();
                ex2.Should().BeOfType<InvalidOperationException>();
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
                    ReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2016, MonthNumber.Eleven)),
                    UnitsToAdd = 8,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Month
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Three), new FiscalMonth(2016, MonthNumber.Eleven)),
                    UnitsToAdd = 3,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Quarter
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2014, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Three)),
                    UnitsToAdd = 3,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year
                }
            };

            // Act, Assert
            foreach (var test in tests)
            {
                var ex = Record.Exception(() => test.ReportingPeriod.CloneWithAdjustment<ReportingPeriod<FiscalMonth>>(ReportingPeriodComponent.Start, test.UnitsToAdd, test.GranularityOfUnitsToAdd));
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
                    ReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2016, MonthNumber.Eleven)),
                    UnitsToAdd = -8,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Month
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Three), new FiscalMonth(2016, MonthNumber.Eleven)),
                    UnitsToAdd = -3,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Quarter
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2014, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Three)),
                    UnitsToAdd = -3,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year
                }
            };

            // Act, Assert
            foreach (var test in tests)
            {
                var ex = Record.Exception(() => test.ReportingPeriod.CloneWithAdjustment<ReportingPeriod<FiscalMonth>>(ReportingPeriodComponent.End, test.UnitsToAdd, test.GranularityOfUnitsToAdd));
                ex.Should().BeOfType<InvalidOperationException>();
            }
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_throw_ArgumentNullException___When_parameter_reportingPeriod_is_null()
        {
            // Arrange
            var unitOfTime = A.Dummy<UnitOfTime>();

            // Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.Contains(null, unitOfTime));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_throw_ArgumentNullException___When_parameter_unitOfTime_is_null()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod<UnitOfTime>>();
            UnitOfTime unitOfTime = null;

            // Act
            // ReSharper disable ExpressionIsAlwaysNull
            var ex = Record.Exception(() => reportingPeriod.Contains(unitOfTime));
            // ReSharper restore ExpressionIsAlwaysNull

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_throw_ArgumentException___When_parameter_unitOfTime_has_a_different_UnitOfTimeKind_than_reportingPeriod()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod<UnitOfTime>>();
            var unitOfTime = A.Dummy<UnitOfTime>().Whose(_ => _.UnitOfTimeKind != reportingPeriod.GetUnitOfTimeKind());

            // Act
            var ex = Record.Exception(() => reportingPeriod.Contains(unitOfTime));

            // Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_false___When_parameter_unitOfTime_is_not_contained_within_reportingPeriod_and_both_represent_CalendarDay()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Thirty));
            var reportingPeriod2 = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight));

            var unitOfTime1a = new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentySeven);
            var unitOfTime1b = new CalendarDay(2016, MonthOfYear.March, DayOfMonth.ThirtyOne);
            var unitOfTime1c = new CalendarDay(2016, MonthOfYear.February, DayOfMonth.Ten);
            var unitOfTime1d = new CalendarDay(2017, MonthOfYear.March, DayOfMonth.One);
            var unitOfTime1e = new CalendarDay(2015, MonthOfYear.March, DayOfMonth.One);

            var unitOfTime2a = new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentySeven);
            var unitOfTime2b = new CalendarDay(2016, MonthOfYear.March, DayOfMonth.ThirtyOne);
            var unitOfTime2c = new CalendarDay(2017, MonthOfYear.March, DayOfMonth.TwentyEight);
            var unitOfTime2d = new CalendarDay(2016, MonthOfYear.March, DayOfMonth.TwentyEight);

            // Act
            var contains1a = reportingPeriod1.Contains(unitOfTime1a);
            var contains1b = reportingPeriod1.Contains(unitOfTime1b);
            var contains1c = reportingPeriod1.Contains(unitOfTime1c);
            var contains1d = reportingPeriod1.Contains(unitOfTime1d);
            var contains1e = reportingPeriod1.Contains(unitOfTime1e);

            var contains2a = reportingPeriod2.Contains(unitOfTime2a);
            var contains2b = reportingPeriod2.Contains(unitOfTime2b);
            var contains2c = reportingPeriod2.Contains(unitOfTime2c);
            var contains2d = reportingPeriod2.Contains(unitOfTime2d);

            // Assert
            contains1a.Should().BeFalse();
            contains1b.Should().BeFalse();
            contains1c.Should().BeFalse();
            contains1d.Should().BeFalse();
            contains1e.Should().BeFalse();

            contains2a.Should().BeFalse();
            contains2b.Should().BeFalse();
            contains2c.Should().BeFalse();
            contains2d.Should().BeFalse();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_contained_within_reportingPeriod_and_both_represent_CalendarDay()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Thirty));
            var reportingPeriod2 = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight));

            var unitOfTime1a = new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight);
            var unitOfTime1b = new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Thirty);
            var unitOfTime1c = new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine);
            var unitOfTime1d = new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Ten);

            var unitOfTime2 = new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight);

            // Act
            var contains1a = reportingPeriod1.Contains(unitOfTime1a);
            var contains1b = reportingPeriod1.Contains(unitOfTime1b);
            var contains1c = reportingPeriod1.Contains(unitOfTime1c);
            var contains1d = reportingPeriod1.Contains(unitOfTime1d);

            var contains2 = reportingPeriod2.Contains(unitOfTime2);

            // Assert
            contains1a.Should().BeTrue();
            contains1b.Should().BeTrue();
            contains1c.Should().BeTrue();
            contains1d.Should().BeTrue();

            contains2.Should().BeTrue();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_false___When_parameter_unitOfTime_is_not_contained_within_reportingPeriod_and_both_represent_CalendarMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.September), new CalendarMonth(2017, MonthOfYear.February));
            var reportingPeriod2 = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.September), new CalendarMonth(2016, MonthOfYear.September));

            var unitOfTime1a = new CalendarMonth(2016, MonthOfYear.August);
            var unitOfTime1b = new CalendarMonth(2017, MonthOfYear.March);
            var unitOfTime1c = new CalendarMonth(2017, MonthOfYear.October);

            var unitOfTime2a = new CalendarMonth(2016, MonthOfYear.August);
            var unitOfTime2b = new CalendarMonth(2016, MonthOfYear.October);
            var unitOfTime2c = new CalendarMonth(2017, MonthOfYear.September);
            var unitOfTime2d = new CalendarMonth(2015, MonthOfYear.September);

            // Act
            var contains1a = reportingPeriod1.Contains(unitOfTime1a);
            var contains1b = reportingPeriod1.Contains(unitOfTime1b);
            var contains1c = reportingPeriod1.Contains(unitOfTime1c);
            var contains2a = reportingPeriod2.Contains(unitOfTime2a);
            var contains2b = reportingPeriod2.Contains(unitOfTime2b);
            var contains2c = reportingPeriod2.Contains(unitOfTime2c);
            var contains2d = reportingPeriod2.Contains(unitOfTime2d);

            // Assert
            contains1a.Should().BeFalse();
            contains1b.Should().BeFalse();
            contains1c.Should().BeFalse();

            contains2a.Should().BeFalse();
            contains2b.Should().BeFalse();
            contains2c.Should().BeFalse();
            contains2d.Should().BeFalse();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_contained_within_reportingPeriod_and_both_represent_CalendarMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.September), new CalendarMonth(2017, MonthOfYear.February));
            var reportingPeriod2 = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.September), new CalendarMonth(2016, MonthOfYear.September));

            var unitOfTime1a = new CalendarMonth(2016, MonthOfYear.September);
            var unitOfTime1b = new CalendarMonth(2017, MonthOfYear.February);
            var unitOfTime1c = new CalendarMonth(2016, MonthOfYear.December);

            var unitOfTime2 = new CalendarMonth(2016, MonthOfYear.September);

            // Act
            var contains1a = reportingPeriod1.Contains(unitOfTime1a);
            var contains1b = reportingPeriod1.Contains(unitOfTime1b);
            var contains1c = reportingPeriod1.Contains(unitOfTime1c);

            var contains2 = reportingPeriod2.Contains(unitOfTime2);

            // Assert
            contains1a.Should().BeTrue();
            contains1b.Should().BeTrue();
            contains1c.Should().BeTrue();

            contains2.Should().BeTrue();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_false___When_parameter_unitOfTime_is_not_contained_within_reportingPeriod_and_both_represent_FiscalMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Nine), new FiscalMonth(2017, MonthNumber.Two));
            var reportingPeriod2 = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Nine), new FiscalMonth(2016, MonthNumber.Nine));

            var unitOfTime1a = new FiscalMonth(2016, MonthNumber.Eight);
            var unitOfTime1b = new FiscalMonth(2017, MonthNumber.Three);
            var unitOfTime1c = new FiscalMonth(2017, MonthNumber.Ten);

            var unitOfTime2a = new FiscalMonth(2016, MonthNumber.Eight);
            var unitOfTime2b = new FiscalMonth(2016, MonthNumber.Ten);
            var unitOfTime2c = new FiscalMonth(2017, MonthNumber.Nine);
            var unitOfTime2d = new FiscalMonth(2015, MonthNumber.Nine);

            // Act
            var contains1a = reportingPeriod1.Contains(unitOfTime1a);
            var contains1b = reportingPeriod1.Contains(unitOfTime1b);
            var contains1c = reportingPeriod1.Contains(unitOfTime1c);

            var contains2a = reportingPeriod2.Contains(unitOfTime2a);
            var contains2b = reportingPeriod2.Contains(unitOfTime2b);
            var contains2c = reportingPeriod2.Contains(unitOfTime2c);
            var contains2d = reportingPeriod2.Contains(unitOfTime2d);

            // Assert
            contains1a.Should().BeFalse();
            contains1b.Should().BeFalse();
            contains1c.Should().BeFalse();

            contains2a.Should().BeFalse();
            contains2b.Should().BeFalse();
            contains2c.Should().BeFalse();
            contains2d.Should().BeFalse();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_contained_within_reportingPeriod_and_both_represent_FiscalMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Nine), new FiscalMonth(2017, MonthNumber.Two));
            var reportingPeriod2 = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Nine), new FiscalMonth(2016, MonthNumber.Nine));

            var unitOfTime1a = new FiscalMonth(2016, MonthNumber.Nine);
            var unitOfTime1b = new FiscalMonth(2017, MonthNumber.Two);
            var unitOfTime1c = new FiscalMonth(2016, MonthNumber.Twelve);

            var unitOfTime2 = new FiscalMonth(2016, MonthNumber.Nine);

            // Act
            var contains1a = reportingPeriod1.Contains(unitOfTime1a);
            var contains1b = reportingPeriod1.Contains(unitOfTime1b);
            var contains1c = reportingPeriod1.Contains(unitOfTime1c);

            var contains2 = reportingPeriod2.Contains(unitOfTime2);

            // Assert
            contains1a.Should().BeTrue();
            contains1b.Should().BeTrue();
            contains1c.Should().BeTrue();

            contains2.Should().BeTrue();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_false___When_parameter_unitOfTime_is_not_contained_within_reportingPeriod_and_both_represent_GenericMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Nine), new GenericMonth(2017, MonthNumber.Two));
            var reportingPeriod2 = new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Nine), new GenericMonth(2016, MonthNumber.Nine));

            var unitOfTime1a = new GenericMonth(2016, MonthNumber.Eight);
            var unitOfTime1b = new GenericMonth(2017, MonthNumber.Three);
            var unitOfTime1c = new GenericMonth(2017, MonthNumber.Ten);

            var unitOfTime2a = new GenericMonth(2016, MonthNumber.Eight);
            var unitOfTime2b = new GenericMonth(2016, MonthNumber.Ten);
            var unitOfTime2c = new GenericMonth(2017, MonthNumber.Nine);
            var unitOfTime2d = new GenericMonth(2015, MonthNumber.Nine);

            // Act
            var contains1a = reportingPeriod1.Contains(unitOfTime1a);
            var contains1b = reportingPeriod1.Contains(unitOfTime1b);
            var contains1c = reportingPeriod1.Contains(unitOfTime1c);
            var contains2a = reportingPeriod2.Contains(unitOfTime2a);
            var contains2b = reportingPeriod2.Contains(unitOfTime2b);
            var contains2c = reportingPeriod2.Contains(unitOfTime2c);
            var contains2d = reportingPeriod2.Contains(unitOfTime2d);

            // Assert
            contains1a.Should().BeFalse();
            contains1b.Should().BeFalse();
            contains1c.Should().BeFalse();

            contains2a.Should().BeFalse();
            contains2b.Should().BeFalse();
            contains2c.Should().BeFalse();
            contains2d.Should().BeFalse();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_contained_within_reportingPeriod_and_both_represent_GenericMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Nine), new GenericMonth(2017, MonthNumber.Two));
            var reportingPeriod2 = new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Nine), new GenericMonth(2016, MonthNumber.Nine));

            var unitOfTime1a = new GenericMonth(2016, MonthNumber.Nine);
            var unitOfTime1b = new GenericMonth(2017, MonthNumber.Two);
            var unitOfTime1c = new GenericMonth(2016, MonthNumber.Twelve);

            var unitOfTime2 = new GenericMonth(2016, MonthNumber.Nine);

            // Act
            var contains1a = reportingPeriod1.Contains(unitOfTime1a);
            var contains1b = reportingPeriod1.Contains(unitOfTime1b);
            var contains1c = reportingPeriod1.Contains(unitOfTime1c);

            var contains2 = reportingPeriod2.Contains(unitOfTime2);

            // Assert
            contains1a.Should().BeTrue();
            contains1b.Should().BeTrue();
            contains1c.Should().BeTrue();

            contains2.Should().BeTrue();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_false___When_parameter_unitOfTime_is_not_contained_within_reportingPeriod_and_both_represent_CalendarQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2017, QuarterNumber.Q3));
            var reportingPeriod2 = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q2));

            var unitOfTime1a = new CalendarQuarter(2016, QuarterNumber.Q1);
            var unitOfTime1b = new CalendarQuarter(2017, QuarterNumber.Q4);
            var unitOfTime1c = new CalendarQuarter(2015, QuarterNumber.Q2);

            var unitOfTime2a = new CalendarQuarter(2016, QuarterNumber.Q1);
            var unitOfTime2b = new CalendarQuarter(2016, QuarterNumber.Q3);
            var unitOfTime2c = new CalendarQuarter(2017, QuarterNumber.Q2);
            var unitOfTime2d = new CalendarQuarter(2015, QuarterNumber.Q2);

            // Act
            var contains1a = reportingPeriod1.Contains(unitOfTime1a);
            var contains1b = reportingPeriod1.Contains(unitOfTime1b);
            var contains1c = reportingPeriod1.Contains(unitOfTime1c);

            var contains2a = reportingPeriod2.Contains(unitOfTime2a);
            var contains2b = reportingPeriod2.Contains(unitOfTime2b);
            var contains2c = reportingPeriod2.Contains(unitOfTime2c);
            var contains2d = reportingPeriod2.Contains(unitOfTime2d);

            // Assert
            contains1a.Should().BeFalse();
            contains1b.Should().BeFalse();
            contains1c.Should().BeFalse();

            contains2a.Should().BeFalse();
            contains2b.Should().BeFalse();
            contains2c.Should().BeFalse();
            contains2d.Should().BeFalse();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_contained_within_reportingPeriod_and_both_represent_CalendarQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2017, QuarterNumber.Q3));
            var reportingPeriod2 = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q2));

            var unitOfTime1a = new CalendarQuarter(2016, QuarterNumber.Q2);
            var unitOfTime1b = new CalendarQuarter(2016, QuarterNumber.Q3);
            var unitOfTime1c = new CalendarQuarter(2016, QuarterNumber.Q4);
            var unitOfTime1d = new CalendarQuarter(2017, QuarterNumber.Q1);
            var unitOfTime1e = new CalendarQuarter(2017, QuarterNumber.Q2);
            var unitOfTime1f = new CalendarQuarter(2017, QuarterNumber.Q3);

            var unitOfTime2 = new CalendarQuarter(2016, QuarterNumber.Q2);

            // Act
            var contains1a = reportingPeriod1.Contains(unitOfTime1a);
            var contains1b = reportingPeriod1.Contains(unitOfTime1b);
            var contains1c = reportingPeriod1.Contains(unitOfTime1c);
            var contains1d = reportingPeriod1.Contains(unitOfTime1d);
            var contains1e = reportingPeriod1.Contains(unitOfTime1e);
            var contains1f = reportingPeriod1.Contains(unitOfTime1f);

            var contains2 = reportingPeriod2.Contains(unitOfTime2);

            // Assert
            contains1a.Should().BeTrue();
            contains1b.Should().BeTrue();
            contains1c.Should().BeTrue();
            contains1d.Should().BeTrue();
            contains1e.Should().BeTrue();
            contains1f.Should().BeTrue();

            contains2.Should().BeTrue();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_false___When_parameter_unitOfTime_is_not_contained_within_reportingPeriod_and_both_represent_FiscalQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2017, QuarterNumber.Q3));
            var reportingPeriod2 = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q2));

            var unitOfTime1a = new FiscalQuarter(2016, QuarterNumber.Q1);
            var unitOfTime1b = new FiscalQuarter(2017, QuarterNumber.Q4);
            var unitOfTime1c = new FiscalQuarter(2015, QuarterNumber.Q2);

            var unitOfTime2a = new FiscalQuarter(2016, QuarterNumber.Q1);
            var unitOfTime2b = new FiscalQuarter(2016, QuarterNumber.Q3);
            var unitOfTime2c = new FiscalQuarter(2017, QuarterNumber.Q2);
            var unitOfTime2d = new FiscalQuarter(2015, QuarterNumber.Q2);

            // Act
            var contains1a = reportingPeriod1.Contains(unitOfTime1a);
            var contains1b = reportingPeriod1.Contains(unitOfTime1b);
            var contains1c = reportingPeriod1.Contains(unitOfTime1c);

            var contains2a = reportingPeriod2.Contains(unitOfTime2a);
            var contains2b = reportingPeriod2.Contains(unitOfTime2b);
            var contains2c = reportingPeriod2.Contains(unitOfTime2c);
            var contains2d = reportingPeriod2.Contains(unitOfTime2d);

            // Assert
            contains1a.Should().BeFalse();
            contains1b.Should().BeFalse();
            contains1c.Should().BeFalse();

            contains2a.Should().BeFalse();
            contains2b.Should().BeFalse();
            contains2c.Should().BeFalse();
            contains2d.Should().BeFalse();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_contained_within_reportingPeriod_and_both_represent_FiscalQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2017, QuarterNumber.Q3));
            var reportingPeriod2 = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q2));

            var unitOfTime1a = new FiscalQuarter(2016, QuarterNumber.Q2);
            var unitOfTime1b = new FiscalQuarter(2016, QuarterNumber.Q3);
            var unitOfTime1c = new FiscalQuarter(2016, QuarterNumber.Q4);
            var unitOfTime1d = new FiscalQuarter(2017, QuarterNumber.Q1);
            var unitOfTime1e = new FiscalQuarter(2017, QuarterNumber.Q2);
            var unitOfTime1f = new FiscalQuarter(2017, QuarterNumber.Q3);

            var unitOfTime2 = new FiscalQuarter(2016, QuarterNumber.Q2);

            // Act
            var contains1a = reportingPeriod1.Contains(unitOfTime1a);
            var contains1b = reportingPeriod1.Contains(unitOfTime1b);
            var contains1c = reportingPeriod1.Contains(unitOfTime1c);
            var contains1d = reportingPeriod1.Contains(unitOfTime1d);
            var contains1e = reportingPeriod1.Contains(unitOfTime1e);
            var contains1f = reportingPeriod1.Contains(unitOfTime1f);

            var contains2 = reportingPeriod2.Contains(unitOfTime2);

            // Assert
            contains1a.Should().BeTrue();
            contains1b.Should().BeTrue();
            contains1c.Should().BeTrue();
            contains1d.Should().BeTrue();
            contains1e.Should().BeTrue();
            contains1f.Should().BeTrue();

            contains2.Should().BeTrue();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_false___When_parameter_unitOfTime_is_not_contained_within_reportingPeriod_and_both_represent_GenericQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2017, QuarterNumber.Q3));
            var reportingPeriod2 = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q2));

            var unitOfTime1a = new GenericQuarter(2016, QuarterNumber.Q1);
            var unitOfTime1b = new GenericQuarter(2017, QuarterNumber.Q4);
            var unitOfTime1c = new GenericQuarter(2015, QuarterNumber.Q2);

            var unitOfTime2a = new GenericQuarter(2016, QuarterNumber.Q1);
            var unitOfTime2b = new GenericQuarter(2016, QuarterNumber.Q3);
            var unitOfTime2c = new GenericQuarter(2017, QuarterNumber.Q2);
            var unitOfTime2d = new GenericQuarter(2015, QuarterNumber.Q2);

            // Act
            var contains1a = reportingPeriod1.Contains(unitOfTime1a);
            var contains1b = reportingPeriod1.Contains(unitOfTime1b);
            var contains1c = reportingPeriod1.Contains(unitOfTime1c);

            var contains2a = reportingPeriod2.Contains(unitOfTime2a);
            var contains2b = reportingPeriod2.Contains(unitOfTime2b);
            var contains2c = reportingPeriod2.Contains(unitOfTime2c);
            var contains2d = reportingPeriod2.Contains(unitOfTime2d);

            // Assert
            contains1a.Should().BeFalse();
            contains1b.Should().BeFalse();
            contains1c.Should().BeFalse();

            contains2a.Should().BeFalse();
            contains2b.Should().BeFalse();
            contains2c.Should().BeFalse();
            contains2d.Should().BeFalse();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_contained_within_reportingPeriod_and_both_represent_GenericQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2017, QuarterNumber.Q3));
            var reportingPeriod2 = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q2));

            var unitOfTime1a = new GenericQuarter(2016, QuarterNumber.Q2);
            var unitOfTime1b = new GenericQuarter(2016, QuarterNumber.Q3);
            var unitOfTime1c = new GenericQuarter(2016, QuarterNumber.Q4);
            var unitOfTime1d = new GenericQuarter(2017, QuarterNumber.Q1);
            var unitOfTime1e = new GenericQuarter(2017, QuarterNumber.Q2);
            var unitOfTime1f = new GenericQuarter(2017, QuarterNumber.Q3);

            var unitOfTime2 = new GenericQuarter(2016, QuarterNumber.Q2);

            // Act
            var contains1a = reportingPeriod1.Contains(unitOfTime1a);
            var contains1b = reportingPeriod1.Contains(unitOfTime1b);
            var contains1c = reportingPeriod1.Contains(unitOfTime1c);
            var contains1d = reportingPeriod1.Contains(unitOfTime1d);
            var contains1e = reportingPeriod1.Contains(unitOfTime1e);
            var contains1f = reportingPeriod1.Contains(unitOfTime1f);

            var contains2 = reportingPeriod2.Contains(unitOfTime2);

            // Assert
            contains1a.Should().BeTrue();
            contains1b.Should().BeTrue();
            contains1c.Should().BeTrue();
            contains1d.Should().BeTrue();
            contains1e.Should().BeTrue();
            contains1f.Should().BeTrue();

            contains2.Should().BeTrue();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_false___When_parameter_unitOfTime_is_not_contained_within_reportingPeriod_and_both_represent_CalendarYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2018));
            var reportingPeriod2 = new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2016));

            var unitOfTime1a = new CalendarYear(2015);
            var unitOfTime1b = new CalendarYear(2019);

            var unitOfTime2a = new CalendarYear(2015);
            var unitOfTime2b = new CalendarYear(2017);

            // Act
            var contains1a = reportingPeriod1.Contains(unitOfTime1a);
            var contains1b = reportingPeriod1.Contains(unitOfTime1b);

            var contains2a = reportingPeriod2.Contains(unitOfTime2a);
            var contains2b = reportingPeriod2.Contains(unitOfTime2b);

            // Assert
            contains1a.Should().BeFalse();
            contains1b.Should().BeFalse();

            contains2a.Should().BeFalse();
            contains2b.Should().BeFalse();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_contained_within_reportingPeriod_and_both_represent_CalendarYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2018));
            var reportingPeriod2 = new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2016));

            var unitOfTime1a = new CalendarYear(2016);
            var unitOfTime1b = new CalendarYear(2017);
            var unitOfTime1c = new CalendarYear(2018);

            var unitOfTime2 = new CalendarYear(2016);

            // Act
            var contains1a = reportingPeriod1.Contains(unitOfTime1a);
            var contains1b = reportingPeriod1.Contains(unitOfTime1b);
            var contains1c = reportingPeriod1.Contains(unitOfTime1c);

            var contains2 = reportingPeriod2.Contains(unitOfTime2);

            // Assert
            contains1a.Should().BeTrue();
            contains1b.Should().BeTrue();
            contains1c.Should().BeTrue();

            contains2.Should().BeTrue();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_false___When_parameter_unitOfTime_is_not_contained_within_reportingPeriod_and_both_represent_FiscalYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2018));
            var reportingPeriod2 = new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2016));

            var unitOfTime1a = new FiscalYear(2015);
            var unitOfTime1b = new FiscalYear(2019);

            var unitOfTime2a = new FiscalYear(2015);
            var unitOfTime2b = new FiscalYear(2017);

            // Act
            var contains1a = reportingPeriod1.Contains(unitOfTime1a);
            var contains1b = reportingPeriod1.Contains(unitOfTime1b);
            var contains2a = reportingPeriod2.Contains(unitOfTime2a);
            var contains2b = reportingPeriod2.Contains(unitOfTime2b);

            // Assert
            contains1a.Should().BeFalse();
            contains1b.Should().BeFalse();

            contains2a.Should().BeFalse();
            contains2b.Should().BeFalse();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_contained_within_reportingPeriod_and_both_represent_FiscalYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2018));
            var reportingPeriod2 = new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2016));

            var unitOfTime1a = new FiscalYear(2016);
            var unitOfTime1b = new FiscalYear(2017);
            var unitOfTime1c = new FiscalYear(2018);

            var unitOfTime2 = new FiscalYear(2016);

            // Act
            var contains1a = reportingPeriod1.Contains(unitOfTime1a);
            var contains1b = reportingPeriod1.Contains(unitOfTime1b);
            var contains1c = reportingPeriod1.Contains(unitOfTime1c);

            var contains2 = reportingPeriod2.Contains(unitOfTime2);

            // Assert
            contains1a.Should().BeTrue();
            contains1b.Should().BeTrue();
            contains1c.Should().BeTrue();

            contains2.Should().BeTrue();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_false___When_parameter_unitOfTime_is_not_contained_within_reportingPeriod_and_both_represent_GenericYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2018));
            var reportingPeriod2 = new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2016));

            var unitOfTime1a = new GenericYear(2015);
            var unitOfTime1b = new GenericYear(2019);

            var unitOfTime2a = new GenericYear(2015);
            var unitOfTime2b = new GenericYear(2017);

            // Act
            var contains1a = reportingPeriod1.Contains(unitOfTime1a);
            var contains1b = reportingPeriod1.Contains(unitOfTime1b);

            var contains2a = reportingPeriod2.Contains(unitOfTime2a);
            var contains2b = reportingPeriod2.Contains(unitOfTime2b);

            // Assert
            contains1a.Should().BeFalse();
            contains1b.Should().BeFalse();

            contains2a.Should().BeFalse();
            contains2b.Should().BeFalse();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_contained_within_reportingPeriod_and_both_represent_GenericYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2018));
            var reportingPeriod2 = new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2016));

            var unitOfTime1a = new GenericYear(2016);
            var unitOfTime1b = new GenericYear(2017);
            var unitOfTime1c = new GenericYear(2018);

            var unitOfTime2 = new GenericYear(2016);

            // Act
            var contains1a = reportingPeriod1.Contains(unitOfTime1a);
            var contains1b = reportingPeriod1.Contains(unitOfTime1b);
            var contains1c = reportingPeriod1.Contains(unitOfTime1c);

            var contains2 = reportingPeriod2.Contains(unitOfTime2);

            // Assert
            contains1a.Should().BeTrue();
            contains1b.Should().BeTrue();
            contains1c.Should().BeTrue();

            contains2.Should().BeTrue();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_not_contained_within_reportingPeriod_and_both_represent_CalendarUnbounded()
        {
            // Arrange
            var reportingPeriod = new ReportingPeriod<CalendarUnbounded>(new CalendarUnbounded(), new CalendarUnbounded());
            var unitOfTime = new CalendarUnbounded();

            // Act
            var result = reportingPeriod.Contains(unitOfTime);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_not_contained_within_reportingPeriod_and_both_represent_FiscalUnbounded()
        {
            // Arrange
            var reportingPeriod = new ReportingPeriod<FiscalUnbounded>(new FiscalUnbounded(), new FiscalUnbounded());
            var unitOfTime = new FiscalUnbounded();

            // Act
            var result = reportingPeriod.Contains(unitOfTime);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_not_contained_within_reportingPeriod_and_both_represent_GenericUnbounded()
        {
            // Arrange
            var reportingPeriod = new ReportingPeriod<GenericUnbounded>(new GenericUnbounded(), new GenericUnbounded());
            var unitOfTime = new GenericUnbounded();

            // Act
            var result = reportingPeriod.Contains(unitOfTime);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_false___When_parameter_unitOfTime_is_not_contained_within_a_CalendarDay_reportingPeriod_and_unitOfTime_is_not_a_CalendarDay()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.February, DayOfMonth.Two),
                        new CalendarDay(2016, MonthOfYear.March, DayOfMonth.ThirtyOne)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2016, MonthOfYear.February)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.February, DayOfMonth.One),
                        new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Thirty)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2016, MonthOfYear.March)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.February, DayOfMonth.One),
                        new CalendarDay(2016, MonthOfYear.March, DayOfMonth.ThirtyOne)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2016, MonthOfYear.January)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.February, DayOfMonth.One),
                        new CalendarDay(2016, MonthOfYear.March, DayOfMonth.ThirtyOne)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2016, MonthOfYear.April)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Thirty)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2016, MonthOfYear.March)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Thirty)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2016, MonthOfYear.April)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Thirty)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2017, MonthOfYear.January)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Two),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2016, MonthOfYear.March)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2016, MonthOfYear.January)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2015, MonthOfYear.March)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.January, DayOfMonth.Two),
                        new CalendarDay(2016, MonthOfYear.September, DayOfMonth.Thirty)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.January, DayOfMonth.One),
                        new CalendarDay(2016, MonthOfYear.September, DayOfMonth.TwentyNine)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q3)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.January, DayOfMonth.One),
                        new CalendarDay(2016, MonthOfYear.September, DayOfMonth.Thirty)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2015, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.January, DayOfMonth.One),
                        new CalendarDay(2016, MonthOfYear.September, DayOfMonth.Thirty)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarDay(2016, MonthOfYear.September, DayOfMonth.TwentyNine)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q3)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarDay(2016, MonthOfYear.September, DayOfMonth.Thirty)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarDay(2016, MonthOfYear.September, DayOfMonth.Thirty)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2017, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.October, DayOfMonth.Two),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.October, DayOfMonth.One),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q3)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.October, DayOfMonth.One),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2015, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.January, DayOfMonth.One),
                        new CalendarDay(2017, MonthOfYear.December, DayOfMonth.Thirty)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.January, DayOfMonth.Two),
                        new CalendarDay(2017, MonthOfYear.December, DayOfMonth.ThirtyOne)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.January, DayOfMonth.One),
                        new CalendarDay(2017, MonthOfYear.December, DayOfMonth.ThirtyOne)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2015)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.January, DayOfMonth.One),
                        new CalendarDay(2017, MonthOfYear.December, DayOfMonth.ThirtyOne)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2018)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarDay(2017, MonthOfYear.December, DayOfMonth.Thirty)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarDay(2017, MonthOfYear.December, DayOfMonth.ThirtyOne)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2018)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Two),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2017, MonthOfYear.December, DayOfMonth.ThirtyOne),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.January, DayOfMonth.One),
                        new CalendarDay(2017, MonthOfYear.December, DayOfMonth.ThirtyOne)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarUnbounded()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarDay(2017, MonthOfYear.December, DayOfMonth.ThirtyOne)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarUnbounded()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2017, MonthOfYear.December, DayOfMonth.ThirtyOne),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarUnbounded()
                }
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_contained_within_a_CalendarDay_reportingPeriod_and_unitOfTime_is_not_a_CalendarDay()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.February, DayOfMonth.Two),
                        new CalendarDay(2016, MonthOfYear.March, DayOfMonth.ThirtyOne)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2016, MonthOfYear.March)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.February, DayOfMonth.One),
                        new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Thirty)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2016, MonthOfYear.February)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarDay(2016, MonthOfYear.March, DayOfMonth.ThirtyOne)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2016, MonthOfYear.March)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarDay(2016, MonthOfYear.March, DayOfMonth.ThirtyOne)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2016, MonthOfYear.January)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarDay(2016, MonthOfYear.March, DayOfMonth.ThirtyOne)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2015, MonthOfYear.March)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2016, MonthOfYear.March)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2016, MonthOfYear.April)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2017, MonthOfYear.January)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.January, DayOfMonth.One),
                        new CalendarDay(2016, MonthOfYear.September, DayOfMonth.TwentyNine)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.January, DayOfMonth.Two),
                        new CalendarDay(2016, MonthOfYear.September, DayOfMonth.TwentyNine)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.January, DayOfMonth.One),
                        new CalendarDay(2016, MonthOfYear.September, DayOfMonth.Thirty)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q3)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarDay(2016, MonthOfYear.September, DayOfMonth.Thirty)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q3)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarDay(2016, MonthOfYear.September, DayOfMonth.Thirty)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarDay(2016, MonthOfYear.September, DayOfMonth.Thirty)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2015, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.October, DayOfMonth.One),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.October, DayOfMonth.One),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2017, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.October, DayOfMonth.One),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2017, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.January, DayOfMonth.One),
                        new CalendarDay(2017, MonthOfYear.December, DayOfMonth.Thirty)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2016, MonthOfYear.January, DayOfMonth.Two),
                        new CalendarDay(2017, MonthOfYear.December, DayOfMonth.ThirtyOne)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarDay(2017, MonthOfYear.December, DayOfMonth.ThirtyOne)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarDay(2017, MonthOfYear.December, DayOfMonth.ThirtyOne)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2018)
                }
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_false___When_parameter_unitOfTime_is_not_contained_within_a_CalendarMonth_reportingPeriod_and_unitOfTime_is_not_a_CalendarMonth()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.February),
                        new CalendarMonth(2016, MonthOfYear.March)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2016, MonthOfYear.January, DayOfMonth.ThirtyOne)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.February),
                        new CalendarMonth(2016, MonthOfYear.March)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2016, MonthOfYear.April, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.February),
                        new CalendarMonth(2016, MonthOfYear.March)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2017, MonthOfYear.February, DayOfMonth.Ten)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.February),
                        new CalendarMonth(2016, MonthOfYear.March)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2015, MonthOfYear.February, DayOfMonth.Ten)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarMonth(2016, MonthOfYear.March)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2016, MonthOfYear.April, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarMonth(2016, MonthOfYear.March)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2017, MonthOfYear.March, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.March),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.March),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2015, MonthOfYear.March, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.February),
                        new CalendarMonth(2016, MonthOfYear.June)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.January),
                        new CalendarMonth(2016, MonthOfYear.May)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.January),
                        new CalendarMonth(2016, MonthOfYear.June)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q3)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.January),
                        new CalendarMonth(2016, MonthOfYear.June)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2015, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.January),
                        new CalendarMonth(2016, MonthOfYear.June)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2015, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.January),
                        new CalendarMonth(2016, MonthOfYear.June)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2017, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarMonth(2016, MonthOfYear.June)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q3)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarMonth(2016, MonthOfYear.June)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2017, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.April),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.April),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2015, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.January),
                        new CalendarMonth(2017, MonthOfYear.November)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.February),
                        new CalendarMonth(2017, MonthOfYear.December)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.January),
                        new CalendarMonth(2017, MonthOfYear.December)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2015)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.January),
                        new CalendarMonth(2017, MonthOfYear.December)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2018)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarMonth(2017, MonthOfYear.December)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2018)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.January),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2015)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarMonth(2017, MonthOfYear.December)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarUnbounded()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2017, MonthOfYear.December),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarUnbounded()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.January),
                        new CalendarMonth(2017, MonthOfYear.December)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarUnbounded()
                },
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_contained_within_a_CalendarMonth_reportingPeriod_and_unitOfTime_is_not_a_CalendarMonth()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.February),
                        new CalendarMonth(2016, MonthOfYear.March)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2016, MonthOfYear.February, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.February),
                        new CalendarMonth(2016, MonthOfYear.March)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2016, MonthOfYear.March, DayOfMonth.ThirtyOne)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.February),
                        new CalendarMonth(2016, MonthOfYear.March)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarMonth(2016, MonthOfYear.March)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2016, MonthOfYear.March, DayOfMonth.ThirtyOne)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarMonth(2016, MonthOfYear.March)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarMonth(2016, MonthOfYear.March)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2015, MonthOfYear.December, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.March),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.March),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2016, MonthOfYear.April, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.March),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.February),
                        new CalendarMonth(2016, MonthOfYear.June)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.January),
                        new CalendarMonth(2016, MonthOfYear.May)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.February),
                        new CalendarMonth(2016, MonthOfYear.August)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarMonth(2016, MonthOfYear.August)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarMonth(2016, MonthOfYear.August)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarMonth(2016, MonthOfYear.August)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2015, QuarterNumber.Q3)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.May),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q3)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.May),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.May),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2017, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.January),
                        new CalendarMonth(2017, MonthOfYear.November)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.February),
                        new CalendarMonth(2017, MonthOfYear.December)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarMonth(2017, MonthOfYear.November)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarMonth(2017, MonthOfYear.December)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.January),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarMonth(2016, MonthOfYear.March),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2017)
                },
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_false___When_parameter_unitOfTime_is_not_contained_within_a_CalendarQuarter_reportingPeriod_and_unitOfTime_is_not_a_CalendarQuarter()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Thirty)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2017, MonthOfYear.October, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2018, MonthOfYear.April, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2015, MonthOfYear.April, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2017, MonthOfYear.October, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2018, MonthOfYear.July, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2017, QuarterNumber.Q3),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2017, MonthOfYear.June, DayOfMonth.Thirty)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2017, QuarterNumber.Q3),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2016, MonthOfYear.August, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2016, MonthOfYear.March)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2017, MonthOfYear.October)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2018, MonthOfYear.July)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2015, MonthOfYear.July)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2017, MonthOfYear.October)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2018, MonthOfYear.July)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2016, MonthOfYear.March)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2015, MonthOfYear.June)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2018)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2018)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2019)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2015)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarUnbounded()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarUnbounded()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarUnbounded()
                },
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_contained_within_a_CalendarQuarter_reportingPeriod_and_unitOfTime_is_not_a_CalendarQuarter()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2016, MonthOfYear.April, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2017, MonthOfYear.September, DayOfMonth.Thirty)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2017, MonthOfYear.April, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2017, MonthOfYear.September, DayOfMonth.Thirty)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2017, MonthOfYear.June, DayOfMonth.Thirty)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2017, QuarterNumber.Q3),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2017, MonthOfYear.July, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2017, QuarterNumber.Q3),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2017, MonthOfYear.October, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2016, MonthOfYear.April)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2017, MonthOfYear.September)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2016, MonthOfYear.December)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2017, MonthOfYear.September)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2017, MonthOfYear.April)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2016, MonthOfYear.April)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2016, MonthOfYear.August)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q1),
                        new CalendarQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarQuarter(2018, QuarterNumber.Q4)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2018)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q2),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarQuarter(2016, QuarterNumber.Q1),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarYear(2016)
                }
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_false___When_parameter_unitOfTime_is_not_contained_within_a_CalendarYear_reportingPeriod_and_unitOfTime_is_not_a_CalendarYear()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2016),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2015, MonthOfYear.December, DayOfMonth.ThirtyOne)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2016),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2019, MonthOfYear.January, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2019, MonthOfYear.January, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2018),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2017, MonthOfYear.December, DayOfMonth.ThirtyOne)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2016),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2019, MonthOfYear.January)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2016),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2015, MonthOfYear.December)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2019, MonthOfYear.January)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2018),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2017, MonthOfYear.December)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2016),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2015, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2016),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2019, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2019, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2018),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2017, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2016),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarUnbounded()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarUnbounded()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2016),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarUnbounded()
                }
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_contained_within_a_CalendarYear_reportingPeriod_and_unitOfTime_is_not_a_CalendarYear()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2016),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2016, MonthOfYear.January, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2016),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2018, MonthOfYear.December, DayOfMonth.ThirtyOne)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2016),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2017, MonthOfYear.June, DayOfMonth.Fifteen)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2018, MonthOfYear.December, DayOfMonth.ThirtyOne)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2017, MonthOfYear.June, DayOfMonth.Fifteen)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2018),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2018, MonthOfYear.January, DayOfMonth.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2018),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarDay(2019, MonthOfYear.June, DayOfMonth.Fifteen)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2016),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2016, MonthOfYear.January)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2016),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2018, MonthOfYear.December)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2016),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2017, MonthOfYear.June)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2018, MonthOfYear.December)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2018, MonthOfYear.January)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2017, MonthOfYear.June)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2018),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2018, MonthOfYear.December)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2018),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2018, MonthOfYear.January)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2018),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarMonth(2019, MonthOfYear.June)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2016),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2016, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2016),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2018, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2016),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2017, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2018, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2018, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarYear(2018)),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2017, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2018),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2018, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2018),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2018, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarYear(2018),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)new CalendarQuarter(2019, QuarterNumber.Q2)
                }
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_reportingPeriod_has_CalendarUnbounded_Start_and_End()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)A.Dummy<CalendarDay>()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)A.Dummy<CalendarMonth>()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)A.Dummy<CalendarQuarter>()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<CalendarUnitOfTime>(
                        new CalendarUnbounded(),
                        new CalendarUnbounded()),
                    UnitOfTime = (CalendarUnitOfTime)A.Dummy<CalendarYear>()
                }
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_false___When_parameter_unitOfTime_is_not_contained_within_a_FiscalMonth_reportingPeriod_and_unitOfTime_is_not_a_FiscalMonth()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.Two),
                        new FiscalMonth(2016, MonthNumber.Six)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2016, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.One),
                        new FiscalMonth(2016, MonthNumber.Five)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2016, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.One),
                        new FiscalMonth(2016, MonthNumber.Six)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2016, QuarterNumber.Q3)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.One),
                        new FiscalMonth(2016, MonthNumber.Six)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2015, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.One),
                        new FiscalMonth(2016, MonthNumber.Six)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2015, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.One),
                        new FiscalMonth(2016, MonthNumber.Six)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2017, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalMonth(2016, MonthNumber.Six)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2016, QuarterNumber.Q3)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalMonth(2016, MonthNumber.Six)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2017, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.Four),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2016, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.Four),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2015, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.One),
                        new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.Two),
                        new FiscalMonth(2017, MonthNumber.Twelve)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.One),
                        new FiscalMonth(2017, MonthNumber.Twelve)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2015)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.One),
                        new FiscalMonth(2017, MonthNumber.Twelve)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2018)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalMonth(2017, MonthNumber.Twelve)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2018)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.One),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2015)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalMonth(2017, MonthNumber.Twelve)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalUnbounded()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2017, MonthNumber.Twelve),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalUnbounded()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.One),
                        new FiscalMonth(2017, MonthNumber.Twelve)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalUnbounded()
                },
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_contained_within_a_FiscalMonth_reportingPeriod_and_unitOfTime_is_not_a_FiscalMonth()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.Two),
                        new FiscalMonth(2016, MonthNumber.Six)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2016, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.One),
                        new FiscalMonth(2016, MonthNumber.Five)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2016, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.Two),
                        new FiscalMonth(2016, MonthNumber.Eight)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2016, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalMonth(2016, MonthNumber.Eight)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2016, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalMonth(2016, MonthNumber.Eight)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2016, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalMonth(2016, MonthNumber.Eight)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2015, QuarterNumber.Q3)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.Five),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2016, QuarterNumber.Q3)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.Five),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2016, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.Five),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2017, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.One),
                        new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.Two),
                        new FiscalMonth(2017, MonthNumber.Twelve)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalMonth(2017, MonthNumber.Twelve)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.One),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalMonth(2016, MonthNumber.Three),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2017)
                },
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_false___When_parameter_unitOfTime_is_not_contained_within_a_FiscalQuarter_reportingPeriod_and_unitOfTime_is_not_a_FiscalQuarter()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalQuarter(2016, QuarterNumber.Q2),
                        new FiscalQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2016, MonthNumber.Three)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalQuarter(2016, QuarterNumber.Q2),
                        new FiscalQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2017, MonthNumber.Ten)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalQuarter(2016, QuarterNumber.Q2),
                        new FiscalQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2018, MonthNumber.Seven)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalQuarter(2016, QuarterNumber.Q2),
                        new FiscalQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2015, MonthNumber.Seven)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2017, MonthNumber.Ten)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2018, MonthNumber.Seven)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalQuarter(2016, QuarterNumber.Q2),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2016, MonthNumber.Three)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalQuarter(2016, QuarterNumber.Q2),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2015, MonthNumber.Six)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalQuarter(2016, QuarterNumber.Q2),
                        new FiscalQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalQuarter(2016, QuarterNumber.Q2),
                        new FiscalQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2018)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2018)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2019)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalQuarter(2016, QuarterNumber.Q2),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalQuarter(2016, QuarterNumber.Q2),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2015)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalQuarter(2016, QuarterNumber.Q2),
                        new FiscalQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalUnbounded()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalUnbounded()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalQuarter(2016, QuarterNumber.Q2),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalUnbounded()
                },
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_contained_within_a_FiscalQuarter_reportingPeriod_and_unitOfTime_is_not_a_FiscalQuarter()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalQuarter(2016, QuarterNumber.Q2),
                        new FiscalQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2016, MonthNumber.Four)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalQuarter(2016, QuarterNumber.Q2),
                        new FiscalQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2017, MonthNumber.Nine)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalQuarter(2016, QuarterNumber.Q2),
                        new FiscalQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2016, MonthNumber.Twelve)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2017, MonthNumber.Nine)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2017, MonthNumber.Four)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalQuarter(2016, QuarterNumber.Q2),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2016, MonthNumber.Four)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalQuarter(2016, QuarterNumber.Q2),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2016, MonthNumber.Eight)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalQuarter(2016, QuarterNumber.Q2),
                        new FiscalQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalQuarter(2016, QuarterNumber.Q1),
                        new FiscalQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalQuarter(2018, QuarterNumber.Q4)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2018)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalQuarter(2016, QuarterNumber.Q2),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalQuarter(2016, QuarterNumber.Q1),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalYear(2016)
                }
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_false___When_parameter_unitOfTime_is_not_contained_within_a_FiscalYear_reportingPeriod_and_unitOfTime_is_not_a_FiscalYear()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalYear(2016),
                        new FiscalYear(2018)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2019, MonthNumber.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalYear(2016),
                        new FiscalYear(2018)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2015, MonthNumber.Twelve)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalYear(2018)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2019, MonthNumber.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalYear(2018),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2017, MonthNumber.Twelve)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalYear(2016),
                        new FiscalYear(2018)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2015, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalYear(2016),
                        new FiscalYear(2018)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2019, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalYear(2018)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2019, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalYear(2018),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2017, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalYear(2016),
                        new FiscalYear(2018)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalUnbounded()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalYear(2018)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalUnbounded()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalYear(2016),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalUnbounded()
                }
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_contained_within_a_FiscalYear_reportingPeriod_and_unitOfTime_is_not_a_FiscalYear()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalYear(2016),
                        new FiscalYear(2018)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2016, MonthNumber.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalYear(2016),
                        new FiscalYear(2018)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2018, MonthNumber.Twelve)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalYear(2016),
                        new FiscalYear(2018)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2017, MonthNumber.Six)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalYear(2018)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2018, MonthNumber.Twelve)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalYear(2018)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2018, MonthNumber.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalYear(2018)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2017, MonthNumber.Six)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalYear(2018),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2018, MonthNumber.Twelve)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalYear(2018),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2018, MonthNumber.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalYear(2018),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalMonth(2019, MonthNumber.Six)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalYear(2016),
                        new FiscalYear(2018)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2016, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalYear(2016),
                        new FiscalYear(2018)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2018, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalYear(2016),
                        new FiscalYear(2018)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2017, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalYear(2018)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2018, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalYear(2018)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2018, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalYear(2018)),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2017, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalYear(2018),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2018, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalYear(2018),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2018, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalYear(2018),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)new FiscalQuarter(2019, QuarterNumber.Q2)
                }
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_reportingPeriod_has_FiscalUnbounded_Start_and_End()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)A.Dummy<FiscalMonth>()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)A.Dummy<FiscalQuarter>()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<FiscalUnitOfTime>(
                        new FiscalUnbounded(),
                        new FiscalUnbounded()),
                    UnitOfTime = (FiscalUnitOfTime)A.Dummy<FiscalYear>()
                }
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_false___When_parameter_unitOfTime_is_not_contained_within_a_GenericMonth_reportingPeriod_and_unitOfTime_is_not_a_GenericMonth()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.Two),
                        new GenericMonth(2016, MonthNumber.Six)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2016, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.One),
                        new GenericMonth(2016, MonthNumber.Five)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2016, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.One),
                        new GenericMonth(2016, MonthNumber.Six)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2016, QuarterNumber.Q3)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.One),
                        new GenericMonth(2016, MonthNumber.Six)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2015, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.One),
                        new GenericMonth(2016, MonthNumber.Six)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2015, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.One),
                        new GenericMonth(2016, MonthNumber.Six)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2017, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericMonth(2016, MonthNumber.Six)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2016, QuarterNumber.Q3)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericMonth(2016, MonthNumber.Six)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2017, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.Four),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2016, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.Four),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2015, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.One),
                        new GenericMonth(2017, MonthNumber.Eleven)),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.Two),
                        new GenericMonth(2017, MonthNumber.Twelve)),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.One),
                        new GenericMonth(2017, MonthNumber.Twelve)),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2015)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.One),
                        new GenericMonth(2017, MonthNumber.Twelve)),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2018)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericMonth(2017, MonthNumber.Twelve)),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2018)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.One),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2015)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericMonth(2017, MonthNumber.Twelve)),
                    UnitOfTime = (GenericUnitOfTime)new GenericUnbounded()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2017, MonthNumber.Twelve),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericUnbounded()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.One),
                        new GenericMonth(2017, MonthNumber.Twelve)),
                    UnitOfTime = (GenericUnitOfTime)new GenericUnbounded()
                },
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_contained_within_a_GenericMonth_reportingPeriod_and_unitOfTime_is_not_a_GenericMonth()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.Two),
                        new GenericMonth(2016, MonthNumber.Six)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2016, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.One),
                        new GenericMonth(2016, MonthNumber.Five)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2016, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.Two),
                        new GenericMonth(2016, MonthNumber.Eight)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2016, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericMonth(2016, MonthNumber.Eight)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2016, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericMonth(2016, MonthNumber.Eight)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2016, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericMonth(2016, MonthNumber.Eight)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2015, QuarterNumber.Q3)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.Five),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2016, QuarterNumber.Q3)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.Five),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2016, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.Five),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2017, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.One),
                        new GenericMonth(2017, MonthNumber.Eleven)),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.Two),
                        new GenericMonth(2017, MonthNumber.Twelve)),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericMonth(2017, MonthNumber.Eleven)),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericMonth(2017, MonthNumber.Twelve)),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.One),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericMonth(2016, MonthNumber.Three),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2017)
                },
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_false___When_parameter_unitOfTime_is_not_contained_within_a_GenericQuarter_reportingPeriod_and_unitOfTime_is_not_a_GenericQuarter()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericQuarter(2016, QuarterNumber.Q2),
                        new GenericQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2016, MonthNumber.Three)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericQuarter(2016, QuarterNumber.Q2),
                        new GenericQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2017, MonthNumber.Ten)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericQuarter(2016, QuarterNumber.Q2),
                        new GenericQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2018, MonthNumber.Seven)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericQuarter(2016, QuarterNumber.Q2),
                        new GenericQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2015, MonthNumber.Seven)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2017, MonthNumber.Ten)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2018, MonthNumber.Seven)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericQuarter(2016, QuarterNumber.Q2),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2016, MonthNumber.Three)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericQuarter(2016, QuarterNumber.Q2),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2015, MonthNumber.Six)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericQuarter(2016, QuarterNumber.Q2),
                        new GenericQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericQuarter(2016, QuarterNumber.Q2),
                        new GenericQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2018)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2018)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2019)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericQuarter(2016, QuarterNumber.Q2),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericQuarter(2016, QuarterNumber.Q2),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2015)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericQuarter(2016, QuarterNumber.Q2),
                        new GenericQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (GenericUnitOfTime)new GenericUnbounded()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (GenericUnitOfTime)new GenericUnbounded()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericQuarter(2016, QuarterNumber.Q2),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericUnbounded()
                },
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_contained_within_a_GenericQuarter_reportingPeriod_and_unitOfTime_is_not_a_GenericQuarter()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericQuarter(2016, QuarterNumber.Q2),
                        new GenericQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2016, MonthNumber.Four)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericQuarter(2016, QuarterNumber.Q2),
                        new GenericQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2017, MonthNumber.Nine)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericQuarter(2016, QuarterNumber.Q2),
                        new GenericQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2016, MonthNumber.Twelve)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2017, MonthNumber.Nine)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericQuarter(2017, QuarterNumber.Q3)),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2017, MonthNumber.Four)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericQuarter(2016, QuarterNumber.Q2),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2016, MonthNumber.Four)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericQuarter(2016, QuarterNumber.Q2),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2016, MonthNumber.Eight)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericQuarter(2016, QuarterNumber.Q2),
                        new GenericQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericQuarter(2016, QuarterNumber.Q1),
                        new GenericQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2016)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericQuarter(2018, QuarterNumber.Q3)),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericQuarter(2018, QuarterNumber.Q4)),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2018)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericQuarter(2016, QuarterNumber.Q2),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2017)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericQuarter(2016, QuarterNumber.Q1),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericYear(2016)
                }
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_false___When_parameter_unitOfTime_is_not_contained_within_a_GenericYear_reportingPeriod_and_unitOfTime_is_not_a_GenericYear()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericYear(2016),
                        new GenericYear(2018)),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2019, MonthNumber.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericYear(2016),
                        new GenericYear(2018)),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2015, MonthNumber.Twelve)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericYear(2018)),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2019, MonthNumber.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericYear(2018),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2017, MonthNumber.Twelve)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericYear(2016),
                        new GenericYear(2018)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2015, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericYear(2016),
                        new GenericYear(2018)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2019, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericYear(2018)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2019, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericYear(2018),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2017, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericYear(2016),
                        new GenericYear(2018)),
                    UnitOfTime = (GenericUnitOfTime)new GenericUnbounded()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericYear(2018)),
                    UnitOfTime = (GenericUnitOfTime)new GenericUnbounded()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericYear(2016),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericUnbounded()
                }
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_unitOfTime_is_contained_within_a_GenericYear_reportingPeriod_and_unitOfTime_is_not_a_GenericYear()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericYear(2016),
                        new GenericYear(2018)),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2016, MonthNumber.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericYear(2016),
                        new GenericYear(2018)),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2018, MonthNumber.Twelve)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericYear(2016),
                        new GenericYear(2018)),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2017, MonthNumber.Six)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericYear(2018)),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2018, MonthNumber.Twelve)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericYear(2018)),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2018, MonthNumber.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericYear(2018)),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2017, MonthNumber.Six)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericYear(2018),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2018, MonthNumber.Twelve)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericYear(2018),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2018, MonthNumber.One)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericYear(2018),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericMonth(2019, MonthNumber.Six)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericYear(2016),
                        new GenericYear(2018)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2016, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericYear(2016),
                        new GenericYear(2018)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2018, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericYear(2016),
                        new GenericYear(2018)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2017, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericYear(2018)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2018, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericYear(2018)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2018, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericYear(2018)),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2017, QuarterNumber.Q2)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericYear(2018),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2018, QuarterNumber.Q1)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericYear(2018),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2018, QuarterNumber.Q4)
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericYear(2018),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)new GenericQuarter(2019, QuarterNumber.Q2)
                }
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_unitOfTime___Should_return_true___When_parameter_reportingPeriod_has_GenericUnbounded_Start_and_End()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)A.Dummy<GenericMonth>()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)A.Dummy<GenericQuarter>()
                },
                new
                {
                    ReportingPeriod =
                    new ReportingPeriod<GenericUnitOfTime>(
                        new GenericUnbounded(),
                        new GenericUnbounded()),
                    UnitOfTime = (GenericUnitOfTime)A.Dummy<GenericYear>()
                }
            };

            // Act
            var results = new List<bool>();
            foreach (var test in tests)
            {
                results.Add(test.ReportingPeriod.Contains(test.UnitOfTime));
            }

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_throw_ArgumentNullException___When_parameter_reportingPeriod1_is_null()
        {
            // Arrange
            var reportingPeriod2 = A.Dummy<ReportingPeriod<UnitOfTime>>();

            // Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.Contains(null, reportingPeriod2));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_throw_ArgumentNullException___When_parameter_reportingPeriod2_is_null()
        {
            // Arrange
            var reportingPeriod1 = A.Dummy<ReportingPeriod<UnitOfTime>>();
            IReportingPeriod<UnitOfTime> reportingPeriod2 = null;

            // Act
            // ReSharper disable ExpressionIsAlwaysNull
            var ex = Record.Exception(() => reportingPeriod1.Contains(reportingPeriod2));
            // ReSharper restore ExpressionIsAlwaysNull

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_throw_ArgumentException___When_reporting_periods_have_different_UnitOfTimeKind()
        {
            // Arrange
            var reportingPeriod1 = A.Dummy<ReportingPeriod<UnitOfTime>>();
            var reportingPeriod2 = A.Dummy<ReportingPeriod<UnitOfTime>>().Whose(_ => _.GetUnitOfTimeKind() != reportingPeriod1.GetUnitOfTimeKind());

            // Act
            // ReSharper disable ExpressionIsAlwaysNull
            var ex = Record.Exception(() => reportingPeriod1.Contains(reportingPeriod2));
            // ReSharper restore ExpressionIsAlwaysNull

            // Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_false___When_reportingPeriod1_does_not_contain_reportingPeriod2_an_both_are_of_type_CalendarDay()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Thirty));
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.One), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentySeven)),
                    new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.One), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight)),
                    new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.One), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine)),
                    new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.One), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Thirty)),
                    new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.ThirtyOne)),
                    new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.ThirtyOne)),
                    new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Thirty), new CalendarDay(2016, MonthOfYear.April, DayOfMonth.Ten)),
                    new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.April, DayOfMonth.One), new CalendarDay(2016, MonthOfYear.April, DayOfMonth.Ten))
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_contain_reportingPeriod2_an_both_are_of_type_CalendarDay()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Thirty));
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.TwentyNine)),
                    new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.TwentyNine)),
                    new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Thirty)),
                    new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Thirty))
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_false___When_reportingPeriod1_does_not_contain_reportingPeriod2_an_both_are_of_type_CalendarMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2015, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.May));
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<CalendarMonth>(new CalendarMonth(2014, MonthOfYear.January), new CalendarMonth(2015, MonthOfYear.January)),
                    new ReportingPeriod<CalendarMonth>(new CalendarMonth(2015, MonthOfYear.January), new CalendarMonth(2015, MonthOfYear.February)),
                    new ReportingPeriod<CalendarMonth>(new CalendarMonth(2015, MonthOfYear.January), new CalendarMonth(2016, MonthOfYear.January)),
                    new ReportingPeriod<CalendarMonth>(new CalendarMonth(2015, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.June)),
                    new ReportingPeriod<CalendarMonth>(new CalendarMonth(2015, MonthOfYear.March), new CalendarMonth(2016, MonthOfYear.June)),
                    new ReportingPeriod<CalendarMonth>(new CalendarMonth(2015, MonthOfYear.March), new CalendarMonth(2016, MonthOfYear.June)),
                    new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.May), new CalendarMonth(2016, MonthOfYear.June)),
                    new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.June), new CalendarMonth(2016, MonthOfYear.September))
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_contain_reportingPeriod2_an_both_are_of_type_CalendarMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2015, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.May));
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<CalendarMonth>(new CalendarMonth(2015, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.May)),
                    new ReportingPeriod<CalendarMonth>(new CalendarMonth(2015, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.April)),
                    new ReportingPeriod<CalendarMonth>(new CalendarMonth(2015, MonthOfYear.April), new CalendarMonth(2016, MonthOfYear.May)),
                    new ReportingPeriod<CalendarMonth>(new CalendarMonth(2015, MonthOfYear.April), new CalendarMonth(2016, MonthOfYear.April)),
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_false___When_reportingPeriod1_does_not_contain_reportingPeriod2_an_both_are_of_type_CalendarQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2015, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q3));
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2014, QuarterNumber.Q3), new CalendarQuarter(2015, QuarterNumber.Q1)),
                    new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2015, QuarterNumber.Q1), new CalendarQuarter(2015, QuarterNumber.Q2)),
                    new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2015, QuarterNumber.Q1), new CalendarQuarter(2016, QuarterNumber.Q1)),
                    new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2015, QuarterNumber.Q1), new CalendarQuarter(2016, QuarterNumber.Q3)),
                    new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2015, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q4)),
                    new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2015, QuarterNumber.Q3), new CalendarQuarter(2016, QuarterNumber.Q4)),
                    new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q4)),
                    new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q4), new CalendarQuarter(2017, QuarterNumber.Q1))
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_contain_reportingPeriod2_an_both_are_of_type_CalendarQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2015, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q3));
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2015, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q3)),
                    new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2015, QuarterNumber.Q3), new CalendarQuarter(2016, QuarterNumber.Q3)),
                    new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2015, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q2)),
                    new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2015, QuarterNumber.Q3), new CalendarQuarter(2016, QuarterNumber.Q2))
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_false___When_reportingPeriod1_does_not_contain_reportingPeriod2_an_both_are_of_type_CalendarYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarYear>(new CalendarYear(2015), new CalendarYear(2017));
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<CalendarYear>(new CalendarYear(2012), new CalendarYear(2014)),
                    new ReportingPeriod<CalendarYear>(new CalendarYear(2012), new CalendarYear(2015)),
                    new ReportingPeriod<CalendarYear>(new CalendarYear(2012), new CalendarYear(2016)),
                    new ReportingPeriod<CalendarYear>(new CalendarYear(2012), new CalendarYear(2017)),
                    new ReportingPeriod<CalendarYear>(new CalendarYear(2015), new CalendarYear(2018)),
                    new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2018)),
                    new ReportingPeriod<CalendarYear>(new CalendarYear(2017), new CalendarYear(2018)),
                    new ReportingPeriod<CalendarYear>(new CalendarYear(2018), new CalendarYear(2019))
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_contain_reportingPeriod2_an_both_are_of_type_CalendarYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarYear>(new CalendarYear(2015), new CalendarYear(2017));
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<CalendarYear>(new CalendarYear(2015), new CalendarYear(2017)),
                    new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2017)),
                    new ReportingPeriod<CalendarYear>(new CalendarYear(2015), new CalendarYear(2016)),
                    new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2016))
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_does_not_contain_reportingPeriod2_an_both_are_of_type_CalendarUnbounded()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarUnbounded>(new CalendarUnbounded(), new CalendarUnbounded());
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<CalendarUnbounded>(new CalendarUnbounded(), new CalendarUnbounded())
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_false___When_reportingPeriod1_does_not_contain_reportingPeriod2_an_both_are_of_type_FiscalMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2015, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Five));
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<FiscalMonth>(new FiscalMonth(2014, MonthNumber.One), new FiscalMonth(2015, MonthNumber.One)),
                    new ReportingPeriod<FiscalMonth>(new FiscalMonth(2015, MonthNumber.One), new FiscalMonth(2015, MonthNumber.Two)),
                    new ReportingPeriod<FiscalMonth>(new FiscalMonth(2015, MonthNumber.One), new FiscalMonth(2016, MonthNumber.One)),
                    new ReportingPeriod<FiscalMonth>(new FiscalMonth(2015, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Six)),
                    new ReportingPeriod<FiscalMonth>(new FiscalMonth(2015, MonthNumber.Three), new FiscalMonth(2016, MonthNumber.Six)),
                    new ReportingPeriod<FiscalMonth>(new FiscalMonth(2015, MonthNumber.Three), new FiscalMonth(2016, MonthNumber.Six)),
                    new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Five), new FiscalMonth(2016, MonthNumber.Six)),
                    new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Six), new FiscalMonth(2016, MonthNumber.Nine))
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_contain_reportingPeriod2_an_both_are_of_type_FiscalMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2015, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Five));
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<FiscalMonth>(new FiscalMonth(2015, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Five)),
                    new ReportingPeriod<FiscalMonth>(new FiscalMonth(2015, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Four)),
                    new ReportingPeriod<FiscalMonth>(new FiscalMonth(2015, MonthNumber.Four), new FiscalMonth(2016, MonthNumber.Five)),
                    new ReportingPeriod<FiscalMonth>(new FiscalMonth(2015, MonthNumber.Four), new FiscalMonth(2016, MonthNumber.Four)),
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_false___When_reportingPeriod1_does_not_contain_reportingPeriod2_an_both_are_of_type_FiscalQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2015, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q3));
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2014, QuarterNumber.Q3), new FiscalQuarter(2015, QuarterNumber.Q1)),
                    new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2015, QuarterNumber.Q1), new FiscalQuarter(2015, QuarterNumber.Q2)),
                    new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2015, QuarterNumber.Q1), new FiscalQuarter(2016, QuarterNumber.Q1)),
                    new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2015, QuarterNumber.Q1), new FiscalQuarter(2016, QuarterNumber.Q3)),
                    new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2015, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q4)),
                    new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2015, QuarterNumber.Q3), new FiscalQuarter(2016, QuarterNumber.Q4)),
                    new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q4)),
                    new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q4), new FiscalQuarter(2017, QuarterNumber.Q1))
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_contain_reportingPeriod2_an_both_are_of_type_FiscalQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2015, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q3));
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2015, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q3)),
                    new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2015, QuarterNumber.Q3), new FiscalQuarter(2016, QuarterNumber.Q3)),
                    new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2015, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q2)),
                    new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2015, QuarterNumber.Q3), new FiscalQuarter(2016, QuarterNumber.Q2))
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_false___When_reportingPeriod1_does_not_contain_reportingPeriod2_an_both_are_of_type_FiscalYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalYear>(new FiscalYear(2015), new FiscalYear(2017));
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<FiscalYear>(new FiscalYear(2012), new FiscalYear(2014)),
                    new ReportingPeriod<FiscalYear>(new FiscalYear(2012), new FiscalYear(2015)),
                    new ReportingPeriod<FiscalYear>(new FiscalYear(2012), new FiscalYear(2016)),
                    new ReportingPeriod<FiscalYear>(new FiscalYear(2012), new FiscalYear(2017)),
                    new ReportingPeriod<FiscalYear>(new FiscalYear(2015), new FiscalYear(2018)),
                    new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2018)),
                    new ReportingPeriod<FiscalYear>(new FiscalYear(2017), new FiscalYear(2018)),
                    new ReportingPeriod<FiscalYear>(new FiscalYear(2018), new FiscalYear(2019))
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_contain_reportingPeriod2_an_both_are_of_type_FiscalYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalYear>(new FiscalYear(2015), new FiscalYear(2017));
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<FiscalYear>(new FiscalYear(2015), new FiscalYear(2017)),
                    new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2017)),
                    new ReportingPeriod<FiscalYear>(new FiscalYear(2015), new FiscalYear(2016)),
                    new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2016))
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_does_not_contain_reportingPeriod2_an_both_are_of_type_FiscalUnbounded()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalUnbounded>(new FiscalUnbounded(), new FiscalUnbounded());
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<FiscalUnbounded>(new FiscalUnbounded(), new FiscalUnbounded())
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_false___When_reportingPeriod1_does_not_contain_reportingPeriod2_an_both_are_of_type_GenericMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericMonth>(new GenericMonth(2015, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Five));
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<GenericMonth>(new GenericMonth(2014, MonthNumber.One), new GenericMonth(2015, MonthNumber.One)),
                    new ReportingPeriod<GenericMonth>(new GenericMonth(2015, MonthNumber.One), new GenericMonth(2015, MonthNumber.Two)),
                    new ReportingPeriod<GenericMonth>(new GenericMonth(2015, MonthNumber.One), new GenericMonth(2016, MonthNumber.One)),
                    new ReportingPeriod<GenericMonth>(new GenericMonth(2015, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Six)),
                    new ReportingPeriod<GenericMonth>(new GenericMonth(2015, MonthNumber.Three), new GenericMonth(2016, MonthNumber.Six)),
                    new ReportingPeriod<GenericMonth>(new GenericMonth(2015, MonthNumber.Three), new GenericMonth(2016, MonthNumber.Six)),
                    new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Five), new GenericMonth(2016, MonthNumber.Six)),
                    new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Six), new GenericMonth(2016, MonthNumber.Nine))
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_contain_reportingPeriod2_an_both_are_of_type_GenericMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericMonth>(new GenericMonth(2015, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Five));
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<GenericMonth>(new GenericMonth(2015, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Five)),
                    new ReportingPeriod<GenericMonth>(new GenericMonth(2015, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Four)),
                    new ReportingPeriod<GenericMonth>(new GenericMonth(2015, MonthNumber.Four), new GenericMonth(2016, MonthNumber.Five)),
                    new ReportingPeriod<GenericMonth>(new GenericMonth(2015, MonthNumber.Four), new GenericMonth(2016, MonthNumber.Four)),
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_false___When_reportingPeriod1_does_not_contain_reportingPeriod2_an_both_are_of_type_GenericQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2015, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q3));
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<GenericQuarter>(new GenericQuarter(2014, QuarterNumber.Q3), new GenericQuarter(2015, QuarterNumber.Q1)),
                    new ReportingPeriod<GenericQuarter>(new GenericQuarter(2015, QuarterNumber.Q1), new GenericQuarter(2015, QuarterNumber.Q2)),
                    new ReportingPeriod<GenericQuarter>(new GenericQuarter(2015, QuarterNumber.Q1), new GenericQuarter(2016, QuarterNumber.Q1)),
                    new ReportingPeriod<GenericQuarter>(new GenericQuarter(2015, QuarterNumber.Q1), new GenericQuarter(2016, QuarterNumber.Q3)),
                    new ReportingPeriod<GenericQuarter>(new GenericQuarter(2015, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q4)),
                    new ReportingPeriod<GenericQuarter>(new GenericQuarter(2015, QuarterNumber.Q3), new GenericQuarter(2016, QuarterNumber.Q4)),
                    new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q4)),
                    new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q4), new GenericQuarter(2017, QuarterNumber.Q1))
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_contain_reportingPeriod2_an_both_are_of_type_GenericQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2015, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q3));
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<GenericQuarter>(new GenericQuarter(2015, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q3)),
                    new ReportingPeriod<GenericQuarter>(new GenericQuarter(2015, QuarterNumber.Q3), new GenericQuarter(2016, QuarterNumber.Q3)),
                    new ReportingPeriod<GenericQuarter>(new GenericQuarter(2015, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q2)),
                    new ReportingPeriod<GenericQuarter>(new GenericQuarter(2015, QuarterNumber.Q3), new GenericQuarter(2016, QuarterNumber.Q2))
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_false___When_reportingPeriod1_does_not_contain_reportingPeriod2_an_both_are_of_type_GenericYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericYear>(new GenericYear(2015), new GenericYear(2017));
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<GenericYear>(new GenericYear(2012), new GenericYear(2014)),
                    new ReportingPeriod<GenericYear>(new GenericYear(2012), new GenericYear(2015)),
                    new ReportingPeriod<GenericYear>(new GenericYear(2012), new GenericYear(2016)),
                    new ReportingPeriod<GenericYear>(new GenericYear(2012), new GenericYear(2017)),
                    new ReportingPeriod<GenericYear>(new GenericYear(2015), new GenericYear(2018)),
                    new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2018)),
                    new ReportingPeriod<GenericYear>(new GenericYear(2017), new GenericYear(2018)),
                    new ReportingPeriod<GenericYear>(new GenericYear(2018), new GenericYear(2019))
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_contain_reportingPeriod2_an_both_are_of_type_GenericYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericYear>(new GenericYear(2015), new GenericYear(2017));
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<GenericYear>(new GenericYear(2015), new GenericYear(2017)),
                    new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2017)),
                    new ReportingPeriod<GenericYear>(new GenericYear(2015), new GenericYear(2016)),
                    new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2016))
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_does_not_contain_reportingPeriod2_an_both_are_of_type_GenericUnbounded()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericUnbounded>(new GenericUnbounded(), new GenericUnbounded());
            var reportingPeriod2 = new[]
                {
                    new ReportingPeriod<GenericUnbounded>(new GenericUnbounded(), new GenericUnbounded())
                };

            // Act
            var results = reportingPeriod2.Select(_ => reportingPeriod1.Contains(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_false___When_reportingPeriod1_uses_CalendarDay_and_does_not_contain_reportingPeriod2_which_does_not_use_CalendarDay()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_uses_CalendarDay_and_contains_reportingPeriod2_which_does_not_use_CalendarDay()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_false___When_reportingPeriod1_uses_CalendarMonth_and_does_not_contain_reportingPeriod2_which_does_not_use_CalendarMonth()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_uses_CalendarMonth_and_contains_reportingPeriod2_which_does_not_use_CalendarMonth()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_false___When_reportingPeriod1_uses_CalendarQuarter_and_does_not_contain_reportingPeriod2_which_does_not_use_CalendarQuarter()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_uses_CalendarQuarter_and_contains_reportingPeriod2_which_does_not_use_CalendarQuarter()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_false___When_reportingPeriod1_uses_CalendarYear_and_does_not_contain_reportingPeriod2_which_does_not_use_CalendarYear()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_uses_CalendarYear_and_contains_reportingPeriod2_which_does_not_use_CalendarYear()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_uses_CalendarUnbounded_and_contains_reportingPeriod2_which_does_not_use_CalendarYear()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_false___When_reportingPeriod1_uses_FiscalMonth_and_does_not_contain_reportingPeriod2_which_does_not_use_FiscalMonth()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_uses_FiscalMonth_and_contains_reportingPeriod2_which_does_not_use_FiscalMonth()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_false___When_reportingPeriod1_uses_FiscalQuarter_and_does_not_contain_reportingPeriod2_which_does_not_use_FiscalQuarter()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_uses_FiscalQuarter_and_contains_reportingPeriod2_which_does_not_use_FiscalQuarter()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_false___When_reportingPeriod1_uses_FiscalYear_and_does_not_contain_reportingPeriod2_which_does_not_use_FiscalYear()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_uses_FiscalYear_and_contains_reportingPeriod2_which_does_not_use_FiscalYear()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_uses_FiscalUnbounded_and_contains_reportingPeriod2_which_does_not_use_FiscalYear()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_false___When_reportingPeriod1_uses_GenericMonth_and_does_not_contain_reportingPeriod2_which_does_not_use_GenericMonth()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_uses_GenericMonth_and_contains_reportingPeriod2_which_does_not_use_GenericMonth()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_false___When_reportingPeriod1_uses_GenericQuarter_and_does_not_contain_reportingPeriod2_which_does_not_use_GenericQuarter()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_uses_GenericQuarter_and_contains_reportingPeriod2_which_does_not_use_GenericQuarter()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_false___When_reportingPeriod1_uses_GenericYear_and_does_not_contain_reportingPeriod2_which_does_not_use_GenericYear()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_uses_GenericYear_and_contains_reportingPeriod2_which_does_not_use_GenericYear()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void Contains_with_reportingPeriods___Should_return_true___When_reportingPeriod1_uses_GenericUnbounded_and_contains_reportingPeriod2_which_does_not_use_GenericYear()
        {
        }

        [Fact]
        public static void HasOverlapWith___Should_throw_ArgumentNullException___When_parameter_reportingPeriod1_is_null()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod<UnitOfTime>>();

            // Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.HasOverlapWith(null, reportingPeriod));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void HasOverlapWith___Should_throw_ArgumentNullException___When_parameter_reportingPeriod2_is_null()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod<UnitOfTime>>();

            // Act
            var ex = Record.Exception(() => reportingPeriod.HasOverlapWith(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void HasOverlapWith___Should_throw_ArgumentException___When_reporting_periods_have_different_UnitOfTimeKind()
        {
            // Arrange
            var reportingPeriod1 = A.Dummy<ReportingPeriod<UnitOfTime>>();
            var reportingPeriod2 = A.Dummy<ReportingPeriod<UnitOfTime>>().Whose(_ => _.GetUnitOfTimeKind() != reportingPeriod1.GetUnitOfTimeKind());

            // Act
            // ReSharper disable ExpressionIsAlwaysNull
            var ex = Record.Exception(() => reportingPeriod1.HasOverlapWith(reportingPeriod2));
            // ReSharper restore ExpressionIsAlwaysNull

            // Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_false___When_parameters_reportingPeriod1_and_reportingPeriod2_do_not_overlap_and_are_of_type_CalendarDay()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Thirty));

            var reportingPeriod2a = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.One), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentySeven));
            var reportingPeriod2b = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.March, DayOfMonth.ThirtyOne), new CalendarDay(2016, MonthOfYear.April, DayOfMonth.Five));

            // Act
            var hasOverlap1a = reportingPeriod1.HasOverlapWith(reportingPeriod2a);
            var hasOverlap1b = reportingPeriod2a.HasOverlapWith(reportingPeriod1);

            var hasOverlap2a = reportingPeriod1.HasOverlapWith(reportingPeriod2b);
            var hasOverlap2b = reportingPeriod2b.HasOverlapWith(reportingPeriod1);

            // Assert
            hasOverlap1a.Should().BeFalse();
            hasOverlap1b.Should().BeFalse();
            hasOverlap2a.Should().BeFalse();
            hasOverlap2b.Should().BeFalse();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_true___When_parameters_reportingPeriod1_and_reportingPeriod2_overlap_and_are_of_type_CalendarDay()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Thirty));

            var reportingPeriod2a = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.One), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight));
            var reportingPeriod2b = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.One), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine));
            var reportingPeriod2c = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.TwentyNine));
            var reportingPeriod2d = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.TwentyNine));
            var reportingPeriod2e = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Thirty));
            var reportingPeriod2f = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.ThirtyOne));
            var reportingPeriod2g = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Thirty), new CalendarDay(2016, MonthOfYear.April, DayOfMonth.Ten));

            // Act
            var hasOverlap1a = reportingPeriod1.HasOverlapWith(reportingPeriod2a);
            var hasOverlap1b = reportingPeriod2a.HasOverlapWith(reportingPeriod1);

            var hasOverlap2a = reportingPeriod1.HasOverlapWith(reportingPeriod2b);
            var hasOverlap2b = reportingPeriod2b.HasOverlapWith(reportingPeriod1);

            var hasOverlap3a = reportingPeriod1.HasOverlapWith(reportingPeriod2c);
            var hasOverlap3b = reportingPeriod2c.HasOverlapWith(reportingPeriod1);

            var hasOverlap4a = reportingPeriod1.HasOverlapWith(reportingPeriod2d);
            var hasOverlap4b = reportingPeriod2d.HasOverlapWith(reportingPeriod1);

            var hasOverlap5a = reportingPeriod1.HasOverlapWith(reportingPeriod2e);
            var hasOverlap5b = reportingPeriod2e.HasOverlapWith(reportingPeriod1);

            var hasOverlap6a = reportingPeriod1.HasOverlapWith(reportingPeriod2f);
            var hasOverlap6b = reportingPeriod2f.HasOverlapWith(reportingPeriod1);

            var hasOverlap7a = reportingPeriod1.HasOverlapWith(reportingPeriod2g);
            var hasOverlap7b = reportingPeriod2g.HasOverlapWith(reportingPeriod1);

            // Assert
            hasOverlap1a.Should().BeTrue();
            hasOverlap1b.Should().BeTrue();
            hasOverlap2a.Should().BeTrue();
            hasOverlap2b.Should().BeTrue();
            hasOverlap3a.Should().BeTrue();
            hasOverlap3b.Should().BeTrue();
            hasOverlap4a.Should().BeTrue();
            hasOverlap4b.Should().BeTrue();
            hasOverlap5a.Should().BeTrue();
            hasOverlap5b.Should().BeTrue();
            hasOverlap6a.Should().BeTrue();
            hasOverlap6b.Should().BeTrue();
            hasOverlap7a.Should().BeTrue();
            hasOverlap7b.Should().BeTrue();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_false___When_parameters_reportingPeriod1_and_reportingPeriod2_do_not_overlap_and_are_of_type_CalendarMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.April));

            var reportingPeriod2a = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2014, MonthOfYear.January), new CalendarMonth(2016, MonthOfYear.January));
            var reportingPeriod2b = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.May), new CalendarMonth(2017, MonthOfYear.July));

            // Act
            var hasOverlap1a = reportingPeriod1.HasOverlapWith(reportingPeriod2a);
            var hasOverlap1b = reportingPeriod2a.HasOverlapWith(reportingPeriod1);

            var hasOverlap2a = reportingPeriod1.HasOverlapWith(reportingPeriod2b);
            var hasOverlap2b = reportingPeriod2b.HasOverlapWith(reportingPeriod1);

            // Assert
            hasOverlap1a.Should().BeFalse();
            hasOverlap1b.Should().BeFalse();
            hasOverlap2a.Should().BeFalse();
            hasOverlap2b.Should().BeFalse();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_true___When_parameters_reportingPeriod1_and_reportingPeriod2_overlap_and_are_of_type_CalendarMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.April));

            var reportingPeriod2a = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2015, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.February));
            var reportingPeriod2b = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2015, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.March));
            var reportingPeriod2c = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.March));
            var reportingPeriod2d = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.March), new CalendarMonth(2016, MonthOfYear.March));
            var reportingPeriod2e = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.March), new CalendarMonth(2016, MonthOfYear.April));
            var reportingPeriod2f = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.March), new CalendarMonth(2017, MonthOfYear.June));
            var reportingPeriod2g = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.April), new CalendarMonth(2017, MonthOfYear.June));

            // Act
            var hasOverlap1a = reportingPeriod1.HasOverlapWith(reportingPeriod2a);
            var hasOverlap1b = reportingPeriod2a.HasOverlapWith(reportingPeriod1);

            var hasOverlap2a = reportingPeriod1.HasOverlapWith(reportingPeriod2b);
            var hasOverlap2b = reportingPeriod2b.HasOverlapWith(reportingPeriod1);

            var hasOverlap3a = reportingPeriod1.HasOverlapWith(reportingPeriod2c);
            var hasOverlap3b = reportingPeriod2c.HasOverlapWith(reportingPeriod1);

            var hasOverlap4a = reportingPeriod1.HasOverlapWith(reportingPeriod2d);
            var hasOverlap4b = reportingPeriod2d.HasOverlapWith(reportingPeriod1);

            var hasOverlap5a = reportingPeriod1.HasOverlapWith(reportingPeriod2e);
            var hasOverlap5b = reportingPeriod2e.HasOverlapWith(reportingPeriod1);

            var hasOverlap6a = reportingPeriod1.HasOverlapWith(reportingPeriod2f);
            var hasOverlap6b = reportingPeriod2f.HasOverlapWith(reportingPeriod1);

            var hasOverlap7a = reportingPeriod1.HasOverlapWith(reportingPeriod2g);
            var hasOverlap7b = reportingPeriod2g.HasOverlapWith(reportingPeriod1);

            // Assert
            hasOverlap1a.Should().BeTrue();
            hasOverlap1b.Should().BeTrue();
            hasOverlap2a.Should().BeTrue();
            hasOverlap2b.Should().BeTrue();
            hasOverlap3a.Should().BeTrue();
            hasOverlap3b.Should().BeTrue();
            hasOverlap4a.Should().BeTrue();
            hasOverlap4b.Should().BeTrue();
            hasOverlap5a.Should().BeTrue();
            hasOverlap5b.Should().BeTrue();
            hasOverlap6a.Should().BeTrue();
            hasOverlap6b.Should().BeTrue();
            hasOverlap7a.Should().BeTrue();
            hasOverlap7b.Should().BeTrue();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_false___When_parameters_reportingPeriod1_and_reportingPeriod2_do_not_overlap_and_are_of_type_FiscalMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Four));

            var reportingPeriod2a = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2014, MonthNumber.One), new FiscalMonth(2016, MonthNumber.One));
            var reportingPeriod2b = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Five), new FiscalMonth(2017, MonthNumber.Seven));

            // Act
            var hasOverlap1a = reportingPeriod1.HasOverlapWith(reportingPeriod2a);
            var hasOverlap1b = reportingPeriod2a.HasOverlapWith(reportingPeriod1);

            var hasOverlap2a = reportingPeriod1.HasOverlapWith(reportingPeriod2b);
            var hasOverlap2b = reportingPeriod2b.HasOverlapWith(reportingPeriod1);

            // Assert
            hasOverlap1a.Should().BeFalse();
            hasOverlap1b.Should().BeFalse();
            hasOverlap2a.Should().BeFalse();
            hasOverlap2b.Should().BeFalse();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_true___When_parameters_reportingPeriod1_and_reportingPeriod2_overlap_and_are_of_type_FiscalMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Four));

            var reportingPeriod2a = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2015, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Two));
            var reportingPeriod2b = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2015, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Three));
            var reportingPeriod2c = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Three));
            var reportingPeriod2d = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Three), new FiscalMonth(2016, MonthNumber.Three));
            var reportingPeriod2e = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Three), new FiscalMonth(2016, MonthNumber.Four));
            var reportingPeriod2f = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Three), new FiscalMonth(2017, MonthNumber.Six));
            var reportingPeriod2g = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Six));

            // Act
            var hasOverlap1a = reportingPeriod1.HasOverlapWith(reportingPeriod2a);
            var hasOverlap1b = reportingPeriod2a.HasOverlapWith(reportingPeriod1);

            var hasOverlap2a = reportingPeriod1.HasOverlapWith(reportingPeriod2b);
            var hasOverlap2b = reportingPeriod2b.HasOverlapWith(reportingPeriod1);

            var hasOverlap3a = reportingPeriod1.HasOverlapWith(reportingPeriod2c);
            var hasOverlap3b = reportingPeriod2c.HasOverlapWith(reportingPeriod1);

            var hasOverlap4a = reportingPeriod1.HasOverlapWith(reportingPeriod2d);
            var hasOverlap4b = reportingPeriod2d.HasOverlapWith(reportingPeriod1);

            var hasOverlap5a = reportingPeriod1.HasOverlapWith(reportingPeriod2e);
            var hasOverlap5b = reportingPeriod2e.HasOverlapWith(reportingPeriod1);

            var hasOverlap6a = reportingPeriod1.HasOverlapWith(reportingPeriod2f);
            var hasOverlap6b = reportingPeriod2f.HasOverlapWith(reportingPeriod1);

            var hasOverlap7a = reportingPeriod1.HasOverlapWith(reportingPeriod2g);
            var hasOverlap7b = reportingPeriod2g.HasOverlapWith(reportingPeriod1);

            // Assert
            hasOverlap1a.Should().BeTrue();
            hasOverlap1b.Should().BeTrue();
            hasOverlap2a.Should().BeTrue();
            hasOverlap2b.Should().BeTrue();
            hasOverlap3a.Should().BeTrue();
            hasOverlap3b.Should().BeTrue();
            hasOverlap4a.Should().BeTrue();
            hasOverlap4b.Should().BeTrue();
            hasOverlap5a.Should().BeTrue();
            hasOverlap5b.Should().BeTrue();
            hasOverlap6a.Should().BeTrue();
            hasOverlap6b.Should().BeTrue();
            hasOverlap7a.Should().BeTrue();
            hasOverlap7b.Should().BeTrue();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_false___When_parameters_reportingPeriod1_and_reportingPeriod2_do_not_overlap_and_are_of_type_GenericMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Four));

            var reportingPeriod2a = new ReportingPeriod<GenericMonth>(new GenericMonth(2014, MonthNumber.One), new GenericMonth(2016, MonthNumber.One));
            var reportingPeriod2b = new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Five), new GenericMonth(2017, MonthNumber.Seven));

            // Act
            var hasOverlap1a = reportingPeriod1.HasOverlapWith(reportingPeriod2a);
            var hasOverlap1b = reportingPeriod2a.HasOverlapWith(reportingPeriod1);

            var hasOverlap2a = reportingPeriod1.HasOverlapWith(reportingPeriod2b);
            var hasOverlap2b = reportingPeriod2b.HasOverlapWith(reportingPeriod1);

            // Assert
            hasOverlap1a.Should().BeFalse();
            hasOverlap1b.Should().BeFalse();
            hasOverlap2a.Should().BeFalse();
            hasOverlap2b.Should().BeFalse();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_true___When_parameters_reportingPeriod1_and_reportingPeriod2_overlap_and_are_of_type_GenericMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Four));

            var reportingPeriod2a = new ReportingPeriod<GenericMonth>(new GenericMonth(2015, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Two));
            var reportingPeriod2b = new ReportingPeriod<GenericMonth>(new GenericMonth(2015, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Three));
            var reportingPeriod2c = new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Three));
            var reportingPeriod2d = new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Three), new GenericMonth(2016, MonthNumber.Three));
            var reportingPeriod2e = new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Three), new GenericMonth(2016, MonthNumber.Four));
            var reportingPeriod2f = new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Three), new GenericMonth(2017, MonthNumber.Six));
            var reportingPeriod2g = new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Four), new GenericMonth(2017, MonthNumber.Six));

            // Act
            var hasOverlap1a = reportingPeriod1.HasOverlapWith(reportingPeriod2a);
            var hasOverlap1b = reportingPeriod2a.HasOverlapWith(reportingPeriod1);

            var hasOverlap2a = reportingPeriod1.HasOverlapWith(reportingPeriod2b);
            var hasOverlap2b = reportingPeriod2b.HasOverlapWith(reportingPeriod1);

            var hasOverlap3a = reportingPeriod1.HasOverlapWith(reportingPeriod2c);
            var hasOverlap3b = reportingPeriod2c.HasOverlapWith(reportingPeriod1);

            var hasOverlap4a = reportingPeriod1.HasOverlapWith(reportingPeriod2d);
            var hasOverlap4b = reportingPeriod2d.HasOverlapWith(reportingPeriod1);

            var hasOverlap5a = reportingPeriod1.HasOverlapWith(reportingPeriod2e);
            var hasOverlap5b = reportingPeriod2e.HasOverlapWith(reportingPeriod1);

            var hasOverlap6a = reportingPeriod1.HasOverlapWith(reportingPeriod2f);
            var hasOverlap6b = reportingPeriod2f.HasOverlapWith(reportingPeriod1);

            var hasOverlap7a = reportingPeriod1.HasOverlapWith(reportingPeriod2g);
            var hasOverlap7b = reportingPeriod2g.HasOverlapWith(reportingPeriod1);

            // Assert
            hasOverlap1a.Should().BeTrue();
            hasOverlap1b.Should().BeTrue();
            hasOverlap2a.Should().BeTrue();
            hasOverlap2b.Should().BeTrue();
            hasOverlap3a.Should().BeTrue();
            hasOverlap3b.Should().BeTrue();
            hasOverlap4a.Should().BeTrue();
            hasOverlap4b.Should().BeTrue();
            hasOverlap5a.Should().BeTrue();
            hasOverlap5b.Should().BeTrue();
            hasOverlap6a.Should().BeTrue();
            hasOverlap6b.Should().BeTrue();
            hasOverlap7a.Should().BeTrue();
            hasOverlap7b.Should().BeTrue();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_false___When_parameters_reportingPeriod1_and_reportingPeriod2_do_not_overlap_and_are_of_type_CalendarQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q4));

            var reportingPeriod2a = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2014, QuarterNumber.Q1), new CalendarQuarter(2016, QuarterNumber.Q1));
            var reportingPeriod2b = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2017, QuarterNumber.Q1), new CalendarQuarter(2018, QuarterNumber.Q1));

            // Act
            var hasOverlap1a = reportingPeriod1.HasOverlapWith(reportingPeriod2a);
            var hasOverlap1b = reportingPeriod2a.HasOverlapWith(reportingPeriod1);

            var hasOverlap2a = reportingPeriod1.HasOverlapWith(reportingPeriod2b);
            var hasOverlap2b = reportingPeriod2b.HasOverlapWith(reportingPeriod1);

            // Assert
            hasOverlap1a.Should().BeFalse();
            hasOverlap1b.Should().BeFalse();
            hasOverlap2a.Should().BeFalse();
            hasOverlap2b.Should().BeFalse();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_true___When_parameters_reportingPeriod1_and_reportingPeriod2_overlap_and_are_of_type_CalendarQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q4));

            var reportingPeriod2a = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2015, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q2));
            var reportingPeriod2b = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2015, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q3));
            var reportingPeriod2c = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q3));
            var reportingPeriod2d = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q2));
            var reportingPeriod2e = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q3), new CalendarQuarter(2016, QuarterNumber.Q4));
            var reportingPeriod2f = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q3), new CalendarQuarter(2017, QuarterNumber.Q1));
            var reportingPeriod2g = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q4), new CalendarQuarter(2017, QuarterNumber.Q1));

            // Act
            var hasOverlap1a = reportingPeriod1.HasOverlapWith(reportingPeriod2a);
            var hasOverlap1b = reportingPeriod2a.HasOverlapWith(reportingPeriod1);

            var hasOverlap2a = reportingPeriod1.HasOverlapWith(reportingPeriod2b);
            var hasOverlap2b = reportingPeriod2b.HasOverlapWith(reportingPeriod1);

            var hasOverlap3a = reportingPeriod1.HasOverlapWith(reportingPeriod2c);
            var hasOverlap3b = reportingPeriod2c.HasOverlapWith(reportingPeriod1);

            var hasOverlap4a = reportingPeriod1.HasOverlapWith(reportingPeriod2d);
            var hasOverlap4b = reportingPeriod2d.HasOverlapWith(reportingPeriod1);

            var hasOverlap5a = reportingPeriod1.HasOverlapWith(reportingPeriod2e);
            var hasOverlap5b = reportingPeriod2e.HasOverlapWith(reportingPeriod1);

            var hasOverlap6a = reportingPeriod1.HasOverlapWith(reportingPeriod2f);
            var hasOverlap6b = reportingPeriod2f.HasOverlapWith(reportingPeriod1);

            var hasOverlap7a = reportingPeriod1.HasOverlapWith(reportingPeriod2g);
            var hasOverlap7b = reportingPeriod2g.HasOverlapWith(reportingPeriod1);

            // Assert
            hasOverlap1a.Should().BeTrue();
            hasOverlap1b.Should().BeTrue();
            hasOverlap2a.Should().BeTrue();
            hasOverlap2b.Should().BeTrue();
            hasOverlap3a.Should().BeTrue();
            hasOverlap3b.Should().BeTrue();
            hasOverlap4a.Should().BeTrue();
            hasOverlap4b.Should().BeTrue();
            hasOverlap5a.Should().BeTrue();
            hasOverlap5b.Should().BeTrue();
            hasOverlap6a.Should().BeTrue();
            hasOverlap6b.Should().BeTrue();
            hasOverlap7a.Should().BeTrue();
            hasOverlap7b.Should().BeTrue();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_false___When_parameters_reportingPeriod1_and_reportingPeriod2_do_not_overlap_and_are_of_type_FiscalQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q4));

            var reportingPeriod2a = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2014, QuarterNumber.Q1), new FiscalQuarter(2016, QuarterNumber.Q1));
            var reportingPeriod2b = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2017, QuarterNumber.Q1), new FiscalQuarter(2018, QuarterNumber.Q1));

            // Act
            var hasOverlap1a = reportingPeriod1.HasOverlapWith(reportingPeriod2a);
            var hasOverlap1b = reportingPeriod2a.HasOverlapWith(reportingPeriod1);

            var hasOverlap2a = reportingPeriod1.HasOverlapWith(reportingPeriod2b);
            var hasOverlap2b = reportingPeriod2b.HasOverlapWith(reportingPeriod1);

            // Assert
            hasOverlap1a.Should().BeFalse();
            hasOverlap1b.Should().BeFalse();
            hasOverlap2a.Should().BeFalse();
            hasOverlap2b.Should().BeFalse();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_true___When_parameters_reportingPeriod1_and_reportingPeriod2_overlap_and_are_of_type_FiscalQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q4));

            var reportingPeriod2a = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2015, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q2));
            var reportingPeriod2b = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2015, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q3));
            var reportingPeriod2c = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q3));
            var reportingPeriod2d = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q2));
            var reportingPeriod2e = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q3), new FiscalQuarter(2016, QuarterNumber.Q4));
            var reportingPeriod2f = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q3), new FiscalQuarter(2017, QuarterNumber.Q1));
            var reportingPeriod2g = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q4), new FiscalQuarter(2017, QuarterNumber.Q1));

            // Act
            var hasOverlap1a = reportingPeriod1.HasOverlapWith(reportingPeriod2a);
            var hasOverlap1b = reportingPeriod2a.HasOverlapWith(reportingPeriod1);

            var hasOverlap2a = reportingPeriod1.HasOverlapWith(reportingPeriod2b);
            var hasOverlap2b = reportingPeriod2b.HasOverlapWith(reportingPeriod1);

            var hasOverlap3a = reportingPeriod1.HasOverlapWith(reportingPeriod2c);
            var hasOverlap3b = reportingPeriod2c.HasOverlapWith(reportingPeriod1);

            var hasOverlap4a = reportingPeriod1.HasOverlapWith(reportingPeriod2d);
            var hasOverlap4b = reportingPeriod2d.HasOverlapWith(reportingPeriod1);

            var hasOverlap5a = reportingPeriod1.HasOverlapWith(reportingPeriod2e);
            var hasOverlap5b = reportingPeriod2e.HasOverlapWith(reportingPeriod1);

            var hasOverlap6a = reportingPeriod1.HasOverlapWith(reportingPeriod2f);
            var hasOverlap6b = reportingPeriod2f.HasOverlapWith(reportingPeriod1);

            var hasOverlap7a = reportingPeriod1.HasOverlapWith(reportingPeriod2g);
            var hasOverlap7b = reportingPeriod2g.HasOverlapWith(reportingPeriod1);

            // Assert
            hasOverlap1a.Should().BeTrue();
            hasOverlap1b.Should().BeTrue();
            hasOverlap2a.Should().BeTrue();
            hasOverlap2b.Should().BeTrue();
            hasOverlap3a.Should().BeTrue();
            hasOverlap3b.Should().BeTrue();
            hasOverlap4a.Should().BeTrue();
            hasOverlap4b.Should().BeTrue();
            hasOverlap5a.Should().BeTrue();
            hasOverlap5b.Should().BeTrue();
            hasOverlap6a.Should().BeTrue();
            hasOverlap6b.Should().BeTrue();
            hasOverlap7a.Should().BeTrue();
            hasOverlap7b.Should().BeTrue();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_false___When_parameters_reportingPeriod1_and_reportingPeriod2_do_not_overlap_and_are_of_type_GenericQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q4));

            var reportingPeriod2a = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2014, QuarterNumber.Q1), new GenericQuarter(2016, QuarterNumber.Q1));
            var reportingPeriod2b = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2017, QuarterNumber.Q1), new GenericQuarter(2018, QuarterNumber.Q1));

            // Act
            var hasOverlap1a = reportingPeriod1.HasOverlapWith(reportingPeriod2a);
            var hasOverlap1b = reportingPeriod2a.HasOverlapWith(reportingPeriod1);

            var hasOverlap2a = reportingPeriod1.HasOverlapWith(reportingPeriod2b);
            var hasOverlap2b = reportingPeriod2b.HasOverlapWith(reportingPeriod1);

            // Assert
            hasOverlap1a.Should().BeFalse();
            hasOverlap1b.Should().BeFalse();
            hasOverlap2a.Should().BeFalse();
            hasOverlap2b.Should().BeFalse();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_true___When_parameters_reportingPeriod1_and_reportingPeriod2_overlap_and_are_of_type_GenericQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q4));

            var reportingPeriod2a = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2015, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q2));
            var reportingPeriod2b = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2015, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q3));
            var reportingPeriod2c = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q3));
            var reportingPeriod2d = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q2));
            var reportingPeriod2e = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q3), new GenericQuarter(2016, QuarterNumber.Q4));
            var reportingPeriod2f = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q3), new GenericQuarter(2017, QuarterNumber.Q1));
            var reportingPeriod2g = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q4), new GenericQuarter(2017, QuarterNumber.Q1));

            // Act
            var hasOverlap1a = reportingPeriod1.HasOverlapWith(reportingPeriod2a);
            var hasOverlap1b = reportingPeriod2a.HasOverlapWith(reportingPeriod1);

            var hasOverlap2a = reportingPeriod1.HasOverlapWith(reportingPeriod2b);
            var hasOverlap2b = reportingPeriod2b.HasOverlapWith(reportingPeriod1);

            var hasOverlap3a = reportingPeriod1.HasOverlapWith(reportingPeriod2c);
            var hasOverlap3b = reportingPeriod2c.HasOverlapWith(reportingPeriod1);

            var hasOverlap4a = reportingPeriod1.HasOverlapWith(reportingPeriod2d);
            var hasOverlap4b = reportingPeriod2d.HasOverlapWith(reportingPeriod1);

            var hasOverlap5a = reportingPeriod1.HasOverlapWith(reportingPeriod2e);
            var hasOverlap5b = reportingPeriod2e.HasOverlapWith(reportingPeriod1);

            var hasOverlap6a = reportingPeriod1.HasOverlapWith(reportingPeriod2f);
            var hasOverlap6b = reportingPeriod2f.HasOverlapWith(reportingPeriod1);

            var hasOverlap7a = reportingPeriod1.HasOverlapWith(reportingPeriod2g);
            var hasOverlap7b = reportingPeriod2g.HasOverlapWith(reportingPeriod1);

            // Assert
            hasOverlap1a.Should().BeTrue();
            hasOverlap1b.Should().BeTrue();
            hasOverlap2a.Should().BeTrue();
            hasOverlap2b.Should().BeTrue();
            hasOverlap3a.Should().BeTrue();
            hasOverlap3b.Should().BeTrue();
            hasOverlap4a.Should().BeTrue();
            hasOverlap4b.Should().BeTrue();
            hasOverlap5a.Should().BeTrue();
            hasOverlap5b.Should().BeTrue();
            hasOverlap6a.Should().BeTrue();
            hasOverlap6b.Should().BeTrue();
            hasOverlap7a.Should().BeTrue();
            hasOverlap7b.Should().BeTrue();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_false___When_parameters_reportingPeriod1_and_reportingPeriod2_do_not_overlap_and_are_of_type_CalendarYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarYear>(new CalendarYear(2011), new CalendarYear(2014));

            var reportingPeriod2a = new ReportingPeriod<CalendarYear>(new CalendarYear(2009), new CalendarYear(2010));
            var reportingPeriod2b = new ReportingPeriod<CalendarYear>(new CalendarYear(2015), new CalendarYear(2018));

            // Act
            var hasOverlap1a = reportingPeriod1.HasOverlapWith(reportingPeriod2a);
            var hasOverlap1b = reportingPeriod2a.HasOverlapWith(reportingPeriod1);

            var hasOverlap2a = reportingPeriod1.HasOverlapWith(reportingPeriod2b);
            var hasOverlap2b = reportingPeriod2b.HasOverlapWith(reportingPeriod1);

            // Assert
            hasOverlap1a.Should().BeFalse();
            hasOverlap1b.Should().BeFalse();
            hasOverlap2a.Should().BeFalse();
            hasOverlap2b.Should().BeFalse();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_true___When_parameters_reportingPeriod1_and_reportingPeriod2_overlap_and_are_of_type_CalendarYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarYear>(new CalendarYear(2011), new CalendarYear(2014));

            var reportingPeriod2a = new ReportingPeriod<CalendarYear>(new CalendarYear(2009), new CalendarYear(2011));
            var reportingPeriod2b = new ReportingPeriod<CalendarYear>(new CalendarYear(2009), new CalendarYear(2012));
            var reportingPeriod2c = new ReportingPeriod<CalendarYear>(new CalendarYear(2011), new CalendarYear(2013));
            var reportingPeriod2d = new ReportingPeriod<CalendarYear>(new CalendarYear(2012), new CalendarYear(2013));
            var reportingPeriod2e = new ReportingPeriod<CalendarYear>(new CalendarYear(2012), new CalendarYear(2014));
            var reportingPeriod2f = new ReportingPeriod<CalendarYear>(new CalendarYear(2012), new CalendarYear(2016));
            var reportingPeriod2g = new ReportingPeriod<CalendarYear>(new CalendarYear(2014), new CalendarYear(2017));

            // Act
            var hasOverlap1a = reportingPeriod1.HasOverlapWith(reportingPeriod2a);
            var hasOverlap1b = reportingPeriod2a.HasOverlapWith(reportingPeriod1);

            var hasOverlap2a = reportingPeriod1.HasOverlapWith(reportingPeriod2b);
            var hasOverlap2b = reportingPeriod2b.HasOverlapWith(reportingPeriod1);

            var hasOverlap3a = reportingPeriod1.HasOverlapWith(reportingPeriod2c);
            var hasOverlap3b = reportingPeriod2c.HasOverlapWith(reportingPeriod1);

            var hasOverlap4a = reportingPeriod1.HasOverlapWith(reportingPeriod2d);
            var hasOverlap4b = reportingPeriod2d.HasOverlapWith(reportingPeriod1);

            var hasOverlap5a = reportingPeriod1.HasOverlapWith(reportingPeriod2e);
            var hasOverlap5b = reportingPeriod2e.HasOverlapWith(reportingPeriod1);

            var hasOverlap6a = reportingPeriod1.HasOverlapWith(reportingPeriod2f);
            var hasOverlap6b = reportingPeriod2f.HasOverlapWith(reportingPeriod1);

            var hasOverlap7a = reportingPeriod1.HasOverlapWith(reportingPeriod2g);
            var hasOverlap7b = reportingPeriod2g.HasOverlapWith(reportingPeriod1);

            // Assert
            hasOverlap1a.Should().BeTrue();
            hasOverlap1b.Should().BeTrue();
            hasOverlap2a.Should().BeTrue();
            hasOverlap2b.Should().BeTrue();
            hasOverlap3a.Should().BeTrue();
            hasOverlap3b.Should().BeTrue();
            hasOverlap4a.Should().BeTrue();
            hasOverlap4b.Should().BeTrue();
            hasOverlap5a.Should().BeTrue();
            hasOverlap5b.Should().BeTrue();
            hasOverlap6a.Should().BeTrue();
            hasOverlap6b.Should().BeTrue();
            hasOverlap7a.Should().BeTrue();
            hasOverlap7b.Should().BeTrue();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_false___When_parameters_reportingPeriod1_and_reportingPeriod2_do_not_overlap_and_are_of_type_FiscalYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalYear>(new FiscalYear(2011), new FiscalYear(2014));

            var reportingPeriod2a = new ReportingPeriod<FiscalYear>(new FiscalYear(2009), new FiscalYear(2010));
            var reportingPeriod2b = new ReportingPeriod<FiscalYear>(new FiscalYear(2015), new FiscalYear(2018));

            // Act
            var hasOverlap1a = reportingPeriod1.HasOverlapWith(reportingPeriod2a);
            var hasOverlap1b = reportingPeriod2a.HasOverlapWith(reportingPeriod1);

            var hasOverlap2a = reportingPeriod1.HasOverlapWith(reportingPeriod2b);
            var hasOverlap2b = reportingPeriod2b.HasOverlapWith(reportingPeriod1);

            // Assert
            hasOverlap1a.Should().BeFalse();
            hasOverlap1b.Should().BeFalse();
            hasOverlap2a.Should().BeFalse();
            hasOverlap2b.Should().BeFalse();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_true___When_parameters_reportingPeriod1_and_reportingPeriod2_overlap_and_are_of_type_FiscalYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalYear>(new FiscalYear(2011), new FiscalYear(2014));

            var reportingPeriod2a = new ReportingPeriod<FiscalYear>(new FiscalYear(2009), new FiscalYear(2011));
            var reportingPeriod2b = new ReportingPeriod<FiscalYear>(new FiscalYear(2009), new FiscalYear(2012));
            var reportingPeriod2c = new ReportingPeriod<FiscalYear>(new FiscalYear(2011), new FiscalYear(2013));
            var reportingPeriod2d = new ReportingPeriod<FiscalYear>(new FiscalYear(2012), new FiscalYear(2013));
            var reportingPeriod2e = new ReportingPeriod<FiscalYear>(new FiscalYear(2012), new FiscalYear(2014));
            var reportingPeriod2f = new ReportingPeriod<FiscalYear>(new FiscalYear(2012), new FiscalYear(2016));
            var reportingPeriod2g = new ReportingPeriod<FiscalYear>(new FiscalYear(2014), new FiscalYear(2017));

            // Act
            var hasOverlap1a = reportingPeriod1.HasOverlapWith(reportingPeriod2a);
            var hasOverlap1b = reportingPeriod2a.HasOverlapWith(reportingPeriod1);

            var hasOverlap2a = reportingPeriod1.HasOverlapWith(reportingPeriod2b);
            var hasOverlap2b = reportingPeriod2b.HasOverlapWith(reportingPeriod1);

            var hasOverlap3a = reportingPeriod1.HasOverlapWith(reportingPeriod2c);
            var hasOverlap3b = reportingPeriod2c.HasOverlapWith(reportingPeriod1);

            var hasOverlap4a = reportingPeriod1.HasOverlapWith(reportingPeriod2d);
            var hasOverlap4b = reportingPeriod2d.HasOverlapWith(reportingPeriod1);

            var hasOverlap5a = reportingPeriod1.HasOverlapWith(reportingPeriod2e);
            var hasOverlap5b = reportingPeriod2e.HasOverlapWith(reportingPeriod1);

            var hasOverlap6a = reportingPeriod1.HasOverlapWith(reportingPeriod2f);
            var hasOverlap6b = reportingPeriod2f.HasOverlapWith(reportingPeriod1);

            var hasOverlap7a = reportingPeriod1.HasOverlapWith(reportingPeriod2g);
            var hasOverlap7b = reportingPeriod2g.HasOverlapWith(reportingPeriod1);

            // Assert
            hasOverlap1a.Should().BeTrue();
            hasOverlap1b.Should().BeTrue();
            hasOverlap2a.Should().BeTrue();
            hasOverlap2b.Should().BeTrue();
            hasOverlap3a.Should().BeTrue();
            hasOverlap3b.Should().BeTrue();
            hasOverlap4a.Should().BeTrue();
            hasOverlap4b.Should().BeTrue();
            hasOverlap5a.Should().BeTrue();
            hasOverlap5b.Should().BeTrue();
            hasOverlap6a.Should().BeTrue();
            hasOverlap6b.Should().BeTrue();
            hasOverlap7a.Should().BeTrue();
            hasOverlap7b.Should().BeTrue();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_false___When_parameters_reportingPeriod1_and_reportingPeriod2_do_not_overlap_and_are_of_type_GenericYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericYear>(new GenericYear(2011), new GenericYear(2014));

            var reportingPeriod2a = new ReportingPeriod<GenericYear>(new GenericYear(2009), new GenericYear(2010));
            var reportingPeriod2b = new ReportingPeriod<GenericYear>(new GenericYear(2015), new GenericYear(2018));

            // Act
            var hasOverlap1a = reportingPeriod1.HasOverlapWith(reportingPeriod2a);
            var hasOverlap1b = reportingPeriod2a.HasOverlapWith(reportingPeriod1);

            var hasOverlap2a = reportingPeriod1.HasOverlapWith(reportingPeriod2b);
            var hasOverlap2b = reportingPeriod2b.HasOverlapWith(reportingPeriod1);

            // Assert
            hasOverlap1a.Should().BeFalse();
            hasOverlap1b.Should().BeFalse();
            hasOverlap2a.Should().BeFalse();
            hasOverlap2b.Should().BeFalse();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_true___When_parameters_reportingPeriod1_and_reportingPeriod2_overlap_and_are_of_type_GenericYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericYear>(new GenericYear(2011), new GenericYear(2014));

            var reportingPeriod2a = new ReportingPeriod<GenericYear>(new GenericYear(2009), new GenericYear(2011));
            var reportingPeriod2b = new ReportingPeriod<GenericYear>(new GenericYear(2009), new GenericYear(2012));
            var reportingPeriod2c = new ReportingPeriod<GenericYear>(new GenericYear(2011), new GenericYear(2013));
            var reportingPeriod2d = new ReportingPeriod<GenericYear>(new GenericYear(2012), new GenericYear(2013));
            var reportingPeriod2e = new ReportingPeriod<GenericYear>(new GenericYear(2012), new GenericYear(2014));
            var reportingPeriod2f = new ReportingPeriod<GenericYear>(new GenericYear(2012), new GenericYear(2016));
            var reportingPeriod2g = new ReportingPeriod<GenericYear>(new GenericYear(2014), new GenericYear(2017));

            // Act
            var hasOverlap1a = reportingPeriod1.HasOverlapWith(reportingPeriod2a);
            var hasOverlap1b = reportingPeriod2a.HasOverlapWith(reportingPeriod1);

            var hasOverlap2a = reportingPeriod1.HasOverlapWith(reportingPeriod2b);
            var hasOverlap2b = reportingPeriod2b.HasOverlapWith(reportingPeriod1);

            var hasOverlap3a = reportingPeriod1.HasOverlapWith(reportingPeriod2c);
            var hasOverlap3b = reportingPeriod2c.HasOverlapWith(reportingPeriod1);

            var hasOverlap4a = reportingPeriod1.HasOverlapWith(reportingPeriod2d);
            var hasOverlap4b = reportingPeriod2d.HasOverlapWith(reportingPeriod1);

            var hasOverlap5a = reportingPeriod1.HasOverlapWith(reportingPeriod2e);
            var hasOverlap5b = reportingPeriod2e.HasOverlapWith(reportingPeriod1);

            var hasOverlap6a = reportingPeriod1.HasOverlapWith(reportingPeriod2f);
            var hasOverlap6b = reportingPeriod2f.HasOverlapWith(reportingPeriod1);

            var hasOverlap7a = reportingPeriod1.HasOverlapWith(reportingPeriod2g);
            var hasOverlap7b = reportingPeriod2g.HasOverlapWith(reportingPeriod1);

            // Assert
            hasOverlap1a.Should().BeTrue();
            hasOverlap1b.Should().BeTrue();
            hasOverlap2a.Should().BeTrue();
            hasOverlap2b.Should().BeTrue();
            hasOverlap3a.Should().BeTrue();
            hasOverlap3b.Should().BeTrue();
            hasOverlap4a.Should().BeTrue();
            hasOverlap4b.Should().BeTrue();
            hasOverlap5a.Should().BeTrue();
            hasOverlap5b.Should().BeTrue();
            hasOverlap6a.Should().BeTrue();
            hasOverlap6b.Should().BeTrue();
            hasOverlap7a.Should().BeTrue();
            hasOverlap7b.Should().BeTrue();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_true___When_parameters_reportingPeriod1_and_reportingPeriod2_overlap_and_are_of_type_CalendarUnbounded()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarUnbounded>(new CalendarUnbounded(), new CalendarUnbounded());
            var reportingPeriod2 = new ReportingPeriod<CalendarUnbounded>(new CalendarUnbounded(), new CalendarUnbounded());

            // Act
            var hasOverlap = reportingPeriod1.HasOverlapWith(reportingPeriod2);

            // Assert
            hasOverlap.Should().BeTrue();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_true___When_parameters_reportingPeriod1_and_reportingPeriod2_overlap_and_are_of_type_FiscalUnbounded()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalUnbounded>(new FiscalUnbounded(), new FiscalUnbounded());
            var reportingPeriod2 = new ReportingPeriod<FiscalUnbounded>(new FiscalUnbounded(), new FiscalUnbounded());

            // Act
            var hasOverlap = reportingPeriod1.HasOverlapWith(reportingPeriod2);

            // Assert
            hasOverlap.Should().BeTrue();
        }

        [Fact]
        public static void HasOverlapWith___Should_return_true___When_parameters_reportingPeriod1_and_reportingPeriod2_overlap_and_are_of_type_GenericUnbounded()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericUnbounded>(new GenericUnbounded(), new GenericUnbounded());
            var reportingPeriod2 = new ReportingPeriod<GenericUnbounded>(new GenericUnbounded(), new GenericUnbounded());

            // Act
            var hasOverlap = reportingPeriod1.HasOverlapWith(reportingPeriod2);

            // Assert
            hasOverlap.Should().BeTrue();
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_false___When_reportingPeriod1_uses_CalendarDay_and_does_not_contain_reportingPeriod2_which_does_not_use_CalendarDay()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_true___When_reportingPeriod1_uses_CalendarDay_and_contains_reportingPeriod2_which_does_not_use_CalendarDay()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_false___When_reportingPeriod1_uses_CalendarMonth_and_does_not_contain_reportingPeriod2_which_does_not_use_CalendarMonth()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_true___When_reportingPeriod1_uses_CalendarMonth_and_contains_reportingPeriod2_which_does_not_use_CalendarMonth()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_false___When_reportingPeriod1_uses_CalendarQuarter_and_does_not_contain_reportingPeriod2_which_does_not_use_CalendarQuarter()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_true___When_reportingPeriod1_uses_CalendarQuarter_and_contains_reportingPeriod2_which_does_not_use_CalendarQuarter()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_false___When_reportingPeriod1_uses_CalendarYear_and_does_not_contain_reportingPeriod2_which_does_not_use_CalendarYear()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_true___When_reportingPeriod1_uses_CalendarYear_and_contains_reportingPeriod2_which_does_not_use_CalendarYear()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_true___When_reportingPeriod1_uses_CalendarUnbounded_and_contains_reportingPeriod2_which_does_not_use_CalendarYear()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_false___When_reportingPeriod1_uses_FiscalMonth_and_does_not_contain_reportingPeriod2_which_does_not_use_FiscalMonth()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_true___When_reportingPeriod1_uses_FiscalMonth_and_contains_reportingPeriod2_which_does_not_use_FiscalMonth()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_false___When_reportingPeriod1_uses_FiscalQuarter_and_does_not_contain_reportingPeriod2_which_does_not_use_FiscalQuarter()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_true___When_reportingPeriod1_uses_FiscalQuarter_and_contains_reportingPeriod2_which_does_not_use_FiscalQuarter()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_false___When_reportingPeriod1_uses_FiscalYear_and_does_not_contain_reportingPeriod2_which_does_not_use_FiscalYear()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_true___When_reportingPeriod1_uses_FiscalYear_and_contains_reportingPeriod2_which_does_not_use_FiscalYear()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_true___When_reportingPeriod1_uses_FiscalUnbounded_and_contains_reportingPeriod2_which_does_not_use_FiscalYear()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_false___When_reportingPeriod1_uses_GenericMonth_and_does_not_contain_reportingPeriod2_which_does_not_use_GenericMonth()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_true___When_reportingPeriod1_uses_GenericMonth_and_contains_reportingPeriod2_which_does_not_use_GenericMonth()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_false___When_reportingPeriod1_uses_GenericQuarter_and_does_not_contain_reportingPeriod2_which_does_not_use_GenericQuarter()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_true___When_reportingPeriod1_uses_GenericQuarter_and_contains_reportingPeriod2_which_does_not_use_GenericQuarter()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_false___When_reportingPeriod1_uses_GenericYear_and_does_not_contain_reportingPeriod2_which_does_not_use_GenericYear()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_true___When_reportingPeriod1_uses_GenericYear_and_contains_reportingPeriod2_which_does_not_use_GenericYear()
        {
        }

        [Fact(Skip = "too cumbersome to test for now")]
        public static void HasOverlapWith___Should_return_true___When_reportingPeriod1_uses_GenericUnbounded_and_contains_reportingPeriod2_which_does_not_use_GenericYear()
        {
        }

        [Fact]
        public static void NumberOfUnitsWithin___Should_throw_ArgumentNullException___When_parameter_reportingPeriod_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.NumberOfUnitsWithin(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void NumberOfUnitsWithin___Should_throw_ArgumentException___When_parameter_reportingPeriod_Start_and_or_End_is_unbounded()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarUnitOfTime>(new CalendarUnbounded(), A.Dummy<CalendarUnitOfTime>());
            var reportingPeriod2 = new ReportingPeriod<GenericUnitOfTime>(A.Dummy<GenericUnitOfTime>(), new GenericUnbounded());
            var reportingPeriod3 = new ReportingPeriod<FiscalUnitOfTime>(new FiscalUnbounded(), new FiscalUnbounded());

            // Act
            var ex1 = Record.Exception(() => reportingPeriod1.NumberOfUnitsWithin());
            var ex2 = Record.Exception(() => reportingPeriod2.NumberOfUnitsWithin());
            var ex3 = Record.Exception(() => reportingPeriod3.NumberOfUnitsWithin());

            // Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex2.Should().BeOfType<ArgumentException>();
            ex3.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void NumberOfUnitsWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_CalendarDay()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight));
            var reportingPeriod2 = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Three));
            var reportingPeriod3 = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Three), new CalendarDay(2017, MonthOfYear.March, DayOfMonth.Two));

            // Act
            var actualUnits1 = reportingPeriod1.NumberOfUnitsWithin();
            var actualUnits2 = reportingPeriod2.NumberOfUnitsWithin();
            var actualUnits3 = reportingPeriod3.NumberOfUnitsWithin();

            // Assert
            actualUnits1.Should().Be(1);
            actualUnits2.Should().Be(5);
            actualUnits3.Should().Be(365);
        }

        [Fact]
        public static void NumberOfUnitsWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_CalendarMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.February));
            var reportingPeriod2 = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.April));
            var reportingPeriod3 = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2017, MonthOfYear.January));

            // Act
            var actualUnits1 = reportingPeriod1.NumberOfUnitsWithin();
            var actualUnits2 = reportingPeriod2.NumberOfUnitsWithin();
            var actualUnits3 = reportingPeriod3.NumberOfUnitsWithin();

            // Assert
            actualUnits1.Should().Be(1);
            actualUnits2.Should().Be(3);
            actualUnits3.Should().Be(12);
        }

        [Fact]
        public static void NumberOfUnitsWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_FiscalMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Two));
            var reportingPeriod2 = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Three));
            var reportingPeriod3 = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2017, MonthNumber.One));

            // Act
            var actualUnits1 = reportingPeriod1.NumberOfUnitsWithin();
            var actualUnits2 = reportingPeriod2.NumberOfUnitsWithin();
            var actualUnits3 = reportingPeriod3.NumberOfUnitsWithin();

            // Assert
            actualUnits1.Should().Be(1);
            actualUnits2.Should().Be(2);
            actualUnits3.Should().Be(12);
        }

        [Fact]
        public static void NumberOfUnitsWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_GenericMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Two));
            var reportingPeriod2 = new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Three));
            var reportingPeriod3 = new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2017, MonthNumber.One));

            // Act
            var actualUnits1 = reportingPeriod1.NumberOfUnitsWithin();
            var actualUnits2 = reportingPeriod2.NumberOfUnitsWithin();
            var actualUnits3 = reportingPeriod3.NumberOfUnitsWithin();

            // Assert
            actualUnits1.Should().Be(1);
            actualUnits2.Should().Be(2);
            actualUnits3.Should().Be(12);
        }

        [Fact]
        public static void NumberOfUnitsWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_CalendarQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q2));
            var reportingPeriod2 = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q4));
            var reportingPeriod3 = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2017, QuarterNumber.Q3));

            // Act
            var actualUnits1 = reportingPeriod1.NumberOfUnitsWithin();
            var actualUnits2 = reportingPeriod2.NumberOfUnitsWithin();
            var actualUnits3 = reportingPeriod3.NumberOfUnitsWithin();

            // Assert
            actualUnits1.Should().Be(1);
            actualUnits2.Should().Be(3);
            actualUnits3.Should().Be(6);
        }

        [Fact]
        public static void NumberOfUnitsWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_FiscalQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q2));
            var reportingPeriod2 = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q4));
            var reportingPeriod3 = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2017, QuarterNumber.Q3));

            // Act
            var actualUnits1 = reportingPeriod1.NumberOfUnitsWithin();
            var actualUnits2 = reportingPeriod2.NumberOfUnitsWithin();
            var actualUnits3 = reportingPeriod3.NumberOfUnitsWithin();

            // Assert
            actualUnits1.Should().Be(1);
            actualUnits2.Should().Be(3);
            actualUnits3.Should().Be(6);
        }

        [Fact]
        public static void NumberOfUnitsWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_GenericQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q2));
            var reportingPeriod2 = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q4));
            var reportingPeriod3 = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2017, QuarterNumber.Q3));

            // Act
            var actualUnits1 = reportingPeriod1.NumberOfUnitsWithin();
            var actualUnits2 = reportingPeriod2.NumberOfUnitsWithin();
            var actualUnits3 = reportingPeriod3.NumberOfUnitsWithin();

            // Assert
            actualUnits1.Should().Be(1);
            actualUnits2.Should().Be(3);
            actualUnits3.Should().Be(6);
        }

        [Fact]
        public static void NumberOfUnitsWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_CalendarYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2016));
            var reportingPeriod2 = new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2017));
            var reportingPeriod3 = new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2018));

            // Act
            var actualUnits1 = reportingPeriod1.NumberOfUnitsWithin();
            var actualUnits2 = reportingPeriod2.NumberOfUnitsWithin();
            var actualUnits3 = reportingPeriod3.NumberOfUnitsWithin();

            // Assert
            actualUnits1.Should().Be(1);
            actualUnits2.Should().Be(2);
            actualUnits3.Should().Be(3);
        }

        [Fact]
        public static void NumberOfUnitsWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_FiscalYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2016));
            var reportingPeriod2 = new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2017));
            var reportingPeriod3 = new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2018));

            // Act
            var actualUnits1 = reportingPeriod1.NumberOfUnitsWithin();
            var actualUnits2 = reportingPeriod2.NumberOfUnitsWithin();
            var actualUnits3 = reportingPeriod3.NumberOfUnitsWithin();

            // Assert
            actualUnits1.Should().Be(1);
            actualUnits2.Should().Be(2);
            actualUnits3.Should().Be(3);
        }

        [Fact]
        public static void NumberOfUnitsWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_GenericYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2016));
            var reportingPeriod2 = new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2017));
            var reportingPeriod3 = new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2018));

            // Act
            var actualUnits1 = reportingPeriod1.NumberOfUnitsWithin();
            var actualUnits2 = reportingPeriod2.NumberOfUnitsWithin();
            var actualUnits3 = reportingPeriod3.NumberOfUnitsWithin();

            // Assert
            actualUnits1.Should().Be(1);
            actualUnits2.Should().Be(2);
            actualUnits3.Should().Be(3);
        }

        [Fact]
        public static void GetUnitsWithin___Should_throw_ArgumentNullException___When_parameter_reportingPeriod_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.GetUnitsWithin<UnitOfTime>(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void GetUnitsWithin___Should_throw_ArgumentException___When_parameter_reportingPeriod_Start_and_or_End_is_unbounded()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarUnitOfTime>(new CalendarUnbounded(), A.Dummy<CalendarUnitOfTime>());
            var reportingPeriod2 = new ReportingPeriod<GenericUnitOfTime>(A.Dummy<GenericUnitOfTime>(), new GenericUnbounded());
            var reportingPeriod3 = new ReportingPeriod<FiscalUnitOfTime>(new FiscalUnbounded(), new FiscalUnbounded());

            // Act
            var ex1 = Record.Exception(() => reportingPeriod1.GetUnitsWithin());
            var ex2 = Record.Exception(() => reportingPeriod2.GetUnitsWithin());
            var ex3 = Record.Exception(() => reportingPeriod3.GetUnitsWithin());

            // Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex2.Should().BeOfType<ArgumentException>();
            ex3.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void GetUnitsWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_CalendarDay()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight));
            var reportingPeriod2 = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Three));

            // Act
            var actualUnits1 = reportingPeriod1.GetUnitsWithin<UnitOfTime>();
            var actualUnits2 = reportingPeriod2.GetUnitsWithin<UnitOfTime>();

            // Assert
            actualUnits1.Should().Equal(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight));
            actualUnits2.Should().Equal(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Two), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Three));
        }

        [Fact]
        public static void GetUnitsWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_CalendarMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.February));
            var reportingPeriod2 = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.April));
            var reportingPeriod3 = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2017, MonthOfYear.January));

            // Act
            var actualUnits1 = reportingPeriod1.GetUnitsWithin<UnitOfTime>();
            var actualUnits2 = reportingPeriod2.GetUnitsWithin<UnitOfTime>();
            var actualUnits3 = reportingPeriod3.GetUnitsWithin<UnitOfTime>();

            // Assert
            actualUnits1.Should().Equal(new CalendarMonth(2016, MonthOfYear.February));
            actualUnits2.Should().Equal(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.March), new CalendarMonth(2016, MonthOfYear.April));
            actualUnits3.Should().Equal(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.March), new CalendarMonth(2016, MonthOfYear.April), new CalendarMonth(2016, MonthOfYear.May), new CalendarMonth(2016, MonthOfYear.June), new CalendarMonth(2016, MonthOfYear.July), new CalendarMonth(2016, MonthOfYear.August), new CalendarMonth(2016, MonthOfYear.September), new CalendarMonth(2016, MonthOfYear.October), new CalendarMonth(2016, MonthOfYear.November), new CalendarMonth(2016, MonthOfYear.December), new CalendarMonth(2017, MonthOfYear.January));
        }

        [Fact]
        public static void GetUnitsWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_FiscalMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Two));
            var reportingPeriod2 = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Three));
            var reportingPeriod3 = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2017, MonthNumber.One));

            // Act
            var actualUnits1 = reportingPeriod1.GetUnitsWithin<UnitOfTime>();
            var actualUnits2 = reportingPeriod2.GetUnitsWithin<UnitOfTime>();
            var actualUnits3 = reportingPeriod3.GetUnitsWithin<UnitOfTime>();

            // Assert
            actualUnits1.Should().Equal(new FiscalMonth(2016, MonthNumber.Two));
            actualUnits2.Should().Equal(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Three));
            actualUnits3.Should().Equal(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Three), new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2016, MonthNumber.Five), new FiscalMonth(2016, MonthNumber.Six), new FiscalMonth(2016, MonthNumber.Seven), new FiscalMonth(2016, MonthNumber.Eight), new FiscalMonth(2016, MonthNumber.Nine), new FiscalMonth(2016, MonthNumber.Ten), new FiscalMonth(2016, MonthNumber.Eleven), new FiscalMonth(2016, MonthNumber.Twelve), new FiscalMonth(2017, MonthNumber.One));
        }

        [Fact]
        public static void GetUnitsWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_GenericMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Two));
            var reportingPeriod2 = new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Three));
            var reportingPeriod3 = new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2017, MonthNumber.One));

            // Act
            var actualUnits1 = reportingPeriod1.GetUnitsWithin<UnitOfTime>();
            var actualUnits2 = reportingPeriod2.GetUnitsWithin<UnitOfTime>();
            var actualUnits3 = reportingPeriod3.GetUnitsWithin<UnitOfTime>();

            // Assert
            actualUnits1.Should().Equal(new GenericMonth(2016, MonthNumber.Two));
            actualUnits2.Should().Equal(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Three));
            actualUnits3.Should().Equal(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Three), new GenericMonth(2016, MonthNumber.Four), new GenericMonth(2016, MonthNumber.Five), new GenericMonth(2016, MonthNumber.Six), new GenericMonth(2016, MonthNumber.Seven), new GenericMonth(2016, MonthNumber.Eight), new GenericMonth(2016, MonthNumber.Nine), new GenericMonth(2016, MonthNumber.Ten), new GenericMonth(2016, MonthNumber.Eleven), new GenericMonth(2016, MonthNumber.Twelve), new GenericMonth(2017, MonthNumber.One));
        }

        [Fact]
        public static void GetUnitsWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_CalendarQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q2));
            var reportingPeriod2 = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q4));
            var reportingPeriod3 = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2017, QuarterNumber.Q3));

            // Act
            var actualUnits1 = reportingPeriod1.GetUnitsWithin<UnitOfTime>();
            var actualUnits2 = reportingPeriod2.GetUnitsWithin<UnitOfTime>();
            var actualUnits3 = reportingPeriod3.GetUnitsWithin<UnitOfTime>();

            // Assert
            actualUnits1.Should().Equal(new CalendarQuarter(2016, QuarterNumber.Q2));
            actualUnits2.Should().Equal(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q3), new CalendarQuarter(2016, QuarterNumber.Q4));
            actualUnits3.Should().Equal(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q3), new CalendarQuarter(2016, QuarterNumber.Q4), new CalendarQuarter(2017, QuarterNumber.Q1), new CalendarQuarter(2017, QuarterNumber.Q2), new CalendarQuarter(2017, QuarterNumber.Q3));
        }

        [Fact]
        public static void GetUnitsWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_FiscalQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q2));
            var reportingPeriod2 = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q4));
            var reportingPeriod3 = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2017, QuarterNumber.Q3));

            // Act
            var actualUnits1 = reportingPeriod1.GetUnitsWithin<UnitOfTime>();
            var actualUnits2 = reportingPeriod2.GetUnitsWithin<UnitOfTime>();
            var actualUnits3 = reportingPeriod3.GetUnitsWithin<UnitOfTime>();

            // Assert
            actualUnits1.Should().Equal(new FiscalQuarter(2016, QuarterNumber.Q2));
            actualUnits2.Should().Equal(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q3), new FiscalQuarter(2016, QuarterNumber.Q4));
            actualUnits3.Should().Equal(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q3), new FiscalQuarter(2016, QuarterNumber.Q4), new FiscalQuarter(2017, QuarterNumber.Q1), new FiscalQuarter(2017, QuarterNumber.Q2), new FiscalQuarter(2017, QuarterNumber.Q3));
        }

        [Fact]
        public static void GetUnitsWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_GenericQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q2));
            var reportingPeriod2 = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q4));
            var reportingPeriod3 = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2017, QuarterNumber.Q3));

            // Act
            var actualUnits1 = reportingPeriod1.GetUnitsWithin<UnitOfTime>();
            var actualUnits2 = reportingPeriod2.GetUnitsWithin<UnitOfTime>();
            var actualUnits3 = reportingPeriod3.GetUnitsWithin<UnitOfTime>();

            // Assert
            actualUnits1.Should().Equal(new GenericQuarter(2016, QuarterNumber.Q2));
            actualUnits2.Should().Equal(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q3), new GenericQuarter(2016, QuarterNumber.Q4));
            actualUnits3.Should().Equal(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q3), new GenericQuarter(2016, QuarterNumber.Q4), new GenericQuarter(2017, QuarterNumber.Q1), new GenericQuarter(2017, QuarterNumber.Q2), new GenericQuarter(2017, QuarterNumber.Q3));
        }

        [Fact]
        public static void GetUnitsWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_CalendarYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2016));
            var reportingPeriod2 = new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2017));
            var reportingPeriod3 = new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2018));

            // Act
            var actualUnits1 = reportingPeriod1.GetUnitsWithin<UnitOfTime>();
            var actualUnits2 = reportingPeriod2.GetUnitsWithin<UnitOfTime>();
            var actualUnits3 = reportingPeriod3.GetUnitsWithin<UnitOfTime>();

            // Assert
            actualUnits1.Should().Equal(new CalendarYear(2016));
            actualUnits2.Should().Equal(new CalendarYear(2016), new CalendarYear(2017));
            actualUnits3.Should().Equal(new CalendarYear(2016), new CalendarYear(2017), new CalendarYear(2018));
        }

        [Fact]
        public static void GetUnitsWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_FiscalYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2016));
            var reportingPeriod2 = new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2017));
            var reportingPeriod3 = new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2018));

            // Act
            var actualUnits1 = reportingPeriod1.GetUnitsWithin<UnitOfTime>();
            var actualUnits2 = reportingPeriod2.GetUnitsWithin<UnitOfTime>();
            var actualUnits3 = reportingPeriod3.GetUnitsWithin<UnitOfTime>();

            // Assert
            actualUnits1.Should().Equal(new FiscalYear(2016));
            actualUnits2.Should().Equal(new FiscalYear(2016), new FiscalYear(2017));
            actualUnits3.Should().Equal(new FiscalYear(2016), new FiscalYear(2017), new FiscalYear(2018));
        }

        [Fact]
        public static void GetUnitsWithin___Should_return_number_of_units_contained_within_reportingPeriod___When_called_on_reporting_period_of_GenericYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2016));
            var reportingPeriod2 = new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2017));
            var reportingPeriod3 = new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2018));

            // Act
            var actualUnits1 = reportingPeriod1.GetUnitsWithin<UnitOfTime>();
            var actualUnits2 = reportingPeriod2.GetUnitsWithin<UnitOfTime>();
            var actualUnits3 = reportingPeriod3.GetUnitsWithin<UnitOfTime>();

            // Assert
            actualUnits1.Should().Equal(new GenericYear(2016));
            actualUnits2.Should().Equal(new GenericYear(2016), new GenericYear(2017));
            actualUnits3.Should().Equal(new GenericYear(2016), new GenericYear(2017), new GenericYear(2018));
        }

        [Fact]
        public static void CreatePermutations___Should_throw_ArgumentNullException___When_parameter_reportingPeriod_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.CreatePermutations<UnitOfTime>(null, A.Dummy<PositiveInteger>()));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void CreatePermutations___Should_throw_ArgumentException___When_parameter_reportingPeriod_Start_and_or_End_is_unbounded()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarUnitOfTime>(new CalendarUnbounded(), A.Dummy<CalendarUnitOfTime>());
            var reportingPeriod2 = new ReportingPeriod<GenericUnitOfTime>(A.Dummy<GenericUnitOfTime>(), new GenericUnbounded());
            var reportingPeriod3 = new ReportingPeriod<FiscalUnitOfTime>(new FiscalUnbounded(), new FiscalUnbounded());

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
            var reportingPeriod = A.Dummy<ReportingPeriod<UnitOfTime>>().Whose(_ => _.Start.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded && _.End.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded);

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
            var reportingPeriod1 = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight));
            var reportingPeriod2 = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Three));

            // Act
            var permutations1a = reportingPeriod1.CreatePermutations(1);
            var permutations1b = reportingPeriod1.CreatePermutations(5);

            var permutations2a = reportingPeriod2.CreatePermutations(1);
            var permutations2b = reportingPeriod2.CreatePermutations(3);

            // Assert
            permutations1a.Should().Equal(reportingPeriod1);
            permutations1b.Should().Equal(reportingPeriod1);

            permutations2a.Should().Equal(
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight)),
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine)),
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One)),
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Two), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Two)),
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Three), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Three)));

            permutations2b.Should().Equal(
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight)),
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine)),
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One)),
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine)),
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One)),
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Two)),
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One)),
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Two)),
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Three)),
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Two), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Two)),
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Two), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Three)),
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Three), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Three)));
        }

        [Fact]
        public static void CreatePermutations___Should_return_permutations___When_called_for_reporting_period_of_CalendarMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.February));
            var reportingPeriod2 = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.June));

            // Act
            var permutations1a = reportingPeriod1.CreatePermutations(1);
            var permutations1b = reportingPeriod1.CreatePermutations(5);

            var permutations2a = reportingPeriod2.CreatePermutations(1);
            var permutations2b = reportingPeriod2.CreatePermutations(3);

            // Assert
            permutations1a.Should().Equal(reportingPeriod1);
            permutations1b.Should().Equal(reportingPeriod1);

            permutations2a.Should().Equal(
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.February)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.March), new CalendarMonth(2016, MonthOfYear.March)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.April), new CalendarMonth(2016, MonthOfYear.April)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.May), new CalendarMonth(2016, MonthOfYear.May)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.June), new CalendarMonth(2016, MonthOfYear.June)));

            permutations2b.Should().Equal(
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.February)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.March)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.April)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.March), new CalendarMonth(2016, MonthOfYear.March)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.March), new CalendarMonth(2016, MonthOfYear.April)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.March), new CalendarMonth(2016, MonthOfYear.May)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.April), new CalendarMonth(2016, MonthOfYear.April)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.April), new CalendarMonth(2016, MonthOfYear.May)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.April), new CalendarMonth(2016, MonthOfYear.June)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.May), new CalendarMonth(2016, MonthOfYear.May)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.May), new CalendarMonth(2016, MonthOfYear.June)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.June), new CalendarMonth(2016, MonthOfYear.June)));
        }

        [Fact]
        public static void CreatePermutations___Should_return_permutations___When_called_for_reporting_period_of_FiscalMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Two));
            var reportingPeriod2 = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Six));

            // Act
            var permutations1a = reportingPeriod1.CreatePermutations(1);
            var permutations1b = reportingPeriod1.CreatePermutations(5);

            var permutations2a = reportingPeriod2.CreatePermutations(1);
            var permutations2b = reportingPeriod2.CreatePermutations(3);

            // Assert
            permutations1a.Should().Equal(reportingPeriod1);
            permutations1b.Should().Equal(reportingPeriod1);

            permutations2a.Should().Equal(
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Two)),
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Three), new FiscalMonth(2016, MonthNumber.Three)),
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2016, MonthNumber.Four)),
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Five), new FiscalMonth(2016, MonthNumber.Five)),
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Six), new FiscalMonth(2016, MonthNumber.Six)));

            permutations2b.Should().Equal(
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Two)),
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Three)),
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Four)),
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Three), new FiscalMonth(2016, MonthNumber.Three)),
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Three), new FiscalMonth(2016, MonthNumber.Four)),
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Three), new FiscalMonth(2016, MonthNumber.Five)),
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2016, MonthNumber.Four)),
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2016, MonthNumber.Five)),
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2016, MonthNumber.Six)),
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Five), new FiscalMonth(2016, MonthNumber.Five)),
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Five), new FiscalMonth(2016, MonthNumber.Six)),
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Six), new FiscalMonth(2016, MonthNumber.Six)));
        }

        [Fact]
        public static void CreatePermutations___Should_return_permutations___When_called_for_reporting_period_of_GenericMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Two));
            var reportingPeriod2 = new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Six));

            // Act
            var permutations1a = reportingPeriod1.CreatePermutations(1);
            var permutations1b = reportingPeriod1.CreatePermutations(5);

            var permutations2a = reportingPeriod2.CreatePermutations(1);
            var permutations2b = reportingPeriod2.CreatePermutations(3);

            // Assert
            permutations1a.Should().Equal(reportingPeriod1);
            permutations1b.Should().Equal(reportingPeriod1);

            permutations2a.Should().Equal(
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Two)),
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Three), new GenericMonth(2016, MonthNumber.Three)),
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Four), new GenericMonth(2016, MonthNumber.Four)),
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Five), new GenericMonth(2016, MonthNumber.Five)),
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Six), new GenericMonth(2016, MonthNumber.Six)));

            permutations2b.Should().Equal(
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Two)),
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Three)),
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Four)),
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Three), new GenericMonth(2016, MonthNumber.Three)),
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Three), new GenericMonth(2016, MonthNumber.Four)),
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Three), new GenericMonth(2016, MonthNumber.Five)),
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Four), new GenericMonth(2016, MonthNumber.Four)),
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Four), new GenericMonth(2016, MonthNumber.Five)),
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Four), new GenericMonth(2016, MonthNumber.Six)),
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Five), new GenericMonth(2016, MonthNumber.Five)),
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Five), new GenericMonth(2016, MonthNumber.Six)),
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Six), new GenericMonth(2016, MonthNumber.Six)));
        }

        [Fact]
        public static void CreatePermutations___Should_return_permutations___When_called_for_reporting_period_of_CalendarQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q2));
            var reportingPeriod2 = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2017, QuarterNumber.Q2));

            // Act
            var permutations1a = reportingPeriod1.CreatePermutations(1);
            var permutations1b = reportingPeriod1.CreatePermutations(5);

            var permutations2a = reportingPeriod2.CreatePermutations(1);
            var permutations2b = reportingPeriod2.CreatePermutations(3);

            // Assert
            permutations1a.Should().Equal(reportingPeriod1);
            permutations1b.Should().Equal(reportingPeriod1);

            permutations2a.Should().Equal(
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q2)),
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q3), new CalendarQuarter(2016, QuarterNumber.Q3)),
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q4), new CalendarQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2017, QuarterNumber.Q1), new CalendarQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2017, QuarterNumber.Q2), new CalendarQuarter(2017, QuarterNumber.Q2)));

            permutations2b.Should().Equal(
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q2)),
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q3)),
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q3), new CalendarQuarter(2016, QuarterNumber.Q3)),
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q3), new CalendarQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q3), new CalendarQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q4), new CalendarQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q4), new CalendarQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q4), new CalendarQuarter(2017, QuarterNumber.Q2)),
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2017, QuarterNumber.Q1), new CalendarQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2017, QuarterNumber.Q1), new CalendarQuarter(2017, QuarterNumber.Q2)),
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2017, QuarterNumber.Q2), new CalendarQuarter(2017, QuarterNumber.Q2)));
        }

        [Fact]
        public static void CreatePermutations___Should_return_permutations___When_called_for_reporting_period_of_FiscalQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q2));
            var reportingPeriod2 = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2017, QuarterNumber.Q2));

            // Act
            var permutations1a = reportingPeriod1.CreatePermutations(1);
            var permutations1b = reportingPeriod1.CreatePermutations(5);

            var permutations2a = reportingPeriod2.CreatePermutations(1);
            var permutations2b = reportingPeriod2.CreatePermutations(3);

            // Assert
            permutations1a.Should().Equal(reportingPeriod1);
            permutations1b.Should().Equal(reportingPeriod1);

            permutations2a.Should().Equal(
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q2)),
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q3), new FiscalQuarter(2016, QuarterNumber.Q3)),
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q4), new FiscalQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2017, QuarterNumber.Q1), new FiscalQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2017, QuarterNumber.Q2), new FiscalQuarter(2017, QuarterNumber.Q2)));

            permutations2b.Should().Equal(
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q2)),
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q3)),
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q3), new FiscalQuarter(2016, QuarterNumber.Q3)),
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q3), new FiscalQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q3), new FiscalQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q4), new FiscalQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q4), new FiscalQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q4), new FiscalQuarter(2017, QuarterNumber.Q2)),
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2017, QuarterNumber.Q1), new FiscalQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2017, QuarterNumber.Q1), new FiscalQuarter(2017, QuarterNumber.Q2)),
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2017, QuarterNumber.Q2), new FiscalQuarter(2017, QuarterNumber.Q2)));
        }

        [Fact]
        public static void CreatePermutations___Should_return_permutations___When_called_for_reporting_period_of_GenericQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q2));
            var reportingPeriod2 = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2017, QuarterNumber.Q2));

            // Act
            var permutations1a = reportingPeriod1.CreatePermutations(1);
            var permutations1b = reportingPeriod1.CreatePermutations(5);

            var permutations2a = reportingPeriod2.CreatePermutations(1);
            var permutations2b = reportingPeriod2.CreatePermutations(3);

            // Assert
            permutations1a.Should().Equal(reportingPeriod1);
            permutations1b.Should().Equal(reportingPeriod1);

            permutations2a.Should().Equal(
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q2)),
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q3), new GenericQuarter(2016, QuarterNumber.Q3)),
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q4), new GenericQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2017, QuarterNumber.Q1), new GenericQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2017, QuarterNumber.Q2), new GenericQuarter(2017, QuarterNumber.Q2)));

            permutations2b.Should().Equal(
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q2)),
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q3)),
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q3), new GenericQuarter(2016, QuarterNumber.Q3)),
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q3), new GenericQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q3), new GenericQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q4), new GenericQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q4), new GenericQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q4), new GenericQuarter(2017, QuarterNumber.Q2)),
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2017, QuarterNumber.Q1), new GenericQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2017, QuarterNumber.Q1), new GenericQuarter(2017, QuarterNumber.Q2)),
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2017, QuarterNumber.Q2), new GenericQuarter(2017, QuarterNumber.Q2)));
        }

        [Fact]
        public static void CreatePermutations___Should_return_permutations___When_called_for_reporting_period_of_CalendarYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2016));
            var reportingPeriod2 = new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2019));

            // Act
            var permutations1a = reportingPeriod1.CreatePermutations(1);
            var permutations1b = reportingPeriod1.CreatePermutations(5);

            var permutations2a = reportingPeriod2.CreatePermutations(1);
            var permutations2b = reportingPeriod2.CreatePermutations(3);

            // Assert
            permutations1a.Should().Equal(reportingPeriod1);
            permutations1b.Should().Equal(reportingPeriod1);

            permutations2a.Should().Equal(
                new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2016)),
                new ReportingPeriod<CalendarYear>(new CalendarYear(2017), new CalendarYear(2017)),
                new ReportingPeriod<CalendarYear>(new CalendarYear(2018), new CalendarYear(2018)),
                new ReportingPeriod<CalendarYear>(new CalendarYear(2019), new CalendarYear(2019)));

            permutations2b.Should().Equal(
                new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2016)),
                new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2017)),
                new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2018)),
                new ReportingPeriod<CalendarYear>(new CalendarYear(2017), new CalendarYear(2017)),
                new ReportingPeriod<CalendarYear>(new CalendarYear(2017), new CalendarYear(2018)),
                new ReportingPeriod<CalendarYear>(new CalendarYear(2017), new CalendarYear(2019)),
                new ReportingPeriod<CalendarYear>(new CalendarYear(2018), new CalendarYear(2018)),
                new ReportingPeriod<CalendarYear>(new CalendarYear(2018), new CalendarYear(2019)),
                new ReportingPeriod<CalendarYear>(new CalendarYear(2019), new CalendarYear(2019)));
        }

        [Fact]
        public static void CreatePermutations___Should_return_permutations___When_called_for_reporting_period_of_FiscalYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2016));
            var reportingPeriod2 = new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2019));

            // Act
            var permutations1a = reportingPeriod1.CreatePermutations(1);
            var permutations1b = reportingPeriod1.CreatePermutations(5);

            var permutations2a = reportingPeriod2.CreatePermutations(1);
            var permutations2b = reportingPeriod2.CreatePermutations(3);

            // Assert
            permutations1a.Should().Equal(reportingPeriod1);
            permutations1b.Should().Equal(reportingPeriod1);

            permutations2a.Should().Equal(
                new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2016)),
                new ReportingPeriod<FiscalYear>(new FiscalYear(2017), new FiscalYear(2017)),
                new ReportingPeriod<FiscalYear>(new FiscalYear(2018), new FiscalYear(2018)),
                new ReportingPeriod<FiscalYear>(new FiscalYear(2019), new FiscalYear(2019)));

            permutations2b.Should().Equal(
                new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2016)),
                new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2017)),
                new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2018)),
                new ReportingPeriod<FiscalYear>(new FiscalYear(2017), new FiscalYear(2017)),
                new ReportingPeriod<FiscalYear>(new FiscalYear(2017), new FiscalYear(2018)),
                new ReportingPeriod<FiscalYear>(new FiscalYear(2017), new FiscalYear(2019)),
                new ReportingPeriod<FiscalYear>(new FiscalYear(2018), new FiscalYear(2018)),
                new ReportingPeriod<FiscalYear>(new FiscalYear(2018), new FiscalYear(2019)),
                new ReportingPeriod<FiscalYear>(new FiscalYear(2019), new FiscalYear(2019)));
        }

        [Fact]
        public static void CreatePermutations___Should_return_permutations___When_called_for_reporting_period_of_GenericYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2016));
            var reportingPeriod2 = new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2019));

            // Act
            var permutations1a = reportingPeriod1.CreatePermutations(1);
            var permutations1b = reportingPeriod1.CreatePermutations(5);

            var permutations2a = reportingPeriod2.CreatePermutations(1);
            var permutations2b = reportingPeriod2.CreatePermutations(3);

            // Assert
            permutations1a.Should().Equal(reportingPeriod1);
            permutations1b.Should().Equal(reportingPeriod1);

            permutations2a.Should().Equal(
                new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2016)),
                new ReportingPeriod<GenericYear>(new GenericYear(2017), new GenericYear(2017)),
                new ReportingPeriod<GenericYear>(new GenericYear(2018), new GenericYear(2018)),
                new ReportingPeriod<GenericYear>(new GenericYear(2019), new GenericYear(2019)));

            permutations2b.Should().Equal(
                new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2016)),
                new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2017)),
                new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2018)),
                new ReportingPeriod<GenericYear>(new GenericYear(2017), new GenericYear(2017)),
                new ReportingPeriod<GenericYear>(new GenericYear(2017), new GenericYear(2018)),
                new ReportingPeriod<GenericYear>(new GenericYear(2017), new GenericYear(2019)),
                new ReportingPeriod<GenericYear>(new GenericYear(2018), new GenericYear(2018)),
                new ReportingPeriod<GenericYear>(new GenericYear(2018), new GenericYear(2019)),
                new ReportingPeriod<GenericYear>(new GenericYear(2019), new GenericYear(2019)));
        }

        [Fact]
        public static void SerializeToString__Should_throw_ArgumentNullException___When_parameter_reportingPeriod_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.SerializeToString(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Need to test various flavors of unit-of-time.")]
        public static void SerializeToString__Should_return_expected_serialized_string_representation_of_reportingPeriod___When_reportingPeriod_is_a_IReportingPeriod()
        {
            // Arrange
            var reportingPeriods = new Dictionary<string, IReportingPeriod<UnitOfTime>>
            {
                { "c-2017-05-17,c-2018-12-09", new ReportingPeriod<CalendarDay>(new CalendarDay(2017, MonthOfYear.May, DayOfMonth.Seventeen), new CalendarDay(2018, MonthOfYear.December, DayOfMonth.Nine)) },
                { "c-2017-05,c-2018-12", new ReportingPeriod<CalendarMonth>(new CalendarMonth(2017, MonthOfYear.May), new CalendarMonth(2018, MonthOfYear.December)) },
                { "f-2017-05,f-2018-12", new ReportingPeriod<FiscalMonth>(new FiscalMonth(2017, MonthNumber.Five), new FiscalMonth(2018, MonthNumber.Twelve)) },
                { "g-2017-05,g-2018-12", new ReportingPeriod<GenericMonth>(new GenericMonth(2017, MonthNumber.Five), new GenericMonth(2018, MonthNumber.Twelve)) },
                { "c-2017-Q2,c-2018-Q4", new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2017, QuarterNumber.Q2), new CalendarQuarter(2018, QuarterNumber.Q4)) },
                { "f-2017-Q2,f-2018-Q4", new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2017, QuarterNumber.Q2), new FiscalQuarter(2018, QuarterNumber.Q4)) },
                { "g-2017-Q2,g-2018-Q4", new ReportingPeriod<GenericQuarter>(new GenericQuarter(2017, QuarterNumber.Q2), new GenericQuarter(2018, QuarterNumber.Q4)) },
                { "c-2017,c-2018", new ReportingPeriod<CalendarYear>(new CalendarYear(2017), new CalendarYear(2018)) },
                { "f-2017,f-2018", new ReportingPeriod<FiscalYear>(new FiscalYear(2017), new FiscalYear(2018)) },
                { "g-2017,g-2018", new ReportingPeriod<GenericYear>(new GenericYear(2017), new GenericYear(2018)) },
                { "c-unbounded,c-2012-02", new ReportingPeriod<CalendarUnitOfTime>(new CalendarUnbounded(), new CalendarMonth(2012, MonthOfYear.February)) },
                { "g-2012-02,g-unbounded", new ReportingPeriod<GenericUnitOfTime>(new GenericMonth(2012, MonthNumber.Two), new GenericUnbounded()) },
                { "f-unbounded,f-unbounded", new ReportingPeriod<FiscalUnitOfTime>(new FiscalUnbounded(), new FiscalUnbounded()) }
            };

            // Act
            var serialized = reportingPeriods.Select(_ => new { Actual = _.Value.SerializeToString(), Expected = _.Key }).ToList();

            // Assert
            serialized.ForEach(_ => _.Actual.Should().Be(_.Expected));
        }

        [Fact]
        public static void DeserializeFromString___Should_throw_ArgumentNullException___When_parameter_unitOfTime_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.DeserializeFromString<IReportingPeriod<UnitOfTime>>(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void DeserializeFromString___Should_throw_ArgumentException___When_parameter_unitOfTime_is_whitespace()
        {
            // Arrange
            var reportingPeriods = new[] { string.Empty, "  ", "  \r\n " };

            // Act
            var exceptions = reportingPeriods.Select(_ => Record.Exception(() => _.DeserializeFromString<IReportingPeriod<UnitOfTime>>())).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<ArgumentException>());
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Thoroughly checking this test-case requires lots of types.")]
        public static void DeserializeFromString___Should_throw_InvalidOperationException___When_the_return_type_cannot_be_assigned_to_a_ReportingPeriod()
        {
            // Arrange
            var allTypes = new[]
            {
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<UnitOfTime>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<UnitOfTime>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<CalendarUnitOfTime>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<CalendarUnitOfTime>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<FiscalUnitOfTime>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<FiscalUnitOfTime>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<GenericUnitOfTime>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<GenericUnitOfTime>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<CalendarDay>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<CalendarDay>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<CalendarMonth>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<CalendarMonth>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<CalendarQuarter>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<CalendarQuarter>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<CalendarYear>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<CalendarYear>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<CalendarUnbounded>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<CalendarUnbounded>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<FiscalMonth>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<FiscalMonth>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<FiscalQuarter>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<FiscalQuarter>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<FiscalYear>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<FiscalYear>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<FiscalUnbounded>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<FiscalUnbounded>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<GenericMonth>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<GenericMonth>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<GenericQuarter>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<GenericQuarter>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<GenericYear>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<GenericYear>) },
                new { ReportingPeriodType = typeof(Common.ReportingPeriodTest<GenericUnbounded>) },
                new { ReportingPeriodType = typeof(Common.IReportingPeriodTest<GenericUnbounded>) }
            };

            var reportingPeriods = new[]
            {
                "c-2015-11-11,c-2016-11-11",
                "c-2017-03,c-2017-04",
                "f-2017-03,f-2017-04",
                "g-2017-03,g-2017-04",
                "c-2017-Q3,c-2017-Q4",
                "f-2017-Q3,f-2017-Q4",
                "g-2017-Q3,g-2017-Q4",
                "c-2017,c-2018",
                "f-2017,f-2018",
                "g-2017,g-2018",
                "c-unbounded,c-unbounded",
                "c-unbounded,c-2018",
                "c-2017-Q3,c-unbounded",
                "f-unbounded,f-unbounded",
                "f-unbounded,f-2018",
                "f-2017-Q3,f-unbounded",
                "g-unbounded,g-unbounded",
                "g-unbounded,g-2018",
                "g-2017-Q3,g-unbounded"
            };

            var deserializeFromString = typeof(ReportingPeriodExtensions).GetMethod(nameof(ReportingPeriodExtensions.DeserializeFromString));

            // Act
            var exceptions = new List<Exception>();
            foreach (var reportingPeriod in reportingPeriods)
            {
                foreach (var type in allTypes)
                {
                    var genericMethod = deserializeFromString.MakeGenericMethod(type.ReportingPeriodType);
                    // ReSharper disable PossibleNullReferenceException
                    exceptions.Add(Record.Exception(() => genericMethod.Invoke(null, new object[] { reportingPeriod })).InnerException);
                    // ReSharper restore PossibleNullReferenceException
                }
            }

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Need to test various flavors of unit-of-time.")]
        public static void DeserializeFromString___Should_throw_InvalidOperationException___When_the_kind_of_unit_of_times_encoded_cannot_be_assigned_to_the_return_types_unit_of_time()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new { ReportingPeriod = "c-2015-11-11,c-2016-11-11", ReportingPeriodType = typeof(ReportingPeriod<FiscalMonth>) },
                new { ReportingPeriod = "c-2017-03,c-2017-04", ReportingPeriodType = typeof(ReportingPeriod<FiscalMonth>) },
                new { ReportingPeriod = "f-2017-03,f-2017-04", ReportingPeriodType = typeof(ReportingPeriod<CalendarUnitOfTime>) },
                new { ReportingPeriod = "g-2017-03,g-2017-04", ReportingPeriodType = typeof(ReportingPeriod<GenericYear>) },
                new { ReportingPeriod = "c-2017-Q3,c-2017-Q4", ReportingPeriodType = typeof(ReportingPeriod<FiscalUnitOfTime>) },
                new { ReportingPeriod = "f-2017-Q3,f-2017-Q4", ReportingPeriodType = typeof(ReportingPeriod<GenericQuarter>) },
                new { ReportingPeriod = "g-2017-Q3,g-2017-Q4", ReportingPeriodType = typeof(ReportingPeriod<CalendarDay>) },
                new { ReportingPeriod = "c-2017,c-2018", ReportingPeriodType = typeof(ReportingPeriod<GenericUnitOfTime>) },
                new { ReportingPeriod = "f-2017,f-2018", ReportingPeriodType = typeof(ReportingPeriod<FiscalMonth>) },
                new { ReportingPeriod = "g-2017,g-2018", ReportingPeriodType = typeof(ReportingPeriod<GenericQuarter>) },
                new { ReportingPeriod = "c-2015-11-11,f-2017-04", ReportingPeriodType = typeof(ReportingPeriod<FiscalMonth>) },
                new { ReportingPeriod = "f-2017-04,f-2018", ReportingPeriodType = typeof(ReportingPeriod<FiscalMonth>) },
                new { ReportingPeriod = "c-2017-Q3,f-2017-04", ReportingPeriodType = typeof(ReportingPeriod<CalendarUnitOfTime>) },
                new { ReportingPeriod = "g-2017-03,g-2018", ReportingPeriodType = typeof(ReportingPeriod<GenericYear>) },
                new { ReportingPeriod = "f-2017-Q3,c-2017-Q4", ReportingPeriodType = typeof(ReportingPeriod<FiscalUnitOfTime>) },
                new { ReportingPeriod = "f-2017-Q3,g-2017-Q4", ReportingPeriodType = typeof(ReportingPeriod<GenericQuarter>) },
                new { ReportingPeriod = "c-2015-11-11,g-2017-Q4", ReportingPeriodType = typeof(ReportingPeriod<CalendarDay>) },
                new { ReportingPeriod = "g-2018,c-2018", ReportingPeriodType = typeof(ReportingPeriod<GenericUnitOfTime>) },
                new { ReportingPeriod = "f-2017-04,f-2018", ReportingPeriodType = typeof(ReportingPeriod<FiscalMonth>) },
                new { ReportingPeriod = "g-2017,g-2017-Q4", ReportingPeriodType = typeof(ReportingPeriod<GenericQuarter>) },
                new { ReportingPeriod = "c-unbounded,c-2017-Q4", ReportingPeriodType = typeof(ReportingPeriod<GenericUnitOfTime>) },
                new { ReportingPeriod = "c-2017-Q4,c-unbounded", ReportingPeriodType = typeof(ReportingPeriod<CalendarQuarter>) },
                new { ReportingPeriod = "c-unbounded,c-unbounded", ReportingPeriodType = typeof(ReportingPeriod<CalendarDay>) },
                new { ReportingPeriod = "f-unbounded,f-2017-Q4", ReportingPeriodType = typeof(ReportingPeriod<FiscalQuarter>) },
                new { ReportingPeriod = "f-2017-Q4,f-unbounded", ReportingPeriodType = typeof(ReportingPeriod<CalendarUnitOfTime>) },
                new { ReportingPeriod = "f-unbounded,f-unbounded", ReportingPeriodType = typeof(ReportingPeriod<FiscalYear>) },
                new { ReportingPeriod = "g-unbounded,g-2017-10", ReportingPeriodType = typeof(ReportingPeriod<GenericMonth>) },
                new { ReportingPeriod = "g-2017-10,g-unbounded", ReportingPeriodType = typeof(ReportingPeriod<GenericQuarter>) },
                new { ReportingPeriod = "g-unbounded,g-unbounded", ReportingPeriodType = typeof(ReportingPeriod<CalendarUnitOfTime>) }
            };

            var deserializeFromString = typeof(ReportingPeriodExtensions).GetMethod(nameof(ReportingPeriodExtensions.DeserializeFromString));

            // Act
            var exceptions = new List<Exception>();
            foreach (var reportingPeriod in reportingPeriods)
            {
                var genericMethod = deserializeFromString.MakeGenericMethod(reportingPeriod.ReportingPeriodType);
                // ReSharper disable PossibleNullReferenceException
                exceptions.Add(Record.Exception(() => genericMethod.Invoke(null, new object[] { reportingPeriod.ReportingPeriod })).InnerException);
                // ReSharper restore PossibleNullReferenceException
            }

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromString___Should_throw_InvalidOperationException___When_reportingPeriod_has_the_wrong_number_of_tokens()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                ",",
                "c-2017-04",
                ",c-2017-04",
                "c-2017-04,",
                "c-2017-03,,",
                ",c-2017-03,",
                "c-2017-03,c-2017-04,",
                "c-2017-03,c-2017-04,c-2017-04"
            };

            // Act
            var exceptions = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromString<IReportingPeriod<UnitOfTime>>())).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromString___Should_throw_InvalidOperationException___When_token_representing_start_of_reporting_period_is_malformed()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                "c-201a-11,c-2017-10",
                "c-xxxx-11,c-2017-10",
                "c-10000-11,c-2017-10",
                "c-T001-11,c-2017-10",
                "c-0-11,c-2017-10",
                "c-200-11,c-2017-10",
                "c-0000-11,c-2017-10",
                "c-999-11,c-2017-10",
                "c-2007-1,c-2017-10",
                "c-2007-9,c-2017-10",
                "c-2007-13,c-2017-10",
                "c-2007-99,c-2017-10",
                "c-2007-00,c-2017-10",
                "c-2007-001,c-2017-10",
                "c-2007-012,c-2017-10"
            };

            // Act
            var exceptions = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromString<IReportingPeriod<UnitOfTime>>())).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromString___Should_throw_InvalidOperationException___When_token_representing_end_of_reporting_period_is_malformed()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                "c-2017-04,c-201a-11",
                "c-2017-04,c-xxxx-11",
                "c-2017-04,c-10000-11",
                "c-2017-04,c-T001-11",
                "c-2017-04,c-0-11",
                "c-2017-04,c-200-11",
                "c-2017-04,c-0000-11",
                "c-2017-04,c-999-11",
                "c-2017-04,c-2007-1",
                "c-2017-04,c-2007-9",
                "c-2017-04,c-2007-13",
                "c-2017-04,c-2007-99",
                "c-2017-04,c-2007-00",
                "c-2017-04,c-2007-001",
                "c-2017-04,c-2007-012"
            };

            // Act
            var exceptions = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromString<IReportingPeriod<UnitOfTime>>())).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromString___Should_throw_InvalidOperationException___When_tokens_representing_start_and_end_of_reporting_period_are_bounded_and_do_not_deserialize_into_same_concrete_type()
        {
            // Arrange
            var unitsOfTime1 = new[]
            {
                "c-2017-04,c-2017-04-11",
                "f-2017-Q4,g-2018-Q1",
                "c-2017,f-2018-05"
            };

            var unitsOfTime2 = new[]
            {
                "c-2017-04,c-2017-04-11",
                "c-2017,c-2018-05"
            };

            // Act
            var exceptions1 = unitsOfTime1.Select(_ => Record.Exception(() => _.DeserializeFromString<IReportingPeriod<UnitOfTime>>())).ToList();
            var exceptions2 = unitsOfTime2.Select(_ => Record.Exception(() => _.DeserializeFromString<IReportingPeriod<CalendarUnitOfTime>>())).ToList();

            // Assert
            exceptions1.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
            exceptions2.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromString___Should_throw_InvalidOperationException___When_tokens_representing_start_and_end_of_reporting_period_are_bounded_and_start_is_greater_than_end()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                "c-2017-04,c-2016-04",
                "c-2017-Q3,c-2017-Q2",
                "c-2017-03-04,c-2017-03-01",
                "f-2017,f-2016"
            };

            // Act
            var exceptions = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromString<IReportingPeriod<UnitOfTime>>())).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void DeserializeFromString___Should_throw_InvalidOperationException___When_one_or_both_tokens_representing_start_and_end_of_reporting_period_are_unbounded_and_are_different_kinds_of_unit_of_time()
        {
            // Arrange
            var unitsOfTime = new[]
            {
                "g-unbounded,c-2016-04",
                "c-2017-Q3,g-unbounded",
                "c-unbounded,g-unbounded",
                "f-unbounded,c-unbounded"
            };

            // Act
            var exceptions1 = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromString<IReportingPeriod<UnitOfTime>>())).ToList();
            var exceptions2 = unitsOfTime.Select(_ => Record.Exception(() => _.DeserializeFromString<IReportingPeriod<CalendarUnitOfTime>>())).ToList();

            // Assert
            exceptions1.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
            exceptions2.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_CalendarDay_string()
        {
            // Arrange
            var reportingPeriod = "c-2001-01-10,c-2016-02-29";
            var expected = new ReportingPeriod<CalendarDay>(new CalendarDay(2001, MonthOfYear.January, DayOfMonth.Ten), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine));

            // Act
            var deserialized1 = reportingPeriod.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized2 = reportingPeriod.DeserializeFromString<ReportingPeriod<UnitOfTime>>();

            var deserialized3 = reportingPeriod.DeserializeFromString<IReportingPeriod<CalendarUnitOfTime>>();
            var deserialized4 = reportingPeriod.DeserializeFromString<ReportingPeriod<CalendarUnitOfTime>>();

            var deserialized5 = reportingPeriod.DeserializeFromString<IReportingPeriod<CalendarDay>>();
            var deserialized6 = reportingPeriod.DeserializeFromString<ReportingPeriod<CalendarDay>>();

            // Assert
            deserialized1.Should().Be(expected);
            deserialized2.Should().Be(expected);
            deserialized3.Should().Be(expected);
            deserialized4.Should().Be(expected);
            deserialized5.Should().Be(expected);
            deserialized6.Should().Be(expected);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_CalendarMonth_string()
        {
            // Arrange
            var reportingPeriod = "c-2001-01,c-2001-02";
            var expected = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2001, MonthOfYear.January), new CalendarMonth(2001, MonthOfYear.February));

            // Act
            var deserialized1 = reportingPeriod.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized2 = reportingPeriod.DeserializeFromString<ReportingPeriod<UnitOfTime>>();

            var deserialized3 = reportingPeriod.DeserializeFromString<IReportingPeriod<CalendarUnitOfTime>>();
            var deserialized4 = reportingPeriod.DeserializeFromString<ReportingPeriod<CalendarUnitOfTime>>();

            var deserialized5 = reportingPeriod.DeserializeFromString<IReportingPeriod<CalendarMonth>>();
            var deserialized6 = reportingPeriod.DeserializeFromString<ReportingPeriod<CalendarMonth>>();

            // Assert
            deserialized1.Should().Be(expected);
            deserialized2.Should().Be(expected);
            deserialized3.Should().Be(expected);
            deserialized4.Should().Be(expected);
            deserialized5.Should().Be(expected);
            deserialized6.Should().Be(expected);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_FiscalMonth_string()
        {
            // Arrange
            var reportingPeriod = "f-2001-01,f-2001-02";
            var expected = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2001, MonthNumber.One), new FiscalMonth(2001, MonthNumber.Two));

            // Act
            var deserialized1 = reportingPeriod.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized2 = reportingPeriod.DeserializeFromString<ReportingPeriod<UnitOfTime>>();

            var deserialized3 = reportingPeriod.DeserializeFromString<IReportingPeriod<FiscalUnitOfTime>>();
            var deserialized4 = reportingPeriod.DeserializeFromString<ReportingPeriod<FiscalUnitOfTime>>();

            var deserialized5 = reportingPeriod.DeserializeFromString<IReportingPeriod<FiscalMonth>>();
            var deserialized6 = reportingPeriod.DeserializeFromString<ReportingPeriod<FiscalMonth>>();

            // Assert
            deserialized1.Should().Be(expected);
            deserialized2.Should().Be(expected);
            deserialized3.Should().Be(expected);
            deserialized4.Should().Be(expected);
            deserialized5.Should().Be(expected);
            deserialized6.Should().Be(expected);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_GenericMonth_string()
        {
            // Arrange
            var reportingPeriod = "g-2001-01,g-2001-02";
            var expected = new ReportingPeriod<GenericMonth>(new GenericMonth(2001, MonthNumber.One), new GenericMonth(2001, MonthNumber.Two));

            // Act
            var deserialized1 = reportingPeriod.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized2 = reportingPeriod.DeserializeFromString<ReportingPeriod<UnitOfTime>>();

            var deserialized3 = reportingPeriod.DeserializeFromString<IReportingPeriod<GenericUnitOfTime>>();
            var deserialized4 = reportingPeriod.DeserializeFromString<ReportingPeriod<GenericUnitOfTime>>();

            var deserialized5 = reportingPeriod.DeserializeFromString<IReportingPeriod<GenericMonth>>();
            var deserialized6 = reportingPeriod.DeserializeFromString<ReportingPeriod<GenericMonth>>();

            // Assert
            deserialized1.Should().Be(expected);
            deserialized2.Should().Be(expected);
            deserialized3.Should().Be(expected);
            deserialized4.Should().Be(expected);
            deserialized5.Should().Be(expected);
            deserialized6.Should().Be(expected);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_CalendarQuarter_string()
        {
            // Arrange
            var reportingPeriod = "c-2001-Q1,c-2001-Q2";
            var expected = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2001, QuarterNumber.Q1), new CalendarQuarter(2001, QuarterNumber.Q2));

            // Act
            var deserialized1 = reportingPeriod.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized2 = reportingPeriod.DeserializeFromString<ReportingPeriod<UnitOfTime>>();

            var deserialized3 = reportingPeriod.DeserializeFromString<IReportingPeriod<CalendarUnitOfTime>>();
            var deserialized4 = reportingPeriod.DeserializeFromString<ReportingPeriod<CalendarUnitOfTime>>();

            var deserialized5 = reportingPeriod.DeserializeFromString<IReportingPeriod<CalendarQuarter>>();
            var deserialized6 = reportingPeriod.DeserializeFromString<ReportingPeriod<CalendarQuarter>>();

            // Assert
            deserialized1.Should().Be(expected);
            deserialized2.Should().Be(expected);
            deserialized3.Should().Be(expected);
            deserialized4.Should().Be(expected);
            deserialized5.Should().Be(expected);
            deserialized6.Should().Be(expected);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_FiscalQuarter_string()
        {
            // Arrange
            var reportingPeriod = "f-2001-Q1,f-2001-Q2";
            var expected = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2001, QuarterNumber.Q1), new FiscalQuarter(2001, QuarterNumber.Q2));

            // Act
            var deserialized1 = reportingPeriod.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized2 = reportingPeriod.DeserializeFromString<ReportingPeriod<UnitOfTime>>();

            var deserialized3 = reportingPeriod.DeserializeFromString<IReportingPeriod<FiscalUnitOfTime>>();
            var deserialized4 = reportingPeriod.DeserializeFromString<ReportingPeriod<FiscalUnitOfTime>>();

            var deserialized5 = reportingPeriod.DeserializeFromString<IReportingPeriod<FiscalQuarter>>();
            var deserialized6 = reportingPeriod.DeserializeFromString<ReportingPeriod<FiscalQuarter>>();

            // Assert
            deserialized1.Should().Be(expected);
            deserialized2.Should().Be(expected);
            deserialized3.Should().Be(expected);
            deserialized4.Should().Be(expected);
            deserialized5.Should().Be(expected);
            deserialized6.Should().Be(expected);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_GenericQuarter_string()
        {
            // Arrange
            var reportingPeriod = "g-2001-Q1,g-2001-Q2";
            var expected = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2001, QuarterNumber.Q1), new GenericQuarter(2001, QuarterNumber.Q2));

            // Act
            var deserialized1 = reportingPeriod.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized2 = reportingPeriod.DeserializeFromString<ReportingPeriod<UnitOfTime>>();

            var deserialized3 = reportingPeriod.DeserializeFromString<IReportingPeriod<GenericUnitOfTime>>();
            var deserialized4 = reportingPeriod.DeserializeFromString<ReportingPeriod<GenericUnitOfTime>>();

            var deserialized5 = reportingPeriod.DeserializeFromString<IReportingPeriod<GenericQuarter>>();
            var deserialized6 = reportingPeriod.DeserializeFromString<ReportingPeriod<GenericQuarter>>();

            // Assert
            deserialized1.Should().Be(expected);
            deserialized2.Should().Be(expected);
            deserialized3.Should().Be(expected);
            deserialized4.Should().Be(expected);
            deserialized5.Should().Be(expected);
            deserialized6.Should().Be(expected);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_CalendarYear_string()
        {
            // Arrange
            var reportingPeriod = "c-2001,c-2002";
            var expected = new ReportingPeriod<CalendarYear>(new CalendarYear(2001), new CalendarYear(2002));

            // Act
            var deserialized1 = reportingPeriod.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized2 = reportingPeriod.DeserializeFromString<ReportingPeriod<UnitOfTime>>();

            var deserialized3 = reportingPeriod.DeserializeFromString<IReportingPeriod<CalendarUnitOfTime>>();
            var deserialized4 = reportingPeriod.DeserializeFromString<ReportingPeriod<CalendarUnitOfTime>>();

            var deserialized5 = reportingPeriod.DeserializeFromString<IReportingPeriod<CalendarYear>>();
            var deserialized6 = reportingPeriod.DeserializeFromString<ReportingPeriod<CalendarYear>>();

            // Assert
            deserialized1.Should().Be(expected);
            deserialized2.Should().Be(expected);
            deserialized3.Should().Be(expected);
            deserialized4.Should().Be(expected);
            deserialized5.Should().Be(expected);
            deserialized6.Should().Be(expected);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_FiscalYear_string()
        {
            // Arrange
            var reportingPeriod = "f-2001,f-2002";
            var expected = new ReportingPeriod<FiscalYear>(new FiscalYear(2001), new FiscalYear(2002));

            // Act
            var deserialized1 = reportingPeriod.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized2 = reportingPeriod.DeserializeFromString<ReportingPeriod<UnitOfTime>>();

            var deserialized3 = reportingPeriod.DeserializeFromString<IReportingPeriod<FiscalUnitOfTime>>();
            var deserialized4 = reportingPeriod.DeserializeFromString<ReportingPeriod<FiscalUnitOfTime>>();

            var deserialized5 = reportingPeriod.DeserializeFromString<IReportingPeriod<FiscalYear>>();
            var deserialized6 = reportingPeriod.DeserializeFromString<ReportingPeriod<FiscalYear>>();

            // Assert
            deserialized1.Should().Be(expected);
            deserialized2.Should().Be(expected);
            deserialized3.Should().Be(expected);
            deserialized4.Should().Be(expected);
            deserialized5.Should().Be(expected);
            deserialized6.Should().Be(expected);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_GenericYear_string()
        {
            // Arrange
            var reportingPeriod = "g-2001,g-2002";
            var expected = new ReportingPeriod<GenericYear>(new GenericYear(2001), new GenericYear(2002));

            // Act
            var deserialized1 = reportingPeriod.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized2 = reportingPeriod.DeserializeFromString<ReportingPeriod<UnitOfTime>>();

            var deserialized3 = reportingPeriod.DeserializeFromString<IReportingPeriod<GenericUnitOfTime>>();
            var deserialized4 = reportingPeriod.DeserializeFromString<ReportingPeriod<GenericUnitOfTime>>();

            var deserialized5 = reportingPeriod.DeserializeFromString<IReportingPeriod<GenericYear>>();
            var deserialized6 = reportingPeriod.DeserializeFromString<ReportingPeriod<GenericYear>>();

            // Assert
            deserialized1.Should().Be(expected);
            deserialized2.Should().Be(expected);
            deserialized3.Should().Be(expected);
            deserialized4.Should().Be(expected);
            deserialized5.Should().Be(expected);
            deserialized6.Should().Be(expected);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_CalendarUnbounded_string()
        {
            // Arrange
            var reportingPeriod1 = "c-unbounded,c-2002";
            var reportingPeriod2 = "c-2002,c-unbounded";
            var reportingPeriod3 = "c-unbounded,c-unbounded";

            var expected1 = new ReportingPeriod<CalendarUnitOfTime>(new CalendarUnbounded(), new CalendarYear(2002));
            var expected2 = new ReportingPeriod<CalendarUnitOfTime>(new CalendarYear(2002), new CalendarUnbounded());
            var expected3 = new ReportingPeriod<CalendarUnbounded>(new CalendarUnbounded(), new CalendarUnbounded());

            // Act
            var deserialized1a = reportingPeriod1.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized1b = reportingPeriod1.DeserializeFromString<ReportingPeriod<UnitOfTime>>();
            var deserialized1c = reportingPeriod1.DeserializeFromString<IReportingPeriod<CalendarUnitOfTime>>();
            var deserialized1d = reportingPeriod1.DeserializeFromString<ReportingPeriod<CalendarUnitOfTime>>();

            var deserialized2a = reportingPeriod2.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized2b = reportingPeriod2.DeserializeFromString<ReportingPeriod<UnitOfTime>>();
            var deserialized2c = reportingPeriod2.DeserializeFromString<IReportingPeriod<CalendarUnitOfTime>>();
            var deserialized2d = reportingPeriod2.DeserializeFromString<ReportingPeriod<CalendarUnitOfTime>>();

            var deserialized3a = reportingPeriod3.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized3b = reportingPeriod3.DeserializeFromString<ReportingPeriod<UnitOfTime>>();
            var deserialized3c = reportingPeriod3.DeserializeFromString<IReportingPeriod<CalendarUnitOfTime>>();
            var deserialized3d = reportingPeriod3.DeserializeFromString<ReportingPeriod<CalendarUnitOfTime>>();
            var deserialized3e = reportingPeriod3.DeserializeFromString<IReportingPeriod<CalendarUnbounded>>();
            var deserialized3f = reportingPeriod3.DeserializeFromString<ReportingPeriod<CalendarUnbounded>>();

            // Assert
            deserialized1a.Should().Be(expected1);
            deserialized1b.Should().Be(expected1);
            deserialized1c.Should().Be(expected1);
            deserialized1d.Should().Be(expected1);

            deserialized2a.Should().Be(expected2);
            deserialized2b.Should().Be(expected2);
            deserialized2c.Should().Be(expected2);
            deserialized2d.Should().Be(expected2);

            deserialized3a.Should().Be(expected3);
            deserialized3b.Should().Be(expected3);
            deserialized3c.Should().Be(expected3);
            deserialized3d.Should().Be(expected3);
            deserialized3e.Should().Be(expected3);
            deserialized3f.Should().Be(expected3);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_FiscalUnbounded_string()
        {
            // Arrange
            var reportingPeriod1 = "f-unbounded,f-2002";
            var reportingPeriod2 = "f-2002,f-unbounded";
            var reportingPeriod3 = "f-unbounded,f-unbounded";

            var expected1 = new ReportingPeriod<FiscalUnitOfTime>(new FiscalUnbounded(), new FiscalYear(2002));
            var expected2 = new ReportingPeriod<FiscalUnitOfTime>(new FiscalYear(2002), new FiscalUnbounded());
            var expected3 = new ReportingPeriod<FiscalUnbounded>(new FiscalUnbounded(), new FiscalUnbounded());

            // Act
            var deserialized1a = reportingPeriod1.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized1b = reportingPeriod1.DeserializeFromString<ReportingPeriod<UnitOfTime>>();
            var deserialized1c = reportingPeriod1.DeserializeFromString<IReportingPeriod<FiscalUnitOfTime>>();
            var deserialized1d = reportingPeriod1.DeserializeFromString<ReportingPeriod<FiscalUnitOfTime>>();

            var deserialized2a = reportingPeriod2.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized2b = reportingPeriod2.DeserializeFromString<ReportingPeriod<UnitOfTime>>();
            var deserialized2c = reportingPeriod2.DeserializeFromString<IReportingPeriod<FiscalUnitOfTime>>();
            var deserialized2d = reportingPeriod2.DeserializeFromString<ReportingPeriod<FiscalUnitOfTime>>();

            var deserialized3a = reportingPeriod3.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized3b = reportingPeriod3.DeserializeFromString<ReportingPeriod<UnitOfTime>>();
            var deserialized3c = reportingPeriod3.DeserializeFromString<IReportingPeriod<FiscalUnitOfTime>>();
            var deserialized3d = reportingPeriod3.DeserializeFromString<ReportingPeriod<FiscalUnitOfTime>>();
            var deserialized3e = reportingPeriod3.DeserializeFromString<IReportingPeriod<FiscalUnbounded>>();
            var deserialized3f = reportingPeriod3.DeserializeFromString<ReportingPeriod<FiscalUnbounded>>();

            // Assert
            deserialized1a.Should().Be(expected1);
            deserialized1b.Should().Be(expected1);
            deserialized1c.Should().Be(expected1);
            deserialized1d.Should().Be(expected1);

            deserialized2a.Should().Be(expected2);
            deserialized2b.Should().Be(expected2);
            deserialized2c.Should().Be(expected2);
            deserialized2d.Should().Be(expected2);

            deserialized3a.Should().Be(expected3);
            deserialized3b.Should().Be(expected3);
            deserialized3c.Should().Be(expected3);
            deserialized3d.Should().Be(expected3);
            deserialized3e.Should().Be(expected3);
            deserialized3f.Should().Be(expected3);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Testing this method is inherently complex.")]
        public static void DeserializeFromString___Should_deserialize_into_various_flavors_of_IReportingPeriod___When_reportingPeriod_is_a_well_formed_ReportingPeriod_of_GenericUnbounded_string()
        {
            // Arrange
            var reportingPeriod1 = "g-unbounded,g-2002";
            var reportingPeriod2 = "g-2002,g-unbounded";
            var reportingPeriod3 = "g-unbounded,g-unbounded";

            var expected1 = new ReportingPeriod<GenericUnitOfTime>(new GenericUnbounded(), new GenericYear(2002));
            var expected2 = new ReportingPeriod<GenericUnitOfTime>(new GenericYear(2002), new GenericUnbounded());
            var expected3 = new ReportingPeriod<GenericUnbounded>(new GenericUnbounded(), new GenericUnbounded());

            // Act
            var deserialized1a = reportingPeriod1.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized1b = reportingPeriod1.DeserializeFromString<ReportingPeriod<UnitOfTime>>();
            var deserialized1c = reportingPeriod1.DeserializeFromString<IReportingPeriod<GenericUnitOfTime>>();
            var deserialized1d = reportingPeriod1.DeserializeFromString<ReportingPeriod<GenericUnitOfTime>>();

            var deserialized2a = reportingPeriod2.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized2b = reportingPeriod2.DeserializeFromString<ReportingPeriod<UnitOfTime>>();
            var deserialized2c = reportingPeriod2.DeserializeFromString<IReportingPeriod<GenericUnitOfTime>>();
            var deserialized2d = reportingPeriod2.DeserializeFromString<ReportingPeriod<GenericUnitOfTime>>();

            var deserialized3a = reportingPeriod3.DeserializeFromString<IReportingPeriod<UnitOfTime>>();
            var deserialized3b = reportingPeriod3.DeserializeFromString<ReportingPeriod<UnitOfTime>>();
            var deserialized3c = reportingPeriod3.DeserializeFromString<IReportingPeriod<GenericUnitOfTime>>();
            var deserialized3d = reportingPeriod3.DeserializeFromString<ReportingPeriod<GenericUnitOfTime>>();
            var deserialized3e = reportingPeriod3.DeserializeFromString<IReportingPeriod<GenericUnbounded>>();
            var deserialized3f = reportingPeriod3.DeserializeFromString<ReportingPeriod<GenericUnbounded>>();

            // Assert
            deserialized1a.Should().Be(expected1);
            deserialized1b.Should().Be(expected1);
            deserialized1c.Should().Be(expected1);
            deserialized1d.Should().Be(expected1);

            deserialized2a.Should().Be(expected2);
            deserialized2b.Should().Be(expected2);
            deserialized2c.Should().Be(expected2);
            deserialized2d.Should().Be(expected2);

            deserialized3a.Should().Be(expected3);
            deserialized3b.Should().Be(expected3);
            deserialized3c.Should().Be(expected3);
            deserialized3d.Should().Be(expected3);
            deserialized3e.Should().Be(expected3);
            deserialized3f.Should().Be(expected3);
        }

        [Fact]
        public static void GetUnitOfTimeKind___Should_throw_ArgumentNullException___When_parameter_reportingPeriod_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.GetUnitOfTimeKind(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Need to test all flavors of unit-of-time")]
        public static void GetUnitOfTimeKind___Should_return_the_kind_of_unit_of_time_used_in_the_reporting_period___When_called()
        {
            // Arrange
            var reportingPeriods = new List<Tuple<IReportingPeriod<UnitOfTime>, UnitOfTimeKind>>
            {
                { A.Dummy<IReportingPeriod<CalendarUnitOfTime>>(), UnitOfTimeKind.Calendar },
                { A.Dummy<IReportingPeriod<CalendarDay>>(), UnitOfTimeKind.Calendar },
                { A.Dummy<IReportingPeriod<CalendarMonth>>(), UnitOfTimeKind.Calendar },
                { A.Dummy<IReportingPeriod<CalendarQuarter>>(), UnitOfTimeKind.Calendar },
                { A.Dummy<IReportingPeriod<CalendarYear>>(), UnitOfTimeKind.Calendar },
                { A.Dummy<IReportingPeriod<CalendarUnbounded>>(), UnitOfTimeKind.Calendar },
                { A.Dummy<IReportingPeriod<FiscalUnitOfTime>>(), UnitOfTimeKind.Fiscal },
                { A.Dummy<IReportingPeriod<FiscalMonth>>(), UnitOfTimeKind.Fiscal },
                { A.Dummy<IReportingPeriod<FiscalQuarter>>(), UnitOfTimeKind.Fiscal },
                { A.Dummy<IReportingPeriod<FiscalYear>>(), UnitOfTimeKind.Fiscal },
                { A.Dummy<IReportingPeriod<FiscalUnbounded>>(), UnitOfTimeKind.Fiscal },
                { A.Dummy<IReportingPeriod<GenericUnitOfTime>>(), UnitOfTimeKind.Generic },
                { A.Dummy<IReportingPeriod<GenericMonth>>(), UnitOfTimeKind.Generic },
                { A.Dummy<IReportingPeriod<GenericQuarter>>(), UnitOfTimeKind.Generic },
                { A.Dummy<IReportingPeriod<GenericYear>>(), UnitOfTimeKind.Generic },
                { A.Dummy<IReportingPeriod<GenericUnbounded>>(), UnitOfTimeKind.Generic },
            };

            // Act
            var unitOfTimeKinds = reportingPeriods.Select(_ => new { Actual = _.Item1.GetUnitOfTimeKind(), Expected = _.Item2 }).ToList();

            // Assert
            unitOfTimeKinds.ForEach(_ => _.Actual.Should().Be(_.Expected));
        }

        [Fact]
        public static void GetUnitOfTimeGranularity___Should_throw_ArgumentNullException___When_parameter_reportingPeriod_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.GetUnitOfTimeGranularity(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Need to test all flavors of unit-of-time")]
        public static void GetUnitOfTimeGranularity___Should_throw_ArgumentException___When_reportingPeriod_Start_and_End_has_different_granularity()
        {
            // Arrange
            var reportingPeriods = new IReportingPeriod<UnitOfTime>[]
            {
                A.Dummy<IReportingPeriod<CalendarUnitOfTime>>().Whose(_ => (_.Start.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded) && (_.End.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded)),
                A.Dummy<IReportingPeriod<CalendarUnitOfTime>>().Whose(_ => (_.Start.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded) && (_.End.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded)),
                A.Dummy<IReportingPeriod<FiscalUnitOfTime>>().Whose(_ => (_.Start.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded) && (_.End.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded)),
                A.Dummy<IReportingPeriod<FiscalUnitOfTime>>().Whose(_ => (_.Start.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded) && (_.End.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded)),
                A.Dummy<IReportingPeriod<GenericUnitOfTime>>().Whose(_ => (_.Start.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded) && (_.End.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded)),
                A.Dummy<IReportingPeriod<GenericUnitOfTime>>().Whose(_ => (_.Start.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded) && (_.End.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded)),
            };

            // Act
            var unitOfTimeGranularity = reportingPeriods.Select(_ => Record.Exception(() => _.GetUnitOfTimeGranularity())).ToList();

            // Assert
            unitOfTimeGranularity.ForEach(_ => _.Should().BeOfType<ArgumentException>());
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Need to test all flavors of unit-of-time")]
        public static void GetUnitOfTimeGranularity___Should_return_the_granularity_of_unit_of_time_used_in_the_reporting_period___When_called()
        {
            // Arrange
            var reportingPeriods = new List<Tuple<IReportingPeriod<UnitOfTime>, UnitOfTimeGranularity>>
            {
                { A.Dummy<IReportingPeriod<CalendarDay>>(), UnitOfTimeGranularity.Day },
                { A.Dummy<IReportingPeriod<CalendarMonth>>(), UnitOfTimeGranularity.Month },
                { A.Dummy<IReportingPeriod<CalendarQuarter>>(), UnitOfTimeGranularity.Quarter },
                { A.Dummy<IReportingPeriod<CalendarYear>>(), UnitOfTimeGranularity.Year },
                { A.Dummy<IReportingPeriod<CalendarUnbounded>>(), UnitOfTimeGranularity.Unbounded },
                { A.Dummy<IReportingPeriod<FiscalMonth>>(), UnitOfTimeGranularity.Month },
                { A.Dummy<IReportingPeriod<FiscalQuarter>>(), UnitOfTimeGranularity.Quarter },
                { A.Dummy<IReportingPeriod<FiscalYear>>(), UnitOfTimeGranularity.Year },
                { A.Dummy<IReportingPeriod<FiscalUnbounded>>(), UnitOfTimeGranularity.Unbounded },
                { A.Dummy<IReportingPeriod<GenericMonth>>(), UnitOfTimeGranularity.Month },
                { A.Dummy<IReportingPeriod<GenericQuarter>>(), UnitOfTimeGranularity.Quarter },
                { A.Dummy<IReportingPeriod<GenericYear>>(), UnitOfTimeGranularity.Year },
                { A.Dummy<IReportingPeriod<GenericUnbounded>>(), UnitOfTimeGranularity.Unbounded }
            };

            // Act
            var unitOfTimeGranularity = reportingPeriods.Select(_ => new { Actual = _.Item1.GetUnitOfTimeGranularity(), Expected = _.Item2 }).ToList();

            // Assert
            unitOfTimeGranularity.ForEach(_ => _.Actual.Should().Be(_.Expected));
        }

        [Fact]
        public static void HasComponentWithUnboundedGranularity___Should_throw_ArgumentNullException___When_parameter_reportingPeriod_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.HasComponentWithUnboundedGranularity(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void HasComponentWithUnboundedGranularity___Should_return_false___When_neither_the_Start_nor_End_component_has_an_Unbounded_UnitOfTimeGranularity()
        {
            // Arrange
            var reportingPeriod = A.Dummy<IReportingPeriod<UnitOfTime>>().Whose(_ => (_.Start.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded) && (_.End.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded));

            // Act
            var result = reportingPeriod.HasComponentWithUnboundedGranularity();

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void HasComponentWithUnboundedGranularity___Should_return_true___When_either_the_Start_or_End_or_both_components_has_an_Unbounded_UnitOfTimeGranularity()
        {
            // Arrange
            var reportingPeriod1 = A.Dummy<IReportingPeriod<UnitOfTime>>().Whose(_ => (_.Start.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded) && (_.End.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded));
            var reportingPeriod2 = A.Dummy<IReportingPeriod<UnitOfTime>>().Whose(_ => (_.Start.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded) && (_.End.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded));
            var reportingPeriod3 = A.Dummy<IReportingPeriod<UnitOfTime>>().Whose(_ => (_.Start.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded) && (_.End.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded));

            // Act
            var result1 = reportingPeriod1.HasComponentWithUnboundedGranularity();
            var result2 = reportingPeriod2.HasComponentWithUnboundedGranularity();
            var result3 = reportingPeriod3.HasComponentWithUnboundedGranularity();

            // Assert
            result1.Should().BeTrue();
            result2.Should().BeTrue();
            result3.Should().BeTrue();
        }

        [Fact]
        public static void ToCalendar_with_IReportingPeriod_of_GenericUnitOfTime___Should_throw_ArgumentNullException___When_parameter_genericUnitOfTime_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.ToCalendar(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void ToCalendar_with_IReportingPeriod_of_GenericUnitOfTime___Should_return_the_reportingPeriod_converted_into_an_IReportingPeriod_of_CalendarUnitOfTime____When_called()
        {
            // Arrange
            var toConvert = new List<Tuple<IReportingPeriod<GenericUnitOfTime>, IReportingPeriod<CalendarUnitOfTime>>>
            {
                {
                    new ReportingPeriod<GenericUnitOfTime>(new GenericMonth(2017, MonthNumber.Eleven), new GenericMonth(2017, MonthNumber.Six)),
                    new ReportingPeriod<CalendarUnitOfTime>(new CalendarMonth(2017, MonthOfYear.November), new CalendarMonth(2017, MonthOfYear.June))
                },
                {
                    new ReportingPeriod<GenericUnitOfTime>(new GenericUnbounded(), new GenericMonth(2017, MonthNumber.Six)),
                    new ReportingPeriod<CalendarUnitOfTime>(new CalendarUnbounded(), new CalendarMonth(2017, MonthOfYear.June))
                },
                {
                    new ReportingPeriod<GenericUnitOfTime>(new GenericMonth(2017, MonthNumber.Eleven), new GenericUnbounded()),
                    new ReportingPeriod<CalendarUnitOfTime>(new CalendarMonth(2017, MonthOfYear.November), new CalendarUnbounded())
                },
                {
                    new ReportingPeriod<GenericUnitOfTime>(new GenericQuarter(2017, QuarterNumber.Q2), new GenericQuarter(2018, QuarterNumber.Q4)),
                    new ReportingPeriod<CalendarUnitOfTime>(new CalendarQuarter(2017, QuarterNumber.Q2), new CalendarQuarter(2018, QuarterNumber.Q4))
                },
                {
                    new ReportingPeriod<GenericUnitOfTime>(new GenericUnbounded(), new GenericQuarter(2018, QuarterNumber.Q4)),
                    new ReportingPeriod<CalendarUnitOfTime>(new CalendarUnbounded(), new CalendarQuarter(2018, QuarterNumber.Q4))
                },
                {
                    new ReportingPeriod<GenericUnitOfTime>(new GenericQuarter(2017, QuarterNumber.Q2), new GenericUnbounded()),
                    new ReportingPeriod<CalendarUnitOfTime>(new CalendarQuarter(2017, QuarterNumber.Q2), new CalendarUnbounded())
                },
                {
                    new ReportingPeriod<GenericUnitOfTime>(new GenericYear(2017), new GenericYear(2018)),
                    new ReportingPeriod<CalendarUnitOfTime>(new CalendarYear(2017), new CalendarYear(2018))
                },
                {
                    new ReportingPeriod<GenericUnitOfTime>(new GenericUnbounded(), new GenericYear(2018)),
                    new ReportingPeriod<CalendarUnitOfTime>(new CalendarUnbounded(), new CalendarYear(2018))
                },
                {
                    new ReportingPeriod<GenericUnitOfTime>(new GenericYear(2017), new GenericUnbounded()),
                    new ReportingPeriod<CalendarUnitOfTime>(new CalendarYear(2017), new CalendarUnbounded())
                },
                {
                    new ReportingPeriod<GenericUnitOfTime>(new GenericUnbounded(), new GenericUnbounded()),
                    new ReportingPeriod<CalendarUnitOfTime>(new CalendarUnbounded(), new CalendarUnbounded())
                }
            };

            // Act
            var results = toConvert.Select(_ => new { Actual = _.Item1.ToCalendar(), Expected = _.Item2 }).ToList();

            // Assert
            results.ForEach(_ => _.Actual.Should().Be(_.Expected));
        }

        // ReSharper restore InconsistentNaming
    }
}

// ReSharper restore CheckNamespace