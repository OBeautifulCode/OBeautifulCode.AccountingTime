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
    public static class TypeHelper
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

        /// <summary>
        /// Checks to see if the type provided is a <see cref="UnitOfTime" />.
        /// </summary>
        /// <param name="type">Type to check.</param>
        /// <returns>A value indicating whether or not it's a valid type.</returns>
        public static bool IsUnitOfTimeType(
            this Type type)
        {
            var result = GetAllUnitOfTimeTypes().Contains(type);

            return result;
        }

        /// <summary>
        /// Checks to see if the type provided is a <see cref="ReportingPeriod{T}" /> type.
        /// </summary>
        /// <param name="type">Type to check.</param>
        /// <returns>A value indicating whether or not it's a valid type.</returns>
        public static bool IsReportingPeriodType(
            this Type type)
        {
            var result = GetAllBoundReportingPeriodTypes().Contains(type);

            return result;
        }
    }
}
