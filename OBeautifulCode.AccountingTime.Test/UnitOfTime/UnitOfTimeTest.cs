// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfTimeTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using FakeItEasy;

    using FluentAssertions;

    using Xunit;

    public static class UnitOfTimeTest
    {
        // ReSharper disable InconsistentNaming
        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "This test is inherently complex.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "This test is inherently complex.")]
        public static void Clone___Should_throw_InvalidOperationException___When_cloned_object_cannot_be_casted_to_generic_type_parameter()
        {
            // Arrange
            var unitsOfTime = new Dictionary<UnitOfTime, IEnumerable<Type>>
            {
                { A.Dummy<CalendarDay>(), Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(CalendarDay)) && (_ != typeof(CalendarUnitOfTime))) },
                { A.Dummy<CalendarMonth>(), Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(CalendarMonth)) && (_ != typeof(CalendarUnitOfTime))) },
                { A.Dummy<CalendarQuarter>(), Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(CalendarQuarter)) && (_ != typeof(CalendarUnitOfTime))) },
                { A.Dummy<CalendarYear>(), Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(CalendarYear)) && (_ != typeof(CalendarUnitOfTime))) },
                { A.Dummy<CalendarUnbounded>(),  Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(CalendarUnbounded)) && (_ != typeof(CalendarUnitOfTime))) },
                { A.Dummy<FiscalMonth>(), Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(FiscalMonth)) && (_ != typeof(FiscalUnitOfTime))) },
                { A.Dummy<FiscalQuarter>(), Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(FiscalQuarter)) && (_ != typeof(FiscalUnitOfTime))) },
                { A.Dummy<FiscalYear>(), Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(FiscalYear)) && (_ != typeof(FiscalUnitOfTime))) },
                { A.Dummy<FiscalUnbounded>(),  Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(FiscalUnbounded)) && (_ != typeof(FiscalUnitOfTime))) },
                { A.Dummy<GenericQuarter>(), Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(GenericQuarter)) && (_ != typeof(GenericUnitOfTime))) },
                { A.Dummy<GenericMonth>(), Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(GenericMonth)) && (_ != typeof(GenericUnitOfTime))) },
                { A.Dummy<GenericYear>(),  Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(GenericYear)) && (_ != typeof(GenericUnitOfTime))) },
                { A.Dummy<GenericUnbounded>(),  Common.AllUnitOfTimeTypesExceptUnitOfTime.Where(_ => (_ != typeof(GenericUnbounded)) && (_ != typeof(GenericUnitOfTime))) },
            };

            var cloneMethod = typeof(UnitOfTime).GetMethods().Single(_ => _.Name == nameof(UnitOfTime.Clone) && _.ContainsGenericParameters);

            // Act
            var exceptions = new List<Exception>();
            foreach (var unitOfTime in unitsOfTime.Keys)
            {
                foreach (var type in unitsOfTime[unitOfTime])
                {
                    var genericMethod = cloneMethod.MakeGenericMethod(type);
                    // ReSharper disable PossibleNullReferenceException
                    exceptions.Add(Record.Exception(() => genericMethod.Invoke(unitOfTime, null)).InnerException);
                    // ReSharper restore PossibleNullReferenceException
                }
            }

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "This test is inherently complex.")]
        public static void Clone___Should_return_cloned_object___When_cloned_object_can_be_casted_to_generic_type_parameter()
        {
            // Arrange
            var unitsOfTime = new Dictionary<UnitOfTime, IEnumerable<Type>>
            {
                { A.Dummy<CalendarDay>(), new[] { typeof(CalendarDay), typeof(CalendarUnitOfTime), typeof(UnitOfTime) } },
                { A.Dummy<CalendarMonth>(), new[] { typeof(CalendarMonth), typeof(CalendarUnitOfTime), typeof(UnitOfTime) } },
                { A.Dummy<CalendarQuarter>(), new[] { typeof(CalendarQuarter), typeof(CalendarUnitOfTime), typeof(UnitOfTime) } },
                { A.Dummy<CalendarYear>(), new[] { typeof(CalendarYear), typeof(CalendarUnitOfTime), typeof(UnitOfTime) } },
                { A.Dummy<CalendarUnbounded>(), new[] { typeof(CalendarUnbounded), typeof(CalendarUnitOfTime), typeof(UnitOfTime) } },
                { A.Dummy<FiscalMonth>(), new[] { typeof(FiscalMonth), typeof(FiscalUnitOfTime), typeof(UnitOfTime) } },
                { A.Dummy<FiscalQuarter>(), new[] { typeof(FiscalQuarter), typeof(FiscalUnitOfTime), typeof(UnitOfTime) } },
                { A.Dummy<FiscalYear>(), new[] { typeof(FiscalYear), typeof(FiscalUnitOfTime), typeof(UnitOfTime) } },
                { A.Dummy<FiscalUnbounded>(), new[] { typeof(FiscalUnbounded), typeof(FiscalUnitOfTime), typeof(UnitOfTime) } },
                { A.Dummy<GenericQuarter>(), new[] { typeof(GenericQuarter), typeof(GenericUnitOfTime), typeof(UnitOfTime) } },
                { A.Dummy<GenericMonth>(), new[] { typeof(GenericMonth), typeof(GenericUnitOfTime), typeof(UnitOfTime) } },
                { A.Dummy<GenericYear>(), new[] { typeof(GenericYear), typeof(GenericUnitOfTime), typeof(UnitOfTime) } },
                { A.Dummy<GenericUnbounded>(), new[] { typeof(GenericUnbounded), typeof(GenericUnitOfTime), typeof(UnitOfTime) } }
            };

            var cloneMethod = typeof(UnitOfTime).GetMethods().Single(_ => _.Name == nameof(UnitOfTime.Clone) && _.ContainsGenericParameters);

            // Act, Assert
            foreach (var unitOfTime in unitsOfTime.Keys)
            {
                foreach (var type in unitsOfTime[unitOfTime])
                {
                    var genericMethod = cloneMethod.MakeGenericMethod(type);
                    var result = genericMethod.Invoke(unitOfTime, null);
                    unitOfTime.Should().Be(result);
                    unitOfTime.Should().NotBeSameAs(result);
                }
            }
        }

        // ReSharper restore InconsistentNaming
    }
}

// ReSharper restore CheckNamespace