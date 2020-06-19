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
            var result = typeof(UnitOfTime)
                .Assembly
                .GetTypes()
                .Where(_ => (_ != null) && (_ == typeof(UnitOfTime) || _.IsSubclassOf(typeof(UnitOfTime))))
                .ToList();

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
    }
}
