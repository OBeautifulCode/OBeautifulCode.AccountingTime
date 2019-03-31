// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeHelper.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Provides helper methods related to account time types.
    /// </summary>
    public class TypeHelper
    {
        /// <summary>
        /// Gets all <see cref="UnitOfTime"/> types (that type along with all subclasses).
        /// </summary>
        /// <returns>
        /// All <see cref="UnitOfTime"/> types.
        /// </returns>
        public static IReadOnlyCollection<Type> GetAllUnitOfTimeTypes()
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            var result = typeof(UnitOfTime).Assembly.GetTypes().Where(_ => (_ != null) && (_ == typeof(UnitOfTime) || _.IsSubclassOf(typeof(UnitOfTime)))).ToList();

            return result;
        }

        /// <summary>
        /// Gets all bound <see cref="IReportingPeriod{T}"/> and <see cref="ReportingPeriod{T}"/> types
        /// (e.g. <see cref="IReportingPeriod{CalendarDay}"/>, <see cref="ReportingPeriod{FiscalYear}"/>, ...) .
        /// </summary>
        /// <returns>
        /// All bound reporting period types.
        /// </returns>
        public static IReadOnlyCollection<Type> GetAllBoundReportingPeriodTypes()
        {
            var unitOfTimeTypes = GetAllUnitOfTimeTypes();

            var unboundReportingPeriodTypes = new[] { typeof(IReportingPeriod<>), typeof(ReportingPeriod<>) };

            var result = unboundReportingPeriodTypes.SelectMany(rp => unitOfTimeTypes.Select(ut => rp.MakeGenericType(ut))).ToList();

            return result;
        }
    }
}
